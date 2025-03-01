using APC.BLL;
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
    public partial class FormConstitution : Form
    {
        public FormConstitution()
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
        public bool isUpdate = false;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormConstitution_Load(object sender, EventArgs e)
        {
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label3.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label4.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            txtConstitution.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtAmount.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtSection.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtShortDescription.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSave.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            if (isUpdate)
            {
                labelTitle.Text = "Edit Constitution";
                txtAmount.Text = detail.Fine.ToString();
                txtSection.Text = detail.Section;
                txtConstitution.Text = detail.ConstitutionText;
            }
            else if (!isUpdate)
            {
                labelTitle.Text = "Add Constitution";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAmount.Text.Trim() == "")
            {
                MessageBox.Show("Amount is empty.");
            }
            else if (txtSection.Text.Trim() == "")
            {
                MessageBox.Show("Title is empty.");
            }
            else if (txtConstitution.Text.Trim() == "")
            {
                MessageBox.Show("Constitution is empty.");
            }
            else if (txtShortDescription.Text.Trim() == "")
            {
                MessageBox.Show("Short description is empty.");
            }
            else
            {
                if (!isUpdate)
                {
                    ConstitutionDetailDTO constitution = new ConstitutionDetailDTO();
                    constitution.ConstitutionText = txtConstitution.Text.Trim();
                    constitution.Fine = Convert.ToInt32(txtAmount.Text.Trim());
                    constitution.Section = txtSection.Text.Trim();
                    constitution.ShortDescription = txtShortDescription.Text.Trim();
                    if (bll.Insert(constitution))
                    {
                        MessageBox.Show("Constitution is added.");
                        txtAmount.Clear();
                        txtConstitution.Clear();
                        txtSection.Clear();
                    }
                }
                else if (isUpdate)
                {
                    if (detail.ConstitutionText == txtConstitution.Text.Trim() 
                        && detail.Section == txtSection.Text.Trim()
                        && detail.Fine == Convert.ToDecimal(txtAmount.Text.Trim()) 
                        && detail.ShortDescription == txtShortDescription.Text.Trim())
                    {
                        MessageBox.Show("There is no change!");
                    }
                    else
                    {
                        detail.ConstitutionText = txtConstitution.Text.Trim();
                        detail.Fine = Convert.ToDecimal(txtAmount.Text.Trim());
                        detail.Section = txtSection.Text.Trim();
                        detail.ShortDescription = txtShortDescription.Text.Trim();
                        if (bll.Update(detail))
                        {
                            MessageBox.Show("Constitution is updated!");
                            this.Close();
                        }
                    }
                }
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

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
