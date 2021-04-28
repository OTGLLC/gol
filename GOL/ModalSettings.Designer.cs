
namespace GOL
{
    partial class ModalSettings
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.universeWidthText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.universeHeightText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.generationalDelayText = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(13, 268);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;

            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(328, 268);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Universe Width";
            // 
            // universeWidthText
            // 
            this.universeWidthText.Location = new System.Drawing.Point(139, 32);
            this.universeWidthText.Name = "universeWidthText";
            this.universeWidthText.Size = new System.Drawing.Size(41, 20);
            this.universeWidthText.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Universe Height";
            // 
            // universeHeightText
            // 
            this.universeHeightText.Location = new System.Drawing.Point(139, 60);
            this.universeHeightText.Name = "universeHeightText";
            this.universeHeightText.Size = new System.Drawing.Size(41, 20);
            this.universeHeightText.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Generational Delay";
            // 
            // generationalDelayText
            // 
            this.generationalDelayText.Location = new System.Drawing.Point(139, 89);
            this.generationalDelayText.Name = "generationalDelayText";
            this.generationalDelayText.Size = new System.Drawing.Size(41, 20);
            this.generationalDelayText.TabIndex = 7;
           
            // 
            // ModalSettings
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(415, 303);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.generationalDelayText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.universeHeightText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.universeWidthText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModalSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ModalSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox universeWidthText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox universeHeightText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox generationalDelayText;
        private System.Windows.Forms.Button okButton;
    }
}