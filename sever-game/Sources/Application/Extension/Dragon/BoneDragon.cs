using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Model.Character;
using TienKiemV2Remastered.Model.SkillCharacter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Application.Extension.Dragon
{
    public class BoneDragon 
    {
        public string Điều_Ước = "Ta sẽ ban cho ngươi một điều ước, ngươi có 5 phút, hãy suy nghĩ thật kĩ:\n" +
                                 "1) Đổi cả 3 kỹ năng đầu của đệ tử (Lưu ý:Kỹ năng mới có cấp 1 và vẫn có\n" +
                                 "thể trùng lại với kỹ năng vốn có).\n" +
                                 "2) Tăng thêm 20% tấn công khi hợp thể bằng bông tai Porata trong 90\n" +
                                 "phút.\n" +
                                 "3) X2 sức mạnh và tiềm năng đệ tử khi đánh quái cùng sư phụ trong 90\n" +
                                 "phút.\n" +
                                 "4) Ngươi có thể nhận 3 phiếu giảm giá";
        public List<string> Menu_Con_Rồng = new List<string> { "Điều\nước 1" ,"Điều\nƯớc 2", "Điều\nƯớc 3", "Điều\nƯớc 4"};
        public static BoneDragon instance;
        public static BoneDragon gI()
        {
            if (instance == null)
            {
                instance = new BoneDragon();
            }
            return instance;
        }
        public void Mở_Menu(Character character)
        {
            for (int i = 807; i <= 813; i++)
            {
                if (character.CharacterHandler.GetItemBagById(i).Quantity <= 0 || character.CharacterHandler.GetItemBagById(i)== null)
                {
                    character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_GENDER));
                    return;
                }
            }
            for (short dball = 807; dball <= 813; dball++)
            {
                character.CharacterHandler.RemoveItemBagById(dball, 1, reason: "Gọi rồng");
            }
            MapManager.SetDragonAppeared(true);
            character.CharacterHandler.SendMessage(Service.SendBag(character));
            character.CharacterHandler.SendMessage(Service.CallDragon(character, 2));
            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(24, Điều_Ước, Menu_Con_Rồng, 2));
        }
        public void Ước(Character nhân_vật,int lựa_chọn)
        {
            switch (lựa_chọn)
            {
                case 0:
                    var disciple = nhân_vật.Disciple;
                    var disciplePower = disciple.InfoChar.Power;
                    var randomSkill = DataCache.IdSkillDisciple1[ServerUtils.RandomNumber(DataCache.IdSkillDisciple1.Count)];
                    disciple.Skills[0] = new SkillCharacter()
                    {
                        Id = randomSkill,
                        SkillId = Disciple.GetSkillId(randomSkill),
                        Point = 1,
                    };
                    //if (disciplePower >= 1200 && disciple.Skills.Count >= 1)
                    //{
                    //    randomSkill = DataCache.IdSkillDisciple1[ServerUtils.RandomNumber(DataCache.IdSkillDisciple1.Count)];
                    //    disciple.Skills[0] = new SkillCharacter() // skill 1
                    //    {
                    //        Id = randomSkill,
                    //        SkillId = Disciple.GetSkillId(randomSkill),
                    //        Point = 1,
                    //    };
                    //}
                    if (disciplePower >= 150000000 && disciple.Skills.Count >= 2)
                    {
                        randomSkill = DataCache.IdSkillDisciple2[ServerUtils.RandomNumber(DataCache.IdSkillDisciple2.Count)];
                        disciple.Skills[1] = new SkillCharacter() // skill 2
                        {
                            Id = randomSkill,
                            SkillId = Disciple.GetSkillId(randomSkill),
                            Point = 1,
                        };
                    }

                    if (disciplePower >= 1500000000 && disciple.Skills.Count >= 3)
                    {
                        randomSkill = DataCache.IdSkillDisciple3[ServerUtils.RandomNumber(DataCache.IdSkillDisciple3.Count)];
                        disciple.Skills[2] = new SkillCharacter() // skill 3
                        {
                            Id = randomSkill,
                            SkillId = Disciple.GetSkillId(randomSkill),
                            Point = 1,
                        };
                    }
                    break;
                case 1:
                    nhân_vật.InfoBuff.effRongXuong = true;
                    nhân_vật.InfoBuff.effRongXuongTime = ServerUtils.CurrentTimeMillis() +5400000;
                    break;
                case 2:
                    var pgg = ItemCache.GetItemDefault(459);
                    pgg.Quantity = 3;
                    nhân_vật.CharacterHandler.AddItemToBag(true, pgg, "Ước Rồng Xương");
                    nhân_vật.CharacterHandler.SendMessage(Service.SendBag(nhân_vật));
                    break;
            }
            nhân_vật.CharacterHandler.SendMessage(Service.CallDragon(1, 0, nhân_vật));
            MapManager.SetDragonAppeared(false);
        }
    }
}
