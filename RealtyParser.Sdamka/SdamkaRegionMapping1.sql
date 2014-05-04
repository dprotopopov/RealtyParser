CREATE TABLE IF NOT EXISTS SiteRegionMapping(
SiteId INTEGER,
RegionId INTEGER,
SiteRegionId VARCHAR,
PRIMARY KEY(SiteId,RegionId));
BEGIN;
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,66,'\– ОБЛАСТЬ –\');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2931,'\все\');
COMMIT;

