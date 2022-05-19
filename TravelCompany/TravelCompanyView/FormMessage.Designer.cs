
namespace TravelCompanyView
{
    partial class FormMessage
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
            this.LabelSender = new System.Windows.Forms.Label();
            this.LabelSubject = new System.Windows.Forms.Label();
            this.LabelReply = new System.Windows.Forms.Label();
            this.LabelText = new System.Windows.Forms.Label();
            this.ReplyTextRichBox = new System.Windows.Forms.RichTextBox();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.LabelSendDate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LabelSender
            // 
            this.LabelSender.AutoSize = true;
            this.LabelSender.Location = new System.Drawing.Point(129, 13);
            this.LabelSender.Name = "LabelSender";
            this.LabelSender.Size = new System.Drawing.Size(50, 20);
            this.LabelSender.TabIndex = 0;
            this.LabelSender.Text = "label1";
            // 
            // LabelSubject
            // 
            this.LabelSubject.AutoSize = true;
            this.LabelSubject.Location = new System.Drawing.Point(128, 48);
            this.LabelSubject.Name = "LabelSubject";
            this.LabelSubject.Size = new System.Drawing.Size(50, 20);
            this.LabelSubject.TabIndex = 1;
            this.LabelSubject.Text = "label1";
            // 
            // LabelReply
            // 
            this.LabelReply.AutoSize = true;
            this.LabelReply.Location = new System.Drawing.Point(14, 300);
            this.LabelReply.Name = "LabelReply";
            this.LabelReply.Size = new System.Drawing.Size(51, 20);
            this.LabelReply.TabIndex = 2;
            this.LabelReply.Text = "Ответ:";
            // 
            // LabelText
            // 
            this.LabelText.AutoSize = true;
            this.LabelText.Location = new System.Drawing.Point(12, 123);
            this.LabelText.Name = "LabelText";
            this.LabelText.Size = new System.Drawing.Size(50, 20);
            this.LabelText.TabIndex = 3;
            this.LabelText.Text = "label1";
            // 
            // ReplyTextRichBox
            // 
            this.ReplyTextRichBox.Location = new System.Drawing.Point(12, 333);
            this.ReplyTextRichBox.Name = "ReplyTextRichBox";
            this.ReplyTextRichBox.Size = new System.Drawing.Size(685, 169);
            this.ReplyTextRichBox.TabIndex = 4;
            this.ReplyTextRichBox.Text = "";
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(395, 523);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(129, 39);
            this.ButtonCancel.TabIndex = 5;
            this.ButtonCancel.Text = "Отмена";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(556, 523);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(129, 39);
            this.ButtonSave.TabIndex = 6;
            this.ButtonSave.Text = "Сохранить";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // LabelSendDate
            // 
            this.LabelSendDate.AutoSize = true;
            this.LabelSendDate.Location = new System.Drawing.Point(616, 13);
            this.LabelSendDate.Name = "LabelSendDate";
            this.LabelSendDate.Size = new System.Drawing.Size(50, 20);
            this.LabelSendDate.TabIndex = 7;
            this.LabelSendDate.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Текст:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Тема:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Отправитель:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(474, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Дата отправки:";
            // 
            // FormMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 574);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LabelSendDate);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ReplyTextRichBox);
            this.Controls.Add(this.LabelText);
            this.Controls.Add(this.LabelReply);
            this.Controls.Add(this.LabelSubject);
            this.Controls.Add(this.LabelSender);
            this.Name = "FormMessage";
            this.Text = "Сообщение";
            this.Load += new System.EventHandler(this.FormMessage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelSenser;
        private System.Windows.Forms.Label LabelSubject;
        private System.Windows.Forms.Label LabelReply;
        private System.Windows.Forms.Label LabelText;
        private System.Windows.Forms.RichTextBox ReplyTextRichBox;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.Label LabelSendDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LabelSender;
    }
}