using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using FontAwesome.Sharp;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using APC.DAL;
using APC.BLL;
using APC.DAL.DTO;
using OfficeOpenXml.Drawing.Chart;

namespace APC.AllForms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
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
        MemberBLL memberBLL = new MemberBLL();
        
        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() == "" || txtUsername.Text.Trim() == "")
            {
                MessageBox.Show("Enter password and username");
            }
            else
            {
                List<MEMBER> memberList = memberBLL.CheckMember(txtPassword.Text, txtUsername.Text);
                if (memberList.Count == 0)
                {
                    MessageBox.Show("Member does not exist");
                }
                else
                {
                    MEMBER member = new MEMBER();
                    member = memberList.First();
                    LoginInfo.MemberID = member.memberID;
                    LoginInfo.Username = member.username;
                    LoginInfo.Password = member.password;
                    LoginInfo.AccessLevel = member.permissionID;
                    int absenteesCount = memberBLL.Select3MonthsAbsentesCount();
                    if (LoginInfo.AccessLevel == 4)
                    {
                        if (absenteesCount > 0)
                        {
                            FormNotifications open = new FormNotifications();
                            open.isAdmin = true;
                            open.isLogin = true;
                            open.numbers = absenteesCount;
                            this.Hide();
                            open.ShowDialog();
                        }
                        else
                        {
                            FormDashboard open = new FormDashboard();
                            this.Hide();
                            open.isAdmin = true;
                            open.ShowDialog();
                        }
                    }
                    else if (LoginInfo.AccessLevel == 3)
                    {
                        if (absenteesCount > 0)
                        {
                            FormNotifications open = new FormNotifications();
                            open.isEditor = true;
                            open.isLogin = true;
                            this.Hide();
                            open.ShowDialog();
                        }
                        else
                        {
                            FormDashboard open = new FormDashboard();
                            this.Hide();
                            open.isEditor = true;
                            open.ShowDialog();
                        }                        
                    }
                    else
                    {
                        FormDashboard open = new FormDashboard();
                        this.Hide();
                        open.ShowDialog();
                    }                    
                }
            }            
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            txtPassword.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtUsername.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnEnter.Font = new Font("Segoe UI", 14, FontStyle.Bold);
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
