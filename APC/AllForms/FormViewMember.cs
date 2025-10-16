using APC.DAL.DTO;
using APC.BLL;
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
using APC.DAL;
using System.Runtime.InteropServices;

namespace APC.AllForms
{
    public partial class FormViewMember : Form
    {
        public FormViewMember()
        {
            InitializeComponent();
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
        
        public bool isView = false;
        public bool isFormer = false;
        public MemberDetailDTO detail = new MemberDetailDTO();
        MemberBLL bll = new MemberBLL();
        int attendancePresentCount = 0;
        int attendanceAbsentCount = 0;
        decimal amountContributed = 0;
        decimal amountExpected = 0;
        decimal Balance = 0;
        ChildBLL childBLL = new ChildBLL();
        int noOfChildren = 0;
        CommentBLL commentBLL = new CommentBLL();
        CommentDetailDTO commentDetail = new CommentDetailDTO();
        CommentDTO dto = new CommentDTO();
        int commentCount = 0;
        FinedMemberBLL finedMemberBLL = new FinedMemberBLL();
        int finesCount = 0;

        private void FormViewMember_Load(object sender, EventArgs e)
        {
            #region
            labelMemberNameTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
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
            label11.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label12.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label13.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label14.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label15.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label16.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label17.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label18.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label19.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label20.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label21.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label22.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label23.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label25.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label26.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label27.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelPhone2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelPhone3.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelBirthday.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelMemSince.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelAmountContributed.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelAmountExpected.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelChildren.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelCommentText.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelFinesText.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelNoOfAbsent.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelNoOfChildren.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelNoOfComments.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelNoOfFines.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelNoOfPresent.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelPersonalBalance.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            txtAddress.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtEmail.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtLGA.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtName.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtPhone1.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtPhone2.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtPhone3.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtSurname.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtCountry.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtEmpStatus.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtGender.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtMaritalStatus.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtNationality.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtNextOfKin.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtNextOfKinRelationship.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtPassword.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtPermission.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtPosition.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtProfession.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtUsername.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnNoComments.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnViewAbsentAttendance.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnViewAmountContributed.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnViewAmountExpected.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnViewChildren.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnViewFines.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnViewPersonalBalance.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnViewPresentAttendance.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            #endregion

            labelCommentText.Hide();
            labelNoOfComments.Hide();
            btnNoComments.Hide();            

            dto = commentBLL.SelectMembersCommentList(detail.MemberID);
            commentCount = dto.Comments.Count(x => x.MemberID == detail.MemberID);
            labelNoOfComments.Text = commentCount.ToString();
            if (commentCount == 1)
            {
                labelCommentText.Visible = true;
                labelNoOfComments.Visible = true;
                btnNoComments.Visible = true;
                btnNoComments.Text = "View Comment";
            }
            else if(commentCount > 1)
            {
                labelCommentText.Visible = true;
                labelCommentText.Text = "Comments";
                labelNoOfComments.Visible = true;
                btnNoComments.Visible = true;
            }

            noOfChildren = childBLL.SelectAllChildrenCount(detail.MemberID);
            btnViewChildren.Hide();
            labelNoOfChildren.Text = noOfChildren.ToString();
            if (noOfChildren > 0)
            {
                labelChildren.Text = "Child";
                btnViewChildren.Text = "View Child";
                btnViewChildren.Visible = true;
            }
            if (noOfChildren > 1)
            {
                labelChildren.Text = "Children";
                btnViewChildren.Text = "View Children";
                btnViewChildren.Visible = true;
            }

            finesCount = finedMemberBLL.SelectAllFinesCount(detail.MemberID);
            btnViewFines.Hide();
            labelNoOfFines.Text = finesCount.ToString();
            if (finesCount > 0)
            {
                labelFinesText.Text = "Fine";
                btnViewFines.Text = "View Fine";
                btnViewFines.Visible = true;
            }
            if (finesCount > 1)
            {
                labelFinesText.Text = "Fines";
                btnViewFines.Text = "View Fines";
                btnViewFines.Visible = true;
            }

            attendancePresentCount = bll.GetNoOfMembersPresentAttendance(detail.MemberID);
            labelNoOfPresent.Text = attendancePresentCount.ToString();
            btnViewPresentAttendance.Hide();
            if (attendancePresentCount > 0)
            {
                btnViewPresentAttendance.Visible = true;
                btnViewPresentAttendance.Text = "View Attendance";
            }

            attendanceAbsentCount = bll.GetNoOfMembersAbsentAttendance(detail.MemberID);
            labelNoOfAbsent.Text = attendanceAbsentCount.ToString();
            btnViewAbsentAttendance.Hide();
            if (attendanceAbsentCount > 0)
            {
                btnViewAbsentAttendance.Visible = true;
                btnViewAbsentAttendance.Text = "View Attendance";
            }
            amountContributed = bll.GetAmountContributed(detail.MemberID);
            labelAmountContributed.Text = "€" + amountContributed;
            btnViewAmountContributed.Hide();
            if (amountContributed > 0)
            {
                btnViewAmountContributed.Visible = true;
                btnViewAmountContributed.Text = "View Amount";
            }
            amountExpected = bll.GetAmountExpected(detail.MemberID);
            //labelAmountExpected.Text = "€" + amountExpected;
            labelAmountExpected.Text = "€ 120";
            btnViewAmountExpected.Hide();
            if (amountExpected > 0)
            {
                btnViewAmountExpected.Visible = true;
                btnViewAmountExpected.Text = "View Amount";
            }
            Balance = 120 - amountContributed;
            btnViewPersonalBalance.Hide();
            if (Balance > 0)
            {
                labelPersonalBalance.Text = "€" + Balance;
                btnViewPersonalBalance.Visible = true;
                btnViewPersonalBalance.Text = "View Amount";    
            }

            txtPhone2.Hide();
            txtPhone3.Hide();
            labelPhone2.Hide();
            labelPhone3.Hide();
            if (isView)
            {
                labelMemberNameTitle.Text = detail.Surname + " " + detail.Name;
                string imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
                picProfilePic.ImageLocation = imagePath;

                txtName.Text = detail.Name;
                txtSurname.Text = detail.Surname;
                txtAddress.Text = detail.HouseAddress;
                txtPosition.Text = detail.PositionName;
                labelBirthday.Text = detail.Birthday.ToShortDateString();
                labelMemSince.Text = detail.MembershipDate.ToShortDateString();
                txtUsername.Text = detail.Username;
                txtPassword.Text = detail.Password;
                txtEmail.Text = detail.EmailAddress;
                txtPhone1.Text = detail.PhoneNumber;
                txtLGA.Text = detail.LGA;
                if (detail.PhoneNumber2 != "")
                {
                    txtPhone2.Visible = true;
                    labelPhone2.Visible = true;
                }
                if (detail.PhoneNumber3 != "")
                {
                    txtPhone3.Visible = true;
                    labelPhone3.Visible = true;
                }
                txtPhone2.Text = detail.PhoneNumber2;
                txtPhone3.Text = detail.PhoneNumber3;
                txtCountry.Text = detail.CountryName;
                txtProfession.Text = detail.ProfessionName;
                txtEmpStatus.Text = detail.EmploymentStatusName;
                txtGender.Text = detail.GenderName;
                txtNationality.Text = detail.NationalityName;
                txtMaritalStatus.Text = detail.MaritalStatusName;
                txtPermission.Text = detail.PermissionName;
                txtNextOfKin.Text = detail.NameOfNextOfKin;
                txtNextOfKinRelationship.Text = detail.RelationshipToKin;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnViewChildren_Click_1(object sender, EventArgs e)
        {
            if (noOfChildren > 1)
            {
                FormViewChildrenList open = new FormViewChildrenList();
                open.memberID = detail.MemberID;
                open.isParent = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
            }
            else if (noOfChildren < 2 && noOfChildren > 0)
            {
                FormViewChild open = new FormViewChild();
                open.memberID = detail.MemberID;
                open.isParent = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
            }
        }

        private void btnNoComments_Click(object sender, EventArgs e)
        {
            if (commentCount > 1)
            {
                FormSingleCommentList open = new FormSingleCommentList();
                open.memberID = detail.MemberID;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
            }
            else if (commentCount < 2 && commentCount > 0)
            {
                commentDetail = dto.Comments.First(x => x.MemberID == detail.MemberID);
                FormViewComment open = new FormViewComment();
                open.detail = commentDetail;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
            }
        }

        private void btnViewPresentAttendance_Click(object sender, EventArgs e)
        {
            if (attendancePresentCount > 0)
            {
                FormViewPersonalAttendances open = new FormViewPersonalAttendances();
                open.detail = detail;
                open.isPresent = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
            }
        }

        private void btnViewAbsentAttendance_Click_1(object sender, EventArgs e)
        {
            if (attendanceAbsentCount > 0)
            {
                FormViewPersonalAttendances open = new FormViewPersonalAttendances();
                open.detail = detail;
                open.isAbsent = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
            }
        }

        private void btnViewAmountContributed_Click_1(object sender, EventArgs e)
        {
            if (amountContributed > 0)
            {
                FormViewPersonalAttendances open = new FormViewPersonalAttendances();
                open.detail = detail;
                open.isAmountContributed = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
            }
        }

        private void btnViewAmountExpected_Click_1(object sender, EventArgs e)
        {
            if (Balance > 0)
            {
                FormViewPersonalAttendances open = new FormViewPersonalAttendances();
                open.detail = detail;
                open.isAmountExpected = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
            }
        }

        private void btnViewPersonalBalance_Click_1(object sender, EventArgs e)
        {
            if (Balance > 0)
            {
                FormViewPersonalAttendances open = new FormViewPersonalAttendances();
                open.detail = detail;
                open.isPersonalBalance = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
            }
        }

        private void btnViewFines_Click(object sender, EventArgs e)
        {
            if (finesCount > 0)
            {
                FormViewPersonalAttendances open = new FormViewPersonalAttendances();
                open.detail = detail;
                open.isPersonalFines = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
            }
        }
    }
}
