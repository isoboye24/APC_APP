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

namespace APC.AllForms
{
    public partial class FormSingleCommentList : Form
    {
        public FormSingleCommentList()
        {
            InitializeComponent();
        }
        public CommentDetailDTO detail = new CommentDetailDTO();
        CommentDTO dto = new CommentDTO();
        CommentBLL bll = new CommentBLL();
        public int memberID;
        private void FormSingleCommentList_Load(object sender, EventArgs e)
        {
            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnView.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            dto = bll.SelectMembersCommentList(memberID);
            dataGridView1.DataSource = dto.Comments;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Comment";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].HeaderText = "Day";
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].HeaderText = "Month";
            dataGridView1.Columns[11].HeaderText = "Year";
            dataGridView1.Columns[12].Visible = false;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            }
            this.Text = detail.Surname + " " + detail.Name + "'s comments";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (detail.CommentID == 0)
            {
                MessageBox.Show("Please choose a comment from the table.");
            }
            else
            {
                FormViewComment open = new FormViewComment();
                open.detail = detail;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                dto = bll.SelectMembersCommentList(memberID);
                dataGridView1.DataSource = dto.Comments;
            }
        }

        private void dataGridView1_RowEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            detail = new CommentDetailDTO();
            detail.CommentID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            detail.CommentName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            detail.MemberID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            detail.Surname = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            detail.Name = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            detail.ImagePath = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            detail.GenderID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
            detail.GenderName = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            detail.Day = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
            detail.MonthID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
            detail.MonthName = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            detail.Year = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            detail.isMemberDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[12].Value);
        }
    }
}
