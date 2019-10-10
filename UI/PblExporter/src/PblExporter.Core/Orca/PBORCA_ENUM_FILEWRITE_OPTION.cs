using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PblExporter.Core.Orca
{
    /// <summary>
    /// PBORCA_ConfigureSession に設定するファイルの上書き方法を定義します。
    /// </summary>
    public enum PBORCA_ENUM_FILEWRITE_OPTION
    {
        /// <summary>
        /// 既存のファイルを上書きしない
        /// </summary>
        PBORCA_NOCLOBBER,

        /// <summary>
        /// 書き込み保護されていない既存のファイルを上書きします
        /// </summary>
        PBORCA_CLOBBER,

        /// <summary>
        /// 書き込み保護されている既存のファイルを上書きします
        /// </summary>
        PBORCA_CLOBBER_ALWAYS,

        /// <summary>
        /// 上記の機能は、以前のORCAリリースと同じように動作します
        /// </summary>
        PBORCA_CLOBBER_DECIDED_BY_SYSTEM
    }
}
