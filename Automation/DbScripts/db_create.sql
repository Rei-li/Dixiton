
CREATE DATABASE `mydixiton` 

CREATE TABLE `user` (
  `iduser` int(11) NOT NULL AUTO_INCREMENT,
  `login` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  `email` varchar(45) NOT NULL,
  PRIMARY KEY (`iduser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


CREATE TABLE `userprofile` (
  `idprofile` int(11) NOT NULL AUTO_INCREMENT,
  `iduser` int(11) NOT NULL,
  `firstname` varchar(45) DEFAULT NULL,
  `lastname` varchar(45) DEFAULT NULL,
  `avatarurl` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`idprofile`),
  KEY `fk_profile_user_idx` (`iduser`),
  CONSTRAINT `fk_profile_user` FOREIGN KEY (`iduser`) REFERENCES `user` (`iduser`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



CREATE TABLE `mydixiton`.`carddeck` (
  `iddeck` INT NOT NULL AUTO_INCREMENT,
  `iduser` INT NOT NULL,
  `type` TINYINT(1) NOT NULL,
  PRIMARY KEY (`iddeck`),
  INDEX `fk_deck_user_idx` (`iduser` ASC),
  CONSTRAINT `fk_deck_user`
    FOREIGN KEY (`iduser`)
    REFERENCES `mydixiton`.`user` (`iduser`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
	
	
	
	
	CREATE TABLE `mydixiton`.`card` (
  `idcard` INT NOT NULL AUTO_INCREMENT,
  `imageurl` VARCHAR(200) NOT NULL,
  `iddeck` INT NOT NULL,
  `ischecked` TINYINT(1) NOT NULL,
  PRIMARY KEY (`idcard`),
  INDEX `fk_card_deck_idx` (`iddeck` ASC),
  CONSTRAINT `fk_card_deck`
    FOREIGN KEY (`iddeck`)
    REFERENCES `mydixiton`.`carddeck` (`iddeck`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
	
	
	
	CREATE TABLE `mydixiton`.`game` (
  `idgame` INT NOT NULL AUTO_INCREMENT,
  `type` TINYINT(1) NOT NULL,
  `status` TINYINT(1) NULL,
  `idowner` INT NULL,
  `idwinner` INT NULL,
  PRIMARY KEY (`idgame`),
  INDEX `fk_game_owner_user_idx` (`idowner` ASC),
  INDEX `fk_game_winner_user_idx` (`idwinner` ASC),
  CONSTRAINT `fk_game_owner_user`
    FOREIGN KEY (`idowner`)
    REFERENCES `mydixiton`.`user` (`iduser`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_game_winner_user`
    FOREIGN KEY (`idwinner`)
    REFERENCES `mydixiton`.`user` (`iduser`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
	
	
	
	
	CREATE TABLE `storylogrecord` (
  `idstorylogrecord` int(11) NOT NULL AUTO_INCREMENT,
  `idcard` int(11) NOT NULL,
  `description` varchar(500) NOT NULL,
  `idstoryteller` int(11) NOT NULL,
  `idsusers` varchar(1000) DEFAULT NULL,
  `idgame` int(11) NOT NULL,
  `isownerbot` tinyint(1) NOT NULL,
  `datetime` datetime DEFAULT NULL,
  PRIMARY KEY (`idstorylogrecord`),
  KEY `fk_story_log_card_idx` (`idcard`),
  KEY `fk_story_log_user_idx` (`idstoryteller`),
  KEY `fk_story_log_game_idx` (`idgame`),
  CONSTRAINT `fk_story_log_game` FOREIGN KEY (`idgame`) REFERENCES `game` (`idgame`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_story_log_card` FOREIGN KEY (`idcard`) REFERENCES `card` (`idcard`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_story_log_user` FOREIGN KEY (`idstoryteller`) REFERENCES `user` (`iduser`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



CREATE TABLE `additionallogrecord` (
  `idadditionallogrecord` int(11) NOT NULL AUTO_INCREMENT,
  `idstorylogrecord` int(11) NOT NULL,
  `idcard` int(11) NOT NULL,
  `idowner` int(11) NOT NULL,
  `idsusers` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`idadditionallogrecord`),
  KEY `fk_additional_story_log_idx` (`idstorylogrecord`),
  KEY `fk_additional_log_card_idx` (`idcard`),
  KEY `fk_additional_log_user_idx` (`idowner`),
  CONSTRAINT `fk_additional_log_user` FOREIGN KEY (`idowner`) REFERENCES `user` (`iduser`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_additional_log_card` FOREIGN KEY (`idcard`) REFERENCES `card` (`idcard`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_additional_story_log` FOREIGN KEY (`idstorylogrecord`) REFERENCES `storylogrecord` (`idstorylogrecord`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

	
	
