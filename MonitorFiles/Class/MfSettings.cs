using System.Globalization;

namespace MonitorFiles
{
    public static class MfSettings
    {
        public const string SystemMenu = "2022";  // For the systemmenu line

        /// <summary>
        /// The name of the config file.
        /// </summary>
        public const string ConfigFile = "MonitorFiles.cfg";

        /// <summary>
        /// The name of the log file.
        /// </summary>
        public const string LogFileName = "MonitorFiles.log";

        /// <summary>
        /// Copyright.
        /// </summary>
        public const string Copyright = "2022";  // Started in 2022

        /// <summary>
        /// The name of the application database.
        /// </summary>
        public const string SqlLiteDatabaseName = "MonitorFilesDatabase.db";

        /// <summary>
        /// Database version number.
        /// </summary>
        public const int DatabaseVersion = 1;  // Start with 1

        /// <summary>
        /// Settings folder name.
        /// </summary>
        public const string SettingsFolder = "Settings\\";

        /// <summary>
        /// Database folder name.
        /// </summary>
        public const string DatabaseFolder = "Database\\";

        /// <summary>
        /// Back-up folder.
        /// </summary>
        public const string BackUpFolder = "Backup\\";

        /// <summary>
        /// The name of the application.
        /// </summary>
        public const string ApplicationName = "Monitor Files";

        /// <summary>
        /// Main Form Text.
        /// </summary>
        public const string ApplicationNameShow = "Monitor Files";  // Show in form

        /// <summary>
        /// Application version.
        /// </summary>
        public const string ApplicationVersion = "0.0.0.1";

        /// <summary>
        /// gets the application build date.
        /// </summary>
        public static string ApplicationBuildDate
        {
            get { return DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture); }
        }
    }
}
