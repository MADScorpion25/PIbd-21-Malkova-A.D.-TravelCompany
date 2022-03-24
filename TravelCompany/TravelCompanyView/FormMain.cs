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
        public FormMain(OrderLogic orderLogic, ReportLogic reportLogic)
        {
            InitializeComponent();
            _orderLogic = orderLogic;
            _reportLogic = reportLogic;
            foreach (ToolStripMenuItem mainItem in menuStrip.Items)
            {
                if (mainItem.Text.Equals("Справочники"))
                {
                    mainItem.DropDownItems[0].Click += conditionToolStripMenuItem_Click;
                    mainItem.DropDownItems[1].Click += travelToolStripMenuItem_Click;
                    mainItem.DropDownItems[2].Click += warehousesToolStripMenuItem_Click;
                }
                else if(mainItem.Text.Equals("Пополнить склад"))
                {
                    mainItem.Click += warehouseAddToolStripMenuItem_Click;;
                }
                else
                {
                    mainItem.DropDownItems[0].Click += travelListToolStripMenuItem_Click;
                    mainItem.DropDownItems[1].Click += conditionTravelsToolStripMenuItem_Click;
                    mainItem.DropDownItems[2].Click += ordersListToolStripMenuItem_Click;
                    mainItem.DropDownItems[3].Click += warehousesListToolStripMenuItem_Click;
                    mainItem.DropDownItems[4].Click += warehouseConditionsToolStripMenuItem_Click;
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
                    dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
        private void travelListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //using var dialog = new SaveFileDialog { Filter = "docx|*.docx" };
            //if (dialog.ShowDialog() == DialogResult.OK)
            //{
            //    _reportLogic.SaveConditionsToWordFile(new ReportBindingModel
            //    {
            //        FileName = dialog.FileName
            //    });
            //    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
            //    MessageBoxIcon.Information);
            //}
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

        private void ButtonCreateOrder_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormCreateOrder>();
            form.ShowDialog();
            LoadData();
        }

        private void ButtonTakeInWork_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    _orderLogic.TakeOrderInWork(new ChangeStatusBindingModel { OrderId = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonOrderReady_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    _orderLogic.FinishOrder(new ChangeStatusBindingModel
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
