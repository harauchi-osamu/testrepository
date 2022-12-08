DROP TABLE dbctr.SCAN_BATCH_CTL;
CREATE TABLE dbctr.SCAN_BATCH_CTL (
    INPUT_ROUTE number(1,0) NOT NULL,
    BATCH_FOLDER_NAME varchar2(20) NOT NULL,
    OC_BK_NO number(4,0) default -1  NOT NULL,
    OC_BR_NO number(4,0) default -1  NOT NULL,
    SCAN_BR_NO number(4,0) default -1  NOT NULL,
    SCAN_DATE number(8,0) default 0  NOT NULL,
    CLEARING_DATE number(8,0) default 0  NOT NULL,
    SCAN_COUNT number(6,0) default 0  NOT NULL,
    TOTAL_COUNT number(6,0) default 0  NOT NULL,
    TOTAL_AMOUNT number(18,0) default 0  NOT NULL,
    IMAGE_COUNT number(7,0) default 0  NOT NULL,
    STATUS number(1,0) default 0  NOT NULL,
    LOCK_TERM varchar2(20),
CONSTRAINT PK_DBCTR_SCAN_BATCH_CTL PRIMARY KEY (
     INPUT_ROUTE
    ,BATCH_FOLDER_NAME
));

COMMENT ON TABLE dbctr.SCAN_BATCH_CTL IS '�X�L�����o�b�`�Ǘ�';
COMMENT ON COLUMN dbctr.SCAN_BATCH_CTL.INPUT_ROUTE IS '�捞���[�g:1:�ʏ�o�b�`���[�g 2:�t�уo�b�`���[�g';
COMMENT ON COLUMN dbctr.SCAN_BATCH_CTL.BATCH_FOLDER_NAME IS '�o�b�`�t�H���_��';
COMMENT ON COLUMN dbctr.SCAN_BATCH_CTL.OC_BK_NO IS '���o��s�R�[�h';
COMMENT ON COLUMN dbctr.SCAN_BATCH_CTL.OC_BR_NO IS '���o�x�X�R�[�h';
COMMENT ON COLUMN dbctr.SCAN_BATCH_CTL.SCAN_BR_NO IS '�X�L�����x�X�R�[�h';
COMMENT ON COLUMN dbctr.SCAN_BATCH_CTL.SCAN_DATE IS '�X�L������';
COMMENT ON COLUMN dbctr.SCAN_BATCH_CTL.CLEARING_DATE IS '������]��';
COMMENT ON COLUMN dbctr.SCAN_BATCH_CTL.SCAN_COUNT IS '�X�L��������';
COMMENT ON COLUMN dbctr.SCAN_BATCH_CTL.TOTAL_COUNT IS '���v����';
COMMENT ON COLUMN dbctr.SCAN_BATCH_CTL.TOTAL_AMOUNT IS '���v���z';
COMMENT ON COLUMN dbctr.SCAN_BATCH_CTL.IMAGE_COUNT IS '�C���[�W��';
COMMENT ON COLUMN dbctr.SCAN_BATCH_CTL.STATUS IS '���:1:������ 2:������ 3:������ 5:�ۗ� 9:�폜';
COMMENT ON COLUMN dbctr.SCAN_BATCH_CTL.LOCK_TERM IS '���b�N���:�C���[�W�捞�̃o�b�`�[��ʂ��J���Ă���R���s���[�^�[����';
exit;