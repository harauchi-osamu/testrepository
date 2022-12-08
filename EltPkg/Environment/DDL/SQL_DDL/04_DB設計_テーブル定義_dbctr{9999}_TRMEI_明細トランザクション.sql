DROP TABLE dbctr9999.TRMEI;
CREATE TABLE dbctr9999.TRMEI (
    GYM_ID number(3,0) NOT NULL,
    OPERATION_DATE number(8,0) NOT NULL,
    SCAN_TERM varchar2(20) NOT NULL,
    BAT_ID number(6,0) NOT NULL,
    DETAILS_NO number(6,0) NOT NULL,
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
    MEMO varchar2(256),
    DELETE_DATE number(8,0) default 0  NOT NULL,
    DELETE_FLG number(1,0) default 0  NOT NULL,
 CONSTRAINT PK_DBCTR9999_TRMEI PRIMARY KEY (
     GYM_ID
    ,OPERATION_DATE
    ,SCAN_TERM
    ,BAT_ID
    ,DETAILS_NO
));
COMMENT ON TABLE dbctr9999.TRMEI IS '明細トランザクション';
COMMENT ON COLUMN dbctr9999.TRMEI.GYM_ID IS '業務番号';
COMMENT ON COLUMN dbctr9999.TRMEI.OPERATION_DATE IS '処理日';
COMMENT ON COLUMN dbctr9999.TRMEI.SCAN_TERM IS 'イメージ取込端末:IPアドレス（.→_に変換）';
COMMENT ON COLUMN dbctr9999.TRMEI.BAT_ID IS 'バッチ番号';
COMMENT ON COLUMN dbctr9999.TRMEI.DETAILS_NO IS '明細番号';
COMMENT ON COLUMN dbctr9999.TRMEI.DSP_ID IS '画面番号';
COMMENT ON COLUMN dbctr9999.TRMEI.IC_OC_BK_NO IS '持出銀行(持帰)';
COMMENT ON COLUMN dbctr9999.TRMEI.IC_OLD_OC_BK_NO IS '旧持出銀行(持帰)';
COMMENT ON COLUMN dbctr9999.TRMEI.BUA_DATE IS '二重持出通知日(持出)';
COMMENT ON COLUMN dbctr9999.TRMEI.BUB_DATE IS '二重持出通知日(持帰)';
COMMENT ON COLUMN dbctr9999.TRMEI.BCA_DATE IS '持出取消通知日(持帰)';
COMMENT ON COLUMN dbctr9999.TRMEI.GMA_DATE IS '証券データ訂正通知日(持出)';
COMMENT ON COLUMN dbctr9999.TRMEI.GMB_DATE IS '証券データ訂正通知日(持帰)';
COMMENT ON COLUMN dbctr9999.TRMEI.GRA_DATE IS '不渡返還通知日(持出)';
COMMENT ON COLUMN dbctr9999.TRMEI.GXA_DATE IS '決済後訂正通知日(持出)';
COMMENT ON COLUMN dbctr9999.TRMEI.GXB_DATE IS '決済後訂正通知日(持帰)';
COMMENT ON COLUMN dbctr9999.TRMEI.MRA_DATE IS '金融機関読替情報変更通知日(持出銀行コード変更・継承銀行向け)(持出)';
COMMENT ON COLUMN dbctr9999.TRMEI.MRB_DATE IS '金融機関読替情報変更通知日(持出銀行コード変更・持帰銀行向け)(持帰)';
COMMENT ON COLUMN dbctr9999.TRMEI.MRC_DATE IS '金融機関読替情報変更通知日(持帰銀行コード変更・持出銀行向け)(持出)';
COMMENT ON COLUMN dbctr9999.TRMEI.MRD_DATE IS '金融機関読替情報変更通知日(持帰銀行コード変更・継承銀行向け)(持帰)';
COMMENT ON COLUMN dbctr9999.TRMEI.YCA_MARK IS '判定不可(持帰):０：なし １：あり';
COMMENT ON COLUMN dbctr9999.TRMEI.EDIT_FLG IS '編集フラグ:０：未編集 １：編集中';
COMMENT ON COLUMN dbctr9999.TRMEI.BCA_STS IS '持出取消アップロード状態:0：未作成 1：再作成対象 5：ファイル作成 10：アップロード 19：結果エラー 20：結果正常';
COMMENT ON COLUMN dbctr9999.TRMEI.GMA_STS IS '持帰訂正データアップロード状態:0：未作成 1：再作成対象 5：ファイル作成 10：アップロード 19：結果エラー 20：結果正常';
COMMENT ON COLUMN dbctr9999.TRMEI.GRA_STS IS '不渡返還登録アップロード状態:0：未作成 1：再作成対象 5：ファイル作成 10：アップロード 19：結果エラー 20：結果正常';
COMMENT ON COLUMN dbctr9999.TRMEI.GRA_CONFIRMDATE IS '不渡返還登録アップロード確定日';
COMMENT ON COLUMN dbctr9999.TRMEI.MEMO IS 'メモ';
COMMENT ON COLUMN dbctr9999.TRMEI.DELETE_DATE IS '削除日';
COMMENT ON COLUMN dbctr9999.TRMEI.DELETE_FLG IS '削除フラグ:0：未削除 1：削除済';
exit;