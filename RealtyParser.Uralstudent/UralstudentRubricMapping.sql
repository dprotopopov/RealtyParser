CREATE TABLE IF NOT EXISTS SiteRubricMapping(
SiteId INTEGER,
RubricId INTEGER,
SiteRubricId VARCHAR,
PRIMARY KEY(SiteId,RubricId));
BEGIN;
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (12,1382,'\Квартиры\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (12,1383,'\Квартиры\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (12,1384,'\Квартиры\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (12,1385,'\Квартиры\комнату');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (12,1386,'\Квартиры\1-к квартиру');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (12,1387,'\Квартиры\2-к квартиру');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (12,1388,'\Квартиры\3-к квартиру');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (12,1389,'\Квартиры\более 3-х комнатной');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (12,1390,'\Квартиры\более 3-х комнатной');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (12,1392,'\Квартиры\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (12,1393,'\Квартиры\1-к квартиру');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (12,1394,'\Квартиры\2-к квартиру');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (12,1395,'\Квартиры\3-к квартиру');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (12,1396,'\Квартиры\более 3-х комнатной');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (12,1397,'\Квартиры\более 3-х комнатной');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (12,1398,'\Квартиры\более 3-х комнатной');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (12,1399,'\дом\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (12,1400,'\дом\');
COMMIT;

