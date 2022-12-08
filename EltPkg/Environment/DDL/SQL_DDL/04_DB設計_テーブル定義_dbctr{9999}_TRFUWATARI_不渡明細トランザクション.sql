DROP TABLE dbctr9999.TRFUWATARI;
CREATE TABLE dbctr9999.TRFUWATARI (
    GYM_ID number(3,0) NOT NULL,
    OPERATION_DATE number(8,0) NOT NULL,
    SCAN_TERM varchar2(20) NOT NULL,
    BAT_ID number(6,0) NOT NULL,
    DETAILS_NO number(6,0) NOT NULL,
    FUBI_KBN_01 number(1,0) default -1  NOT NULL,
    ZERO_FUBINO_01 number(2,0) default -1  NOT NULL,
    FUBI_KBN_02 number(1,0) default -1  NOT NULL,
    ZERO_FUBINO_02 number(2,0) default -1  NOT NULL,
    FUBI_KBN_03 number(1,0) default -1  NOT NULL,
    ZERO_FUBINO_03 number(2,0) default -1  NOT NULL,
    FUBI_KBN_04 number(1,0) default -1  NOT NULL,
    ZERO_FUBINO_04 number(2,0) default -1  NOT NULL,
    FUBI_KBN_05 number(1,0) default -1  NOT NULL,
    ZERO_FUBINO_05 number(2,0) default -1  NOT NULL,
    DELETE_DATE number(8,0) default 0  NOT NULL,
    DELETE_FLG number(1) default 0  NOT NULL,
    E_TERM varchar2(20),
    E_OPENO varchar2(20),
    E_YMD number(8,0) default 0  NOT NULL,
    E_TIME number(11,0) default 0  NOT NULL,
 CONSTRAINT PK_DBCTR9999_TRFUWATARI PRIMARY KEY (
     GYM_ID
    ,OPERATION_DATE
    ,SCAN_TERM
    ,BAT_ID
    ,DETAILS_NO
));
COMMENT ON TABLE dbctr9999.TRFUWATARI IS '不渡明細トランザクション';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.GYM_ID IS '業務番号';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.OPERATION_DATE IS '処理日';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.SCAN_TERM IS 'イメージ取込端末';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.BAT_ID IS 'バッチ番号';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.DETAILS_NO IS '明細番号';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.FUBI_KBN_01 IS '不渡返還区分１:不渡返還区分をセットする。 ・"0":0号不渡 ・"1":第1号不渡 ・"2":第2号不渡 ・登録区分が"9"(取消)の場合は、省略値をセットする。';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.ZERO_FUBINO_01 IS '0号不渡事由コード１:不渡事由コードをセットする。 詳細は「6.3. 不渡事由コード」を参照。 ・不渡返還区分１が"0"(0号不渡)の場合は、不渡事由コードをセットする。 ・不渡返還区分１が"0"(0号不渡)以外の場合は、省略値をセットする。 ・登録区分が"9"(取消)の場合は、省略値をセットする。';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.FUBI_KBN_02 IS '不渡返還区分２:不渡返還区分をセットする。 ・"0":0号不渡 ・"1":第1号不渡 ・"2":第2号不渡 ・不渡返還区分1が省略値以外の場合のみセット可能 ・登録区分が"9"(取消)の場合は、省略値をセットする。 ・登録情報がない場合は、省略値をセットする。';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.ZERO_FUBINO_02 IS '0号不渡事由コード２:不渡事由コードをセットする。 詳細は「6.3. 不渡事由コード」を参照。 ・不渡返還区分２が"0"(0号不渡)の場合は、不渡事由コードをセットする。 ・不渡返還区分２が"0"(0号不渡)以外の場合は、省略値をセットする。 ・登録区分が"9"(取消)の場合は、省略値をセットする。';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.FUBI_KBN_03 IS '不渡返還区分３:不渡返還区分をセットする。 ・"0":0号不渡 ・"1":第1号不渡 ・"2":第2号不渡 ・不渡返還区分1～2が省略値以外の場合のみセット可能 ・登録区分が"9"(取消)の場合は、省略値をセットする。 ・登録情報がない場合は、省略値をセットする。';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.ZERO_FUBINO_03 IS '0号不渡事由コード３:不渡事由コードをセットする。 詳細は「6.3. 不渡事由コード」を参照。 ・不渡返還区分３が"0"(0号不渡)の場合は、不渡事由コードをセットする。 ・不渡返還区分３が"0"(0号不渡)以外の場合は、省略値をセットする。 ・登録区分が"9"(取消)の場合は、省略値をセットする。';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.FUBI_KBN_04 IS '不渡返還区分４:不渡返還区分をセットする。 ・"0":0号不渡 ・"1":第1号不渡 ・"2":第2号不渡 ・不渡返還区分1～3が省略値以外の場合のみセット可能 ・登録区分が"9"(取消)の場合は、省略値をセットする。 ・登録情報がない場合は、省略値をセットする。';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.ZERO_FUBINO_04 IS '0号不渡事由コード４:不渡事由コードをセットする。 詳細は「6.3. 不渡事由コード」を参照。 ・不渡返還区分４が"0"(0号不渡)の場合は、不渡事由コードをセットする。 ・不渡返還区分４が"0"(0号不渡)以外の場合は、省略値をセットする。 ・登録区分が"9"(取消)の場合は、省略値をセットする。';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.FUBI_KBN_05 IS '不渡返還区分５:不渡返還区分をセットする。 ・"0":0号不渡 ・"1":第1号不渡 ・"2":第2号不渡 ・不渡返還区分1～4が省略値以外の場合のみセット可能 ・登録区分が"9"(取消)の場合は、省略値をセットする。 ・登録情報がない場合は、省略値をセットする。';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.ZERO_FUBINO_05 IS '0号不渡事由コード５:不渡事由コードをセットする。 詳細は「6.3. 不渡事由コード」を参照。 ・不渡返還区分５が"0"(0号不渡)の場合は、不渡事由コードをセットする。 ・不渡返還区分５が"0"(0号不渡)以外の場合は、省略値をセットする。 ・登録区分が"9"(取消)の場合は、省略値をセットする。';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.DELETE_DATE IS '取消日';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.DELETE_FLG IS '取消フラグ:0：未取消 1：取消済';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.E_TERM IS '入力端末';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.E_OPENO IS '入力オペレーター番号';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.E_YMD IS '入力日付';
COMMENT ON COLUMN dbctr9999.TRFUWATARI.E_TIME IS '入力時間';
exit;