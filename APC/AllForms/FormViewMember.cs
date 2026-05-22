using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.BLL;
using APC.DAL.DAO;
using APC.DAL.DTO;
using APC.Helper;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormViewMember : Form
    {
        private readonly IMemberService _memberService;

        private MemberFullDetailsDTO _memberFullDetailsDTO;
        public FormViewMember(IMemberService memberService)
        {
            InitializeComponent();
            _memberService = memberService;
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
        public bool isCommittment = false;

        public MemberDetailDTO detail { get; set; }
        public int memberID;
        MemberBLL bll = new MemberBLL();
        MemberDTO DTO = new MemberDTO();
        int attendancePresentCount = 0;
        int attendanceAbsentCount = 0;
        decimal amountContributed = 0;
        decimal amountExpected = 0;
        decimal Balance = 0;
        CommentBLL commentBLL = new CommentBLL();
        CommentDetailDTO commentDetail = new CommentDetailDTO();
        CommentDTO dto = new CommentDTO();
        int commentCount = 0;
        FinedMemberBLL finedMemberBLL = new FinedMemberBLL();
        int finesCount = 0;

        public void MemberDetail(MemberFullDetailsDTO memberFullDetailsDTO)
        {
            _memberFullDetailsDTO = memberFullDetailsDTO;
        }

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(14, labelMemberNameTitle, label1, label2, label3, label4, label5, label6, label7, label8, label9,
                label10, label11, label12, label13, label14, label15, label16, label17, label18, label19, label20, label21, label22,
                label23, label25, label26, label27, labelPhone2, labelPhone3, labelBirthday, labelMemSince, labelAmountContributed,
                labelAmountExpected, labelCommentText, labelFinesText, labelNoOfAbsent, labelNoOfComments, labelNoOfFines, labelNoOfPresent,
                labelPersonalBalance, btnClose, btnNoComments, btnViewAbsentAttendance, btnViewAmountContributed, btnViewAmountExpected,
                btnViewFines, btnViewPersonalBalance, btnViewPresentAttendance
                );


            GeneralHelper.ApplyRegularFont(14, txtAddress, txtEmail, txtLGA, txtName, txtPhone1, txtCountry, txtEmpStatus, txtGender,
                txtPhone2, txtPhone3, txtSurname, txtMaritalStatus, txtNationality, txtNextOfKin, txtNextOfKinRelationship, txtPermission,
                txtPosition, txtProfession
                );
        }

        private void FormViewMember_Load(object sender, EventArgs e)
        {
            resizeControls();

            if (isCommittment)
            {
                DTO = bll.Select();

                detail = DTO.Members.FirstOrDefault(x => x.MemberID == memberID);

                if (detail == null)
                {
                    MessageBox.Show("No detail data received!");
                    return;
                }
                else
                {
                    labelMemberNameTitle.Text = _memberFullDetailsDTO.LastName + " " + _memberFullDetailsDTO.FirstName;
                    string imagePath = Application.StartupPath + "\\images\\" + _memberFullDetailsDTO.ImagePath;
                    picProfilePic.ImageLocation = imagePath;

                    txtName.Text = _memberFullDetailsDTO.FirstName;
                    txtSurname.Text = _memberFullDetailsDTO.LastName;
                    txtAddress.Text = _memberFullDetailsDTO.HouseAddress;
                    txtPosition.Text = _memberFullDetailsDTO.Position;
                    labelBirthday.Text = _memberFullDetailsDTO.Birthday.ToShortDateString();
                    labelMemSince.Text = _memberFullDetailsDTO.MembershipDate.ToString();
                    txtEmail.Text = _memberFullDetailsDTO.Email;
                    txtPhone1.Text = _memberFullDetailsDTO.PhoneNumber;
                    txtLGA.Text = _memberFullDetailsDTO.LGA;
                    if (_memberFullDetailsDTO.PhoneNumber2 != "")
                    {
                        txtPhone2.Visible = true;
                        labelPhone2.Visible = true;
                    }
                    if (_memberFullDetailsDTO.PhoneNumber3 != "")
                    {
                        txtPhone3.Visible = true;
                        labelPhone3.Visible = true;
                    }
                    txtPhone2.Text = _memberFullDetailsDTO.PhoneNumber2;
                    txtPhone3.Text = _memberFullDetailsDTO.PhoneNumber3;
                    txtCountry.Text = _memberFullDetailsDTO.Country;
                    txtProfession.Text = _memberFullDetailsDTO.Profession;
                    txtEmpStatus.Text = _memberFullDetailsDTO.EmploymentStatus;
                    txtGender.Text = _memberFullDetailsDTO.Gender;
                    txtNationality.Text = _memberFullDetailsDTO.Nationality;
                    txtMaritalStatus.Text = _memberFullDetailsDTO.MaritalStatus;
                    txtPermission.Text = _memberFullDetailsDTO.Permission;
                    txtNextOfKin.Text = _memberFullDetailsDTO.NextOfKin;
                    txtNextOfKinRelationship.Text = _memberFullDetailsDTO.RelationshipToNextOfKin;
                }


            }

            labelCommentText.Hide();
            labelNoOfComments.Hide();
            btnNoComments.Hide();            

            dto = commentBLL.SelectMembersCommentList(_memberFullDetailsDTO.MemberId);
            commentCount = dto.Comments.Count(x => x.MemberID == _memberFullDetailsDTO.MemberId);
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

            
            finesCount = finedMemberBLL.SelectAllFinesCount(_memberFullDetailsDTO.MemberId);
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

            attendancePresentCount = bll.GetNoOfMembersPresentAttendance(_memberFullDetailsDTO.MemberId);
            labelNoOfPresent.Text = attendancePresentCount.ToString();
            btnViewPresentAttendance.Hide();
            if (attendancePresentCount > 0)
            {
                btnViewPresentAttendance.Visible = true;
                btnViewPresentAttendance.Text = "View Attendance";
            }

            attendanceAbsentCount = bll.GetNoOfMembersAbsentAttendance(_memberFullDetailsDTO.MemberId);
            labelNoOfAbsent.Text = attendanceAbsentCount.ToString();
            btnViewAbsentAttendance.Hide();
            if (attendanceAbsentCount > 0)
            {
                btnViewAbsentAttendance.Visible = true;
                btnViewAbsentAttendance.Text = "View Attendance";
            }
            amountContributed = bll.GetAmountContributed(_memberFullDetailsDTO.MemberId);
            labelAmountContributed.Text = "€" + amountContributed;
            btnViewAmountContributed.Hide();
            if (amountContributed > 0)
            {
                btnViewAmountContributed.Visible = true;
                btnViewAmountContributed.Text = "View Amount";
            }
            amountExpected = bll.GetAmountExpected(_memberFullDetailsDTO.MemberId);
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
                labelMemberNameTitle.Text = _memberFullDetailsDTO.LastName + " " + _memberFullDetailsDTO.FirstName;
                string imagePath = Application.StartupPath + "\\images\\" + _memberFullDetailsDTO.ImagePath;
                picProfilePic.ImageLocation = imagePath;

                txtName.Text = _memberFullDetailsDTO.FirstName;
                txtSurname.Text = _memberFullDetailsDTO.LastName;
                txtAddress.Text = _memberFullDetailsDTO.HouseAddress;
                txtPosition.Text = _memberFullDetailsDTO.Position;
                labelBirthday.Text = _memberFullDetailsDTO.Birthday.ToShortDateString();
                labelMemSince.Text = _memberFullDetailsDTO.MembershipDate.ToString();
                txtEmail.Text = _memberFullDetailsDTO.Email;
                txtPhone1.Text = _memberFullDetailsDTO.PhoneNumber;
                txtLGA.Text = _memberFullDetailsDTO.LGA;
                if (_memberFullDetailsDTO.PhoneNumber2 != "")
                {
                    txtPhone2.Visible = true;
                    labelPhone2.Visible = true;
                }
                if (detail.PhoneNumber3 != "")
                {
                    txtPhone3.Visible = true;
                    labelPhone3.Visible = true;
                }
                txtPhone2.Text = _memberFullDetailsDTO.PhoneNumber2;
                txtPhone3.Text = _memberFullDetailsDTO.PhoneNumber3;
                txtCountry.Text = _memberFullDetailsDTO.Country;
                txtProfession.Text = _memberFullDetailsDTO.Profession;
                txtEmpStatus.Text = _memberFullDetailsDTO.EmploymentStatus;
                txtGender.Text = _memberFullDetailsDTO.Gender;
                txtNationality.Text = _memberFullDetailsDTO.Nationality;
                txtMaritalStatus.Text = _memberFullDetailsDTO.MaritalStatus;
                txtPermission.Text = _memberFullDetailsDTO.Permission;
                txtNextOfKin.Text = _memberFullDetailsDTO.NextOfKin;
                txtNextOfKinRelationship.Text = _memberFullDetailsDTO.RelationshipToNextOfKin;
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
