using System;
using System.Runtime.InteropServices;
using System.Text;
using PblExporter.Core.Orca;

namespace PblExportTools2019
{
    internal class OrcaExecutor : OrcaExecutorBase
    {
        [DllImport("PBORC.DLL", EntryPoint = "PBORCA_SessionOpen", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int PBORCA_SessionOpen();

        [DllImport("PBORC.DLL", EntryPoint = "PBORCA_SessionClose", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern void PBORCA_SessionClose(int hORCASession);

        [DllImport("PBORC.DLL", EntryPoint = "PBORCA_LibraryDirectory", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int PBORCA_LibraryDirectory(
            int hORCASession,
            [MarshalAs(UnmanagedType.LPTStr)] string lpszLibName,
            [MarshalAs(UnmanagedType.LPTStr)] string lpszLibComments,
            int iCmntsBufflen,
            PBORCA_LISTPROC pListProc,
            IntPtr pUserData
        );

        [DllImport("PBORC.DLL", EntryPoint = "PBORCA_ConfigureSession", CharSet = CharSet.Auto)]
        private static extern int PBORCA_ConfigureSession(
            int hORCASession,
            PBORCA_CONFIG_SESSION pSessionConfig);

        [DllImport("PBORC.DLL", EntryPoint = "PBORCA_LibraryEntryExport", CharSet = CharSet.Auto)]
        private static extern int PBORCA_LibraryEntryExport(
            int hORCASession,
            [MarshalAs(UnmanagedType.LPWStr)] string lpszLibraryName,
            [MarshalAs(UnmanagedType.LPWStr)] string lpszEntryName,
            EntryType otEntryType,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpszExportBuffer,
            System.Int32 lExportBufferSize);


        public override int ConfigureSession(int hORCASession, PBORCA_CONFIG_SESSION pSessionConfig)
        {
            return PBORCA_ConfigureSession(hORCASession, pSessionConfig);
        }

        public override int LibraryDirectory(int hORCASession, string lpszLibName, string lpszLibComments, int iCmntsBufflen, PBORCA_LISTPROC pListProc, IntPtr pUserData)
        {
            return PBORCA_LibraryDirectory(hORCASession, lpszLibName, lpszLibComments, iCmntsBufflen, pListProc, pUserData);
        }

        public override int LibraryEntryExport(int hORCASession, string lpszLibraryName, string lpszEntryName, EntryType otEntryType, StringBuilder lpszExportBuffer, int lExportBufferSize)
        {
            return PBORCA_LibraryEntryExport(hORCASession, lpszLibraryName, lpszEntryName, otEntryType, lpszExportBuffer, lExportBufferSize);
        }

        public override void SessionClose(int hORCASession)
        {
            PBORCA_SessionClose(hORCASession);
        }

        public override int SessionOpen()
        {
            return PBORCA_SessionOpen();
        }
    }
}
