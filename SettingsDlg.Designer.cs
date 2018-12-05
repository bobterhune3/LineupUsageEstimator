namespace LIneupUsageEstimator
{
    partial class SettingsDlg
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
            this.BTN_OK = new System.Windows.Forms.Button();
            this.BTN_CANCEL = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtABAddition = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // BTN_OK
            // 
            this.BTN_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BTN_OK.Location = new System.Drawing.Point(573, 401);
            this.BTN_OK.Name = "BTN_OK";
            this.BTN_OK.Size = new System.Drawing.Size(75, 23);
            this.BTN_OK.TabIndex = 0;
            this.BTN_OK.Text = "OK";
            this.BTN_OK.UseVisualStyleBackColor = true;
            this.BTN_OK.Click += new System.EventHandler(this.BTN_OK_Click);
            // 
            // BTN_CANCEL
            // 
            this.BTN_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTN_CANCEL.Location = new System.Drawing.Point(655, 400);
            this.BTN_CANCEL.Name = "BTN_CANCEL";
            this.BTN_CANCEL.Size = new System.Drawing.Size(75, 23);
            this.BTN_CANCEL.TabIndex = 1;
            this.BTN_CANCEL.Text = "CANCEL";
            this.BTN_CANCEL.UseVisualStyleBackColor = true;
            this.BTN_CANCEL.Click += new System.EventHandler(this.BTN_CANCEL_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Max Usage Additional AB";
            // 
            // txtABAddition
            // 
            this.txtABAddition.Location = new System.Drawing.Point(242, 73);
            this.txtABAddition.Name = "txtABAddition";
            this.txtABAddition.Size = new System.Drawing.Size(49, 22);
            this.txtABAddition.TabIndex = 3;
            this.txtABAddition.Text = "0";
            // 
            // SettingsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 429);
            this.Controls.Add(this.txtABAddition);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BTN_CANCEL);
            this.Controls.Add(this.BTN_OK);
            this.Name = "SettingsDlg";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTN_OK;
        private System.Windows.Forms.Button BTN_CANCEL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtABAddition;
    }
}