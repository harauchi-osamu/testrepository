DROP TABLE dbctr9999.GYM_PARAM;
CREATE TABLE dbctr9999.GYM_PARAM (
    GYM_ID NUMBER(3,0) NOT NULL,
    GYM_KANA VARCHAR2(80),
    GYM_KANJI VARCHAR2(80),
    DONE_FLG VARCHAR2(1),
CONSTRAINT PK_DBCTR9999_GYM_PARAM PRIMARY KEY (
     GYM_ID
));
COMMENT ON TABLE dbctr9999.GYM_PARAM IS '業務パラメータ';
COMMENT ON COLUMN dbctr9999.GYM_PARAM.GYM_ID IS '業務番号:0：共通 1：持出 2：持帰';
COMMENT ON COLUMN dbctr9999.GYM_PARAM.GYM_KANA IS '業務名カナ';
COMMENT ON COLUMN dbctr9999.GYM_PARAM.GYM_KANJI IS '業務名';
COMMENT ON COLUMN dbctr9999.GYM_PARAM.DONE_FLG IS '完成フラグ:0：メンテ中 1： 通常';
exit;