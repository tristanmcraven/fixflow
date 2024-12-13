CREATE DATABASE  IF NOT EXISTS `fixflow` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `fixflow`;
-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: localhost    Database: fixflow
-- ------------------------------------------------------
-- Server version	8.0.39

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
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=69 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `device_brand`
--

LOCK TABLES `device_brand` WRITE;
/*!40000 ALTER TABLE `device_brand` DISABLE KEYS */;
INSERT INTO `device_brand` VALUES (23,'Alcatel'),(62,'Alienware'),(1,'Apple'),(13,'Asus'),(34,'Blackview'),(29,'BLU'),(30,'BQ'),(52,'Chuwi'),(61,'Clevo'),(28,'Coolpad'),(35,'Cubot'),(46,'Dell'),(32,'Doogee'),(55,'Dynabook'),(63,'Eluktronics'),(44,'Essential'),(59,'Eurocom'),(39,'Fairphone'),(64,'Framework'),(54,'Fujitsu'),(51,'Gigabyte'),(43,'Gionee'),(9,'Google'),(14,'Honor'),(47,'HP'),(18,'HTC'),(4,'Huawei'),(20,'Infinix'),(22,'Itel'),(27,'Karbonn'),(26,'Lava'),(36,'Leagoo'),(16,'Lenovo'),(17,'LG'),(19,'Meizu'),(25,'Micromax'),(48,'Microsoft'),(12,'Motorola'),(49,'MSI'),(11,'Nokia'),(8,'OnePlus'),(5,'Oppo'),(37,'Oukitel'),(41,'Panasonic'),(67,'pizda'),(58,'Purism'),(50,'Razer'),(7,'Realme'),(2,'Samsung'),(60,'Schenker'),(42,'Sharp'),(40,'Shiftphone'),(10,'Sony'),(57,'System76'),(24,'TCL'),(53,'Teclast'),(21,'Tecno'),(33,'Ulefone'),(38,'Umidigi'),(56,'VAIO'),(45,'Vertu'),(6,'Vivo'),(31,'Wiko'),(3,'Xiaomi'),(68,'zalup123'),(66,'zalupa'),(65,'zhopa'),(15,'ZTE');
/*!40000 ALTER TABLE `device_brand` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `device_model`
--

DROP TABLE IF EXISTS `device_model`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `device_model` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `device_brand_id` int unsigned NOT NULL,
  `name` varchar(250) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_devicemodel_device_brand_id_idx` (`device_brand_id`),
  CONSTRAINT `fk_devicemodel_device_brand_id` FOREIGN KEY (`device_brand_id`) REFERENCES `device_brand` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `device_model`
--

LOCK TABLES `device_model` WRITE;
/*!40000 ALTER TABLE `device_model` DISABLE KEYS */;
INSERT INTO `device_model` VALUES (1,1,'iPhone 15'),(2,1,'iPhone 16'),(3,1,'iphoon 14'),(4,1,'iphon 13');
/*!40000 ALTER TABLE `device_model` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `status`
--

DROP TABLE IF EXISTS `status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `status` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `status`
--

LOCK TABLES `status` WRITE;
/*!40000 ALTER TABLE `status` DISABLE KEYS */;
INSERT INTO `status` VALUES (13,'Без ремонта'),(10,'В ремонте'),(12,'Выданные'),(11,'Готово'),(8,'На согласовании'),(9,'Ожидание запчастей'),(7,'Принят');
/*!40000 ALTER TABLE `status` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ticket`
--

DROP TABLE IF EXISTS `ticket`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ticket` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `device_brand_id` int unsigned NOT NULL,
  `device_model_id` int unsigned NOT NULL,
  `client_fullname` varchar(150) DEFAULT NULL,
  `client_phone_number` varchar(20) DEFAULT NULL,
  `timestamp` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `note` varchar(5000) DEFAULT NULL,
  `description` varchar(5000) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_ticket_device_model_id_idx` (`device_model_id`),
  KEY `fk_ticket_device_brand_id_idx` (`device_brand_id`),
  CONSTRAINT `fk_ticket_device_brand_id` FOREIGN KEY (`device_brand_id`) REFERENCES `device_brand` (`id`),
  CONSTRAINT `fk_ticket_device_model_id` FOREIGN KEY (`device_model_id`) REFERENCES `device_model` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ticket`
--

LOCK TABLES `ticket` WRITE;
/*!40000 ALTER TABLE `ticket` DISABLE KEYS */;
INSERT INTO `ticket` VALUES (5,1,1,'барак монголов','79998887766','2024-11-17 08:13:56','изменяешь примечание окно закрываешь оно сохраняется бОзарю о_0',NULL),(6,1,2,'налог сдоходов','79998887766','2024-11-17 08:16:24','примечаю бля',NULL);
/*!40000 ALTER TABLE `ticket` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ticket_kit`
--

DROP TABLE IF EXISTS `ticket_kit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ticket_kit` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `ticket_id` int unsigned NOT NULL,
  `name` varchar(150) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_ticketkit_ticket_id_idx` (`ticket_id`),
  CONSTRAINT `fk_ticketkit_ticket_id` FOREIGN KEY (`ticket_id`) REFERENCES `ticket` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ticket_kit`
--

LOCK TABLES `ticket_kit` WRITE;
/*!40000 ALTER TABLE `ticket_kit` DISABLE KEYS */;
INSERT INTO `ticket_kit` VALUES (3,5,'хуй'),(4,5,'да'),(5,5,'нихуя'),(6,6,'говно'),(7,6,'хер'),(8,6,'залупа');
/*!40000 ALTER TABLE `ticket_kit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ticket_malfunctions`
--

DROP TABLE IF EXISTS `ticket_malfunctions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ticket_malfunctions` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `ticket_id` int unsigned NOT NULL,
  `name` varchar(150) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_ticketmalfunctions_ticket_id_idx` (`ticket_id`),
  CONSTRAINT `fk_ticketmalfunctions_ticket_id` FOREIGN KEY (`ticket_id`) REFERENCES `ticket` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ticket_malfunctions`
--

LOCK TABLES `ticket_malfunctions` WRITE;
/*!40000 ALTER TABLE `ticket_malfunctions` DISABLE KEYS */;
INSERT INTO `ticket_malfunctions` VALUES (1,5,'отвал хуя'),(2,5,'отвал пизды'),(3,5,'отвал жопы'),(4,5,'отвал говна'),(5,6,'отвал очка'),(6,6,'отвал ноги'),(7,6,'отвал хуя');
/*!40000 ALTER TABLE `ticket_malfunctions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ticket_repairs`
--

DROP TABLE IF EXISTS `ticket_repairs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ticket_repairs` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `ticket_id` int unsigned NOT NULL,
  `repair` varchar(150) NOT NULL,
  `price` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_ticketrepairs_ticket_id_idx` (`ticket_id`),
  CONSTRAINT `fk_ticketrepairs_ticket_id` FOREIGN KEY (`ticket_id`) REFERENCES `ticket` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ticket_repairs`
--

LOCK TABLES `ticket_repairs` WRITE;
/*!40000 ALTER TABLE `ticket_repairs` DISABLE KEYS */;
INSERT INTO `ticket_repairs` VALUES (1,5,'отмыв говна',1500),(2,5,'полировка очка',3000);
/*!40000 ALTER TABLE `ticket_repairs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ticket_status`
--

DROP TABLE IF EXISTS `ticket_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ticket_status` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `ticket_id` int unsigned NOT NULL,
  `status_id` int unsigned NOT NULL,
  `timestamp` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_ticketstatus_ticket_id_idx` (`ticket_id`),
  KEY `fk_ticketstatus_status_id_idx` (`status_id`),
  CONSTRAINT `fk_ticketstatus_status_id` FOREIGN KEY (`status_id`) REFERENCES `status` (`id`),
  CONSTRAINT `fk_ticketstatus_ticket_id` FOREIGN KEY (`ticket_id`) REFERENCES `ticket` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ticket_status`
--

LOCK TABLES `ticket_status` WRITE;
/*!40000 ALTER TABLE `ticket_status` DISABLE KEYS */;
INSERT INTO `ticket_status` VALUES (1,5,7,'2024-11-17 08:14:43'),(2,6,7,'2024-11-17 08:16:24'),(3,5,8,'2024-11-17 20:28:33');
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

-- Dump completed on 2024-11-18  1:52:16