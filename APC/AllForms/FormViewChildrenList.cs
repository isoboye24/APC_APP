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
using System.Xml.Linq;
using APC.BLL;
using APC.DAL.DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace APC.AllForms
{
    public partial class FormViewChildrenList : Form
    {
        public FormViewChildrenList()
        {
            InitializeComponent();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            FormViewChild open = new FormViewChild();
            open.detail = detail;
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            dto = bll.SelectViewParentChild(memberID);
            dataGridView1.DataSource = dto.Children;
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);
        
        private void panel1_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ChildBLL bll = new ChildBLL();
        ChildDTO dto = new ChildDTO();
        ChildDetailDTO detail = new ChildDetailDTO();
        public int memberID;
        public bool isParent = false;
        private void FormViewChildrenList_Load(object sender, EventArgs e)
        {
            #region
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label3.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label4.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label5.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label6.Font = new Font("Segoe UI", 21, FontStyle.Bold);
            label7.Font = new Font("Segoe UI", 21, FontStyle.Bold);
            label8.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label9.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label10.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            btnView.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            #endregion

            dto = bll.SelectViewParentChild(memberID);
            dataGridView1.DataSource = dto.Children;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Surname";
            dataGridView1.Columns[2].HeaderText = "Name";
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].HeaderText = "Gender";
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].HeaderText = "Nationality";
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
            dataGridView1.Columns[19].Visible = false;
            dataGridView1.Columns[20].Visible = false;
            dataGridView1.Columns[21].Visible = false;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            }

            List<ChildDetailDTO> list = dto.Children;
            foreach (var item in list)
            {                    
                string fatherImagePath = Application.StartupPath + "\\images\\" + item.FatherImagePath;
                picFather.ImageLocation = fatherImagePath;
                txtFathersName.Text = item.FathersName;
                txtFathersSurname.Text = item.FathersSurname;
                txtFathersNationality.Text = item.FatherNationalityName;
                string motherImagePath = Application.StartupPath + "\\images\\" + item.MotherImagePath;
                picMother.ImageLocation = motherImagePath;
                txtMothersName.Text = item.MothersName;
                txtMothersSurname.Text = item.MothersSurname;
                txtMothersNationality.Text = item.MotherNationalityName;
                labelTitle.Text = item.FathersSurname + "'s Children";
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail = new ChildDetailDTO();
            detail.ChildID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            detail.Surname = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            detail.Name = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            detail.Birthday = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
            detail.ImagePath = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            detail.GenderID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
            detail.GenderName = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            detail.NationalityID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
            detail.NationalityName = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            detail.MotherID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
            detail.MothersName = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            detail.MothersSurname = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            detail.MotherNationalityID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[12].Value);
            detail.MotherNationalityName = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
            detail.MotherImagePath = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
            detail.FatherID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[15].Value);
            detail.FathersName = dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString();
            detail.FathersSurname = dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString();
            detail.FatherNationalityID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[18].Value);
            detail.FatherNationalityName = dataGridView1.Rows[e.RowIndex].Cells[19].Value.ToString();
            detail.FatherImagePath = dataGridView1.Rows[e.RowIndex].Cells[20].Value.ToString();
        }        
    }
}
