﻿namespace MissileAtackImitatorNS.View.Forms
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
                graphics.Dispose();
                bufferedGraphics.Dispose();
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
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
            this.splitContainer.Panel1.Controls.Add(this.dataGridView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.AutoScroll = true;
            this.splitContainer.Panel2.Controls.Add(this.pictureBox);
            this.splitContainer.Size = new System.Drawing.Size(875, 414);
            this.splitContainer.SplitterDistance = 277;
            this.splitContainer.TabIndex = 2;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.RowHeadersWidth = 4;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(273, 410);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView_RowPrePaint);
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
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "MissileAtackImitator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
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
        private System.Windows.Forms.DataGridView dataGridView;
    }
}

