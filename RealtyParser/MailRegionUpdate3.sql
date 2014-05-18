UPDATE SiteRegion 
SET RegionPath=(SELECT SiteRegion_0.RegionPath
FROM
                  SiteRegion SiteRegion_0 WHERE  SiteRegion_0.SiteId = SiteRegion.SiteId AND SiteRegion_0.SiteRegionId = ParentId  AND SiteRegion_0.Level = 2 AND SiteRegion_0.SiteId = 16
)
WHERE  SiteId = 16 AND Level = 3;