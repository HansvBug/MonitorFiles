using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace MonitorFiles.Class
{
    public class MfItemData : IDisposable
    {
        private readonly SafeHandle safeHandle = new SafeFileHandle(IntPtr.Zero, true); // Instantiate a SafeHandle instance.
        private bool disposed;  // Flag: Has Dispose already been called?

        public int ID { get; set; }
        public string GUID { get; set; }
        public string FILE_OR_FOLDER_NAME { get; set; }
        public string FILE_NAME { get; set; }
        public string FOLDER_NAME { get; set; }
        public int SOURCE_ID { get; set; }
        public string SOURCE_NAME { get; set; }
        public int TONWSHIP_ID { get; set; }
        public string TONWSHIP_NAME { get; set; }
        public int FILETYPE_ID { get; set; }
        public int TOPFOLDER_ID { get; set; }
        public string FILETYPE_NAME { get; set; }
        public string TOPFOLDER_NAME { get; set; }
        public int DIFF_MAX { get; set; }
        public int FILE_ORDER { get; set; }
        public string COMMENT { get; set; }

        public string FileStatus { get; set; }  // Voeldoet de datum het gecontroleerde bestand aan diff_max
        public DateTime fileModificationDate { get; set; }
        public int daysDifference { get; set; }
        private string ConcatItemData { get; set; }



        public MfItemData()
        {
            GUID = string.Empty;
            FILE_OR_FOLDER_NAME = string.Empty;
            FILE_NAME = string.Empty;
            FOLDER_NAME = string.Empty;
            SOURCE_ID = -1;
            TONWSHIP_ID = -1;
            FILETYPE_ID = -1;
            SOURCE_NAME = string.Empty;
            TONWSHIP_NAME = string.Empty;
            FILETYPE_NAME = string.Empty;
            DIFF_MAX = -1;
            FILE_ORDER = -1;
            COMMENT = string.Empty;
            ConcatItemData = string.Empty;
        }

        public string Concat()
        {
            ConcatItemData = FILE_NAME + ";" + FOLDER_NAME + ";" + SOURCE_ID.ToString() + ";" + TONWSHIP_ID.ToString();
            return ConcatItemData;
        }

        /// <summary>
        /// Implement IDisposable.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of Dispose pattern.
        /// </summary>
        /// <param name="disposing">Has Dispose already been called.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.safeHandle?.Dispose();

                    // Free other state (managed objects).
                }

                // Free your own state (unmanaged objects).
                // Set large fields to null.
                this.disposed = true;
            }
        }
    }
}
