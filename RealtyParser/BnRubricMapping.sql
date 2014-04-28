CREATE TABLE IF NOT EXISTS SiteRubricMapping(
SiteId INTEGER,
RubricId INTEGER,
SiteRubricId VARCHAR,
PRIMARY KEY(SiteId,RubricId));
BEGIN;
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1382,'\/city/rooms');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1383,'\/city/rooms');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1384,'\/city/flats');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1385,'\/city/flats');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1386,'\/city/flats');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1393,'\/city/rooms');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1387,'\/city/flats');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1394,'\/city/rooms');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1388,'\/city/flats');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1395,'\/city/rooms');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1389,'\/city/flats');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1396,'\/city/rooms');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1392,'\/city/newflats');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1398,'\/city/elite');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1399,'\/city/low_rise');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1400,'\/country/cottages');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1403,'\/commerce/different');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1404,'\/commerce/offices');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1405,'\/commerce/storage');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1406,'\/commerce/different');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,1407,'\/commerce/comm_lands');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (6,4220,'\/country/lands');
COMMIT;

