using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace MonitorFiles
{
    public class MfProcessArguments : IDisposable
    {// Instantiate a SafeHandle instance.
        private readonly SafeHandle safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        private bool disposed;  // Flag: Has Dispose already been called?

        private List<string> cmdLineArg = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="PdProcessArguments"/> class.
        /// </summary>
        public MfProcessArguments()
        {
            this.GetArguments();
        }

        /// <summary>
        /// Gets or sets a list with arguments.
        /// </summary>
        public List<string> CmdLineArg
        {
            get { return this.cmdLineArg; }
            set { this.cmdLineArg = value; }
        }

        /// <summary>
        /// Gets or sets the Install argument.
        /// </summary>
        public string? ArgIntall { get; set; }

        /// <summary>
        /// Gets or sets the DebugMode argument.
        /// </summary>
        public string? ArgDebug { get; set; }

        #region Dispose
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
                    this.CmdLineArg.Clear();
                    this.ArgIntall = string.Empty;
                    this.ArgDebug = string.Empty;

                    // Free other state (managed objects).
                }

                // Free your own state (unmanaged objects).
                // Set large fields to null.
                this.disposed = true;
            }
        }

        private void GetArguments()
        {
            string[] args = Environment.GetCommandLineArgs();   // Store command line arguments

            foreach (string arg in args)
            {
                string argument = Convert.ToString(arg, CultureInfo.InvariantCulture);
                this.CmdLineArg.Add(argument);  // 0 = program name
                switch (argument)
                {
                    case "Install":
                        this.ArgIntall = "Install";
                        break;
                    case "DebugMode=On":
                        this.ArgDebug = "DebugMode=On";
                        break;
                }
            }
        }
        #endregion Dispose
    }
}
