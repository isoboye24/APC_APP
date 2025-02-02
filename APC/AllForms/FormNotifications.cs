using APC.BLL;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace APC.AllForms
{
    public partial class FormNotifications : Form
    {
        public FormNotifications()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (isLogin && isAdmin)
            {
                FormDashboard open = new FormDashboard();
                this.Hide();
                open.isAdmin = true;
                open.ShowDialog();
            }
            else if (isLogin && isEditor)
            {
                FormDashboard open = new FormDashboard();
                this.Hide();
                open.isEditor = true;
                open.ShowDialog();
            }
            else
            {
                this.Close();
            }
        }
        MemberBLL bll = new MemberBLL();
        MemberDTO dto = new MemberDTO();
        public bool isLogin = false;
        public bool isAdmin = false;
        public bool isEditor = false;
        public int numbers;
        private void FormNotifications_Load(object sender, EventArgs e)
        {
            #region
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            txtSearchSurname.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            #endregion

            dto = bll.Select();
            dataGridView1.DataSource = dto.Absentees;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Surname";
            dataGridView1.Columns[2].HeaderText = "Name";
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].HeaderText = "Position";
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].HeaderText = "Gender";
            dataGridView1.Columns[9].Visible = false;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            }

            int absenteesCount = bll.Select3MonthsAbsentesCount();
            if (absenteesCount < 2)
            {
                labelTitle.Text = "MEMBER ABSENT FOR ATLEAST THE LAST 3 MONTHS";
            }
        }
    }
}
