DROP TABLE dbctr9999.TRBATCH;
CREATE TABLE dbctr9999.TRBATCH (
    GYM_ID number(3,0) NOT NULL,
    OPERATION_DATE number(8,0) NOT NULL,
    SCAN_TERM varchar2(20) NOT NULL,
    BAT_ID number(6,0) NOT NULL,
    STS number(2,0) default 0  NOT NULL,
    INPUT_ROUTE number(1,0) default 0  NOT NULL,
    OC_BK_NO number(4,0) default -1  NOT NULL,
    OC_BR_NO number(4,0) default -1  NOT NULL,
    SCAN_BR_NO number(4,0) default -1  NOT NULL,
    SCAN_DATE number(8,0) default 0  NOT NULL,
    CLEARING_DATE number(8,0) default 0  NOT NULL,
    SCAN_COUNT number(6,0) default 0  NOT NULL,
    TOTAL_COUNT number(6,0) default 0  NOT NULL,
    TOTAL_AMOUNT number(18,0) default 0  NOT NULL,
    DELETE_DATE number(8,0) default 0  NOT NULL,
    DELETE_FLG number(1,0) default 0  NOT NULL,
    E_TERM varchar2(20),
    E_OPENO varchar2(20),
    E_YMD number(8,0) default 0  NOT NULL,
    E_TIME number(9,0) default 0  NOT NULL,
 CONSTRAINT PK_DBCTR9999_TRBATCH PRIMARY KEY (
     GYM_ID
    ,OPERATION_DATE
    ,SCAN_TERM
    ,BAT_ID
));
COMMENT ON TABLE dbctr9999.TRBATCH IS 'バッチデータ';
COMMENT ON COLUMN dbctr9999.TRBATCH.GYM_ID IS '業務番号';
COMMENT ON COLUMN dbctr9999.TRBATCH.OPERATION_DATE IS '処理日';
COMMENT ON COLUMN dbctr9999.TRBATCH.SCAN_TERM IS 'イメージ取込端末:IPアドレス（.→_に変換）';
COMMENT ON COLUMN dbctr9999.TRBATCH.BAT_ID IS 'バッチ番号';
COMMENT ON COLUMN dbctr9999.TRBATCH.STS IS '状態:0：入力待ち 1：入力中 5：入力保留 10：入力完了';
COMMENT ON COLUMN dbctr9999.TRBATCH.INPUT_ROUTE IS '取込ルート:1:通常バッチルート 2:付帯バッチルート 3:期日管理ルート';
COMMENT ON COLUMN dbctr9999.TRBATCH.OC_BK_NO IS '持出銀行（持出バッチ票値）';
COMMENT ON COLUMN dbctr9999.TRBATCH.OC_BR_NO IS '持出支店（持出バッチ票値）';
COMMENT ON COLUMN dbctr9999.TRBATCH.SCAN_BR_NO IS 'スキャン支店（持出バッチ票値）';
COMMENT ON COLUMN dbctr9999.TRBATCH.SCAN_DATE IS 'スキャン日（持出バッチ票値）';
COMMENT ON COLUMN dbctr9999.TRBATCH.CLEARING_DATE IS '交換希望日（持出バッチ票値）';
COMMENT ON COLUMN dbctr9999.TRBATCH.SCAN_COUNT IS 'スキャン枚数（持出バッチ票値）';
COMMENT ON COLUMN dbctr9999.TRBATCH.TOTAL_COUNT IS '合計枚数（持出バッチ票値）';
COMMENT ON COLUMN dbctr9999.TRBATCH.TOTAL_AMOUNT IS '合計金額（持出バッチ票値）';
COMMENT ON COLUMN dbctr9999.TRBATCH.DELETE_DATE IS '削除日';
COMMENT ON COLUMN dbctr9999.TRBATCH.DELETE_FLG IS '削除フラグ:0：未削除 1：削除済';
COMMENT ON COLUMN dbctr9999.TRBATCH.E_TERM IS 'バッチ票入力端末:TERM.iniの値';
COMMENT ON COLUMN dbctr9999.TRBATCH.E_OPENO IS 'バッチ票入力オペレーター番号';
COMMENT ON COLUMN dbctr9999.TRBATCH.E_YMD IS 'バッチ票入力日付';
COMMENT ON COLUMN dbctr9999.TRBATCH.E_TIME IS 'バッチ票入力時間';
exit;