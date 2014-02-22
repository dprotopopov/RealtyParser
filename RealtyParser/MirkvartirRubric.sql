CREATE TABLE IF NOT EXISTS SiteRubric(
SiteId INTEGER,
SiteRubricId VARCHAR,
SiteRubricTitle VARCHAR,
ParentId VARCHAR,
Level INTEGER,
PRIMARY KEY(SiteId,SiteRubricId));
BEGIN;
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\www.\','Продажа квартир','\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\new.\','Новостройки','\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\arenda.\','Аренда квартир','\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\dom.\','Продажа домов, участков','\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\arendadoma.\','Аренда домов','\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\zem.\','Земельные участки','\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\ipoteka.\','Ипотека','\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\www.\/','Квартира','\www.\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\www.\/комнаты/','Комната','\www.\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\www.\/новостройки/','Новостройка','\www.\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\new.\/','Новостройка','\new.\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\arenda.\/','Квартира','\arenda.\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\arenda.\/комнаты/','Комната','\arenda.\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\arenda.\/новостройки/','Новостройка','\arenda.\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\dom.\/','Дом','\dom.\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\dom.\/коттеджи/','Коттедж','\dom.\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\dom.\/участки/','Участок','\dom.\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\dom.\/дачи/','Дача','\dom.\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\dom.\/таунхаусы/','Таунхаус','\dom.\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\arendadoma.\/','Дом','\arendadoma.\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\arendadoma.\/коттеджи/','Коттедж','\arendadoma.\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\arendadoma.\/участки/','Участок','\arendadoma.\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\arendadoma.\/дачи/','Дача','\arendadoma.\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\arendadoma.\/таунхаусы/','Таунхаус','\arendadoma.\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\zem.\/','Участок','\zem.\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\\/','Квартира','\\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\\/комнаты/','Комната','\\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (3,'\\/новостройки/','Новостройка','\\',2);
COMMIT;

