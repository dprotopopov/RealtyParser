CREATE TABLE IF NOT EXISTS SiteRubric(
SiteId INTEGER,
SiteRubricId VARCHAR,
SiteRubricTitle VARCHAR,
ParentId VARCHAR,
Level INTEGER,
PRIMARY KEY(SiteId,SiteRubricId));
BEGIN;
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/living\\','Квартиры, комнаты','\\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/country\\','Дома, участки','\\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/commercial\\','Коммерческая недвижимость','\\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/realty\\','За рубежом','\\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/living\/ru-mos-moskva\','Квартиры, комнаты','\/living\\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/country\/ru-mos-moskva\','Дома, участки','\/country\\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/commercial\/ru-mos-moskva\','Коммерческая недвижимость','\/commercial\\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/realty\/foreign\','За рубежом','\/realty\\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/living\/ru-mos-moskva\/secondary_flat','Квартира','\/living\/ru-mos-moskva\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/living\/ru-mos-moskva\/flat','Квартира в новостройке','\/living\/ru-mos-moskva\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/living\/ru-mos-moskva\/room','Комната','\/living\/ru-mos-moskva\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/living\/ru-mos-moskva\/new_building','Новостройка','\/living\/ru-mos-moskva\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/country\/ru-mos-moskva\/cottage','Коттедж','\/country\/ru-mos-moskva\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/country\/ru-mos-moskva\/house','Дача / Дом','\/country\/ru-mos-moskva\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/country\/ru-mos-moskva\/townhouse','Таунхаус','\/country\/ru-mos-moskva\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/country\/ru-mos-moskva\/country_plot','Участок','\/country\/ru-mos-moskva\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/country\/ru-mos-moskva\/cottage_village','Коттеджный посёлок','\/country\/ru-mos-moskva\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/country\/ru-mos-moskva\/country_other','Другое','\/country\/ru-mos-moskva\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/commercial\/ru-mos-moskva\/office','Офис','\/commercial\/ru-mos-moskva\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/commercial\/ru-mos-moskva\/building','Здание','\/commercial\/ru-mos-moskva\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/commercial\/ru-mos-moskva\/business_place','Торговое помещение','\/commercial\/ru-mos-moskva\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/commercial\/ru-mos-moskva\/commercial_plot','Участок','\/commercial\/ru-mos-moskva\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/commercial\/ru-mos-moskva\/service','Предприятие сферы услуг','\/commercial\/ru-mos-moskva\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/commercial\/ru-mos-moskva\/factory_store','Склад','\/commercial\/ru-mos-moskva\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/commercial\/ru-mos-moskva\/commercial_other','Другое (коммерческая недвижимость)','\/commercial\/ru-mos-moskva\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/realty\/foreign\/secondary_flat','Квартира','\/realty\/foreign\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/realty\/foreign\/flat','Квартира в новостройке','\/realty\/foreign\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/realty\/foreign\/new_building','Новостройка','\/realty\/foreign\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/realty\/foreign\/cottage','Коттедж','\/realty\/foreign\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/realty\/foreign\/house','Дача / Дом','\/realty\/foreign\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/realty\/foreign\/townhouse','Таунхаус','\/realty\/foreign\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/realty\/foreign\/country_plot','Участок','\/realty\/foreign\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/realty\/foreign\/country_other','Другое','\/realty\/foreign\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/realty\/foreign\/office','Офис','\/realty\/foreign\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/realty\/foreign\/building','Здание','\/realty\/foreign\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/realty\/foreign\/business_place','Торговое помещение','\/realty\/foreign\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/realty\/foreign\/commercial_plot','Участок','\/realty\/foreign\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/realty\/foreign\/service','Предприятие сферы услуг','\/realty\/foreign\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/realty\/foreign\/factory_store','Склад','\/realty\/foreign\',3);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (16,'\/realty\/foreign\/commercial_other','Другое (коммерческая недвижимость)','\/realty\/foreign\',3);
COMMIT;

