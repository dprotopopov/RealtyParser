CREATE TABLE IF NOT EXISTS SiteRubricMapping(
SiteId INTEGER,
RubricId INTEGER,
SiteRubricId VARCHAR,
PRIMARY KEY(SiteId,RubricId));
BEGIN;
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,4220,'\Загородная недвижимость\\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1383,'\Жилая\\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1384,'\Жилая\Комнаты\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1384,'\Жилая\Комнаты\комнаты');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1385,'\Жилая\Комнаты\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1385,'\Жилая\Комнаты\комнаты');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1386,'\Жилая\Квартиры\1 комнатные');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1393,'\Жилая\Квартиры\1 комнатные');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1387,'\Жилая\Квартиры\2-х комнатные');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1394,'\Жилая\Квартиры\2-х комнатные');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1388,'\Жилая\Квартиры\3-х комнатные');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1395,'\Жилая\Квартиры\3-х комнатные');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1389,'\Жилая\Квартиры\4-х комнатные');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1396,'\Жилая\Квартиры\4-х комнатные');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1390,'\Жилая\Квартиры\1 комнатные');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1397,'\Жилая\Квартиры\1 комнатные');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1392,'\Жилая\Квартиры\малосемейки');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1398,'\Жилая\Квартиры\4-х комнатные');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1399,'\Жилая\Дома, коттеджи\таунхаусы');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1400,'\Жилая\Дома, коттеджи\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1403,'\Коммерческая\\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1404,'\Коммерческая\офисные\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1405,'\Коммерческая\производственные и складские\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1406,'\Коммерческая\торговые\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,1407,'\Земельные участки\\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,4220,'\Земельные участки\\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,4221,'\Гаражи\\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,4223,'\Гаражи\\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,4224,'\Гаражи\\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,4225,'\Гаражи\\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,4226,'\Гаражи\\');
INSERT OR REPLACE INTO SiteRubricMapping(SiteId,RubricId,SiteRubricId) VALUES (10,4227,'\Гаражи\\');
COMMIT;

