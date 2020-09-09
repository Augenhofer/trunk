namespace Feuchte_Rapport
{
    partial class FLogin
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
            this.Abbrechen = new System.Windows.Forms.Button();
            this.Anmelden = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.passText = new System.Windows.Forms.MaskedTextBox();
            this.cmbBoxUser = new System.Windows.Forms.ComboBox();
            this.labHint = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Abbrechen
            // 
            this.Abbrechen.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Abbrechen.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Abbrechen.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
            this.Abbrechen.Location = new System.Drawing.Point(228, 159);
            this.Abbrechen.Name = "Abbrechen";
            this.Abbrechen.Size = new System.Drawing.Size(109, 34);
            this.Abbrechen.TabIndex = 11;
            this.Abbrechen.Text = "Cancel";
            this.Abbrechen.UseVisualStyleBackColor = false;
            this.Abbrechen.Click += new System.EventHandler(this.Abbrechen_Click);
            // 
            // Anmelden
            // 
            this.Anmelden.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Anmelden.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
            this.Anmelden.Location = new System.Drawing.Point(93, 159);
            this.Anmelden.Name = "Anmelden";
            this.Anmelden.Size = new System.Drawing.Size(109, 34);
            this.Anmelden.TabIndex = 10;
            this.Anmelden.Text = "Ok";
            this.Anmelden.UseVisualStyleBackColor = false;
            this.Anmelden.Click += new System.EventHandler(this.Anmelden_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Window;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(94, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 23);
            this.label2.TabIndex = 9;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(94, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "User:";
            // 
            // passText
            // 
            this.passText.BackColor = System.Drawing.SystemColors.Window;
            this.passText.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
            this.passText.Location = new System.Drawing.Point(203, 111);
            this.passText.Name = "passText";
            this.passText.PasswordChar = 'x';
            this.passText.Size = new System.Drawing.Size(134, 31);
            this.passText.TabIndex = 7;
            this.passText.UseSystemPasswordChar = true;
            // 
            // cmbBoxUser
            // 
            this.cmbBoxUser.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
            this.cmbBoxUser.FormattingEnabled = true;
            this.cmbBoxUser.Location = new System.Drawing.Point(203, 69);
            this.cmbBoxUser.Name = "cmbBoxUser";
            this.cmbBoxUser.Size = new System.Drawing.Size(134, 31);
            this.cmbBoxUser.TabIndex = 12;
            // 
            // labHint
            // 
            this.labHint.AutoSize = true;
            this.labHint.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
            this.labHint.Location = new System.Drawing.Point(56, 207);
            this.labHint.Name = "labHint";
            this.labHint.Size = new System.Drawing.Size(345, 23);
            this.labHint.TabIndex = 13;
            this.labHint.Text = "User oder Password sind nicht korrekt !!!!!";
            // 
            // FLogin
            // 
            this.AcceptButton = this.Anmelden;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.Abbrechen;
            this.ClientSize = new System.Drawing.Size(435, 259);
            this.Controls.Add(this.labHint);
            this.Controls.Add(this.cmbBoxUser);
            this.Controls.Add(this.Abbrechen);
            this.Controls.Add(this.Anmelden);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FLogin_FormClosed);
            this.Load += new System.EventHandler(this.FLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Abbrechen;
        private System.Windows.Forms.Button Anmelden;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox passText;
        private System.Windows.Forms.ComboBox cmbBoxUser;
        private System.Windows.Forms.Label labHint;
    }
}