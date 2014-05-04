CREATE TABLE IF NOT EXISTS SiteRubricMapping(
SiteId INTEGER,
RubricId INTEGER,
SiteRubricId VARCHAR,
PRIMARY KEY(SiteId,RubricId));
BEGIN;
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1383,'\квартиру\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1384,'\квартиру\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1385,'\квартиру\однокомнатную');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1386,'\квартиру\однокомнатную');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1387,'\квартиру\двухкомнатную');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1388,'\квартиру\трехкомнатную');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1389,'\квартиру\четырехкомнатную');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1390,'\квартиру\многокомнатную');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1392,'\квартиру\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1393,'\квартиру\однокомнатную');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1394,'\квартиру\двухкомнатную');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1395,'\квартиру\трехкомнатную');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1396,'\квартиру\четырехкомнатную');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1397,'\квартиру\многокомнатную');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1398,'\квартиру\многокомнатную');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1399,'\дом, участок\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1400,'\дом, участок\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1403,'\помещение\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1404,'\помещение\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1405,'\помещение\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1406,'\помещение\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,1407,'\дом, участок\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,4220,'\дом, участок\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,4221,'\гараж\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (11,4223,'\гараж\');
COMMIT;

