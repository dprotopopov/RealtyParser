CREATE TABLE IF NOT EXISTS SiteRegion(
SiteId INTEGER,
SiteRegionId VARCHAR,
SiteRegionTitle VARCHAR,
ParentId VARCHAR,
Level INTEGER,
PRIMARY KEY(SiteId,SiteRegionId));
BEGIN;
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\32-y-gorodok','32-й городок','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\avtovokzal-yuzhnyy','Автовокзал (Южный)','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\akademicheskiy','Академический','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\botanika','Ботаника','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\viz','ВИЗ','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\vokzal','Вокзал','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\vtorchermet','Вторчермет','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\vtuzgorodok','Втузгородок','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\elizavet','Елизавет','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\zhbi','ЖБИ','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\zavokzalnyy','Завокзальный','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\zarechnyy','Заречный','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\izoplit','Изоплит','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\istok','Исток','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\kamennye-palatki','Каменные палатки','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\keramika','Керамика','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\kolcovo','Кольцово','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\kompressornyy','Компрессорный','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\krasnolese','Краснолесье','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\lechebnyy','Лечебный','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\nizhne-isetskiy','Нижне-Исетский','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\novaya-sortirovka','Новая сортировка','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\parkovyy','Парковый','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\pionerskiy','Пионерский','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\pticefabrika','Птицефабрика','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\sem-klyuchey','Семь ключей','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\sibirskiy-trakt','Сибирский тракт','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\sinie-kamni','Синие камни','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\staraya-sortirovka','Старая сортировка','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\uktus','Уктус','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\unc','УНЦ','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\uralmash','Уралмаш','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\himmash','Химмаш','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\centr','Центр','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\shartash','Шарташ','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\shartashskiy-rynok','Шарташский рынок','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\shirokaya-rechka','Широкая речка','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\elmash','Эльмаш','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (12,'\yugo-zapadnyy','Юго-Западный','\',1);
COMMIT;

