
namespace TravelCompanyView
{
    partial class FormReportTotalOrders
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
            this.panel = new System.Windows.Forms.Panel();
            this.buttonForming = new System.Windows.Forms.Button();
            this.buttonToRdf = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.buttonForming);
            this.panel.Controls.Add(this.buttonToRdf);
            this.panel.Location = new System.Drawing.Point(1, 1);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1154, 56);
            this.panel.TabIndex = 7;
            // 
            // buttonForming
            // 
            this.buttonForming.Location = new System.Drawing.Point(11, 11);
            this.buttonForming.Name = "buttonForming";
            this.buttonForming.Size = new System.Drawing.Size(177, 29);
            this.buttonForming.TabIndex = 4;
            this.buttonForming.Text = "Сформировать";
            this.buttonForming.UseVisualStyleBackColor = true;
            this.buttonForming.Click += new System.EventHandler(this.buttonForming_Click);
            // 
            // buttonToRdf
            // 
            this.buttonToRdf.Location = new System.Drawing.Point(212, 11);
            this.buttonToRdf.Name = "buttonToRdf";
            this.buttonToRdf.Size = new System.Drawing.Size(177, 29);
            this.buttonToRdf.TabIndex = 5;
            this.buttonToRdf.Text = "В Pdf";
            this.buttonToRdf.UseVisualStyleBackColor = true;
            this.buttonToRdf.Click += new System.EventHandler(this.buttonToRdf_Click);
            // 
            // FormReportTotalOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1157, 566);
            this.Controls.Add(this.panel);
            this.Name = "FormReportTotalOrders";
            this.Text = "Заказы по датам";
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button buttonForming;
        private System.Windows.Forms.Button buttonToRdf;
    }
}