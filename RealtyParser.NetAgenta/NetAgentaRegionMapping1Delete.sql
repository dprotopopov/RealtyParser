BEGIN;
DELETE FROM SiteRegionMapping
WHERE  EXISTS
                      (SELECT SiteId, SiteRegionId, SiteRegionTitle, ParentId, Level
                       FROM      SiteRegion
                       WHERE   (SiteRegionMapping.SiteId = SiteId) AND (SiteRegionMapping.SiteRegionId = SiteRegionId) AND (SiteRegionMapping.SiteId = 5) AND (Level < 3));
COMMIT;