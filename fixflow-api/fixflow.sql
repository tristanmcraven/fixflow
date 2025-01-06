CREATE DATABASE  IF NOT EXISTS `fixflow` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `fixflow`;
-- MySQL dump 10.13  Distrib 8.0.40, for Win64 (x86_64)
--
-- Host: localhost    Database: fixflow
-- ------------------------------------------------------
-- Server version	8.0.40

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `device_brand`
--

DROP TABLE IF EXISTS `device_brand`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `device_brand` (
  `guid` char(36) NOT NULL,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`guid`),
  UNIQUE KEY `guid_UNIQUE` (`guid`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `device_brand`
--

LOCK TABLES `device_brand` WRITE;
/*!40000 ALTER TABLE `device_brand` DISABLE KEYS */;
INSERT INTO `device_brand` VALUES ('06e163c1-b1fa-47d3-81f6-e06b019b9131','Alcatel'),('385b06e8-6d19-4d2d-bd2a-d97229caa3a1','Alienware'),('10d926a0-fc1a-4e08-a579-95369443da31','Apple'),('050bacc1-a494-4f71-adde-77f903009b53','Asus'),('87af5021-f7a1-41d0-88bf-fd3240871c14','Blackview'),('bcf133d7-be64-4269-8a1e-d5bc3cec84b9','BLU'),('e3433bd3-96eb-4290-bde6-6698d3c6d4ac','BQ'),('a2a51ece-de8b-4284-a3a6-987aeb2d1b37','Chuwi'),('768da48f-54c7-449f-892f-d657489835bb','Clevo'),('3708ef72-92e1-49de-92f2-660cce0f2584','Coolpad'),('1d5824f5-44b8-44b9-838c-b7092189133f','Cubot'),('99117b09-b4fb-4888-934a-5766422a74ae','Dell'),('f2ce68f8-ba93-49fe-aec4-b2af884f80b0','Doogee'),('58bb24bb-08c7-4c2b-b96c-1d6efbeda8e5','Dynabook'),('3dd316e3-9045-4186-a47a-bdc07bbb2bf5','Eluktronics'),('8940b07f-d1f5-4a89-b43f-c7b088be6b22','Essential'),('59169b57-9418-4f03-aed7-c914a9b61018','Eurocom'),('1a1cf764-5112-4e74-9c0a-83a4dd88e184','Fairphone'),('84537510-e8dc-4ac9-a28a-c72a9f410230','Framework'),('5ce6df2e-d2d3-4062-a8f1-88b6c3ae12c1','Fujitsu'),('9617c472-9211-4f23-9364-9476ce632044','Gigabyte'),('f8a1c213-a6c8-4247-8ecf-9afe6fed47f6','Gionee'),('faa8767f-90ce-414f-840d-3693a54d5828','Google'),('eadd3290-44fa-47cf-860a-531036e63f51','Honor'),('b5103dac-2545-4f76-b481-e28a1e417747','HP'),('b149256c-90ce-4505-9238-656206fd7005','HTC'),('362fd497-39b6-4487-b5fc-9ef5ae0b7ec4','Huawei'),('4424b044-9fb2-4644-84a6-198d24d02f67','Infinix'),('dba9ce47-35ef-412a-8e8e-1fa6967752e4','Itel'),('9dd5b425-9e8e-4ed2-b06d-d12a028cd81a','Karbonn'),('a9c3095b-fd5b-4b13-b4cd-bbcb2c7f70de','Lava'),('b286195f-cc46-4f35-9190-567cecf6ce0a','Leagoo'),('7f514363-501e-40a1-ae46-1cc6df809781','Lenovo'),('9aedb775-d45a-441a-b91c-dad2b3ba81b9','LG'),('f9259207-a7a7-4146-b097-d61e4a8a82be','Meizu'),('a4f204fd-fcd8-4a39-8fa8-f9055a25aa47','Micromax'),('1ebace03-8961-430b-98dc-711c1dab2fbd','Microsoft'),('0b214092-7fe9-4506-bd47-6c54e2f926f2','Motorola'),('d8dde1d5-cc75-4f42-b125-1449a790a029','MSI'),('ba156c74-4a6f-48e7-9a74-a04d15865e39','Nokia'),('70c793f3-f342-401c-81ad-e9852f7af366','OnePlus'),('31ae1b21-9a14-4fd9-8079-01078267ddb8','Oppo'),('c9f25681-6ac8-442a-90e3-b30dc6b2a5a7','Oukitel'),('d2444326-d788-4723-ba8a-015801606978','Panasonic'),('db8b87c0-386c-4663-80d3-cb57e9d247a5','Purism'),('44b214a0-8183-4d18-bf69-38dceb2ec58d','Razer'),('afd00fb7-a2a4-4dc9-a81c-58034389395a','Realme'),('926538e2-3431-403a-9350-71ccd61bc74b','Samsung'),('67e96b80-c3f6-4c19-bf01-0eb1195edf1e','Schenker'),('06a04e0c-f430-48ba-a946-75aff55dd854','Sharp'),('c649a98a-c52a-4788-b8e6-d3db804e071b','Shiftphone'),('43bc55ea-1cbc-420f-8278-4f2611bd77a1','Sony'),('4a2fc236-06b8-4a59-b271-4257f50d7256','System76'),('198c143f-84e8-4995-9524-be2255928d7a','TCL'),('064783a5-e2c8-4e12-bdd5-a23d5ed2ec7a','Teclast'),('db3aea23-18f8-4f32-9f3e-246d2679629a','Tecno'),('1da20cc8-4020-4489-85e4-9f9c56107103','Ulefone'),('b53c446f-dbba-4215-a93a-69802d4fc07e','Umidigi'),('788f450b-7033-4284-836e-921fbc764dd5','VAIO'),('5a47ff40-6280-4572-99f3-ea46ca212607','Vertu'),('b0d9618b-7149-4a09-a975-734a43a99109','Vivo'),('70e29f51-3459-4ccb-bf9e-eaf7f98c852b','Wiko'),('fe91cced-4fab-4864-bb41-e12023c2bb06','Xiaomi'),('5526e78c-3011-459a-9a51-b0b4d29b83bb','ZTE');
/*!40000 ALTER TABLE `device_brand` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `device_model`
--

DROP TABLE IF EXISTS `device_model`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `device_model` (
  `guid` char(36) NOT NULL,
  `device_brand_guid` char(36) NOT NULL,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`guid`),
  UNIQUE KEY `guid_UNIQUE` (`guid`),
  KEY `fk_dm_device_brand_guid_idx` (`device_brand_guid`),
  CONSTRAINT `fk_dm_device_brand_guid` FOREIGN KEY (`device_brand_guid`) REFERENCES `device_brand` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `device_model`
--

LOCK TABLES `device_model` WRITE;
/*!40000 ALTER TABLE `device_model` DISABLE KEYS */;
INSERT INTO `device_model` VALUES ('0fc77b7d-2751-49d7-a56d-07230af71eff','10d926a0-fc1a-4e08-a579-95369443da31','iPhone 13'),('397436ac-8db8-4052-832e-746a319fb3d1','10d926a0-fc1a-4e08-a579-95369443da31','iPhone 16'),('653fcb07-a0c1-4cac-9deb-82802f46d53f','10d926a0-fc1a-4e08-a579-95369443da31','iPhone 14'),('9eaaccdf-7b42-4192-870d-720ac167ff5e','06e163c1-b1fa-47d3-81f6-e06b019b9131','A1'),('a49c33eb-4e7f-4fc5-b188-fb4583cf3ad6','87af5021-f7a1-41d0-88bf-fd3240871c14','A7 PRO'),('aa917282-3a67-4bc3-beee-d2611a6194ef','10d926a0-fc1a-4e08-a579-95369443da31','iPhone 15'),('b6116419-988a-4a1e-b934-c4e6dd0496b0','926538e2-3431-403a-9350-71ccd61bc74b','S22'),('fd3a0565-05bc-4616-b34f-c9bbff0e3599','050bacc1-a494-4f71-adde-77f903009b53','A6');
/*!40000 ALTER TABLE `device_model` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `device_type`
--

DROP TABLE IF EXISTS `device_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `device_type` (
  `guid` char(36) NOT NULL,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`guid`),
  UNIQUE KEY `guid_UNIQUE` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `device_type`
--

LOCK TABLES `device_type` WRITE;
/*!40000 ALTER TABLE `device_type` DISABLE KEYS */;
INSERT INTO `device_type` VALUES ('4d3476aa-565a-4e7d-8914-37996698c45a','Телевизор'),('5e4dfb44-43b7-4e77-9104-f527519686b9','Ноутбук'),('b44dc495-9430-40e4-8662-72ee7601b649','Планшет'),('c9b35dde-48bf-4e73-a7e9-f207badb537b','Смартфон');
/*!40000 ALTER TABLE `device_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `repair`
--

DROP TABLE IF EXISTS `repair`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `repair` (
  `guid` char(36) NOT NULL,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`guid`),
  UNIQUE KEY `guid_UNIQUE` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `repair`
--

LOCK TABLES `repair` WRITE;
/*!40000 ALTER TABLE `repair` DISABLE KEYS */;
INSERT INTO `repair` VALUES ('0aae4d87-52c2-40e7-b849-63bf5eff9fa1','Замена разъема зарядки'),('22ec1fb9-6f55-44ce-b30b-2ae14ca022ca','Замена АКБ'),('2d69ae5b-f979-4456-9703-b0a504d9f8e8','Переустановка ПО/Windows/Linux/MacOS'),('58690056-913b-4558-beee-7d8d90a38fcd','жопа'),('63ddfa14-787a-4958-a2b6-c654fc495a6e','Замена дисплея'),('a6278c0c-2ebb-494e-a950-b1507b5e22a0','Замена чего-то нового'),('d729d0c4-abbe-43ac-9d67-7b4e7a81cabb','Замена очка'),('d871d1f7-eac8-4d19-9f47-30ec2b8416f2','ЖОРПА'),('e610c425-ca2c-4b9b-9389-06e587562527','456НЕКПВА');
/*!40000 ALTER TABLE `repair` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `status`
--

DROP TABLE IF EXISTS `status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `status` (
  `guid` char(36) NOT NULL,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`guid`),
  UNIQUE KEY `guid_UNIQUE` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `status`
--

LOCK TABLES `status` WRITE;
/*!40000 ALTER TABLE `status` DISABLE KEYS */;
INSERT INTO `status` VALUES ('05c9122a-e36c-416f-8414-65a2d8f80e6c','Выданные'),('4daa493c-1ba4-4e90-80d1-4b93fe0c4cc4','Ожидание запчастей'),('57890dc4-5764-4285-8bc3-6394c132c750','Готово'),('6e4ab3b1-980a-48d1-97cf-fbf3996008b5','На согласовании'),('90c6289c-e8d9-404c-b717-87d2853b3d08','Без ремонта'),('98cfe9f7-e782-4149-be9b-05c7c296e362','В ремонте'),('d17feffe-6386-4cd2-b23b-633242ede3b9','Принят');
/*!40000 ALTER TABLE `status` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ticket`
--

DROP TABLE IF EXISTS `ticket`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ticket` (
  `guid` char(36) NOT NULL,
  `device_brand_guid` char(36) NOT NULL,
  `device_model_guid` char(36) NOT NULL,
  `device_type_guid` char(36) NOT NULL,
  `client_fullname` varchar(150) DEFAULT NULL,
  `client_phone_number` varchar(20) DEFAULT NULL,
  `timestamp` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `note` varchar(5000) DEFAULT NULL,
  `description` varchar(5000) DEFAULT NULL,
  PRIMARY KEY (`guid`),
  UNIQUE KEY `guid_UNIQUE` (`guid`),
  KEY `fk_ticket_device_brand_guid_idx` (`device_brand_guid`),
  KEY `fk_ticket_device_model_guid_idx` (`device_model_guid`),
  KEY `fk_ticket_device_type_guid_idx` (`device_type_guid`),
  CONSTRAINT `fk_ticket_device_brand_guid` FOREIGN KEY (`device_brand_guid`) REFERENCES `device_brand` (`guid`),
  CONSTRAINT `fk_ticket_device_model_guid` FOREIGN KEY (`device_model_guid`) REFERENCES `device_model` (`guid`),
  CONSTRAINT `fk_ticket_device_type_guid` FOREIGN KEY (`device_type_guid`) REFERENCES `device_type` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ticket`
--

LOCK TABLES `ticket` WRITE;
/*!40000 ALTER TABLE `ticket` DISABLE KEYS */;
INSERT INTO `ticket` VALUES ('0ba445a5-7c78-4f01-b34d-b876f6ed4517','06e163c1-b1fa-47d3-81f6-e06b019b9131','9eaaccdf-7b42-4192-870d-720ac167ff5e','4d3476aa-565a-4e7d-8914-37996698c45a','dassddsads','2313213123','2024-12-31 11:27:49','dsa',NULL),('5440ad9c-2fba-4d63-99a3-22837cf752e3','926538e2-3431-403a-9350-71ccd61bc74b','b6116419-988a-4a1e-b934-c4e6dd0496b0','c9b35dde-48bf-4e73-a7e9-f207badb537b','Учет Расходов','9264384393','2024-12-18 11:51:14','Клиент говорит, что купил телефон недавно',NULL),('61e2e38d-882c-4f18-923b-d8aa2689205f','10d926a0-fc1a-4e08-a579-95369443da31','653fcb07-a0c1-4cac-9deb-82802f46d53f','c9b35dde-48bf-4e73-a7e9-f207badb537b','Улов Налимов','9657016776','2024-12-21 22:40:58','Клиент заявляет, что недавно ронял телефон.',NULL),('6715cc81-700a-4420-a267-c9ee5607a120','06e163c1-b1fa-47d3-81f6-e06b019b9131','9eaaccdf-7b42-4192-870d-720ac167ff5e','4d3476aa-565a-4e7d-8914-37996698c45a','ИЗМЕНА','ИЗМЕНА','2024-12-29 11:16:42','gdfsxfdgsfgfdsggdfs',NULL),('6e29662c-dad1-48dd-8eec-de021072ed67','06e163c1-b1fa-47d3-81f6-e06b019b9131','9eaaccdf-7b42-4192-870d-720ac167ff5e','4d3476aa-565a-4e7d-8914-37996698c45a','12312123123','1322313131','2025-01-06 02:06:42','fffff',NULL),('901a1694-cda2-49b4-990f-bd45a3da116e','10d926a0-fc1a-4e08-a579-95369443da31','0fc77b7d-2751-49d7-a56d-07230af71eff','c9b35dde-48bf-4e73-a7e9-f207badb537b','Налог Сдоходов','9959108244','2024-12-18 11:42:25','Клиент утверждает, что телефон ранее выпал из кармана.',NULL),('d8af7ef6-2674-4201-9828-c2335ab10905','06e163c1-b1fa-47d3-81f6-e06b019b9131','9eaaccdf-7b42-4192-870d-720ac167ff5e','4d3476aa-565a-4e7d-8914-37996698c45a','qwe','123213123','2024-12-31 12:15:58','qwe',NULL);
/*!40000 ALTER TABLE `ticket` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ticket_kit`
--

DROP TABLE IF EXISTS `ticket_kit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ticket_kit` (
  `guid` char(36) NOT NULL,
  `ticket_guid` char(36) NOT NULL,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`guid`),
  UNIQUE KEY `guid_UNIQUE` (`guid`),
  KEY `fk_tk_ticket_guid_idx` (`ticket_guid`),
  CONSTRAINT `fk_tk_ticket_guid` FOREIGN KEY (`ticket_guid`) REFERENCES `ticket` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ticket_kit`
--

LOCK TABLES `ticket_kit` WRITE;
/*!40000 ALTER TABLE `ticket_kit` DISABLE KEYS */;
INSERT INTO `ticket_kit` VALUES ('064c1dda-0bdc-4bdf-99aa-1106effe9588','5440ad9c-2fba-4d63-99a3-22837cf752e3','Телефон'),('128a2c52-7261-4b9a-8db6-96ae43814b57','d8af7ef6-2674-4201-9828-c2335ab10905','qwe'),('1e84d54f-e778-4c82-88fa-703cc70a39a1','6715cc81-700a-4420-a267-c9ee5607a120','ads'),('23eb6a27-c08b-47bd-b561-ae2bd8965b8c','5440ad9c-2fba-4d63-99a3-22837cf752e3','Зарядное устройство'),('5098a387-b903-4ab1-9f6b-277004009c62','6715cc81-700a-4420-a267-c9ee5607a120','НОВОЕ'),('56b0bede-5f8e-414f-98a6-3ec46d1a27f9','5440ad9c-2fba-4d63-99a3-22837cf752e3','SIM-карта 2'),('7064547f-7c92-4764-a1e8-73271587f375','901a1694-cda2-49b4-990f-bd45a3da116e','Телефон'),('782c0d55-29fe-4d58-b4cf-57fa778b2717','901a1694-cda2-49b4-990f-bd45a3da116e','SIM-карта 2'),('7d0da5be-9fd8-4195-b3c9-dc70652cebec','6e29662c-dad1-48dd-8eec-de021072ed67','asd'),('98cf0828-c4a9-4673-ad67-6f81c33b8e6c','0ba445a5-7c78-4f01-b34d-b876f6ed4517','asd'),('ab305ef9-164f-4202-8389-31c289a10bbd','61e2e38d-882c-4f18-923b-d8aa2689205f','SIM-карта'),('d08726b1-081c-41ff-bc40-13d752d3f63d','5440ad9c-2fba-4d63-99a3-22837cf752e3','SIM-карта 1'),('f262390d-8046-41de-a054-5cc5e2a7fb37','61e2e38d-882c-4f18-923b-d8aa2689205f','Телефонfmdskdmsalfkdsamfkldasmfadklsadfmklsadmfdlskmsdfaklsmafkldfsmksdakldafmlkdsfamd'),('f9d44d94-c7a0-436f-8201-9481b04c9ac2','901a1694-cda2-49b4-990f-bd45a3da116e','SIM-карта 1');
/*!40000 ALTER TABLE `ticket_kit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ticket_malfunctions`
--

DROP TABLE IF EXISTS `ticket_malfunctions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ticket_malfunctions` (
  `guid` char(36) NOT NULL,
  `ticket_guid` char(36) NOT NULL,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`guid`),
  UNIQUE KEY `guid_UNIQUE` (`guid`),
  KEY `fk_tm_ticket_guid_idx` (`ticket_guid`),
  CONSTRAINT `fk_tm_ticket_guid` FOREIGN KEY (`ticket_guid`) REFERENCES `ticket` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ticket_malfunctions`
--

LOCK TABLES `ticket_malfunctions` WRITE;
/*!40000 ALTER TABLE `ticket_malfunctions` DISABLE KEYS */;
INSERT INTO `ticket_malfunctions` VALUES ('03cac5af-4a6f-45ba-895d-a4cc99717e58','5440ad9c-2fba-4d63-99a3-22837cf752e3','Плохой звук'),('15a2e585-029b-4bb7-984a-28e46b0f673b','901a1694-cda2-49b4-990f-bd45a3da116e','Сломался дисплей'),('1cff2d2d-c433-46e8-a066-40a240a8275a','61e2e38d-882c-4f18-923b-d8aa2689205f','Медленно заряжается'),('34cbd237-bf1d-4cc3-8c72-3b7ddf26230d','d8af7ef6-2674-4201-9828-c2335ab10905','qwe'),('5a7f3e80-f73a-40a0-8f80-04c64d51d08a','901a1694-cda2-49b4-990f-bd45a3da116e','Сломан порт зарядки'),('5c04422b-0115-4e26-887b-f36d75eb9afb','5440ad9c-2fba-4d63-99a3-22837cf752e3','Поломка экрана'),('80a9aa97-9cbc-4cff-979f-6a89abed2faa','6e29662c-dad1-48dd-8eec-de021072ed67','asd'),('95b0de35-c700-46a5-908c-2d7106fff161','6715cc81-700a-4420-a267-c9ee5607a120','НОВОЕ + ИЗМЕНЕННОЕ'),('9cf1d2dd-e796-4032-9a2e-9b71c2551ec5','0ba445a5-7c78-4f01-b34d-b876f6ed4517','asd'),('9df03fa6-881a-46c4-bd4f-cffaaad07879','6715cc81-700a-4420-a267-c9ee5607a120','ИЗМЕНА'),('c2f9d492-b9df-4b1b-a69b-50ca33616580','6715cc81-700a-4420-a267-c9ee5607a120','НОВОЕ'),('ce7fc745-c9d5-4b3c-9f52-ebc489822f85','61e2e38d-882c-4f18-923b-d8aa2689205f','Не работает экранfnsdjkfdnsjfnkjanfsdjkdsnfjkanjkfdnas'),('cf9fc976-c737-4f79-b59d-1087d34e41c3','5440ad9c-2fba-4d63-99a3-22837cf752e3','Случайные выключения'),('db94b425-9aa7-4dce-a627-5dadfa443421','901a1694-cda2-49b4-990f-bd45a3da116e','Быстро разряжается батарея');
/*!40000 ALTER TABLE `ticket_malfunctions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ticket_repairs`
--

DROP TABLE IF EXISTS `ticket_repairs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ticket_repairs` (
  `guid` char(36) NOT NULL,
  `ticket_guid` char(36) NOT NULL,
  `repair_guid` char(36) NOT NULL,
  `price` int unsigned NOT NULL,
  PRIMARY KEY (`guid`),
  UNIQUE KEY `guid_UNIQUE` (`guid`),
  KEY `fk_tr_ticket_guid_idx` (`ticket_guid`),
  KEY `fk_tr_repair_guid_idx` (`repair_guid`),
  CONSTRAINT `fk_tr_repair_guid` FOREIGN KEY (`repair_guid`) REFERENCES `repair` (`guid`),
  CONSTRAINT `fk_tr_ticket_guid` FOREIGN KEY (`ticket_guid`) REFERENCES `ticket` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ticket_repairs`
--

LOCK TABLES `ticket_repairs` WRITE;
/*!40000 ALTER TABLE `ticket_repairs` DISABLE KEYS */;
INSERT INTO `ticket_repairs` VALUES ('ea0b8f5c-3f8c-4385-9b01-7b976685391a','61e2e38d-882c-4f18-923b-d8aa2689205f','a6278c0c-2ebb-494e-a950-b1507b5e22a0',1500);
/*!40000 ALTER TABLE `ticket_repairs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ticket_status`
--

DROP TABLE IF EXISTS `ticket_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ticket_status` (
  `guid` char(36) NOT NULL,
  `ticket_guid` char(36) NOT NULL,
  `status_guid` char(36) NOT NULL,
  `timestamp` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`guid`),
  UNIQUE KEY `guid_UNIQUE` (`guid`),
  KEY `fk_ts_ticket_guid_idx` (`ticket_guid`),
  KEY `fk_ts_status_guid_idx` (`status_guid`),
  CONSTRAINT `fk_ts_status_guid` FOREIGN KEY (`status_guid`) REFERENCES `status` (`guid`),
  CONSTRAINT `fk_ts_ticket_guid` FOREIGN KEY (`ticket_guid`) REFERENCES `ticket` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ticket_status`
--

LOCK TABLES `ticket_status` WRITE;
/*!40000 ALTER TABLE `ticket_status` DISABLE KEYS */;
INSERT INTO `ticket_status` VALUES ('07d8b9af-8056-4157-a2d6-db3f6c1dd441','61e2e38d-882c-4f18-923b-d8aa2689205f','90c6289c-e8d9-404c-b717-87d2853b3d08','2024-12-31 10:40:50'),('13e34708-e9dc-41d6-a8a2-c1703ce87bb1','5440ad9c-2fba-4d63-99a3-22837cf752e3','d17feffe-6386-4cd2-b23b-633242ede3b9','2024-12-18 11:51:14'),('29ef2912-0a4f-450a-b289-aa303c7c64ac','901a1694-cda2-49b4-990f-bd45a3da116e','d17feffe-6386-4cd2-b23b-633242ede3b9','2024-12-18 11:42:25'),('42d24b75-f89d-4497-8e6a-8d3dbd4fe10a','61e2e38d-882c-4f18-923b-d8aa2689205f','6e4ab3b1-980a-48d1-97cf-fbf3996008b5','2024-12-21 23:25:06'),('45fed133-8ea3-4aed-be55-20563e607e56','6715cc81-700a-4420-a267-c9ee5607a120','57890dc4-5764-4285-8bc3-6394c132c750','2024-12-29 11:16:42'),('88722291-5a37-490a-b164-e0cd84bcbaa9','901a1694-cda2-49b4-990f-bd45a3da116e','4daa493c-1ba4-4e90-80d1-4b93fe0c4cc4','2024-12-18 11:44:41'),('a5228fb0-7d06-4018-bf23-fbef55c6f03c','d8af7ef6-2674-4201-9828-c2335ab10905','6e4ab3b1-980a-48d1-97cf-fbf3996008b5','2025-01-03 02:58:03'),('abf85b91-24ce-45ea-9584-cd97e8d6c7bf','6715cc81-700a-4420-a267-c9ee5607a120','05c9122a-e36c-416f-8414-65a2d8f80e6c','2024-12-29 11:16:42'),('c13caff1-7a30-4505-93fb-ea1bf15677c3','0ba445a5-7c78-4f01-b34d-b876f6ed4517','05c9122a-e36c-416f-8414-65a2d8f80e6c','2024-12-31 11:27:50'),('cae5db5d-385d-4caa-bd4f-78f647ef34dd','61e2e38d-882c-4f18-923b-d8aa2689205f','d17feffe-6386-4cd2-b23b-633242ede3b9','2024-12-21 22:40:58'),('f5ceb58c-fe39-4d63-8e3a-43ad8675ed0c','6e29662c-dad1-48dd-8eec-de021072ed67','05c9122a-e36c-416f-8414-65a2d8f80e6c','2025-01-06 02:06:42'),('fed99347-6a24-4cea-8386-160f38b6dbb4','d8af7ef6-2674-4201-9828-c2335ab10905','98cfe9f7-e782-4149-be9b-05c7c296e362','2024-12-31 12:15:58');
/*!40000 ALTER TABLE `ticket_status` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-01-06  7:16:29
