UPDATE     SiteRegion
SET SiteRegionTitle=TRIM(REPLACE(SiteRegionTitle,"\n",""))
WHERE  (SiteId = 16)