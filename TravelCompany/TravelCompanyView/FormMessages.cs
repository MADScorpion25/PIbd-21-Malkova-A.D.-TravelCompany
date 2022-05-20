using System;
using System.Linq;
using System.Windows.Forms;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.BusinessLogicsContracts;
using Unity;

namespace TravelCompanyView
{
    public partial class FormMessages : Form
    {
        private bool hasNext = false;
        private readonly int PAGE_SIZE = 4;
        private int currentPage = 0;
        private readonly IMessageInfoLogic logic;
        public FormMessages(IMessageInfoLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }
        private void FormMessages_Load(object sender, EventArgs e)
        {
            PageLabel.Text = currentPage.ToString();
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                Program.ConfigGrid(logic.Read(new MessageInfoBindingModel
                {
                    ToSkip = currentPage * PAGE_SIZE,
                    ToTake = PAGE_SIZE + 1
                }), dataGridView);
                PageLabel.Text = currentPage.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonPrev_Click(object sender, EventArgs e)
        {
            if ((currentPage - 1) >= 0)
            {
                currentPage--;
                PageLabel.Text = (currentPage + 1).ToString();
                ButtonNext.Enabled = true;
                ButtonNext.Text = "Next " + (currentPage + 2);
                if (currentPage == 0)
                {
                    ButtonPrev.Enabled = false;
                    ButtonPrev.Text = "Prev";
                }
                else
                {
                    ButtonPrev.Text = "Prev " + (currentPage);
                }
                LoadData();
            }
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {
            if (hasNext)
            {
                currentPage++;
                PageLabel.Text = (currentPage + 1).ToString();
                ButtonPrev.Enabled = true;
                ButtonPrev.Text = "Prev " + (currentPage);
                LoadData();
            }
        }

        private void ButtonMailRef_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormMessage>();
                form.MessageId = dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                form.ShowDialog();
                LoadData();
            }
        }
    }
}
