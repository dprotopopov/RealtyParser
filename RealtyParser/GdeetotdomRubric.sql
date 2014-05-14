CREATE TABLE IF NOT EXISTS SiteRubric(
SiteId INTEGER,
SiteRubricId VARCHAR,
SiteRubricTitle VARCHAR,
ParentId VARCHAR,
Level INTEGER,
PRIMARY KEY(SiteId,SiteRubricId));
BEGIN;
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\1\','Жилая','\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\3\','Элитная','\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\4\','Загородная','\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\2\','Коммерческая','\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\1\2','квартира','\1\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\1\1','комната','\1\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\1\3','дом / коттедж','\1\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\1\16','таунхаус','\1\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\1\5','земельный участок','\1\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\3\2','квартира','\3\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\3\3','дом / коттедж','\3\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\3\14','замок','\3\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\3\15','поместье','\3\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\3\16','таунхаус','\3\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\3\5','земельный участок','\3\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\4\3','дом / коттедж','\4\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\4\16','таунхаус','\4\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\4\14','замок','\4\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\4\15','поместье','\4\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\4\5','земельный участок','\4\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\2\6','офис','\2\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\2\7','склад','\2\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\2\5','земельный участок','\2\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\2\18','гараж','\2\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\2\4','готовый бизнес','\2\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\2\9','торговое','\2\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\2\8','производственное','\2\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\2\10','свободного назначения','\2\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\2\11','под услуги','\2\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\2\17','под кафе','\2\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\2\13','под банк','\2\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (14,'\2\12','под клуб','\2\',2);
COMMIT;

