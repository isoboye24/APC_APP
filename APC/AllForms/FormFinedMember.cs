using APC.BLL;
using APC.DAL;
using APC.DAL.DAO;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormFinedMember : Form
    {
        public FormFinedMember()
        {
            InitializeComponent();
        }
        // Drag From
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);

        FinedMemberBLL bll = new FinedMemberBLL();
        FinedMemberDTO dto = new FinedMemberDTO();
        public FinedMemberDetailDTO detail = new FinedMemberDetailDTO();

        public MemberDetailDTO memberDetail = new MemberDetailDTO();
        public ConstitutionDetailDTO constitutionDetail = new ConstitutionDetailDTO();
        public bool isUpdate = false;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private bool isChangeMember = false;
        private bool isChangeConstitution = false;
        private void btnChangeMember_Click(object sender, EventArgs e)
        {
            isChangeMember = !isChangeMember;
            if (isChangeMember)
            {
                btnChangeMember.Text = "prev. mem.";
            }
            else
            {
                btnChangeMember.Text = "Change member";
            }
        }

        private void btnChangeConstitution_Click(object sender, EventArgs e)
        {
            isChangeConstitution = !isChangeConstitution;
            if (isChangeConstitution)
            {
                btnChangeConstitution.Text = "prev. const.";
            }
            else
            {
                btnChangeConstitution.Text = "Change const.";
            }
        }

        private void FormFinedMember_Load(object sender, EventArgs e)
        {
            btnChangeMember.Hide();
            btnChangeConstitution.Hide();
            #region
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label3.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label4.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label5.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label6.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label7.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label8.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label9.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label10.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelName.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            labelSurname.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            labelPosition.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            labelConstitutionSection.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            labelFine.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            txtAmount.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtSummary.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtSearchSurname.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtSection.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSave.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            #endregion

            dto = bll.Select();
            #region
            dataGridViewMembers.DataSource = dto.Members;
            dataGridViewMembers.Columns[0].Visible = false;
            dataGridViewMembers.Columns[1].Visible = false;
            dataGridViewMembers.Columns[2].Visible = false;
            dataGridViewMembers.Columns[3].HeaderText = "Surname";
            dataGridViewMembers.Columns[4].HeaderText = "Name";
            dataGridViewMembers.Columns[5].Visible = false;
            dataGridViewMembers.Columns[6].Visible = false;
            dataGridViewMembers.Columns[7].Visible = false;
            dataGridViewMembers.Columns[8].Visible = false;
            dataGridViewMembers.Columns[9].Visible = false;
            dataGridViewMembers.Columns[10].Visible = false;
            dataGridViewMembers.Columns[11].Visible = false;
            dataGridViewMembers.Columns[12].Visible = false;
            dataGridViewMembers.Columns[13].Visible = false;
            dataGridViewMembers.Columns[14].Visible = false;
            dataGridViewMembers.Columns[15].Visible = false;
            dataGridViewMembers.Columns[16].Visible = false;
            dataGridViewMembers.Columns[17].Visible = false;
            dataGridViewMembers.Columns[18].Visible = false;
            dataGridViewMembers.Columns[19].HeaderText = "Gender";
            dataGridViewMembers.Columns[20].Visible = false;
            dataGridViewMembers.Columns[21].Visible = false;
            dataGridViewMembers.Columns[22].Visible = false;
            dataGridViewMembers.Columns[23].Visible = false;
            dataGridViewMembers.Columns[24].Visible = false;
            dataGridViewMembers.Columns[25].Visible = false;
            dataGridViewMembers.Columns[26].Visible = false;
            dataGridViewMembers.Columns[27].Visible = false;
            dataGridViewMembers.Columns[28].Visible = false;
            dataGridViewMembers.Columns[29].Visible = false;
            dataGridViewMembers.Columns[30].Visible = false;
            dataGridViewMembers.Columns[31].Visible = false;
            dataGridViewMembers.Columns[32].Visible = false;
            dataGridViewMembers.Columns[33].Visible = false;
            dataGridViewMembers.Columns[34].Visible = false;
            dataGridViewMembers.Columns[35].Visible = false;
            dataGridViewMembers.Columns[36].Visible = false;
            dataGridViewMembers.Columns[37].Visible = false;
            dataGridViewMembers.Columns[38].Visible = false;
            dataGridViewMembers.Columns[39].Visible = false;
            dataGridViewMembers.Columns[40].Visible = false;
            dataGridViewMembers.Columns[41].Visible = false;
            dataGridViewMembers.Columns[42].Visible = false;
            foreach (DataGridViewColumn column in dataGridViewMembers.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            }
            #endregion

            #region
            dataGridViewConstitutions.DataSource = dto.Constitutions;
            dataGridViewConstitutions.Columns[0].Visible = false;
            dataGridViewConstitutions.Columns[1].Visible = false;
            dataGridViewConstitutions.Columns[2].HeaderText = "Section Des.";
            dataGridViewConstitutions.Columns[3].Visible = false;
            dataGridViewConstitutions.Columns[4].Visible = false;
            dataGridViewConstitutions.Columns[5].HeaderText = "Fine";
            dataGridViewConstitutions.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            foreach (DataGridViewColumn column in dataGridViewConstitutions.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            }
            #endregion

            if (isUpdate)
            {
                txtSummary.Text = detail.Summary;
                txtAmount.Text = detail.AmountPaid.ToString();
                dateTimePickerFineDate.Value = detail.FineDate;
                labelFine.Text = detail.ExpectedAmountWithCurrency;
                labelConstitutionSection.Text = detail.ConstitutionSection;
                labelPosition.Text = detail.Position;
                labelSurname.Text = detail.Surname;
                labelName.Text = detail.Name;
                string imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
                picProfilePic.ImageLocation = imagePath;
                labelTitle.Text = "Edit Fined Member";
                btnChangeMember.Visible = true;
                btnChangeConstitution.Visible = true;
            }            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!isUpdate)
            {
                FinedMemberDetailDTO finedMember = new FinedMemberDetailDTO();
                finedMember.Summary = txtSearchSurname.Text.Trim();
                if (txtAmount.Text.Trim() == "")
                {
                    finedMember.AmountPaid = 0;
                }
                else
                {
                    finedMember.AmountPaid = Convert.ToDecimal(txtAmount.Text.Trim());
                }
                finedMember.ConstitutionID = constitutionDetail.ConstitutionID;
                finedMember.MemberID = memberDetail.MemberID;
                finedMember.Day = dateTimePickerFineDate.Value.Day;
                finedMember.MonthID = dateTimePickerFineDate.Value.Month;
                finedMember.Year = dateTimePickerFineDate.Value.Year;
                finedMember.FineDate = dateTimePickerFineDate.Value;
                if (bll.Insert(finedMember))
                {
                    MessageBox.Show("Member was fined successfully!");
                    txtAmount.Clear();
                    txtSearchSurname.Clear();
                    txtSummary.Clear();
                    txtSection.Clear();
                }
            }
            else if (isUpdate)
            {
                if (txtAmount.Text.Trim() == detail.AmountPaid.ToString()
                    && txtSummary.Text.Trim() == detail.Summary
                    && detail.MemberID == memberDetail.MemberID
                    && detail.ConstitutionID == constitutionDetail.ConstitutionID
                    && detail.FineDate == dateTimePickerFineDate.Value
                    )
                {
                    MessageBox.Show("There is no change!");
                }
                else
                {
                    detail.Summary = txtSummary.Text.Trim();
                    if (txtAmount.Text.Trim() == "")
                    {
                        detail.AmountPaid = 0;
                    }
                    else
                    {
                        detail.AmountPaid = Convert.ToDecimal(txtAmount.Text.Trim());
                    }
                    if (isChangeConstitution)
                    {
                        detail.ConstitutionID = constitutionDetail.ConstitutionID;
                    }
                    else
                    {
                        detail.ConstitutionID = detail.ConstitutionID;
                    }
                    if (isChangeMember)
                    {
                        detail.MemberID = memberDetail.MemberID;
                    }
                    else
                    {
                        detail.MemberID = detail.MemberID;
                    }
                    detail.FineDate = dateTimePickerFineDate.Value;
                    detail.Day = dateTimePickerFineDate.Value.Day;
                    detail.MonthID = dateTimePickerFineDate.Value.Month;
                    detail.Year = dateTimePickerFineDate.Value.Year;
                    if (bll.Update(detail))
                    {
                        MessageBox.Show("Fined member was updated!");
                        this.Close();
                    }
                }
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            memberDetail = new MemberDetailDTO();
            memberDetail.MemberID = Convert.ToInt32(dataGridViewMembers.Rows[e.RowIndex].Cells[0].Value);
            memberDetail.Username = dataGridViewMembers.Rows[e.RowIndex].Cells[1].Value.ToString();
            memberDetail.Password = dataGridViewMembers.Rows[e.RowIndex].Cells[2].Value.ToString();
            memberDetail.Surname = dataGridViewMembers.Rows[e.RowIndex].Cells[3].Value.ToString();
            memberDetail.Name = dataGridViewMembers.Rows[e.RowIndex].Cells[4].Value.ToString();
            memberDetail.Birthday = Convert.ToDateTime(dataGridViewMembers.Rows[e.RowIndex].Cells[5].Value);
            memberDetail.ImagePath = dataGridViewMembers.Rows[e.RowIndex].Cells[6].Value.ToString();
            memberDetail.EmailAddress = dataGridViewMembers.Rows[e.RowIndex].Cells[7].Value.ToString();
            memberDetail.HouseAddress = dataGridViewMembers.Rows[e.RowIndex].Cells[8].Value.ToString();
            memberDetail.MembershipDate = Convert.ToDateTime(dataGridViewMembers.Rows[e.RowIndex].Cells[9].Value);
            memberDetail.CountryID = Convert.ToInt32(dataGridViewMembers.Rows[e.RowIndex].Cells[10].Value);
            memberDetail.CountryName = dataGridViewMembers.Rows[e.RowIndex].Cells[11].Value.ToString();
            memberDetail.NationalityID = Convert.ToInt32(dataGridViewMembers.Rows[e.RowIndex].Cells[12].Value);
            memberDetail.NationalityName = dataGridViewMembers.Rows[e.RowIndex].Cells[13].Value.ToString();
            memberDetail.ProfessionID = Convert.ToInt32(dataGridViewMembers.Rows[e.RowIndex].Cells[14].Value);
            memberDetail.ProfessionName = dataGridViewMembers.Rows[e.RowIndex].Cells[15].Value.ToString();
            memberDetail.PositionID = Convert.ToInt32(dataGridViewMembers.Rows[e.RowIndex].Cells[16].Value);
            memberDetail.PositionName = dataGridViewMembers.Rows[e.RowIndex].Cells[17].Value.ToString();
            memberDetail.GenderID = Convert.ToInt32(dataGridViewMembers.Rows[e.RowIndex].Cells[18].Value);
            memberDetail.GenderName = dataGridViewMembers.Rows[e.RowIndex].Cells[19].Value.ToString();
            memberDetail.EmploymentStatusID = Convert.ToInt32(dataGridViewMembers.Rows[e.RowIndex].Cells[20].Value);
            memberDetail.EmploymentStatusName = dataGridViewMembers.Rows[e.RowIndex].Cells[21].Value.ToString();
            memberDetail.MaritalStatusID = Convert.ToInt32(dataGridViewMembers.Rows[e.RowIndex].Cells[22].Value);
            memberDetail.MaritalStatusName = dataGridViewMembers.Rows[e.RowIndex].Cells[23].Value.ToString();
            memberDetail.PermissionID = Convert.ToInt32(dataGridViewMembers.Rows[e.RowIndex].Cells[24].Value);
            memberDetail.PermissionName = dataGridViewMembers.Rows[e.RowIndex].Cells[25].Value.ToString();
            memberDetail.PhoneNumber = dataGridViewMembers.Rows[e.RowIndex].Cells[26].Value.ToString();
            memberDetail.PhoneNumber2 = dataGridViewMembers.Rows[e.RowIndex].Cells[27].Value.ToString();
            memberDetail.PhoneNumber3 = dataGridViewMembers.Rows[e.RowIndex].Cells[28].Value.ToString();
            memberDetail.isCountryDeleted = Convert.ToBoolean(dataGridViewMembers.Rows[e.RowIndex].Cells[29].Value);
            memberDetail.isNationalityDeleted = Convert.ToBoolean(dataGridViewMembers.Rows[e.RowIndex].Cells[30].Value);
            memberDetail.isProfessionDeleted = Convert.ToBoolean(dataGridViewMembers.Rows[e.RowIndex].Cells[31].Value);
            memberDetail.isPositionDeleted = Convert.ToBoolean(dataGridViewMembers.Rows[e.RowIndex].Cells[32].Value);
            memberDetail.isEmpStatusDeleted = Convert.ToBoolean(dataGridViewMembers.Rows[e.RowIndex].Cells[33].Value);
            memberDetail.isMarStatusDeleted = Convert.ToBoolean(dataGridViewMembers.Rows[e.RowIndex].Cells[34].Value);
            memberDetail.MembershipStatusID = Convert.ToInt32(dataGridViewMembers.Rows[e.RowIndex].Cells[35].Value);
            memberDetail.MembershipStatus = dataGridViewMembers.Rows[e.RowIndex].Cells[36].Value.ToString();
            memberDetail.DeadDate = Convert.ToDateTime(dataGridViewMembers.Rows[e.RowIndex].Cells[37].Value);
            memberDetail.DeadAge = Convert.ToDouble(dataGridViewMembers.Rows[e.RowIndex].Cells[38].Value);
            memberDetail.LGA = dataGridViewMembers.Rows[e.RowIndex].Cells[39].Value.ToString();
            memberDetail.NameOfNextOfKin = dataGridViewMembers.Rows[e.RowIndex].Cells[40].Value.ToString();
            memberDetail.RelationshipToKinID = Convert.ToInt32(dataGridViewMembers.Rows[e.RowIndex].Cells[41].Value);
            memberDetail.RelationshipToKin = dataGridViewMembers.Rows[e.RowIndex].Cells[42].Value.ToString();

            string imagePath = Application.StartupPath + "\\images\\" + memberDetail.ImagePath;
            picProfilePic.ImageLocation = imagePath;

            labelName.Text = memberDetail.Name;
            labelSurname.Text = memberDetail.Surname;
            labelPosition.Text = memberDetail.PositionName;
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e, (TextBox)sender);
        }

        private void dataGridViewConstitutions_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            constitutionDetail = new ConstitutionDetailDTO();
            constitutionDetail.ConstitutionID = Convert.ToInt32(dataGridViewConstitutions.Rows[e.RowIndex].Cells[0].Value);
            constitutionDetail.ConstitutionText = dataGridViewConstitutions.Rows[e.RowIndex].Cells[1].Value.ToString();
            constitutionDetail.Section = dataGridViewConstitutions.Rows[e.RowIndex].Cells[2].Value.ToString();
            constitutionDetail.ShortDescription = dataGridViewConstitutions.Rows[e.RowIndex].Cells[3].Value.ToString();
            constitutionDetail.Fine = Convert.ToDecimal(dataGridViewConstitutions.Rows[e.RowIndex].Cells[4].Value);
            constitutionDetail.FineWithCurrency = dataGridViewConstitutions.Rows[e.RowIndex].Cells[5].Value.ToString();

            labelFine.Text = constitutionDetail.FineWithCurrency;
            labelConstitutionSection.Text = constitutionDetail.Section;
        }

        private void txtSearchSurname_TextChanged(object sender, EventArgs e)
        {
            List<MemberDetailDTO> list = dto.Members;
            list = list.Where(x => x.Surname.Contains(txtSearchSurname.Text.Trim())).ToList();
            dataGridViewMembers.DataSource = list;
        }

        private void txtSection_TextChanged(object sender, EventArgs e)
        {
            List<ConstitutionDetailDTO> list = dto.Constitutions;
            list = list.Where(x => x.Section.Contains(txtSection.Text.Trim())).ToList();
            dataGridViewConstitutions.DataSource = list;
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
    }
}
