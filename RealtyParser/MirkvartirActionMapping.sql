CREATE TABLE IF NOT EXISTS SiteActionMapping(
SiteId INTEGER,
ActionId INTEGER,
SiteActionId VARCHAR,
PRIMARY KEY(SiteId,ActionId));
BEGIN;
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,1,'\www.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,10,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,11,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,12,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,13,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,14,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,15,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,16,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,17,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,18,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,19,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,2,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,20,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,21,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,22,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,23,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,24,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,3,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,4,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,5,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,6,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,7,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,8,'\ipoteka.');
INSERT OR REPLACE INTO SiteActionMapping(SiteId,ActionId,SiteActionId) VALUES (3,9,'\ipoteka.');
COMMIT;

