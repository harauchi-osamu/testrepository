@echo OFF

rem "[�ڑ����[�U�[]/[�p�X���[�h]@[�ڑ�������] AS SYSDBA" @[SQL�t�@�C����]

set DB_CON="sys/admin001@IPRUN AS SYSDBA"

rem dbctr
sqlplus %DB_CON% @04_DB�݌v_���[�U�����t�^_DBCTR.sql

pause
