using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.BusinessLogicsContracts;
using TravelCompanyContracts.ViewModels;
using Unity;

namespace TravelCompanyView
{
    public partial class FormTravel : Form
    {
        public int Id { set { id = value; } }
        private readonly ITravelLogic _logic;
        private int? id;
        private Dictionary<int, (string, int)> travelConditions;
        public FormTravel(ITravelLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void FormTravel_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    TravelViewModel view = _logic.Read(new TravelBindingModel
                    {
                        Id =
                   id.Value
                    })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.TravelName;
                        textBoxPrice.Text = view.Price.ToString();
                        travelConditions = view.TravelConditions;
                        LoadData();
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
                travelConditions = new Dictionary<int, (string, int)>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (travelConditions != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in travelConditions)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1, pc.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormTravelCondition>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (travelConditions.ContainsKey(form.Id))
                {
                    travelConditions[form.Id] = (form.ConditionName, form.Count);
                }
                else
                {
                    travelConditions.Add(form.Id, (form.ConditionName, form.Count));
                }
                LoadData();
            }
        }

        private void ButtonChange_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        travelConditions.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
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

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormTravelCondition>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = travelConditions[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    travelConditions[form.Id] = (form.ConditionName, form.Count);
                    LoadData();
                }
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (travelConditions == null || travelConditions.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new TravelBindingModel
                {
                    Id = id,
                    TravelName = textBoxName.Text,
                    Price = Convert.ToDecimal(textBoxPrice.Text),
                    TravelConditions = travelConditions
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

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
