using APC.BLL;
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
    public partial class FormViewDeadMember : Form
    {
        public FormViewDeadMember()
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public MemberDetailDTO detail = new MemberDetailDTO();
        public bool isView = false;
        ChildBLL childBLL = new ChildBLL();
        int noOfChildren =  0;
        MemberBLL bll = new MemberBLL();
        int attendancePresentCount = 0;
        int attendanceAbsentCount = 0;
        int commentCount = 0;
        CommentBLL commentBLL = new CommentBLL();
        CommentDTO dto = new CommentDTO();
        CommentDetailDTO commentDetail = new CommentDetailDTO();
        private void FormViewDeadMember_Load(object sender, EventArgs e)
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
            label14.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label15.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label25.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelBirthday.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelDeadDate.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelMemSince.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelNoOfAbsent.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelNoOfChildren.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelNoOfComments.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelNoOfPresent.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelAge.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            txtName.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtNameOfNextOfKin.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtSurname.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtCountry.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtGender.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtMaritalStatus.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtNationality.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtPosition.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtProfession.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtRelationshipToKin.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnViewAbsentAttendance.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnViewChildren.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnViewPresentAttendance.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnNoComments.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            #endregion

            noOfChildren = childBLL.SelectAllChildrenCount(detail.MemberID);
            
            btnViewChildren.Hide();
            labelNoOfChildren.Text = noOfChildren.ToString();
            if (Convert.ToInt32(labelNoOfChildren.Text) > 0)
            {
                labelChildren.Text = "Child";
                btnViewChildren.Text = "View Child";
                btnViewChildren.Visible = true;
            }
            if (Convert.ToInt32(labelNoOfChildren.Text) > 1)
            {
                labelChildren.Text = "Children";
                btnViewChildren.Text = "View Children";
                btnViewChildren.Visible = true;
            }

            attendancePresentCount = bll.GetNoOfMembersPresentAttendance(detail.MemberID);
            labelNoOfPresent.Text = attendancePresentCount.ToString();
            btnViewPresentAttendance.Hide();
            if (attendancePresentCount > 0)
            {
                btnViewPresentAttendance.Visible = true;
            }

            attendanceAbsentCount = bll.GetNoOfMembersAbsentAttendance(detail.MemberID);
            labelNoOfAbsent.Text = attendanceAbsentCount.ToString();
            btnViewAbsentAttendance.Hide();
            if (attendanceAbsentCount > 0)
            {
                btnViewAbsentAttendance.Visible = true;
            }

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
            else if (commentCount > 1)
            {
                labelCommentText.Visible = true;
                labelCommentText.Text = "Comments";
                labelNoOfComments.Visible = true;
                btnNoComments.Visible = true;
            }
                labelMemberNameTitle.Text = detail.Surname + " " + detail.Name;
                string imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
                picProfilePic.ImageLocation = imagePath;

                txtName.Text = detail.Name;
                txtSurname.Text = detail.Surname;
                txtPosition.Text = detail.PositionName;
                labelBirthday.Text = detail.Birthday.ToShortDateString();
                labelMemSince.Text = detail.MembershipDate.ToShortDateString();
                txtCountry.Text = detail.CountryName;
                txtProfession.Text = detail.ProfessionName;
                txtGender.Text = detail.GenderName;
                txtNationality.Text = detail.NationalityName;
                txtMaritalStatus.Text = detail.MaritalStatusName;
                TimeSpan difference;
                DateTime died = Convert.ToDateTime(detail.DeadDate);
                labelDeadDate.Text = died.ToShortDateString();
                difference = detail.DeadDate - detail.Birthday;
                labelAge.Text = Math.Floor(difference.TotalDays / 365.25).ToString() + " years";
                txtNameOfNextOfKin.Text = detail.NameOfNextOfKin;
                txtRelationshipToKin.Text = detail.RelationshipToKin;
        }

        private void btnViewChildren_Click(object sender, EventArgs e)
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

        private void btnViewAbsentAttendance_Click(object sender, EventArgs e)
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
