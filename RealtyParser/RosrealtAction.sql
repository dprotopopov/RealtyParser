CREATE TABLE IF NOT EXISTS SiteAction(
SiteId INTEGER,
SiteActionId VARCHAR,
SiteActionTitle VARCHAR,
PRIMARY KEY(SiteId,SiteActionId));
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle) VALUES (2,'0',' - Сделка - ');
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle) VALUES (2,'1','продажа');
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle) VALUES (2,'2','покупка');
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle) VALUES (2,'3','аренда');
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle) VALUES (2,'4','сниму');
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle) VALUES (2,'5','посуточная аренда');
INSERT OR REPLACE INTO SiteAction(SiteId,SiteActionId,SiteActionTitle) VALUES (2,'6','обмен');
