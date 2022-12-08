DROP TABLE dbctr9999.TRITEM_HIST;
CREATE TABLE dbctr9999.TRITEM_HIST (
    GYM_ID number(3,0) NOT NULL,
    OPERATION_DATE number(8,0) NOT NULL,
    SCAN_TERM varchar2(20) NOT NULL,
    BAT_ID number(6,0) NOT NULL,
    DETAILS_NO number(6,0) NOT NULL,
    ITEM_ID number(3,0) NOT NULL,
    SEQ number(3,0) NOT NULL,
    ITEM_NAME varchar2(40),
    OCR_ENT_DATA varchar2(100),
    OCR_VFY_DATA varchar2(100),
    ENT_DATA varchar2(100),
    VFY_DATA varchar2(100),
    END_DATA varchar2(100),
    BUA_DATA varchar2(100),
    CTR_DATA varchar2(100),
    ICTEISEI_DATA varchar2(100),
    MRC_CHG_BEFDATA varchar2(100),
    E_TERM varchar2(20),
    E_OPENO varchar2(20),
    E_STIME number(9,0) default 0  NOT NULL,
    E_ETIME number(9,0) default 0  NOT NULL,
    E_YMD number(8,0) default 0  NOT NULL,
    E_TIME number(9,0) default 0  NOT NULL,
    V_TERM varchar2(20),
    V_OPENO varchar2(20),
    V_STIME number(9,0) default 0  NOT NULL,
    V_ETIME number(9,0) default 0  NOT NULL,
    V_YMD number(8,0) default 0  NOT NULL,
    V_TIME number(9,0) default 0  NOT NULL,
    C_TERM varchar2(20),
    C_OPENO varchar2(20),
    C_STIME number(9,0) default 0  NOT NULL,
    C_ETIME number(9,0) default 0  NOT NULL,
    C_YMD number(8,0) default 0  NOT NULL,
    C_TIME number(9,0) default 0  NOT NULL,
    O_TERM varchar2(20),
    O_OPENO varchar2(20),
    O_STIME number(9,0) default 0  NOT NULL,
    O_ETIME number(9,0) default 0  NOT NULL,
    O_YMD number(8,0) default 0  NOT NULL,
    O_TIME number(9,0) default 0  NOT NULL,
    ITEM_TOP number(10,1) default -1  NOT NULL,
    ITEM_LEFT number(10,1) default -1  NOT NULL,
    ITEM_WIDTH number(10,1) default -1  NOT NULL,
    ITEM_HEIGHT number(10,1) default -1  NOT NULL,
    UPDATE_DATE number(8,0) NOT NULL,
    UPDATE_TIME number(9,0) NOT NULL,
    UPDATE_KBN number(1,0) NOT NULL,
    FIX_TRIGGER varchar2(20),
 CONSTRAINT PK_DBCTR9999_TRITEM_HIST PRIMARY KEY (
     GYM_ID
    ,OPERATION_DATE
    ,SCAN_TERM
    ,BAT_ID
    ,DETAILS_NO
    ,ITEM_ID
    ,SEQ
));
COMMENT ON TABLE dbctr9999.TRITEM_HIST IS '���ڃg�����U�N�V��������';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.GYM_ID IS '�Ɩ��ԍ�';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.OPERATION_DATE IS '������';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.SCAN_TERM IS '�C���[�W�捞�[��';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.BAT_ID IS '�o�b�`�ԍ�';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.DETAILS_NO IS '���הԍ�';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.ITEM_ID IS '����ID';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.SEQ IS '�o�͘A��';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.ITEM_NAME IS '���ږ���';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.OCR_ENT_DATA IS '�n�b�q�l�i�G���g���K�p�j';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.OCR_VFY_DATA IS '�n�b�q�l�i�x���t�@�C�K�p�j';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.ENT_DATA IS '�G���g���[�l';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.VFY_DATA IS '�x���t�@�C�l';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.END_DATA IS '�ŏI�m��l';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.BUA_DATA IS '���o�A�b�v���[�h�l';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.CTR_DATA IS '�d�q���������ʒl';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.ICTEISEI_DATA IS '���A�����m��l';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.MRC_CHG_BEFDATA IS '�ʒm�Ǒ֑O�l';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.E_TERM IS '�G���g���[�[��';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.E_OPENO IS '�G���g���[�I�y���[�^�[�ԍ�';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.E_STIME IS '�G���g���[�J�n�����i�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.E_ETIME IS '�G���g���[�I�������i�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.E_YMD IS '�G���g���[������';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.E_TIME IS '�G���g���[���ԁi�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.V_TERM IS '�x���t�@�C�[��';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.V_OPENO IS '�x���t�@�C�I�y���[�^�[�ԍ�';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.V_STIME IS '�x���t�@�C�J�n�����i�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.V_ETIME IS '�x���t�@�C�I�������i�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.V_YMD IS '�x���t�@�C������';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.V_TIME IS '�x���t�@�C���ԁi�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.C_TERM IS '���������[��';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.C_OPENO IS '���������I�y���[�^�[�ԍ�';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.C_STIME IS '���������J�n�����i�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.C_ETIME IS '���������I�������i�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.C_YMD IS '��������������';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.C_TIME IS '�����������ԁi�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.O_TERM IS '���o�G���[�����[��';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.O_OPENO IS '���o�G���[�����I�y���[�^�[�ԍ�';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.O_STIME IS '���o�G���[�����J�n�����i�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.O_ETIME IS '���o�G���[�����I�������i�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.O_YMD IS '���o�G���[����������';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.O_TIME IS '���o�G���[�������ԁi�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.ITEM_TOP IS 'OCR�F�����ڈʒu(TOP)';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.ITEM_LEFT IS 'OCR�F�����ڈʒu(LEFT)';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.ITEM_WIDTH IS 'OCR�F�����ڈʒu(WIDTH)';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.ITEM_HEIGHT IS 'OCR�F�����ڈʒu(HEIGHT)';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.UPDATE_DATE IS '�X�V��:�X�V�V�X�e�����t';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.UPDATE_TIME IS '�X�V����:�X�V�V�X�e������';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.UPDATE_KBN IS '�X�V�敪:1�F�V�K�o�^ 2�F�A�b�v�f�[�g';
COMMENT ON COLUMN dbctr9999.TRITEM_HIST.FIX_TRIGGER IS '�C���g���K�[';
exit;