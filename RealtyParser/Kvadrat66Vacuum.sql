BEGIN;
DELETE FROM SiteTableBuilderMapping WHERE SiteId<>10;
DELETE FROM SiteBuilderMapping WHERE SiteId<>10;
DELETE FROM SiteReturnFieldMapping WHERE SiteId<>10;
DELETE FROM SiteRubricActionMapping WHERE SiteId<>10;
DELETE FROM SiteRegionRubricMapping WHERE SiteId<>10;
DELETE FROM SiteActionMapping WHERE SiteId<>10;
DELETE FROM SiteRubricMapping WHERE SiteId<>10;
DELETE FROM SiteRegionMapping WHERE SiteId<>10;
DELETE FROM SiteAction WHERE SiteId<>10;
DELETE FROM SiteRubric WHERE SiteId<>10;
DELETE FROM SiteRegion WHERE SiteId<>10;
DELETE FROM Site WHERE SiteId<>10;
COMMIT;
VACUUM;
