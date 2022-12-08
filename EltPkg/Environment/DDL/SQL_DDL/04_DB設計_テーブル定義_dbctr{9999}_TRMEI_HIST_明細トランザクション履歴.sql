DROP TABLE dbctr9999.TRMEI_HIST;
CREATE TABLE dbctr9999.TRMEI_HIST (
    GYM_ID number(3,0) NOT NULL,
    OPERATION_DATE number(8,0) NOT NULL,
    SCAN_TERM varchar2(20) NOT NULL,
    BAT_ID number(6,0) NOT NULL,
    DETAILS_NO number(6,0) NOT NULL,
    SEQ number(3,0) NOT NULL,
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
    DELETE_DATE number(8,0) default 0  NOT NULL,
    DELETE_FLG number(1,0) default 0  NOT NULL,
    UPDATE_DATE number(8,0) default 0  NOT NULL,
    UPDATE_TIME number(9,0) default 0  NOT NULL,
    UPDATE_KBN number(1,0) default 1  NOT NULL,
 CONSTRAINT PK_DBCTR9999_TRMEI_HIST PRIMARY KEY (
     GYM_ID
    ,OPERATION_DATE
    ,SCAN_TERM
    ,BAT_ID
    ,DETAILS_NO
    ,SEQ
));
COMMENT ON TABLE dbctr9999.TRMEI_HIST IS '���׃g�����U�N�V��������';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.GYM_ID IS '�Ɩ��ԍ�';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.OPERATION_DATE IS '������';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.SCAN_TERM IS '�C���[�W�捞�[��';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.BAT_ID IS '�o�b�`�ԍ�';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.DETAILS_NO IS '���הԍ�';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.SEQ IS '�o�͘A��';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.DSP_ID IS '��ʔԍ�';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.IC_OC_BK_NO IS '���o��s(���A)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.IC_OLD_OC_BK_NO IS '�����o��s(���A)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.BUA_DATE IS '��d���o�ʒm��(���o)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.BUB_DATE IS '��d���o�ʒm��(���A)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.BCA_DATE IS '���o����ʒm��(���A)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.GMA_DATE IS '�،��f�[�^�����ʒm��(���o)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.GMB_DATE IS '�،��f�[�^�����ʒm��(���A)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.GRA_DATE IS '�s�n�ԊҒʒm��(���o)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.GXA_DATE IS '���ό�����ʒm��(���o)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.GXB_DATE IS '���ό�����ʒm��(���A)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.MRA_DATE IS '���Z�@�֓Ǒ֏��ύX�ʒm��(���o��s�R�[�h�ύX�E�p����s����)(���o)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.MRB_DATE IS '���Z�@�֓Ǒ֏��ύX�ʒm��(���o��s�R�[�h�ύX�E���A��s����)(���A)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.MRC_DATE IS '���Z�@�֓Ǒ֏��ύX�ʒm��(���A��s�R�[�h�ύX�E���o��s����)(���o)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.MRD_DATE IS '���Z�@�֓Ǒ֏��ύX�ʒm��(���A��s�R�[�h�ύX�E�p����s����)(���A)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.YCA_MARK IS '����s��(���A)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.EDIT_FLG IS '�ҏW�t���O';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.BCA_STS IS '���o����A�b�v���[�h���';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.GMA_STS IS '���A�����f�[�^�A�b�v���[�h���';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.GRA_STS IS '�s�n�Ԋғo�^�A�b�v���[�h���';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.GRA_CONFIRMDATE IS '�s�n�Ԋғo�^�A�b�v���[�h�m���';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.DELETE_DATE IS '�폜��';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.DELETE_FLG IS '�폜�t���O:0�F���폜 1�F�폜��';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.UPDATE_DATE IS '�X�V��:�X�V�V�X�e�����t';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.UPDATE_TIME IS '�X�V����:�X�V�V�X�e������';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.UPDATE_KBN IS '�X�V�敪:1�F�V�K�o�^ 2�F�A�b�v�f�[�g';
exit;