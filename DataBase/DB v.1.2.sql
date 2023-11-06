-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: webstore
-- ------------------------------------------------------
-- Server version	8.0.33

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
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20231016214318_Initial','7.0.12'),('20231101214729_ChangingUserFields','7.0.12'),('20231103121945_СhanginTеablesAndPropertiesOrderDeliveryPayment','7.0.12'),('20231103123049_СhanginTheTеablesAndPropertiesDeliveryPayment','7.0.12'),('20231106175608_AddTableCategory','7.0.12'),('20231106180631_AddRelationshipBetweenTablesProductToCategory','7.0.12'),('20231106182521_AddFieldOrder','7.0.12');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `category`
--

DROP TABLE IF EXISTS `category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `category` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Description` varchar(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `category`
--

LOCK TABLES `category` WRITE;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
INSERT INTO `category` VALUES (1,'Phone','Do more using your voice.Ask Google Assistant to summarize, read aloud, or translate web pages.10 And write messages twice as fast with your voice. Explore before you go.Get extra help with emails.Gmail provides useful prompts and suggestions to help you write, send, and prioritize emails.Gmail Reimagine your photos. Right on your phone. Use Magic Editor in Google Photos to add custom edits and studio-quality enhancements to any photo.27 Improve lighting and background, move a subject, and more, with just a few taps. Reimagine your photos. Right on your phone. Use Magic Editor in Google Photos to add custom edits and studio-quality enhancements to any photo.27 Improve lighting and background, move a subject, and more, with just a few taps.'),(2,'Watch','Watch your heart.Get alerted when you’re above or below your resting heart rate. Check your heart rhythm for AFib from the ECG app and receive irregular heart rhythm notifications. Tune into your wellness.Detect possible changes to your health by tracking your skin temperature, blood oxygen levels, heart rate variability, resting heart rate, and more. Improve your sleep habits. Learn about the quality and duration of your previous night\'s rest with your Sleep Score.4 See the time spent in light, deep, and REM stages. And get a monthly analysis from your Sleep Profile.'),(3,'Headphones','Active Noise Cancellation adapts to your ear.Powered by a custom processor, algorithms, and speakers, Active Noise Cancellation uses Silent Seal™ to maximize the outside noise being blocked.Small buds built for big music.Custom-designed 11 mm speaker drivers make music sound powerful, yet nuanced. A full 5-band EQ lets you customize the sound to your preferences. Volume EQ brings out the details.It adjusts the tuning as you turn the volume up or down, so highs, mids, and lows consistently sound balanced at any level.Transparency mode helps you hear what’s outside.Hear outside sounds in real time so you can be more aware of your surroundings, like when crossing the street.'),(4,'Pixel Tablet','Home is where Hub Mode is. With Hub Mode, you get the best features of a smart display when your tablet is locked and docked – like a digital photo frame, smart home controls, and hands-free help from Google Assistant. Ask Google Assistant for hands-free help.Play music and videos, get answers, control smart home devices, set timers, and more. Just say “Hey Google” to get started. The perfect setup for video calls and streaming. Simply dock the tablet for stationary work and play. It’s the ideal setup for Google Meet calls and binge-watching your favorite TV shows. See your photos come to life. View your pics on the Pixel Tablet’s immersive screen.'),(5,'Accessories','Tech specs Dimensions and weight Length: 6.6 in (167.0 mm) Width: 3.2 in (80.9 mm). Height: 0.5 in (13.4 mm). Weight: 1.1 oz (31.3 g). Materials Case is made with 42% recycled materials based on product weight. Case has a polycarbonate shell made with 75% recycled plastics. The external portion of the case’s power and volume buttons is made with 100% recycled aluminum');
/*!40000 ALTER TABLE `category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `deliveries`
--

DROP TABLE IF EXISTS `deliveries`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `deliveries` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Type` enum('pickup','courier','mail') CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `deliveries`
--

LOCK TABLES `deliveries` WRITE;
/*!40000 ALTER TABLE `deliveries` DISABLE KEYS */;
/*!40000 ALTER TABLE `deliveries` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orderitems`
--

DROP TABLE IF EXISTS `orderitems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orderitems` (
  `OrderId` int NOT NULL,
  `ProductId` int NOT NULL,
  `Quantity` int NOT NULL,
  PRIMARY KEY (`OrderId`,`ProductId`),
  KEY `ProductId` (`ProductId`),
  CONSTRAINT `orderitems_ibfk_1` FOREIGN KEY (`OrderId`) REFERENCES `orders` (`Id`),
  CONSTRAINT `orderitems_ibfk_2` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orderitems`
--

LOCK TABLES `orderitems` WRITE;
/*!40000 ALTER TABLE `orderitems` DISABLE KEYS */;
/*!40000 ALTER TABLE `orderitems` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orders` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreatedAt` datetime NOT NULL,
  `Status` enum('new','processed','delivered') CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DeliverId` int NOT NULL DEFAULT '0',
  `PaymentId` int NOT NULL DEFAULT '0',
  `TotalPrice` decimal(10,2) NOT NULL DEFAULT '0.00',
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  KEY `IX_orders_DeliverId` (`DeliverId`),
  KEY `IX_orders_PaymentId` (`PaymentId`),
  CONSTRAINT `orders_delivery_ibfk` FOREIGN KEY (`DeliverId`) REFERENCES `deliveries` (`Id`),
  CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`),
  CONSTRAINT `orders_payment_ibfk` FOREIGN KEY (`PaymentId`) REFERENCES `payments` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payments`
--

DROP TABLE IF EXISTS `payments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `payments` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Type` enum('cash','card','e-money') CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Amount` decimal(10,2) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payments`
--

LOCK TABLES `payments` WRITE;
/*!40000 ALTER TABLE `payments` DISABLE KEYS */;
INSERT INTO `payments` VALUES (5,'card',0.50),(6,'e-money',3.00),(7,'cash',0.00);
/*!40000 ALTER TABLE `payments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `products`
--

DROP TABLE IF EXISTS `products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `products` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Price` decimal(10,2) NOT NULL,
  `CategoryId` int NOT NULL DEFAULT '0',
  `Color` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Memory` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_products_CategoryId` (`CategoryId`),
  CONSTRAINT `products_category_bdab` FOREIGN KEY (`CategoryId`) REFERENCES `category` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products`
--

LOCK TABLES `products` WRITE;
/*!40000 ALTER TABLE `products` DISABLE KEYS */;
INSERT INTO `products` VALUES (1,'Pixel 7','Name: Pixel 7, Price: 599$, Memory: 12\\128 GB, Color: Lemongrass',599.00,1,'Lemongrass','12\\128 GB'),(2,'Pixel 6 a','Name: Pixel 6 a, Price: 345$, Memory: 6\\128 GB, Color: Sage',345.00,1,'Sage','6\\128 GB'),(3,'Pixel 8','Name: Pixel 8, Price: 699$, Memory: 6\\128 GB, Color: Hazel',699.00,1,'Hazel','6\\128 GB'),(4,'Pixel 8 pro','Name: Pixel 8 pro, Price: 1399$, Memory: 12\\1 TB, Color: Bay',1399.00,1,'Bay','12\\1 TB'),(5,'Pixel 8','Name: Pixel 8, Price: 699$, Memory: 6\\128 GB, Color: Obsidian',699.00,1,'Obsidian','6\\128 GB'),(6,'Pixel 8','Name: Pixel 8, Price: 699$, Memory: 8\\256 GB, Color: Rose',759.00,1,'Rose','8\\256 GB'),(7,'Pixel 8 pro','Name: Pixel 8 pro, Price: 1179$, Memory: 12\\512 GB, Color: Porcelain',1179.00,1,'Porcelain','12\\512 GB'),(8,'Pixel 8 pro','Name: Pixel 8 pro, Price: 1059$, Memory: 12\\256 GB, Color: Obsidian',1059.00,1,'Obsidian','12\\256 GB'),(9,'Pixel Watch ','Name: Pixel Watch, Price: 449$, Memory: 1 GB, Color: Black',449.00,2,'Black','1 GB'),(10,'Pixel Watch ','Name: Pixel Watch, Price: 449$, Memory: 1 GB, Color: Rose',449.00,2,'Rose','1 GB'),(11,'Pixel Watch ','Name: Pixel Watch, Price: 449$, Memory: 1 GB, Color: Blue',449.00,2,'Blue','1 GB'),(12,'Pixel Watch 2','Name: Pixel Watch2, Price: 549$, Memory: 3 GB, Color: Black',549.00,2,'Black','3 GB'),(13,'Pixel Watch 2','Name: Pixel Watch2, Price: 549$, Memory: 3 GB, Color: Rose',549.00,2,'Rose','3 GB'),(14,'Pixel Watch 2','Name: Pixel Watch2, Price: 549$, Memory: 3 GB, Color: Brown',549.00,2,'Brown','3 GB'),(15,'Pixel Buds Pro','Name: Pixel Buds Pro, Price: 299$, Memory: 0.5 MB, Color: Bay',299.00,3,'Bay','0.5 MB'),(16,'Pixel Buds Pro','Name: Pixel Buds Pro, Price: 299$, Memory: 0.5 MB, Color: Porcelain',299.00,3,'Porcelain','0.5 MB'),(17,'Pixel Buds Pro','Name: Pixel Buds Pro, Price: 299$, Memory: 0.5 MB, Color: Lemongrass',299.00,3,'Lemongrass','0.5 MB'),(18,'Pixel Buds Pro','Name: Pixel Buds Pro, Price: 299$, Memory: 0.5 MB, Color:  Charcoal',299.00,3,'Charcoal','0.5 MB'),(19,'Pixel Buds Pro','Name: Pixel Buds Pro, Price: 299$, Memory: 0.5 MB, Color:  Fog',299.00,3,'Fog','0.5 MB'),(20,'Pixel Tablet','Name: Pixel Tablet, Price: 899$, Memory: 12/512  GB, Color:  Porcelain',899.00,4,'Porcelain','12/512  GB'),(21,'Pixel Tablet','Name: Pixel Tablet, Price: 899$, Memory: 12/512  GB, Color:  Hazel',899.00,4,'Hazel','12/512  GB'),(22,'Pixel 8 Pro Case','Name: Pixel 8 Pro Case, Price: 54.99$, Memory: 12/512  GB, Color:  Coral | Google Store exclusive',54.99,5,'Coral | Google Store exclusive','12/512  GB'),(23,'Pixel 8 Case','Name: Pixel 8 Pro Case, Price: 54.99$, Memory: 12/512  GB, Color:  Rose',54.99,5,'Rose','12/512  GB'),(24,'Pixel Watch Woven Band','Name: Pixel Watch Woven Band, Price: 89$, Memory: 0.5 MB, Color:  Lemongrass',89.00,5,'Lemongrass','0.5 MB'),(25,'Pixel Watch Stretch Band','Name: Pixel Watch Stretch Band, Price: 89$, Memory: 0.5 MB, Color:  Coral',89.00,5,'Coral','0.5 MB'),(26,'Pixel Watch Active Sport Band','Name: Pixel Watch Active Sport Band, Price: 69$, Memory: 0.5 MB, Color:  Porcelain',69.00,5,'Porcelain','0.5 MB');
/*!40000 ALTER TABLE `products` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roleclaims`
--

DROP TABLE IF EXISTS `roleclaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roleclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoleId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roleclaims`
--

LOCK TABLES `roleclaims` WRITE;
/*!40000 ALTER TABLE `roleclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `roleclaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roles` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `NormalizedName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roles`
--

LOCK TABLES `roles` WRITE;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` VALUES ('54b18c4c-e4a6-43ea-89ee-51c93a62f0ea','Admin','ADMIN',NULL),('d968d618-f044-4a8c-a1ed-164133e36da4','User','USER',NULL);
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userclaims`
--

DROP TABLE IF EXISTS `userclaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `userclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userclaims`
--

LOCK TABLES `userclaims` WRITE;
/*!40000 ALTER TABLE `userclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `userclaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userlogins`
--

DROP TABLE IF EXISTS `userlogins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `userlogins` (
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderKey` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderDisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`UserId`,`LoginProvider`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userlogins`
--

LOCK TABLES `userlogins` WRITE;
/*!40000 ALTER TABLE `userlogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `userlogins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userroles`
--

DROP TABLE IF EXISTS `userroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `userroles` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userroles`
--

LOCK TABLES `userroles` WRITE;
/*!40000 ALTER TABLE `userroles` DISABLE KEYS */;
INSERT INTO `userroles` VALUES ('04375911-da48-464f-8990-ebab53c9331f','d968d618-f044-4a8c-a1ed-164133e36da4'),('262c53a6-c40c-4171-8b09-d265be4160a6','d968d618-f044-4a8c-a1ed-164133e36da4'),('3fef2645-b259-4974-a8c1-3dfb4dfa1ccd','d968d618-f044-4a8c-a1ed-164133e36da4'),('5657c1af-fac3-41dc-888f-b221c1f94365','d968d618-f044-4a8c-a1ed-164133e36da4'),('cec3f26b-354c-40a0-a504-0fd66b410e0d','d968d618-f044-4a8c-a1ed-164133e36da4'),('e532e613-6ebb-4bff-abee-4eda9e69f13d','54b18c4c-e4a6-43ea-89ee-51c93a62f0ea');
/*!40000 ALTER TABLE `userroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Password` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `NormalizedUserName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `NormalizedEmail` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SecurityStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  `Address` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `City` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `State` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES ('04375911-da48-464f-8990-ebab53c9331f','user3','das@s1D','user3','USER3','user3@example.com','USER3@EXAMPLE.COM',0,'AQAAAAIAAYagAAAAEJmTRu/cJLPoenLMAilJcrGzmhKNAs3Srn3f94UvqQZfcjtQaBRLrTuJQX1S7WxX4Q==','MMOMHLCG5OBKCEDLGGCCH62ETYUEMWC4','715030d3-746e-4c96-addd-cc54445d03f7','44444444',0,0,NULL,1,0,'adress3','city3','state3'),('262c53a6-c40c-4171-8b09-d265be4160a6','user4','saff@acsA1','user4','USER4','user4@example.com','USER4@EXAMPLE.COM',0,'AQAAAAIAAYagAAAAEGQiyUESXkB/5eOIeeT45svOWUHxRzRdLXOAqj9tQSMNhVv9nbtFJBPWdKxYoszCzA==','WFRNWBBQQU6LFCGQXCQ7YGWUKAMD4LJQ','5d4c50fd-c80b-4d97-8d27-a8bf94265cda','34157751',0,0,NULL,1,0,'adress4','city4','state4'),('3fef2645-b259-4974-a8c1-3dfb4dfa1ccd','user2','das@s1D','user2','USER2','user2@example.com','USER2@EXAMPLE.COM',0,'AQAAAAIAAYagAAAAEGFHsewSidEmA66Wfdwo5Y5uqMKHQ+bFTmbLkS4FaR5moMh71tjhchfYuryE8asn6Q==','FP42YYV4AGC76YL5M4WTQRT5HGWDQONH','62ac1d48-148b-41e7-b5f5-58fb32870c28','3333333',0,0,NULL,1,0,NULL,NULL,NULL),('5657c1af-fac3-41dc-888f-b221c1f94365','user1','das@s1D','user1','USER1','user1@example.com','USER1@EXAMPLE.COM',0,'AQAAAAIAAYagAAAAEGEnZSv7oNgQOc7kLtx/RT3DqL0+G4/lBRqDF+2zZuVCwoWx5R+eM8QV6BQIm+Apyg==','OOWVFEJP2IMMHOO6UMVEH6MN3HQE3YZR','49fd9b03-188a-4884-9d17-7cf483097156','22222222',0,0,NULL,1,0,NULL,NULL,NULL),('cec3f26b-354c-40a0-a504-0fd66b410e0d','user','das@s1D','user','USER','user@example.com','USER@EXAMPLE.COM',0,'AQAAAAIAAYagAAAAELj57jlr/aDASRMUyM/hQXTJfTCFa89pMZlq2LblmzSEpCxiTVvWTDsiyMeaKmu7YA==','3LX6YPXAPJLXOI66WDRFVZNNR2HK74IK','fa26f3ad-bac9-4d92-a5fe-f11df7f71d28','11111111',0,0,NULL,1,0,NULL,NULL,NULL),('e532e613-6ebb-4bff-abee-4eda9e69f13d','admin','admin@1Q','admin','ADMIN','admin@example.com','ADMIN@EXAMPLE.COM',0,'AQAAAAIAAYagAAAAEHqVN2mAjzfnp8ewX7wjI+10eyfWBfXgLcTZVGHV+76qQlC+e8Dv9YZMIlsYkWlCng==','ZQ324PK2MAXMJ3FVAJDTCAVDW4AVPLGM','deae34d5-7a60-4bde-a1da-61cf6828ac22','43146210',0,0,NULL,1,0,NULL,NULL,NULL);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usertokens`
--

DROP TABLE IF EXISTS `usertokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usertokens` (
  `UserId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `LoginProvider` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usertokens`
--

LOCK TABLES `usertokens` WRITE;
/*!40000 ALTER TABLE `usertokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `usertokens` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-11-07  0:31:07
