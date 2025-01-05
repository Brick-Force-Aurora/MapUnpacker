namespace MapUnpacker
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.buttonLoadFile = new MaterialSkin.Controls.MaterialRaisedButton();
            this.pictureThumbnail = new System.Windows.Forms.PictureBox();
            this.buttonLoadAllFiles = new MaterialSkin.Controls.MaterialRaisedButton();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.comboMapSelection = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonExport = new MaterialSkin.Controls.MaterialRaisedButton();
            this.buttonExportObj = new MaterialSkin.Controls.MaterialRaisedButton();
            this.textBoxConsole = new System.Windows.Forms.RichTextBox();
            this.labelMapIdData = new MaterialSkin.Controls.MaterialLabel();
            this.labelVersionData = new MaterialSkin.Controls.MaterialLabel();
            this.labelOfficial = new MaterialSkin.Controls.MaterialLabel();
            this.labelCreatorData = new MaterialSkin.Controls.MaterialLabel();
            this.labelClan = new MaterialSkin.Controls.MaterialLabel();
            this.labelDateData = new MaterialSkin.Controls.MaterialLabel();
            this.labelDate = new MaterialSkin.Controls.MaterialLabel();
            this.labelClanData = new MaterialSkin.Controls.MaterialLabel();
            this.labelCreator = new MaterialSkin.Controls.MaterialLabel();
            this.labelOfficialData = new MaterialSkin.Controls.MaterialLabel();
            this.labelBlocks = new MaterialSkin.Controls.MaterialLabel();
            this.labelMapId = new MaterialSkin.Controls.MaterialLabel();
            this.labelAliasData = new MaterialSkin.Controls.MaterialLabel();
            this.labelAlias = new MaterialSkin.Controls.MaterialLabel();
            this.labelModesData = new MaterialSkin.Controls.MaterialLabel();
            this.labelModes = new MaterialSkin.Controls.MaterialLabel();
            this.checkSkipNoGeometry = new MaterialSkin.Controls.MaterialCheckBox();
            this.comboSortBy = new System.Windows.Forms.ComboBox();
            this.labelSortBy = new MaterialSkin.Controls.MaterialLabel();
            this.buttonCLeanFolder = new MaterialSkin.Controls.MaterialFlatButton();
            this.buttonExportAll = new MaterialSkin.Controls.MaterialRaisedButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureThumbnail)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.InitialDirectory = "Resources\\\\Maps";
            // 
            // buttonLoadFile
            // 
            this.buttonLoadFile.AutoSize = true;
            this.buttonLoadFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonLoadFile.Depth = 0;
            this.buttonLoadFile.Icon = null;
            this.buttonLoadFile.Location = new System.Drawing.Point(292, 26);
            this.buttonLoadFile.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonLoadFile.Name = "buttonLoadFile";
            this.buttonLoadFile.Primary = true;
            this.buttonLoadFile.Size = new System.Drawing.Size(48, 36);
            this.buttonLoadFile.TabIndex = 1;
            this.buttonLoadFile.Text = "File";
            this.buttonLoadFile.UseVisualStyleBackColor = true;
            this.buttonLoadFile.Click += new System.EventHandler(this.buttonLoadFile_Click);
            // 
            // pictureThumbnail
            // 
            this.pictureThumbnail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pictureThumbnail.Location = new System.Drawing.Point(0, 0);
            this.pictureThumbnail.Name = "pictureThumbnail";
            this.pictureThumbnail.Size = new System.Drawing.Size(128, 128);
            this.pictureThumbnail.TabIndex = 2;
            this.pictureThumbnail.TabStop = false;
            // 
            // buttonLoadAllFiles
            // 
            this.buttonLoadAllFiles.AutoSize = true;
            this.buttonLoadAllFiles.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonLoadAllFiles.Depth = 0;
            this.buttonLoadAllFiles.Icon = null;
            this.buttonLoadAllFiles.Location = new System.Drawing.Point(346, 26);
            this.buttonLoadAllFiles.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonLoadAllFiles.Name = "buttonLoadAllFiles";
            this.buttonLoadAllFiles.Primary = true;
            this.buttonLoadAllFiles.Size = new System.Drawing.Size(71, 36);
            this.buttonLoadAllFiles.TabIndex = 3;
            this.buttonLoadAllFiles.Text = "Folder";
            this.buttonLoadAllFiles.UseVisualStyleBackColor = true;
            this.buttonLoadAllFiles.Click += new System.EventHandler(this.buttonLoadAllFiles_Click);
            // 
            // comboMapSelection
            // 
            this.comboMapSelection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.comboMapSelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboMapSelection.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboMapSelection.ForeColor = System.Drawing.Color.White;
            this.comboMapSelection.FormattingEnabled = true;
            this.comboMapSelection.Location = new System.Drawing.Point(168, 68);
            this.comboMapSelection.Name = "comboMapSelection";
            this.comboMapSelection.Size = new System.Drawing.Size(128, 21);
            this.comboMapSelection.TabIndex = 4;
            this.comboMapSelection.SelectedIndexChanged += new System.EventHandler(this.comboMapSelection_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.pictureThumbnail);
            this.panel1.Location = new System.Drawing.Point(168, 92);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(128, 128);
            this.panel1.TabIndex = 6;
            // 
            // buttonExport
            // 
            this.buttonExport.AutoSize = true;
            this.buttonExport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonExport.Depth = 0;
            this.buttonExport.Icon = null;
            this.buttonExport.Location = new System.Drawing.Point(549, 26);
            this.buttonExport.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Primary = true;
            this.buttonExport.Size = new System.Drawing.Size(92, 36);
            this.buttonExport.TabIndex = 24;
            this.buttonExport.Text = "Export RE";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonExportObj
            // 
            this.buttonExportObj.AutoSize = true;
            this.buttonExportObj.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonExportObj.Depth = 0;
            this.buttonExportObj.Icon = null;
            this.buttonExportObj.Location = new System.Drawing.Point(647, 26);
            this.buttonExportObj.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonExportObj.Name = "buttonExportObj";
            this.buttonExportObj.Primary = true;
            this.buttonExportObj.Size = new System.Drawing.Size(51, 36);
            this.buttonExportObj.TabIndex = 25;
            this.buttonExportObj.Text = ".OBJ";
            this.buttonExportObj.UseVisualStyleBackColor = true;
            this.buttonExportObj.Click += new System.EventHandler(this.buttonExportObj_Click);
            // 
            // textBoxConsole
            // 
            this.textBoxConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.textBoxConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxConsole.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxConsole.ForeColor = System.Drawing.SystemColors.Window;
            this.textBoxConsole.Location = new System.Drawing.Point(12, 68);
            this.textBoxConsole.Name = "textBoxConsole";
            this.textBoxConsole.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.textBoxConsole.Size = new System.Drawing.Size(150, 152);
            this.textBoxConsole.TabIndex = 26;
            this.textBoxConsole.Text = "";
            this.textBoxConsole.TextChanged += new System.EventHandler(this.textBoxConsole_TextChanged);
            // 
            // labelMapIdData
            // 
            this.labelMapIdData.AutoSize = true;
            this.labelMapIdData.Depth = 0;
            this.labelMapIdData.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelMapIdData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelMapIdData.Location = new System.Drawing.Point(366, 87);
            this.labelMapIdData.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelMapIdData.Name = "labelMapIdData";
            this.labelMapIdData.Size = new System.Drawing.Size(13, 19);
            this.labelMapIdData.TabIndex = 16;
            this.labelMapIdData.Text = "-";
            // 
            // labelVersionData
            // 
            this.labelVersionData.AutoSize = true;
            this.labelVersionData.Depth = 0;
            this.labelVersionData.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelVersionData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelVersionData.Location = new System.Drawing.Point(366, 106);
            this.labelVersionData.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelVersionData.Name = "labelVersionData";
            this.labelVersionData.Size = new System.Drawing.Size(13, 19);
            this.labelVersionData.TabIndex = 17;
            this.labelVersionData.Text = "-";
            // 
            // labelOfficial
            // 
            this.labelOfficial.AutoSize = true;
            this.labelOfficial.Depth = 0;
            this.labelOfficial.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelOfficial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelOfficial.Location = new System.Drawing.Point(303, 201);
            this.labelOfficial.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelOfficial.Name = "labelOfficial";
            this.labelOfficial.Size = new System.Drawing.Size(57, 19);
            this.labelOfficial.TabIndex = 15;
            this.labelOfficial.Text = "Official";
            this.labelOfficial.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelCreatorData
            // 
            this.labelCreatorData.AutoSize = true;
            this.labelCreatorData.Depth = 0;
            this.labelCreatorData.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelCreatorData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelCreatorData.Location = new System.Drawing.Point(366, 125);
            this.labelCreatorData.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelCreatorData.Name = "labelCreatorData";
            this.labelCreatorData.Size = new System.Drawing.Size(13, 19);
            this.labelCreatorData.TabIndex = 18;
            this.labelCreatorData.Text = "-";
            // 
            // labelClan
            // 
            this.labelClan.AutoSize = true;
            this.labelClan.Depth = 0;
            this.labelClan.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelClan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelClan.Location = new System.Drawing.Point(303, 182);
            this.labelClan.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelClan.Name = "labelClan";
            this.labelClan.Size = new System.Drawing.Size(39, 19);
            this.labelClan.TabIndex = 14;
            this.labelClan.Text = "Clan";
            this.labelClan.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelDateData
            // 
            this.labelDateData.AutoSize = true;
            this.labelDateData.Depth = 0;
            this.labelDateData.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelDateData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelDateData.Location = new System.Drawing.Point(366, 144);
            this.labelDateData.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelDateData.Name = "labelDateData";
            this.labelDateData.Size = new System.Drawing.Size(13, 19);
            this.labelDateData.TabIndex = 19;
            this.labelDateData.Text = "-";
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Depth = 0;
            this.labelDate.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelDate.Location = new System.Drawing.Point(302, 144);
            this.labelDate.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(40, 19);
            this.labelDate.TabIndex = 12;
            this.labelDate.Text = "Date";
            this.labelDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelClanData
            // 
            this.labelClanData.AutoSize = true;
            this.labelClanData.Depth = 0;
            this.labelClanData.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelClanData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelClanData.Location = new System.Drawing.Point(366, 182);
            this.labelClanData.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelClanData.Name = "labelClanData";
            this.labelClanData.Size = new System.Drawing.Size(13, 19);
            this.labelClanData.TabIndex = 21;
            this.labelClanData.Text = "-";
            // 
            // labelCreator
            // 
            this.labelCreator.AutoSize = true;
            this.labelCreator.Depth = 0;
            this.labelCreator.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelCreator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelCreator.Location = new System.Drawing.Point(303, 125);
            this.labelCreator.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelCreator.Name = "labelCreator";
            this.labelCreator.Size = new System.Drawing.Size(59, 19);
            this.labelCreator.TabIndex = 11;
            this.labelCreator.Text = "Creator";
            this.labelCreator.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelOfficialData
            // 
            this.labelOfficialData.AutoSize = true;
            this.labelOfficialData.Depth = 0;
            this.labelOfficialData.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelOfficialData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelOfficialData.Location = new System.Drawing.Point(366, 201);
            this.labelOfficialData.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelOfficialData.Name = "labelOfficialData";
            this.labelOfficialData.Size = new System.Drawing.Size(13, 19);
            this.labelOfficialData.TabIndex = 22;
            this.labelOfficialData.Text = "-";
            // 
            // labelBlocks
            // 
            this.labelBlocks.AutoSize = true;
            this.labelBlocks.Depth = 0;
            this.labelBlocks.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelBlocks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelBlocks.Location = new System.Drawing.Point(302, 106);
            this.labelBlocks.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelBlocks.Name = "labelBlocks";
            this.labelBlocks.Size = new System.Drawing.Size(51, 19);
            this.labelBlocks.TabIndex = 10;
            this.labelBlocks.Text = "Bricks";
            this.labelBlocks.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelMapId
            // 
            this.labelMapId.AutoSize = true;
            this.labelMapId.Depth = 0;
            this.labelMapId.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelMapId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelMapId.Location = new System.Drawing.Point(302, 87);
            this.labelMapId.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelMapId.Name = "labelMapId";
            this.labelMapId.Size = new System.Drawing.Size(56, 19);
            this.labelMapId.TabIndex = 9;
            this.labelMapId.Text = "Map ID";
            this.labelMapId.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelAliasData
            // 
            this.labelAliasData.AutoSize = true;
            this.labelAliasData.Depth = 0;
            this.labelAliasData.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelAliasData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelAliasData.Location = new System.Drawing.Point(366, 68);
            this.labelAliasData.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelAliasData.Name = "labelAliasData";
            this.labelAliasData.Size = new System.Drawing.Size(13, 19);
            this.labelAliasData.TabIndex = 8;
            this.labelAliasData.Text = "-";
            // 
            // labelAlias
            // 
            this.labelAlias.AutoSize = true;
            this.labelAlias.Depth = 0;
            this.labelAlias.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelAlias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelAlias.Location = new System.Drawing.Point(302, 68);
            this.labelAlias.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelAlias.Name = "labelAlias";
            this.labelAlias.Size = new System.Drawing.Size(43, 19);
            this.labelAlias.TabIndex = 7;
            this.labelAlias.Text = "Alias";
            this.labelAlias.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelModesData
            // 
            this.labelModesData.AutoSize = true;
            this.labelModesData.Depth = 0;
            this.labelModesData.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelModesData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelModesData.Location = new System.Drawing.Point(366, 163);
            this.labelModesData.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelModesData.Name = "labelModesData";
            this.labelModesData.Size = new System.Drawing.Size(13, 19);
            this.labelModesData.TabIndex = 20;
            this.labelModesData.Text = "-";
            // 
            // labelModes
            // 
            this.labelModes.AutoSize = true;
            this.labelModes.Depth = 0;
            this.labelModes.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelModes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelModes.Location = new System.Drawing.Point(302, 163);
            this.labelModes.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelModes.Name = "labelModes";
            this.labelModes.Size = new System.Drawing.Size(55, 19);
            this.labelModes.TabIndex = 13;
            this.labelModes.Text = "Modes";
            this.labelModes.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // checkSkipNoGeometry
            // 
            this.checkSkipNoGeometry.AutoSize = true;
            this.checkSkipNoGeometry.Checked = true;
            this.checkSkipNoGeometry.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkSkipNoGeometry.Depth = 0;
            this.checkSkipNoGeometry.Font = new System.Drawing.Font("Roboto", 10F);
            this.checkSkipNoGeometry.Location = new System.Drawing.Point(512, 90);
            this.checkSkipNoGeometry.Margin = new System.Windows.Forms.Padding(0);
            this.checkSkipNoGeometry.MouseLocation = new System.Drawing.Point(-1, -1);
            this.checkSkipNoGeometry.MouseState = MaterialSkin.MouseState.HOVER;
            this.checkSkipNoGeometry.Name = "checkSkipNoGeometry";
            this.checkSkipNoGeometry.Ripple = true;
            this.checkSkipNoGeometry.Size = new System.Drawing.Size(197, 30);
            this.checkSkipNoGeometry.TabIndex = 27;
            this.checkSkipNoGeometry.Text = "Skip Missing Geometry File";
            this.checkSkipNoGeometry.UseVisualStyleBackColor = true;
            this.checkSkipNoGeometry.CheckedChanged += new System.EventHandler(this.checkSkipNoGeometry_CheckedChanged);
            // 
            // comboSortBy
            // 
            this.comboSortBy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.comboSortBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboSortBy.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboSortBy.ForeColor = System.Drawing.Color.White;
            this.comboSortBy.FormattingEnabled = true;
            this.comboSortBy.Items.AddRange(new object[] {
            "Map ID",
            "Alias",
            "Bricks",
            "Creator"});
            this.comboSortBy.Location = new System.Drawing.Point(569, 68);
            this.comboSortBy.Name = "comboSortBy";
            this.comboSortBy.Size = new System.Drawing.Size(129, 21);
            this.comboSortBy.TabIndex = 28;
            this.comboSortBy.SelectedIndexChanged += new System.EventHandler(this.comboSortBy_SelectedIndexChanged);
            // 
            // labelSortBy
            // 
            this.labelSortBy.AutoSize = true;
            this.labelSortBy.Depth = 0;
            this.labelSortBy.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelSortBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelSortBy.Location = new System.Drawing.Point(515, 71);
            this.labelSortBy.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelSortBy.Name = "labelSortBy";
            this.labelSortBy.Size = new System.Drawing.Size(57, 19);
            this.labelSortBy.TabIndex = 29;
            this.labelSortBy.Text = "Sort By";
            this.labelSortBy.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonCLeanFolder
            // 
            this.buttonCLeanFolder.AutoSize = true;
            this.buttonCLeanFolder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonCLeanFolder.Depth = 0;
            this.buttonCLeanFolder.Icon = null;
            this.buttonCLeanFolder.Location = new System.Drawing.Point(507, 120);
            this.buttonCLeanFolder.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.buttonCLeanFolder.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonCLeanFolder.Name = "buttonCLeanFolder";
            this.buttonCLeanFolder.Primary = false;
            this.buttonCLeanFolder.Size = new System.Drawing.Size(117, 36);
            this.buttonCLeanFolder.TabIndex = 30;
            this.buttonCLeanFolder.Text = "Clean Folder";
            this.buttonCLeanFolder.UseVisualStyleBackColor = true;
            this.buttonCLeanFolder.Click += new System.EventHandler(this.buttonCLeanFolder_Click);
            // 
            // buttonExportAll
            // 
            this.buttonExportAll.AutoSize = true;
            this.buttonExportAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonExportAll.Depth = 0;
            this.buttonExportAll.Icon = null;
            this.buttonExportAll.Location = new System.Drawing.Point(423, 26);
            this.buttonExportAll.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonExportAll.Name = "buttonExportAll";
            this.buttonExportAll.Primary = true;
            this.buttonExportAll.Size = new System.Drawing.Size(120, 36);
            this.buttonExportAll.TabIndex = 31;
            this.buttonExportAll.Text = "Export All RE";
            this.buttonExportAll.UseVisualStyleBackColor = true;
            this.buttonExportAll.Click += new System.EventHandler(this.buttonExportAll_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 226);
            this.Controls.Add(this.buttonExportAll);
            this.Controls.Add(this.buttonCLeanFolder);
            this.Controls.Add(this.labelSortBy);
            this.Controls.Add(this.comboSortBy);
            this.Controls.Add(this.checkSkipNoGeometry);
            this.Controls.Add(this.textBoxConsole);
            this.Controls.Add(this.buttonExportObj);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.labelOfficialData);
            this.Controls.Add(this.labelClanData);
            this.Controls.Add(this.labelModesData);
            this.Controls.Add(this.labelDateData);
            this.Controls.Add(this.labelCreatorData);
            this.Controls.Add(this.labelVersionData);
            this.Controls.Add(this.labelMapIdData);
            this.Controls.Add(this.labelOfficial);
            this.Controls.Add(this.labelClan);
            this.Controls.Add(this.labelModes);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.labelCreator);
            this.Controls.Add(this.labelBlocks);
            this.Controls.Add(this.labelMapId);
            this.Controls.Add(this.labelAliasData);
            this.Controls.Add(this.labelAlias);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.comboMapSelection);
            this.Controls.Add(this.buttonLoadAllFiles);
            this.Controls.Add(this.buttonLoadFile);
            this.Name = "Form1";
            this.Text = "Brick-Force Map Unpacker";
            ((System.ComponentModel.ISupportInitialize)(this.pictureThumbnail)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private MaterialSkin.Controls.MaterialRaisedButton buttonLoadFile;
        private System.Windows.Forms.PictureBox pictureThumbnail;
        private MaterialSkin.Controls.MaterialRaisedButton buttonLoadAllFiles;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ComboBox comboMapSelection;
        private System.Windows.Forms.Panel panel1;
        private MaterialSkin.Controls.MaterialRaisedButton buttonExport;
        private MaterialSkin.Controls.MaterialRaisedButton buttonExportObj;
        private System.Windows.Forms.RichTextBox textBoxConsole;
        private MaterialSkin.Controls.MaterialLabel labelMapIdData;
        private MaterialSkin.Controls.MaterialLabel labelVersionData;
        private MaterialSkin.Controls.MaterialLabel labelOfficial;
        private MaterialSkin.Controls.MaterialLabel labelCreatorData;
        private MaterialSkin.Controls.MaterialLabel labelClan;
        private MaterialSkin.Controls.MaterialLabel labelDateData;
        private MaterialSkin.Controls.MaterialLabel labelDate;
        private MaterialSkin.Controls.MaterialLabel labelClanData;
        private MaterialSkin.Controls.MaterialLabel labelCreator;
        private MaterialSkin.Controls.MaterialLabel labelOfficialData;
        public MaterialSkin.Controls.MaterialLabel labelBlocks;
        private MaterialSkin.Controls.MaterialLabel labelMapId;
        private MaterialSkin.Controls.MaterialLabel labelAliasData;
        private MaterialSkin.Controls.MaterialLabel labelAlias;
        private MaterialSkin.Controls.MaterialLabel labelModesData;
        private MaterialSkin.Controls.MaterialLabel labelModes;
        private MaterialSkin.Controls.MaterialCheckBox checkSkipNoGeometry;
        private System.Windows.Forms.ComboBox comboSortBy;
        private MaterialSkin.Controls.MaterialLabel labelSortBy;
        private MaterialSkin.Controls.MaterialFlatButton buttonCLeanFolder;
        private MaterialSkin.Controls.MaterialRaisedButton buttonExportAll;
    }
}

