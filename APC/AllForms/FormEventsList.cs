using APC.AllForms;
using APC.BLL;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                FormEvenImages open = new FormEvenImages();
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
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            txtEventTitle.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtEventYear.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnAdd.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnUpdate.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnView.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            dto = bll.Select();

            dataGridView1.DataSource = dto.Events;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].HeaderText = "Year";
            dataGridView1.Columns[5].HeaderText = "Event Title";
            dataGridView1.Columns[6].HeaderText = "Summary";
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            }
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
            e.Handled = General.isNumber(e);
        }
        EventsDetailDTO detail = new EventsDetailDTO();
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail = new EventsDetailDTO();
            detail.EventID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            detail.Day = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            detail.MonthID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            detail.MonthName = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            detail.Year = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            detail.EventTitle = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            detail.Summary = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            detail.CoverImagePath = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            detail.EventDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
            string imagePath = Application.StartupPath + "\\images\\" + detail.CoverImagePath;
            picViewEventCoverImage.ImageLocation = imagePath;
        }
    }
}
