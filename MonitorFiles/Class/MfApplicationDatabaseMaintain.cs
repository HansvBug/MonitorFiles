using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;


namespace MonitorFiles.Class
{
    public class MfApplicationDatabaseMaintain : MfSqliteDatabaseConnection, IDisposable
    {
        private readonly SafeHandle safeHandle = new SafeFileHandle(IntPtr.Zero, true); // Instantiate a SafeHandle instance.
        private bool disposed;  // Flag: Has Dispose already been called?      
        private bool WrongLogFilesExist { get; set; }
        private BindingSource bndSource = new();

        /// <summary>
        /// Get or set the bindingsource.
        /// </summary>
        public BindingSource BndSource
        {
            get { return this.bndSource; }
            set { this.bndSource = value; }
        }

        private DataTable dt = new();

        /// <summary>
        /// Gets or sets the selected data table.
        /// </summary>
        public DataTable Dt
        {
            get { return this.dt; }
            set
            {
                if (value != null)
                {
                    this.dt = value;
                }
            }
        }

        private SQLiteDataAdapter da = new();

        /// <summary>
        /// Gets or sets the DataAdapeter used for updating the selected table.
        /// </summary>
        public SQLiteDataAdapter Da
        {
            get { return this.da; }
            set { this.da = value; }
        }

        private readonly DataGridView dgv;

        public MfApplicationDatabaseMaintain()
        {
           //  default
        }

        public MfApplicationDatabaseMaintain(DataGridView dgv, BindingSource bndSource)
        {
            this.dgv = dgv;
            this.bndSource = bndSource;
        }

        public bool InsertIntoSourceTbl(string newSource)
        {            
            string insertSQL = string.Format("insert into {0} (GUID, SOURCE_NAME, CREATE_DATE) select @GUID, @SOURCE_NAME, @CREATE_DATE ", MfTableName.SOURCE);
            insertSQL += string.Format("where not exists (select SOURCE_NAME from {0} where SOURCE_NAME = @SOURCE_NAME)", MfTableName.SOURCE);

            if (MfDebugMode.DebugMode)
            {
                MfLogging.WriteToLogDebug(string.Format("Sql statement: {0}", insertSQL));
            }

            DbConnection.Open();
            using var tr = DbConnection.BeginTransaction();

            SQLiteCommand command = new(insertSQL, DbConnection);
            try
            {
                command.Parameters.Add(new SQLiteParameter("@GUID", Guid.NewGuid().ToString()));
                command.Parameters.Add(new SQLiteParameter("@SOURCE_NAME", newSource));
                command.Parameters.Add(new SQLiteParameter("@CREATE_DATE", DateTime.Now.ToString()));

                command.ExecuteNonQuery();
                MfLogging.WriteToLogInformation(string.Format("'{0}' is toegevoegd aan de tabel {1}.", newSource, MfTableName.SOURCE));
                tr.Commit();
                return true;
            }
            catch (SQLiteException ex)
            {
                MfLogging.WriteToLogError(string.Format("Het invoeren van de nieuwe source '{0}' in de tabel {1} is mislukt.", newSource, MfTableName.SOURCE));
                MfLogging.WriteToLogError(ex.Message);
                MessageBox.Show(string.Format("Het invoeren van de nieuwe source '{0}' in de tabel {1} is mislukt." + Environment.NewLine +
                    "Kijk in het log bestand voor de foutmelding."
                    , newSource, MfTableName.SOURCE)
                    , "Fout.", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (MfDebugMode.DebugMode)
                {
                    MfLogging.WriteToLogDebug(ex.ToString());
                }
                return false;
            }
            finally
            {
                command.Dispose();
                DbConnection.Close();
            }
        }

        public bool DeleteFromSourceTbl(string source)
        {
            DbConnection.Open();
            using var tr = DbConnection.BeginTransaction();
            string deleteSQL = string.Format("delete from {0} where SOURCE_NAME = @SOURCE_NAME", MfTableName.SOURCE);

            SQLiteCommand command = new(deleteSQL, DbConnection);
            try
            {
                command.Parameters.Add(new SQLiteParameter("@SOURCE_NAME", source));

                command.ExecuteNonQuery();
                MfLogging.WriteToLogInformation(string.Format("'{0}' is verwijderd uit de tabel {1}.", source, MfTableName.SOURCE));
                tr.Commit();
                return true;
            }
            catch (SQLiteException ex)
            {
                MfLogging.WriteToLogError(string.Format("Het verwijderen de source '{0}' uit de tabel {1} is mislukt.", source, MfTableName.SOURCE));
                MfLogging.WriteToLogError(ex.Message);
                MessageBox.Show(string.Format("Het verwijderen de source '{0}' uit de tabel {1} is mislukt." + Environment.NewLine +
                    "Kijk in het log bestand voor de foutmelding."
                    , source, MfTableName.SOURCE)
                    , "Fout.", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (MfDebugMode.DebugMode)
                {
                    MfLogging.WriteToLogDebug(ex.ToString());
                }
                return false;
            }
            finally
            {
                command.Dispose();
                DbConnection.Close();
            }
        }

        public bool InsertIntoTownshipTbl(string newTownship)
        {            
            string insertSQL = string.Format("insert into {0} (GUID, TOWNSHIP_NAME, CREATE_DATE) select @GUID, @TOWNSHIP_NAME, @CREATE_DATE ", MfTableName.TOWNSHIP);
            insertSQL += string.Format("where not exists (select TOWNSHIP_NAME from {0} where TOWNSHIP_NAME = @TOWNSHIP_NAME)", MfTableName.TOWNSHIP);

            if (MfDebugMode.DebugMode)
            {
                MfLogging.WriteToLogDebug(string.Format("Sql statement: {0}", insertSQL));
            }

            DbConnection.Open();
            using var tr = DbConnection.BeginTransaction();

            SQLiteCommand command = new(insertSQL, DbConnection);
            try
            {
                command.Parameters.Add(new SQLiteParameter("@GUID", Guid.NewGuid().ToString()));
                command.Parameters.Add(new SQLiteParameter("@TOWNSHIP_NAME", newTownship));
                command.Parameters.Add(new SQLiteParameter("@CREATE_DATE", DateTime.Now.ToString()));

                command.ExecuteNonQuery();
                MfLogging.WriteToLogInformation(string.Format("'{0}' is toegevoegd aan de tabel {1}.", newTownship, MfTableName.TOWNSHIP));
                tr.Commit();
                return true;
            }
            catch (SQLiteException ex)
            {
                MfLogging.WriteToLogError(string.Format("Het invoeren van de nieuwe gemeente '{0}' in de tabel {1} is mislukt.", newTownship, MfTableName.TOWNSHIP));
                MfLogging.WriteToLogError(ex.Message);
                MessageBox.Show(string.Format("Het invoeren van de nieuwe gemeente '{0}' in de tabel {1} is mislukt." + Environment.NewLine +
                    "Kijk in het log bestand voor de foutmelding."
                    , newTownship, MfTableName.TOWNSHIP)
                    , "Fout.", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (MfDebugMode.DebugMode)
                {
                    MfLogging.WriteToLogDebug(ex.ToString());
                }
                return false;
            }
            finally
            {
                command.Dispose();
                DbConnection.Close();
            }
        }

        public bool DeleteFromTownshipTbl(string township)
        {
            DbConnection.Open();
            using var tr = DbConnection.BeginTransaction();
            string deleteSQL = string.Format("delete from {0} where TOWNSHIP_NAME = @TOWNSHIP_NAME", MfTableName.TOWNSHIP);

            SQLiteCommand command = new(deleteSQL, DbConnection);
            try
            {
                command.Parameters.Add(new SQLiteParameter("@TOWNSHIP_NAME", township));

                command.ExecuteNonQuery();
                MfLogging.WriteToLogInformation(string.Format("'{0}' is verwijderd uit de tabel {1}.", township, MfTableName.TOWNSHIP));
                tr.Commit();
                return true;
            }
            catch (SQLiteException ex)
            {
                MfLogging.WriteToLogError(string.Format("Het verwijderen de source '{0}' uit de tabel {1} is mislukt.", township, MfTableName.TOWNSHIP));
                MfLogging.WriteToLogError(ex.Message);
                MessageBox.Show(string.Format("Het verwijderen de source '{0}' uit de tabel {1} is mislukt." + Environment.NewLine +
                    "Kijk in het log bestand voor de foutmelding."
                    , township, MfTableName.TOWNSHIP)
                    , "Fout.", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (MfDebugMode.DebugMode)
                {
                    MfLogging.WriteToLogDebug(ex.ToString());
                }
                return false;
            }
            finally
            {
                command.Dispose();
                DbConnection.Close();
            }
        }

        public bool InsertIntoFileTypeTbl(string newFileType)
        {
            string insertSQL = string.Format("insert into {0} (GUID, FILETYPE_NAME, CREATE_DATE) select @GUID, @FILETYPE_NAME, @CREATE_DATE ", MfTableName.FILETYPE);
            insertSQL += string.Format("where not exists (select FILETYPE_NAME from {0} where FILETYPE_NAME = @FILETYPE_NAME)", MfTableName.FILETYPE);

            if (MfDebugMode.DebugMode)
            {
                MfLogging.WriteToLogDebug(string.Format("Sql statement: {0}", insertSQL));
            }

            DbConnection.Open();
            using var tr = DbConnection.BeginTransaction();

            SQLiteCommand command = new(insertSQL, DbConnection);
            try
            {
                command.Parameters.Add(new SQLiteParameter("@GUID", Guid.NewGuid().ToString()));
                command.Parameters.Add(new SQLiteParameter("@FILETYPE_NAME", newFileType));
                command.Parameters.Add(new SQLiteParameter("@CREATE_DATE", DateTime.Now.ToString()));

                command.ExecuteNonQuery();
                MfLogging.WriteToLogInformation(string.Format("'{0}' is toegevoegd aan de tabel {1}.", newFileType, MfTableName.FILETYPE));
                tr.Commit();
                return true;
            }
            catch (SQLiteException ex)
            {
                MfLogging.WriteToLogError(string.Format("Het invoeren van een nieuw bestandtype '{0}' in de tabel {1} is mislukt.", newFileType, MfTableName.FILETYPE));
                MfLogging.WriteToLogError(ex.Message);
                MessageBox.Show(string.Format("Het invoeren van een nieuw bestandtype '{0}' in de tabel {1} is mislukt." + Environment.NewLine +
                    "Kijk in het log bestand voor de foutmelding."
                    , newFileType, MfTableName.FILETYPE)
                    , "Fout.", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (MfDebugMode.DebugMode)
                {
                    MfLogging.WriteToLogDebug(ex.ToString());
                }
                return false;
            }
            finally
            {
                command.Dispose();
                DbConnection.Close();
            }
        }

        public bool DeleteFromFileTypeTbl(string fileType)
        {
            DbConnection.Open();
            using var tr = DbConnection.BeginTransaction();
            string deleteSQL = string.Format("delete from {0} where FILETYPE_NAME = @FILETYPE_NAME", MfTableName.FILETYPE);

            SQLiteCommand command = new(deleteSQL, DbConnection);
            try
            {
                command.Parameters.Add(new SQLiteParameter("@FILETYPE_NAME", fileType));

                command.ExecuteNonQuery();
                MfLogging.WriteToLogInformation(string.Format("'{0}' is verwijderd uit de tabel {1}.", fileType, MfTableName.FILETYPE));
                tr.Commit();
                return true;
            }
            catch (SQLiteException ex)
            {
                MfLogging.WriteToLogError(string.Format("Het verwijderen de source '{0}' uit de tabel {1} is mislukt.", fileType, MfTableName.FILETYPE));
                MfLogging.WriteToLogError(ex.Message);
                MessageBox.Show(string.Format("Het verwijderen de source '{0}' uit de tabel {1} is mislukt." + Environment.NewLine +
                    "Kijk in het log bestand voor de foutmelding."
                    , fileType, MfTableName.TOWNSHIP)
                    , "Fout.", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (MfDebugMode.DebugMode)
                {
                    MfLogging.WriteToLogDebug(ex.ToString());
                }
                return false;
            }
            finally
            {
                command.Dispose();
                DbConnection.Close();
            }
        }

        public Dictionary<int, string> GetSourceNames()
        {
            Dictionary<int, string> attribItems = new Dictionary<int, string>();

            string selectSql = string.Format("select ID, SOURCE_NAME FROM {0} order by 1", MfTableName.SOURCE);

            DbConnection.Open();
            SQLiteCommand command = new(selectSql, this.DbConnection);
            try
            {    
                SQLiteDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (!string.IsNullOrEmpty(dr[0].ToString()))
                        {
                            attribItems.Add(int.Parse(dr[0].ToString() ?? string.Empty, CultureInfo.InvariantCulture), dr[1].ToString() ?? string.Empty);                            
                        }
                    }
                }

                dr.Close();
                return attribItems;
            }
            catch (SQLiteException ex)
            {
                MfLogging.WriteToLogError("Opvragen van de source namen is mislukt.");
                MfLogging.WriteToLogError("Melding :");
                MfLogging.WriteToLogError(ex.Message);
                if (MfDebugMode.DebugMode)
                {
                    MfLogging.WriteToLogDebug(ex.ToString());
                }
                return attribItems;
            }
            finally
            {
                command.Dispose();
                this.DbConnection.Close();                
            }
        }

        public Dictionary<int, string> GetTownshipNames()
        {
            Dictionary<int, string> attribItems = new Dictionary<int, string>();

            string selectSql = string.Format("select ID, TOWNSHIP_NAME FROM {0} order by 1", MfTableName.TOWNSHIP);

            DbConnection.Open();
            SQLiteCommand command = new(selectSql, this.DbConnection);
            try
            {
                SQLiteDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (!string.IsNullOrEmpty(dr[0].ToString()))
                        {
                            attribItems.Add(int.Parse(dr[0].ToString() ?? string.Empty), dr[1].ToString() ?? string.Empty);
                        }
                    }
                }

                dr.Close();
                return attribItems;
            }
            catch (SQLiteException ex)
            {
                MfLogging.WriteToLogError("Opvragen van de gemeente namen is mislukt.");
                MfLogging.WriteToLogError("Melding :");
                MfLogging.WriteToLogError(ex.Message);
                if (MfDebugMode.DebugMode)
                {
                    MfLogging.WriteToLogDebug(ex.ToString());
                }
                return attribItems;
            }
            finally
            {
                command.Dispose();
                this.DbConnection.Close();
            }
        }

        public Dictionary<int, string> GetFileTypeNames()
        {
            Dictionary<int, string> attribItems = new Dictionary<int, string>();

            string selectSql = string.Format("select ID, FILETYPE_NAME FROM {0} order by 1", MfTableName.FILETYPE);

            DbConnection.Open();
            SQLiteCommand command = new(selectSql, this.DbConnection);
            try
            {
                SQLiteDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (!string.IsNullOrEmpty(dr[0].ToString()))
                        {
                            attribItems.Add(int.Parse(dr[0].ToString() ?? string.Empty), dr[1].ToString() ?? string.Empty);
                        }
                    }
                }

                dr.Close();
                return attribItems;
            }
            catch (SQLiteException ex)
            {
                MfLogging.WriteToLogError("Opvragen van de bestand extensie namen is mislukt.");
                MfLogging.WriteToLogError("Melding :");
                MfLogging.WriteToLogError(ex.Message);
                if (MfDebugMode.DebugMode)
                {
                    MfLogging.WriteToLogDebug(ex.ToString());
                }
                return attribItems;
            }
            finally
            {
                command.Dispose();
                this.DbConnection.Close();
            }
        }

        public Dictionary<int, string> GetTopFolderItems()
        {
            Dictionary<int, string> attribItems = new Dictionary<int, string>();

            string selectSql = string.Format("select ID, TOPFOLDER_NAME FROM {0} order by 1", MfTableName.TOPFOLDER);

            DbConnection.Open();
            SQLiteCommand command = new(selectSql, this.DbConnection);
            try
            {
                SQLiteDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (!string.IsNullOrEmpty(dr[0].ToString()))
                        {
                            attribItems.Add(int.Parse(dr[0].ToString() ?? string.Empty), dr[1].ToString() ?? string.Empty);
                        }
                    }
                }

                dr.Close();
                return attribItems;
            }
            catch (SQLiteException ex)
            {
                MfLogging.WriteToLogError("Opvragen gegevens is mislukt.");
                MfLogging.WriteToLogError("Melding :");
                MfLogging.WriteToLogError(ex.Message);
                if (MfDebugMode.DebugMode)
                {
                    MfLogging.WriteToLogDebug(ex.ToString());
                }
                return attribItems;
            }
            finally
            {
                command.Dispose();
                this.DbConnection.Close();
            }
        }

        public Dictionary<int, string> GetFileOrFolder()
        {
            Dictionary<int, string> attribItems = new Dictionary<int, string>();

            string selectSql = string.Format("select ID, FILE_OR_FOLDER_NAME FROM {0} order by 1", MfTableName.FILEFOLDER);

            DbConnection.Open();
            SQLiteCommand command = new(selectSql, this.DbConnection);
            try
            {
                SQLiteDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (!string.IsNullOrEmpty(dr[0].ToString()))
                        {
                            attribItems.Add(int.Parse(dr[0].ToString() ?? string.Empty), dr[1].ToString() ?? string.Empty);
                        }
                    }
                }

                dr.Close();
                return attribItems;
            }
            catch (SQLiteException ex)
            {
                MfLogging.WriteToLogError("Opvragen van de File / folder naam is mislukt.");
                MfLogging.WriteToLogError("Melding :");
                MfLogging.WriteToLogError(ex.Message);
                if (MfDebugMode.DebugMode)
                {
                    MfLogging.WriteToLogDebug(ex.ToString());
                }
                return attribItems;
            }
            finally
            {
                command.Dispose();
                this.DbConnection.Close();
            }
        }

        public void InsertIntoItemTbl(MonitorItem Mi)
        {
            string insertSQL = string.Format("insert into {0} (GUID, FILE_OR_FOLDER_ID, FILE_NAME, FILETYPE_ID, FOLDER_NAME, DIFF_MAX, SOURCE_ID, TOWNSHIP_ID, TOPFOLDER_ID, FILE_ORDER, COMMENT, CREATE_DATE, CREATED_BY) ", MfTableName.ITEMS);
            insertSQL += "values (@GUID, @FILE_OR_FOLDER_ID, @FILE_NAME, @FILETYPE_ID, @FOLDER_NAME, @DIFF_MAX, @SOURCE_ID, @TOWNSHIP_ID, @TOPFOLDER_ID, @FILE_ORDER, @COMMENT, @CREATE_DATE, @CREATED_BY)";

            DbConnection.Open();
            using var tr = DbConnection.BeginTransaction();

            SQLiteCommand command = new(insertSQL, DbConnection);
            try
            {
                command.Parameters.Add(new SQLiteParameter("@GUID", Guid.NewGuid().ToString()));
                command.Parameters.Add(new SQLiteParameter("@FILE_OR_FOLDER_ID", Mi.FileOrFolder_id));
                command.Parameters.Add(new SQLiteParameter("@FILE_NAME", Mi.FileName));
                command.Parameters.Add(new SQLiteParameter("@FOLDER_NAME", Mi.FolderName));
                command.Parameters.Add(new SQLiteParameter("@FILETYPE_ID", Mi.FileType_id));
                command.Parameters.Add(new SQLiteParameter("@DIFF_MAX", Mi.MaxDiffDays));
                command.Parameters.Add(new SQLiteParameter("@SOURCE_ID", Mi.Source_id));
                command.Parameters.Add(new SQLiteParameter("@TOPFOLDER_ID", Mi.TopFolder_id));               
                command.Parameters.Add(new SQLiteParameter("@TOWNSHIP_ID", Mi.Township_id));
                command.Parameters.Add(new SQLiteParameter("@FILE_ORDER", Mi.Order));
                command.Parameters.Add(new SQLiteParameter("@COMMENT", Mi.Comment));
                command.Parameters.Add(new SQLiteParameter("@CREATE_DATE", DateTime.Now.ToString()));
                command.Parameters.Add(new SQLiteParameter("@CREATED_BY", Environment.UserName));

                command.ExecuteNonQuery();
                MfLogging.WriteToLogInformation(string.Format("Een nieuw te monitoren item is toegevoegd aan de tabel {0}.", MfTableName.ITEMS));
                tr.Commit();
            }
            catch (SQLiteException ex)
            {
                MfLogging.WriteToLogError(string.Format("Het invoeren van een nieuw Item in de tabel {)} is mislukt.", MfTableName.ITEMS));
                MfLogging.WriteToLogError(ex.Message);
                MessageBox.Show(string.Format("Het invoeren van het nieuwe item in de tabel {1} is mislukt." + Environment.NewLine +
                    "Kijk in het log bestand voor de foutmelding."
                    , MfTableName.ITEMS)
                    , "Fout.", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (MfDebugMode.DebugMode)
                {
                    MfLogging.WriteToLogDebug(ex.ToString());
                }
            }
            finally
            {
                command.Dispose();
                DbConnection.Close();
            }
        }

        public void GetAllItemsToMaintain()
        {
            string selectSql = "select ID, GUID, FILE_OR_FOLDER_ID, FILETYPE_ID, FILE_NAME as Log_bestand, FOLDER_NAME as Locatie, ";
            selectSql += "SOURCE_ID, TOWNSHIP_ID, TOPFOLDER_ID, ";            
            selectSql += "DIFF_MAX as Max_verschil_in_dagen, ";
            selectSql += "FILE_ORDER as Volgorde, COMMENT as Opmerking, ";
            selectSql += "CREATE_DATE as Datum_aangemaakt, MODIFY_DATE as Datum_gewijzigd, ";
            selectSql += "CREATED_BY as Aangemaakt_door, MODIFIED_BY as Gewijzigd_door ";
            selectSql += string.Format("from {0}", MfTableName.ITEMS);

            // Create a new data adapter based on the specified query.
            this.DbConnection.Open();            

            try
            {
                this.Da = new SQLiteDataAdapter(selectSql, this.DbConnection);

                SQLiteCommandBuilder commandBuilder = new(this.Da);

                // Populate a new data table and bind it to the BindingSource.
                this.Dt = new DataTable
                {
                    Locale = CultureInfo.InvariantCulture,
                    TableName = MfTableName.ITEMS,
                };
                this.Da.Fill(this.Dt);

                this.BndSource.DataSource = this.Dt.DefaultView;  // DefaultView is needed for filtering the data
                this.dgv.DataSource = this.BndSource;

                Dictionary<DataGridViewComboBoxColumn, string> cmbAndColumnName = new();

                cmbAndColumnName.Add(this.CreateDgvComboBox(string.Format("SELECT ID, SOURCE_NAME FROM {0} where SOURCE_NAME is not null", MfTableName.SOURCE), "SOURCE_ID"), "SOURCE_ID");
                cmbAndColumnName.Add(this.CreateDgvComboBox(string.Format("SELECT ID, TOWNSHIP_NAME FROM {0} where TOWNSHIP_NAME is not null", MfTableName.TOWNSHIP), "TOWNSHIP_ID"), "TOWNSHIP_ID");
                cmbAndColumnName.Add(this.CreateDgvComboBox(string.Format("SELECT ID, TOPFOLDER_NAME FROM {0} where TOPFOLDER_NAME is not null", MfTableName.TOPFOLDER), "TOPFOLDER_ID"), "TOPFOLDER_ID");
                cmbAndColumnName.Add(this.CreateDgvComboBox(string.Format("SELECT ID, FILE_OR_FOLDER_NAME FROM {0}", MfTableName.FILEFOLDER), "FILE_OR_FOLDER_ID"), "FILE_OR_FOLDER_ID");
                cmbAndColumnName.Add(this.CreateDgvComboBox(string.Format("SELECT ID, FILETYPE_NAME FROM {0}", MfTableName.FILETYPE), "FILETYPE_ID"), "FILETYPE_ID");

                // add combobox
                int index = 0;

                foreach (KeyValuePair<DataGridViewComboBoxColumn, string> pair in cmbAndColumnName)
                {
                    foreach (DataColumn column in this.Dt.Columns)
                    {
                        if (!string.IsNullOrEmpty(pair.Value))
                        {
                            if (column.ColumnName.Equals(pair.Value))
                            {
                                index = column.Ordinal;

                                this.dgv.Columns[pair.Value].Visible = false;  // Do not show the column <...>_ID
                                this.dgv.Columns.Insert(index, pair.Key);
                            }
                        }
                    }
                }
            }
            catch (FormatException ex)
            {
                MfLogging.WriteToLogError(string.Format("Fout bij het ophalen van alle gegevens van de tabel : {0}.", MfTableName.ITEMS));
                MfLogging.WriteToLogError(ex.Message);
                MfLogging.WriteToLogError(ex.ToString());
                //return null;
            }
            catch (SQLiteException ex)
            {
                MfLogging.WriteToLogError(string.Format("Fout bij het ophalen van alle gegevens van de tabel : {0}.", MfTableName.ITEMS));
                MfLogging.WriteToLogError(ex.Message);
                MfLogging.WriteToLogError(ex.ToString());
                //return null;
            }
            finally
            {                
                this.DbConnection.Close();
            }
        }

        private DataGridViewComboBoxColumn CreateDgvComboBox(string query, string columnName)
        {
            var ordeAdapter = new SQLiteDataAdapter(query, this.DbConnection);
            DataTable dt = new();

            ordeAdapter.Fill(dt);

            DataGridViewComboBoxColumn newComboBoxColumn = new();
            if (!string.IsNullOrEmpty(query))
            {
                newComboBoxColumn.Name = columnName;
                newComboBoxColumn.HeaderText = columnName.Replace("_ID", "_NAME");

                newComboBoxColumn.DataSource = dt;
                newComboBoxColumn.DataPropertyName = columnName;
                newComboBoxColumn.DisplayMember = columnName.Replace("_ID", "_NAME");
                newComboBoxColumn.ValueMember = "ID";
                newComboBoxColumn.ValueType = typeof(int);

                newComboBoxColumn.AutoComplete = true;
                newComboBoxColumn.MaxDropDownItems = 8;

                newComboBoxColumn.FlatStyle = FlatStyle.Flat;
                newComboBoxColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;  // You see only a combobox when editing

                // add new empty combobox item
                DataRow newRow = dt.NewRow();
                newRow[columnName.Replace("_ID", "_NAME")] = " ";
                newRow["ID"] = DBNull.Value;
                dt.Rows.Add(newRow);
            }
            else
            {
                newComboBoxColumn.DataSource = dt;
                newComboBoxColumn.Name = columnName;
                newComboBoxColumn.HeaderText = "Bestand_of_map";
                newComboBoxColumn.AutoComplete = true;
                newComboBoxColumn.MaxDropDownItems = 2;
                newComboBoxColumn.FlatStyle = FlatStyle.Flat;
                newComboBoxColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                newComboBoxColumn.Items.Add("Bestand");
                newComboBoxColumn.Items.Add("Folder");
            }

            return newComboBoxColumn;
        }

        public MfItemsData GetAllItemsToMonitor()
        {
            string selectSql = "select ID, GUID, FILE_OR_FOLDER_NAME as Bestand_of_map, FILE_NAME as Log_bestand, FOLDER_NAME as Locatie, ";
            selectSql += "SOURCE_NAME as Bron, TOWNSHIP_NAME as Gemeente, FILETYPE_NAME as bestandsextensie, ";
            selectSql += "TOPFOLDER_NAME as Alleen_huidige_map, ";
            selectSql += "DIFF_MAX as Max_verschil_in_dagen, ";
            selectSql += "FILE_ORDER as Volgorde, COMMENT as Opmerking ";
            // selectSql += "CREATE_DATE as Datum_aangemaakt, MODIFY_DATE as Datum_gewijzigd, ";
            // selectSql += "CREATED_BY as Aangemaakt_door, MODIFIED_BY as Gewijzigd_door ";
            selectSql += string.Format("from {0}", MfTableName.VW_ITEMS);

            
            MfItemsData itemsData = new();

            DbConnection.Open();
            SQLiteCommand command = new(selectSql, this.DbConnection);
            try
            {
                SQLiteDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        using MfItemData itemData = new();
                        itemData.ID = int.Parse(dr[0].ToString() ?? string.Empty, CultureInfo.InvariantCulture);
                        itemData.GUID = dr[1].ToString() ?? string.Empty;

                        if (dr[2] != DBNull.Value)
                        {
                            itemData.FILE_OR_FOLDER_NAME = dr[2].ToString() ?? string.Empty;
                        }
                        else
                        {
                            itemData.FILE_OR_FOLDER_NAME = String.Empty;
                        }

                        if (dr[3] != DBNull.Value)
                        {
                            itemData.FILE_NAME = dr[3].ToString() ?? string.Empty;
                        }
                        else
                        {
                            itemData.FILE_NAME = String.Empty;
                        }

                        if (dr[4] != DBNull.Value)
                        {
                            itemData.FOLDER_NAME = dr[4].ToString() ?? string.Empty;
                        }
                        else
                        {
                            itemData.FOLDER_NAME = String.Empty;
                        }

                        if (dr[5] != DBNull.Value)
                        {
                            itemData.SOURCE_NAME = dr[5].ToString() ?? string.Empty;
                        }
                        else
                        {
                            itemData.SOURCE_NAME = String.Empty;
                        }

                        if(dr[6] != DBNull.Value)
                        {
                            itemData.TONWSHIP_NAME = dr[6].ToString() ?? string.Empty;
                        }
                        else
                        {
                            itemData.TONWSHIP_NAME = String.Empty;
                        }

                        if(dr[7] != DBNull.Value)
                        {
                            itemData.FILETYPE_NAME = dr[7].ToString() ?? string.Empty;
                        }
                        else
                        {
                            itemData.FILETYPE_NAME = String.Empty;
                        }

                        if (dr[8] != DBNull.Value)
                        {
                            itemData.TOPFOLDER_NAME = dr[8].ToString() ?? string.Empty;
                        }
                        else
                        {
                            itemData.TOPFOLDER_NAME = String.Empty;
                        }


                        if (dr[9] != DBNull.Value)
                        {
                            itemData.DIFF_MAX = int.Parse(dr[9].ToString() ?? string.Empty, CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            itemData.DIFF_MAX = -1;
;
                        }

                        if (dr[10] != DBNull.Value)
                        {
                            itemData.FILE_ORDER = int.Parse(dr[10].ToString() ?? string.Empty, CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            itemData.FILE_ORDER = -1;
                        }
                       
                        // Check how old the file or files in the foldeer are
                        if (itemData.FILE_OR_FOLDER_NAME == "Bestand")
                        {
                            itemsData.Items.Add(CheckTheFile(itemData));
                        }
                        else if (itemData.FILE_OR_FOLDER_NAME == "Map")
                        {
                            itemsData.Items.Add(CheckTheFolder(itemData));
                        }                        
                    }                    
                }

                dr.Close();
                return itemsData;
            }
            
            catch (FormatException ex)
            {
                MfLogging.WriteToLogError(string.Format("Fout bij het ophalen van alle gegevens van de tabel : {0}.", MfTableName.ITEMS));
                MfLogging.WriteToLogError(ex.Message);
                MfLogging.WriteToLogError(ex.ToString());

                return itemsData;
            }
            catch (SQLiteException ex)
            {
                MfLogging.WriteToLogError(string.Format("Fout bij het ophalen van alle gegevens van de tabel : {0}.", MfTableName.ITEMS));
                MfLogging.WriteToLogError(ex.Message);
                MfLogging.WriteToLogError(ex.ToString());
                return itemsData;
            }
            finally
            {
                this.DbConnection.Close();                
            }
        }

        private MfItemData CheckTheFile(MfItemData itemData)
        {
            if (!string.IsNullOrEmpty(itemData.FOLDER_NAME))
            {
                DateTime fileCreationDate;
                DateTime fileModification;
                DateTime dateModification;
                DateTime currentDateTime = DateTime.UtcNow.Date;
                int daysDifference;
                string curFile = string.Empty;

                if (!string.IsNullOrEmpty(itemData.FILE_NAME))
                {
                    curFile = Path.Combine(itemData.FOLDER_NAME, itemData.FILE_NAME);

                    if (File.Exists(curFile))
                    {
                        if (itemData.FILE_OR_FOLDER_NAME == "Bestand")
                        {
                            fileCreationDate = GetFileCreationDate(curFile);
                        }

                        fileModification = File.GetLastWriteTime(curFile);
                        dateModification = fileModification.Date;
                        daysDifference = (int)currentDateTime.Subtract(dateModification).TotalDays;

                        itemData.fileModificationDate = fileModification;
                        itemData.daysDifference = daysDifference;

                        if (itemData.DIFF_MAX != -1)
                        {
                            if (daysDifference > itemData.DIFF_MAX)
                            {
                                itemData.FileStatus = "Fout";
                            }
                            else if (daysDifference <= itemData.DIFF_MAX)
                            {
                                itemData.FileStatus = "Goed";
                            }
                        }
                        else
                        {
                            itemData.FileStatus = string.Empty;
                        }
                    }
                    else
                    {
                        itemData.FileStatus = "Bestand bestaat niet (meer).";
                    }
                }
                else
                {
                    MfLogging.WriteToLogWarning("Item bevat geen bestandsnaam.");
                }
            }
            else
            {
                MfLogging.WriteToLogWarning("Item bevat geen map naam.");
            }

            return itemData;
        }

        private MfItemData CheckTheFolder(MfItemData itemData)
        {
            if (!string.IsNullOrEmpty(itemData.FOLDER_NAME))
            {
                string searchfolder;
                searchfolder = itemData.FOLDER_NAME;

                if (!string.IsNullOrEmpty(itemData.FILETYPE_NAME))
                {
                    DateTime fileModification;
                    DateTime dateModification;
                    DateTime currentDateTime = DateTime.UtcNow.Date;
                    int daysDifference;

                    string FileType = "*." + itemData.FILETYPE_NAME;
                    string[] allfiles;

                    if (itemData.TOPFOLDER_ID == 1)  // Ja
                    {
                        allfiles = Directory.GetFiles(searchfolder, FileType, SearchOption.TopDirectoryOnly);
                    }
                    else if (itemData.TOPFOLDER_ID == 2)  // Nee
                    {
                        allfiles = Directory.GetFiles(searchfolder, FileType, SearchOption.AllDirectories);
                    }
                    else
                    {
                        allfiles = Directory.GetFiles(searchfolder, FileType, SearchOption.TopDirectoryOnly);
                    }

                    itemData.fileModificationDate = DateTime.Now;
                    foreach (string f in allfiles)
                    {
                        if (itemData.DIFF_MAX != -1)
                        {
                            fileModification = File.GetLastWriteTime(f);
                            dateModification = fileModification.Date;
                            daysDifference = (int)currentDateTime.Subtract(dateModification).TotalDays;

                            if (daysDifference >= itemData.daysDifference)
                            {
                                itemData.daysDifference = daysDifference;
                            }                            

                            if (fileModification < itemData.fileModificationDate)
                            {
                                itemData.fileModificationDate = fileModification;
                            }

                            if (itemData.daysDifference > itemData.DIFF_MAX)
                            {
                                itemData.FileStatus = "Fout";                                
                            }
                            else if (itemData.daysDifference <= itemData.DIFF_MAX && (itemData.FileStatus != "Fout" || itemData.FileStatus != string.Empty))
                            {
                                itemData.FileStatus = "Goed";
                            }
                        }
                        else
                        {
                            itemData.FileStatus = string.Empty;
                        }
                    }                    
                }
            }
            
            return itemData;
        }
        private DateTime GetFileCreationDate(string file)
        {
            DateTime FileCreation;
            return FileCreation = File.GetCreationTime(file);
        }

        public void Compress()
        {
            // First make a copy
            if (this.CopyDatabaseFile())
            {
                this.DbConnection.Close();

                this.DbConnection.Open();
                SQLiteCommand command = new(this.DbConnection);
                command.Prepare();
                command.CommandText = "vacuum;";

                try
                {
                    command.ExecuteNonQuery();
                    MfLogging.WriteToLogInformation("De database is succesvol gecomprimeerd.");
                }
                catch (SQLiteException ex)
                {
                    MfLogging.WriteToLogError("Het comprimeren van de database is mislukt.");
                    MfLogging.WriteToLogError("Melding :");
                    MfLogging.WriteToLogError(ex.Message);
                    if (MfLogging.DebugMode)
                    {
                        MfLogging.WriteToLogDebug(ex.ToString());
                    }
                }
                finally
                {
                    command.Dispose();
                    this.DbConnection.Close();
                }
            }
        }

        private bool CopyDatabaseFile()
        {
            MfLogging.WriteToLogInformation("Maak eerst een kopie van de applicatie database voordat deze wordt gecomprimeerd.");
            bool result = false;

            if (string.IsNullOrEmpty(this.DatabaseFileName) || string.IsNullOrEmpty(Path.GetDirectoryName(this.DatabaseFileName)))
            {
                if (string.IsNullOrEmpty(this.DatabaseFileName))
                {
                    MfLogging.WriteToLogError("Database bestandsnaam ontbreekt.");
                }
                else if (string.IsNullOrEmpty(Path.GetDirectoryName(this.DatabaseFileName)))
                {
                    MfLogging.WriteToLogError("Pad naar Database bestand ontbreekt.");
                }

                return result;
            }

            MfLogging.WriteToLogInformation("Maak eerst een kopie van de applicatie database voordat deze wordt gecomprimeerd...");
            string fileToCopy = this.DatabaseFileName;
            DateTime dateTime = DateTime.UtcNow.Date;
            string currentDate = dateTime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            string backUpPath = Path.Combine(Path.GetDirectoryName(this.DatabaseFileName), MfSettings.BackUpFolder);
            string newLocation = backUpPath + currentDate + "_" + MfSettings.SqlLiteDatabaseName;

            if (Directory.Exists(backUpPath))
            {
                if (File.Exists(fileToCopy))
                {
                    if (!File.Exists(newLocation))
                    {
                        File.Copy(fileToCopy, newLocation, false);  // Overwrite file = false
                        MfLogging.WriteToLogInformation("Het kopiëren van het bestand '" + MfSettings.SqlLiteDatabaseName + "' is gereed.");
                        result = true;
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Het bestand bestaat reeds. Wilt u het bestand overschrijven?", "Waarschuwing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            File.Copy(fileToCopy, newLocation, true);  // overwrite file = true
                            MfLogging.WriteToLogInformation("Het kopiëren van het bestand '" + MfSettings.SqlLiteDatabaseName + "' is gereed.");
                            result = true;
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            MfLogging.WriteToLogInformation("Het kopiëren van het bestand '" + MfSettings.SqlLiteDatabaseName + "' is afgebroken.");
                            MfLogging.WriteToLogInformation("Het bestand komt reeds voor.");
                            result = false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("De map '{0}' is niet aanwezig.", MfSettings.BackUpFolder), "Fout.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result = false;
                }
            }

            return result;
        }


        #region dispose
        /// <summary>
        /// Implement IDisposable.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of Dispose pattern.
        /// </summary>
        /// <param name="disposing">Has Dispose already been called.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    safeHandle?.Dispose();

                    // Free other state (managed objects).
                }

                // Free your own state (unmanaged objects).
                // Set large fields to null.
                disposed = true;
            }
        }
        #endregion dispose
    }
}