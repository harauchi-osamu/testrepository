namespace CheckDigitApi
{
    /// <summary>
    /// チェックディジット
    /// </summary>
    public class CheckDigit
    {
        private CheckDigit() { }

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// チェックディジットによるチェック処理を行う
        /// </summary>
        /// <param name="accountNo"></param>
        /// <returns>true:チェックOK、false:チェックNG</returns>
        public static bool DoCheck(int accountNo)
        {
            // 銀行毎に算出ロジックが異なるのでここに実装する

            // 0の場合エラーとする2022.07.14
            if (accountNo == 0)
            {
                // NG
                return false;
            }

            // OK
            return true;
        }
    }
}
