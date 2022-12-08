DROP TABLE dbctr9999.TRMEI_HIST;
CREATE TABLE dbctr9999.TRMEI_HIST (
    GYM_ID number(3,0) NOT NULL,
    OPERATION_DATE number(8,0) NOT NULL,
    SCAN_TERM varchar2(20) NOT NULL,
    BAT_ID number(6,0) NOT NULL,
    DETAILS_NO number(6,0) NOT NULL,
    SEQ number(3,0) NOT NULL,
    DSP_ID number(3,0) default 999  NOT NULL,
    IC_OC_BK_NO number(4,0) default -1  NOT NULL,
    IC_OLD_OC_BK_NO number(4,0) default -1  NOT NULL,
    BUA_DATE number(8,0) default 0  NOT NULL,
    BUB_DATE number(8,0) default 0  NOT NULL,
    BCA_DATE number(8,0) default 0  NOT NULL,
    GMA_DATE number(8,0) default 0  NOT NULL,
    GMB_DATE number(8,0) default 0  NOT NULL,
    GRA_DATE number(8,0) default 0  NOT NULL,
    GXA_DATE number(8,0) default 0  NOT NULL,
    GXB_DATE number(8,0) default 0  NOT NULL,
    MRA_DATE number(8,0) default 0  NOT NULL,
    MRB_DATE number(8,0) default 0  NOT NULL,
    MRC_DATE number(8,0) default 0  NOT NULL,
    MRD_DATE number(8,0) default 0  NOT NULL,
    YCA_MARK number(2,0) default 0  NOT NULL,
    EDIT_FLG number(1,0) default 0  NOT NULL,
    BCA_STS number(2,0) default 0  NOT NULL,
    GMA_STS number(2,0) default 0  NOT NULL,
    GRA_STS number(2,0) default 0  NOT NULL,
    GRA_CONFIRMDATE number(8,0) default 0  NOT NULL,
    DELETE_DATE number(8,0) default 0  NOT NULL,
    DELETE_FLG number(1,0) default 0  NOT NULL,
    UPDATE_DATE number(8,0) default 0  NOT NULL,
    UPDATE_TIME number(9,0) default 0  NOT NULL,
    UPDATE_KBN number(1,0) default 1  NOT NULL,
 CONSTRAINT PK_DBCTR9999_TRMEI_HIST PRIMARY KEY (
     GYM_ID
    ,OPERATION_DATE
    ,SCAN_TERM
    ,BAT_ID
    ,DETAILS_NO
    ,SEQ
));
COMMENT ON TABLE dbctr9999.TRMEI_HIST IS '明細トランザクション履歴';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.GYM_ID IS '業務番号';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.OPERATION_DATE IS '処理日';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.SCAN_TERM IS 'イメージ取込端末';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.BAT_ID IS 'バッチ番号';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.DETAILS_NO IS '明細番号';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.SEQ IS '出力連番';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.DSP_ID IS '画面番号';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.IC_OC_BK_NO IS '持出銀行(持帰)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.IC_OLD_OC_BK_NO IS '旧持出銀行(持帰)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.BUA_DATE IS '二重持出通知日(持出)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.BUB_DATE IS '二重持出通知日(持帰)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.BCA_DATE IS '持出取消通知日(持帰)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.GMA_DATE IS '証券データ訂正通知日(持出)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.GMB_DATE IS '証券データ訂正通知日(持帰)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.GRA_DATE IS '不渡返還通知日(持出)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.GXA_DATE IS '決済後訂正通知日(持出)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.GXB_DATE IS '決済後訂正通知日(持帰)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.MRA_DATE IS '金融機関読替情報変更通知日(持出銀行コード変更・継承銀行向け)(持出)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.MRB_DATE IS '金融機関読替情報変更通知日(持出銀行コード変更・持帰銀行向け)(持帰)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.MRC_DATE IS '金融機関読替情報変更通知日(持帰銀行コード変更・持出銀行向け)(持出)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.MRD_DATE IS '金融機関読替情報変更通知日(持帰銀行コード変更・継承銀行向け)(持帰)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.YCA_MARK IS '判定不可(持帰)';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.EDIT_FLG IS '編集フラグ';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.BCA_STS IS '持出取消アップロード状態';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.GMA_STS IS '持帰訂正データアップロード状態';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.GRA_STS IS '不渡返還登録アップロード状態';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.GRA_CONFIRMDATE IS '不渡返還登録アップロード確定日';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.DELETE_DATE IS '削除日';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.DELETE_FLG IS '削除フラグ:0：未削除 1：削除済';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.UPDATE_DATE IS '更新日:更新システム日付';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.UPDATE_TIME IS '更新時間:更新システム時刻';
COMMENT ON COLUMN dbctr9999.TRMEI_HIST.UPDATE_KBN IS '更新区分:1：新規登録 2：アップデート';
exit;