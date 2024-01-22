using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.Handlers.Skill;
using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.Interfaces.Map;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.Model.Character;
using TienKiemV2Remastered.Model.Item;
using TienKiemV2Remastered.Model.Map;
using TienKiemV2Remastered.Model.Monster;
using TienKiemV2Remastered.Model.Template;
using TienKiemV2Remastered.Model;
using static System.GC;
using TienKiemV2Remastered.Application.Extension;
using TienKiemV2Remastered.Application.Extension.BlackballWar;
using System.Threading;
using System.Runtime.InteropServices;

namespace TienKiemV2Remastered.Application.Handlers.Map
{
    public class ZoneHandler : IZoneHandler
    {
        public Zone Zone { get; set; }

        public ZoneHandler(Zone zone)
        {
            Zone = zone;
        }
        #region TypeTeleport
        public static int AUTO_SPACE_SHIP = -1;
        public static int NON_SPACE_SHIP = 0;
        public static int DEFAULT_SPACE_SHIP = 1;
        public static int TELEPORT_YARDRAT = 2;
        public static int TENNIS_SPACE_SHIP = 3;
        #endregion

        public int GetCountMob()
        {
            var count = 0;
            foreach (var mob in Zone.MonsterMaps)
            {
                if (!mob.IsDie)
                {
                    count++;
                }
            }
            return count;
        }
        public void JoinZone(Model.Character.Character character, bool isDefault, bool isTeleport, int typeTeleport)
        {

            lock (Zone.Characters)
            {
                if (character == null || character.GetType() != typeof(Model.Character.Character)) return;
                character.CharacterHandler.SendMessage(Service.SendImageBag(character.Id, character.GetBag()));
                character.TypeTeleport = typeTeleport;
                character.InfoChar.MapId = Zone.Map.Id;
                character.InfoChar.ZoneId = Zone.Id;
                character.CharacterHandler.SendMessage(Service.MapClear());
                character.CharacterHandler.SendMessage(Service.SendStamina(character.InfoChar.Stamina));
                if (isDefault && !isTeleport)
                {
                    character.InfoChar.X = Zone.Map.TileMap.toX;
                    character.InfoChar.Y = Zone.Map.TileMap.toY;
                } else if (isTeleport)
                {
                    character.InfoChar.X = (short)ServerUtils.RandomNumber(250, 450);
                    character.InfoChar.Y = 0;
                }
                character.UpdateOldMap();
                if (!character.InfoChar.IsOutZone && character.Zone != null)
                {
                    character.Zone.ZoneHandler.OutZone(character);
                    Server.Gi().Logger.Print("Outzone character: " + character.Id + " success", "cyan");
                }
                if (Zone.Characters.TryAdd(character.Id, character))
                {
                    var disciple = character.Disciple;
                    var checkHaveDisciple = false;
                    if (disciple is { Status: < 3 } && character.InfoChar.IsHavePet && !character.InfoChar.Fusion.IsFusion && !DataCache.IdMapKarin.Contains(Zone.Map.Id))
                    {
                        if (Zone.Disciples.TryAdd(disciple.Id, disciple))
                        {
                            checkHaveDisciple = true;
                            disciple.Zone = Zone;
                            disciple.CharacterHandler.SetUpPosition(isRandom: true);
                            disciple.PlusPoint.RandomPoint(disciple);
                        }
                    }

                    var checkHavePet = false;
                    var pet = character.Pet;
                    if (pet != null && !DataCache.IdMapKarin.Contains(Zone.Map.Id))
                    {
                        if (Zone.Pets.TryAdd(pet.Id, pet))
                        {
                            checkHavePet = true;
                            pet.Zone = Zone;
                            pet.CharacterHandler.SetUpPosition(isRandom: true);
                        }
                    }

                    if (character.InfoSkill.Egg.Monster != null)
                    {
                        if (Zone.MonsterPets.TryAdd(character.InfoSkill.Egg.Monster.IdMap, character.InfoSkill.Egg.Monster))
                        {
                            character.InfoSkill.Egg.Monster.Zone = Zone;
                            SendMessage(Service.UpdateMonsterMe0(character.InfoSkill.Egg.Monster));
                        }
                        else
                        {
                            SkillHandler.RemoveMonsterPet(character);
                        }
                    }
                    character.IsNextMap = true;
                    foreach (var @char in Zone.Characters.Values.Where(c => c.Id != character.Id))
                    {
                        @char.CharacterHandler.SendMessage(Service.PlayerAdd(character));
                        if (checkHaveDisciple) @char.CharacterHandler.SendMessage(Service.PlayerAdd(disciple, "#"));
                        if (checkHavePet) @char.CharacterHandler.SendMessage(Service.PlayerAdd(pet, "#"));
                    }
                    character.Zone = Zone;
                    character.CharacterHandler.SendMessage(Service.MapInfo(Zone, character));
                    character.CharacterHandler.SetUpInfo();
                    character.CharacterHandler.SendMessage(Service.UpdateBody(character));
                    //UpdateBag
                    character.CharacterHandler.SendMessage(Service.PlayerLoadSpeed(character));
                    if (character.Flag > 0 || character.Flag <= 12)
                    {
                        character.CharacterHandler.SendMessage(Service.ChangeFlag2(character.Flag));
                    }
                    if (!isTeleport || typeTeleport != 3) return;
                    character.InfoChar.Hp = character.HpFull;
                    character.InfoChar.Mp = character.MpFull;
                    character.CharacterHandler.SendMessage(Service.SendHp((int)character.InfoChar.Hp));
                    character.CharacterHandler.SendMessage(Service.SendMp((int)character.InfoChar.Mp));
                    SendMessage(Service.PlayerLevel(character));
                    character.InfoChar.IsOutZone = false;
                }
                else
                {
                    switch (character.InfoChar.MapId)
                    {
                        case 153:
                            ClanManager.Get(character.ClanId).ClanZone.Maps[0].OutZone(character, 153);
                            break;
                        case int i when DataCache.IdMapKarin.Contains(i):
                            character.MapPrivate.GetMapById(i).OutZone(character, i);
                            Server.Gi().Logger.Print("Out character: " + character.Id + " from Karin zone success", "cyan");
                            break;
                        default:
                            character.Zone.ZoneHandler.OutZone(character);
                            Server.Gi().Logger.Print("Out character: " + character.Id + " from zone success", "cyan");
                            break;
                    }
                    ClientManager.Gi().KickSession(character.Player.Session);
                    Server.Gi().Logger.Print("Tryadd character: " + character.Id + " fail", "cyan");

                }
            }
        }


        public void AddDisciple(Disciple disciple)
        {
            if (Zone.Map.IsMapCustom())
            {
                return;
            }
            if (!Zone.Disciples.TryAdd(disciple.Id, disciple)) return;
            disciple.Zone = Zone;
            lock (Zone.Characters)
            {
                foreach (var character in Zone.Characters.Values)
                {
                    var text = "#";
                    if (character.Id + disciple.Id == 0) text = "$";
                    character?.CharacterHandler.SendMessage(Service.PlayerAdd(disciple, text));
                }
            }
        }

        public void RemoveDisciple(Disciple disciple)
        {
            lock (Zone.Disciples)
            {
                if (!Zone.Disciples.TryRemove(disciple.Id, out _)) return;
                SendMessage(Service.PlayerRemove(disciple.Id));
                if (disciple.InfoSkill.Egg.Monster == null) return;
                RemoveMonsterMe(disciple.InfoSkill.Egg.Monster.IdMap);
                SendMessage(Service.UpdateMonsterMe7(disciple.InfoSkill.Egg.Monster.IdMap));

            }
        }

        public void AddPet(Pet pet)
        {
            if (Zone.Map.IsMapCustom())
            {
                return;
            }
            if (!Zone.Pets.TryAdd(pet.Id, pet)) return;
            pet.Zone = Zone;
            lock (Zone.Characters)
            {
                foreach (var character in Zone.Characters.Values)
                {
                    var text = "#";
                    if (character.Id + pet.Id == 0) text = "$";
                    character?.CharacterHandler.SendMessage(Service.PlayerAdd(pet, text));
                }
            }
        }

        public void RemovePet(Pet pet)
        {
            lock (Zone.Pets)
            {
                try
                {
                    if (!Zone.Pets.TryRemove(pet.Id, out _)) return;
                    SendMessage(Service.PlayerRemove(pet.Id));
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error RemovePet in ZoneHandler.cs {e.Message} \n {e.StackTrace}", e);
                }
            }
        }

        public void AddBoss(Boss boss)
        {
            if (Zone.Map.IsMapCustom())
            {
                return;
            }
            if (!Zone.Bosses.TryAdd(boss.Id, boss)) return;
            boss.Zone = Zone;
            boss.CharacterHandler.SetUpPosition(mapNew: Zone.Map.Id);
            lock (Zone.Characters)
            {
                foreach (var character in Zone.Characters.Values)
                {
                    character?.CharacterHandler.SendMessage(Service.PlayerAdd(boss));
                }
            }
        }

        public void RemoveBoss(Boss boss)
        {
            lock (Zone.Bosses)
            {
                try
                {
                    if (!Zone.Bosses.TryRemove(boss.Id, out _)) return;
                    SendMessage(Service.PlayerRemove(boss.Id));
                    if (boss.InfoSkill.Egg.Monster == null) return;
                    RemoveMonsterMe(boss.InfoSkill.Egg.Monster.IdMap);
                    SendMessage(Service.UpdateMonsterMe7(boss.InfoSkill.Egg.Monster.IdMap));
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error RemoveBoss in ZoneHandler.cs: {e.Message} \n {e.StackTrace}", e);
                }
            }
        }



        public void OutZone(ICharacter character, bool isOutZone = false)
        {

            if (character == null) return;
            if (Zone == null || Zone.Map == null) return;
            lock (Zone.Characters)
            {
                try
                {
                    var @charReal = (Model.Character.Character)character;
                    var mapOld = character.InfoChar.MapId;
                    if (Zone.Characters.TryRemove(character.Id, out _))
                    {
                        SendMessage(Service.PlayerRemove(character.Id), character.Id);

                        if (character.InfoSkill.Egg.Monster != null)
                        {
                            RemoveMonsterMe(character.InfoSkill.Egg.Monster.IdMap);
                            SendMessage(Service.UpdateMonsterMe7(character.InfoSkill.Egg.Monster.IdMap));
                        }

                        var disciple = @charReal.Disciple;
                        if (disciple != null && (disciple.Status < 3 || disciple.InfoChar.IsDie) && character.InfoChar.IsHavePet && !character.InfoChar.Fusion.IsFusion)
                        {
                            if (Zone.Disciples.TryRemove(disciple.Id, out _))
                            {
                                //Zone.Disciples.Remove(disciple);
                                disciple.MonsterFocus = null;
                                SendMessage(Service.PlayerRemove(disciple.Id), character.Id);
                                if (disciple.InfoSkill.Egg.Monster != null)
                                {
                                    RemoveMonsterMe(disciple.InfoSkill.Egg.Monster.IdMap);
                                    SendMessage(Service.UpdateMonsterMe7(disciple.InfoSkill.Egg.Monster.IdMap));
                                }
                            }
                        }

                        var pet = @charReal.Pet;
                        if (pet != null)
                        {
                            if (Zone.Pets.TryRemove(pet.Id, out _))
                            {
                                SendMessage(Service.PlayerRemove(pet.Id), character.Id);
                            }
                        }
                    }

                    if (@charReal.Trade.IsTrade)
                    {
                        var charTemp = (Model.Character.Character)GetCharacter(@charReal.Trade.CharacterId);
                        if (charTemp != null && charTemp.Trade.CharacterId == character.Id)
                        {
                            charTemp.CloseTrade(true);
                            charTemp.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().CLOSE_TRADE));
                        }
                        @charReal.CloseTrade(false);
                    }
                    if (Blackball.gI().ListMapNRSD.Contains(@charReal.InfoChar.MapId) && @charReal.Blackball.AlreadyPick(charReal))
                    {
                        @charReal.Blackball.ExitMapOrDie(@charReal);
                    }
                    if (charReal.Challenge.isChallenge)
                    {
                        var player = (Model.Character.Character)ClientManager.Gi().GetCharacter(character.Challenge.PlayerChallengeID);
                        var gold = (player.Challenge.Gold - (character.Challenge.Gold / 100)) + (player.Challenge.Gold - (character.Challenge.Gold / 100));
                        player.CharacterHandler.SendMessage(Service.ServerMessage($"Đối thủ đã bỏ trốn, bạn đã nhận được {gold} vàng"));
                        player.PlusGold(gold);
                        player.CharacterHandler.SendMessage(Service.MeLoadInfo(player));
                        character.Challenge.SetStatusEnd((Model.Character.Character)character);
                        player.Challenge.SetStatusEnd(player);
                    }
                    if (character.DataNgocRongNamek.AlreadyPick(charReal) && (mapOld >= 24 || mapOld <= 26))
                    {
                        var itm = new ItemMap(-1, ItemCache.GetItemDefault((short)(character.DataNgocRongNamek.IdNamekBall)));
                        itm.X = character.InfoChar.X;
                        itm.Y = character.InfoChar.Y;
                        character.Zone.ZoneHandler.LeaveItemMap(itm);
                        character.InfoChar.TypePk = 0;
                        character.DataNgocRongNamek.IdNamekBall = -1;
                        character.InfoChar.Bag = ClanManager.Get(character.ClanId) != null ? (sbyte)ClanManager.Get(character.ClanId).ImgId : (sbyte)-1;
                    }
                    charReal.InfoChar.IsOutZone = true;
                } catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error OutZone in ZoneHandler.cs: {e.Message} \n {e.StackTrace}", e);
                }
            }
        }

        public void InitMob()
        {
            var tileMap = Zone.Map.TileMap;
            MonsterTemplate monsterTemplate;
            var i = 0;
            tileMap.MonsterMaps.ForEach(monster =>
            {
                monsterTemplate = Cache.Gi().MONSTER_TEMPLATES.FirstOrDefault(x => x.Id == monster.Id);

                if (monsterTemplate != null)
                {
                    switch (monsterTemplate.Id)
                    {
                        case 77:
                            Zone.MonsterMaps.Add(new MonsterMap()
                            {
                                IdMap = i++,
                                Id = monster.Id,
                                X = monster.X,
                                Y = monster.Y,
                                Status = 5,
                                IsRefresh = false,
                                IsDie = true,
                                
                                Level = monster.Level,
                                LvBoss = 200,
                                IsBoss = false,
                                Zone = Zone,
                                OriginalHp = 10000000,//GetMonsterHP(Zone.Map.Id, monster.Id, monsterTemplate),
                                LeaveItemType = monsterTemplate.LeaveItemType,
                            });
                            break;
                        default:
                            Zone.MonsterMaps.Add(new MonsterMap()
                            {
                                IdMap = i++,
                                Id = monster.Id,
                                X = monster.X,
                                Y = monster.Y,
                                Status = 5,
                                Level = monster.Level,
                                LvBoss = 0,
                                IsBoss = false,
                                Zone = Zone,
                                OriginalHp = monsterTemplate.Hp,//GetMonsterHP(Zone.Map.Id, monster.Id, monsterTemplate),
                                LeaveItemType = monsterTemplate.LeaveItemType,
                            });
                            break;
                    }
                }
            });
            Zone.MonsterMaps.ForEach(monster => monster.MonsterHandler.SetUpMonster());
        }

        public void Update()
        {
            if (Zone.Characters.Count > 0)
            {
                try
                {
                    UpdatePlayer();
                    UpdateDisciple();
                    UpdateItemMap();
                    UpdateMonsterPet();
                    UpdatePet();
                    UpdateMonsterMap();
                    UpdateBoss();
                }catch(Exception e)
                {
                    Server.Gi().Logger.Error($"Error Update in ZoneHandler.cs: {e.Message} \n {e.StackTrace}", e);

                }
            }
        }

        #region Update Zone
        private void UpdateMonsterMap()
        {
            var monsterMap = CollectionsMarshal.AsSpan(Zone.MonsterMaps.ToList());
            for (int i = 0; i < monsterMap.Length; i++)
            {
                monsterMap[i].MonsterHandler.Update();
            }
            Thread.Sleep(50);
        }

        private void UpdateMonsterPet()
        {
            var monsterPets = CollectionsMarshal.AsSpan(Zone.MonsterPets.ToList());
            for (int i = 0; i < monsterPets.Length; i++)
            {
                monsterPets[i].Value.MonsterHandler.Update();
            }
            Thread.Sleep(50);
        }

        private void UpdateItemMap()
        {
            var items = CollectionsMarshal.AsSpan(Zone.ItemMaps.ToList());
            for (int i = 0; i < items.Length; i++)
            {
                items[i].Value.ItemMapHandler.Update(Zone);
            }
            Thread.Sleep(50);
        }
        private void UpdatePlayer()
        {
            var characters = CollectionsMarshal.AsSpan(Zone.Characters.ToList());
            for (int i = 0; i < characters.Length; i++)
            {
                var character = characters[i].Value;
                if (character != null) character.CharacterHandler.Update();
            }
            Thread.Sleep(50);
        }


        private void UpdateDisciple()
        {
            var disciples = CollectionsMarshal.AsSpan(Zone.Disciples.ToList());
            for (int i = 0; i < disciples.Length; i++)
            {
                disciples[i].Value.CharacterHandler.Update();
            }
            Thread.Sleep(50);
        }

        private void UpdatePet()
        {
            var pets = CollectionsMarshal.AsSpan(Zone.Pets.ToList());
            for (int i = 0; i < pets.Length; i++)
            {
                pets[i].Value.CharacterHandler.Update();
            }
            Thread.Sleep(50);
        }

        private void UpdateBoss()
        {
            try
            {
                var bosses = CollectionsMarshal.AsSpan(Zone.Bosses.ToList());
                for (int i = 0; i < bosses.Length; i++)
                {
                    bosses[i].Value.CharacterHandler.Update();
                }
            }
            catch
            {

            }
            Thread.Sleep(50);
        }
        #endregion

        public void Close()
        {
            Zone.ItemMaps.Clear();
            Zone.MonsterPets.Clear();
            Zone.MonsterMaps.Clear();
            Zone.Bosses.Clear();
            Zone.Disciples.Clear();
            Zone.Pets.Clear();
            Zone.Map = null;
            SuppressFinalize(this);
        }

        public void SendMessage(Message message, bool isSkillMessage = false)
        {
            lock (Zone.Characters)
            {
                foreach (var character in Zone.Characters.Values)
                {
                    if (isSkillMessage && !character.InfoChar.HieuUngDonDanh) continue;
                    character?.CharacterHandler.SendMessage(message);
                }
            }
        }
        public void SendMessage(Message message, int id)
        {
            lock (Zone.Characters)
            {
                foreach (var character in Zone.Characters.Values)
                {
                    character?.CharacterHandler.SendMessage(message);
                }
            }
        }

        public void LeaveItemMap(ItemMap itemMap)
        {
            if(itemMap == null) return;
            try
            {
                lock (Zone.ItemMaps)
                {
                    if(itemMap?.Item == null) return;
                    if (Zone.ItemMaps.Count > 500) RemoveItemMap(0);
                    {
                        itemMap.Id = GetItemMapNotId();
                        itemMap.X = (short)ServerUtils.RandomNumber(itemMap.X - 15, itemMap.X + 15);
                        itemMap.Y = Zone.Map.TileMap.TouchY(itemMap.X, itemMap.Y);
                    }
                    if (Zone.ItemMaps.TryAdd(itemMap.Id, itemMap))
                    {
                        SendMessage(Service.ItemMapAdd(itemMap));
                    }
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error LeaveItemMap in ZoneHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }

        public void LeaveItemMap(ItemMap itemMap, MonsterMap monster)
        {
            if(itemMap == null) return;
            try
            {
                lock (Zone.ItemMaps)
                {
                    if(itemMap?.Item == null) return;
                    if (Zone.ItemMaps.Count > 500) RemoveItemMap(0);
                    itemMap.Id = GetItemMapNotId();
                    itemMap.X = (short)ServerUtils.RandomNumber(itemMap.X - 30, itemMap.X + 30);
                    itemMap.Y = Zone.Map.TileMap.TouchY(itemMap.X, itemMap.Y);
                    if (Zone.ItemMaps.TryAdd(itemMap.Id, itemMap))
                    {
                        if (Cache.Gi().MONSTER_TEMPLATES[monster.Id].Type == 4)
                        {
                            SendMessage(Service.MonsterFlyLeaveItem(monster.IdMap, itemMap));
                        }
                        else
                        {
                            SendMessage(Service.ItemMapAdd(itemMap));
                        }   
                    }
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error LeaveItemMap in ZoneHandler.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }

        public void RemoveItemMap(int id)
        {
            lock (Zone.ItemMaps)
            {
                if (Zone.ItemMaps.TryRemove(id, out var it))
                {
                    SendMessage(Service.ItemMapRemove(id));
                    it.Dispose();
                }
            }
        }

        public int GetItemMapNotId()
        {
            if (Zone.ItemMapId > 500) Zone.ItemMapId = 0;
            return Zone.ItemMapId++;
        }

        public void RemoveCharacter()
        {
            lock (Zone.Characters)
            {
                //Remove character
            }
        }

        public void RemoveMonsterMe(int id)
        {
            lock (Zone.MonsterPets)
            {
                Zone.MonsterPets.TryRemove(id, out _);
            }
        }

        public bool AddMonsterPet(MonsterPet monsterPet)
        {
            lock (Zone.MonsterPets)
            {
                return Zone.MonsterPets.TryAdd(monsterPet.IdMap, monsterPet);
            }
        }
        public ICharacter GetDisciple(int id)
        {
            return GetDiscipleKeyValue(id).Value;
        }
        public ICharacter GetCharacter(int id)
        {
            return GetCharacterKeyValue(id).Value;
        }
         
            public List<ICharacter> GetCharacterClanInMap(int idClan)
        {
            List<ICharacter> charactersClan = new List<ICharacter>();

            lock (Zone.Characters)
            {
                foreach (var character in Zone.Characters.Values)
                {
                    if (character.ClanId == idClan) {
                        charactersClan.Add(character);
                    } 
                }
            }

            return charactersClan;
        }
        public List<ICharacter> GetCharacterClanHasNamecBallInMap(int idClan)
        {
            List<ICharacter> charactersClan = new List<ICharacter>();

            lock (Zone.Characters)
            {
                foreach (var character in Zone.Characters.Values)
                {
                    if (character.ClanId == idClan && character.DataNgocRongNamek.AlreadyPick(character))
                    {
                        charactersClan.Add(character);
                    }
                }
            }

            return charactersClan;
        }

        public List<ICharacter> CharacterInMap(){
            List<ICharacter> characters = new List<ICharacter>();
            lock(Zone.Characters){
                foreach(var @char in Zone.Characters.Values){
                    characters.Add(@char);
                }
            }
            return characters;
        }
        public List<ICharacter> BossInMap(){
             List<ICharacter> bosses = new List<ICharacter>();
            lock(Zone.Bosses){
                foreach(var @boss in Zone.Bosses.Values){
                    bosses.Add(@boss);
                }
            }
            return bosses;
        }
        public List<ICharacter> GetBossInMap(int type)
        {
            List<ICharacter> BossInMap = new List<ICharacter>();

            lock (Zone.Bosses)
            {
                foreach (var boss in Zone.Bosses.Values)
                {
                    if (boss.Type == type)
                    {
                        BossInMap.Add(boss);
                    }
                }
            }

            return BossInMap;
        }
        public List<ICharacter> GetBossInMap()
        {
            List<ICharacter> BossInMap = new List<ICharacter>();

            lock (Zone.Bosses)
            {
                foreach (var boss in Zone.Bosses.Values)
                {
                    
                        BossInMap.Add(boss);
                    
                }
            }

            return BossInMap;
        }
        public List<ItemMap> GetItemInMap(int type)
        {
            List<ItemMap> ItemInMap = new List<ItemMap>();

            lock (Zone.ItemMaps)
            {
                foreach (var ItemMaps in Zone.ItemMaps.Values)
                {
                    if (ItemCache.ItemTemplate(ItemMaps.Item.Id).Type== type)
                    {
                        ItemInMap.Add(ItemMaps);
                    }
                }
            }

            return ItemInMap;
        }
            public List<ItemMap> GetItemMapsByID(int id){
                List<ItemMap> ItemMaps = new List<ItemMap>();
                foreach (var itm in Zone.ItemMaps.Values){
                    if (itm.Item.Id == id){
                        ItemMaps.Add(itm);
                    }
                }
                return ItemMaps;
            }
      

        public ICharacter GetPet(int id)
        {
            return GetPetKeyValue(id).Value;
        }

        public ICharacter GetBoss(int id)
        {
            return GetBossKeyValue(id).Value;
        }

        public MonsterMap GetMonsterMap(int id)
        {
            lock (Zone.MonsterMaps)
            {
                return Zone.MonsterMaps.FirstOrDefault(m => m.IdMap == id);
            }
        }
        public MonsterMap GetMobId(int id)
        {
            lock (Zone.MonsterMaps)
            {
                return Zone.MonsterMaps.FirstOrDefault(m => m.Id == id);
            }
        }

        public MonsterPet GetMonsterPet(int id)
        {
            lock (Zone.MonsterPets)
            {
                return Zone.MonsterPets.GetValueOrDefault((short)id);
            }
        }
       

        public KeyValuePair<int, Model.Character.Character> GetCharacterKeyValue(int id)
        {
            lock (Zone.Characters)
            {
                return Zone.Characters.FirstOrDefault(c => c.Key == id);
            }
        }

        public KeyValuePair<int, Disciple> GetDiscipleKeyValue(int id)
        {
            lock (Zone.Disciples)
            {
                return Zone.Disciples.FirstOrDefault(c => c.Key == id);
            }
        }

        public KeyValuePair<int, Pet> GetPetKeyValue(int id)
        {
            lock (Zone.Pets)
            {
                return Zone.Pets.FirstOrDefault(c => c.Key == id);
            }
        }

        public KeyValuePair<int, Boss> GetBossKeyValue(int id)
        {
            lock (Zone.Bosses)
            {
                return Zone.Bosses.FirstOrDefault(c => c.Key == id);
            }
        }
    }
}