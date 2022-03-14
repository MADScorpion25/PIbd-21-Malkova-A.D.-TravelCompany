using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.BusinessLogicsContracts;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyView
{
    public partial class FormWarehouseCondition : Form
    {
        public int ConditionId
        {
            get { return Convert.ToInt32(comboBoxCondition.SelectedValue); }
            set { comboBoxCondition.SelectedValue = value; }
        }
        public int WarehouseId
        {
            get { return Convert.ToInt32(comboBoxWarehouse.SelectedValue); }
            set { comboBoxCondition.SelectedValue = value; }
        }
        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set { comboBoxCondition.SelectedValue = value; }
        }
        IWarehouseLogic logicWarehouse;
        public FormWarehouseCondition(IWarehouseLogic logicWarehouse, IConditionLogic logicCondition)
        {
            InitializeComponent();
            List<ConditionViewModel> listCondition = logicCondition.Read(null);
            if (listCondition != null)
            {
                comboBoxCondition.DisplayMember = "ConditionName";
                comboBoxCondition.ValueMember = "Id";
                comboBoxCondition.DataSource = listCondition;
                comboBoxCondition.SelectedItem = null;
            }
            List<WarehouseViewModel> listWarehouse = logicWarehouse.Read(null);
            if (listWarehouse != null)
            {
                comboBoxWarehouse.DisplayMember = "WarehouseName";
                comboBoxWarehouse.ValueMember = "Id";
                comboBoxWarehouse.DataSource = listWarehouse;
                comboBoxWarehouse.SelectedItem = null;
            }
            this.logicWarehouse = logicWarehouse;
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxCondition.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (comboBoxWarehouse.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Введите количество", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            logicWarehouse.AddCondition(new WarehouseBindingModel { Id = WarehouseId }, ConditionId, Count);
            DialogResult = DialogResult.OK;
            Close();
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
