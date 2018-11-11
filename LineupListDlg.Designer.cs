namespace LIneupUsageEstimator
{
    partial class LineupListDlg
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.BTN_ADD = new System.Windows.Forms.Button();
            this.BTN_DELETE = new System.Windows.Forms.Button();
            this.BTN_SAVE = new System.Windows.Forms.Button();
            this.BTN_CANCEL = new System.Windows.Forms.Button();
            this.BTN_EDIT = new System.Windows.Forms.Button();
            this.useAsTemplate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(67, 36);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(201, 244);
            this.listBox1.TabIndex = 0;
            this.listBox1.Click += new System.EventHandler(this.listBox1_Click);
            // 
            // BTN_ADD
            // 
            this.BTN_ADD.Location = new System.Drawing.Point(205, 286);
            this.BTN_ADD.Name = "BTN_ADD";
            this.BTN_ADD.Size = new System.Drawing.Size(63, 23);
            this.BTN_ADD.TabIndex = 22;
            this.BTN_ADD.Text = "Add";
            this.BTN_ADD.UseVisualStyleBackColor = true;
            this.BTN_ADD.Click += new System.EventHandler(this.BTN_ADD_Click);
            // 
            // BTN_DELETE
            // 
            this.BTN_DELETE.Location = new System.Drawing.Point(67, 286);
            this.BTN_DELETE.Name = "BTN_DELETE";
            this.BTN_DELETE.Size = new System.Drawing.Size(63, 23);
            this.BTN_DELETE.TabIndex = 23;
            this.BTN_DELETE.Text = "Delete";
            this.BTN_DELETE.UseVisualStyleBackColor = true;
            this.BTN_DELETE.Click += new System.EventHandler(this.BTN_DELETE_Click);
            // 
            // BTN_SAVE
            // 
            this.BTN_SAVE.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BTN_SAVE.Location = new System.Drawing.Point(192, 354);
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
            this.BTN_CANCEL.Location = new System.Drawing.Point(75, 354);
            this.BTN_CANCEL.Name = "BTN_CANCEL";
            this.BTN_CANCEL.Size = new System.Drawing.Size(75, 23);
            this.BTN_CANCEL.TabIndex = 25;
            this.BTN_CANCEL.Text = "CANCEL";
            this.BTN_CANCEL.UseVisualStyleBackColor = true;
            this.BTN_CANCEL.Visible = false;
            this.BTN_CANCEL.Click += new System.EventHandler(this.BTN_CANCEL_Click);
            // 
            // BTN_EDIT
            // 
            this.BTN_EDIT.Location = new System.Drawing.Point(136, 286);
            this.BTN_EDIT.Name = "BTN_EDIT";
            this.BTN_EDIT.Size = new System.Drawing.Size(63, 23);
            this.BTN_EDIT.TabIndex = 26;
            this.BTN_EDIT.Text = "Edit";
            this.BTN_EDIT.UseVisualStyleBackColor = true;
            this.BTN_EDIT.Click += new System.EventHandler(this.BTN_EDIT_Click);
            // 
            // useAsTemplate
            // 
            this.useAsTemplate.Location = new System.Drawing.Point(67, 315);
            this.useAsTemplate.Name = "useAsTemplate";
            this.useAsTemplate.Size = new System.Drawing.Size(200, 33);
            this.useAsTemplate.TabIndex = 27;
            this.useAsTemplate.Text = "Apply template for all teams";
            this.useAsTemplate.UseVisualStyleBackColor = true;
            this.useAsTemplate.Click += new System.EventHandler(this.useAsTemplate_Click);
            // 
            // LineupListDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 450);
            this.Controls.Add(this.useAsTemplate);
            this.Controls.Add(this.BTN_EDIT);
            this.Controls.Add(this.BTN_CANCEL);
            this.Controls.Add(this.BTN_SAVE);
            this.Controls.Add(this.BTN_DELETE);
            this.Controls.Add(this.BTN_ADD);
            this.Controls.Add(this.listBox1);
            this.Name = "LineupListDlg";
            this.Text = "Lineup List Dialog";
            this.Load += new System.EventHandler(this.LineupListDlg_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button BTN_ADD;
        private System.Windows.Forms.Button BTN_DELETE;
        private System.Windows.Forms.Button BTN_SAVE;
        private System.Windows.Forms.Button BTN_CANCEL;
        private System.Windows.Forms.Button BTN_EDIT;
        private System.Windows.Forms.Button useAsTemplate;
    }
}