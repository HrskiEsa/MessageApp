CREATE DATABASE IF NOT EXISTS `messagesapp`;
USE `messagesapp`;

CREATE TABLE IF NOT EXISTS `messages` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `receiver` varchar(20) NOT NULL DEFAULT '',
  `message` varchar(500) NOT NULL DEFAULT '',
  `encrypted` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

INSERT INTO `messages` (`id`, `receiver`, `message`, `encrypted`) VALUES
	(1, 'matti', 'viesti matille', 0),
	(2, 'sirpa', 'viesti sirpalle', 0),
	(3, 'admin', 'viesti adminille', 0),
	(8, 'matti', 'Salattu viesti matille', 1);

CREATE TABLE IF NOT EXISTS `users` (
  `username` varchar(50) NOT NULL,
  `password` varchar(200) NOT NULL,
  `salt` VARCHAR(100) NULL,
  `isadmin` int(11) DEFAULT 0,
  PRIMARY KEY (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `users` (`username`, `password`, `isadmin`) VALUES
	('admin', 'admin1', 1),
	('matti', 'matti1', 0),
	('sirpa', 'sirpa1', 0);

