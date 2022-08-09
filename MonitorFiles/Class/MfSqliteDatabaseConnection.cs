using System.Data.SQLite;
using System.IO;

namespace MonitorFiles
{
    public class MfSqliteDatabaseConnection
    {
        public MfSqliteDatabaseConnection()
        {
            this.LoadSettings();
            if (this.JsonObjSettings != null)
            {
                if (!string.IsNullOrEmpty(this.JsonObjSettings.AppParam[0].DatabaseLocation))
                {
                    this.DbLocation = this.JsonObjSettings.AppParam[0].DatabaseLocation;
                    this.DatabaseFileName = Path.Combine(this.DbLocation, MfSettings.SqlLiteDatabaseName);
                    this.DbConnection = new SQLiteConnection("Data Source=" + this.DatabaseFileName);
                }
            }
        }

        /// <summary>
        /// Gets or sets the SQlite connection.
        /// </summary>
        protected SQLiteConnection DbConnection { get; set; }

        /// <summary>
        /// Gets or sets the name of the database file.
        /// </summary>
        protected string DatabaseFileName { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the name of the database file location.
        /// </summary>
        protected string DbLocation { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the appliction settings.
        /// </summary>
        private dynamic? JsonObjSettings { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdSqliteDatabaseConnection"/> class.
        /// Create the database connection.
        /// </summary>

        private void LoadSettings()
        {
            using MfSettingsManager set = new();
            set.LoadSettings();
            this.JsonObjSettings = set.JsonObjSettings;
        }
    }
}
