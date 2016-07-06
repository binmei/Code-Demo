namespace ParsingExcelFile
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.browseInput = new System.Windows.Forms.Button();
            this.runCalculation = new System.Windows.Forms.Button();
            this.navigateOutput = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "2. Browse for Input File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "3. Click Run to Calculate Results";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "4. Save Output File";
            // 
            // browseInput
            // 
            this.browseInput.Location = new System.Drawing.Point(200, 60);
            this.browseInput.Name = "browseInput";
            this.browseInput.Size = new System.Drawing.Size(99, 23);
            this.browseInput.TabIndex = 3;
            this.browseInput.Text = "Browse File";
            this.browseInput.UseVisualStyleBackColor = true;
            this.browseInput.Click += new System.EventHandler(this.browseInput_Click);
            // 
            // runCalculation
            // 
            this.runCalculation.Location = new System.Drawing.Point(200, 93);
            this.runCalculation.Name = "runCalculation";
            this.runCalculation.Size = new System.Drawing.Size(99, 23);
            this.runCalculation.TabIndex = 4;
            this.runCalculation.Text = "Run";
            this.runCalculation.UseVisualStyleBackColor = true;
            this.runCalculation.Click += new System.EventHandler(this.runCalculation_Click);
            // 
            // navigateOutput
            // 
            this.navigateOutput.Location = new System.Drawing.Point(199, 122);
            this.navigateOutput.Name = "navigateOutput";
            this.navigateOutput.Size = new System.Drawing.Size(99, 23);
            this.navigateOutput.TabIndex = 5;
            this.navigateOutput.Text = "Save";
            this.navigateOutput.UseVisualStyleBackColor = true;
            this.navigateOutput.Click += new System.EventHandler(this.navigateOutput_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(199, 31);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "1. Enter Work Sheet Name Here:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 181);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.navigateOutput);
            this.Controls.Add(this.runCalculation);
            this.Controls.Add(this.browseInput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "ParseExelFile";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button browseInput;
        private System.Windows.Forms.Button runCalculation;
        private System.Windows.Forms.Button navigateOutput;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
    }
}

