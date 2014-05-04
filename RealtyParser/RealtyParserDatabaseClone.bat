copy /v /y RealtyParser.sqlite3 RosrealtParser.sqlite3
copy /v /y RealtyParser.sqlite3 MirkvartirParser.sqlite3
copy /v /y RealtyParser.sqlite3 EgentParser.sqlite3
copy /v /y RealtyParser.sqlite3 NetAgentaParser.sqlite3
copy /v /y RealtyParser.sqlite3 BnParser.sqlite3
copy /v /y RealtyParser.sqlite3 BeBossParser.sqlite3
copy /v /y RealtyParser.sqlite3 Arenda66Parser.sqlite3
copy /v /y RealtyParser.sqlite3 UpnParser.sqlite3
copy /v /y RealtyParser.sqlite3 Kvadrat66Parser.sqlite3
copy /v /y RealtyParser.sqlite3 SdamkaParser.sqlite3
copy /v /y RealtyParser.sqlite3 UralstudentParser.sqlite3

sqlite3 RosrealtParser.sqlite3 < RosrealtVacuum.sql
sqlite3 MirkvartirParser.sqlite3 < MirkvartirVacuum.sql
sqlite3 EgentParser.sqlite3 < EgentVacuum.sql
sqlite3 NetAgentaParser.sqlite3 < NetAgentaVacuum.sql
sqlite3 BnParser.sqlite3 < BnVacuum.sql
sqlite3 BeBossParser.sqlite3 < BeBossVacuum.sql
sqlite3 Arenda66Parser.sqlite3 < Arenda66Vacuum.sql
sqlite3 UpnParser.sqlite3 < UpnVacuum.sql
sqlite3 Kvadrat66Parser.sqlite3 < Kvadrat66Vacuum.sql
sqlite3 SdamkaParser.sqlite3 < SdamkaVacuum.sql
sqlite3 UralstudentParser.sqlite3 < UralstudentVacuum.sql

copy /v /y RosrealtParser.sqlite3 ..\RealtyParser.Rosrealt\RosrealtParser.sqlite3
copy /v /y MirkvartirParser.sqlite3 ..\RealtyParser.Mirkvartir\MirkvartirParser.sqlite3
copy /v /y EgentParser.sqlite3 ..\RealtyParser.Egent\EgentParser.sqlite3
copy /v /y NetAgentaParser.sqlite3 ..\RealtyParser.NetAgenta\NetAgentaParser.sqlite3
copy /v /y BnParser.sqlite3 ..\RealtyParser.Bn\BnParser.sqlite3
copy /v /y BeBossParser.sqlite3 ..\RealtyParser.BeBoss\BeBossParser.sqlite3
copy /v /y Arenda66Parser.sqlite3 ..\RealtyParser.Arenda66\Arenda66Parser.sqlite3
copy /v /y UpnParser.sqlite3 ..\RealtyParser.Upn\UpnParser.sqlite3
copy /v /y Kvadrat66Parser.sqlite3 ..\RealtyParser.Kvadrat66\Kvadrat66Parser.sqlite3
copy /v /y SdamkaParser.sqlite3 ..\RealtyParser.Sdamka\SdamkaParser.sqlite3
copy /v /y UralstudentParser.sqlite3 ..\RealtyParser.Uralstudent\UralstudentParser.sqlite3

copy /v /y RealtyParser.sqlite3 ..\RealtyParser.Editor\RealtyParser.sqlite3
copy /v /y RosrealtParser.sqlite3 ..\RealtyParser.Editor\RosrealtParser.sqlite3
copy /v /y MirkvartirParser.sqlite3 ..\RealtyParser.Editor\MirkvartirParser.sqlite3
copy /v /y EgentParser.sqlite3 ..\RealtyParser.Editor\EgentParser.sqlite3
copy /v /y NetAgentaParser.sqlite3 ..\RealtyParser.Editor\NetAgentaParser.sqlite3
copy /v /y BnParser.sqlite3 ..\RealtyParser.Editor\BnParser.sqlite3
copy /v /y BeBossParser.sqlite3 ..\RealtyParser.Editor\BeBossParser.sqlite3
copy /v /y Arenda66Parser.sqlite3 ..\RealtyParser.Editor\Arenda66Parser.sqlite3
copy /v /y UpnParser.sqlite3 ..\RealtyParser.Editor\UpnParser.sqlite3
copy /v /y Kvadrat66Parser.sqlite3 ..\RealtyParser.Editor\Kvadrat66Parser.sqlite3
copy /v /y SdamkaParser.sqlite3 ..\RealtyParser.Editor\SdamkaParser.sqlite3
copy /v /y UralstudentParser.sqlite3 ..\RealtyParser.Editor\UralstudentParser.sqlite3
