CREATE TABLE IF NOT EXISTS SiteRegion(
SiteId INTEGER,
SiteRegionId VARCHAR,
SiteRegionTitle VARCHAR,
ParentId VARCHAR,
Level INTEGER,
PRIMARY KEY(SiteId,SiteRegionId));
BEGIN;
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/ua/','Украина','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/by/','Беларусь','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/ru/','Вся Россия','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/msk','Москва','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/spb','Санкт-Петербург','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/astrakhan','Астрахань','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/barnaul','Барнаул','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/vladivostok','Владивосток','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/volgograd','Волгоград','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/vrn','Воронеж','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/ekb','Екатеринбург','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/izh','Ижевск','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/irk','Иркутск','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/kzn','Казань','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/klgd','Калининград','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/kaluga','Калуга','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/kemer','Кемерово','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/kirov','Киров','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/krd','Краснодар','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/krsk','Красноярск','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/lipetsk','Липецк','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/msk','Москва','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/chelny','Набережные Челны','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/nn','Нижний Новгород','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/nkz','Новокузнецк','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/nsk','Новосибирск','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/omsk','Омск','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/oren','Оренбург','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/pnz','Пенза','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/perm','Пермь','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/rnd','Ростов-на-Дону','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/ryazan','Рязань','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/samara','Самара','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/spb','Санкт-Петербург','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/sar','Саратов','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/sochi','Сочи','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/stv','Ставрополь','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/tver','Тверь','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/tlt','Тольятти','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/tula','Тула','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/tmn','Тюмень','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/ulsk','Ульяновск','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/ufa','Уфа','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/khb','Хабаровск','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/chel','Челябинск','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/yar','Ярославль','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/adygeya','Адыгея','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/altay','Алтай','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/altayskiy-kray','Алтайский край','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/amur-obl','Амурская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/arkhangelsk-obl','Архангельская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/astrakhan-obl','Астраханская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/bashkortostan','Башкортостан','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/belgorod-obl','Белгородская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/bryansk-obl','Брянская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/buryatiya','Бурятия','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/vladimir-obl','Владимирская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/volgograd-obl','Волгоградская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/vologodsk-obl','Вологодская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/voronezh-obl','Воронежская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/dagestan','Дагестан','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/eao','Еврейская АО','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/zabaykalskiy-kray','Забайкальский край','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/ivanovo-obl','Ивановская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/ingushetiya','Ингушетия','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/irkutsk-obl','Иркутская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/kabardino-balkariya','Кабардино-Балкария','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/kaliningrad-obl','Калининградская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/kalmykiya','Калмыкия','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/kaluga-obl','Калужская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/kamchatskiy-kray','Камчатский край','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/cherkessiya','Карачаево-Черкессия','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/kareliya','Карелия','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/kemerovo-obl','Кемеровская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/kirov-obl','Кировская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/komi-obl','Коми','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/kostroma-obl','Костромская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/krasnodarsk-kray','Краснодарский край','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/krasnoyarsk-kray','Красноярский край','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/kurgan-obl','Курганская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/kursk-obl','Курская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/leningrad-obl','Ленинградская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/lipetsk-obl','Липецкая обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/magadan-obl','Магаданская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/mariy-el','Марий-Эл','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/mordoviya','Мордовия','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/moscow-obl','Московская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/murmansk-obl','Мурманская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/nao','Ненецкий АО','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/nizhegorod-obl','Нижегородская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/novgorod-obl','Новгородская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/novosibirsk-obl','Новосибирская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/omsk-obl','Омская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/orenburg-obl','Оренбургская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/orlovskaya-obl','Орловская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/penza-obl','Пензенская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/perm-kray','Пермский край','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/primorskiy-kray','Приморский край','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/pskov-obl','Псковская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/rostov-obl','Ростовская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/ryazan-obl','Рязанская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/samara-obl','Самарская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/saratov-obl','Саратовская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/saha','Саха','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/sahalin-obl','Сахалинская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/sverdlovsk-obl','Свердловская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/alaniya','Северная Осетия-Алания','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/smolensk-obl','Смоленская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/stavropolskiy-kray','Ставропольский край','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/tambov-obl','Тамбовская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/tatarstan','Татарстан','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/tver-obl','Тверская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/tomsk-obl','Томская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/tula-obl','Тульская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/tyva','Тыва','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/tumen-obl','Тюменская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/udmurtiya','Удмуртия','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/ulyanovsk-obl','Ульяновская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/khabarovsk-kray','Хабаровский край','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/hakasiya','Хакасия','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/hmao','Ханты-Мансийский АО','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/chelyabinsk-obl','Челябинская обл.','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/chechnya','Чечня','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/chuvashiya','Чувашия','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/chao','Чукотский АО','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/yamao','Ямало-Ненецкий АО','\',1);
INSERT OR REPLACE INTO SiteRegion(SiteId,SiteRegionId,SiteRegionTitle,ParentId,Level) VALUES (7,'\http://www.beboss.ru/kn/yaroslavl-obl','Ярославская обл.','\',1);
COMMIT;

