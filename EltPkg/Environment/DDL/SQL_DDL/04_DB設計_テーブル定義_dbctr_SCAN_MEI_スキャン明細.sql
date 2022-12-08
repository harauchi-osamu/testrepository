DROP TABLE dbctr.SCAN_MEI;
CREATE TABLE dbctr.SCAN_MEI (
    IMG_NAME varchar2(100) NOT NULL,
    OPERATION_DATE number(8,0) NOT NULL,
    INPUT_ROUTE number(1,0) default 0  NOT NULL,
    BATCH_FOLDER_NAME varchar2(20) NOT NULL,
    BATCH_UCHI_RENBAN number(6,0) default 0  NOT NULL,
    IMG_KBN number(2,0) default 0  NOT NULL,
    I_TERM varchar2(20),
    I_OPENO varchar2(20),
    I_YMD number(8,0) default 0  NOT NULL,
    I_TIME number(9,0) default 0  NOT NULL,
 CONSTRAINT PK_DBCTR_SCAN_MEI PRIMARY KEY (
     IMG_NAME
    ,OPERATION_DATE
    ,BATCH_FOLDER_NAME
));

COMMENT ON TABLE dbctr.SCAN_MEI IS 'スキャン明細';
COMMENT ON COLUMN dbctr.SCAN_MEI.IMG_NAME IS 'イメージファイル名';
COMMENT ON COLUMN dbctr.SCAN_MEI.OPERATION_DATE IS '処理日';
COMMENT ON COLUMN dbctr.SCAN_MEI.INPUT_ROUTE IS '取込ルート:2:付帯バッチルート';
COMMENT ON COLUMN dbctr.SCAN_MEI.BATCH_FOLDER_NAME IS 'バッチフォルダ名';
COMMENT ON COLUMN dbctr.SCAN_MEI.BATCH_UCHI_RENBAN IS 'バッチ内連番';
COMMENT ON COLUMN dbctr.SCAN_MEI.IMG_KBN IS '表裏等の別';
COMMENT ON COLUMN dbctr.SCAN_MEI.I_TERM IS '入力端末番号';
COMMENT ON COLUMN dbctr.SCAN_MEI.I_OPENO IS '入力オペレーター番号';
COMMENT ON COLUMN dbctr.SCAN_MEI.I_YMD IS '入力日付';
COMMENT ON COLUMN dbctr.SCAN_MEI.I_TIME IS '入力時間';
exit;