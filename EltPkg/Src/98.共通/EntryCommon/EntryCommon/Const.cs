namespace EntryCommon
{
    public class Const
    {
        private Const() { }

        // *******************************************************************
        // 桁数
        // *******************************************************************

        /// <summary>業務ID桁数</summary>
        public const string GYM_ID_LEN_STR = "D5";
        public const int GYM_ID_LEN_5 = 5;
        /// <summary>画面ID桁数</summary>
        public const string DSP_ID_LEN_STR = "D6";

        /// <summary>バッチID桁数</summary>
        public const int BAT_ID_LEN = 10;
        /// <summary>バッチID桁数</summary>
        public const string BAT_ID_LEN_STR = "D6";
        /// <summary>明細番号桁数</summary>
        public const int DETAILS_NO_LEN = 6;
        /// <summary>明細番号桁数</summary>
        public const string DETAILS_NO_LEN_STR = "D6";
        /// <summary>店番桁数</summary>
        public const int BR_NO_LEN = 4;
        /// <summary>店番桁数</summary>
        public const string BR_NO_LEN_STR = "D4";
        /// <summary>銀行番号桁数</summary>
        public const int BANK_NO_LEN = 4;
        /// <summary>銀行番号桁数</summary>
        public const string BANK_NO_LEN_STR = "D4";
        /// <summary>金額桁数</summary>
        public const int AMOUNT_NO_LEN = 12;
        /// <summary>金額桁数</summary>
        public const string AMOUNT_NO_LEN_STR = "D12";
        /// <summary>交換証券種類コード桁数</summary>
        public const int BILL_CD_LEN = 3;
        /// <summary>交換証券種類コード桁数</summary>
        public const string BILL_CD_LEN_STR = "D3";
        /// <summary>手形種類コード桁数</summary>
        public const int SYURUI_CD_LEN = 3;
        /// <summary>手形種類コード桁数</summary>
        public const string SYURUI_CD_LEN_STR = "D3";
        /// <summary>口座番号桁数</summary>
        public const int KOZA_NO_LEN = 10;
        /// <summary>口座番号桁数</summary>
        public const string KOZA_NO_LEN_STR = "D10";
        /// <summary>手形番号桁数</summary>
        public const int TEGATA_NO_LEN = 10;
        /// <summary>手形番号桁数</summary>
        public const string TEGATA_NO_LEN_STR = "D10";
        /// <summary>決済対象区分桁数</summary>
        public const int PAYKBN_LEN = 1;
        /// <summary>決済対象区分桁数</summary>
        public const string PAYKBN_LEN_STR = "D1";

        /// <summary>電子交換ユーザーマスタ：ユーザーID桁数</summary>
        public const int CTRUSER_USERID_LEN = 14;
        /// <summary>電子交換ユーザーマスタ：パスワード桁数</summary>
        public const int CTRUSER_PASSWORD_LEN = 32;


        // *******************************************************************
        // 文字定数
        // *******************************************************************

        /// <summary>Oracleエラー：行ロック中</summary>
        public const string ORACLE_ERR_LOCK = "ORA-00054";
        /// <summary>Oracleエラー：一意制約</summary>
        public const string ORACLE_ERR_UNIQUE = "ORA-00001";

        ///// <summary>HULFT：配信ログ</summary>
        //public const string HULFT_SEND_LOG = "sendlog.txt";
        ///// <summary>HULFT：配信エラーログ</summary>
        //public const string HULFT_SEND_ERRLOG = "senderr.txt";
        ///// <summary>HULFT：集信ログ</summary>
        //public const string HULFT_RECV_LOG = "recvlog.txt";
        ///// <summary>HULFT：集信エラーログ</summary>
        //public const string HULFT_RECV_ERRLOG = "recverr.txt";

        /// <summary>HULFT：改行コード</summary>
        public const string CRLF = "\r\n";


        // *******************************************************************
        // 定義値
        // *******************************************************************

        /// <summary>フォント名（デフォルト）</summary>
        public const string FONT_NAME_DEF = "MS UI Gothic";
        /// <summary>フォントサイズ：ファンクション（デフォルト）</summary>
        public const float FONT_SIZE_FUNC_DEF = 14F;
        /// <summary>フォントサイズ：ファンクション（小）</summary>
        public const float FONT_SIZE_FUNC_LOW = 10F;

        /// <summary>フォントサイズ（項目用）</summary>
        public const float FONT_SIZE_ITEM_DEF = 11F;

        /// <summary>イメージ枠マージン調整（横）</summary>
        public const int IMAGE_RECTANGLE_X = 20;
        /// <summary>イメージ枠マージン調整（縦）</summary>
        public const int IMAGE_RECTANGLE_Y = 200;

        /// <summary>イメージ拡大</summary>
        public const int IMAGE_ZOOM_IN = 0;
        /// <summary>イメージ縮小</summary>
        public const int IMAGE_ZOOM_OUT = 1;
        /// <summary>イメージ拡大率</summary>
        public const float IMAGE_ZOOM_PER = 0.15f;
        /// <summary>スクロールバーの幅</summary>
        public const int SCROLLBAR_WIDTH = 20;

        /// <summary>ユーザー情報ファイル</summary>
        public const string FILE_NAME_USERID = "USERID.txt";

    }
}
