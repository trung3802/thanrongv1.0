﻿using System;
using System.Collections.Generic;
using System.Linq;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.Interfaces.Monster;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.Model;
using TienKiemV2Remastered.Model.Character;
using TienKiemV2Remastered.Model.Item;
using TienKiemV2Remastered.Model.Monster;
using TienKiemV2Remastered.Model.SkillCharacter;
using TienKiemV2Remastered.Model.Template;
using TienKiemV2Remastered.Application.MainTasks;
using TienKiemV2Remastered.Application.Extension.Bosses.Mabu12Gio;
using Org.BouncyCastle.Math.Field;
using Task = System.Threading.Tasks.Task;
using System.Threading;
using TienKiemV2Remastered.Application.Extension;
using TienKiemV2Remastered.Application.Extension.ChampionShip;
using TienKiemV2Remastered.DatabaseManager.Player;
using TienKiemV2Remastered.Application.Extension.ChampionShip.ChampionShip_23;

namespace TienKiemV2Remastered.Application.Handlers.Skill
{
    public static class SkillHandler
    {
        //Player
        public static void SkillNotFocus(ICharacter character, Message message)
        {
            var zone = character.Zone;
            if (zone == null)
            {
                return;
            }
            try
            {
                if (character.InfoChar.Stamina <= 0)
                {
                    character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_STAMINA));
                    return;
                }

                var status = message.Reader.ReadByte();
                //Get skill
                var skillCharFocus = character.Skills.FirstOrDefault(skl => skl.Id == character.InfoChar.CSkill);
                if(skillCharFocus == null) return;
                var skillTemplate = Cache.Gi().SKILL_TEMPLATES.FirstOrDefault(skl => skl.Id == skillCharFocus.Id);
                var skillData = skillTemplate.SkillDataTemplates.FirstOrDefault(skl => skl.SkillId == skillCharFocus.SkillId);
                if (skillData == null) return;
                // SkillDataTemplate skillData = skillDataReal.Clone();
                // Xử lý nội tại
                // skillData = HandleSpecialSkill(character, skillTemplate, skillData);

              //  Server.Gi().Logger.Debug($"Client: {character.Player.Session.Id} ---- use skill not focus --- Skill template: {skillTemplate.Id} --- status: {status}");
                //Check mana
                var manaUse = skillData.ManaUse;
                var manaUseType = skillTemplate.ManaUseType;
                var manaChar = character.InfoChar.Mp;
                manaUse = manaUseType switch
                {
                    1 => manaUse * (int) character.MpFull / 100,
                    2 => (int) manaChar,
                    _ => manaUse
                };

                //Remove Tái Tạo Năng Lượng 
                if (status != 2 && character.InfoSkill.TaiTaoNangLuong.IsTTNL)
                {
                    RemoveTTNL(character, skillTemplate.Id);
                }
                var timeServer = ServerUtils.CurrentTimeMillis();
                switch (status)
                {
                    case 20:
                        {   
                            var idTemplateSkill = message.Reader.ReadByte();
                            var myCharzX = message.Reader.ReadShort();
                            var myCharzY = message.Reader.ReadShort();
                            var dir = message.Reader.ReadByte();
                            var focusX = message.Reader.ReadShort();
                            var focusY = message.Reader.ReadShort();
                            var skill = character.Skills.FirstOrDefault(i => i.Id == idTemplateSkill);
                            if (skill == null || skill.CoolDown > timeServer) return;
                            var pointX = dir == 1 ? myCharzX + (50 * skill.Point) + 200 : myCharzX - (50 * skill.Point) - 200;
                            //id skill == idtemplate skill || skillId = skill.skillId;
                            skillTemplate = Cache.Gi().SKILL_TEMPLATES.FirstOrDefault(skl => skl.Id == skill.Id);
                            skillData = skillTemplate.SkillDataTemplates.FirstOrDefault(skl => skl.SkillId == skill.SkillId);
                            switch (idTemplateSkill)
                            {
                                case 24:
                                    {
                                        pointX = skillData.Dx + (50 * skill.Point) + 100;
                                        var real = (Model.Character.Character)character;
                                        character.CharacterHandler.SendZoneMessage(Service.SkillNotFocus20(character.Id, 24, 1, 2200, 0, (byte)dir)); // 1 là bay dm đẹp vc 
                                        async void Action()
                                        {
                                            await Task.Delay(2200);
                                            character.InfoSkill.SuperKamejoko.time = 4400 + (200 * skill.Point) + timeServer;
                                            character.CharacterHandler.SendZoneMessage(Service.SkillNotFocus21(character.Id, 24, (short)(pointX), (short)(myCharzY), (short)(2000 + (200 * skill.Point)), 0, null, 0));
                                            while (character.InfoSkill.SuperKamejoko.time > timeServer)
                                            {
                                                timeServer = ServerUtils.CurrentTimeMillis();
                                                var damage = (character.DamageFull * (skillData.Damage / 100)) / 2.5;
                                                lock (zone.MonsterMaps)
                                                {
                                                    foreach (var monsterMap in zone.MonsterMaps.Where(m => !m.IsDie && (dir == 1 ? (m.X >= myCharzX && m.X <= pointX) : (m.X >= pointX && m.X <= myCharzX)) && (m.Y >= myCharzY ? Math.Abs(m.Y - myCharzY) <= skillData.Dy : Math.Abs(myCharzY - m.Y) <= skillData.Dy)))
                                                    {
                                                        var damageMonsterAfterHandle = monsterMap.MonsterHandler.UpdateHp((long)damage, character.Id);
                                                        monsterMap.MonsterHandler.AddPlayerAttack(character, damageMonsterAfterHandle);
                                                        character.CharacterHandler.SendZoneMessage(Service.MonsterHp(monsterMap, true, damageMonsterAfterHandle, -1));
                                                        character.CharacterHandler.PlusTiemNang(monsterMap, damageMonsterAfterHandle);
                                                    }
                                                }
                                                lock (zone.Bosses)
                                                {
                                                    foreach (var @boss in zone.Bosses.Values.Where(b => !b.InfoChar.IsDie && (dir == 1 ? (b.InfoChar.X >= myCharzX && b.InfoChar.X <= pointX) : (b.InfoChar.X >= pointX && b.InfoChar.X <= myCharzX)) && (b.InfoChar.Y >= myCharzY ? Math.Abs(b.InfoChar.Y - myCharzY) <= skillData.Dy : Math.Abs(myCharzY - b.InfoChar.Y) <= skillData.Dy)))
                                                    {
                                                        @boss.CharacterHandler.MineHp((long)damage);
                                                        @boss.CharacterHandler.SendMessage(Service.SendHp((int)@boss.InfoChar.Hp));
                                                        @boss.CharacterHandler.SendZoneMessage(Service.PlayerLevel(@boss));
                                                        character.CharacterHandler.SendZoneMessage(Service.HaveAttackPlayer(@boss, true, (long)damage, -1));
                                                        if (boss.InfoChar.IsDie)
                                                        {
                                                            boss.CharacterHandler.SendDie();
                                                            boss.CharacterHandler.LeaveItem(character);
                                                            TaskHandler.gI().CheckTaskDoneKillBoss((Model.Character.Character)character, boss.Type);
                                                        }
                                                    }
                                                }
                                                lock (zone.Characters)
                                                {
                                                    foreach (var @char in zone.Characters.Values.Where(c => !c.InfoChar.IsDie && ((c.InfoChar.TypePk != 0 || c.Flag != 0) && (character.InfoChar.TypePk != 0 || character.Flag != 0)) && (dir == 1 ? (c.InfoChar.X >= myCharzX && c.InfoChar.X <= pointX) : (c.InfoChar.X >= pointX && c.InfoChar.X <= myCharzX)) && (c.InfoChar.Y >= myCharzY ? Math.Abs(c.InfoChar.Y - myCharzY) <= skillData.Dy : Math.Abs(myCharzY - c.InfoChar.Y) <= skillData.Dy) && c.Id != character.Id && c.Id != -character.Id))
                                                    {
                                                        if (damage >= @char.InfoChar.Hp)
                                                        {
                                                            damage = (int)@char.InfoChar.Hp;
                                                        }

                                                        // Bổ Huyết
                                                        if (damage > 0 && @char.InfoBuff.GiapXen)
                                                        {
                                                            damage -= (damage * 50 / 100);
                                                        }
                                                        if (damage > 0 && @char.InfoBuff.GiapXen2)
                                                        {
                                                            damage -= (damage * 60 / 100);
                                                        }
                                                        @char.CharacterHandler.MineHp((long)damage);
                                                        @char.CharacterHandler.SendMessage(Service.SendHp((int)@char.InfoChar.Hp));
                                                        @char.CharacterHandler.SendZoneMessage(Service.PlayerLevel(@char));
                                                        character.CharacterHandler.SendZoneMessage(Service.HaveAttackPlayer(@char, true, (long)damage, -1));
                                                        if (@char.InfoChar.IsDie)
                                                        {
                                                            @char.CharacterHandler.SendDie();
                                                            if (@char.Enemies.FirstOrDefault(cg => cg.Id == character.Id) == null && !real.InfoBuff.AnDanh)
                                                            {
                                                                @char.Enemies.Add(real.Me);
                                                                if (@char.Enemies.Count > 30)
                                                                {
                                                                    @char.Enemies.RemoveAt(0);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                Thread.Sleep(150);
                                            }
                                        }
                                        var task = new Task(Action);
                                        task.Start();
                                        break;
                                    }
                                case 25:
                                    {
                                        pointX = dir == 1 ? Math.Abs(myCharzX + skillData.Dx) + (50 * skill.Point) + 100 : Math.Abs(myCharzX - skillData.Dx) - (50 * skill.Point) - 100;
                                        var real = (Model.Character.Character)character;
                                        character.CharacterHandler.SendZoneMessage(Service.SkillNotFocus20(character.Id, 25, 2, 1000, 0, (byte)dir));
                                        async void Action()
                                        {
                                            await Task.Delay(1000);
                                            character.CharacterHandler.SendZoneMessage(Service.SkillNotFocus21(character.Id, 25, (short)(pointX - 100), myCharzY, 3000, 100, null, 0));
                                            character.InfoSkill.CadicLienHoanChuong.Time = 3000 + timeServer;
                                            while (character.InfoSkill.CadicLienHoanChuong.Time > timeServer)
                                            {
                                                timeServer = ServerUtils.CurrentTimeMillis();
                                                var damage = (character.DamageFull * (skillData.Damage / 100)) / 2.5;
                                                lock (zone.MonsterMaps)
                                                {
                                                    //    foreach (var monsterMap in zone.MonsterMaps.Where(m => !m.IsDie && (dir == 1 ? (m.X >= myCharzX && m.X <= pointX) : (m.X >= pointX && m.X <= myCharzX)) && (Cache.Gi().MONSTER_TEMPLATES[m.Id].Type != 4 ? Math.Abs(m.Y - myCharzY) <= 50 + (skill.Point > 5 ? skill.Point * 5 : 0) : Math.Abs(myCharzY - m.Y) <= 50 + (skill.Point > 5 ? skill.Point * 5 : 0))))
                                                    foreach (var monsterMap in zone.MonsterMaps.Where(m => !m.IsDie && dir == 1 ? (m.X >= myCharzX && m.X <= pointX) : (m.X >= pointX && m.X <= myCharzX) && (m.Y >= myCharzY ? Math.Abs(m.Y - myCharzY) <= skillData.Dy : Math.Abs(myCharzY - m.Y) <= skillData.Dy)))
                                                    {
                                                        var damageMonsterAfterHandle = monsterMap.MonsterHandler.UpdateHp((long)damage, character.Id);
                                                        monsterMap.MonsterHandler.AddPlayerAttack(character, damageMonsterAfterHandle);
                                                        character.CharacterHandler.SendZoneMessage(Service.MonsterHp(monsterMap, true, damageMonsterAfterHandle, -1));
                                                        character.CharacterHandler.PlusTiemNang(monsterMap, damageMonsterAfterHandle);
                                                    }
                                                }
                                                lock (zone.Bosses)
                                                {
                                                    foreach (var @boss in zone.Bosses.Values.Where(b => !b.InfoChar.IsDie && dir == 1 ? (b.InfoChar.X >= myCharzX && b.InfoChar.X <= pointX) : (b.InfoChar.X >= pointX && b.InfoChar.X <= myCharzX) && (b.InfoChar.Y >= myCharzY ? Math.Abs(b.InfoChar.Y - myCharzY) <= skillData.Dy : Math.Abs(myCharzY - b.InfoChar.Y) <= skillData.Dy)))
                                                    {
                                                        @boss.CharacterHandler.MineHp((long)damage);
                                                        @boss.CharacterHandler.SendMessage(Service.SendHp((int)@boss.InfoChar.Hp));
                                                        @boss.CharacterHandler.SendZoneMessage(Service.PlayerLevel(@boss));
                                                        character.CharacterHandler.SendZoneMessage(Service.HaveAttackPlayer(@boss, true, (long)damage, -1));
                                                        if (boss.InfoChar.IsDie)
                                                        {
                                                            boss.CharacterHandler.SendDie();
                                                            boss.CharacterHandler.LeaveItem(character);
                                                            TaskHandler.gI().CheckTaskDoneKillBoss((Model.Character.Character)character, boss.Type);
                                                        }
                                                    }
                                                }
                                                lock (zone.Characters)
                                                {
                                                    foreach (var @char in zone.Characters.Values.Where(c => !c.InfoChar.IsDie && ((c.InfoChar.TypePk != 0 || c.Flag != 0) && (character.InfoChar.TypePk != 0 || character.Flag != 0)) && (dir == 1 ? (c.InfoChar.X >= myCharzX && c.InfoChar.X <= pointX) : (c.InfoChar.X >= pointX && c.InfoChar.X <= myCharzX)) && c.Id != character.Id && c.Id != -character.Id && (c.InfoChar.Y >= myCharzY ? Math.Abs(c.InfoChar.Y - myCharzY) <= skillData.Dy : Math.Abs(myCharzY - c.InfoChar.Y) <= skillData.Dy)))
                                                    {
                                                        if (damage >= @char.InfoChar.Hp)
                                                        {
                                                            damage = (int)@char.InfoChar.Hp;
                                                        }

                                                        // Bổ Huyết
                                                        if (damage > 0 && @char.InfoBuff.GiapXen)
                                                        {
                                                            damage -= (damage * 50 / 100);
                                                        }
                                                        if (damage > 0 && @char.InfoBuff.GiapXen2)
                                                        {
                                                            damage -= (damage * 60 / 100);
                                                        }
                                                        @char.CharacterHandler.MineHp((long)damage);
                                                        @char.CharacterHandler.SendMessage(Service.SendHp((int)@char.InfoChar.Hp));
                                                        @char.CharacterHandler.SendZoneMessage(Service.PlayerLevel(@char));
                                                        character.CharacterHandler.SendZoneMessage(Service.HaveAttackPlayer(@char, true, (long)damage, -1));
                                                        if (@char.InfoChar.IsDie)
                                                        {
                                                            @char.CharacterHandler.SendDie();
                                                            if (@char.Enemies.FirstOrDefault(cg => cg.Id == character.Id) == null && !real.InfoBuff.AnDanh)
                                                            {
                                                                @char.Enemies.Add(real.Me);
                                                                if (@char.Enemies.Count > 30)
                                                                {
                                                                    @char.Enemies.RemoveAt(0);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                Thread.Sleep(150);
                                            }
                                        }
                                        var task = new System.Threading.Tasks.Task(Action);
                                        task.Start();
                                        break;
                                    }
                                case 26:
                                    {
                                        var real = (Model.Character.Character)character;
                                        character.CharacterHandler.SendZoneMessage(Service.SkillNotFocus20(character.Id, 26, 3, 3000, 1, (byte)dir));
                                        async void Action()
                                        {
                                            await System.Threading.Tasks.Task.Delay(3000);
                                            character.CharacterHandler.SendZoneMessage(Service.SkillNotFocus21(character.Id, 26, (short)myCharzX, character.InfoChar.Y, 3000, (short)(350 + (50 * skill.Point)), real));
                                            await System.Threading.Tasks.Task.Delay(3000);
                                            for (int i = 0; i < character.InfoSkill.MaPhongBa.IdCharacters.Count; i++)
                                            {
                                                var player = character.Zone.ZoneHandler.GetCharacter(character.InfoSkill.MaPhongBa.IdCharacters[i]);
                                                if (player != null)
                                                {
                                                    player.InfoSkill.MaPhongBa.isMaPhongBa = true;
                                                    player.InfoSkill.MaPhongBa.timeMaPhongBa = 15000 + timeServer;
                                                    player.CharacterHandler.SendZoneMessage(Service.UpdateBody(player));
                                                    player.CharacterHandler.SendMessage(Service.ItemTime(11175, 10));
                                                }
                                            }
                                            character.InfoSkill.MaPhongBa.IdCharacters.Clear();
                                            for (int i2 = 0; i2 < character.InfoSkill.MaPhongBa.IdMonsterMaps.Count; i2++)
                                            {
                                                var mob = character.Zone.ZoneHandler.GetMonsterMap(i2);
                                                if (mob != null)
                                                {
                                                    character.Zone.ZoneHandler.SendMessage(Service.ChangeMonsterBody(1, i2, 11175));
                                                    mob.InfoSkill.MaPhongBa.isMaPhongBa = true;
                                                    mob.InfoSkill.MaPhongBa.timeMaPhongBa = 15000 + timeServer;
                                                }
                                            }
                                            character.InfoSkill.MaPhongBa.IdMonsterMaps.Clear();
                                            for (int i3 = 0; i3 < character.InfoSkill.MaPhongBa.IdBosses.Count; i3++)
                                            {
                                                var boss = character.Zone.ZoneHandler.GetBoss(character.InfoSkill.MaPhongBa.IdBosses[i3]);
                                                if (boss != null)
                                                {
                                                    boss.InfoSkill.MaPhongBa.isMaPhongBa = true;
                                                    boss.InfoSkill.MaPhongBa.timeMaPhongBa = 15000 + timeServer;
                                                    boss.CharacterHandler.SendZoneMessage(Service.UpdateBody(boss));
                                                    boss.CharacterHandler.SendMessage(Service.ItemTime(11175, 10));
                                                }
                                            }
                                            character.InfoSkill.MaPhongBa.IdBosses.Clear();
                                        }
                                        var task = new Task(Action);
                                        task.Start();
                                        break;
                                    }
                            }
                            skill.CurrExp += 10;
                            skill.CoolDown = skillData.CoolDown + timeServer;
                            character.CharacterHandler.SendMessage(Service.UpdateSkill0((short)skill.SkillId, skill.CurrExp));
                            break;
                        }
                
                    //Start Choáng
                    case 0:
                    {
                        if(character.InfoChar.Gender != 0 || manaUse > manaChar || skillCharFocus.CoolDown > timeServer) return;
                        // Nội tại thái dương hạ san

                        var charRel = (Model.Character.Character)character;
                        if (charRel.SpecialSkill.Id != -1 && charRel.SpecialSkill.SkillId == 6) //Đã có nội tại
                        {
                            manaUse += charRel.SpecialSkill.Value;
                            character.CharacterHandler.MineMp(manaUse);

                            skillCharFocus.CoolDown = skillData.CoolDown + timeServer;
                            skillCharFocus.CoolDown -= skillData.CoolDown*charRel.SpecialSkill.Value/100;

                            if (skillCharFocus.CoolDown < 0)
                            {
                                skillCharFocus.CoolDown = 500;
                            }
                        }
                        else 
                        {
                            skillCharFocus.CoolDown = skillData.CoolDown + timeServer;
                            character.CharacterHandler.MineMp(manaUse);
                        }


                        var monsters = new List<IMonster>();
                        var characters = new List<ICharacter>();

                        int time = DataCache.TimeStun[skillData.Point];
                        
                        //if (character.InfoSet.IsFullSetThienXinHang)
                        //{
                        //    time*=2;
                        //}

                        lock (zone.MonsterMaps)
                        {
                            foreach (var monsterMap in zone.MonsterMaps.Where(m => m is {IsDie: false}))
                            {
                                lock (monsterMap.InfoSkill.ThaiDuongHanSan)
                                {
                                    monsterMap.InfoSkill.ThaiDuongHanSan.IsStun = true;
                                    monsterMap.InfoSkill.ThaiDuongHanSan.Time = time*1000 + timeServer;
                                    monsterMap.InfoSkill.ThaiDuongHanSan.TimeReal = time;
                                    character.Zone.ZoneHandler.SendMessage(Service.SkillEffectMonster(character.Id, monsterMap.IdMap, 1, 40));
                                }
                                monsterMap.IsDontMove = true;
                                character.Zone.ZoneHandler.SendMessage(Service.MonsterDontMove(monsterMap.IdMap, true));
                                monsters.Add(monsterMap); 
                            } 
                        }
                        
                        if (character.Flag != 0 || (charRel.Test != null && charRel.Test.IsTest) || charRel.InfoChar.TypePk == 3)
                        {
                            lock (zone.Characters)
                            {
                                    foreach (var real in zone.Characters.Values.Where(c => c != null && c.Id != character.Id && !c.InfoChar.IsDie))
                                    {
                                        if (real.InfoChar.TypePk != 3 && real.Flag != 8 &&
                                            (real.Flag == 0 || real.Flag == character.Flag)){
                                            lock (real.InfoSkill.ThaiDuongHanSan)
                                            {
                                                var timeReal = time;
                                                //Cải trang Giảm thời gian choáng
                                                if (true) timeReal -= 0;
                                                real.InfoSkill.ThaiDuongHanSan.IsStun = true;
                                                real.InfoSkill.ThaiDuongHanSan.Time = timeReal * 1000 + timeServer;
                                                real.InfoSkill.ThaiDuongHanSan.TimeReal = timeReal;

                                                zone.ZoneHandler.SendMessage(Service.SkillEffectPlayer(character.Id, real.Id, 1, 40));
                                                zone.ZoneHandler.SendMessage(Service.PublicChat(real.Id, TextServer.gI().SKILL_BLIND[ServerUtils.RandomNumber(TextServer.gI().SKILL_BLIND.Count)]));
                                            }
                                            characters.Add(real);
                                        }
                                    }
                                    }
                            

                            lock (zone.Disciples)
                            {
                                foreach (var real in zone.Disciples.Values.Where(c => c != null && (c.Id + character.Id) != 0 && !c.InfoChar.IsDie))
                                {
                                    if (real.Character.InfoChar.TypePk != 3 && real.Character.Flag != 8 &&
                                        (real.Character.Flag == 0 || real.Character.Flag == character.Flag) &&
                                        (!real.Character.Test.IsTest || real.Character.Test.TestCharacterId != character.Id)) continue;
                                    lock (real.InfoSkill.ThaiDuongHanSan)
                                    {
                                        var timeReal = time;
                                        //Cải trang Giảm thời gian choáng
                                        if (true) timeReal -= 0;
                                        real.InfoSkill.ThaiDuongHanSan.IsStun = true;
                                        real.InfoSkill.ThaiDuongHanSan.Time = timeReal*1000 + timeServer;
                                        real.InfoSkill.ThaiDuongHanSan.TimeReal = timeReal;

                                        zone.ZoneHandler.SendMessage(Service.SkillEffectPlayer(character.Id, real.Id, 1, 40));
                                        zone.ZoneHandler.SendMessage(Service.PublicChat(real.Id, TextServer.gI().SKILL_BLIND[ServerUtils.RandomNumber(TextServer.gI().SKILL_BLIND.Count)]));
                                    }
                                    characters.Add(real);
                                }
                            }
                        }

                        lock (zone.Bosses)
                        {
                            foreach (var real in zone.Bosses.Values.Where(c => c != null && c.Id != character.Id && !c.InfoChar.IsDie))
                            {
                                lock (real.InfoSkill.ThaiDuongHanSan)
                                {
                                    var timeReal = time/2;
                                    real.InfoSkill.ThaiDuongHanSan.IsStun = true;
                                    real.InfoSkill.ThaiDuongHanSan.Time = timeReal*1000 + timeServer;
                                    real.InfoSkill.ThaiDuongHanSan.TimeReal = timeReal;

                                    zone.ZoneHandler.SendMessage(Service.SkillEffectPlayer(character.Id, real.Id, 1, 40));
                                    zone.ZoneHandler.SendMessage(Service.PublicChat(real.Id, TextServer.gI().SKILL_BLIND[ServerUtils.RandomNumber(TextServer.gI().SKILL_BLIND.Count)]));
                                }
                                characters.Add(real);
                            }
                        }
                        
                        zone.ZoneHandler.SendMessage(Service.SkillNotFocus0(character.Id, skillData.SkillId, characters, monsters));
                        break;
                    }
                    //START Dùng skill tái tạo năng lượng
                    case 1:
                    {
                        if(character.InfoChar.Gender != 2 || skillCharFocus.CoolDown > timeServer) return;
                        zone.ZoneHandler.SendMessage(Service.SkillNotFocus1(character.Id, skillData.SkillId));
                        skillCharFocus.CoolDown = skillData.CoolDown + timeServer;
                     //   character.InfoSkill.TaiTaoNangLuong.IsTTNL = true;
                     //   character.InfoSkill.TaiTaoNangLuong.DelayTTNL = 5000 + timeServer;
                     //   character.DataBoMong.Count[15]++;
                        break;
                    }
                    //DOING Xử lý tái tạo năng lượng
                    case 2:
                    {
                        if(character.InfoChar.Gender != 2) return;
                        var hpFull = character.HpFull;
                        var mpFull = character.MpFull;
                        var hpNow = character.InfoChar.Hp;
                        var mpNow = character.InfoChar.Mp;
                        var percentHp = (int)((hpFull- hpNow)*100/character.HpFull);
                        var percentMp = (int)((mpFull - mpNow)*100/character.HpFull);
                        if(percentHp > 30 || percentMp > 30) {
                            zone.ZoneHandler.SendMessage(Service.PublicChat(character.Id, $"Tái tạo năng lượng: {(percentHp > percentMp ? percentHp : percentMp)}%"));
                        }

                        if (hpNow < hpFull)
                        {
                            character.CharacterHandler.PlusHp((int)(skillData.Damage * hpFull / 100));
                            zone.ZoneHandler.SendMessage(Service.PlayerLevel(character));
                        }

                        if (mpNow < mpFull)
                        {
                            character.CharacterHandler.PlusMp((int)(skillData.Damage * mpFull / 100));
                        }

                        if (character.InfoSkill.TaiTaoNangLuong.IsTTNL && character.InfoSkill.TaiTaoNangLuong.Crit <= 0)
                        {
                            character.InfoSkill.TaiTaoNangLuong.Crit = ServerUtils.RandomNumber(3);
                        }

                        if (character.InfoChar.Hp == hpFull && character.InfoChar.Mp == mpFull)
                        {
                            RemoveTTNL(character, skillTemplate.Id);
                        }
                        break;
                    }
                    //STOP Dừng tái tạo năng lượng
                    case 3:
                    {
                        if(character.InfoChar.Gender != 2) return;
                        RemoveTTNL(character, skillTemplate.Id);
                        break;
                    }
                    //QCKK + LAZE
                    case 4:
                    {
                        //     character.DataBoMong.Count[15]++;
                        switch (character.InfoChar.Gender)
                        {
                            //QCKK
                            case 0:
                            {
                         //       Server.Gi().Logger.Debug($"Check skill ------------------------- manause: {manaUse} ------ manaChar: {manaChar}");
                                if (manaUse > manaChar || skillCharFocus.CoolDown > timeServer) return;
                                character.InfoSkill.Qckk.Time = 3000 + timeServer;
                                character.CharacterHandler.SendZoneMessage(Service.SkillNotFocus4(character.Id, skillData.SkillId, 5000));

                                var task = new Task(() =>
                                {
                                    while (character.InfoSkill.Qckk.Time > ServerUtils.CurrentTimeMillis() && !character.InfoChar.IsDie)
                                    {
                                        foreach (var c in zone.Characters.Values.Where(c => c != null && !c.InfoChar.IsDie && c.Id != character.Id))
                                        {
                                            if (Math.Abs(c.InfoChar.X - character.InfoChar.X) > skillData.Dx) continue;
                                            character.InfoSkill.Qckk.ListId.Clear();
                                            if(!character.InfoSkill.Qckk.ListId.Contains(c.Id)) character.InfoSkill.Qckk.ListId.Add(c.Id);
                                        }
                                    }
                                });
                                task.Start();

                                break;
                            }
                            //LAZE
                            case 1:
                            {
                                Server.Gi().Logger.Debug($"Check skill ------------------------- manause: {manaUse} ------ manaChar: {manaChar}");
                                if (manaChar <= 0 || skillCharFocus.CoolDown > timeServer) return;
                                character.InfoSkill.Laze.Hold = true;
                                character.InfoSkill.Laze.Time = 3000 + timeServer;
                                character.CharacterHandler.SendZoneMessage(Service.SkillNotFocus4(character.Id, skillData.SkillId, 5000));
                                break;
                            }
                        }
                        break;
                    }
                    //Biến khỉ
                    case 6:
                    {
                        if (character.InfoChar.Gender != 2 || skillCharFocus.CoolDown > timeServer) return;
                        skillCharFocus.CoolDown = skillData.CoolDown + timeServer;
                          //   character.DataBoMong.Count[15]++;
                            async void Action()
                        {
                            zone.ZoneHandler.SendMessage(Service.SkillNotFocus6(character.Id, skillData.SkillId));
                            var headMonkey = skillData.SkillId switch
                            {
                                92 => 195,
                                93 => 196,
                                94 => 199,
                                95 => 197,
                                96 => 200,
                                97 => 198,
                                _ => 192
                            };
                            character.InfoSkill.Monkey.IsStart = true;
                            character.InfoSkill.Monkey.HeadMonkey = (short) headMonkey;
                            character.InfoSkill.Monkey.Delay = 2000 + timeServer;
                            await Task.Delay(3000);
                            character.InfoSkill.Monkey.IsStart = false;
                            if (!character.InfoChar.IsDie)
                            {
                                HandleMonkey(character, true);
                            }
                        }
                        var task = new System.Threading.Tasks.Task(Action);
                        task.Start();
                        break;
                    }
                    //Tự sát
                    case 7:
                    {
                        //Check Gender, mana, cooldownSkill
                        if(character.InfoChar.Gender != 2 || manaUse > manaChar || skillCharFocus.CoolDown > timeServer) return;
                        zone.ZoneHandler.SendMessage(Service.SkillNotFocus1(character.Id, skillData.SkillId));
                        skillCharFocus.CoolDown = skillData.CoolDown + timeServer;
                     
                        character.CharacterHandler.MineMp(manaUse);
                        //Send eff tự sát
                        zone.ZoneHandler.SendMessage(Service.SkillNotFocus7(character.Id, skillData.SkillId, 3000));
                        //Set delay tự sát
                        character.InfoSkill.TuSat.Delay = 2500 + timeServer;
                        character.InfoSkill.TuSat.Damage = skillData.Damage;
                        break;
                    }
                    case 8:
                    {      
                        if(character.InfoChar.IsDie) return;
                        switch (character.InfoChar.Gender)
                        {
                            //Start Đẻ trứng
                            case 1:
                            {
                                if(character.InfoChar.Gender != 1 || manaUse > manaChar || skillCharFocus.CoolDown > timeServer) return;
                                skillCharFocus.CoolDown = skillData.CoolDown + timeServer;
                                character.CharacterHandler.MineMp(manaUse);

                                if (character.InfoSkill.Egg.Monster is {IsDie: false})
                                {
                                    zone.ZoneHandler.SendMessage(Service.UpdateMonsterMe7(character.InfoSkill.Egg.Monster.IdMap));
                                    zone.ZoneHandler.RemoveMonsterMe(character.InfoSkill.Egg.Monster.IdMap);
                                }
                            //    character.DataBoMong.Count[15]++;
                                var hp = (20 + skillData.Point*10)*character.HpFull/100;
                                var damage = skillData.Damage*character.DamageFull/100;
                                var timeUse = 300000 + 60000 * skillData.Point + timeServer;
                                character.InfoSkill.Egg.Monster = new MonsterPet(character, zone, DataCache.IdMonsterPet[skillData.Point-1], hp, damage);
                                character.InfoSkill.Egg.Time = timeUse;
                                if (zone.ZoneHandler.AddMonsterPet(character.InfoSkill.Egg.Monster))
                                {
                                    zone.ZoneHandler.SendMessage(Service.UpdateMonsterMe0(character.InfoSkill.Egg.Monster));
                                }
                                else
                                {
                                    RemoveMonsterPet(character);
                                }
                                // Nội tại giảm thời gian hồi chiêu
                                var charRel = (Model.Character.Character)character;
                                var specialId = charRel.SpecialSkill.Id;
                                if (specialId == 14)
                                {
                                    var thoiGianHoiChieu = skillData.CoolDown - ((skillData.CoolDown*charRel.SpecialSkill.Value)/100);
                                    skillCharFocus.CoolDown = thoiGianHoiChieu + timeServer;
                                    if (skillCharFocus.CoolDown < 0)
                                    {
                                        skillCharFocus.CoolDown = 500;
                                    }
                                    character.CharacterHandler.SendMessage(Service.UpdateCooldown(character));
                                }
                                break;
                            }
                            //Xử lý tự sát
                            case 2:
                            {
                                HandleTuSat(character, skillCharFocus, skillData);
                                break;
                            }
                        }
                        break;
                    }
                    //Lá chắn
                    case 9:
                    {
                        if(manaUse > manaChar || skillCharFocus.CoolDown > timeServer) return;
                        character.CharacterHandler.MineMp(manaUse);
                     
                        skillCharFocus.CoolDown = skillData.CoolDown + timeServer;
                        var timeUse = DataCache.TimeProtect[skillData.Point];
                        zone.ZoneHandler.SendMessage(Service.SkillEffectPlayer(character.Id, character.Id, 1, 33));
                        character.CharacterHandler.SendMessage(Service.ItemTime(DataCache.TimeProtect[0], timeUse/10));
                        character.InfoSkill.Protect.IsProtect = true;
                        character.InfoSkill.Protect.Time = timeUse*100 + timeServer;
                           //  character.DataBoMong.Count[15]++;
                            // Nội tại giảm thời gian hồi chiêu
                            var charRel = (Model.Character.Character)character;
                        var specialId = charRel.SpecialSkill.Id;
                        if (specialId == 4 || specialId == 17 || specialId == 25)
                        {
                            var thoiGianHoiChieu = skillData.CoolDown - ((skillData.CoolDown*charRel.SpecialSkill.Value)/100);
                            skillCharFocus.CoolDown = thoiGianHoiChieu + timeServer;
                            if (skillCharFocus.CoolDown < 0)
                            {
                                skillCharFocus.CoolDown = 500;
                            }
                            character.CharacterHandler.SendMessage(Service.UpdateCooldown(character));
                        }
                        break;
                    }
                    //Huýt sáo
                    case 10:
                    {
                        if(character.InfoChar.Gender != 2 || manaUse > manaChar || skillCharFocus.CoolDown > timeServer) return;
                        skillCharFocus.CoolDown = skillData.CoolDown + timeServer;
                     
                        character.CharacterHandler.MineMp(manaUse);
                        var timeUse = DataCache.TimeHuytSao[1];
                        lock (character.Zone.Characters)
                        {
                            character.Zone.Characters.Values.Where(c => !c.InfoChar.IsDie).ToList().ForEach(c =>
                            {
                                lock (c)
                                {
                                    c.CharacterHandler.SendZoneMessage(Service.SkillEffectPlayer(c.Id, c.Id,1, 39));
                                    c.CharacterHandler.SendMessage(Service.ItemTime(DataCache.TimeHuytSao[0], timeUse/10));
                                    if (!c.InfoSkill.HuytSao.IsHuytSao)
                                    {
                                        c.HpFull += c.HpFull * skillData.Damage / 100;
                                    }
                                    c.InfoSkill.HuytSao.IsHuytSao = true;
                                    c.InfoSkill.HuytSao.Percent = skillData.Damage;
                                    c.InfoSkill.HuytSao.Time = timeUse*100 + timeServer;
                                    //character.DataBoMong.Count[15]++;
                                    // c.CharacterHandler.SetHpFull();
                                    c.CharacterHandler.SendMessage(Service.MeLoadPoint(c));
                                    c.CharacterHandler.PlusHp((int)(c.HpFull*c.InfoSkill.HuytSao.Percent/100));
                                    c.CharacterHandler.SendMessage(Service.SendHp((int)c.InfoChar.Hp));
                                    c.CharacterHandler.SendZoneMessage(Service.PlayerLevel(c));
                                }
                            });
                        }
                        lock (character.Zone.Disciples.Values)
                        {
                            character.Zone.Disciples.Values.Where(c => !c.InfoChar.IsDie).ToList().ForEach(c =>
                            {
                                lock (c)
                                {
                                    c.CharacterHandler.SendZoneMessage(Service.SkillEffectPlayer(c.Id, c.Id,1, 39));
                                    if (!c.InfoSkill.HuytSao.IsHuytSao)
                                    {
                                        c.HpFull += c.HpFull * skillData.Damage / 100;
                                    }
                                    c.InfoSkill.HuytSao.IsHuytSao = true;
                                    c.InfoSkill.HuytSao.Percent = skillData.Damage;
                                    c.InfoSkill.HuytSao.Time = timeUse*100 + timeServer;
                                    // c.CharacterHandler.SetHpFull();
                                    c.CharacterHandler.PlusHp((int)(c.HpFull*c.InfoSkill.HuytSao.Percent/100));
                                    c.CharacterHandler.SendZoneMessage(Service.PlayerLevel(c));
                                }
                            });
                        }

                        var charRel = (Model.Character.Character)character;
                        var specialId = charRel.SpecialSkill.Id;
                        if (specialId == 23)
                        {
                            var thoiGianHoiChieu = skillData.CoolDown - ((skillData.CoolDown*charRel.SpecialSkill.Value)/100);
                            skillCharFocus.CoolDown = thoiGianHoiChieu + timeServer;
                            if (skillCharFocus.CoolDown < 0)
                            {
                                skillCharFocus.CoolDown = 500;
                            }
                            character.CharacterHandler.SendMessage(Service.UpdateCooldown(character));
                        }
                        break;
                    }
                }
                character.CharacterHandler.MineStamina(1);
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error SkillNotFocus in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
            finally
            {
                message?.CleanUp();
            }
        }
        //Disciple
        public static void SkillNotFocus(Disciple disciple, int id, int status)
        {
            var zone = disciple.Zone;
            if (zone == null)
            {
                return;
            }
            try
            {
                if (disciple.InfoChar.Stamina <= 0)
                {
                    disciple.Character.CharacterHandler.SendMessage(Service.PublicChat(disciple.Id, TextServer.gI().NOT_ENOUGH_STAMINA_DISCIPLE));
                    return;
                }
                //Get skill
                var skillCharFocus = disciple.Skills.FirstOrDefault(skl => skl.Id == id);
                if(skillCharFocus == null) return;
                var skillTemplate = Cache.Gi().SKILL_TEMPLATES.FirstOrDefault(skl => skl.Id == skillCharFocus.Id);
                var skillData = skillTemplate?.SkillDataTemplates.FirstOrDefault(skl => skl.SkillId == skillCharFocus.SkillId);
                if(skillData == null) return;

                //Check mana
                var manaUse = skillData.ManaUse;
                var manaUseType = skillTemplate.ManaUseType;
                var manaChar = disciple.InfoChar.Mp;
                manaUse = manaUseType switch
                {
                    1 => manaUse * (int) disciple.MpFull / 100,
                    2 => (int) manaChar,
                    _ => manaUse
                };

                //Remove Tái Tạo Năng Lượng 
                if (status != 2 && disciple.InfoSkill.TaiTaoNangLuong.IsTTNL)
                {
                    RemoveTTNL(disciple, skillTemplate.Id);
                }
                
                var timeServer = ServerUtils.CurrentTimeMillis();
                switch (status)
                {
                   
                    
                    //Start Choáng
                    case 0:
                    {
                        if(manaUse > manaChar || skillCharFocus.CoolDown > timeServer) return;
                        skillCharFocus.CoolDown = skillData.CoolDown + timeServer;
                        disciple.CharacterHandler.MineMp(manaUse);

                        var monsters = new List<IMonster>();
                        var characters = new List<ICharacter>();

                        int time = DataCache.TimeStun[skillData.Point];
                        var timeUse = time*1000 + timeServer;

                        //if (disciple.InfoSet.IsFullSetThienXinHang)
                        //{
                        //    time*=2;
                        //}

                        lock (zone.MonsterMaps)
                        {
                            foreach (var monsterMap in zone.MonsterMaps.Where(m => m is {IsDie: false}))
                            {
                                lock (monsterMap.InfoSkill.ThaiDuongHanSan)
                                {
                                    monsterMap.InfoSkill.ThaiDuongHanSan.IsStun = true;
                                    monsterMap.InfoSkill.ThaiDuongHanSan.Time = timeUse;
                                    monsterMap.InfoSkill.ThaiDuongHanSan.TimeReal = time;
                                    zone.ZoneHandler.SendMessage(Service.SkillEffectMonster(disciple.Id, monsterMap.IdMap, 1, 40));
                                }
                                monsterMap.IsDontMove = true;
                                zone.ZoneHandler.SendMessage(Service.MonsterDontMove(monsterMap.IdMap, true));
                                monsters.Add(monsterMap);
                            }
                        }

                        lock (zone.Characters)
                        {
                            foreach (var real in zone.Characters.Values.Where(c => c != null && (c.Id + disciple.Id) != 0 && !c.InfoChar.IsDie))
                            {
                                if (real.Id + disciple.Id != 0 && real.InfoChar.TypePk != 3 && real.Flag != 8 &&
                                    (real.Flag == 0 || real.Flag == disciple.Flag) && (!real.Challenge.isChallenge ||
                                        real.Challenge.PlayerChallengeID != disciple.Character.Id)) continue;
                                lock (real.InfoSkill.ThaiDuongHanSan)
                                {
                                    var timeReal = time;
                                    //Cải trang Giảm thời gian choáng
                                    if (true) timeReal -= 0;
                                    real.InfoSkill.ThaiDuongHanSan.IsStun = true;
                                    real.InfoSkill.ThaiDuongHanSan.Time = timeReal*1000 + timeServer;
                                    real.InfoSkill.ThaiDuongHanSan.TimeReal = timeReal;

                                    zone.ZoneHandler.SendMessage(Service.SkillEffectPlayer(disciple.Id, real.Id, 1, 40));
                                    zone.ZoneHandler.SendMessage(Service.PublicChat(real.Id, TextServer.gI().SKILL_BLIND[ServerUtils.RandomNumber(TextServer.gI().SKILL_BLIND.Count)]));
                                }
                                characters.Add(real);
                            }
                        }
                        zone.ZoneHandler.SendMessage(Service.SkillNotFocus0(disciple.Id, skillData.SkillId, characters, monsters));
                        break;
                    }
                    //START Dùng skill tái tạo năng lượng
                    case 1:
                    {
                        if(skillCharFocus.CoolDown > timeServer) return;
                        zone.ZoneHandler.SendMessage(Service.SkillNotFocus1(disciple.Id, skillData.SkillId));
                        skillCharFocus.CoolDown = skillData.CoolDown + timeServer;
                        disciple.InfoSkill.TaiTaoNangLuong.IsTTNL = true;
                        disciple.InfoSkill.TaiTaoNangLuong.DelayTTNL = 20000 + timeServer;
                        break;
                    }
                    //DOING Xử lý tái tạo năng lượng
                    case 2:
                    {
                        var hpFull = disciple.HpFull;
                        var mpFull = disciple.MpFull;
                        var hpNow = disciple.InfoChar.Hp;
                        var mpNow = disciple.InfoChar.Mp;
                        var percentHp = (int)((hpFull- hpNow)*100/disciple.HpFull);
                        var percentMp = (int)((mpFull - mpNow)*100/disciple.HpFull);
                        if(percentHp > 30 || percentMp > 30) {
                            zone.ZoneHandler.SendMessage(Service.PublicChat(disciple.Id, $"Tái tạo năng lượng: {(percentHp > percentMp ? percentHp : percentMp)}%"));
                        }

                        if (hpNow < hpFull)
                        {
                            disciple.CharacterHandler.PlusHp((int)(skillData.Damage * hpFull / 100));
                            zone.ZoneHandler.SendMessage(Service.PlayerLevel(disciple));
                        }

                        if (mpNow < mpFull)
                        {
                            disciple.CharacterHandler.PlusMp((int)(skillData.Damage * mpFull / 100));
                        }

                        if (disciple.InfoSkill.TaiTaoNangLuong.IsTTNL && disciple.InfoSkill.TaiTaoNangLuong.Crit <= 0)
                        {
                            disciple.InfoSkill.TaiTaoNangLuong.Crit = ServerUtils.RandomNumber(3);
                        }

                        if (disciple.InfoChar.Hp == hpFull && disciple.InfoChar.Mp == mpFull)
                        {
                            RemoveTTNL(disciple, skillTemplate.Id);
                        }
                        break;
                    }
                    //STOP Dừng tái tạo năng lượng
                    case 3:
                    {
                        RemoveTTNL(disciple, skillTemplate.Id);
                        break;
                    }
                    //QCKK + LAZE
                    case 4:
                    {
                        switch (id)
                        {
                            //QCKK
                            case 10:
                            {
                                Server.Gi().Logger.Debug($"Check skill ------------------------- manause: {manaUse} ------ manaChar: {manaChar}");
                                if (manaUse > manaChar || skillCharFocus.CoolDown > timeServer) return;
                                disciple.InfoSkill.Qckk.Time = 3000 + timeServer;
                                disciple.CharacterHandler.SendZoneMessage(Service.SkillNotFocus4(disciple.Id, skillData.SkillId, 5000));

                                var task = new Task(() =>
                                {
                                    while (disciple.InfoSkill.Qckk.Time > ServerUtils.CurrentTimeMillis() && !disciple.InfoChar.IsDie)
                                    {
                                        foreach (var c in zone.Characters.Values.Where(c => c != null && !c.InfoChar.IsDie && c.Id != disciple.Id))
                                        {
                                            if (Math.Abs(c.InfoChar.X - disciple.InfoChar.X) > skillData.Dx) continue;
                                            disciple.InfoSkill.Qckk.ListId.Clear();
                                            if(!disciple.InfoSkill.Qckk.ListId.Contains(c.Id)) disciple.InfoSkill.Qckk.ListId.Add(c.Id); 
                                        }
                                    }
                                });
                                task.Start();
                                break;
                            }
                            //LAZE
                            case 11:
                            {
                                Server.Gi().Logger.Debug($"Check skill ------------------------- manause: {manaUse} ------ manaChar: {manaChar}");
                                if (manaChar <= 0 || skillCharFocus.CoolDown > timeServer) return;
                                disciple.InfoSkill.Laze.Hold = true;
                                disciple.InfoSkill.Laze.Time = 3000 + timeServer;
                                disciple.CharacterHandler.SendZoneMessage(Service.SkillNotFocus4(disciple.Id, skillData.SkillId, 5000));
                                break;
                            }
                        }
                        break;
                    }
                    //Biến khỉ
                    case 6:
                    {
                        if (skillCharFocus.CoolDown > timeServer) return;
                        skillCharFocus.CoolDown = skillData.CoolDown + timeServer;
                        
                        async void Action()
                        {
                            zone.ZoneHandler.SendMessage(Service.SkillNotFocus6(disciple.Id, skillData.SkillId));
                            var headMonkey = skillData.SkillId switch
                            {
                                92 => 195,
                                93 => 196,
                                94 => 199,
                                95 => 197,
                                96 => 200,
                                97 => 198,
                                _ => 192
                            };
                            disciple.InfoSkill.Monkey.IsStart = true;
                            disciple.InfoSkill.Monkey.HeadMonkey = (short) headMonkey;
                            disciple.InfoSkill.Monkey.Delay = 2000 + timeServer;
                            await System.Threading.Tasks.Task.Delay(3000);
                            disciple.InfoSkill.Monkey.IsStart = false;
                            if (!disciple.InfoChar.IsDie)
                            {
                                HandleMonkey(disciple, true);
                            }
                        }
                        var task = new System.Threading.Tasks.Task(Action);
                        task.Start();
                        break;
                    }
                    //Tự sát
                    case 7:
                    {
                        //Check Gender, mana, cooldownSkill
                        if(manaUse > manaChar || skillCharFocus.CoolDown > timeServer) return;
                        zone.ZoneHandler.SendMessage(Service.SkillNotFocus1(disciple.Id, skillData.SkillId));
                        skillCharFocus.CoolDown = skillData.CoolDown + timeServer;
                     
                        disciple.CharacterHandler.MineMp(manaUse);

                        //Send eff tự sát
                        zone.ZoneHandler.SendMessage(Service.SkillNotFocus7(disciple.Id, skillData.SkillId, 3000));
                        //Set delay tự sát
                        disciple.InfoSkill.TuSat.Delay = 2500 + timeServer;
                        disciple.InfoSkill.TuSat.Damage = skillData.Damage;
                        break;
                    }
                    case 8:
                    {      
                        if(disciple.InfoChar.IsDie) return;
                        switch (id)
                        {
                            //Start Đẻ trứng
                            case 12:
                            {
                                if(manaUse > manaChar || skillCharFocus.CoolDown > timeServer) return;
                                skillCharFocus.CoolDown = skillData.CoolDown + timeServer;
                                disciple.CharacterHandler.MineMp(manaUse);

                                if (disciple.InfoSkill.Egg.Monster is {IsDie: false})
                                {
                                    zone.ZoneHandler.SendMessage(Service.UpdateMonsterMe7(disciple.InfoSkill.Egg.Monster.IdMap));
                                    zone.ZoneHandler.RemoveMonsterMe(disciple.InfoSkill.Egg.Monster.IdMap);
                                }

                                var hp = (20 + skillData.Point*10)*disciple.HpFull/100;
                                var damage = skillData.Damage*disciple.DamageFull/100;
                                var timeUse = 300000 + 60000 * skillData.Point + timeServer;
                                disciple.InfoSkill.Egg.Monster = new MonsterPet(disciple, zone, DataCache.IdMonsterPet[skillData.Point-1], hp, damage);
                                disciple.InfoSkill.Egg.Time = timeUse;
                                if (zone.ZoneHandler.AddMonsterPet(disciple.InfoSkill.Egg.Monster))
                                {
                                    zone.ZoneHandler.SendMessage(Service.UpdateMonsterMe0(disciple.InfoSkill.Egg.Monster));
                                }
                                else
                                {
                                    RemoveMonsterPet(disciple);
                                }
                                break;
                            }
                            //Xử lý tự sát
                            case 2:
                            {
                                HandleTuSat(disciple, skillCharFocus, skillData);
                                break;
                            }
                        }
                        break;
                    }
                    //Lá chắn
                    case 9:
                    {
                        if(manaUse > manaChar || skillCharFocus.CoolDown > timeServer) return;
                        disciple.CharacterHandler.MineMp(manaUse);
                     
                        skillCharFocus.CoolDown = skillData.CoolDown + timeServer;
                        var timeUse = DataCache.TimeProtect[skillData.Point];
                        zone.ZoneHandler.SendMessage(Service.SkillEffectPlayer(disciple.Id, disciple.Id, 1, 33));
                        disciple.InfoSkill.Protect.IsProtect = true;
                        disciple.InfoSkill.Protect.Time = timeUse*100 + timeServer;
                        break;
                    }
                    //Huýt sáo
                    case 10:
                    {
                        if(manaUse > manaChar || skillCharFocus.CoolDown > timeServer) return;
                        skillCharFocus.CoolDown = skillData.CoolDown + timeServer;
                        disciple.CharacterHandler.MineMp(manaUse);
                        var timeUse = DataCache.TimeHuytSao[1];
                        lock (disciple.Zone.Characters)
                        {
                            foreach (var c in disciple.Zone.Characters.Values.Where(c => !c.InfoChar.IsDie))
                            {
                                lock (c)
                                {
                                    c.CharacterHandler.SendZoneMessage(Service.SkillEffectPlayer(c.Id, c.Id,1, 39));
                                    c.CharacterHandler.SendMessage(Service.ItemTime(DataCache.TimeHuytSao[0], timeUse/10));
                                    c.InfoSkill.HuytSao.IsHuytSao = true;
                                    c.InfoSkill.HuytSao.Percent = skillData.Damage;
                                    c.InfoSkill.HuytSao.Time = timeUse*100 + timeServer;
                                    c.CharacterHandler.SetHpFull();
                                    c.CharacterHandler.SendMessage(Service.MeLoadPoint(c));
                                    c.CharacterHandler.PlusHp((int)(c.HpFull*c.InfoSkill.HuytSao.Percent/100));
                                    c.CharacterHandler.SendMessage(Service.SendHp((int)c.InfoChar.Hp));
                                    c.CharacterHandler.SendZoneMessage(Service.PlayerLevel(c));
                                }  
                            }
                        }
                        lock (disciple.Zone.Disciples.Values)
                        {
                            foreach (var c in disciple.Zone.Disciples.Values.Where(c => !c.InfoChar.IsDie))
                            {
                                lock (c)
                                {
                                    c.CharacterHandler.SendZoneMessage(Service.SkillEffectPlayer(c.Id, c.Id,1, 39));
                                    c.InfoSkill.HuytSao.IsHuytSao = true;
                                    c.InfoSkill.HuytSao.Percent = skillData.Damage;
                                    c.InfoSkill.HuytSao.Time = timeUse*100 + timeServer;
                                    c.CharacterHandler.SetHpFull();
                                    c.CharacterHandler.PlusHp((int)(c.HpFull*c.InfoSkill.HuytSao.Percent/100));
                                    c.CharacterHandler.SendZoneMessage(Service.PlayerLevel(c));
                                }
                            }
                        }
                        break;
                    }
                }
                disciple.CharacterHandler.MineStamina(1);
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error SkillNotFocusDisciple in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }

        // Boss
        public static void BossSkillNotFocus(Boss character, int id, int status)
        {
            var zone = character.Zone;
            if (zone == null)
            {
                return;
            }
            try
            {
              

                //Get skill
                var skillCharFocus = character.Skills.FirstOrDefault(skl => skl.Id == id);
                if(skillCharFocus == null) return;
                var skillTemplate = Cache.Gi().SKILL_TEMPLATES.FirstOrDefault(skl => skl.Id == skillCharFocus.Id);
                var skillData = skillTemplate?.SkillDataTemplates.FirstOrDefault(skl => skl.SkillId == skillCharFocus.SkillId);
                if(skillData == null) return;

                //Check mana
                var manaUse = skillData.ManaUse;
                var manaUseType = skillTemplate.ManaUseType;
                var manaChar = character.InfoChar.Mp;
                manaUse = manaUseType switch
                {
                    1 => manaUse * (int) character.MpFull / 100,
                    2 => (int) manaChar,
                    _ => manaUse
                };

                //Remove Tái Tạo Năng Lượng 
                if (status != 2 && character.InfoSkill.TaiTaoNangLuong.IsTTNL)
                {
                    RemoveTTNL(character, skillTemplate.Id);
                }

                var timeServer = ServerUtils.CurrentTimeMillis();
                switch (status)
                {
                    //Start Choáng
                    case 0:
                    {
                        if(manaUse > manaChar || skillCharFocus.CoolDown > timeServer) return;
                        skillCharFocus.CoolDown = skillData.CoolDown + timeServer;
                        character.CharacterHandler.MineMp(manaUse);

                        var monsters = new List<IMonster>();
                        var characters = new List<ICharacter>();

                        int time = DataCache.TimeStun[skillData.Point];
                        var timeUse = time*1000 + timeServer;
                        lock (zone.MonsterMaps)
                        {
                            foreach (var monsterMap in zone.MonsterMaps.Where(m => m is {IsDie: false}))
                            {
                                lock (monsterMap.InfoSkill.ThaiDuongHanSan)
                                {
                                    monsterMap.InfoSkill.ThaiDuongHanSan.IsStun = true;
                                    monsterMap.InfoSkill.ThaiDuongHanSan.Time = timeUse;
                                    monsterMap.InfoSkill.ThaiDuongHanSan.TimeReal = time;
                                    character.Zone.ZoneHandler.SendMessage(Service.SkillEffectMonster(character.Id, monsterMap.IdMap, 1, 40));
                                }
                                monsterMap.IsDontMove = true;
                                character.Zone.ZoneHandler.SendMessage(Service.MonsterDontMove(monsterMap.IdMap, true));
                                monsters.Add(monsterMap); 
                            } 
                        }
                        
                        lock (zone.Characters)
                        {
                            foreach (var real in zone.Characters.Values.Where(c => c != null && c.Id != character.Id && !c.InfoChar.IsDie))
                            {
                                lock (real.InfoSkill.ThaiDuongHanSan)
                                {
                                    var timeReal = time;
                                    //Cải trang Giảm thời gian choáng
                                    if (true) timeReal -= 0;
                                    real.InfoSkill.ThaiDuongHanSan.IsStun = true;
                                    real.InfoSkill.ThaiDuongHanSan.Time = timeReal*1000 + timeServer;
                                    real.InfoSkill.ThaiDuongHanSan.TimeReal = timeReal;

                                    zone.ZoneHandler.SendMessage(Service.SkillEffectPlayer(character.Id, real.Id, 1, 40));
                                    zone.ZoneHandler.SendMessage(Service.PublicChat(real.Id, TextServer.gI().SKILL_BLIND[ServerUtils.RandomNumber(TextServer.gI().SKILL_BLIND.Count)]));
                                }
                                characters.Add(real);
                            }
                        }

                        lock (zone.Disciples)
                        {
                            foreach (var real in zone.Disciples.Values.Where(c => c != null && c.Id != character.Id && !c.InfoChar.IsDie))
                            {
                                lock (real.InfoSkill.ThaiDuongHanSan)
                                {
                                    var timeReal = time;
                                    //Cải trang Giảm thời gian choáng
                                    if (true) timeReal -= 0;
                                    real.InfoSkill.ThaiDuongHanSan.IsStun = true;
                                    real.InfoSkill.ThaiDuongHanSan.Time = timeReal*1000 + timeServer;
                                    real.InfoSkill.ThaiDuongHanSan.TimeReal = timeReal;

                                    zone.ZoneHandler.SendMessage(Service.SkillEffectPlayer(character.Id, real.Id, 1, 40));
                                    zone.ZoneHandler.SendMessage(Service.PublicChat(real.Id, TextServer.gI().SKILL_BLIND[ServerUtils.RandomNumber(TextServer.gI().SKILL_BLIND.Count)]));
                                }
                                characters.Add(real);
                            }
                        }
                        zone.ZoneHandler.SendMessage(Service.SkillNotFocus0(character.Id, skillData.SkillId, characters, monsters));
                        break;
                    }
                    //START Dùng skill tái tạo năng lượng
                    case 1:
                    {
                            
                                if (skillCharFocus.CoolDown > timeServer) return;
                                zone.ZoneHandler.SendMessage(Service.SkillNotFocus1(character.Id, skillData.SkillId));
                                skillCharFocus.CoolDown = skillData.CoolDown + timeServer;
                                character.InfoSkill.TaiTaoNangLuong.IsTTNL = true;
                                character.InfoSkill.TaiTaoNangLuong.DelayTTNL = 2000 + timeServer;
                            
                            
                        break;
                    }
                    //DOING Xử lý tái tạo năng lượng
                    case 2:
                    {
                        var hpFull = character.HpFull;
                        var mpFull = character.MpFull;
                        var hpNow = character.InfoChar.Hp;
                        var mpNow = character.InfoChar.Mp;
                        var percentHp = (int)((hpFull- hpNow)*100/character.HpFull);
                        var percentMp = (int)((mpFull - mpNow)*100/character.HpFull);
                        if(percentHp > 30 || percentMp > 30) {
                            zone.ZoneHandler.SendMessage(Service.PublicChat(character.Id, $"Tái tạo năng lượng: {(percentHp > percentMp ? percentHp : percentMp)}%"));
                        }
                        
                        if (hpNow < hpFull)
                            {
                          //  if (cha)    
//                            hpFull += hpFull * 10;
                             
                            character.CharacterHandler.PlusHp((int)(skillData.Damage * hpFull / 100));
                            zone.ZoneHandler.SendMessage(Service.PlayerLevel(character));
                        }

                        if (mpNow < mpFull)
                        {
                            character.CharacterHandler.PlusMp((int)(skillData.Damage * mpFull / 100));
                        }

                        if (character.InfoSkill.TaiTaoNangLuong.IsTTNL && character.InfoSkill.TaiTaoNangLuong.Crit <= 0)
                        {
                            character.InfoSkill.TaiTaoNangLuong.Crit = ServerUtils.RandomNumber(3);
                        }

                        if (character.InfoChar.Hp == hpFull && character.InfoChar.Mp == mpFull)
                        {
                            RemoveTTNL(character, skillTemplate.Id);
                        }
                        break;
                    }
                    //STOP Dừng tái tạo năng lượng
                    case 3:
                    {
                        RemoveTTNL(character, skillTemplate.Id);
                        break;
                    }
                    case 7:
                        {
                            //Check Gender, mana, cooldownSkill
                            if (character.InfoChar.Gender != 2 || manaUse > manaChar || skillCharFocus.CoolDown > timeServer) return;
                            zone.ZoneHandler.SendMessage(Service.SkillNotFocus1(character.Id, skillData.SkillId));
                            skillCharFocus.CoolDown = skillData.CoolDown + timeServer;

                            character.CharacterHandler.MineMp(manaUse);

                            //Send eff tự sát
                            zone.ZoneHandler.SendMessage(Service.SkillNotFocus7(character.Id, skillData.SkillId, 3000));
                            //Set delay tự sát
                            character.InfoSkill.TuSat.Delay = 2500 + timeServer;
                            character.InfoSkill.TuSat.Damage = skillData.Damage;
                            break;
                        }
                    case 8:
                        switch (id)
                        {
                            case 2:
                                HandleTuSat(character, skillCharFocus, skillData);
                                break;
                        }
                        break;
                    //Lá chắn
                    case 9:
                    {
                        if(manaUse > manaChar || skillCharFocus.CoolDown > timeServer) return;
                        character.CharacterHandler.MineMp(manaUse);
                     
                        skillCharFocus.CoolDown = skillData.CoolDown + timeServer;
                        var timeUse = DataCache.TimeProtect[skillData.Point];
                        zone.ZoneHandler.SendMessage(Service.SkillEffectPlayer(character.Id, character.Id, 1, 33));
                        character.CharacterHandler.SendMessage(Service.ItemTime(DataCache.TimeProtect[0], timeUse/10));
                        character.InfoSkill.Protect.IsProtect = true;
                        character.InfoSkill.Protect.Time = timeUse*100 + timeServer;
                        break;
                    }
                }
                character.CharacterHandler.MineStamina(1);
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error SkillNotFocus in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }
        // Check is boss
        public static bool CharIdIsBoss(int id)
        {
            if (id >= DataCache.BASE_BOSS_ID && id <= DataCache.MAX_BASE_BOSS_ID)
            {
                return true;
            }
            return false;
        }

        #region Attack Monster
        //Player attack
        public static void AttackMonster(ICharacter character, Message message)
        {
            try
            {
                var timeServer = ServerUtils.CurrentTimeMillis();
                var charRel = (Model.Character.Character)character;
                if (character.InfoSkill.TaiTaoNangLuong.IsTTNL)
                {
                    RemoveTTNL(character);
                }
                if (character.InfoChar.Stamina <= 0)
                {
                    character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_STAMINA));
                    return;
                }

                if (charRel.InfoMore.BuaBatTu)
                {
                    if (charRel.InfoMore.BuaBatTuTime > timeServer)
                    {
                        // Neus damage lớn hơn máu thì set máu bằng 1
                        if (charRel.InfoChar.Hp == 1)
                        {
                            charRel.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().PLEASE_USE_PEA));
                            return;
                        }
                    }
                    else 
                    {
                        charRel.InfoMore.BuaBatTu = false;
                    }
                }

                if (character.InfoChar.CSkill == -1)
                {
                    character.InfoChar.CSkill = (short) (character.InfoChar.Gender * 2);
                }

                var skillChar = character.Skills.FirstOrDefault(skl => skl.Id == character.InfoChar.CSkill);
                if (skillChar == null) 
                {
                    skillChar = character.Skills[0];
                }

                if (skillChar.Id == 11 && !character.InfoSkill.Laze.Hold)
                {
                    return;
                }

                var zone = character.Zone;
                if (zone == null) return;
                
                var skillTemplate = Cache.Gi().SKILL_TEMPLATES.FirstOrDefault(skl => skl.Id == skillChar.Id);
                var skillData =
                    skillTemplate?.SkillDataTemplates.FirstOrDefault(skl => skl.SkillId == skillChar.SkillId);
                if (skillData == null) return;
                // SkillDataTemplate skillData = skillDataReal.Clone();
                // Xử lý nội tại
                // skillData = HandleSpecialSkill(character, skillTemplate, skillData);
                //Check mana
                var manaUse = skillData.ManaUse;
                var manaUseType = skillTemplate.ManaUseType;
                var manaChar = character.InfoChar.Mp;
                manaUse = manaUseType switch
                {
                    1 => manaUse * (int) character.MpFull / 100,
                    2 => (int) manaChar,
                    _ => manaUse
                };

                if (manaUse > manaChar || skillChar.CoolDown > timeServer) return;
                else
                {
                    character.CharacterHandler.MineMp(manaUse);
                    skillChar.CoolDown = skillData.CoolDown + timeServer;
                }

                //get monster id
                var modId = message.Reader.ReadUnsignedByte();
                IMonster monsterAtt; 
                if (modId == 255)
                {
                    var petId = message.Reader.ReadInt();
                    monsterAtt = zone.ZoneHandler.GetMonsterPet(petId);
                }
                else
                {
                    monsterAtt = zone.ZoneHandler.GetMonsterMap(modId);
                }
                var listMonster = new List<IMonster>();
                if (monsterAtt is not {IsDie: false}) return;
                
                listMonster.Add(monsterAtt);
                var fightSize = 1;
                while (message.Reader.Available() > 0)
                {
                    var mobNext = zone.ZoneHandler.GetMonsterMap(message.Reader.ReadUnsignedByte());
                    if (mobNext is not {IsDie: false} || mobNext.IdMap == monsterAtt.IdMap) continue;
                    fightSize += 1;
                    if (fightSize >= skillData.MaxFight)
                    {
                        break;
                    }
                    listMonster.Add(mobNext);
                }
                //Handling player attack with skill
             //   Server.Gi().Logger.Print($"Client: {character.Player.Session.Id} --------------- PlayerAttackMonster id: {modId} - SkillTemplate: {skillTemplate.Id}");
                switch (skillTemplate.Id)
                {
                    //kaioken
                    case 9:
                    {
                        if(character.InfoChar.Gender != 0) return;
                        var hpMine = character.HpFull / 10;
                        if (hpMine >= character.InfoChar.Hp)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_HP));
                            return;
                        }
                        character.CharacterHandler.MineHp(hpMine);
                        character.CharacterHandler.SendMessage(Service.SendHp((int)character.InfoChar.Hp));
                        zone.ZoneHandler.SendMessage(Service.PlayerLevel(character));
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackMonster(character.Id, listMonster, skillData.SkillId));
                        HandlePlayerAttackMonster(character, skillChar, skillData, listMonster);
                        break;
                    } 
                    //QCKK
                    case 10:
                    {
                        if(character.InfoChar.Gender != 0 || character.InfoSkill.Qckk.Time > timeServer) return;
                        var damage = ServerUtils.RandomNumber(character.DamageFull * 9 / 10, character.DamageFull);
                        damage *= (skillData.Damage + character.InfoSkill.Qckk.ListId.Count*10) / 100;

                        if (charRel.InfoSet.IsFullSetKirin)
                        {
                            damage*=3;
                        }
                            if (charRel.ItemBody[5] != null && charRel.ItemBody[5].Id == 1270)
                            {
                                damage *= 6;
                            }
                            zone.ZoneHandler.SendMessage(Service.PlayerAttackMonster(character.Id, listMonster, skillData.SkillId));
                        HandlePlayerAttackMonster(character, skillChar, skillData, listMonster, damage:damage);

                        var specialId = charRel.SpecialSkill.Id;
                        if (specialId == 3)
                        {
                            var thoiGianHoiChieu = skillData.CoolDown - ((skillData.CoolDown*charRel.SpecialSkill.Value)/100);
                            skillChar.CoolDown = thoiGianHoiChieu + timeServer;
                            if (skillChar.CoolDown < 0)
                            {
                                skillChar.CoolDown = 500;
                            }
                            character.CharacterHandler.SendMessage(Service.UpdateCooldown(character));
                        }
                        break;
                    }
                    //Makankosappo
                    case 11:
                    {
                        if (character.InfoChar.Gender != 1 || character.InfoSkill.Laze.Time > timeServer || !character.InfoSkill.Laze.Hold) return;
                        long damage = (long)((long)manaUse*skillData.Damage / 100);

                        if (character.InfoSet.IsFullSetPicolo)
                        {
                                damage += damage * 2;
                        }
                            if (character.ItemBody[5].Id == 1273)
                            {
                                damage += damage * 6;
                            }
                            character.InfoSkill.Laze.Hold = false;
                        Server.Gi().Logger.Debug($"Check ---------------------- attack monster by Makankosappo with damage: {damage}");
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackMonster(character.Id, listMonster, skillData.SkillId));
                        HandlePlayerAttackMonster(character, skillChar, skillData, listMonster, damage:Math.Abs(((long)damage)));

                        var specialId = charRel.SpecialSkill.Id;
                        if (specialId == 13)
                        {
                            var thoiGianHoiChieu = skillData.CoolDown - ((skillData.CoolDown*charRel.SpecialSkill.Value)/100);
                            skillChar.CoolDown = thoiGianHoiChieu + timeServer;
                            if (skillChar.CoolDown < 0)
                            {
                                skillChar.CoolDown = 500;
                            }
                            character.CharacterHandler.SendMessage(Service.UpdateCooldown(character));
                        }
                        break;
                    }
                    //Biến Sôcôla
                    case 18:
                    {
                        if(character.InfoChar.Gender != 1) return;
                        if(listMonster[0] != null && listMonster[0].IsMobMe) return;
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackMonster(character.Id, listMonster,skillData.SkillId));
                        HandleSocolaMonster(character, listMonster, skillData);

                        // Kiểm tra có nội tại Sô cô la không
                        var specialId = charRel.SpecialSkill.Id;
                        if (specialId != -1 && charRel.SpecialSkill.SkillId == 18)
                        {
                            charRel.SpecialSkill.nextAttackDmgPercent = charRel.SpecialSkill.Value;
                        }
                        break;
                    }
                    //Dich chuyen tuc thoi
                    case 20:
                    {
                        if (character.InfoChar.Gender != 0) return;
                        if (listMonster[0] != null && !listMonster[0].IsMobMe)
                        {
                            HandleDichChuyenMonster(character, listMonster, skillData);
                        }
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackMonster(character.Id, listMonster,skillData.SkillId));
                        HandlePlayerAttackMonster(character, skillChar, skillData, listMonster, damage:character.DamageFull, isCrit:true);

                        var specialId = charRel.SpecialSkill.Id;
                        if (specialId != -1 && charRel.SpecialSkill.SkillId == 20)
                        {
                            charRel.SpecialSkill.nextAttackDmgPercent = charRel.SpecialSkill.Value;
                        }

                        charRel.SpecialSkill.isCrit = true;
                        break;
                    }
                    //Thôi miên
                    case 22:
                    {
                        if(character.InfoChar.Gender != 0) return;
                        if(listMonster[0] != null && listMonster[0].IsMobMe) return;
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackMonster(character.Id, listMonster,skillData.SkillId));
                        HandleThoiMienMonster(character, skillData, listMonster);

                        var specialId = charRel.SpecialSkill.Id;
                        if (specialId != -1 && charRel.SpecialSkill.SkillId == 22)
                        {
                            charRel.SpecialSkill.nextAttackDmgPercent = charRel.SpecialSkill.Value;
                        }
                        break;
                    }
                    //Skill trói xayda
                    case 23:
                    {
                        if(character.InfoChar.Gender != 2) return;
                        if(listMonster[0] != null && listMonster[0].IsMobMe) return;
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackMonster(character.Id, listMonster,skillData.SkillId));
                        HandleTroiMonster(character, skillData, listMonster);

                        var specialId = charRel.SpecialSkill.Id;
                        if (specialId != -1 && charRel.SpecialSkill.SkillId == 23)
                        {
                            charRel.SpecialSkill.nextAttackDmgPercent = charRel.SpecialSkill.Value;
                        }
                        break;
                    }
                    default:
                    {
                                               zone.ZoneHandler.SendMessage(Service.PlayerAttackMonster(character.Id, listMonster,skillData.SkillId));
                        HandlePlayerAttackMonster(character, skillChar, skillData, listMonster);
                        break;
                    }
                }
                character.CharacterHandler.MineStamina(1);
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error AttackMonster in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
            finally
            {
                message?.CleanUp();
            }
        }
        //Disciple attack
        public static void AttackMonster(Disciple disciple, SkillCharacter skillChar, int modId, SkillTemplate skillTemplate = null, SkillDataTemplate skillData = null)
        {
            try
            {
                if (disciple.InfoSkill.TaiTaoNangLuong.IsTTNL)
                {
                    RemoveTTNL(disciple);
                }
                if (disciple.InfoChar.Stamina <= 0)
                {
                    disciple.Character.CharacterHandler.SendMessage(Service.PublicChat(disciple.Id, TextServer.gI().NOT_ENOUGH_STAMINA_DISCIPLE));
                    return;
                }

                if (skillChar == null) return;
                var zone = disciple.Zone;
                if (zone == null) return;
                skillTemplate ??= Cache.Gi().SKILL_TEMPLATES.FirstOrDefault(skl => skl.Id == skillChar.Id);
                if (skillTemplate == null) return;
                skillData ??= skillTemplate?.SkillDataTemplates.FirstOrDefault(skl => skl.SkillId == skillChar.SkillId);
                if (skillData == null) return;
                //Check mana
                var manaUse = skillData.ManaUse;
                var manaUseType = skillTemplate.ManaUseType;
                var manaChar = disciple.InfoChar.Mp;
                manaUse = manaUseType switch
                {
                    1 => manaUse * (int) disciple.MpFull / 100,
                    2 => (int) manaChar,
                    _ => manaUse
                };

      
                var timeServer = ServerUtils.CurrentTimeMillis();
                if (manaUse > manaChar || skillChar.CoolDown > timeServer) return;
                else
                {
                    disciple.CharacterHandler.MineMp(manaUse);
                    if (skillChar.Id <= 5)
                    {
                        skillChar.CoolDown = 1000;
                    }
                    else 
                    {
                        skillChar.CoolDown = (skillData.CoolDown/2) + timeServer;
                    }
                }

                //get monster id
                var monsterAtt = zone.ZoneHandler.GetMonsterMap(modId) ?? (IMonster) zone.ZoneHandler.GetMonsterPet(modId);

                var listMonster = new List<IMonster>();
                if (monsterAtt is not {IsDie: false}) return;
                listMonster.Add(monsterAtt);
                //Handling player attack with skill
                switch (skillTemplate.Id)
                {
                    //kaioken
                    case 9:
                    {
                        var hpMine = disciple.HpFull / 10;
                        if (hpMine >= disciple.InfoChar.Hp)
                        {
                            disciple.Character.CharacterHandler.SendMessage(Service.PublicChat(disciple.Id, TextServer.gI().NOT_ENOUGH_HP_DISCIPLE));
                            return;
                        }
                        disciple.CharacterHandler.MineHp(hpMine);
                        disciple.CharacterHandler.SendMessage(Service.PlayerLevel(disciple));
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackMonster(disciple.Id, listMonster, skillData.SkillId));
                        HandlePlayerAttackMonster(disciple, skillChar, skillData, listMonster);
                        break;
                    } 
                    //QCKK
                    case 10:
                    {
                        if(disciple.InfoSkill.Qckk.Time > timeServer) return;
                        var damage = ServerUtils.RandomNumber(disciple.DamageFull * 9 / 10, disciple.DamageFull);
                        damage *= (skillData.Damage + disciple.InfoSkill.Qckk.ListId.Count*10) / 100;
                        if (disciple.InfoSet.IsFullSetKirin)
                        {
                            damage*=3;
                        }
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackMonster(disciple.Id, listMonster, skillData.SkillId));
                        HandlePlayerAttackMonster(disciple, skillChar, skillData, listMonster, damage:damage);
                        break;
                    }
                    //Makankosappo
                    case 11:
                    {
                        if (disciple.InfoSkill.Laze.Time > timeServer || !disciple.InfoSkill.Laze.Hold) return;
                        long damage = (long)((long)manaUse*skillData.Damage / 100);
                        disciple.InfoSkill.Laze.Hold = false;
                        if (disciple.InfoSet.IsFullSetPicolo)
                        {
                                damage += damage * 2;
                        }
                            if (disciple.ItemBody[5].Id == 1273)
                            {
                                damage += damage * 6;
                            }
                            Server.Gi().Logger.Debug($"Check ---------------------- attack monster by Makankosappo with damage: {damage}");
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackMonster(disciple.Id, listMonster, skillData.SkillId));
                        HandlePlayerAttackMonster(disciple, skillChar, skillData, listMonster, damage:Math.Abs((long)damage));
                        break;
                    }
                    //Biến Sôcôla
                    case 18:
                    {
                        if(listMonster[0] != null && listMonster[0].IsMobMe) return;
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackMonster(disciple.Id, listMonster,skillData.SkillId));
                        HandleSocolaMonster(disciple, listMonster, skillData);
                        break;
                    }
                    //Dich chuyen tuc thoi
                    case 20:
                    {
                        if (listMonster[0] != null && !listMonster[0].IsMobMe)
                        {
                            HandleDichChuyenMonster(disciple, listMonster, skillData);
                        }
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackMonster(disciple.Id, listMonster,skillData.SkillId));
                        HandlePlayerAttackMonster(disciple, skillChar, skillData, listMonster);
                        break;
                    }
                    //Thôi miên
                    case 22:
                    {
                        if(listMonster[0] != null && listMonster[0].IsMobMe) return;
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackMonster(disciple.Id, listMonster,skillData.SkillId));
                        HandleThoiMienMonster(disciple, skillData, listMonster);
                        break;
                    }
                    //Skill trói xayda
                    case 23:
                    {
                        if(listMonster[0] != null && listMonster[0].IsMobMe) return;
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackMonster(disciple.Id, listMonster,skillData.SkillId));
                        HandleTroiMonster(disciple, skillData, listMonster);
                        break;
                    }
                    default:
                    {
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackMonster(disciple.Id, listMonster,skillData.SkillId));
                        HandlePlayerAttackMonster(disciple, skillChar, skillData, listMonster);
                        break;
                    }
                }
                disciple.CharacterHandler.MineStamina(1);
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error AttackMonster in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }

        public static void HandlePlayerAttackMonster(ICharacter character, SkillCharacter skillChar, SkillDataTemplate skillDataTemplate, IEnumerable<IMonster> monsters, long damage = 0, bool isCrit = false)  
        {
            // Update giáp luyện tập
            character.CharacterHandler.UpdateLuyenTap();
            // Tính Damage
            Model.Character.Character charRel = null;
            if (character.Id > 0)
            {
                charRel = (Model.Character.Character)character;
            }
            
            var timeServer = ServerUtils.CurrentTimeMillis();
            if (damage == 0)
            {
                var dmgFull = character.DamageFull;

                // Kiểm tra xem có bùa mạnh mẽ không
                if (character.Id > 0 && charRel.InfoMore.BuaManhMe)
                {
                    if (charRel.InfoMore.BuaManhMeTime > timeServer)
                    {
                        dmgFull = dmgFull*150/100;
                    }
                    else 
                    {
                        charRel.InfoMore.BuaManhMe = false;
                    }
                }

                damage = ServerUtils.RandomNumber(dmgFull * 9 / 10, dmgFull);
                damage = damage*skillDataTemplate.Damage / 100;

                // Nội tại các chiêu đấm
                if (character.Id > 0)
                {
                    if (skillChar == null) 
                    {
                        skillChar = character.Skills[0];
                    }
                    var specialId = charRel.SpecialSkill.Id;
                    var listSkChuong = new List<int> { 1,3,5};
                    if (listSkChuong.Contains(skillChar.Id))
                    {
                    //    character.DataBoMong.Count[5]++;
                    }
                    if (specialId != -1 && DataCache.SpecialSkillTSD.Contains(charRel.SpecialSkill.SkillId) && charRel.SpecialSkill.SkillId == skillChar.Id)
                    {
                        damage += damage*charRel.SpecialSkill.Value/100;
                    }
                    if (skillChar.Id == 1 && character.InfoSet.IsFullSetSongoku)
                    {
                        damage *= 2;
                    }

                    else if (skillChar.Id == 4 && character.InfoSet.IsFullSetKakarot)
                    {
                        damage *= 2;
                    }
                    else if (skillChar.Id == 17 && character.InfoSet.IsFullSetZelot)
                    {
                        damage += damage + (damage * (15/100));
                    }
                }
                
                if (character.ItemBody[5] != null)
                {
                    switch (character.ItemBody[5].Id)
                    {
                        case 610 or 611:
                            var xkame = character.ItemBody[5].Options.FirstOrDefault(i => i.Id == 159);
                            if ((skillChar.Id == 1 || skillChar.Id == 3 || skillChar.Id == 5) && xkame != null && timeServer >= charRel.Delay.timeDelayXKame)
                            {
                                charRel.Delay.timeDelayXKame = 60000 + timeServer;
                                damage *= xkame.Param;
                            }
                            break;
                        case 1271:
                            if (skillChar.Id == 4)
                            {
                                damage *= 2;
                            }
                            break;
                        case 1272:
                            if (skillChar.Id == 17)
                            {
                                damage *= 2;
                            }
                            break;
                        default:
                            var xDamageOnMonster = character.ItemBody[5].Options.FirstOrDefault(i => i.Id == 19);
                            if (xDamageOnMonster != null)
                            {
                                damage += damage * xDamageOnMonster.Param / 100;
                            }
                            var CongDonDam = character.ItemBody[5].Options.FirstOrDefault(i => i.Id == 156);
                            if (CongDonDam != null && character.InfoOption.PhanTramTangSatThuongDam < 50 && (skillChar.Id == 0 || skillChar.Id == 2 || skillChar.Id == 4))
                            {
                                damage += damage * (character.InfoOption.PhanTramTangSatThuongDam / 100);
                            }
                            break;
                    }
                }
            }

            if (character.InfoSkill.ThoiMien.TimePercent > 0 &&
                character.InfoSkill.ThoiMien.TimePercent > timeServer)
            {
                var reduceDamage = damage * character.InfoSkill.ThoiMien.Percent / 100;
                if (damage <= reduceDamage)
                {
                    damage = 0;
                }
                else 
                {
                    damage -= reduceDamage;
                }
            }
            
            if (character.InfoSkill.TaiTaoNangLuong.Crit > 0)
            {
                character.InfoSkill.TaiTaoNangLuong.Crit--;
                isCrit = true;
            }

            if (character.InfoSkill.Socola.IsSocola)
            {
                var reduceDamage = damage * character.InfoSkill.Socola.Percent / 100;
                if (damage <= reduceDamage)
                {
                    damage = 0;
                }
                else 
                {
                    damage -= reduceDamage;
                }
            }

            if (character.InfoSkill.Socola.IsCarot)
            {
                damage = 1;
            }

            if (ServerUtils.RandomNumber(115) < character.CritFull)
            {
                isCrit = true;
            }

            // Nội tại đòn đánh tiếp theo
            if (character.Id > 0 && DataCache.SpecialSkillTSD.Contains(skillChar.Id))
            {
                var specialId = charRel.SpecialSkill.Id;
                if (charRel.SpecialSkill.nextAttackDmgPercent > 0)
                {
                    damage += damage*charRel.SpecialSkill.nextAttackDmgPercent/100;
                    charRel.SpecialSkill.nextAttackDmgPercent = 0;
                }
                // Nội tại chí mạng liên tục
                if (specialId == 9 || specialId == 20 || specialId == 30)
                {
                    var hpFull = character.HpFull;
                    var hpNow = character.InfoChar.Hp;
                    var percentHp = 100 - ((int)((hpFull- hpNow)*100/character.HpFull));
                    if (percentHp <= charRel.SpecialSkill.Value)
                    {
                        isCrit = true;
                    }
                    else 
                    {
                        isCrit = false;
                    }
                }

                if (charRel.SpecialSkill.isCrit)
                {
                    isCrit = true;
                    charRel.SpecialSkill.isCrit = false;
                }
            }

            // Kiểm tra phải đệ tử gây damage không
            // Kiểm tra xem có bùa đệ tử không
            if (character.Id < 0 && damage > 0)
            {
                var discipleReal = (Model.Character.Disciple)character;
                if (discipleReal.Character.InfoMore.BuaDeTu)
                {
                    if (discipleReal.Character.InfoMore.BuaDeTuTime > timeServer)
                    {
                        damage *= 2;
                    }
                    else 
                    {
                        discipleReal.Character.InfoMore.BuaDeTu = false;
                    }
                }
            }

            int damageMonsterAfterHandle = 0;
            
            monsters.Where(monster => monster is {IsDie: false})
                .ToList()
                .ForEach(monster =>
            {
                lock (monster)
                {
                    if (!monster.IsMobMe)
                    {
                        if(Math.Abs(character.InfoChar.X - monster.X) > skillDataTemplate.Dx && Math.Abs(character.InfoChar.Y - monster.Y) > skillDataTemplate.Dy) 
                        {
                            return;
                        }
                    }
                    else
                    {
                        if(Math.Abs(character.InfoChar.X - monster.X) > skillDataTemplate.Dx) return;
                        var charMonster = (Model.Character.Character)character.Zone.ZoneHandler.GetCharacter(monster.IdMap);
                        if(charMonster == null) return;
                        if (charMonster.Id > 0)
                        {
                            if(charMonster.Test.IsTest && charMonster.Test.TestCharacterId != character.Id) return;
                        }
                        else
                        {
                            if(charMonster.Test.IsTest && charMonster.Test.TestCharacterId != -character.Id) return;
                        }
                        
                        if(charMonster.InfoChar.TypePk == 0 && charMonster.Flag == 0) return;
                        if(charMonster.Flag != 0 && charMonster.Flag != 8 && charMonster.Flag == character.Flag) return;
                    }

                    //if (monster.LvBoss == 1)
                    //{
                    //    if (damage >= monster.HpMax / 20)
                    //        damage = (int)monster.HpMax / 20;
                    //}

                    if(skillChar != null && monster.InfoSkill.PlayerTroi.IsPlayerTroi && monster.InfoSkill.PlayerTroi.TimeTroi > timeServer &&DataCache.SpecialSkillTSD.Contains(skillChar.Id))
                    {
                        isCrit = true;
                    }

                    if (isCrit && monster.LvBoss == 0)
                    {
                        damage += damage;
                    }

                    var percentMiss = ServerUtils.RandomNumber(100);

                    if (percentMiss < 10)
                    {

                        damage = 0;
                    }
                    // if (Math.Abs(character.InfoChar.X - monster.X) < (skillDataTemplate.Dx+10))
                    // {
                    // }
                    // else if(percentMiss < 30)
                    // `
                    //     damage = 0;
                    // }
                    damageMonsterAfterHandle = monster.MonsterHandler.UpdateHp(damage, character.Id);
                    
                    if (damageMonsterAfterHandle > 0)
                    {
                        var hpPlus = damageMonsterAfterHandle * character.HpPlusFromDamage / 100;
                        var mpPlus = damageMonsterAfterHandle * character.MpPlusFromDamage / 100;
                        var hpPlusMonster = damageMonsterAfterHandle * character.HpPlusFromDamageMonster / 100;

                        hpPlus += hpPlusMonster > 0 ? hpPlusMonster : 0;

                        if (hpPlus > 0)
                        {
                            character.CharacterHandler.PlusHp(hpPlus);
                           // if (character.Id > 0)
                         //   {
                                character.CharacterHandler.SendMessage(Service.SendHp((int)character.InfoChar.Hp));
                          //  }
                            character.CharacterHandler.SendZoneMessage(Service.PlayerLevel(character));
                        }

                        if (mpPlus > 0)
                        {
                            character.CharacterHandler.PlusMp(mpPlus);
                         //   if (character.Id > 0)
                       //     {
                                character.CharacterHandler.SendMessage(Service.SendMp((int)character.InfoChar.Mp));
                          //  }
                        }
                    }

                    // Nội tại giảm thời gian hồi chiêu
                    if (character.Id > 0)
                    {
                        var specialId = charRel.SpecialSkill.Id;
                        if (skillChar == null) return;
                        if (specialId != -1 && DataCache.SpecialSkillTGHP.Contains(charRel.SpecialSkill.SkillId) && charRel.SpecialSkill.SkillId == skillChar.Id)
                        {
                            var thoiGianHoiChieu = skillDataTemplate.CoolDown - ((skillDataTemplate.CoolDown * charRel.SpecialSkill.Value) / 100);
                            skillChar.CoolDown = thoiGianHoiChieu + timeServer;
                            if (skillChar.CoolDown < 0)
                            {
                                skillChar.CoolDown = 500;
                            }
                            character.CharacterHandler.SendMessage(Service.UpdateCooldown(character));
                        }
                    }

                    if (monster.IsMobMe)
                    {
                        character.CharacterHandler.SendZoneMessage(Service.UpdateMonsterMe5(character.Id, monster.IdMap, skillDataTemplate.SkillId, damageMonsterAfterHandle, (int)monster.Hp));
                    }
                    else
                    {
                        monster.MonsterHandler.AddPlayerAttack(character, damageMonsterAfterHandle);
                        character.CharacterHandler.SendZoneMessage(Service.MonsterHp(monster, isCrit, damageMonsterAfterHandle, (character.HpPlusFromDamage > 0 ? 37 : character.HpPlusFromDamageMonster > 0 ? 37 : - 1)));
                        character.CharacterHandler.PlusTiemNang(monster, damageMonsterAfterHandle);
                        if (character.InfoOption.HieuUngLua && ServerUtils.RandomNumber(100) < 30)
                        {
                            async void Action()
                            {
                                await Task.Delay(1000);
                                if (!monster.IsDie)
                                {
                                    monster.MonsterHandler.UpdateHp(damageMonsterAfterHandle / 2, character.Id);
                                    monster.MonsterHandler.AddPlayerAttack(character, damageMonsterAfterHandle);
                                    character.CharacterHandler.SendZoneMessage(Service.MonsterHp(monster, isCrit, damageMonsterAfterHandle, 14));
                                    character.CharacterHandler.PlusTiemNang(monster, damageMonsterAfterHandle);
                                }
                            }
                            var task = new Task(Action);
                            task.Start();
                        }
                    }
                    
                    //Monster Pet
                    var pet = character.InfoSkill.Egg.Monster;
                    if (pet is {IsDie: false})
                    {
                        int damageMonsterPetAfterHandle = pet.MonsterHandler.PetAttackMonster(monster);
                        character.CharacterHandler.PlusTiemNang(monster, damageMonsterPetAfterHandle);
                    }

                    if (monster.IsDie)
                    {
                        if (monster.Id == 70)
                        {
                            monster.MonsterHandler.Recovery();
                            if (monster.TypeHidru < 2)
                            {
                                monster.TypeHidru++;
                                switch (monster.TypeHidru)
                                {
                                    case 1:
                                        monster.Zone.ZoneHandler.SendMessage(monster.MonsterHandler.Hidru(6)); // hoa vang
                                        break;
                                    case 2:
                                        monster.Zone.ZoneHandler.SendMessage(monster.MonsterHandler.Hidru(5)); // cụt đầu
                                        break;
                                }
                            }
                            else
                            {
                                monster.Zone.ZoneHandler.SendMessage(Service.MonsterDie(monster.IdMap));
                                monster.Hp = 0;
                                monster.IsDie = true;
                                monster.Status = 0;
                                monster.MonsterHandler.RemoveEffect(ServerUtils.CurrentTimeMillis(), true);
                                monster.IsRefresh = false;
                            }

                        }
                        if (character.Id > 0)
                        {
                            if (character.InfoChar.MapId == 148 && ClanManager.Get(character.ClanId) != null && character.Zone.ZoneHandler.GetCountMob() <= 0)
                            {
                                async void DrLychee()
                                {
                                    await Task.Delay(3000);
                                    var dr = new Boss();
                                    dr.CreateBoss(23);
                                    dr.CharacterHandler.SetUpInfo();
                                    ClanManager.Get(character.ClanId).Gas.GasMaps[4].Zones[0].ZoneHandler.AddBoss(dr);
                                    await Task.Delay(3000);
                                    dr.InfoChar.TypePk = 5;
                                    dr.Zone.ZoneHandler.SendMessage(Service.ChangeTypePk(dr.Id, 5));
                                }
                                var task = new Task(DrLychee);
                                task.Start();
                            }
                            //var listMobBay = new List<int> { 28,29,30,31,7,8,9,10,11,12,37,43,50};
                            //if (listMobBay.Contains(monster.Id))
                            //{
                            //    //character.DataBoMong.Count[7]++;
                            //}
                            //if (monster.Id == 0)
                            //{
                            //    //character.DataBoMong.Count[8]++;
                            //}
                            // if (DataCache.IdMapSpecial.Contains(monster.Zone.Map.Id))
                            // {
                            //if (monster.Zone.Map.Id == 148 && monster.Zone.MonsterMaps.Count <= 0) ClanManagerr.Get(character.ClanId).Gas.InitDrLyche(ClanManagerr.Get(character.ClanId).Gas.Level);
                            // }
                            monster.MonsterHandler.LeaveItem(character);

                            //task
                            TaskHandler.gI().CheckTaskDoneKillMob(character, (MonsterMap)monster);
                        }
                    }
                }
            });

           

        }
        #endregion

        #region Attack player
        public static void AttackPlayer(ICharacter character, Message message)
        {
            var charReal = (Model.Character.Character)character;
            var ver = int.Parse(character.Player.Session.Version.Replace(".", ""));

            try
            {
                if (character.InfoSkill.TaiTaoNangLuong.IsTTNL) RemoveTTNL(character);
                
                if (character.InfoChar.IsDie) return;
                
                if (character.InfoChar.Stamina <= 0)
                {
                    character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_STAMINA));
                    return;
                }

                if (character.InfoChar.CSkill == -1)
                {
                    character.InfoChar.CSkill = (short) (character.InfoChar.Gender * 2);
                }

                var skillChar = character.Skills.FirstOrDefault(skl => skl.Id == character.InfoChar.CSkill);
                if (skillChar == null) return;
                var zone = character.Zone;
                if (zone == null) return;

                var skillTemplate = Cache.Gi().SKILL_TEMPLATES.FirstOrDefault(skl => skl.Id == skillChar.Id);
                
                if (skillChar.Id == 11 && !character.InfoSkill.Laze.Hold)
                {
                    return;
                }

                var skillData =
                    skillTemplate?.SkillDataTemplates.FirstOrDefault(skl => skl.SkillId == skillChar.SkillId);
                if (skillData == null) return;
                // SkillDataTemplate skillData = skillDataReal.Clone();
                // Xử lý nội tại
                // skillData = HandleSpecialSkill(character, skillTemplate, skillData);
                //Check mana
                var manaUse = skillData.ManaUse;
                var manaUseType = skillTemplate.ManaUseType;
                var manaChar = character.InfoChar.Mp;
                manaUse = manaUseType switch
                {
                    1 => manaUse * (int) character.MpFull / 100,
                    2 => (int) manaChar,
                    _ => manaUse
                };

                var timeServer = ServerUtils.CurrentTimeMillis();
                if (manaUse > manaChar || skillChar.CoolDown > timeServer) return;
                else
                {
                    character.CharacterHandler.MineMp(manaUse);
                    skillChar.CoolDown = skillData.CoolDown + timeServer;
                }

                var listPlayer = new List<ICharacter>();
                var charId = message.Reader.ReadInt();
                var dir = ver >= 231 ? message.Reader.ReadByte() : -1;
                ICharacter charAtt = null;

                if (charId > 0)
                {
                    if (CharIdIsBoss(charId)) 
                    {
                        charAtt = zone.ZoneHandler.GetBoss(charId);
                    }
                    else 
                    {
                        charAtt = zone.ZoneHandler.GetCharacter(charId);
                    }
                }
                else 
                {
                    charAtt = zone.ZoneHandler.GetDisciple(charId);
                }
                if (charAtt == null) return;
                listPlayer.Add(charAtt);

                var fightSize = 1;
                while ((message.Reader.Available() > 1 && ver >= 231) || (message.Reader.Available() > 0 && ver < 231))
                {
                    var charNextId = message.Reader.ReadInt();
                    var charNextDir = message.Reader.ReadByte();
                    var charNext = zone.ZoneHandler.GetCharacter(charNextId);
                    if (CharIdIsBoss(charNextId)) 
                    {
                        charNext = zone.ZoneHandler.GetBoss(charNextId);
                    }
                    if (charNext == null || charNext.InfoChar.IsDie) continue;
                    fightSize += 1;
                    if (fightSize >= skillData.MaxFight)
                    {
                        break;
                    }
                    listPlayer.Add(charNext);
                }
                //Handling player attack with skill
              ///  Server.Gi().Logger.Debug($"Client: {character.Player.Session.Id} --------------- AttackPlayer - SkillTemplate: {skillTemplate.Id}");
                //Check skill
                
                // if (charReal != null && charReal.Flag == 0 && !charReal.Test.IsTest && skillTemplate.Id != 7 && !CharIdIsBoss(charId)) return;
                switch (skillTemplate.Id)
                {
                    //Trị thương
                    case 7:
                    {
                        if(character.InfoChar.Gender != 1) return;
                        SendZoneSkillAttackPlayer(character, Service.PlayerAttackPlayer(character.Id, listPlayer,skillData.SkillId));
                        if (charId > 0)
                        {
                            HandleHealPlayer(character, skillData, listPlayer);
                        }
                        else 
                        {
                            HandleHealDisciple(character, skillData, listPlayer);
                        }

                        // Nội tại
                        if (character.Id > 0 && !CharIdIsBoss(character.Id))
                        {
                            var charRel = (Model.Character.Character)character;
                            var specialId = charRel.SpecialSkill.Id;
                            if (specialId == 12)
                            {
                                var thoiGianHoiChieu = skillData.CoolDown - ((skillData.CoolDown*charRel.SpecialSkill.Value)/100);
                                skillChar.CoolDown = thoiGianHoiChieu + timeServer;
                                if (skillChar.CoolDown < 0)
                                {
                                    skillChar.CoolDown = 500;
                                }
                                character.CharacterHandler.SendMessage(Service.UpdateCooldown(character));
                            }
                        }
                        break;
                    }
                    //Kaioken
                    case 9:
                    {
                        if(character.InfoChar.Gender != 0) return;
                        var hpMine = character.HpFull / 10;
                        if (hpMine >= character.InfoChar.Hp)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_HP));
                            return;
                        }
                        character.CharacterHandler.MineHp(hpMine);
                        character.CharacterHandler.SendMessage(Service.SendHp((int)character.InfoChar.Hp));
                        zone.ZoneHandler.SendMessage(Service.PlayerLevel(character));
                        SendZoneSkillAttackPlayer(character, Service.PlayerAttackPlayer(character.Id, listPlayer,skillData.SkillId));
                        HandlePlayerAttackPlayer(character, skillChar, skillData, listPlayer);
                        break;
                    }
                    //QCKK
                    case 10:
                    {
                        if (character.InfoChar.Gender != 0 || character.InfoSkill.Qckk.Time > timeServer) return;
                        var damage = ServerUtils.RandomNumber(character.DamageFull * 9 / 10, character.DamageFull);
                        damage *= (skillData.Damage + character.InfoSkill.Qckk.ListId.Count*10) / 100;
                        if (character.InfoSet.IsFullSetKirin)
                        {
                            damage*=3;
                        }
                            if (character.ItemBody[5] != null && character.ItemBody[5].Id == 1270)
                            {
                                damage *= 6;
                            }
                            SendZoneSkillAttackPlayer(character, Service.PlayerAttackPlayer(character.Id, listPlayer,skillData.SkillId));
                        HandlePlayerAttackPlayer(character, skillChar, skillData, listPlayer, damage);

                        if (character.Id > 0 && !CharIdIsBoss(character.Id))
                        {
                            var charRel = (Model.Character.Character)character;
                            var specialId = charRel.SpecialSkill.Id;
                            if (specialId == 3)
                            {
                                var thoiGianHoiChieu = skillData.CoolDown - ((skillData.CoolDown*charRel.SpecialSkill.Value)/100);
                                skillChar.CoolDown = thoiGianHoiChieu + timeServer;
                                if (skillChar.CoolDown < 0)
                                {
                                    skillChar.CoolDown = 500;
                                }
                                character.CharacterHandler.SendMessage(Service.UpdateCooldown(character));
                            }
                        }
                        break;
                    } 
                    //Makankosappo
                    case 11:
                    {
                        if(character.InfoChar.Gender != 1 || character.InfoSkill.Laze.Time > timeServer || !character.InfoSkill.Laze.Hold) return;
                        character.InfoSkill.Laze.Hold = false;
                        long damage = (long)((long)manaUse*skillData.Damage / 100);
                        if (character.InfoSet.IsFullSetPicolo)
                        {
                                damage += damage * 2;
                        }
                        SendZoneSkillAttackPlayer(character, Service.PlayerAttackPlayer(character.Id, listPlayer, skillData.SkillId));
                        HandlePlayerAttackPlayer(character, skillChar, skillData, listPlayer, Math.Abs(((long)damage)));

                        if (character.Id > 0 && !CharIdIsBoss(character.Id))
                        {
                            var charRel = (Model.Character.Character)character;
                            var specialId = charRel.SpecialSkill.Id;
                            if (specialId == 13)
                            {
                                var thoiGianHoiChieu = skillData.CoolDown - ((skillData.CoolDown*charRel.SpecialSkill.Value)/100);
                                skillChar.CoolDown = thoiGianHoiChieu + timeServer;
                                if (skillChar.CoolDown < 0)
                                {
                                    skillChar.CoolDown = 500;
                                }
                                character.CharacterHandler.SendMessage(Service.UpdateCooldown(character));
                            }
                        }
                        break;
                    }
                    //Biến Sôcôla
                    case 18:
                    {
                        if(character.InfoChar.Gender != 1) return;
                        SendZoneSkillAttackPlayer(character, Service.PlayerAttackPlayer(character.Id, listPlayer,skillData.SkillId));
                        HandleSocolaPlayer(character, listPlayer, skillData);

                        // Kiểm tra có nội tại Sô cô la không
                        if (character.Id > 0 && !CharIdIsBoss(character.Id))
                        {
                            var charRel = (Model.Character.Character)character;
                            var specialId = charRel.SpecialSkill.Id;
                            if (specialId != -1 && charRel.SpecialSkill.SkillId == 18)
                            {
                                charRel.SpecialSkill.nextAttackDmgPercent = charRel.SpecialSkill.Value;
                            }
                        }
                        break;
                    }
                    //Dịch chuyển tức thời
                    case 20:
                    {
                        if (character.InfoChar.Gender != 0) return;
                        if (listPlayer[0] != null && CharIdIsBoss(listPlayer[0].Id))
                        {
                            HandleDichChuyenBoss(character, listPlayer[0], skillData);
                        }
                        else
                        {
                            HandleDichChuyenPlayer(character, listPlayer, skillData);
                        }
                        SendZoneSkillAttackPlayer(character, Service.PlayerAttackPlayer(character.Id, listPlayer,skillData.SkillId));
                        HandlePlayerAttackPlayer(character, skillChar, skillData, listPlayer, damage: character.DamageFull, isCrit:true);

                        if (character.Id > 0 && !CharIdIsBoss(character.Id))
                        {
                            var charRel = (Model.Character.Character)character;
                            var specialId = charRel.SpecialSkill.Id;
                            if (specialId != -1 && charRel.SpecialSkill.SkillId == 20)
                            {
                                charRel.SpecialSkill.nextAttackDmgPercent = charRel.SpecialSkill.Value;
                            }
                            charReal.SpecialSkill.isCrit = true;
                        }
                        break;
                    }
                    //thôi miên
                    case 22:
                    {
                        if (character.InfoChar.Gender != 0) return;
                        SendZoneSkillAttackPlayer(character, Service.PlayerAttackPlayer(character.Id, listPlayer,skillData.SkillId));
                        if (listPlayer[0] != null && CharIdIsBoss(listPlayer[0].Id))
                        {
                            HandleThoiMienBoss(character, skillData, listPlayer[0]);
                        }
                        else
                        {
                            HandleThoiMienPlayer(character, skillData, listPlayer);
                        }

                        if (character.Id > 0 && !CharIdIsBoss(character.Id))
                        {
                            var charRel = (Model.Character.Character)character;
                            var specialId = charRel.SpecialSkill.Id;
                            if (specialId != -1 && charRel.SpecialSkill.SkillId == 22)
                            {
                                charRel.SpecialSkill.nextAttackDmgPercent = charRel.SpecialSkill.Value;
                            }
                        }
                        break;
                    }
                    //Skill trói xayda
                    case 23:
                    {
                        if (character.InfoChar.Gender != 2) return;
                        SendZoneSkillAttackPlayer(character, Service.PlayerAttackPlayer(character.Id, listPlayer,skillData.SkillId));
                        if (listPlayer[0] != null && CharIdIsBoss(listPlayer[0].Id))
                        {
                            HandleTroiBoss(character, skillData, listPlayer[0]);
                        }
                        else if (listPlayer[0].Id > 0)
                        {
                            HandleTroiPlayer(character, skillData, listPlayer);
                        }
                        else 
                        {
                            // Handle trói dis
                        }

                        if (character.Id > 0 && !CharIdIsBoss(character.Id))
                        {
                            var charRel = (Model.Character.Character)character;
                            var specialId = charRel.SpecialSkill.Id;
                            if (specialId != -1 && charRel.SpecialSkill.SkillId == 23)
                            {
                                charRel.SpecialSkill.nextAttackDmgPercent = charRel.SpecialSkill.Value;
                            }
                        }
                        break;
                    }
                    default:
                    {
                        SendZoneSkillAttackPlayer(character, Service.PlayerAttackPlayer(character.Id, listPlayer, skillData.SkillId));
                        HandlePlayerAttackPlayer(character, skillChar, skillData, listPlayer);
                        break;
                    }
                }
                character.CharacterHandler.MineStamina(3);
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error AttackMonster in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
            finally
            {
                message?.CleanUp();
            }
        }
        // Boss attack
        public static void BossAttackPlayer(ICharacter character, SkillCharacter skillChar, int characterId)
        {
            try
            {
                if (character.InfoSkill.TaiTaoNangLuong.IsTTNL) RemoveTTNL(character);
                
                if (character.InfoChar.IsDie) return;
                
                if (character.InfoChar.Stamina <= 0)
                {
                    character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_STAMINA));
                    return;
                }

                if (skillChar == null) return;
                var zone = character.Zone;
                if (zone == null) return;

                var skillTemplate = Cache.Gi().SKILL_TEMPLATES.FirstOrDefault(skl => skl.Id == skillChar.Id);
                if (skillTemplate == null) return;

                var skillData =
                    skillTemplate?.SkillDataTemplates.FirstOrDefault(skl => skl.SkillId == skillChar.SkillId);

                if (skillData == null) return;
                //Check mana
                var manaUse = skillData.ManaUse;
                var manaUseType = skillTemplate.ManaUseType;
                var manaChar = character.InfoChar.Mp;
                manaUse = manaUseType switch
                {
                    1 => manaUse * (int) character.MpFull / 100,
                    2 => (int) manaChar,
                    _ => manaUse
                };

                var timeServer = ServerUtils.CurrentTimeMillis();
                if (manaUse > manaChar || skillChar.CoolDown > timeServer) return;
                else
                {
                    character.CharacterHandler.MineMp(manaUse);
                    if (skillChar.Id <= 5)
                    {
                        skillChar.CoolDown = 800;
                    }
                    else 
                    {
                        skillChar.CoolDown = (skillData.CoolDown/2) + timeServer;
                    }
                }

                //get monster id
                var charId = characterId;
                var charAtt = zone.ZoneHandler.GetCharacter(charId);
                var listPlayer = new List<ICharacter>();

                if (charAtt == null || charAtt.InfoChar.IsDie) return;
                
                listPlayer.Add(charAtt);
                var fightSize = 1;
                zone.Characters.Values.Where(c => !c.InfoChar.IsDie && c.Id != character.Id && Math.Abs(c.InfoChar.X - character.InfoChar.X) <= skillData.Dx && Math.Abs(c.InfoChar.Y - character.InfoChar.Y) <= 600).ToList().ForEach(temp =>
                {
                    fightSize += 1;
                    if (fightSize < skillData.MaxFight)
                    {
                        listPlayer.Add(temp);
                    }
                });
                //Handling player attack with skill
                //Check skill
                
                switch (skillTemplate.Id)
                {
                    //Kaioken
                    case 9:
                    {
                        var hpMine = character.HpFull / 10;
                        if (hpMine >= character.InfoChar.Hp)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_HP));
                            return;
                        }
                        character.CharacterHandler.MineHp(hpMine);
                        // character.CharacterHandler.SendMessage(Service.SendHp((int)character.InfoChar.Hp));
                        zone.ZoneHandler.SendMessage(Service.PlayerLevel(character));
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackPlayer(character.Id, listPlayer,skillData.SkillId));
                        HandlePlayerAttackPlayer(character, skillChar, skillData, listPlayer);
                        break;
                    }
                    //QCKK
                    case 10:
                    {
                        if (character.InfoSkill.Qckk.Time > timeServer) return;
                        var damage = ServerUtils.RandomNumber(character.DamageFull * 9 / 10, character.DamageFull);
                        damage *= (skillData.Damage + character.InfoSkill.Qckk.ListId.Count*10) / 100;
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackPlayer(character.Id, listPlayer,skillData.SkillId));
                        HandlePlayerAttackPlayer(character, skillChar, skillData, listPlayer, damage);
                        break;
                    } 
                    //Makankosappo
                    case 11:
                    {
                        if(character.InfoSkill.Laze.Time > timeServer) return;
                        long damage = (long)((long)manaUse*skillData.Damage / 100);
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackPlayer(character.Id, listPlayer,skillData.SkillId));
                        HandlePlayerAttackPlayer(character, skillChar, skillData, listPlayer, Math.Abs(((long)damage)));
                        break;
                    }
                    //Biến Sôcôla
                    case 18:
                    {
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackPlayer(character.Id, listPlayer,skillData.SkillId));
                        HandleSocolaPlayer(character, listPlayer, skillData);
                        break;
                    }
                    //Dịch chuyển tức thời
                    case 20:
                    {
                        HandleDichChuyenPlayer(character, listPlayer, skillData);
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackPlayer(character.Id, listPlayer,skillData.SkillId));
                        HandlePlayerAttackPlayer(character, skillChar, skillData, listPlayer, character.DamageFull,isCrit:true);
                        break;
                    }
                    //thôi miên
                    case 22:
                    {
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackPlayer(character.Id, listPlayer,skillData.SkillId));
                        HandleThoiMienPlayer(character, skillData, listPlayer);
                        break;
                    }
                    //Skill trói xayda
                    case 23:
                    {
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackPlayer(character.Id, listPlayer,skillData.SkillId));
                        HandleTroiPlayer(character, skillData, listPlayer);
                        break;
                    }
                    default:
                    {
                        zone.ZoneHandler.SendMessage(Service.PlayerAttackPlayer(character.Id, listPlayer, skillData.SkillId));
                        HandlePlayerAttackPlayer(character, skillChar, skillData, listPlayer);
                        break;
                    }
                }
                character.CharacterHandler.MineStamina(3);
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error AttackMonster in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }
        public static void DiscipleAttackPlayer(ICharacter character, SkillCharacter skillChar, int characterId)
        {
            try
            {
                if (character.InfoSkill.TaiTaoNangLuong.IsTTNL) RemoveTTNL(character);

                if (character.InfoChar.IsDie) return;

                if (character.InfoChar.Stamina <= 0)
                {
                    character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_STAMINA));
                    return;
                }

                if (skillChar == null) return;
                var zone = character.Zone;
                if (zone == null) return;

                var skillTemplate = Cache.Gi().SKILL_TEMPLATES.FirstOrDefault(skl => skl.Id == skillChar.Id);
                if (skillTemplate == null) return;

                var skillData =
                    skillTemplate?.SkillDataTemplates.FirstOrDefault(skl => skl.SkillId == skillChar.SkillId);

                if (skillData == null) return;
                //Check mana
                var manaUse = skillData.ManaUse;
                var manaUseType = skillTemplate.ManaUseType;
                var manaChar = character.InfoChar.Mp;
                manaUse = manaUseType switch
                {
                    1 => manaUse * (int)character.MpFull / 100,
                    2 => (int)manaChar,
                    _ => manaUse
                };

                var timeServer = ServerUtils.CurrentTimeMillis();
                if (manaUse > manaChar || skillChar.CoolDown > timeServer) return;
                else
                {
                    character.CharacterHandler.MineMp(manaUse);
                    if (skillChar.Id <= 5)
                    {
                        skillChar.CoolDown = 800;
                    }
                    else
                    {
                        skillChar.CoolDown = (skillData.CoolDown / 2) + timeServer;
                    }
                }

                //get monster id
                var charId = characterId;
                var charAtt = zone.ZoneHandler.GetCharacter(charId);
                var listPlayer = new List<ICharacter>();

                if (charAtt == null || charAtt.InfoChar.IsDie) return;

                listPlayer.Add(charAtt);
                var fightSize = 1;
                zone.Characters.Values.Where(c => !c.InfoChar.IsDie && c.Id != character.Id && Math.Abs(c.InfoChar.X - character.InfoChar.X) <= skillData.Dx && Math.Abs(c.InfoChar.Y - character.InfoChar.Y) <= 600).ToList().ForEach(temp =>
                {
                    fightSize += 1;
                    if (fightSize < skillData.MaxFight)
                    {
                        listPlayer.Add(temp);
                    }
                });
                //Handling player attack with skill
                //Check skill

                switch (skillTemplate.Id)
                {
                    //Kaioken
                    case 9:
                        {
                            var hpMine = character.HpFull / 10;
                            if (hpMine >= character.InfoChar.Hp)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_HP));
                                return;
                            }
                            character.CharacterHandler.MineHp(hpMine);
                            // character.CharacterHandler.SendMessage(Service.SendHp((int)character.InfoChar.Hp));
                            zone.ZoneHandler.SendMessage(Service.PlayerLevel(character));
                            zone.ZoneHandler.SendMessage(Service.PlayerAttackPlayer(character.Id, listPlayer, skillData.SkillId));
                            HandlePlayerAttackPlayer(character, skillChar, skillData, listPlayer);
                            break;
                        }
                    //QCKK
                    case 10:
                        {
                            if (character.InfoSkill.Qckk.Time > timeServer) return;
                            var damage = ServerUtils.RandomNumber(character.DamageFull * 9 / 10, character.DamageFull);
                            damage *= (skillData.Damage + character.InfoSkill.Qckk.ListId.Count * 10) / 100;
                            zone.ZoneHandler.SendMessage(Service.PlayerAttackPlayer(character.Id, listPlayer, skillData.SkillId));
                            HandlePlayerAttackPlayer(character, skillChar, skillData, listPlayer, damage);
                            break;
                        }
                    //Makankosappo
                    case 11:
                        {
                            if (character.InfoSkill.Laze.Time > timeServer) return;
                            long damage = (long)((long)manaUse * skillData.Damage / 100);
                            zone.ZoneHandler.SendMessage(Service.PlayerAttackPlayer(character.Id, listPlayer, skillData.SkillId));
                            HandlePlayerAttackPlayer(character, skillChar, skillData, listPlayer, Math.Abs(((long)damage)));
                            break;
                        }
                    //Biến Sôcôla
                    case 18:
                        {
                            zone.ZoneHandler.SendMessage(Service.PlayerAttackPlayer(character.Id, listPlayer, skillData.SkillId));
                            HandleSocolaPlayer(character, listPlayer, skillData);
                            break;
                        }
                    //Dịch chuyển tức thời
                    case 20:
                        {
                            HandleDichChuyenPlayer(character, listPlayer, skillData);
                            zone.ZoneHandler.SendMessage(Service.PlayerAttackPlayer(character.Id, listPlayer, skillData.SkillId));
                            HandlePlayerAttackPlayer(character, skillChar, skillData, listPlayer, character.DamageFull, isCrit: true);
                            break;
                        }
                    //thôi miên
                    case 22:
                        {
                            zone.ZoneHandler.SendMessage(Service.PlayerAttackPlayer(character.Id, listPlayer, skillData.SkillId));
                            HandleThoiMienPlayer(character, skillData, listPlayer);
                            break;
                        }
                    //Skill trói xayda
                    case 23:
                        {
                            zone.ZoneHandler.SendMessage(Service.PlayerAttackPlayer(character.Id, listPlayer, skillData.SkillId));
                            HandleTroiPlayer(character, skillData, listPlayer);
                            break;
                        }
                    default:
                        {
                            zone.ZoneHandler.SendMessage(Service.PlayerAttackPlayer(character.Id, listPlayer, skillData.SkillId));
                            HandlePlayerAttackPlayer(character, skillChar, skillData, listPlayer);
                            break;
                        }
                }
                character.CharacterHandler.MineStamina(3);
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error AttackMonster in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }

        public static void HandlePlayerAttackPlayer(ICharacter character, SkillCharacter skillChar, SkillDataTemplate skillDataTemplate, IEnumerable<ICharacter> characters, long damage = 0, bool isCrit = false)
        {
            var player = characters.FirstOrDefault(c => c != null && !c.InfoChar.IsDie &&
                                                        Math.Abs(character.InfoChar.X - c.InfoChar.X) <= skillDataTemplate.Dx
                                                        && Math.Abs(character.InfoChar.Y - c.InfoChar.Y) <= skillDataTemplate.Dy);
            if(player == null) return;
            //if (CharIdIsBoss(player.Id) && player.InfoChar.TypePk == 0)
            //{
            //    return;
            //}
            //if (!CharIdIsBoss(player.Id) && !CharIdIsBoss(character.Id)) //Nếu ko phải   à boss, là người thì kiểm tra
            //{
            //    if (player.InfoChar.TypePk == 0 && CharIdIsBoss(player.Id)) return;
            //    if (player.InfoChar.TypePk == 0 && player.Flag == 0) return;
            //    if (player.Flag == 0 && player.Flag == character.Flag) return;
            //    if (player.InfoChar.TypePk == 0 || (player.Flag == 0 && !CharIdIsBoss(player.Id))
            //    || (player.Flag == character.Flag && !CharIdIsBoss(player.Id) && player.Flag == 0 && character.Flag == 0))
            //    {
            //        return;
            //    }
            //    //if ((player.InfoChar.TypePk == 0 && character.InfoChar.TypePk == 0) || (character.Flag == 0 && player.Flag == 0) || (character.Flag == player.Flag))
            //    //{
            //    //    return;
            //    //}
            //}
            if (!CharIdIsBoss(player.Id) && !CharIdIsBoss(character.Id) && character.Id > 0)
            {
                if (character.Challenge.isChallenge && character.Challenge.PlayerChallengeID != player.Id)
                {
                    Server.Gi().Logger.Print("STATUS PK CAN'T ATTACK | " + player.Id + " | " + character.Challenge.PlayerChallengeID,"manager");
                    return;
                }
                else if ((character.InfoChar.TypePk == 0 && character.Flag == 0) || (character.Flag == player.Flag && character.Flag != 0 && character.Flag != 8))
                {
                    Server.Gi().Logger.Print("FLAG OR TYPEPK IS VALID ALSO LIKE PLAYER ATTACK FLAG", "manager");
                    return;

                }
            }
            var timeServer = ServerUtils.CurrentTimeMillis();

            lock (player)
            {
                if (damage == 0)
                {
                    damage = ServerUtils.RandomNumber(character.DamageFull * 9 / 10, character.DamageFull);
                    damage = damage * skillDataTemplate.Damage / 100;
                    // Nội tại các chiêu đấm
                    if (character.Id > 0 && !CharIdIsBoss(character.Id))
                    {
                        var charRel = (Model.Character.Character)character;
                        if (skillChar == null) return;
                        var specialId = charRel.SpecialSkill.Id;
                        if (specialId != -1 && DataCache.SpecialSkillTSD.Contains(charRel.SpecialSkill.SkillId) && charRel.SpecialSkill.SkillId == skillChar.Id)
                        {
                            damage += damage * charRel.SpecialSkill.Value / 100;
                        }

                        if (skillChar.Id == 1 && character.InfoSet.IsFullSetSongoku)
                        {
                            damage *= 2;
                        }

                        if (skillChar.Id == 4 && character.InfoSet.IsFullSetKakarot)
                        {
                            damage *= 2;
                        }
                        
                        if (skillChar.Id == 17 && character.InfoSet.IsFullSetZelot)
                        {
                            damage += damage + (damage * (15 / 100));
                        }

                        if (character.ItemBody[5] != null)
                        {
                            switch (character.ItemBody[5].Id)
                            {
                                case 610 or 611:
                                    var xkame = character.ItemBody[5].Options.FirstOrDefault(i => i.Id == 159);
                                    if ((skillChar.Id == 1 || skillChar.Id == 3 || skillChar.Id == 5) && xkame != null && timeServer >= charRel.Delay.timeDelayXKame)
                                    {
                                        charRel.Delay.timeDelayXKame = 60000 + timeServer;
                                        damage *= xkame.Param;
                                    }
                                    break;
                                case 1271:
                                    if (skillChar.Id == 4)
                                    {
                                        damage *= 2;
                                    }
                                    break;
                                case 1272:
                                    if (skillChar.Id == 17)
                                    {
                                        damage *= 2;
                                    }
                                    break;
                                default:
                                    var CongDonDam = character.ItemBody[5].Options.FirstOrDefault(i => i.Id == 156);
                                    if (CongDonDam != null && character.InfoOption.PhanTramTangSatThuongDam < 50 && (skillChar.Id == 0 || skillChar.Id == 2 || skillChar.Id == 4))
                                    {
                                        damage += damage * (character.InfoOption.PhanTramTangSatThuongDam / 100);
                                    }
                                    break;
                            }

                        }
                    }
                }


                //if (damage <= player.DefenceFull)
                //{
                //    damage = 0;
                //    //character.CharacterHandler.SendZoneMessage(Service.PublicChat(character.Id, "Sao hắn mạnh thế?"));
                //    //player.CharacterHandler.SendZoneMessage(Service.PublicChat(character.Id, "Yếu !"));
                //}
                //else
                //{
                    damage -= player.DefenceFull;
               // }

                // Xử lý xuyên giáp
                // Cộng lại % giáp xuyên thành damage
                int PhanTramXuyenGiapChuong = character.InfoOption.PhanTramXuyenGiapChuong;
                int PhanTramXuyenGiapCanChien = character.InfoOption.PhanTramXuyenGiapCanChien;

                if (PhanTramXuyenGiapChuong > 0 && DataCache.SkillIdChuong.Contains(skillDataTemplate.SkillId))
                {
                    damage += player.DefenceFull * PhanTramXuyenGiapChuong / 100;
                }
                else if (PhanTramXuyenGiapCanChien > 0 && DataCache.SkillIdCanChien.Contains(skillDataTemplate.SkillId))
                {
                    damage += player.DefenceFull * PhanTramXuyenGiapCanChien / 100;
                }

                // 

                if (character.InfoSkill.ThoiMien.IsThoiMien)
                {
                    var reduceDamage = damage * character.InfoSkill.ThoiMien.Percent / 100;

                    if (damage <= reduceDamage)
                    {
                        damage = 0;
                    }
                    else
                    {
                        damage -= reduceDamage;
                    }
                }

                if (character.InfoSkill.TaiTaoNangLuong.Crit > 0)
                {
                    character.InfoSkill.TaiTaoNangLuong.Crit--;
                    isCrit = true;
                }
                
                //if (player.ItemBody[5] != null)
                //{
                //    if (player.ItemBody[5].Id == 524 || player.ItemBody[5].Id == 525) {
                //        if (skillChar.Id == 1 || skillChar.Id == 3 || skillChar.Id == 5)
                //        {
                //            var optionVoHieuHoaChuong = player.ItemBody[5].Options.FirstOrDefault(option => option.Id == 3);
                //            int pa = (int)damage * optionVoHieuHoaChuong.Param / 100;
                //            player.CharacterHandler.PlusMp(pa);
                //            player.CharacterHandler.SendMessage(Service.MeLoadPoint(player));
                //            damage = 0;
                //        }
                //    }   
                //}
                if (player.InfoOption.VoHieuHoaChuong > 0 && (skillChar.Id == 1 || skillChar.Id == 3 || skillChar.Id == 5))
                {
                    player.CharacterHandler.PlusMp((int)damage * (player.InfoOption.VoHieuHoaChuong / 100));
                    player.CharacterHandler.SetUpInfo();
                    player.CharacterHandler.SendMessage(Service.MeLoadPoint(player));
                    damage = 0;
                }
                if (ServerUtils.RandomNumber(115) < character.CritFull)
                {
                    isCrit = true;
                }

                if(player.InfoSkill.PlayerTroi.IsPlayerTroi && player.InfoSkill.PlayerTroi.TimeTroi > timeServer && DataCache.SpecialSkillTSD.Contains(skillChar.Id))
                {
                    isCrit = true;
                }

                // Nội tại đòn đánh tiếp theo
                if (character.Id > 0 && !CharIdIsBoss(character.Id) && DataCache.SpecialSkillTSD.Contains(skillChar.Id))
                {
                    var charRel = (Model.Character.Character)character;
                    var specialId = charRel.SpecialSkill.Id;
                    if (charRel.SpecialSkill.nextAttackDmgPercent > 0)
                    {
                        damage += damage*charRel.SpecialSkill.nextAttackDmgPercent/100;
                        charRel.SpecialSkill.nextAttackDmgPercent = 0;
                    }
                    // Nội tại chí mạng liên tục
                    if (specialId == 9 || specialId == 20 || specialId == 30)
                    {
                        var hpFull = character.HpFull;
                        var hpNow = character.InfoChar.Hp;
                        var percentHp = 100 - ((int)((hpFull- hpNow)*100/character.HpFull));
                        if (percentHp <= charRel.SpecialSkill.Value)
                        {
                            isCrit = true;
                        }
                        else 
                        {
                            isCrit = false;
                        }
                    }

                    if (charRel.SpecialSkill.isCrit)
                    {
                        isCrit = true;
                        charRel.SpecialSkill.isCrit = false;
                    }
                }

                if (isCrit)
                {
                    if (character.InfoOption.PhanTramSatThuongChiMang > 0)
                    {
                        damage += damage + (damage * (character.InfoOption.PhanTramSatThuongChiMang / 100));
                    }
                    else
                    {
                        damage *= 2;
                    }
                }

                if (character.InfoSkill.Socola.IsSocola && character.InfoSkill.Socola.CharacterId == player.Id &&
                    character.InfoSkill.Socola.Fight > 0)
                {
                    damage = 1;
                    character.InfoSkill.Socola.Fight--;
                }
                else
                {
                    if (character.InfoSkill.Socola.IsSocola)
                    {
                        var reduceDamage = damage*character.InfoSkill.Socola.Percent/100;
                        if (damage <= reduceDamage)
                        {
                            damage = 0;
                        }
                        else 
                        {
                            damage -= reduceDamage;
                        }
                    }

                    if(player.InfoSkill.Protect.IsProtect) {
                        if(player.HpFull < damage){
                            player.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().DROP_PROTECT));
                            RemoveProtect(player);
                            player.CharacterHandler.SendMessage(Service.ItemTime(DataCache.TimeProtect[0], 0));
                            return;
                        }
                        damage = 1;
                    }

                    var percentMiss = ServerUtils.RandomNumber(100);
                    if ((percentMiss < player.InfoOption.PhanTramNeDon && player.InfoOption.PhanTramNeDon != 0) || (percentMiss <= 20))
                    {
                        damage = 0;
                        player.CharacterHandler.SendMessage(Service.PublicChat(player.Id,"Xí hụt"));
                    }
                }

                // Giáp xên
                
                if (CharIdIsBoss(character.Id))
                {
                    var bossReal = (Boss)character;
                    switch (bossReal.Type)
                    {
                        case 1:
                            damage = player.HpFull / 10;
                            break;
                        case 41:
                            damage = player.HpFull / 10;
                            break;
                    }
                }
                // Last dmg boss
                if (CharIdIsBoss(player.Id) && character != null)
                {
                    var bossReal = (Boss)player;
                    var charRel = (Model.Character.Character)character;
                    if (!bossReal.CharacterAttack.Contains(character.Id) && !character.InfoSkill.Socola.IsSocola)
                    {
                        bossReal.CharacterAttack.Add(character.Id);
                    }      
                    switch (bossReal.Type)
                    {
                        case 95:
                        case 96:
                        case 97:
                            character.DameBossBangHoi += damage;
                            break;
                        case 12: damage = 1;
                            break;
                        case 47:
                            if (bossReal.Zone.MonsterMaps.FirstOrDefault(i => i.Id == 22 && !i.IsDie) != null) damage = 0;
                            break;
                        case 36 or 37 or 38 or 39:
                            charRel.PPower++;
                            character.CharacterHandler.SendMessage(Mabu12hService.sendPowerInfo("TL", (short)charRel.PPower, 50, 3));
                            break;
                        case 41:
                            damage = bossReal.HpFull / 100;
                            break;
                        case 83:
                        case 84:
                            damage = 100;
                            break;
                        case 85:
                            damage = 500;
                            break;
                        case 80:
                        case 81:
                        case 64:
                            damage = bossReal.HpFull / 100;
                            if (damage >= bossReal.InfoChar.Hp)
                            {
                                damage = 0;
                                bossReal.InfoChar.TypePk = 0;
                                bossReal.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(bossReal.Id, 0));
                                var item = ItemCache.GetItemDefault(590);
                                var itemmap = new ItemMap(character.Id, item);
                                itemmap.X = bossReal.InfoChar.X;
                                itemmap.Y = bossReal.InfoChar.Y;
                                bossReal.Zone.ZoneHandler.LeaveItemMap(itemmap);
                                bossReal.InfoDelayBoss.AutoPlusHP = 3000 + timeServer;
                            }
                            break;
                        case 43:
                        case 44:
                        case 45:
                            if (damage >= bossReal.InfoChar.Hp)
                            {
                                damage = 0;
                                bossReal.InfoChar.TypePk = 0;
                                bossReal.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(bossReal.Id, 0));
                                bossReal.CharacterHandler.SendZoneMessage(Service.PublicChat(bossReal.Id, "Biến hình"));
                                async void Summon()
                                {
                                    await Task.Delay(2000);
                                    bossReal.Zone.ZoneHandler.RemoveBoss(bossReal);
                                    var boss = new Boss();
                                    boss.CreateBoss(43 + (bossReal.Type - 42), bossReal.InfoChar.X, bossReal.InfoChar.Y);
                                    boss.CharacterHandler.SetUpInfo();
                                    bossReal.Zone.ZoneHandler.AddBoss(boss);
                                    bossReal.CharacterHandler.SendDie();
                                    bossReal.CharacterHandler.LeaveItem(character);
                                }
                                var task = new Task(Summon);
                                task.Start();
                            }
                            break;
                    }
                }
               
                if (character.InfoSkill.Socola.IsSocola) damage = 0;
                if (character.InfoSkill.MaPhongBa.isMaPhongBa) damage = 0;
                if (!CharIdIsBoss(player.Id) && player.Id > 0)
                {
                    var playerRel = (Model.Character.Character)player;
                    if (damage > 0 && playerRel.InfoBuff.GiapXen)
                    {
                        damage -= (damage * 50 / 100);
                    }
                    if (damage > 0 && playerRel.InfoBuff.GiapXen2)
                    {
                        damage -= (damage * 60 / 100);
                    }
                    if (playerRel.Disciple != null && playerRel.Disciple.Zone != null&&(playerRel.Disciple.Status == 1 || playerRel.Disciple.Status == 2))
                    {
                        var disciple = playerRel.Disciple;
                        disciple.CharacterFocus = character;
                        disciple.Zone.ZoneHandler.SendMessage(Service.PublicChat(disciple.Id, "Ơ đcm, thằng này dám pem sư phụ, hộ giáaa !!"));
                    }
                }
                if (damage < 0) damage = 1;
                player.CharacterHandler.MineHp(damage); 
                player.CharacterHandler.SendZoneMessage(Service.PlayerLevel(player));
                character.CharacterHandler.SendZoneMessage(Service.HaveAttackPlayer(player, isCrit, damage, -1));
                
                // Xử lý phản phần trăm sát thương
                int phanTramPhanSatThuong = player.InfoOption.PhanPercentSatThuong;

                if (damage > 0 && phanTramPhanSatThuong > 0)
                {
                    long phanDamage = damage * phanTramPhanSatThuong / 100;
                    if (phanDamage < character.InfoChar.Hp)
                    {
                        character.CharacterHandler.MineHp(phanDamage);
                    }
                    else
                    {
                        if (CharIdIsBoss(character.Id))
                        {
                            var bossReal = (Boss)character;
                            switch (bossReal.Type)
                            {
                                case 41:
                                    bossReal.HpPst += phanDamage;
                                    break;
                            }
                        }
                        character.InfoChar.Hp = 1;
                        phanDamage = 0;
                    }
                    character.CharacterHandler.SendZoneMessage(Service.PlayerLevel(character));
                    player.CharacterHandler.SendZoneMessage(Service.HaveAttackPlayer(character, false, phanDamage, -1));
                }
                var hpPlus = damage * character.HpPlusFromDamage / 100;
                var mpPlus = damage * character.MpPlusFromDamage / 100;
                if(hpPlus > 0) {
                    character.CharacterHandler.PlusHp((int)hpPlus);
                    character.CharacterHandler.SendMessage(Service.SendHp((int)character.InfoChar.Hp));
                    character.CharacterHandler.SendZoneMessage(Service.PlayerLevel(character));
                }

                if(mpPlus > 0) {
                    character.CharacterHandler.PlusMp((int)mpPlus);
                    character.CharacterHandler.SendMessage(Service.SendMp((int)character.InfoChar.Mp));
                }

                //Monster Pet
                var pet = character.InfoSkill.Egg.Monster;
                if (pet is {IsDie: false} && !player.InfoChar.IsDie)
                {
                    pet.MonsterHandler.PetAttackPlayer(player);
                }

                if (player.InfoChar.IsDie)
                {
                    if (character.Id < 0)
                    {
                        var disciple = (Model.Character.Disciple)character;
                        disciple.CharacterFocus = null;
                        player.CharacterHandler.LeaveItem(character);
                        player.CharacterHandler.SendDie();
                    }
                    if (!CharIdIsBoss(player.Id) && !CharIdIsBoss(character.Id))
                    {
                        var charRel = (Model.Character.Character)character;
                        if (TaskHandler.CheckTask(charRel, 17, 0)) TaskHandler.gI().PlusSubTask(charRel, 1);
                        switch (character.InfoChar.MapId)
                        {
                            case 113:
                                ChampionShip.gI().KillPlayer(character, player);
                                break;
                            case 114 or 115 or 117 or 118 or 119 or 120:
                                charRel.PPower += 5;
                                character.CharacterHandler.SendMessage(Mabu12hService.sendPowerInfo("TL", (short)charRel.PPower, 50, 10));
                                break;
                        }



                    }
                    //giết boss
                    if (player != null && CharIdIsBoss(player.Id))
                    {
                        var bossReal = (Boss)player;
                        var charReal = (Model.Character.Character)character;
                        ABoss.gI().BossDied(bossReal);
                        // charReal.DiemSuKien++;
                        TaskHandler.gI().CheckTaskDoneKillBoss(charReal, bossReal.Type);
                        bossReal.KillerId = character.Id;
                        switch (bossReal.Type) {

                            case 95:
                            case 96:
                            case 97:
                                var clan = ClanManager.Get(character.ClanId);
                                if (clan != null)
                                {
                                    clan.ClanBoss.Time = 30000 + ServerUtils.CurrentTimeMillis();
                                    clan.ClanBoss.Level++;
                                    if (clan.ClanBoss.Level >= 3)
                                    {
                                        clan.ClanBoss.Close = true;
                                        character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã chiến thắng, mai hãy quay lại đánh boss tiếp nhé"));
                                    }
                                    clan.Capsule_Bang += 1000;
                                    List<List<long>> Object = new List<List<long>>();
                                    for (int i =0; i < clan.Thành_viên.Count; i++)
                                    {
                                        List<long> Object2 = new List<long>();
                                        var clanMember = ClientManager.Gi().GetCharacter(clan.Thành_viên[i].Id);
                                        if (clanMember != null)
                                        {
                                            Object2.Add(clanMember.Id);
                                            Object2.Add(clanMember.DameBossBangHoi);
                                            Object.Add(Object2);
                                        }
                                    }
                                    Object.Sort((g2, g1) => g2[1].CompareTo(g1[1]));
                                    for (int i2 = 0; i2 < Object.Count; i2++) 
                                    {
                                        var idObject = Object[i2][0];
                                        var CurrentObject = ClientManager.Gi().GetCharacter((int)idObject);
                                        if (CurrentObject != null)
                                        {
                                            var CurrentObjectClanMember = ClanManager.Get(CurrentObject.ClanId).Thành_viên.FirstOrDefault(i => i.Id == idObject);
                                            var CSBCollect = 5 * (i2 <= 4 ? 3 : i2 <= 8 ? 2 : 1) + (idObject == bossReal.KillerId ? 10 : 0);
                                            CurrentObjectClanMember.Capsule_Bang = CurrentObjectClanMember.Capsule_Cá_Nhân += CSBCollect;
                                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận được " + CSBCollect + " Capsule Bang"));
                                        }
                                    }

                                }
                                break; 
                            case 68:
                            case 69:
                            case 70:
                            case 71:
                            case 72:
                                Died_Ring.gI().Kill((Model.Character.Character)character);
                                break;
                            case 73:
                            case 74:
                            case 75:
                            case 76:
                            case 77:
                                {
                                    var bossNext = ClanManager.Get(character.ClanId).cdrd.Bosses[bossReal.Type - 72];
                                    var bossNow = ClanManager.Get(character.ClanId).cdrd.Bosses[bossReal.Type - 73];
                                    character.InfoSkill.ThoiMien.IsThoiMien = true;
                                    character.InfoSkill.ThoiMien.Time = 2800 + ServerUtils.CurrentTimeMillis();
                                    character.CharacterHandler.SendMessage(Service.ItemTime(DataCache.TimeThoiMien[0], 3));
                                    character.CharacterHandler.SendMessage(Service.SkillEffectPlayer(character.Id, character.Id, 1, 40));
                                    bossNext.InfoChar.TypePk = 5;
                                    bossNext.Zone.ZoneHandler.SendMessage(Service.ChangeTypePk(bossNext.Id, 5));
                                    break;
                                }
                            case 78:
                                {
                                    var bossNext = ClanManager.Get(character.ClanId).cdrd.Bosses[bossReal.Type - 72];
                                    bossNext.InfoChar.TypePk = 5;
                                    bossNext.Zone.ZoneHandler.SendMessage(Service.ChangeTypePk(bossNext.Id, 5));
                                    break;
                                }
                            case 36:
                                for (int i = 0; i < 11; i++)
                                {
                                    bossReal.Zone.ZoneHandler.SendMessage(Mabu12hService.NoTrungMabu((byte)(10 * i)));
                                    Thread.Sleep(500);
                                }
                                Thread.Sleep(1500);
                                Mabu12h.gI().InitMabu(bossReal.Zone.Id);
                                break;
                            case 37:
                            case 38:
                            case 39:
                                if (DataCache.IdMapMabu.Contains(bossReal.Zone.Map.Id))
                                {
                                    charReal.PPower+=50;
                                    character.CharacterHandler.SendMessage(Mabu12hService.sendPowerInfo("TL", (short)charReal.PPower, 50, 10));
                                }
                                Thread.Sleep(30000);
                                var bossAgain = new Boss();
                                bossAgain.CreateBoss(bossReal.Type);
                                bossAgain.CharacterHandler.SetUpInfo();
                                bossReal.Zone.ZoneHandler.AddBoss(bossAgain);
                                break;
                           
                           
                            case 1:
                                if (character.InfoChar.ThoiGianTrungMaBu - ServerUtils.CurrentTimeMillis()< 0)
                                {
                                    character.InfoChar.ThoiGianTrungMaBu += (DataCache.TRUNG_MA_BU_TIME + ServerUtils.CurrentTimeMillis());
                                    character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã giết được Super Broly, hãy về nhà và kiểm tra phần thưởng của bạn"));
                                }
                                //    var oldDisciple = charReal.Disciple;
                                //if (oldDisciple != null || DiscipleDB.IsAlreadyExist(-character.Id))
                                //{
                                //    oldDisciple = new Disciple();
                                //    oldDisciple.CreateNewMaBuDisciple(charReal, character.InfoChar.Gender);
                                //    oldDisciple.Player = character.Player;
                                //    oldDisciple.CharacterHandler.SetUpInfo();
                                //    charReal.Disciple = oldDisciple;
                                //    DiscipleDB.Update(oldDisciple);
                                //}
                                //else
                                //{
                                //    var disciple = new Disciple();
                                //    disciple.CreateNewMaBuDisciple(charReal, character.InfoChar.Gender);
                                //    disciple.Player = character.Player;
                                //    disciple.CharacterHandler.SetUpInfo();
                                //    charReal.Disciple = disciple;
                                //    character.InfoChar.IsHavePet = true;
                                //    character.CharacterHandler.SendMessage(Service.Disciple(1, null));
                                //    DiscipleDB.Create(disciple);
                                //}
                                //    character.CharacterHandler.SendMessage(Service.NoTrungMaBu());
                                //    character.InfoChar.ThoiGianTrungMaBu = 0;

                                // character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().GET_NEW_MABU_DISCIPLE));
                                break;
                            case 23:
                                ClanManager.Get(character.ClanId).Gas.InitHachijack(ClanManager.Get(character.ClanId).Gas.Level);
                                break;                          
                            case 52:
                            case 53:
                            case 54:
                            case 55:
                            case 56:
                            case 57:
                            case 58:
                            case 59:
                            case 60:
                            case 61:
                            case 62:
                                ChampionShip_23.gI().Kill(charReal, character.DataDaiHoiVoThuat23.Round);
                                break;

                        }
                        player.CharacterHandler.LeaveItem(character);
                        player.CharacterHandler.SendDie();

                    }
                    // Bị boss giết, hoặc 
                    else if (player != null && character != null && CharIdIsBoss(character.Id))
                    {
                        switch (character.InfoChar.MapId)
                        {
                            case 154:
                                var charRel = (Model.Character.Character)character;
                                charRel.Zone.ZoneHandler.RemoveBoss((Model.Character.Boss)character.Zone.ZoneHandler.GetBossInMap(107)[0]);
                                charRel.DataTraining.DataWhis.Count--;
                                charRel.CharacterHandler.LeaveFromDead(true);
                                break;
                            default:
                                player.CharacterHandler.SendDie();
                                break;
                        }
                    }
                    else //người khác giết
                    {
                        player.CharacterHandler.SendDie();
                        if (player.Id > 0 && character.Id > 0 && !CharIdIsBoss(character.Id))
                        {
                            var charReal = (Model.Character.Character)character;
                            var playerReal = (Model.Character.Character)player;
                            if (playerReal.Enemies.FirstOrDefault(c => c.Id == charReal.Id) == null && !charReal.InfoBuff.AnDanh)
                            {
                                playerReal.Enemies.Add(charReal.Me) ;
                                if (playerReal.Enemies.Count > 30)
                                {
                                    playerReal.Enemies.RemoveAt(0);
                                }
                            }
                        }
                    }
                }

                // Nội tại giảm thời gian hồi chiêu
                if (character.Id > 0 && !CharIdIsBoss(character.Id))
                {
                    var charReal = (Model.Character.Character)character;
                    var specialId = charReal.SpecialSkill.Id;
                    if (skillChar == null) return;
                    if (specialId != -1 && DataCache.SpecialSkillTGHP.Contains(charReal.SpecialSkill.SkillId) && charReal.SpecialSkill.SkillId == skillChar.Id)
                    {
                        var thoiGianHoiChieu = skillDataTemplate.CoolDown - ((skillDataTemplate.CoolDown* charReal.SpecialSkill.Value)/100);
                        skillChar.CoolDown = thoiGianHoiChieu + timeServer;
                        if (skillChar.CoolDown < 0)
                        {
                            skillChar.CoolDown = 500;
                        }
                        character.CharacterHandler.SendMessage(Service.UpdateCooldown(character));
                    }
                }
            }
        }
        #endregion

         #region Remove Skill
        public static void Miễn_Khống_Chế(ICharacter character)
        {
         //   RemoveDichChuyen(character);
         //   RemoveHuytSao(character);
         //   RemoveProtect(character);
           RemoveSocola(character);
            RemoveStone(character);
         //   RemoveThoiMien(character);
         //   RemoveTroi(character);
         //   RemoveTTNL(character);
        }
        public static void RemoveStone(ICharacter character)
        { 

        }
        public static void RemoveTTNL(ICharacter character, int id = 8)
        {
            character.InfoSkill.TaiTaoNangLuong.IsTTNL = false;
            character.InfoSkill.TaiTaoNangLuong.DelayTTNL = -1;
            character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
            character.CharacterHandler.SendZoneMessage(Service.SkillNotFocus3(character.Id, id));
        }

        public static void RemoveProtect(ICharacter character)
        {
            character.InfoSkill.Protect.IsProtect = false;
            character.InfoSkill.Protect.Time = -1;
            character.CharacterHandler.SendZoneMessage(Service.SkillEffectPlayer(character.Id, character.Id, 2, 33));
        }

        public static void RemoveHuytSao(ICharacter character)
        {
            
            character.InfoSkill.HuytSao.IsHuytSao = false;
            character.InfoSkill.HuytSao.Time = -1;
            character.InfoSkill.HuytSao.Percent = 0;
            character.CharacterHandler.SetHpFull();
            if (character.InfoChar.Hp >= character.HpFull)
            {
                character.InfoChar.Hp = character.HpFull;
            }
            character.CharacterHandler.SendMessage(Service.MeLoadPoint(character));
            character.CharacterHandler.SendZoneMessage(Service.PlayerLevel(character));
        }

        public static void RemoveTroi(ICharacter character)
        {
            if(character == null) return;
            if (character.InfoSkill.MeTroi.Monster != null)
            {
                character.InfoSkill.MeTroi.Monster?.MonsterHandler.RemoveTroi(character.Id);
            }
            else 
            {
                character.InfoSkill.MeTroi.Character?.CharacterHandler.RemoveTroi(character.Id);
            }

            character.CharacterHandler.SendZoneMessage(Service.SkillEffectPlayer(character.Id, character.Id, 2, 32));
            character.InfoSkill.MeTroi.IsMeTroi = false;
            character.InfoSkill.MeTroi.Monster = null;
            character.InfoSkill.MeTroi.Character = null;
            character.InfoSkill.MeTroi.DelayStart = -1;
            character.InfoSkill.MeTroi.TimeTroi = -1;
        }

        public static void RemoveDichChuyen(ICharacter character)
        {
            character.CharacterHandler.SendZoneMessage(Service.SkillEffectPlayer(character.Id, character.Id, 2, 40));
            character.InfoSkill.DichChuyen.IsStun = false;
            character.InfoSkill.DichChuyen.Time = -1;
        }

        public static void RemoveThaiDuongHanSan(ICharacter character)
        {
            character.CharacterHandler.SendZoneMessage(Service.SkillEffectPlayer(character.Id, character.Id, 2, 40));
            character.InfoSkill.ThaiDuongHanSan.IsStun = false;
            character.InfoSkill.ThaiDuongHanSan.Time = -1;
            character.InfoSkill.ThaiDuongHanSan.TimeReal = 0;
        }

        public static void RemoveThoiMien(ICharacter character)
        {
            character.CharacterHandler.SendZoneMessage(Service.SkillEffectPlayer(character.Id, character.Id, 2, 41));
            character.InfoSkill.ThoiMien.IsThoiMien = false;
            character.InfoSkill.ThoiMien.Time = -1;
        }
        
        public static void RemoveSocola(ICharacter character)
        {
            character.InfoSkill.Socola.IsSocola = false;
            character.InfoSkill.Socola.IsCarot = false;
            character.InfoSkill.Socola.Time = -1;
            character.InfoSkill.Socola.CharacterId = -1;
            character.InfoSkill.Socola.Fight = 0;
            character.InfoSkill.Socola.Percent = 0;
            character.CharacterHandler.SendZoneMessage(Service.UpdateBody(character));
        }
        public static void RemoveHoaDa(ICharacter character)
        {
            character.InfoSkill.HoaDa.IsHoaDa = false;
            character.InfoSkill.HoaDa.Time = -1;
            character.InfoSkill.HoaDa.CharacterId = -1;
            character.InfoSkill.HoaDa.Fight = 0;
            character.InfoSkill.HoaDa.Percent = 0;
            character.CharacterHandler.SendZoneMessage(Service.UpdateBody(character));
        }

        public static void RemoveMonsterPet(ICharacter character)
        {
            character.InfoSkill.Egg.Time = -1;
            character.InfoSkill.Egg.Monster = null;
        }

        #endregion

        #region Handle Skill
        public static void HandleMonkey(ICharacter character, bool isMonkey)
        {
            try 
            {
                if(character.InfoSkill.Socola.IsSocola) return;
                if (isMonkey)
                {
                    character.InfoSkill.Monkey.MonkeyId = 1;
                    character.InfoSkill.Monkey.BodyMonkey = 193;
                    character.InfoSkill.Monkey.LegMonkey = 194;
                    character.InfoSkill.Monkey.TimeMonkey = DataCache.SkillMonkeys.FirstOrDefault(m => m.Id == character.InfoSkill.Monkey.HeadMonkey)!.Time + ServerUtils.CurrentTimeMillis();
                    character.InfoSkill.Monkey.Damage = DataCache.SkillMonkeys.FirstOrDefault(m => m.Id == character.InfoSkill.Monkey.HeadMonkey)!.Damage;
                    character.InfoSkill.Monkey.Hp = DataCache.SkillMonkeys.FirstOrDefault(m => m.Id == character.InfoSkill.Monkey.HeadMonkey)!.Hp;
                    
                    //if (character.InfoSet.IsFullSetCadic)
                    //{
                    //    character.InfoSkill.Monkey.TimeMonkey *=2;
                    //}
                }
                else
                {
                    character.InfoSkill.Monkey.MonkeyId = 0;
                    character.InfoSkill.Monkey.HeadMonkey = -1;
                    character.InfoSkill.Monkey.BodyMonkey = -1;
                    character.InfoSkill.Monkey.LegMonkey = -1;
                    character.InfoSkill.Monkey.TimeMonkey = -1;
                }
                character.CharacterHandler.SetUpInfo();
                if (isMonkey)
                {
                    character.CharacterHandler.PlusHp((int)character.HpFull);
                    character.CharacterHandler.PlusMp((int)character.MpFull);
                }
                character.CharacterHandler.SendZoneMessage(Service.UpdateBody(character));
                character.CharacterHandler.SendMessage(Service.PlayerLoadSpeed(character));
                character.CharacterHandler.SendMessage(Service.MeLoadPoint(character));
                character.CharacterHandler.SendMessage(Service.SendHp((int)character.InfoChar.Hp));
                character.CharacterHandler.SendMessage(Service.SendMp((int)character.InfoChar.Mp));
                character.CharacterHandler.SendZoneMessage(Service.PlayerLevel(character));
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error HandleTroiMonster in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }

        public static void HandleTuSat(ICharacter character, SkillCharacter skillChar, SkillDataTemplate skillDataTemplate)
        {
            var zone = character.Zone;
            if(zone == null || character.InfoSkill.TuSat.Delay > 0 && character.InfoSkill.TuSat.Delay > ServerUtils.CurrentTimeMillis() || character.InfoSkill.Socola.IsSocola) return;
            long damage = (long)((character.InfoChar.Hp) * character.InfoSkill.TuSat.Damage / 100);

            if (character.InfoSkill.Socola.IsSocola)
            {
                var reduceDamage = damage * character.InfoSkill.Socola.Percent / 100;
                if (damage <= reduceDamage)
                {
                    damage = 0;
                } else 
                {
                    damage -= reduceDamage;
                }
            }

            //Attack monster
            zone.MonsterMaps.Where(monster => monster is {IsDie: false}).ToList().ForEach(monsterMap =>
            {
                lock (monsterMap)
                {
                    var damageMonster = damage;

                    if (monsterMap.LvBoss == 1)
                    {
                        damageMonster = monsterMap.HpMax / 20;
                    }

                    if (character.InfoSkill.ThoiMien.TimePercent > 0 &&
                        character.InfoSkill.ThoiMien.TimePercent > ServerUtils.CurrentTimeMillis())
                    {
                        damageMonster -= damageMonster * character.InfoSkill.ThoiMien.Percent / 100;
                    }
                
                    if (ServerUtils.RandomNumber(100) < 15)
                    {
                        damageMonster = 0;
                    }

                    int damageMonsterAfterHandle = monsterMap.MonsterHandler.UpdateHp(damageMonster, character.Id, true);
                    monsterMap.MonsterHandler.AddPlayerAttack(character, damageMonsterAfterHandle);
                    character.CharacterHandler.SendZoneMessage(Service.MonsterHp(monsterMap, false, damageMonsterAfterHandle, -1));
                    // character.CharacterHandler.PlusPoint(monsterMap, (int)damageMonsterAfterHandle);
                    if (monsterMap.IsDie)
                    {
                        monsterMap.MonsterHandler.LeaveItem(character);
                       
                    }
                }
            });

            zone.MonsterPets.Values.Where(monster => monster is {IsDie: false}).ToList().ForEach(pet =>
            {
                lock (pet)
                {
                    var damageMonster = pet.HpMax / 10;
                    int damageMonsterAfterHandle = pet.MonsterHandler.UpdateHp(damageMonster, character.Id);
                    character.CharacterHandler.SendZoneMessage(Service.MonsterHp(pet, false, damageMonsterAfterHandle, -1));
                }
            });

            // Không biến khỉ 50%
            if (character.InfoSkill.Monkey.MonkeyId == 0)
            {
                damage = (long)(damage*0.5);
            }
            else 
            {
                // Biển khỉ 30% 
                damage = (long)(damage*0.3);
            }

            // Attack Boss
            zone.Bosses.Values.Where(c => !c.InfoChar.IsDie && c.Id != character.Id && c.InfoChar.TypePk == 5).ToList().ForEach(temp =>
            {
                lock (temp)
                {
                    var damagerReal = damage;
                    if (temp.Type == DataCache.BOSS_THO_PHE_CO_TYPE) damage = 1;
                    //Check DoanhTrai
                    if(Math.Abs(temp.InfoChar.X - character.InfoChar.X) <= 900)
                    {
                        var damageChar = damagerReal;

                        //Check skill protect
                        if (temp.InfoSkill.Protect.IsProtect)
                        {
                            if (temp.HpFull <= damageChar)
                            {
                                //HANDLE REMOVE SKILL PROTECT
                                temp.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().DROP_PROTECT));
                                character.CharacterHandler.SendMessage(Service.ItemTime(DataCache.TimeProtect[0], 0));
                                RemoveProtect(temp);
                                return;
                            }
                            damageChar = 1;
                        }

                        if (damageChar >= temp.InfoChar.Hp)
                        {
                            damageChar = temp.InfoChar.Hp;
                        }

                        temp.CharacterHandler.MineHp(damageChar);
                        // temp.CharacterHandler.SendMessage(Service.SendHp((int)temp.InfoChar.Hp));
                        temp.CharacterHandler.SendZoneMessage(Service.PlayerLevel(temp));

                        if (temp.InfoChar.IsDie)
                        {
                            temp.CharacterHandler.SendDie();
                            temp.CharacterHandler.LeaveItem(character);
                            TaskHandler.gI().CheckTaskDoneKillBoss((Model.Character.Character)character, temp.Type);
                            ABoss.gI().BossDied(temp);
                        }

                        if (!((Boss)temp).CharacterAttack.Contains(character.Id) && !(temp.Type >= 52 && temp.Type <= 62))
                        {
                            ((Boss)temp).CharacterAttack.Add(character.Id);
                        }
                    }
                }
            });

            //Attack character
            zone.Characters.Values.Where(c => !c.InfoChar.IsDie && c.Id != character.Id).ToList().ForEach(temp =>
            {
                lock (temp)
                {
                    var damagerReal = damage;
                    var real = (Model.Character.Character) character;
                    var c = (Model.Character.Character) temp;
                   
                     if(Math.Abs(temp.InfoChar.X - character.InfoChar.X) <= 900)
                    {
                        var damageChar = damagerReal;
                        if (character.Flag == 0 || temp.Flag == 0 || temp.Flag == character.Flag && character.Flag != 8)
                        {
                            damageChar = 0;//(int) temp.InfoChar.Hp / 10;
                        }

                        //Check skill protect
                        if (temp.InfoSkill.Protect.IsProtect)
                        {
                            if (temp.HpFull <= damageChar)
                            {
                                //HANDLE REMOVE SKILL PROTECT
                                temp.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().DROP_PROTECT));
                                character.CharacterHandler.SendMessage(Service.ItemTime(DataCache.TimeProtect[0], 0));
                                RemoveProtect(temp);
                                return;
                            }
                            damageChar = 1;
                        }

                        if (damageChar >= temp.InfoChar.Hp)
                        {
                            damageChar = temp.InfoChar.Hp;
                        }

                        // Bổ Huyết
                        if (damageChar > 0 && c.InfoBuff.GiapXen)
                        {
                            damageChar -= (damageChar*50/100);
                        }
                        if (damageChar > 0 && c.InfoBuff.GiapXen2)
                        {
                            damageChar -= (damageChar * 60 / 100);
                        }
                        temp.CharacterHandler.MineHp(damageChar);
                        temp.CharacterHandler.SendMessage(Service.SendHp((int)temp.InfoChar.Hp));
                        temp.CharacterHandler.SendZoneMessage(Service.PlayerLevel(temp));

                        if (temp.InfoChar.IsDie)
                        {
                            temp.CharacterHandler.SendDie();
                            if (c.Enemies.FirstOrDefault(cg => cg.Id == real.Id) == null && !real.InfoBuff.AnDanh)
                            {
                                c.Enemies.Add(real.Me);
                                if (c.Enemies.Count > 30)
                                {
                                    c.Enemies.RemoveAt(0);
                                }
                            }
                        }
                    }
                }
            });

            character.CharacterHandler.MineHp(character.InfoChar.Hp);
            character.CharacterHandler.SendDie();
            character.InfoSkill.TuSat.Delay = -1;

            if (character.Id > 0)
            {
                var charRel = (Model.Character.Character)character;
                var specialId = charRel.SpecialSkill.Id;
                if (specialId == 24)
                {
                    if (skillChar == null) return;
                    var timeServer = ServerUtils.CurrentTimeMillis();
                    var thoiGianHoiChieu = skillDataTemplate.CoolDown - ((skillDataTemplate.CoolDown*charRel.SpecialSkill.Value)/100);
                    skillChar.CoolDown = thoiGianHoiChieu + timeServer;
                    if (skillChar.CoolDown < 0)
                    {
                        skillChar.CoolDown = 500;
                    }
                    character.CharacterHandler.SendMessage(Service.UpdateCooldown(character));
                }
            }
        }
        #endregion

        #region Handle Skill Monster
        public static void HandleTroiMonster(ICharacter character, SkillDataTemplate skillData, List<IMonster> monsters)
        {
            try
            {
                var monster = monsters[0];
                if(monster == null) return;
                if(Math.Abs(character.InfoChar.X - monster.X) > skillData.Dx || Math.Abs(character.InfoChar.Y - monster.Y) > skillData.Dy) return;
                lock (monster)
                {
                    var timeUse = DataCache.TimeTroi[skillData.Point];
                    var timeLong = timeUse * 100 + ServerUtils.CurrentTimeMillis();

                    //Setup monster skill
                    monster.IsDontMove = true;
                    lock (monster.InfoSkill.PlayerTroi)
                    {
                        monster.InfoSkill.PlayerTroi.IsPlayerTroi = true;
                        monster.InfoSkill.PlayerTroi.TimeTroi = timeLong;
                        if (!monster.InfoSkill.PlayerTroi.PlayerId.Contains(character.Id))
                        {
                            monster.InfoSkill.PlayerTroi.PlayerId.Add(character.Id);
                        }
                    }

                    //Setup char skill
                    character.InfoSkill.MeTroi.IsMeTroi = true;
                    character.InfoSkill.MeTroi.Monster = monster;
                    character.InfoSkill.MeTroi.Character = null;
                    character.InfoSkill.MeTroi.DelayStart = 1000 + ServerUtils.CurrentTimeMillis();
                    character.InfoSkill.MeTroi.TimeTroi = timeLong;

                    character.Zone.ZoneHandler.SendMessage(Service.MonsterDontMove(monster.IdMap, true));
                    character.Zone.ZoneHandler.SendMessage(Service.SkillEffectMonster(character.Id, monster.IdMap, 1, 32));
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error HandleTroiMonster in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }

        public static void HandleThoiMienMonster(ICharacter character, SkillDataTemplate skillData, List<IMonster> monsters)
        {
            try
            {
                var monster = monsters[0];
                if(monster == null) return;
                if(Math.Abs(character.InfoChar.X - monster.X) > skillData.Dx || Math.Abs(character.InfoChar.Y - monster.Y) > skillData.Dy) return;
                lock (monster)
                {
                    var timeUse = DataCache.TimeThoiMien[skillData.Point];
                    var timeLong = timeUse * 100 + ServerUtils.CurrentTimeMillis();

                    //Setup monster skill
                    monster.IsDontMove = true;
                    lock (monster.InfoSkill.ThoiMien)
                    {
                        monster.InfoSkill.ThoiMien.IsThoiMien = true;
                        monster.InfoSkill.ThoiMien.Time = timeLong;
                        monster.InfoSkill.ThoiMien.TimePercent = -1;
                        monster.InfoSkill.ThoiMien.Percent = skillData.Damage*5;
                    }

                    character.Zone.ZoneHandler.SendMessage(Service.MonsterDontMove(monster.IdMap, true));
                    character.Zone.ZoneHandler.SendMessage(Service.SkillEffectMonster(character.Id, monster.IdMap, 1, 41));
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error HandleTroiMonster in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }

        public static void HandleSocolaMonster(ICharacter character, List<IMonster> monsters,
            SkillDataTemplate skillData)
        {
            try
            {
                var monster = monsters[0];
                if(monster == null) return;
                if(Math.Abs(character.InfoChar.X - monster.X) > skillData.Dx || Math.Abs(character.InfoChar.Y - monster.Y) > skillData.Dy) return;
                lock (monster)
                {
                    var timeUse = 30000 + ServerUtils.CurrentTimeMillis();

                    //Setup monster skill
                    lock (monster.InfoSkill.Socola)
                    {
                        monster.InfoSkill.Socola.IsSocola = true;
                        monster.InfoSkill.Socola.Time = timeUse;
                        monster.InfoSkill.Socola.CharacterId = character.Id;
                        monster.InfoSkill.Socola.Fight = 0;
                        monster.InfoSkill.Socola.Percent = skillData.Damage;
                    }
                    character.Zone.ZoneHandler.SendMessage(Service.ChangeMonsterBody(1, monster.IdMap, 4132));
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error HandleTroiMonster in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }

        public static void HandleDichChuyenMonster(ICharacter character, List<IMonster> monsters, SkillDataTemplate skillData)
        {
            try
            {
                var monster = monsters[0];
                if(monster == null) return;
                if(Math.Abs(character.InfoChar.X - monster.X) > skillData.Dx || Math.Abs(character.InfoChar.Y - monster.Y) > skillData.Dy) return;
                lock (monster)
                {
                    var timeUse = DataCache.TimeDichChuyen[skillData.Point];
                    var timeLong = timeUse * 100 + ServerUtils.CurrentTimeMillis();

                    //Setup monster skill
                    monster.IsDontMove = true;
                    lock (monster.InfoSkill.DichChuyen)
                    {
                        monster.InfoSkill.DichChuyen.IsStun = true;
                        monster.InfoSkill.DichChuyen.Time = timeLong;
                    }
                    character.InfoChar.X = monster.X;
                    character.InfoChar.Y = monster.Y;
                    character.Zone.ZoneHandler.SendMessage(Service.MonsterDontMove(monster.IdMap, true));
                    character.Zone.ZoneHandler.SendMessage(Service.SkillEffectMonster(character.Id, monster.IdMap, 1, 40));
                    character.Zone.ZoneHandler.SendMessage(Service.SendPos(character, 1));
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error HandleTroiMonster in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }
        #endregion 

        #region handle skill player

        public static void HandleThoiMienPlayer(ICharacter character, SkillDataTemplate skillData, List<ICharacter> characters)
        {
            try
            {
                var player = characters[0];
                if(player == null || player.InfoChar.IsDie) return;
                if(Math.Abs(character.InfoChar.X - player.InfoChar.X) > skillData.Dx || Math.Abs(character.InfoChar.Y - player.InfoChar.Y) > skillData.Dy || character.InfoSkill.Socola.IsSocola) return;

                if (!CharIdIsBoss(character.Id) && character.Id > 0) 
                {
                
                    if(player.Challenge.isChallenge && player.Challenge.PlayerChallengeID != character.Id) return;
                    if(player.InfoChar.TypePk == 0 && player.Flag == 0) return;
                    if(player.Flag != 0 && player.Flag != 8 && player.Flag == character.Flag) return;
                }
                
                lock (player)
                {
                    var timeUse = DataCache.TimeThoiMien[skillData.Point];
                    var timeLong = timeUse * 100 + ServerUtils.CurrentTimeMillis();

                    //Setup monster skill
                    lock (player.InfoSkill.ThoiMien)
                    {
                        player.InfoSkill.ThoiMien.IsThoiMien = true;
                        player.InfoSkill.ThoiMien.Time = timeLong;
                        player.InfoSkill.ThoiMien.TimePercent = -1;
                        player.InfoSkill.ThoiMien.Percent = skillData.Damage*5;
                    }
                    player.CharacterHandler.SendMessage(Service.ItemTime(DataCache.TimeThoiMien[0], timeUse/10));
                    character.Zone.ZoneHandler.SendMessage(Service.SkillEffectPlayer(character.Id, player.Id, 1, 41));
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error HandleTroiMonster in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }
        public static void HandleDichChuyenPlayer(ICharacter character, List<ICharacter> characters, SkillDataTemplate skillData)
        {
            try
            {
                var player = characters[0];
                if(player == null || player.InfoChar.IsDie) return;
                if(Math.Abs(character.InfoChar.X - player.InfoChar.X) > skillData.Dx || Math.Abs(character.InfoChar.Y - player.InfoChar.Y) > skillData.Dy || character.InfoSkill.Socola.IsSocola) return;
                
                if (!CharIdIsBoss(character.Id)) 
                {
                    //if(player.Test.IsTest && player.Test.TestCharacterId != character.Id) return;
                    if(player.InfoChar.TypePk == 0 && player.Flag == 0) return;
                    if(player.Flag != 0 && player.Flag != 8 && player.Flag == character.Flag) return;
                }
                
                lock (player)
                {
                    var timeUse = DataCache.TimeDichChuyen[skillData.Point];
                    var timeLong = timeUse * 100 + ServerUtils.CurrentTimeMillis();

                    //Setup monster skill
                    lock (player.InfoSkill.DichChuyen)
                    {
                        player.InfoSkill.DichChuyen.IsStun = true;
                        player.InfoSkill.DichChuyen.Time = timeLong;
                    }
                    character.InfoChar.X = player.InfoChar.X;
                    character.InfoChar.Y = player.InfoChar.Y;
                    character.Zone.ZoneHandler.SendMessage(Service.SkillEffectPlayer(character.Id, player.Id, 1, 40));
                    character.Zone.ZoneHandler.SendMessage(Service.SendPos(character, 1));
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error HandleTroiMonster in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }
        public static void HandleTroiPlayer(ICharacter character, SkillDataTemplate skillData, List<ICharacter> players)
        {
            try
            {
                var player = players[0];
                if(player == null || player.InfoChar.IsDie) return;
                if(Math.Abs(character.InfoChar.X - player.InfoChar.X) > skillData.Dx || Math.Abs(character.InfoChar.Y - player.InfoChar.Y) > skillData.Dy || character.InfoSkill.Socola.IsSocola) return;
                
                if (!CharIdIsBoss(character.Id)) 
                {
                    //if(player.Test.IsTest && player.Test.TestCharacterId != character.Id) return;
                    if(player.InfoChar.TypePk == 0 && player.Flag == 0) return;
                    if(player.Flag != 0 && player.Flag != 8 && player.Flag == character.Flag) return;
                }
                
                lock (player)
                {
                    if (player.InfoSkill.TaiTaoNangLuong.IsTTNL)
                    {
                        RemoveTTNL(player);
                    }
                    var timeUse = DataCache.TimeTroi[skillData.Point];
                    var timeLong = timeUse * 100 + ServerUtils.CurrentTimeMillis();

                    //Setup monster skill
                    lock (player.InfoSkill.PlayerTroi)
                    {
                        player.InfoSkill.PlayerTroi.IsPlayerTroi = true;
                        player.InfoSkill.PlayerTroi.TimeTroi = timeLong;
                        if (!player.InfoSkill.PlayerTroi.PlayerId.Contains(character.Id))
                        {
                            player.InfoSkill.PlayerTroi.PlayerId.Add(character.Id);
                        }
                        
                    }
                    player.CharacterHandler.SendMessage(Service.ItemTime(DataCache.TimeTroi[0], timeUse/10));

                    //Setup char skill
                    character.InfoSkill.MeTroi.IsMeTroi = true;
                    character.InfoSkill.MeTroi.Monster = null;
                    character.InfoSkill.MeTroi.Character = player;
                    character.InfoSkill.MeTroi.DelayStart = 1000 + ServerUtils.CurrentTimeMillis();
                    character.InfoSkill.MeTroi.TimeTroi = timeLong;

                    character.Zone.ZoneHandler.SendMessage(Service.SkillEffectPlayer(character.Id, player.Id, 1, 32));
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error HandleTroiMonster in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }

        public static void HandleSocolaPlayer(ICharacter character, List<ICharacter> characters, SkillDataTemplate skillData)
        {
            try
            {
              //  if(CharIdIsBoss(characters[0].Id)) return;
                var player = (Model.Character.Character)characters[0];
                if(player == null) return;
                if(Math.Abs(character.InfoChar.X - player.InfoChar.X) > skillData.Dx || Math.Abs(character.InfoChar.Y - player.InfoChar.Y) > skillData.Dy || character.InfoSkill.Socola.IsSocola) return;
                
                if (!CharIdIsBoss(character.Id)) 
                {
                    //if(player.Test.IsTest && player.Test.TestCharacterId != character.Id) return;
                    if(player.InfoChar.TypePk == 0 && player.Flag == 0) return;
                    if(player.Flag != 0 && player.Flag != 8 && player.Flag == character.Flag) return;
                }
                
                lock (player)
                {
                    var timeUse = 30000 + ServerUtils.CurrentTimeMillis();

                    //Setup monster skill
                    lock (player.InfoSkill.Socola)
                    {
                        player.InfoSkill.Socola.IsSocola = true;
                        player.InfoSkill.Socola.Time = timeUse;
                        player.InfoSkill.Socola.CharacterId = character.Id;
                        player.InfoSkill.Socola.Fight = ServerUtils.RandomNumber(2,4);
                        player.InfoSkill.Socola.Percent = skillData.Damage;
                        if (CharIdIsBoss(character.Id))
                        {
                            var bossReal = (Boss) character;
                            switch (bossReal.Type)
                            {
                                case int Type when Type == DataCache.BOSS_THO_PHE_CO_TYPE:
                                    player.InfoSkill.Socola.Time += 90000;
                                    player.InfoSkill.Socola.IsCarot = true;
                                    break;
                            }
                           
                        }
                    }
                    player.CharacterHandler.SendZoneMessage(Service.UpdateBody(player));
                    if (player.InfoSkill.Socola.IsCarot)
                    {
                        player.CharacterHandler.SendMessage(Service.ItemTime(4076, 120));
                    }
                    else 
                    {
                        player.CharacterHandler.SendMessage(Service.ItemTime(3780, 30));
                    }
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error HandleTroiMonster in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }

        public static void HandleHealPlayer(ICharacter character, SkillDataTemplate skillData, List<ICharacter> players)
        {
            try
            {
                if(CharIdIsBoss(players[0].Id)) return;
                var player = (Model.Character.Character)players[0];
                if(player == null) return;
                if(Math.Abs(character.InfoChar.X - player.InfoChar.X) > skillData.Dx || Math.Abs(character.InfoChar.Y - player.InfoChar.Y) > skillData.Dy) 
                {
                    if (player.InfoChar.X > character.InfoChar.X)
                    {
                        character.InfoChar.X = (short)(player.InfoChar.X - skillData.Dx);
                    }
                    else
                    {
                        character.InfoChar.X = (short)(player.InfoChar.X + skillData.Dx);
                    }

                    character.InfoChar.Y = player.InfoChar.Y;
                    character.CharacterHandler.MoveMap(character.InfoChar.X, character.InfoChar.Y);
                    // character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().SO_FAR_SKILL));
                    // return;
                }
                //if(player.Test.IsTest && player.Test.TestCharacterId != character.Id) return;
                if(player.InfoChar.TypePk == 3 || player.Flag == 8) return;
                if (player.InfoChar.Hp == player.HpFull && player.InfoChar.Mp == player.MpFull)
                {
                    character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().DO_NOT_HEAL_FOR_ORTHER));
                    return;
                }

                lock (player)
                {
                    if (player.InfoChar.IsDie)
                    {
                        player.CharacterHandler.LeaveFromDead(true);
                        player.CharacterHandler.SendZoneMessage(Service.PublicChat(player.Id, string.Format(TextServer.gI().THANKS_FOR_SAVE_ME, character.Name)));
                    }
                    else
                    {
                        var hpPlus = (int)player.HpFull * skillData.Damage / 100;
                        var mpPlus = (int)player.MpFull * skillData.Damage / 100;
                        player.CharacterHandler.PlusHp(hpPlus);
                        player.CharacterHandler.PlusMp(mpPlus);
                        player.CharacterHandler.SendMessage(Service.SendHp((int)player.InfoChar.Hp));
                        player.CharacterHandler.SendMessage(Service.SendMp((int)player.InfoChar.Mp));
                        player.CharacterHandler.SendZoneMessage(Service.PlayerLevel(player));
                        player.CharacterHandler.SendZoneMessage(Service.PublicChat(player.Id, string.Format(TextServer.gI().THANKS_FOR_HEAL_ME, character.Name)));
                    }

                    var hpMePlus = (int)character.HpFull * skillData.Damage / 100;
                    var mpMePlus = (int)character.MpFull * skillData.Damage / 100;
                    character.CharacterHandler.PlusHp(hpMePlus);
                    character.CharacterHandler.PlusMp(mpMePlus);
                    character.CharacterHandler.SendMessage(Service.SendHp((int)character.InfoChar.Hp));
                    character.CharacterHandler.SendMessage(Service.SendMp((int)character.InfoChar.Mp));
                    character.CharacterHandler.SendZoneMessage(Service.PlayerLevel(character));
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error HandleHealPlayer in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }

        public static void HandleHealDisciple(ICharacter character, SkillDataTemplate skillData, List<ICharacter> players)
        {
            try
            {
                var disciple = (Model.Character.Disciple)players[0];
                if(disciple == null) return;
                if(Math.Abs(character.InfoChar.X - disciple.InfoChar.X) > skillData.Dx || Math.Abs(character.InfoChar.Y - disciple.InfoChar.Y) > skillData.Dy) 
                {
                    if (disciple.InfoChar.X > character.InfoChar.X)
                    {
                        character.InfoChar.X = (short)(disciple.InfoChar.X - skillData.Dx);
                    }
                    else
                    {
                        character.InfoChar.X = (short)(disciple.InfoChar.X + skillData.Dx);
                    }

                    character.InfoChar.Y = disciple.InfoChar.Y;
                    character.CharacterHandler.MoveMap(character.InfoChar.X, character.InfoChar.Y);
                    // character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().SO_FAR_SKILL));
                    // return;
                }
                if(disciple.InfoChar.TypePk == 3 || disciple.Flag == 8) return;
                if (disciple.InfoChar.Hp == disciple.HpFull && disciple.InfoChar.Mp == disciple.MpFull)
                {
                    character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().DO_NOT_HEAL_FOR_ORTHER));
                    return;
                }

                lock (disciple)
                {
                    if (disciple.InfoChar.IsDie)
                    {
                        disciple.CharacterHandler.LeaveFromDead(true);
                        disciple.CharacterHandler.SendZoneMessage(Service.PublicChat(disciple.Id, TextServer.gI().THANKS_FOR_SAVE_ME_DIS));
                    }
                    else
                    {
                        var hpPlus = (int)disciple.HpFull * skillData.Damage / 100;
                        var mpPlus = (int)disciple.MpFull * skillData.Damage / 100;
                        disciple.CharacterHandler.PlusHp(hpPlus);
                        disciple.CharacterHandler.PlusMp(mpPlus);
                        disciple.CharacterHandler.SendMessage(Service.SendHp((int)disciple.InfoChar.Hp));
                        disciple.CharacterHandler.SendMessage(Service.SendMp((int)disciple.InfoChar.Mp));
                        disciple.CharacterHandler.SendZoneMessage(Service.PlayerLevel(disciple));
                        disciple.CharacterHandler.SendZoneMessage(Service.PublicChat(disciple.Id, TextServer.gI().THANKS_FOR_SAVE_ME_DIS));
                    }

                    var hpMePlus = (int)character.HpFull * skillData.Damage / 100;
                    var mpMePlus = (int)character.MpFull * skillData.Damage / 100;
                    character.CharacterHandler.PlusHp(hpMePlus);
                    character.CharacterHandler.PlusMp(mpMePlus);
                    character.CharacterHandler.SendMessage(Service.SendHp((int)character.InfoChar.Hp));
                    character.CharacterHandler.SendMessage(Service.SendMp((int)character.InfoChar.Mp));
                    character.CharacterHandler.SendZoneMessage(Service.PlayerLevel(character));
                }

                // Nội tại
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error HandleHealDisciple in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }

        #endregion

        #region handle skill boss
        public static void HandleThoiMienBoss(ICharacter character, SkillDataTemplate skillData, ICharacter iboss)
        {
            try
            {
                var boss = (Boss)iboss;
                if(boss == null || boss.InfoChar.IsDie) return;
                if(Math.Abs(character.InfoChar.X - boss.InfoChar.X) > skillData.Dx || Math.Abs(character.InfoChar.Y - boss.InfoChar.Y) > skillData.Dy) return;
                lock (boss)
                {
                    var timeUse = DataCache.TimeThoiMien[skillData.Point];
                    var timeLong = timeUse * 100 + ServerUtils.CurrentTimeMillis();

                    //Setup monster skill
                    lock (boss.InfoSkill.ThoiMien)
                    {
                        boss.InfoSkill.ThoiMien.IsThoiMien = true;
                        boss.InfoSkill.ThoiMien.Time = timeLong/2;
                        boss.InfoSkill.ThoiMien.TimePercent = -1;
                        boss.InfoSkill.ThoiMien.Percent = skillData.Damage*5;
                    }
                    boss.CharacterHandler.SendMessage(Service.ItemTime(DataCache.TimeThoiMien[0], timeUse/10));
                    character.Zone.ZoneHandler.SendMessage(Service.SkillEffectPlayer(character.Id, boss.Id, 1, 41));
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error HandleThoiMienBoss in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }

        public static void HandleDichChuyenBoss(ICharacter character, ICharacter iboss, SkillDataTemplate skillData)
        {
            try
            {
                var boss = (Boss)iboss;
                if(boss == null || boss.InfoChar.IsDie) return;
                if(Math.Abs(character.InfoChar.X - boss.InfoChar.X) > skillData.Dx || Math.Abs(character.InfoChar.Y - boss.InfoChar.Y) > skillData.Dy) return;
                
                lock (boss)
                {
                    var timeUse = DataCache.TimeDichChuyen[skillData.Point];
                    var timeLong = timeUse * 100 + ServerUtils.CurrentTimeMillis();

                    //Setup monster skill
                    lock (boss.InfoSkill.DichChuyen)
                    {
                        boss.InfoSkill.DichChuyen.IsStun = true;
                        boss.InfoSkill.DichChuyen.Time = timeLong/2;
                    }
                    character.InfoChar.X = boss.InfoChar.X;
                    character.InfoChar.Y = boss.InfoChar.Y;
                    character.Zone.ZoneHandler.SendMessage(Service.SkillEffectPlayer(character.Id, boss.Id, 1, 40));
                    character.Zone.ZoneHandler.SendMessage(Service.SendPos(character, 1));
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error HandleDichChuyenBoss in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }

        public static void HandleTroiBoss(ICharacter character, SkillDataTemplate skillData, ICharacter iboss)
        {
            try
            {
                var boss = (Boss)iboss;
                if(boss == null || boss.InfoChar.IsDie || boss.KhangTroi) return;
                if(Math.Abs(character.InfoChar.X - boss.InfoChar.X) > skillData.Dx || Math.Abs(character.InfoChar.Y - boss.InfoChar.Y) > skillData.Dy) return;
                
                lock (boss)
                {
                    if (boss.InfoSkill.TaiTaoNangLuong.IsTTNL)
                    {
                        RemoveTTNL(boss);
                    }
                    var timeUse = DataCache.TimeTroi[skillData.Point];
                    var timeLong = timeUse * 100 + ServerUtils.CurrentTimeMillis();

                    //Setup monster skill
                    lock (boss.InfoSkill.PlayerTroi)
                    {
                        boss.InfoSkill.PlayerTroi.IsPlayerTroi = true;
                        boss.InfoSkill.PlayerTroi.TimeTroi = timeLong;
                        if (!boss.InfoSkill.PlayerTroi.PlayerId.Contains(character.Id))
                        {
                            boss.InfoSkill.PlayerTroi.PlayerId.Add(character.Id);
                        }
                        
                    }
                    boss.CharacterHandler.SendMessage(Service.ItemTime(DataCache.TimeTroi[0], timeUse/10));

                    //Setup char skill
                    character.InfoSkill.MeTroi.IsMeTroi = true;
                    character.InfoSkill.MeTroi.Monster = null;
                    character.InfoSkill.MeTroi.Character = boss;
                    character.InfoSkill.MeTroi.DelayStart = 1000 + ServerUtils.CurrentTimeMillis();
                    character.InfoSkill.MeTroi.TimeTroi = timeLong;

                    character.Zone.ZoneHandler.SendMessage(Service.SkillEffectPlayer(character.Id, boss.Id, 1, 32));
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error HandleTroiBoss in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }
        #endregion

        #region handle skill dis
        #endregion
        public static bool isSpecialSkillNextDame = false;
        #region handle special skill
        public static SkillDataTemplate HandleSpecialSkill(ICharacter character, SkillTemplate skillTemplate, SkillDataTemplate skillData)
        {
            // Xử lý nội tại
            var charRel = (Model.Character.Character)character;
            var specialId = charRel.SpecialSkill.Id;
            if (specialId != -1 && skillTemplate.Id == charRel.SpecialSkill.SkillId) //Đã có nội tại
            {
                switch (charRel.SpecialSkill.SkillId)
                {
                    case 0: //Đấm dragon
                    case 1://Kamejoko
                    case 2://đấm Demon
                    case 3://Masenko
                    case 4://Đấm Galick
                    case 5://Antomic
                    case 13://biến hình
                         {
                        skillData.Damage += skillData.Damage*charRel.SpecialSkill.Value/100;
                        break;
                    }
                    case 17://liên hoàn
                    {
                         
                        skillData.Damage += skillData.Damage*charRel.SpecialSkill.Value/100;
                        break;
                    }
                    case 6://Thái dương hạ san
                    {
                        skillData.ManaUse += charRel.SpecialSkill.Value;
                        skillData.CoolDown -= skillData.CoolDown*charRel.SpecialSkill.Value/100;
                        if (skillData.CoolDown < 0)
                        {
                            skillData.CoolDown = 500;
                        }
                        break;
                    }
                    case 7://Trị thương
                    case 11://Makanko
                    case 12://Đẻ trứng
                    case 14://Tự phát nổ
                    case 10://QCKK
                    case 19://Khiên năng lượng
                    case 21://Huýt sáo
                    {
                        skillData.CoolDown -= skillData.CoolDown*charRel.SpecialSkill.Value/100;
                        if (skillData.CoolDown < 0)
                        {
                            skillData.CoolDown = 500;
                        }
                        break;
                    }
                    case 18://biến sô cô la
                    case 20://Dịch chuyển tức thời
                    case 22://Thôi miên
                    case 23://trói
                    {
                        charRel.SpecialSkill.nextAttackDmgPercent = charRel.SpecialSkill.Value;
                      //  isSpecialSkillNextDame = true;
                        break;
                    }
                }
            }
            //if (isSpecialSkillNextDame)
            //{
            ////    charRel.SpecialSkill.nextAttackDmgPercent = charRel.SpecialSkill.Value;
            //    skillData.Damage += skillData.Damage * charRel.SpecialSkill.nextAttackDmgPercent;
            //    isSpecialSkillNextDame = false;
            //    charRel.SpecialSkill.nextAttackDmgPercent = 0;
            //}
        if (charRel.SpecialSkill.nextAttackDmgPercent > 0)
           {
               skillData.Damage += skillData.Damage*(charRel.SpecialSkill.nextAttackDmgPercent/100);
                charRel.SpecialSkill.nextAttackDmgPercent = 0;
           }

            if (specialId == 9 || specialId == 20 || specialId == 30)
            {
                var hpFull = character.HpFull;
                var hpNow = character.InfoChar.Hp;
                var percentHp = 100 - ((int)((hpFull- hpNow)*100/character.HpFull));
             
                    charRel.SpecialSkill.isCrit = true;
                
               
                  
            }

            return skillData;
        }
        #endregion

        #region Send Message Attack
        public static void SendZoneSkillAttackPlayer(ICharacter character, Message message)
        {
            try 
            {
                var timeServer = ServerUtils.CurrentTimeMillis();
                var charReal = (Model.Character.Character)character;
                var zone = character.Zone;
                if (zone == null) return;
                if (charReal.Delay.DelaySkillZone < timeServer)
                {
                    zone.ZoneHandler.SendMessage(message, isSkillMessage:true);
                    charReal.Delay.DelaySkillZone = 1000 + timeServer;
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error SendZoneSkillAttackPlayer in SkillHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
            
        } 
        #endregion
    }
}