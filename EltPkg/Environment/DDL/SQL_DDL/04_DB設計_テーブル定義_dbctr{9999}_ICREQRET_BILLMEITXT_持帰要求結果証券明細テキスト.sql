DROP TABLE dbctr9999.ICREQRET_BILLMEITXT;
CREATE TABLE dbctr9999.ICREQRET_BILLMEITXT (
    MEI_TXT_NAME varchar2(32) NOT NULL,
    CAP_KBN number(1,0) NOT NULL,
    IMG_NAME varchar2(62) NOT NULL,
    IMG_ARCH_NAME varchar2(32),
    FRONT_IMG_NAME varchar2(62),
    IMG_KBN number(2,0) default 0  NOT NULL,
    FILE_OC_BK_NO varchar2(4),
    CHG_OC_BK_NO varchar2(4),
    OC_BR_NO varchar2(4),
    OC_DATE number(8,0) default 0  NOT NULL,
    OC_METHOD varchar2(1),
    OC_USERID varchar2(14),
    PAY_KBN varchar2(1),
    BALANCE_FLG varchar2(1),
    OCR_IC_BK_NO varchar2(4),
    QR_IC_BK_NO varchar2(4),
    MICR_IC_BK_NO varchar2(4),
    FILE_IC_BK_NO varchar2(4),
    CHG_IC_BK_NO varchar2(4),
    TEISEI_IC_BK_NO varchar2(4),
    PAY_IC_BK_NO varchar2(4),
    PAYAFT_REV_IC_BK_NO varchar2(4),
    OCR_IC_BK_NO_CONF varchar2(3),
    OCR_AMOUNT varchar2(12),
    MICR_AMOUNT varchar2(12),
    QR_AMOUNT varchar2(12),
    FILE_AMOUNT varchar2(12),
    TEISEI_AMOUNT varchar2(12),
    PAY_AMOUNT varchar2(12),
    PAYAFT_REV_AMOUNT varchar2(12),
    OCR_AMOUNT_CONF varchar2(3),
    OC_CLEARING_DATE varchar2(8),
    TEISEI_CLEARING_DATE varchar2(8),
    CLEARING_DATE varchar2(8),
    QR_IC_BR_NO varchar2(4),
    KAMOKU varchar2(1),
    ACCOUNT varchar2(10),
    BK_CTL_NO varchar2(20),
    FREEFIELD varchar2(30),
    BILL_CODE varchar2(3),
    BILL_CODE_CONF varchar2(3),
    QR varchar2(160),
    MICR varchar2(65),
    MICR_CONF varchar2(3),
    BILL_NO varchar2(10),
    BILL_NO_CONF varchar2(3),
    FUBI_KBN_01 varchar2(1),
    ZERO_FUBINO_01 number(2,0) default 0  NOT NULL,
    FUBI_KBN_02 varchar2(1),
    ZRO_FUBINO_02 number(2,0) default 0  NOT NULL,
    FUBI_KBN_03 varchar2(1),
    ZRO_FUBINO_03 number(2,0) default 0  NOT NULL,
    FUBI_KBN_04 varchar2(1),
    ZRO_FUBINO_04 number(2,0) default 0  NOT NULL,
    FUBI_KBN_05 varchar2(1),
    ZRO_FUBINO_05 number(2,0) default 0  NOT NULL,
    IC_FLG varchar2(1),
    KAKUTEI_FLG number(1,0) default 0  NOT NULL,
    KAKUTEI_DATE number(8,0) default 0  NOT NULL,
    KAKUTEI_TIME number(9,0) default 0  NOT NULL,
    KAKUTEI_OPE varchar2(20),
 CONSTRAINT PK_DBCTR9999_ICREQRET_BILLMEITXT PRIMARY KEY (
     MEI_TXT_NAME
    ,CAP_KBN
    ,IMG_NAME
));
COMMENT ON TABLE dbctr9999.ICREQRET_BILLMEITXT IS '持帰要求結果証券明細テキスト';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.MEI_TXT_NAME IS '証券明細テキストファイル名';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.CAP_KBN IS '取込区分:0：電子交換所 1：行内交換連携';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.IMG_NAME IS '証券イメージファイル名';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.IMG_ARCH_NAME IS 'イメージアーカイブ名';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.FRONT_IMG_NAME IS '表証券イメージファイル名';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.IMG_KBN IS '表裏等の別';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.FILE_OC_BK_NO IS 'ファイル名持出銀行コード';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.CHG_OC_BK_NO IS '読替持出銀行コード';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.OC_BR_NO IS '持出支店コード';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.OC_DATE IS '持出日';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.OC_METHOD IS '持出時接続方式';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.OC_USERID IS 'ユーザID(持出者)';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.PAY_KBN IS '決済対象区分';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.BALANCE_FLG IS '交換尻確定フラグ';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.OCR_IC_BK_NO IS 'OCR持帰銀行コード';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.QR_IC_BK_NO IS 'QRコード持帰銀行コード';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.MICR_IC_BK_NO IS 'MICR持帰銀行コード';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.FILE_IC_BK_NO IS 'ファイル名持帰銀行コード';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.CHG_IC_BK_NO IS '読替持帰銀行コード';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.TEISEI_IC_BK_NO IS '証券データ訂正持帰銀行コード';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.PAY_IC_BK_NO IS '決済持帰銀行コード';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.PAYAFT_REV_IC_BK_NO IS '決済後訂正持帰銀行コード';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.OCR_IC_BK_NO_CONF IS 'OCR持帰銀行コード確信度';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.OCR_AMOUNT IS 'OCR金額';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.MICR_AMOUNT IS 'MICR金額';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.QR_AMOUNT IS 'QRコード金額';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.FILE_AMOUNT IS 'ファイル名金額';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.TEISEI_AMOUNT IS '証券データ訂正金額';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.PAY_AMOUNT IS '決済金額';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.PAYAFT_REV_AMOUNT IS '決済後訂正金額';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.OCR_AMOUNT_CONF IS 'OCR金額確信度';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.OC_CLEARING_DATE IS '持出時交換希望日';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.TEISEI_CLEARING_DATE IS '証券データ訂正交換希望日';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.CLEARING_DATE IS '交換日';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.QR_IC_BR_NO IS 'QRコード持帰支店コード';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.KAMOKU IS '科目コード';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.ACCOUNT IS '口座番号';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.BK_CTL_NO IS '銀行管理番号';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.FREEFIELD IS '自由記述欄';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.BILL_CODE IS '交換証券種類コード';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.BILL_CODE_CONF IS '交換証券種類コード確信度';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.QR IS 'QRコード情報';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.MICR IS 'MICR情報';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.MICR_CONF IS 'MICR情報確信度';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.BILL_NO IS '手形・小切手番号';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.BILL_NO_CONF IS '手形・小切手番号確信度';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.FUBI_KBN_01 IS '不渡返還区分１';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.ZERO_FUBINO_01 IS '0号不渡事由コード１';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.FUBI_KBN_02 IS '不渡返還区分２';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.ZRO_FUBINO_02 IS '0号不渡事由コード２';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.FUBI_KBN_03 IS '不渡返還区分３';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.ZRO_FUBINO_03 IS '0号不渡事由コード３';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.FUBI_KBN_04 IS '不渡返還区分４';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.ZRO_FUBINO_04 IS '0号不渡事由コード４';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.FUBI_KBN_05 IS '不渡返還区分５';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.ZRO_FUBINO_05 IS '0号不渡事由コード５';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.IC_FLG IS '持帰状況フラグ';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.KAKUTEI_FLG IS '確定フラグ:0:未確定 1:確定';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.KAKUTEI_DATE IS '確定日';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.KAKUTEI_TIME IS '確定時間';
COMMENT ON COLUMN dbctr9999.ICREQRET_BILLMEITXT.KAKUTEI_OPE IS '確定者ID';
exit;