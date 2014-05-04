CREATE TABLE IF NOT EXISTS SiteRegionMapping(
SiteId INTEGER,
RegionId INTEGER,
SiteRegionId VARCHAR,
PRIMARY KEY(SiteId,RegionId));
BEGIN;
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (10,66,'\66\');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (10,2931,'\66\2931');
COMMIT;

