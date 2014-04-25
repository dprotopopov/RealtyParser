CREATE TABLE IF NOT EXISTS SiteRubric(
SiteId INTEGER,
SiteRubricId VARCHAR,
SiteRubricTitle VARCHAR,
ParentId VARCHAR,
Level INTEGER,
PRIMARY KEY(SiteId,SiteRubricId));
BEGIN;
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/kvartira/\','Квартиры','\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/komnata/\','Комнаты','\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/koyko-mesta/\','Койко-места','\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/doma/\','Дома','\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/comercial/\','Комм. недвижимость','\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/yas/\','Поиск','\\',1);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/kvartira/\/kvartira/1kom/','Однокомнатные','\/kvartira/\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/kvartira/\/kvartira/2kom/','Двухкомнатные','\/kvartira/\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/kvartira/\/kvartira/3kom/','Трехкомнатные','\/kvartira/\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/kvartira/\/kvartira/4kom/','4х и более','\/kvartira/\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/kvartira/\/kvartira-dolgosrochno/','На длительный срок','\/kvartira/\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/kvartira/\/kvartira-sutki/','Посуточно','\/kvartira/\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/kvartira/\/kvartira-po-chasam/','По часам','\/kvartira/\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/kvartira/\/kvartira-sosedi/','Ищу соседа','\/kvartira/\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/komnata/\/komnata-dolgosrochno/','На длительный срок','\/komnata/\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/komnata/\/komnata-sutki/','Посуточно','\/komnata/\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/komnata/\/komnata-po-chasam/','По часам','\/komnata/\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/komnata/\/komnata-sosedi/','Ищу соседа','\/komnata/\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/koyko-mesta/\/koyko-mesta-dolgosrochno/','На длительный срок','\/koyko-mesta/\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/doma/\/doma-dolgosrochno/','На длительный срок','\/doma/\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/comercial/\/comercial/ofis/','Офисы','\/comercial/\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/comercial/\/comercial/pomeshenie/','Помещения','\/comercial/\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/comercial/\/comercial/sklad/','Склады','\/comercial/\',2);
INSERT OR REPLACE INTO SiteRubric(SiteId,SiteRubricId,SiteRubricTitle,ParentId,Level) VALUES (4,'\/comercial/\/comercial/uchastok/','Участки','\/comercial/\',2);
COMMIT;

