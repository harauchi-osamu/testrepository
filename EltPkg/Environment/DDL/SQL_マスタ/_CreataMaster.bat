@echo OFF

rem "[�ڑ����[�U�[]/[�p�X���[�h]@[�ڑ�������] AS SYSDBA" @[SQL�t�@�C����]

set DB_CON="sys/admin001@IPRUN AS SYSDBA"

rem dbctr
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_BANKMF_��s�}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_BILLMF_�،���ރ}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_BKCHANGEMF_��s�Ǒփ}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_ERA_�����}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_FUWATARIMF_�s�n���R�R�[�h�}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_HOLIDAY_�x���}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_OPERATION_DATE_�������t.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_CHANGE_DSPIDMF_��ʔԍ��ϊ��}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_GENERALTEXTMF_�e�L�X�g�ėp�}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_TERM_LOCK_�戵�[�����b�N.sql

rem dbctr9999
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_BRANCHMF_�x�X�}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_CHANGEMF_�Ǒփ}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_CTRUSERINFO_�d�q�������[�U�[���}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_DSP_ITEM_��ʍ��ڒ�`.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_DSP_PARAM_��ʃp�����[�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_FILE_PARAM_�t�@�C���p�����[�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_CTR_OCR_PARAM_�d�q������OCR�p�����[�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_CTR_OCRCONF_PARAM_�d�q������OCR�m�M�x�p�����[�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_GYM_PARAM_�Ɩ��p�����[�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_HOSEIMODE_DSP_ITEM_�␳���[�h��ʍ��ڒ�`.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_HOSEIMODE_PARAM_�␳���[�h�p�����[�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_IMG_CURSOR_PARAM_�C���[�W�J�[�\���p�����[�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_IMG_PARAM_�C���[�W�p�����[�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_ITEM_MASTER_���ڒ�`.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_PAYERMF_�x���l�}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_SUB_RTN_�T�u���[�`��.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_SYURUIMF_��ރ}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_CHANGE_BILLMF_�����،���ޕϊ��}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_CTR_MICRCUTINFO_PARAM_�d�q������MICR�؏o���p�����[�^.sql

pause
