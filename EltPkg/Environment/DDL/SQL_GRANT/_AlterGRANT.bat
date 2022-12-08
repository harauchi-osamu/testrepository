@echo OFF

rem "[接続ユーザー]/[パスワード]@[接続文字列] AS SYSDBA" @[SQLファイル名]

set DB_CON="sys/admin001@IPRUN AS SYSDBA"

rem dbctr
sqlplus %DB_CON% @04_DB設計_ユーザ権限付与_DBCTR.sql

pause
