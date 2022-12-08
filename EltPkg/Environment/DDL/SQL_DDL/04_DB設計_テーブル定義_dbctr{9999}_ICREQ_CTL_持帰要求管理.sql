DROP TABLE dbctr9999.ICREQ_CTL;
CREATE TABLE dbctr9999.ICREQ_CTL (
    REQ_TXT_NAME varchar2(32) NOT NULL,
    REQ_DATE number(8,0) default 0  NOT NULL,
    REQ_TIME number(9,0) default 0  NOT NULL,
    CLEARING_DATE_S number(8,0) default 0  NOT NULL,
    CLEARING_DATE_E number(8,0) default 0  NOT NULL,
    BILL_CODE varchar2(3),
    IC_TYPE number(1,0) default -1  NOT NULL,
    IMG_NEED number(1,0) default -1  NOT NULL,
    RET_TXT_NAME varchar2(32),
    RET_COUNT number(3,0) default 0  NOT NULL,
    RET_STS number(4,0) default 0  NOT NULL,
    RET_MAKE_DATE number(8,0) default 0  NOT NULL,
    RET_REQ_TXT_NAME varchar2(32),
    RET_FILE_CHK_CODE varchar2(10),
    RET_CLEARING_DATE_S varchar2(8),
    RET_CLEARING_DATE_E varchar2(8),
    RET_BILL_CODE varchar2(3),
    RET_IC_TYPE varchar2(1),
    RET_IMG_NEED varchar2(1),
    RET_PROC_RETCODE varchar2(10),
    RET_DATE number(8,0) default 0  NOT NULL,
    RET_TIME number(9,0) default 0  NOT NULL,
 CONSTRAINT PK_DBCTR9999_ICREQ_CTL PRIMARY KEY (
     REQ_TXT_NAME
));
COMMENT ON TABLE dbctr9999.ICREQ_CTL IS '持帰要求管理';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.REQ_TXT_NAME IS '要求テキストファイル名';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.REQ_DATE IS '要求日:要求テキスト作成日（OS日付）';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.REQ_TIME IS '要求時刻:要求テキスト作成時刻（OS時刻）';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.CLEARING_DATE_S IS '交換希望日（開始）';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.CLEARING_DATE_E IS '交換希望日（終了）';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.BILL_CODE IS '交換証券種類コード';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.IC_TYPE IS '持帰対象区分:"0"：「持帰状況フラグ」のステータスによらず全量 "1"：「持帰状況フラグ」が"0":未持帰のデータ';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.IMG_NEED IS '証券イメージ要否:"0"：証券イメージ不要 "1"：証券イメージ要';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_TXT_NAME IS '結果テキストファイル名:(結果ヘッダー情報)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_COUNT IS '結果件数:(結果トレーラー情報)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_STS IS '結果銀行コード:(結果ヘッダー情報)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_MAKE_DATE IS '結果作成日:(結果ヘッダー情報)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_REQ_TXT_NAME IS '結果持帰要求テキストファイル名:(結果ヘッダー情報)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_FILE_CHK_CODE IS '結果ファイルチェック結果コード:(結果ヘッダー情報)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_CLEARING_DATE_S IS '結果交換希望日１:(結果ヘッダー情報)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_CLEARING_DATE_E IS '結果交換希望日２:(結果ヘッダー情報)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_BILL_CODE IS '結果交換証券種類コード:(結果ヘッダー情報)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_IC_TYPE IS '結果持帰対象区分:(結果ヘッダー情報)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_IMG_NEED IS '結果証券イメージ要否区分:(結果ヘッダー情報)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_PROC_RETCODE IS '結果処理結果コード:(結果ヘッダー情報)';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_DATE IS '結果取込日:要求結果取込日（OS日付）';
COMMENT ON COLUMN dbctr9999.ICREQ_CTL.RET_TIME IS '結果取込時刻:要求結果取込時刻（OS時刻）';
exit;