using APC.BLL;
using APC.DAL;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace APC
{
    public partial class FormComments : Form
    {
        public FormComments()
        {
            InitializeComponent();
        }
        /// <summary>
        ///  Drag
        /// </summary>
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);
        
        private void panel1_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void FormComments_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void FormComments_Click(object sender, EventArgs e)
        {

        }
        CommentDTO dto = new CommentDTO();
        CommentBLL bll = new CommentBLL();
        MemberDetailDTO memberDetail = new MemberDetailDTO();
        public CommentDetailDTO detail = new CommentDetailDTO();
        public bool isUpdate = false;
        private void FormComments_Load(object sender, EventArgs e)
        {
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label4.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label5.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelCommentDate.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            labelCommentTime.Font = new Font("Segoe UI", 11, FontStyle.Regular);

            txtImagePath.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtName.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtSurname.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtComment.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtSurnameReadOnly.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSave.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            dto = bll.Select();
            #region
            dataGridView1.DataSource = dto.Members;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].HeaderText = "Surname";
            dataGridView1.Columns[4].HeaderText = "Name";
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[15].Visible = false;
            dataGridView1.Columns[16].Visible = false;
            dataGridView1.Columns[17].Visible = false;
            dataGridView1.Columns[18].Visible = false;
            dataGridView1.Columns[19].HeaderText = "Gender";
            dataGridView1.Columns[20].Visible = false;
            dataGridView1.Columns[21].Visible = false;
            dataGridView1.Columns[22].Visible = false;
            dataGridView1.Columns[23].Visible = false;
            dataGridView1.Columns[24].Visible = false;
            dataGridView1.Columns[25].Visible = false;
            dataGridView1.Columns[26].Visible = false;
            dataGridView1.Columns[27].Visible = false;
            dataGridView1.Columns[28].Visible = false;
            dataGridView1.Columns[29].Visible = false;
            dataGridView1.Columns[30].Visible = false;
            dataGridView1.Columns[31].Visible = false;
            dataGridView1.Columns[32].Visible = false;
            dataGridView1.Columns[33].Visible = false;
            dataGridView1.Columns[34].Visible = false;
            dataGridView1.Columns[35].Visible = false;
            dataGridView1.Columns[36].Visible = false;
            dataGridView1.Columns[37].Visible = false;
            dataGridView1.Columns[38].Visible = false;
            dataGridView1.Columns[39].Visible = false;
            dataGridView1.Columns[40].Visible = false;
            dataGridView1.Columns[41].Visible = false;
            dataGridView1.Columns[42].Visible = false;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            }
            #endregion
            labelCommentDate.Hide();
            labelCommentTime.Hide();

            if (isUpdate)
            {
                string imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
                picProfilePic.ImageLocation = imagePath;
                txtName.Text = detail.Name;
                txtImagePath.Text = detail.ImagePath;
                txtSurnameReadOnly.Text = detail.Surname;
                txtComment.Text = detail.CommentName;
                labelCommentDate.Text = detail.Day.ToString() + "/"+ detail.MonthID.ToString() + "/"+ detail.Year.ToString();
                memberDetail.MemberID = detail.MemberID;
                labelImagePath.Visible = true;
                txtImagePath.Visible = true;
                labelTitle.Text = "Edit Comment";
            }
            else
            {
                labelImagePath.Hide();
                txtImagePath.Hide();
                labelTitle.Text = "Add Comment";
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (!isUpdate)
            {
                if (memberDetail.MemberID == 0)
                {
                    MessageBox.Show("Please select member from the table");
                }
                else if (txtComment.Text.Trim() == "")
                {
                    MessageBox.Show("Comment is empty");
                }
                else
                {
                    CommentDetailDTO comment = new CommentDetailDTO();
                    comment.MemberID = memberDetail.MemberID;
                    comment.CommentName = txtComment.Text;
                    comment.Day = DateTime.Today.Day;
                    comment.MonthID = DateTime.Today.Month;
                    comment.Year = DateTime.Today.Year.ToString();
                    if (bll.Insert(comment))
                    {
                        MessageBox.Show("Comment was added");
                        txtComment.Clear();
                    }
                }                    
            }
            else if (isUpdate)
            {
                if (detail.Name == txtName.Text && detail.Surname == txtSurnameReadOnly.Text && detail.CommentName == txtComment.Text)
                {
                    MessageBox.Show("There is no change");
                }
                else
                {                    
                    detail.MemberID = detail.MemberID;
                    detail.CommentName = txtComment.Text;
                    detail.Day = DateTime.Today.Day;
                    detail.MonthID = DateTime.Today.Month;
                    detail.Year = DateTime.Today.Year.ToString();
                    if (bll.Update(detail))
                    {
                        MessageBox.Show("Comment was updated");
                        this.Close();
                    }
                }
            }            
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            memberDetail = new MemberDetailDTO();
            memberDetail.MemberID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtSurnameReadOnly.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            memberDetail.ImagePath = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            string imagePath = Application.StartupPath + "\\images\\" + memberDetail.ImagePath;
            picProfilePic.ImageLocation = imagePath;
        }

        private void txtSurname_TextChanged(object sender, EventArgs e)
        {
            List <MemberDetailDTO> list = dto.Members;
            list = list.Where(x => x.Surname.Contains(txtSurname.Text)).ToList();
            dataGridView1.DataSource = list;
        }

        private void iconMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void picMinimize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Minimized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }
    }
}
