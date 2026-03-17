using APC.BLL;
using APC.DAL;
using APC.Domain.Interfaces;
using APC.Helper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormLogin : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMemberService _memberService;
        private readonly ICurrentUserService _currentUserService;
        public FormLogin(IServiceProvider serviceProvider, IMemberService memberService, ICurrentUserService currentUserService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _memberService = memberService;
            _currentUserService = currentUserService;
        }

        // Drag From
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void FormLogin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() == "" || txtUsername.Text.Trim() == "")
            {
                MessageBox.Show("Enter password and username");
            }
            else
            {
                var auth = _memberService.Authenticate(txtUsername.Text, txtPassword.Text);

                if (auth == null)
                {
                    MessageBox.Show("Invalid username or password");
                    return;
                }
                else
                {
                    int absenteesCount = _memberService.Get3MonthsAbsentesCount();
                    if (_currentUserService.AccessLevel == 4)
                    {
                        if (absenteesCount > 0)
                        {
                            var form = _serviceProvider.GetRequiredService<FormNotifications>();
                            form.isAdmin = true;
                            form.isLogin = true;
                            form.numbers = absenteesCount;
                            this.Hide();
                            form.ShowDialog();
                        }
                        else
                        {
                            var form = _serviceProvider.GetRequiredService<FormDashboard>();
                            this.Hide();
                            form.isAdmin = true;
                            form.ShowDialog();
                        }
                    }
                    else if (_currentUserService.AccessLevel == 3)
                    {
                        if (absenteesCount > 0)
                        {
                            var form = _serviceProvider.GetRequiredService<FormNotifications>();
                            form.isEditor = true;
                            form.isLogin = true;
                            this.Hide();
                            form.ShowDialog();
                        }
                        else
                        {
                            var form = _serviceProvider.GetRequiredService<FormDashboard>();
                            this.Hide();
                            form.isEditor = true;
                            form.ShowDialog();
                        }
                    }
                    else
                    {
                        var form = _serviceProvider.GetRequiredService<FormDashboard>();
                        this.Hide();
                        form.ShowDialog();
                    }
                }
            }
        }

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(14, labelTitle, label1, label2, btnClose, btnEnter);
            GeneralHelper.ApplyRegularFont(14, txtPassword, txtUsername);
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            resizeControls();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
