DROP TABLE dbctr9999.ICREQRET_CTL;
CREATE TABLE dbctr9999.ICREQRET_CTL (
    RET_REQ_TXT_NAME varchar2(32) NOT NULL,
    MEI_TXT_NAME varchar2(32) NOT NULL,
    CAP_KBN number(1,0) NOT NULL,
    CAP_STS number(2,0) default 0  NOT NULL,
    IMG_ARCH_NAME varchar2(32),
    IMG_ARCH_CAP_STS number(2,0) default -1  NOT NULL,
CONSTRAINT PK_DBCTR9999_ICREQRET_CTL PRIMARY KEY (
     RET_REQ_TXT_NAME
    ,MEI_TXT_NAME
    ,CAP_KBN
));
COMMENT ON TABLE dbctr9999.ICREQRET_CTL IS '���A�v�����ʊǗ�';
COMMENT ON COLUMN dbctr9999.ICREQRET_CTL.RET_REQ_TXT_NAME IS '�v�����ʃe�L�X�g�t�@�C����';
COMMENT ON COLUMN dbctr9999.ICREQRET_CTL.MEI_TXT_NAME IS '�،����׃e�L�X�g�t�@�C����';
COMMENT ON COLUMN dbctr9999.ICREQRET_CTL.CAP_KBN IS '�捞�敪:0�F�d�q������ 1�F�s�������A�g';
COMMENT ON COLUMN dbctr9999.ICREQRET_CTL.CAP_STS IS '�،����׃e�L�X�g�t�@�C���捞���:0�F���捞 5�F�_�E�����[�h�m��� 10�F�_�E�����[�h�m���';
COMMENT ON COLUMN dbctr9999.ICREQRET_CTL.IMG_ARCH_NAME IS '�C���[�W�A�[�J�C�u��:���C���[�W�v���ۂ̏ꍇ��null';
COMMENT ON COLUMN dbctr9999.ICREQRET_CTL.IMG_ARCH_CAP_STS IS '�C���[�W�A�[�J�C�u�捞���:-1�F�ΏۊO�i�v���ΏۊO�̏ꍇ�j 0�F���捞 1�FOCR�� 5�F�_�E�����[�h�m��� 10�F�_�E�����[�h�m���';
exit;