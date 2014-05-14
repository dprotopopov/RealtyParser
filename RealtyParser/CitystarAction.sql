CREATE TABLE IF NOT EXISTS SiteAction(
SiteId INTEGER,
SiteActionId VARCHAR,
SiteActionTitle VARCHAR,
ParentId VARCHAR,
Level INTEGER,
PRIMARY KEY(SiteId,SiteActionId));
BEGIN;
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (13,'\продам','Продам','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (13,'\куплю','Куплю','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (13,'\сдам','Сдам','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (13,'\сниму','Сниму','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (13,'\сдам посуточно','Сдам посуточно','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (13,'\продам','Продам','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (13,'\куплю','Куплю','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (13,'\сдам','Сдам','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (13,'\сниму','Сниму','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (13,'\продам','Продам','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (13,'\куплю','Куплю','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (13,'\сдам','Сдам','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (13,'\сниму','Сниму','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (13,'\продам','Продам','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (13,'\сдам','Сдам','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (13,'\обмен','Обмен','\',1);
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle,ParentId,Level) VALUES (13,'\продам','Продам','\',1);
COMMIT;

