DROP TABLE dbctr9999.TRMEI;
CREATE TABLE dbctr9999.TRMEI (
    GYM_ID number(3,0) NOT NULL,
    OPERATION_DATE number(8,0) NOT NULL,
    SCAN_TERM varchar2(20) NOT NULL,
    BAT_ID number(6,0) NOT NULL,
    DETAILS_NO number(6,0) NOT NULL,
    DSP_ID number(3,0) default 999  NOT NULL,
    IC_OC_BK_NO number(4,0) default -1  NOT NULL,
    IC_OLD_OC_BK_NO number(4,0) default -1  NOT NULL,
    BUA_DATE number(8,0) default 0  NOT NULL,
    BUB_DATE number(8,0) default 0  NOT NULL,
    BCA_DATE number(8,0) default 0  NOT NULL,
    GMA_DATE number(8,0) default 0  NOT NULL,
    GMB_DATE number(8,0) default 0  NOT NULL,
    GRA_DATE number(8,0) default 0  NOT NULL,
    GXA_DATE number(8,0) default 0  NOT NULL,
    GXB_DATE number(8,0) default 0  NOT NULL,
    MRA_DATE number(8,0) default 0  NOT NULL,
    MRB_DATE number(8,0) default 0  NOT NULL,
    MRC_DATE number(8,0) default 0  NOT NULL,
    MRD_DATE number(8,0) default 0  NOT NULL,
    YCA_MARK number(2,0) default 0  NOT NULL,
    EDIT_FLG number(1,0) default 0  NOT NULL,
    BCA_STS number(2,0) default 0  NOT NULL,
    GMA_STS number(2,0) default 0  NOT NULL,
    GRA_STS number(2,0) default 0  NOT NULL,
    GRA_CONFIRMDATE number(8,0) default 0  NOT NULL,
    MEMO varchar2(256),
    DELETE_DATE number(8,0) default 0  NOT NULL,
    DELETE_FLG number(1,0) default 0  NOT NULL,
 CONSTRAINT PK_DBCTR9999_TRMEI PRIMARY KEY (
     GYM_ID
    ,OPERATION_DATE
    ,SCAN_TERM
    ,BAT_ID
    ,DETAILS_NO
));
COMMENT ON TABLE dbctr9999.TRMEI IS '���׃g�����U�N�V����';
COMMENT ON COLUMN dbctr9999.TRMEI.GYM_ID IS '�Ɩ��ԍ�';
COMMENT ON COLUMN dbctr9999.TRMEI.OPERATION_DATE IS '������';
COMMENT ON COLUMN dbctr9999.TRMEI.SCAN_TERM IS '�C���[�W�捞�[��:IP�A�h���X�i.��_�ɕϊ��j';
COMMENT ON COLUMN dbctr9999.TRMEI.BAT_ID IS '�o�b�`�ԍ�';
COMMENT ON COLUMN dbctr9999.TRMEI.DETAILS_NO IS '���הԍ�';
COMMENT ON COLUMN dbctr9999.TRMEI.DSP_ID IS '��ʔԍ�';
COMMENT ON COLUMN dbctr9999.TRMEI.IC_OC_BK_NO IS '���o��s(���A)';
COMMENT ON COLUMN dbctr9999.TRMEI.IC_OLD_OC_BK_NO IS '�����o��s(���A)';
COMMENT ON COLUMN dbctr9999.TRMEI.BUA_DATE IS '��d���o�ʒm��(���o)';
COMMENT ON COLUMN dbctr9999.TRMEI.BUB_DATE IS '��d���o�ʒm��(���A)';
COMMENT ON COLUMN dbctr9999.TRMEI.BCA_DATE IS '���o����ʒm��(���A)';
COMMENT ON COLUMN dbctr9999.TRMEI.GMA_DATE IS '�،��f�[�^�����ʒm��(���o)';
COMMENT ON COLUMN dbctr9999.TRMEI.GMB_DATE IS '�،��f�[�^�����ʒm��(���A)';
COMMENT ON COLUMN dbctr9999.TRMEI.GRA_DATE IS '�s�n�ԊҒʒm��(���o)';
COMMENT ON COLUMN dbctr9999.TRMEI.GXA_DATE IS '���ό�����ʒm��(���o)';
COMMENT ON COLUMN dbctr9999.TRMEI.GXB_DATE IS '���ό�����ʒm��(���A)';
COMMENT ON COLUMN dbctr9999.TRMEI.MRA_DATE IS '���Z�@�֓Ǒ֏��ύX�ʒm��(���o��s�R�[�h�ύX�E�p����s����)(���o)';
COMMENT ON COLUMN dbctr9999.TRMEI.MRB_DATE IS '���Z�@�֓Ǒ֏��ύX�ʒm��(���o��s�R�[�h�ύX�E���A��s����)(���A)';
COMMENT ON COLUMN dbctr9999.TRMEI.MRC_DATE IS '���Z�@�֓Ǒ֏��ύX�ʒm��(���A��s�R�[�h�ύX�E���o��s����)(���o)';
COMMENT ON COLUMN dbctr9999.TRMEI.MRD_DATE IS '���Z�@�֓Ǒ֏��ύX�ʒm��(���A��s�R�[�h�ύX�E�p����s����)(���A)';
COMMENT ON COLUMN dbctr9999.TRMEI.YCA_MARK IS '����s��(���A):�O�F�Ȃ� �P�F����';
COMMENT ON COLUMN dbctr9999.TRMEI.EDIT_FLG IS '�ҏW�t���O:�O�F���ҏW �P�F�ҏW��';
COMMENT ON COLUMN dbctr9999.TRMEI.BCA_STS IS '���o����A�b�v���[�h���:0�F���쐬 1�F�č쐬�Ώ� 5�F�t�@�C���쐬 10�F�A�b�v���[�h 19�F���ʃG���[ 20�F���ʐ���';
COMMENT ON COLUMN dbctr9999.TRMEI.GMA_STS IS '���A�����f�[�^�A�b�v���[�h���:0�F���쐬 1�F�č쐬�Ώ� 5�F�t�@�C���쐬 10�F�A�b�v���[�h 19�F���ʃG���[ 20�F���ʐ���';
COMMENT ON COLUMN dbctr9999.TRMEI.GRA_STS IS '�s�n�Ԋғo�^�A�b�v���[�h���:0�F���쐬 1�F�č쐬�Ώ� 5�F�t�@�C���쐬 10�F�A�b�v���[�h 19�F���ʃG���[ 20�F���ʐ���';
COMMENT ON COLUMN dbctr9999.TRMEI.GRA_CONFIRMDATE IS '�s�n�Ԋғo�^�A�b�v���[�h�m���';
COMMENT ON COLUMN dbctr9999.TRMEI.MEMO IS '����';
COMMENT ON COLUMN dbctr9999.TRMEI.DELETE_DATE IS '�폜��';
COMMENT ON COLUMN dbctr9999.TRMEI.DELETE_FLG IS '�폜�t���O:0�F���폜 1�F�폜��';
exit;