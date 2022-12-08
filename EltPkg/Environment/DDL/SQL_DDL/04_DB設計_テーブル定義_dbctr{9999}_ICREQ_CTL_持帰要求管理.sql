DROP TABLE dbctr9999.ICREQ_CTL;
CREATE TABLE dbctr9999.ICREQ_CTL (
    REQ_TXT_NAME varchar2(32) NOT NULL,
    REQ_DATE number(8,0) default 0  NOT NULL,
    REQ_TIME number(9,0) default 0  NOT NULL,
    CLEARING_DATE_S number(8,0) default 0  NOT NULL,
    CLEARING_DATE_E number(8,0) default 0  NOT NULL,
    BILL_CODE varchar2(3),
    IC_TYPE number(1,0) default -1  NOT NULL,
    IMG_NEED number(1,0) default -1  NOT NULL,
    RET_TXT_NAME varchar2(32),
    RET_COUNT number(3,0) default 0  NOT NULL,
    RET_STS number(4,0) default 0  NOT NULL,
    RET_MAKE_DATE number(8,0) default 0  NOT NULL,
    RET_REQ_TXT_NAME varchar2(32),
    RET_FILE_CHK_CODE varchar2(10),
    RET_CLEARING_DATE_S varchar2(8),
    RET_CLEARING_DATE_E varchar2(8),
    RET_BILL_CODE varchar2(3),
    RET_IC_TYPE varchar2(1),
    RET_IMG_NEED varchar2(1),
    RET_PROC_RETCODE varchar2(10),
    RET_DATE number(8,0) default 0  NOT NULL,
    RET_TIME number(9,0) default 0  NOT NULL,
 CONSTRAINT PK_DBCTR9999_ICREQ_CTL PRIMARY KEY (
     REQ_TXT_NAME
));
COMMENT ON TABLE dbctr9999.ICREQ_CTL IS '���A�v���Ǘ�';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.REQ_TXT_NAME IS '�v���e�L�X�g�t�@�C����';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.REQ_DATE IS '�v����:�v���e�L�X�g�쐬���iOS���t�j';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.REQ_TIME IS '�v������:�v���e�L�X�g�쐬�����iOS�����j';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.CLEARING_DATE_S IS '������]���i�J�n�j';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.CLEARING_DATE_E IS '������]���i�I���j';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.BILL_CODE IS '�����،���ރR�[�h';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.IC_TYPE IS '���A�Ώۋ敪:"0"�F�u���A�󋵃t���O�v�̃X�e�[�^�X�ɂ�炸�S�� "1"�F�u���A�󋵃t���O�v��"0":�����A�̃f�[�^';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.IMG_NEED IS '�،��C���[�W�v��:"0"�F�،��C���[�W�s�v "1"�F�،��C���[�W�v';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_TXT_NAME IS '���ʃe�L�X�g�t�@�C����:(���ʃw�b�_�[���)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_COUNT IS '���ʌ���:(���ʃg���[���[���)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_STS IS '���ʋ�s�R�[�h:(���ʃw�b�_�[���)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_MAKE_DATE IS '���ʍ쐬��:(���ʃw�b�_�[���)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_REQ_TXT_NAME IS '���ʎ��A�v���e�L�X�g�t�@�C����:(���ʃw�b�_�[���)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_FILE_CHK_CODE IS '���ʃt�@�C���`�F�b�N���ʃR�[�h:(���ʃw�b�_�[���)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_CLEARING_DATE_S IS '���ʌ�����]���P:(���ʃw�b�_�[���)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_CLEARING_DATE_E IS '���ʌ�����]���Q:(���ʃw�b�_�[���)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_BILL_CODE IS '���ʌ����،���ރR�[�h:(���ʃw�b�_�[���)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_IC_TYPE IS '���ʎ��A�Ώۋ敪:(���ʃw�b�_�[���)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_IMG_NEED IS '���ʏ،��C���[�W�v�ۋ敪:(���ʃw�b�_�[���)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_PROC_RETCODE IS '���ʏ������ʃR�[�h:(���ʃw�b�_�[���)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_DATE IS '���ʎ捞��:�v�����ʎ捞���iOS���t�j';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_TIME IS '���ʎ捞����:�v�����ʎ捞�����iOS�����j';
exit;