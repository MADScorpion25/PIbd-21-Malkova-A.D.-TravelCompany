using System;
using System.Windows.Forms;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.BusinessLogicsContracts;

namespace TravelCompanyView
{
    public partial class FormClients : Form
    {
        private readonly IClientLogic _logicС;
        public FormClients(IClientLogic logicC)
        {
            InitializeComponent();
            _logicС = logicC;
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id =
                    Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        _logicС.Delete(new ClientBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void FormClients_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                Program.ConfigGrid(_logicС.Read(null), dataGridView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
