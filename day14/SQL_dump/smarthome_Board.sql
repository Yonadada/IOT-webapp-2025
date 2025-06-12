-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: smarthome
-- ------------------------------------------------------
-- Server version	9.2.0

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
-- Table structure for table `Board`
--

DROP TABLE IF EXISTS `Board`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Board` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(125) NOT NULL,
  `Writer` varchar(50) DEFAULT NULL,
  `Title` varchar(45) NOT NULL,
  `Contents` longtext NOT NULL,
  `PostDate` datetime DEFAULT NULL,
  `ReadCount` int DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Board`
--

LOCK TABLES `Board` WRITE;
/*!40000 ALTER TABLE `Board` DISABLE KEYS */;
INSERT INTO `Board` VALUES (9,'hong5683@naver.com','yonadada','테스트1번','<p>테스트 중입니다. 이게 잘 들어가야 할텐데....</p>','2025-06-11 15:20:09',0),(10,'hong5683@naver','테스트만 하는 사람1','테스트2','<p>지금도 테스트 중입니다. 12개는 작성해야 할듯</p>','2025-06-11 15:21:02',0),(11,'123@naver.com','테스트만 하는 사람2','테스트3번','<p>테스트 3번째중</p>','2025-06-11 15:21:33',0),(12,'345@gmail.com','테스트만 하는 사람 3','테스트4','<p>얼른얼른~</p>','2025-06-11 15:22:02',0),(13,'456@naver.com','테스트만 하는 사람 4','가는 세월에~~~','<p>테스트 중입니다</p>','2025-06-11 15:22:52',0),(14,'678@gmail.com','테스트만 하는 사람 5','손목이 아프군요 ','<p>안녕하쇼</p>','2025-06-11 15:23:18',0),(15,'890@gmail.com','이제 테스트 안하고 싶은 사람 1','할 수 있다 아자아자','<p>손목 아푸요</p>','2025-06-11 15:23:48',0),(16,'123145@naver.com','쉽지 않군요 코딩','쉽지 않은 인생','<p>우와웅</p>','2025-06-11 15:24:35',0),(17,'diwen@yahoo.com','일본으로 여행가는 사람 2','언제 일본으로 갈까나','<p>여름 여행은 힘들것 같아</p>','2025-06-11 15:25:53',0),(18,'hello3@yahoo.co.jp','아뇨하세요 저는 외국인입뉘다','한쿡 어 서툴러요','<p>ㅋㅋㅋㅋ</p>','2025-06-11 15:26:38',0),(19,'hong5683@naver.com','영어를 더 잘 하고 싶은 주인장','영어 열심히 하는 방법','<p>매일 꾸준히 하는 게 정답</p>','2025-06-11 15:27:12',3);
/*!40000 ALTER TABLE `Board` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-06-12 16:06:35
