CREATE TABLE IF NOT EXISTS SiteActionMapping(
SiteId INTEGER,
ActionId INTEGER,
SiteActionId VARCHAR,
PRIMARY KEY(SiteId,ActionId));
BEGIN;
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (12,1,'\Продажа');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (12,3,'\Аренда');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (12,5,'\посуточно');
COMMIT;

