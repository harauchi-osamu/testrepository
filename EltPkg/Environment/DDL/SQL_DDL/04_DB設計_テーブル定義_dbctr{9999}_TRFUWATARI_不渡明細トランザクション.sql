DROP TABLE dbctr9999.TRFUWATARI;
CREATE TABLE dbctr9999.TRFUWATARI (
    GYM_ID number(3,0) NOT NULL,
    OPERATION_DATE number(8,0) NOT NULL,
    SCAN_TERM varchar2(20) NOT NULL,
    BAT_ID number(6,0) NOT NULL,
    DETAILS_NO number(6,0) NOT NULL,
    FUBI_KBN_01 number(1,0) default -1  NOT NULL,
    ZERO_FUBINO_01 number(2,0) default -1  NOT NULL,
    FUBI_KBN_02 number(1,0) default -1  NOT NULL,
    ZERO_FUBINO_02 number(2,0) default -1  NOT NULL,
    FUBI_KBN_03 number(1,0) default -1  NOT NULL,
    ZERO_FUBINO_03 number(2,0) default -1  NOT NULL,
    FUBI_KBN_04 number(1,0) default -1  NOT NULL,
    ZERO_FUBINO_04 number(2,0) default -1  NOT NULL,
    FUBI_KBN_05 number(1,0) default -1  NOT NULL,
    ZERO_FUBINO_05 number(2,0) default -1  NOT NULL,
    DELETE_DATE number(8,0) default 0  NOT NULL,
    DELETE_FLG number(1) default 0  NOT NULL,
    E_TERM varchar2(20),
    E_OPENO varchar2(20),
    E_YMD number(8,0) default 0  NOT NULL,
    E_TIME number(11,0) default 0  NOT NULL,
 CONSTRAINT PK_DBCTR9999_TRFUWATARI PRIMARY KEY (
     GYM_ID
    ,OPERATION_DATE
    ,SCAN_TERM
    ,BAT_ID
    ,DETAILS_NO
));
COMMENT ON TABLE dbctr9999.TRFUWATARI IS '�s�n���׃g�����U�N�V����';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.GYM_ID IS '�Ɩ��ԍ�';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.OPERATION_DATE IS '������';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.SCAN_TERM IS '�C���[�W�捞�[��';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.BAT_ID IS '�o�b�`�ԍ�';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.DETAILS_NO IS '���הԍ�';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.FUBI_KBN_01 IS '�s�n�Ԋҋ敪�P:�s�n�Ԋҋ敪���Z�b�g����B �E"0":0���s�n �E"1":��1���s�n �E"2":��2���s�n �E�o�^�敪��"9"(���)�̏ꍇ�́A�ȗ��l���Z�b�g����B';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.ZERO_FUBINO_01 IS '0���s�n���R�R�[�h�P:�s�n���R�R�[�h���Z�b�g����B �ڍׂ́u6.3. �s�n���R�R�[�h�v���Q�ƁB �E�s�n�Ԋҋ敪�P��"0"(0���s�n)�̏ꍇ�́A�s�n���R�R�[�h���Z�b�g����B �E�s�n�Ԋҋ敪�P��"0"(0���s�n)�ȊO�̏ꍇ�́A�ȗ��l���Z�b�g����B �E�o�^�敪��"9"(���)�̏ꍇ�́A�ȗ��l���Z�b�g����B';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.FUBI_KBN_02 IS '�s�n�Ԋҋ敪�Q:�s�n�Ԋҋ敪���Z�b�g����B �E"0":0���s�n �E"1":��1���s�n �E"2":��2���s�n �E�s�n�Ԋҋ敪1���ȗ��l�ȊO�̏ꍇ�̂݃Z�b�g�\ �E�o�^�敪��"9"(���)�̏ꍇ�́A�ȗ��l���Z�b�g����B �E�o�^��񂪂Ȃ��ꍇ�́A�ȗ��l���Z�b�g����B';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.ZERO_FUBINO_02 IS '0���s�n���R�R�[�h�Q:�s�n���R�R�[�h���Z�b�g����B �ڍׂ́u6.3. �s�n���R�R�[�h�v���Q�ƁB �E�s�n�Ԋҋ敪�Q��"0"(0���s�n)�̏ꍇ�́A�s�n���R�R�[�h���Z�b�g����B �E�s�n�Ԋҋ敪�Q��"0"(0���s�n)�ȊO�̏ꍇ�́A�ȗ��l���Z�b�g����B �E�o�^�敪��"9"(���)�̏ꍇ�́A�ȗ��l���Z�b�g����B';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.FUBI_KBN_03 IS '�s�n�Ԋҋ敪�R:�s�n�Ԋҋ敪���Z�b�g����B �E"0":0���s�n �E"1":��1���s�n �E"2":��2���s�n �E�s�n�Ԋҋ敪1�`2���ȗ��l�ȊO�̏ꍇ�̂݃Z�b�g�\ �E�o�^�敪��"9"(���)�̏ꍇ�́A�ȗ��l���Z�b�g����B �E�o�^��񂪂Ȃ��ꍇ�́A�ȗ��l���Z�b�g����B';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.ZERO_FUBINO_03 IS '0���s�n���R�R�[�h�R:�s�n���R�R�[�h���Z�b�g����B �ڍׂ́u6.3. �s�n���R�R�[�h�v���Q�ƁB �E�s�n�Ԋҋ敪�R��"0"(0���s�n)�̏ꍇ�́A�s�n���R�R�[�h���Z�b�g����B �E�s�n�Ԋҋ敪�R��"0"(0���s�n)�ȊO�̏ꍇ�́A�ȗ��l���Z�b�g����B �E�o�^�敪��"9"(���)�̏ꍇ�́A�ȗ��l���Z�b�g����B';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.FUBI_KBN_04 IS '�s�n�Ԋҋ敪�S:�s�n�Ԋҋ敪���Z�b�g����B �E"0":0���s�n �E"1":��1���s�n �E"2":��2���s�n �E�s�n�Ԋҋ敪1�`3���ȗ��l�ȊO�̏ꍇ�̂݃Z�b�g�\ �E�o�^�敪��"9"(���)�̏ꍇ�́A�ȗ��l���Z�b�g����B �E�o�^��񂪂Ȃ��ꍇ�́A�ȗ��l���Z�b�g����B';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.ZERO_FUBINO_04 IS '0���s�n���R�R�[�h�S:�s�n���R�R�[�h���Z�b�g����B �ڍׂ́u6.3. �s�n���R�R�[�h�v���Q�ƁB �E�s�n�Ԋҋ敪�S��"0"(0���s�n)�̏ꍇ�́A�s�n���R�R�[�h���Z�b�g����B �E�s�n�Ԋҋ敪�S��"0"(0���s�n)�ȊO�̏ꍇ�́A�ȗ��l���Z�b�g����B �E�o�^�敪��"9"(���)�̏ꍇ�́A�ȗ��l���Z�b�g����B';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.FUBI_KBN_05 IS '�s�n�Ԋҋ敪�T:�s�n�Ԋҋ敪���Z�b�g����B �E"0":0���s�n �E"1":��1���s�n �E"2":��2���s�n �E�s�n�Ԋҋ敪1�`4���ȗ��l�ȊO�̏ꍇ�̂݃Z�b�g�\ �E�o�^�敪��"9"(���)�̏ꍇ�́A�ȗ��l���Z�b�g����B �E�o�^��񂪂Ȃ��ꍇ�́A�ȗ��l���Z�b�g����B';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.ZERO_FUBINO_05 IS '0���s�n���R�R�[�h�T:�s�n���R�R�[�h���Z�b�g����B �ڍׂ́u6.3. �s�n���R�R�[�h�v���Q�ƁB �E�s�n�Ԋҋ敪�T��"0"(0���s�n)�̏ꍇ�́A�s�n���R�R�[�h���Z�b�g����B �E�s�n�Ԋҋ敪�T��"0"(0���s�n)�ȊO�̏ꍇ�́A�ȗ��l���Z�b�g����B �E�o�^�敪��"9"(���)�̏ꍇ�́A�ȗ��l���Z�b�g����B';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.DELETE_DATE IS '�����';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.DELETE_FLG IS '����t���O:0�F����� 1�F�����';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.E_TERM IS '���͒[��';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.E_OPENO IS '���̓I�y���[�^�[�ԍ�';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.E_YMD IS '���͓��t';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.E_TIME IS '���͎���';
exit;