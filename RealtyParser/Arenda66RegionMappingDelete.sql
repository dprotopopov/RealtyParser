DELETE
FROM     SiteRegionMapping
WHERE  (SiteId = 8) AND (RegionId <> 28755) AND EXISTS
                      (SELECT RegionId, RegionTitle, ParentId, Level, HasChild
                       FROM      Region
                       WHERE   (SiteRegionMapping.RegionId = RegionId) AND (ParentId <> 66));