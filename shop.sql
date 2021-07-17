-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               10.5.8-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             11.0.0.5919
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for shopbridge
CREATE DATABASE IF NOT EXISTS `shopbridge` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_unicode_ci */;
USE `shopbridge`;

-- Dumping structure for table shopbridge.product_items
CREATE TABLE IF NOT EXISTS `product_items` (
  `Product_id` varchar(20) NOT NULL,
  `Product_Code` varchar(20) NOT NULL,
  `Product_Name` varchar(100) DEFAULT NULL,
  `Product_Description` varchar(500) DEFAULT NULL,
  `Product_Price` double DEFAULT NULL,
  `Product_Quantity` bigint(20) DEFAULT NULL,
  `User_id` varchar(50) DEFAULT NULL,
  `Client_ip` varchar(80) DEFAULT NULL,
  `Entry_Date_Time` datetime DEFAULT NULL,
  PRIMARY KEY (`Product_id`,`Product_Code`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumping data for table shopbridge.product_items: ~0 rows (approximately)
DELETE FROM `product_items`;
/*!40000 ALTER TABLE `product_items` DISABLE KEYS */;
/*!40000 ALTER TABLE `product_items` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
