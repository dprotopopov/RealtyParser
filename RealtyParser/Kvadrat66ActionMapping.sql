CREATE TABLE IF NOT EXISTS SiteActionMapping(
SiteId INTEGER,
ActionId INTEGER,
SiteActionId VARCHAR,
PRIMARY KEY(SiteId,ActionId));
BEGIN;
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (10,1,'\Аренда\');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (10,3,'\Продажа\');
COMMIT;

