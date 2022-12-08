DROP TABLE dbctr.BR_TOTAL;
CREATE TABLE dbctr.BR_TOTAL (
    GYM_ID number(3,0) NOT NULL,
    OPERATION_DATE number(8,0) NOT NULL,
    SCAN_IMG_FLNM varchar2(100) NOT NULL,
    IMPORT_IMG_FLNM varchar2(62),
    BK_NO number(4,0) default -1  NOT NULL,
    BR_NO number(4,0) default -1  NOT NULL,
    SCAN_DATE number(8,0) default 0  NOT NULL,
    SCAN_BR_NO number(4,0) default -1  NOT NULL,
    TOTAL_COUNT number(6,0) default 0  NOT NULL,
    TOTAL_AMOUNT number(18,0) default 0  NOT NULL,
    STATUS number(1,0) default 0  NOT NULL,
    LOCK_TERM varchar2(20),
CONSTRAINT PK_DBCTR_BR_TOTAL PRIMARY KEY (
     GYM_ID
    ,OPERATION_DATE
    ,SCAN_IMG_FLNM
));
COMMENT ON TABLE dbctr.BR_TOTAL IS '�x�X�ʍ��v�[';
COMMENT ON COLUMN dbctr.BR_TOTAL.GYM_ID IS '�Ɩ��ԍ�';
COMMENT ON COLUMN dbctr.BR_TOTAL.OPERATION_DATE IS '������';
COMMENT ON COLUMN dbctr.BR_TOTAL.SCAN_IMG_FLNM IS '�X�L�����C���[�W�t�@�C����';
COMMENT ON COLUMN dbctr.BR_TOTAL.IMPORT_IMG_FLNM IS '�捞��C���[�W�t�@�C����';
COMMENT ON COLUMN dbctr.BR_TOTAL.BK_NO IS '���o��s';
COMMENT ON COLUMN dbctr.BR_TOTAL.BR_NO IS '���o�x�X';
COMMENT ON COLUMN dbctr.BR_TOTAL.SCAN_DATE IS '�X�L������';
COMMENT ON COLUMN dbctr.BR_TOTAL.SCAN_BR_NO IS '�X�L�����x�X';
COMMENT ON COLUMN dbctr.BR_TOTAL.TOTAL_COUNT IS '������';
COMMENT ON COLUMN dbctr.BR_TOTAL.TOTAL_AMOUNT IS '�����z';
COMMENT ON COLUMN dbctr.BR_TOTAL.STATUS IS '���:1:������ 2:������ 3:������ 5:�ۗ� 9:�폜';
COMMENT ON COLUMN dbctr.BR_TOTAL.LOCK_TERM IS '���b�N:�C���[�W�捞�̓��͉�ʂ��J���Ă���R���s���[�^�[���� TERM.init���ݒ�';
exit;