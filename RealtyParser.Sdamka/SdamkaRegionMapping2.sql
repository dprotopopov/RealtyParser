CREATE TABLE IF NOT EXISTS SiteRegionMapping(
SiteId INTEGER,
RegionId INTEGER,
SiteRegionId VARCHAR,
PRIMARY KEY(SiteId,RegionId));
BEGIN;
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2932,'\– ОБЛАСТЬ –\Нижний Тагил');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2933,'\– ОБЛАСТЬ –\Каменск-Уральский');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2934,'\– ОБЛАСТЬ –\Первоуральск');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2935,'\– ОБЛАСТЬ –\Серов');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2936,'\– ОБЛАСТЬ –\Новоуральск');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2937,'\– ОБЛАСТЬ –\Асбест');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2938,'\– ОБЛАСТЬ –\Полевской');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2939,'\– ОБЛАСТЬ –\Краснотурьинск');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2940,'\– ОБЛАСТЬ –\Ревда');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2941,'\– ОБЛАСТЬ –\Верхняя Пышма');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2943,'\– ОБЛАСТЬ –\Верхняя Салда');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2944,'\– ОБЛАСТЬ –\Березовский');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2945,'\– ОБЛАСТЬ –\Качканар');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2946,'\– ОБЛАСТЬ –\Алапаевск');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2947,'\– ОБЛАСТЬ –\Ирбит');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2948,'\– ОБЛАСТЬ –\Красноуфимск');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2949,'\– ОБЛАСТЬ –\Реж');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2954,'\– ОБЛАСТЬ –\Богданович');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2961,'\– ОБЛАСТЬ –\Нижняя Тура');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2962,'\– ОБЛАСТЬ –\Кировград');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2963,'\– ОБЛАСТЬ –\Сысерть');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2964,'\– ОБЛАСТЬ –\Среднеуральск');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2965,'\– ОБЛАСТЬ –\Талица');
INSERT OR REPLACE INTO SiteRegionMapping(SiteId,RegionId,SiteRegionId) VALUES (11,2971,'\– ОБЛАСТЬ –\Арамиль');
COMMIT;

