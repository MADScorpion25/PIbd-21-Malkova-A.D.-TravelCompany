using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.BusinessLogicsContracts;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyView
{
    public partial class FormImplementer : Form
    {
        private readonly IImplementerLogic _impLogic;
        private int? id;
        public int Id { set { id = value; } }
        public FormImplementer(IImplementerLogic impLogic) 
        {
            InitializeComponent();
            _impLogic = impLogic;
        }

        private void FormImplementer_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ImplementerViewModel view = _impLogic.Read(new ImplementerBindingModel { Id = id.Value })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.ImplementerFIO;
                        textBoxWorkTime.Text = view.WorkingTime.ToString();
                        textBoxRestTime.Text = view.PauseTime.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxWorkTime.Text))
            {
                MessageBox.Show("Заполните время работы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxRestTime.Text))
            {
                MessageBox.Show("Заполните время отдыха", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                _impLogic.CreateOrUpdate(new ImplementerBindingModel
                {
                    Id = id,
                    ImplementerFIO = textBoxName.Text,
                    WorkingTime = Convert.ToInt32(textBoxWorkTime.Text),
                    PauseTime = Convert.ToInt32(textBoxRestTime.Text),
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
