namespace MissileAtackImitatorNS.View.Forms
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btChangePythonScriptFilename = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbPythonScriptFilename = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudPointsCount = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btApply = new System.Windows.Forms.Button();
            this.nudMissileVelocity = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointsCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMissileVelocity)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.btChangePythonScriptFilename, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.lbPythonScriptFilename, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.nudPointsCount, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.btApply, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.nudMissileVelocity, 1, 3);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 6;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(461, 458);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // btChangePythonScriptFilename
            // 
            this.btChangePythonScriptFilename.AutoSize = true;
            this.btChangePythonScriptFilename.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btChangePythonScriptFilename.Dock = System.Windows.Forms.DockStyle.Right;
            this.btChangePythonScriptFilename.Location = new System.Drawing.Point(390, 25);
            this.btChangePythonScriptFilename.Name = "btChangePythonScriptFilename";
            this.btChangePythonScriptFilename.Size = new System.Drawing.Size(68, 23);
            this.btChangePythonScriptFilename.TabIndex = 4;
            this.btChangePythonScriptFilename.Text = "Изменить";
            this.btChangePythonScriptFilename.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Путь к скрипту";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbPythonScriptFilename
            // 
            this.lbPythonScriptFilename.AutoSize = true;
            this.lbPythonScriptFilename.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbPythonScriptFilename.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPythonScriptFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbPythonScriptFilename.Location = new System.Drawing.Point(157, 0);
            this.lbPythonScriptFilename.Name = "lbPythonScriptFilename";
            this.lbPythonScriptFilename.Size = new System.Drawing.Size(301, 22);
            this.lbPythonScriptFilename.TabIndex = 1;
            this.lbPythonScriptFilename.Text = "none";
            this.lbPythonScriptFilename.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(3, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "Количество точек";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudPointsCount
            // 
            this.nudPointsCount.AutoSize = true;
            this.nudPointsCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudPointsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nudPointsCount.Location = new System.Drawing.Point(157, 54);
            this.nudPointsCount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudPointsCount.Name = "nudPointsCount";
            this.nudPointsCount.Size = new System.Drawing.Size(301, 26);
            this.nudPointsCount.TabIndex = 5;
            this.nudPointsCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(3, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 40);
            this.label3.TabIndex = 6;
            this.label3.Text = "Скорость ракеты\r\nпо умолчанию";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btApply
            // 
            this.btApply.Dock = System.Windows.Forms.DockStyle.Right;
            this.btApply.Location = new System.Drawing.Point(383, 126);
            this.btApply.Name = "btApply";
            this.btApply.Size = new System.Drawing.Size(75, 23);
            this.btApply.TabIndex = 7;
            this.btApply.Text = "ОК";
            this.btApply.UseVisualStyleBackColor = true;
            this.btApply.Click += new System.EventHandler(this.btApply_Click);
            // 
            // nudMissileVelocity
            // 
            this.nudMissileVelocity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudMissileVelocity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nudMissileVelocity.Location = new System.Drawing.Point(157, 86);
            this.nudMissileVelocity.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudMissileVelocity.Name = "nudMissileVelocity";
            this.nudMissileVelocity.Size = new System.Drawing.Size(301, 26);
            this.nudMissileVelocity.TabIndex = 8;
            this.nudMissileVelocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(461, 458);
            this.Controls.Add(this.tableLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "Настройки";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointsCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMissileVelocity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbPythonScriptFilename;
        private System.Windows.Forms.Button btChangePythonScriptFilename;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudPointsCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btApply;
        private System.Windows.Forms.NumericUpDown nudMissileVelocity;
    }
}