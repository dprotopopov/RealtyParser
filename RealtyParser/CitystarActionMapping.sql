CREATE TABLE IF NOT EXISTS SiteActionMapping(
SiteId INTEGER,
ActionId INTEGER,
SiteActionId VARCHAR,
PRIMARY KEY(SiteId,ActionId));
BEGIN;
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (13,1,'\продам');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (13,2,'\куплю');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (13,3,'\сдам');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (13,4,'\сниму');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (13,5,'\сдам посуточно');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (13,6,'\обмен');
COMMIT;

