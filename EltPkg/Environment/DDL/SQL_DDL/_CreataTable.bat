@echo OFF

rem "[接続ユーザー]/[パスワード]@[接続文字列] AS SYSDBA" @[SQLファイル名]

set DB_CON="sys/admin001@IPRUN AS SYSDBA"

rem dbctr
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_BANKMF_銀行マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_BKCHANGEMF_銀行読替マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_HOLIDAY_休日マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_OCR_DATA_OCR認識結果.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_OPERATION_DATE_処理日付.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_SCAN_BATCH_CTL_スキャンバッチ管理.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_SCAN_MEI_スキャン明細.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_TERM_LOCK_取扱端末ロック.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_WK_IMGELIST_明細イメージリスト.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_CHANGE_DSPIDMF_画面番号変換マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_FUWATARIMF_不渡事由コードマスタ.sql
sqlplus %DB_CON% @04_DB設計_ストアド_dbctr_SCAN_BATCH_CTL_EXTRACTION_バッチデータ取得.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_GENERALTEXTMF_テキスト汎用マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_LOCK_SYSTEM_PROCESS_システムプロセスロック.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_BR_TOTAL_支店別合計票.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_OPERATOR_オペレータマスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_IC_OCR_FINISH_持帰OCR完了明細.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_BILLMF_交換証券種類マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr_ERA_元号マスタ.sql

rem dbctr9999
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_BRANCHMF_支店マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_CHANGEMF_読替マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_CTRUSERINFO_電子交換ユーザー情報マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_DSP_ITEM_画面項目定義.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_DSP_PARAM_画面パラメータ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_FILE_CTL_ファイル集配信管理.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_FILE_SEQ_ファイル一連番号採番.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_GYM_PARAM_業務パラメータ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_HOSEIMODE_DSP_ITEM_補正モード画面項目定義.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_HOSEIMODE_PARAM_補正モードパラメータ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_ICREQRET_BILLMEITXT_持帰要求結果証券明細テキスト.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_ICREQRET_CTL_持帰要求結果管理.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_HOSEI_STATUS_補正ステータス.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_IMG_CURSOR_PARAM_イメージカーソルパラメータ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_IMG_PARAM_イメージパラメータ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_ITEM_MASTER_項目定義.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_PAYERMF_支払人マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_RESULTTXT_CTL_結果テキスト管理.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_RESULTTXT_結果テキスト.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_SYURUIMF_種類マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_TRBATCH_バッチデータ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_TRFUWATARI_不渡明細トランザクション.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_TRITEM_項目トランザクション.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_TRMEIIMG_明細イメージトランザクション.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_TRMEI_明細トランザクション.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_BATCH_SEQ_バッチ番号採番.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_TRBATCHIMG_バッチイメージトランザクション.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_BALANCETXT_CTL_交換尻管理.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_BALANCETXT_交換尻.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_FILE_PARAM_ファイルパラメータ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_BILLMEITXT_CTL_証券明細テキスト管理.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_BILLMEITXT_証券明細テキスト.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_CTR_OCR_PARAM_電子交換所OCRパラメータ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_ICREQ_CTL_持帰要求管理.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_TRITEM_HIST_項目トランザクション履歴.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_TRBATCH_HIST_バッチデータ履歴.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_TSUCHITXT_CTL_通知テキスト管理.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_TSUCHITXT_通知テキスト.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_LOCK_BANK_PROCESS_銀行別業務ロック.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_TRMEI_HIST_明細トランザクション履歴.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_TRMEIIMG_HIST_明細イメージトランザクション履歴.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_SUB_RTN_サブルーチン.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_CTR_OCRCONF_PARAM_電子交換所OCR確信度パラメータ.sql
sqlplus %DB_CON% @04_DB設計_トリガー_dbctr{9999}_TRBATCH_LOG_TRG_バッチデータ履歴.sql
sqlplus %DB_CON% @04_DB設計_トリガー_dbctr{9999}_TRITEM_LOG_TRG_項目トランザクション履歴.sql
sqlplus %DB_CON% @04_DB設計_トリガー_dbctr{9999}_TRMEI_LOG_TRG_明細トランザクション履歴.sql
sqlplus %DB_CON% @04_DB設計_トリガー_dbctr{9999}_TRMEIIMG_LOG_TRG_明細イメージトランザクション履歴.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_CHANGE_BILLMF_交換証券種類変換マスタ.sql
sqlplus %DB_CON% @04_DB設計_ビュー定義_dbctr{9999}_BRANCHMF_支店別合計票支店マスタ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_CTR_MICRCUTINFO_PARAM_電子交換所MICR切出情報パラメータ.sql
sqlplus %DB_CON% @04_DB設計_テーブル定義_dbctr{9999}_SEND_FILE_TRMEI_配信ファイル明細内訳.sql

pause
