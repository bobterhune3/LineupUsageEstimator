namespace LIneupUsageEstimator
{
    partial class LineupMgrDlg
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonRH = new System.Windows.Forms.RadioButton();
            this.radioButtonLH = new System.Windows.Forms.RadioButton();
            this.BTN_SAVE = new System.Windows.Forms.Button();
            this.BTN_CANCEL = new System.Windows.Forms.Button();
            this.LABEL_LINEUP_NAME = new System.Windows.Forms.Label();
            this.CB_FROM = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CB_TO = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonRH);
            this.groupBox1.Controls.Add(this.radioButtonLH);
            this.groupBox1.Location = new System.Drawing.Point(27, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(176, 163);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pitcher Arm";
            // 
            // radioButtonRH
            // 
            this.radioButtonRH.AutoSize = true;
            this.radioButtonRH.Location = new System.Drawing.Point(26, 87);
            this.radioButtonRH.Name = "radioButtonRH";
            this.radioButtonRH.Size = new System.Drawing.Size(116, 21);
            this.radioButtonRH.TabIndex = 1;
            this.radioButtonRH.TabStop = true;
            this.radioButtonRH.Text = "Right Handed";
            this.radioButtonRH.UseVisualStyleBackColor = true;
            this.radioButtonRH.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButtonLH
            // 
            this.radioButtonLH.AutoSize = true;
            this.radioButtonLH.Location = new System.Drawing.Point(26, 45);
            this.radioButtonLH.Name = "radioButtonLH";
            this.radioButtonLH.Size = new System.Drawing.Size(107, 21);
            this.radioButtonLH.TabIndex = 0;
            this.radioButtonLH.TabStop = true;
            this.radioButtonLH.Text = "Left Handed";
            this.radioButtonLH.UseVisualStyleBackColor = true;
            this.radioButtonLH.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // BTN_SAVE
            // 
            this.BTN_SAVE.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BTN_SAVE.Location = new System.Drawing.Point(381, 268);
            this.BTN_SAVE.Name = "BTN_SAVE";
            this.BTN_SAVE.Size = new System.Drawing.Size(75, 23);
            this.BTN_SAVE.TabIndex = 24;
            this.BTN_SAVE.Text = "SAVE";
            this.BTN_SAVE.UseVisualStyleBackColor = true;
            this.BTN_SAVE.Click += new System.EventHandler(this.BTN_SAVE_Click);
            // 
            // BTN_CANCEL
            // 
            this.BTN_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTN_CANCEL.Location = new System.Drawing.Point(264, 268);
            this.BTN_CANCEL.Name = "BTN_CANCEL";
            this.BTN_CANCEL.Size = new System.Drawing.Size(75, 23);
            this.BTN_CANCEL.TabIndex = 25;
            this.BTN_CANCEL.Text = "CANCEL";
            this.BTN_CANCEL.UseVisualStyleBackColor = true;
            this.BTN_CANCEL.Click += new System.EventHandler(this.BTN_CANCEL_Click);
            // 
            // LABEL_LINEUP_NAME
            // 
            this.LABEL_LINEUP_NAME.Location = new System.Drawing.Point(211, 24);
            this.LABEL_LINEUP_NAME.Name = "LABEL_LINEUP_NAME";
            this.LABEL_LINEUP_NAME.Size = new System.Drawing.Size(260, 23);
            this.LABEL_LINEUP_NAME.TabIndex = 26;
            this.LABEL_LINEUP_NAME.Text = "Not Set";
            this.LABEL_LINEUP_NAME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CB_FROM
            // 
            this.CB_FROM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_FROM.FormattingEnabled = true;
            this.CB_FROM.Items.AddRange(new object[] {
            "9L",
            "8L",
            "7L",
            "6L",
            "5L",
            "4L",
            "3L",
            "2L",
            "1L",
            "E",
            "1R",
            "2R",
            "3R",
            "4R",
            "5R",
            "6R",
            "7R",
            "8R",
            "9R"});
            this.CB_FROM.Location = new System.Drawing.Point(335, 92);
            this.CB_FROM.Name = "CB_FROM";
            this.CB_FROM.Size = new System.Drawing.Size(121, 24);
            this.CB_FROM.TabIndex = 27;
            this.CB_FROM.SelectedIndexChanged += new System.EventHandler(this.CB_FROM_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(283, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 28;
            this.label1.Text = "From:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(283, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 17);
            this.label2.TabIndex = 30;
            this.label2.Text = "To:";
            // 
            // CB_TO
            // 
            this.CB_TO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_TO.FormattingEnabled = true;
            this.CB_TO.Items.AddRange(new object[] {
            "9L",
            "8L",
            "7L",
            "6L",
            "5L",
            "4L",
            "3L",
            "2L",
            "1L",
            "E",
            "1R",
            "2R",
            "3R",
            "4R",
            "5R",
            "6R",
            "7R",
            "8R",
            "9R"});
            this.CB_TO.Location = new System.Drawing.Point(335, 161);
            this.CB_TO.Name = "CB_TO";
            this.CB_TO.Size = new System.Drawing.Size(121, 24);
            this.CB_TO.TabIndex = 29;
            this.CB_TO.SelectedIndexChanged += new System.EventHandler(this.CB_TO_SelectedIndexChanged);
            // 
            // LineupMgrDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 326);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CB_TO);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CB_FROM);
            this.Controls.Add(this.LABEL_LINEUP_NAME);
            this.Controls.Add(this.BTN_CANCEL);
            this.Controls.Add(this.BTN_SAVE);
            this.Controls.Add(this.groupBox1);
            this.Name = "LineupMgrDlg";
            this.Text = "Lineup Management Dialog";
            this.Load += new System.EventHandler(this.LineupMgrDlg_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonRH;
        private System.Windows.Forms.RadioButton radioButtonLH;
        private System.Windows.Forms.Button BTN_SAVE;
        private System.Windows.Forms.Button BTN_CANCEL;
        private System.Windows.Forms.Label LABEL_LINEUP_NAME;
        private System.Windows.Forms.ComboBox CB_FROM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CB_TO;
    }
}