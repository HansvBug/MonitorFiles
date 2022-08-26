using MonitorFiles.Class;
using System;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MonitorFiles
{
    public partial class FormMain : Form
    {
        #region Initialize
        public FormMain()
        {
            InitializeComponent();

            bindingSourceItemsMaintain = new();
            bindingSourceItemsMonitor = new();
            dtMonitorItems = new();

            this.Text = MfSettings.ApplicationName;
            this.SetStatusLabelMain = "Welkom.";
            CheckAppEnvironment();      // Check if the application path exists. If not then it will be created.
            CreateSettingsFile();       // Create a settings file with default values. (If the file does not exists).
            this.GetSettings();         // Get the settings.
            GetDebugMode();             // DebugMode is a static class.
            this.StartLogging();
            this.LoadFormPosition();    // Load the last saved form position.            
            InitializeApp();            

            bindingSourceDgvModify = new();
            changeTableAdded = new();
            changeTableModified = new();
            this.changeTableDeleted = new();
            bndMonitorItems = new();
        }

        private BindingSource bindingSourceDgvModify;

        /// <summary>
        /// Gets or sets the application settings. Holds the user and application setttings.
        /// </summary>
        public dynamic? JsonObjSettings { get; set; }
        private bool NoMessageBoxArgument { get; set; } = false; // Prevent message boxes when the tool is activated via the cmd line
        private bool CloseArgument { get; set; } = false;

        private enum ApplicationAccess
        {
            None,
            Minimal,
            Full,
            RowsRemoved,
            CanNotSave,
            CellValueChanged,
            AddRecord,
            Saved,
        }

        private BindingSource bindingSourceItemsMaintain;
        private BindingSource bindingSourceItemsMonitor;

        private MfApplicationDatabaseMaintain tblMaintenance;  // wordt nog niet gebruikt !!!

        private DataTable? changeTableDeleted;
        private DataTable? changeTableAdded;
        private DataTable? changeTableModified;
        private DataTable dtMonitorItems;
        private BindingSource bndMonitorItems;

        private void GetSettings()
        {
            try
            {
                using MfSettingsManager setMan = new();
                setMan.LoadSettings();

                if (setMan.JsonObjSettings != null && setMan.JsonObjSettings.AppParam != null)
                {
                    this.JsonObjSettings = setMan.JsonObjSettings;
                }
                else
                {
                    MessageBox.Show("Het settingsbestand is niet gevonden.", "Waarschuwing.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (AccessViolationException aex)
            {
                // Logging is not available here
                MessageBox.Show(
                    "Fout bij het laden van de instellingen. " + Environment.NewLine +
                    "De default instellingen worden ingeladen.",
                    "Waarschuwing.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                MessageBox.Show(
                    string.Format("Fout: {0}", aex.Message) + Environment.NewLine + Environment.NewLine +
                    string.Format("Fout: {0}", aex.ToString()),
                    "Fout.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void GetDebugMode()
        {
            using MfProcessArguments getArg = new();
            foreach (string arg in getArg.CmdLineArg)
            {
                string argument = Convert.ToString(arg, CultureInfo.InvariantCulture);

                if (argument == getArg.ArgDebug)
                {
                    MfDebugMode.DebugMode = true;
                }

                if (argument == getArg.CloseArgument)
                {
                    CloseArgument = true;
                }

                if (argument == getArg.NoMessageBoxArgument)
                {
                    this.NoMessageBoxArgument = true;
                }
            }
        }

        private void StartLogging()
        {
            MfLogging.NameLogFile = MfSettings.LogFileName;
            MfLogging.LogFolder = this.JsonObjSettings.AppParam[0].LogFileLocation;
            MfLogging.AppendLogFile = this.JsonObjSettings.AppParam[0].AppendLogFile;
            MfLogging.ActivateLogging = this.JsonObjSettings.AppParam[0].ActivateLogging;

            MfLogging.ApplicationName = MfSettings.ApplicationName;
            MfLogging.ApplicationVersion = MfSettings.ApplicationVersion;
            MfLogging.ApplicationBuildDate = MfSettings.ApplicationBuildDate;
            MfLogging.Parent = this;

            if (MfDebugMode.DebugMode)
            {
                MfLogging.DebugMode = true;
            }

            if (!MfLogging.StartLogging())
            {
                MfLogging.WriteToFile = false;  // Stop the logging
                MfLogging.ActivateLogging = false;
                this.JsonObjSettings.AppParam[0].ActivateLogging = false;
                this.JsonObjSettings.AppParam[0].AppendLogFile = false;
            }
            else
            {
                if (MfDebugMode.DebugMode)
                {
                    MfLogging.WriteToLogDebug(string.Empty);
                    MfLogging.WriteToLogDebug("DebugMode = ON.");
                    MfLogging.WriteToLogDebug(string.Empty);
                }
            }
        }

        private void LoadFormPosition()
        {
            using MfFormPosition frmPosition = new(this);
            frmPosition.LoadMainFormPosition();
        }

        private void InitializeApp()
        {
            this.EnableFunctions(ApplicationAccess.Full.ToString());

            RadioButtonAddSource.Checked = true;
            RadioButtonAddTownship.Checked = true;
            ButtonNewSave.Enabled = false;
            this.CellValueChanged = false;

            SetColumnNames();

            string ItemsToShow = this.JsonObjSettings.AppParam[0].ItemTypeToShow;

            if (ItemsToShow == "All")
            {
                this.OptionsToolStripMenuItemShowAllItems.Checked = true;
                this.OptionsToolStripMenuItemShowFaultedItems.Checked = false;
                this.OptionsToolStripMenuItemShowFileIsGoneItems.Checked = false;
                this.OptionsToolStripMenuItemShowValidItems.Checked = false;
            }
            else if (ItemsToShow == "Faulted")
            {
                this.OptionsToolStripMenuItemShowAllItems.Checked = false;
                this.OptionsToolStripMenuItemShowFaultedItems.Checked = true;
                this.OptionsToolStripMenuItemShowFileIsGoneItems.Checked = false;
                this.OptionsToolStripMenuItemShowValidItems.Checked = false;
            }
            else if (ItemsToShow  == "Gone")
            {
                this.OptionsToolStripMenuItemShowAllItems.Checked = false;
                this.OptionsToolStripMenuItemShowFaultedItems.Checked = false;
                this.OptionsToolStripMenuItemShowFileIsGoneItems.Checked = true;
                this.OptionsToolStripMenuItemShowValidItems.Checked = false;
            }
            else if (ItemsToShow  == "Valid")
            {
                this.OptionsToolStripMenuItemShowAllItems.Checked = false;
                this.OptionsToolStripMenuItemShowFaultedItems.Checked = false;
                this.OptionsToolStripMenuItemShowFileIsGoneItems.Checked = false;
                this.OptionsToolStripMenuItemShowValidItems.Checked = true;
            }
        }

        #endregion Initialize


        private bool CellValueChanged { get; set; }

        /// <summary>
        /// Sets the ToolStripStatusLabelMain text.
        /// </summary>
        public string SetStatusLabelMain
        {
            set
            {
                this.ToolStripStatusLabelMain.Text = value;
                this.Refresh();
            }
        }
        
        private int CurpageIndex { get; set; } = -1;
        private static void CheckAppEnvironment() // Checks if the application path exists. If not then it will be created.
        {
            using MfAppEnvironment checkPath = new();
            MfAppEnvironment.CreateFolder(MfSettings.ApplicationName, true);
            MfAppEnvironment.CreateFolder(MfSettings.ApplicationName + @"\" + MfSettings.SettingsFolder, true);
            MfAppEnvironment.CreateFolder(MfSettings.DatabaseFolder, false);
            MfAppEnvironment.CreateFolder(MfSettings.DatabaseFolder + @"\" + MfSettings.BackUpFolder, false);
        }

        private static void CreateSettingsFile() // Create a settings file with default values. (If the file does not exists)
        {
            MfSettingsManager.CreateSettingsFile();
        }

        private void ProgramToolStripMenuItemClose_Click(object sender, EventArgs e)
        {
            if (this.CellValueChanged)
            {
                MessageBox.Show("Niet opgeslagen wijzigingen gevonden. Kies eerst Opslaan.", "Waarschuwing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.ButtonModifySave;
            }
            else
            {
                this.Close();
            }
        }

        private void TextBoxNewMaxDiffDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #region form close
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.CellValueChanged)
            {
                MessageBox.Show("Niet opgeslagen wijzigingen gevonden. Kies eerst 'Opslaan'." + Environment.NewLine +
                                "Of maak de wijzigingen ongedaan door op 'Opnieuw' te klikken.",
                                "Waarschuwing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = this.ButtonModifySave;
                e.Cancel = this.CellValueChanged;
            }
            else
            {
                this.SaveFormPosition();
                this.SaveSettings();
                MfLogging.StopLogging();
            }
        }

        private void SaveFormPosition()
        {
            using MfFormPosition frmPosition = new(this);
            frmPosition.SaveMainFormPosition();
        }

        private void SaveSettings()
        {
            MfSettingsManager.SaveSettings(this.JsonObjSettings);
        }
        #endregion form close

        #region form load
        private void FormMain_Load(object sender, EventArgs e)
        {
            this.CreateDatabase();   // Create the application database file with tables.

            if (this.CheckDatabaseFileExists())
            {
                this.EnableFunctions(ApplicationAccess.Full.ToString());
                GetComboboxItems(true); // get all source names and township names as items for the comboboxes
                LoadFile();  // Get all records from the Items table
            }
            else
            {
                this.EnableFunctions(ApplicationAccess.Minimal.ToString());
            }
        }

        private void CreateDatabase()
        {
            using MfProcessArguments getArg = new();
            foreach (string arg in getArg.CmdLineArg)
            {
                string argument = Convert.ToString(arg, CultureInfo.InvariantCulture);

                if (argument == getArg.ArgIntall)
                {
                    MfApplicationDatabaseCreate createAppDb = new();
                    if (createAppDb.CreateDatabase())
                    {
                        this.EnableFunctions(ApplicationAccess.Full.ToString());
                    }
                    else
                    {
                        this.EnableFunctions(ApplicationAccess.Minimal.ToString());
                    }
                }
            }
        }

        private bool CheckDatabaseFileExists()
        {
            string dbLocation = this.JsonObjSettings.AppParam[0].DatabaseLocation;
            string databaseFileName = Path.Combine(dbLocation, MfSettings.SqlLiteDatabaseName);

            if (File.Exists(databaseFileName))
            {
                return true;
            }
            else
            {
                MfLogging.WriteToLogWarning("De applicatie database is niet gevonden. De meeste functionaliteit wordt uitgeschakeld.");
                MessageBox.Show(
                    "De applicatie database is niet gevonden." + Environment.NewLine +
                    "De meeste functionaliteit wordt uitgeschakeld.",
                    "Informatie.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;
            }
        }

        private void GetComboboxItems(bool allComboboxes)
        {
            using MfApplicationDatabaseMaintain getAttributes = new();
            Dictionary<int, string> cbItems = new();

            // New Source
            ComboBoxNewSource.DataSource = null;
            ComboBoxNewSource.Items.Clear();
            ComboBoxOptionsSource.Items.Clear();

            cbItems = getAttributes.GetSourceNames();

            ComboBoxNewSource.DataSource = new BindingSource(cbItems, null);
            foreach (KeyValuePair<int, string> kvp in cbItems)
            {                
                ComboBoxNewSource.DisplayMember = "Value";
                ComboBoxNewSource.ValueMember = "Key";
                
                ComboBoxOptionsSource.DisplayMember = "Value";
                ComboBoxOptionsSource.ValueMember = "Key";
            }

            // Township
            ComboBoxNewTownship.DataSource = null;
            ComboBoxNewTownship.Items.Clear();
            ComboBoxOptionsTownship.Items.Clear();

            cbItems.Clear();
            cbItems = getAttributes.GetTownshipNames();
            ComboBoxNewTownship.DataSource = new BindingSource(cbItems, null);
            foreach (KeyValuePair<int, string> kvp in cbItems)
            {
                ComboBoxNewTownship.DisplayMember = "Value";
                ComboBoxNewTownship.ValueMember = "Key";

                ComboBoxOptionsTownship.DisplayMember = "Value";
                ComboBoxOptionsTownship.ValueMember = "Key";
            }

            //File or Folder
            if (allComboboxes)
            {
                ComboBoxNewFileOrFolder.DataSource = null;
                ComboBoxNewFileOrFolder.Items.Clear();

                cbItems.Clear();
                cbItems = getAttributes.GetFileOrFolder();
                ComboBoxNewFileOrFolder.DataSource = new BindingSource(cbItems, null);
                foreach (KeyValuePair<int, string> kvp in cbItems)
                {
                    ComboBoxNewFileOrFolder.DisplayMember = "Value";
                    ComboBoxNewFileOrFolder.ValueMember = "Key";
                }
                ComboBoxNewFileOrFolder.SelectedIndex = 0;
            }
        }

        private void EnableFunctions(string ApplicationAccess)
        {
            switch (ApplicationAccess)
            {
                case "Start_Application":
                    //
                    break;
                case "Full":
                    //
                    break;
                case "Minimal":
                    //
                    break;
                case "None":
                    //
                    break;
                case "RowsRemoved":
                    this.ButtonModifyCancel.Enabled = true;
                    break;
                case "CanNotSave":
                    this.ButtonModifyCancel.Enabled = true;
                    break;
                case "CellValueChanged":
                    this.ButtonModifyCancel.Enabled = true;
                    break;
                case "AddRecord":
                    this.ButtonModifyCancel.Enabled = true;                    
                    break;
                case "Saved":
                    this.ButtonModifyCancel.Enabled = false;
                    break;
                default:
                    this.ButtonModifyCancel.Enabled = false;
                    // add every new button here.
                    break;
            }
        }

        private void SetColumnNames()
        {           
            DataTable dt = new DataTable();
            dt.Clear();
            
            dt.Columns.Add("Status");
            dt.Columns.Add("ID");
            dt.Columns.Add("GUID");
            dt.Columns.Add("Bestand of map");
            dt.Columns.Add("Log bestand");
            dt.Columns.Add("Locatie");
            dt.Columns.Add("Datum gewijzigd");
            dt.Columns.Add("Max verschil in dagen");
            dt.Columns.Add("Werkelijk verschil in dagen");
            dt.Columns.Add("Bron");
            dt.Columns.Add("Gemeente");
            dt.Columns.Add("Volgorde");
            dt.Columns.Add("Opmerking");
        }
        private void LoadFile()
        {
            LabelCurrentAction.Text = "De bestanden worden gecontroleerd...";

            Cursor.Current = Cursors.WaitCursor;
            ToolStripMenuItemInfo.BackColor = Color.LightGray;
            ToolStripMenuItemInfo.Text = "Bezig Met controleren...";
            ToolStripMenuItemInfo.Visible = true;

            try
            {
                this.DataGridViewMonitor.SuspendLayout();
                this.DataGridViewMonitor.CellFormatting -= new DataGridViewCellFormattingEventHandler(this.DataGridViewMonitor_CellFormatting);
                MfApplicationDatabaseMaintain getItemms = new(DataGridViewMonitor, bindingSourceItemsMonitor);

                AddItemToMonitorDgv(getItemms.GetAllItemsToMonitor(), DataGridViewMonitor);

                this.DataGridViewMonitor.Columns["Id"].Visible = false;
                this.DataGridViewMonitor.Columns["Guid"].Visible = false;
                this.DataGridViewMonitor.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                DataGridViewMonitor.CellFormatting += new DataGridViewCellFormattingEventHandler(this.DataGridViewMonitor_CellFormatting);
                this.DataGridViewMonitor.ResumeLayout();

                if (DataGridViewMonitor.RowCount == 0)
                {
                    ToolStripMenuItemInfo.Text = string.Empty;
                    ToolStripMenuItemInfo.Visible = false;
                }

                LabelCurrentAction.Text = string.Empty;
                ToolStripMenuItemInfo.Text = string.Empty;

                l

                this.Refresh();

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                LabelCurrentAction.Text = string.Empty;
                ToolStripMenuItemInfo.Text = "Bezig Met controleren...  --> Onverwachte fout opgetreden.";
                this.Refresh();

                Cursor.Current = Cursors.Default;
                if (!this.NoMessageBoxArgument)
                {
                    MessageBox.Show("Het bestand met de logbestand namen is niet ingelezen." + Environment.NewLine + Environment.NewLine +
                                "Controleer het log bestand.",
                           "Fout",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error);
                }
                MfLogging.WriteToLogError(string.Format("De tabel {0} is neit ingelezen.", MfTableName.ITEMS));
                MfLogging.WriteToLogError("Melding:");
                MfLogging.WriteToLogError(ex.Message);
            }
        }

        private void AddItemToMonitorDgv(MfItemsData AllItems, DataGridView dgv)
        {
            // filter dtable
            this.dtMonitorItems.Columns.Clear();

            this.dtMonitorItems.Columns.Add("Status", typeof(string));
            this.dtMonitorItems.Columns.Add("Id", typeof(int));
            this.dtMonitorItems.Columns.Add("Guid", typeof(string));
            this.dtMonitorItems.Columns.Add("Bestand of map", typeof(string));
            this.dtMonitorItems.Columns.Add("Log bestand", typeof(string));
            this.dtMonitorItems.Columns.Add("Locatie", typeof(string));
            this.dtMonitorItems.Columns.Add("Datum gewijzigd", typeof(DateTime));
            this.dtMonitorItems.Columns.Add("Max verschil in dagen", typeof(int));
            this.dtMonitorItems.Columns.Add("Werkelijk verschil in dagen", typeof(int));
            this.dtMonitorItems.Columns.Add("Bron", typeof(string));
            this.dtMonitorItems.Columns.Add("Gemeente", typeof(string));
            this.dtMonitorItems.Columns.Add("Volgorde", typeof(int));
            this.dtMonitorItems.Columns.Add("Opmerking", typeof(string));            

            // string ItemsToShow = this.JsonObjSettings.AppParam[0].ItemTypeToShow;
            foreach (MfItemData item in AllItems.Items)
            {
                DataRow row = this.dtMonitorItems.NewRow();

                row["Status"] = item.FileStatus;
                row["Id"] = item.ID;
                row["Guid"] = item.GUID;
                row["Bestand of map"] = item.FILE_OR_FOLDER_NAME;
                row["Log bestand"] = item.FILE_NAME;
                row["Locatie"] = item.FOLDER_NAME;
                row["Datum gewijzigd"] = item.fileModificationDate;
                row["Max verschil in dagen"] = item.DIFF_MAX;
                row["Werkelijk verschil in dagen"] = item.daysDifference;
                row["Bron"] = item.SOURCE_NAME;
                row["Gemeente"] = item.TONWSHIP_NAME;
                row["Volgorde"] = item.FILE_ORDER;
                row["Opmerking"] = item.COMMENT;

                this.dtMonitorItems.Rows.Add(row);
            }

            bndMonitorItems.DataSource = this.dtMonitorItems;
            this.DataGridViewMonitor.DataSource = bndMonitorItems;
        }
        #endregion form load

        #region add new item
        private void ButtonNewCancel_Click(object sender, EventArgs e)
        {
            ClearNewItemEntrys();
            EanalbeAllTabpages();
        }

        private void ClearNewItemEntrys()
        {
            ComboBoxNewFileOrFolder.Text = string.Empty;
            TextBoxNewFile.Text = string.Empty;
            TextBoxNewFolder.Text = string.Empty;
            TextBoxNewMaxDiffDays.Text = string.Empty;
            ComboBoxNewSource.Text = string.Empty;
            ComboBoxNewTownship.Text = string.Empty;
            TextBoxNewOrder.Text = string.Empty;
            TextBoxNewComment.Text = string.Empty;
            ButtonNewSave.Enabled = false;
            ComboBoxNewFileOrFolder.Text = "Bestand";

            ActiveControl = ButtonSelectFileOrFolder;
        }
        private void ButtonSelectFileOrFolder_Click(object sender, EventArgs e)
        {
            if (ComboBoxNewFileOrFolder.Text == "Bestand")
            {
                using OpenFileDialog openFileDialog = new();
                openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    TextBoxNewFile.Text = Path.GetFileName(openFileDialog.FileName);
                    TextBoxNewFolder.Text = Path.GetDirectoryName(openFileDialog.FileName);
                }
            }
            else if (ComboBoxNewFileOrFolder.Text == "Map")
            {
                using FolderBrowserDialog folderBrowserDialog = new();
                folderBrowserDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    TextBoxNewFolder.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void ComboBoxNewFileOrFolder_TextChanged(object sender, EventArgs e)
        {
            if (ComboBoxNewFileOrFolder.Text != string.Empty)
            {    
                ButtonSelectFileOrFolder.Enabled = true;
            }
            else
            {
                ButtonSelectFileOrFolder.Enabled = false;
            }
            ActivateNewItemSave();
        }

        private void ActivateNewItemSave()
        {
            if ((!string.IsNullOrEmpty(ComboBoxNewFileOrFolder.Text)) &&
               ((!string.IsNullOrEmpty(TextBoxNewFile.Text)) || (!string.IsNullOrEmpty(TextBoxNewFolder.Text))) &&
               (TextBoxNewFile.Text.Length <= 200) &&
               (TextBoxNewFolder.Text.Length <= 10000) &&
               (!string.IsNullOrEmpty(TextBoxNewMaxDiffDays.Text)))
            {
                ButtonNewSave.Enabled = true;
            }
            else
            {
                ButtonNewSave.Enabled = false;
            }
        }

        private void ComboBoxNewFileOrFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxNewFileOrFolder.Text == "Bestand")
            {
                ButtonSelectFileOrFolder.Text = "Selecteer een bestand.";
                TextBoxNewFile.Enabled = true;
                TextBoxNewFolder.Enabled = false;
                ActiveControl = ButtonSelectFileOrFolder;
            }
            else if (ComboBoxNewFileOrFolder.Text == "Map")
            {
                ButtonSelectFileOrFolder.Text = "Selecteer een map.";
                TextBoxNewFile.Enabled = false;
                TextBoxNewFolder.Enabled = true;
                TextBoxNewFile.Text = string.Empty;
                ActiveControl = ButtonSelectFileOrFolder;
            }
            else
            {
                ButtonSelectFileOrFolder.Text = "Selecteer";
                TextBoxNewFile.Enabled = false;
                TextBoxNewFolder.Enabled = false;
            }
        }

        private void ButtonNewSave_Click(object sender, EventArgs e)
        {
            SetStatusLabelMain = "opslaan nieuw item...";

            string orderAsString = TextBoxNewOrder.Text;
            int? orderAsNumber;

            if (!string.IsNullOrEmpty(orderAsString))
            {
                orderAsNumber = int.Parse(orderAsString);
            }
            else
            {
                orderAsNumber = null;
            }

            MonitorItem Mi = new()
            {
                Guid = Guid.NewGuid().ToString(),
                FileOrFolder_id = int.Parse(ComboBoxNewFileOrFolder.SelectedValue.ToString()),
                FileName = TextBoxNewFile.Text,
                FolderName = TextBoxNewFolder.Text,
                MaxDiffDays = int.Parse(TextBoxNewMaxDiffDays.Text),
                Source_id = int.Parse(ComboBoxNewSource.SelectedValue.ToString()),
                Township_id = int.Parse(ComboBoxNewTownship.SelectedValue.ToString()),
                Order = orderAsNumber,
                Comment = TextBoxNewComment.Text
            };

            using MfApplicationDatabaseMaintain AppDb = new();
            AppDb.InsertIntoItemTbl(Mi);
            ClearNewItemEntrys();
            MfLogging.WriteToLogInformation("Nieuw item opgeslagen.");
            SetStatusLabelMain = string.Empty;
        }

        private void TextBoxNewFile_TextChanged(object sender, EventArgs e)
        {
            const Int16 textlength = 200;

            if (TextBoxNewFile.Text.Length > textlength)
            {
                TextBoxNewFile.ForeColor = Color.Red;
                ButtonNewSave.Enabled = false;
                MessageBox.Show(string.Format("De tekst mag maximaal {0} tekens bevatten.", textlength.ToString()), "Informatie.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ActivateNewItemSave();
                TextBoxNewFile.ForeColor = Color.Black;
            }
        }

        private void TextBoxNewFolder_TextChanged(object sender, EventArgs e)
        {
            const Int16 textlength = 10000;

            ActivateNewItemSave();

            if (TextBoxNewFolder.Text.Length > textlength)
            {
                TextBoxNewFolder.ForeColor = Color.Red;
                ButtonNewSave.Enabled = false;
                MessageBox.Show(string.Format("De tekst mag maximaal {0} tekens bevatten.", textlength.ToString()), "Informatie.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ActivateNewItemSave();
                TextBoxNewFolder.ForeColor = Color.Black;
            }
        }

        private void TextBoxNewMaxDiffDays_TextChanged(object sender, EventArgs e)
        {
            ActivateNewItemSave();
        }

        #endregion add new item

        #region manage source and township
        private void RadioButtonAddSource_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonAddSource.Checked)
            {
                ButtonOptionModifySource.Text = "Toevoegen";
            }
            else
            {
                ButtonOptionModifySource.Text = "Verwijderen";
            }
        }

        private void ButtonOptionModifySource_Click(object sender, EventArgs e)
        {
            using MfApplicationDatabaseMaintain ModifySource = new();
            if (RadioButtonAddSource.Checked)
            {
                // add new source
                string newSource = ComboBoxOptionsSource.Text;
                if (!string.IsNullOrEmpty(newSource))
                {                    
                    if (!ComboBoxOptionsSource.Items.Contains(newSource))
                    {
                        // if insert succeeds then add the new source as item in the combobox list
                        if (ModifySource.InsertIntoSourceTbl(newSource))
                        {
                            ComboBoxOptionsSource.Items.Add(newSource);

                            ComboBoxOptionsSource.Text = string.Empty;
                            SetStatusLabelMain = string.Format("'{0}' is toegevoegd aan de tabel {1}.", newSource, MfTableName.SOURCE);
                            ActiveControl = ComboBoxOptionsSource;
                        }
                    }
                }
            }
            else
            {
                // delete selected source
                string deleteSource = ComboBoxOptionsSource.Text;
                if (!string.IsNullOrEmpty(deleteSource))
                {
                    if (ModifySource.DeleteFromSourceTbl(deleteSource))
                    {
                        ComboBoxOptionsSource.Items.Remove(deleteSource);
                        ComboBoxOptionsSource.Text = string.Empty;
                        SetStatusLabelMain = string.Format("'{0}' is verwijderd uit de tabel {1}.", deleteSource, MfTableName.SOURCE);
                        ActiveControl = ComboBoxOptionsSource;
                    }
                }
            }
        }

        private void ButtonOptionModifySource_MouseLeave(object sender, EventArgs e)
        {
            SetStatusLabelMain = string.Empty;
        }

        private void ComboBoxOptionsSource_TextChanged(object sender, EventArgs e)
        {
            const Int16 textlength = 100;
            if (ComboBoxOptionsSource.Text.Length > textlength)
            {
                ComboBoxOptionsSource.ForeColor = Color.Red;
                ButtonOptionModifySource.Enabled = false;
                MessageBox.Show(string.Format("De tekst mag maximaal {0} tekens bevatten.", textlength.ToString()), "Informatie.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ButtonOptionModifySource.Enabled = true;
                ComboBoxOptionsSource.ForeColor = Color.Black;
            }
        }

        private void RadioButtonAddTownship_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonAddTownship.Checked)
            {
                ButtonOptionModifyTownship.Text = "Toevoegen";
            }
            else
            {
                ButtonOptionModifyTownship.Text = "Verwijderen";
            }
        }

        private void ButtonOptionModifyTownship_Click(object sender, EventArgs e)
        {
            using MfApplicationDatabaseMaintain ModifyTownship = new();
            if (RadioButtonAddTownship.Checked)
            {
                // add new source
                string newTownship = ComboBoxOptionsTownship.Text;
                if (!string.IsNullOrEmpty(newTownship))
                {
                    if (!ComboBoxOptionsTownship.Items.Contains(newTownship))
                    {
                        // if insert succeeds then add the new township as item in the combobox list
                        if (ModifyTownship.InsertIntoTownshipTbl(newTownship))
                        {
                            ComboBoxOptionsTownship.Items.Add(newTownship);

                            ComboBoxOptionsTownship.Text = string.Empty;
                            SetStatusLabelMain = string.Format("'{0}' is toegevoegd aan de tabel {1}.", newTownship, MfTableName.TOWNSHIP);
                            ActiveControl = ComboBoxOptionsTownship;
                        }
                    }
                }
            }
            else
            {
                // delete selected township
                string deleteTownship = ComboBoxOptionsTownship.Text;
                if (!string.IsNullOrEmpty(deleteTownship))
                {
                    if (ModifyTownship.DeleteFromTownshipTbl(deleteTownship))
                    {
                        ComboBoxOptionsTownship.Items.Remove(deleteTownship);
                        ComboBoxOptionsTownship.Text = string.Empty;
                        SetStatusLabelMain = string.Format("'{0}' is verwijderd uit de tabel {1}.", deleteTownship, MfTableName.TOWNSHIP);
                        ActiveControl = ComboBoxOptionsTownship;
                    }
                }
            }
        }


        private void ButtonOptionModifyTownship_MouseLeave(object sender, EventArgs e)
        {
            SetStatusLabelMain = string.Empty;
        }

        private void ComboBoxOptionsTownship_TextChanged(object sender, EventArgs e)
        {
            const Int16 textlength = 50;
            if (ComboBoxOptionsSource.Text.Length > textlength)
            {
                ComboBoxOptionsSource.ForeColor = Color.Red;
                ButtonOptionModifySource.Enabled = false;
                MessageBox.Show(string.Format("De tekst mag maximaal {0} tekens bevatten.", textlength.ToString()), "Informatie.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ButtonOptionModifySource.Enabled = true;
                ComboBoxOptionsSource.ForeColor = Color.Black;
            }
        }


        #endregion manage source and township

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetComboboxItems(false);
            if (TabControlMaintain.SelectedIndex == 1)
            {
                LoadEntrysMaintain();
            }
        }

        private void LoadEntrysMaintain()
        {
            // load all items
            DataGridViewModify.DataSource = null;
            DataGridViewModify.AutoGenerateColumns = true;
            MfApplicationDatabaseMaintain getItemms = new(DataGridViewModify, bindingSourceItemsMaintain);            

            this.DataGridViewModify.EditingControlShowing -= new DataGridViewEditingControlShowingEventHandler(this.DataGridViewModify_EditingControlShowing);
            this.DataGridViewModify.CellValidated -= new DataGridViewCellEventHandler(this.DataGridViewModify_CellValidated);
           
            getItemms.GetAllItemsToMaintain();  // !

            DataGridViewModify.Columns[0].Visible = false; // ID
            DataGridViewModify.Columns[1].Visible = false; // GUID
            DataGridViewModify.Columns[13].Visible = false; // CREATE_DATE
            DataGridViewModify.Columns[15].Visible = false; // CREATED_BY            

            this.DataGridViewModify.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.DataGridViewModify_EditingControlShowing);
            this.DataGridViewModify.CellValidated += new DataGridViewCellEventHandler(this.DataGridViewModify_CellValidated); // System.ArgumentException: DataGridViewComboBoxCell value is not valid
            // the comboboxes give a System.ArgumentException. To avoid this the dataerror eventhandler is added (e.Cancel = true;)

            this.DataGridViewModify.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            this.CellValueChanged = this.CheckForDuplicates(DataGridViewModify);
            this.tblMaintenance = getItemms;  // SAVE
            this.CellValueChanged = false;
            this.EnableFunctions(ApplicationAccess.Saved.ToString());
        }

        private void DataGridViewModify_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Make the Comboboxes in the datagridview edit able
            if (e != null && e.Control is DataGridViewComboBoxEditingControl)
            {
                if (e.Control is not ComboBox combo)
                {
                    return;
                }

                combo.DropDownStyle = ComboBoxStyle.DropDown;
                combo.AutoCompleteMode = AutoCompleteMode.Suggest;  // SuggestAppend;

                // combo.Sorted = true;  //Not possible when the list is a datasource
            }
        }

        private void DataGridViewModify_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Drop down the combobox with 1 mouse click.
            bool validClick = e.RowIndex != -1 && e.ColumnIndex != -1; // Make sure the selected cell is valid.
            var datagridview = sender as DataGridView;

            if (datagridview != null)
            {
                // Check to make sure the selected cell is the cell containing the combobox 
                if (datagridview.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && validClick)
                {
                    datagridview.BeginEdit(true);
                    ((ComboBox)datagridview.EditingControl).DroppedDown = true;
                }
            }
        }

        private void DataGridViewModify_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    // Bestand wordt gewijzigd naar map: maak de cell met bestandsnaam leeg.

                    if (e.ColumnIndex > 0)
                    {
                        // Cells[4] this is the order of the selecte statement
                        if (DataGridViewModify.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "2")  // 2 = Map
                        {
                            DataGridViewModify.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value = string.Empty;
                            DataGridViewModify.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Style.BackColor = Color.White;
                        }
                        else if (DataGridViewModify.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "1" &&
                                 string.IsNullOrEmpty(DataGridViewModify.Rows[e.RowIndex].Cells[4].Value.ToString())                            
                                )  // 1 = Bestand
                        {
                            DataGridViewModify.Rows[e.RowIndex].Cells[4].Style.BackColor = Color.Red;
                        }
                        
                        if (DataGridViewModify.Rows[e.RowIndex].Cells[2].Value.ToString() == "1" &&
                                 !string.IsNullOrEmpty(DataGridViewModify.Rows[e.RowIndex].Cells[4].Value.ToString())
                           )
                        {
                            DataGridViewModify.Rows[e.RowIndex].Cells[e.ColumnIndex + 0].Style.BackColor = Color.White;
                        }
                    }

                    // Set the DATE_ALTERED...
                    if (e.RowIndex != this.DataGridViewModify.NewRowIndex)
                    {
                        this.CellValueChanged = true;
                        //this.DataGridViewModify.Rows[e.RowIndex].Cells["Datum_gewijzigd"].Value = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                        this.DataGridViewModify.Rows[e.RowIndex].Cells["Datum_gewijzigd"].Value = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

                        //this.DataGridViewModify.Rows[e.RowIndex].Cells["Datum_gewijzigd"].Value = DateTime.Today;
                        this.DataGridViewModify.Rows[e.RowIndex].Cells["Gewijzigd_door"].Value = Environment.UserName;
                    }

                    if (this.CellValueChanged)
                    {
                        this.EnableFunctions(ApplicationAccess.CellValueChanged.ToString());
                    }
                    else
                    {
                        this.EnableFunctions(ApplicationAccess.Saved.ToString());
                    }

                    this.CellValueChanged = this.CheckForDuplicates(DataGridViewModify);
                }
            }
            catch (Exception ex)
            {
                string tmp = ex.Message.ToString();
            }
        }

        private void ButtonModifySave_Click(object sender, EventArgs e)
        {
            if (this.tblMaintenance.Dt != null)
            {
                SetStatusLabelMain = "Bezig met opslaan...";
                Cursor.Current = Cursors.WaitCursor;

                this.EnableFunctions(String.Empty);  //Disable all buttons 

                // Get the changes in the data table
                this.changeTableDeleted = this.tblMaintenance.Dt.GetChanges(DataRowState.Deleted);   // TODO move to MfApplicationDatabaseMaintain
                this.changeTableAdded = this.tblMaintenance.Dt.GetChanges(DataRowState.Added);       // TODO move to MfApplicationDatabaseMaintain
                this.changeTableModified = this.tblMaintenance.Dt.GetChanges(DataRowState.Modified); // TODO move to MfApplicationDatabaseMaintain

                // check for duplicates in the combination: Folder + File + Source + Township

                MfItemsData mfItems = new();

                Dictionary<int, string> newData = new();

                foreach (DataRow row in this.tblMaintenance.Dt.Rows)
                {
                    // Skip deleted rows
                    if (row != null && row.RowState != DataRowState.Deleted)
                    {
                        MfItemData mfItem = new();
                        // add to class
                        mfItem.ID = int.Parse(row[0].ToString());
                        mfItem.FILE_NAME = row[3].ToString() ?? string.Empty;               // File name 
                        mfItem.FOLDER_NAME = row[4].ToString() ?? string.Empty;             // File path
                        mfItem.SOURCE_ID = int.Parse(row[6].ToString());    // source_id
                        mfItem.TONWSHIP_ID = int.Parse(row[7].ToString());  // Township id                    

                        mfItems.Items.Add(mfItem);

                        newData.Add(mfItem.ID, mfItem.Concat().Trim());                        
                    }
                }

                //check for duplicate values (FILE_NAME+FOLDER_NAME+SOURCE_ID+TONWSHIP_ID)
                var duplicateValues = newData.GroupBy(x => x.Value).Where(x => x.Count() > 1);
                bool abortUpdate = false;
                string aDuplicateValue = string.Empty;
                foreach (var item in duplicateValues)
                {
                    // If found something dan abort updating
                    abortUpdate = true;

                    aDuplicateValue += item.Key + "; ";
                }

                if (abortUpdate)
                {
                    MfLogging.WriteToLogWarning("Dubbele waarden aangetroffen in de combinatie 'FILE_NAME+FOLDER_NAME+SOURCE_ID+TONWSHIP_ID'");
                    SetStatusLabelMain = "Opslaan onderbreken.";

                    DialogResult dialogResult = MessageBox.Show(
                    "Dubbele waarde aangetroffen in de combinatie 'FILE_NAME+FOLDER_NAME+SOURCE_ID+TONWSHIP_ID'." + Environment.NewLine +
                    Environment.NewLine +
                    "Wilt u de wijzigingen toch opslaan?",
                    "Waarschuwing",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);
                    if (dialogResult == DialogResult.Yes)
                    {
                        abortUpdate = false;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        abortUpdate = true;
                    }
                }
                if (!abortUpdate)
                {
                    SetStatusLabelMain = "Bezig met opslaan...";
                    try
                    {
                        DataGridViewModify.SuspendLayout();
                        DataGridViewModify.Enabled = false;

                        if (this.changeTableModified != null)
                        {
                            this.tblMaintenance.Da.Update(this.tblMaintenance.Dt.Select(null, null, DataViewRowState.ModifiedCurrent));
                        }

                        if (this.changeTableAdded != null)
                        {
                            this.tblMaintenance.Da.Update(this.tblMaintenance.Dt.Select(null, null, DataViewRowState.Added));
                        }

                        if (this.changeTableDeleted != null)
                        {
                            this.tblMaintenance.Da.Update(this.tblMaintenance.Dt.Select(null, null, DataViewRowState.Deleted));
                        }

                        DataGridViewModify.ResumeLayout();
                        DataGridViewModify.Enabled = true;

                        this.LogTableUpdate();

                        this.ClearChangeDataTables();
                        this.CellValueChanged = false;

                        this.ButtonModifyCancel.Enabled = false;
                    }
                    catch (SQLiteException ex)
                    {
                        SetStatusLabelMain = "Fout bij het opslaan...";
                        this.Refresh();
                        MfLogging.WriteToLogError(string.Format("Opslaan wijzigingen in de {0} zijn mislukt.", MfTableName.ITEMS));
                        MfLogging.WriteToLogError(ex.Message);
                        MfLogging.WriteToLogError(ex.ToString());

                        if (ex.Message.Contains("FOREIGN KEY constraint failed"))
                        {
                            MessageBox.Show("Mutaties worden niet verwerkt." + Environment.NewLine + "Domeinwaarden die in gebruik zijn kunnen niet verwijderd worden.", "Fout.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Mutaties worden niet verwerkt. Er is een onverwachte fout opgetreden .", "Fout.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        this.DataGridViewModify.DataSource = null;
                        this.tblMaintenance.BndSource.DataSource = null;
                        this.tblMaintenance.GetAllItemsToMaintain();  // Reload the data
                        this.EnableFunctions(ApplicationAccess.CellValueChanged.ToString());
                    }
                    catch (Exception ex)
                    {
                        SetStatusLabelMain = "Fout bij het opslaan...";
                        this.Refresh();
                        MfLogging.WriteToLogError(string.Format("Opslaan wijzigingen in de tabel {0} zijn mislukt.", MfTableName.ITEMS));
                        MfLogging.WriteToLogError(ex.Message);

                        if (MfLogging.DebugMode)
                        {
                            MfLogging.WriteToLogError(ex.ToString());
                        }

                        this.tblMaintenance.GetAllItemsToMaintain();  // Reload the data
                        this.EnableFunctions(ApplicationAccess.CellValueChanged.ToString());
                    }
                    // Update domain values in all tables. If a record in Class is deleted then in Orde the attrib. class_id must be updated.
                }

                
                this.CellValueChanged = false;

                SetStatusLabelMain = "";
                Cursor.Current = Cursors.Default;
            }
        }

        private void LogTableUpdate()
        {
            if (this.changeTableDeleted != null)
            {
                if (this.changeTableDeleted.Rows.Count > 0)
                {
                    MfLogging.WriteToLogInformation(string.Format("Er zijn {0} records verwijderd uit de tabel: {1}.", this.changeTableDeleted.Rows.Count.ToString(), MfTableName.ITEMS));
                }
            }

            if (this.changeTableAdded != null)
            {
                if (this.changeTableAdded.Rows.Count > 0)
                {
                    MfLogging.WriteToLogInformation(string.Format("Er zijn {0} records toegevoegd aan de tabel : {1}.", this.changeTableAdded.Rows.Count.ToString(), MfTableName.ITEMS));
                }
            }

            if (this.changeTableModified != null)
            {
                if (this.changeTableModified.Rows.Count > 0)
                {
                    MfLogging.WriteToLogInformation(string.Format("Er zijn {0} mutaties doorgevoerd in de tabel : {1}", this.changeTableModified.Rows.Count.ToString(), MfTableName.ITEMS));
                }
            }
        }

        private void ClearChangeDataTables()
        {
            if (this.changeTableModified != null)
            {
                this.changeTableModified = null;
            }

            if (this.changeTableDeleted != null)
            {
                this.changeTableDeleted = null;
            }

            if (this.changeTableAdded != null)
            {
                this.changeTableAdded = null;
            }
        }

        private void DataGridViewModify_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.CellValueChanged = true;
        }

        private void DataGridViewModify_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            this.CellValueChanged = true;
        }

        private void ButtonModifyCancel_Click(object sender, EventArgs e)
        {
            if (this.tblMaintenance.Dt != null)
            {
                SetStatusLabelMain = "Bezig met ongedaan maken...";
                this.Refresh();
                this.DataGridViewModify.SuspendLayout();
                this.DataGridViewModify.Enabled = false;

                this.tblMaintenance.Dt.RejectChanges();
                this.CellValueChanged = false;
                this.EnableFunctions(ApplicationAccess.None.ToString());

                this.DataGridViewModify.ResumeLayout();
                this.DataGridViewModify.Enabled = true;

                this.CheckForDuplicates(DataGridViewModify);  // Check for duplicates but don't set the CellValueChanged with this function.
                this.CellValueChanged = false;
                LabelModifyWarning.Text = string.Empty;
                SetStatusLabelMain = string.Empty;
                if (!CellValueChanged)
                {
                    this.EnableFunctions(ApplicationAccess.Saved.ToString());
                }
                else 
                {
                    this.EnableFunctions(ApplicationAccess.CellValueChanged.ToString());
                }
                this.Refresh();
            }
        }

        private void tabControl2_MouseUp(object sender, MouseEventArgs e)
        {            
            if (this.CellValueChanged)
            {
                MessageBox.Show("Sla eerst de wijzigingen op.", "Waarschuwing.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TabControlMaintain.SelectedIndex = 1;
                this.ActiveControl = this.ButtonModifySave;
            }    
        }

        private void DataGridViewModify_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            var datagridview = sender as DataGridView;

            if (datagridview != null && e != null)
            {
                datagridview.SuspendLayout();
                bool canSave = false;
                canSave = this.CheckForDuplicates(DataGridViewModify);

                if (!canSave)
                {
                    this.EnableFunctions(ApplicationAccess.CanNotSave.ToString());
                    LabelModifyWarning.Text = "Let op, de combinatie 'Bestand of map' - 'Logbestand' - 'Locatie' - 'Source_name' - 'Township_name' moet uniek zijn.";
                }
                else
                {
                    LabelModifyWarning.Text = string.Empty;
                }
                datagridview.ResumeLayout();
            }
        }

        private void EanalbeAllTabpages()
        {
            for (int i = 0; i < TabControlMaintain.TabCount; i++)
            {
                TabControlMaintain.TabPages[i].Enabled = true;
            }
        }

        private bool CheckForDuplicates(DataGridView dgv)
        {
            string combiCellValues;            
            Dictionary<int, string> itemsToMonitor = new();
            bool canSave = true;

            if (dgv.Rows.Count > 1)
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    combiCellValues = string.Empty;
                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        if (dgv.Columns[i].HeaderText == "FILE_OR_FOLDER_NAME" ||
                            dgv.Columns[i].HeaderText == "Log_bestand" ||
                            dgv.Columns[i].HeaderText == "Locatie" ||
                            dgv.Columns[i].HeaderText == "SOURCE_ID" ||
                            dgv.Columns[i].HeaderText == "TOWNSHIP_ID"
                            )
                        {
                            if (row.Cells[i].Value != null)
                            {
                                combiCellValues += row.Cells[i].Value.ToString();
                            }
                        }
                    }
                    itemsToMonitor.Add(row.Index, combiCellValues);
                }

                // Color the duplcate values in the datagridview.
                // First make all values black. And then change the color for the double values.
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }

                dgv.DefaultCellStyle.ForeColor = Color.Black;
                this.Refresh();

                // Get duplicate keys
                var result = itemsToMonitor
                .GroupBy(z => z.Value)
                .Where(z => z.Count() > 1)
                .SelectMany(z => z)
                .Select(z => z.Key)
                .ToList();

                if (result.Count > 0)
                {
                    canSave = false;
                }

                foreach (int itemKey in result)
                {
                    foreach (int duplicateKey in result)
                    {
                        foreach (DataGridViewRow row in dgv.Rows)
                        {
                            if (row.Index == duplicateKey)
                            {
                                //row.DefaultCellStyle.BackColor = Color.Red;
                                row.DefaultCellStyle.ForeColor = Color.Red;
                            }
                        }
                    }
                }
                return canSave;
            }
            else
            {
                return canSave;
            }
        }

        private void DataGridViewModify_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void TabControlMaintain_MouseDown(object sender, MouseEventArgs e)
        {
            CurpageIndex = TabControlMaintain.SelectedIndex;
        }

        private void OptionsToolStripMenuItemOptions_Click(object sender, EventArgs e)
        {
            this.SaveSettings();
            FormConfigure frm = new FormConfigure();
            frm.ShowDialog(this);
            frm.Dispose();
            this.GetSettings();
        }

        private void DataGridViewMonitor_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //Formating the columns
            for (int i = 0; i < DataGridViewMonitor.Rows.Count; i++)
            {
                for (int j = 0; j < DataGridViewMonitor.Columns.Count; j++)
                {
                    if (DataGridViewMonitor.Rows[i].Cells[j].Value != null)
                    {
                        DataGridViewMonitor.Columns["Max verschil in dagen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DataGridViewMonitor.Columns["Werkelijk verschil in dagen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DataGridViewMonitor.Columns["Volgorde"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        //format the numbers
                        this.DataGridViewMonitor.Columns["Max verschil in dagen"].DefaultCellStyle.Format = "#,0.";
                        this.DataGridViewMonitor.Columns["Werkelijk verschil in dagen"].DefaultCellStyle.Format = "#,0.";
                        this.DataGridViewMonitor.Columns["Volgorde"].DefaultCellStyle.Format = "#,0.";
                    }
                }
            }

            //Color the rows
            
            int good = 0;
            int wrong = 0;

            if (DataGridViewMonitor.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in DataGridViewMonitor.Rows)
                {
                    if (row.Cells["Status"].Value.ToString() == "Goed")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                        good++;
                    }
                    else if (row.Cells["Status"].Value.ToString() == "Fout")
                    {
                        row.DefaultCellStyle.BackColor = Color.Tomato;
                        wrong++;
                    }
                    else if (row.Cells["Status"].Value.ToString() != "Bestand bestaat niet (meer).")
                    {
                        row.DefaultCellStyle.BackColor = Color.Orange;
                        wrong++;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                }

                //this.Text = "Monitor Log Files" + "   --> Aantal goed: " + good.ToString() + "   --   Aantal fout: " + wrong.ToString();

                if (wrong == 0)
                {
                    ToolStripMenuItemInfo.BackColor = Color.LawnGreen;
                    ToolStripMenuItemInfo.Text = "Alle bestanden zijn actueel.";
                }
                else
                {
                    ToolStripMenuItemInfo.BackColor = Color.Tomato;
                    ToolStripMenuItemInfo.Text = wrong.ToString() + " Fout(en) gevonden.";
                }
            }
        }

        private void DataGridViewMonitor_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll || e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                DataGridViewMonitor.CellFormatting -= new DataGridViewCellFormattingEventHandler(this.DataGridViewMonitor_CellFormatting);
            }
            else
            {
                DataGridViewMonitor.CellFormatting += new DataGridViewCellFormattingEventHandler(this.DataGridViewMonitor_CellFormatting);
            }
        }

        private void ProgramToolStripMenuItemLoadItems_Click(object sender, EventArgs e)
        {
            this.LoadFile();
        }

        private void OptionsToolStripMenuItemShowAllItems_Click(object sender, EventArgs e)
        {
            this.JsonObjSettings.AppParam[0].ItemTypeToShow = "All";

            this.OptionsToolStripMenuItemShowAllItems.Checked = true;
            this.OptionsToolStripMenuItemShowFaultedItems.Checked = false;
            this.OptionsToolStripMenuItemShowFileIsGoneItems.Checked = false;
            this.OptionsToolStripMenuItemShowValidItems.Checked = false;

            this.bndMonitorItems.Filter = string.Empty;
        }

        private void OptionsToolStripMenuItemShowValidItems_Click(object sender, EventArgs e)
        {
            this.JsonObjSettings.AppParam[0].ItemTypeToShow = "Valid";

            this.OptionsToolStripMenuItemShowAllItems.Checked = false;
            this.OptionsToolStripMenuItemShowFaultedItems.Checked = false;
            this.OptionsToolStripMenuItemShowFileIsGoneItems.Checked = false;
            this.OptionsToolStripMenuItemShowValidItems.Checked = true;

            this.bndMonitorItems.Filter = "Status = 'Goed'";
        }

        private void OptionsToolStripMenuItemShowFaultedItems_Click(object sender, EventArgs e)
        {
            this.JsonObjSettings.AppParam[0].ItemTypeToShow = "Faulted";

            this.OptionsToolStripMenuItemShowAllItems.Checked = false;
            this.OptionsToolStripMenuItemShowFaultedItems.Checked = true;
            this.OptionsToolStripMenuItemShowFileIsGoneItems.Checked = false;
            this.OptionsToolStripMenuItemShowValidItems.Checked = false;

            this.bndMonitorItems.Filter = "Status = 'Fout'";
        }

        private void OptionsToolStripMenuItemShowFileIsGoneItems_Click(object sender, EventArgs e)
        {
            this.JsonObjSettings.AppParam[0].ItemTypeToShow = "Gone";

            this.OptionsToolStripMenuItemShowAllItems.Checked = false;
            this.OptionsToolStripMenuItemShowFaultedItems.Checked = false;
            this.OptionsToolStripMenuItemShowFileIsGoneItems.Checked = true;
            this.OptionsToolStripMenuItemShowValidItems.Checked = false;

            this.bndMonitorItems.Filter = "Status = 'Bestand bestaat niet(meer).'";
        }

    }

    public struct MonitorItem
    {
        public string Guid { get; set; }
        public int FileOrFolder_id { get; set; }
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public int MaxDiffDays { get; set; }
        public int Source_id { get; set; }
        public int Township_id { get; set; }
        public int? Order { get; set; }
        public string Comment { get; set; }
    }
}