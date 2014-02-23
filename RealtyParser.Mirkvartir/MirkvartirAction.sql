CREATE TABLE IF NOT EXISTS SiteAction(
SiteId INTEGER,
SiteActionId VARCHAR,
SiteActionTitle VARCHAR,
ParentId VARCHAR,
Level INTEGER,
PRIMARY KEY(SiteId,SiteActionId));
BEGIN;
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (3,'\www.','Продажа квартир','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (3,'\new.','Новостройки','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (3,'\arenda.','Аренда квартир','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (3,'\dom.','Продажа домов, участков','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (3,'\arendadoma.','Аренда домов','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (3,'\zem.','Земельные участки','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (3,'\ipoteka.','Ипотека','\',1);
COMMIT;

