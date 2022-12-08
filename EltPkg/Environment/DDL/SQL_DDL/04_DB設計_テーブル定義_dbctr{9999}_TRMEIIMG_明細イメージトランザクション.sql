DROP TABLE dbctr9999.TRMEIIMG;
CREATE TABLE dbctr9999.TRMEIIMG (
    GYM_ID number(3,0) NOT NULL,
    OPERATION_DATE number(8,0) NOT NULL,
    SCAN_TERM varchar2(20) NOT NULL,
    BAT_ID number(6,0) NOT NULL,
    DETAILS_NO number(6,0) NOT NULL,
    IMG_KBN number(2,0) NOT NULL,
    IMG_FLNM varchar2(62),
    IMG_FLNM_OLD varchar2(100),
    OC_OC_BK_NO varchar2(4),
    OC_OC_BR_NO varchar2(4),
    OC_IC_BK_NO varchar2(4),
    OC_OC_DATE varchar2(8),
    OC_CLEARING_DATE varchar2(8),
    OC_AMOUNT varchar2(12),
    PAY_KBN varchar2(1),
    UNIQUE_CODE varchar2(15),
    FILE_EXTENSION varchar2(4),
    BUA_STS number(2,0) default 0  NOT NULL,
    BUB_CONFIRMDATE number(8,0) default 0  NOT NULL,
    BUA_DATE number(8,0) default 0  NOT NULL,
    BUA_TIME number(6,0) default 0  NOT NULL,
    GDA_DATE number(8,0) default 0  NOT NULL,
    GDA_TIME number(6,0) default 0  NOT NULL,
    IMG_ARCH_NAME varchar2(32),
    DELETE_DATE number(8,0) default 0  NOT NULL,
    DELETE_FLG number(1) default 0  NOT NULL,
 CONSTRAINT PK_DBCTR9999_TRMEIIMG PRIMARY KEY (
     GYM_ID
    ,OPERATION_DATE
    ,SCAN_TERM
    ,BAT_ID
    ,DETAILS_NO
    ,IMG_KBN
));
COMMENT ON TABLE dbctr9999.TRMEIIMG IS '���׃C���[�W�g�����U�N�V����';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.GYM_ID IS '�Ɩ��ԍ�';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.OPERATION_DATE IS '������';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.SCAN_TERM IS '�C���[�W�捞�[��:IP�A�h���X�i.��_�ɕϊ��j';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.BAT_ID IS '�o�b�`�ԍ�';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.DETAILS_NO IS '���הԍ�';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.IMG_KBN IS '�\�����̕�:01:�\ 02:�� 03:��� 04:�t� 05:�����ؖ� 06:�\�i�đ����j 07:���i�đ����j 08�`10:���̑�';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.IMG_FLNM IS '�،��C���[�W�t�@�C����';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.IMG_FLNM_OLD IS '�X�L�����A�g���t�@�C����';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.OC_OC_BK_NO IS '(���o�t�@�C�����v�f)���o��s�R�[�h:�O�[������';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.OC_OC_BR_NO IS '(���o�t�@�C�����v�f)���o�x�X�R�[�h:�O�[������ �ȗ��l��';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.OC_IC_BK_NO IS '(���o�t�@�C�����v�f)���A��s�R�[�h:�O�[������ �ȗ��l��';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.OC_OC_DATE IS '(���o�t�@�C�����v�f)���o��:�C���[�W�捞��';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.OC_CLEARING_DATE IS '(���o�t�@�C�����v�f)������]��:�␳�m��l';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.OC_AMOUNT IS '(���o�t�@�C�����v�f)���z:�O�[������ �ȗ��l��';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.PAY_KBN IS '(���o�t�@�C�����v�f)���ϑΏۋ敪:0:���ϑΏ� 1:���ϑΏۊO';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.UNIQUE_CODE IS '(���o�t�@�C�����v�f)��ӃR�[�h:�C���[�W�捞�ɂĊm��';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.FILE_EXTENSION IS '(���o�t�@�C�����v�f)�g���q:".jpg"';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.BUA_STS IS '���o�A�b�v���[�h���(���o):0�F���쐬 1�F�č쐬�Ώ� 5�F�t�@�C���쐬 10�F�A�b�v���[�h 19�F���ʃG���[ 20�F���ʐ���';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.BUB_CONFIRMDATE IS '���o�A�b�v���[�h�m���(���o)';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.BUA_DATE IS '���o��(���o)';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.BUA_TIME IS '���o����(���o)';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.GDA_DATE IS '(���A)���A�捞��';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.GDA_TIME IS '���A�捞����(���A)';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.IMG_ARCH_NAME IS '�C���[�W�A�[�J�C�u�t�@�C����(����)';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.DELETE_DATE IS '�폜��';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.DELETE_FLG IS '�폜�t���O';
exit;