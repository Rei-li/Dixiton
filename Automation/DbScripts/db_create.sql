
CREATE DATABASE `mydixiton` 



CREATE TABLE `user` (
  `id` varchar(64) NOT NULL ,
  `login` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  `email` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


CREATE TABLE `userprofile` (
  `id` varchar(64) NOT NULL ,
  `iduser` varchar(64) NOT NULL,
  `firstname` varchar(45) DEFAULT NULL,
  `lastname` varchar(45) DEFAULT NULL,
  `avatarurl` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_profile_user_idx` (`iduser`),
  CONSTRAINT `fk_profile_user` FOREIGN KEY (`iduser`) REFERENCES `user` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



CREATE TABLE `mydixiton`.`carddeck` (
  `id` varchar(64) NOT NULL ,
  `iduser` varchar(64) NOT NULL,
  `type` TINYINT(1) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_deck_user_idx` (`iduser` ASC),
  CONSTRAINT `fk_deck_user`
    FOREIGN KEY (`iduser`)
    REFERENCES `mydixiton`.`user` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
	
	
	
	
	CREATE TABLE `mydixiton`.`card` (
  `id` varchar(64) NOT NULL ,
  `imageurl` VARCHAR(200) NOT NULL,
  `iddeck` varchar(64) NOT NULL,
  `ischecked` TINYINT(1) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_card_deck_idx` (`iddeck` ASC),
  CONSTRAINT `fk_card_deck`
    FOREIGN KEY (`iddeck`)
    REFERENCES `mydixiton`.`carddeck` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
	
	
	
	CREATE TABLE `mydixiton`.`game` (
  `id` varchar(64) NOT NULL ,
  `type` TINYINT(1) NOT NULL,
  `status` TINYINT(1) NULL,
  `idowner` varchar(64) NULL,
  `idwinner` varchar(64) NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_game_owner_user_idx` (`idowner` ASC),
  INDEX `fk_game_winner_user_idx` (`idwinner` ASC),
  CONSTRAINT `fk_game_owner_user`
    FOREIGN KEY (`idowner`)
    REFERENCES `mydixiton`.`user` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_game_winner_user`
    FOREIGN KEY (`idwinner`)
    REFERENCES `mydixiton`.`user` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
	
	
	
	
	CREATE TABLE `storylogrecord` (
  `id` varchar(64) NOT NULL,
  `idcard` varchar(64) NOT NULL,
  `description` varchar(500) NOT NULL,
  `idstoryteller` varchar(64) NOT NULL,
  `idsusers` varchar(1000) DEFAULT NULL,
  `idgame` varchar(64) NOT NULL,
  `isownerbot` tinyint(1) NOT NULL,
  `datetime` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_story_log_card_idx` (`idcard`),
  KEY `fk_story_log_user_idx` (`idstoryteller`),
  KEY `fk_story_log_game_idx` (`idgame`),
  CONSTRAINT `fk_story_log_game` FOREIGN KEY (`idgame`) REFERENCES `game` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_story_log_card` FOREIGN KEY (`idcard`) REFERENCES `card` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_story_log_user` FOREIGN KEY (`idstoryteller`) REFERENCES `user` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



CREATE TABLE `additionallogrecord` (
  `id` varchar(64) NOT NULL ,
  `idstorylogrecord` varchar(64) NOT NULL,
  `idcard` varchar(64) NOT NULL,
  `idowner` varchar(64) NOT NULL,
  `idsusers` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_additional_story_log_idx` (`idstorylogrecord`),
  KEY `fk_additional_log_card_idx` (`idcard`),
  KEY `fk_additional_log_user_idx` (`idowner`),
  CONSTRAINT `fk_additional_log_user` FOREIGN KEY (`idowner`) REFERENCES `user` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_additional_log_card` FOREIGN KEY (`idcard`) REFERENCES `card` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_additional_story_log` FOREIGN KEY (`idstorylogrecord`) REFERENCES `storylogrecord` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

	
	
