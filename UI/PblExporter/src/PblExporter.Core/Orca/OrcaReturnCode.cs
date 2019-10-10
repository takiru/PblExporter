using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PblExporter.Core.Orca
{
    /// <summary>
    /// ORCAの実行戻り値を提供します。
    /// </summary>
    public static class OrcaReturnCode
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        public const int PBORCA_OK = 0;
        /// <summary>
        /// 無効なパラメーターリスト
        /// </summary>
        public const int PBORCA_INVALIDPARMS = -1;
        /// <summary>
        /// 重複操作
        /// </summary>
        public const int PBORCA_DUPOPERATION = -2;
        /// <summary>
        /// オブジェクトが見つかりません
        /// </summary>
        public const int PBORCA_OBJNOTFOUND = -3;
        /// <summary>
        /// 悪いライブラリ名
        /// </summary>
        public const int PBORCA_BADLIBRARY = -4;
        /// <summary>
        /// ライブラリリストが設定されていません
        /// </summary>
        public const int PBORCA_LIBLISTNOTSET = -5;
        /// <summary>
        /// ライブラリリストにないライブラリ
        /// </summary>
        public const int PBORCA_LIBNOTINLIST = -6;
        /// <summary>
        /// ライブラリI / Oエラー
        /// </summary>
        public const int PBORCA_LIBIOERROR = -7;
        /// <summary>
        /// オブジェクトが存在します
        /// </summary>
        public const int PBORCA_OBJEXISTS = -8;
        /// <summary>
        /// 無効な名前
        /// </summary>
        public const int PBORCA_INVALIDNAME = -9;
        /// <summary>
        /// バッファサイズが小さすぎます
        /// </summary>
        public const int PBORCA_BUFFERTOOSMALL = -10;
        /// <summary>
        /// コンパイルエラー
        /// </summary>
        public const int PBORCA_COMPERROR = -11;
        /// <summary>
        /// リンクエラー
        /// </summary>
        public const int PBORCA_LINKERROR = -12;
        /// <summary>
        /// 現在のアプリケーションが設定されていません
        /// </summary>
        public const int PBORCA_CURRAPPLNOTSET = -13;
        /// <summary>
        /// オブジェクトには祖先がありません
        /// </summary>
        public const int PBORCA_OBJHASNOANCS = -14;
        /// <summary>
        /// オブジェクトには参照がありません
        /// </summary>
        public const int PBORCA_OBJHASNOREFS = -15;
        /// <summary>
        /// 無効なPBDの数
        /// </summary>
        public const int PBORCA_PBDCOUNTERROR = -16;
        /// <summary>
        /// PBD作成エラー
        /// </summary>
        public const int PBORCA_PBDCREATERROR = -17;
        /// <summary>
        /// ソース管理エラー（廃止）
        /// </summary>
        public const int PBORCA_CHECKOUTERROR = -18;
        /// <summary>
        /// ComponentBuilderクラスをインスタンス化できませんでした
        /// </summary>
        public const int PBORCA_CBCREATEERROR = -19;
        /// <summary>
        /// コンポーネントビルダーの初期化メソッドが失敗しました
        /// </summary>
        public const int PBORCA_CBINITERROR = -20;
        /// <summary>
        /// コンポーネントビルダーBuildProjectメソッドが失敗しました
        /// </summary>
        public const int PBORCA_CBBUILDERROR = -21;
        /// <summary>
        /// ソース管理に接続できませんでした
        /// </summary>
        public const int PBORCA_SCCFAILURE = -22;
        /// <summary>
        /// レジストリを読み取れませんでした
        /// </summary>
        public const int PBORCA_REGREADERROR = -23;
        /// <summary>
        /// DLLをロードできませんでした
        /// </summary>
        public const int PBORCA_SCCLOADDLLFAILED = -24;
        /// <summary>
        /// SCC接続を初期化できませんでした
        /// </summary>
        public const int PBORCA_SCCINITFAILED = -25;
        /// <summary>
        /// SCCプロジェクトを開けませんでした
        /// </summary>
        public const int PBORCA_OPENPROJFAILED = -26;
        /// <summary>
        /// ターゲットファイルが見つかりません
        /// </summary>
        public const int PBORCA_TARGETNOTFOUND = -27;
        /// <summary>
        /// ターゲットファイルを読み取れません
        /// </summary>
        public const int PBORCA_TARGETREADERR = -28;
        /// <summary>
        /// SCCインターフェースにアクセスできません
        /// </summary>
        public const int PBORCA_GETINTERFACEERROR = -29;
        /// <summary>
        /// Sccオフライン接続にはIMPORTONLY更新オプションが必要です
        /// </summary>
        public const int PBORCA_IMPORTONLY_REQ = -30;
        /// <summary>
        /// SCCオフライン接続には、Exclude_Checkoutを指定したGetConnectPropertiesが必要です
        /// </summary>
        public const int PBORCA_GETCONNECT_REQ = -31;
        /// <summary>
        /// Exclude_Checkoutを使用したSCCのオフライン接続にはPBCファイルが必要です
        /// </summary>
        public const int PBORCA_PBCFILE_REQ = -32;
    }
}
