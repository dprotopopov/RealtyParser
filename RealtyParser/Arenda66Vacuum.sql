BEGIN;
DELETE FROM SiteBuilderMapping WHERE SiteId<>8;
DELETE FROM SiteReturnFieldMapping WHERE SiteId<>8;
DELETE FROM SiteRubricActionMapping WHERE SiteId<>8;
DELETE FROM SiteRegionRubricMapping WHERE SiteId<>8;
DELETE FROM SiteActionMapping WHERE SiteId<>8;
DELETE FROM SiteRubricMapping WHERE SiteId<>8;
DELETE FROM SiteRegionMapping WHERE SiteId<>8;
DELETE FROM SiteAction WHERE SiteId<>8;
DELETE FROM SiteRubric WHERE SiteId<>8;
DELETE FROM SiteRegion WHERE SiteId<>8;
DELETE FROM Site WHERE SiteId<>8;
COMMIT;
VACUUM;