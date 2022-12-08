using System;
using System.Collections.Generic;
using System.IO;

namespace CommonClass
{
    public class TarArchive
    {
       #region tar圧縮・解凍

        /// <summary>
        /// tarファイルの解凍
        /// </summary>
        /// <param name="tarFile">解凍対象tarファイルパス(絶対パス)</param>
        /// <param name="tarPath">解凍先パス(絶対パス)</param>
        /// <returns>True:成功、False:失敗</returns>
        public static bool UnPackTarImg(string tarFile,string tarPath)
        {
            // 解凍用コマンドの作成
            string cmd = "--display-dialog=0 -xf " + tarFile + " -o " + tarPath;
            // dllファイルから解凍を実施
            return TarCmd(cmd);
        }

        /// <summary>
        /// tarファイルの圧縮
        /// </summary>
        /// <param name="tarFilePath">作成するtarファイルパス(絶対パス)</param>
        /// <param name="tarDirPath">圧縮対象ファイル格納先フォルダパス(絶対パス)</param>
        /// <param name="cmpFileNameList">圧縮対象ファイル名(ファイル名のみ)</param>
        /// <returns>True:成功、False:失敗</returns>
        public static bool PackTarImg(string tarFilePath, string tarDirPath, List<string> cmpFileNameList)
        {
            // 圧縮用コマンドの作成
            string cmd = "--use-directory=0 --display-dialog=0 -cf " + tarFilePath;
            cmd = cmd + " -o " + tarDirPath;
            foreach (string tarFile in cmpFileNameList)
            {
                cmd = cmd + " " + tarFile;
            }
            // dllファイルから圧縮を実施
            if (TarCmd(cmd))
            {
                return true;
            }
            else
            {
                // 失敗時はファイルを削除
                File.Delete(tarFilePath);
                return false;
            }
        }

        /// <summary>
        /// tarファイルの圧縮・解凍共通処理
        /// </summary>
        /// <param name="cmd">実行コマンド</param>
        private static bool TarCmd(string cmd)
        {
            // 戻り値が0なら成功
            return (TarArchiveHandler.Tar(0, cmd, null, 0) == 0);
        }

        #endregion
    }
}
