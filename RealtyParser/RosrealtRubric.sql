CREATE TABLE IF NOT EXISTS SiteRubric(
SiteId INTEGER,
SiteRubricId VARCHAR,
SiteRubricTitle VARCHAR,
ParentId VARCHAR,
Level INTEGER,
PRIMARY KEY(SiteId,SiteRubricId));
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'0',' - Недвижимость -','',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'1','квартира','',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'2','дом / дача','',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'3','земельный участок','',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'4','коммерческая недвижимость','',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'5','Комната','1',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'6','Эллинг','2',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'7','Автозаправочная станция','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'8','Аптека','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'9','База','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'10','Здание','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'11','Кафе','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'12','Магазин непродуктовый','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'13','Магазин продуктовый','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'14','Производственный цех / база','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'15','Производство пищевое','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'16','Развлекательное заведение','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'17','Сауна','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'18','Торговые площади','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'19','База отдыха / Туристический комплекс','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'20','Коммерческая недвижимость','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'21','Производственно - складское помещение','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'22','Ангар','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'23','Эллинг','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'24','Гараж жилой','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'25','Ресторан','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'26','Столовая','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'27','Автостоянка','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'28','Машиноместо на крытой парковке','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'29','Гараж для грузового авто','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'30','Салон красоты / Парикмахерская','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'31','Ателье','4',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (2,'32','Конференц-зал','4',2);
