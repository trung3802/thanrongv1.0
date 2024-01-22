using System;
using System.Collections.Generic;
using System.Linq;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.Handlers.Item;
using TienKiemV2Remastered.Application.Handlers.Skill;
using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.Interfaces.Monster;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.Map;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.DatabaseManager.Player;
using TienKiemV2Remastered.Model;
using TienKiemV2Remastered.Model.Character;
using TienKiemV2Remastered.Model.Info;
using TienKiemV2Remastered.Model.Item;
using TienKiemV2Remastered.Model.Map;
using TienKiemV2Remastered.Model.Monster;
using TienKiemV2Remastered.Model.Effect;
using TienKiemV2Remastered.Model.Option;
using static System.GC;
using Google.Protobuf.WellKnownTypes;
using TienKiemV2Remastered.Model.Template;
using TienKiemV2Remastered.Application.MainTasks;
using TienKiemV2Remastered.Application.Extension.Ấp_trứng;
using TienKiemV2Remastered.Application.Extension;
using TienKiemV2Remastered.Application.Extension.BlackballWar;
using TienKiemV2Remastered.Application.Extension.ChampionShip;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.Train;
using TienKiemV2Remastered.Application.Extension.Namecball;
using TienKiemV2Remastered.Source.Application.Handlers;

namespace TienKiemV2Remastered.Application.Handlers.Character
{
    public class CharacterHandler : ICharacterHandler
    {
        public Model.Character.Character Character { get; set; }
        public void SetupAmulet()
        {

        }
        public CharacterHandler(Model.Character.Character character)
        {
            Character = character;
        }
        public void SetPlayer(Player player)
        {
            Character.Player = player;
        }

        public void SendMessage(Message message)
        {
            Character?.Player?.Session?.SendMessage(message);
        }

        public void SendZoneMessage(Message message)
        {
            Character?.Zone?.ZoneHandler?.SendMessage(message);
        }

        public void SendMeMessage(Message message)
        {
            Character?.Zone?.ZoneHandler?.SendMessage(message, Character.Id);
        }

        private void UpdateAntiChangeServerTime(string reason = "")
        {
            //var timeServer = ServerUtils.CurrentTimeMillis();
            //if ((Character.InfoChar.ThoiGianDoiMayChu - timeServer) < 180000)
            //{
            //    Character.InfoChar.ThoiGianDoiMayChu = timeServer + 300000;
            //}
            //DelayInventoryAction(timeServer, reason);
        }

        //private void DelayInventoryAction(long timeServer, string reason)
        //{
        //    if (reason == "Nhặt từ map" || reason == "CSKB" || reason == "CSTT" || reason == "Ăn bánh tt" || reason == "Dùng đá nâng cấp" || reason == "Dùng đá bảo vệ" || reason == "Bán cho shop")
        //    {
        //        Character.Delay.SaveInvData = 8000 + timeServer;
        //    }
        //    else
        //    {
        //        Character.Delay.InvAction = timeServer + 1000;
        //    }
        //}

        public void Close()
        {
            if (!Character.BeforeDispose)
            {
                Character.BeforeDispose = true;
                CharacterDB.Update(Character);
                Character?.Disciple?.CharacterHandler.Close();
                Character?.Pet?.CharacterHandler.Close();
                if (Character.DataNgocRongNamek.AlreadyPick(Character))
                {
                    var itm = new ItemMap(-1, ItemCache.GetItemDefault((short)(Character.DataNgocRongNamek.IdNamekBall)));
                    itm.X = Character.InfoChar.X;
                    itm.Y = Character.InfoChar.Y;
                    Character.Zone.ZoneHandler.LeaveItemMap(itm);
                    Character.InfoChar.TypePk = 0;
                    Character.DataNgocRongNamek.IdNamekBall = -1;
                    Character.InfoChar.Bag = ClanManager.Get(Character.ClanId) != null ? (sbyte)ClanManager.Get(Character.ClanId).ImgId : (sbyte)-1;
                }
                if (Character.Blackball.AlreadyPick(Character))
                {
                    BlackBallHandler.ForPlayer.gI().ExitMapOrDie(Character);
                }
                if (Character.DataTraining.isTraining)
                {
                    Character.DataTraining.lastTimeLogout = ServerUtils.CurrentTimeMillis();
                }

                Character.Zone?.ZoneHandler?.OutZone(Character);

                Character.Me = new InfoFriend(Character)
                {
                    IsOnline = false
                };
                Clear();
            }
        }

        public void UpdateInfo()
        {
            SetUpInfo();
            SendMessage(Service.SendBody(Character));
            SendZoneMessage(Service.UpdateBody(Character));
            SendMessage(Service.MeLoadPoint(Character));
            Character.Me = new InfoFriend(Character);
        }

        public void Update()
        {
            var timeServer = ServerUtils.CurrentTimeMillis();
            lock (Character)
            {
                try
                {
                    ChanLeHandler.PhatPhanThuong(Character.Id);
                    RemoveSkill(timeServer);
                    if (!Character.InfoChar.IsDie)
                    {
                        UpdateMask(timeServer);
                        UpdateEffect(timeServer);
                        UpdateFusion(timeServer);
                        UpdateAutoPlay(timeServer);
                        UpdateOther(timeServer);  
                    }
                    
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error UpdatePlayer in CharacterHandler.cs: {e.Message} \n {e.StackTrace}", e);

                }
            }
        }
        public void RemoveHoaDa(ICharacter character)
        {
            var infoSkill = Character.InfoSkill;
            infoSkill.HoaDa.IsHoaDa = false;
            infoSkill.HoaDa.Time = -1;
            infoSkill.HoaDa.CharacterId = -1;
            infoSkill.HoaDa.Percent = -1;
            infoSkill.HoaDa.Fight = -1;
            Character.CharacterHandler.SendZoneMessage(Service.UpdateBody(character));
            character.CharacterHandler.SendZoneMessage(Service.SkillEffectPlayer(character.Id, character.Id, 2, 42));

        }
        public void RemoveHoaBang(ICharacter character)
        {
            var infoSkill = character.InfoSkill;
            infoSkill.HoaBang.Time = -1;
            infoSkill.HoaBang.isHoaBang = false;
            Character.CharacterHandler.SendZoneMessage(Service.UpdateBody(character));
            character.CharacterHandler.SendZoneMessage(Service.SkillEffectPlayer(character.Id, character.Id, 2, 42));

        }
        public void RemoveMaPhongBa(ICharacter character)
        {
            var infoSkill = character.InfoSkill;
            infoSkill.MaPhongBa.isMaPhongBa = false;
            infoSkill.MaPhongBa.timeMaPhongBa = -1;
            Character.CharacterHandler.SendZoneMessage(Service.UpdateBody(character));
        }
        public void RemoveSkill(long timeServer, bool globalReset = false)
        {
            var infoSkill = Character.InfoSkill;
            if ((infoSkill.TaiTaoNangLuong.IsTTNL &&
                 infoSkill.TaiTaoNangLuong.DelayTTNL <= timeServer) || globalReset)
            {
                SkillHandler.RemoveTTNL(Character);
            }
            if (infoSkill.MaPhongBa.isMaPhongBa && infoSkill.MaPhongBa.timeMaPhongBa <= timeServer || globalReset)
            {
                RemoveMaPhongBa(Character);
            }
            if (infoSkill.Monkey.MonkeyId == 1 && infoSkill.Monkey.TimeMonkey <= timeServer || globalReset)
            {
                SkillHandler.HandleMonkey(Character, false);
            }
            if (infoSkill.HoaDa.IsHoaDa && infoSkill.HoaDa.Time <= timeServer || globalReset)
            {
                RemoveHoaDa(Character);
            }
            if ((infoSkill.HoaBang.isHoaBang && infoSkill.HoaBang.Time <= timeServer) || globalReset)
            {
                RemoveHoaBang(Character);
            }
            if ((infoSkill.Protect.IsProtect && infoSkill.Protect.Time <= timeServer) || globalReset)
            {
                SkillHandler.RemoveProtect(Character);
                if (globalReset && infoSkill.Protect.IsProtect)
                {
                    SendMessage(Service.ItemTime(DataCache.TimeProtect[0], 0));
                }
            }

            if (infoSkill.HuytSao.IsHuytSao && infoSkill.HuytSao.Time <= timeServer)
            {
                SkillHandler.RemoveHuytSao(Character);
            }
            if (infoSkill.MeTroi.IsMeTroi) // mình là người trói
            {
                var monsterMap = infoSkill.MeTroi.Monster;
                var charTemp = infoSkill.MeTroi.Character; // nhân vật bị mình trói
                
                if (globalReset)
                {
                    SkillHandler.RemoveTroi(Character);
                }
                if (monsterMap is { IsDie: true })
                {
                    SkillHandler.RemoveTroi(Character);
                }
                else if ((charTemp != null && charTemp.InfoChar.IsDie))
                {
                    SkillHandler.RemoveTroi(Character);
                }
                else if (infoSkill.MeTroi.TimeTroi <= timeServer || monsterMap is { IsDie: true } || charTemp != null && charTemp.InfoChar.IsDie)
                {
                    SkillHandler.RemoveTroi(Character);
                }
            }

            if (infoSkill.PlayerTroi.IsPlayerTroi || globalReset) // mình là người bị trói
            {
                if (globalReset && infoSkill.PlayerTroi.IsPlayerTroi)
                {
                    SendMessage(Service.ItemTime(DataCache.TimeTroi[0], 0));
                    infoSkill.PlayerTroi.PlayerId.ForEach(i => SkillHandler.RemoveTroi(Character.Zone.ZoneHandler.GetCharacter(i)));
                }
                else if (infoSkill.PlayerTroi.IsPlayerTroi && infoSkill.PlayerTroi.TimeTroi <= timeServer)
                {
                    infoSkill.PlayerTroi.IsPlayerTroi = false;
                    infoSkill.PlayerTroi.TimeTroi = -1;
                    infoSkill.PlayerTroi.PlayerId.Clear();
                    SkillHandler.RemoveTroi(Character);
                }
            }

            if (infoSkill.DichChuyen.IsStun && infoSkill.DichChuyen.Time <= timeServer || globalReset)
            {
                SkillHandler.RemoveDichChuyen(Character);
            }

            if (infoSkill.ThaiDuongHanSan.IsStun && infoSkill.ThaiDuongHanSan.Time <= timeServer || globalReset)
            {
                SkillHandler.RemoveThaiDuongHanSan(Character);
            }

            if (infoSkill.ThoiMien.IsThoiMien && infoSkill.ThoiMien.Time <= timeServer || globalReset)
            {
                SkillHandler.RemoveThoiMien(Character);
                if (globalReset && infoSkill.ThoiMien.IsThoiMien)
                {
                    SendMessage(Service.ItemTime(DataCache.TimeThoiMien[0], 0));
                }
            }

            if (infoSkill.Socola.IsSocola && infoSkill.Socola.Time <= timeServer || globalReset)
            {
                SkillHandler.RemoveSocola(Character);
                if (globalReset && infoSkill.Socola.IsSocola)
                {
                    SendMessage(Service.ItemTime(3780, 0));
                }
            }
        }

        public void UpdateAutoPlay(long timeServer)
        {
            if (Character.InfoChar.TimeAutoPlay > 0 && Character.Delay.AutoPlay <= timeServer)
            {
                if (Character.InfoChar.TimeAutoPlay < timeServer || Character.InfoChar.TimeAutoPlay < 0)
                {
                    Character.InfoChar.TimeAutoPlay = 0;
                    SendMessage(Service.ItemTime(4387, 0));
                    SendMessage(Service.AutoPlay(false));
                }
                Character.Delay.AutoPlay = 60000 + timeServer;
            }
        }

        public void UpdateOther(long timeServer)
        {
            //   #region Phó bản Clan
            if (ClanManager.Get(Character.ClanId) != null)
            {
                var clan = ClanManager.Get(Character.ClanId);
                if ((clan.Reddot.Open && clan.Reddot.timeDoanhTrai < timeServer) || clan.Reddot.isFinish)
                {
                    clan.Reddot.Close(Character);
                }
                if ((clan.Gas.Open && clan.Gas.timeKhiGas < timeServer) || clan.Gas.isFinish)
                {
                    clan.Gas.Close(Character);
                }
                if ((clan.cdrd.Open && clan.cdrd.timeCDRD < timeServer) || clan.cdrd.isFinish)
                {
                    clan.cdrd.Close(Character);
                }
                if ((clan.bdkb.Open && clan.bdkb.timeBDKB < timeServer) || clan.bdkb.isFinish)
                {
                    clan.bdkb.Close(Character);
                }
                if (clan.ClanBoss.Open)
                {
                    switch (clan.ClanBoss.Time < timeServer)
                    {
                        case true:
                            if (Character.InfoChar.MapId == 165)
                            {
                                clan.ClanZone.Maps[1].OutZone(Character, clan.ClanZone.Maps[0].Id);
                                clan.ClanZone.Maps[0].JoinZone(Character, 0);
                            }
                            if (clan.ClanBoss.Open)
                            {
                                clan.ClanBoss.Open = false;

                            }
                            break;
                        case false:
                            if (Character.Delay.DelayBossBangHoi < timeServer && ((Character.Delay.DelayBossBangHoi - timeServer) / 60000) <= 1 && (Character.InfoChar.MapId == 165))
                            {
                                Character.Delay.DelayBossBangHoi = 15000 + timeServer;
                                var timeExist = (clan.ClanBoss.Time - timeServer);
                                Character.CharacterHandler.SendMessage(Service.ServerMessage("về khu vực bang trong " + timeExist / 1000 + " giây nữa"));
                            }
                            break;
                    }
                }
            }
            //  #endregion
            //  #region Đại hội võ thuật
            if ((ChampionShip.gI().CheckIdRegister(Character.Id) && !ChampionShip.gI().CheckIdCharacters(Character.Id)))
            {
                if (Character.Delay.Delay10Giay <= timeServer)
                {
                    var time = (ChampionShip.gI().TimeStart - timeServer) / 1000;
                    if (ChampionShip.gI().roundNow != 0)
                    {
                        time = (ChampionShip.gI().TimeMatch - timeServer) / 1000;
                    }
                    if (time < 1)
                    {
                        time = time / 60000;
                    }
                    Character.Delay.Delay10Giay = 15000 + timeServer;
                    SendMessage(Service.ServerMessage("Trận đấu của bạn sẽ diễn ra trong vòng " + time + "s nữa"));
                }
            }

            if (ChampionShip.gI().IsSetMatch && Character.DataDaiHoiVoThuat.IdCharacterFight != -1 && !Character.DataDaiHoiVoThuat.isWin && !Character.DataDaiHoiVoThuat.isFailed)
            {
                if (ClientManager.Gi().GetCharacter(Character.DataDaiHoiVoThuat.IdCharacterFight) != null)
                {
                    var charPK = ClientManager.Gi().GetCharacter(Character.DataDaiHoiVoThuat.IdCharacterFight);
                    if (charPK.InfoChar.IsDie || charPK.InfoChar.Hp <= 0 || charPK.InfoChar.MapId != 113)
                    {
                        ChampionShip.gI().WinRound(Character);
                        SendZoneMessage(Service.ChangeTypePk(charPK.Id, 0));
                        charPK.DataDaiHoiVoThuat.IdCharacterFight = -1;
                        SendMessage(Service.ServerMessage("Bạn đã thua, hẹn gặp lại ở giải sau"));
                        charPK.DataDaiHoiVoThuat.isFailed = true;
                        ChampionShip.gI().CharacterRegister.Remove(charPK.Id);
                    }
                    else if ((charPK.InfoChar.X >= 35 && charPK.InfoChar.X <= 157) || (charPK.InfoChar.X >= 611 && charPK.InfoChar.X <= 733) || charPK.InfoChar.Y == 288)
                    {
                        charPK.InfoChar.X = 500;
                        SendZoneMessage(Service.SendPos(charPK, 1));
                        SendZoneMessage(Service.ChangeTypePk(charPK.Id, 0));
                        charPK.DataDaiHoiVoThuat.IdCharacterFight = -1;
                        SendMessage(Service.ServerMessage("Do bạn đã vi phạm luật, nên đã thua"));
                        charPK.DataDaiHoiVoThuat.isFailed = true;
                        ChampionShip.gI().CharacterRegister.Remove(charPK.Id);
                    }
                }
                else
                {
                    ChampionShip.gI().WinRound(Character);
                }
            }
            if (Character.DataDaiHoiVoThuat.delayChangeTypePK <= timeServer && Character.InfoChar.MapId == 113)
            {
                Character.DataDaiHoiVoThuat.delayChangeTypePK = 300000 + timeServer;
                if (ChampionShip.gI().Characters1.Contains(Character.Id)) ChampionShip.gI().SetPos1(Character);
                else if (ChampionShip.gI().Characters2.Contains(Character.Id)) ChampionShip.gI().SetPos2(Character);
                ChampionShip.gI().SetTypeCombat(Character, 3);
            }
            if (ChampionShip.gI().EndRound && Character.InfoChar.MapId == 113 && Character.DataDaiHoiVoThuat.IdCharacterFight != -1 && !Character.DataDaiHoiVoThuat.isWin && !Character.DataDaiHoiVoThuat.isFailed)
            {
                if (ClientManager.Gi().GetCharacter(Character.DataDaiHoiVoThuat.IdCharacterFight) != null)
                {
                    var charPK = ClientManager.Gi().GetCharacter(Character.DataDaiHoiVoThuat.IdCharacterFight);
                    if (charPK.InfoChar.IsDie || charPK.InfoChar.Hp <= Character.InfoChar.Hp || charPK.InfoChar.MapId != 113)
                    {
                        ChampionShip.gI().WinRound(Character);
                        SendZoneMessage(Service.ChangeTypePk(charPK.Id, 0));
                        charPK.DataDaiHoiVoThuat.IdCharacterFight = -1;
                        SendMessage(Service.ServerMessage("Bạn đã thua, hẹn gặp lại ở giải sau"));
                        charPK.DataDaiHoiVoThuat.isFailed = true;
                        ChampionShip.gI().CharacterRegister.Remove(charPK.Id);
                    }
                }
                else
                {
                    ChampionShip.gI().WinRound(Character);
                }
            }
            if (Character.DataDaiHoiVoThuat.Round == ChampionShip.gI().roundNow + 1)
            {
                if (Character.Delay.Delay10Giay <= timeServer)
                {
                    SendMessage(Service.ServerMessage("Trận đấu của bạn sẽ diễn ra khi các trận đấu của mọi người hoàn tất"));
                    Character.Delay.Delay10Giay = 15000 + timeServer;
                }
            }
            if (Character.DataDaiHoiVoThuat.delayNextRound <= timeServer && Character.InfoChar.MapId == 113)
            {
                if (Character.DataDaiHoiVoThuat.isWin)
                {
                    Character.DataDaiHoiVoThuat.isWin = false;
                    Character.DataDaiHoiVoThuat.Round++;
                    MapManager.JoinMap(Character, 52, MapManager.Get(52).GetZoneNotMaxPlayer().Id, false, false, 0);
                    SendMessage(Service.ServerMessage("Bạn đã thắng vòng này, xin chờ tại đây ít phút để thi tiếp vòng sau"));

                }
                if (Character.DataDaiHoiVoThuat.isVoDich)
                {
                    Character.DataDaiHoiVoThuat.isVoDich = false;
                    SendMessage(Service.ServerMessage($"Xin chúc mừng,Bạn đã vô địch giải {ChampionShip.gI().TextTypeChampion()}"));
                    ClientManager.Gi().SendMessageCharacter(Service.ServerChat($"{Character.Name} vừa vô địch giải {ChampionShip.gI().TextTypeChampion()}"));
                    //async void Action()
                    //{
                    //   await Task.Delay(2000);
                    MapManager.JoinMap(Character, 52, MapManager.Get(52).GetZoneNotMaxPlayer().Id, false, false, 0);
                    //}
                    //var task = new Task(Action);
                    //task.Start();
                }
                if (Character.DataDaiHoiVoThuat.isFailed)
                {
                    Character.DataDaiHoiVoThuat.isFailed = false;
                    BackHome();
                    SendMessage(Service.ServerMessage("Bạn đã thua, hẹn gặp lại ở giải sau"));
                    //ChampionShip.gI().CharacterRegister.Remove(Character.Id);
                }
            }
            // #endregion
            //  #region Thách Đấu
            if (Character.Challenge.isChallenge && Character.Challenge.PlayerChallengeID != -1)
            {
                Character.Challenge.Runtime(Character, timeServer);
            }
            //  #endregion
            //   #region Ngọc Rồng Sao Đen
            if (Blackball.gI().ListMapNRSD.Contains(Character.InfoChar.MapId))
            {

                Character.Blackball.Runtime(Character, timeServer);
            }

            //  #endregion
        }
        
        public void UpdateEffect(long timeServer)
        {
            var effect = Character.Effect;
            // Vệ tinh trí lực

            if (Character.InfoMore.IsNearAuraTriLucItem && effect.AuraBuffKi30S.Time <= timeServer && Character.InfoChar.Mp < Character.MpFull)
            {
                PlusMp((int)(Character.MpFull * 5 / 100));
                SendMessage(Service.SendMp((int)Character.InfoChar.Mp));
                effect.AuraBuffKi30S.Time = 30000 + timeServer;
                Character.InfoMore.IsNearAuraTriLucItem = false;
            }

            if (Character.InfoMore.IsNearAuraSinhLucItem && effect.AuraBuffHp30S.Time <= timeServer && Character.InfoChar.Hp < Character.HpFull)
            {
                PlusHp((int)(Character.HpFull * 5 / 100));
                SendMessage(Service.SendHp((int)Character.InfoChar.Hp));
                SendZoneMessage(Service.PlayerLevel(Character));
                effect.AuraBuffHp30S.Time = 30000 + timeServer;
                Character.InfoMore.IsNearAuraSinhLucItem = false;
            }
            //if ()

            if (effect.BuffHp30S.Value > 0 && effect.BuffHp30S.Time <= timeServer && Character.InfoChar.Hp < Character.HpFull)
            {
                PlusHp(effect.BuffHp30S.Value);
                SendMessage(Service.SendHp((int)Character.InfoChar.Hp));
                SendZoneMessage(Service.PlayerLevel(Character));
                effect.BuffHp30S.Time = 30000 + timeServer;
            }

            if (effect.BuffKi1s.Value > 0 && effect.BuffKi1s.Time <= timeServer && Character.InfoChar.Mp < Character.MpFull)
            {
                PlusMp(effect.BuffKi1s.Value);
                SendMessage(Service.SendMp((int)Character.InfoChar.Mp));
                effect.BuffKi1s.Time = 1500 + timeServer;
            }

            // Effect giáp luyện tập
            // Nếu vừa tháo giáp luyện tập ra thì sẽ trừ thời gian
            if (Character.InfoMore.LastGiapLuyenTapItemId != 0 && Character.Delay.GiapLuyenTap != -1 && Character.Delay.GiapLuyenTap < timeServer)
            {
                var giapLuyenTap = GetItemBagById(Character.InfoMore.LastGiapLuyenTapItemId);
                if (giapLuyenTap != null && ItemCache.ItemIsGiapLuyenTap(giapLuyenTap.Id))
                {
                    var optionCheck = giapLuyenTap.Options.FirstOrDefault(option => option.Id == 9);
                    if ((optionCheck.Param - 1) > 0)
                    {
                        optionCheck.Param -= 1;
                        SendMessage(Service.SendBag(Character));
                        Character.Delay.GiapLuyenTap = 60000 + timeServer;
                    }
                    else
                    {
                        optionCheck.Param = 0;
                        Character.InfoMore.LastGiapLuyenTapItemId = 0;
                        SendMessage(Service.SendBag(Character));
                        UpdateInfo();
                        Character.Delay.GiapLuyenTap = -1;
                    }
                }
                else
                {
                    Character.InfoMore.LastGiapLuyenTapItemId = 0;
                    Character.Delay.GiapLuyenTap = -1;
                }
            }

            bool IsRemoveBuffEffect = false;
            // Effect thức ăn
            if (Character.InfoBuff.BanhTrungThuTime < timeServer && Character.InfoBuff.BanhTrungThuId > -1)
            {
                Character.InfoBuff.BanhTrungThuId = -1;
                IsRemoveBuffEffect = true;
            }
            if (Character.InfoBuff.KichDucX2Time < timeServer && Character.InfoBuff.KichDucX2)
            {
                Character.InfoBuff.KichDucX2 = false;
                IsRemoveBuffEffect = true;
            }
            if (Character.InfoBuff.KichDucX5Time < timeServer && Character.InfoBuff.KichDucX5)
            {
                Character.InfoBuff.KichDucX2 = false;
                IsRemoveBuffEffect = true;
            }
            if (Character.InfoBuff.KichDucX7Time < timeServer && Character.InfoBuff.KichDucX7)  
            {
                Character.InfoBuff.KichDucX2 = false;
                IsRemoveBuffEffect = true;
            }
            // Effect thức ăn
            if (Character.InfoBuff.ThucAnTime < timeServer && Character.InfoBuff.ThucAnId > -1)
            {
                Character.InfoBuff.ThucAnId = -1;
                IsRemoveBuffEffect = true;
            }
            if (Character.InfoBuff.timeEnchantCrit < timeServer && Character.InfoBuff.isEnchantCrit && Character.InfoBuff.isActiveCrit)
            {
                Character.InfoBuff.isEnchantCrit = false;
                IsRemoveBuffEffect = true;
            }
            if (Character.InfoBuff.delayEnchantCrit < timeServer && !Character.InfoBuff.isEnchantCrit && Character.InfoBuff.isActiveCrit) 
            {
                Character.InfoBuff.delayEnchantCrit = 25000 + timeServer;
                Character.InfoBuff.timeEnchantCrit = 5000 + timeServer;
                Character.InfoBuff.isEnchantCrit = true;
                Character.CharacterHandler.SendMessage(Service.ItemTimeWithMessage($"Chí mạng +5%", 100, 5));
                IsRemoveBuffEffect = true;
            }

            if (Character.InfoBuff.timeEnchantGiap < timeServer && Character.InfoBuff.isEnchantGiap && Character.InfoBuff.isActiveGiap)
            {
                Character.InfoBuff.isEnchantGiap = false;
                IsRemoveBuffEffect = true;
            }
            if (Character.InfoBuff.delayEnchantGiap < timeServer && !Character.InfoBuff.isEnchantGiap && Character.InfoBuff.isActiveGiap)
            { 
                Character.InfoBuff.delayEnchantGiap = 30000 + timeServer;
                Character.InfoBuff.timeEnchantGiap = 10000 + timeServer;
                Character.InfoBuff.isEnchantGiap = true;
                Character.CharacterHandler.SendMessage(Service.ItemTimeWithMessage($"Giáp +15%", 100, 10));
                IsRemoveBuffEffect = true;
            }
            if (Character.InfoBuff.XiMuoiHoaDaoTime < timeServer && Character.InfoBuff.XiMuoiHoaDao)
            {
                Character.InfoBuff.XiMuoiHoaDao = false;
                IsRemoveBuffEffect = true;
            }
            if (Character.InfoBuff.XiMuoiHoaMaiTime < timeServer && Character.InfoBuff.XiMuoiHoaMai)
            {
                Character.InfoBuff.XiMuoiHoaMai = false;
                IsRemoveBuffEffect = true;
            }
            // Effect cuồng nộ
            if (Character.InfoBuff.CuongNoTime < timeServer && Character.InfoBuff.CuongNo)
            {
                Character.InfoBuff.CuongNo = false;
                IsRemoveBuffEffect = true;
            }
            // Effect Bổ huyết
            if (Character.InfoBuff.BoHuyetTime < timeServer && Character.InfoBuff.BoHuyet)
            {
                Character.InfoBuff.BoHuyet = false;
                IsRemoveBuffEffect = true;
            }
            // Effect Bo Khi
            if (Character.InfoBuff.BoKhiTime < timeServer && Character.InfoBuff.BoKhi)
            {
                Character.InfoBuff.BoKhi = false;
                IsRemoveBuffEffect = true;
            }
            // Effect giap xen
            if (Character.InfoBuff.GiapXenTime < timeServer && Character.InfoBuff.GiapXen)
            {
                Character.InfoBuff.GiapXen = false;
                IsRemoveBuffEffect = true;
            }
            if (Character.InfoBuff.effRongXuongTime < timeServer && Character.InfoBuff.effRongXuong)
            {
                Character.InfoBuff.effRongXuong = false;
                IsRemoveBuffEffect = true;
            }
            if (Character.InfoBuff.BinhChuaCommesonTime < timeServer && Character.InfoBuff.BinhChuaCommeson)
            {
                Character.InfoBuff.BinhChuaCommeson = false;
                IsRemoveBuffEffect = true;
            }
            // Effect An danh
            if (Character.InfoBuff.AnDanhTime < timeServer && Character.InfoBuff.AnDanh)
            {
                Character.InfoBuff.AnDanh = false;
                IsRemoveBuffEffect = true;
            }
            if (Character.InfoBuff.CuongNoTime2 < timeServer && Character.InfoBuff.CuongNo2)
            {
                Character.InfoBuff.CuongNo2 = false;
                IsRemoveBuffEffect = true;
            }
            // Effect Bổ huyết
            if (Character.InfoBuff.BoHuyetTime2 < timeServer && Character.InfoBuff.BoHuyet2)
            {
                Character.InfoBuff.BoHuyet2 = false;
                IsRemoveBuffEffect = true;
            }
            // Effect Bo Khi
            if (Character.InfoBuff.BoKhiTime2 < timeServer && Character.InfoBuff.BoKhi2)
            {
                Character.InfoBuff.BoKhi2 = false;
                IsRemoveBuffEffect = true;
            }
            // Effect giap xen
            if (Character.InfoBuff.GiapXenTime2 < timeServer && Character.InfoBuff.GiapXen2)
            {
                Character.InfoBuff.GiapXen2 = false;
                IsRemoveBuffEffect = true;
            }
            // Effect An danh
            if (Character.InfoBuff.AnDanhTime2 < timeServer && Character.InfoBuff.AnDanh2)
            {
                Character.InfoBuff.AnDanh2 = false;
                IsRemoveBuffEffect = true;
            }
            // Effect Do CSKB
            if (Character.InfoBuff.MayDoCSKBTime < timeServer && Character.InfoBuff.MayDoCSKB)
            {
                Character.InfoBuff.MayDoCSKB = false;
            }
            if (Character.DataEnchant.timeMiNuong < timeServer && Character.DataEnchant.MiNuong)
            {
                IsRemoveBuffEffect = true;
                Character.DataEnchant.MiNuong = false;
            }

            if (IsRemoveBuffEffect)
            {
                SetUpInfo();
                SendMessage(Service.MeLoadPoint(Character));
            }
       
        }

        public void UpdateMask(long timeServer)
        {
            var item = Character.ItemBody[5];
            if (item == null) return;
            var loadPoint = false;
            switch (item.Id)
            {
                case 860:
                    Character.Zone.Characters.Values.ToList().ForEach(temp =>
                    {
                        if (!temp.DataEnchant.MiNuong)
                        {
                            temp.DataEnchant.MiNuong = true;
                            temp.DataEnchant.timeMiNuong = 15000 + timeServer;
                        }
                    });
                    break;
                case 464:
                    if (Character.Delay.BeautifulTalk < timeServer)
                    {
                        // get khoản cách ở gần 50 pixel r nói đẹp quá.
                        Character.Zone.Characters.Values.Where(c => !c.InfoChar.IsDie && c.Id != Character.Id && Math.Abs(c.InfoChar.X - Character.InfoChar.X) <= 50 && Math.Abs(c.InfoChar.Y - Character.InfoChar.Y) <= 600).ToList().ForEach(temp =>
                        {
                            temp.CharacterHandler.SendZoneMessage(Service.PublicChat(temp.Id, TextServer.gI().CHAT_SEXY[ServerUtils.RandomNumber(TextServer.gI().CHAT_SEXY.Count)]));
                        });
                        Character.Delay.BeautifulTalk = timeServer + 3000;
                    }
                    break;
                case 455:
                    if (Character.Delay.BeautifulTalk < timeServer)
                    {
                        Character.Zone.Characters.Values.Where(c => !c.InfoChar.IsDie && c.Id != Character.Id && Math.Abs(c.InfoChar.X - Character.InfoChar.X) <= 100 && Math.Abs(c.InfoChar.Y - Character.InfoChar.Y) <= 600).ToList().ForEach(temp =>
                        {
                            temp.CharacterHandler.SendZoneMessage(Service.PublicChat(temp.Id, "Hôi quá, Tránh xa ta ra!"));
                            temp.CharacterHandler.MineHp(temp.InfoChar.Hp * 10 / 100);
                            temp.CharacterHandler.SetUpInfo();
                        });
                        Character.Delay.BeautifulTalk = timeServer + 6000;
                    }
                    break;
                case 577:
                    if (Character.Delay.DelayHoaDa <= timeServer)
                    {
                        Character.Zone.Characters.Values.Where(c => !c.InfoChar.IsDie && c.Id != Character.Id && Math.Abs(c.InfoChar.X - Character.InfoChar.X) <= 100 && Math.Abs(c.InfoChar.Y - Character.InfoChar.Y) <= 600).ToList().ForEach(temp =>
                        {
                            var ifs = temp.InfoSkill;
                            ifs.HoaDa.IsHoaDa = true;
                            ifs.HoaDa.Time = 5000 + timeServer;
                            temp.CharacterHandler.SendZoneMessage(Service.UpdateBody(temp));
                            temp.CharacterHandler.SendZoneMessage(Service.SkillEffectPlayer(Character.Id, temp.Id, 1, 42));
                            temp.CharacterHandler.SendMessage(Service.PublicChat(temp.Id, "Khét !"));
                        });
                        Character.Delay.DelayHoaDa = timeServer + 15000;
                    }
                    break;
                case 1205:
                    if (Character.Delay.DelayHoaBang <= timeServer)
                    {
                        Character.Zone.Characters.Values.Where(c => !c.InfoChar.IsDie && c.Id != Character.Id && Math.Abs(c.InfoChar.X - Character.InfoChar.X) <= 100 && Math.Abs(c.InfoChar.Y - Character.InfoChar.Y) <= 600).ToList().ForEach(temp =>
                        {
                            temp.InfoSkill.HoaBang.isHoaBang = true;
                            temp.InfoSkill.HoaBang.Time = 7000 + timeServer;
                            temp.CharacterHandler.SendZoneMessage(Service.UpdateBody(temp));
                            temp.CharacterHandler.SendZoneMessage(Service.SkillEffectPlayer(temp.Id, temp.Id, 1, 42));
                            temp.CharacterHandler.SendMessage(Service.ItemTime(11085, 7));
                            temp.CharacterHandler.SendZoneMessage(EffectCharacter.sendInfoEffChar((short)temp.Id, (short)202, (byte)1, 1, (short)100, 1));
                        });
                        Character.Delay.DelayHoaBang = timeServer + 20000;
                    }
                    break;
                case 636:
                    if (Character.Delay.BeautifulTalk <= timeServer)
                    {
                        Character.Zone.Characters.Values.Where(c => !c.InfoChar.IsDie && c.Id != Character.Id && Math.Abs(c.InfoChar.X - Character.InfoChar.X) <= 200 && Math.Abs(c.InfoChar.Y - Character.InfoChar.Y) <= 600).ToList().ForEach(temp =>
                        {

                            temp.CharacterHandler.SendZoneMessage(Service.PublicChat(temp.Id, TextServer.gI().CHAT_FOR_MAI[ServerUtils.RandomNumber(TextServer.gI().CHAT_FOR_MAI.Count)]));
                            Character.Delay.BeautifulTalk = timeServer + 3000;
                        });
                    }
                    break;
                default:
                    var option = Character.ItemBody[5].Options.FirstOrDefault(i => i.Id == 8);
                    if (option != null && Character.Delay.DelayHupHp5s <= timeServer)
                    {
                        Character.Zone.Characters.Values.Where(c => !c.InfoChar.IsDie && c.Id != Character.Id && Math.Abs(c.InfoChar.X - Character.InfoChar.X) <= 100 && Math.Abs(c.InfoChar.Y - Character.InfoChar.Y) <= 600).ToList().ForEach(temp =>
                        {
                            Character.CharacterHandler.PlusHp((int)temp.InfoChar.Hp * option.Param / 100);
                            temp.CharacterHandler.MineHp((int)temp.InfoChar.Hp * option.Param / 100);
                            temp.CharacterHandler.SendMessage(Service.MeLoadPoint(temp));
                            Character.CharacterHandler.SendMessage(Service.MeLoadPoint(temp));
                            Character.CharacterHandler.SendZoneMessage(Service.PublicChat(Character.Id, "Két két két!, Hút máu đã thật 😎"));
                        });
                        Character.Delay.DelayHupHp5s = timeServer + 5000;
                    }
                    break;
            }
            if (loadPoint)
            {
                SetUpInfo();
                SendMessage(Service.MeLoadPoint(Character));
            }
        }
       
        public void UpdateLuyenTap()
        {
            var timeServer = ServerUtils.CurrentTimeMillis();
            if (Character.Delay.TrainGiapLuyenTap > timeServer) return;
            var item = Character.ItemBody[6];
            if (item == null || !ItemCache.ItemIsGiapLuyenTap(item.Id)) return;
            var optionCheck = item.Options.FirstOrDefault(option => option.Id == 9);
            if ((optionCheck.Param + 1) <= ItemCache.GetGiapLuyenTapLimit(item.Id))
            {
                optionCheck.Param += 1;
                SendMessage(Service.SendBag(Character));
            }
            Character.Delay.TrainGiapLuyenTap = 60000 + timeServer;
        }

        private void UpdateFusion(long timeServer)
        {
            var fusion = Character.InfoChar.Fusion;
            var disciple = Character.Disciple;
            if (disciple is { Status: 4 } && fusion.IsFusion)
            {
                // Update đệ đang hợp thể
                // if (disciple.InfoSkill.HuytSao.IsHuytSao && disciple.InfoSkill.HuytSao.Time <= timeServer)
                // {
                //     disciple.InfoSkill.HuytSao.IsHuytSao = false;
                //     disciple.InfoSkill.HuytSao.Time = -1;
                //     disciple.InfoSkill.HuytSao.Percent = 0;
                //     disciple.CharacterHandler.SetHpFull();

                //     if (disciple.InfoChar.Hp >= disciple.HpFull)
                //     {
                //         disciple.InfoChar.Hp = disciple.HpFull;
                //     }

                //     SetHpFull();
                //     if (Character.InfoChar.Hp >= Character.HpFull)
                //     {
                //         Character.InfoChar.Hp = Character.HpFull;
                //     }
                //     SendMessage(Service.MeLoadPoint(Character));
                //     SendZoneMessage(Service.PlayerLevel(Character));
                // }

                if (disciple.InfoSkill.Monkey.MonkeyId == 1 && disciple.InfoSkill.Monkey.TimeMonkey <= timeServer)
                {
                    // reset lại máu sư phụ
                    disciple.InfoSkill.Monkey.MonkeyId = 0;
                    disciple.InfoSkill.Monkey.HeadMonkey = -1;
                    disciple.InfoSkill.Monkey.BodyMonkey = -1;
                    disciple.InfoSkill.Monkey.LegMonkey = -1;
                    disciple.InfoSkill.Monkey.TimeMonkey = -1;
                    disciple.CharacterHandler.SetUpInfo();
                    SetUpInfo();
                    if (Character.InfoChar.Hp >= Character.HpFull)
                    {
                        Character.InfoChar.Hp = Character.HpFull;
                    }
                    SendMessage(Service.MeLoadPoint(Character));
                    SendZoneMessage(Service.PlayerLevel(Character));
                }

                if (timeServer >= fusion.TimeStart + fusion.TimeUse && (!fusion.IsPorata && !fusion.IsPorata2))
                {
                    disciple.CharacterHandler.SetUpPosition(isRandom: true);
                    Character.Zone.ZoneHandler.AddDisciple(disciple);
                    SendZoneMessage(Service.Fusion(Character.Id, 1));
                    lock (Character.InfoChar.Fusion)
                    {
                        Character.InfoChar.Fusion.IsFusion = false;
                        Character.InfoChar.Fusion.IsPorata = false;
                        Character.InfoChar.Fusion.IsPorata2 = false;
                        Character.InfoChar.Fusion.TimeStart = timeServer;
                        Character.InfoChar.Fusion.DelayFusion = timeServer + 600000;
                        Character.InfoChar.Fusion.TimeUse = 0;
                    }

                    disciple.Status = 0;
                    SetUpInfo();
                    SendZoneMessage(Service.UpdateBody(Character));
                    SendMessage(Service.SendBody(Character));
                    SendMessage(Service.PlayerLoadSpeed(Character));
                    SendMessage(Service.MeLoadPoint(Character));
                    SendMessage(Service.SendHp((int)Character.InfoChar.Hp));
                    SendMessage(Service.SendMp((int)Character.InfoChar.Mp));
                    SendZoneMessage(Service.PlayerLevel(Character));
                }
            }
        }

        public void SetUpPosition(int mapOld, int mapNew, bool isRandom = false)
        {
            PositionHandler.SetUpPosition(Character, mapOld, mapNew);
        }
        public void SetPos(short x, short y)
        {
            PositionHandler.SetupPositionXy(Character, x, y);
        }
        private void CheckExpireItem(Model.Item.Item item, int timeServer, int type)
        {
            try
            {
                // Kiểm tra xem có option HSD ngày hoặc giây không
                var expireOption = item.Options.FirstOrDefault(option => option.Id == 93);
                if (expireOption != null)
                {
                    // Kiểm tra hạn sử dụng
                    var expireTimeOption = item.Options.FirstOrDefault(option => (option.Id == 73));
                    if (expireTimeOption != null)
                    {
                        if (expireTimeOption.Param < timeServer)
                        {
                            switch (type)
                            {
                                case 0://body
                                    {
                                        RemoveItemBody(item.IndexUI);
                                        break;
                                    }
                                case 1:
                                    {
                                        RemoveItemBagByIndex(item.IndexUI, item.Quantity, false, reason: "Item hết hạn sử dụng");
                                        break;
                                    }
                                case 2:
                                    {
                                        RemoveItemBoxByIndex(item.IndexUI, item.Quantity, false);
                                        break;
                                    }
                                case 3:
                                    {
                                        RemoveItemLuckyBox(item.IndexUI, false);
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            // tính ngày từ giây
                            var leftTime = (expireTimeOption.Param - timeServer) / 1000;
                            expireOption.Param = ServerUtils.ConvertSecondToDay(leftTime);
                        }
                    }
                }
              
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error CheckExpireItem in CharacterHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }
        public int GetThoiVangInBag()
        {
            return soLuongThoiVangTrenNguoi;
        }
        public int GetThoiVangInRuong()
        {
            return soLuongThoiVangTrongRuong;
        }
        public static int soLuongThoiVangTrenNguoi = 0;
        public static int soLuongThoiVangTrongRuong = 0;
        public void SendInfo()
        {
            //Check hạn sử dụng item
            // Body Item

            var timeServer = ServerUtils.CurrentTimeSecond();
            Character.ItemBody.Where(item => item != null).ToList().ForEach(item =>
            {
                CheckExpireItem(item, timeServer, 0);
            });
            // Bag item
            Character.ItemBag.Where(item => item != null).ToList().ForEach(item =>
            {
                CheckExpireItem(item, timeServer, 1);
                if (item.Id == 457)
                {
                    soLuongThoiVangTrenNguoi += item.Quantity;
                }
            });
            // Box Item
            Character.ItemBox.Where(item => item != null).ToList().ForEach(item =>
            {
                CheckExpireItem(item, timeServer, 2);
                if (item.Id == 457)
                {
                    soLuongThoiVangTrongRuong += item.Quantity;
                }
            });

            // LuckyBox Item
            Character.LuckyBox.Where(item => item != null).ToList().ForEach(item =>
            {
                CheckExpireItem(item, timeServer, 3);
            });

          //  try
         //   {
                Character.ItemBody.Where(item => item != null).ToList().ForEach(item =>
                {
                    if (item.Id == 561)
                    {
                        if (item.GetParamOption(72) == 4 && item.GetParamOption(14) > 22)
                        {
                            var option = item.Options.FirstOrDefault(op => op.Id == 14);
                            if (option != null)
                            {
                                option.Param = 22;
                            }
                        }
                        else if (item.GetParamOption(72) == 5 && item.GetParamOption(14) > 24)
                        {
                            var option = item.Options.FirstOrDefault(op => op.Id == 14);
                            if (option != null)
                            {
                                option.Param = 24;
                            }
                        }
                        else if (item.GetParamOption(72) == 6 && item.GetParamOption(14) > 27)
                        {
                            var option = item.Options.FirstOrDefault(op => op.Id == 14);
                            if (option != null)
                            {
                                option.Param = 27;
                            }
                        }
                        else if (item.GetParamOption(72) == 7 && item.GetParamOption(14) > 30)
                        {
                            var option = item.Options.FirstOrDefault(op => op.Id == 14);
                            if (option != null)
                            {
                                option.Param = 30;
                            }
                        }
                        else if (item.GetParamOption(72) == 8 && item.GetParamOption(14) > 33)
                        {
                            var option = item.Options.FirstOrDefault(op => op.Id == 14);
                            if (option != null)
                            {
                                option.Param = 33;
                            }
                        }
                    }
                });

                // Bag item
                Character.ItemBag.Where(item => item != null).ToList().ForEach(item =>
                {
                    if (item.Id == 561)
                    {
                        if (item.GetParamOption(72) == 4 && item.GetParamOption(14) > 22)
                        {
                            var option = item.Options.FirstOrDefault(op => op.Id == 14);
                            if (option != null)
                            {
                                option.Param = 22;
                            }
                        }
                        else if (item.GetParamOption(72) == 5 && item.GetParamOption(14) > 24)
                        {
                            var option = item.Options.FirstOrDefault(op => op.Id == 14);
                            if (option != null)
                            {
                                option.Param = 24;
                            }
                        }
                        else if (item.GetParamOption(72) == 6 && item.GetParamOption(14) > 27)
                        {
                            var option = item.Options.FirstOrDefault(op => op.Id == 14);
                            if (option != null)
                            {
                                option.Param = 27;
                            }
                        }
                        else if (item.GetParamOption(72) == 7 && item.GetParamOption(14) > 30)
                        {
                            var option = item.Options.FirstOrDefault(op => op.Id == 14);
                            if (option != null)
                            {
                                option.Param = 30;
                            }
                        }
                        else if (item.GetParamOption(72) == 8 && item.GetParamOption(14) > 33)
                        {
                            var option = item.Options.FirstOrDefault(op => op.Id == 14);
                            if (option != null)
                            {
                                option.Param = 33;
                            }
                        }
                    }
                });

           // }
           // catch (Exception)
          //  {

          //  }

            // Kiểm tra vàng nhiều
            //Check đệ tử
            if (Character.InfoChar.IsHavePet)
            {
                Character.Disciple = DiscipleDB.GetById(-Character.Id);
                if (Character.Disciple != null)
                {
                    Character.Disciple.Character = Character;
                    Character.Disciple.Player = Character.Player;
                    Character.Disciple.CharacterHandler.SetUpPosition(isRandom: true);
                }
                else
                {
                    Character.InfoChar.IsHavePet = false;
                    Character.InfoChar.Fusion.Reset();
                }
            }

            //if (Character.InfoChar.PetId != -1)
            //{
            //    var PetItemIndex = Character.ItemBag.FirstOrDefault(item => item.Id == Character.InfoChar.PetId && item.GetParamOption(73) == Character.InfoChar.PetImei);

            //    if (PetItemIndex != null)
            //    {
            //        var pet = new Pet(PetItemIndex.Id, Character);
            //        Character.Pet = pet;
            //        Character.Pet.CharacterHandler.SetUpPosition(isRandom: true);
            //        Character.InfoMore.PetItemIndex = PetItemIndex.IndexUI;
            //    }
            //    else
            //    {//Không tìm thấy pet
            //        Character.InfoChar.PetId = -1;
            //        Character.InfoChar.PetImei = -1;
            //        Character.InfoMore.PetItemIndex = -1;
            //    }

            //}

            if (Character.ClanId == -1 || Character.ClanId == -100)
            {
                Character.InfoChar.Bag = -1;
            }
            else
            {
                var clan = ClanManager.Get(Character.ClanId);
                if (clan?.ClanHandler.GetMember(Character.Id) != null)
                {
                    Character.InfoChar.Bag = (sbyte)clan.ImgId;
                }
                else
                {
                    Character.ClanId = -1;
                    Character.InfoChar.Bag = -1;
                }
            }

            if (Character.InfoChar.OSkill.Count == 0)
            {
                Character.InfoChar.OSkill = new List<sbyte>() { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
            }

            if (Character.InfoChar.KSkill.Count == 0)
            {
                Character.InfoChar.KSkill = new List<sbyte>() { -1, -1, -1, -1, -1, -1,-1,-1,-1,-1 };
            }

            var maxPower = Cache.Gi().LIMIT_POWERS[DataCache.MAX_LIMIT_POWER_LEVEL - 1].Power;
            if (Character.InfoChar.Power > maxPower)
            {
                Character.InfoChar.Power = maxPower;
            }

            Character.InfoChar. Level = (sbyte)(Cache.Gi().EXPS.Count(exp => exp < Character.InfoChar.Power) - 1);
            if (maxPower > Character.InfoChar.Power)
            {
                Character.InfoChar.IsPower = true;
            }
            else
            {
                Character.InfoChar.IsPower = false;
            }

            SetUpInfo();
            if (Character.InfoChar.PhukienPart == -1) SendMessage(Service.SendImageBag(Character.Id, Character.InfoChar.Bag));
            SendZoneMessage(Service.PlayerLoadAll(Character));
            SendMessage(Service.SendBox(Character));
            SendMessage(Service.SendBag(Character));
            SendMessage(Service.SendBody(Character));
            SendMessage(Service.MeLoadPoint(Character));
            var task = Cache.Gi().TASK_TEMPLATES_0.Values.FirstOrDefault(i => i.Id == Character.InfoTask.Id);
            switch (Character.InfoChar.Gender)
            {
                case 0:
                    switch(Character.InfoTask.Index > task.SubNames.Count)
                    {
                        case true:
                            Character.InfoTask.Index = (sbyte)task.SubNames.Count;
                            break;
                    }
                    break;
                case 1:
                    task = Cache.Gi().TASK_TEMPLATES_1.Values.FirstOrDefault(i => i.Id == Character.InfoTask.Id);
                    switch (Character.InfoTask.Index > task.SubNames.Count)
                    {
                        case true:
                            Character.InfoTask.Index = (sbyte)task.SubNames.Count;
                            break;
                    }
                    break;
                case 2:
                    task = Cache.Gi().TASK_TEMPLATES_2.Values.FirstOrDefault(i => i.Id == Character.InfoTask.Id);
                    switch (Character.InfoTask.Index > task.SubNames.Count)
                    {
                        case true:
                            Character.InfoTask.Index = (sbyte)task.SubNames.Count;
                            break;
                    }
                    break;
            }
            switch (Character.InfoChar.LitmitPower)
            {
                case >= 13:
                    Character.InfoChar.idEff_Set_Item = 8;
                    break;
                case >= 11:
                    Character.InfoChar.idEff_Set_Item = 7;
                    break;
                case >= 9:
                    Character.InfoChar.idEff_Set_Item = 6;
                    break;
                case >= 7:
                    Character.InfoChar.idEff_Set_Item = 5;
                    break;
                case >= 5:
                    Character.InfoChar.idEff_Set_Item = 4;
                    break;
            }
            SendMessage(Service.SendTask(Character));
            SendMessage(Service.SpeacialSkill(Character, 0));
            SendMessage(Service.MyClanInfo(Character));
            SendMessage(Service.MeLoadAll(Character));
            SendMessage(Service.SendStamina(Character.InfoChar.Stamina));
            SendMessage(Service.SendMaxStamina(Character.InfoChar.MaxStamina));
            SendMessage(Service.SendNangDong(Character.InfoChar.NangDong));
            SendMessage(Service.GameInfo());
            SendMessage(Service.UpdateCooldown(Character));
            SendMessage(Service.ChangeOnSkill(Character.InfoChar.OSkill));
            if (Character.Skills.FirstOrDefault(sks=> sks.Id == (Character.InfoChar.Gender == 0 ? 24 : Character.InfoChar.Gender == 1 ? 25 : 26))!=null){
                var skill = Character.Skills.FirstOrDefault(sks=> sks.Id == (Character.InfoChar.Gender == 0 ? 24 : Character.InfoChar.Gender == 1 ? 25 : 26));
                SendMessage(Service.UpdateSkill0((short)skill.SkillId, skill.CurrExp));
            }
            SendZoneMessage(Service.UpdateBody(Character));
            if (Character.InfoChar.TypePk != 0)
            {
                Character.InfoChar.TypePk = 0;
                SendZoneMessage(Service.ChangeTypePk(Character.Id, 0));
            }
            if (Character.InfoChar.IsHavePet && Character.Disciple != null)
            {
                SendMessage(Service.Disciple(1, null));
            }
            else
            {
                SendMessage(Service.Disciple(0, null));
            }
            UpdateMountId();
            UpdatePhukien();
            UpdateHaoQuangDacBiet();
            UpdateEffectCharacter();
            Update_Linh_Thú();
            UpdateEffective();
           
            //if (Character.DataTraining.isTraining)
            //{
            //    TrainingHandler.gI().Training(Character);
            //}
            //  if (Character.isNRSD)
            //    {


            //      Character.UpdateOldMapNRSD();
            //     } else
            //  {
            Character.UpdateOldMap();
            //    }
            // Setup Bùa
            Character.SetupAmulet();
          //  Character.DataBoMong.Count[0] = (int)Character.InfoChar.Điểm_thành_tích;
          //  Character.DataBoMong.Count[1] = (int)Character.InfoChar.Điểm_thành_tích;

            // setup thời gian sáng tối
            // if (DatabaseManager.Manager.gI().SuKienTrungThu)
            // {
            //     SendMessage(Service.SetNight(Character));
            // }
        }
        public void SetUpPhoBan()
        {   
            var clan = ClanManager.Get(Character.ClanId);
            if (clan != null)
            {
                var time = (ServerUtils.CurrentTimeMillis() - clan.Reddot.timeDoanhTrai);
                if (clan.Reddot.Open)
                {
                    Character.CharacterHandler.SendMessage(Service.ItemTimeWithMessage("Doanh trại độc nhãn", 0, (int)time));
                }
            }
        } 
        public void SendDie()
        {
            lock (Character)
            {
                RemoveSkill(ServerUtils.CurrentTimeMillis(), true);
                Character.InfoChar.IsDie = true;
                Character.InfoSkill.Monkey.MonkeyId = 0;
                SetUpInfo();
                SendMessage(Service.PlayerLoadSpeed(Character));
                // SendZoneMessage(Service.UpdateBody(Character));
                SendMessage(Service.MeLoadPoint(Character));
                SendMessage(Service.MeLoadInfo(Character));
                SendMessage(Service.MeDie(Character, 0));
                SendZoneMessage(Service.PlayerDie(Character));
                LeaveGold();
                if (Character.DataNgocRongNamek.AlreadyPick(Character))
                {
                    var itm = new ItemMap(-1, ItemCache.GetItemDefault((short)(Character.DataNgocRongNamek.IdNamekBall)));
                    itm.X = Character.InfoChar.X;
                    itm.Y = Character.InfoChar.Y;
                    Character.Zone.ZoneHandler.LeaveItemMap(itm);
                    Character.InfoChar.TypePk = 0;
                    Character.DataNgocRongNamek.IdNamekBall = -1;
                    Character.InfoChar.Bag = ClanManager.Get(Character.ClanId) != null ? (sbyte)ClanManager.Get(Character.ClanId).ImgId : (sbyte)-1;
                    UpdatePhukien();
                }
                if (Character.Blackball.AlreadyPick(Character)){
                    Character.Blackball.ExitMapOrDie(Character);
                }
                if (Character.Challenge.isChallenge){
                    var player = (Model.Character.Character)ClientManager.Gi().GetCharacter(Character.Challenge.PlayerChallengeID);
                    var gold = (player.Challenge.Gold - (Character.Challenge.Gold / 100)) + (player.Challenge.Gold - (Character.Challenge.Gold / 100));
                    player.CharacterHandler.SendMessage(Service.ServerMessage($"Đối thủ đã kiệt sức,bạn đã nhận được {gold} vàng"));
                    player.PlusGold(gold);
                    player.CharacterHandler.SendMessage(Service.MeLoadInfo(player));
                    Character.Challenge.SetStatusEnd(Character);
                    player.Challenge.SetStatusEnd(player);
                }
                if (Character.DataDaiHoiVoThuat.IdCharacterFight != -1 && Character.InfoChar.MapId == 113){
                    var charPK = (Model.Character.Character)ClientManager.Gi().GetCharacter(Character.DataDaiHoiVoThuat.IdCharacterFight);
                    ChampionShip.gI().WinRound(charPK);
                }
               
                if (Character.Trade.IsTrade)
                {
                    var charTemp = (Model.Character.Character)Character.Zone.ZoneHandler.GetCharacter(Character.Trade.CharacterId);
                    if (charTemp != null && charTemp.Trade.CharacterId == Character.Id)
                    {
                        charTemp.CloseTrade(true);
                        charTemp.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().CLOSE_TRADE));
                    }
                    Character.CloseTrade(true);
                }
                
            }
        }
        public void PlusDiamondLock(int diamond)
        {
            Character.PlusDiamondLock(diamond);
            SendMessage(Service.MeLoadInfo(Character));
        }
        public void ClearTest()
        {
            if (Character.Test != null)
            {
                Character.Test.IsTest = false;
                Character.Test.TestCharacterId = -1;
                Character.Test.GoldTest = 0;
            }
            Character.InfoChar.TypePk = 0;
            SendZoneMessage(Service.ChangeTypePk(Character.Id, 0));
        }

        public void RemoveTroi(int charId)
        {
            var infoSkill = Character.InfoSkill.PlayerTroi;
            if (infoSkill.IsPlayerTroi)
            {
                infoSkill.PlayerId.RemoveAll(i => i == charId);
                if (infoSkill.PlayerId.Count <= 0)
                {
                    infoSkill.IsPlayerTroi = false;
                    infoSkill.TimeTroi = -1;
                    infoSkill.PlayerId.Clear();
                    SendZoneMessage(Service.SkillEffectPlayer(charId, Character.Id, 2, 32));
                }
            }
        }

        private void LeaveGold()
        {
            var quantity = Character.InfoChar.Gold switch
            {
                > 1000 and <= 500000 => Character.InfoChar.Gold / 30,
                > 500000 and <= 1000000 => ServerUtils.RandomNumber(10000, 20000),
                > 1000000 and <= 100000000 => ServerUtils.RandomNumber(20000, 30000),
                > 100000000 => ServerUtils.RandomNumber(30000, 50000),
                _ => 1
            };
            var itemMap = LeaveItemHandler.LeaveGoldPlayer(Character.Id, (int)quantity);
            itemMap.X = Character.InfoChar.X;
            itemMap.Y = Character.InfoChar.Y;
            Character.Zone.ZoneHandler.LeaveItemMap(itemMap);
        }

        public void UpdateMountId()
        {
            var itemBag = Character.ItemBag.FirstOrDefault(item => DataCache.IdMount.Contains(item.Id));
            var ver = int.Parse(Character.Player.Session.Version.Replace(".", ""));
            if (ver < 220) {
                itemBag = Character.ItemBody[8];
            }
                if (itemBag != null)
                {
                    var id = itemBag.Id;
                    id = id switch
                    {
                        733 => 30001,
                        734 => 30002,
                        735 => 30003,
                        743 => 30004,
                        744 => 30005,
                        746 => 30006,
                        795 => 30007,
                        849 => 30008,
                        897 => 30009,
                        920 => 30010,
                        1092 => 30011,
                        1139 => 30012,
                        1144 => 30013,
                        1172 => 30014,
                        1159 => 30015,
                        1260 => 30016,
                        _ => id
                    };

                    Character.InfoChar.MountId = id;
                }
            
            else
            {
                Character.InfoChar.MountId = -1;
            }
        }
        public void Update_Linh_Thú()
        {
            var itemBody = Character.ItemBody[10];
            if (itemBody != null)
            {
                var Linh_Thú_Template = Cache.Gi().LinhThu.Values.FirstOrDefault(a => a.Id == itemBody.Id);
                if (Linh_Thú_Template != null)
                {
                 //   Character.InfoChar.Linh_Thú_ID = Linh_Thú_Template.Id;
                 //   Character.InfoChar.LinhThuFrame = Linh_Thú_Template.Frame;
                 //   Character.InfoChar.LinhThuImage = Linh_Thú_Template.IdImage;
                    Character.CharacterHandler.SendMessage(Epic_Pet.Call_EpicPet(Character, Linh_Thú_Template.IdImage, Linh_Thú_Template.Frame));
                }
                else
                {
                    Character.CharacterHandler.SendMessage(Service.DialogMessage("Cannot Find Data Linh Thu In Database !"));
                }
            }
            else
            {
              //  Character.InfoChar.Linh_Thú_ID = -1;
                Character.CharacterHandler.SendMessage(Epic_Pet.Remove_EpicPet(Character));
            }
        }
        public void UpdatePet()
        {
            if (Character != null)
            {
                var itemBody = Character.ItemBody[9];
                    
                if (itemBody != null)
                {
                    var pet = Character.Pet;
                    if (pet != null)
                    {
                        Character.Zone.ZoneHandler.RemovePet(pet);
                        pet = new Pet(itemBody.Id, Character);
                        Character.Pet = pet;
                        Character.Pet.CharacterHandler.SetUpPosition(isRandom: true);
                        Character.InfoChar.PetId = itemBody.Id;
                        Character.Zone.ZoneHandler.AddPet(pet);
                    }
                    else
                    {
                        pet = new Pet(itemBody.Id, Character);
                        Character.Pet = pet;
                        Character.Pet.CharacterHandler.SetUpPosition(isRandom: true);
                        Character.InfoChar.PetId = itemBody.Id;
                        Character.Zone.ZoneHandler.AddPet(pet);
                    }
                }
            }
        }
        public void UpdateHaoQuangDacBiet()
        {
            var auraId = Character.InfoChar.EffectAuraId;
            if (auraId == -1) return;
            Character.CharacterHandler.SendMessage(Service.Radar4(Character.Id, auraId));

        }
        public void UpdateEffective()
        {
            var itemBody = Character.ItemBody[9];
            if (itemBody != null)
            {
                var timeServer = ServerUtils.CurrentTimeMillis();
                switch (itemBody.Id)
                {
                    case 1202:
                    case 1203:
                    case 1207:
                        Character.InfoBuff.delayEnchantCrit = 20000 + timeServer;
                        Character.InfoBuff.isEnchantCrit = false;
                        Character.InfoBuff.isActiveCrit = true;
                        break;
                    case 1230:
                    case 1231:
                    case 1232:
                        Character.InfoBuff.delayEnchantGiap = 30000 + timeServer;
                        Character.InfoBuff.isEnchantGiap = false;
                        Character.InfoBuff.isActiveGiap = true;
                        break;
                }
            }
        }
        public void UpdatePhukien()
        {
            if (GetItemBagById(78)!= null)
            {
                Character.InfoChar.PhukienPart = 78;
                Character.CharacterHandler.SendZoneMessage(Service.SendImageBag(Character.Id, 109));
                return;
            }
            switch (Character.InfoChar.Bag)
            {
                case 107:
                case 108:
                    Character.InfoChar.PhukienPart = Character.InfoChar.Bag;
                    Character.CharacterHandler.SendZoneMessage(Service.SendImageBag(Character.Id, Character.InfoChar.Bag));
                    break;
                default:
                    var itemBody = Character.ItemBody[7];
                    if (itemBody != null)
                    {
                        var itemTemplate = ItemCache.ItemTemplate(itemBody.Id);
                        Character.InfoChar.PhukienPart = itemTemplate.Part;
                        Character.CharacterHandler.SendZoneMessage(Service.SendImageBag(Character.Id, itemTemplate.Part));

                    }
                    else
                    {
                        Character.InfoChar.PhukienPart = -1;
                        Character.CharacterHandler.SendZoneMessage(Character.ClanId == -1 || Character.ClanId == -100
                            ? Service.SendImageBag(Character.Id, -1)
                            : Service.SendImageBag(Character.Id, Character.InfoChar.Bag));
                    }
                    break;
            }
        }
        public void UpdateEffectCharacter()
        {
            
            if (Character.InfoSet.IsFullSetNhatAn) Character.CharacterHandler.SendMessage(EffectCharacter.sendInfoEffChar((short)Character.Id, (short)56, (byte)1, -1, (short)10, 1));
            if (Character.InfoSet.IsFullSetTinhAn) Character.CharacterHandler.SendMessage(EffectCharacter.sendInfoEffChar((short)Character.Id, (short)57, (byte)1, -1, (short)10, 1));
            if (Character.InfoSet.IsFullSetNguyetAn) Character.CharacterHandler.SendMessage(EffectCharacter.sendInfoEffChar((short)Character.Id, (short)58, (byte)1, -1, (short)10, 1));
            
        }
        public bool GetParamItemExist(int id)
        {
            var exist = false;
            Character.ItemBody.Where(item => item != null).ToList().ForEach(item =>
            {
                if (item.Options.Where(option => option.Id == id).ToList().Count > 0) exist = true;
            });
            
            return exist;
        }
        public int GetParamItemExistCount(int id)
        {
            var count = 0;
            Character.ItemBody.Where(item => item != null).ToList().ForEach(item =>
            {
                if (item.isHaveOption(id)) count++;

            });
            return count;
        }
        public int GetParamItem(int id)
        {
            var param = 0;
            Character.ItemBody.Where(item => item != null).ToList().ForEach(item =>
            {
                var option = item.Options.Where(option => option.Id == id).ToList();
                param += option.Sum(optionItem => optionItem.Param);
            });
            Character.InfoChar.Cards.Values.Where(r => r.Used == 1).ToList().ForEach(r =>
            {
                foreach (var optionRadar in r.Options.Where(optionRadar => optionRadar.Id == id))
                {
                    if (optionRadar.ActiveCard == r.Level)
                    {
                        param += optionRadar.Param;
                    }
                    else if (r.Level == -1 && optionRadar.ActiveCard == 0)
                    {
                        param += optionRadar.Param;
                    }
                }
            });
            // itemBag = Character.ItemBag.FirstOrDefault(item => ItemCache.ItemTemplate(item.Id).Type == 11);
            //if (itemBag != null) param += itemBag.GetParamOption(id);

            //if (Character.InfoMore.PetItemIndex != -1)
            //{
            //    var petItem = Character.ItemBag.FirstOrDefault(item => item.IndexUI == Character.InfoMore.PetItemIndex);
            //    if (petItem != null) param += petItem.GetParamOption(id);
            //}
            return param;
        }

        public List<int> GetListParamItem(int id)
        {
            var param = new List<int>();
            Character.ItemBody.Where(item => item != null).ToList().ForEach(item =>
            {
                var option = item.Options.Where(option => option.Id == id).ToList();
                param.AddRange(option.Select(optionItem => optionItem.Param));
            });
            Character.InfoChar.Cards.Values.Where(r => r.Used == 1).ToList().ForEach(r =>
            {
                foreach (var optionRadar in r.Options.Where(optionRadar => optionRadar.Id == id))
                {
                    if (optionRadar.ActiveCard == r.Level)
                    {
                        param.Add(optionRadar.Param);
                    }
                    else if (r.Level == -1 && optionRadar.ActiveCard == 0)
                    {
                        param.Add(optionRadar.Param);
                    }
                }
            });

            //var itemBag = Character.ItemBag.FirstOrDefault(item => ItemCache.ItemTemplate(item.Id).Type == 11);
            //if (itemBag != null) param.Add(itemBag.GetParamOption(id));

            //if (Character.InfoMore.PetItemIndex != -1)
            //{
            //    var petItem = Character.ItemBag.FirstOrDefault(item => item.IndexUI == Character.InfoMore.PetItemIndex);
            //    if (petItem != null) param.Add(petItem.GetParamOption(id));
            //}
            return param;
        }

        public void SetUpFriend()
        {
            Character.Me = new InfoFriend(Character);
            Character.Friends.ForEach(friend =>
            {
                var charCheck = (Model.Character.Character)ClientManager.Gi().GetCharacter(friend.Id);
                friend = charCheck != null ? new InfoFriend(charCheck) : CharacterDB.GetInfoCharacter(friend.Id);
            });
        }

        public void SetUpInfo()
        {
            SetInfoSet();
            SetHpFull();
            SetMpFull();
            SetDamageFull();
            SetDefenceFull();
            SetCritFull();
            SetSpeed();
            SetHpPlusFromDamage();
            SetMpPlusFromDamage();
            SetBuffMp1s();
            SetBuffHp5s();
            SetBuffHp10s();
            SetBuffHp30s();
            SetTuDongLuyenTap();
            SetEnhancedOption();
            SetInfoBuff();
        }

        private void SetupPetIndex()
        {
            //if (Character.InfoChar.PetId != -1)
            //{
            //    var PetItemIndex = Character.ItemBag.FirstOrDefault(item => item.Id == Character.InfoChar.PetId && item.GetParamOption(73) == Character.InfoChar.PetImei);
            //    if (PetItemIndex != null)
            //    {
            //        Character.InfoMore.PetItemIndex = PetItemIndex.IndexUI;
            //    }
            //    else
            //    {
            //        Character.InfoMore.PetItemIndex = -1;
            //    }
            //}
            //else
            //{
            //    Character.InfoMore.PetItemIndex = -1;
            //}
        }

        public void SetInfoSet()
        {
            Character.InfoSet.Reset();
            Character.InfoSet.IsFullSetThanLinh = true;
            for (int i = 0; i < 5; i++)
            {
                if (Character.ItemBody[i] == null || Character.ItemBody[i].Id > 567 || Character.ItemBody[i].Id < 555)
                {
                    Character.InfoSet.IsFullSetThanLinh = false;
                    break;
                }
            }           
            Character.InfoSet.IsFullSetHuyDiet = true;
            for (int i = 0; i < 5; i++)
            {
                if (Character.ItemBody[i] == null || ItemCache.ItemTemplate(Character.ItemBody[i].Id).Level != 14)
                {
                    Character.InfoSet.IsFullSetHuyDiet = false;
                    break;
                }
            }
            Character.InfoSet.IsFullSetTinhAn = GetParamItemExistCount(34) >= 5;
            Character.InfoSet.IsFullSetNguyetAn = GetParamItemExistCount(35) >= 5;
            Character.InfoSet.IsFullSetNhatAn = GetParamItemExistCount(36) >= 5;
            switch (Character.InfoChar.Gender)
            {
                case 0:
                    {
                        Character.InfoSet.IsFullSetThienXinHang = GetParamItemExistCount(127) >= 5;
                        Character.InfoSet.IsFullSetKirin = GetParamItemExistCount(128) >= 5;
                        Character.InfoSet.IsFullSetSongoku = GetParamItemExistCount(129) >= 5;
                        break;
                    }
                case 1:
                    {
                        Character.InfoSet.IsFullSetPicolo = GetParamItemExistCount(130) >= 5;
                        Character.InfoSet.IsFullSetOcTieu = GetParamItemExistCount(131) >= 5;
                        Character.InfoSet.IsFullSetPikkoro = GetParamItemExistCount(132) >= 5;
                        Character.InfoSet.IsFullSetZelot = GetParamItemExistCount(213) >= 5;
                    }
                    break;
                case 2:
                    {
                        Character.InfoSet.IsFullSetKakarot = GetParamItemExistCount(133) >= 5;
                        Character.InfoSet.IsFullSetCadic = GetParamItemExistCount(134) >= 5;
                        Character.InfoSet.IsFullSetNappa = GetParamItemExistCount(135) >= 5;
                    }
                    break;
            }
        }

        public void SetInfoBuff()
        {
            var timeServer = ServerUtils.CurrentTimeMillis();
            // thuc an
            // Kiểm tra thời gian thức ăn và hiện item time
            // Effect thức ăn
            
            if (Character.InfoBuff.ThucAnTime > timeServer && Character.InfoBuff.ThucAnId != -1)
            {
                var giayConLai = (Character.InfoBuff.ThucAnTime - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                var template = ItemCache.ItemTemplate(Character.InfoBuff.ThucAnId);
                SendMessage(Service.ItemTime(template.IconId, (int)giayConLai));
            }
            // Effect banh trung thu
            if (Character.InfoBuff.BanhTrungThuTime > timeServer && Character.InfoBuff.BanhTrungThuId != -1)
            {
                var giayConLai = (Character.InfoBuff.BanhTrungThuTime - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                var template = ItemCache.ItemTemplate(Character.InfoBuff.BanhTrungThuId);
                SendMessage(Service.ItemTime(template.IconId, (int)giayConLai));
            }
            if (Character.InfoBuff.KichDucX2Time > timeServer)
            {
                var giayConLai = (Character.InfoBuff.KichDucX2Time - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                SendMessage(Service.ItemTime(9374, (int)giayConLai));
            }
            if (Character.InfoBuff.KichDucX5Time > timeServer)
            {
                var giayConLai = (Character.InfoBuff.KichDucX5Time - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                SendMessage(Service.ItemTime(9443, (int)giayConLai));
            }
            if (Character.InfoBuff.KichDucX7Time > timeServer)
            {
                var giayConLai = (Character.InfoBuff.KichDucX7Time - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                SendMessage(Service.ItemTime(9444, (int)giayConLai));
            }
            // Effect cuồng nộ
            if (Character.InfoBuff.CuongNoTime > timeServer)
            {
                var giayConLai = (Character.InfoBuff.CuongNoTime - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                SendMessage(Service.ItemTime(2754, (int)giayConLai));
            }
            if (Character.InfoBuff.XiMuoiHoaDaoTime > timeServer)
            {
                var giayConLai = (Character.InfoBuff.XiMuoiHoaDaoTime - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                SendMessage(Service.ItemTime(10905, (int)giayConLai));
            }
            if (Character.InfoBuff.XiMuoiHoaMaiTime > timeServer)
            {
                var giayConLai = (Character.InfoBuff.XiMuoiHoaMaiTime - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                SendMessage(Service.ItemTime(10904, (int)giayConLai));
            }
            if (Character.InfoBuff.BinhChuaCommesonTime > timeServer)
            {
                var giayConLai = (Character.InfoBuff.BinhChuaCommesonTime - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                SendMessage(Service.ItemTime(5829, (int)giayConLai));
            }
            // Effect Bổ huyết
            if (Character.InfoBuff.BoHuyetTime > timeServer)
            {
                var giayConLai = (Character.InfoBuff.BoHuyetTime - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                SendMessage(Service.ItemTime(2755, (int)giayConLai));
            }
            // Effect Bo Khi
            if (Character.InfoBuff.BoKhiTime > timeServer)
            {
                var giayConLai = (Character.InfoBuff.BoKhiTime - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                SendMessage(Service.ItemTime(2756, (int)giayConLai));
            }
            // Effect giap xen
            if (Character.InfoBuff.GiapXenTime > timeServer)
            {
                var giayConLai = (Character.InfoBuff.GiapXenTime - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                SendMessage(Service.ItemTime(2757, (int)giayConLai));
            }
            // Effect An danh
            if (Character.InfoBuff.AnDanhTime > timeServer)
            {
                var giayConLai = (Character.InfoBuff.AnDanhTime - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                SendMessage(Service.ItemTime(2760, (int)giayConLai));
            }



            if (Character.InfoBuff.CuongNoTime2 > timeServer)
            {
                var giayConLai = (Character.InfoBuff.CuongNoTime2 - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                SendMessage(Service.ItemTime(10716, (int)giayConLai));
            }
            // Effect Bổ huyết
            if (Character.InfoBuff.BoHuyetTime2 > timeServer)
            {
                var giayConLai = (Character.InfoBuff.BoHuyetTime2 - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                SendMessage(Service.ItemTime(10715, (int)giayConLai));
            }
            // Effect Bo Khi
            if (Character.InfoBuff.BoKhiTime2 > timeServer)
            {
                var giayConLai = (Character.InfoBuff.BoKhiTime2 - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                SendMessage(Service.ItemTime(10714, (int)giayConLai));
            }
            // Effect giap xen
            if (Character.InfoBuff.GiapXenTime2 > timeServer)
            {
                var giayConLai = (Character.InfoBuff.GiapXenTime2 - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                SendMessage(Service.ItemTime(10712, (int)giayConLai));
            }
            // Effect An danh
            if (Character.InfoBuff.AnDanhTime2 > timeServer)
            {
                var giayConLai = (Character.InfoBuff.AnDanhTime2 - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                SendMessage(Service.ItemTime(10717, (int)giayConLai));
            }
            // Effect Do CSKB
            if (Character.InfoBuff.MayDoCSKBTime > timeServer)
            {
                var giayConLai = (Character.InfoBuff.MayDoCSKBTime - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                SendMessage(Service.ItemTime(2758, (int)giayConLai));
            }
            // Effect Cu Ca Rot
            if (Character.InfoBuff.CuCarotTime > timeServer)
            {
                var giayConLai = (Character.InfoBuff.CuCarotTime - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                SendMessage(Service.ItemTime(4082, (int)giayConLai));
            }
        }

        public void SetEnhancedOption()
        {
            Character.InfoOption.Reset();

            Character.InfoOption.PhanPercentSatThuong += GetParamItem(97);

            Character.InfoOption.PhanTramXuyenGiapChuong += GetParamItem(98);

            Character.InfoOption.PhanTramXuyenGiapCanChien += GetParamItem(99);

            Character.InfoOption.PhanTramNeDon += GetParamItem(108);

            Character.InfoOption.PhanTramVangTuQuai += GetParamItem(100);

            Character.InfoOption.PhanTramTNSM += GetParamItem(101);

            Character.InfoOption.PhanTramHutHP += GetParamItem(95);

            Character.InfoOption.PhanTramHutKI += GetParamItem(96);
            Character.InfoOption.PhanTramSatThuongChiMang += GetParamItem(5);
            Character.InfoOption.VoHieuHoaChuong += GetParamItem(3);

            Character.InfoOption.HieuUngLua = GetParamItemExist(165);
            Character.InfoOption.QuanBoi = GetParamItemExist(215);
            Character.InfoOption.X2TiemNang = GetParamItemExist(155);
            
        }

        public void SetTuDongLuyenTap()
        {
            var timeServer = ServerUtils.CurrentTimeMillis();
            if (Character.InfoChar.TimeAutoPlay > 0)
            {
                var giayConLai = (Character.InfoChar.TimeAutoPlay - timeServer) / 1000;
                if (giayConLai <= 0) giayConLai = 0;
                SendMessage(Service.ItemTime(4387, (int)giayConLai));
                SendMessage(Service.AutoPlay(true));
            }
        }

        public void SetHpFull()
        {
            var hp = Character.InfoChar.OriginalHp;
            hp += GetParamItem(2) * 100;
            hp += GetParamItem(6);
            hp += GetParamItem(22) * 1000;
            hp += GetParamItem(48);
            //GetListParamItem(77).ForEach(param => hp += hp * param / (300 + param));
            GetListParamItem(77).ForEach(param => hp += hp * param / 100);
            GetListParamItem(109).ForEach(param => hp -= hp * param / (300 + param));
            if (Character.DataEnchant.PhuHoMabu2h)
            {
                hp += 1000000;
            }
            if (Character.Blackball.CurrentListBuff.Contains(2)) hp += hp * 20 / 100;
            if (Character.InfoSet.IsFullSetNguyetAn)
            {
                hp += hp * 30 / 100;
            }
            if (Character.InfoBuff.XiMuoiHoaMai)
            {
                hp += hp * 20 / 100;
            }
            if (Character.InfoOption.X2TiemNang) hp /= 2;
            if (Character.Blackball.CurrentPercentPlusHp != -1 && Character.Blackball.AlreadyPick(Character)){
                hp *= Character.Blackball.CurrentPercentPlusHp;
            }
            if (Character.InfoChar.Fusion.IsFusion && Character.Disciple != null) {
                // Đệ ma bư +120%
                var disHP = Character.Disciple.HpFull;

                if (Character.Disciple.Type == 2)
                {
                    hp += (disHP + (disHP * 20 / 100));
                }
                // đệ mua 150%
                if (Character.Disciple.Type == 3 || Character.Disciple.Type == 4 || Character.Disciple.Type == 5 || Character.Disciple.Type == 7 || Character.Disciple.Type == 8 || Character.Disciple.Type == 6)
                {
                    hp += (disHP + (disHP * 50 / 100));
                }
                //if (Character.Disciple.Type == 4)
                //{
                //    hp += (disHP + (disHP * 40 + (Character.Disciple.InfoChar.Power > 150000000000 ? 10 : 0) / 100));
                //}

                // Đệ thường +100%
                else if (Character.Disciple.Type == 1)
                {
                    hp += disHP;
                }
                // Bông tai porata 2
                if (Character.InfoChar.Fusion.IsPorata2)
                {
                    var bongTaiPorata2 = GetItemBagById(921);
                    if (bongTaiPorata2 != null)
                    {
                        var optionCheck = bongTaiPorata2.Options.FirstOrDefault(option => option.Id != 72);
                        if (optionCheck != null && optionCheck.Id == 77)
                        {
                            hp += hp * optionCheck.Param / 100;
                        }
                    }
                    // bông tai pt2 tăng 10%
                    hp += hp * 10 / 100;
                }
            }
           
           
            // Nappa
            if (Character.InfoSet.IsFullSetNappa)
            {
                hp += hp * 80 / 100;
            }
            if (Character.isColer)
            {
                hp -= hp / 2;
            }
            if (Character.InfoSkill.Monkey.MonkeyId != 0)
            {
                hp += hp * Character.InfoSkill.Monkey.Hp / 100;
            }

            if (Character.InfoSkill.HuytSao.IsHuytSao)
            {
                hp += hp * Character.InfoSkill.HuytSao.Percent / 100;
            }
            // Bổ Huyết
            if (Character.InfoBuff.BoHuyet)
            {
                hp += hp;
            }
            if (Character.InfoBuff.BoHuyet2)
            {
                hp *= 2;
            }
            if (Character.InfoBuff.BanhTrungThuId != -1)
            {
                switch (Character.InfoBuff.BanhTrungThuId)
                {
                    case 465:
                        {
                            hp += hp * 5 / 100;
                            break;
                        }
                    case 466:
                        {
                            hp += hp * 10 / 100;
                            break;
                        }
                    case 472:
                        {
                            hp += hp * 15 / 100;
                            break;
                        }
                    case 473:
                        {
                            hp += hp * 20 / 100;
                            break;
                        }
                }
            }


            Character.HpFull = hp;
        }

        public void SetMpFull()
        {
            var mp = Character.InfoChar.OriginalMp;
            mp += GetParamItem(2) * 100;
            mp += GetParamItem(7);
            mp += GetParamItem(23) * 1000;
            mp += GetParamItem(48);
            //GetListParamItem(103).ForEach(param => mp += mp * param / (300 + param) );
            GetListParamItem(103).ForEach(param => mp += mp * param / 100);
            if (Character.DataEnchant.PhuHoMabu2h)
            {
                mp += 1000000;
            }
            if (Character.InfoSet.IsFullSetNhatAn)
                {
                    mp += mp * 30 / 100;
                }
            if (Character.Blackball.CurrentListBuff.Contains(3)) mp += mp * 20 / 100;

            if (Character.InfoOption.X2TiemNang) mp /= 2;
            if (Character.InfoChar.Fusion.IsFusion && Character.Disciple != null) {
                // Đệ ma bư +120%
                if (Character.Disciple.Type == 2)
                {
                    mp += (Character.Disciple.MpFull + (Character.Disciple.MpFull * 20 / 100));
                }
                //đệ mua +150
                if (Character.Disciple.Type == 3 || Character.Disciple.Type == 4 || Character.Disciple.Type == 5 || Character.Disciple.Type == 6 || Character.Disciple.Type == 7 || Character.Disciple.Type == 8)
                {
                    mp += (Character.Disciple.MpFull + (Character.Disciple.MpFull * 50 / 100));
                }
                //if (Character.Disciple.Type == 4)
                //{
                //    mp += (Character.Disciple.MpFull + (Character.Disciple.MpFull * 40  + (Character.Disciple.InfoChar.Power > 150000000000 ? 10 : 0) / 100));
                //}
                // Đệ thường +100%
                else if (Character.Disciple.Type == 1)
                {
                    mp += Character.Disciple.MpFull;
                }
                
                // Bông tai porata 2
                if (Character.InfoChar.Fusion.IsPorata2)
                {
                    var bongTaiPorata2 = GetItemBagById(921);
                    if (bongTaiPorata2 != null)
                    {
                        var optionCheck = bongTaiPorata2.Options.FirstOrDefault(option => option.Id != 72);
                        if (optionCheck != null && optionCheck.Id == 103)
                        {
                            mp += mp * optionCheck.Param / 100;
                        }
                    }

                    mp += mp * 10 / 100;
                }
            }
            
            if (Character.isColer)
            {
                mp -= mp / 2;
            }
            if (Character.InfoBuff.BanhTrungThuId != -1)
            {
                switch (Character.InfoBuff.BanhTrungThuId)
                {
                    case 465:
                        {
                            mp += mp * 5 / 100;
                            break;
                        }
                    case 466:
                        {
                            mp += mp * 10 / 100;
                            break;
                        }
                    case 472:
                        {
                            mp += mp * 15 / 100;
                            break;
                        }
                    case 473:
                        {
                            mp += mp * 20 / 100;
                            break;
                        }
                }
            }

            // Bổ khí
            if (Character.InfoBuff.BoKhi)
            {
                mp += mp;
            }
            if (Character.InfoBuff.BoKhi2)
            {
                mp *= 2;
            }
            Character.MpFull = mp;
        }

        
        public void SetDamageFull()
        {
            var damage = Character.InfoChar.OriginalDamage;
            damage += GetParamItem(0);
            //GetListParamItem(50).ForEach(param => damage += damage * param / (300 + param));
            GetListParamItem(50).ForEach(param => damage += damage * param / 100);
            GetListParamItem(147).ForEach(param => damage += damage * param / (300 + param)) ;
            if (Character.DataEnchant.PhuHoMabu2h)
            {
                damage += 100000;
            }
            if (Character.InfoOption.X2TiemNang) damage /= 2;
            if (Character.InfoSet.IsFullSetTinhAn)
            {
                damage += damage * 30 / 100;
            }
            if (Character.DataEnchant.MiNuong)
            {
                damage += damage * 24 / 100;
            }
            if (Character.Blackball.CurrentListBuff.Contains(1)) damage += damage * 20 / 100;
            if (Character.InfoChar.Fusion.IsFusion && Character.Disciple != null) {
                // Đệ ma bư +120%
                var disDmg = Character.Disciple.DamageFull;

                if (Character.Disciple.Type == 2 )
                {
                    damage += (disDmg + (disDmg * 20 / 100));
                }
                // đệ mua
                if (Character.Disciple.Type == 3 || Character.Disciple.Type == 4 || Character.Disciple.Type == 5 || Character.Disciple.Type == 6 || Character.Disciple.Type == 7 || Character.Disciple.Type == 8)
                {
                    damage += (disDmg + (disDmg * 50 / 100));
                }
                //    if (Character.Disciple.Type == 4)
                //{
                //    damage += (disDmg + (disDmg * 40 + (Character.Disciple.InfoChar.Power > 150000000000 ? 10 : 0)/ 100));
                //}
                // Đệ thường +100%
                if (Character.Disciple.Type == 1)
                {
                    damage += disDmg;
                }
                if (Character.InfoBuff.effRongXuong)
                {
                    damage += damage * 20 / 100;
                }
                // Bông tai porata 2
                if (Character.InfoChar.Fusion.IsPorata2)
                {
                    var bongTaiPorata2 = GetItemBagById(921);
                    if (bongTaiPorata2 != null)
                    {
                        var optionCheck = bongTaiPorata2.Options.FirstOrDefault(option => option.Id != 72);
                        if (optionCheck != null && optionCheck.Id == 50)
                        {
                            damage += damage * optionCheck.Param / 100;
                        }
                    }

                    damage += damage * 10 / 100;
                }
            }

            if (Character.InfoSkill.Monkey.MonkeyId != 0) damage += damage * Character.InfoSkill.Monkey.Damage / 100;
            // Cuồng nộ
            if (Character.InfoBuff.CuongNo)
            {
                damage += damage;
            }
            if (Character.InfoBuff.XiMuoiHoaDao)
            {
                damage += damage * 20 / 100;
            }
            if (Character.InfoBuff.CuongNo2)
            {
                damage *= 2;
            }
            if (Character.isColer)
            {
                damage -= damage / 2;
            }
            // Kiểm tra có mặc giáp luyện tập hay không
            var itemGiap = Character.ItemBody[6];
            if (itemGiap != null && ItemCache.ItemIsGiapLuyenTap(itemGiap.Id))
            {
                damage -= (damage * ItemCache.GetGiapLuyenTapPTSucManh(itemGiap.Id)) / 100;
            }

            // Kiểm tra xem có vừa tháo giáp tập luyện ra không
            if (Character.InfoMore.LastGiapLuyenTapItemId != 0)
            {
                var giapLuyenTap = GetItemBagById(Character.InfoMore.LastGiapLuyenTapItemId);
                if (giapLuyenTap != null && ItemCache.ItemIsGiapLuyenTap(giapLuyenTap.Id))
                {
                    var optionCheck = giapLuyenTap.Options.FirstOrDefault(option => option.Id == 9);
                    if (optionCheck.Param > 0)
                    {
                        damage += (damage * ItemCache.GetGiapLuyenTapPTSucManh(giapLuyenTap.Id)) / 100;
                    }
                }
                else
                {
                    Character.InfoMore.LastGiapLuyenTapItemId = 0;
                    Character.Delay.GiapLuyenTap = -1;
                }
            }

            // Thức ăn
            if (Character.InfoBuff.ThucAnId != -1)
            {
                damage += damage * 10 / 100;
            }

            if (Character.InfoBuff.BanhTrungThuId != -1)
            {
                switch (Character.InfoBuff.BanhTrungThuId)
                {
                    case 465:
                        {
                            damage += damage * 10 / 100;
                            break;
                        }
                    case 466:
                        {
                            damage += damage * 15 / 100;
                            break;
                        }
                    case 472:
                        {
                            damage += damage * 20 / 100;
                            break;
                        }
                    case 473:
                        {
                            damage += damage * 25 / 100;
                            break;
                        }
                }
            }
            Character.DamageFull = damage;
        }

        public void SetDefenceFull()
        {
            var defence = Character.InfoChar.OriginalDefence * 4;
            defence += GetParamItem(47);
            //GetListParamItem(94).ForEach(param => defence += defence * param / 100);
            GetListParamItem(94).ForEach(param => defence += defence * param / 100);
            if (Character.InfoBuff.isEnchantGiap) defence += defence * 15 / 100;
            if (Character.Blackball.CurrentListBuff.Contains(4)) defence += defence * 20 / 100;
            if (Character.InfoChar.Fusion.IsFusion && Character.Disciple != null) {
                // Bông tai porata 2
                if (Character.InfoChar.Fusion.IsPorata2)
                {
                    var bongTaiPorata2 = GetItemBagById(921);
                    if (bongTaiPorata2 != null)
                    {
                        var optionCheck = bongTaiPorata2.Options.FirstOrDefault(option => option.Id != 72);
                        if (optionCheck != null && optionCheck.Id == 94)
                        {
                            defence += defence * optionCheck.Param / 100;
                        }
                    }

                    defence += defence * 10 / 100;
                }
            }
            Character.DefenceFull = Math.Abs(defence);
        }

        public void SetCritFull()
        {
            int crtCal;
            if (Character.InfoSkill.Monkey.MonkeyId != 0)
            {
                crtCal = 115;
            }
            else
            {
                crtCal = Character.InfoChar.OriginalCrit;
                crtCal += GetParamItem(14);
            }
            if (Character.InfoBuff.isEnchantCrit)
            {
                crtCal += 3;
            }
            if (Character.InfoChar.Fusion.IsFusion && Character.Disciple != null) {
                // Bông tai porata 2
                if (Character.InfoChar.Fusion.IsPorata2)
                {
                    var bongTaiPorata2 = GetItemBagById(921);
                    if (bongTaiPorata2 != null)
                    {
                        var optionCheck = bongTaiPorata2.Options.FirstOrDefault(option => option.Id != 72);
                        if (optionCheck != null && optionCheck.Id == 14)
                        {
                            crtCal += optionCheck.Param;
                        }
                    }
                }
            }
            Character.CritFull = crtCal;
        }

        public void SetHpPlusFromDamage()
        {
            var hpPlus = GetParamItem(95);
            Character.HpPlusFromDamage = hpPlus;

            Character.HpPlusFromDamageMonster = GetParamItem(104);
        }

        public void SetMpPlusFromDamage()
        {
            var mpPlus = GetParamItem(96);
            Character.MpPlusFromDamage = mpPlus;
        }

        public void SetSpeed()
        {
            var speed = 5;
            if (Character.InfoSkill.Monkey.MonkeyId != 0) speed = 8;
            if (Character.InfoChar.Fusion.IsFusion) speed = 7;
            if (Character.ItemBody[5] != null && Character.ItemBody[5].Id == 461) speed = 8;
            if (Character.InfoChar.MapId == 117) speed = 2;
            var plus = speed * (GetParamItem(148) + GetParamItem(114) + GetParamItem(16)) / 100;
            switch (plus)
            {
                case <= 1:
                    speed += 1;
                    break;
                case > 1 and <= 2:
                    speed += 2;
                    break;
                    // case > 2:
                    //     speed += plus;
                    //     break;
            }
            Character.InfoChar. Speed = (sbyte)speed;
        }

        public void SetBuffHp30s()
        {
            var hpPlus = GetParamItem(27);
            Character.Effect.BuffHp30S.Value = hpPlus;
            if (Character.Effect.BuffHp30S.Time == -1)
            {
                Character.Effect.BuffHp30S.Time = 30000 + ServerUtils.CurrentTimeMillis();
            }

        }

        public void SetBuffMp1s()
        {
            var mpPlus = (int)Character.MpFull * GetParamItem(162) / 100;
            Character.Effect.BuffKi1s.Value = mpPlus;
            if (Character.Effect.BuffKi1s.Time == -1)
            {
                Character.Effect.BuffKi1s.Time = 1500 + ServerUtils.CurrentTimeMillis();
            }
        }

        public void SetBuffHp5s()
        {
            //TODO HANDLE PLUS HP 5s
        }

        public void SetBuffHp10s()
        {
            //TODO HANDLE PLUS HP 10s
        }

        public void Clear() => SuppressFinalize(this);

        public void BagSort()
        {
            var listItemCheck = Character.ItemBag
                .Where(item => ItemCache.ItemTemplate(item.Id).IsUpToUp && item.Quantity < 9999).ToList();
            Character.ItemBag.RemoveAll(item => listItemCheck.Contains(item));
            var enumerable = listItemCheck
                .GroupBy(i => i.Id)
                .Select(g =>
                {
                    var item = ItemCache.GetItemDefault(g.Key);
                    item.Quantity = g.Sum(it => it.Quantity);
                    return item;
                }).ToList();
            enumerable.ToList().ForEach(item =>
            {
                if (item.Quantity <= 9999) return;
                var itemNew = ItemHandler.Clone(item);
                itemNew.Quantity = item.Quantity - 9999;
                enumerable.Add(itemNew);
                item.Quantity = 9999;
            });
            Character.ItemBag.AddRange(enumerable);
            var count = 0;
            Character.ItemBag.ForEach(item => item.IndexUI = count++);
        }

        public void Upindex(int index)
        {
            var itemBag = GetItemBagByIndex(index);
            if (itemBag == null) return;
            if (index >= Character.ItemBag.Count) return;
            var count = 0;
            Character.ItemBag.ForEach(item => item.IndexUI = count++);
        }
        public void BoxSort()
        {
            var listItemCheck = Character.ItemBox
                .Where(item => ItemCache.ItemTemplate(item.Id).IsUpToUp && item.Quantity < 99).ToList();
            Character.ItemBox.RemoveAll(item => listItemCheck.Contains(item));
            var enumerable = listItemCheck
                .GroupBy(i => i.Id)
                .Select(g =>
                {
                    var item = ItemCache.GetItemDefault(g.Key);
                    item.Quantity = g.Sum(it => it.Quantity);
                    return item;
                }).ToList();
            enumerable.ToList().ForEach(item =>
            {
                if (item.Quantity <= 99) return;
                var itemNew = ItemHandler.Clone(item);
                itemNew.Quantity = item.Quantity - 99;
                enumerable.Add(itemNew);
                item.Quantity = 99;
            });
            Character.ItemBox.AddRange(enumerable);
            var count = 0;
            Character.ItemBox.ForEach(item => item.IndexUI = count++);
        }

        public void HandleJoinMap(Zone zone)
        {
            lock (zone.Characters)
            {
                zone.Characters.Values.Where(x => x.Id != Character.Id).ToList().ForEach(c =>
                {
                    SendMessage(Service.PlayerAdd(c));
                    var infoSkill = c.InfoSkill;
                    if (infoSkill.MeTroi.IsMeTroi)
                    {
                        if (infoSkill.MeTroi.Monster != null)
                        {
                            SendMessage(Service.SkillEffectMonster(c.Id, infoSkill.MeTroi.Monster.IdMap, 1, 32));
                        }
                    }

                    if (infoSkill.PlayerTroi.IsPlayerTroi)
                    {
                        infoSkill.PlayerTroi.PlayerId.ForEach(o =>
                        {
                            SendMessage(Service.SkillEffectPlayer(o, c.Id, 1, 32));
                        });
                    }

                    if (infoSkill.ThaiDuongHanSan.IsStun)
                    {
                        SendMessage(Service.SkillEffectPlayer(c.Id, c.Id, 1, 40));
                    }

                    if (infoSkill.DichChuyen.IsStun)
                    {
                        SendMessage(Service.SkillEffectPlayer(c.Id, c.Id, 1, 40));
                    }

                    if (infoSkill.Protect.IsProtect)
                    {
                        SendMessage(Service.SkillEffectPlayer(c.Id, c.Id, 1, 33));
                    }

                    if (infoSkill.ThoiMien.IsThoiMien)
                    {
                        SendMessage(Service.SkillEffectPlayer(c.Id, c.Id, 1, 41));
                    }
                });
            }

            lock (zone.Disciples)
            {
                foreach (var disciplesValue in zone.Disciples.Values)
                {
                    var text = "#";
                    if (Character.Id + disciplesValue.Id == 0) text = "$";
                    SendMessage(Service.PlayerAdd(disciplesValue, text));
                    var infoSkill = disciplesValue.InfoSkill;
                    if (infoSkill.MeTroi.IsMeTroi)
                    {
                        if (infoSkill.MeTroi.Monster != null)
                        {
                            SendMessage(Service.SkillEffectMonster(disciplesValue.Id, infoSkill.MeTroi.Monster.IdMap, 1, 32));
                        }
                    }

                    if (infoSkill.PlayerTroi.IsPlayerTroi)
                    {
                        infoSkill.PlayerTroi.PlayerId.ForEach(o =>
                        {
                            SendMessage(Service.SkillEffectPlayer(o, disciplesValue.Id, 1, 32));
                        });
                    }

                    if (infoSkill.ThaiDuongHanSan.IsStun)
                    {
                        SendMessage(Service.SkillEffectPlayer(disciplesValue.Id, disciplesValue.Id, 1, 40));
                    }

                    if (infoSkill.DichChuyen.IsStun)
                    {
                        SendMessage(Service.SkillEffectPlayer(disciplesValue.Id, disciplesValue.Id, 1, 40));
                    }

                    if (infoSkill.Protect.IsProtect)
                    {
                        SendMessage(Service.SkillEffectPlayer(disciplesValue.Id, disciplesValue.Id, 1, 33));
                    }

                    if (infoSkill.ThoiMien.IsThoiMien)
                    {
                        SendMessage(Service.SkillEffectPlayer(disciplesValue.Id, disciplesValue.Id, 1, 41));
                    }
                }
            }

            lock (zone.Pets)
            {
                foreach (var petValue in zone.Pets.Values)
                {
                    var text = "#";
                    if ((Character.Id + 1000) + petValue.Id == 0) text = "$";
                    SendMessage(Service.PlayerAdd(petValue, text));
                }
            }

            lock (zone.Bosses)
            {
                foreach (var bossesValue in zone.Bosses.Values)
                {
                    SendMessage(Service.PlayerAdd(bossesValue));
                    var infoSkill = bossesValue.InfoSkill;
                    if (infoSkill.MeTroi.IsMeTroi)
                    {
                        if (infoSkill.MeTroi.Monster != null)
                        {
                            SendMessage(Service.SkillEffectMonster(bossesValue.Id, infoSkill.MeTroi.Monster.IdMap, 1, 32));
                        }
                    }

                    if (infoSkill.PlayerTroi.IsPlayerTroi)
                    {
                        infoSkill.PlayerTroi.PlayerId.ForEach(o =>
                        {
                            SendMessage(Service.SkillEffectPlayer(o, bossesValue.Id, 1, 32));
                        });
                    }

                    if (infoSkill.ThaiDuongHanSan.IsStun)
                    {
                        SendMessage(Service.SkillEffectPlayer(bossesValue.Id, bossesValue.Id, 1, 40));
                    }

                    if (infoSkill.DichChuyen.IsStun)
                    {
                        SendMessage(Service.SkillEffectPlayer(bossesValue.Id, bossesValue.Id, 1, 40));
                    }

                    if (infoSkill.Protect.IsProtect)
                    {
                        SendMessage(Service.SkillEffectPlayer(bossesValue.Id, bossesValue.Id, 1, 33));
                    }

                    if (infoSkill.ThoiMien.IsThoiMien)
                    {
                        SendMessage(Service.SkillEffectPlayer(bossesValue.Id, bossesValue.Id, 1, 41));
                    }
                }
            }

            lock (zone.MonsterPets)
            {
                zone.MonsterPets.Values.Where(m => m is { IsDie: false } && m.IdMap != Character.Id).ToList().ForEach(m =>
                {
                    SendMessage(Service.UpdateMonsterMe0(m));
                });
            }

            zone.MonsterMaps.Where(m => !m.IsDie).ToList().ForEach(m =>
            {
                var infoSkill = m.InfoSkill;
                if (infoSkill.ThaiDuongHanSan.IsStun)
                {
                    SendMessage(Service.SkillEffectMonster(-1, m.IdMap, 1, 40));
                }

                if (infoSkill.DichChuyen.IsStun)
                {
                    SendMessage(Service.SkillEffectMonster(-1, m.IdMap, 1, 40));
                }

                if (infoSkill.ThoiMien.IsThoiMien)
                {
                    SendMessage(Service.SkillEffectMonster(-1, m.IdMap, 1, 41));
                }
            });

            // Xử lý trứng Ma bư, dưa hấu tại đây
            if (Character.InfoChar.ThoiGianTrungMaBu > 0 && Character.InfoChar.MapId - 21 == Character.InfoChar.Gender)
            {
                SendMessage(Service.TrungMaBu(Character));
            }
            if (Character.InfoChar.ThoiGianDuaHau > 0 && Character.InfoChar.MapId - 21 == Character.InfoChar.Gender)
            {
                SendMessage(Service.DuaHau(Character));
            }
            if (Character.InfoChar.MapId == 111)
            {
                SendMessage(Service_Trứng.Trứng_rồng_hắc_long(Character));
            ///    SendMessage(Service_Trứng.Trứng_rồng_thanh_long(Character));
            }
            // Gửi status trứng ma bư qua cmd duahau
        }

        public void AddItemToBody(Model.Item.Item item, int index)
        {
            if (item == null) return;
            item.IndexUI = index;
            Character.ItemBody[index] = item;

          //  UpdateAntiChangeServerTime();
         //   Character.Delay.NeedToSaveBody = true;

        }

        #region ItemBag
        public Model.Item.Item GetItemBagByIndex(int index)
        {
            return Character.ItemBag.FirstOrDefault(item => item.IndexUI == index);
        }
        public Model.Item.Item GetItemBodyByIndex(int index)
        {
            return Character.ItemBody.FirstOrDefault(item=>item.IndexUI==index);
        }
        public Model.Item.Item GetItemBagById(int id)
        {
            return Character.ItemBag.FirstOrDefault(item => item.Id == id);
        }

        private int IndexItemBagNotMaxQuantity(short id)
        {
            var item = Character.ItemBag.FirstOrDefault(item => (item.Quantity < 99 || ItemCache.IsUnlimitItem(id)) && item.Id == id);
            return item?.IndexUI ?? -1;
        }

        public Model.Item.Item ItemBagNotMaxQuantity(short id)
        {
            return Character.ItemBag.FirstOrDefault(item => (item.Quantity < 99 || ItemCache.IsUnlimitItem(id)) && item.Id == id);
        }

        public Model.Item.Item ItemBagNotMaxQuantity(short id, int indexUi)
        {
            return Character.ItemBag.FirstOrDefault(item => item.IndexUI != indexUi && (item.Quantity < 99 || ItemCache.IsUnlimitItem(id)) && item.Id == id);
        }

        public bool AddItemToBag(bool isUpToUp, Model.Item.Item item, string reason = "")
        {
            try
            {
                if (item == null) return false;
                var index = IndexItemBagNotMaxQuantity(item.Id);
                var itemTemplate = ItemCache.ItemTemplate(item.Id);
                if (isUpToUp && itemTemplate.IsUpToUp && index != -1)
                {
                    var itemBag = GetItemBagByIndex(index);
                    var quantity = itemBag.Quantity + item.Quantity;
                    if (quantity > 99 && !ItemCache.IsUnlimitItem(item.Id) && !ItemCache.IsSpecialAmountItem(item.Id))
                    {
                        var itemClone = ItemHandler.Clone(itemBag);
                        itemClone.Quantity = quantity - 99;
                        if (!AddItemToBag(itemClone, reason)) return false;
                        quantity = 99;
                    }
                    else if (ItemCache.IsSpecialAmountItem(item.Id))
                    {
                        var opt = item.Options.FirstOrDefault(i => i.Id == 31);
                        if (opt != null)
                        {

                            opt.Param += item.Quantity;
                           // Server.Gi().Logger.Print("Param: " + opt.Param);
                        }
                        else
                        {
                            item.Options.Add(new OptionItem()
                            {
                                Id = 31,
                                Param = item.Quantity
                            });
                        }
                    }
                    ServerUtils.WriteLog("additem/" + Character.Id, $"BAG:{Character.Name} add {quantity}x{itemTemplate.Name} (old: {itemBag.Quantity}) lydo: " + reason);
                    if (!ItemCache.IsSpecialAmountItem(item.Id)) itemBag.Quantity = quantity;
                    else itemBag.Quantity = 1;

                 //   UpdateAntiChangeServerTime(reason);
                 //   Character.Delay.NeedToSaveBag = true;

                    return true;
                }
                else
                {
                    return AddItemToBag(item, reason);
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error AddItemToBag in Service.cs: {e.Message} \n {e.StackTrace}", e);
                return false;
            }
        }

        private bool AddItemToBag(Model.Item.Item item, string reason)
        {
            if (item != null)
            {
                if (Character.LengthBagNull() > 0)
                {
                    var itemTemplate = ItemCache.ItemTemplate(item.Id);
                    lock (Character.ItemBag)
                    {
                        var index = Character.ItemBag.Count;
                        item.IndexUI = index;
                        if (ItemCache.IsSpecialAmountItem(item.Id))
                        {
                            var opt = item.Options.FirstOrDefault(i => i.Id == 31);
                            if (opt != null) opt.Param += item.Quantity;
                            else
                            {
                                item.Options.Add(new OptionItem()
                                {
                                    Id = 31,
                                    Param = item.Quantity
                                });
                            }
                        }
                        Character.ItemBag.Add(item);
                    }

                    ServerUtils.WriteLog("additem/" + Character.Id, $"BAG:{Character.Name} add {item.Quantity}x{itemTemplate.Name} lydo: " + reason);
                    //UpdateAntiChangeServerTime(reason);
                    //Character.Delay.NeedToSaveBag = true;

                    
                    return true;
                }
                else
                {
                    SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_BAG));
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void RemoveItemBagById(short id, int quantity, string reason = "")
        {
            var num = 0;
            var lengthOld = Character.ItemBag.Count;
            Character.ItemBag.ToList().ForEach(itemBag =>
            {
                if (itemBag == null || itemBag.Id != id) return;
                if (num + itemBag.Quantity >= quantity)
                {
                    RemoveItemBagByIndex(itemBag.IndexUI, quantity - num, false, reason);
                    id = -1;
                    return;
                }
                num += itemBag.Quantity;
                RemoveItemBagByIndex(itemBag.IndexUI, itemBag.Quantity, false, reason);
                var itemTemplate = ItemCache.ItemTemplate(itemBag.Id);
                
            });
            if (lengthOld == Character.ItemBag.Count) return;
            num = 0;
            Character.ItemBag.ForEach(item => item.IndexUI = num++);
        }

        public void RemoveItemBagByIndex(int index, int quantity, bool reset = true, string reason = "")
        {
            lock (Character.ItemBag)
            {
                var itemBag = GetItemBagByIndex(index);
                if (itemBag == null) return;
                itemBag.Quantity -= quantity;

                if (itemBag.Quantity <= 0) Character.ItemBag.RemoveAll(item => item.IndexUI == index);

              //  UpdateAntiChangeServerTime(reason);

             //   Character.Delay.NeedToSaveBag = true;

                var itemTemplate = ItemCache.ItemTemplate(itemBag.Id);
                ServerUtils.WriteLog("removeitem/" + Character.Id, $"BAG:{Character.Name} remove {quantity}x{itemTemplate.Name} lydo: " + reason);
               
                if (!reset || index >= Character.ItemBag.Count) return;
                {
                    var count = 0;
                    Character.ItemBag.ForEach(item => item.IndexUI = count++);
                }
              //  if (itemTemplate.Type is 23 or 24) UpdateMountId();
            }
        }
        public void RemoveItemBodyByIndex(int index, int quantity, bool reset = true, string reason = "")
        {
            lock (Character.ItemBag)
            {
                var itemBag = GetItemBagByIndex(index);
                if (itemBag == null) return;
                itemBag.Quantity -= quantity;

                if (itemBag.Quantity <= 0) Character.ItemBag.RemoveAll(item => item.IndexUI == index);

              //  UpdateAntiChangeServerTime(reason);

              //  Character.Delay.NeedToSaveBag = true;

                var itemTemplate = ItemCache.ItemTemplate(itemBag.Id);
                ServerUtils.WriteLog("removeitem/" + Character.Id, $"BAG:{Character.Name} remove {quantity}x{itemTemplate.Name} lydo: " + reason);

                if (!reset || index >= Character.ItemBag.Count) return;
                {
                    var count = 0;
                    Character.ItemBag.ForEach(item => item.IndexUI = count++);
                }
               // if (itemTemplate.Type is 23 or 24) UpdateMountId();
            }
        }
        public Model.Item.Item RemoveItemBag(int index, bool isReset = true, string reason = "")
        {
            var itemBag = GetItemBagByIndex(index);
            lock (Character.ItemBag)
            {
                if (itemBag == null) return null;
                Character.ItemBag.RemoveAll(item => item.IndexUI == index);
                if (isReset && index < Character.ItemBag.Count)
                {
                    var count = 0;
                    Character.ItemBag.ForEach(item => item.IndexUI = count++);
                }
                SendMessage(Service.SendBag(Character));
                var itemTemplate = ItemCache.ItemTemplate(itemBag.Id);
                ServerUtils.WriteLog("removeitem/" + Character.Id, $"BAG:{Character.Name} remove {itemTemplate.Name} lydo: " + reason);
                
            //    UpdateAntiChangeServerTime(reason);
           //     Character.Delay.NeedToSaveBag = true;

            }
            return itemBag;
        }
        #endregion

        #region Item Box
        public bool AddItemToBox(bool isUpToUp, Model.Item.Item item)
        {
            try
            {
                if (item == null) return false;
                var index = IndexItemBoxNotMaxQuantity(item.Id);
                var itemTemplate = ItemCache.ItemTemplate(item.Id);
                if (isUpToUp && itemTemplate.IsUpToUp && index != -1)
                {
                    var itemBox = GetItemBoxByIndex(index);
                    var quantity = itemBox.Quantity + item.Quantity;
                    if (quantity > 99)
                    {
                        var itemClone = ItemHandler.Clone(itemBox);
                        itemClone.Quantity = quantity - 99;
                        if (!AddItemToBox(itemClone)) return false;
                        quantity = 99;
                    }

                    itemBox.Quantity = quantity;
                  //  UpdateAntiChangeServerTime();
                 //   Character.Delay.NeedToSaveBox = true;

                    return true;
                }
                else
                {
                    return AddItemToBox(item);
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error AddItemToBox in Service.cs: {e.Message} \n {e.StackTrace}", e);
                return false;
            }
        }

        private bool AddItemToBox(Model.Item.Item item)
        {
            if (item != null)
            {
                if (Character.LengthBoxNull() > 0)
                {
                    lock (Character.ItemBox)
                    {
                        var index = Character.ItemBox.Count;
                        item.IndexUI = index;
                        Character.ItemBox.Add(item);
                    }
               //    UpdateAntiChangeServerTime();
               //     Character.Delay.NeedToSaveBox = true;

                    return true;
                }
                else
                {
                    SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_BOX));
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public Model.Item.Item GetItemBoxByIndex(int index)
        {
            return Character.ItemBox.FirstOrDefault(item => item.IndexUI == index);
        }
        public Model.Item.Item GetItemLuckyBoxByIndex(int index)
        {
            return Character.LuckyBox.FirstOrDefault(item => item.IndexUI == index);
        }
        public Model.Item.Item GetItemClanBoxByIndex(int index)
        {
            return ClanManager.Get(Character.ClanId).ClanBox.FirstOrDefault(item => item.IndexUI == index);
        }
        public Model.Item.Item GetItemBoxById(int id)
        {
            return Character.ItemBox.FirstOrDefault(item => item.Id == id);
        }

        public void RemoveMonsterMe()
        {
            var skillEgg = Character.InfoSkill.Egg;
            if (skillEgg.Monster is { IsDie: true })
            {
                SendZoneMessage(Service.UpdateMonsterMe7(skillEgg.Monster.Id));
                Character.Zone.ZoneHandler.RemoveMonsterMe(skillEgg.Monster.Id);
                SkillHandler.RemoveMonsterPet(Character);
            }
        }

        public void PlusTiemNang(IMonster monster, int damage)
        {
            if (monster.Character != null) return;
            if (damage <= 0) return;
            var timeServer = ServerUtils.CurrentTimeMillis();
            long fixDmg = (long)((damage) + (monster.OriginalHp * 0.00125));
            long damagePlusPoint = fixDmg;
            //if (Character.InfoChar.Task.Id == 5 && Character.InfoChar.Task.Index == 0 && Character.InfoChar.Điểm_thành_tích >= 200000 * 100)
            //{
            //    Character.InfoChar.Task.Index++;
            //    Character.CharacterHandler.SendMessage(Service.SendTask(Character));
            //}
            //if (Character.InfoChar.Task.Id == 6 && Character.InfoChar.Task.Index == 0 && Character.InfoChar.Điểm_thành_tích >= 400000 * 100)
            //{
            //    Character.InfoChar.Task.Index++;
            //    Character.CharacterHandler.SendMessage(Service.SendTask(Character));
            //}
           
            if (Character.InfoChar.Power > DatabaseManager.ConfigManager.gI().LimitPowerExpUp)
            {
                damagePlusPoint /= 2;
            }
          //  var CacheLitmit = Cache.Gi().LIMIT_POWERS.FirstOrDefault(i => i.Key == Character.InfoChar.LitmitPower).Value;
            if(Character.InfoChar.LitmitPower >= 4)
            {
                damagePlusPoint /= (Character.InfoChar.LitmitPower/2);

            }
            //if (Character.InfoChar.LitmitPower >= 6 && Character.InfoChar.Power >= CacheLitmit.Power)
            //{
            //    damagePlusPoint /= 2;
            //}
            //if (Character.InfoChar.LitmitPower >= 8 && Character.InfoChar.Power >= CacheLitmit.Power)
            //{
            //    damagePlusPoint /= 2;
            //}
            //if (Character.InfoChar.LitmitPower >= 10 && Character.InfoChar.Power >= CacheLitmit.Power)
            //{
            //    damagePlusPoint /= 2;
            //}
            //if (Character.InfoChar.LitmitPower >= 13 && Character.InfoChar.Power >= CacheLitmit.Power)
            //{
            //    damagePlusPoint /= 2;
            //}
            //if (Character.InfoChar.LitmitPower >= 16 && Character.InfoChar.Power >= CacheLitmit.Power)
            //{
            //    damagePlusPoint /= 2;
            //}
            //if (Character.InfoChar.LitmitPower >= 18 && Character.InfoChar.Power >= CacheLitmit.Power)
            //{
            //    damagePlusPoint /= 2;
            //}
            //if (Character.InfoChar.LitmitPower >= 20 && Character.InfoChar.Power >= CacheLitmit.Power)
            //{
            //    damagePlusPoint /= 2;
            //}
            //if (Character.InfoChar.LitmitPower >= 21 && Character.InfoChar.Power >= CacheLitmit.Power)
            //{
            //    damagePlusPoint /= 2;
            //}

            if (damagePlusPoint <= 0)
            {
                damagePlusPoint = 1;
            }

            if (monster.Id != 0)
            {
                //var levelChar = Character.InfoChar.Level;
                //var levelMonster = monster.Level;
                //var checkLevel = Math.Abs(levelChar - levelMonster);
                //if ((checkLevel > 5 && levelChar > levelMonster) || (levelMonster > levelChar && levelMonster - levelChar > 5))
                //{
                //    damagePlusPoint = 1;
                //}
                if (monster.Id is (77 or 70))
                {
                    damagePlusPoint = 1;
                }
            }
            // else
            // {
            //     damagePlusPoint = 1;//ServerUtils.RandomNumber(100) < 3 ? 1 : 0;
            // }

            switch (Character.Flag)
            {
                case 0: break;
                case 8:
                    damagePlusPoint += damagePlusPoint * 10 / 100;
                    break;
                default:
                    damagePlusPoint += damagePlusPoint * 5 / 100;
                    break;
            }

            if (DatabaseManager.ConfigManager.gI().ExpUp > 1)
            {
                if (Character.InfoChar.Power < DatabaseManager.ConfigManager.gI().LimitPowerExpUp)
                {
                    damagePlusPoint *= DatabaseManager.ConfigManager.gI().ExpUp;
                }
                else
                {
                    damagePlusPoint *= (DatabaseManager.ConfigManager.gI().ExpUp / 2);
                }
            }

            // Nội tại tăng tiềm năng sức mạnh đánh quái
            var specialId = Character.SpecialSkill.Id;
            if (specialId != -1 && (specialId == 8 || specialId == 19 || specialId == 29))
            {
                damagePlusPoint += damagePlusPoint * Character.SpecialSkill.Value / 100;
            }
            // Option Sao pha lê
            var OptionPhanTramTNSM = Character.InfoOption.PhanTramTNSM;
            if (OptionPhanTramTNSM > 0)
            {
                damagePlusPoint += damagePlusPoint * OptionPhanTramTNSM / 100;
            }

            // // Bùa Trí Tuệ x4
            if (Character.InfoMore.BuaTriTueX4)
            {
                if (Character.InfoMore.BuaTriTueX4Time > timeServer)
                {
                    damagePlusPoint *= 4;
                }
                else
                {
                    Character.InfoMore.BuaTriTueX4 = false;
                }
            }
            else if (Character.InfoMore.BuaTriTueX3) // Bùa Trí Tuệ x3
            {
                if (Character.InfoMore.BuaTriTueX3Time > timeServer)
                {
                    damagePlusPoint *= 3;
                }
                else
                {
                    Character.InfoMore.BuaTriTueX3 = false;
                }
            }
            // Bùa Trí Tuệ 213
            else if (Character.InfoMore.BuaTriTue)
            {
                if (Character.InfoMore.BuaTriTueTime > timeServer)
                {
                    damagePlusPoint *= 2;
                }
                else
                {
                    Character.InfoMore.BuaTriTue = false;
                }
            }
            // Vệ tinh Trí tuệ
            if (Character.InfoMore.IsNearAuraTriTueItem)
            {
                if (Character.InfoMore.AuraTriTueTime > timeServer)
                {
                    damagePlusPoint += damagePlusPoint * 20 / 100;
                }
                else
                {
                    Character.InfoMore.IsNearAuraTriTueItem = false;
                }
            }
            if (DataCache.IdMapNguHanhSon.Contains(Character.InfoChar.MapId))
            {
                damagePlusPoint *= 2;
            }
            if (Character.InfoBuff.KichDucX2)
            {
                damagePlusPoint *= 2;
            }
            if (Character.InfoBuff.KichDucX5)
            {
                damagePlusPoint *= 5;
            }
            if (Character.InfoBuff.KichDucX7)
            {
                damagePlusPoint *= 7;
            }
            if (Character.Blackball.CurrentListBuff.Contains(6)) damagePlusPoint += damagePlusPoint * 35 / 100;
            if (Character.Blackball.CurrentListBuff.Contains(7)) damagePlusPoint += damagePlusPoint * 35 / 100;

            if (Character.InfoOption.X2TiemNang) damagePlusPoint *= 2;
            //if (damagePlusPoint >= 6000000)
            //{
            //    damagePlusPoint = 6000000;
            //}
            if (Character.InfoSet.IsFullSetOcTieu || Character.InfoSet.IsFullSetCadic || Character.InfoSet.IsFullSetThienXinHang) damagePlusPoint *= 2;// giảm 10 xg 2
            if (DataCache.IdMapBDKB.Contains(Character.InfoChar.MapId)) damagePlusPoint *= 3;
            if (Character.InfoChar.IsPower)
            {
                PlusTiemNang(damagePlusPoint, damagePlusPoint, true);
                TaskHandler.gI().CheckTaskDonePower(Character);
              //  TaskHandler.CheckPowerToAction(this.Character);
            }
            else
            {
                PlusTiemNang(0, damagePlusPoint, false);
                TaskHandler.gI().CheckTaskDonePower(Character);
                //  TaskHandler.CheckPowerToAction(this.Character);
            }

            foreach (var clanChar in Character.Zone.Characters.Values.ToList().Where(c => c.ClanId != -1 && c.ClanId == Character.ClanId && c.Id != Character.Id))
            {
                clanChar.CharacterHandler.PlusTiemNang(0, damagePlusPoint / 2, false);
            }
        }

        public void PlusTiemNang(long power, long potential, bool isAll)
        {
            // if (!Character.InfoChar.IsPremium && Character.InfoChar.Điểm_thành_tích >= DataCache.PREMIUM_LIMIT_UP_POWER)
            // {
            //     SendMessage(Service.ServerMessage(TextServer.gI().NOT_PREMIUM_LIMIT_POWER));
            //     return;
            // }

            if (isAll && power > 0 && potential > 0)
            {
                PlusPower(power);
                PlusPotential(potential);
                SendMessage(Service.UpdateExp(2, power));
            }
            else
            {
                if (power > 0)
                {
                    PlusPower(power);
                    SendMessage(Service.UpdateExp(0, power));
                }

                if (potential > 0)
                {
                    PlusPotential(potential);
                    SendMessage(Service.UpdateExp(1, potential));
                }
            }
        }

        public void LeaveFromDead(bool isHeal = false)
        {
            lock (Character)
            {
                if (!isHeal)
                {
                    Character.MineDiamond(1);
                }
                SendMessage(Service.MeLoadInfo(Character));
                Character.InfoChar.IsDie = false;
                Character.InfoChar.Hp = Character.HpFull;
                Character.InfoChar.Mp = Character.MpFull;
                SendMessage(Service.MeLive());
                SendZoneMessage(Service.ReturnPointMap(Character));
                SendZoneMessage(Service.PlayerLoadLive(Character));
                Character.DataBoMong.Count[14]++;
            }
        }

        public void BackHome()
        {
            lock (Character)
            {
                SendZoneMessage(Service.SendTeleport(Character.Id, Character.InfoChar.Teleport));
                switch (Character.InfoChar.MapId)
                {
                    case 165:
                        ClanManager.Get(Character.ClanId).ClanZone.Maps[1].OutZone(Character, 21 + Character.InfoChar.Gender);
                        break;
                    default:
                        Character.Zone.Map.OutZone(Character, Character.InfoChar.Gender + 21);
                        break;
                }
                Character.InfoChar.IsDie = false;
                Character.InfoChar.Hp = 1;
                SendMessage(Service.MeLive());
                SendMessage(Service.PlayerLevel(Character));
                SendMessage(Service.MeLoadInfo(Character));
                Character.MapPrivate.GetMapById(21 + Character.InfoChar.Gender).JoinZone(Character, 0, true, true, Character.InfoChar.Teleport);

            }
        }

        private int IndexItemBoxNotMaxQuantity(short id)
        {
            var item = Character.ItemBox.FirstOrDefault(item => item.Quantity < 99 && item.Id == id);
            return item?.IndexUI ?? -1;
        }

        public void RemoveItemBoxByIndex(int index, int quantity, bool reset = true)
        {
            lock (Character.ItemBox)
            {
                var itemBox = GetItemBoxByIndex(index);
                if (itemBox == null) return;
                itemBox.Quantity -= quantity;
                if (itemBox.Quantity <= 0) Character.ItemBox.RemoveAll(item => item.IndexUI == index);
            //    UpdateAntiChangeServerTime();
           //     Character.Delay.NeedToSaveBox = true;

                if (!reset || index >= Character.ItemBox.Count) return;
                {
                    var count = 0;
                    Character.ItemBox.ForEach(item => item.IndexUI = count++);
                }
            }
        }

        public Model.Item.Item RemoveItemBox(int index, bool isReset = true)
        {
            lock (Character.ItemBox)
            {
                var itemBox = Character.ItemBox.FirstOrDefault(item => item.IndexUI == index);
                if (itemBox == null) return null;
                Character.ItemBox.RemoveAll(item => item.IndexUI == index);
                if (isReset && index < Character.ItemBox.Count)
                {
                    var count = 0;
                    Character.ItemBox.ForEach(item => item.IndexUI = count++);
                }
                SendMessage(Service.SendBox(Character));
        //        UpdateAntiChangeServerTime();
             //   Character.Delay.NeedToSaveBox = true;

                return itemBox;
            }

        }
        public Model.Item.Item RemoveItemLuckyBox(int index, bool isReset = true)
        {
            lock (Character.LuckyBox)
            {
                var itemBox = Character.LuckyBox.FirstOrDefault(item => item.IndexUI == index);
                if (itemBox == null) return null;
                Character.LuckyBox.RemoveAll(item => item.IndexUI == index);
                if (isReset && index < Character.LuckyBox.Count)
                {
                    var count = 0;
                    Character.LuckyBox.ForEach(item => item.IndexUI = count++);
                }
             //   UpdateAntiChangeServerTime();
             //   Character.Delay.NeedToSaveLucky = true;

                return itemBox;
            }

        }
        public Model.Item.Item RemoveItemClanBox(int index, bool isReset = true)
        {
            lock (ClanManager.Get(Character.ClanId).ClanBox)
            {
                var itemBox = ClanManager.Get(Character.ClanId).ClanBox.FirstOrDefault(item => item.IndexUI == index);
                if (itemBox == null) return null;
                ClanManager.Get(Character.ClanId).ClanBox.RemoveAll(item => item.IndexUI == index);
                if (isReset && index < ClanManager.Get(Character.ClanId).ClanBox.Count)
                {
                    var count = 0;
                    ClanManager.Get(Character.ClanId).ClanBox.ForEach(item => item.IndexUI = count++);
                }
             //   UpdateAntiChangeServerTime();
             //   Character.Delay.NeedToSaveLucky = true;

                return itemBox;
            }

        }
        public void OpenUiSay(string say)
        {
            SendMessage(Service.OpenUiSay(5, say));
        }
        public void SendServerMessage(string say)
        {
            SendMessage(Service.ServerMessage(say));
        }
        public void MoveMap(short toX, short toY, int type = 0)
        {
            Character.InfoChar.X = toX;
            Character.InfoChar.Y = toY;
            if (type == 1)
            {
                var mpMine = (int)Character.InfoChar.OriginalMp / 100 *
                             (Character.InfoSkill.Monkey.MonkeyId > 0 ? 2 : 1);
                if (Character.InfoChar.Mp > mpMine)
                {
                    if (Character.InfoChar.MountId == -1)
                    {
                        MineMp(mpMine);
                    }
                }
            }
            //if (TaskHandler.CheckTask(Character, 0, 0) && Character.InfoChar.X >= 635)
            //{
            //    Server.Gi().Logger.Print("zz", "cyan");
            //    SendMessage(Service.OpenUiSay(5, $"Làm tốt lắm..\n"
            //                + $"Bây giờ bạn hãy vào nhà {TaskHandler.gI().Replace(Character, "#onggia")} bên phải để nhận nhiệm vụ mới nhé"));
            //    TaskHandler.gI().PlusSubTask(Character, 1);
            //   // TaskHandler.gI().PlusSubTask(Character, 1);

            //}
            SendZoneMessage(Service.PlayerMove(Character.Id, Character.InfoChar.X, Character.InfoChar.Y));
            if (Character.InfoSkill.MeTroi.IsMeTroi &&
                Character.InfoSkill.MeTroi.DelayStart <= ServerUtils.CurrentTimeMillis())
            {
                
                SkillHandler.RemoveTroi(Character);
            }

            var disciple = Character.Disciple;
            if (disciple != null && Character.InfoChar.IsHavePet && !Character.InfoChar.Fusion.IsFusion)
            {
                if (disciple.Status == 0 || disciple.Status == 1 && Math.Abs(Character.InfoChar.X - disciple.InfoChar.X) > 60 || disciple.Status == 2 && Math.Abs(Character.InfoChar.X - disciple.InfoChar.X) > 300 || disciple.Status == 3 && Math.Abs(Character.InfoChar.X - disciple.InfoChar.X) > 600)
                {
                    Character.Disciple.CharacterHandler.MoveMap(Character.InfoChar.X, Character.InfoChar.Y);
                }
            }

            var pet = Character.Pet;
            if (pet != null)
            {
                if (Math.Abs(Character.InfoChar.X - pet.InfoChar.X) > 60)
                {
                    Character.Pet.CharacterHandler.MoveMap(Character.InfoChar.X, Character.InfoChar.Y);
                }
            }
        }

        #endregion

        public void PlusHp(int hp)
        {
            lock (Character.InfoChar)
            {
                if (Character.InfoChar.IsDie) return;
                Character.InfoChar.Hp += hp;
                if (Character.InfoChar.Hp >= Character.HpFull) Character.InfoChar.Hp = Character.HpFull;
            }
        }

        public void MineHp(long hp)
        {
            lock (Character.InfoChar)
            {
                if (Character.InfoChar.IsDie || hp <= 0) return;

                if (hp > Character.InfoChar.Hp)
                {
                    Character.InfoChar.Hp = 0;
                }
                else
                {
                    Character.InfoChar.Hp -= hp;
                }

                if (Character.InfoChar.Hp <= 0)
                {
                    Character.InfoChar.IsDie = true;
                    Character.InfoChar.Hp = 0;
                }
            }
        }

        public void PlusMp(int mp)
        {
            lock (Character.InfoChar)
            {
                if (Character.InfoChar.IsDie) return;
                Character.InfoChar.Mp += mp;
                if (Character.InfoChar.Mp >= Character.MpFull) Character.InfoChar.Mp = Character.MpFull;
            }
        }

        public void MineMp(int mp)
        {
            lock (Character.InfoChar)
            {
                if (Character.InfoChar.IsDie || mp < 0) return;
                Character.InfoChar.Mp -= mp;
                if (Character.InfoChar.Mp <= 0) Character.InfoChar.Mp = 0;
            }
        }

        public void PlusStamina(int stamina)
        {
            lock (Character.InfoChar)
            {
                Character.InfoChar.Stamina += (short)stamina;
                if (Character.InfoChar.Stamina > 10000) Character.InfoChar.Stamina = 10000;
            }
        }

        public void MineStamina(int stamina)
        {
            // Bùa Dẻo Dai 218
            if (Character.InfoMore.BuaDeoDai)
            {
                if (Character.InfoMore.BuaDeoDaiTime > ServerUtils.CurrentTimeMillis())
                {
                    return;
                }
                else
                {
                    Character.InfoMore.BuaDeoDai = false;
                }
            }
            // 
            lock (Character.InfoChar)
            {
                if (stamina < 0) return;
                Character.InfoChar.Stamina -= (short)stamina;
                if (Character.InfoChar.Stamina <= 0) Character.InfoChar.Stamina = 0;
            }
        }

        public void PlusPower(long power)
        {
            lock (Character.InfoChar)
            {
                Character.InfoChar.Power += power;
                Character.InfoChar.Level = (sbyte)(Cache.Gi().EXPS.Count(exp => exp < Character.InfoChar.Power) - 1);
                if (Cache.Gi().LIMIT_POWERS[Character.InfoChar.LitmitPower].Power > Character.InfoChar.Power)
                {
                    Character.InfoChar.IsPower = true;
                }
                else
                {
                    Character.InfoChar.IsPower = false;
                }
            }
        }

        public void PlusPotential(long potential)
        {
            lock (Character.InfoChar)
            {
                Character.InfoChar.Potential += potential;
            }
        }

        public Model.Item.Item RemoveItemBody(int index)
        {
            Model.Item.Item item;
            lock (Character.ItemBody)
            {
                item = Character.ItemBody[index];
                if (item == null) return null;
                Character.ItemBody[index] = null;
                UpdateInfo();
             //   UpdateAntiChangeServerTime();
             //   Character.Delay.NeedToSaveBody = true;

                if (Character.ItemBody[5] != null) return item;

                SendMessage(Service.SendBody(Character));
                SendMessage(Service.PlayerLoadVuKhi(Character));
               
            }
            return item;
        }

        public void DropItemBody(int index)
        {
            var item = RemoveItemBody(index);
            var zone = MapManager.Get(Character.InfoChar.MapId)?.GetZoneById(Character.InfoChar.ZoneId);
            if (item == null || zone == null) return;
            zone.ZoneHandler.LeaveItemMap(new ItemMap()
            {
                PlayerId = Character.Id,
                X = Character.InfoChar.X,
                Y = Character.InfoChar.Y,
                Item = item,
            });

        }

        public void DropItemBag(int index)
        {
            var item = RemoveItemBag(index, reason: "Vứt vật phẩm");
            var zone = MapManager.Get(Character.InfoChar.MapId)?.GetZoneById(Character.InfoChar.ZoneId);
            if (item == null || zone == null) return;
            zone.ZoneHandler.LeaveItemMap(new ItemMap()
            {
                PlayerId = Character.Id,
                X = Character.InfoChar.X,
                Y = Character.InfoChar.Y,
                Item = item,
            });

        }
        public void CreatePetNormal()
        {
            var detu = new Disciple();
            detu.CreateNewDisciple(Character, ServerUtils.RandomNumber(0, 2));
            detu.Player = Character.Player;
            detu.CharacterHandler.SetUpInfo();

            Character.Disciple = detu;
            Character.InfoChar.IsHavePet = true;
            Character.CharacterHandler.SendMessage(Service.Disciple(1, null));
            Character.Zone.ZoneHandler.AddDisciple(detu);
            DiscipleDB.Create(detu);
        }
        public void PickItemMap(short id)
        {
           // try
           // {
                var zone = DataCache.IdMapCustom.Contains(Character.InfoChar.MapId)
                    ? MapManager.GetMapCustom(Character.InfoChar.MapCustomId)?.GetMapById(Character.InfoChar.MapId)?.GetZoneById(Character.InfoChar.ZoneId)
                    : MapManager.Get(Character.InfoChar.MapId)?.GetZoneById(Character.InfoChar.ZoneId);
                if (DataCache.IdMapSpecial.Contains(Character.InfoChar.MapId) || DataCache.IdMapKarin.Contains(Character.InfoChar.MapId))
                {
                    zone = Character.Zone;
                }
                var itemMap = zone.ItemMaps.Values.FirstOrDefault(item => item.Id == id);

                if (itemMap == null) return;
                if (itemMap.PlayerId == -2) return;
                if ((itemMap.PlayerId != -1 && itemMap.PlayerId != Character.Id) || (Character.InfoSkill.Egg.Monster != null && Character.InfoSkill.Egg.Monster.Character.Id != itemMap.PlayerId))
                {
                    SendMessage(Service.ServerMessage(TextServer.gI().ITEM_OF_ORTHER));
                    return;
                }

                if (Math.Abs(itemMap.X - Character.InfoChar.X) >= 70 && !Character.InfoMore.BuaThuHut)
                {
                    SendMessage(Service.ServerMessage(TextServer.gI().SO_FAR));
                    return;
                }

                lock (zone.ItemMaps)
                {
                    var itemNew = itemMap.Item;
                    var itemTemplate = ItemCache.ItemTemplate(itemNew.Id);
                    if (itemNew == null) return;
                    
                    switch (itemNew.Id)
                    {
                        case 380:
                            if (TaskHandler.CheckTask(Character, 30, 1))
                            {
                                TaskHandler.gI().PlusSubTask(Character, 1);

                            }
                            if (AddItemToBag(itemNew, "nhiệm vụ"))
                            {
                                zone.ZoneHandler.SendMessage(Service.ItemMapMePick(itemMap.Id, itemNew.Quantity, "a"));
                                SendMessage(Service.ServerMessage("Bạn nhận được " + itemTemplate.Name));
                            }
                            break;
                        case 85:
                            if (TaskHandler.CheckTask(Character, 15, 1))
                            {
                                TaskHandler.gI().PlusSubTask(Character, 1);
                                if (AddItemToBag(itemNew, "nhiệm vụ"))
                                {
                                    UpdatePhukien();
                                    zone.ZoneHandler.SendMessage(Service.ItemMapMePick(itemMap.Id, itemNew.Quantity, "a"));
                                    SendMessage(Service.ServerMessage("Bạn nhận được " + itemTemplate.Name));
                                }
                            }
                            break;
                        case 78:
                            TaskHandler.gI().PlusSubTask(Character, 1);
                            TaskHandler.gI().DoSendMessage(Character, "Wow, một cậu bé dễ thương\nHãy bế cậu bé về nhà!");
                            if (AddItemToBag(itemNew, "nhiệm vụ")){
                                UpdatePhukien();
                                zone.ZoneHandler.SendMessage(Service.ItemMapMePick(itemMap.Id, itemNew.Quantity, "a"));
                            }
                            
                            break;
                        case 861:
                            Character.PlusDiamondLock(itemNew.Quantity);
                          //  Character.DataBoMong.Count[16]++;
                            // Character.InfoChar.DiamondLock += itemNew.Quantity;
                            SendMessage(Service.MeLoadInfo(Character));
                           // if (itemNew.Quantity > 32767)
                           // {
                                SendMessage(Service.ServerMessage("Bạn nhặt được " + ServerUtils.GetMoney(itemNew.Quantity) + " ruby"));
                            zone.ZoneHandler.SendMessage(Service.ItemMapMePick(itemMap.Id, itemNew.Quantity, "a"));
                            // }

                            break;
                        case 372:
                        case 373:
                        case 374:
                        case 375:
                        case 376:
                        case 377:
                        case 378:
                            var timeserver = ServerUtils.CurrentTimeMillis();
                            if (ServerUtils.TimeNow().Hour == 20 && ServerUtils.TimeNow().Minute < 30)
                            {
                                if (timeserver - Blackball.gI().currMiliTimeStartBlackBall <= 1800000)
                                {
                                    var timeExist = timeserver - Blackball.gI().currMiliTimeStartBlackBall;
                                    Character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn sẽ được lụm sau " + timeExist / 1000 + "s nữa"));
                                    Server.Gi().Logger.Print("timeserver: " + timeserver + " - " + Blackball.gI().currMiliTimeStartBlackBall + " = " + (timeserver - Blackball.gI().currMiliTimeStartBlackBall), "manager");
                                    return;
                                }
                            }
                            Character.Blackball.PickBlackball(Character, itemNew.Id);
                            Character.CharacterHandler.SendMessage(Service.SendImageBag(Character.Id, 107));
                            break;
                        case 353:
                        case 354:
                        case 355:
                        case 356:
                        case 357:
                        case 358:
                        case 359:
                            Character.DataNgocRongNamek.DelayWish = 600000 + ServerUtils.CurrentTimeMillis();
                            Character.DataNgocRongNamek.IdNamekBall = itemNew.Id;
                            Init.NamecBalls.FirstOrDefault(i => i.Id == itemNew.Id).PlayerPick = Character.Id;
                            Character.InfoChar.Bag = 108;
                            Character.CharacterHandler.SendMessage(Service.SendImageBag(Character.Id, 108));
                            Character.InfoChar.TypePk = 5;
                            Character.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(Character.Id, 5));
                            Character.CharacterHandler.SendMessage(Service.ServerMessage($"Bạn nhận được {ItemCache.ItemTemplate(itemNew.Id).Name} !"));
                            break;
                      
                     
                        case 516:
                            {
                                PlusHp((int)Character.HpFull / 10);
                                PlusMp((int)Character.MpFull / 10);
                                SendMessage(Service.SendHp((int)Character.InfoChar.Hp));
                                SendMessage(Service.SendMp((int)Character.InfoChar.Mp));
                                zone.ZoneHandler.SendMessage(Service.PlayerLevel(Character), Character.Id);
                                zone.ZoneHandler.SendMessage(Service.ItemMapMePick(itemMap.Id, itemNew.Quantity, "a"));
                                SendMessage(Service.ServerMessage("Bạn nhận được " + itemTemplate.Name));

                                break;
                            }
                        case 20:
                            if (TaskHandler.CheckTask(Character, 9, 1))
                            {
                                TaskHandler.gI().PlusSubTask(Character, 1);
                                if (AddItemToBag(itemNew, "nhiệm vụ"))
                                {
                                    zone.ZoneHandler.SendMessage(Service.ItemMapMePick(itemMap.Id, itemNew.Quantity, "a"));
                                    SendMessage(Service.SendBag(Character));
                                    SendMessage(Service.ServerMessage("Bạn nhận được " + itemTemplate.Name));

                                }
                            }
                            if (AddItemToBag(itemNew, "nhiệm vụ"))
                            {
                                Character.Zone.ZoneHandler.SendMessage(Service.ItemMapMePick(itemMap.Id, itemNew.Quantity, "a"));
                                SendMessage(Service.SendBag(Character));

                                SendMessage(Service.ServerMessage("Bạn nhận được " + itemTemplate.Name));

                            }
                            break;
                        case 74:
                            {

                                if (Character.InfoChar.MapId - Character.InfoChar.Gender != 21)
                                {
                                    if (TaskHandler.CheckTask(Character, 2, 0))
                                    {
                                        TaskHandler.gI().PlusSubTask(Character, 1);
                                        if (AddItemToBag(itemNew, "nhiệm vụ"))
                                        {
                                            Character.Zone.ZoneHandler.SendMessage(Service.ItemMapMePick(itemMap.Id, itemNew.Quantity, "a"));
                                            SendMessage(Service.SendBag(Character));
                                            SendMessage(Service.ServerMessage("Bạn nhận được " + itemTemplate.Name));
                                        }
                                    }
                                }
                                else
                                {

                                    PlusHp((int)Character.HpFull);
                                    PlusMp((int)Character.MpFull);
                                    PlusStamina((int)Character.InfoChar.MaxStamina);
                                    SendMessage(Service.SendHp((int)Character.InfoChar.Hp));
                                    SendMessage(Service.SendMp((int)Character.InfoChar.Mp));
                                    SendMessage(Service.SendStamina(Character.InfoChar.Stamina));
                                    Character.Zone.ZoneHandler.SendMessage(Service.PlayerLevel(Character), Character.Id);
                                    Character.Zone.ZoneHandler.SendMessage(Service.ItemMapMePick(itemMap.Id, itemNew.Quantity, "a"));
                                    SendMessage(Service.ServerMessage("Bạn nhận được " + itemTemplate.Name));

                                }
                                break;


                            }
                        case 568: //Trứng ma bư
                            {
                               // if (Character.InfoChar.ThoiGianTrungMaBu > 0)
                               // {
                                    Character.InfoChar.ThoiGianTrungMaBu += (DataCache.TRUNG_MA_BU_TIME + ServerUtils.CurrentTimeMillis());
                                    if(AddItemToBag(false, itemNew, "Nhặt từ map")) SendMessage(Service.SendBag(Character));
                                    zone.ZoneHandler.SendMessage(Service.ItemMapMePick(itemMap.Id, itemNew.Quantity, "a"));
                                    SendMessage(Service.ServerMessage("Bạn đã nhặt được một quả trứng Ma Bư\nHãy về nhà kiểm tra"));
                               // }
                               
                                break;
                            }
                        //case 933:
                        //    {
                        //        // if(Character.LengthBagNull() < 1) {
                        //        //     SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_BAG));
                        //        //     return;
                        //        // }
                        //        // mảnh vỡ
                        //        var itemManhVoBongTai = Character.CharacterHandler.GetItemBagById(933);
                        //        if (itemManhVoBongTai != null)
                        //        {
                        //            var soLuongManhVoBongTaiHT = itemManhVoBongTai.Options.FirstOrDefault(opt => opt.Id == 31); //Số lượng bông tai
                        //            var soLuongManhVoBongTaiDrop = itemNew.Options.FirstOrDefault(opt => opt.Id == 31);
                        //            if (soLuongManhVoBongTaiHT != null && soLuongManhVoBongTaiDrop != null)
                        //            {
                        //                soLuongManhVoBongTaiHT.Param += soLuongManhVoBongTaiDrop.Param;
                        //            }
                        //            else
                        //            {
                        //                soLuongManhVoBongTaiHT.Param += 1;//default
                        //            }
                        //        }
                        //        else
                        //        {
                        //            if (!Character.CharacterHandler.AddItemToBag(true, itemNew, "Nhặt từ map")) return;
                        //        }
                        //        Character.CharacterHandler.SendMessage(Service.SendBag(Character));
                        //        SendMessage(Service.ItemMapMePick(itemMap.Id, itemNew.Quantity, TextServer.gI().EMPTY));
                        //        break;
                        //    }
                        case 992://nhẫn thời không
                            {
                                if (TaskHandler.CheckTask(Character, 32, 0))
                                {
                                    TaskHandler.gI().PlusSubTask(Character, 1); 
                                }
                                var itemNhanThoiKhongBag = Character.CharacterHandler.GetItemBagById(992);
                                var itemNhanThoiKhongBox = Character.CharacterHandler.GetItemBoxById(992);
                                if (itemNhanThoiKhongBag != null || itemNhanThoiKhongBox != null)
                                {
                                    SendMessage(Service.ServerMessage("Bạn đã có Nhẫn thời không sai lệch, không thể nhặt thêm"));
                                    return;
                                }
                                SendMessage(Service.ItemMapMePick(itemMap.Id, itemNew.Quantity, TextServer.gI().EMPTY));
                                if (AddItemToBag(false, itemNew, "Nhặt từ map")) SendMessage(Service.SendBag(Character));
                                SendMessage(Service.ServerMessage("Bạn nhận được " + itemTemplate.Name));

                                break;
                            }
                        case 1152://trứng linh thú
                            {
                                var timeServer = ServerUtils.CurrentTimeSecond();
                                var expireHours = 12;
                                var expireTime = timeServer + (expireHours * 3600);
                                itemNew.Options.Add(new OptionItem()
                                {
                                    Id = 211,
                                    Param = expireHours,
                                });
                                var optionHiden = itemNew.Options.FirstOrDefault(option => option.Id == 73);

                                if (optionHiden != null)
                                {
                                    optionHiden.Param = expireTime;
                                }
                                else
                                {
                                    itemNew.Options.Add(new OptionItem()
                                    {
                                        Id = 73,
                                        Param = expireTime,
                                    });
                                }
                                SendMessage(Service.ItemMapMePick(itemMap.Id, itemNew.Quantity, TextServer.gI().EMPTY));
                                if (AddItemToBag(false, itemNew, "Nhặt từ map")) SendMessage(Service.SendBag(Character));
                                SendMessage(Service.ServerMessage("Bạn nhận được " + itemTemplate.Name));

                                break;
                            }

                        default:
                            {
                                var text = TextServer.gI().EMPTY;
                                switch (itemTemplate.Type)
                                {
                                    case 9:
                                        {
                                            Character.PlusGold(itemNew.Quantity);
                                            // Character.InfoChar.Gold += itemNew.Quantity;
                                            SendMessage(Service.MeLoadInfo(Character));
                                        //if (itemNew.Quantity > 32767)
                                        //{
                                        text = "Bạn nhặt được " + ServerUtils.GetMoney(itemNew.Quantity) + " vàng";
                                        //}
                                        break;
                                        }
                                    case 10:
                                        {
                                            Character.PlusDiamond(itemNew.Quantity);
                                            Character.DataBoMong.Count[16]++;
                                            // Character.InfoChar.Diamond += itemNew.Quantity;
                                            SendMessage(Service.MeLoadInfo(Character));
                                            //if (itemNew.Quantity > 32767)
                                            //{
                                                text = "Bạn nhặt được " + ServerUtils.GetMoney(itemNew.Quantity) + " ngọc";
                                            //}
                                            break;
                                        }
                                    case 34:    
                                        {
                                            Character.PlusDiamondLock(itemNew.Quantity);
                                            Character.DataBoMong.Count[16]++;
                                            // Character.InfoChar.DiamondLock += itemNew.Quantity;
                                            SendMessage(Service.MeLoadInfo(Character));
                                         //  if (itemNew.Quantity > 32767)
                                          //  {
                                                text = "Bạn nhặt được " + ServerUtils.GetMoney(itemNew.Quantity) + " ruby";
                                           // }
                                            break;
                                        }
                                    default:
                                        {

                                            if (Character.LengthBagNull() < 1)
                                            {
                                                SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_BAG));
                                                return;
                                            }
                                            if (AddItemToBag(true, itemNew, "Nhặt từ map"))
                                            {
                                                SendMessage(Service.ServerMessage("Bạn nhận được " + itemTemplate.Name));
                                                SendMessage(Service.SendBag(Character));
                                            }
                                            else return;
                                        }
                                        break;
                                }
                                break;
                            }
                           
                    }
                }
                zone.ZoneHandler.SendMessage(Service.ItemMapPlayerPick(itemMap.Id, Character.Id), Character.Id);
             //   SendMessage(Service.ItemMapMePick(itemMap.Id, itemNew.Quantity, text));
                zone.ZoneHandler.RemoveItemMap(itemMap.Id);
        //    }
        //


           // catch (Exception e)
           // {
          //      Server.Gi().Logger.Error($"Error Send Handshake Message in Service.cs: {e.Message} \n {e.StackTrace}", e);
          //  }
        }
        

        public void LeaveItem(ICharacter character)
        {
            // Ignore
        }

        public void Dispose()
        {
            SuppressFinalize(this);
        }
    }
}