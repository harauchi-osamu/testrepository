@echo OFF

rem "[�ڑ����[�U�[]/[�p�X���[�h]@[�ڑ�������] AS SYSDBA" @[SQL�t�@�C����]

set DB_CON="sys/admin001@IPRUN AS SYSDBA"

rem dbctr
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_BANKMF_��s�}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_BKCHANGEMF_��s�Ǒփ}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_HOLIDAY_�x���}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_OCR_DATA_OCR�F������.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_OPERATION_DATE_�������t.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_SCAN_BATCH_CTL_�X�L�����o�b�`�Ǘ�.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_SCAN_MEI_�X�L��������.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_TERM_LOCK_�戵�[�����b�N.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_WK_IMGELIST_���׃C���[�W���X�g.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_CHANGE_DSPIDMF_��ʔԍ��ϊ��}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_FUWATARIMF_�s�n���R�R�[�h�}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�X�g�A�h_dbctr_SCAN_BATCH_CTL_EXTRACTION_�o�b�`�f�[�^�擾.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_GENERALTEXTMF_�e�L�X�g�ėp�}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_LOCK_SYSTEM_PROCESS_�V�X�e���v���Z�X���b�N.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_BR_TOTAL_�x�X�ʍ��v�[.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_OPERATOR_�I�y���[�^�}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_IC_OCR_FINISH_���AOCR��������.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_BILLMF_�����،���ރ}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr_ERA_�����}�X�^.sql

rem dbctr9999
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_BRANCHMF_�x�X�}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_CHANGEMF_�Ǒփ}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_CTRUSERINFO_�d�q�������[�U�[���}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_DSP_ITEM_��ʍ��ڒ�`.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_DSP_PARAM_��ʃp�����[�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_FILE_CTL_�t�@�C���W�z�M�Ǘ�.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_FILE_SEQ_�t�@�C����A�ԍ��̔�.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_GYM_PARAM_�Ɩ��p�����[�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_HOSEIMODE_DSP_ITEM_�␳���[�h��ʍ��ڒ�`.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_HOSEIMODE_PARAM_�␳���[�h�p�����[�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_ICREQRET_BILLMEITXT_���A�v�����ʏ،����׃e�L�X�g.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_ICREQRET_CTL_���A�v�����ʊǗ�.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_HOSEI_STATUS_�␳�X�e�[�^�X.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_IMG_CURSOR_PARAM_�C���[�W�J�[�\���p�����[�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_IMG_PARAM_�C���[�W�p�����[�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_ITEM_MASTER_���ڒ�`.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_PAYERMF_�x���l�}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_RESULTTXT_CTL_���ʃe�L�X�g�Ǘ�.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_RESULTTXT_���ʃe�L�X�g.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_SYURUIMF_��ރ}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_TRBATCH_�o�b�`�f�[�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_TRFUWATARI_�s�n���׃g�����U�N�V����.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_TRITEM_���ڃg�����U�N�V����.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_TRMEIIMG_���׃C���[�W�g�����U�N�V����.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_TRMEI_���׃g�����U�N�V����.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_BATCH_SEQ_�o�b�`�ԍ��̔�.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_TRBATCHIMG_�o�b�`�C���[�W�g�����U�N�V����.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_BALANCETXT_CTL_�����K�Ǘ�.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_BALANCETXT_�����K.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_FILE_PARAM_�t�@�C���p�����[�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_BILLMEITXT_CTL_�،����׃e�L�X�g�Ǘ�.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_BILLMEITXT_�،����׃e�L�X�g.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_CTR_OCR_PARAM_�d�q������OCR�p�����[�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_ICREQ_CTL_���A�v���Ǘ�.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_TRITEM_HIST_���ڃg�����U�N�V��������.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_TRBATCH_HIST_�o�b�`�f�[�^����.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_TSUCHITXT_CTL_�ʒm�e�L�X�g�Ǘ�.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_TSUCHITXT_�ʒm�e�L�X�g.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_LOCK_BANK_PROCESS_��s�ʋƖ����b�N.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_TRMEI_HIST_���׃g�����U�N�V��������.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_TRMEIIMG_HIST_���׃C���[�W�g�����U�N�V��������.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_SUB_RTN_�T�u���[�`��.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_CTR_OCRCONF_PARAM_�d�q������OCR�m�M�x�p�����[�^.sql
sqlplus %DB_CON% @04_DB�݌v_�g���K�[_dbctr{9999}_TRBATCH_LOG_TRG_�o�b�`�f�[�^����.sql
sqlplus %DB_CON% @04_DB�݌v_�g���K�[_dbctr{9999}_TRITEM_LOG_TRG_���ڃg�����U�N�V��������.sql
sqlplus %DB_CON% @04_DB�݌v_�g���K�[_dbctr{9999}_TRMEI_LOG_TRG_���׃g�����U�N�V��������.sql
sqlplus %DB_CON% @04_DB�݌v_�g���K�[_dbctr{9999}_TRMEIIMG_LOG_TRG_���׃C���[�W�g�����U�N�V��������.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_CHANGE_BILLMF_�����،���ޕϊ��}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�r���[��`_dbctr{9999}_BRANCHMF_�x�X�ʍ��v�[�x�X�}�X�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_CTR_MICRCUTINFO_PARAM_�d�q������MICR�؏o���p�����[�^.sql
sqlplus %DB_CON% @04_DB�݌v_�e�[�u����`_dbctr{9999}_SEND_FILE_TRMEI_�z�M�t�@�C�����ד���.sql

pause
