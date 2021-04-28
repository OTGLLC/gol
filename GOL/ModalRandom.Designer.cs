
namespace GOL
{
    partial class ModalRandom
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
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.noSeedRandom = new System.Windows.Forms.RadioButton();
            this.seedRandom = new System.Windows.Forms.RadioButton();
            this.seedDisplayPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.seedTextBox = new System.Windows.Forms.TextBox();
            this.randomSeedButton = new System.Windows.Forms.Button();
            this.noRandom = new System.Windows.Forms.RadioButton();
            this.seedDisplayPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 246);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(281, 246);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // noSeedRandom
            // 
            this.noSeedRandom.AutoSize = true;
            this.noSeedRandom.Location = new System.Drawing.Point(129, 101);
            this.noSeedRandom.Name = "noSeedRandom";
            this.noSeedRandom.Size = new System.Drawing.Size(112, 17);
            this.noSeedRandom.TabIndex = 2;
            this.noSeedRandom.TabStop = true;
            this.noSeedRandom.Text = "Random (no seed)";
            this.noSeedRandom.UseVisualStyleBackColor = true;
            // 
            // seedRandom
            // 
            this.seedRandom.AutoSize = true;
            this.seedRandom.Location = new System.Drawing.Point(129, 124);
            this.seedRandom.Name = "seedRandom";
            this.seedRandom.Size = new System.Drawing.Size(105, 17);
            this.seedRandom.TabIndex = 3;
            this.seedRandom.TabStop = true;
            this.seedRandom.Text = "Seeded Random";
            this.seedRandom.UseVisualStyleBackColor = true;
            // 
            // seedDisplayPanel
            // 
            this.seedDisplayPanel.Controls.Add(this.randomSeedButton);
            this.seedDisplayPanel.Controls.Add(this.seedTextBox);
            this.seedDisplayPanel.Controls.Add(this.label1);
            this.seedDisplayPanel.Location = new System.Drawing.Point(12, 167);
            this.seedDisplayPanel.Name = "seedDisplayPanel";
            this.seedDisplayPanel.Size = new System.Drawing.Size(344, 30);
            this.seedDisplayPanel.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Seed:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // seedTextBox
            // 
            this.seedTextBox.Location = new System.Drawing.Point(48, 7);
            this.seedTextBox.Name = "seedTextBox";
            this.seedTextBox.Size = new System.Drawing.Size(223, 20);
            this.seedTextBox.TabIndex = 1;
            // 
            // randomSeedButton
            // 
            this.randomSeedButton.Location = new System.Drawing.Point(277, 7);
            this.randomSeedButton.Name = "randomSeedButton";
            this.randomSeedButton.Size = new System.Drawing.Size(64, 23);
            this.randomSeedButton.TabIndex = 2;
            this.randomSeedButton.Text = "Random";
            this.randomSeedButton.UseVisualStyleBackColor = true;
            this.randomSeedButton.Click += new System.EventHandler(this.randomSeedButton_Click);
            // 
            // noRandom
            // 
            this.noRandom.AutoSize = true;
            this.noRandom.Location = new System.Drawing.Point(129, 78);
            this.noRandom.Name = "noRandom";
            this.noRandom.Size = new System.Drawing.Size(113, 17);
            this.noRandom.TabIndex = 5;
            this.noRandom.TabStop = true;
            this.noRandom.Text = "Do not Randomize";
            this.noRandom.UseVisualStyleBackColor = true;
            // 
            // ModalRandom
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(371, 279);
            this.Controls.Add(this.noRandom);
            this.Controls.Add(this.seedDisplayPanel);
            this.Controls.Add(this.seedRandom);
            this.Controls.Add(this.noSeedRandom);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModalRandom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ModalRandom";
            this.seedDisplayPanel.ResumeLayout(false);
            this.seedDisplayPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.RadioButton noSeedRandom;
        private System.Windows.Forms.RadioButton seedRandom;
        private System.Windows.Forms.Panel seedDisplayPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox seedTextBox;
        private System.Windows.Forms.Button randomSeedButton;
        private System.Windows.Forms.RadioButton noRandom;
    }
}