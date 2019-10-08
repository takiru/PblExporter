using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NewPblParser
{
    /// <summary>
    /// PBORCA_ConfigureSession に設定する設定情報を提供します。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class PBORCA_CONFIG_SESSION
    {
        /// <summary>
        /// 既存のファイルを上書きするかどうか。
        /// </summary>
        public PBORCA_ENUM_FILEWRITE_OPTION eClobber = PBORCA_ENUM_FILEWRITE_OPTION.PBORCA_CLOBBER;

        /// <summary>
        /// エクスポートされたソースのエンコーディング。
        /// </summary>
        public PBORCA_ENCODING eExportEncoding = PBORCA_ENCODING.PBORCA_UNICODE;

        /// <summary>
        /// エクスポートヘッダーでソースをフォーマットするかどうか。
        /// </summary>
        public bool bExportHeaders = false;

        /// <summary>
        /// エクスポートにバイナリを含めるかどうか。
        /// </summary>
        public bool bExportIncludeBinary = false;

        /// <summary>
        /// ソースをファイルにエクスポートするかどうか。
        /// </summary>
        public bool bExportCreateFile = false;

        /// <summary>
        /// エクスポートするディレクトリ。
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)] public string pExportDirectory = "";

        /// <summary>
        /// インポートされたソースのエンコーディング。
        /// </summary>
        public PBORCA_ENCODING eImportEncoding = PBORCA_ENCODING.PBORCA_UNICODE;

        /// <summary>
        /// コンパイラディレクティブをデバッグするかどうか。
        /// </summary>
        public bool bDebug = false;

        /// <summary>
        /// 将来の使用のために予約済み
        /// </summary>
        public IntPtr filler2 = new IntPtr();

        /// <summary>
        /// 将来の使用のために予約済み
        /// </summary>
        public IntPtr filler3 = new IntPtr();

        /// <summary>
        /// 将来の使用のために予約済み
        /// </summary>
        public IntPtr filler4 = new IntPtr();
    }
}
