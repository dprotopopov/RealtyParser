BEGIN;
DELETE FROM SiteBuilderMapping WHERE SiteId<>14;
DELETE FROM SiteReturnFieldMapping WHERE SiteId<>14;
DELETE FROM SiteRubricActionMapping WHERE SiteId<>14;
DELETE FROM SiteRegionRubricMapping WHERE SiteId<>14;
DELETE FROM SiteActionMapping WHERE SiteId<>14;
DELETE FROM SiteRubricMapping WHERE SiteId<>14;
DELETE FROM SiteRegionMapping WHERE SiteId<>14;
DELETE FROM SiteAction WHERE SiteId<>14;
DELETE FROM SiteRubric WHERE SiteId<>14;
DELETE FROM SiteRegion WHERE SiteId<>14;
DELETE FROM Site WHERE SiteId<>14;
COMMIT;
VACUUM;