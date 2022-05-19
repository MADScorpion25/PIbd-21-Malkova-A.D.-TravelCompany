using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TravelCompanyBusinessLogic.MailWorker;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.BusinessLogicsContracts;
using TravelCompanyContracts.Enums;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyView
{
    public partial class FormMessage : Form
    {
        public string MessageId
        {
            set { _messageId = value; }
        }
        private readonly IMessageInfoLogic _messageLogic;

        private readonly IClientLogic _clientLogic;

        private readonly AbstractMailWorker _mailWorker;
        private string _messageId;
        public FormMessage(IMessageInfoLogic messageLogic, IClientLogic clientLogic, AbstractMailWorker mailWorker)
        {
            InitializeComponent();
            _messageLogic = messageLogic;
            _clientLogic = clientLogic;
            _mailWorker = mailWorker;
        }

        private void FormMessage_Load(object sender, EventArgs e)
        {
            if (_messageId != null)
            {
                try
                {
                    MessageInfoViewModel view = _messageLogic.Read(new MessageInfoBindingModel { MessageId = _messageId })?[0];
                    if (view != null)
                    {
                        if (view.MessageStatus == MessageStatus.Не_просмотрено)
                        {
                            _messageLogic.CreateOrUpdate(new MessageInfoBindingModel
                            {
                                ClientId = _clientLogic.Read(new ClientBindingModel { Login = view.SenderName })?[0].Id,
                                MessageId = _messageId,
                                FromMailAddress = view.SenderName,
                                Subject = view.Subject,
                                Body = view.Body,
                                DateDelivery = view.DateDelivery,
                                MessageStatus = MessageStatus.Просмотрено,
                                ReplyText = view.ReplyText
                            });
                        }
                        LabelText.Text = view.Body;
                        LabelSender.Text = view.SenderName;
                        LabelSubject.Text = view.Subject;
                        LabelSendDate.Text = view.DateDelivery.ToString();
                        ReplyTextRichBox.Text = view.ReplyText;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ReplyTextRichBox.Text))
            {
                MessageBox.Show("Введите текст ответа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                _mailWorker.MailSendAsync(new MailSendInfoBindingModel
                {
                    MailAddress = LabelSender.Text,
                    Subject = "Ответ на: " + LabelSubject.Text,
                    Text = ReplyTextRichBox.Text
                });

                _messageLogic.CreateOrUpdate(new MessageInfoBindingModel
                {
                    ClientId = _clientLogic.Read(new ClientBindingModel { Login = LabelSender.Text })?[0].Id,
                    MessageId = _messageId,
                    FromMailAddress = LabelSender.Text,
                    Subject = LabelSubject.Text,
                    Body = LabelText.Text,
                    DateDelivery = DateTime.Parse(LabelSendDate.Text),
                    MessageStatus = MessageStatus.Просмотрено,
                    ReplyText = ReplyTextRichBox.Text
                });

                MessageBox.Show("Ответ отправлени", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
