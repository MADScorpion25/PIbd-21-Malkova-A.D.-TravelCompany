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
        private readonly BackUpLogic _backUpLogic;
        public FormMain(OrderLogic orderLogic, ReportLogic reportLogic, ImplementerLogic impLogic, BackUpLogic backUpLogic)
        {
            InitializeComponent();
            _orderLogic = orderLogic;
            _reportLogic = reportLogic;
            _impLogic = impLogic;
            _backUpLogic = backUpLogic;
            foreach (ToolStripMenuItem mainItem in menuStrip.Items)
            {
                if (mainItem.Text.Equals("Справочники"))
                {
                    mainItem.DropDownItems[0].Click += conditionToolStripMenuItem_Click;
                    mainItem.DropDownItems[1].Click += travelToolStripMenuItem_Click;
                    mainItem.DropDownItems[2].Click += warehousesToolStripMenuItem_Click;
                    mainItem.DropDownItems[3].Click += clientToolStripMenuItem_Click;
                    mainItem.DropDownItems[4].Click += implementerToolStripMenuItem_Click;
                }
                else if(mainItem.Text.Equals("Пополнить склад"))
                {
                    mainItem.Click += warehouseAddToolStripMenuItem_Click;
                }
                else if(mainItem.Text.Equals("Отчеты"))
                {
                    mainItem.DropDownItems[0].Click += travelsListToolStripMenuItem_Click;
                    mainItem.DropDownItems[1].Click += conditionTravelsToolStripMenuItem_Click;
                    mainItem.DropDownItems[2].Click += ordersListToolStripMenuItem_Click;
                    mainItem.DropDownItems[3].Click += warehousesListToolStripMenuItem_Click;
                    mainItem.DropDownItems[4].Click += warehouseConditionsToolStripMenuItem_Click;
                    mainItem.DropDownItems[5].Click += ordersTotalToolStripMenuItem_Click;
                }
                else if(mainItem.Text.Equals("Запуск работы"))
                {
                    mainItem.Click += startWorkToolStripMenuItem_Click; 
                }
                else if(mainItem.Text.Equals("Сообщения"))
                {
                    mainItem.Click += showMessagesToolStripMenuItem_Click;
                }
                else
                {
                    mainItem.Click += createBackUpToolStripMenuItem_Click;
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
                Program.ConfigGrid(_orderLogic.Read(null), dataGridView);
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
        private void warehousesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormWarehouses>();
            form.ShowDialog();
        }
        private void warehouseAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormWarehouseCondition>();
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
        private void warehousesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog { Filter = "docx|*.docx" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _reportLogic.SaveWarehousesToWordFile(new ReportBindingModel
                {
                    FileName = dialog.FileName
                });
                MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
        }
        private void warehouseConditionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormReportWarehouseConditions>();
            form.ShowDialog();
        }
        private void ordersTotalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormReportTotalOrders>();
            form.ShowDialog();
        }
        private void createBackUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_backUpLogic != null)
                {
                    var fbd = new FolderBrowserDialog();
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        _backUpLogic.CreateBackUp(new
                        BackUpSaveBinidngModel
                        { FolderName = fbd.SelectedPath });
                        MessageBox.Show("Бекап создан", "Сообщение",
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void startWorkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var workModeling = Program.Container.Resolve<WorkModeling>();
            workModeling.DoWork(_impLogic, _orderLogic);
            MessageBox.Show("Работы запущены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void showMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormMessages>();
            form.ShowDialog();
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
