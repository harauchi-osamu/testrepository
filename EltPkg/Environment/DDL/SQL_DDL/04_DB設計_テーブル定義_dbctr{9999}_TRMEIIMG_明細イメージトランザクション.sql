DROP TABLE dbctr9999.TRMEIIMG;
CREATE TABLE dbctr9999.TRMEIIMG (
    GYM_ID number(3,0) NOT NULL,
    OPERATION_DATE number(8,0) NOT NULL,
    SCAN_TERM varchar2(20) NOT NULL,
    BAT_ID number(6,0) NOT NULL,
    DETAILS_NO number(6,0) NOT NULL,
    IMG_KBN number(2,0) NOT NULL,
    IMG_FLNM varchar2(62),
    IMG_FLNM_OLD varchar2(100),
    OC_OC_BK_NO varchar2(4),
    OC_OC_BR_NO varchar2(4),
    OC_IC_BK_NO varchar2(4),
    OC_OC_DATE varchar2(8),
    OC_CLEARING_DATE varchar2(8),
    OC_AMOUNT varchar2(12),
    PAY_KBN varchar2(1),
    UNIQUE_CODE varchar2(15),
    FILE_EXTENSION varchar2(4),
    BUA_STS number(2,0) default 0  NOT NULL,
    BUB_CONFIRMDATE number(8,0) default 0  NOT NULL,
    BUA_DATE number(8,0) default 0  NOT NULL,
    BUA_TIME number(6,0) default 0  NOT NULL,
    GDA_DATE number(8,0) default 0  NOT NULL,
    GDA_TIME number(6,0) default 0  NOT NULL,
    IMG_ARCH_NAME varchar2(32),
    DELETE_DATE number(8,0) default 0  NOT NULL,
    DELETE_FLG number(1) default 0  NOT NULL,
 CONSTRAINT PK_DBCTR9999_TRMEIIMG PRIMARY KEY (
     GYM_ID
    ,OPERATION_DATE
    ,SCAN_TERM
    ,BAT_ID
    ,DETAILS_NO
    ,IMG_KBN
));
COMMENT ON TABLE dbctr9999.TRMEIIMG IS '明細イメージトランザクション';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.GYM_ID IS '業務番号';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.OPERATION_DATE IS '処理日';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.SCAN_TERM IS 'イメージ取込端末:IPアドレス（.→_に変換）';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.BAT_ID IS 'バッチ番号';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.DETAILS_NO IS '明細番号';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.IMG_KBN IS '表裏等の別:01:表 02:裏 03:補箋 04:付箋 05:入金証明 06:表（再送分） 07:裏（再送分） 08～10:その他';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.IMG_FLNM IS '証券イメージファイル名';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.IMG_FLNM_OLD IS 'スキャン連携時ファイル名';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.OC_OC_BK_NO IS '(持出ファイル名要素)持出銀行コード:前ゼロ埋め';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.OC_OC_BR_NO IS '(持出ファイル名要素)持出支店コード:前ゼロ埋め 省略値可';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.OC_IC_BK_NO IS '(持出ファイル名要素)持帰銀行コード:前ゼロ埋め 省略値可';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.OC_OC_DATE IS '(持出ファイル名要素)持出日:イメージ取込日';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.OC_CLEARING_DATE IS '(持出ファイル名要素)交換希望日:補正確定値';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.OC_AMOUNT IS '(持出ファイル名要素)金額:前ゼロ埋め 省略値可';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.PAY_KBN IS '(持出ファイル名要素)決済対象区分:0:決済対象 1:決済対象外';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.UNIQUE_CODE IS '(持出ファイル名要素)一意コード:イメージ取込にて確定';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.FILE_EXTENSION IS '(持出ファイル名要素)拡張子:".jpg"';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.BUA_STS IS '持出アップロード状態(持出):0：未作成 1：再作成対象 5：ファイル作成 10：アップロード 19：結果エラー 20：結果正常';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.BUB_CONFIRMDATE IS '持出アップロード確定日(持出)';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.BUA_DATE IS '持出日(持出)';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.BUA_TIME IS '持出時間(持出)';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.GDA_DATE IS '(持帰)持帰取込日';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.GDA_TIME IS '持帰取込時間(持帰)';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.IMG_ARCH_NAME IS 'イメージアーカイブファイル名(共通)';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.DELETE_DATE IS '削除日';
COMMENT ON COLUMN dbctr9999.TRMEIIMG.DELETE_FLG IS '削除フラグ';
exit;