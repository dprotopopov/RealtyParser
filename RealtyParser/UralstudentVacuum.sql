BEGIN;
DELETE FROM SiteBuilderMapping WHERE SiteId<>12;
DELETE FROM SiteReturnFieldMapping WHERE SiteId<>12;
DELETE FROM SiteRubricActionMapping WHERE SiteId<>12;
DELETE FROM SiteRegionRubricMapping WHERE SiteId<>12;
DELETE FROM SiteActionMapping WHERE SiteId<>12;
DELETE FROM SiteRubricMapping WHERE SiteId<>12;
DELETE FROM SiteRegionMapping WHERE SiteId<>12;
DELETE FROM SiteAction WHERE SiteId<>12;
DELETE FROM SiteRubric WHERE SiteId<>12;
DELETE FROM SiteRegion WHERE SiteId<>12;
DELETE FROM Site WHERE SiteId<>12;
COMMIT;
VACUUM;
