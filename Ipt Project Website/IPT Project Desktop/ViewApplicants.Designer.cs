
namespace IPT_Project_Desktop
{
    partial class ViewApplicants
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
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.JobsBox = new System.Windows.Forms.ComboBox();
            this.CandidatesBox = new System.Windows.Forms.ComboBox();
            this.BackButton = new System.Windows.Forms.Button();
            this.ViewButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label4.Location = new System.Drawing.Point(12, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "Posted Jobs:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(12, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Candidates:";
            // 
            // JobsBox
            // 
            this.JobsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JobsBox.FormattingEnabled = true;
            this.JobsBox.Location = new System.Drawing.Point(167, 36);
            this.JobsBox.Name = "JobsBox";
            this.JobsBox.Size = new System.Drawing.Size(610, 33);
            this.JobsBox.TabIndex = 31;
            this.JobsBox.SelectedIndexChanged += new System.EventHandler(this.JobsBox_SelectedIndexChanged);
            // 
            // CandidatesBox
            // 
            this.CandidatesBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CandidatesBox.FormattingEnabled = true;
            this.CandidatesBox.Location = new System.Drawing.Point(167, 97);
            this.CandidatesBox.Name = "CandidatesBox";
            this.CandidatesBox.Size = new System.Drawing.Size(610, 33);
            this.CandidatesBox.TabIndex = 32;
            // 
            // BackButton
            // 
            this.BackButton.BackColor = System.Drawing.Color.Lavender;
            this.BackButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackButton.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BackButton.Location = new System.Drawing.Point(351, 215);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(246, 38);
            this.BackButton.TabIndex = 34;
            this.BackButton.Text = "Back";
            this.BackButton.UseVisualStyleBackColor = false;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // ViewButton
            // 
            this.ViewButton.BackColor = System.Drawing.Color.Lavender;
            this.ViewButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewButton.ForeColor = System.Drawing.SystemColors.Desktop;
            this.ViewButton.Location = new System.Drawing.Point(351, 162);
            this.ViewButton.Name = "ViewButton";
            this.ViewButton.Size = new System.Drawing.Size(246, 38);
            this.ViewButton.TabIndex = 35;
            this.ViewButton.Text = "View Resume";
            this.ViewButton.UseVisualStyleBackColor = false;
            this.ViewButton.Click += new System.EventHandler(this.ViewButton_Click);
            // 
            // ViewApplicants
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 293);
            this.Controls.Add(this.ViewButton);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.CandidatesBox);
            this.Controls.Add(this.JobsBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Name = "ViewApplicants";
            this.Text = "View Applicants";
            this.Load += new System.EventHandler(this.ViewApplicants_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox JobsBox;
        private System.Windows.Forms.ComboBox CandidatesBox;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Button ViewButton;
    }
}