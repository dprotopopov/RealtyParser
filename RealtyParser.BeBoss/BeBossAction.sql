CREATE TABLE IF NOT EXISTS SiteAction(
SiteId INTEGER,
SiteActionId VARCHAR,
SiteActionTitle VARCHAR,
ParentId VARCHAR,
Level INTEGER,
PRIMARY KEY(SiteId,SiteActionId));
BEGIN;
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (7,'\/rent','Аренда','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (7,'\/sell','Продажа','\',1);
COMMIT;

