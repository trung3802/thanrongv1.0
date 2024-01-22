using System;
using System.Collections.Generic;
using TienKiemV2Remastered.Model;
using TienKiemV2Remastered.Model.Info;
using TienKiemV2Remastered.Model.Map;
using TienKiemV2Remastered.Model.SkillCharacter;
using TienKiemV2Remastered.Application.Extension;
using TienKiemV2Remastered.Application.Extension.Chẵn_Lẻ_Momo;
using TienKiemV2Remastered.Model.Task;
using TienKiemV2Remastered.Application.Extension.ChampionShip.ChampionShip_23;
using static TienKiemV2Remastered.Application.Extension.ChampionShip.ChampionShip;
using TienKiemV2Remastered.Application.Extension.Namecball;
using TienKiemV2Remastered.Application.Extension.Transfaction.Thách_Đấu;

namespace TienKiemV2Remastered.Application.Interfaces.Character
{
    public interface ICharacter : IDisposable
    {
        ICharacterHandler CharacterHandler { get; set; }
         InfoSkill InfoSkill { get; set; }
         Zone Zone { get; set; }
         int Id { get; set; }
        NamecBallHandler.DataNgocRongNamek DataNgocRongNamek { get; set; }

        int ClanId { get; set; }
         Player Player { get; set; }
         string Name { get; set; }
         InfoChar InfoChar { get; set; }
         TaskInfo InfoTask { get; set; }
         MapPrivate.MapPrivate MapPrivate { get; set; }
        long DameBossBangHoi { get; set; }
        int TypeTeleport { get; set; }
         sbyte Flag { get; set; }
         long HpFull { get; set; }
         long MpFull { get; set; }
         int DamageFull { get; set; }
         int DefenceFull { get; set; }
         int CritFull { get; set; }
         int HpPlusFromDamage { get; set; }
         int MpPlusFromDamage { get; set; }
         int HpPlusFromDamageMonster { get; set; }
         bool IsGetHpFull { get; set; }
         bool IsGetMpFull { get; set; }
         bool IsGetDamageFull { get; set; }
         bool IsGetDefenceFull { get; set; }
         bool IsGetCritFull { get; set; }
         bool IsHpPlusFromDamage { get; set; }
         bool IsMpPlusFromDamage { get; set; }
         int DiemSuKien { get; set; }
         Died_Ring DataVoDaiSinhTu { get; set; }
         Extension.Bo_Mong.BoMong DataBoMong { get; set; }
         List<SkillCharacter> Skills { get; set; }
         List<Model.Item.Item> ItemBody { get; set; }
         List<Model.Item.Item> ItemBag { get; set; }
         List<Model.Item.Item> ItemBox { get; set; }
         InfoOption InfoOption { get; set; }
         InfoSet InfoSet { get; set; }
         DataCharacter DataDaiHoiVoThuat { get; set; }
       DataDaiHoiVoThuat23 DataDaiHoiVoThuat23 { get; set; }
         /// <summary>
         ChanLe DataMiniGame { get; set; }
        int TypeDragon { get; set; }
        Sources.Base.Info.Enchant DataEnchant { get; set; }
        Thách_Đấu Challenge { get; set; }
         /// </summary>
         /// <returns></returns>
         bool IsInvisible();
         short GetHead(bool isMonkey = true);
         short GetBody(bool isMonkey = true);
         short GetLeg(bool isMonkey = true);
         void PlusGold(int gold);
         short GetBag();
         void SetGetFull(bool isGet);
         int LengthBagNull();
         int LengthBoxNull();

         int BagLength();
         int BoxLength();
         int BodyLength();
         bool CheckLockInventory();
         bool IsDontMove();

    }
}