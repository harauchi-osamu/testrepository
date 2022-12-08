DROP TABLE dbctr9999.CTRUSERINFO;
CREATE TABLE dbctr9999.CTRUSERINFO (
    USERID varchar2(14) NOT NULL,
    S_DATE number(8,0) default 0  NOT NULL,
    PASSWORD varchar2(64),
    E_DATE number(8,0) default 99991231  NOT NULL,
CONSTRAINT PK_DBCTR9999_CTRUSERINFO PRIMARY KEY (
     USERID
    ,S_DATE
));
COMMENT ON TABLE dbctr9999.CTRUSERINFO IS '�d�q�������[�U�[���}�X�^';
COMMENT ON COLUMN dbctr9999.CTRUSERINFO.USERID IS '���[�UID:�d�q�������o�^���e';
COMMENT ON COLUMN dbctr9999.CTRUSERINFO.S_DATE IS '�L�������J�n��:�d�q�������o�^���e';
COMMENT ON COLUMN dbctr9999.CTRUSERINFO.PASSWORD IS '�p�X���[�h:�d�q�������o�^���e';
COMMENT ON COLUMN dbctr9999.CTRUSERINFO.E_DATE IS '�L�������I����:�d�q�������o�^���e';
exit;