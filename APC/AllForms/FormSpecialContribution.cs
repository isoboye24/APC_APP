using APC.BLL;
using APC.DAL.DTO;
using APC.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormSpecialContribution : Form
    {
        public FormSpecialContribution()
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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        private void iconMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                buttonSize = 18;
                panelSize = 3.05f;
                panel1.Size = new Size(1001, 60);
                iconZoomIn.IconSize = 40;
                iconZoomOut.IconSize = 40;

                ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
            }
            else
            {
                WindowState = FormWindowState.Normal;
                buttonSize = 14;
                panelSize = 1.05f;
                panel1.Size = new Size(1001, 51);
                iconZoomIn.IconSize = 32;
                iconZoomOut.IconSize = 32;

                ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
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
                buttonSize = 14;
                panelSize = 1.05f;
                panel1.Size = new Size(1001, 51);
                iconZoomIn.IconSize = 32;
                iconZoomOut.IconSize = 32;

                ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
            }
        }

        private void iconZoomOut_Click(object sender, EventArgs e)
        {
            buttonSize -= 1;
            panelSize -= 1.05f;
            ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
        }

        private void iconZoomIn_Click(object sender, EventArgs e)
        {
            buttonSize += 1;
            panelSize += 1.05f;

            ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
        }

        private int buttonSize = 14;
        private float panelSize;
        public bool isUpdate = false;
        private bool noChanges;

        MemberBLL memberBLL = new MemberBLL();
        MemberDTO memberDTO = new MemberDTO();
        MemberDetailDTO memberDetail = new MemberDetailDTO();

        SpecialContributionsBLL bll = new SpecialContributionsBLL();

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormSpecialContribution_Load(object sender, EventArgs e)
        {
            memberDTO = memberBLL.Select();

            #region
            dataGridView1.DataSource = memberDTO.Members;
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
            dataGridView1.Columns[19].Visible = false;
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
                column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            }
            #endregion
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            memberDetail = new MemberDetailDTO();
            memberDetail.MemberID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            memberDetail.Username = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            memberDetail.Password = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            memberDetail.Surname = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            memberDetail.Name = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            memberDetail.Birthday = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
            memberDetail.ImagePath = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            memberDetail.EmailAddress = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            memberDetail.HouseAddress = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            memberDetail.MembershipDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
            memberDetail.CountryID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
            memberDetail.CountryName = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            memberDetail.NationalityID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[12].Value);
            memberDetail.NationalityName = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
            memberDetail.ProfessionID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[14].Value);
            memberDetail.ProfessionName = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
            memberDetail.PositionID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[16].Value);
            memberDetail.PositionName = dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString();
            memberDetail.GenderID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[18].Value);
            memberDetail.GenderName = dataGridView1.Rows[e.RowIndex].Cells[19].Value.ToString();
            memberDetail.EmploymentStatusID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[20].Value);
            memberDetail.EmploymentStatusName = dataGridView1.Rows[e.RowIndex].Cells[21].Value.ToString();
            memberDetail.MaritalStatusID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[22].Value);
            memberDetail.MaritalStatusName = dataGridView1.Rows[e.RowIndex].Cells[23].Value.ToString();
            memberDetail.PermissionID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[24].Value);
            memberDetail.PermissionName = dataGridView1.Rows[e.RowIndex].Cells[25].Value.ToString();
            memberDetail.PhoneNumber = dataGridView1.Rows[e.RowIndex].Cells[26].Value.ToString();
            memberDetail.PhoneNumber2 = dataGridView1.Rows[e.RowIndex].Cells[27].Value.ToString();
            memberDetail.PhoneNumber3 = dataGridView1.Rows[e.RowIndex].Cells[28].Value.ToString();
            memberDetail.isCountryDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[29].Value);
            memberDetail.isNationalityDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[30].Value);
            memberDetail.isProfessionDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[31].Value);
            memberDetail.isPositionDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[32].Value);
            memberDetail.isEmpStatusDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[33].Value);
            memberDetail.isMarStatusDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[34].Value);
            memberDetail.MembershipStatusID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[35].Value);
            memberDetail.MembershipStatus = dataGridView1.Rows[e.RowIndex].Cells[36].Value.ToString();
            memberDetail.DeadDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[37].Value);
            memberDetail.DeadAge = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[38].Value);
            memberDetail.LGA = dataGridView1.Rows[e.RowIndex].Cells[39].Value.ToString();
            memberDetail.NameOfNextOfKin = dataGridView1.Rows[e.RowIndex].Cells[40].Value.ToString();
            memberDetail.RelationshipToKinID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[41].Value);
            memberDetail.RelationshipToKin = dataGridView1.Rows[e.RowIndex].Cells[42].Value.ToString();

            string imagePath = Application.StartupPath + "\\images\\" + memberDetail.ImagePath;
            picSupervisor.ImageLocation = imagePath;

            labelSupervisorsName.Text = memberDetail.Name;
            labelSupervisorsSurname.Text = memberDetail.Surname;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAmountToContribute.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the amount each member will contribute.");
            }
            else if (txtTitle.Text.Trim() == "")
            {
                MessageBox.Show("Please enter title of the contribution.");
            }
            else if (txtAmountExpected.Text.Trim() == "")
            {
                MessageBox.Show("Please enter amount expected.");
            }
            else if (dateTimePickerEndDate.Value < dateTimePickerStartDate.Value)
            {
                MessageBox.Show("The end date cannot be earlier than the start date.");
            }
            else
            {
                if (!isUpdate)
                {
                    SpecialContributionDetailDTO detail = new SpecialContributionDetailDTO();
                    detail.SupervisorID = memberDetail.MemberID;
                    detail.ContributionTitle = txtTitle.Text.Trim();
                    detail.Summary = txtSummary.Text.Trim();
                    detail.AmountExpected = Convert.ToDecimal(txtAmountExpected.Text.Trim());
                    detail.AmountToContribute = Convert.ToDecimal(txtAmountToContribute.Text.Trim());
                    detail.ContributionStartDate = dateTimePickerStartDate.Value;
                    detail.ContributionEndDate = dateTimePickerEndDate.Value;

                    if (bll.Insert(detail))
                    {
                        txtAmountExpected.Clear();
                        txtSummary.Clear();
                        txtTitle.Clear();
                        txtAmountToContribute.Clear();
                        dateTimePickerStartDate.Value = DateTime.Today;
                        dateTimePickerEndDate.Value = DateTime.Today;
                        MessageBox.Show("Special Contribution Created");
                    }
                }
            }
        }
    }
}
