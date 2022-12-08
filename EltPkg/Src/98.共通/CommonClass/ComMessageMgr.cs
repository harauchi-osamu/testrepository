using System.Windows.Forms;


namespace CommonClass
{
    public class ComMessageMgr
    {
        /*
         コード命名ルール
         
         先頭1文字目（メッセージタイプ）
          E … Error
          W … Warning
          I … Infomation
          Q … Question(確認ダイアログ系)
         
         2-3文字目（メッセージ種類）
          00 … 共通
          01 … 取得関連
          02 … 入力関連
          99 … DB関連

         4-6文字目(連番の値)
        */

        #region 00(共通)
        public const string E00001 = "エラーが発生しました。";
        public const string E00002 = "引数が不正です。";
        public const string E00003 = "CtrServer.iniファイルが確認できませんでした。";
        public const string E00004 = "エラーが発生しました。（{0}）";
        public const string E00005 = "DB接続に失敗しました。（{0}）";
        public const string E00006 = "{0}の取得に失敗しました。";

        public const string W00001 = "対象データがありません。";
        public const string W00002 = "対象行が選択されていません。";
        public const string W00003 = "取得件数が{0}件を超えています。絞込条件を指定してください。";

        public const string I00001 = "{0}が完了しました。";
        public const string I00002 = "処理中です。";
        public const string I00003 = "印刷中です。";
        public const string I00004 = "PDF出力中です。";
        public const string I00005 = "ファイル出力中です。";
        public const string I00006 = "[{0}]　件数：{1:#,0}件";
        public const string I00007 = "[{0}]　件数：{1:#,0}件　金額合計：{2:#,0}円";
        public const string I00008 = "[{0}]　件数：{1:#,0}件　枚数合計：{2:#,0}枚　金額合計：{3:#,0}円";

        #endregion

        #region 01(取得関連)
        public const string E01001 = "設定情報を取得できませんでした。({0})";
        public const string E01002 = "業務情報を取得できませんでした。";
        public const string E01003 = "他のユーザーが処理中です。しばらくしてから再度実行してください。";
        public const string E01004 = "他のユーザーによって更新されました。登録内容を再度確認してください。";

        #endregion

        #region 02(入力関連)
        public const string E02001 = "{0}を入力してください。";
        public const string E02002 = "{0}は数値のみ入力可能です。";
        public const string E02003 = "{0}が有効な日付ではありません。";
        public const string E02004 = "{0}が入力可能な値ではありません。";
        public const string E02005 = "{0}が営業日ではありません。";
        public const string E02006 = "{0}は1以上を入力してください。";
        public const string E02007 = "{0}は{1}を入力してください。";

        #endregion

        #region 99(DB系)

        public const string E99001 = "データの取得に失敗しました。";

        #endregion 

        public static void MessageInformation(string msg, params object[] keys)
        {
            MessageBox.Show(string.Format(msg, keys), "通知", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void MessageWarning(string msg, params object[] keys)
        {
            MessageBox.Show(string.Format(msg, keys), "通知", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void MessageError(string msg, params object[] keys)
        {
            MessageBox.Show(string.Format(msg, keys), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static DialogResult MessageQuestion(MessageBoxButtons Buttons, string msg, params object[] keys)
        {
            return MessageQuestion(Buttons, MessageBoxDefaultButton.Button1, msg, keys);
        }

        public static DialogResult MessageQuestion(MessageBoxButtons Buttons, MessageBoxDefaultButton DefButtons, string msg, params object[] keys)
        {
            return MessageBox.Show(string.Format(msg, keys), "確認", Buttons, MessageBoxIcon.Question, DefButtons);
        }

        public static string Msg(string msg, params object[] keys)
        {
            return string.Format(msg, keys);
        }
    }
}
