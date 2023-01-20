using System.Windows.Forms;

namespace wxDCS_Injector.Presentation
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.grpFile = new System.Windows.Forms.GroupBox();
            this.btnFile = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.dlgFile = new System.Windows.Forms.OpenFileDialog();
            this.btnExit = new System.Windows.Forms.Button();
            this.grpWeather = new System.Windows.Forms.GroupBox();
            this.btnICAO = new System.Windows.Forms.Button();
            this.txtICAO = new System.Windows.Forms.TextBox();
            this.grpMETAR = new System.Windows.Forms.GroupBox();
            this.txtMETAR = new System.Windows.Forms.TextBox();
            this.btnInject = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.btnLog = new System.Windows.Forms.Button();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.miFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miLog = new System.Windows.Forms.ToolStripMenuItem();
            this.miSave = new System.Windows.Forms.ToolStripMenuItem();
            this.miOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.miCurrentDate = new System.Windows.Forms.ToolStripMenuItem();
            this.miCurrentTime = new System.Windows.Forms.ToolStripMenuItem();
            this.grpFile.SuspendLayout();
            this.grpWeather.SuspendLayout();
            this.grpMETAR.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpFile
            // 
            this.grpFile.Controls.Add(this.btnFile);
            this.grpFile.Controls.Add(this.txtFile);
            this.grpFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFile.Location = new System.Drawing.Point(12, 28);
            this.grpFile.Name = "grpFile";
            this.grpFile.Size = new System.Drawing.Size(220, 83);
            this.grpFile.TabIndex = 0;
            this.grpFile.TabStop = false;
            this.grpFile.Text = "DCS mission file";
            // 
            // btnFile
            // 
            this.btnFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFile.Location = new System.Drawing.Point(7, 47);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(200, 23);
            this.btnFile.TabIndex = 1;
            this.btnFile.Text = "Select &File...";
            this.ttMain.SetToolTip(this.btnFile, "Select File");
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // txtFile
            // 
            this.txtFile.Enabled = false;
            this.txtFile.Location = new System.Drawing.Point(7, 20);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(200, 20);
            this.txtFile.TabIndex = 0;
            this.txtFile.TabStop = false;
            this.txtFile.Text = "No file selected";
            this.txtFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFile.WordWrap = false;
            // 
            // dlgFile
            // 
            this.dlgFile.RestoreDirectory = true;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(303, 204);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(50, 23);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "E&xit";
            this.ttMain.SetToolTip(this.btnExit, "Exit");
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // grpWeather
            // 
            this.grpWeather.Controls.Add(this.btnICAO);
            this.grpWeather.Controls.Add(this.txtICAO);
            this.grpWeather.Location = new System.Drawing.Point(238, 28);
            this.grpWeather.Name = "grpWeather";
            this.grpWeather.Size = new System.Drawing.Size(115, 83);
            this.grpWeather.TabIndex = 2;
            this.grpWeather.TabStop = false;
            this.grpWeather.Text = "ICAO";
            // 
            // btnICAO
            // 
            this.btnICAO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnICAO.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnICAO.Location = new System.Drawing.Point(6, 46);
            this.btnICAO.Name = "btnICAO";
            this.btnICAO.Size = new System.Drawing.Size(100, 23);
            this.btnICAO.TabIndex = 3;
            this.btnICAO.Text = "&Get METAR";
            this.ttMain.SetToolTip(this.btnICAO, "Get ICAO");
            this.btnICAO.UseVisualStyleBackColor = true;
            this.btnICAO.Click += new System.EventHandler(this.btnICAO_Click);
            // 
            // txtICAO
            // 
            this.txtICAO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtICAO.Location = new System.Drawing.Point(7, 20);
            this.txtICAO.MaxLength = 4;
            this.txtICAO.Name = "txtICAO";
            this.txtICAO.Size = new System.Drawing.Size(100, 20);
            this.txtICAO.TabIndex = 2;
            this.txtICAO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtICAO.WordWrap = false;
            this.txtICAO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtICAO_KeyDown);
            // 
            // grpMETAR
            // 
            this.grpMETAR.Controls.Add(this.txtMETAR);
            this.grpMETAR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpMETAR.Location = new System.Drawing.Point(12, 117);
            this.grpMETAR.Name = "grpMETAR";
            this.grpMETAR.Size = new System.Drawing.Size(341, 81);
            this.grpMETAR.TabIndex = 3;
            this.grpMETAR.TabStop = false;
            this.grpMETAR.Text = "METAR";
            // 
            // txtMETAR
            // 
            this.txtMETAR.Enabled = false;
            this.txtMETAR.Location = new System.Drawing.Point(6, 19);
            this.txtMETAR.Multiline = true;
            this.txtMETAR.Name = "txtMETAR";
            this.txtMETAR.ReadOnly = true;
            this.txtMETAR.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMETAR.Size = new System.Drawing.Size(327, 56);
            this.txtMETAR.TabIndex = 0;
            this.txtMETAR.TabStop = false;
            // 
            // btnInject
            // 
            this.btnInject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInject.Enabled = false;
            this.btnInject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInject.Location = new System.Drawing.Point(12, 204);
            this.btnInject.Name = "btnInject";
            this.btnInject.Size = new System.Drawing.Size(100, 23);
            this.btnInject.TabIndex = 4;
            this.btnInject.Text = "&Inject";
            this.ttMain.SetToolTip(this.btnInject, "Inject");
            this.btnInject.UseVisualStyleBackColor = false;
            this.btnInject.Click += new System.EventHandler(this.btnInject_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(118, 204);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 23);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "&Clear";
            this.ttMain.SetToolTip(this.btnClear, "Clear");
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnLog
            // 
            this.btnLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLog.Location = new System.Drawing.Point(247, 204);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(50, 23);
            this.btnLog.TabIndex = 7;
            this.btnLog.Text = "&Log";
            this.ttMain.SetToolTip(this.btnLog, "Open Log");
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.miLog,
            this.miOptions});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(362, 24);
            this.mnuMain.TabIndex = 8;
            this.mnuMain.Text = "menuStrip1";
            // 
            // miFile
            // 
            this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miExit});
            this.miFile.Name = "miFile";
            this.miFile.Size = new System.Drawing.Size(37, 20);
            this.miFile.Text = "&File";
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(93, 22);
            this.miExit.Text = "E&xit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miLog
            // 
            this.miLog.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSave});
            this.miLog.Name = "miLog";
            this.miLog.Size = new System.Drawing.Size(39, 20);
            this.miLog.Text = "&Log";
            // 
            // miSave
            // 
            this.miSave.Name = "miSave";
            this.miSave.Size = new System.Drawing.Size(180, 22);
            this.miSave.Text = "&Save To File";
            this.miSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // miOptions
            // 
            this.miOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCurrentDate,
            this.miCurrentTime});
            this.miOptions.Name = "miOptions";
            this.miOptions.Size = new System.Drawing.Size(61, 20);
            this.miOptions.Text = "&Options";
            // 
            // miCurrentDate
            // 
            this.miCurrentDate.Name = "miCurrentDate";
            this.miCurrentDate.Size = new System.Drawing.Size(180, 22);
            this.miCurrentDate.Text = "&Current Date";
            this.miCurrentDate.Click += new System.EventHandler(this.miCurrentDate_Click);
            // 
            // miCurrentTime
            // 
            this.miCurrentTime.Name = "miCurrentTime";
            this.miCurrentTime.Size = new System.Drawing.Size(180, 22);
            this.miCurrentTime.Text = "Current Time";
            this.miCurrentTime.Click += new System.EventHandler(this.miCurrentTime_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 237);
            this.Controls.Add(this.btnLog);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnInject);
            this.Controls.Add(this.grpMETAR);
            this.Controls.Add(this.grpWeather);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.grpFile);
            this.Controls.Add(this.mnuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "wxDCS-Injector";
            this.grpFile.ResumeLayout(false);
            this.grpFile.PerformLayout();
            this.grpWeather.ResumeLayout(false);
            this.grpWeather.PerformLayout();
            this.grpMETAR.ResumeLayout(false);
            this.grpMETAR.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFile;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.OpenFileDialog dlgFile;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox grpWeather;
        private System.Windows.Forms.Button btnICAO;
        private System.Windows.Forms.TextBox txtICAO;
        private System.Windows.Forms.GroupBox grpMETAR;
        private System.Windows.Forms.TextBox txtMETAR;
        private Button btnInject;
        private Button btnClear;
        private ToolTip ttMain;
        private Button btnLog;
        private MenuStrip mnuMain;
        private ToolStripMenuItem miFile;
        private ToolStripMenuItem miExit;
        private ToolStripMenuItem miLog;
        private ToolStripMenuItem miSave;
        private ToolStripMenuItem miOptions;
        private ToolStripMenuItem miCurrentDate;
        private ToolStripMenuItem miCurrentTime;
    }
}

