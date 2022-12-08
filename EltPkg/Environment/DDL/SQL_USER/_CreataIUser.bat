@echo OFF

rem "[接続ユーザー]/[パスワード]@[接続文字列] AS SYSDBA" @[SQLファイル名]

set DB_CON="sys/admin001@IPRUN AS SYSDBA"

rem dbctr
sqlplus %DB_CON% @04_DB設計_ユーザ定義_DBCTR.sql
sqlplus %DB_CON% @04_DB設計_ユーザ定義_DBCTR9999.sql

pause
