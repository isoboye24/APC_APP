using APC.BLL;
using APC.DAL;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormViewConstitution : Form
    {
        public FormViewConstitution()
        {
            InitializeComponent();
        }
        // Drag From
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);

        public ConstitutionDetailDTO detail = new ConstitutionDetailDTO();
        ConstitutionBLL bll = new ConstitutionBLL();
        public bool isFinedMemberView = false;
        public int ID;

        private void FormViewConstitution_Load(object sender, EventArgs e)
        {
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelFine.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelSection.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            txtConstitution.Text = detail.ConstitutionText;
            labelFine.Text = "€ " + detail.Fine;
            labelSection.Text = detail.Section;
            if (isFinedMemberView)
            {

                List<CONSTITUTION> singleConstitution = bll.GetSingleConstitution(ID);
                if (singleConstitution.Count == 0)
                {
                    MessageBox.Show("This constitution does not exist.");
                }
                else if (singleConstitution.Count > 0)
                {
                    CONSTITUTION constit = new CONSTITUTION();
                    constit = singleConstitution.First();
                    txtConstitution.Text = constit.constitution1;
                    labelFine.Text = "€ " + constit.fine;
                    labelSection.Text = constit.section;
                }
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
    }
}
