using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PblExporter.Core.Orca
{
    /// <summary>
    /// PBORCA_ConfigureSession に設定するインポート／エクスポートの文字エンコーディングを定義。
    /// </summary>
    public enum PBORCA_ENCODING
    {
        /// <summary>
        /// Unicode ORCAクライアントのデフォルト
        /// </summary>
        PBORCA_UNICODE,

        /// <summary>
        /// UTF-8
        /// </summary>
        PBORCA_UTF8,

        /// <summary>
        /// 文字のHEX表記
        /// </summary>
        PBORCA_HEXASCII,

        /// <summary>
        /// ANSI ORCAクライアントのデフォルト
        /// </summary>
        PBORCA_ANSI_DBCS
    }
}
