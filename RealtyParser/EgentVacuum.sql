BEGIN;
DELETE FROM SiteBuilderMapping WHERE SiteId<>4;
DELETE FROM SiteReturnFieldMapping WHERE SiteId<>4;
DELETE FROM SiteRubricActionMapping WHERE SiteId<>4;
DELETE FROM SiteRegionRubricMapping WHERE SiteId<>4;
DELETE FROM SiteActionMapping WHERE SiteId<>4;
DELETE FROM SiteRubricMapping WHERE SiteId<>4;
DELETE FROM SiteRegionMapping WHERE SiteId<>4;
DELETE FROM SiteAction WHERE SiteId<>4;
DELETE FROM SiteRubric WHERE SiteId<>4;
DELETE FROM SiteRegion WHERE SiteId<>4;
DELETE FROM Site WHERE SiteId<>4;
COMMIT;
VACUUM;
