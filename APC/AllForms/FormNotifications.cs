using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Helper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static APC.Helper.MemberHelper;

namespace APC.AllForms
{
    public partial class FormNotifications : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMemberService _memberService;

        private List<MemberFullDetailsDTO> _absentees;

        private bool _isLogin = false;
        private bool _isAdmin = false;
        private bool _isEditor = false;

        public FormNotifications(IServiceProvider serviceProvider, IMemberService memberService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _memberService = memberService;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (_isLogin && _isAdmin)
            {
                var form = _serviceProvider.GetRequiredService<FormDashboard>();
                this.Hide();
                form.AccessControl(true, false);
                form.ShowDialog();
            }
            else if (_isLogin && _isEditor)
            {
                var form = _serviceProvider.GetRequiredService<FormDashboard>();
                this.Hide();
                form.AccessControl(false, true);
                form.ShowDialog();
            }
            else
            {
                this.Close();
            }
        }

        private void loadMembers()
        {
            dataGridView1.DataSource = _memberService.Get3MonthsAbsentes();
            _absentees = _memberService.Get3MonthsAbsentes();
            ConfigureMemberGrid(dataGridView1, MemberGridType.SemiBasic);
        }

        public void AccessControl(bool isLogin, bool isAdmin, bool isEditor)
        {
            _isLogin = isLogin;
            _isAdmin = isAdmin;
            _isEditor = isEditor;
        }

        private void controlsFont()
        {
            GeneralHelper.ApplyBoldFont(14, labelTitle, label1, txtSearchSurname, btnClose);
        }

        private void FormNotifications_Load(object sender, EventArgs e)
        {
            controlsFont();

            loadMembers();

            int absenteesCount = _memberService.Get3MonthsAbsentesCount();

            labelTitle.Text = "MEMBER" + (absenteesCount > 2 ? "S " : " ") + "ABSENT FOR ATLEAST THE LAST 3 MONTHS";
        }
    }
}
