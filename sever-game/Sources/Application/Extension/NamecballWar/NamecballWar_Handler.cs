using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Model.Clan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienKiemV2Remastered.Model.Character;
using TienKiemV2Remastered.Application.Main;

namespace TienKiemV2Remastered.Application.Extension.NamecballWar
{
    public class NamecballWar_Handler
    {
          
        public static void ConfirmMenu(short npcId,Character character,int select)
        {
            switch (character.TypeMenu)
            {
                case 0:
                    {
                        switch (select)
                        {
                            case 0:
                                string.Format(NamecballWar.ListMenus[1][1], character.InfoChar.TotalPotential);
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ngọc rồng Namec đang bị 2 thế lực tranh giành\nHãy chọn cấp độ tham gia tùy chọn theo sức mạnh bản thân", NamecballWar.ListMenus[1], character.InfoChar.Gender));
                                character.TypeMenu = 1;
                                break;
                        }
                    }
                    break;
                case 1:
                    {
                        switch (select)
                        {
                            case 0:
                                break;
                            case 1:

                                break;
                        }
                    }
                    break;
            }
        }
       
    }
}
