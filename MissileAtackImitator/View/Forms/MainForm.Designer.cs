namespace MissileAtackImitator.View.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbtPlay = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtAddAircraftPoints = new System.Windows.Forms.ToolStripButton();
            this.tsbtAddMissile = new System.Windows.Forms.ToolStripButton();
            this.tsbtCancellModes = new System.Windows.Forms.ToolStripButton();
            this.tsbtClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtSettings = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtPlay,
            this.toolStripSeparator1,
            this.tsbtAddAircraftPoints,
            this.tsbtAddMissile,
            this.tsbtCancellModes,
            this.tsbtClear,
            this.toolStripSeparator2,
            this.tsbtSettings});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(875, 50);
            this.toolStrip.TabIndex = 0;
            // 
            // tsbtPlay
            // 
            this.tsbtPlay.AutoSize = false;
            this.tsbtPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtPlay.Image = global::MissileAtackImitatorNS.Properties.Resources.Play;
            this.tsbtPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtPlay.Name = "tsbtPlay";
            this.tsbtPlay.Size = new System.Drawing.Size(47, 47);
            this.tsbtPlay.ToolTipText = "Начать имитацию";
            this.tsbtPlay.Click += new System.EventHandler(this.TsbtPlay_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 50);
            // 
            // tsbtAddAircraftPoints
            // 
            this.tsbtAddAircraftPoints.AutoSize = false;
            this.tsbtAddAircraftPoints.CheckOnClick = true;
            this.tsbtAddAircraftPoints.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtAddAircraftPoints.Image = global::MissileAtackImitatorNS.Properties.Resources.PlaceMarker;
            this.tsbtAddAircraftPoints.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtAddAircraftPoints.Name = "tsbtAddAircraftPoints";
            this.tsbtAddAircraftPoints.Size = new System.Drawing.Size(47, 47);
            this.tsbtAddAircraftPoints.Click += new System.EventHandler(this.TsbtAddAircraftPoints_Click);
            // 
            // tsbtAddMissile
            // 
            this.tsbtAddMissile.AutoSize = false;
            this.tsbtAddMissile.CheckOnClick = true;
            this.tsbtAddMissile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtAddMissile.Image = global::MissileAtackImitatorNS.Properties.Resources.Missile1;
            this.tsbtAddMissile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtAddMissile.Name = "tsbtAddMissile";
            this.tsbtAddMissile.Size = new System.Drawing.Size(47, 47);
            this.tsbtAddMissile.Click += new System.EventHandler(this.TsbtAddMissile_Click);
            // 
            // tsbtCancellModes
            // 
            this.tsbtCancellModes.AutoSize = false;
            this.tsbtCancellModes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtCancellModes.Image = global::MissileAtackImitatorNS.Properties.Resources.Cancell;
            this.tsbtCancellModes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtCancellModes.Name = "tsbtCancellModes";
            this.tsbtCancellModes.Size = new System.Drawing.Size(47, 47);
            this.tsbtCancellModes.Click += new System.EventHandler(this.TsbtCancellModes_Click);
            // 
            // tsbtClear
            // 
            this.tsbtClear.AutoSize = false;
            this.tsbtClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtClear.Image = global::MissileAtackImitatorNS.Properties.Resources.Cross;
            this.tsbtClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtClear.Name = "tsbtClear";
            this.tsbtClear.Size = new System.Drawing.Size(47, 47);
            this.tsbtClear.Click += new System.EventHandler(this.TsbtClear_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 50);
            // 
            // tsbtSettings
            // 
            this.tsbtSettings.AutoSize = false;
            this.tsbtSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtSettings.Image = global::MissileAtackImitatorNS.Properties.Resources.Settings;
            this.tsbtSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtSettings.Name = "tsbtSettings";
            this.tsbtSettings.Size = new System.Drawing.Size(47, 47);
            this.tsbtSettings.ToolTipText = "Настройки";
            this.tsbtSettings.Click += new System.EventHandler(this.TsBtSettings_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 464);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(875, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 50);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.AutoScroll = true;
            this.splitContainer.Panel2.Controls.Add(this.pictureBox);
            this.splitContainer.Size = new System.Drawing.Size(875, 414);
            this.splitContainer.SplitterDistance = 277;
            this.splitContainer.TabIndex = 2;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox.Location = new System.Drawing.Point(0, -2);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(583, 410);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(273, 410);
            this.dataGridView1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 486);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "MissileAtackImitator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripButton tsbtPlay;
        private System.Windows.Forms.ToolStripButton tsbtClear;
        private System.Windows.Forms.ToolStripButton tsbtSettings;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtAddAircraftPoints;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbtAddMissile;
        private System.Windows.Forms.ToolStripButton tsbtCancellModes;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

