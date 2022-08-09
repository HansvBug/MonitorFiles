using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace MonitorFiles
{
    public class MfFormPosition : IDisposable
    {
        private readonly SafeHandle safeHandle = new SafeFileHandle(IntPtr.Zero, true); // Instantiate a SafeHandle instance.
        private bool disposed;  // Flag: Has Dispose already been called?

        private readonly FormMain? MainForm;
        private readonly FormConfigure? ConfigureForm;
        private dynamic? JsonObjSettings { get; set; }


        public MfFormPosition(FormMain MainForm)
        {
            this.MainForm = MainForm;
            this.JsonObjSettings = MainForm.JsonObjSettings;
        }

        public MfFormPosition(FormConfigure ConfigureForm)
        {
            this.ConfigureForm = ConfigureForm;
            this.JsonObjSettings = ConfigureForm.JsonObjSettings;
        }

        #region Helper
        private static bool IsVisibleOnAnyScreen(Rectangle rect)
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.IntersectsWith(rect))
                {
                    return true;
                }
            }

            return false;
        }
        #endregion Helper

        #region FormMain
        public void LoadMainFormPosition()
        {
            if (MfDebugMode.DebugMode)
            {
                MfLogging.WriteToLogInformation("Ophalen scherm positie hoofdscherm.");
            }

            // default
            this.MainForm.WindowState = FormWindowState.Normal;
            this.MainForm.StartPosition = FormStartPosition.WindowsDefaultBounds;


            if (this.JsonObjSettings != null)
            {
                if (this.JsonObjSettings.FormMain != null)
                {
                    Rectangle FrmRect = new()
                    {
                        X = this.JsonObjSettings.FormMain[0].FrmX,
                        Y = this.JsonObjSettings.FormMain[0].FrmY,
                        Width = this.JsonObjSettings.FormMain[0].FrmWidth,
                        Height = this.JsonObjSettings.FormMain[0].FrmHeight,
                    };

                    // check if the saved bounds are nonzero and visible on any screen
                    if (FrmRect != Rectangle.Empty && IsVisibleOnAnyScreen(FrmRect))
                    {   // first set the bounds
                        this.MainForm.StartPosition = FormStartPosition.Manual;
                        this.MainForm.DesktopBounds = FrmRect;

                        // afterwards set the window state to the saved value (which could be Maximized)
                        this.MainForm.WindowState = this.JsonObjSettings.FormMain[0].FrmWindowState;
                    }
                    else
                    {
                        // this resets the upper left corner of the window to windows standards
                        this.MainForm.StartPosition = FormStartPosition.WindowsDefaultLocation;

                        // we can still apply the saved size
                        if (FrmRect != Rectangle.Empty)
                        {
                            this.MainForm.Size = FrmRect.Size;
                        }
                    }
                }
            }
        }

        public void SaveMainFormPosition()
        {
            if (MfDebugMode.DebugMode)
            {
                MfLogging.WriteToLogInformation("Opslaan scherm positie hoofdscherm.");
            }

            string SettingsFile = string.Empty;
            if (this.JsonObjSettings != null)
            {
                SettingsFile = this.JsonObjSettings.AppParam[0].SettingsFileLocation;

                if (File.Exists(SettingsFile))
                {
                    if (this.MainForm != null && this.MainForm.WindowState == FormWindowState.Normal)
                    {
                        this.JsonObjSettings.FormMain[0].FrmWindowState = FormWindowState.Normal;

                        if (this.MainForm.Location.X >= 0)
                        {
                            this.JsonObjSettings.FormMain[0].FrmX = this.MainForm.Location.X;
                        }
                        else
                        {
                            this.JsonObjSettings.FormMain[0].FrmX = 0;
                        }

                        if (this.MainForm.Location.Y >= 0)
                        {
                            this.JsonObjSettings.FormMain[0].FrmY = this.MainForm.Location.Y;
                        }
                        else
                        {
                            this.JsonObjSettings.FormMain[0].FrmY = 0;
                        }

                        this.JsonObjSettings.FormMain[0].FrmHeight = this.MainForm.Height;
                        this.JsonObjSettings.FormMain[0].FrmWidth = this.MainForm.Width;
                    }
                    else
                    {
                        if (this.MainForm != null)
                        {
                            this.JsonObjSettings.FormMain[0].FrmWindowState = this.MainForm.WindowState;
                        }
                        else
                        {
                            this.JsonObjSettings.FormMain[0].FrmWindowState = 0;
                        }
                    }
                }
            }
            
        }

        #endregion FormMain

        #region FormConfigure
        public void LoadConfigureFormPosition()
        {
            if (MfDebugMode.DebugMode)
            {
                MfLogging.WriteToLogInformation("Ophalen scherm positie scherm Instellingen.");
            }

            this.ConfigureForm.WindowState = FormWindowState.Normal;
            this.ConfigureForm.StartPosition = FormStartPosition.WindowsDefaultBounds;

            if (this.JsonObjSettings != null && this.JsonObjSettings.FormConfigure != null)
            {
                Rectangle FrmRect = new()
                {
                    X = this.JsonObjSettings.FormConfigure[0].FrmX,
                    Y = this.JsonObjSettings.FormConfigure[0].FrmY,
                    Width = this.JsonObjSettings.FormConfigure[0].FrmWidth,
                    Height = this.JsonObjSettings.FormConfigure[0].FrmHeight,
                };

                if (FrmRect != Rectangle.Empty && IsVisibleOnAnyScreen(FrmRect))
                {
                    this.ConfigureForm.StartPosition = FormStartPosition.Manual;
                    this.ConfigureForm.DesktopBounds = FrmRect;

                    this.ConfigureForm.WindowState = this.JsonObjSettings.FormConfigure[0].FrmWindowState;
                }
                else
                {
                    this.ConfigureForm.StartPosition = FormStartPosition.WindowsDefaultLocation;

                    if (FrmRect != Rectangle.Empty)
                    {
                        this.ConfigureForm.Size = FrmRect.Size;
                    }
                }
            }
        }
        public void SaveConfigureFormPosition()
        {
            if (MfDebugMode.DebugMode)
            {
                MfLogging.WriteToLogInformation("Opslaan scherm positie scherm Instellingen.");
            }

            string SettingsFile = this.JsonObjSettings.AppParam[0].SettingsFileLocation;

            if (File.Exists(SettingsFile))
            {
                if (this.ConfigureForm.WindowState == FormWindowState.Normal)
                {
                    this.JsonObjSettings.FormConfigure[0].FrmWindowState = FormWindowState.Normal;

                    if (this.ConfigureForm.Location.X >= 0)
                    {
                        this.JsonObjSettings.FormConfigure[0].FrmX = this.ConfigureForm.Location.X;
                    }
                    else
                    {
                        this.JsonObjSettings.FormConfigure[0].FrmX = 0;
                    }

                    if (this.ConfigureForm.Location.Y >= 0)
                    {
                        this.JsonObjSettings.FormConfigure[0].FrmY = this.ConfigureForm.Location.Y;
                    }
                    else
                    {
                        this.JsonObjSettings.FormConfigure[0].FrmY = 0;
                    }

                    this.JsonObjSettings.FormConfigure[0].FrmHeight = this.ConfigureForm.Height;
                    this.JsonObjSettings.FormConfigure[0].FrmWidth = this.ConfigureForm.Width;
                }
                else
                {
                    this.JsonObjSettings.FormConfigure[0].FrmWindowState = this.ConfigureForm.WindowState;
                }
            }
        }
        #endregion FormConfigure

        #region dispose
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
        #endregion dispose
    }
}
