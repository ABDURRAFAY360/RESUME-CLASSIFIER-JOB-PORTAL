
namespace IPT_Project_Desktop
{
    partial class JobPost
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
            this.JobDescriptionBox = new System.Windows.Forms.RichTextBox();
            this.BackButton = new System.Windows.Forms.Button();
            this.PostButton = new System.Windows.Forms.Button();
            this.JobDesignationBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label4.Location = new System.Drawing.Point(12, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(183, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "Job Description:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Job Designation:";
            // 
            // JobDescriptionBox
            // 
            this.JobDescriptionBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JobDescriptionBox.Location = new System.Drawing.Point(17, 168);
            this.JobDescriptionBox.Name = "JobDescriptionBox";
            this.JobDescriptionBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.JobDescriptionBox.Size = new System.Drawing.Size(771, 334);
            this.JobDescriptionBox.TabIndex = 7;
            this.JobDescriptionBox.Text = "";
            // 
            // BackButton
            // 
            this.BackButton.BackColor = System.Drawing.Color.Lavender;
            this.BackButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackButton.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BackButton.Location = new System.Drawing.Point(124, 513);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(246, 38);
            this.BackButton.TabIndex = 28;
            this.BackButton.Text = "Back";
            this.BackButton.UseVisualStyleBackColor = false;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // PostButton
            // 
            this.PostButton.BackColor = System.Drawing.Color.Lavender;
            this.PostButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PostButton.ForeColor = System.Drawing.SystemColors.Desktop;
            this.PostButton.Location = new System.Drawing.Point(376, 513);
            this.PostButton.Name = "PostButton";
            this.PostButton.Size = new System.Drawing.Size(246, 38);
            this.PostButton.TabIndex = 29;
            this.PostButton.Text = "Post Job";
            this.PostButton.UseVisualStyleBackColor = false;
            this.PostButton.Click += new System.EventHandler(this.PostButton_Click);
            // 
            // JobDesignationBox
            // 
            this.JobDesignationBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JobDesignationBox.FormattingEnabled = true;
            this.JobDesignationBox.Items.AddRange(new object[] {
            "Java Developer",
            "Testing",
            "DevOps Engineer",
            "Python Developer",
            "Web Designing",
            "HR",
            "Hadoop",
            "Data Science",
            "Mechanical Engineer",
            "Sales",
            "Operations Manager",
            "ETL Developer",
            "Blockchain",
            "Arts",
            "Database",
            "Health and fitness",
            "Electrical Engineer",
            "PMO",
            "Business Analyst",
            "DotNet Developer",
            "Automation Testing",
            "Network Security Engineer",
            "Civil Engineer",
            "SAP Developer",
            "Advocate"});
            this.JobDesignationBox.Location = new System.Drawing.Point(17, 80);
            this.JobDesignationBox.Name = "JobDesignationBox";
            this.JobDesignationBox.Size = new System.Drawing.Size(771, 33);
            this.JobDesignationBox.TabIndex = 30;
            // 
            // JobPost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 563);
            this.Controls.Add(this.JobDesignationBox);
            this.Controls.Add(this.PostButton);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.JobDescriptionBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Name = "JobPost";
            this.Text = "Job Post";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox JobDescriptionBox;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Button PostButton;
        private System.Windows.Forms.ComboBox JobDesignationBox;
    }
}