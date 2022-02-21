using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.BusinessLogicsContracts;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyView
{
    public partial class FormWarehouse : Form
    {
        public int Id { set { id = value; } }
        private readonly IWarehouseLogic logic;
        private int? id;
        private Dictionary<int, (string, int)> warehouseConditions;
        public FormWarehouse(IWarehouseLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormWarehouse_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    WarehouseViewModel view = logic.Read(new WarehouseBindingModel { Id = id.Value })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.WarehouseName;
                        textBoxRespName.Text = view.ResponsibleFullName;
                        warehouseConditions = view.WarehouseConditions;
                        if (warehouseConditions != null)
                        {
                            dataGridView.Rows.Clear();
                            foreach (var pc in warehouseConditions)
                            {
                                dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1, pc.Value.Item2 });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                warehouseConditions = new Dictionary<int, (string, int)>();
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxRespName.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new WarehouseBindingModel
                {
                    Id = id,
                    WarehouseName = textBoxName.Text,
                    ResponsibleFullName = textBoxRespName.Text,
                    WarehouseConditions = warehouseConditions,
                    CreateDate = DateTime.Now
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
