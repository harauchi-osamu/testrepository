DROP TABLE dbctr9999.SUB_RTN;
CREATE TABLE dbctr9999.SUB_RTN (
    SUB_KBN number(1,0) NOT NULL,
    SUB_SUB varchar2(10) NOT NULL,
    SUB_VALUE varchar2(30),
 CONSTRAINT PK_DBCTR9999_SUB_RTN PRIMARY KEY (
     SUB_KBN
    ,SUB_SUB
));
COMMENT ON TABLE dbctr9999.SUB_RTN IS '�T�u���[�`��';
COMMENT ON COLUMN dbctr9999.SUB_RTN.SUB_KBN IS '�T�u���[�`�����:1�F���ړ��̓`�F�b�N 2�F���ړ��͒l�Ǒ�';
COMMENT ON COLUMN dbctr9999.SUB_RTN.SUB_SUB IS '�T�u���[�`����:�T�u���[�`����';
COMMENT ON COLUMN dbctr9999.SUB_RTN.SUB_VALUE IS '�`�F�b�N�o�����[�l:�`�F�b�N�o�����[�l';
exit;