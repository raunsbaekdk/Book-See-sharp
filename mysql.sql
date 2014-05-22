# Dump of table Busses
# ------------------------------------------------------------

DROP TABLE IF EXISTS `Busses`;

CREATE TABLE `Busses` (
  `regNo` varchar(7) NOT NULL,
  `comCenter` varchar(60) NOT NULL,
  PRIMARY KEY (`regNo`),
  KEY `comCenter` (`comCenter`)
);

INSERT INTO `Busses` (`regNo`, `comCenter`)
VALUES
	('AB12345','Lokalcenter Marselis'),
	('BA12345','Sundheds- og Kulturcenter Frederiksbjerg & Langenæs'),
	('AC12345','Lokalcenter Dalgas'),
	('CA12345','Lokalcenter Rosenvang');


# Dump of table ComCenters
# ------------------------------------------------------------

DROP TABLE IF EXISTS `ComCenters`;

CREATE TABLE `ComCenters` (
  `name` varchar(60) NOT NULL,
  `address` varchar(120) NOT NULL,
  `contactName` varchar(80) NOT NULL,
  `contactPhone` int(11) NOT NULL,
  PRIMARY KEY (`name`)
);

INSERT INTO `ComCenters` (`name`, `address`, `contactName`, `contactPhone`)
VALUES
	('Lokalcenter Marselis','Marselis Boulevard 94 A, 8000 Aarhus C','Rigmor',24784167),
	('Sundheds- og Kulturcenter Frederiksbjerg & Langenæs','Ankersgade 21, 8000 Aarhus C','Rigmor',24784167),
	('Lokalcenter Dalgas','Dalgas Avenue 54, 8000 Aarhus C','Rigmor',24784167),
	('Lokalcenter Rosenvang','Rosenvangs Allé 76, 8260 Viby J','Rigmor',24784167);


# Dump of table Passwords
# ------------------------------------------------------------

DROP TABLE IF EXISTS `Passwords`;

CREATE TABLE `Passwords` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `password` char(255) NOT NULL,
  `appkey` varchar(60) NOT NULL,
  `created` int(11) NOT NULL,
  PRIMARY KEY (`id`)
);

INSERT INTO `Passwords` (`id`, `password`, `appkey`, `created`)
VALUES
	(1,'123','dqQEqqJcyB6GT4H73HEw2gcAytEwTZRWyxJLKANsh2WkUvf6m3svtLUQ3quM',1400484521),
	(2,'1234567','',1400484521);


# Dump of table Reservations
# ------------------------------------------------------------

DROP TABLE IF EXISTS `Reservations`;

CREATE TABLE `Reservations` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` int(11) NOT NULL,
  `bus` varchar(7) NOT NULL,
  `fromDate` datetime NOT NULL,
  `toDate` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `bus` (`bus`,`fromDate`,`toDate`),
  KEY `username` (`username`)
);

INSERT INTO `Reservations` (`id`, `username`, `bus`, `fromDate`, `toDate`)
VALUES
	(1,12345678,'AB12345','2014-05-22 13:00:01','2014-05-22 15:59:59');

DELIMITER ;;
CREATE TRIGGER `insert_Reservations` BEFORE INSERT ON `Reservations` FOR EACH ROW BEGIN
		IF EXISTS ( SELECT id, username, bus, fromDate, toDate, Users.name AS contactName, Users.mobile AS contactPhone FROM Reservations LEFT JOIN Users ON Users.mobile = Reservations.username
			WHERE
			((NEW.fromDate BETWEEN fromDate AND toDate)
			OR (NEW.toDate BETWEEN fromDate AND toDate)
			OR (NEW.fromDate >= fromDate AND NEW.toDate <= toDate)
			OR (NEW.fromDate <= fromDate AND NEW.toDate >= toDate )) AND bus = NEW.bus ) THEN
			SIGNAL SQLSTATE '12345'
			SET MESSAGE_TEXT = 'Cannot insert';
		END IF;
	END;;
DELIMITER ;


# Dump of table Users
# ------------------------------------------------------------

DROP TABLE IF EXISTS `Users`;

CREATE TABLE `Users` (
  `mobile` int(11) NOT NULL,
  `passwordId` int(11) NOT NULL,
  `admin` bit(1) NOT NULL,
  `name` varchar(80) NOT NULL,
  `email` varchar(255) NOT NULL,
  PRIMARY KEY (`mobile`),
  KEY `passwordId` (`passwordId`)
);

INSERT INTO `Users` (`mobile`, `passwordId`, `admin`, `name`, `email`)
VALUES
	(12345678,1,00000001,'Stefan Weibel','Stefan.Weibel@gmail.com');