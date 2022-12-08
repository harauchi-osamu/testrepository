DROP TABLE dbctr9999.TRBATCH_HIST;
CREATE TABLE dbctr9999.TRBATCH_HIST (
    GYM_ID number(3,0) NOT NULL,
    OPERATION_DATE number(8,0) NOT NULL,
    SCAN_TERM varchar(20) NOT NULL,
    BAT_ID number(6,0) NOT NULL,
    SEQ number(3,0) NOT NULL,
    STS number(2) default 0  NOT NULL,
    INPUT_ROUTE number(1,0) default 0  NOT NULL,
    OC_BK_NO number(4,0) default -1  NOT NULL,
    OC_BR_NO number(4,0) default -1  NOT NULL,
    SCAN_BR_NO number(4,0) default -1  NOT NULL,
    SCAN_DATE number(8,0) default 0  NOT NULL,
    CLEARING_DATE number(8,0) default 0  NOT NULL,
    SCAN_COUNT number(6,0) default 0  NOT NULL,
    TOTAL_COUNT number(6,0) default 0  NOT NULL,
    TOTAL_AMOUNT number(18,0) default 0  NOT NULL,
    DELETE_DATE number(8,0) default 0  NOT NULL,
    DELETE_FLG number(1,0) default 0  NOT NULL,
    E_TERM varchar(20),
    E_OPENO varchar(20),
    E_YMD number(8,0) default 0  NOT NULL,
    E_TIME number(11,0) default 0  NOT NULL,
    UPDATE_DATE number(8,0) default 0  NOT NULL,
    UPDATE_TIME number(11,0) default 0  NOT NULL,
    UPDATE_KBN number(1,0) default 1  NOT NULL,
 CONSTRAINT PK_DBCTR9999_TRBATCH_HIST PRIMARY KEY (
     GYM_ID
    ,OPERATION_DATE
    ,SCAN_TERM
    ,BAT_ID
    ,SEQ
));
COMMENT ON TABLE dbctr9999.TRBATCH_HIST IS '�o�b�`�f�[�^����';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.GYM_ID IS '�Ɩ��ԍ�';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.OPERATION_DATE IS '������';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.SCAN_TERM IS '�C���[�W�捞�[��';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.BAT_ID IS '�o�b�`�ԍ�';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.SEQ IS '�o�͘A��';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.STS IS '���';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.INPUT_ROUTE IS '�捞���[�g';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.OC_BK_NO IS '���o��s�i���o�o�b�`�[�l�j';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.OC_BR_NO IS '���o�x�X�i���o�o�b�`�[�l�j';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.SCAN_BR_NO IS '�X�L�����x�X�i���o�o�b�`�[�l�j';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.SCAN_DATE IS '�X�L�������i���o�o�b�`�[�l�j';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.CLEARING_DATE IS '������]���i���o�o�b�`�[�l�j';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.SCAN_COUNT IS '�X�L���������i���o�o�b�`�[�l�j';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.TOTAL_COUNT IS '���v�����i���o�o�b�`�[�l�j';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.TOTAL_AMOUNT IS '���v���z�i���o�o�b�`�[�l�j';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.DELETE_DATE IS '�폜��';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.DELETE_FLG IS '�폜�t���O';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.E_TERM IS '�o�b�`�[���͒[��';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.E_OPENO IS '�o�b�`�[���̓I�y���[�^�[�ԍ�';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.E_YMD IS '�o�b�`�[���͓��t';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.E_TIME IS '�o�b�`�[���͎���';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.UPDATE_DATE IS '�X�V��:�X�V�V�X�e�����t';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.UPDATE_TIME IS '�X�V����:�X�V�V�X�e������';
COMMENT ON COLUMN dbctr9999.TRBATCH_HIST.UPDATE_KBN IS '�X�V�敪:1�F�V�K�o�^ 2�F�A�b�v�f�[�g';
exit;