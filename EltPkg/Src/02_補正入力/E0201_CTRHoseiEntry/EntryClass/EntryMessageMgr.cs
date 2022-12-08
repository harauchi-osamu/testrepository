using CommonClass;


namespace EntryClass
{
    public class EntMessageMgr
    {
        // エントリ系固有
        public const string ENT10001 = "指定された明細は処理できる状態ではありません。";
        public const string ENT10002 = "指定した明細は処理できる状態ではありません。\n他のユーザーに更新された可能性があります。";
        public const string ENT10003 = "指定した項目は既に登録されています。";
        public const string ENT10004 = "選択した項目を削除します。よろしいですか？\n{0}";

        // msg.ini にあったメッセージ
        public const string E0103 = "業務番号：{0} の業務の削除に失敗しました。\nLOGを確認してください。";
        public const string E0104 = "画面番号：{0} の情報の保存に失敗しました。\nLOGを確認してください。";
        public const string E0105 = "業務番号：{0} の情報の保存に失敗しました。\nLOGを確認してください。";
        public const string E0106 = "画面情報の保存で排他処理に失敗しました。";
        public const string E0111 = "イメージカーソルの情報の保存に失敗しました。";
        public const string E0112 = "イメージの詳細情報の保存に失敗しました。";

        public const string I0101 = "メンテナンス業務番号を入力してください。";
        public const string I0102 = "業務番号：{0} が存在しません。";
        public const string I0104 = "メンテナンス業務番号を入力してください。";
        public const string I0105 = "新規のメンテナンス業務番号を入力した場合は、先に業務登録を行ってください。";
        public const string I0106 = "メンテナンス画面番号を入力してください。";
        public const string I0107 = "画面番号：{0} が存在しません。";
        public const string I0109 = "コピー先の業務番号が存在します。先に業務番号：{0} を削除してください。";
        public const string I0110 = "業務番号：{0} の業務を削除しました。";
        public const string I0114 = "コピー先の業務番号が存在します。先に業務番号：{0} を削除してください。";
        public const string I0120 = "業務名カナを入力してください。";
        public const string I0121 = "業務名を入力してください。";
        public const string I0122 = "更新に成功しました。";
        public const string I0123 = "業務番号：{0} の情報を更新しました。";
        public const string I0124 = "業務番号：{0} 内にメニュー表示ありの画面ＩＤがありません。";
        public const string I0130 = "画面番号：{0} の画面を削除しました。";
        public const string I0133 = "画面パラメータ(処理)の操作が確定されずに終了しています。";

        public const string I0140 = "業務：{0}  初期画面の画面パラメータが存在しません。";
        public const string I0142 = "業務：{0}  画面：{1}  画面パラメータの画面番号と同じ番号の項目パラメータが存在しません。";
        public const string I0143 = "業務：{0}  画面：{1}  合計パラメータで合計プルーフ指定を行なっていないのに、画面パラメータで合計カウント指定が行われました。";
        public const string I0144 = "業務：{0}  画面：{1}  画面パラメータの次の画面番号：{2} で指定した画面パラメータが存在しません。";
        public const string I0145 = "業務：{0}  画面：{1}  １画面内で全ての項目にＤＵＰありの指定がされています。";
        public const string I0146 = "業務：{0}  画面：{1}  この画面にはベリファイありが設定されています。ベリファイ項目を指定してください。";
        public const string I0147 = "業務：{0}  画面：{1}  連記式で縮小率が設定されていません。";
        public const string I0148 = "業務：{0}  画面：{1}  項目：{2}  合計パラメータで合計プルーフ指定を行なっていないのに、項目パラメータで合計カウント指定が行われました。";
        public const string I0149 = "業務：{0}  画面：{1}  この画面にはベリファイなしが設定されています。ベリファイ項目を解除してください。";
        public const string I0150 = "画面番号：{0} のイメージ詳細情報を更新しました。";
        public const string I0163 = "業務：{0}  画面：{1}  項目パラメータの桁数、位置の値は\n業務パラメータのレコード長{2} より小さくなければいけません。";

        public const string W0101 = "名称位置(TOP)は５～１０００の範囲で設定してください。 ";
        public const string W0102 = "名称位置(LEFT)は５～１２００の範囲で設定してください。 ";
        public const string W0103 = "合計位置(TOP)は５～１０００の範囲で設定してください。 ";
        public const string W0104 = "合計位置(LEFT)は５～１２００の範囲で設定してください。 ";
        public const string W0105 = "入力位置(TOP)は５～１００の範囲で設定してください。 ";
        public const string W0106 = "入力位置(LEFT)は５～１２００の範囲で設定してください。 ";
        public const string W0107 = "桁数は０より大きい値のみ入力可能です。 ";
        public const string W0108 = "タイプは「{0} 」の場合、\n桁数は「{1} 」値のみ入力可能です。 ";
        public const string W0109 = "タイプは「{0} 」の場合、\n位置は「{1} 」値のみ入力可能です。 ";
        public const string W0110 = "オーバーヘッド／ダイレクト もしくは明細追加の場合はOCR帳票の選択ができません。 ";
        public const string W0114 = "イメージファイル名を入力してください。 ";
        public const string W0115 = "文字列を入力してください。 ";
        public const string W0116 = "数字を入力してください。 ";
        public const string W0117 = "項目情報シートに入力エラーがあります。 ";
        public const string W0118 = "項目情報シートに項目情報を入力してください。 ";
        public const string W0119 = "項目が表示しない場合、\n位置にはゼロを入力してください。 ";
        public const string W0120 = "イメージファイルが存在しません。\n確認してください。 ";
        public const string W0123 = "抽出時のレコード長は１～1024の範囲で入力してください。 ";
        public const string W0130 = "縮小率は1～100の範囲で入力してください。 ";
        public const string W0137 = "OCR帳票名の{0} 行目に２０文字以内で入力して下さい。 ";
        public const string W0138 = "サブルーチンが存在しません。 ";
        public const string W0142 = "未入力のデータが存在します。 ";
        public const string W0144 = "抽出サブルーチン：{0} が存在しません。 ";
        public const string W0147 = "抽出業務名カナを入力してください。 ";
        public const string W0148 = "抽出業務名を入力してください。 ";
        public const string W0150 = "抽出ファイル名を入力してください。 ";
        public const string W0151 = "抽出業務番号を入力してください。 ";

        public const string Q0101 = "業務番号：{0} の業務を削除します。\nよろしいですか？";
        public const string Q0102 = "コピー元業務番号：{0} が存在しません。メンテナンス業務番号：{1} を修正しますか？";
        public const string Q0103 = "コピー元業務番号：{0} が存在しません。メンテナンス業務番号：{1} を新規作成しますか？";
        public const string Q0104 = "画面番号：{0} の画面を削除します。\nよろしいですか？";
        public const string Q0105 = "コピー元画面番号：{0} が存在しません。メンテナンス画面番号：{1} を新規作成しますか？";
        public const string Q0106 = "画面の入力項目に変更があります。破棄しますか？";
        public const string Q0108 = "コピー元画面番号：{0} が存在しません。メンテナンス画面番号：{1} を訂正しますか？";
        public const string Q0109 = "業務番号:「{0} 」のエントリデータが存在します。{1} 、\nエントリデータが適合しなくなる可能性があります。よろしいでしょうか？";
        public const string Q0117 = "新規業務：{0} はまだ保存していません。破棄しますか？";
        public const string Q0118 = "新規複写業務：{0} はまだ保存していません。破棄しますか？";
        public const string Q0122 = "{0}中です。終了しますか？";
        public const string Q0130 = "業務番号：{0} が未確定です。確定せずに終了しますか？";
        public const string Q0140 = "画面番号：{0} の画面を削除します。\nよろしいですか？";
        public const string Q0141 = "新規複写画面：{0} はまだ保存していません。破棄しますか？";
    }
}
