BEGIN;
DELETE FROM SiteBuilderMapping WHERE SiteId<>7;
DELETE FROM SiteReturnFieldMapping WHERE SiteId<>7;
DELETE FROM SiteRubricActionMapping WHERE SiteId<>7;
DELETE FROM SiteRegionRubricMapping WHERE SiteId<>7;
DELETE FROM SiteActionMapping WHERE SiteId<>7;
DELETE FROM SiteRubricMapping WHERE SiteId<>7;
DELETE FROM SiteRegionMapping WHERE SiteId<>7;
DELETE FROM SiteAction WHERE SiteId<>7;
DELETE FROM SiteRubric WHERE SiteId<>7;
DELETE FROM SiteRegion WHERE SiteId<>7;
DELETE FROM Site WHERE SiteId<>7;
COMMIT;
VACUUM;