using System.Runtime.InteropServices;
using System.Text.Json;
using Microsoft.Win32.SafeHandles;


namespace MonitorFiles
{
    public class MfSettingsManager : IDisposable
    {
        private readonly SafeHandle safeHandle = new SafeFileHandle(IntPtr.Zero, true); // Instantiate a SafeHandle instance.
        private bool disposed; // Flag: Has Dispose already been called?

        public MfSettingsManager()
        {
            this.SettingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), MfSettings.ApplicationName, MfSettings.SettingsFolder, MfSettings.ConfigFile);    // ...\appdata\roaming\<application>\settings\...
        }

        public AppSettingsMeta? JsonObjSettings { get; set; }

        private string SettingsFile { get; set; }

        public static void CreateSettingsFile()
        {
            var getOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            string settingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), MfSettings.ApplicationName, MfSettings.SettingsFolder, MfSettings.ConfigFile);

            if (!File.Exists(settingsFile))
            {
                var appSettings = new AppSettingsMeta()
                {
                    AppParam = new List<AppParams>()
                    {
                        new AppParams()
                        {
                            ApplicationName = MfSettings.ApplicationName,
                            ApplicationVersion = MfSettings.ApplicationVersion,
                            ApplicationBuildDate = MfSettings.ApplicationBuildDate,
                            ActivateLogging = true,
                            AppendLogFile = true,
                            SettingsFileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), MfSettings.ApplicationName, MfSettings.SettingsFolder, MfSettings.ConfigFile),
                            LogFileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), MfSettings.ApplicationName, MfSettings.SettingsFolder),

                            // TODO; make it run un usb: DatabaseLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), SettingsDefault.ApplicationName, SettingsDefault.DatabaseFolder),
                            DatabaseLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, MfSettings.DatabaseFolder),  // Database will be stored next to de Program. Not in the roaming folder because everyone uses the same database
                            ResetAllAutoIncrementFields = true,
                            ItemTypeToShow = "All",
                            TopMost = false,
                        },
                    },
                    FormMain = new List<FormMainParams>()
                    {
                        new FormMainParams()
                        {
                            FrmX = 200,  //default = 200
                            FrmY = 100,
                            FrmHeight = 580,
                            FrmWidth = 815,
                            FrmWindowState = FormWindowState.Normal
                        }
                    },
                    FormConfigure = new List<FormConfigureParams>()
                    {
                        new FormConfigureParams()
                        {
                            FrmX = 20,
                            FrmY = 20,
                            FrmHeight = 490,
                            FrmWidth = 815,
                            FrmWindowState = FormWindowState.Normal,
                        }
                    },
                };

                string jsonString;
                jsonString = JsonSerializer.Serialize(appSettings, getOptions);

                File.WriteAllText(settingsFile, jsonString);
            }
        }

        /// <summary>
        /// Save the sttings to the settings file.
        /// </summary>
        /// <param name="jsonObjSettings">Object with all the current settings.</param>
        public static void SaveSettings(dynamic jsonObjSettings)
        {
            if (jsonObjSettings != null)
            {
                // Get settings location
                string fileLocation = jsonObjSettings.AppParam[0].SettingsFileLocation;

                if (string.IsNullOrEmpty(fileLocation))
                {
                    // Defaul location
                    fileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), MfSettings.ApplicationName, MfSettings.SettingsFolder, MfSettings.ConfigFile);
                }

                try
                {
                    var saveOptions = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                    };

                    string jsonString = JsonSerializer.Serialize(jsonObjSettings, saveOptions);

                    if (!string.IsNullOrEmpty(fileLocation) && !string.IsNullOrEmpty(jsonString))
                    {
                        File.WriteAllText(fileLocation, jsonString);
                    }
                }
                catch (AccessViolationException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Load the application settings.
        /// The fist time (else) the is no settings file. Default values will be used.
        /// </summary>
        public void LoadSettings()
        {
            if (File.Exists(this.SettingsFile))
            {
                string json = File.ReadAllText(this.SettingsFile);
                this.JsonObjSettings = JsonSerializer.Deserialize<AppSettingsMeta>(json);
            }
            else
            {
                if (this.JsonObjSettings != null)
                {
                    this.JsonObjSettings.AppParam[0].SettingsFileLocation = this.SettingsFile;
                }
            }
        }

        /// <summary>
        /// Allpication setttings meta class.
        /// </summary>
        public class AppSettingsMeta
        {
            /// <summary>
            /// Gets or sets a list with application settings.
            /// </summary>
            public List<AppParams>? AppParam { get; set; }
            public List<FormMainParams>? FormMain { get; set; }

            public List<FormConfigureParams>? FormConfigure { get; set; }
        }

        /// <summary>
        /// The application parameters/settings.
        /// </summary>
        public class AppParams
        {
            /// <summary>
            /// Gets or sets a value indicating whether logging will be activated.
            /// </summary>
            public bool ActivateLogging { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether the log file will be appended.
            /// </summary>
            public bool AppendLogFile { get; set; }

            /// <summary>
            /// Gets or sets the application name.
            /// </summary>
            public string? ApplicationName { get; set; }

            /// <summary>
            /// Gets or sets the version.
            /// </summary>
            public string? ApplicationVersion { get; set; }

            /// <summary>
            /// Gets or sets the application build date.
            /// </summary>
            public string? ApplicationBuildDate { get; set; }

            /// <summary>
            /// Gets or sets the settings file location.
            /// </summary>
            public string? SettingsFileLocation { get; set; }

            /// <summary>
            /// Gets or sets log file location.
            /// </summary>
            public string? LogFileLocation { get; set; }

            /// <summary>
            /// Gets or sets application database file location.
            /// </summary>
            public string? DatabaseLocation { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether all Increment fields will be restted. = Sequences reset.
            /// </summary>
            public bool ResetAllAutoIncrementFields { get; set; }

            /// <summary>
            /// Gets or sets ItemTypeToShow.  (Valid, faulted, does not exists)
            /// </summary>
            public string? ItemTypeToShow { get; set; }

            /// <summary>
            /// Get or sets Topmost property of the main Window.
            /// </summary>
            public bool TopMost { get; set; }
        }

        /// <summary>
        /// The Main Form parameters/settings.
        /// </summary>
        public class FormMainParams
        {
            //system.drawing.rectangle = 10; 10; 700; 500 ==> x, y, width, height
            public int FrmX { get; set; }
            public int FrmY { get; set; }
            public int FrmHeight { get; set; }
            public int FrmWidth { get; set; }
            public FormWindowState FrmWindowState { get; set; }
        }


        /// <summary>
        /// The Form Configure parameters/settings.
        /// </summary>
        public class FormConfigureParams
        {
            public int FrmX { get; set; }
            public int FrmY { get; set; }
            public int FrmHeight { get; set; }
            public int FrmWidth { get; set; }
            public FormWindowState FrmWindowState { get; set; }
        }

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
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.safeHandle?.Dispose();

                // Free any other managed objects here.
            }

            this.disposed = true;
        }
        #endregion dispose

    }
}
