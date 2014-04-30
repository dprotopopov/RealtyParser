CREATE TABLE IF NOT EXISTS SiteAction(
SiteId INTEGER,
SiteActionId VARCHAR,
SiteActionTitle VARCHAR,
ParentId VARCHAR,
Level INTEGER,
PRIMARY KEY(SiteId,SiteActionId));
BEGIN;
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (8,'\59','Снять','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (8,'\63','Купить','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (8,'\86','Снять на сутки','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (8,'\58','Сдать в аренду','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (8,'\62','Продать','\',1);
COMMIT;

