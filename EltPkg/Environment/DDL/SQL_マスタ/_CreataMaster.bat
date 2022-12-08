@echo OFF

rem "[接続ユーザー]/[パスワード]@[接続文字列] AS SYSDBA" @[SQLファイル名]

set DB_CON="sys/admin001@IPRUN AS SYSDBA"

rem dbctr
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_BANKMF_銀行マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_BILLMF_証券種類マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_BKCHANGEMF_銀行読替マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_ERA_元号マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_FUWATARIMF_不渡事由コードマスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_HOLIDAY_休日マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_OPERATION_DATE_処理日付.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_CHANGE_DSPIDMF_画面番号変換マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_GENERALTEXTMF_テキスト汎用マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_TERM_LOCK_取扱端末ロック.sql

rem dbctr9999
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_BRANCHMF_支店マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_CHANGEMF_読替マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_CTRUSERINFO_電子交換ユーザー情報マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_DSP_ITEM_画面項目定義.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_DSP_PARAM_画面パラメータ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_FILE_PARAM_ファイルパラメータ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_CTR_OCR_PARAM_電子交換所OCRパラメータ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_CTR_OCRCONF_PARAM_電子交換所OCR確信度パラメータ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_GYM_PARAM_業務パラメータ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_HOSEIMODE_DSP_ITEM_補正モード画面項目定義.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_HOSEIMODE_PARAM_補正モードパラメータ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_IMG_CURSOR_PARAM_イメージカーソルパラメータ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_IMG_PARAM_イメージパラメータ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_ITEM_MASTER_項目定義.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_PAYERMF_支払人マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_SUB_RTN_サブルーチン.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_SYURUIMF_種類マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_CHANGE_BILLMF_交換証券種類変換マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_CTR_MICRCUTINFO_PARAM_電子交換所MICR切出情報パラメータ.sql

pause
