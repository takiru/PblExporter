using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PblExporter.Core.Orca
{
    /// <summary>
    /// 実際にORCA操作を行う呼出し命令のベースを提供します。
    /// </summary>
    public abstract class OrcaExecutorBase
    {
        /// <summary>
        /// ORCAセッションを確立し、後続のORCA呼び出しに使用するハンドルを返します。
        /// </summary>
        /// <returns>HPBORCA。成功した場合はORCAセッションへのハンドルを返し、失敗した場合は0を返します。セッションを開くことができないのは、使用可能なメモリがない場合のみです。</returns>
        public abstract int SessionOpen();

        /// <summary>
        /// ORCAセッションを終了します。
        /// </summary>
        /// <param name="hORCASession">以前に確立されたORCAセッションの処理</param>
        public abstract void SessionClose(int hORCASession);

        /// <summary>
        /// ディレクトリ内のオブジェクトのリストなど、PowerBuilderライブラリのディレクトリに関する情報を報告します。
        /// </summary>
        /// <param name="hORCASession">以前に確立されたORCAセッションのハンドル。</param>
        /// <param name="lpszLibName">ディレクトリ情報が必要なライブラリのファイル名を値とする文字列へのポインタ。</param>
        /// <param name="lpszLibComments">ORCAがライブラリに保存されているコメントを入れるバッファーへのポインター。</param>
        /// <param name="iCmntsBufflen">lpszLibCommentsが指すバッファーの長さ（TCHARで指定）。推奨される長さはPBORCA_MAXCOMMENTS + 1です。</param>
        /// <param name="pListProc">PBORCA_LibraryDirectoryコールバック関数へのポインター。ライブラリ内のエントリごとにコールバック関数が呼び出されます。ORCAがコールバック関数に渡す情報は、エントリ名、コメント、エントリのサイズ、および変更時間であり、PBORCA_DIRENTRY型の構造体に格納されます。</param>
        /// <param name="pUserData">PBORCA_LibraryDirectoryコールバック関数に渡されるユーザーデータへのポインター。通常、ユーザーデータには、バッファまたはバッファへのポインタが含まれ、コールバック関数はディレクトリ情報とバッファのサイズに関する情報をフォーマットします。</param>
        /// <returns>PBORCA_OK, PBORCA_INVALIDPARMS, PBORCA_BADLIBRARY, PBORCA_LIBIOERROR</returns>
        public abstract int LibraryDirectory(int hORCASession, string lpszLibName,
                string lpszLibComments, int iCmntsBufflen, PBORCA_LISTPROC pListProc,
                IntPtr pUserData);

        public delegate void PBORCA_LISTPROC(IntPtr pDirEntry, IntPtr lpUserData);

        /// <summary>
        /// PBORCA_ConfigureSessionは、PowerBuilder 10との下位互換性を促進します。APIの柔軟性が向上し、他のORCA関数シグネチャに必要な変更が最小限に抑えられます。
        /// </summary>
        /// <param name="hORCASession">以前に確立されたORCAセッションのハンドル。</param>
        /// <param name="pSessionConfig">ORCAクライアントが後続のリクエストの動作を指定できる構造。設定は、セッションの間、またはPBORCA_ConfigureSessionを再度呼び出すまで有効です。PBORCA_ConfigureSessionを呼び出すたびに、必ずすべての設定を指定してください。</param>
        /// <returns>PBORCA_OK, PBORCA_INVALIDPARMS</returns>
        public abstract int ConfigureSession(int hORCASession,
            PBORCA_CONFIG_SESSION pSessionConfig);

        /// <summary>
        /// PowerBuilderライブラリエントリのソースコードをソースバッファまたはファイルにエクスポートします。
        /// </summary>
        /// <param name="hORCASession">以前に確立されたORCAセッションのハンドル。</param>
        /// <param name="lpszLibraryName">エクスポートするオブジェクトを含むライブラリのファイル名を値とする文字列へのポインター。</param>
        /// <param name="lpszEntryName">値がエクスポートされるオブジェクトの名前である文字列へのポインタ。</param>
        /// <param name="otEntryType">エクスポートされるエントリのオブジェクトタイプを指定するPBORCA_TYPE列挙データタイプの値。</param>
        /// <param name="lpszExportBuffer">PBORCA_CONFIG_SESSIONプロパティbExportCreateFileがFALSEの場合、エクスポートされたソースのコードをORCAが保存するデータバッファーへのポインター。bExportCreateFileがTRUEの場合、この引数はNULLになります。</param>
        /// <param name="lExportBufferSize">lpszExportBufferのバイト単位のサイズ。PBORCA_CONFIG_SESSIONプロパティbExportCreateFileがTRUEの場合、この引数は不要です。</param>
        /// <returns></returns>
        public abstract int LibraryEntryExport(int hORCASession,
                string lpszLibraryName,
                string lpszEntryName,
                EntryType otEntryType,
                StringBuilder lpszExportBuffer,
                int lExportBufferSize);
    }
}
