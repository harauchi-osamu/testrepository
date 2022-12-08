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
COMMENT ON TABLE dbctr9999.TRITEM IS '項目トランザクション';
COMMENT ON COLUMN dbctr9999.TRITEM.GYM_ID IS '業務番号';
COMMENT ON COLUMN dbctr9999.TRITEM.OPERATION_DATE IS '処理日';
COMMENT ON COLUMN dbctr9999.TRITEM.SCAN_TERM IS 'イメージ取込端末:IPアドレス（.→_に変換）';
COMMENT ON COLUMN dbctr9999.TRITEM.BAT_ID IS 'バッチ番号';
COMMENT ON COLUMN dbctr9999.TRITEM.DETAILS_NO IS '明細番号';
COMMENT ON COLUMN dbctr9999.TRITEM.ITEM_ID IS '項目ID';
COMMENT ON COLUMN dbctr9999.TRITEM.ITEM_NAME IS '項目名称';
COMMENT ON COLUMN dbctr9999.TRITEM.OCR_ENT_DATA IS 'ＯＣＲ値（エントリ適用）';
COMMENT ON COLUMN dbctr9999.TRITEM.OCR_VFY_DATA IS 'ＯＣＲ値（ベリファイ適用）';
COMMENT ON COLUMN dbctr9999.TRITEM.ENT_DATA IS 'エントリー値';
COMMENT ON COLUMN dbctr9999.TRITEM.VFY_DATA IS 'ベリファイ値';
COMMENT ON COLUMN dbctr9999.TRITEM.END_DATA IS '最終確定値';
COMMENT ON COLUMN dbctr9999.TRITEM.BUA_DATA IS '持出アップロード値';
COMMENT ON COLUMN dbctr9999.TRITEM.CTR_DATA IS '電子交換所結果値';
COMMENT ON COLUMN dbctr9999.TRITEM.ICTEISEI_DATA IS '持帰訂正確定値:訂正データ結果取込で更新 訂正有無の判断に使用';
COMMENT ON COLUMN dbctr9999.TRITEM.MRC_CHG_BEFDATA IS '通知読替前値';
COMMENT ON COLUMN dbctr9999.TRITEM.E_TERM IS 'エントリー端末:TERM.iniの値';
COMMENT ON COLUMN dbctr9999.TRITEM.E_OPENO IS 'エントリーオペレーター番号';
COMMENT ON COLUMN dbctr9999.TRITEM.E_STIME IS 'エントリー開始時刻（ミリ秒）';
COMMENT ON COLUMN dbctr9999.TRITEM.E_ETIME IS 'エントリー終了時刻（ミリ秒）';
COMMENT ON COLUMN dbctr9999.TRITEM.E_YMD IS 'エントリー処理日';
COMMENT ON COLUMN dbctr9999.TRITEM.E_TIME IS 'エントリー時間（ミリ秒）';
COMMENT ON COLUMN dbctr9999.TRITEM.V_TERM IS 'ベリファイ端末:TERM.iniの値';
COMMENT ON COLUMN dbctr9999.TRITEM.V_OPENO IS 'ベリファイオペレーター番号';
COMMENT ON COLUMN dbctr9999.TRITEM.V_STIME IS 'ベリファイ開始時刻（ミリ秒）';
COMMENT ON COLUMN dbctr9999.TRITEM.V_ETIME IS 'ベリファイ終了時刻（ミリ秒）';
COMMENT ON COLUMN dbctr9999.TRITEM.V_YMD IS 'ベリファイ処理日';
COMMENT ON COLUMN dbctr9999.TRITEM.V_TIME IS 'ベリファイ時間（ミリ秒）';
COMMENT ON COLUMN dbctr9999.TRITEM.C_TERM IS '完了訂正端末:TERM.iniの値';
COMMENT ON COLUMN dbctr9999.TRITEM.C_OPENO IS '完了訂正オペレーター番号';
COMMENT ON COLUMN dbctr9999.TRITEM.C_STIME IS '完了訂正開始時刻（ミリ秒）';
COMMENT ON COLUMN dbctr9999.TRITEM.C_ETIME IS '完了訂正終了時刻（ミリ秒）';
COMMENT ON COLUMN dbctr9999.TRITEM.C_YMD IS '完了訂正処理日';
COMMENT ON COLUMN dbctr9999.TRITEM.C_TIME IS '完了訂正時間（ミリ秒）';
COMMENT ON COLUMN dbctr9999.TRITEM.O_TERM IS '持出エラー訂正端末:TERM.iniの値';
COMMENT ON COLUMN dbctr9999.TRITEM.O_OPENO IS '持出エラー訂正オペレーター番号';
COMMENT ON COLUMN dbctr9999.TRITEM.O_STIME IS '持出エラー訂正開始時刻（ミリ秒）';
COMMENT ON COLUMN dbctr9999.TRITEM.O_ETIME IS '持出エラー訂正終了時刻（ミリ秒）';
COMMENT ON COLUMN dbctr9999.TRITEM.O_YMD IS '持出エラー訂正処理日';
COMMENT ON COLUMN dbctr9999.TRITEM.O_TIME IS '持出エラー訂正時間（ミリ秒）';
COMMENT ON COLUMN dbctr9999.TRITEM.ITEM_TOP IS 'OCR認識項目位置(TOP)';
COMMENT ON COLUMN dbctr9999.TRITEM.ITEM_LEFT IS 'OCR認識項目位置(LEFT)';
COMMENT ON COLUMN dbctr9999.TRITEM.ITEM_WIDTH IS 'OCR認識項目位置(WIDTH)';
COMMENT ON COLUMN dbctr9999.TRITEM.ITEM_HEIGHT IS 'OCR認識項目位置(HEIGHT)';
COMMENT ON COLUMN dbctr9999.TRITEM.FIX_TRIGGER IS '修正トリガー:補正エントリー 補正エントリー訂正 持帰訂正 ほか';
exit;