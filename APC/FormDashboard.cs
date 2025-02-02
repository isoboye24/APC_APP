﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using APC.AllForms;
using APC.BLL;
using APC.DAL.DTO;
using FontAwesome.Sharp;

namespace APC
{
    public partial class FormDashboard : Form
    {
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        public FormDashboard()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(5, 40);
            tableLayoutPanelSidebar.Controls.Add(leftBorderBtn);
            //Form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
        }
        private struct RBGColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(245, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
            public static Color normal = Color.MidnightBlue;
        }
        // Button Methods
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                // Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                // Left Border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = false;
                leftBorderBtn.BringToFront();
                // Icon Current Child Form
                iconCurrentChildForm.IconChar = currentBtn.IconChar;
                iconCurrentChildForm.IconColor = color;
            }
        }
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.MidnightBlue;
                currentBtn.ForeColor = Color.PaleTurquoise;
                currentBtn.IconColor = Color.PaleTurquoise; ;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }
        // Drag From
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.Home;
            iconCurrentChildForm.IconColor = Color.MediumPurple;
            labelTitleChildForm.Text = "Dashboard";
            RefreshAllCards();
        }

        private void panelTitleBar_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                // open only a form
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            labelTitleChildForm.Text = childForm.Text;
        }

        private void picBoxMin_Click(object sender, EventArgs e)
        {

        }
        MemberBLL memberBLL = new MemberBLL();
        CommentBLL commentBLL = new CommentBLL();
        ChildBLL childBLL = new ChildBLL();
        GeneralAttendanceBLL generalAttendanceBLL = new GeneralAttendanceBLL();
        PersonalAttendanceBLL personalAttendanceBLL = new PersonalAttendanceBLL();
        EventsBLL eventBLL = new EventsBLL();
        FinancialReportBLL finBLL = new FinancialReportBLL();
        FormProperties initialDetail = new FormProperties();
        GraphBLL graphBLL = new GraphBLL();
        ExpenditureBLL expenditureBLL = new ExpenditureBLL();
        FinedMemberBLL finedMemberBLL = new FinedMemberBLL();
        public bool isAdmin = false;
        public bool isEditor = false;
        private void FormDashboard_Load(object sender, EventArgs e)
        {
            #region
            labelName.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            labelSurname.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            labelAccessLevel.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            labelPosition.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label3.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label6.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            labelDuesMonthName.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            labelTotalDuesYear.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label13.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label16.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label11.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label14.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label4.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label12.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            labelCommentMonthName.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label7.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label5.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label8.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label9.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            labelAmountRaisedYearly.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            labelExpendituresYearly.Font = new Font("Segoe UI", 16, FontStyle.Bold);

            labelNoOfRegMem.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            labelNoOfChildren.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            labelMonthlyDues.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            labelYearlyDues.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            labelExpendituresInYear.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            labelTotalExpenditures.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            labelLastMeetingAttendance.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            labelLastEventDate.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            labelTotalPaidFines.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            labelTotalFineExpected.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            labelMonthlyComments.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            labelTotalComments.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            #endregion

            picProfilePic.SizeMode = PictureBoxSizeMode.StretchImage;
            picProfilePic.BorderStyle = BorderStyle.None;
            picProfilePic.Width = picProfilePic.Height = 40;
            picProfilePic.Paint += new PaintEventHandler(picProfilePic_Paint);

            int minWidthPercentage = 70;
            int minHeightPercentage = 70;
            int minWidth = Screen.PrimaryScreen.Bounds.Width * minWidthPercentage / 100;
            int minHeight = Screen.PrimaryScreen.Bounds.Height * minHeightPercentage / 100;
            this.MinimumSize = new Size(minWidth, minHeight);

            MemberDTO memberDTO = memberBLL.Select();
            MemberDetailDTO detail = memberDTO.Members.First(x => x.MemberID == LoginInfo.MemberID);
            string imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
            picProfilePic.ImageLocation = imagePath;
            labelName.Text = detail.Name;
            labelSurname.Text = detail.Surname;
            labelAccessLevel.Text = detail.PermissionName;
            labelPosition.Text = detail.PositionName;

            if (!isAdmin && !isEditor)
            {
                tableLayoutPanelRealCards.Hide();
                btnAttendance.Hide();
                btnFinancialReport.Hide();
                btnEvents.Hide();
                btnDocuments.Hide();
                btnManage.Hide();
                btnMembers.Text = "    Profile";
                btnMembers.Location = new Point(0, 118);
                
            }

            initialDetail.StartPosition = FormStartPosition.Manual;
            initialDetail.Location = this.Location;
            initialDetail.Size = this.Size;
            initialDetail.WindowState = this.WindowState;

            this.ControlBox = false;
            RefreshAllCards();            
        }       

        private void picProfilePic_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, picProfilePic.Width - 1, picProfilePic.Height - 1);
            Region rg = new Region(gp);
            picProfilePic.Region = rg;
        }
        
        private void RefreshAllCards()
        {
            string yearlyDues = "SELECT PERSONAL_ATTENDANCE.year, SUM(PERSONAL_ATTENDANCE.monthlyDues)\r\n" +
            "FROM PERSONAL_ATTENDANCE\r\n" +
            "WHERE PERSONAL_ATTENDANCE.isDeleted = 0\r\n" +
            "GROUP BY PERSONAL_ATTENDANCE.year\r\n" +
            "ORDER BY PERSONAL_ATTENDANCE.year ASC";
            string yearlyExpenditures = "SELECT EXPENDITURE.year, SUM(EXPENDITURE.amountSpent)\r\n" +
                "FROM EXPENDITURE\r\n" +
                "WHERE EXPENDITURE.isDeleted = 0\r\n" +
                "GROUP BY EXPENDITURE.year\r\n" +
                "ORDER BY EXPENDITURE.year ASC";

            labelNoOfRegMem.Text = memberBLL.SelectAllMembersCount().ToString();
            labelTotalComments.Text = commentBLL.SelectAllCommentsCount().ToString();
            labelMonthlyComments.Text = commentBLL.SelectMonthlyCommentsCount().ToString();
            labelNoOfChildren.Text = childBLL.SelectAllChildren().ToString();
            labelLastMeetingAttendance.Text = personalAttendanceBLL.SelectLastMeetingAttendance().ToString();
            labelLastEventDate.Text = eventBLL.SelectRecentEvent();

            string monthToday = DateTime.Now.ToString("MMMM");
            string yearToday = DateTime.Now.Year.ToString();
            if (Convert.ToInt32(labelMonthlyComments.Text) > 1)
            {
                labelCommentMonthName.Text = "Comments in " + monthToday + " " + yearToday;
            }
            else
            {
                labelCommentMonthName.Text = "Comment in " + monthToday + " " + yearToday;
            }
            int todayMonth = DateTime.Now.Month;
            int todayYear = DateTime.Today.Year;
            labelDuesMonthName.Text = "Dues in "+ monthToday + " "+ yearToday;
            labelTotalDuesYear.Text = "Total Dues + Fines in " + yearToday;
            label13.Text = "Expenditures in " + yearToday;

            labelMonthlyDues.Text = "€ " + finBLL.SelectTotalRaisedAmountMonthly(todayMonth);
            labelYearlyDues.Text = "€ " + finBLL.SelectTotalRaisedAmountYearly(todayYear);
            labelExpendituresInYear.Text = "€ " + expenditureBLL.SelectTotalExpendituresYearly(todayYear);
            labelTotalExpenditures.Text = "€ " + expenditureBLL.SelectTotalExpenditures();
            labelTotalFineExpected.Text = "€ " + finedMemberBLL.SelectTotalFinedExpected();
            labelTotalPaidFines.Text = "€ " + finedMemberBLL.SelectTotalPaidFines();

            labelAmountRaisedYearly.Text = "Dues Raised in each year";
            labelExpendituresYearly.Text = "Expenditures in each year";
            General.CreateChart(chartAmountRaisedYearly, yearlyDues, SeriesChartType.Column, "Dues", "");
            General.CreateChart(chartExpenditures, yearlyExpenditures, SeriesChartType.Column, "Expenses", "");
        }

        private void iconClose_MouseEnter(object sender, EventArgs e)
        {
            iconClose.BackColor = Color.DarkOliveGreen;
        }

        private void iconClose_MouseHover(object sender, EventArgs e)
        {
            iconClose.BackColor = Color.DarkOliveGreen;
        }

        private void iconClose_MouseLeave(object sender, EventArgs e)
        {
            iconClose.BackColor = Color.DarkOliveGreen;
        }
        private void picBoxMin_Click_1(object sender, EventArgs e)
        {

        }
        private void iconClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconMaximize_MouseEnter(object sender, EventArgs e)
        {
            iconMaximize.BackColor = Color.DarkOliveGreen;
        }

        private void iconMaximize_MouseHover(object sender, EventArgs e)
        {
            iconMaximize.BackColor = Color.DarkOliveGreen;
        }

        private void iconMaximize_MouseLeave(object sender, EventArgs e)
        {
            iconMaximize.BackColor = Color.DarkOliveGreen;
        }

        private void iconMaximize_Click_1(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.StartPosition = initialDetail.StartPosition;
                this.Location = initialDetail.Location;
                this.Size = initialDetail.Size;
                this.WindowState = initialDetail.WindowState;
            }
        }

        private void iconMinimize_Click(object sender, EventArgs e)
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

        private void iconMinimize_MouseEnter(object sender, EventArgs e)
        {
            iconMinimize.BackColor = Color.DarkOliveGreen;
        }

        private void iconMinimize_MouseHover(object sender, EventArgs e)
        {
            iconMinimize.BackColor = Color.DarkOliveGreen;
        }

        private void iconMinimize_MouseLeave(object sender, EventArgs e)
        {
            iconMinimize.BackColor = Color.DarkOliveGreen;
        }
        private bool buttonWasClicked = false;
        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            if (buttonWasClicked)
            {
                currentChildForm.Close();
                Reset();
            }
        }

        private void btnMembers_Click_1(object sender, EventArgs e)
        {            
            int memberCount = memberBLL.SelectAllMembersCount();
            if (memberCount > 0)
            {
                if (!isAdmin && !isEditor)
                {
                    MemberDTO dto = memberBLL.Select();
                    MemberDetailDTO detail = dto.Members.First(x => x.MemberID == LoginInfo.MemberID);
                    FormViewMember open = new FormViewMember();
                    open.detail = detail;
                    open.isView = true;
                    this.Hide();
                    open.ShowDialog();
                    this.Visible = true;
                }
                else
                {
                    buttonWasClicked = true;
                    ActivateButton(sender, RBGColors.color2);
                    OpenChildForm(new FormMembersBoard());
                }

            }
        }

        private void btnFinancialReport_Click_1(object sender, EventArgs e)
        {
            buttonWasClicked = true;
            ActivateButton(sender, RBGColors.color2);
            OpenChildForm(new FormReportsBoard());
        }

        private void btnManage_Click_1(object sender, EventArgs e)
        {
            buttonWasClicked = true;
            ActivateButton(sender, RBGColors.color2);
            OpenChildForm(new FormSettings());            
        }

        private void labelLogo_Click(object sender, EventArgs e)
        {
            if (buttonWasClicked)
            {
                currentChildForm.Close();
                Reset();                
            }
        }

        private void panelDesktop_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            FormLogin open = new FormLogin();
            this.Hide();
            open.ShowDialog();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelRegMembers_Click(object sender, EventArgs e)
        {
            btnMembers.PerformClick();
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            buttonWasClicked = true;
            ActivateButton(sender, RBGColors.color2);
            OpenChildForm(new FormMeetingBoard());
        }

        private void btnEvents_Click(object sender, EventArgs e)
        {
            buttonWasClicked = true;
            ActivateButton(sender, RBGColors.color2);
            OpenChildForm(new FormEventsList());
        }

        private void btnDocuments_Click(object sender, EventArgs e)
        {
            buttonWasClicked = true;
            ActivateButton(sender, RBGColors.color2);
            OpenChildForm(new FormDocumentList());
        }

        private void panelMeetingAttend_Click(object sender, EventArgs e)
        {
            btnAttendance.PerformClick();
        }

        private void panelLastEvent_Click(object sender, EventArgs e)
        {
            btnEvents.PerformClick();
        }

        private void panelMonthlyDues_Click(object sender, EventArgs e)
        {
            btnAttendance.PerformClick();
        }

        private void panelYearlyDues_Click(object sender, EventArgs e)
        {
            btnAttendance.PerformClick();
        }

        public void TriggerEventButtonOnClick()
        {
            btnEvents.PerformClick();
        }

        private void labelLastEventDate_Click(object sender, EventArgs e)
        {

        }

        private void panelLastEvent_Click_1(object sender, EventArgs e)
        {
            btnEvents.PerformClick();
        }
    }
}
