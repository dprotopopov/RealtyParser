BEGIN;
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (5,'Аренда::Предложение','Аренда::Предложение','Всё::',2);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (5,'Аренда::Спрос','Аренда::Спрос','Всё::',2);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (5,'Продажа::Предложение','Продажа::Предложение','Всё::',2);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (5,'Продажа::Спрос','Продажа::Спрос','Всё::',2);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (5,'Посуточная, почасовая, сезонная аренда::','Посуточная, почасовая, сезонная аренда::','Всё::',2);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (5,'Обмен жилья::','Обмен жилья::','Всё::',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (5,'Всё::','Всё::','Всё::',1);
COMMIT;
