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

namespace APC
{
    public partial class FormEmploymentStatus : Form
    {
        public FormEmploymentStatus()
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
        private void FormEmploymentStatus_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        EmploymentStatusBLL bll = new EmploymentStatusBLL();
        public EmploymentStatusDetailDTO detail = new EmploymentStatusDetailDTO();
        public bool isUpdate = false;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtEmpStatus.Text.Trim()=="")
            {
                MessageBox.Show("Employment status is empty");
            }
            else
            {
                if (!isUpdate)
                {
                    EmploymentStatusDetailDTO employmentStatus = new EmploymentStatusDetailDTO();
                    employmentStatus.EmploymentStatus = txtEmpStatus.Text;
                    if (bll.Insert(employmentStatus))
                    {
                        MessageBox.Show("Employment status was added");
                        txtEmpStatus.Clear();
                    }
                }
                else if (isUpdate)
                {
                    if (detail.EmploymentStatus==txtEmpStatus.Text.Trim())
                    {
                        MessageBox.Show("There is no change");
                    }
                    else
                    {
                        detail.EmploymentStatus = txtEmpStatus.Text;
                        if (bll.Update(detail))
                        {
                            MessageBox.Show("Employment status was updated");
                            this.Close();
                        }
                    }
                }
            }
        }
        private void FormEmploymentStatus_Load(object sender, EventArgs e)
        {
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            txtEmpStatus.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSave.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            if (isUpdate)
            {
                txtEmpStatus.Text = detail.EmploymentStatus;
                labelTitle.Text = "Edit Employement Status";
            }
            else
            {
                labelTitle.Text = "Add Employement Status";
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
