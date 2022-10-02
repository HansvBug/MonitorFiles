using System.Data;
using System.Data.SQLite;
using System.Globalization;

namespace MonitorFiles
{
    public class MfApplicationDatabaseCreate : MfSqliteDatabaseConnection
    {
        private int latestDbVersion;
        private bool TablesExist;

        public MfApplicationDatabaseCreate()
        {
            this.Error = false;
            this.latestDbVersion = MfSettings.DatabaseVersion;
            TablesExist = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether an Error has occurred.
        /// </summary>
        private bool Error { get; set; }

        private readonly string creTblSettingsMeta = string.Format("CREATE TABLE IF NOT EXISTS {0} (", MfTableName.SETTINGS_META) +
                    "ID                 INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE ," +
                    "GUID               VARCHAR(50)          ," +
                    "KEY                VARCHAR(50)  UNIQUE  ," +
                    "VALUE              VARCHAR(255))";

        private readonly string creTblItems = string.Format("CREATE TABLE IF NOT EXISTS {0} (", MfTableName.ITEMS) +
                    "ID                 INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE   ," +
                    "GUID               VARCHAR(50)                                         ," +
                    "FILE_OR_FOLDER_ID  INTEGER                                             ," +
                    "FILE_NAME          VARCHAR(200)                                        ," +
                    "FOLDER_NAME        VARCHAR(10000)                                      ," +
                    "DIFF_MAX           INTEGER                                             ," +
                    "SOURCE_ID          INTEGER                                             ," +
                    "TOWNSHIP_ID        INTEGER                                             ," +
                    "FILE_ORDER         INTEGER                                             ," +
                    "FILETYPE_ID       INTEGER                                             ," +
                    "COMMENT            VARCHAR(500)                                        ," +
                    "TOPFOLDER_ID       INTEGER                                             ," +
                    "FILE_TYPE          VARCHAR(10)                                         ," +
                    "CREATE_DATE        VARCHAR(10)                                         ," +
                    "MODIFY_DATE        VARCHAR(10)                                         , " +
                    "CREATED_BY         VARCHAR(50)                                         , " +
                    "MODIFIED_BY        VARCHAR(50)                                         , " +
                    string.Format("FOREIGN KEY (SOURCE_ID) REFERENCES {0}(ID) ", MfTableName.SOURCE) +
                    string.Format("FOREIGN KEY (TOWNSHIP_ID) REFERENCES {0}(ID) ", MfTableName.TOWNSHIP) +
                                  "ON UPDATE RESTRICT " +
                                  "ON DELETE RESTRICT )";

        private readonly string creTblSource = string.Format("CREATE TABLE IF NOT EXISTS {0} (", MfTableName.SOURCE) +
                    "ID                 INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE   ," +
                    "GUID               VARCHAR(50)  UNIQUE                                 ," +
                    "SOURCE_NAME        VARCHAR(100)                                        ," +
                    "CREATE_DATE        VARCHAR(10)                                         ," +
                    "MODIFY_DATE        VARCHAR(10)                                         )";

        private readonly string createTblSourceIndex = string.Format("CREATE UNIQUE INDEX IF NOT EXISTS {0} ON {1}(ID)", MfTableName.SOURCE_ID_IDX, MfTableName.SOURCE);

        private readonly string creTblTownship = string.Format("CREATE TABLE IF NOT EXISTS {0} (", MfTableName.TOWNSHIP) +
                    "ID                 INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE   ," +
                    "GUID               VARCHAR(50)  UNIQUE                                 ," +
                    "TOWNSHIP_NAME      VARCHAR(50)                                         ," +
                    "CREATE_DATE        VARCHAR(10)                                         ," +
                    "MODIFY_DATE        VARCHAR(10)                                         )";

        private readonly string createTblTownshipIndex = string.Format("CREATE UNIQUE INDEX IF NOT EXISTS {0} ON {1}(ID)", MfTableName.TOWNSHIP_ID_IDX, MfTableName.TOWNSHIP);

        private readonly string creTblFileFolder = string.Format("CREATE TABLE IF NOT EXISTS {0} (", MfTableName.FILEFOLDER) +
                    "ID                     INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE   ," +
                    "GUID                   VARCHAR(50)  UNIQUE                                 ," +
                    "FILE_OR_FOLDER_NAME    VARCHAR(50)                                         ," +
                    "CREATE_DATE            VARCHAR(10)                                         ," +
                    "MODIFY_DATE            VARCHAR(10)                                         )";

        private readonly string creTblFileType = string.Format("CREATE TABLE IF NOT EXISTS {0} (", MfTableName.FILETYPE) +
                    "ID                 INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE   ," +
                    "GUID               VARCHAR(50)  UNIQUE                                 ," +
                    "FILETYPE_NAME      VARCHAR(100)                                        ," +
                    "CREATE_DATE        VARCHAR(10)                                         ," +
                    "MODIFY_DATE        VARCHAR(10)                                         )";

        private readonly string createTblFileTypeIndex = string.Format("CREATE UNIQUE INDEX IF NOT EXISTS {0} ON {1}(ID)", MfTableName.FILETYPE_ID_IDX, MfTableName.FILETYPE);

        private readonly string creTblTopFolder = string.Format("CREATE TABLE IF NOT EXISTS {0} (", MfTableName.TOPFOLDER) +
                    "ID                 INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE   ," +
                    "GUID               VARCHAR(50)  UNIQUE                                 ," +
                    "TOPFOLDER_NAME     VARCHAR(100)                                        ," +
                    "CREATE_DATE        VARCHAR(10)                                         ," +
                    "MODIFY_DATE        VARCHAR(10)                                         )";

        private readonly string createTblTopFolderIndex = string.Format("CREATE UNIQUE INDEX IF NOT EXISTS {0} ON {1}(ID)", MfTableName.TOPFOLDER_ID_IDX, MfTableName.TOPFOLDER);

        private readonly string creVwItems = string.Format("create view if not exists {0} as ", MfTableName.VW_ITEMS) +
                    "select i.ID, " +
                    "i.GUID, " +
                    "f.FILE_OR_FOLDER_NAME, " +
                    "i.FILE_NAME, " +
                    "i.FOLDER_NAME, " +
                    "s.SOURCE_NAME, " + 
                    "t.TOWNSHIP_NAME, " +
                    "i.DIFF_MAX, " +
                    "i.FILE_ORDER, " +
                    "ft.FILETYPE_NAME, " +
                    "tf.TOPFOLDER_NAME, " +
                    "i.COMMENT, " +
                    "i.CREATE_DATE, " +
                    "i.MODIFY_DATE, " +
                    "i.CREATED_BY, " +
                    "i.MODIFIED_BY " +
                    "from " +
                    "ITEMS i " +                                                //TODO; string.format()
                    "left join FILEFOLDER f on i.FILE_OR_FOLDER_ID = f.ID " +   //TODO; string.format()
                    "left join SOURCE s on i.SOURCE_ID = s.ID " +               //TODO; string.format()
                    "left join TOWNSHIP t on i.TOWNSHIP_ID = t.ID " +           //TODO; string.format()
                    "left join FILETYPE ft on i.FILETYPE_ID = ft.ID " +        //TODO; string.format()
                    "left join TOPFOLDER tf on i.TOPFOLDER_ID = tf.ID";

        /// <summary>
        /// Create the database file and the tables.
        /// </summary>
        public bool CreateDatabase()
        {
            string version = "1";
            
            if (this.latestDbVersion >= 1 && this.SelectMeta() == 0)
            {
                this.CreDatabaseFile();
                version = "1";

                this.CreateTable(this.creTblSettingsMeta, MfTableName.SETTINGS_META, version);
                this.CreateTable(this.creTblItems, MfTableName.ITEMS, version);
                this.CreateTable(this.creTblSource, MfTableName.SOURCE, version);
                this.CreateTable(this.creTblTownship, MfTableName.TOWNSHIP, version);
                this.CreateTable(this.creTblFileFolder, MfTableName.FILEFOLDER, version);
                this.CreateTable(this.creTblFileType, MfTableName.FILETYPE, version);                
                this.CreateTable(this.creTblTopFolder, MfTableName.TOPFOLDER, version);
                
                this.CreateIndex(this.createTblSourceIndex, MfTableName.SOURCE_ID_IDX, version);
                this.CreateIndex(this.createTblTownshipIndex, MfTableName.TOWNSHIP_ID_IDX, version);
                this.CreateIndex(this.createTblFileTypeIndex, MfTableName.FILETYPE_ID_IDX, version);
                this.CreateIndex(this.createTblTopFolderIndex, MfTableName.TOPFOLDER_ID_IDX, version);                

                this.CreateTable(this.creVwItems, MfTableName.VW_ITEMS, version); // Create view

                this.InsertFileFolder(version);
                this.InsertTopDirOnly(version);
                this.InsertEmptyRow(MfTableName.TOWNSHIP, "TOWNSHIP_NAME", version);
                this.InsertEmptyRow(MfTableName.SOURCE, "SOURCE_NAME", version);
                this.InsertEmptyRow(MfTableName.FILETYPE, "FILETYPE_NAME", version);
                this.InsertEmptyRow(MfTableName.TOPFOLDER, "TOPFOLDER_NAME", version);

                this.TablesExist = false;
                this.InsertMeta(version);  // Set the version 1
            }
            else if (this.latestDbVersion >= 2 && this.SelectMeta() == 1)
            {
                version = "2";

                // new...

                this.TablesExist = false;
                this.UpdateMeta(version);  // Set the version 2
            }

            this.DbConnection.Close();
            this.DbConnection.Dispose();

            this.ErrorMessage();
            if (!this.Error && this.TablesExist)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CreateTable(string sqlCreateString, string tableName, string version)
        {
            if (!this.Error)
            {
                SQLiteCommand command = new(sqlCreateString, this.DbConnection);
                try
                {
                    if (this.DbConnection != null && this.DbConnection.State == ConnectionState.Closed)
                    {
                        this.DbConnection.Open();
                    }

                    command.ExecuteNonQuery();
                    MfLogging.WriteToLogInformation(string.Format("De tabel {0} is aangemaakt. (Versie {1}).", tableName, version));
                }
                catch (SQLiteException ex)
                {
                    MfLogging.WriteToLogError(string.Format("Aanmaken van de tabel {0} is mislukt. (Versie {1}).", tableName, version));
                    MfLogging.WriteToLogError("Melding :");
                    MfLogging.WriteToLogError(ex.Message);
                    if (MfDebugMode.DebugMode)
                    {
                        MfLogging.WriteToLogDebug(ex.ToString());
                    }

                    this.Error = true;
                }
                finally
                {
                    command.Dispose();
                }
            }
            else
            {
                MfLogging.WriteToLogError(string.Format("Het aanmaken van de tabel {0} is niet uitgevoerd.", tableName));
            }
        }

        private void CreateIndex(string sqlCreIndexString, string indexName, string version)
        {
            if (!this.Error)
            {
                if (this.DbConnection != null && this.DbConnection.State == ConnectionState.Closed)
                {
                    this.DbConnection.Open();
                }

                SQLiteCommand command = new(sqlCreIndexString, this.DbConnection);
                try
                {
                    command.ExecuteNonQuery();
                    MfLogging.WriteToLogInformation(string.Format("De index {0} is aangemaakt. (Versie {1}).", indexName, version));
                }
                catch (SQLiteException ex)
                {
                    MfLogging.WriteToLogError(string.Format("Aanmaken van de index {0} is mislukt. (Versie {1}).", indexName, version));
                    MfLogging.WriteToLogError("Melding :");
                    MfLogging.WriteToLogError(ex.Message);
                    if (MfLogging.DebugMode)
                    {
                        MfLogging.WriteToLogDebug(ex.ToString());
                    }

                    this.Error = true;
                }
                finally
                {
                    command.Dispose();
                }
            }
            else
            {
                MfLogging.WriteToLogError(string.Format("Het aanmaken van de index {0} is niet uitgevoerd.", indexName));
            }
        }

        private void CreDatabaseFile()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (!string.IsNullOrEmpty(this.DatabaseFileName))
            {
                try
                {
                    // Only with a first install. (Unless a user removed the database file)
                    if (!File.Exists(this.DatabaseFileName))
                    {
                        SQLiteConnection.CreateFile(this.DatabaseFileName); // The creation of a new empty database file.

                        MfLogging.WriteToLogInformation("De database '" + this.DatabaseFileName + "' is aangemaakt.");
                    }
                    else
                    {
                        MfLogging.WriteToLogInformation("Het database bestand is aanwezig, er is géén nieuw leeg database bestand aangemaakt.");
                    }
                }
                catch (IOException ex)
                {
                    this.Error = true;

                    MfLogging.WriteToLogError(string.Format("De applicatie database is niet aangemaakt. Een IOException heeft opgetreden. ({0}).", this.DatabaseFileName));
                    MfLogging.WriteToLogError("Melding :");
                    MfLogging.WriteToLogError(ex.Message);
                    if (MfDebugMode.DebugMode)
                    {
                        MfLogging.WriteToLogDebug(ex.ToString());
                    }

                    MessageBox.Show("Onverwachte fout bij het aanmaken van een leeg database bestand. (IOException)", "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    this.Error = true;

                    MfLogging.WriteToLogError("De applicatie database is niet aangemaakt. (" + this.DatabaseFileName + ").");
                    MfLogging.WriteToLogError("Melding :");
                    MfLogging.WriteToLogError(ex.Message);
                    if (MfDebugMode.DebugMode)
                    {
                        MfLogging.WriteToLogDebug(ex.ToString());
                    }

                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Onverwachte fout bij het aanmaken van een leeg database bestand.", "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MfLogging.WriteToLogError("De SQlite database is niet aangemaakt omdat er geen locatie of database naam is opgegeven.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Bestandlocatie ontbreekt.", "Fout.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Get the database version.
        /// </summary>
        /// <returns>The database version number.</returns>
        public int SelectMeta() // Made public so you can check the version on every application start
        {
            int sqlLiteMetaVersion = 0;

            // First check if the table exists. (The first time when de database file is created, the table does noet exists).
            if (this.GetTableName(MfTableName.SETTINGS_META))
            {
                string selectSql = string.Format("SELECT VALUE FROM {0} WHERE KEY = 'VERSION'", MfTableName.SETTINGS_META);

                MfLogging.WriteToLogInformation("Controle op versie van de applicatie database.");

                SQLiteCommand command = new(selectSql, this.DbConnection);
                try
                {
                    SQLiteDataReader dr = command.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        if (!string.IsNullOrEmpty(dr[0].ToString()))
                        {
                            sqlLiteMetaVersion = int.Parse(dr[0].ToString() ?? string.Empty, CultureInfo.InvariantCulture);  // ?? = assign a nullable type to a non-nullable type
                        }
                        else
                        {
                            sqlLiteMetaVersion = -1;
                            MfLogging.WriteToLogError("Fout bij ophalen database versie. return versie: -1");
                            MessageBox.Show("Fout bij ophalen database versie.", "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    dr.Close();
                }
                catch (SQLiteException ex)
                {
                    MfLogging.WriteToLogError("Opvragen meta versie is mislukt. (Versie " + Convert.ToString(this.latestDbVersion, CultureInfo.InvariantCulture) + ").");
                    MfLogging.WriteToLogError("Melding :");
                    MfLogging.WriteToLogError(ex.Message);
                    if (MfDebugMode.DebugMode)
                    {
                        MfLogging.WriteToLogDebug(ex.ToString());
                    }

                    this.Error = true;
                }
                finally
                {
                    command.Dispose();
                    this.DbConnection.Close();
                }
            }

            return sqlLiteMetaVersion;
        }

        private bool GetTableName(string tblName)
        {
            string selectSql = string.Format("select NAME from sqlite_master where type = 'table' and NAME = '{0}' order by NAME", tblName);

            if (this.DbConnection != null)
            {
                if (this.DbConnection != null && this.DbConnection.State == ConnectionState.Closed)
                {
                    try
                    {
                        this.DbConnection.Open();
                    }
                    catch (SQLiteException ex)
                    {
                        MfLogging.WriteToLogError("Melding :");
                        MfLogging.WriteToLogError(ex.Message);
                        return false;
                    }
                }


                SQLiteCommand command = new(selectSql, this.DbConnection);
                try
                {
                    SQLiteDataReader dr = command.ExecuteReader();
                    dr.Read();

                    if (dr.HasRows)
                    {
                        // Returns only one row... so no while reader read
                        if (dr.GetValue(0).ToString() == tblName)
                        {
                            dr.Close();
                            return true;
                        }
                    }

                    return false;
                }
                catch (SQLiteException ex)
                {
                    MfLogging.WriteToLogError(string.Format("Opvragen tablenaam is mislukt. Betreft tabelnaam: {0}", MfTableName.SETTINGS_META));
                    MfLogging.WriteToLogError("Melding :");
                    MfLogging.WriteToLogError(ex.Message);
                    if (MfDebugMode.DebugMode)
                    {
                        MfLogging.WriteToLogDebug(ex.ToString());
                    }

                    return false;
                }
                finally
                {
                    command.Dispose();
                }
            }
            else
            {
                return false;
            }

        }

        private void InsertMeta(string version)
        {
            if (!this.Error)
            {
                string insertSQL = string.Format("INSERT INTO {0} (GUID, KEY, VALUE) VALUES(@GUID, @KEY, @VERSION)", MfTableName.SETTINGS_META);

                SQLiteCommand command = new(insertSQL, this.DbConnection);
                try
                {
                    command.Parameters.Add(new SQLiteParameter("@GUID", Guid.NewGuid().ToString()));
                    command.Parameters.Add(new SQLiteParameter("@KEY", "VERSION"));
                    command.Parameters.Add(new SQLiteParameter("@VERSION", version));

                    command.ExecuteNonQuery();
                    MfLogging.WriteToLogInformation(string.Format("De tabel {0} is gewijzigd. (Versie ", MfTableName.SETTINGS_META) + version + ").");
                }
                catch (SQLiteException ex)
                {
                    MfLogging.WriteToLogError(string.Format("Het invoeren van het database versienummer in de tabel {0} is mislukt. (Versie " + Convert.ToString(this.latestDbVersion, CultureInfo.InvariantCulture) + ").", MfTableName.SETTINGS_META));
                    MfLogging.WriteToLogError(ex.Message);
                    if (MfDebugMode.DebugMode)
                    {
                        MfLogging.WriteToLogDebug(ex.ToString());
                    }

                    this.Error = true;
                }
                finally
                {
                    command.Dispose();
                    this.DbConnection.Close();
                }
            }
            else
            {
                MfLogging.WriteToLogError(string.Format("Het invoeren van het database versienummer in de tabel {0} is mislukt.", MfTableName.SETTINGS_META));
            }
        }

        private void UpdateMeta(string version)
        {
            using var tr = this.DbConnection.BeginTransaction();
            string updateSQL = string.Format("UPDATE {0} SET VALUE  = @VERSION WHERE KEY = @KEY", MfTableName.SETTINGS_META);

            SQLiteCommand command = new(updateSQL, this.DbConnection);
            try
            {
                command.Parameters.Add(new SQLiteParameter("@VERSION", version));
                command.Parameters.Add(new SQLiteParameter("@KEY", "VERSION"));

                command.ExecuteNonQuery();
                MfLogging.WriteToLogInformation(string.Format("De tabel {0} is gewijzigd. (Versie " + version + ").", MfTableName.SETTINGS_META));
                command.Dispose();
                tr.Commit();
            }
            catch (SQLiteException ex)
            {
                MfLogging.WriteToLogError(string.Format("het wijzigen van de versie in tabel {0} is mislukt. (Versie " + version + ").", MfTableName.SETTINGS_META));
                MfLogging.WriteToLogError(ex.Message);
                if (MfDebugMode.DebugMode)
                {
                    MfLogging.WriteToLogDebug(ex.ToString());
                }

                command.Dispose();
                tr.Rollback();
            }
        }

        private void InsertFileFolder(string version)
        {
            if (!this.Error)
            {
                string insertSql = string.Format("INSERT INTO {0} (GUID, FILE_OR_FOLDER_NAME, CREATE_DATE) ", MfTableName.FILEFOLDER);
                insertSql += "VALUES(@GUID, @FILE_OR_FOLDER_NAME, @CREATE_DATE)";

                short counter = 1;

                SQLiteCommand command = new(insertSql, this.DbConnection);
                try
                {
                    command.Parameters.Add(new SQLiteParameter("@GUID", Guid.NewGuid().ToString()));
                    command.Parameters.Add(new SQLiteParameter("@FILE_OR_FOLDER_NAME", "Bestand"));
                    command.Parameters.Add(new SQLiteParameter("@CREATE_DATE", DateTime.Now.ToString()));

                    command.ExecuteNonQuery();
                    MfLogging.WriteToLogInformation(string.Format("'Map' is toegevoegd aan de tabel {0} . (Versie {1}).", MfTableName.FILEFOLDER, version));

                    counter++;

                    command.Parameters.Add(new SQLiteParameter("@GUID", Guid.NewGuid().ToString()));
                    command.Parameters.Add(new SQLiteParameter("@FILE_OR_FOLDER_NAME", "Map"));
                    command.Parameters.Add(new SQLiteParameter("@CREATE_DATE", DateTime.Now.ToString()));

                    command.ExecuteNonQuery();
                    MfLogging.WriteToLogInformation(string.Format("'Bestand' is toegevoegd aan de tabel {0} . (Versie {1}).", MfTableName.FILEFOLDER, version));


                }
                catch (SQLiteException ex)
                {
                    MfLogging.WriteToLogError(string.Format("Het invoeren een waarde in de tabel {0} is mislukt.", MfTableName.FILEFOLDER));
                    MfLogging.WriteToLogError(ex.Message);
                    if (MfDebugMode.DebugMode)
                    {
                        MfLogging.WriteToLogDebug(ex.ToString());
                    }

                    this.Error = true;
                }
                finally
                {
                    command.Dispose();
                }
            }
            else
            {
                MfLogging.WriteToLogError(string.Format("Het invoeren een waarde in de tabel {0} is niet gestart.", MfTableName.FILEFOLDER));
            }
        }

        private void InsertTopDirOnly(string version)
        {
            if (!this.Error)
            {
                string insertSql = string.Format("INSERT INTO {0} (GUID, TOPFOLDER_NAME, CREATE_DATE) ", MfTableName.TOPFOLDER);
                insertSql += "VALUES(@GUID, @TOPFOLDER_NAME, @CREATE_DATE)";

                short counter = 1;

                SQLiteCommand command = new(insertSql, this.DbConnection);
                try
                {
                    command.Parameters.Add(new SQLiteParameter("@GUID", Guid.NewGuid().ToString()));
                    command.Parameters.Add(new SQLiteParameter("@TOPFOLDER_NAME", "Ja"));
                    command.Parameters.Add(new SQLiteParameter("@CREATE_DATE", DateTime.Now.ToString()));

                    command.ExecuteNonQuery();
                    MfLogging.WriteToLogInformation(string.Format("'Ja' is toegevoegd aan de tabel {0} . (Versie {1}).", MfTableName.TOPFOLDER, version));

                    counter++;

                    command.Parameters.Add(new SQLiteParameter("@GUID", Guid.NewGuid().ToString()));
                    command.Parameters.Add(new SQLiteParameter("@TOPFOLDER_NAME", "Nee"));
                    command.Parameters.Add(new SQLiteParameter("@CREATE_DATE", DateTime.Now.ToString()));

                    command.ExecuteNonQuery();
                    MfLogging.WriteToLogInformation(string.Format("'Nee' is toegevoegd aan de tabel {0} . (Versie {1}).", MfTableName.TOPFOLDER, version));


                }
                catch (SQLiteException ex)
                {
                    MfLogging.WriteToLogError(string.Format("Het invoeren een waarde in de tabel {0} is mislukt.", MfTableName.TOPFOLDER));
                    MfLogging.WriteToLogError(ex.Message);
                    if (MfDebugMode.DebugMode)
                    {
                        MfLogging.WriteToLogDebug(ex.ToString());
                    }

                    this.Error = true;
                }
                finally
                {
                    command.Dispose();
                }
            }
            else
            {
                MfLogging.WriteToLogError(string.Format("Het invoeren een waarde in de tabel {0} is niet gestart.", MfTableName.TOPFOLDER));
            }
        }

        private void InsertEmptyRow(string tableName, string columnName, string version)
        {
            if (!this.Error)
            {
                string insertSql = string.Format("INSERT INTO {0} (GUID, {1}, CREATE_DATE) ", tableName, columnName);
                insertSql += string.Format("VALUES(@GUID, @{0}, @CREATE_DATE)", columnName);
                                
                SQLiteCommand command = new(insertSql, this.DbConnection);
                try
                {
                    command.Parameters.Add(new SQLiteParameter("@GUID", Guid.NewGuid().ToString()));                    
                    command.Parameters.Add(new SQLiteParameter(string.Format("@{0}", columnName), null));
                    command.Parameters.Add(new SQLiteParameter("@CREATE_DATE", DateTime.Now.ToString()));

                    command.ExecuteNonQuery();
                    MfLogging.WriteToLogInformation(string.Format("een lege regel is toegevoegd aan de tabel {0} . (Versie {1}).", tableName, version));
                }
                catch (SQLiteException ex)
                {
                    MfLogging.WriteToLogError(string.Format("Het invoeren van een lege regel in de tabel {0} is mislukt. (Versie " + Convert.ToString(this.latestDbVersion, CultureInfo.InvariantCulture) + ").", tableName));
                    MfLogging.WriteToLogError(ex.Message);
                    if (MfDebugMode.DebugMode)
                    {
                        MfLogging.WriteToLogDebug(ex.ToString());
                    }

                    this.Error = true;
                }
                finally
                {
                    command.Dispose();
                }
            }
            else
            {
                MfLogging.WriteToLogError(string.Format("Het invoeren van het database versienummer in de tabel {0} is mislukt.", MfTableName.SETTINGS_META));
            }
        }
        private void ErrorMessage()
        {
            if (!this.Error)
            {
                if (!this.TablesExist)
                {
                    MessageBox.Show("De database is aangemaakt.", "Informatie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(
                    "Het database bestand of één van de tabellen is niet aangemaakt." + Environment.NewLine +
                    Environment.NewLine +
                    "Controleer het log bestand.",
                    "Fout",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}
