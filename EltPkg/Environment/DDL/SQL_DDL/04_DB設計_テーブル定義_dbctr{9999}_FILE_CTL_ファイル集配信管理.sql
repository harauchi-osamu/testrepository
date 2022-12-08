DROP TABLE dbctr9999.FILE_CTL;
CREATE TABLE dbctr9999.FILE_CTL (
    FILE_ID varchar2(5) NOT NULL,
    FILE_DIVID varchar2(3) NOT NULL,
    SEND_FILE_NAME varchar2(32) NOT NULL,
    SEND_FILE_LENGTH number(15,0) default 0  NOT NULL,
    SEND_STS number(2,0) default 0  NOT NULL,
    MAKE_OPENO varchar2(20),
    MAKE_DATE number(8,0) default 0  NOT NULL,
    MAKE_TIME number(9,0) default 0  NOT NULL,
    SEND_DATE number(8,0) default 0  NOT NULL,
    SEND_TIME number(9,0) default 0  NOT NULL,
    CAP_FILE_NAME varchar2(32) NOT NULL,
    CAP_FILE_LENGTH number(10,0) default 0  NOT NULL,
    CAP_STS number(2,0) default 0  NOT NULL,
    CAP_DATE number(8,0) default 0  NOT NULL,
    CAP_TIME number(9,0) default 0  NOT NULL,
CONSTRAINT PK_DBCTR9999_FILE_CTL PRIMARY KEY (
     FILE_ID
    ,FILE_DIVID
    ,SEND_FILE_NAME
    ,CAP_FILE_NAME
));
COMMENT ON TABLE dbctr9999.FILE_CTL IS '�t�@�C���W�z�M�Ǘ�';
COMMENT ON COLUMN dbctr9999.FILE_CTL.FILE_ID IS '�t�@�C��ID';
COMMENT ON COLUMN dbctr9999.FILE_CTL.FILE_DIVID IS '�t�@�C�����ʋ敪';
COMMENT ON COLUMN dbctr9999.FILE_CTL.SEND_FILE_NAME IS '�z�M�t�@�C����';
COMMENT ON COLUMN dbctr9999.FILE_CTL.SEND_FILE_LENGTH IS '�z�M�t�@�C���T�C�Y';
COMMENT ON COLUMN dbctr9999.FILE_CTL.SEND_STS IS '�z�M�t�@�C�����:0:�t�@�C���쐬 1:�z�M�� 9:�z�M�G���[ 10:�z�M��';
COMMENT ON COLUMN dbctr9999.FILE_CTL.MAKE_OPENO IS '�z�M�t�@�C���쐬�҂h�c:�iyyyyMMdd)';
COMMENT ON COLUMN dbctr9999.FILE_CTL.MAKE_DATE IS '�z�M�t�@�C���쐬��:�iyyyyMMdd)';
COMMENT ON COLUMN dbctr9999.FILE_CTL.MAKE_TIME IS '�z�M�t�@�C���쐬����:�ihhmmssfff)';
COMMENT ON COLUMN dbctr9999.FILE_CTL.SEND_DATE IS '�z�M������:�iyyyyMMdd)';
COMMENT ON COLUMN dbctr9999.FILE_CTL.SEND_TIME IS '�z�M��������:�ihhmmssfff)';
COMMENT ON COLUMN dbctr9999.FILE_CTL.CAP_FILE_NAME IS '�捞�t�@�C����';
COMMENT ON COLUMN dbctr9999.FILE_CTL.CAP_FILE_LENGTH IS '�捞�t�@�C���T�C�Y';
COMMENT ON COLUMN dbctr9999.FILE_CTL.CAP_STS IS '�捞���:1:�捞�� 5:�捞�ۗ� 9:�捞�G���[ 10:�捞����';
COMMENT ON COLUMN dbctr9999.FILE_CTL.CAP_DATE IS '�捞������:�iyyyyMMdd)';
COMMENT ON COLUMN dbctr9999.FILE_CTL.CAP_TIME IS '�捞��������:�ihhmmssfff)';
exit;