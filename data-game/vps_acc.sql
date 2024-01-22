/*
 Navicat Premium Data Transfer

 Source Server         : localhost_3306
 Source Server Type    : MySQL
 Source Server Version : 100428
 Source Host           : localhost:3306
 Source Schema         : vps_acc

 Target Server Type    : MySQL
 Target Server Version : 100428
 File Encoding         : 65001

 Date: 22/01/2024 15:49:28
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for bossriviu
-- ----------------------------
DROP TABLE IF EXISTS `bossriviu`;
CREATE TABLE `bossriviu`  (
  `ID` int NOT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Hp` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Time` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Idmap` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Timedie` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Itemvp` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of bossriviu
-- ----------------------------
INSERT INTO `bossriviu` VALUES (1, 'Super Broly', '20000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (2, 'Black Goku', '1000000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (3, 'Super Black Goku', '2000000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (4, 'Fide Đại Ca 1', '11000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (5, 'Fide Đại Ca 2', '13000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (6, 'Fide Đại Ca 3', '17000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (7, 'Xên Bọ Hung Cấp 1', '30000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (8, 'Xên Bọ Hung Cấp 2', '60000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (9, 'Xên Hoàn Thiện', '100000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (10, 'Cooler 1', '2000000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (11, 'Cooler 2', '2000000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (12, 'Thỏ Phê Cỏ', '250000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (13, 'Thỏ Đại Ca', '100000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (14, 'Chilled', '250000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (15, 'Chilled 2', '250000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (16, 'Số 4', '30000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (17, 'Số 3', '60000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (18, 'Số 1', '70000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (19, 'Tiểu Đội Trưởng', '100000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (20, 'Tàu 77', '1000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (21, 'Cell Max', '2000000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (23, 'Dr.lyChee', '11000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (24, 'Kuku', '5000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (25, 'Mập Đầu Đinh', '8000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (26, 'Rambo', '11000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (27, 'Píc', '250000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (28, 'Póc', '350000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (29, 'KingKong', '375000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (30, 'Android 19', '375000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (31, 'Dr.Kore', '375000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (32, 'Android 13', '375000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (33, 'Android 14', '325000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (34, 'Android 15', '425000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (35, 'Chilled 2', '1475000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (36, 'Drabula', '1500000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (37, 'Puipui', '1500000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (38, 'Yacon', '1500000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (39, 'Mabu', '1500000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (40, 'Ông Già Noel', '1000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (41, 'Broly', '1200', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (42, 'Xên con', '5000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (43, 'Mabư mập', '500000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (44, 'Super Bư', '1000000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (45, 'Bư Tênk', '1500000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (46, 'Gohan Bư', '2000000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (47, 'Trung úy Trắng', '100000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (48, 'Trung úy Xanh Lơ', '1500000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (49, 'Trung úy Thép', '2000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (50, 'Ninja Áo Tím', '2500000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (51, 'Robot Vệ sĩ', '3000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (52, 'Sói hẹc quyn', '10000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (53, 'Ở dơ', '25000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (54, 'Xinbatô', '50000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (55, 'Cha pa', '100000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (56, 'Pon pui', '250000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (57, 'Chan xư', '500000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (58, 'Tàu Pảy Pảy', '2000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (59, 'Yamcha', '5000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (60, 'Jacky Chun', '25000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (61, 'Thiên xin hăng', '75000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (62, 'Liu liu', '150000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (63, 'Siêu bọ hung', '2000000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (64, 'Tập sự', '350000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (65, 'Clone', '1000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (67, 'Hachijack', '11000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (68, 'Dracula', '10000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (69, 'Người vô hình', '10000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (70, 'Bông băng', '1', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (71, 'Vua quỷ sa tăng', '1', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (72, 'Thỏ đầu bạc', '1', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (73, 'Saibamen 1', '1100000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (74, 'Saibamen 2', '1100000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (75, 'Saibamen 3', '1100000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (76, 'Saibamen 4', '1100000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (77, 'Saibamen 5', '1100000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (78, 'Nappa', '9800000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (79, 'Cadic', '11000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (80, 'Tân binh', '450000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (81, 'Chiến binh', '500000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (82, 'Trọng Tài', '500', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (83, 'Thủy Tinh', '200000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (84, 'Sơn Tinh', '200000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (85, 'Tàu Pảy Pảy', '1100', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (86, 'Tàu Pảy Pảy', '10000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (87, 'Thần mèo Karin', '500', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (88, 'Yajiro', '1100', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (89, 'Thượng Đế', '10000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (90, 'Mr.PôPô', '5100', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (91, 'Khỉ Bubbles', '30000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (92, 'Thần vũ trụ', '45000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (93, 'Số 2', '90000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (94, 'Tàu Pảy Pảy', '10000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (95, 'Fide Đại Ca', '30000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (96, 'Siêu bọ hung', '60000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (97, 'Ma bư', '250000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (98, 'Kingkong Hè', '200000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (99, 'Pic hè', '200000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (100, 'Basil', '200000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (101, 'Lavender', '200000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (102, 'Bergamo', '200000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (103, 'Bướm đêm', '1', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (104, 'Bọ cánh cứng', '1000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (105, 'Cumber', '1000000000', NULL, NULL, NULL, NULL);
INSERT INTO `bossriviu` VALUES (106, 'Siêu Cumber', '2000000000', NULL, NULL, NULL, NULL);

-- ----------------------------
-- Table structure for character
-- ----------------------------
DROP TABLE IF EXISTS `character`;
CREATE TABLE `character`  (
  `id` int UNSIGNED NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT '',
  `Skills` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `ItemBody` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `ItemBag` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `ItemBox` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `InfoChar` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `BoughtSkill` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `InfoTask` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `PlusBag` int NULL DEFAULT 0,
  `PlusBox` int NULL DEFAULT 0,
  `Friends` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `Enemies` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `Me` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT '[]',
  `ClanId` int NULL DEFAULT -1,
  `LuckyBox` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `LastLogin` datetime(0) NULL DEFAULT '2022-03-05 18:25:21',
  `CreateDate` datetime(0) NULL DEFAULT '2022-03-05 18:25:21',
  `SpecialSkill` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `InfoBuff` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `DataEvent` int NOT NULL DEFAULT 0,
  `DataMinigame` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `DataBlackBall` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `DataBoMong` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `DataDaiHoiVoThuat` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `DataTraining` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `DataSieuHang` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `DiemSuKien` int NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 10010 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of character
-- ----------------------------
INSERT INTO `character` VALUES (10001, 'admin', '[{\"Id\":0,\"SkillId\":0,\"CoolDown\":1705781998266,\"Point\":1,\"CurrExp\":0}]', '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":0,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":47,\"Param\":2}]},{\"IndexUI\":1,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":6,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":6,\"Param\":30}]},null,null,null,null,null,null,null,{\"IndexUI\":9,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":943,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":197,\"Param\":0},{\"Id\":207,\"Param\":100},{\"Id\":50,\"Param\":10},{\"Id\":77,\"Param\":10},{\"Id\":103,\"Param\":10},{\"Id\":14,\"Param\":5}]},null,null]', '[{\"IndexUI\":0,\"SaleCoin\":1,\"BuyPotential\":0,\"Id\":1019,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":50,\"Param\":25},{\"Id\":77,\"Param\":25},{\"Id\":103,\"Param\":25},{\"Id\":94,\"Param\":17},{\"Id\":108,\"Param\":15},{\"Id\":148,\"Param\":20}]},{\"IndexUI\":1,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":860,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":50,\"Param\":25},{\"Id\":117,\"Param\":20},{\"Id\":114,\"Param\":20},{\"Id\":77,\"Param\":25},{\"Id\":30,\"Param\":0}]},{\"IndexUI\":2,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":1018,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":50,\"Param\":25},{\"Id\":77,\"Param\":25},{\"Id\":103,\"Param\":25},{\"Id\":94,\"Param\":17},{\"Id\":108,\"Param\":15},{\"Id\":148,\"Param\":20}]},{\"IndexUI\":3,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":1041,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":50,\"Param\":25},{\"Id\":77,\"Param\":23},{\"Id\":103,\"Param\":22},{\"Id\":94,\"Param\":18},{\"Id\":108,\"Param\":12},{\"Id\":148,\"Param\":20},{\"Id\":196,\"Param\":3}]},{\"IndexUI\":4,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":611,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":73,\"Param\":0}]}]', '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":12,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":14,\"Param\":1}]},{\"IndexUI\":1,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":523,\"Vang\":0,\"Ngoc\":0,\"Quantity\":11,\"Reason\":\"\",\"Options\":[{\"Id\":2,\"Param\":128}]}]', '{\"NClass\":0,\"Gender\":0,\"MapId\":21,\"MapCustomId\":-1,\"ZoneId\":0,\"Hair\":30,\"Bag\":26,\"Level\":8,\"Speed\":6,\"Pk\":0,\"TypePk\":0,\"Potential\":4966240,\"TotalPotential\":0,\"Power\":4967440,\"IsDie\":false,\"IsPower\":true,\"LitmitPower\":0,\"KSkill\":[-1,-1,-1,-1,-1,-1,-1,-1,-1,-1],\"OSkill\":[0,-1,-1,-1,-1,-1,-1,-1,-1,-1],\"CSkill\":0,\"CSkillDelay\":500,\"X\":204,\"Y\":336,\"HpFrom1000\":20,\"MpFrom1000\":20,\"DamageFrom1000\":1,\"Exp\":100,\"OriginalHp\":100,\"OriginalMp\":100,\"OriginalDamage\":15,\"OriginalDefence\":0,\"OriginalCrit\":0,\"Hp\":143,\"Mp\":85,\"Stamina\":9990,\"VIP\":0,\"MaxStamina\":10000,\"NangDong\":0,\"MountId\":-1,\"Teleport\":1,\"Gold\":5000007897,\"Diamond\":99628,\"DiamondLock\":898900,\"LimitGold\":50000000000,\"LimitDiamond\":100000,\"LimitDiamondLock\":200000000,\"IsNewMember\":true,\"IsNhanBua\":true,\"PhukienPart\":-1,\"IsHavePet\":false,\"IsPremium\":false,\"ThoiGianTrungMaBu\":0,\"ThoiGianDuaHau\":0,\"TimeAutoPlay\":0,\"CountGoiRong\":0,\"Fusion\":{\"IsFusion\":false,\"IsPorata\":false,\"IsPorata2\":false,\"TimeStart\":-1,\"DelayFusion\":-1,\"TimeUse\":0},\"LockInventory\":{\"IsLock\":false,\"Pass\":-1,\"PassTemp\":-1},\"LearnSkill\":null,\"LearnSkillTemp\":null,\"ItemAmulet\":{},\"Cards\":{},\"TrainManhVo\":0,\"TrainManhHon\":0,\"SoLanGiaoDich\":0,\"ThoiGianGiaoDich\":0,\"ThoiGianChatTheGioi\":0,\"ThoiGianDoiMayChu\":1705781009512,\"HieuUngDonDanh\":true,\"EffectAuraId\":-1,\"idEff_Set_Item\":-1,\"idHat\":-1,\"PetId\":943,\"InfoSideTask\":{\"countChange\":20,\"countMission\":-1,\"mission\":\"\",\"levelMission\":-1,\"reward\":null,\"Counting\":0,\"IdMission\":-1,\"typeMission\":-1},\"isDiemDanh\":false,\"PointQuayThuong\":0,\"PointSanBoss\":0,\"IsOutZone\":true}', '[66]', '{\"Id\":32,\"Index\":1,\"Count\":0,\"Time\":1696030049427}', 19, 1, '[]', '[]', '{\"Id\":10001,\"Head\":30,\"Body\":14,\"Leg\":15,\"Bag\":26,\"Name\":\"admin\",\"IsOnline\":true,\"Power\":4967440}', 109, '[]', '2024-01-20 08:57:29', '2023-09-29 23:21:15', '{\"Id\":9,\"Info\":\"Chí mạng liên tục khi HP dưới 43% [20 đến 50]\",\"Img\":716,\"SkillId\":-1,\"Value\":43,\"nextAttackDmgPercent\":0,\"isCrit\":false}', '{\"ThucAnId\":-1,\"ThucAnTime\":0,\"CuongNo\":false,\"CuongNoTime\":0,\"BinhChuaCommeson\":false,\"BinhChuaCommesonTime\":0,\"BoHuyet\":false,\"BoHuyetTime\":0,\"XiMuoiHoaDao\":false,\"XiMuoiHoaDaoTime\":0,\"XiMuoiHoaMai\":false,\"XiMuoiHoaMaiTime\":0,\"BoKhi\":false,\"BoKhiTime\":0,\"effRongXuong\":false,\"effRongXuongTime\":1705107704883,\"GiapXen\":false,\"GiapXenTime\":0,\"AnDanh\":false,\"AnDanhTime\":0,\"CuongNo2\":false,\"CuongNoTime2\":1704466253970,\"BoHuyet2\":false,\"BoHuyetTime2\":1704466097601,\"BoKhi2\":false,\"BoKhiTime2\":1704466066397,\"GiapXen2\":false,\"GiapXenTime2\":1704466095858,\"AnDanh2\":false,\"AnDanhTime2\":0,\"MayDoCSKB\":false,\"MayDoCSKBTime\":0,\"CuCarot\":false,\"CuCarotTime\":0,\"BanhTrungThuId\":-1,\"BanhTrungThuTime\":0,\"isActiveCrit\":false,\"isEnchantCrit\":false,\"delayEnchantCrit\":-1,\"timeEnchantCrit\":-1,\"isActiveGiap\":false,\"isEnchantGiap\":false,\"delayEnchantGiap\":-1,\"timeEnchantGiap\":-1,\"KichDucX2\":false,\"KichDucX2Time\":-1,\"KichDucX5\":true,\"KichDucX5Time\":1704498242102,\"KichDucX7\":false,\"KichDucX7Time\":-1}', 0, '{\"pickChan\":false,\"pickLe\":false,\"thoivang\":0}', '[]', '{\"Count\":[5140831,5140831,9,0,0,0,0,0,0,0,0,0,0,0,68,0,0,0,0,0],\"isFinish\":[true,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false],\"isCollect\":[true,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false]}', '{\"Round\":0,\"Battled\":false,\"isCollected\":false,\"ChestLevel\":0,\"Count\":0}', '{\"DataTapLuyenn\":{\"isPractice\":false,\"currentTimePractice\":-1},\"Potenial\":-1,\"MapTraning\":-1,\"OldMap\":null,\"isTraining\":false,\"lastTimeLogout\":-1,\"Level\":0,\"DataWhis\":{\"Level\":1,\"Time\":-1,\"Count\":0,\"TimeStart\":-1}}', '{\"Ticket\":3,\"Top\":0,\"History\":{\"Text\":[]},\"Gem\":100,\"point\":{\"HP\":-1,\"SD\":-1,\"Amor\":-1,\"Win\":0}}', 100);
INSERT INTO `character` VALUES (10002, 'adadsdsd', '[{\"Id\":4,\"SkillId\":28,\"CoolDown\":0,\"Point\":1,\"CurrExp\":0}]', '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":2,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":47,\"Param\":3}]},{\"IndexUI\":1,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":8,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":6,\"Param\":20}]},null,null,null,null,null,null,null,null,null,null]', '[]', '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":12,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":14,\"Param\":1}]}]', '{\"NClass\":2,\"Gender\":2,\"MapId\":0,\"MapCustomId\":-1,\"ZoneId\":0,\"Hair\":27,\"Bag\":-1,\"Level\":0,\"Speed\":6,\"Pk\":0,\"TypePk\":0,\"Potential\":0,\"TotalPotential\":0,\"Power\":1200,\"IsDie\":false,\"IsPower\":true,\"LitmitPower\":0,\"KSkill\":[-1,-1,-1,-1,-1,-1,-1,-1,-1,-1],\"OSkill\":[4,-1,-1,-1,-1,-1,-1,-1,-1,-1],\"CSkill\":4,\"CSkillDelay\":500,\"X\":552,\"Y\":408,\"HpFrom1000\":20,\"MpFrom1000\":20,\"DamageFrom1000\":1,\"Exp\":100,\"OriginalHp\":100,\"OriginalMp\":100,\"OriginalDamage\":15,\"OriginalDefence\":0,\"OriginalCrit\":0,\"Hp\":100,\"Mp\":98,\"Stamina\":10000,\"VIP\":0,\"MaxStamina\":10000,\"NangDong\":0,\"MountId\":-1,\"Teleport\":1,\"Gold\":5000000000,\"Diamond\":100000,\"DiamondLock\":0,\"LimitGold\":50000000000,\"LimitDiamond\":100000,\"LimitDiamondLock\":200000000,\"IsNewMember\":true,\"IsNhanBua\":false,\"PhukienPart\":-1,\"IsHavePet\":false,\"IsPremium\":false,\"ThoiGianTrungMaBu\":0,\"ThoiGianDuaHau\":0,\"TimeAutoPlay\":0,\"CountGoiRong\":0,\"Fusion\":{\"IsFusion\":false,\"IsPorata\":false,\"IsPorata2\":false,\"TimeStart\":-1,\"DelayFusion\":-1,\"TimeUse\":0},\"LockInventory\":{\"IsLock\":false,\"Pass\":-1,\"PassTemp\":-1},\"LearnSkill\":null,\"LearnSkillTemp\":null,\"ItemAmulet\":{},\"Cards\":{},\"TrainManhVo\":0,\"TrainManhHon\":0,\"SoLanGiaoDich\":0,\"ThoiGianGiaoDich\":0,\"ThoiGianChatTheGioi\":0,\"ThoiGianDoiMayChu\":0,\"HieuUngDonDanh\":true,\"EffectAuraId\":-1,\"idEff_Set_Item\":-1,\"idHat\":-1,\"PetId\":-1,\"InfoSideTask\":{\"countChange\":20,\"countMission\":-1,\"mission\":\"\",\"levelMission\":-1,\"reward\":null,\"Counting\":0,\"IdMission\":-1,\"typeMission\":-1},\"isDiemDanh\":false,\"PointQuayThuong\":0,\"PointSanBoss\":0,\"IsOutZone\":true}', '[87]', '{\"Id\":1,\"Index\":0,\"Count\":0,\"Time\":0}', 0, 0, '[]', '[]', '{\"Id\":0,\"Head\":0,\"Body\":57,\"Leg\":58,\"Bag\":-1,\"Name\":null,\"IsOnline\":true,\"Power\":1200}', -1, '[]', '2024-01-05 01:46:50', '2024-01-05 01:46:50', '{\"Id\":-1,\"Info\":\"Chưa có Nội Tại\nBấm vào để xem chi tiết\",\"Img\":5223,\"SkillId\":-1,\"Value\":0,\"nextAttackDmgPercent\":0,\"isCrit\":false}', '{\"ThucAnId\":-1,\"ThucAnTime\":0,\"CuongNo\":false,\"CuongNoTime\":0,\"BinhChuaCommeson\":false,\"BinhChuaCommesonTime\":0,\"BoHuyet\":false,\"BoHuyetTime\":0,\"XiMuoiHoaDao\":false,\"XiMuoiHoaDaoTime\":0,\"XiMuoiHoaMai\":false,\"XiMuoiHoaMaiTime\":0,\"BoKhi\":false,\"BoKhiTime\":0,\"effRongXuong\":false,\"effRongXuongTime\":0,\"GiapXen\":false,\"GiapXenTime\":0,\"AnDanh\":false,\"AnDanhTime\":0,\"CuongNo2\":false,\"CuongNoTime2\":0,\"BoHuyet2\":false,\"BoHuyetTime2\":0,\"BoKhi2\":false,\"BoKhiTime2\":0,\"GiapXen2\":false,\"GiapXenTime2\":0,\"AnDanh2\":false,\"AnDanhTime2\":0,\"MayDoCSKB\":false,\"MayDoCSKBTime\":0,\"CuCarot\":false,\"CuCarotTime\":0,\"BanhTrungThuId\":-1,\"BanhTrungThuTime\":0,\"isActiveCrit\":false,\"isEnchantCrit\":false,\"delayEnchantCrit\":-1,\"timeEnchantCrit\":-1,\"isActiveGiap\":false,\"isEnchantGiap\":false,\"delayEnchantGiap\":-1,\"timeEnchantGiap\":-1,\"KichDucX2\":false,\"KichDucX2Time\":-1,\"KichDucX5\":false,\"KichDucX5Time\":-1,\"KichDucX7\":false,\"KichDucX7Time\":-1}', 0, '{\"pickChan\":false,\"pickLe\":false,\"thoivang\":0}', '[]', '{\"Count\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"isFinish\":[false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false],\"isCollect\":[false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false]}', '{\"Round\":0,\"Battled\":false,\"isCollected\":false,\"ChestLevel\":0,\"Count\":0}', '{\"DataTapLuyenn\":{\"isPractice\":false,\"currentTimePractice\":-1},\"Potenial\":-1,\"MapTraning\":-1,\"OldMap\":null,\"isTraining\":false,\"lastTimeLogout\":-1,\"Level\":0,\"DataWhis\":{\"Level\":1,\"Time\":-1,\"Count\":0,\"TimeStart\":-1}}', '{\"Ticket\":3,\"Top\":0,\"History\":{\"Text\":[]},\"Gem\":100,\"point\":{\"HP\":-1,\"SD\":-1,\"Amor\":-1,\"Win\":0}}', NULL);
INSERT INTO `character` VALUES (10003, 'adsdsdsd', '[{\"Id\":2,\"SkillId\":14,\"CoolDown\":1705422377810,\"Point\":1,\"CurrExp\":0},{\"Id\":26,\"SkillId\":176,\"CoolDown\":1705422495549,\"Point\":1,\"CurrExp\":90}]', '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":2,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":47,\"Param\":3}]},{\"IndexUI\":1,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":8,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":6,\"Param\":20}]},null,null,null,{\"IndexUI\":5,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":548,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":73,\"Param\":0},{\"Id\":30,\"Param\":0}]},null,null,null,null,null,null]', '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":865,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":50,\"Param\":25},{\"Id\":77,\"Param\":15},{\"Id\":103,\"Param\":15},{\"Id\":14,\"Param\":10}]},{\"IndexUI\":1,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":1206,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":50,\"Param\":11},{\"Id\":77,\"Param\":11},{\"Id\":103,\"Param\":11},{\"Id\":14,\"Param\":11},{\"Id\":30,\"Param\":0}]},{\"IndexUI\":2,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":1155,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":50,\"Param\":11},{\"Id\":77,\"Param\":11},{\"Id\":103,\"Param\":11},{\"Id\":14,\"Param\":11},{\"Id\":30,\"Param\":0}]},{\"IndexUI\":3,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":1156,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":50,\"Param\":11},{\"Id\":77,\"Param\":11},{\"Id\":103,\"Param\":11},{\"Id\":14,\"Param\":11},{\"Id\":30,\"Param\":0}]},{\"IndexUI\":4,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":1157,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":50,\"Param\":11},{\"Id\":77,\"Param\":11},{\"Id\":103,\"Param\":11},{\"Id\":14,\"Param\":11},{\"Id\":30,\"Param\":0}]},{\"IndexUI\":5,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":1205,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":50,\"Param\":25},{\"Id\":77,\"Param\":35},{\"Id\":103,\"Param\":35},{\"Id\":30,\"Param\":0}]},{\"IndexUI\":6,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":999,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":50,\"Param\":10},{\"Id\":77,\"Param\":10},{\"Id\":103,\"Param\":10},{\"Id\":14,\"Param\":10}]},{\"IndexUI\":7,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":1266,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":77,\"Param\":17},{\"Id\":103,\"Param\":17},{\"Id\":50,\"Param\":15}]},{\"IndexUI\":8,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":1185,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":50,\"Param\":11},{\"Id\":77,\"Param\":11},{\"Id\":103,\"Param\":11},{\"Id\":14,\"Param\":11},{\"Id\":30,\"Param\":0}]},{\"IndexUI\":9,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":957,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":73,\"Param\":0},{\"Id\":30,\"Param\":0}]},{\"IndexUI\":10,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":934,\"Vang\":0,\"Ngoc\":0,\"Quantity\":9108,\"Reason\":\"\",\"Options\":[{\"Id\":73,\"Param\":0},{\"Id\":30,\"Param\":0}]},{\"IndexUI\":11,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":457,\"Vang\":0,\"Ngoc\":0,\"Quantity\":9982,\"Reason\":\"\",\"Options\":[{\"Id\":73,\"Param\":0},{\"Id\":30,\"Param\":0}]},{\"IndexUI\":12,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":921,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":72,\"Param\":2},{\"Id\":50,\"Param\":17}]},{\"IndexUI\":13,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":1548,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":50,\"Param\":50},{\"Id\":5,\"Param\":50},{\"Id\":117,\"Param\":25},{\"Id\":103,\"Param\":20},{\"Id\":77,\"Param\":20},{\"Id\":116,\"Param\":0},{\"Id\":219,\"Param\":5},{\"Id\":30,\"Param\":0}]},{\"IndexUI\":14,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":925,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":73,\"Param\":0}]},{\"IndexUI\":15,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":581,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":73,\"Param\":0},{\"Id\":30,\"Param\":0}]},{\"IndexUI\":16,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":607,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":19,\"Param\":21},{\"Id\":77,\"Param\":18},{\"Id\":103,\"Param\":18},{\"Id\":30,\"Param\":0}]},{\"IndexUI\":17,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":935,\"Vang\":0,\"Ngoc\":0,\"Quantity\":90,\"Reason\":\"\",\"Options\":[{\"Id\":30,\"Param\":1}]},{\"IndexUI\":18,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":74,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":73,\"Param\":0}]},{\"IndexUI\":19,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":194,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":73,\"Param\":0}]}]', '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":12,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":14,\"Param\":1}]}]', '{\"NClass\":1,\"Gender\":1,\"MapId\":22,\"MapCustomId\":-1,\"ZoneId\":0,\"Hair\":29,\"Bag\":-1,\"Level\":8,\"Speed\":6,\"Pk\":0,\"TypePk\":0,\"Potential\":1727292,\"TotalPotential\":0,\"Power\":1728492,\"IsDie\":false,\"IsPower\":true,\"LitmitPower\":0,\"KSkill\":[-1,-1,-1,-1,-1,-1,-1,-1,-1,-1],\"OSkill\":[-1,2,2,-1,-1,-1,-1,-1,26,-1],\"CSkill\":2,\"CSkillDelay\":500,\"X\":383,\"Y\":336,\"HpFrom1000\":20,\"MpFrom1000\":20,\"DamageFrom1000\":1,\"Exp\":100,\"OriginalHp\":100,\"OriginalMp\":100,\"OriginalDamage\":15,\"OriginalDefence\":0,\"OriginalCrit\":0,\"Hp\":1,\"Mp\":96,\"Stamina\":9932,\"VIP\":0,\"MaxStamina\":10000,\"NangDong\":0,\"MountId\":-1,\"Teleport\":1,\"Gold\":4790010441,\"Diamond\":97290,\"DiamondLock\":0,\"LimitGold\":50000000000,\"LimitDiamond\":100000,\"LimitDiamondLock\":200000000,\"IsNewMember\":true,\"IsNhanBua\":true,\"PhukienPart\":-1,\"IsHavePet\":true,\"IsPremium\":false,\"ThoiGianTrungMaBu\":0,\"ThoiGianDuaHau\":0,\"TimeAutoPlay\":0,\"CountGoiRong\":0,\"Fusion\":{\"IsFusion\":false,\"IsPorata\":false,\"IsPorata2\":false,\"TimeStart\":1705012696299,\"DelayFusion\":1705013296299,\"TimeUse\":0},\"LockInventory\":{\"IsLock\":false,\"Pass\":-1,\"PassTemp\":-1},\"LearnSkill\":null,\"LearnSkillTemp\":null,\"ItemAmulet\":{},\"Cards\":{},\"TrainManhVo\":0,\"TrainManhHon\":0,\"SoLanGiaoDich\":0,\"ThoiGianGiaoDich\":1705103486730,\"ThoiGianChatTheGioi\":0,\"ThoiGianDoiMayChu\":1705296872,\"HieuUngDonDanh\":true,\"EffectAuraId\":-1,\"idEff_Set_Item\":-1,\"idHat\":-1,\"PetId\":-1,\"InfoSideTask\":{\"countChange\":20,\"countMission\":-1,\"mission\":\"\",\"levelMission\":-1,\"reward\":null,\"Counting\":0,\"IdMission\":-1,\"typeMission\":-1},\"isDiemDanh\":false,\"PointQuayThuong\":0,\"PointSanBoss\":0,\"IsOutZone\":true}', '[79]', '{\"Id\":22,\"Index\":0,\"Count\":1,\"Time\":1704467392042}', 0, 0, '[]', '[{\"Id\":10001,\"Head\":30,\"Body\":14,\"Leg\":15,\"Bag\":-1,\"Name\":\"admin\",\"IsOnline\":true,\"Power\":6624218},{\"Id\":10004,\"Head\":27,\"Body\":16,\"Leg\":17,\"Bag\":-1,\"Name\":\"adadadaf\",\"IsOnline\":true,\"Power\":3450}]', '{\"Id\":10003,\"Head\":465,\"Body\":466,\"Leg\":467,\"Bag\":-1,\"Name\":\"adsdsdsd\",\"IsOnline\":true,\"Power\":1728492}', -1, '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":190,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"Vòng Quay May Mắn\",\"Options\":[{\"Id\":73,\"Param\":2137896872},{\"Id\":93,\"Param\":5},{\"Id\":171,\"Param\":16}]},{\"IndexUI\":1,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":190,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"Vòng Quay May Mắn\",\"Options\":[{\"Id\":73,\"Param\":1791576872},{\"Id\":93,\"Param\":0},{\"Id\":171,\"Param\":11}]},{\"IndexUI\":2,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":190,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"Vòng Quay May Mắn\",\"Options\":[{\"Id\":73,\"Param\":0},{\"Id\":171,\"Param\":10}]},{\"IndexUI\":3,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":997,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"Vòng Quay May Mắn\",\"Options\":[{\"Id\":50,\"Param\":10},{\"Id\":77,\"Param\":10},{\"Id\":103,\"Param\":10},{\"Id\":14,\"Param\":10}]},{\"IndexUI\":4,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":190,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"Vòng Quay May Mắn\",\"Options\":[{\"Id\":73,\"Param\":2137896872},{\"Id\":93,\"Param\":5},{\"Id\":171,\"Param\":14}]},{\"IndexUI\":5,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":190,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"Vòng Quay May Mắn\",\"Options\":[{\"Id\":73,\"Param\":1878156872},{\"Id\":93,\"Param\":1},{\"Id\":171,\"Param\":19}]},{\"IndexUI\":6,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":190,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"Vòng Quay May Mắn\",\"Options\":[{\"Id\":73,\"Param\":0},{\"Id\":171,\"Param\":17}]},{\"IndexUI\":7,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":190,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"Vòng Quay May Mắn\",\"Options\":[{\"Id\":73,\"Param\":2137896882},{\"Id\":93,\"Param\":5},{\"Id\":171,\"Param\":15}]},{\"IndexUI\":8,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":1263,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"Vòng Quay May Mắn\",\"Options\":[{\"Id\":77,\"Param\":17},{\"Id\":103,\"Param\":17},{\"Id\":50,\"Param\":15}]},{\"IndexUI\":10,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":190,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"Vòng Quay May Mắn\",\"Options\":[{\"Id\":73,\"Param\":1878158948},{\"Id\":93,\"Param\":1},{\"Id\":171,\"Param\":12}]},{\"IndexUI\":13,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":999,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"Vòng Quay May Mắn\",\"Options\":[{\"Id\":50,\"Param\":10},{\"Id\":77,\"Param\":10},{\"Id\":103,\"Param\":10},{\"Id\":14,\"Param\":10}]},{\"IndexUI\":14,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":190,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"Vòng Quay May Mắn\",\"Options\":[{\"Id\":73,\"Param\":0},{\"Id\":171,\"Param\":12}]},{\"IndexUI\":17,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":190,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"Vòng Quay May Mắn\",\"Options\":[{\"Id\":73,\"Param\":1878158963},{\"Id\":93,\"Param\":1},{\"Id\":171,\"Param\":15}]},{\"IndexUI\":18,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":190,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"Vòng Quay May Mắn\",\"Options\":[{\"Id\":73,\"Param\":1791578963},{\"Id\":93,\"Param\":0},{\"Id\":171,\"Param\":14}]},{\"IndexUI\":19,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":953,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"Vòng Quay May Mắn\",\"Options\":[{\"Id\":73,\"Param\":0}]},{\"IndexUI\":20,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":997,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"Vòng Quay May Mắn\",\"Options\":[{\"Id\":50,\"Param\":10},{\"Id\":77,\"Param\":10},{\"Id\":103,\"Param\":10},{\"Id\":14,\"Param\":10}]},{\"IndexUI\":21,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":1106,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"Vòng Quay May Mắn\",\"Options\":[{\"Id\":50,\"Param\":11},{\"Id\":77,\"Param\":11},{\"Id\":103,\"Param\":11},{\"Id\":14,\"Param\":11}]},{\"IndexUI\":23,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":829,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"Vòng Quay May Mắn\",\"Options\":[{\"Id\":73,\"Param\":0}]}]', '2024-01-16 16:24:58', '2024-01-05 15:06:55', '{\"Id\":-1,\"Info\":\"Chưa có Nội Tại\nBấm vào để xem chi tiết\",\"Img\":5223,\"SkillId\":-1,\"Value\":0,\"nextAttackDmgPercent\":0,\"isCrit\":false}', '{\"ThucAnId\":-1,\"ThucAnTime\":0,\"CuongNo\":false,\"CuongNoTime\":0,\"BinhChuaCommeson\":false,\"BinhChuaCommesonTime\":0,\"BoHuyet\":false,\"BoHuyetTime\":0,\"XiMuoiHoaDao\":false,\"XiMuoiHoaDaoTime\":1705000637111,\"XiMuoiHoaMai\":false,\"XiMuoiHoaMaiTime\":1705000650053,\"BoKhi\":false,\"BoKhiTime\":0,\"effRongXuong\":false,\"effRongXuongTime\":0,\"GiapXen\":false,\"GiapXenTime\":0,\"AnDanh\":false,\"AnDanhTime\":0,\"CuongNo2\":false,\"CuongNoTime2\":0,\"BoHuyet2\":false,\"BoHuyetTime2\":0,\"BoKhi2\":false,\"BoKhiTime2\":0,\"GiapXen2\":false,\"GiapXenTime2\":0,\"AnDanh2\":false,\"AnDanhTime2\":0,\"MayDoCSKB\":false,\"MayDoCSKBTime\":0,\"CuCarot\":false,\"CuCarotTime\":0,\"BanhTrungThuId\":-1,\"BanhTrungThuTime\":0,\"isActiveCrit\":false,\"isEnchantCrit\":false,\"delayEnchantCrit\":-1,\"timeEnchantCrit\":-1,\"isActiveGiap\":false,\"isEnchantGiap\":false,\"delayEnchantGiap\":-1,\"timeEnchantGiap\":-1,\"KichDucX2\":false,\"KichDucX2Time\":-1,\"KichDucX5\":false,\"KichDucX5Time\":-1,\"KichDucX7\":false,\"KichDucX7Time\":-1}', 0, '{\"pickChan\":false,\"pickLe\":false,\"thoivang\":0}', '[]', '{\"Count\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,11,0,0,0,0,0],\"isFinish\":[false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false],\"isCollect\":[false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false]}', '{\"Round\":0,\"Battled\":false,\"isCollected\":false,\"ChestLevel\":0,\"Count\":0}', '{\"DataTapLuyenn\":{\"isPractice\":false,\"currentTimePractice\":-1},\"Potenial\":-1,\"MapTraning\":-1,\"OldMap\":null,\"isTraining\":false,\"lastTimeLogout\":-1,\"Level\":0,\"DataWhis\":{\"Level\":1,\"Time\":-1,\"Count\":0,\"TimeStart\":-1}}', '{\"Ticket\":3,\"Top\":0,\"History\":{\"Text\":[]},\"Gem\":100,\"point\":{\"HP\":-1,\"SD\":-1,\"Amor\":-1,\"Win\":0}}', NULL);
INSERT INTO `character` VALUES (10004, 'adadadaf', '[{\"Id\":4,\"SkillId\":28,\"CoolDown\":1704496820114,\"Point\":1,\"CurrExp\":0},{\"Id\":25,\"SkillId\":166,\"CoolDown\":1704497343313,\"Point\":1,\"CurrExp\":50}]', '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":2,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":47,\"Param\":3}]},{\"IndexUI\":1,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":8,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":6,\"Param\":20}]},null,null,null,null,null,null,null,null,null,null]', '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":194,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":73,\"Param\":0}]},{\"IndexUI\":1,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":13,\"Vang\":0,\"Ngoc\":0,\"Quantity\":4,\"Reason\":\"\",\"Options\":[{\"Id\":48,\"Param\":100}]}]', '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":12,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":14,\"Param\":1}]}]', '{\"NClass\":2,\"Gender\":2,\"MapId\":1,\"MapCustomId\":-1,\"ZoneId\":0,\"Hair\":27,\"Bag\":-1,\"Level\":1,\"Speed\":6,\"Pk\":0,\"TypePk\":0,\"Potential\":4650,\"TotalPotential\":0,\"Power\":5850,\"IsDie\":false,\"IsPower\":true,\"LitmitPower\":0,\"KSkill\":[-1,-1,-1,-1,-1,-1,-1,-1,-1,-1],\"OSkill\":[4,25,-1,-1,-1,-1,-1,-1,-1,-1],\"CSkill\":4,\"CSkillDelay\":500,\"X\":494,\"Y\":408,\"HpFrom1000\":20,\"MpFrom1000\":20,\"DamageFrom1000\":1,\"Exp\":100,\"OriginalHp\":100,\"OriginalMp\":100,\"OriginalDamage\":15,\"OriginalDefence\":0,\"OriginalCrit\":0,\"Hp\":120,\"Mp\":94,\"Stamina\":9982,\"VIP\":0,\"MaxStamina\":10000,\"NangDong\":0,\"MountId\":-1,\"Teleport\":1,\"Gold\":4990000000,\"Diamond\":98900,\"DiamondLock\":0,\"LimitGold\":50000000000,\"LimitDiamond\":100000,\"LimitDiamondLock\":200000000,\"IsNewMember\":true,\"IsNhanBua\":false,\"PhukienPart\":-1,\"IsHavePet\":false,\"IsPremium\":false,\"ThoiGianTrungMaBu\":0,\"ThoiGianDuaHau\":0,\"TimeAutoPlay\":0,\"CountGoiRong\":0,\"Fusion\":{\"IsFusion\":false,\"IsPorata\":false,\"IsPorata2\":false,\"TimeStart\":-1,\"DelayFusion\":-1,\"TimeUse\":0},\"LockInventory\":{\"IsLock\":false,\"Pass\":-1,\"PassTemp\":-1},\"LearnSkill\":null,\"LearnSkillTemp\":null,\"ItemAmulet\":{},\"Cards\":{},\"TrainManhVo\":0,\"TrainManhHon\":0,\"SoLanGiaoDich\":0,\"ThoiGianGiaoDich\":0,\"ThoiGianChatTheGioi\":0,\"ThoiGianDoiMayChu\":0,\"HieuUngDonDanh\":true,\"EffectAuraId\":-1,\"idEff_Set_Item\":-1,\"idHat\":-1,\"PetId\":-1,\"InfoSideTask\":{\"countChange\":20,\"countMission\":-1,\"mission\":\"\",\"levelMission\":-1,\"reward\":null,\"Counting\":0,\"IdMission\":-1,\"typeMission\":-1},\"isDiemDanh\":false,\"PointQuayThuong\":0,\"PointSanBoss\":0,\"IsOutZone\":true}', '[87]', '{\"Id\":21,\"Index\":0,\"Count\":0,\"Time\":0}', 0, 0, '[]', '[{\"Id\":10001,\"Head\":30,\"Body\":14,\"Leg\":15,\"Bag\":26,\"Name\":\"admin\",\"IsOnline\":true,\"Power\":9000207964}]', '{\"Id\":10004,\"Head\":27,\"Body\":16,\"Leg\":17,\"Bag\":-1,\"Name\":\"adadadaf\",\"IsOnline\":true,\"Power\":3450}', -1, '[]', '2024-01-05 22:45:36', '2024-01-05 22:45:36', '{\"Id\":-1,\"Info\":\"Chưa có Nội Tại\nBấm vào để xem chi tiết\",\"Img\":5223,\"SkillId\":-1,\"Value\":0,\"nextAttackDmgPercent\":0,\"isCrit\":false}', '{\"ThucAnId\":-1,\"ThucAnTime\":0,\"CuongNo\":false,\"CuongNoTime\":0,\"BinhChuaCommeson\":false,\"BinhChuaCommesonTime\":0,\"BoHuyet\":false,\"BoHuyetTime\":0,\"XiMuoiHoaDao\":false,\"XiMuoiHoaDaoTime\":0,\"XiMuoiHoaMai\":false,\"XiMuoiHoaMaiTime\":0,\"BoKhi\":false,\"BoKhiTime\":0,\"effRongXuong\":false,\"effRongXuongTime\":0,\"GiapXen\":false,\"GiapXenTime\":0,\"AnDanh\":false,\"AnDanhTime\":0,\"CuongNo2\":false,\"CuongNoTime2\":0,\"BoHuyet2\":false,\"BoHuyetTime2\":0,\"BoKhi2\":false,\"BoKhiTime2\":0,\"GiapXen2\":false,\"GiapXenTime2\":0,\"AnDanh2\":false,\"AnDanhTime2\":0,\"MayDoCSKB\":false,\"MayDoCSKBTime\":0,\"CuCarot\":false,\"CuCarotTime\":0,\"BanhTrungThuId\":-1,\"BanhTrungThuTime\":0,\"isActiveCrit\":false,\"isEnchantCrit\":false,\"delayEnchantCrit\":-1,\"timeEnchantCrit\":-1,\"isActiveGiap\":false,\"isEnchantGiap\":false,\"delayEnchantGiap\":-1,\"timeEnchantGiap\":-1,\"KichDucX2\":false,\"KichDucX2Time\":-1,\"KichDucX5\":false,\"KichDucX5Time\":-1,\"KichDucX7\":false,\"KichDucX7Time\":-1}', 0, '{\"pickChan\":false,\"pickLe\":false,\"thoivang\":0}', '[]', '{\"Count\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0],\"isFinish\":[false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false],\"isCollect\":[false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false]}', '{\"Round\":0,\"Battled\":false,\"isCollected\":false,\"ChestLevel\":0,\"Count\":0}', '{\"DataTapLuyenn\":{\"isPractice\":false,\"currentTimePractice\":-1},\"Potenial\":-1,\"MapTraning\":-1,\"OldMap\":null,\"isTraining\":false,\"lastTimeLogout\":-1,\"Level\":0,\"DataWhis\":{\"Level\":1,\"Time\":-1,\"Count\":0,\"TimeStart\":-1}}', '{\"Ticket\":3,\"Top\":0,\"History\":{\"Text\":[]},\"Gem\":100,\"point\":{\"HP\":-1,\"SD\":-1,\"Amor\":-1,\"Win\":0}}', NULL);
INSERT INTO `character` VALUES (10005, 'test1', '[{\"Id\":0,\"SkillId\":0,\"CoolDown\":0,\"Point\":1,\"CurrExp\":0}]', '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":0,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":47,\"Param\":2}]},{\"IndexUI\":1,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":6,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":6,\"Param\":30}]},null,null,null,null,null,null,null,null,null,null]', '[]', '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":12,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":14,\"Param\":1}]}]', '{\"NClass\":0,\"Gender\":0,\"MapId\":21,\"MapCustomId\":-1,\"ZoneId\":0,\"Hair\":30,\"Bag\":-1,\"Level\":0,\"Speed\":6,\"Pk\":0,\"TypePk\":0,\"Potential\":0,\"TotalPotential\":0,\"Power\":1200,\"IsDie\":false,\"IsPower\":true,\"LitmitPower\":0,\"KSkill\":[-1,-1,-1,-1,-1,-1,-1,-1,-1,-1],\"OSkill\":[0,-1,-1,-1,-1,-1,-1,-1,-1,-1],\"CSkill\":0,\"CSkillDelay\":500,\"X\":35,\"Y\":336,\"HpFrom1000\":20,\"MpFrom1000\":20,\"DamageFrom1000\":1,\"Exp\":100,\"OriginalHp\":100,\"OriginalMp\":100,\"OriginalDamage\":15,\"OriginalDefence\":0,\"OriginalCrit\":0,\"Hp\":100,\"Mp\":100,\"Stamina\":10000,\"VIP\":0,\"MaxStamina\":10000,\"NangDong\":0,\"MountId\":-1,\"Teleport\":1,\"Gold\":5000000000,\"Diamond\":100000,\"DiamondLock\":0,\"LimitGold\":50000000000,\"LimitDiamond\":100000,\"LimitDiamondLock\":200000000,\"IsNewMember\":true,\"IsNhanBua\":false,\"PhukienPart\":-1,\"IsHavePet\":false,\"IsPremium\":false,\"ThoiGianTrungMaBu\":0,\"ThoiGianDuaHau\":0,\"TimeAutoPlay\":0,\"CountGoiRong\":0,\"Fusion\":{\"IsFusion\":false,\"IsPorata\":false,\"IsPorata2\":false,\"TimeStart\":-1,\"DelayFusion\":-1,\"TimeUse\":0},\"LockInventory\":{\"IsLock\":false,\"Pass\":-1,\"PassTemp\":-1},\"LearnSkill\":null,\"LearnSkillTemp\":null,\"ItemAmulet\":{},\"Cards\":{},\"TrainManhVo\":0,\"TrainManhHon\":0,\"SoLanGiaoDich\":0,\"ThoiGianGiaoDich\":0,\"ThoiGianChatTheGioi\":0,\"ThoiGianDoiMayChu\":0,\"HieuUngDonDanh\":true,\"EffectAuraId\":-1,\"idEff_Set_Item\":-1,\"idHat\":-1,\"PetId\":-1,\"InfoSideTask\":{\"countChange\":20,\"countMission\":-1,\"mission\":\"\",\"levelMission\":-1,\"reward\":null,\"Counting\":0,\"IdMission\":-1,\"typeMission\":-1},\"isDiemDanh\":false,\"PointQuayThuong\":0,\"PointSanBoss\":0,\"IsOutZone\":true}', '[66]', '{\"Id\":1,\"Index\":0,\"Count\":0,\"Time\":0}', 0, 0, '[]', '[]', '{\"Id\":0,\"Head\":0,\"Body\":57,\"Leg\":58,\"Bag\":-1,\"Name\":null,\"IsOnline\":true,\"Power\":1200}', -1, '[]', '2024-01-14 16:06:28', '2024-01-14 16:06:28', '{\"Id\":-1,\"Info\":\"Chưa có Nội Tại\nBấm vào để xem chi tiết\",\"Img\":5223,\"SkillId\":-1,\"Value\":0,\"nextAttackDmgPercent\":0,\"isCrit\":false}', '{\"ThucAnId\":-1,\"ThucAnTime\":0,\"CuongNo\":false,\"CuongNoTime\":0,\"BinhChuaCommeson\":false,\"BinhChuaCommesonTime\":0,\"BoHuyet\":false,\"BoHuyetTime\":0,\"XiMuoiHoaDao\":false,\"XiMuoiHoaDaoTime\":0,\"XiMuoiHoaMai\":false,\"XiMuoiHoaMaiTime\":0,\"BoKhi\":false,\"BoKhiTime\":0,\"effRongXuong\":false,\"effRongXuongTime\":0,\"GiapXen\":false,\"GiapXenTime\":0,\"AnDanh\":false,\"AnDanhTime\":0,\"CuongNo2\":false,\"CuongNoTime2\":0,\"BoHuyet2\":false,\"BoHuyetTime2\":0,\"BoKhi2\":false,\"BoKhiTime2\":0,\"GiapXen2\":false,\"GiapXenTime2\":0,\"AnDanh2\":false,\"AnDanhTime2\":0,\"MayDoCSKB\":false,\"MayDoCSKBTime\":0,\"CuCarot\":false,\"CuCarotTime\":0,\"BanhTrungThuId\":-1,\"BanhTrungThuTime\":0,\"isActiveCrit\":false,\"isEnchantCrit\":false,\"delayEnchantCrit\":-1,\"timeEnchantCrit\":-1,\"isActiveGiap\":false,\"isEnchantGiap\":false,\"delayEnchantGiap\":-1,\"timeEnchantGiap\":-1,\"KichDucX2\":false,\"KichDucX2Time\":-1,\"KichDucX5\":false,\"KichDucX5Time\":-1,\"KichDucX7\":false,\"KichDucX7Time\":-1}', 0, '{\"pickChan\":false,\"pickLe\":false,\"thoivang\":0}', '[]', '{\"Count\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"isFinish\":[false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false],\"isCollect\":[false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false]}', '{\"Round\":0,\"Battled\":false,\"isCollected\":false,\"ChestLevel\":0,\"Count\":0}', '{\"DataTapLuyenn\":{\"isPractice\":false,\"currentTimePractice\":-1},\"Potenial\":-1,\"MapTraning\":-1,\"OldMap\":null,\"isTraining\":false,\"lastTimeLogout\":-1,\"Level\":0,\"DataWhis\":{\"Level\":1,\"Time\":-1,\"Count\":0,\"TimeStart\":-1}}', '{\"Ticket\":3,\"Top\":0,\"History\":{\"Text\":[]},\"Gem\":100,\"point\":{\"HP\":-1,\"SD\":-1,\"Amor\":-1,\"Win\":0}}', NULL);
INSERT INTO `character` VALUES (10006, 'test2', '[{\"Id\":2,\"SkillId\":14,\"CoolDown\":0,\"Point\":1,\"CurrExp\":0}]', '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":1,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":47,\"Param\":2}]},{\"IndexUI\":1,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":7,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":6,\"Param\":20}]},null,null,null,null,null,null,null,null,null,null]', '[]', '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":12,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":14,\"Param\":1}]}]', '{\"NClass\":1,\"Gender\":1,\"MapId\":22,\"MapCustomId\":-1,\"ZoneId\":0,\"Hair\":9,\"Bag\":-1,\"Level\":0,\"Speed\":6,\"Pk\":0,\"TypePk\":0,\"Potential\":0,\"TotalPotential\":0,\"Power\":1200,\"IsDie\":false,\"IsPower\":true,\"LitmitPower\":0,\"KSkill\":[-1,-1,-1,-1,-1,-1,-1,-1,-1,-1],\"OSkill\":[2,-1,-1,-1,-1,-1,-1,-1,-1,-1],\"CSkill\":2,\"CSkillDelay\":500,\"X\":35,\"Y\":336,\"HpFrom1000\":20,\"MpFrom1000\":20,\"DamageFrom1000\":1,\"Exp\":100,\"OriginalHp\":100,\"OriginalMp\":100,\"OriginalDamage\":15,\"OriginalDefence\":0,\"OriginalCrit\":0,\"Hp\":100,\"Mp\":100,\"Stamina\":10000,\"VIP\":0,\"MaxStamina\":10000,\"NangDong\":0,\"MountId\":-1,\"Teleport\":1,\"Gold\":5000000000,\"Diamond\":100000,\"DiamondLock\":0,\"LimitGold\":50000000000,\"LimitDiamond\":100000,\"LimitDiamondLock\":200000000,\"IsNewMember\":true,\"IsNhanBua\":false,\"PhukienPart\":-1,\"IsHavePet\":false,\"IsPremium\":false,\"ThoiGianTrungMaBu\":0,\"ThoiGianDuaHau\":0,\"TimeAutoPlay\":0,\"CountGoiRong\":0,\"Fusion\":{\"IsFusion\":false,\"IsPorata\":false,\"IsPorata2\":false,\"TimeStart\":-1,\"DelayFusion\":-1,\"TimeUse\":0},\"LockInventory\":{\"IsLock\":false,\"Pass\":-1,\"PassTemp\":-1},\"LearnSkill\":null,\"LearnSkillTemp\":null,\"ItemAmulet\":{},\"Cards\":{},\"TrainManhVo\":0,\"TrainManhHon\":0,\"SoLanGiaoDich\":0,\"ThoiGianGiaoDich\":0,\"ThoiGianChatTheGioi\":0,\"ThoiGianDoiMayChu\":0,\"HieuUngDonDanh\":true,\"EffectAuraId\":-1,\"idEff_Set_Item\":-1,\"idHat\":-1,\"PetId\":-1,\"InfoSideTask\":{\"countChange\":20,\"countMission\":-1,\"mission\":\"\",\"levelMission\":-1,\"reward\":null,\"Counting\":0,\"IdMission\":-1,\"typeMission\":-1},\"isDiemDanh\":false,\"PointQuayThuong\":0,\"PointSanBoss\":0,\"IsOutZone\":true}', '[79]', '{\"Id\":1,\"Index\":0,\"Count\":0,\"Time\":0}', 0, 0, '[]', '[]', '{\"Id\":0,\"Head\":0,\"Body\":57,\"Leg\":58,\"Bag\":-1,\"Name\":null,\"IsOnline\":true,\"Power\":1200}', -1, '[]', '2024-01-14 16:06:49', '2024-01-14 16:06:49', '{\"Id\":-1,\"Info\":\"Chưa có Nội Tại\nBấm vào để xem chi tiết\",\"Img\":5223,\"SkillId\":-1,\"Value\":0,\"nextAttackDmgPercent\":0,\"isCrit\":false}', '{\"ThucAnId\":-1,\"ThucAnTime\":0,\"CuongNo\":false,\"CuongNoTime\":0,\"BinhChuaCommeson\":false,\"BinhChuaCommesonTime\":0,\"BoHuyet\":false,\"BoHuyetTime\":0,\"XiMuoiHoaDao\":false,\"XiMuoiHoaDaoTime\":0,\"XiMuoiHoaMai\":false,\"XiMuoiHoaMaiTime\":0,\"BoKhi\":false,\"BoKhiTime\":0,\"effRongXuong\":false,\"effRongXuongTime\":0,\"GiapXen\":false,\"GiapXenTime\":0,\"AnDanh\":false,\"AnDanhTime\":0,\"CuongNo2\":false,\"CuongNoTime2\":0,\"BoHuyet2\":false,\"BoHuyetTime2\":0,\"BoKhi2\":false,\"BoKhiTime2\":0,\"GiapXen2\":false,\"GiapXenTime2\":0,\"AnDanh2\":false,\"AnDanhTime2\":0,\"MayDoCSKB\":false,\"MayDoCSKBTime\":0,\"CuCarot\":false,\"CuCarotTime\":0,\"BanhTrungThuId\":-1,\"BanhTrungThuTime\":0,\"isActiveCrit\":false,\"isEnchantCrit\":false,\"delayEnchantCrit\":-1,\"timeEnchantCrit\":-1,\"isActiveGiap\":false,\"isEnchantGiap\":false,\"delayEnchantGiap\":-1,\"timeEnchantGiap\":-1,\"KichDucX2\":false,\"KichDucX2Time\":-1,\"KichDucX5\":false,\"KichDucX5Time\":-1,\"KichDucX7\":false,\"KichDucX7Time\":-1}', 0, '{\"pickChan\":false,\"pickLe\":false,\"thoivang\":0}', '[]', '{\"Count\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"isFinish\":[false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false],\"isCollect\":[false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false]}', '{\"Round\":0,\"Battled\":false,\"isCollected\":false,\"ChestLevel\":0,\"Count\":0}', '{\"DataTapLuyenn\":{\"isPractice\":false,\"currentTimePractice\":-1},\"Potenial\":-1,\"MapTraning\":-1,\"OldMap\":null,\"isTraining\":false,\"lastTimeLogout\":-1,\"Level\":0,\"DataWhis\":{\"Level\":1,\"Time\":-1,\"Count\":0,\"TimeStart\":-1}}', '{\"Ticket\":3,\"Top\":0,\"History\":{\"Text\":[]},\"Gem\":100,\"point\":{\"HP\":-1,\"SD\":-1,\"Amor\":-1,\"Win\":0}}', NULL);
INSERT INTO `character` VALUES (10007, 'test3', '[{\"Id\":4,\"SkillId\":28,\"CoolDown\":0,\"Point\":1,\"CurrExp\":0}]', '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":2,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":47,\"Param\":3}]},{\"IndexUI\":1,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":8,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":6,\"Param\":20}]},null,null,null,null,null,null,null,null,null,null]', '[]', '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":12,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":14,\"Param\":1}]}]', '{\"NClass\":2,\"Gender\":2,\"MapId\":23,\"MapCustomId\":-1,\"ZoneId\":0,\"Hair\":6,\"Bag\":-1,\"Level\":0,\"Speed\":6,\"Pk\":0,\"TypePk\":0,\"Potential\":0,\"TotalPotential\":0,\"Power\":1200,\"IsDie\":false,\"IsPower\":true,\"LitmitPower\":0,\"KSkill\":[-1,-1,-1,-1,-1,-1,-1,-1,-1,-1],\"OSkill\":[4,-1,-1,-1,-1,-1,-1,-1,-1,-1],\"CSkill\":4,\"CSkillDelay\":500,\"X\":35,\"Y\":336,\"HpFrom1000\":20,\"MpFrom1000\":20,\"DamageFrom1000\":1,\"Exp\":100,\"OriginalHp\":100,\"OriginalMp\":100,\"OriginalDamage\":15,\"OriginalDefence\":0,\"OriginalCrit\":0,\"Hp\":100,\"Mp\":100,\"Stamina\":10000,\"VIP\":0,\"MaxStamina\":10000,\"NangDong\":0,\"MountId\":-1,\"Teleport\":1,\"Gold\":5000000000,\"Diamond\":100000,\"DiamondLock\":0,\"LimitGold\":50000000000,\"LimitDiamond\":100000,\"LimitDiamondLock\":200000000,\"IsNewMember\":true,\"IsNhanBua\":false,\"PhukienPart\":-1,\"IsHavePet\":false,\"IsPremium\":false,\"ThoiGianTrungMaBu\":0,\"ThoiGianDuaHau\":0,\"TimeAutoPlay\":0,\"CountGoiRong\":0,\"Fusion\":{\"IsFusion\":false,\"IsPorata\":false,\"IsPorata2\":false,\"TimeStart\":-1,\"DelayFusion\":-1,\"TimeUse\":0},\"LockInventory\":{\"IsLock\":false,\"Pass\":-1,\"PassTemp\":-1},\"LearnSkill\":null,\"LearnSkillTemp\":null,\"ItemAmulet\":{},\"Cards\":{},\"TrainManhVo\":0,\"TrainManhHon\":0,\"SoLanGiaoDich\":0,\"ThoiGianGiaoDich\":0,\"ThoiGianChatTheGioi\":0,\"ThoiGianDoiMayChu\":0,\"HieuUngDonDanh\":true,\"EffectAuraId\":-1,\"idEff_Set_Item\":-1,\"idHat\":-1,\"PetId\":-1,\"InfoSideTask\":{\"countChange\":20,\"countMission\":-1,\"mission\":\"\",\"levelMission\":-1,\"reward\":null,\"Counting\":0,\"IdMission\":-1,\"typeMission\":-1},\"isDiemDanh\":false,\"PointQuayThuong\":0,\"PointSanBoss\":0,\"IsOutZone\":true}', '[87]', '{\"Id\":1,\"Index\":0,\"Count\":0,\"Time\":0}', 0, 0, '[]', '[]', '{\"Id\":0,\"Head\":0,\"Body\":57,\"Leg\":58,\"Bag\":-1,\"Name\":null,\"IsOnline\":true,\"Power\":1200}', -1, '[]', '2024-01-14 16:07:05', '2024-01-14 16:07:05', '{\"Id\":-1,\"Info\":\"Chưa có Nội Tại\nBấm vào để xem chi tiết\",\"Img\":5223,\"SkillId\":-1,\"Value\":0,\"nextAttackDmgPercent\":0,\"isCrit\":false}', '{\"ThucAnId\":-1,\"ThucAnTime\":0,\"CuongNo\":false,\"CuongNoTime\":0,\"BinhChuaCommeson\":false,\"BinhChuaCommesonTime\":0,\"BoHuyet\":false,\"BoHuyetTime\":0,\"XiMuoiHoaDao\":false,\"XiMuoiHoaDaoTime\":0,\"XiMuoiHoaMai\":false,\"XiMuoiHoaMaiTime\":0,\"BoKhi\":false,\"BoKhiTime\":0,\"effRongXuong\":false,\"effRongXuongTime\":0,\"GiapXen\":false,\"GiapXenTime\":0,\"AnDanh\":false,\"AnDanhTime\":0,\"CuongNo2\":false,\"CuongNoTime2\":0,\"BoHuyet2\":false,\"BoHuyetTime2\":0,\"BoKhi2\":false,\"BoKhiTime2\":0,\"GiapXen2\":false,\"GiapXenTime2\":0,\"AnDanh2\":false,\"AnDanhTime2\":0,\"MayDoCSKB\":false,\"MayDoCSKBTime\":0,\"CuCarot\":false,\"CuCarotTime\":0,\"BanhTrungThuId\":-1,\"BanhTrungThuTime\":0,\"isActiveCrit\":false,\"isEnchantCrit\":false,\"delayEnchantCrit\":-1,\"timeEnchantCrit\":-1,\"isActiveGiap\":false,\"isEnchantGiap\":false,\"delayEnchantGiap\":-1,\"timeEnchantGiap\":-1,\"KichDucX2\":false,\"KichDucX2Time\":-1,\"KichDucX5\":false,\"KichDucX5Time\":-1,\"KichDucX7\":false,\"KichDucX7Time\":-1}', 0, '{\"pickChan\":false,\"pickLe\":false,\"thoivang\":0}', '[]', '{\"Count\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"isFinish\":[false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false],\"isCollect\":[false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false]}', '{\"Round\":0,\"Battled\":false,\"isCollected\":false,\"ChestLevel\":0,\"Count\":0}', '{\"DataTapLuyenn\":{\"isPractice\":false,\"currentTimePractice\":-1},\"Potenial\":-1,\"MapTraning\":-1,\"OldMap\":null,\"isTraining\":false,\"lastTimeLogout\":-1,\"Level\":0,\"DataWhis\":{\"Level\":1,\"Time\":-1,\"Count\":0,\"TimeStart\":-1}}', '{\"Ticket\":3,\"Top\":0,\"History\":{\"Text\":[]},\"Gem\":100,\"point\":{\"HP\":-1,\"SD\":-1,\"Amor\":-1,\"Win\":0}}', NULL);
INSERT INTO `character` VALUES (10008, 'dfdfdfdfdf', '[{\"Id\":2,\"SkillId\":14,\"CoolDown\":0,\"Point\":1,\"CurrExp\":0}]', '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":1,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":47,\"Param\":2}]},{\"IndexUI\":1,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":7,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":6,\"Param\":20}]},null,null,null,null,null,null,null,null,null,null]', '[]', '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":12,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":14,\"Param\":1}]}]', '{\"NClass\":1,\"Gender\":1,\"MapId\":0,\"MapCustomId\":-1,\"ZoneId\":0,\"Hair\":9,\"Bag\":-1,\"Level\":0,\"Speed\":6,\"Pk\":0,\"TypePk\":0,\"Potential\":0,\"TotalPotential\":0,\"Power\":1200,\"IsDie\":false,\"IsPower\":true,\"LitmitPower\":0,\"KSkill\":[-1,-1,-1,-1,-1,-1,-1,-1,-1,-1],\"OSkill\":[2,-1,-1,-1,-1,-1,-1,-1,-1,-1],\"CSkill\":0,\"CSkillDelay\":500,\"X\":0,\"Y\":0,\"HpFrom1000\":20,\"MpFrom1000\":20,\"DamageFrom1000\":1,\"Exp\":100,\"OriginalHp\":100,\"OriginalMp\":100,\"OriginalDamage\":15,\"OriginalDefence\":0,\"OriginalCrit\":0,\"Hp\":100,\"Mp\":100,\"Stamina\":10000,\"VIP\":0,\"MaxStamina\":10000,\"NangDong\":0,\"MountId\":-1,\"Teleport\":1,\"Gold\":5000000000,\"Diamond\":1500,\"DiamondLock\":0,\"LimitGold\":50000000000,\"LimitDiamond\":100000,\"LimitDiamondLock\":200000000,\"IsNewMember\":true,\"IsNhanBua\":false,\"PhukienPart\":-1,\"IsHavePet\":false,\"IsPremium\":false,\"ThoiGianTrungMaBu\":0,\"ThoiGianDuaHau\":0,\"TimeAutoPlay\":0,\"CountGoiRong\":0,\"Fusion\":{\"IsFusion\":false,\"IsPorata\":false,\"IsPorata2\":false,\"TimeStart\":-1,\"DelayFusion\":-1,\"TimeUse\":0},\"LockInventory\":{\"IsLock\":false,\"Pass\":-1,\"PassTemp\":-1},\"LearnSkill\":null,\"LearnSkillTemp\":null,\"ItemAmulet\":{},\"Cards\":{},\"TrainManhVo\":0,\"TrainManhHon\":0,\"SoLanGiaoDich\":0,\"ThoiGianGiaoDich\":0,\"ThoiGianChatTheGioi\":0,\"ThoiGianDoiMayChu\":0,\"HieuUngDonDanh\":true,\"EffectAuraId\":-1,\"idEff_Set_Item\":-1,\"idHat\":-1,\"PetId\":-1,\"InfoSideTask\":{\"countChange\":20,\"countMission\":-1,\"mission\":\"\",\"levelMission\":-1,\"reward\":null,\"Counting\":0,\"IdMission\":-1,\"typeMission\":-1},\"isDiemDanh\":false,\"PointQuayThuong\":0,\"PointSanBoss\":0,\"IsOutZone\":false}', '[79]', '{\"Id\":1,\"Index\":0,\"Count\":0,\"Time\":0}', 0, 0, '[]', '[]', '{\"Id\":0,\"Head\":0,\"Body\":57,\"Leg\":58,\"Bag\":-1,\"Name\":null,\"IsOnline\":true,\"Power\":1200}', -1, '[]', '2024-01-20 09:09:40', '2024-01-20 09:09:40', '{\"Id\":-1,\"Info\":\"Chưa có Nội Tại\nBấm vào để xem chi tiết\",\"Img\":5223,\"SkillId\":-1,\"Value\":0,\"nextAttackDmgPercent\":0,\"isCrit\":false}', '{\"ThucAnId\":-1,\"ThucAnTime\":0,\"CuongNo\":false,\"CuongNoTime\":0,\"BinhChuaCommeson\":false,\"BinhChuaCommesonTime\":0,\"BoHuyet\":false,\"BoHuyetTime\":0,\"XiMuoiHoaDao\":false,\"XiMuoiHoaDaoTime\":0,\"XiMuoiHoaMai\":false,\"XiMuoiHoaMaiTime\":0,\"BoKhi\":false,\"BoKhiTime\":0,\"effRongXuong\":false,\"effRongXuongTime\":0,\"GiapXen\":false,\"GiapXenTime\":0,\"AnDanh\":false,\"AnDanhTime\":0,\"CuongNo2\":false,\"CuongNoTime2\":0,\"BoHuyet2\":false,\"BoHuyetTime2\":0,\"BoKhi2\":false,\"BoKhiTime2\":0,\"GiapXen2\":false,\"GiapXenTime2\":0,\"AnDanh2\":false,\"AnDanhTime2\":0,\"MayDoCSKB\":false,\"MayDoCSKBTime\":0,\"CuCarot\":false,\"CuCarotTime\":0,\"BanhTrungThuId\":-1,\"BanhTrungThuTime\":0,\"isActiveCrit\":false,\"isEnchantCrit\":false,\"delayEnchantCrit\":-1,\"timeEnchantCrit\":-1,\"isActiveGiap\":false,\"isEnchantGiap\":false,\"delayEnchantGiap\":-1,\"timeEnchantGiap\":-1,\"KichDucX2\":false,\"KichDucX2Time\":-1,\"KichDucX5\":false,\"KichDucX5Time\":-1,\"KichDucX7\":false,\"KichDucX7Time\":-1}', 0, '{\"pickChan\":false,\"pickLe\":false,\"thoivang\":0}', '[]', '{\"Count\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"isFinish\":[false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false],\"isCollect\":[false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false]}', '{\"Round\":0,\"Battled\":false,\"isCollected\":false,\"ChestLevel\":0,\"Count\":0}', '{\"DataTapLuyenn\":{\"isPractice\":false,\"currentTimePractice\":-1},\"Potenial\":-1,\"MapTraning\":-1,\"OldMap\":null,\"isTraining\":false,\"lastTimeLogout\":-1,\"Level\":0,\"DataWhis\":{\"Level\":1,\"Time\":-1,\"Count\":0,\"TimeStart\":-1}}', '{\"Ticket\":3,\"Top\":0,\"History\":{\"Text\":[]},\"Gem\":100,\"point\":{\"HP\":-1,\"SD\":-1,\"Amor\":-1,\"Win\":0}}', NULL);
INSERT INTO `character` VALUES (10009, 'dfdfdfdfdfdf', '[{\"Id\":4,\"SkillId\":28,\"CoolDown\":0,\"Point\":1,\"CurrExp\":0}]', '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":2,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":47,\"Param\":3}]},{\"IndexUI\":1,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":8,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":6,\"Param\":20}]},null,null,null,null,null,null,null,null,null,null]', '[]', '[{\"IndexUI\":0,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":12,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":14,\"Param\":1}]}]', '{\"NClass\":2,\"Gender\":2,\"MapId\":23,\"MapCustomId\":-1,\"ZoneId\":0,\"Hair\":27,\"Bag\":-1,\"Level\":0,\"Speed\":6,\"Pk\":0,\"TypePk\":0,\"Potential\":0,\"TotalPotential\":0,\"Power\":1200,\"IsDie\":false,\"IsPower\":true,\"LitmitPower\":0,\"KSkill\":[-1,-1,-1,-1,-1,-1,-1,-1,-1,-1],\"OSkill\":[4,-1,-1,-1,-1,-1,-1,-1,-1,-1],\"CSkill\":4,\"CSkillDelay\":500,\"X\":196,\"Y\":336,\"HpFrom1000\":20,\"MpFrom1000\":20,\"DamageFrom1000\":1,\"Exp\":100,\"OriginalHp\":100,\"OriginalMp\":100,\"OriginalDamage\":15,\"OriginalDefence\":0,\"OriginalCrit\":0,\"Hp\":100,\"Mp\":100,\"Stamina\":10000,\"VIP\":0,\"MaxStamina\":10000,\"NangDong\":0,\"MountId\":-1,\"Teleport\":1,\"Gold\":5000000000,\"Diamond\":50000,\"DiamondLock\":1500,\"LimitGold\":50000000000,\"LimitDiamond\":100000,\"LimitDiamondLock\":200000000,\"IsNewMember\":true,\"IsNhanBua\":false,\"PhukienPart\":-1,\"IsHavePet\":true,\"IsPremium\":false,\"ThoiGianTrungMaBu\":0,\"ThoiGianDuaHau\":0,\"TimeAutoPlay\":0,\"CountGoiRong\":0,\"Fusion\":{\"IsFusion\":false,\"IsPorata\":false,\"IsPorata2\":false,\"TimeStart\":-1,\"DelayFusion\":-1,\"TimeUse\":0},\"LockInventory\":{\"IsLock\":false,\"Pass\":-1,\"PassTemp\":-1},\"LearnSkill\":null,\"LearnSkillTemp\":null,\"ItemAmulet\":{},\"Cards\":{},\"TrainManhVo\":0,\"TrainManhHon\":0,\"SoLanGiaoDich\":0,\"ThoiGianGiaoDich\":0,\"ThoiGianChatTheGioi\":0,\"ThoiGianDoiMayChu\":0,\"HieuUngDonDanh\":true,\"EffectAuraId\":-1,\"idEff_Set_Item\":-1,\"idHat\":-1,\"PetId\":-1,\"InfoSideTask\":{\"countChange\":20,\"countMission\":-1,\"mission\":\"\",\"levelMission\":-1,\"reward\":null,\"Counting\":0,\"IdMission\":-1,\"typeMission\":-1},\"isDiemDanh\":false,\"PointQuayThuong\":0,\"PointSanBoss\":0,\"IsOutZone\":true}', '[87]', '{\"Id\":1,\"Index\":0,\"Count\":0,\"Time\":0}', 0, 0, '[]', '[]', '{\"Id\":0,\"Head\":0,\"Body\":57,\"Leg\":58,\"Bag\":-1,\"Name\":null,\"IsOnline\":true,\"Power\":1200}', -1, '[]', '2024-01-20 09:11:41', '2024-01-20 09:11:41', '{\"Id\":-1,\"Info\":\"Chưa có Nội Tại\nBấm vào để xem chi tiết\",\"Img\":5223,\"SkillId\":-1,\"Value\":0,\"nextAttackDmgPercent\":0,\"isCrit\":false}', '{\"ThucAnId\":-1,\"ThucAnTime\":0,\"CuongNo\":false,\"CuongNoTime\":0,\"BinhChuaCommeson\":false,\"BinhChuaCommesonTime\":0,\"BoHuyet\":false,\"BoHuyetTime\":0,\"XiMuoiHoaDao\":false,\"XiMuoiHoaDaoTime\":0,\"XiMuoiHoaMai\":false,\"XiMuoiHoaMaiTime\":0,\"BoKhi\":false,\"BoKhiTime\":0,\"effRongXuong\":false,\"effRongXuongTime\":0,\"GiapXen\":false,\"GiapXenTime\":0,\"AnDanh\":false,\"AnDanhTime\":0,\"CuongNo2\":false,\"CuongNoTime2\":0,\"BoHuyet2\":false,\"BoHuyetTime2\":0,\"BoKhi2\":false,\"BoKhiTime2\":0,\"GiapXen2\":false,\"GiapXenTime2\":0,\"AnDanh2\":false,\"AnDanhTime2\":0,\"MayDoCSKB\":false,\"MayDoCSKBTime\":0,\"CuCarot\":false,\"CuCarotTime\":0,\"BanhTrungThuId\":-1,\"BanhTrungThuTime\":0,\"isActiveCrit\":false,\"isEnchantCrit\":false,\"delayEnchantCrit\":-1,\"timeEnchantCrit\":-1,\"isActiveGiap\":false,\"isEnchantGiap\":false,\"delayEnchantGiap\":-1,\"timeEnchantGiap\":-1,\"KichDucX2\":false,\"KichDucX2Time\":-1,\"KichDucX5\":false,\"KichDucX5Time\":-1,\"KichDucX7\":false,\"KichDucX7Time\":-1}', 0, '{\"pickChan\":false,\"pickLe\":false,\"thoivang\":0}', '[]', '{\"Count\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"isFinish\":[false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false],\"isCollect\":[false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false]}', '{\"Round\":0,\"Battled\":false,\"isCollected\":false,\"ChestLevel\":0,\"Count\":0}', '{\"DataTapLuyenn\":{\"isPractice\":false,\"currentTimePractice\":-1},\"Potenial\":-1,\"MapTraning\":-1,\"OldMap\":null,\"isTraining\":false,\"lastTimeLogout\":-1,\"Level\":0,\"DataWhis\":{\"Level\":1,\"Time\":-1,\"Count\":0,\"TimeStart\":-1}}', '{\"Ticket\":3,\"Top\":0,\"History\":{\"Text\":[]},\"Gem\":100,\"point\":{\"HP\":-1,\"SD\":-1,\"Amor\":-1,\"Win\":0}}', NULL);

-- ----------------------------
-- Table structure for clan
-- ----------------------------
DROP TABLE IF EXISTS `clan`;
CREATE TABLE `clan`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT '',
  `Khẩu hiệu` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT '',
  `ImgId` int NULL DEFAULT 0,
  `Điểm thành tích` bigint NULL DEFAULT 0,
  `LeaderName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT '',
  `Thành viên hiện tại` int NULL DEFAULT 0,
  `Thành viên tối đa` int NULL DEFAULT 10,
  `Thời gian tạo bang` bigint NULL DEFAULT 0,
  `Cấp độ` int NULL DEFAULT 1,
  `Capsule Bang` int NULL DEFAULT 0,
  `Thành viên` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `Messages` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `CharacterPeas` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `DataBlackBall` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `Leader` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `ClanBox` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `Điểm Danh Vọng` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `KhiGas` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `DateTime` datetime(0) NULL DEFAULT NULL,
  `shortName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 110 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of clan
-- ----------------------------
INSERT INTO `clan` VALUES (109, 'admindz', '', 26, 0, 'admin', 1, 10, 1704494694, 1, 0, '[{\"Id\":10001,\"Head\":1294,\"Body\":1295,\"Leg\":1296,\"Name\":\"admin\",\"Role\":0,\"Power\":90005068607,\"Cho_đậu\":0,\"Nhận_đậu\":0,\"Capsule_Bang\":0,\"Capsule_Cá_Nhân\":0,\"JoinTime\":1704494694,\"DateJoin\":\"2024-01-05T00:00:00\",\"LastRequest\":0,\"DelayPea\":0}]', '[]', '[]', '{\"ListCurrentBlackball\":[]}', '{\"Head\":30,\"Body\":14,\"Leg\":15}', '[]', '0', '{\"GasMaps\":[],\"timeKhiGas\":1704494694207,\"Count\":1,\"Open\":false,\"Level\":0,\"lastTimeMineCount\":-1,\"HighScore\":-1,\"TimeSetHighScore\":-1,\"TimeGoBackLangAru\":-1,\"isFinish\":false,\"LevelScore\":-1}', '2024-01-05 22:44:54', '');

-- ----------------------------
-- Table structure for diendan
-- ----------------------------
DROP TABLE IF EXISTS `diendan`;
CREATE TABLE `diendan`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `tieude` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `noidung` mediumtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `userid` int NULL DEFAULT NULL,
  `created_at` datetime(0) NULL DEFAULT NULL,
  `diendancha` int NULL DEFAULT NULL,
  `avatar` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `anh` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `admin` int NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 198 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of diendan
-- ----------------------------
INSERT INTO `diendan` VALUES (71, 'Săn cải trang HIT', '<h5>Chào các cư dân,</h5>\r\nAdmin sẽ tiến hành bảo trì tất cả máy chủ để cập nhật chuỗi Sư kiện Tân Thủ 2023.</br>\r\nThời gian sự kiện: Vô thời hạn</br>\r\n<b>1/ Gitcode Miễn Phí</b></br>\r\n- Mã Gitcode : BLUE1402</br>\r\n- Phần thưởng bao gồm:</br>\r\n+ 01 Cải trang Hit có tỷ lệ cao vĩnh viễn</br>\r\n+ 1 bộ ngọc rồng </br>\r\n- Không giới hạn số người nhập</br>\r\n<b>2/ Gitcode Đặc Biệt</b></br>\r\n- Mỗi tài khoản khi nạp lần đầu mệnh giá trên 20k,Nhận ngay 1 gitcode đặc biệt</br>\r\n<b>*Lưu ý: Gitcode trao trong vòng 24h và không quá 24h ! Xem tại lịch sử nhận gitcode</b></br>\r\n- Phần thưởng bao gồm:</br>\r\n+ 01 Cải trang Hit vĩnh viễn kèm tỷ lệ random chỉ số</br>\r\n+ 1 bộ ngọc rồng </br>\r\n+ 1 capsule 1 sao.(Chứa vật phẩm phụ kiện hot,...có tỷ lệ vĩnh viễn)</br>\r\n- Không giới hạn số người nhập trên cùng 1 nick</br></br>', 1, '2024-01-05 01:44:27', 0, 'avatar6.png', 'hit.jpg', 1);
INSERT INTO `diendan` VALUES (76, 'Chào Mừng Tân Thủ', '<h5>Chào các cư dân,</h5>\r\n<b>1/ Gitcode tân thủ </b></br>\r\n- Đăng nhập tài khoản để nhận gitcode tân thủ</br>\r\n- Phần thưởng bao gồm: 2 Gitcode tân thủ</br>\r\n- Không giới hạn số người nhập</br>\r\n- Thời gian : vĩnh viễn</br>\r\n<b>2/ Nạp Lần Đầu</b></br>\r\n- Mỗi tài khoản khi nạp lần đần bất kỳ mệnh giá nào sẽ được <b>x3</b> khi nạp.</br>\r\n<b>3/ Tính năng đổi thẻ</b></br>\r\n- Mọi người sẽ cày sự kiện trong game và quy đổi ra vật phẩm trong game hoặc ra thẻ cào điện thoại trên Web !</br>\r\n- 1 điểm sự kiện = 1,000 VNĐ</br>\r\n- Không giới hạn lượt đổi thẻ.</br>\r\n<b>4/ Về sever </b></br>\r\n- Nâng cấp máy chủ liên thông data khi Sever quá tải.</br>\r\n- Dữ liệu sẽ đăng nhập được trên mọi máy chủ 1 - 2 - 3.</br>\r\n', 1, '2024-01-05 16:44:27', 0, 'avatar10.png', 'tanthu.jpg', 1);
INSERT INTO `diendan` VALUES (95, 'EVENT X2 Cày TNSM', '<h5><b>I)Sự kiện đua top nạp</b></h5></br>\r\n<b>1/Nội Dung Đua TOP Tháng 10</b></br>\r\n -	Diễn ra từ ngày 03 -> 15 tháng 10</br>\r\n -	00h00 ngày 16 chốt top nạp vào trao giải</br>\r\n	-	Top 1 : gitcode nhận hòm quà vip 1</br>\r\n	-	Top 2 : gitcode nhận hòm quà vip 2</br>\r\n	-	Top 3 : gitcode nhận hòm quà vip 3</br>\r\n	-	Top 4 : gitcode nhận hòm quà vip 4</br>\r\n	-	Top 5 : gitcode nhận hòm quà vip 5</br>\r\n	-	Top 6 : gitcode nhận hòm quà vip 6</br>\r\n	-	Top 7 : gitcode nhận hòm quà vip 7</br>\r\n	-	Top 8 : gitcode nhận hòm quà vip 8</br>\r\n	-	Top 9 : gitcode nhận hòm quà vip 9</br>\r\n	-	Top 10 : gitcode nhận hòm quà vip 10</br></br>\r\n	<b>2/Giải thưởng</b></br>\r\n+> Hòm quà vip 1 bao gồm : 1 cải trang top 1 – 100 thỏi vàng - 2000 hồng ngọc – 5 bộ nro -1 hòm quà đồ thiên sứ - 1 hòm quà đồ thần – Danh hiệu độc quyền</br></br>\r\n+> Hòm quà vip 2 bao gồm : 1 cải trang top 2 – 60 thỏi vàng - 1500 hồng ngọc – 5 bộ nro -1 hòm quà đồ thiên sứ - 1 hòm quà đồ thần – Danh hiệu độc quyền</br></br>\r\n+> Hòm quà vip 3 bao gồm : 1 cải trang top 3 – 30 thỏi vàng - 1000 hồng ngọc – 5 bộ nro -1 hòm quà đồ thiên sứ - 1 hòm quà đồ thần – Danh hiệu độc quyền</br></br>\r\n+> Hòm quà vip 4 -> 10 bao gồm : 1 cải trang người hùng – 20 thỏi vàng - 500 hồng ngọc – 5 bộ nro -1 hòm quà đồ thiên sứ - 1 hòm quà đồ thần</br></br>\r\n\r\n\r\n<h5><b>II)Sự kiện tháng 10</b></h5></br>\r\n<b>1/Nội Dung Sự Kiện</b></br>\r\n-	Khi tiêu diệt Boss : vô cực ,super, siêu thần super sẽ rơi ra hồng ngọc và capsule vô cực.</br>\r\n-	Thời gian sự kiện : 00h Ngày 29-10</br>\r\n-	Điểm sự kiện kiếm bằng cách : Mở capsule vô cực,làm nhiệm vụ,ngũ hành sơn,....</br>\r\n-	Thời gian boss xuất hiện : 10p 1 lần tại 3 map của 3 hành tinh : rừng dương xỉ,đồi hoa tím,rừng đá</br>\r\n-	Điểm sự kiện sẽ dùng đổi quà tại NPC ở vách núi Aru or các thẻ cào đt trên web.</br>\r\n-	Điểm sự kiện sẽ reset sau 00h ngày 29 tháng 10</br>\r\n<b>2/Đổi Thưởng Thẻ Cào</b></br>\r\n- Điểm sự kiện sẽ dùng để đổi thẻ cào trên web !</br>\r\n- Mỗi 100đ = 1 điểm sự kiện.</br>\r\n- Vip 2 sẽ được đổi !</br>\r\n', 1, '2024-01-05 16:44:27', 0, '8.png', 'duatop.jpg', 1);

-- ----------------------------
-- Table structure for disciple
-- ----------------------------
DROP TABLE IF EXISTS `disciple`;
CREATE TABLE `disciple`  (
  `id` int NOT NULL,
  `Name` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT '',
  `Status` int NOT NULL DEFAULT 0,
  `Skills` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `ItemBody` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `InfoChar` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `CreateDate` datetime(0) NULL DEFAULT NULL,
  `Type` int NULL DEFAULT 1,
  `Info` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of disciple
-- ----------------------------
INSERT INTO `disciple` VALUES (-10009, 'Đệ tử', 0, '[{\"Id\":2,\"SkillId\":14,\"CoolDown\":0,\"Point\":1,\"CurrExp\":0}]', '[null,null,null,null,null,null,null,null,null,null,null,null]', '{\"NClass\":0,\"Gender\":1,\"MapId\":0,\"MapCustomId\":-1,\"ZoneId\":0,\"Hair\":0,\"Bag\":-1,\"Level\":1,\"Speed\":6,\"Pk\":0,\"TypePk\":0,\"Potential\":0,\"TotalPotential\":0,\"Power\":2000,\"IsDie\":false,\"IsPower\":true,\"LitmitPower\":0,\"KSkill\":[],\"OSkill\":[],\"CSkill\":0,\"CSkillDelay\":500,\"X\":187,\"Y\":336,\"HpFrom1000\":20,\"MpFrom1000\":20,\"DamageFrom1000\":1,\"Exp\":100,\"OriginalHp\":1989,\"OriginalMp\":1166,\"OriginalDamage\":32,\"OriginalDefence\":38,\"OriginalCrit\":2,\"Hp\":1989,\"Mp\":1166,\"Stamina\":1250,\"VIP\":0,\"MaxStamina\":1250,\"NangDong\":0,\"MountId\":-1,\"Teleport\":1,\"Gold\":0,\"Diamond\":0,\"DiamondLock\":0,\"LimitGold\":50000000000,\"LimitDiamond\":100000,\"LimitDiamondLock\":200000000,\"IsNewMember\":true,\"IsNhanBua\":false,\"PhukienPart\":-1,\"IsHavePet\":false,\"IsPremium\":false,\"ThoiGianTrungMaBu\":0,\"ThoiGianDuaHau\":0,\"TimeAutoPlay\":0,\"CountGoiRong\":0,\"Fusion\":{\"IsFusion\":false,\"IsPorata\":false,\"IsPorata2\":false,\"TimeStart\":-1,\"DelayFusion\":-1,\"TimeUse\":0},\"LockInventory\":{\"IsLock\":false,\"Pass\":-1,\"PassTemp\":-1},\"LearnSkill\":null,\"LearnSkillTemp\":null,\"ItemAmulet\":{},\"Cards\":{},\"TrainManhVo\":0,\"TrainManhHon\":0,\"SoLanGiaoDich\":0,\"ThoiGianGiaoDich\":0,\"ThoiGianChatTheGioi\":0,\"ThoiGianDoiMayChu\":0,\"HieuUngDonDanh\":true,\"EffectAuraId\":-1,\"idEff_Set_Item\":-1,\"idHat\":-1,\"PetId\":-1,\"InfoSideTask\":{\"countChange\":20,\"countMission\":-1,\"mission\":\"\",\"levelMission\":-1,\"reward\":null,\"Counting\":0,\"IdMission\":-1,\"typeMission\":-1},\"isDiemDanh\":false,\"PointQuayThuong\":0,\"PointSanBoss\":0,\"IsOutZone\":false}', '2024-01-20 09:15:22', 1, '{\"Id\":-10009,\"Head\":288,\"Body\":289,\"Leg\":290,\"Bag\":-1,\"Name\":\"Đệ tử\",\"IsOnline\":true,\"Power\":2000}');
INSERT INTO `disciple` VALUES (-10003, 'Đệ tử', 0, '[{\"Id\":4,\"SkillId\":28,\"CoolDown\":1000,\"Point\":1,\"CurrExp\":0}]', '[null,null,null,null,null,null,null,null,null,null,null,null]', '{\"NClass\":0,\"Gender\":0,\"MapId\":0,\"MapCustomId\":-1,\"ZoneId\":0,\"Hair\":0,\"Bag\":-1,\"Level\":8,\"Speed\":6,\"Pk\":0,\"TypePk\":0,\"Potential\":2876,\"TotalPotential\":0,\"Power\":2583478,\"IsDie\":false,\"IsPower\":true,\"LitmitPower\":0,\"KSkill\":[],\"OSkill\":[],\"CSkill\":0,\"CSkillDelay\":500,\"X\":364,\"Y\":336,\"HpFrom1000\":20,\"MpFrom1000\":20,\"DamageFrom1000\":1,\"Exp\":100,\"OriginalHp\":5398,\"OriginalMp\":3310,\"OriginalDamage\":172,\"OriginalDefence\":48,\"OriginalCrit\":3,\"Hp\":2061,\"Mp\":3219,\"Stamina\":49,\"VIP\":0,\"MaxStamina\":1250,\"NangDong\":0,\"MountId\":-1,\"Teleport\":1,\"Gold\":0,\"Diamond\":0,\"DiamondLock\":0,\"LimitGold\":50000000000,\"LimitDiamond\":100000,\"LimitDiamondLock\":200000000,\"IsNewMember\":true,\"IsNhanBua\":false,\"PhukienPart\":-1,\"IsHavePet\":false,\"IsPremium\":false,\"ThoiGianTrungMaBu\":0,\"ThoiGianDuaHau\":0,\"TimeAutoPlay\":0,\"CountGoiRong\":0,\"Fusion\":{\"IsFusion\":false,\"IsPorata\":false,\"IsPorata2\":false,\"TimeStart\":-1,\"DelayFusion\":-1,\"TimeUse\":0},\"LockInventory\":{\"IsLock\":false,\"Pass\":-1,\"PassTemp\":-1},\"LearnSkill\":null,\"LearnSkillTemp\":null,\"ItemAmulet\":{},\"Cards\":{},\"TrainManhVo\":0,\"TrainManhHon\":0,\"SoLanGiaoDich\":0,\"ThoiGianGiaoDich\":0,\"ThoiGianChatTheGioi\":0,\"ThoiGianDoiMayChu\":0,\"HieuUngDonDanh\":true,\"EffectAuraId\":-1,\"idEff_Set_Item\":-1,\"idHat\":-1,\"PetId\":-1,\"InfoSideTask\":{\"countChange\":20,\"countMission\":-1,\"mission\":\"\",\"levelMission\":-1,\"reward\":null,\"Counting\":0,\"IdMission\":-1,\"typeMission\":-1},\"isDiemDanh\":false,\"PointQuayThuong\":0,\"PointSanBoss\":0,\"IsOutZone\":false}', '2024-01-05 15:48:25', 1, '{\"Id\":-10003,\"Head\":304,\"Body\":286,\"Leg\":287,\"Bag\":-1,\"Name\":\"Đệ tử\",\"IsOnline\":true,\"Power\":2583478}');
INSERT INTO `disciple` VALUES (-10001, 'Đệ Tử Pic Nhạc ', 0, '[{\"Id\":4,\"SkillId\":28,\"CoolDown\":1000,\"Point\":1,\"CurrExp\":0},{\"Id\":3,\"SkillId\":21,\"CoolDown\":1000,\"Point\":1,\"CurrExp\":0},{\"Id\":8,\"SkillId\":56,\"CoolDown\":1705436043369,\"Point\":1,\"CurrExp\":0},{\"Id\":12,\"SkillId\":84,\"CoolDown\":1705436347432,\"Point\":1,\"CurrExp\":0},{\"Id\":14,\"SkillId\":98,\"CoolDown\":0,\"Point\":1,\"CurrExp\":0}]', '[null,null,null,null,{\"IndexUI\":4,\"SaleCoin\":0,\"BuyPotential\":0,\"Id\":1476,\"Vang\":0,\"Ngoc\":0,\"Quantity\":1,\"Reason\":\"\",\"Options\":[{\"Id\":14,\"Param\":25},{\"Id\":21,\"Param\":55},{\"Id\":30,\"Param\":0},{\"Id\":129,\"Param\":0},{\"Id\":141,\"Param\":0},{\"Id\":30,\"Param\":0},{\"Id\":107,\"Param\":8}]},null,null,null,null,null,null,null]', '{\"NClass\":0,\"Gender\":0,\"MapId\":0,\"MapCustomId\":-1,\"ZoneId\":0,\"Hair\":0,\"Bag\":-1,\"Level\":18,\"Speed\":6,\"Pk\":0,\"TypePk\":0,\"Potential\":28259,\"TotalPotential\":0,\"Power\":95000153380,\"IsDie\":false,\"IsPower\":false,\"LitmitPower\":0,\"KSkill\":[],\"OSkill\":[],\"CSkill\":0,\"CSkillDelay\":500,\"X\":315,\"Y\":336,\"HpFrom1000\":20,\"MpFrom1000\":20,\"DamageFrom1000\":1,\"Exp\":100,\"OriginalHp\":700681,\"OriginalMp\":605009,\"OriginalDamage\":285,\"OriginalDefence\":21,\"OriginalCrit\":2,\"Hp\":700681,\"Mp\":604905,\"Stamina\":1152,\"VIP\":0,\"MaxStamina\":1250,\"NangDong\":0,\"MountId\":-1,\"Teleport\":1,\"Gold\":0,\"Diamond\":0,\"DiamondLock\":0,\"LimitGold\":50000000000,\"LimitDiamond\":100000,\"LimitDiamondLock\":200000000,\"IsNewMember\":true,\"IsNhanBua\":false,\"PhukienPart\":-1,\"IsHavePet\":false,\"IsPremium\":false,\"ThoiGianTrungMaBu\":0,\"ThoiGianDuaHau\":0,\"TimeAutoPlay\":0,\"CountGoiRong\":0,\"Fusion\":{\"IsFusion\":false,\"IsPorata\":false,\"IsPorata2\":false,\"TimeStart\":-1,\"DelayFusion\":-1,\"TimeUse\":0},\"LockInventory\":{\"IsLock\":false,\"Pass\":-1,\"PassTemp\":-1},\"LearnSkill\":null,\"LearnSkillTemp\":null,\"ItemAmulet\":{},\"Cards\":{},\"TrainManhVo\":0,\"TrainManhHon\":0,\"SoLanGiaoDich\":0,\"ThoiGianGiaoDich\":0,\"ThoiGianChatTheGioi\":0,\"ThoiGianDoiMayChu\":0,\"HieuUngDonDanh\":true,\"EffectAuraId\":-1,\"idEff_Set_Item\":-1,\"idHat\":-1,\"PetId\":-1,\"InfoSideTask\":{\"countChange\":20,\"countMission\":-1,\"mission\":\"\",\"levelMission\":-1,\"reward\":null,\"Counting\":0,\"IdMission\":-1,\"typeMission\":-1},\"isDiemDanh\":false,\"PointQuayThuong\":0,\"PointSanBoss\":0,\"IsOutZone\":false}', '2024-01-05 16:12:41', 4, '{\"Id\":-10001,\"Head\":1239,\"Body\":1240,\"Leg\":1241,\"Bag\":-1,\"Name\":\"Đệ Tử Pic Nhạc \",\"IsOnline\":true,\"Power\":95000153380}');

-- ----------------------------
-- Table structure for doithe
-- ----------------------------
DROP TABLE IF EXISTS `doithe`;
CREATE TABLE `doithe`  (
  `id` int UNSIGNED NOT NULL AUTO_INCREMENT,
  `username` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `character_id` int NULL DEFAULT NULL,
  `character_ten` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `madonhang` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `menhgia` int NULL DEFAULT NULL,
  `nhamang` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `soluong` int NULL DEFAULT NULL,
  `mathe` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `seri` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `thoigiantaodonhang` datetime(0) NULL DEFAULT NULL,
  `trangthai` int NULL DEFAULT NULL,
  `thoigiantraothe` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 12 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of doithe
-- ----------------------------

-- ----------------------------
-- Table structure for gameinfo
-- ----------------------------
DROP TABLE IF EXISTS `gameinfo`;
CREATE TABLE `gameinfo`  (
  `id` int NULL DEFAULT NULL,
  `main` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of gameinfo
-- ----------------------------
INSERT INTO `gameinfo` VALUES (0, 'CÂY KHẾ ONLINE', 'Chào Mừng !');
INSERT INTO `gameinfo` VALUES (1, 'Chào Mừng !', 'Chào Mừng Bạn Đến Với Máy Chủ \r\nCÂY KHẾ ONLINE\r\n1/ Nhân dịp tân thủ \r\n- Nạp lần đầu sẽ x3.\r\n- Nhận ngay gitcode tân thủ.\r\n- Gitcode VIP khi nạp .\r\n2/ Sự kiện đã dạng\r\n- Săn boss nhận ngay trang bị cực phẩm.\r\n- Tham gia event trong tiktok,zalo nhận ngay gitcode hệ thống.\r\n3/ Item vật phẩm mới lạ\r\n4/ Nâng SKH thiên sứ, vô cực...\r\n5/ PK nảy nửa với bộ skill mới');

-- ----------------------------
-- Table structure for giftcode
-- ----------------------------
DROP TABLE IF EXISTS `giftcode`;
CREATE TABLE `giftcode`  (
  `code` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `count` int NULL DEFAULT 1,
  `time_expire` datetime(0) NULL DEFAULT NULL,
  `type` int NULL DEFAULT 0,
  `trangthai` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`code`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of giftcode
-- ----------------------------
INSERT INTO `giftcode` VALUES ('blue1402', 9494, '2025-01-01 12:00:00', 2, b'1');
INSERT INTO `giftcode` VALUES ('tanthu', 9523, '2025-01-01 12:00:00', 0, b'1');
INSERT INTO `giftcode` VALUES ('test', 999, '2024-01-27 20:34:42', 8, b'1');
INSERT INTO `giftcode` VALUES ('thoivang', 9340, '2025-01-01 12:00:00', 1, b'1');
INSERT INTO `giftcode` VALUES ('thucung', 9618, '2025-01-01 12:00:00', 3, b'1');
INSERT INTO `giftcode` VALUES ('tungbung', 9911, '2023-10-08 20:44:39', 4, b'1');

-- ----------------------------
-- Table structure for giftcode_used
-- ----------------------------
DROP TABLE IF EXISTS `giftcode_used`;
CREATE TABLE `giftcode_used`  (
  `code` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `character` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `time_used` datetime(0) NULL DEFAULT NULL,
  `type` int NULL DEFAULT NULL,
  PRIMARY KEY (`code`, `character`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of giftcode_used
-- ----------------------------
INSERT INTO `giftcode_used` VALUES ('blue1402', 'admin', '2024-01-16 23:48:52', 2);

-- ----------------------------
-- Table structure for giftcodeht
-- ----------------------------
DROP TABLE IF EXISTS `giftcodeht`;
CREATE TABLE `giftcodeht`  (
  `code` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `count` int NULL DEFAULT NULL,
  `time_expire` datetime(0) NULL DEFAULT NULL,
  `type` int NULL DEFAULT NULL,
  `trangthai` bit(1) NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of giftcodeht
-- ----------------------------

-- ----------------------------
-- Table structure for giftcodeht_used
-- ----------------------------
DROP TABLE IF EXISTS `giftcodeht_used`;
CREATE TABLE `giftcodeht_used`  (
  `code` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `character` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `time_used` datetime(0) NULL DEFAULT NULL,
  `type` int NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of giftcodeht_used
-- ----------------------------

-- ----------------------------
-- Table structure for giftcodett
-- ----------------------------
DROP TABLE IF EXISTS `giftcodett`;
CREATE TABLE `giftcodett`  (
  `code` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `count` int NULL DEFAULT NULL,
  `time_expire` datetime(0) NULL DEFAULT NULL,
  `type` int NULL DEFAULT NULL,
  `trangthai` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`code`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of giftcodett
-- ----------------------------

-- ----------------------------
-- Table structure for giftcodett_used
-- ----------------------------
DROP TABLE IF EXISTS `giftcodett_used`;
CREATE TABLE `giftcodett_used`  (
  `code` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `character` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `time_used` datetime(0) NULL DEFAULT NULL,
  `type` int NULL DEFAULT NULL,
  `user_id` int NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of giftcodett_used
-- ----------------------------

-- ----------------------------
-- Table structure for magictree
-- ----------------------------
DROP TABLE IF EXISTS `magictree`;
CREATE TABLE `magictree`  (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `idNpc` int UNSIGNED NOT NULL DEFAULT 0,
  `x` int NULL DEFAULT 0,
  `y` int NULL DEFAULT 0,
  `level` int NULL DEFAULT 1,
  `peas` int NULL DEFAULT 5,
  `maxPea` int NULL DEFAULT 5,
  `seconds` bigint NULL DEFAULT 0,
  `isUpdating` int NULL DEFAULT 0,
  `Diamond` int NULL DEFAULT 0,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 10010 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of magictree
-- ----------------------------
INSERT INTO `magictree` VALUES (10001, 90, 348, 336, 9, 21, 21, 0, 0, 0);
INSERT INTO `magictree` VALUES (10002, 378, 372, 336, 1, 5, 5, 0, 0, 0);
INSERT INTO `magictree` VALUES (10003, 371, 372, 336, 1, 5, 5, 0, 0, 0);
INSERT INTO `magictree` VALUES (10004, 378, 372, 336, 1, 5, 5, 0, 0, 0);
INSERT INTO `magictree` VALUES (10005, 84, 348, 336, 1, 5, 5, 0, 0, 0);
INSERT INTO `magictree` VALUES (10006, 371, 372, 336, 1, 5, 5, 0, 0, 0);
INSERT INTO `magictree` VALUES (10007, 378, 372, 336, 1, 5, 5, 0, 0, 0);
INSERT INTO `magictree` VALUES (10008, 371, 372, 336, 1, 5, 5, 0, 0, 0);
INSERT INTO `magictree` VALUES (10009, 378, 372, 336, 1, 5, 5, 0, 0, 0);

-- ----------------------------
-- Table structure for mapriviu
-- ----------------------------
DROP TABLE IF EXISTS `mapriviu`;
CREATE TABLE `mapriviu`  (
  `ID` int NOT NULL,
  `Namemap` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of mapriviu
-- ----------------------------
INSERT INTO `mapriviu` VALUES (0, 'Làng Aru');
INSERT INTO `mapriviu` VALUES (1, 'Đồi hoa cúc');
INSERT INTO `mapriviu` VALUES (2, 'Thung lũng tre');
INSERT INTO `mapriviu` VALUES (3, 'Rừng nấm');
INSERT INTO `mapriviu` VALUES (4, 'Rừng xương');
INSERT INTO `mapriviu` VALUES (5, 'Đảo Kamê');
INSERT INTO `mapriviu` VALUES (6, 'Đông Karin');
INSERT INTO `mapriviu` VALUES (7, 'Làng Mori');
INSERT INTO `mapriviu` VALUES (8, 'Đồi nấm tím');
INSERT INTO `mapriviu` VALUES (9, 'Thị trấn Moori');
INSERT INTO `mapriviu` VALUES (10, 'Thung lũng Namếc');
INSERT INTO `mapriviu` VALUES (11, 'Thung lũng Maima');
INSERT INTO `mapriviu` VALUES (12, 'Vực maima');
INSERT INTO `mapriviu` VALUES (13, 'Đảo Guru');
INSERT INTO `mapriviu` VALUES (14, 'Làng Kakarot');
INSERT INTO `mapriviu` VALUES (15, 'Đồi hoang');
INSERT INTO `mapriviu` VALUES (16, 'Làng Plant');
INSERT INTO `mapriviu` VALUES (17, 'Rừng nguyên sinh');
INSERT INTO `mapriviu` VALUES (18, 'Rừng thông Xayda');
INSERT INTO `mapriviu` VALUES (19, 'Thành phố Vegeta');
INSERT INTO `mapriviu` VALUES (20, 'Vách núi đen');
INSERT INTO `mapriviu` VALUES (21, 'Nhà Gôhan');
INSERT INTO `mapriviu` VALUES (22, 'Nhà Moori');
INSERT INTO `mapriviu` VALUES (23, 'Nhà Broly');
INSERT INTO `mapriviu` VALUES (24, 'Trạm tàu vũ trụ');
INSERT INTO `mapriviu` VALUES (25, 'Trạm tàu vũ trụ');
INSERT INTO `mapriviu` VALUES (26, 'Trạm tàu vũ trụ');
INSERT INTO `mapriviu` VALUES (27, 'Rừng Bamboo');
INSERT INTO `mapriviu` VALUES (28, 'Rừng dương xỉ');
INSERT INTO `mapriviu` VALUES (29, 'Nam Kamê');
INSERT INTO `mapriviu` VALUES (30, 'Đảo Bulông');
INSERT INTO `mapriviu` VALUES (31, 'Núi hoa vàng');
INSERT INTO `mapriviu` VALUES (32, 'Núi hoa tím');
INSERT INTO `mapriviu` VALUES (33, 'Nam Guru');
INSERT INTO `mapriviu` VALUES (34, 'Đông Nam Guru');
INSERT INTO `mapriviu` VALUES (35, 'Rừng Cọ');
INSERT INTO `mapriviu` VALUES (36, 'Rừng đá');
INSERT INTO `mapriviu` VALUES (37, 'Thung lũng đen');
INSERT INTO `mapriviu` VALUES (38, 'Bờ vực đen');
INSERT INTO `mapriviu` VALUES (39, 'Vách núi Aru');
INSERT INTO `mapriviu` VALUES (40, 'Vách núi Moori');
INSERT INTO `mapriviu` VALUES (41, 'Vực Plant');
INSERT INTO `mapriviu` VALUES (42, 'Vách núi Aru');
INSERT INTO `mapriviu` VALUES (43, 'Vách núi Moori');
INSERT INTO `mapriviu` VALUES (44, 'Vách núi Kakarot');
INSERT INTO `mapriviu` VALUES (45, 'Thần điện');
INSERT INTO `mapriviu` VALUES (46, 'Tháp Karin');
INSERT INTO `mapriviu` VALUES (47, 'Rừng Karin');
INSERT INTO `mapriviu` VALUES (48, 'Hành tinh Kaio');
INSERT INTO `mapriviu` VALUES (49, 'Phòng tập thời gian');
INSERT INTO `mapriviu` VALUES (50, 'Thánh địa kiao');
INSERT INTO `mapriviu` VALUES (51, 'Đấu trường');
INSERT INTO `mapriviu` VALUES (52, 'Đại hội võ thuật');
INSERT INTO `mapriviu` VALUES (53, 'Tường thành 1');
INSERT INTO `mapriviu` VALUES (54, 'Tầng 3');
INSERT INTO `mapriviu` VALUES (55, 'Tầng 1');
INSERT INTO `mapriviu` VALUES (56, 'Tầng 2');
INSERT INTO `mapriviu` VALUES (57, 'Tầng 4');
INSERT INTO `mapriviu` VALUES (58, 'Tường thành 2');
INSERT INTO `mapriviu` VALUES (59, 'Tường thành 3');
INSERT INTO `mapriviu` VALUES (60, 'Trại độc nhãn 1');
INSERT INTO `mapriviu` VALUES (61, 'Trại độc nhãn 2');
INSERT INTO `mapriviu` VALUES (62, 'Trại độc nhãn 3');
INSERT INTO `mapriviu` VALUES (63, 'Trại lính Fide');
INSERT INTO `mapriviu` VALUES (64, 'Núi dây leo');
INSERT INTO `mapriviu` VALUES (65, 'Núi cây quỷ');
INSERT INTO `mapriviu` VALUES (66, 'Trại quỷ già');
INSERT INTO `mapriviu` VALUES (67, 'Vực chết');
INSERT INTO `mapriviu` VALUES (68, 'Thung lũng Nappa');
INSERT INTO `mapriviu` VALUES (69, 'Vực cấm');
INSERT INTO `mapriviu` VALUES (70, 'Núi Appule');
INSERT INTO `mapriviu` VALUES (71, 'Căn cứ Raspberry');
INSERT INTO `mapriviu` VALUES (72, 'Thung lũng Raspberry');
INSERT INTO `mapriviu` VALUES (73, 'Thung lũng chết');
INSERT INTO `mapriviu` VALUES (74, 'Đồi cây Fide');
INSERT INTO `mapriviu` VALUES (75, 'Khe núi tử thần');
INSERT INTO `mapriviu` VALUES (76, 'Núi đá');
INSERT INTO `mapriviu` VALUES (77, 'Rừng đá');
INSERT INTO `mapriviu` VALUES (78, 'Lãnh địa Fize');
INSERT INTO `mapriviu` VALUES (79, 'Núi khỉ đỏ');
INSERT INTO `mapriviu` VALUES (80, 'Núi khỉ vàng');
INSERT INTO `mapriviu` VALUES (81, 'Hang quỷ chim');
INSERT INTO `mapriviu` VALUES (82, 'Núi khỉ đen');
INSERT INTO `mapriviu` VALUES (83, 'Hang khỉ đen');
INSERT INTO `mapriviu` VALUES (84, 'Siêu Thị');
INSERT INTO `mapriviu` VALUES (85, 'Hành tinh M-2');
INSERT INTO `mapriviu` VALUES (86, 'Hành tinh Polaris');
INSERT INTO `mapriviu` VALUES (87, 'Hành tinh Cretaceous');
INSERT INTO `mapriviu` VALUES (88, 'Hành tinh Monmaasu');
INSERT INTO `mapriviu` VALUES (89, 'Hành tinh Rudeeze');
INSERT INTO `mapriviu` VALUES (90, 'Hành tinh Gelbo');
INSERT INTO `mapriviu` VALUES (91, 'Hành tinh Tigere');
INSERT INTO `mapriviu` VALUES (92, 'Thành phố phía đông');
INSERT INTO `mapriviu` VALUES (93, 'Thành phố phía nam');
INSERT INTO `mapriviu` VALUES (94, 'Đảo Balê');
INSERT INTO `mapriviu` VALUES (95, '95');
INSERT INTO `mapriviu` VALUES (96, 'Cao nguyên');
INSERT INTO `mapriviu` VALUES (97, 'Thành phố phía bắc');
INSERT INTO `mapriviu` VALUES (98, 'Ngọn núi phía bắc');
INSERT INTO `mapriviu` VALUES (99, 'Thung lũng phía bắc');
INSERT INTO `mapriviu` VALUES (100, 'Thị trấn Ginder');
INSERT INTO `mapriviu` VALUES (101, '101');
INSERT INTO `mapriviu` VALUES (102, 'Nhà Bunma');
INSERT INTO `mapriviu` VALUES (103, 'Võ đài Xên bọ hung');
INSERT INTO `mapriviu` VALUES (104, 'Sân sau siêu thị');
INSERT INTO `mapriviu` VALUES (105, 'Cánh đồng tuyết');
INSERT INTO `mapriviu` VALUES (106, 'Rừng tuyết');
INSERT INTO `mapriviu` VALUES (107, 'Núi tuyết');
INSERT INTO `mapriviu` VALUES (108, 'Dòng sông băng');
INSERT INTO `mapriviu` VALUES (109, 'Rừng băng');
INSERT INTO `mapriviu` VALUES (110, 'Hang băng');
INSERT INTO `mapriviu` VALUES (111, 'Đông Nam Karin');
INSERT INTO `mapriviu` VALUES (112, 'Võ đài Hạt Mít');
INSERT INTO `mapriviu` VALUES (113, 'Đại hội võ thuật');
INSERT INTO `mapriviu` VALUES (114, 'Cổng Phi Thuyền');
INSERT INTO `mapriviu` VALUES (115, 'Phòng chờ');
INSERT INTO `mapriviu` VALUES (116, 'Thánh địa Kaio');
INSERT INTO `mapriviu` VALUES (117, 'Cửa Ải 1');
INSERT INTO `mapriviu` VALUES (118, 'Cửa Ải 2');
INSERT INTO `mapriviu` VALUES (119, 'Cửa Ải 3');
INSERT INTO `mapriviu` VALUES (120, 'Phòng chỉ Huy');
INSERT INTO `mapriviu` VALUES (121, 'Đấu trường');
INSERT INTO `mapriviu` VALUES (122, 'Ngũ Hành Sơn');
INSERT INTO `mapriviu` VALUES (123, 'Ngũ Hành Sơn');
INSERT INTO `mapriviu` VALUES (124, 'Ngũ Hành Sơn');
INSERT INTO `mapriviu` VALUES (125, 'Võ đài Bang');
INSERT INTO `mapriviu` VALUES (126, 'Thành phố Santa');
INSERT INTO `mapriviu` VALUES (127, 'Cổng Phi Thuyền');
INSERT INTO `mapriviu` VALUES (128, 'Bụng Mabư');
INSERT INTO `mapriviu` VALUES (129, 'Đại hội võ thuật');
INSERT INTO `mapriviu` VALUES (130, 'Đại hội võ thuật Vũ Trụ');
INSERT INTO `mapriviu` VALUES (131, 'Hành tinh Yarrdart');
INSERT INTO `mapriviu` VALUES (132, 'Hành tinh Yarrdart 2');
INSERT INTO `mapriviu` VALUES (133, 'Hành tinh Yarrdart 3');
INSERT INTO `mapriviu` VALUES (134, 'Đại hội võ thuật vũ trụ 6-7');
INSERT INTO `mapriviu` VALUES (135, 'Động hải tặc');
INSERT INTO `mapriviu` VALUES (136, 'Hang bạch tuộc');
INSERT INTO `mapriviu` VALUES (137, 'Động kho báu');
INSERT INTO `mapriviu` VALUES (138, 'Cảng hải tặc');
INSERT INTO `mapriviu` VALUES (139, 'Hành tinh Potaufeu');
INSERT INTO `mapriviu` VALUES (140, 'Hang động Potaufeu');
INSERT INTO `mapriviu` VALUES (141, 'Con đường rắn độc');
INSERT INTO `mapriviu` VALUES (142, 'Con đường rắn độc');
INSERT INTO `mapriviu` VALUES (143, 'Con đường rắn độc');
INSERT INTO `mapriviu` VALUES (144, 'Hoang Mạc');
INSERT INTO `mapriviu` VALUES (145, 'Võ Đài Siêu Cấp');
INSERT INTO `mapriviu` VALUES (146, 'Tây Karin');
INSERT INTO `mapriviu` VALUES (147, 'Sa mạc');
INSERT INTO `mapriviu` VALUES (148, 'Lâu đài Lychee');
INSERT INTO `mapriviu` VALUES (149, 'Thành phố Santa');
INSERT INTO `mapriviu` VALUES (150, 'Lôi Đài');
INSERT INTO `mapriviu` VALUES (151, 'Hành tinh bóng tối');
INSERT INTO `mapriviu` VALUES (152, 'Vùng đất băng giá');
INSERT INTO `mapriviu` VALUES (153, 'Lãnh địa bang hội');
INSERT INTO `mapriviu` VALUES (154, 'Hành tinh Bill');
INSERT INTO `mapriviu` VALUES (155, 'Hành tinh ngục tù');
INSERT INTO `mapriviu` VALUES (156, 'Tây thánh địa');
INSERT INTO `mapriviu` VALUES (157, 'Đông thánh Địa');
INSERT INTO `mapriviu` VALUES (158, 'Bắc thánh địa');
INSERT INTO `mapriviu` VALUES (159, 'Nam thánh Địa');
INSERT INTO `mapriviu` VALUES (160, 'Khu hang động');
INSERT INTO `mapriviu` VALUES (161, 'Bìa rừng nguyên thủy');
INSERT INTO `mapriviu` VALUES (162, 'Rừng nguyên thủy');
INSERT INTO `mapriviu` VALUES (163, 'Làng Plant nguyên thủy');
INSERT INTO `mapriviu` VALUES (164, 'Tranh ngọc Namếc');
INSERT INTO `mapriviu` VALUES (165, 'Boss bang hội');

-- ----------------------------
-- Table structure for napcard
-- ----------------------------
DROP TABLE IF EXISTS `napcard`;
CREATE TABLE `napcard`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `uid` int NULL DEFAULT NULL,
  `telco` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `menhgia` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `seri` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `code` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `magd` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `status` int NULL DEFAULT NULL,
  `created_at` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 158 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of napcard
-- ----------------------------

-- ----------------------------
-- Table structure for naptien
-- ----------------------------
DROP TABLE IF EXISTS `naptien`;
CREATE TABLE `naptien`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `amount` int NOT NULL,
  `time` timestamp(0) NOT NULL DEFAULT current_timestamp(0),
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 34 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of naptien
-- ----------------------------

-- ----------------------------
-- Table structure for quahethong
-- ----------------------------
DROP TABLE IF EXISTS `quahethong`;
CREATE TABLE `quahethong`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `uid` int NULL DEFAULT NULL,
  `code` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `thoigiannhan` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 91 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of quahethong
-- ----------------------------

-- ----------------------------
-- Table structure for regexchat
-- ----------------------------
DROP TABLE IF EXISTS `regexchat`;
CREATE TABLE `regexchat`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `text` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `id`(`id`) USING BTREE,
  INDEX `id_2`(`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 8 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of regexchat
-- ----------------------------
INSERT INTO `regexchat` VALUES (1, 'dm');
INSERT INTO `regexchat` VALUES (2, 'địt');
INSERT INTO `regexchat` VALUES (3, 'djt');
INSERT INTO `regexchat` VALUES (4, 'fuck');
INSERT INTO `regexchat` VALUES (5, 'lồn');
INSERT INTO `regexchat` VALUES (6, 'buồi');
INSERT INTO `regexchat` VALUES (7, 'cặc');

-- ----------------------------
-- Table structure for tanthu
-- ----------------------------
DROP TABLE IF EXISTS `tanthu`;
CREATE TABLE `tanthu`  (
  `ID` int NOT NULL AUTO_INCREMENT,
  `tieude` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `noidung` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 18 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of tanthu
-- ----------------------------
INSERT INTO `tanthu` VALUES (1, 'BOX ZALO', '<a href=\"https://zalo.me/g/tfwoux545\"><b>box 1: https://zalo.me/g/tfwoux545</b></a></br></br>\r\n<a href=\"https://zalo.me/g/gmdvto522\"><b>box 2: https://zalo.me/g/gmdvto522</b></a>');
INSERT INTO `tanthu` VALUES (2, 'Nhập Gitcode', 'Rừng Karin -> NPC : Tara -> Nhập Gitcode \r\n');
INSERT INTO `tanthu` VALUES (3, 'Nhập Gitcode', 'Rừng Karin -> NPC Tara');
INSERT INTO `tanthu` VALUES (4, 'Nhận Đệ Tử Vip, Ấp Trứng Pet', 'Sân sau siêu thị');
INSERT INTO `tanthu` VALUES (5, 'Up Đồ Ăn', 'Mặc đủ set 5 món thần linh -> Sang Cold up');
INSERT INTO `tanthu` VALUES (6, 'Up SKH', 'Quoái tại 3 map đầu');
INSERT INTO `tanthu` VALUES (7, 'Nâng VIP', 'Nhận 1 tỷ vàng + 20/50/100 + Hòm quà Vip theo cấp.</br>*Lưu ý : Vip 2 trở lên sẽ nhận thẻ Vip !');
INSERT INTO `tanthu` VALUES (8, 'Kiếm hồng ngọc', 'Làm Nhiệm Vụ, Săn Boss, Đại Hội Võ Thuật, Nạp,...');
INSERT INTO `tanthu` VALUES (9, 'Nâng SKH Thiên Sứ', 'Nâng tại Thần SKH đảo kame\r\n');
INSERT INTO `tanthu` VALUES (10, 'Nâng SKH Vô Cực', 'Nâng tại Sân sau siêu thị\r\n');
INSERT INTO `tanthu` VALUES (11, 'Đổi Vàng , Hồng Ngọc Nạp', 'Tháp Krin -> Bò Mộng -> Đổi vàng , ngọc\r\n</br>\r\n* Lưu Ý : Đổi xong out game vào lại cập nhật vật phẩm !');
INSERT INTO `tanthu` VALUES (12, 'Up Mảnh Thiên Sứ', 'Mặc set hủy diệt -> Hành tinh ngục tù,</br>\r\n<b>Các map thực vật như :</b></br>\r\nKhu hang động</br>\r\nBìa rừng nguyên thủy</br>\r\nRừng nguyên thủy</br>\r\nLàng Plant nguyên thủy</br>');
INSERT INTO `tanthu` VALUES (13, 'Boss rơi hồng ngọc', 'Boss sự kiện ở diễn đàn, Super broly,...');
INSERT INTO `tanthu` VALUES (14, 'Đối Với Phiển Bản APK', 'Mod Koi tạo nhân vật mới trên APK bị lỗi</br> ae tải kèm theo 1 bản apk khác nữa </br>tạo nhân vật là chơi bình thường nhé !');
INSERT INTO `tanthu` VALUES (15, 'Nâng ẤN', '- Tinh ấn tăng DAME\r\n- Nhật ấn tăng MP\r\n- Nguyệt ấn tăng HP');
INSERT INTO `tanthu` VALUES (16, '-', 'Đang Update !');

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user`  (
  `id` int UNSIGNED NOT NULL AUTO_INCREMENT,
  `username` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT '',
  `password` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT '',
  `character` bigint NULL DEFAULT 0,
  `active` tinyint NULL DEFAULT 0,
  `role` int NULL DEFAULT 0,
  `ban` tinyint NULL DEFAULT 0,
  `online` tinyint NULL DEFAULT 0,
  `created_at` timestamp(0) NULL DEFAULT NULL,
  `updated_at` timestamp(0) NULL DEFAULT NULL,
  `sdt` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `vnd` int NOT NULL DEFAULT 0,
  `tongnap` int NOT NULL DEFAULT 0,
  `email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `diemtichnap` int NOT NULL DEFAULT 0,
  `sv_port` int NOT NULL DEFAULT 14445,
  `logout_time` bigint NOT NULL DEFAULT 0,
  `last_ip` varchar(24) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `is_login` tinyint NULL DEFAULT 0,
  `thoivang` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT '0',
  `hongngoc` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT '0',
  `admin` int NULL DEFAULT 0,
  `trangthai` int NULL DEFAULT NULL,
  `gitcodetrao` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `vip` int NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `character`(`character`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 12 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES (1, 'admin', '1', 10001, 1, 1, 0, 0, '2023-09-08 08:18:37', '2024-01-05 00:52:32', '0382002295', 81000000, 0, 'admin@gmail.com', 0, 14445, 1705783507, '127.0.0.1', 0, '100', '0', 0, 0, NULL, 0);
INSERT INTO `user` VALUES (3, 'thanhdat', '1', 10003, 1, 1, 0, 0, '2024-01-04 22:09:29', NULL, '0763063382', 0, 0, 'tuyenkiet39@gmail.com', 0, 14445, 1705440682, '127.0.0.1', 0, '0', '0', 0, 0, NULL, 0);
INSERT INTO `user` VALUES (6, '3', '1', 10007, 1, 1, 0, 0, '2024-01-04 22:12:25', NULL, '0392901603', 0, 0, 'bongzip@gmail.com', 0, 14445, 1705248460, '127.0.0.1', 0, '0', '0', 0, 0, NULL, 0);
INSERT INTO `user` VALUES (7, '1', '1', 10005, 1, 1, 0, 0, '2024-01-04 22:13:08', NULL, '0325103661', 0, 0, 'tanhat2021x@gmail.com', 0, 14445, 1705248429, '127.0.0.1', 0, '0', '0', 0, 0, NULL, 0);
INSERT INTO `user` VALUES (8, '2', '1', 10006, 1, 1, 0, 0, '2024-01-04 22:31:37', NULL, '0727282967', 0, 0, 'laidjbaj@gmail.com', 0, 14445, 1705248445, '127.0.0.1', 0, '0', '0', 0, 0, NULL, 0);
INSERT INTO `user` VALUES (9, 'onehit', '1', 10004, 1, 1, 0, 0, '2024-01-04 22:39:26', NULL, '0376304320', 0, 0, 'trinhvanson2k2@gmail.com', 0, 14445, 1704497264, '127.0.0.1', 0, '0', '0', 0, 0, NULL, 0);
INSERT INTO `user` VALUES (10, 'test1', '1', 10008, 0, 0, 0, 1, '2024-01-20 09:09:19', NULL, '0382022942', 0, 0, 'gfgfg@gmail.com', 0, 14445, 1705741810, '127.0.0.1', 0, '0', '0', 0, 0, NULL, 0);
INSERT INTO `user` VALUES (11, '4', '1', 10009, 0, 0, 0, 0, NULL, NULL, NULL, 0, 0, NULL, 0, 14445, 1705742162, '127.0.0.1', 0, '0', '0', 0, NULL, NULL, NULL);

SET FOREIGN_KEY_CHECKS = 1;
