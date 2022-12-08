DROP TABLE dbctr9999.TRITEM;
CREATE TABLE dbctr9999.TRITEM (
    GYM_ID number(3,0) NOT NULL,
    OPERATION_DATE number(8,0) NOT NULL,
    SCAN_TERM varchar2(20) NOT NULL,
    BAT_ID number(6,0) NOT NULL,
    DETAILS_NO number(6,0) NOT NULL,
    ITEM_ID number(3,0) NOT NULL,
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
    FIX_TRIGGER varchar2(20),
 CONSTRAINT PK_DBCTR9999_TRITEM PRIMARY KEY (
     GYM_ID
    ,OPERATION_DATE
    ,SCAN_TERM
    ,BAT_ID
    ,DETAILS_NO
    ,ITEM_ID
));
COMMENT ON TABLE dbctr9999.TRITEM IS '���ڃg�����U�N�V����';
COMMENT ON COLUMN dbctr9999.TRITEM.GYM_ID IS '�Ɩ��ԍ�';
COMMENT ON COLUMN dbctr9999.TRITEM.OPERATION_DATE IS '������';
COMMENT ON COLUMN dbctr9999.TRITEM.SCAN_TERM IS '�C���[�W�捞�[��:IP�A�h���X�i.��_�ɕϊ��j';
COMMENT ON COLUMN dbctr9999.TRITEM.BAT_ID IS '�o�b�`�ԍ�';
COMMENT ON COLUMN dbctr9999.TRITEM.DETAILS_NO IS '���הԍ�';
COMMENT ON COLUMN dbctr9999.TRITEM.ITEM_ID IS '����ID';
COMMENT ON COLUMN dbctr9999.TRITEM.ITEM_NAME IS '���ږ���';
COMMENT ON COLUMN dbctr9999.TRITEM.OCR_ENT_DATA IS '�n�b�q�l�i�G���g���K�p�j';
COMMENT ON COLUMN dbctr9999.TRITEM.OCR_VFY_DATA IS '�n�b�q�l�i�x���t�@�C�K�p�j';
COMMENT ON COLUMN dbctr9999.TRITEM.ENT_DATA IS '�G���g���[�l';
COMMENT ON COLUMN dbctr9999.TRITEM.VFY_DATA IS '�x���t�@�C�l';
COMMENT ON COLUMN dbctr9999.TRITEM.END_DATA IS '�ŏI�m��l';
COMMENT ON COLUMN dbctr9999.TRITEM.BUA_DATA IS '���o�A�b�v���[�h�l';
COMMENT ON COLUMN dbctr9999.TRITEM.CTR_DATA IS '�d�q���������ʒl';
COMMENT ON COLUMN dbctr9999.TRITEM.ICTEISEI_DATA IS '���A�����m��l:�����f�[�^���ʎ捞�ōX�V �����L���̔��f�Ɏg�p';
COMMENT ON COLUMN dbctr9999.TRITEM.MRC_CHG_BEFDATA IS '�ʒm�Ǒ֑O�l';
COMMENT ON COLUMN dbctr9999.TRITEM.E_TERM IS '�G���g���[�[��:TERM.ini�̒l';
COMMENT ON COLUMN dbctr9999.TRITEM.E_OPENO IS '�G���g���[�I�y���[�^�[�ԍ�';
COMMENT ON COLUMN dbctr9999.TRITEM.E_STIME IS '�G���g���[�J�n�����i�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM.E_ETIME IS '�G���g���[�I�������i�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM.E_YMD IS '�G���g���[������';
COMMENT ON COLUMN dbctr9999.TRITEM.E_TIME IS '�G���g���[���ԁi�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM.V_TERM IS '�x���t�@�C�[��:TERM.ini�̒l';
COMMENT ON COLUMN dbctr9999.TRITEM.V_OPENO IS '�x���t�@�C�I�y���[�^�[�ԍ�';
COMMENT ON COLUMN dbctr9999.TRITEM.V_STIME IS '�x���t�@�C�J�n�����i�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM.V_ETIME IS '�x���t�@�C�I�������i�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM.V_YMD IS '�x���t�@�C������';
COMMENT ON COLUMN dbctr9999.TRITEM.V_TIME IS '�x���t�@�C���ԁi�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM.C_TERM IS '���������[��:TERM.ini�̒l';
COMMENT ON COLUMN dbctr9999.TRITEM.C_OPENO IS '���������I�y���[�^�[�ԍ�';
COMMENT ON COLUMN dbctr9999.TRITEM.C_STIME IS '���������J�n�����i�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM.C_ETIME IS '���������I�������i�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM.C_YMD IS '��������������';
COMMENT ON COLUMN dbctr9999.TRITEM.C_TIME IS '�����������ԁi�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM.O_TERM IS '���o�G���[�����[��:TERM.ini�̒l';
COMMENT ON COLUMN dbctr9999.TRITEM.O_OPENO IS '���o�G���[�����I�y���[�^�[�ԍ�';
COMMENT ON COLUMN dbctr9999.TRITEM.O_STIME IS '���o�G���[�����J�n�����i�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM.O_ETIME IS '���o�G���[�����I�������i�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM.O_YMD IS '���o�G���[����������';
COMMENT ON COLUMN dbctr9999.TRITEM.O_TIME IS '���o�G���[�������ԁi�~���b�j';
COMMENT ON COLUMN dbctr9999.TRITEM.ITEM_TOP IS 'OCR�F�����ڈʒu(TOP)';
COMMENT ON COLUMN dbctr9999.TRITEM.ITEM_LEFT IS 'OCR�F�����ڈʒu(LEFT)';
COMMENT ON COLUMN dbctr9999.TRITEM.ITEM_WIDTH IS 'OCR�F�����ڈʒu(WIDTH)';
COMMENT ON COLUMN dbctr9999.TRITEM.ITEM_HEIGHT IS 'OCR�F�����ڈʒu(HEIGHT)';
COMMENT ON COLUMN dbctr9999.TRITEM.FIX_TRIGGER IS '�C���g���K�[:�␳�G���g���[ �␳�G���g���[���� ���A���� �ق�';
exit;