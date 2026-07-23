using APC.AllForms;
using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Helper;
using APC.Utility;
using FontAwesome.Sharp;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace APC
{
    public partial class FormDashboard : BaseFormDashboard
    {            
        private readonly IServiceProvider _serviceProvider;
        private readonly IMemberService _memberService;
        private readonly IGeneralMeetingService _generalMeetingService;
        private readonly IGeneralMeetingAttendanceService _generalMeetingAttendanceService;
        private readonly IFinedMemberService _finedMemberService;

        private readonly IGenderService _genderService;
        private readonly IMonthService _monthService;
        private readonly IConstitutionService _constitutionService;
        private readonly ISpecialContributionService _specialContributionService;

        private readonly ISpecialContributorService _specialContributorService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IGraphicalRepresentationService _graphicalRepresentationService;
        private readonly IFinancialReportService _financialReportService;
        private readonly IExpenditureService _expenditureService;

        FormProperties initialDetail = new FormProperties();
        private bool _isAdmin = false;
        private bool _isEditor = false;

        private float buttonSize = 14f;
        private float panelSize;
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;

        private int minWidthPercentage = 70;
        private int minHeightPercentage = 70;
        private bool buttonWasClicked = false;

        private DateTime todaysDate = DateTime.Today;
        Color hoverColor = Color.MediumBlue;

        public FormDashboard(IServiceProvider serviceProvider, IFinedMemberService finedMemberService, IMemberService memberService,
            IGeneralMeetingAttendanceService generalMeetingAttendanceService, IGeneralMeetingService generalMeetingService,
            IGenderService genderService, IMonthService monthService, IConstitutionService constitutionService, 
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
            dashboardTitleChildForm.Text = "Dashboard";
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
            dashboardTitleChildForm.Text = childForm.Text;
        }

        private void picBoxMin_Click(object sender, EventArgs e)
        {

        }

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(20, dvNoOfRegMemLabel, dvMonthlyDuesLabel, dvYearlyDuesAndFinesLabel, dvExpendituresInYearLabel, dvTotalAnnualRevenueLabel,
                dvLastMeetingAttendanceLabel, dvTotalPaidFinesLabel, dvTotalFineExpectedLabel
                );

            GeneralHelper.ApplyBoldFont(17, dashboardDuesMonthName, dashboardTotalDuesYear, dashboardAmountRaisedYearly, dashboardExpendituresYearly, dashboardRegister, dashboardMeetingAttLabel, 
                dashboardPaidFinesInYear, dashboardRevenue, dashboardTotalExpectedFinesInYear, dashboardExpensesInThisYear);

            GeneralHelper.ApplyBoldFont(14, doTodaysDate);
            GeneralHelper.ApplyBoldFont(10, doDescriptionLabel);
        }

        public void AccessControl(bool isAdmin, bool isEditor)
        {
            _isAdmin = isAdmin;
            _isEditor = isEditor;
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            resizeControls();

            int minWidth = Screen.PrimaryScreen.Bounds.Width * minWidthPercentage / 100;
            int minHeight = Screen.PrimaryScreen.Bounds.Height * minHeightPercentage / 100;

            this.MinimumSize = new Size(minWidth, minHeight);

            if (!_isAdmin && !_isEditor)
            {
                dashboardBtnAttendance.Hide();
                dashboardBtnFinancialReport.Hide();
                dashboardBtnEvents.Hide();
                dashboardBtnDocuments.Hide();
                dashboardBtnManage.Hide();
                dashboardBtnMembers.Text = "    Profile";
                dashboardBtnMembers.Location = new Point(0, 118);
                
            }

            initialDetail.StartPosition = FormStartPosition.Manual;
            initialDetail.Location = this.Location;
            initialDetail.Size = this.Size;
            initialDetail.WindowState = this.WindowState;

            this.ControlBox = false;
            RefreshAllCards();
            ResizeableControls();

            doTodaysDate.Text = "Today: " + todaysDate.ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo("en-US"));
        }       

        private void picProfilePic_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void ResizeableControls()
        {
            //label1.Tag = "resizable";
            dvExpendituresInYearLabel.Tag = "resizable";

            dvLastMeetingAttendanceLabel.Tag = "resizable";
            dvMonthlyDuesLabel.Tag = "resizable";
            dvNoOfRegMemLabel.Tag = "resizable";
            dashboardTitleChildForm.Tag = "resizable";

            dvTotalAnnualRevenueLabel.Tag = "resizable";
            dvTotalFineExpectedLabel.Tag = "resizable";
            dvTotalPaidFinesLabel.Tag = "resizable";
            dvYearlyDuesAndFinesLabel.Tag = "resizable";
        }

        private void loadAnnualRaisedDues()
        {
            var data = _graphicalRepresentationService.GetAllAnnualRaisedDues();

            chartAmountRaisedYearly.Series.Clear();

            chartAmountRaisedYearly.DataSource = data;

            chartAmountRaisedYearly.Series.Add("Amount");

            chartAmountRaisedYearly.Series["Amount"].XValueMember = "Year";
            chartAmountRaisedYearly.Series["Amount"].YValueMembers = "Amount";
            chartAmountRaisedYearly.Series["Amount"].IsValueShownAsLabel = true;

            chartAmountRaisedYearly.DataBind();

            chartAmountRaisedYearly.Titles.Clear();
            //labelGraphTitleAnnualReport.Text = $"{year} Report";
        }

        private void loadAnnualExpenditures()
        {
            var data = _graphicalRepresentationService.GetAllAnnualExpenditures();

            chartExpenditures.Series.Clear();

            chartExpenditures.DataSource = data;

            chartExpenditures.Series.Add("Amount");
            chartExpenditures.Series["Amount"].ChartType = SeriesChartType.Column;
            chartExpenditures.Series["Amount"].XValueMember = "Year";
            chartExpenditures.Series["Amount"].YValueMembers = "Amount";
            chartExpenditures.Series["Amount"].IsValueShownAsLabel = true;

            chartExpenditures.DataBind();

            chartExpenditures.Titles.Clear();
            //labelGraphTitleAnnualReport.Text = $"{year} Report";
        }

        private void RefreshAllCards()
        {
            loadAnnualRaisedDues();

            loadAnnualExpenditures();

            dvNoOfRegMemLabel.Text = _memberService.GetAllCurrentMembersCount().ToString();
            dvLastMeetingAttendanceLabel.Text = _generalMeetingAttendanceService.GetLastMeetingPresentMembersCount().ToString();

            string monthToday = DateTime.Now.ToString("MMMM", CultureInfo.GetCultureInfo("en-US"));

            int todayMonth = DateTime.Now.Month;
            int todayYear = DateTime.Today.Year;

            dashboardDuesMonthName.Text = "Dues in "+ monthToday + " "+ todayYear;
            dashboardExpensesInThisYear.Text = "Expenses in " + todayYear;
            dashboardTotalDuesYear.Text = "Dues + Fines in " + todayYear;
            dashboardRevenue.Text = "Revenue in " + todayYear;

            dvMonthlyDuesLabel.Text = AmountHelper.FormatAmount(_financialReportService.GetTotalDuesByMonth(todayMonth, todayYear));
            dvYearlyDuesAndFinesLabel.Text = _financialReportService.TotalDuesAndFinesInYear(todayYear);

            dvExpendituresInYearLabel.Text = AmountHelper.FormatAmount(_financialReportService.GetOverallExpendituresByYear(todayYear));
            dvTotalAnnualRevenueLabel.Text = AmountHelper.FormatAmount(_financialReportService.GetTotalAnnualRevenue(todayYear));

            dashboardTotalExpectedFinesInYear.Text = "Expected Fines in " + todayYear;
            dvTotalFineExpectedLabel.Text = AmountHelper.FormatAmount(_finedMemberService.GetAnnualFinesExpected(todayYear));
            dashboardPaidFinesInYear.Text = "Paid Fines in " + todayYear;
            dvTotalPaidFinesLabel.Text = AmountHelper.FormatAmount(_finedMemberService.GetAnnualPaidFines(todayYear));

            dashboardAmountRaisedYearly.Text = "Overall Amount Raised";
            dashboardExpendituresYearly.Text = "Overall Expenditures";
            dashboardMeetingAttLabel.Text = "Last Meeting's Attend.";
        }

        private void iconClose_MouseEnter(object sender, EventArgs e)
        {
            iconClose.BackColor = hoverColor;
        }

        private void iconClose_MouseHover(object sender, EventArgs e)
        {
            iconClose.BackColor = hoverColor;
        }

        private void iconClose_MouseLeave(object sender, EventArgs e)
        {
            iconClose.BackColor = hoverColor;
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
            iconMaximize.BackColor = hoverColor;
        }

        private void iconMaximize_MouseHover(object sender, EventArgs e)
        {
            iconMaximize.BackColor = hoverColor;
        }

        private void iconMaximize_MouseLeave(object sender, EventArgs e)
        {
            iconMaximize.BackColor = hoverColor;
        }

        private void iconMaximize_Click_1(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                //ZoomManager.ZoomIn(this);

                //buttonSize = 24f;
                //panelSize = 3.05f;
                //ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
            }
            else
            {
                this.StartPosition = initialDetail.StartPosition;
                this.Location = initialDetail.Location;
                this.Size = initialDetail.Size;
                this.WindowState = initialDetail.WindowState;
                //ZoomManager.ZoomIn(this);

                //buttonSize = 18f;
                //panelSize = 1.05f;
                //ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
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

                //buttonSize = 18f;
                //panelSize = 1.05f;
                //ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
            }
        }

        private void iconMinimize_MouseEnter(object sender, EventArgs e)
        {
            iconMinimize.BackColor = hoverColor;
        }

        private void iconMinimize_MouseHover(object sender, EventArgs e)
        {
            iconMinimize.BackColor = hoverColor;
        }

        private void iconMinimize_MouseLeave(object sender, EventArgs e)
        {
            iconMinimize.BackColor = hoverColor;
        }

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

            if (!_isAdmin && !_isEditor)
            {
                MemberFullDetailsDTO detail = _memberService.GetMemberById(_currentUserService.MemberId);

                if (detail == null)
                {
                    MessageBox.Show("Member not found.");
                    return;
                }

                var form = _serviceProvider.GetRequiredService<FormViewMember>();
                form.WindowState = this.WindowState;

                form.ViewMemberDetails(detail.MemberId);

                Hide();
                form.ShowDialog();
                Show();
            }
            else
            {
                buttonWasClicked = true;
                ActivateButton(sender, RBGColors.color2);

                var form = _serviceProvider.GetRequiredService<FormMembersBoard>();
                form.WindowState = this.WindowState;

                OpenChildForm(form);
            }
        }

        private void btnFinancialReport_Click_1(object sender, EventArgs e)
        {
            buttonWasClicked = true;
            ActivateButton(sender, RBGColors.color2);

            var form = _serviceProvider.GetRequiredService<FormReportsBoard>();
            form.WindowState = this.WindowState;
            OpenChildForm(form);
        }

        private void btnManage_Click_1(object sender, EventArgs e)
        {
            buttonWasClicked = true;
            ActivateButton(sender, RBGColors.color2);

            var form = _serviceProvider.GetRequiredService<FormSettings>();
            form.WindowState = this.WindowState;
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
            dashboardBtnMembers.PerformClick();
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            buttonWasClicked = true;
            ActivateButton(sender, RBGColors.color2);

            var form = _serviceProvider.GetRequiredService<FormMeetingBoard>();
            form.WindowState = this.WindowState;
            OpenChildForm(form);
        }

        private void btnEvents_Click(object sender, EventArgs e)
        {
            buttonWasClicked = true;
            ActivateButton(sender, RBGColors.color2);

            var form = _serviceProvider.GetRequiredService<FormEventsList>();
            form.WindowState = this.WindowState;
            OpenChildForm(form);
        }

        private void btnDocuments_Click(object sender, EventArgs e)
        {
            buttonWasClicked = true;
            ActivateButton(sender, RBGColors.color2);

            var form = _serviceProvider.GetRequiredService<FormDocumentList>();
            form.WindowState = this.WindowState;
            OpenChildForm(form);
        }

        private void panelMeetingAttend_Click(object sender, EventArgs e)
        {
            dashboardBtnAttendance.PerformClick();
        }

        private void panelLastEvent_Click(object sender, EventArgs e)
        {
            dashboardBtnEvents.PerformClick();
        }

        private void panelMonthlyDues_Click(object sender, EventArgs e)
        {
            dashboardBtnAttendance.PerformClick();
        }

        private void panelYearlyDues_Click(object sender, EventArgs e)
        {
            dashboardBtnAttendance.PerformClick();
        }

        public void TriggerEventButtonOnClick()
        {
            dashboardBtnEvents.PerformClick();
        }

        private void labelLastEventDate_Click(object sender, EventArgs e)
        {

        }

        private void panelLastEvent_Click_1(object sender, EventArgs e)
        {
            dashboardBtnEvents.PerformClick();
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
