CREATE TABLE IF NOT EXISTS SiteAction(
SiteId INTEGER,
SiteActionId VARCHAR,
SiteActionTitle VARCHAR,
ParentId VARCHAR,
Level INTEGER,
PRIMARY KEY(SiteId,SiteActionId));
BEGIN;
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (4,'\in_msk','в Москве','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (4,'\in_mo','в МО','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (4,'\sd','Сдам','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (4,'\sn','Сниму','\',1);
COMMIT;

