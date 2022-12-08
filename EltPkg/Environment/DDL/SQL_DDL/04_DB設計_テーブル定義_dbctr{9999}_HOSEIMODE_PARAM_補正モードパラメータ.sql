DROP TABLE dbctr9999.HOSEIMODE_PARAM;
CREATE TABLE dbctr9999.HOSEIMODE_PARAM (
    GYM_ID NUMBER(3,0) NOT NULL,
    DSP_ID NUMBER(3,0) NOT NULL,
    HOSEI_ITEMMODE NUMBER(1,0) NOT NULL,
    AUTO_SKIP_MODE_ENT NUMBER(1,0) default -1  NOT NULL,
    AUTO_SKIP_MODE_VFY NUMBER(1,0) default -1  NOT NULL,
    VERY_MODE NUMBER(1,0) default -1  NOT NULL,
CONSTRAINT PK_DBCTR9999_HOSEIMODE_PARAM PRIMARY KEY (
     GYM_ID
    ,DSP_ID
    ,HOSEI_ITEMMODE
));
COMMENT ON TABLE dbctr9999.HOSEIMODE_PARAM IS '補正モードパラメータ';
COMMENT ON COLUMN dbctr9999.HOSEIMODE_PARAM.GYM_ID IS '業務番号:1：持出 2：持帰';
COMMENT ON COLUMN dbctr9999.HOSEIMODE_PARAM.DSP_ID IS '画面番号';
COMMENT ON COLUMN dbctr9999.HOSEIMODE_PARAM.HOSEI_ITEMMODE IS '補正項目モード:1：持帰銀行 2：交換希望日 3：金額 4：自行情報 5：交換尻';
COMMENT ON COLUMN dbctr9999.HOSEIMODE_PARAM.AUTO_SKIP_MODE_ENT IS 'エントリオートスキップ:0：なし 1：あり';
COMMENT ON COLUMN dbctr9999.HOSEIMODE_PARAM.AUTO_SKIP_MODE_VFY IS 'ベリファイオートスキップ:0：なし 1：あり';
COMMENT ON COLUMN dbctr9999.HOSEIMODE_PARAM.VERY_MODE IS 'ベリファイ有無:0：なし 1：あり';
exit;