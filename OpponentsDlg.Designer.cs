namespace LIneupUsageEstimator
{
    partial class OpponentsDlg
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.BTN_SAVE = new System.Windows.Forms.Button();
            this.BTN_CANCEL = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TXT_IN_DIV_COUNT = new System.Windows.Forms.TextBox();
            this.TXT_OUT_DIV_COUNT = new System.Windows.Forms.TextBox();
            this.LABEL_IN_TOTAL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.27197F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.72803F));
            this.tableLayoutPanel.Location = new System.Drawing.Point(12, 60);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(286, 767);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // BTN_SAVE
            // 
            this.BTN_SAVE.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BTN_SAVE.Location = new System.Drawing.Point(428, 60);
            this.BTN_SAVE.Name = "BTN_SAVE";
            this.BTN_SAVE.Size = new System.Drawing.Size(75, 23);
            this.BTN_SAVE.TabIndex = 1;
            this.BTN_SAVE.Text = "Save";
            this.BTN_SAVE.UseVisualStyleBackColor = true;
            this.BTN_SAVE.Click += new System.EventHandler(this.BTN_SAVE_Click);
            // 
            // BTN_CANCEL
            // 
            this.BTN_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTN_CANCEL.Location = new System.Drawing.Point(428, 89);
            this.BTN_CANCEL.Name = "BTN_CANCEL";
            this.BTN_CANCEL.Size = new System.Drawing.Size(75, 23);
            this.BTN_CANCEL.TabIndex = 2;
            this.BTN_CANCEL.Text = "Cancel";
            this.BTN_CANCEL.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Number of games played vs each team:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(343, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "In Division Count";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(331, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Out Division Count";
            // 
            // TXT_IN_DIV_COUNT
            // 
            this.TXT_IN_DIV_COUNT.Location = new System.Drawing.Point(462, 179);
            this.TXT_IN_DIV_COUNT.Name = "TXT_IN_DIV_COUNT";
            this.TXT_IN_DIV_COUNT.Size = new System.Drawing.Size(47, 22);
            this.TXT_IN_DIV_COUNT.TabIndex = 7;
            this.TXT_IN_DIV_COUNT.Text = "8";
            this.TXT_IN_DIV_COUNT.TextChanged += new System.EventHandler(this.TXT_DIV_COUNT_TextChanged);
            // 
            // TXT_OUT_DIV_COUNT
            // 
            this.TXT_OUT_DIV_COUNT.Location = new System.Drawing.Point(462, 210);
            this.TXT_OUT_DIV_COUNT.Name = "TXT_OUT_DIV_COUNT";
            this.TXT_OUT_DIV_COUNT.Size = new System.Drawing.Size(47, 22);
            this.TXT_OUT_DIV_COUNT.TabIndex = 8;
            this.TXT_OUT_DIV_COUNT.Text = "6";
            this.TXT_OUT_DIV_COUNT.TextChanged += new System.EventHandler(this.TXT_DIV_COUNT_TextChanged);
            // 
            // LABEL_IN_TOTAL
            // 
            this.LABEL_IN_TOTAL.AutoSize = true;
            this.LABEL_IN_TOTAL.Location = new System.Drawing.Point(343, 264);
            this.LABEL_IN_TOTAL.Name = "LABEL_IN_TOTAL";
            this.LABEL_IN_TOTAL.Size = new System.Drawing.Size(16, 17);
            this.LABEL_IN_TOTAL.TabIndex = 9;
            this.LABEL_IN_TOTAL.Text = "0";
            // 
            // OpponentsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 839);
            this.Controls.Add(this.LABEL_IN_TOTAL);
            this.Controls.Add(this.TXT_OUT_DIV_COUNT);
            this.Controls.Add(this.TXT_IN_DIV_COUNT);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BTN_CANCEL);
            this.Controls.Add(this.BTN_SAVE);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "OpponentsDlg";
            this.Text = "Games Played Vs Opponents";
            this.Load += new System.EventHandler(this.Opponents_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button BTN_SAVE;
        private System.Windows.Forms.Button BTN_CANCEL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TXT_IN_DIV_COUNT;
        private System.Windows.Forms.TextBox TXT_OUT_DIV_COUNT;
        private System.Windows.Forms.Label LABEL_IN_TOTAL;
    }
}