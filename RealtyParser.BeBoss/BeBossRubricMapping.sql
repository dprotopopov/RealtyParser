CREATE TABLE IF NOT EXISTS SiteRubricMapping(
SiteId INTEGER,
RubricId INTEGER,
SiteRubricId VARCHAR,
PRIMARY KEY(SiteId,RubricId));
BEGIN;
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (7,1404,'\/office');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (7,1405,'\/industry');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (7,1405,'\/stock');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (7,1405,'\/spec');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (7,1406,'\/retail');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (7,1407,'\/land');
COMMIT;

