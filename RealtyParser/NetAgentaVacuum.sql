BEGIN;
DELETE FROM SiteBuilderMapping WHERE SiteId<>5;
DELETE FROM SiteReturnFieldMapping WHERE SiteId<>5;
DELETE FROM SiteRubricActionMapping WHERE SiteId<>5;
DELETE FROM SiteRegionRubricMapping WHERE SiteId<>5;
DELETE FROM SiteActionMapping WHERE SiteId<>5;
DELETE FROM SiteRubricMapping WHERE SiteId<>5;
DELETE FROM SiteRegionMapping WHERE SiteId<>5;
DELETE FROM SiteAction WHERE SiteId<>5;
DELETE FROM SiteRubric WHERE SiteId<>5;
DELETE FROM SiteRegion WHERE SiteId<>5;
DELETE FROM Site WHERE SiteId<>5;
COMMIT;
VACUUM;
