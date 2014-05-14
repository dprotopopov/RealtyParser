CREATE TABLE IF NOT EXISTS SiteRubric(
SiteId INTEGER,
SiteRubricId VARCHAR,
SiteRubricTitle VARCHAR,
ParentId VARCHAR,
Level INTEGER,
PRIMARY KEY(SiteId,SiteRubricId));
BEGIN;
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\комнаты','Комнаты','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\квартиры','Квартиры','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\однокомнатная','Однокомнатная','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\двухкомнатная','Двухкомнатная','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\трехкомнатная','Трехкомнатная','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\четырехкомнатная','Четырехкомнатная','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\многокомнатная','Многокомнатная','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\новостройки','Новостройки','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\все виды жилья','Все виды жилья','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\комнаты','Комнаты','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\квартиры','Квартиры','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\коттеджи','Коттеджи','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\дома','Дома','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\апартаменты','Апартаменты','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\номера в гостиницах','Номера в гостиницах','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\дачи, сады','Дачи, Сады','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\коммерческая недвижимость','Коммерческая недвижимость','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\офисы','Офисы','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\здания и особняки','Здания и особняки','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\торговые помещения','Торговые помещения','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\производственные помещения','Производственные помещения','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\склады','Склады','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\нежилые свободного назначения','Нежилые свободного назначения','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\земельные участки','Земельные участки','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\дома','Дома','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\участки','Участки','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\сады','Сады','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\гаражи','Гаражи','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\загородная недвижимость','Загородная недвижимость','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\городской','Городской','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\иногородний','Иногородний','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\зарубежная жилая','Зарубежная Жилая','\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (13,'\зарубежная коммерческая','Зарубежная Коммерческая','\',1);
COMMIT;

