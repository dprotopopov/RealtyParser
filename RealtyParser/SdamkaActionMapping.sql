CREATE TABLE IF NOT EXISTS SiteActionMapping(
SiteId INTEGER,
ActionId INTEGER,
SiteActionId VARCHAR,
PRIMARY KEY(SiteId,ActionId));
BEGIN;
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (11,1,'\prodam');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (11,2,'\kuplju');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (11,3,'\sdam');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (11,4,'\snimu');
COMMIT;

