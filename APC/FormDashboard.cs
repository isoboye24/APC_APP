using APC.AllForms;
using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.BLL;
using APC.Helper;
using APC.Utility;
using FontAwesome.Sharp;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace APC
{
    public partial class FormDashboard : BaseForm
    {
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;

        private float globalFontSize = 14.0f;
        private float resizeFactor = 16.0f;

        private int minWidthPercentage = 70;
        private int minHeightPercentage = 70;        

        private readonly IServiceProvider _serviceProvider;
        private readonly IMemberService _memberService;
        private readonly IGeneralMeetingService _generalMeetingService;
        private readonly IGeneralMeetingAttendanceService _generalMeetingAttendanceService;
        private readonly IFinedMemberService _finedMemberService;

        private readonly ICommentService _commentService;
        private readonly IGenderService _genderService;
        private readonly IMonthService _monthService;
        private readonly IConstitutionService _constitutionService;
        private readonly ISpecialContributionService _specialContributionService;

        private readonly ISpecialContributorService _specialContributorService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IGraphicalRepresentationService _graphicalRepresentationService;
        private readonly IFinancialReportService _financialReportService;
        private readonly IExpenditureService _expenditureService;

        public FormDashboard(IServiceProvider serviceProvider, IFinedMemberService finedMemberService, IMemberService memberService,
            IGeneralMeetingAttendanceService generalMeetingAttendanceService, IGeneralMeetingService generalMeetingService,
            ICommentService commentService, IGenderService genderService, IMonthService monthService, IConstitutionService constitutionService, 
            ISpecialContributionService specialContributionService, ISpecialContributorService specialContributorService,
            ICurrentUserService currentUserService, IGraphicalRepresentationService graphicalRepresentationService,
            IFinancialReportService financialReportService, IExpenditureService expenditureService
            )
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(5, 40);
            tableLayoutPanelSidebar.Controls.Add(leftBorderBtn);
            //Form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;

            _serviceProvider = serviceProvider;
            _finedMemberService = finedMemberService;
            _memberService = memberService;
            _generalMeetingAttendanceService = generalMeetingAttendanceService;
            _generalMeetingService = generalMeetingService;
            _commentService = commentService;
            _genderService = genderService;
            _monthService = monthService;
            _constitutionService = constitutionService;
            _specialContributionService = specialContributionService;
            _specialContributorService = specialContributorService;
            _currentUserService = currentUserService;
            _graphicalRepresentationService = graphicalRepresentationService;
            _financialReportService = financialReportService;
            _expenditureService = expenditureService;
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

        FormProperties initialDetail = new FormProperties();
        public bool isAdmin = false;
        public bool isEditor = false;

        private float buttonSize = 14f;
        private float panelSize;

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(16, labelDuesMonthName, labelTotalDuesYear, labelAmountRaisedYearly, labelExpendituresYearly);
            GeneralHelper.ApplyBoldFont(24, labelNoOfRegMem, labelMonthlyDues, labelYearlyDues, labelExpendituresInYear, labelTotalExpenditures,
                labelLastMeetingAttendance, labelTotalPaidFines, labelTotalFineExpected
                );
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            resizeControls();

            labelAmountRaisedYearly.Tag = "resizable";
            int minWidth = Screen.PrimaryScreen.Bounds.Width * minWidthPercentage / 100;
            int minHeight = Screen.PrimaryScreen.Bounds.Height * minHeightPercentage / 100;

            this.MinimumSize = new Size(minWidth, minHeight);

            if (!isAdmin && !isEditor)
            {
                tableLayoutPanelCards.Hide();
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
            ResizeableControls();
        }       

        private void picProfilePic_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void ResizeableControls()
        {
            label18.Tag = "resizable";
            //label1.Tag = "resizable";
            label19.Tag = "resizable";
            label21.Tag = "resizable";
            label23.Tag = "resizable";
            label24.Tag = "resizable";
            labelAmountRaisedYearly.Tag = "resizable";
            labelDuesMonthName.Tag = "resizable";
            labelExpendituresInYear.Tag = "resizable";
            labelExpendituresYearly.Tag = "resizable";
            labelExpensesInThisYear.Tag = "resizable";
            labelLastMeetingAttendance.Tag = "resizable";
            labelMonthlyDues.Tag = "resizable";
            labelNoOfRegMem.Tag = "resizable";
            labelTitleChildForm.Tag = "resizable";
            labelTotalDuesYear.Tag = "resizable";
            labelTotalExpenditures.Tag = "resizable";
            labelTotalFineExpected.Tag = "resizable";
            labelTotalPaidFines.Tag = "resizable";
            labelYearlyDues.Tag = "resizable";


            //btnAttendance.Tag = "resizable";
            //btnDocuments.Tag = "resizable";
            //btnEvents.Tag = "resizable";
            //btnFinancialReport.Tag = "resizable";
            //btnManage.Tag = "resizable";
            //btnMembers.Tag = "resizable";
        }

        private void loadAnnualRaisedDues()
        {
            var data = _graphicalRepresentationService.GetAllAnnualRaisedDues();

            chartAmountRaisedYearly.Series.Clear();

            chartAmountRaisedYearly.DataSource = data;

            chartAmountRaisedYearly.Series.Add("TotalAmountRaised");
            chartAmountRaisedYearly.Series["TotalAmountRaised"].ChartType = SeriesChartType.Column;
            chartAmountRaisedYearly.Series["TotalAmountRaised"].XValueMember = "Year";
            chartAmountRaisedYearly.Series["TotalAmountRaised"].YValueMembers = "TotalAmountRaised";
            chartAmountRaisedYearly.Series["TotalAmountRaised"].IsValueShownAsLabel = true;

            chartAmountRaisedYearly.DataBind();

            chartAmountRaisedYearly.Titles.Clear();
            //labelGraphTitleAnnualReport.Text = $"{year} Report";
        }

        private void loadAnnualExpenditures()
        {
            var data = _graphicalRepresentationService.GetAllAnnualExpenditures();

            chartExpenditures.Series.Clear();

            chartExpenditures.DataSource = data;

            chartExpenditures.Series.Add("TotalAmountSpent");
            chartExpenditures.Series["TotalAmountSpent"].ChartType = SeriesChartType.Column;
            chartExpenditures.Series["TotalAmountSpent"].XValueMember = "Year";
            chartExpenditures.Series["TotalAmountSpent"].YValueMembers = "TotalAmountSpent";
            chartExpenditures.Series["TotalAmountSpent"].IsValueShownAsLabel = true;

            chartExpenditures.DataBind();

            chartExpenditures.Titles.Clear();
            //labelGraphTitleAnnualReport.Text = $"{year} Report";
        }

        private void RefreshAllCards()
        {
            loadAnnualRaisedDues();

            loadAnnualExpenditures();

            labelNoOfRegMem.Text = _memberService.GetAllCurrentMembersCount().ToString();
            labelLastMeetingAttendance.Text = _generalMeetingAttendanceService.GetLastMeetingPresentMembersCount().ToString();
            //labelLastEventDate.Text = eventBLL.SelectRecentEvent();

            string monthToday = DateTime.Now.ToString("MMMM");

            int todayMonth = DateTime.Now.Month;
            int todayYear = DateTime.Today.Year;

            labelDuesMonthName.Text = "Dues in "+ monthToday + " "+ todayYear;
            labelExpensesInThisYear.Text = "Total Expenses in " + todayYear;
            labelTotalDuesYear.Text = "Total Dues + Fines in " + todayYear;

            labelMonthlyDues.Text = "€ " + _financialReportService.GetTotalDuesByMonth(todayMonth, todayYear);
            labelYearlyDues.Text = "€ " + _financialReportService.GetTotalDuesByYear(todayYear);

            labelExpendituresInYear.Text = "€ " + _financialReportService.GetOverallExpendituresByYear(todayYear);
            labelTotalExpenditures.Text = "€ " + _financialReportService.GetOverallExpenditures();
            labelTotalFineExpected.Text = "€ " + _finedMemberService.GetTotalFinesExpected();
            labelTotalPaidFines.Text = "€ " + _finedMemberService.GetTotalPaidFines();

            labelAmountRaisedYearly.Text = "Dues Raised in each year";
            labelExpendituresYearly.Text = "Expenditures in each year";            
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
                ZoomManager.ZoomIn(this);

                buttonSize = 18f;
                panelSize = 3.05f;
                ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
            }
            else
            {
                this.StartPosition = initialDetail.StartPosition;
                this.Location = initialDetail.Location;
                this.Size = initialDetail.Size;
                this.WindowState = initialDetail.WindowState;
                ZoomManager.ZoomIn(this);

                buttonSize = 14f;
                panelSize = 1.05f;
                ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
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
                ZoomManager.ZoomOut(this);

                buttonSize = 14f;
                panelSize = 1.05f;
                ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
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
            int memberCount = _memberService.Count();

            if (memberCount <= 0)
                return;

            if (!isAdmin && !isEditor)
            {
                MemberFullDetailsDTO detail = _memberService.GetMemberById(_currentUserService.MemberId);

                if (detail == null)
                {
                    MessageBox.Show("Member not found.");
                    return;
                }

                var form = _serviceProvider.GetRequiredService<FormViewMember>();

                form.MemberDetail(detail);
                form.isView = true;

                Hide();
                form.ShowDialog();
                Show();
            }
            else
            {
                buttonWasClicked = true;
                ActivateButton(sender, RBGColors.color2);

                var form = _serviceProvider.GetRequiredService<FormMembersBoard>();

                OpenChildForm(form);
            }
        }

        private void btnFinancialReport_Click_1(object sender, EventArgs e)
        {
            buttonWasClicked = true;
            ActivateButton(sender, RBGColors.color2);

            var form = _serviceProvider.GetRequiredService<FormReportsBoard>();
            OpenChildForm(form);
        }

        private void btnManage_Click_1(object sender, EventArgs e)
        {
            buttonWasClicked = true;
            ActivateButton(sender, RBGColors.color2);

            var form = _serviceProvider.GetRequiredService<FormSettings>();
            OpenChildForm(form);
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
            var form = _serviceProvider.GetRequiredService<FormLogin>();
            this.Hide();
            form.ShowDialog();
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

            var form = _serviceProvider.GetRequiredService<FormMeetingBoard>();
            OpenChildForm(form);
        }

        private void btnEvents_Click(object sender, EventArgs e)
        {
            buttonWasClicked = true;
            ActivateButton(sender, RBGColors.color2);

            var form = _serviceProvider.GetRequiredService<FormEventsList>();
            OpenChildForm(form);
        }

        private void btnDocuments_Click(object sender, EventArgs e)
        {
            buttonWasClicked = true;
            ActivateButton(sender, RBGColors.color2);

            var form = _serviceProvider.GetRequiredService<FormDocumentList>();
            OpenChildForm(form);
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

        private void iconZoomIn_Click(object sender, EventArgs e)
        {
            ZoomManager.ZoomIn(this);
        }

        private void iconZoomOut_Click(object sender, EventArgs e)
        {
            ZoomManager.ZoomOut(this);
        }
    }
}
