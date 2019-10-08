using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static NewPblParser.Orca;

namespace NewPblParser
{
    public class Orca
    {
        //    private static int session = 0;
        //    private PbVersion currentVersion;

        //    /// <summary>
        //    /// HPBORCA。成功した場合はORCAセッションへのハンドルを返し、失敗した場合は0を返します。セッションを開くことができないのは、使用可能なメモリがない場合のみです。
        //    /// </summary>
        //    /// <returns></returns>
        //    [DllImport("PBORC100.DLL", EntryPoint = "PBORCA_SessionOpen", CharSet = CharSet.Unicode, SetLastError = true)]
        //    //private static unsafe extern int PBORCA_SessionOpen125();
        //    private static extern int PBORCA_SessionOpen125();

        //    public Orca(PbVersion version)
        //    {
        //        this.currentVersion = version;

        //        if (session == 0)
        //            SessionOpen();
        //    }

        //    public void SessionOpen()
        //    {
        //        session = PBORCA_SessionOpen125();
        //    }

        //    private List<LibEntry> libEntries;
        //    private string currentLibrary = null;
        //    private delegate void PBORCA_LIBDIRCALLBACK(IntPtr pDirEntry, IntPtr lpUserData);

        //    public List<LibEntry> DirLibrary(string file)
        //    {
        //        int orcaSession = 0;
        //        PBORCA_LIBDIRCALLBACK PBORCA_LibraryDirectoryCallback = new PBORCA_LIBDIRCALLBACK(PBORCA_LibDirCallback);
        //        IntPtr dummy = new IntPtr();

        //        currentLibrary = file;
        //        libEntries = new List<LibEntry>();

        //        if (session == 0)
        //        {
        //            orcaSession = PBORCA_SessionOpen125();
        //        }
        //        else
        //            orcaSession = session;


        //        PBORCA_LibraryDirectory125(orcaSession, file, "", 0, PBORCA_LibraryDirectoryCallback, dummy);

        //        if (session == 0)
        //        {
        //            PBORCA_SessionClose125(orcaSession);
        //        }

        //        return libEntries;
        //    }

        //    private void PBORCA_LibDirCallback(IntPtr pDirEntry, IntPtr lpUserData)
        //    {
        //        PBORCA_DIRENTRY myDirEntry = (PBORCA_DIRENTRY)Marshal.PtrToStructure(pDirEntry, typeof(PBORCA_DIRENTRY));
        //        DateTime myDateTime = new DateTime(1970, 01, 01, 00, 00, 00).AddSeconds((double)myDirEntry.lCreateTime);

        //        libEntries.Add(new LibEntry(myDirEntry.lpszEntryName, GetObjecttype(myDirEntry.otEntryType), myDateTime, myDirEntry.lEntrySize, currentLibrary, this.currentVersion, myDirEntry.szComments));
        //    }

        //    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        //    private struct PBORCA_DIRENTRY
        //    {
        //        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        //        public string szComments;
        //        public int lCreateTime;
        //        public int lEntrySize;
        //        public string lpszEntryName;
        //        public PBORCA_ENTRY_TYPE otEntryType;
        //    }

        //    //private enum PBORCA_ENTRY_TYPE
        //    //{
        //    //    PBORCA_APPLICATION,
        //    //    PBORCA_DATAWINDOW,
        //    //    PBORCA_FUNCTION,
        //    //    PBORCA_MENU,
        //    //    PBORCA_QUERY,
        //    //    PBORCA_STRUCTURE,
        //    //    PBORCA_USEROBJECT,
        //    //    PBORCA_WINDOW,
        //    //    PBORCA_PIPELINE,
        //    //    PBORCA_PROJECT,
        //    //    PBORCA_PROXYOBJECT,
        //    //    PBORCA_BINARY
        //    //}

        //    //private EntryType GetObjecttype(PBORCA_ENTRY_TYPE entryType)
        //    //{
        //    //    EntryType type = EntryType.None;

        //    //    switch (entryType)
        //    //    {
        //    //        case PBORCA_ENTRY_TYPE.PBORCA_APPLICATION:
        //    //            type = EntryType.Application;
        //    //            break;
        //    //        case PBORCA_ENTRY_TYPE.PBORCA_BINARY:
        //    //            type = EntryType.Binary;
        //    //            break;
        //    //        case PBORCA_ENTRY_TYPE.PBORCA_DATAWINDOW:
        //    //            type = EntryType.Datawindow;
        //    //            break;
        //    //        case PBORCA_ENTRY_TYPE.PBORCA_FUNCTION:
        //    //            type = EntryType.Function;
        //    //            break;
        //    //        case PBORCA_ENTRY_TYPE.PBORCA_MENU:
        //    //            type = EntryType.Menu;
        //    //            break;
        //    //        case PBORCA_ENTRY_TYPE.PBORCA_PIPELINE:
        //    //            type = EntryType.Pipeline;
        //    //            break;
        //    //        case PBORCA_ENTRY_TYPE.PBORCA_PROJECT:
        //    //            type = EntryType.Project;
        //    //            break;
        //    //        case PBORCA_ENTRY_TYPE.PBORCA_PROXYOBJECT:
        //    //            type = EntryType.Proxyobject;
        //    //            break;
        //    //        case PBORCA_ENTRY_TYPE.PBORCA_QUERY:
        //    //            type = EntryType.Query;
        //    //            break;
        //    //        case PBORCA_ENTRY_TYPE.PBORCA_STRUCTURE:
        //    //            type = EntryType.Structure;
        //    //            break;
        //    //        case PBORCA_ENTRY_TYPE.PBORCA_USEROBJECT:
        //    //            type = EntryType.Userobject;
        //    //            break;
        //    //        case PBORCA_ENTRY_TYPE.PBORCA_WINDOW:
        //    //            type = EntryType.Window;
        //    //            break;
        //    //    }

        //    //    return type;
        //    //}

        //    /// <summary>
        //    /// 0 PBORCA_OK 操作成功
        //    /// -1 PBORCA_INVALIDPARMS 無効なパラメーターリスト
        //    /// -4 PBORCA_BADLIBRARY 悪いライブラリ名
        //    /// -7 PBORCA_LIBIOERROR ライブラリI / Oエラー
        //    /// </summary>
        //    /// <param name="hORCASession"></param>
        //    /// <param name="lpszLibName"></param>
        //    /// <param name="lpszLibComments"></param>
        //    /// <param name="iCmntsBufflen"></param>
        //    /// <param name="pListProc"></param>
        //    /// <param name="pUserData"></param>
        //    /// <returns></returns>
        //    [DllImport("PBORC100.DLL", EntryPoint = "PBORCA_LibraryDirectory", CharSet = CharSet.Unicode, SetLastError = true)]
        //    private static extern int PBORCA_LibraryDirectory125(
        //        int hORCASession,
        //        [MarshalAs(UnmanagedType.LPTStr)] string lpszLibName,
        //        [MarshalAs(UnmanagedType.LPTStr)] string lpszLibComments,
        //        int iCmntsBufflen,
        //        PBORCA_LIBDIRCALLBACK pListProc,
        //        IntPtr pUserData
        //    );

        //    /// <summary>
        //    /// 戻り値なし。
        //    /// </summary>
        //    /// <param name="hORCASession"></param>
        //    [DllImport("PBORC100.DLL", EntryPoint = "PBORCA_SessionClose", CharSet = CharSet.Unicode, SetLastError = true)]
        //    //private static unsafe extern void PBORCA_SessionClose125(int hORCASession);
        //    private static extern void PBORCA_SessionClose125(int hORCASession);

        //    public void FillCode(LibEntry libEntry)
        //    {
        //        int orcaSession = 0;
        //        StringBuilder sbSource = new StringBuilder(5242880); // 5 MB

        //        if (session == 0)
        //        {
        //            orcaSession = PBORCA_SessionOpen125();
        //        }
        //        else
        //            orcaSession = session;

        //        PBORCA_LibraryEntryExport125(orcaSession, libEntry.Library, libEntry.Name, GetEntryType(libEntry.Type), sbSource, sbSource.Capacity);


        //        libEntry.Source = sbSource.ToString();

        //        if (session == 0)
        //        {
        //            PBORCA_SessionClose125(orcaSession);
        //        }
        //    }

        //    /// <summary>
        //    /// 0 PBORCA_OK 操作成功
        //    /// -1 PBORCA_INVALIDPARMS 無効なパラメーターリスト
        //    /// -3 PBORCA_OBJNOTFOUND オブジェクトが見つかりません
        //    /// -4 PBORCA_BADLIBRARY 悪いライブラリ名
        //    /// -7 PBORCA_LIBIOERROR ライブラリI / Oエラー
        //    /// -10 PBORCA_BUFFERTOOSMALL バッファサイズが小さすぎます
        //    /// -33 PBORCA_DBCSERROR UnicodeをANSI_DBCSに変換する際のロケール設定エラー
        //    /// </summary>
        //    /// <param name="hORCASession"></param>
        //    /// <param name="lpszLibraryName"></param>
        //    /// <param name="lpszEntryName"></param>
        //    /// <param name="otEntryType"></param>
        //    /// <param name="lpszExportBuffer"></param>
        //    /// <param name="lExportBufferSize"></param>
        //    /// <returns></returns>
        //    [DllImport("PBORC100.DLL", EntryPoint = "PBORCA_LibraryEntryExport", CharSet = CharSet.Auto)]
        //    private static extern int PBORCA_LibraryEntryExport125(
        //        int hORCASession,
        //        [MarshalAs(UnmanagedType.LPWStr)] string lpszLibraryName,
        //        [MarshalAs(UnmanagedType.LPWStr)] string lpszEntryName,
        //        PBORCA_ENTRY_TYPE otEntryType,
        //        [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpszExportBuffer,
        //        System.Int32 lExportBufferSize);

        //    private PBORCA_ENTRY_TYPE GetEntryType(EntryType type)
        //    {
        //        PBORCA_ENTRY_TYPE entryType = PBORCA_ENTRY_TYPE.PBORCA_BINARY;

        //        switch (type)
        //        {
        //            case EntryType.Application:
        //                entryType = PBORCA_ENTRY_TYPE.PBORCA_APPLICATION;
        //                break;
        //            case EntryType.Binary:
        //                entryType = PBORCA_ENTRY_TYPE.PBORCA_BINARY;
        //                break;
        //            case EntryType.Datawindow:
        //                entryType = PBORCA_ENTRY_TYPE.PBORCA_DATAWINDOW;
        //                break;
        //            case EntryType.Function:
        //                entryType = PBORCA_ENTRY_TYPE.PBORCA_FUNCTION;
        //                break;
        //            case EntryType.Menu:
        //                entryType = PBORCA_ENTRY_TYPE.PBORCA_MENU;
        //                break;
        //            case EntryType.Pipeline:
        //                entryType = PBORCA_ENTRY_TYPE.PBORCA_PIPELINE;
        //                break;
        //            case EntryType.Project:
        //                entryType = PBORCA_ENTRY_TYPE.PBORCA_PROJECT;
        //                break;
        //            case EntryType.Proxyobject:
        //                entryType = PBORCA_ENTRY_TYPE.PBORCA_PROXYOBJECT;
        //                break;
        //            case EntryType.Query:
        //                entryType = PBORCA_ENTRY_TYPE.PBORCA_QUERY;
        //                break;
        //            case EntryType.Structure:
        //                entryType = PBORCA_ENTRY_TYPE.PBORCA_STRUCTURE;
        //                break;
        //            case EntryType.Userobject:
        //                entryType = PBORCA_ENTRY_TYPE.PBORCA_USEROBJECT;
        //                break;
        //            case EntryType.Window:
        //                entryType = PBORCA_ENTRY_TYPE.PBORCA_WINDOW;
        //                break;
        //        }

        //        return entryType;
        //    }



        //    public int SetCodeHeader()
        //    {
        //        int orcaSession = 0;
        //        if (session == 0)
        //        {
        //            orcaSession = PBORCA_SessionOpen125();
        //        }
        //        else
        //            orcaSession = session;

        //        var config = new PBORCA_CONFIG_SESSION();
        //        config.bExportHeaders = true;

        //        return PBORCA_ConfigureSession125(orcaSession, config);
        //    }

        //    public int SetCodeHeaderAndExportInfo()
        //    {
        //        int orcaSession = 0;
        //        if (session == 0)
        //        {
        //            orcaSession = PBORCA_SessionOpen125();
        //        }
        //        else
        //            orcaSession = session;

        //        var config = new PBORCA_CONFIG_SESSION();
        //        config.bExportHeaders = true;
        //        config.bExportCreateFile = true;
        //        config.bExportIncludeBinary = true;
        //        config.pExportDirectory = @"D:\abc";
        //        config.eExportEncoding = PBORCA_ENCODING.PBORCA_UNICODE;

        //        return PBORCA_ConfigureSession125(orcaSession, config);
        //    }

        //    /// <summary>
        //    /// 0 PBORCA_OK 操作成功
        //    /// -1 PBORCA_INVALIDPARMS セッションが開いていないか、pConfigポインターがヌルです
        //    /// </summary>
        //    /// <param name="hORCASession"></param>
        //    /// <param name="pSessionConfig"></param>
        //    /// <returns></returns>
        //    [DllImport("PBORC100.DLL", EntryPoint = "PBORCA_ConfigureSession", CharSet = CharSet.Auto)]
        //    private static extern int PBORCA_ConfigureSession125(
        //        int hORCASession,
        //        PBORCA_CONFIG_SESSION pSessionConfig);

        //    //public enum PBORCA_ENUM_FILEWRITE_OPTION
        //    //{
        //    //    PBORCA_NOCLOBBER,   // 既存のファイルを上書きしない
        //    //    PBORCA_CLOBBER, // 書き込み保護されていない既存のファイルを上書きします
        //    //    PBORCA_CLOBBER_ALWAYS,  // 書き込み保護されている既存のファイルを上書きします
        //    //    PBORCA_CLOBBER_DECIDED_BY_SYSTEM    // 上記の機能は、以前のORCAリリースと同じように動作します
        //    //}

        //    //public enum PBORCA_ENCODING
        //    //{
        //    //    PBORCA_UNICODE, // Unicode ORCAクライアントのデフォルト
        //    //    PBORCA_UTF8,    // UTF-8
        //    //    PBORCA_HEXASCII,    // 文字のHEX表記
        //    //    PBORCA_ANSI_DBCS    // ANSI ORCAクライアントのデフォルト
        //    //}
        //}

        ////[StructLayout(LayoutKind.Sequential)]
        ////public class PBORCA_CONFIG_SESSION
        ////{
        ////    public PBORCA_ENUM_FILEWRITE_OPTION eClobber = PBORCA_ENUM_FILEWRITE_OPTION.PBORCA_CLOBBER; //既存のファイルを上書きしますか？
        ////    public PBORCA_ENCODING eExportEncoding = PBORCA_ENCODING.PBORCA_UNICODE; //エクスポートされたソースのエンコーディング
        ////    public bool bExportHeaders = false;     //エクスポートヘッダーでソースをフォーマットします
        ////    public bool bExportIncludeBinary = false;   //バイナリを含めます
        ////    public bool bExportCreateFile = false;   //ソースをファイルにエクスポート
        ////    [MarshalAs(UnmanagedType.LPWStr)]  public string pExportDirectory;  //エクスポートされたファイルのディレクトリ
        ////    public PBORCA_ENCODING eImportEncoding = PBORCA_ENCODING.PBORCA_UNICODE; //インポートされたソースのエンコーディング
        ////    public bool bDebug = false;      //コンパイラディレクティブをデバッグします

        ////    public int filler2;//将来の使用のために予約済み
        ////    public int filler3;
        ////    public int filler4;
    }
}
