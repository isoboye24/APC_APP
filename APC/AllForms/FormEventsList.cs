using APC.AllForms;
using APC.BLL;
using APC.DAL.DAO;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static APC.HelperServices.EventsHelperService;

namespace APC
{
    public partial class FormEventsList : Form
    {
        public FormEventsList()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormEvent open = new FormEvent();
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            FillDataGrid();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.EventID == 0)
            {
                MessageBox.Show("Please choose an event from the table.");
            }
            else
            {
                FormEvent open = new FormEvent();
                open.isUpdate = true;
                open.detail = detail;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                FillDataGrid();
            }            
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (detail.EventID == 0)
            {
                MessageBox.Show("Please choose an event from the table.");
            }
            else
            {
                FormEventDetailsBoard open = new FormEventDetailsBoard();
                open.isView = true;
                open.detail = detail;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                FillDataGrid();
            }            
        }
        EventsBLL bll = new EventsBLL();
        EventsDTO dto = new EventsDTO();
        private void FormEventsList_Load(object sender, EventArgs e)
        {
            label1.Tag = "resizable";
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label3.Tag = "resizable";
            label4.Tag = "resizable";
            label5.Tag = "resizable";
            label6.Tag = "resizable";
            labelOverallBalance.Tag = "resizable";
            labelOverallSold.Tag = "resizable";
            labelOverallSpent.Tag = "resizable";
            

            txtEventTitle.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtEventYear.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnAdd.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnUpdate.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnView.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            dto = bll.Select();

            dataGridView1.DataSource = dto.Events;
            ConfigureEventsGrid(dataGridView1, EventsGridType.Basic);


            decimal overallSales = bll.SelectOverallSales();
            decimal overallExpenditures = bll.SelectOverallExpenditures();

        }
        private void FillDataGrid()
        {
            bll = new EventsBLL();
            dto = bll.Select();
            dataGridView1.DataSource = dto.Events;
        }

        private void txtEventTitle_TextChanged(object sender, EventArgs e)
        {
            List<EventsDetailDTO> list = dto.Events;
            list = list.Where(x => x.EventTitle.Contains(txtEventTitle.Text)).ToList();
            dataGridView1.DataSource = list;
        }

        private void txtEventYear_TextChanged(object sender, EventArgs e)
        {
            List<EventsDetailDTO> list = dto.Events;
            list = list.Where(x => x.Year.Contains(txtEventYear.Text)).ToList();
            dataGridView1.DataSource = list;
        }

        private void txtEventYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = GeneralHelper.isNumber(e, (TextBox)sender);
        }
        EventsDetailDTO detail = new EventsDetailDTO();
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail = GeneralHelper.MapFromGrid<EventsDetailDTO>(dataGridView1, e.RowIndex);

            string imagePath = Path.Combine(Application.StartupPath, "images", detail.CoverImagePath);
            picViewEventCoverImage.ImageLocation = imagePath;

            label3.Text = detail.EventTitle + " " + detail.Year;
            labelOverallSold.Text = detail.AmountSold.ToString();
            labelOverallSpent.Text = detail.AmountSpent.ToString();
            labelOverallBalance.Text = detail.Balance.ToString();
        }
    }
}
