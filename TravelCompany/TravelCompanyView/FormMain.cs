using System;
using System.Windows.Forms;
using TravelCompanyBusinessLogic.BusinessLogics;
using TravelCompanyContracts.BindingModels;
using TravelCompanyFileImplement.Models;
using Unity;

namespace TravelCompanyView
{
    public partial class FormMain : Form
    {
        private readonly OrderLogic _orderLogic;
        private readonly ReportLogic _reportLogic;
        private readonly ImplementerLogic _impLogic;
        public FormMain(OrderLogic orderLogic, ReportLogic reportLogic, ImplementerLogic impLogic)
        {
            InitializeComponent();
            _orderLogic = orderLogic;
            _reportLogic = reportLogic;
            _impLogic = impLogic;
            foreach (ToolStripMenuItem mainItem in menuStrip.Items)
            {
                if (mainItem.Text.Equals("Справочники"))
                {
                    mainItem.DropDownItems[0].Click += conditionToolStripMenuItem_Click;
                    mainItem.DropDownItems[1].Click += travelToolStripMenuItem_Click;
                    mainItem.DropDownItems[2].Click += clientToolStripMenuItem_Click;
                    mainItem.DropDownItems[3].Click += implementerToolStripMenuItem_Click;
                }
                else if(mainItem.Text.Equals("Отчеты"))
                {
                    mainItem.DropDownItems[0].Click += travelsListToolStripMenuItem_Click;
                    mainItem.DropDownItems[1].Click += conditionTravelsToolStripMenuItem_Click;
                    mainItem.DropDownItems[2].Click += ordersListToolStripMenuItem_Click;
                }
                else
                {
                    mainItem.Click += startWorkToolStripMenuItem_Click; 
                }
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var list = _orderLogic.Read(null);
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].Visible = false;
                    dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void conditionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormConditions>();
            form.ShowDialog();
        }
        private void travelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormTravels>();
            form.ShowDialog();
        }
        private void clientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormClients>();
            form.ShowDialog();
        }
        private void implementerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormImplementers>();
            form.ShowDialog();
        }
        private void travelsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog { Filter = "docx|*.docx" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _reportLogic.SaveTravelsToWordFile(new ReportBindingModel
                {
                    FileName = dialog.FileName
                });
                MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
        }
        private void conditionTravelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormReportTravelConditions>();
            form.ShowDialog();
        }
        private void ordersListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormReportOrders>();
            form.ShowDialog();
        }
        private void startWorkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var workModeling = Program.Container.Resolve<WorkModeling>();
            workModeling.DoWork(_impLogic, _orderLogic);
            MessageBox.Show("Работы запущены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void ButtonCreateOrder_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormCreateOrder>();
            form.ShowDialog();
            LoadData();
        }

        private void ButtonOrderDelivered_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    _orderLogic.DeliveryOrder(new ChangeStatusBindingModel
                    {
                        OrderId = id
                    });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonUpdateList_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            FileDataListSingleton.GetInstance().Save();
        }
    }
}
