DROP TABLE dbctr.SCAN_MEI;
CREATE TABLE dbctr.SCAN_MEI (
    IMG_NAME varchar2(100) NOT NULL,
    OPERATION_DATE number(8,0) NOT NULL,
    INPUT_ROUTE number(1,0) default 0  NOT NULL,
    BATCH_FOLDER_NAME varchar2(20) NOT NULL,
    BATCH_UCHI_RENBAN number(6,0) default 0  NOT NULL,
    IMG_KBN number(2,0) default 0  NOT NULL,
    I_TERM varchar2(20),
    I_OPENO varchar2(20),
    I_YMD number(8,0) default 0  NOT NULL,
    I_TIME number(9,0) default 0  NOT NULL,
 CONSTRAINT PK_DBCTR_SCAN_MEI PRIMARY KEY (
     IMG_NAME
    ,OPERATION_DATE
    ,BATCH_FOLDER_NAME
));

COMMENT ON TABLE dbctr.SCAN_MEI IS '�X�L��������';
COMMENT ON COLUMN dbctr.SCAN_MEI.IMG_NAME IS '�C���[�W�t�@�C����';
COMMENT ON COLUMN dbctr.SCAN_MEI.OPERATION_DATE IS '������';
COMMENT ON COLUMN dbctr.SCAN_MEI.INPUT_ROUTE IS '�捞���[�g:2:�t�уo�b�`���[�g';
COMMENT ON COLUMN dbctr.SCAN_MEI.BATCH_FOLDER_NAME IS '�o�b�`�t�H���_��';
COMMENT ON COLUMN dbctr.SCAN_MEI.BATCH_UCHI_RENBAN IS '�o�b�`���A��';
COMMENT ON COLUMN dbctr.SCAN_MEI.IMG_KBN IS '�\�����̕�';
COMMENT ON COLUMN dbctr.SCAN_MEI.I_TERM IS '���͒[���ԍ�';
COMMENT ON COLUMN dbctr.SCAN_MEI.I_OPENO IS '���̓I�y���[�^�[�ԍ�';
COMMENT ON COLUMN dbctr.SCAN_MEI.I_YMD IS '���͓��t';
COMMENT ON COLUMN dbctr.SCAN_MEI.I_TIME IS '���͎���';
exit;