namespace MonitorFiles
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProgramToolStripMenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ToolStripStatusLabelMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.PanelTop = new System.Windows.Forms.Panel();
            this.PanelBottom = new System.Windows.Forms.Panel();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.PanelMain = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TabPageMonitor = new System.Windows.Forms.TabPage();
            this.TabPageMaintain = new System.Windows.Forms.TabPage();
            this.TabControlMaintain = new System.Windows.Forms.TabControl();
            this.TabPageNew = new System.Windows.Forms.TabPage();
            this.GroupBoxAddItem = new System.Windows.Forms.GroupBox();
            this.TextBoxNewComment = new System.Windows.Forms.TextBox();
            this.LabelComment = new System.Windows.Forms.Label();
            this.ButtonNewCancel = new System.Windows.Forms.Button();
            this.ButtonNewSave = new System.Windows.Forms.Button();
            this.ButtonSelectFileOrFolder = new System.Windows.Forms.Button();
            this.LabelOrder = new System.Windows.Forms.Label();
            this.TextBoxNewOrder = new System.Windows.Forms.TextBox();
            this.LabelTownship = new System.Windows.Forms.Label();
            this.ComboBoxNewTownship = new System.Windows.Forms.ComboBox();
            this.LabelSource = new System.Windows.Forms.Label();
            this.ComboBoxNewSource = new System.Windows.Forms.ComboBox();
            this.TextBoxNewMaxDiffDays = new System.Windows.Forms.TextBox();
            this.TextBoxNewFolder = new System.Windows.Forms.TextBox();
            this.TextBoxNewFile = new System.Windows.Forms.TextBox();
            this.ComboBoxNewFileOrFolder = new System.Windows.Forms.ComboBox();
            this.LabelMaxDaysDiff = new System.Windows.Forms.Label();
            this.LabelFolder = new System.Windows.Forms.Label();
            this.LabelFile = new System.Windows.Forms.Label();
            this.LabelFileOrFolder = new System.Windows.Forms.Label();
            this.TabPageModify = new System.Windows.Forms.TabPage();
            this.DataGridViewModify = new System.Windows.Forms.DataGridView();
            this.PanelModifyBottom = new System.Windows.Forms.Panel();
            this.LabelModifyWarning = new System.Windows.Forms.Label();
            this.ButtonModifyCancel = new System.Windows.Forms.Button();
            this.ButtonModifySave = new System.Windows.Forms.Button();
            this.TabPageOptions = new System.Windows.Forms.TabPage();
            this.GroupBoxOptionsTownship = new System.Windows.Forms.GroupBox();
            this.ButtonOptionModifyTownship = new System.Windows.Forms.Button();
            this.ComboBoxOptionsTownship = new System.Windows.Forms.ComboBox();
            this.RadioButtonDeleteTownship = new System.Windows.Forms.RadioButton();
            this.RadioButtonAddTownship = new System.Windows.Forms.RadioButton();
            this.GroupBoxOptionsSource = new System.Windows.Forms.GroupBox();
            this.ButtonOptionModifySource = new System.Windows.Forms.Button();
            this.ComboBoxOptionsSource = new System.Windows.Forms.ComboBox();
            this.RadioButtonDeleteSource = new System.Windows.Forms.RadioButton();
            this.RadioButtonAddSource = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.PanelBottom.SuspendLayout();
            this.PanelMain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.TabPageMaintain.SuspendLayout();
            this.TabControlMaintain.SuspendLayout();
            this.TabPageNew.SuspendLayout();
            this.GroupBoxAddItem.SuspendLayout();
            this.TabPageModify.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewModify)).BeginInit();
            this.PanelModifyBottom.SuspendLayout();
            this.TabPageOptions.SuspendLayout();
            this.GroupBoxOptionsTownship.SuspendLayout();
            this.GroupBoxOptionsSource.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgramToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(10, 4, 0, 4);
            this.menuStrip1.Size = new System.Drawing.Size(1560, 42);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ProgramToolStripMenuItem
            // 
            this.ProgramToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgramToolStripMenuItemClose});
            this.ProgramToolStripMenuItem.Name = "ProgramToolStripMenuItem";
            this.ProgramToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.ProgramToolStripMenuItem.Size = new System.Drawing.Size(139, 34);
            this.ProgramToolStripMenuItem.Text = "Programma";
            // 
            // ProgramToolStripMenuItemClose
            // 
            this.ProgramToolStripMenuItemClose.Name = "ProgramToolStripMenuItemClose";
            this.ProgramToolStripMenuItemClose.Size = new System.Drawing.Size(213, 40);
            this.ProgramToolStripMenuItemClose.Text = "Afsluiten";
            this.ProgramToolStripMenuItemClose.Click += new System.EventHandler(this.ProgramToolStripMenuItemClose_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripStatusLabelMain});
            this.statusStrip1.Location = new System.Drawing.Point(0, 937);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 24, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1560, 39);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ToolStripStatusLabelMain
            // 
            this.ToolStripStatusLabelMain.Name = "ToolStripStatusLabelMain";
            this.ToolStripStatusLabelMain.Size = new System.Drawing.Size(206, 30);
            this.ToolStripStatusLabelMain.Text = "toolStripStatusLabel1";
            // 
            // PanelTop
            // 
            this.PanelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTop.Location = new System.Drawing.Point(0, 42);
            this.PanelTop.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.PanelTop.Name = "PanelTop";
            this.PanelTop.Size = new System.Drawing.Size(1560, 88);
            this.PanelTop.TabIndex = 3;
            // 
            // PanelBottom
            // 
            this.PanelBottom.Controls.Add(this.ButtonClose);
            this.PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelBottom.Location = new System.Drawing.Point(0, 849);
            this.PanelBottom.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.PanelBottom.Name = "PanelBottom";
            this.PanelBottom.Size = new System.Drawing.Size(1560, 88);
            this.PanelBottom.TabIndex = 4;
            // 
            // ButtonClose
            // 
            this.ButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClose.Location = new System.Drawing.Point(1425, 36);
            this.ButtonClose.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(129, 46);
            this.ButtonClose.TabIndex = 0;
            this.ButtonClose.Text = "Afsluiten";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ProgramToolStripMenuItemClose_Click);
            // 
            // PanelMain
            // 
            this.PanelMain.Controls.Add(this.tabControl1);
            this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMain.Location = new System.Drawing.Point(0, 130);
            this.PanelMain.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Size = new System.Drawing.Size(1560, 719);
            this.PanelMain.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TabPageMonitor);
            this.tabControl1.Controls.Add(this.TabPageMaintain);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1560, 719);
            this.tabControl1.TabIndex = 4;
            // 
            // TabPageMonitor
            // 
            this.TabPageMonitor.Location = new System.Drawing.Point(4, 39);
            this.TabPageMonitor.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TabPageMonitor.Name = "TabPageMonitor";
            this.TabPageMonitor.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TabPageMonitor.Size = new System.Drawing.Size(1552, 676);
            this.TabPageMonitor.TabIndex = 0;
            this.TabPageMonitor.Text = "Monitor";
            this.TabPageMonitor.UseVisualStyleBackColor = true;
            // 
            // TabPageMaintain
            // 
            this.TabPageMaintain.Controls.Add(this.TabControlMaintain);
            this.TabPageMaintain.Location = new System.Drawing.Point(4, 39);
            this.TabPageMaintain.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TabPageMaintain.Name = "TabPageMaintain";
            this.TabPageMaintain.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TabPageMaintain.Size = new System.Drawing.Size(1552, 676);
            this.TabPageMaintain.TabIndex = 1;
            this.TabPageMaintain.Text = "Beheren";
            this.TabPageMaintain.UseVisualStyleBackColor = true;
            // 
            // TabControlMaintain
            // 
            this.TabControlMaintain.Controls.Add(this.TabPageNew);
            this.TabControlMaintain.Controls.Add(this.TabPageModify);
            this.TabControlMaintain.Controls.Add(this.TabPageOptions);
            this.TabControlMaintain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControlMaintain.HotTrack = true;
            this.TabControlMaintain.Location = new System.Drawing.Point(5, 6);
            this.TabControlMaintain.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TabControlMaintain.Name = "TabControlMaintain";
            this.TabControlMaintain.SelectedIndex = 0;
            this.TabControlMaintain.Size = new System.Drawing.Size(1542, 664);
            this.TabControlMaintain.TabIndex = 0;
            this.TabControlMaintain.SelectedIndexChanged += new System.EventHandler(this.tabControl2_SelectedIndexChanged);
            this.TabControlMaintain.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.TabControlMaintain_Selecting);
            this.TabControlMaintain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TabControlMaintain_MouseDown);
            this.TabControlMaintain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tabControl2_MouseUp);
            // 
            // TabPageNew
            // 
            this.TabPageNew.Controls.Add(this.GroupBoxAddItem);
            this.TabPageNew.Location = new System.Drawing.Point(4, 39);
            this.TabPageNew.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TabPageNew.Name = "TabPageNew";
            this.TabPageNew.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TabPageNew.Size = new System.Drawing.Size(1534, 621);
            this.TabPageNew.TabIndex = 0;
            this.TabPageNew.Text = "Nieuw";
            this.TabPageNew.UseVisualStyleBackColor = true;
            // 
            // GroupBoxAddItem
            // 
            this.GroupBoxAddItem.Controls.Add(this.TextBoxNewComment);
            this.GroupBoxAddItem.Controls.Add(this.LabelComment);
            this.GroupBoxAddItem.Controls.Add(this.ButtonNewCancel);
            this.GroupBoxAddItem.Controls.Add(this.ButtonNewSave);
            this.GroupBoxAddItem.Controls.Add(this.ButtonSelectFileOrFolder);
            this.GroupBoxAddItem.Controls.Add(this.LabelOrder);
            this.GroupBoxAddItem.Controls.Add(this.TextBoxNewOrder);
            this.GroupBoxAddItem.Controls.Add(this.LabelTownship);
            this.GroupBoxAddItem.Controls.Add(this.ComboBoxNewTownship);
            this.GroupBoxAddItem.Controls.Add(this.LabelSource);
            this.GroupBoxAddItem.Controls.Add(this.ComboBoxNewSource);
            this.GroupBoxAddItem.Controls.Add(this.TextBoxNewMaxDiffDays);
            this.GroupBoxAddItem.Controls.Add(this.TextBoxNewFolder);
            this.GroupBoxAddItem.Controls.Add(this.TextBoxNewFile);
            this.GroupBoxAddItem.Controls.Add(this.ComboBoxNewFileOrFolder);
            this.GroupBoxAddItem.Controls.Add(this.LabelMaxDaysDiff);
            this.GroupBoxAddItem.Controls.Add(this.LabelFolder);
            this.GroupBoxAddItem.Controls.Add(this.LabelFile);
            this.GroupBoxAddItem.Controls.Add(this.LabelFileOrFolder);
            this.GroupBoxAddItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBoxAddItem.Location = new System.Drawing.Point(5, 6);
            this.GroupBoxAddItem.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.GroupBoxAddItem.Name = "GroupBoxAddItem";
            this.GroupBoxAddItem.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.GroupBoxAddItem.Size = new System.Drawing.Size(1524, 609);
            this.GroupBoxAddItem.TabIndex = 0;
            this.GroupBoxAddItem.TabStop = false;
            this.GroupBoxAddItem.Text = "Voeg een bestand of een locatie toe";
            // 
            // TextBoxNewComment
            // 
            this.TextBoxNewComment.Location = new System.Drawing.Point(314, 454);
            this.TextBoxNewComment.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TextBoxNewComment.Name = "TextBoxNewComment";
            this.TextBoxNewComment.Size = new System.Drawing.Size(997, 35);
            this.TextBoxNewComment.TabIndex = 18;
            // 
            // LabelComment
            // 
            this.LabelComment.AutoSize = true;
            this.LabelComment.Location = new System.Drawing.Point(24, 454);
            this.LabelComment.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LabelComment.Name = "LabelComment";
            this.LabelComment.Size = new System.Drawing.Size(113, 30);
            this.LabelComment.TabIndex = 17;
            this.LabelComment.Text = "Toelichting";
            // 
            // ButtonNewCancel
            // 
            this.ButtonNewCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonNewCancel.Location = new System.Drawing.Point(1246, 557);
            this.ButtonNewCancel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonNewCancel.Name = "ButtonNewCancel";
            this.ButtonNewCancel.Size = new System.Drawing.Size(129, 46);
            this.ButtonNewCancel.TabIndex = 16;
            this.ButtonNewCancel.Text = "Op&ieuw";
            this.ButtonNewCancel.UseVisualStyleBackColor = true;
            this.ButtonNewCancel.Click += new System.EventHandler(this.ButtonNewCancel_Click);
            // 
            // ButtonNewSave
            // 
            this.ButtonNewSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonNewSave.Location = new System.Drawing.Point(1385, 557);
            this.ButtonNewSave.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonNewSave.Name = "ButtonNewSave";
            this.ButtonNewSave.Size = new System.Drawing.Size(129, 46);
            this.ButtonNewSave.TabIndex = 15;
            this.ButtonNewSave.Text = "Op&slaan";
            this.ButtonNewSave.UseVisualStyleBackColor = true;
            this.ButtonNewSave.Click += new System.EventHandler(this.ButtonNewSave_Click);
            // 
            // ButtonSelectFileOrFolder
            // 
            this.ButtonSelectFileOrFolder.Location = new System.Drawing.Point(564, 48);
            this.ButtonSelectFileOrFolder.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonSelectFileOrFolder.Name = "ButtonSelectFileOrFolder";
            this.ButtonSelectFileOrFolder.Size = new System.Drawing.Size(240, 46);
            this.ButtonSelectFileOrFolder.TabIndex = 14;
            this.ButtonSelectFileOrFolder.Text = "Selecteer";
            this.ButtonSelectFileOrFolder.UseVisualStyleBackColor = true;
            this.ButtonSelectFileOrFolder.Click += new System.EventHandler(this.ButtonSelectFileOrFolder_Click);
            // 
            // LabelOrder
            // 
            this.LabelOrder.AutoSize = true;
            this.LabelOrder.Location = new System.Drawing.Point(24, 402);
            this.LabelOrder.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LabelOrder.Name = "LabelOrder";
            this.LabelOrder.Size = new System.Drawing.Size(96, 30);
            this.LabelOrder.TabIndex = 13;
            this.LabelOrder.Text = "Volgorde";
            // 
            // TextBoxNewOrder
            // 
            this.TextBoxNewOrder.Location = new System.Drawing.Point(314, 396);
            this.TextBoxNewOrder.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TextBoxNewOrder.Name = "TextBoxNewOrder";
            this.TextBoxNewOrder.Size = new System.Drawing.Size(169, 35);
            this.TextBoxNewOrder.TabIndex = 12;
            this.TextBoxNewOrder.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxNewMaxDiffDays_KeyPress);
            // 
            // LabelTownship
            // 
            this.LabelTownship.AutoSize = true;
            this.LabelTownship.Location = new System.Drawing.Point(24, 344);
            this.LabelTownship.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LabelTownship.Name = "LabelTownship";
            this.LabelTownship.Size = new System.Drawing.Size(108, 30);
            this.LabelTownship.TabIndex = 11;
            this.LabelTownship.Text = "Gemeente";
            // 
            // ComboBoxNewTownship
            // 
            this.ComboBoxNewTownship.FormattingEnabled = true;
            this.ComboBoxNewTownship.Location = new System.Drawing.Point(314, 338);
            this.ComboBoxNewTownship.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ComboBoxNewTownship.Name = "ComboBoxNewTownship";
            this.ComboBoxNewTownship.Size = new System.Drawing.Size(237, 38);
            this.ComboBoxNewTownship.Sorted = true;
            this.ComboBoxNewTownship.TabIndex = 10;
            // 
            // LabelSource
            // 
            this.LabelSource.AutoSize = true;
            this.LabelSource.Location = new System.Drawing.Point(24, 286);
            this.LabelSource.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LabelSource.Name = "LabelSource";
            this.LabelSource.Size = new System.Drawing.Size(56, 30);
            this.LabelSource.TabIndex = 9;
            this.LabelSource.Text = "Bron";
            // 
            // ComboBoxNewSource
            // 
            this.ComboBoxNewSource.FormattingEnabled = true;
            this.ComboBoxNewSource.Location = new System.Drawing.Point(314, 280);
            this.ComboBoxNewSource.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ComboBoxNewSource.Name = "ComboBoxNewSource";
            this.ComboBoxNewSource.Size = new System.Drawing.Size(237, 38);
            this.ComboBoxNewSource.Sorted = true;
            this.ComboBoxNewSource.TabIndex = 8;
            // 
            // TextBoxNewMaxDiffDays
            // 
            this.TextBoxNewMaxDiffDays.Location = new System.Drawing.Point(314, 222);
            this.TextBoxNewMaxDiffDays.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TextBoxNewMaxDiffDays.Name = "TextBoxNewMaxDiffDays";
            this.TextBoxNewMaxDiffDays.Size = new System.Drawing.Size(169, 35);
            this.TextBoxNewMaxDiffDays.TabIndex = 7;
            this.TextBoxNewMaxDiffDays.TextChanged += new System.EventHandler(this.TextBoxNewMaxDiffDays_TextChanged);
            this.TextBoxNewMaxDiffDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxNewMaxDiffDays_KeyPress);
            // 
            // TextBoxNewFolder
            // 
            this.TextBoxNewFolder.Location = new System.Drawing.Point(314, 164);
            this.TextBoxNewFolder.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TextBoxNewFolder.Name = "TextBoxNewFolder";
            this.TextBoxNewFolder.Size = new System.Drawing.Size(997, 35);
            this.TextBoxNewFolder.TabIndex = 6;
            this.TextBoxNewFolder.TextChanged += new System.EventHandler(this.TextBoxNewFolder_TextChanged);
            // 
            // TextBoxNewFile
            // 
            this.TextBoxNewFile.Location = new System.Drawing.Point(314, 106);
            this.TextBoxNewFile.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TextBoxNewFile.Name = "TextBoxNewFile";
            this.TextBoxNewFile.Size = new System.Drawing.Size(487, 35);
            this.TextBoxNewFile.TabIndex = 5;
            this.TextBoxNewFile.TextChanged += new System.EventHandler(this.TextBoxNewFile_TextChanged);
            // 
            // ComboBoxNewFileOrFolder
            // 
            this.ComboBoxNewFileOrFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboBoxNewFileOrFolder.FormattingEnabled = true;
            this.ComboBoxNewFileOrFolder.Location = new System.Drawing.Point(314, 48);
            this.ComboBoxNewFileOrFolder.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ComboBoxNewFileOrFolder.Name = "ComboBoxNewFileOrFolder";
            this.ComboBoxNewFileOrFolder.Size = new System.Drawing.Size(237, 38);
            this.ComboBoxNewFileOrFolder.Sorted = true;
            this.ComboBoxNewFileOrFolder.TabIndex = 4;
            this.ComboBoxNewFileOrFolder.SelectedIndexChanged += new System.EventHandler(this.ComboBoxNewFileOrFolder_SelectedIndexChanged);
            this.ComboBoxNewFileOrFolder.TextChanged += new System.EventHandler(this.ComboBoxNewFileOrFolder_TextChanged);
            // 
            // LabelMaxDaysDiff
            // 
            this.LabelMaxDaysDiff.AutoSize = true;
            this.LabelMaxDaysDiff.Location = new System.Drawing.Point(24, 228);
            this.LabelMaxDaysDiff.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LabelMaxDaysDiff.Name = "LabelMaxDaysDiff";
            this.LabelMaxDaysDiff.Size = new System.Drawing.Size(285, 30);
            this.LabelMaxDaysDiff.TabIndex = 3;
            this.LabelMaxDaysDiff.Text = "Toegestaan verschil in dagen:";
            // 
            // LabelFolder
            // 
            this.LabelFolder.AutoSize = true;
            this.LabelFolder.Location = new System.Drawing.Point(24, 170);
            this.LabelFolder.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LabelFolder.Name = "LabelFolder";
            this.LabelFolder.Size = new System.Drawing.Size(60, 30);
            this.LabelFolder.TabIndex = 2;
            this.LabelFolder.Text = "Map:";
            // 
            // LabelFile
            // 
            this.LabelFile.AutoSize = true;
            this.LabelFile.Location = new System.Drawing.Point(24, 112);
            this.LabelFile.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LabelFile.Name = "LabelFile";
            this.LabelFile.Size = new System.Drawing.Size(92, 30);
            this.LabelFile.TabIndex = 1;
            this.LabelFile.Text = "Bestand:";
            // 
            // LabelFileOrFolder
            // 
            this.LabelFileOrFolder.AutoSize = true;
            this.LabelFileOrFolder.Location = new System.Drawing.Point(24, 54);
            this.LabelFileOrFolder.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LabelFileOrFolder.Name = "LabelFileOrFolder";
            this.LabelFileOrFolder.Size = new System.Drawing.Size(164, 30);
            this.LabelFileOrFolder.TabIndex = 0;
            this.LabelFileOrFolder.Text = "Bestand of map:";
            // 
            // TabPageModify
            // 
            this.TabPageModify.Controls.Add(this.DataGridViewModify);
            this.TabPageModify.Controls.Add(this.PanelModifyBottom);
            this.TabPageModify.Location = new System.Drawing.Point(4, 39);
            this.TabPageModify.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TabPageModify.Name = "TabPageModify";
            this.TabPageModify.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TabPageModify.Size = new System.Drawing.Size(1534, 621);
            this.TabPageModify.TabIndex = 1;
            this.TabPageModify.Text = "Muteer";
            this.TabPageModify.UseVisualStyleBackColor = true;
            // 
            // DataGridViewModify
            // 
            this.DataGridViewModify.AllowUserToAddRows = false;
            this.DataGridViewModify.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewModify.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridViewModify.Location = new System.Drawing.Point(5, 6);
            this.DataGridViewModify.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.DataGridViewModify.Name = "DataGridViewModify";
            this.DataGridViewModify.RowHeadersWidth = 72;
            this.DataGridViewModify.RowTemplate.Height = 25;
            this.DataGridViewModify.Size = new System.Drawing.Size(1524, 551);
            this.DataGridViewModify.TabIndex = 0;
            this.DataGridViewModify.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewModify_CellEnter);
            this.DataGridViewModify.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewModify_CellValidated);
            this.DataGridViewModify.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewModify_CellValueChanged);
            this.DataGridViewModify.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGridViewModify_DataError);
            this.DataGridViewModify.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DataGridViewModify_EditingControlShowing);
            this.DataGridViewModify.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.DataGridViewModify_RowsAdded);
            this.DataGridViewModify.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.DataGridViewModify_RowsRemoved);
            // 
            // PanelModifyBottom
            // 
            this.PanelModifyBottom.Controls.Add(this.LabelModifyWarning);
            this.PanelModifyBottom.Controls.Add(this.ButtonModifyCancel);
            this.PanelModifyBottom.Controls.Add(this.ButtonModifySave);
            this.PanelModifyBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelModifyBottom.Location = new System.Drawing.Point(5, 557);
            this.PanelModifyBottom.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.PanelModifyBottom.Name = "PanelModifyBottom";
            this.PanelModifyBottom.Size = new System.Drawing.Size(1524, 58);
            this.PanelModifyBottom.TabIndex = 1;
            // 
            // LabelModifyWarning
            // 
            this.LabelModifyWarning.AutoSize = true;
            this.LabelModifyWarning.Location = new System.Drawing.Point(3, 14);
            this.LabelModifyWarning.Name = "LabelModifyWarning";
            this.LabelModifyWarning.Size = new System.Drawing.Size(205, 30);
            this.LabelModifyWarning.TabIndex = 2;
            this.LabelModifyWarning.Text = "LabelModifyWarning";
            // 
            // ButtonModifyCancel
            // 
            this.ButtonModifyCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonModifyCancel.Location = new System.Drawing.Point(1239, 6);
            this.ButtonModifyCancel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonModifyCancel.Name = "ButtonModifyCancel";
            this.ButtonModifyCancel.Size = new System.Drawing.Size(129, 46);
            this.ButtonModifyCancel.TabIndex = 1;
            this.ButtonModifyCancel.Text = "Opnieu&w";
            this.ButtonModifyCancel.UseVisualStyleBackColor = true;
            this.ButtonModifyCancel.Click += new System.EventHandler(this.ButtonModifyCancel_Click);
            // 
            // ButtonModifySave
            // 
            this.ButtonModifySave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonModifySave.Location = new System.Drawing.Point(1390, 6);
            this.ButtonModifySave.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonModifySave.Name = "ButtonModifySave";
            this.ButtonModifySave.Size = new System.Drawing.Size(129, 46);
            this.ButtonModifySave.TabIndex = 0;
            this.ButtonModifySave.Text = "Op&slaan";
            this.ButtonModifySave.UseVisualStyleBackColor = true;
            this.ButtonModifySave.Click += new System.EventHandler(this.ButtonModifySave_Click);
            // 
            // TabPageOptions
            // 
            this.TabPageOptions.Controls.Add(this.GroupBoxOptionsTownship);
            this.TabPageOptions.Controls.Add(this.GroupBoxOptionsSource);
            this.TabPageOptions.Location = new System.Drawing.Point(4, 39);
            this.TabPageOptions.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TabPageOptions.Name = "TabPageOptions";
            this.TabPageOptions.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TabPageOptions.Size = new System.Drawing.Size(1534, 621);
            this.TabPageOptions.TabIndex = 2;
            this.TabPageOptions.Text = "Opties";
            this.TabPageOptions.UseVisualStyleBackColor = true;
            // 
            // GroupBoxOptionsTownship
            // 
            this.GroupBoxOptionsTownship.Controls.Add(this.ButtonOptionModifyTownship);
            this.GroupBoxOptionsTownship.Controls.Add(this.ComboBoxOptionsTownship);
            this.GroupBoxOptionsTownship.Controls.Add(this.RadioButtonDeleteTownship);
            this.GroupBoxOptionsTownship.Controls.Add(this.RadioButtonAddTownship);
            this.GroupBoxOptionsTownship.Location = new System.Drawing.Point(10, 230);
            this.GroupBoxOptionsTownship.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.GroupBoxOptionsTownship.Name = "GroupBoxOptionsTownship";
            this.GroupBoxOptionsTownship.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.GroupBoxOptionsTownship.Size = new System.Drawing.Size(513, 174);
            this.GroupBoxOptionsTownship.TabIndex = 5;
            this.GroupBoxOptionsTownship.TabStop = false;
            this.GroupBoxOptionsTownship.Text = "Beheer de gemeente namen";
            // 
            // ButtonOptionModifyTownship
            // 
            this.ButtonOptionModifyTownship.Location = new System.Drawing.Point(338, 92);
            this.ButtonOptionModifyTownship.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonOptionModifyTownship.Name = "ButtonOptionModifyTownship";
            this.ButtonOptionModifyTownship.Size = new System.Drawing.Size(165, 46);
            this.ButtonOptionModifyTownship.TabIndex = 20;
            this.ButtonOptionModifyTownship.Text = "Toevoegen";
            this.ButtonOptionModifyTownship.UseVisualStyleBackColor = true;
            this.ButtonOptionModifyTownship.Click += new System.EventHandler(this.ButtonOptionModifyTownship_Click);
            this.ButtonOptionModifyTownship.MouseLeave += new System.EventHandler(this.ButtonOptionModifyTownship_MouseLeave);
            // 
            // ComboBoxOptionsTownship
            // 
            this.ComboBoxOptionsTownship.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ComboBoxOptionsTownship.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboBoxOptionsTownship.FormattingEnabled = true;
            this.ComboBoxOptionsTownship.Location = new System.Drawing.Point(10, 94);
            this.ComboBoxOptionsTownship.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ComboBoxOptionsTownship.Name = "ComboBoxOptionsTownship";
            this.ComboBoxOptionsTownship.Size = new System.Drawing.Size(314, 38);
            this.ComboBoxOptionsTownship.TabIndex = 19;
            this.ComboBoxOptionsTownship.TextChanged += new System.EventHandler(this.ComboBoxOptionsTownship_TextChanged);
            // 
            // RadioButtonDeleteTownship
            // 
            this.RadioButtonDeleteTownship.AutoSize = true;
            this.RadioButtonDeleteTownship.Location = new System.Drawing.Point(182, 44);
            this.RadioButtonDeleteTownship.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.RadioButtonDeleteTownship.Name = "RadioButtonDeleteTownship";
            this.RadioButtonDeleteTownship.Size = new System.Drawing.Size(123, 34);
            this.RadioButtonDeleteTownship.TabIndex = 18;
            this.RadioButtonDeleteTownship.TabStop = true;
            this.RadioButtonDeleteTownship.Text = "Verwijder";
            this.RadioButtonDeleteTownship.UseVisualStyleBackColor = true;
            // 
            // RadioButtonAddTownship
            // 
            this.RadioButtonAddTownship.AutoSize = true;
            this.RadioButtonAddTownship.Location = new System.Drawing.Point(10, 44);
            this.RadioButtonAddTownship.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.RadioButtonAddTownship.Name = "RadioButtonAddTownship";
            this.RadioButtonAddTownship.Size = new System.Drawing.Size(138, 34);
            this.RadioButtonAddTownship.TabIndex = 17;
            this.RadioButtonAddTownship.TabStop = true;
            this.RadioButtonAddTownship.Text = "Toevoegen";
            this.RadioButtonAddTownship.UseVisualStyleBackColor = true;
            this.RadioButtonAddTownship.CheckedChanged += new System.EventHandler(this.RadioButtonAddTownship_CheckedChanged);
            // 
            // GroupBoxOptionsSource
            // 
            this.GroupBoxOptionsSource.Controls.Add(this.ButtonOptionModifySource);
            this.GroupBoxOptionsSource.Controls.Add(this.ComboBoxOptionsSource);
            this.GroupBoxOptionsSource.Controls.Add(this.RadioButtonDeleteSource);
            this.GroupBoxOptionsSource.Controls.Add(this.RadioButtonAddSource);
            this.GroupBoxOptionsSource.Location = new System.Drawing.Point(10, 12);
            this.GroupBoxOptionsSource.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.GroupBoxOptionsSource.Name = "GroupBoxOptionsSource";
            this.GroupBoxOptionsSource.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.GroupBoxOptionsSource.Size = new System.Drawing.Size(513, 174);
            this.GroupBoxOptionsSource.TabIndex = 4;
            this.GroupBoxOptionsSource.TabStop = false;
            this.GroupBoxOptionsSource.Text = "Beheer de bronnen";
            // 
            // ButtonOptionModifySource
            // 
            this.ButtonOptionModifySource.Location = new System.Drawing.Point(338, 92);
            this.ButtonOptionModifySource.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonOptionModifySource.Name = "ButtonOptionModifySource";
            this.ButtonOptionModifySource.Size = new System.Drawing.Size(165, 46);
            this.ButtonOptionModifySource.TabIndex = 20;
            this.ButtonOptionModifySource.Text = "Toevoegen";
            this.ButtonOptionModifySource.UseVisualStyleBackColor = true;
            this.ButtonOptionModifySource.Click += new System.EventHandler(this.ButtonOptionModifySource_Click);
            this.ButtonOptionModifySource.MouseLeave += new System.EventHandler(this.ButtonOptionModifySource_MouseLeave);
            // 
            // ComboBoxOptionsSource
            // 
            this.ComboBoxOptionsSource.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ComboBoxOptionsSource.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboBoxOptionsSource.FormattingEnabled = true;
            this.ComboBoxOptionsSource.Location = new System.Drawing.Point(10, 94);
            this.ComboBoxOptionsSource.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ComboBoxOptionsSource.Name = "ComboBoxOptionsSource";
            this.ComboBoxOptionsSource.Size = new System.Drawing.Size(314, 38);
            this.ComboBoxOptionsSource.TabIndex = 19;
            this.ComboBoxOptionsSource.TextChanged += new System.EventHandler(this.ComboBoxOptionsSource_TextChanged);
            // 
            // RadioButtonDeleteSource
            // 
            this.RadioButtonDeleteSource.AutoSize = true;
            this.RadioButtonDeleteSource.Location = new System.Drawing.Point(182, 44);
            this.RadioButtonDeleteSource.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.RadioButtonDeleteSource.Name = "RadioButtonDeleteSource";
            this.RadioButtonDeleteSource.Size = new System.Drawing.Size(123, 34);
            this.RadioButtonDeleteSource.TabIndex = 18;
            this.RadioButtonDeleteSource.TabStop = true;
            this.RadioButtonDeleteSource.Text = "Verwijder";
            this.RadioButtonDeleteSource.UseVisualStyleBackColor = true;
            // 
            // RadioButtonAddSource
            // 
            this.RadioButtonAddSource.AutoSize = true;
            this.RadioButtonAddSource.Location = new System.Drawing.Point(10, 44);
            this.RadioButtonAddSource.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.RadioButtonAddSource.Name = "RadioButtonAddSource";
            this.RadioButtonAddSource.Size = new System.Drawing.Size(138, 34);
            this.RadioButtonAddSource.TabIndex = 17;
            this.RadioButtonAddSource.TabStop = true;
            this.RadioButtonAddSource.Text = "Toevoegen";
            this.RadioButtonAddSource.UseVisualStyleBackColor = true;
            this.RadioButtonAddSource.CheckedChanged += new System.EventHandler(this.RadioButtonAddSource_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1560, 976);
            this.Controls.Add(this.PanelMain);
            this.Controls.Add(this.PanelBottom);
            this.Controls.Add(this.PanelTop);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.PanelBottom.ResumeLayout(false);
            this.PanelMain.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.TabPageMaintain.ResumeLayout(false);
            this.TabControlMaintain.ResumeLayout(false);
            this.TabPageNew.ResumeLayout(false);
            this.GroupBoxAddItem.ResumeLayout(false);
            this.GroupBoxAddItem.PerformLayout();
            this.TabPageModify.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewModify)).EndInit();
            this.PanelModifyBottom.ResumeLayout(false);
            this.PanelModifyBottom.PerformLayout();
            this.TabPageOptions.ResumeLayout(false);
            this.GroupBoxOptionsTownship.ResumeLayout(false);
            this.GroupBoxOptionsTownship.PerformLayout();
            this.GroupBoxOptionsSource.ResumeLayout(false);
            this.GroupBoxOptionsSource.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem ProgramToolStripMenuItem;
        private ToolStripMenuItem ProgramToolStripMenuItemClose;
        private StatusStrip statusStrip1;
        private Panel PanelTop;
        private Panel PanelBottom;
        private Button ButtonClose;
        private Panel PanelMain;
        private TabControl tabControl1;
        private TabPage TabPageMonitor;
        private TabPage TabPageMaintain;
        private TabControl TabControlMaintain;
        private TabPage TabPageNew;
        private GroupBox GroupBoxAddItem;
        private TabPage TabPageModify;
        private TabPage TabPageOptions;
        private TextBox TextBoxNewFolder;
        private TextBox TextBoxNewFile;
        private ComboBox ComboBoxNewFileOrFolder;
        private Label LabelMaxDaysDiff;
        private Label LabelFolder;
        private Label LabelFile;
        private Label LabelFileOrFolder;
        private TextBox TextBoxNewMaxDiffDays;
        private ToolStripStatusLabel ToolStripStatusLabelMain;
        private ComboBox ComboBoxNewSource;
        private Label LabelOrder;
        private TextBox TextBoxNewOrder;
        private Label LabelTownship;
        private ComboBox ComboBoxNewTownship;
        private Label LabelSource;
        private Button ButtonNewCancel;
        private Button ButtonNewSave;
        private Button ButtonSelectFileOrFolder;
        private GroupBox GroupBoxOptionsSource;
        private RadioButton RadioButtonDeleteSource;
        private RadioButton RadioButtonAddSource;
        private Button ButtonOptionModifySource;
        private ComboBox ComboBoxOptionsSource;
        private GroupBox GroupBoxOptionsTownship;
        private Button ButtonOptionModifyTownship;
        private ComboBox ComboBoxOptionsTownship;
        private RadioButton RadioButtonDeleteTownship;
        private RadioButton RadioButtonAddTownship;
        private TextBox TextBoxNewComment;
        private Label LabelComment;
        private Panel PanelModifyBottom;
        private DataGridView DataGridViewModify;
        private Button ButtonModifySave;
        private Button ButtonModifyCancel;
        private Label LabelModifyWarning;
    }
}