DROP TABLE dbctr9999.TRBATCH;
CREATE TABLE dbctr9999.TRBATCH (
    GYM_ID number(3,0) NOT NULL,
    OPERATION_DATE number(8,0) NOT NULL,
    SCAN_TERM varchar2(20) NOT NULL,
    BAT_ID number(6,0) NOT NULL,
    STS number(2,0) default 0  NOT NULL,
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
    E_TERM varchar2(20),
    E_OPENO varchar2(20),
    E_YMD number(8,0) default 0  NOT NULL,
    E_TIME number(9,0) default 0  NOT NULL,
 CONSTRAINT PK_DBCTR9999_TRBATCH PRIMARY KEY (
     GYM_ID
    ,OPERATION_DATE
    ,SCAN_TERM
    ,BAT_ID
));
COMMENT ON TABLE dbctr9999.TRBATCH IS '�o�b�`�f�[�^';
COMMENT ON COLUMN dbctr9999.TRBATCH.GYM_ID IS '�Ɩ��ԍ�';
COMMENT ON COLUMN dbctr9999.TRBATCH.OPERATION_DATE IS '������';
COMMENT ON COLUMN dbctr9999.TRBATCH.SCAN_TERM IS '�C���[�W�捞�[��:IP�A�h���X�i.��_�ɕϊ��j';
COMMENT ON COLUMN dbctr9999.TRBATCH.BAT_ID IS '�o�b�`�ԍ�';
COMMENT ON COLUMN dbctr9999.TRBATCH.STS IS '���:0�F���͑҂� 1�F���͒� 5�F���͕ۗ� 10�F���͊���';
COMMENT ON COLUMN dbctr9999.TRBATCH.INPUT_ROUTE IS '�捞���[�g:1:�ʏ�o�b�`���[�g 2:�t�уo�b�`���[�g 3:�����Ǘ����[�g';
COMMENT ON COLUMN dbctr9999.TRBATCH.OC_BK_NO IS '���o��s�i���o�o�b�`�[�l�j';
COMMENT ON COLUMN dbctr9999.TRBATCH.OC_BR_NO IS '���o�x�X�i���o�o�b�`�[�l�j';
COMMENT ON COLUMN dbctr9999.TRBATCH.SCAN_BR_NO IS '�X�L�����x�X�i���o�o�b�`�[�l�j';
COMMENT ON COLUMN dbctr9999.TRBATCH.SCAN_DATE IS '�X�L�������i���o�o�b�`�[�l�j';
COMMENT ON COLUMN dbctr9999.TRBATCH.CLEARING_DATE IS '������]���i���o�o�b�`�[�l�j';
COMMENT ON COLUMN dbctr9999.TRBATCH.SCAN_COUNT IS '�X�L���������i���o�o�b�`�[�l�j';
COMMENT ON COLUMN dbctr9999.TRBATCH.TOTAL_COUNT IS '���v�����i���o�o�b�`�[�l�j';
COMMENT ON COLUMN dbctr9999.TRBATCH.TOTAL_AMOUNT IS '���v���z�i���o�o�b�`�[�l�j';
COMMENT ON COLUMN dbctr9999.TRBATCH.DELETE_DATE IS '�폜��';
COMMENT ON COLUMN dbctr9999.TRBATCH.DELETE_FLG IS '�폜�t���O:0�F���폜 1�F�폜��';
COMMENT ON COLUMN dbctr9999.TRBATCH.E_TERM IS '�o�b�`�[���͒[��:TERM.ini�̒l';
COMMENT ON COLUMN dbctr9999.TRBATCH.E_OPENO IS '�o�b�`�[���̓I�y���[�^�[�ԍ�';
COMMENT ON COLUMN dbctr9999.TRBATCH.E_YMD IS '�o�b�`�[���͓��t';
COMMENT ON COLUMN dbctr9999.TRBATCH.E_TIME IS '�o�b�`�[���͎���';
exit;