using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Newtonsoft.Json;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.Model;
using TienKiemV2Remastered.Model.Character;
using TienKiemV2Remastered.Model.Clan;
using TienKiemV2Remastered.Model.Data;
using TienKiemV2Remastered.Model.Info;
using TienKiemV2Remastered.Model.Item;
using TienKiemV2Remastered.Model.Map;
using TienKiemV2Remastered.Model.Monster;
using TienKiemV2Remastered.Model.Option;
using TienKiemV2Remastered.Model.Template;
using TienKiemV2Remastered.Model.SkillCharacter;
using TienKiemV2Remastered.Application.Extension;
using TienKiemV2Remastered.Application.Extension.BlackballWar;
using TienKiemV2Remastered.Application.Extension.Ký_gửi;
using System.IO;
using System.Runtime.InteropServices;

namespace TienKiemV2Remastered.DatabaseManager
{
    public class InitData
    {
        public InitData()
        {
            ClearCache();
            InitDBData();
            InitCache();
            InitMapServer();
            InitDBAccount();
        }

        private void InitDBData()
        {
            DbContext.gI()?.ConnectToData();
            using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
            {
                if (command != null)
                {

                    Console.WriteLine("LOADING DATA FROM DATABASE [STATUS: LOADING] ");
                    //var i = 0;
                    //while(i >= 0 && i <= 1300)
                    //{
                    //    Cache.Gi().NR_PART2.Add(1);
                    //    Console.WriteLine(Cache.Gi().NR_PART2.Count);
                    //}
                    #region Read Table Task
                    command.CommandText = "SELECT * FROM `taskoption`";
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            if (!Cache.Gi().TASKS.ContainsKey(id))
                            {
                                Cache.Gi().TASKS.Add(id, JsonConvert.DeserializeObject<int[]>(reader.GetString(1)));
                                Cache.Gi().MAPTASKS.Add(id, JsonConvert.DeserializeObject<int[]>(reader.GetString(2)));
                            }
                        }
                    }

                    reader.Close();

                    #endregion
                    //#region Auto Boss Template
                    //command.CommandText = "SELECT * FROM `aboss`";
                    //reader = command.ExecuteReader();
                    //if (reader.HasRows)
                    //{
                    //    while (reader.Read())
                    //    {
                    //        int type = reader.GetInt32(0);
                    //        long timeSpawn = reader.GetInt64(1);
                    //        long delay = reader.GetInt64(2);
                    //        int[] Map= JsonConvert.DeserializeObject<int[]>(reader.GetString(3));
                    //        short[] x = JsonConvert.DeserializeObject<short[]>(reader.GetString(4));
                    //        short[] y = JsonConvert.DeserializeObject<short[]>(reader.GetString(5));
                    //        var timeServer = ServerUtils.CurrentTimeMillis();
                    //        Cache.Gi().AUTOBOSS_TEMPLATE.Add(new ABossTemplate()
                    //        {
                    //            Type = type,
                    //            Delay = delay,
                    //            TimeSpawn = timeSpawn + timeServer,
                    //            Map = Map,
                    //            X = x,
                    //            y = y,
                    //            Count = reader.GetInt32(6),
                    //        });
                    //    }
                    //}

                    //reader.Close();

                    //#endregion

                    #region Read Table Task Bo Mong Template
                    command.CommandText = "SELECT * FROM `Bò Mộng`";

                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            short id = reader.GetInt16(0);
                            string taskName = reader.GetString(1);
                            string taskDescription = reader.GetString(2);
                            int gemCollect = reader.GetInt32(3);
                            long CountTask = reader.GetInt64(4);
                            if (!Cache.Gi().TASK_BO_MONG.ContainsKey(id))
                            {
                                Cache.Gi().TASK_BO_MONG.Add(id, new Application.Extension.Bo_Mong.BoMong_Task_Template()
                                {
                                    Id = id,
                                    TaskName = taskName,
                                    TaskDescription = taskDescription,
                                    GemCollect = gemCollect,
                                    Count = CountTask,
                                    
                                });
                                //  Console.WriteLine("[< ! LOAD> BO MONG TEMP: " +id+ "]");
                            }
                        }
                        Server.Gi().Logger.Print("Load Bo Mong: " + Cache.Gi().TASK_BO_MONG.Count, "yellow");
                    }
                    reader.Close();
                    #endregion
                    #region Ki gui
                    command.CommandText = "SELECT * FROM `Shop Kí Gửi`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var Id = reader.GetInt16(3);
                            var IdPlayerSell = reader.GetInt32(1);
                            var Tab = (byte)reader.GetInt32(2);
                            var ItemId = reader.GetInt16(0);
                            var Cost = reader.GetInt32(5);
                            var BuyType = reader.GetInt32(4);
                            var quantity = reader.GetInt32(6);
                            var Options = JsonConvert.DeserializeObject<List<OptionItem>>(reader.GetString(7));
                            var IsUpTop = reader.GetBoolean(8);
                            var isBuy = reader.GetBoolean(9);

                            Cache.Gi().kyGUIItems.TryAdd(Id, new KyGUIItem()
                            {
                                Id = Id,
                                IdPlayerSell =  IdPlayerSell,
                                Tab = Tab,
                                ItemId = ItemId,
                                Cost = Cost,
                                BuyType = ( byte)BuyType,
                                quantity = quantity,
                                Options = Options,
                                IsUpTop = IsUpTop,
                                isBuy = isBuy,
                            });
                           
                        }
                    }
                    reader.Close();
                    #endregion
                    
                    #region Linh Thú
                    command.CommandText = "SELECT * FROM `Linh Thú`";

                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            short id = (short)reader.GetInt32(0);
                            short idImage = (short)reader.GetInt32(1);
                            int frame = reader.GetInt32(2);
                            int hImg = reader.GetInt32(3);
                            int xImg = reader.GetInt32(4);
                            if (!Cache.Gi().LinhThu.ContainsKey(id))
                            {
                                Cache.Gi().LinhThu.Add(id, new Application.Extension.Epic_Pet()
                                {
                                    Id = id,
                                    IdImage = idImage,
                                    Frame = frame,
                                    hImg = hImg,
                                    xImg = xImg,
                                });
                            }
                        }
                        Server.Gi().Logger.Print("Load Linh Thu: " + Cache.Gi().LinhThu.Count, "yellow");
                    }
                    reader.Close();
                    #endregion
                    #region Info Siêu Hạng
                    command.CommandText = "SELECT * FROM `siêu hạng`";

                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int Top = reader.GetInt32(0);
                            int Id = reader.GetInt32(1);
                            string Name = reader.GetString(2);
                            var Info = JsonConvert.DeserializeObject<List<int>>(reader.GetString(3));
                            var PPoint = JsonConvert.DeserializeObject<List<int>>(reader.GetString(5));
                            var Point = new Application.Extension.Super_Champion.SieuHang.Point(PPoint[0],PPoint[1],PPoint[2],PPoint[3]);
                            Application.Extension.Super_Champion.SieuHang.History history = new Application.Extension.Super_Champion.SieuHang.History();
                            history.Text = JsonConvert.DeserializeObject<string[][]>(reader.GetString(4));
                            if (!Cache.Gi().InfoRankSieuHang.ContainsKey(Top))
                            {
                                Cache.Gi().InfoRankSieuHang.Add(Top, new Application.Extension.Super_Champion.SieuHang.InfoRank()
                                {
                                    Top = Top,
                                    PlayerId = Id,
                                    PlayerName = Name,
                                    Head = (short)Info[0],
                                    Body = (short)Info[1],
                                    Leg = (short)Info[2],
                                    Gem = Info[4],
                                    History = history,
                                    Point = Point
                                });
                            }
                        }
                        Server.Gi().Logger.Print("Load Top Info Rank Sieu Hang: " + Cache.Gi().InfoRankSieuHang.Count, "yellow");
                    }
                    reader.Close();
                    #endregion
                    #region Cai Trang Template
                    command.CommandText = "SELECT * FROM `Cải Trang`";

                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            int head = reader.GetInt32(1);
                            int body = reader.GetInt32(2);
                            int leg = reader.GetInt32(3);
                            bool isAvatar = reader.GetBoolean(4);
                            if (!Cache.Gi().CaiTrangTemplate.ContainsKey(id))
                            {
                                Cache.Gi().CaiTrangTemplate.Add(id, new Application.Extension.CaiTrangTemplate()
                                {
                                    IdTemp = id,
                                    Head = head,
                                    Body = body,
                                    Leg = leg,
                                    isAvatar = isAvatar
                                });
                            }
                        }
                        Server.Gi().Logger.Print("Load Cai Trang: " + Cache.Gi().CaiTrangTemplate.Count, "yellow");
                    }
                    reader.Close();
                    #endregion
                    //#region DISCIPLE TEMPLATE
                    //command.CommandText = "SELECT * FROM `DiscipleTemplate`";

                    //reader = command.ExecuteReader();
                    //if (reader.HasRows)
                    //{
                    //    while (reader.Read())
                    //    {
                    //        var id = reader.GetInt16(0);
                    //        if (!Cache.gI().DISCIPLE_TEMPLATE.ContainsKey(id))
                    //        {
                    //            Cache.gI().DISCIPLE_TEMPLATE.Add(id, new Sources.Model.Template.DiscipleTemplate()
                    //            {
                    //                Id = id,
                    //                Type = reader.GetInt16(1),
                    //                Name = reader.GetString(2),
                    //                Power = reader.GetInt64(3),
                    //                Hp = reader.GetInt64(4),
                    //                Mp = reader.GetInt64(5),
                    //                Damage = reader.GetInt64(6),
                    //                Defend = reader.GetInt64(7),
                    //                Critical = reader.GetInt32(8),
                    //                Head = reader.GetInt32(9),
                    //                Body = reader.GetInt32(10),
                    //                Leg = reader.GetInt32(11),
                    //            });
                    //            //  Console.WriteLine("[< ! LOAD> BO MONG TEMP: " +id+ "]");
                    //        }


                    //    }
                    //}
                    //reader.Close();
                    //#endregion
                    #region Read Table Task Template

                    command.CommandText = "SELECT * FROM `tasktemplate`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            short id = reader.GetInt16(0);
                            string name = reader.GetString(1);
                            short gender = reader.GetInt16(2);
                            string detail = reader.GetString(3);
                            List<string> subNames = JsonConvert.DeserializeObject<List<string>>(reader.GetString(4));
                            List<short> counts = JsonConvert.DeserializeObject<List<short>>(reader.GetString(5));
                            List<string> contentInfo = JsonConvert.DeserializeObject<List<string>>(reader.GetString(6));
                            long[] Reward = JsonConvert.DeserializeObject<long[]>(reader.GetString(7));
                            List < ItemGiftcode > ItemReward = JsonConvert.DeserializeObject<List<ItemGiftcode>>(reader.GetString(8));

                            switch (gender)
                            {
                                case 0:
                                    {
                                        if (!Cache.Gi().TASK_TEMPLATES_0.ContainsKey(id))
                                        {
                                            Cache.Gi().TASK_TEMPLATES_0.Add(id, new TaskTemplate()
                                            {
                                                Id = id,
                                                Name = name,
                                                Detail = detail,
                                                SubNames = subNames,
                                                Counts = counts,
                                                ContentInfo = contentInfo,
                                                Reward = Reward,
                                                ItemReward = ItemReward,
                                               

                                            }) ;
                                           
                                        }
                                        break;
                                    }
                                case 1:
                                    {
                                        if (!Cache.Gi().TASK_TEMPLATES_1.ContainsKey(id))
                                        {
                                            Cache.Gi().TASK_TEMPLATES_1.Add(id, new TaskTemplate()
                                            {
                                                Id = id,
                                                Name = name,
                                                Detail = detail,
                                                SubNames = subNames,
                                                Counts = counts,
                                                ContentInfo = contentInfo,
                                                Reward = Reward,
                                                ItemReward = ItemReward,


                                            });
                                           
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        if (!Cache.Gi().TASK_TEMPLATES_2.ContainsKey(id))
                                        {
                                            Cache.Gi().TASK_TEMPLATES_2.Add(id, new TaskTemplate()
                                            {
                                                Id = id,
                                                Name = name,
                                                Detail = detail,
                                                SubNames = subNames,
                                                Counts = counts,
                                                ContentInfo = contentInfo,
                                                Reward = Reward,
                                                ItemReward = ItemReward,


                                            });
                                           
                                        }
                                        break;
                                    }
                            }

                        }
                    }

                    reader.Close();

                    #endregion

                    #region Read Table nr_arrow

                    command.CommandText = "SELECT * FROM `nr_arrow`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            Cache.Gi().NRArrows.Add(new Arrow()
                            {
                                Id = reader.GetInt16(0),
                                Data = JsonConvert.DeserializeObject<List<short>>(reader.GetString(1))
                            });
                        }

                    reader.Close();

                    #endregion

                    #region Read Table nr_dart

                    command.CommandText = "SELECT * FROM `nr_dart`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            Cache.Gi().NRDarts.AddRange(JsonConvert.DeserializeObject<List<Dart>>(reader.GetString(1)));
                            break;
                        }

                    reader.Close();

                    #endregion

                    #region Read Table nr_effect

                    command.CommandText = "SELECT * FROM `nr_effect`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            Cache.Gi().NREffects.Add(new Effect()
                            {
                                Id = reader.GetInt16(0),
                                Data = JsonConvert.DeserializeObject<short[][]>(reader.GetString(1))
                            });
                        }

                    reader.Close();

                    #endregion

                    #region Read Table nr_image

                    command.CommandText = "SELECT * FROM `nr_image`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            Cache.Gi().NRImages.Add(new Image()
                            {
                                Id = reader.GetInt16(0),
                                Data = JsonConvert.DeserializeObject<short[]>(reader.GetString(1))
                            });
                        }

                    reader.Close();

                    #endregion

                    #region Read Table nr_part

                    command.CommandText = "SELECT * FROM `nr_part`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            Cache.Gi().NRParts.Add(new Part()
                            {
                                Id = reader.GetInt16(0),
                                Type = reader.GetByte(1),
                                Data = JsonConvert.DeserializeObject<short[][]>(reader.GetString(2))
                            });
                        }
                    
                    Server.Gi().Logger.Print("ITEM PART là :" + Cache.Gi().NRParts.Count);

                    reader.Close();

                    #endregion

                    #region Read Table nr_skill

                    command.CommandText = "SELECT * FROM `nr_skill`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            Cache.Gi().NRSkills.Add(new Skill()
                            {
                                Id = reader.GetInt16(0),
                                EffectHappenOnMob = reader.GetInt16(1),
                                NumEff = Byte.Parse(reader.GetString(2)),
                                SkillStand = JsonConvert.DeserializeObject<short[][]>(reader.GetString(3)),
                                SkillFly = JsonConvert.DeserializeObject<short[][]>(reader.GetString(4))
                            });
                        }

                    reader.Close();

                    #endregion

                    #region Read Table exps

                    command.CommandText = "SELECT * FROM `exps`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            Cache.Gi().EXPS.Add(reader.GetInt64(1));
                        }

                    reader.Close();

                    #endregion

                    #region Read Table limitPower

                    command.CommandText = "SELECT * FROM `limitPower`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            var id = reader.GetInt32(0);
                            if (!Cache.Gi().LIMIT_POWERS.ContainsKey(id))
                            {
                                Cache.Gi().LIMIT_POWERS.TryAdd(id,
                                    JsonConvert.DeserializeObject<List<LimitPower>>(reader.GetString(1))!.ElementAt(0));
                            }
                        }

                    reader.Close();

                    #endregion

                    #region Read Table level

                    command.CommandText = "SELECT * FROM `level`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            Level level = new Level()
                            {
                                Id = reader.GetInt32(0),
                                Gender = Byte.Parse(reader.GetString(1)),
                                Name = reader.GetString(2),
                            };
                            Cache.Gi().LEVELS.Add(level);
                        }

                    reader.Close();

                    #endregion

                    #region Read Table datahead

                    command.CommandText = "SELECT * FROM `datahead`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            Cache.Gi().AVATAR.TryAdd(reader.GetInt32(0), reader.GetInt32(1));
                            
                        }
                    Server.Gi().Logger.Print("Load Data Head: " + Cache.Gi().AVATAR.Count, "yellow");
                    reader.Close();

                    #endregion

                    #region Read Table itembackground

                    command.CommandText = "SELECT * FROM `itembackground`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            Cache.Gi().BACKGROUND_ITEM_TEMPLATES.Add(new BackgroundItemTemplate()
                            {
                                Id = reader.GetInt32(0),
                                BackgroundId = reader.GetInt32(1),
                                Layer = reader.GetInt32(2),
                                X = reader.GetInt32(3),
                                Y = reader.GetInt32(4),
                            });
                        }

                    reader.Close();

                    #endregion

                    #region Read Table monster

                    command.CommandText = "SELECT * FROM `monster`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            MonsterTemplate monsterTemplate = new MonsterTemplate()
                            {
                                Id = reader.GetInt32(0),
                                Type = Byte.Parse(reader.GetString(1)),
                                Name = reader.GetString(2),
                                Hp = reader.GetInt32(3),
                                RangeMove = Byte.Parse(reader.GetString(4)),
                                Speed = Byte.Parse(reader.GetString(5)),
                                DartType = Byte.Parse(reader.GetString(6)),
                                LeaveItemType = reader.GetInt32(7)
                            };

                            Cache.Gi().MONSTER_TEMPLATES.Add(monsterTemplate);
                        }

                    reader.Close();

                    #endregion

                    #region Read Table Npc

                    command.CommandText = "SELECT * FROM `npc`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            Cache.Gi().NPC_TEMPLATES.Add(new NpcTemplate()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Head = reader.GetInt16(2),
                                Body = reader.GetInt16(3),
                                Leg = reader.GetInt16(4),
                                Menu = JsonConvert.DeserializeObject<string[][]>(reader.GetString(5))
                            });
                        }

                    reader.Close();

                    #endregion

                    #region Read Table boss

                    command.CommandText = "SELECT * FROM `boss`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            BossTemplate bossTemplate = new BossTemplate()
                            {
                                Id = reader.GetInt32(0),
                                
                                Type = reader.GetInt32(1),
                                Name = reader.GetString(2),
                                Hp = reader.GetInt32(3),
                                Mp = reader.GetInt32(4),
                                Stamina = (short)reader.GetInt32(5),
                                Skills = JsonConvert.DeserializeObject<List<SkillCharacter>>(reader.GetString(6),
                                    DataCache.SettingNull),
                                Damage = reader.GetInt32(7),
                                Defence = reader.GetInt32(8),
                                CritChance = reader.GetInt32(9),
                                Hair = (short)reader.GetInt32(10),
                                KhangTroi = reader.GetBoolean(11),
                           //     Count = reader.GetInt32(12),
                            };
                            //TopInfoBoss.ID.Add(bossTemplate.Id);
                            //TopInfoBoss.Head.Add(bossTemplate.Hair);
                            //TopInfoBoss.Name.Add(bossTemplate.Name);
                            Cache.Gi().BOSS_TEMPLATES.Add(bossTemplate);
                        }

                    reader.Close();

                    #endregion

                    #region Read Table Tile

                    command.CommandText = "SELECT * FROM `tile`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            Cache.Gi().TILE_TYPES = JsonConvert.DeserializeObject<int[][]>(reader.GetString(1));
                            Cache.Gi().TILE_INDEXS = JsonConvert.DeserializeObject<int[][][]>(reader.GetString(2));
                            break;
                        }

                    reader.Close();

                    #endregion

                    #region Read Table optionitem

                    command.CommandText = "SELECT * FROM `optionitem`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            Cache.Gi().ITEM_OPTION_TEMPLATES.Add(new ItemOptionTemplate()
                            {
                                Id = reader.GetInt32(0),
                                Type = Byte.Parse(reader.GetString(1)),
                                Name = reader.GetString(2)
                            });
                         //   Server.Gi().Logger.Print("Load option id: " + reader.GetInt32(0) + " name: " + reader.GetString(2), "yellow");
                        }

                    reader.Close(); ;

                    #endregion

                    #region Read Table item

                    command.CommandText = "SELECT * FROM `item`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            short id = reader.GetInt16(0);
                            List<OptionItem> list =
                                JsonConvert.DeserializeObject<List<OptionItem>>(reader.GetString(16));
                            if (list == null)
                            {
                                list = new List<OptionItem>()
                                {
                                    new OptionItem()
                                    {
                                        Id = 73,
                                        Param = 0
                                    }
                                };
                            }
                            else if (list.Count <= 0)
                                list.Add(new OptionItem()
                                {
                                    Id = 73,
                                    Param = 0
                                });

                            ItemTemplate itemTemplate = new ItemTemplate()
                            {
                                Id = id,
                                Type = Byte.Parse(reader.GetString(1)),
                                Skill = Byte.Parse(reader.GetString(2)),
                                Gender = Byte.Parse(reader.GetString(3)),
                                Name = reader.GetString(4),
                                SubName = reader.GetString(5),
                                Description = reader.GetString(6),
                                Level = Byte.Parse(reader.GetString(7)),
                                IconId = reader.GetInt16(8),
                                Part = reader.GetInt16(9),
                                IsUpToUp = reader.GetBoolean(10),
                                IsDrop = reader.GetBoolean(11),
                                Require = reader.GetInt64(12),
                                IsExpires = reader.GetBoolean(13),
                                SecondsExpires = reader.GetInt64(14),
                                SaleCoin = reader.GetInt32(15)
                            };
                            itemTemplate.Options.AddRange(list);
                            Cache.Gi().ITEM_TEMPLATES.TryAdd(id, itemTemplate);
                        }

                    reader.Close();

                    #endregion

                    #region Read Table Skill

                    command.CommandText = "SELECT * FROM `skilltemplate`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            SkillTemplate skillTemplate = new SkillTemplate()
                            {
                                Id = reader.GetInt32(0),
                                NClass = Byte.Parse(reader.GetString(1)),
                                Name = reader.GetString(2),
                                MaxPoint = Byte.Parse(reader.GetString(3)),
                                ManaUseType = Byte.Parse(reader.GetString(4)),
                                Type = Byte.Parse(reader.GetString(5)),
                                IconId = reader.GetInt16(6),
                                DamageInfo = reader.GetString(7),
                                Description = reader.GetString(8),
                                SkillDataTemplates =
                                    JsonConvert.DeserializeObject<List<SkillDataTemplate>>(reader.GetString(9))
                            };
                            Cache.Gi().SKILL_TEMPLATES.Add(skillTemplate);
                        }

                    reader.Close();

                    #endregion

                    #region Read Table Skill Option

                    command.CommandText = "SELECT * FROM `optionskill`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            Cache.Gi().SKILL_OPTIONS.Add(new SkillOption()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                            });
                        }

                    reader.Close();

                    #endregion

                    #region Read Table map

                    command.CommandText = "SELECT * FROM `map`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            var tileMap = new TileMap()
                            {
                                Id = reader.GetInt32(0),
                                Type = reader.GetInt32(1),
                                PlanetID = Byte.Parse(reader.GetString(2)),
                                TileID = reader.GetInt32(3),
                                BgID = reader.GetInt32(4),
                                BgType = reader.GetInt32(5),
                                IsMapDouble = reader.GetBoolean(6),
                                Name = reader.GetString(7),
                                Teleport = reader.GetInt32(8),
                                MaxPlayers = reader.GetInt32(9),
                                ZoneNumbers = reader.GetInt32(10),
                                toX = reader.GetInt16(11),
                                toY = reader.GetInt16(12)
                            };
                            var objects = JsonConvert.DeserializeObject<object[][]>(reader.GetString(13));
                            if (objects != null)
                                foreach (var wp in objects)
                                {
                                    tileMap.WayPoints.Add(new WayPoint()
                                    {
                                        MinX = short.Parse(wp[0].ToString() ?? "0"),
                                        MinY = short.Parse(wp[1].ToString() ?? "0"),
                                        MaxX = short.Parse(wp[2].ToString() ?? "0"),
                                        MaxY = short.Parse(wp[3].ToString() ?? "0"),
                                        IsEnter = byte.Parse(wp[4].ToString() ?? "0") != 0,
                                        IsOffline = byte.Parse(wp[5].ToString() ?? "0") != 0,
                                        Name = wp[6].ToString(),
                                        MapNextId = byte.Parse(wp[7].ToString() ?? "0"),
                                    });
                                }

                            objects = JsonConvert.DeserializeObject<object[][]>(reader.GetString(14));
                            if (objects != null)
                                foreach (var monster in objects)
                                {
                                    tileMap.MonsterMaps.Add(new MonsterMap()
                                    {
                                        Id = short.Parse(monster[0].ToString() ?? "0"),
                                        Level = int.Parse(monster[1].ToString() ?? "0"),
                                        X = short.Parse(monster[2].ToString() ?? "0"),
                                        Y = short.Parse(monster[3].ToString() ?? "0"),
                                        Status = 5,
                                        LvBoss = byte.Parse(monster[5].ToString() ?? "0"),
                                        IsBoss = byte.Parse(monster[6].ToString() ?? "0") != 0,
                                    });
                                }

                            objects = JsonConvert.DeserializeObject<object[][]>(reader.GetString(15));
                            if (objects != null)
                                foreach (var npc in objects)
                                {
                                    tileMap.Npcs.Add(new Npc()
                                    {
                                        Status = byte.Parse(npc[0].ToString() ?? "0"),
                                        X = short.Parse(npc[1].ToString() ?? "0"),
                                        Y = short.Parse(npc[2].ToString() ?? "0"),
                                        Id = short.Parse(npc[3].ToString() ?? "0"),
                                        Avatar = short.Parse(npc[4].ToString() ?? "0")
                                    });
                                }

                            objects = JsonConvert.DeserializeObject<object[][]>(reader.GetString(16));
                            if (objects != null)
                                foreach (var background in objects)
                                {
                                    tileMap.BackgroundItems.Add(new BackgroundItem()
                                    {
                                        Id = short.Parse(background[0].ToString() ?? "0"),
                                        X = short.Parse(background[1].ToString() ?? "0"),
                                        Y = short.Parse(background[2].ToString() ?? "0"),
                                    });
                                }

                            objects = JsonConvert.DeserializeObject<object[][]>(reader.GetString(17));
                            if (objects != null)
                                foreach (var action in objects)
                                {
                                    tileMap.Actions.Add(new ActionItem()
                                    {
                                        Key = action[0].ToString(),
                                        Value = action[1].ToString(),
                                    });
                                }

                            Cache.Gi().TILE_MAPS.Add(tileMap);
                        }

                    reader.Close();
                    #endregion

                    #region Read Table Store
                    command.CommandText = "SELECT * FROM `shop`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            var shopId = reader.GetInt16(3);
                            var shopTemplate = new ShopTemplate()
                            {
                                Id = reader.GetInt32(0),
                                Type = byte.Parse(reader.GetString(4)),
                                Name = reader.GetString(5),
                                ItemShops = JsonConvert.DeserializeObject<List<ItemShop>>(reader.GetString(6),
                                    DataCache.SettingNull)
                            };
                            if (!Cache.Gi().SHOP_TEMPLATES.ContainsKey(shopId))
                            {
                                Cache.Gi().SHOP_TEMPLATES.Add(shopId, new List<ShopTemplate>());
                            }

                            Cache.Gi().SHOP_TEMPLATES[shopId].Add(shopTemplate);
                        }
                    reader.Close();
                    #endregion

                    #region Read Data RADAR
                    command.CommandText = "SELECT * FROM `radar`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            var template = new RadarTemplate()
                            {
                                Id = reader.GetInt16(0),
                                IconId = reader.GetInt16(1),
                                Rank = reader.GetInt32(2),
                                Max = reader.GetInt32(3),
                                Type = reader.GetInt32(4),
                                Template = reader.GetInt32(5),
                                Data = JsonConvert.DeserializeObject<List<short>>(reader.GetString(6)),
                                Name = reader.GetString(7),
                                Info = reader.GetString(8),
                                Options = JsonConvert.DeserializeObject<List<OptionRadar>>(reader.GetString(9)),
                                Require = reader.GetInt16(10),
                                RequireLevel = reader.GetInt16(11),
                                AuraId = reader.GetInt16(12),
                            };
                            if (Cache.Gi().RADAR_TEMPLATE.FirstOrDefault(r => r.Id == template.Id) == null)
                            {
                                Cache.Gi().RADAR_TEMPLATE.Add(template);
                            }
                        }
                    reader.Close();
                    #endregion

                    #region Read Data MagicTree
                    command.CommandText = "SELECT * FROM `datamagictree`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            Cache.Gi().DATA_MAGICTREE.TryAdd(reader.GetInt16(0), JsonConvert.DeserializeObject<List<List<int>>>(reader.GetString(1)));
                        }
                    reader.Close();
                    #endregion

                    #region Read Table Clan Image

                    command.CommandText = "SELECT * FROM `clanimage`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            var clanImage = new ClanImage()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Gold = reader.GetInt32(2),
                                Diamond = reader.GetInt32(3),
                                Data = JsonConvert.DeserializeObject<List<short>>(reader.GetString(4)),
                            };
                            if (!Cache.Gi().CLAN_IMAGES.Contains(clanImage))
                            {
                                Cache.Gi().CLAN_IMAGES.Add(clanImage);
                            }
                        }

                    reader.Close();

                    #endregion

                    #region Read Table Special Skill
                    command.CommandText = "SELECT * FROM `special_skill`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            var specialSkillId = reader.GetInt32(1);
                            var skillTemplate = new SpecialSkillTemplate()
                            {
                                Id = reader.GetInt32(0),
                                Gender = (short)specialSkillId,
                                Info = reader.GetString(2),
                                InfoFormat = reader.GetString(3),
                                Img = (short)reader.GetInt32(4),
                                SkillId = (short)reader.GetInt32(5),
                                Min = (short)reader.GetInt32(6),
                                Max = (short)reader.GetInt32(7),
                                Vip = reader.GetInt32(8),
                            };

                            if (!Cache.Gi().SPECIAL_SKILL_TEMPLATES.ContainsKey(specialSkillId))
                            {
                                Cache.Gi().SPECIAL_SKILL_TEMPLATES.Add(specialSkillId, new List<SpecialSkillTemplate>());
                            }

                            Cache.Gi().SPECIAL_SKILL_TEMPLATES[specialSkillId].Add(skillTemplate);
                        }
                    reader.Close();
                    #endregion

                }

                #region Create NClass Data
                for (var i = 0; i < 5; i++)
                {
                    var nClass = new NClass();
                    nClass.Id = i;
                    nClass.SkillTemplates = Cache.Gi().SKILL_TEMPLATES.Where(x => i < 4 && (x.NClass == i || x.NClass == 3)).ToList();
                    nClass.Name = i switch
                    {
                        0 => "Trái Đất",
                        1 => "Namek",
                        2 => "Saiyan",
                        3 => "Chung",
                        4 => "Khỉ đột",
                        _ => nClass.Name
                    };
                    Cache.Gi().NClasses.Add(nClass);
                }
                #endregion
                
             //   Extension.gI().Logger.Print("LOADING DATA FROM DATABASE [STATUS: SUCCESSS]");
            } 
            DbContext.gI().CloseConnect();
        }

        private void InitDBAccount()
        {
            DbContext.gI()?.ConnectToAccount();
            using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
            {


                if (command != null)
                {
                    #region Read Game info
                    command.CommandText = "SELECT * FROM `gameinfo`";
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        Cache.Gi().GAME_INFO_TEMPLATES.Clear();
                        while (reader.Read())
                        {
                            Cache.Gi().GAME_INFO_TEMPLATES.Add(new GameInfoTemplate()
                            {
                                Id = reader.GetInt32(0),
                                Main = reader.GetString(1),
                                Content = reader.GetString(2)
                            });
                        }
                    }

                    reader.Close();

                    #endregion

                    #region Read magictree

                    command.CommandText = "SELECT * FROM `magictree`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        MagicTreeManager.Entrys.Clear();
                        while (reader.Read())
                        {
                            var id = reader.GetInt32(0);
                            MagicTreeManager.Add(new MagicTree()
                            {
                                Id = id,
                                NpcId = reader.GetInt16(1),
                                X = reader.GetInt16(2),
                                Y = reader.GetInt16(3),
                                Level = reader.GetInt32(4),
                                Peas = reader.GetInt32(5),
                                MaxPea = reader.GetInt32(6),
                                Seconds = reader.GetInt64(7),
                                IsUpdate = byte.Parse(reader.GetString(8)) == 1 ? true : false,
                                Diamond = reader.GetInt32(9),
                            });
                        }
                    }
                    reader.Close();
                    #endregion

                    #region Read Clan

                    command.CommandText = "SELECT * FROM `clan`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        ClanManager.Entrys.Clear();
                        while (reader.Read())
                        {
                            ClanManager.Add(new Clan()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Khẩu_hiệu = reader.GetString(2),
                                ImgId = reader.GetInt32(3),
                                Điểm_thành_tích = reader.GetInt64(4),
                                LeaderName = reader.GetString(5),
                                Thành_viên_hiện_tại = reader.GetInt32(6),
                                Tối_đa_thành_viên = reader.GetInt32(7),
                                Thời_gian_tạo_bang = reader.GetInt32(8),
                                Cấp_Độ = reader.GetInt32(9),
                                Capsule_Bang = reader.GetInt32(10),
                                Thành_viên = JsonConvert.DeserializeObject<List<ClanMember>>(reader.GetString(11)),
                                Messages = JsonConvert.DeserializeObject<List<ClanMessage>>(reader.GetString(12)),
                                CharacterPeas = JsonConvert.DeserializeObject<List<CharacterPea>>(reader.GetString(13)),
                                DataBlackBall = JsonConvert.DeserializeObject<BlackBallHandler.ForClan.ClanManagerr>(reader.GetString(14)),
                                Leader = JsonConvert.DeserializeObject<ClanLeader>(reader.GetString(15)),
                                ClanBox = JsonConvert.DeserializeObject<List<Item>>(reader.GetString(16)) ?? new List<Item>(),
                                Gas = JsonConvert.DeserializeObject<Gas>(reader.GetString(18)),
                                TimeClanCreate = reader.GetDateTime(19),
                                shortName = reader.GetString(20),
                            });

                        }
                    }

                    reader.Close();
                    #endregion

                    #region Read regex chat
                    command.CommandText = "SELECT * FROM `regexchat`";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        Cache.Gi().RegexText.Clear();
                        while (reader.Read())
                        {
                            var text = reader.GetString(1);
                            text = text.ToLower();
                            if (text != "" && !Cache.Gi().RegexText.Contains(text))
                            {
                                Cache.Gi().RegexText.Add(text);
                            }
                        }
                    }
                    reader.Close();
                    #endregion
                }

            }
            DbContext.gI()?.CloseConnect();
            Server.Gi().Logger.Print("[LOAD DBACCOUNT SUCCESS]");
        }

        private void ClearCache()
        {
            Cache.ClearCache();
            Server.Gi().Logger.Print("[CLEAR CACHE SUCCESS]");
           
        }

        private void InitCache()
        {
            #region Load Map From Resource
            Cache.Gi().TILE_MAPS.ForEach(tile => tile.LoadMapFromResource());
            #endregion
            SetupPart();

            Cache.Gi().NR_DART.AddRange(InitCacheDart());
            Cache.Gi().NR_ARROW.AddRange(InitCacheArrow());
            Cache.Gi().NR_IMAGE.AddRange(InitCacheImage());
            Cache.Gi().NR_EFFECT.AddRange(InitCacheEffect());
            Cache.Gi().NR_PART.AddRange(InitCachePart());
            Cache.Gi().NR_SKILL.AddRange(InitCacheSkill());
            Cache.Gi().NRMAP.AddRange(InitCacheNrmap());
            Cache.Gi().NRSKILL.AddRange(InitCacheNRSKILL());
            Cache.Gi().DataItemTemplateOld.AddRange(SetupItemTemplateOld());
            Cache.Gi().DataItemTemplateNew.AddRange(SetupItemTemplateNew());
            //Cache.gI().VersionIconX1.AddRange(SetupVersionIconX1()); 
            //Cache.gI().VersionIconX2.AddRange(SetupVersionIconX2()); 
            //Cache.gI().VersionIconX3.AddRange(SetupVersionIconX3()); 
            //Cache.gI().VersionIconX4.AddRange(SetupVersionIconX4());
            Cache.Gi().VersionIcon.AddRange(SetupVersionIcon());
        
            Cache.Gi().NRDarts.Clear();
            Cache.Gi().NRArrows.Clear();
            Cache.Gi().NREffects.Clear();
            Cache.Gi().NRImages.Clear();
            Cache.Gi().NRParts.Clear();
            Cache.Gi().NRSkills.Clear();
            Server.Gi().Logger.Print("[INIT CACHE SUCCESS]");
        }

        private void InitMapServer()
        {
            MapManager.InitMapServer();
            Server.Gi().Logger.Print("[LOAD MAP SUCCESS]");
            Server.Gi().Logger.Print("[LOAD EFFECT MAP SUCCESS]");
            Server.Gi().Logger.Print("[INIT OTHER MAP SUCCESS]");

        }

        private static IEnumerable<sbyte> InitCacheDart()
        {
            List<sbyte> data;
            var writer = new MyWriter();
            writer.WriteShort(Cache.Gi().NRDarts.Count);
            Cache.Gi().NRDarts.ForEach(dart =>
            {
                writer.WriteShort(dart.Id);
                writer.WriteShort(dart.NUpdate);
                writer.WriteShort(dart.Va);
                writer.WriteShort(dart.XdPercent);

                writer.WriteShort(dart.Tail.Length);
                foreach (var t in dart.Tail)
                    writer.WriteShort(t);

                writer.WriteShort(dart.TailBorder.Length);
                foreach (var t in dart.TailBorder)
                    writer.WriteShort(t);

                writer.WriteShort(dart.Xd1.Length);
                foreach (var t in dart.Xd1)
                    writer.WriteShort(t);

                writer.WriteShort(dart.Xd2.Length);
                foreach (var t in dart.Xd2)
                    writer.WriteShort(t);

                writer.WriteShort(dart.Head.Length);
                foreach (var h in dart.Head)
                {
                    writer.WriteShort(h.Length);
                    foreach (var s in h)
                        writer.WriteShort((short)s);
                }

                writer.WriteShort(dart.HeadBorder.Length);
                foreach (var h in dart.HeadBorder)
                {
                    writer.WriteShort(h.Length);
                    foreach (var s in h)
                        writer.WriteShort(s);
                }
            });
            data = writer.GetData().ToList();
            writer.Close();
            return data;
        }
        private static IEnumerable<sbyte> InitCacheArrow()
        {
            List<sbyte> data;
            var writer = new MyWriter();
            writer.WriteShort(Cache.Gi().NRArrows.Count);
            Cache.Gi().NRArrows.ForEach(arrow =>
            {
                writer.WriteShort(arrow.Id);
                arrow.Data.ForEach(d => writer.WriteShort(d));
            });
            data = writer.GetData().ToList();
            writer.Close();
            return data;
        }

        private static IEnumerable<sbyte> InitCacheEffect()
        {
            List<sbyte> data;
            var writer = new MyWriter();
            writer.WriteShort(Cache.Gi().NREffects.Count);
            Cache.Gi().NREffects.ForEach(eff =>
            {
                writer.WriteShort(eff.Id);
                writer.WriteByte(eff.Data.Length);
                foreach (var effData in eff.Data)
                {
                    writer.WriteShort(effData[0]);
                    writer.WriteByte(effData[1]);
                    writer.WriteByte(effData[2]);
                }
            });
            data = writer.GetData().ToList();
            writer.Close();
            return data;
        }

        private static IEnumerable<sbyte> InitCacheImage()
        {
            List<sbyte> data;
            var writer = new MyWriter();
            writer.WriteShort(Cache.Gi().NRImages.Count);
            Cache.Gi().NRImages.ForEach(img =>
            {
                writer.WriteByte(img.Data[0]);
                writer.WriteShort(img.Data[1]);
                writer.WriteShort(img.Data[2]);
                writer.WriteShort(img.Data[3]);
                writer.WriteShort(img.Data[4]);
            });
            data = writer.GetData().ToList();
            writer.Close();
            return data;
        }

        private static IEnumerable<sbyte> InitCachePart()
        {
            List<sbyte> data;
            var writer = new MyWriter();
            writer.WriteShort(Cache.Gi().NRParts.Count);
            Cache.Gi().NRParts.ForEach(p =>
            {
                writer.WriteByte(p.Type);
                foreach (var part in p.Data)
                {
                    writer.WriteShort(part[0]);
                    writer.WriteByte(part[1]);
                    writer.WriteByte(part[2]);
                }
            });
            data = writer.GetData().ToList();
            writer.Close();
            return data;
        }

        private static IEnumerable<sbyte> InitCacheSkill()
        {
            List<sbyte> data;
            var writer = new MyWriter();
            writer.WriteShort(Cache.Gi().NRSkills.Count);
            Cache.Gi().NRSkills.ForEach(skill =>
            {
                writer.WriteShort(skill.Id);
                writer.WriteShort(skill.EffectHappenOnMob);
                writer.WriteByte(skill.NumEff);
                writer.WriteByte(skill.SkillStand.Length);
                foreach (var stand in skill.SkillStand)
                {
                    writer.WriteByte(stand[0]);
                    writer.WriteShort(stand[1]);
                    writer.WriteShort(stand[2]);
                    writer.WriteShort(stand[3]);
                    writer.WriteShort(stand[4]);
                    writer.WriteShort(stand[5]);
                    writer.WriteShort(stand[6]);
                    writer.WriteShort(stand[7]);
                    writer.WriteShort(stand[8]);
                    writer.WriteShort(stand[9]);
                    writer.WriteShort(stand[10]);
                    writer.WriteShort(stand[11]);
                    writer.WriteShort(stand[12]);
                }

                writer.WriteByte(skill.SkillFly.Length);
                foreach (var fly in skill.SkillFly)
                {
                    writer.WriteByte(fly[0]);
                    writer.WriteShort(fly[1]);
                    writer.WriteShort(fly[2]);
                    writer.WriteShort(fly[3]);
                    writer.WriteShort(fly[4]);
                    writer.WriteShort(fly[5]);
                    writer.WriteShort(fly[6]);
                    writer.WriteShort(fly[7]);
                    writer.WriteShort(fly[8]);
                    writer.WriteShort(fly[9]);
                    writer.WriteShort(fly[10]);
                    writer.WriteShort(fly[11]);
                    writer.WriteShort(fly[12]);
                }
            });
            data = writer.GetData().ToList();
            writer.Close();
            return data;
        }
        private static IEnumerable<sbyte> InitCacheNrmap()
        {
            List<sbyte> data;
            var writer = new MyWriter();
            writer.WriteByte(ConfigManager.gI().MapVersion);
            writer.WriteByte(Cache.Gi().TILE_MAPS.Count);
            Cache.Gi().TILE_MAPS.ForEach(tileMap =>
            {
                writer.WriteUTF(tileMap.Name);
            });

            writer.WriteByte(Cache.Gi().NPC_TEMPLATES.Count);
            Cache.Gi().NPC_TEMPLATES.ForEach(npcTemplate =>
            {
                writer.WriteUTF(npcTemplate.Name);
                writer.WriteShort(npcTemplate.Head);
                writer.WriteShort(npcTemplate.Body);
                writer.WriteShort(npcTemplate.Leg);
                writer.WriteByte(npcTemplate.Menu.Length);
                foreach (var menus in npcTemplate.Menu)
                {
                    writer.WriteByte(menus.Length);
                    foreach (var menu in menus)
                    {
                        writer.WriteUTF(menu);
                    }
                }
            });

            writer.WriteByte(Cache.Gi().MONSTER_TEMPLATES.Count);
            Cache.Gi().MONSTER_TEMPLATES.ForEach(monsterTemplate =>
            {
                writer.WriteByte(monsterTemplate.Type);
                writer.WriteUTF(monsterTemplate.Name);
                writer.WriteInt(monsterTemplate.Hp);
                writer.WriteByte(monsterTemplate.RangeMove);
                writer.WriteByte(monsterTemplate.Speed);
                writer.WriteByte(monsterTemplate.DartType);
            });
            data = writer.GetData().ToList();
            writer.Close();
            return data;
        }

        private IEnumerable<sbyte> InitCacheNRSKILL()
        {
            List<sbyte> data;
            var writer = new MyWriter();
            writer.WriteByte(ConfigManager.gI().SkillVersion);
            writer.WriteByte(Cache.Gi().SKILL_OPTIONS.Count);
            Cache.Gi().SKILL_OPTIONS.ForEach(option =>
            {
                writer.WriteUTF(option.Name);
            });
            writer.WriteByte(Cache.Gi().NClasses.Count);
            Cache.Gi().NClasses.ForEach(n =>
            {
                writer.WriteUTF(n.Name);
                writer.WriteByte(n.SkillTemplates.Count);
                //Server.Gi().Logger.Print("WritingSkill: " + n.Name + " | " + n.SkillTemplates.Count, "yellow");
                n.SkillTemplates.ForEach(s =>
                {
                    writer.WriteByte(s.Id);
                    writer.WriteUTF(s.Name);
                    writer.WriteByte(s.MaxPoint);
                    writer.WriteByte(s.ManaUseType);
                    writer.WriteByte(s.Type);
                    writer.WriteShort(s.IconId);
                    writer.WriteUTF(s.DamageInfo);
                    writer.WriteUTF(s.Description);
                    writer.WriteByte(s.SkillDataTemplates.Count);
                    s.SkillDataTemplates.ForEach(d =>
                    {
                        writer.WriteShort(d.SkillId);
                        writer.WriteByte(d.Point);
                        writer.WriteLong(d.PowRequire);
                        writer.WriteShort(d.ManaUse);
                        writer.WriteInt(d.CoolDown);
                        writer.WriteShort(d.Dx);
                        writer.WriteShort(d.Dy);
                        writer.WriteByte(d.MaxFight);
                        writer.WriteShort(d.Damage);
                        writer.WriteShort(d.Price);
                        writer.WriteUTF(d.MoreInfo);
                    });
                    //Console.WriteLine("Load " + s.Name + " SUCCESS");
                });
                //Server.Gi().Logger.Print("WriteSkill: " + n.Name + " | SUCCESS\n", "red");
            });
            data = writer.GetData().ToList();
            writer.Close();
            return data;
        }

        public void SetupPart()
        {
            Cache.Gi().NRParts.Where(part => part.Type == 0).ToList().ForEach(p =>
            {
                Cache.Gi().PARTS.TryAdd(p.Data[0][0], p.Id);
                Cache.Gi().PARTS.TryAdd(p.Data[1][0], p.Id);
                Cache.Gi().PARTS.TryAdd(p.Data[2][0], p.Id);
            });
        }

        private static IEnumerable<sbyte> SetupItemTemplateOld()
        {
            List<sbyte> data;
            var writer = new MyWriter();
            writer.WriteShort(ConfigManager.gI().ItemOld);
            for (var i = 0; i < ConfigManager.gI().ItemOld; i++)
            {
                var itemTemplate = Cache.Gi().ITEM_TEMPLATES[(short)i];
                writer.WriteByte(itemTemplate.Type);
                writer.WriteByte(itemTemplate.Gender);
                writer.WriteUTF(itemTemplate.Name);
                writer.WriteUTF(itemTemplate.Description);
                writer.WriteByte(itemTemplate.Level);
                writer.WriteInt((int)itemTemplate.Require);
                writer.WriteShort(itemTemplate.IconId);
                writer.WriteShort(itemTemplate.Part);
                writer.WriteBoolean(itemTemplate.IsUpToUp);
            }
            data = writer.GetData().ToList();
            writer.Close();
            return data;
        }

        private static IEnumerable<sbyte> SetupItemTemplateNew()
        {
            List<sbyte> data;
            var writer = new MyWriter();
            if (Cache.Gi().ITEM_TEMPLATES.Count - ConfigManager.gI().ItemOld == 0)
            {
                writer.WriteShort(0);
                writer.WriteShort(0);
            }
            else
            {
                writer.WriteShort(ConfigManager.gI().ItemOld);
                writer.WriteShort(Cache.Gi().ITEM_TEMPLATES.Count);
                for (var i = ConfigManager.gI().ItemOld; i < Cache.Gi().ITEM_TEMPLATES.Count; i++)
                {
                    var itemTemplate = Cache.Gi().ITEM_TEMPLATES[i];
                    writer.WriteByte(itemTemplate.Type);
                    writer.WriteByte(itemTemplate.Gender);
                    writer.WriteUTF(itemTemplate.Name);
                    writer.WriteUTF(itemTemplate.Description);
                    writer.WriteByte(itemTemplate.Level);
                    writer.WriteInt((int)itemTemplate.Require);
                    writer.WriteShort(itemTemplate.IconId);
                    writer.WriteShort(itemTemplate.Part);
                    writer.WriteBoolean(itemTemplate.IsUpToUp);
                }
            }
            data = writer.GetData().ToList();
            writer.Close();
            return data;
        }

        private static void AddDataMonster(int zoomlevel)
        {
            FileInfo[] fileInfos = new DirectoryInfo(ServerUtils.ProjectDir("RES/monster/x" + zoomlevel)).GetFiles();
            Span<FileInfo> listAsSpan = CollectionsMarshal.AsSpan(fileInfos.ToList());
            for (int i = 0; i < listAsSpan.Length; i++)
            {
                var name = listAsSpan[i].Name;
                var fname = listAsSpan[i].FullName;
                var bytes = File.ReadAllBytes(fname);
                //if (!int.TryParse(name.Replace(".png", ""), out _))
                //{
                //    Server.Gi().Logger.Print("deo load: " + fname);
                //    continue;
                //}
                var id = int.Parse(name.Split("_")[1]);
                switch (zoomlevel)
                {
                    case 1:
                        Cache.Gi().DATA_MONSTERS_X1.TryAdd(id, bytes.ToList());
                        break;
                    case 2:
                        Cache.Gi().DATA_MONSTERS_x2.TryAdd(id, bytes.ToList());
                        break;
                    case 3:
                        Cache.Gi().DATA_MONSTERS_x3.TryAdd(id, bytes.ToList());
                        break;
                    default:
                        Cache.Gi().DATA_MONSTERS_x4.TryAdd(id, bytes.ToList());
                        break;
                }
            }

            Server.Gi().Logger.Print("SETUP DATA MONSTER X" + zoomlevel + ": [STATUS: SUCCESS]", "cyan");

        }
        private static void AddDataIcon(int zoomlevel)
        {
            FileInfo[] fileInfos = new DirectoryInfo(ServerUtils.ProjectDir("RES/icon/x" + zoomlevel)).GetFiles();
            Span<FileInfo> listAsSpan = CollectionsMarshal.AsSpan(fileInfos.ToList());
           
            for (int i = 0; i < listAsSpan.Length; i++) {
                var name = listAsSpan[i].Name;
                var fname = listAsSpan[i].FullName;
                var bytes = File.ReadAllBytes(fname);
                if (!int.TryParse(name.Replace(".png", ""), out _))
                {
                    Server.Gi().Logger.Print("deo load: " + fname);
                    continue;
                }
                var icon = int.Parse(name.Replace(".png", ""));
                switch (zoomlevel)
                {
                    case 1:
                        Cache.Gi().DATA_ICON_X1.TryAdd(icon, bytes.ToList());
                        break;
                    case 2:
                        Cache.Gi().DATA_ICON_X2.TryAdd(icon, bytes.ToList());
                        break;
                    case 3:
                        Cache.Gi().DATA_ICON_X3.TryAdd(icon, bytes.ToList());
                        break;
                    default:
                        Cache.Gi().DATA_ICON_X4.TryAdd(icon, bytes.ToList());
                        break;
                }
            }
            
            Server.Gi().Logger.Print("SETUP DATA ICON X"+zoomlevel+": [STATUS: SUCCESS]", "cyan");

        }
        private static IEnumerable<sbyte> SetupVersionIcon()
        {
            List<sbyte> data;
            var writer = new MyWriter();
            var iconMax = 32000;
            Server.Gi().Logger.Print("SETUP ICON: [STATUS: LOADING]", "cyan");
            for (var i = 0; i < iconMax; i++)
            {
               
                writer.WriteByte(0);
            }
            AddDataIcon(1);
            AddDataIcon(2);
            AddDataIcon(3);
            AddDataIcon(4);
            AddDataMonster(1);
            AddDataMonster(2);
            AddDataMonster(3);
            AddDataMonster(4);
            data = writer.GetData().ToList();
            writer.Close();
            Server.Gi().Logger.Print("SETUP ICON: [STATUS: SUCCESS]: " + iconMax, "cyan");
            return data;
        }
    }
}