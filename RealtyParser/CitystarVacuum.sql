BEGIN;
DELETE FROM SiteBuilderMapping WHERE SiteId<>13;
DELETE FROM SiteReturnFieldMapping WHERE SiteId<>13;
DELETE FROM SiteRubricActionMapping WHERE SiteId<>13;
DELETE FROM SiteRegionRubricMapping WHERE SiteId<>13;
DELETE FROM SiteActionMapping WHERE SiteId<>13;
DELETE FROM SiteRubricMapping WHERE SiteId<>13;
DELETE FROM SiteRegionMapping WHERE SiteId<>13;
DELETE FROM SiteAction WHERE SiteId<>13;
DELETE FROM SiteRubric WHERE SiteId<>13;
DELETE FROM SiteRegion WHERE SiteId<>13;
DELETE FROM Site WHERE SiteId<>13;
COMMIT;
VACUUM;
