using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static NewPblParser.OrcaExecutorBase;

namespace NewPblParser
{
    /// <summary>
    /// ORCAを操作する命令を提供します。
    /// </summary>
    public class Orca
    {
        private OrcaExecutorBase orcaExecutor = null;

        private int session = 0;

        /// <summary>
        /// 新しいOrcaインスタンスを生成します。
        /// </summary>
        /// <param name="orcaExecutor">OrcaExecutorBaseオブジェクト。</param>
        public Orca(OrcaExecutorBase orcaExecutor)
        {
            this.orcaExecutor = orcaExecutor;
        }

        /// <summary>
        /// PB操作を開始します。
        /// </summary>
        /// <returns>true:成功, false:失敗。</returns>
        public bool OpenSession()
        {
            session = orcaExecutor.SessionOpen();
            if (session == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// PB操作を終了します。
        /// </summary>
        public void CloseSession()
        {
            orcaExecutor.SessionClose(session);
        }

        private string pblPath = "";
        private List<ObjectInfo> objectInfoList = null;

        /// <summary>
        /// PBLからオブジェクト一覧を取得します。
        /// </summary>
        /// <param name="pblPath"></param>
        /// <returns>PBORCA_OK, PBORCA_INVALIDPARMS, PBORCA_BADLIBRARY, PBORCA_LIBIOERROR</returns>
        public int GetLibraryDirectory(string pblPath, out List<ObjectInfo> list)
        {
            objectInfoList = new List<ObjectInfo>();
            this.pblPath = pblPath;
            var callback = new PBORCA_LISTPROC(PBORCA_LibraryDirectoryCallback);

            var result = orcaExecutor.LibraryDirectory(session, pblPath, "", 0, callback, new IntPtr());
            list = objectInfoList;
            return result;
        }

        /// <summary>
        /// LibraryDirectory によって発生するコールバック。
        /// </summary>
        /// <param name="pDirEntry">IntPtrオブジェクト。</param>
        /// <param name="lpUserData">IntPtrオブジェクト。</param>
        private void PBORCA_LibraryDirectoryCallback(IntPtr pDirEntry, IntPtr lpUserData)
        {
            ObjectInfoStructure myDirEntry = (ObjectInfoStructure)Marshal.PtrToStructure(pDirEntry, typeof(ObjectInfoStructure));
            DateTime myDateTime = new DateTime(1970, 01, 01, 00, 00, 00).AddSeconds((double)myDirEntry.CreateTime);
            objectInfoList.Add(new ObjectInfo(pblPath, myDirEntry.ObjectName, myDirEntry.ObjectType, myDateTime, myDirEntry.Size, myDirEntry.Comment));
        }

        /// <summary>
        /// LibraryDirectory によって得られた情報の構造体。
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct ObjectInfoStructure
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string Comment;
            public int CreateTime;
            public int Size;
            public string ObjectName;
            public EntryType ObjectType;
        }

        /// <summary>
        /// オブジェクトのエクスポートを行います。
        /// </summary>
        /// <param name="objectInfo">ObjectInfoオブジェクト。</param>
        /// <param name="code">エクスポートしたコード。</param>
        /// <param name="config">PBORCA_CONFIG_SESSIONオブジェクト。</param>
        /// <param name="buffer">エクスポートを行うバッファサイズ。</param>
        /// <returns>PBORCA_OK, PBORCA_INVALIDPARMS, PBORCA_OBJNOTFOUND, PBORCA_BADLIBRARY, PBORCA_LIBIOERROR, PBORCA_BUFFERTOOSMALL, PBORCA_DBCSERROR</returns>
        public int Export(ObjectInfo objectInfo, out string code, PBORCA_CONFIG_SESSION config = null, int buffer = 5242880)
        {
            code = "";

            // ConfigSessionを設定
            if (config != null) {
                var configResult = SetSessionConfig(config);
                if (configResult != OrcaReturnCode.PBORCA_OK)
                {
                    return configResult;
                }
            }

            var result = Export(objectInfo.PblPath, objectInfo.ObjectName, objectInfo.EntryType, out code, buffer);
            return result;
        }

        /// <summary>
        /// オブジェクトのエクスポートを行います。
        /// </summary>
        /// <param name="pblPath">PBLファイルパス。</param>
        /// <param name="objectName">オブジェクト名。</param>
        /// <param name="entryType">オブジェクトのタイプ。</param>
        /// <param name="code">エクスポートしたコード。</param>
        /// <param name="buffer">エクスポートを行うバッファサイズ。</param>
        /// <returns>PBORCA_OK, PBORCA_INVALIDPARMS, PBORCA_OBJNOTFOUND, PBORCA_BADLIBRARY, PBORCA_LIBIOERROR, PBORCA_BUFFERTOOSMALL, PBORCA_DBCSERROR</returns>
        public int Export(string pblPath, string objectName, EntryType entryType, out string code, int buffer = 5242880)
        {
            StringBuilder codeSb = new StringBuilder(buffer);
            var result = orcaExecutor.LibraryEntryExport(session, pblPath, objectName, entryType, codeSb, codeSb.Capacity);
            code = codeSb.ToString();
            return result;
        }

        /// <summary>
        /// インポート／エクスポートに利用する設定を行います。
        /// </summary>
        /// <param name="config">PBORCA_CONFIG_SESSION オブジェクト。</param>
        /// <returns>PBORCA_OK, PBORCA_INVALIDPARMS</returns>
        public int SetSessionConfig(PBORCA_CONFIG_SESSION config)
        {
            return orcaExecutor.ConfigureSession(session, config);
        }
    }

}
