using APC.Domain.Entities;
using APC.Domain.Interfaces;
using APC.Helper;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC
{
    public partial class FormEmploymentStatus : Form
    {
        private readonly IEmploymentStatusService _employmentStatusService;

        private int _statusId = 0;
        private bool _isUpdate = false;
        public FormEmploymentStatus(IEmploymentStatusService employmentStatusService)
        {
            InitializeComponent();
            _employmentStatusService = employmentStatusService;
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
        public void LoadForEdit(int id, string name, bool isUpdate)
        {
            _statusId = id;
            txtEmpStatus.Text = name;
            _isUpdate = isUpdate;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var name = txtEmpStatus.Text.Trim();

                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Please enter employment status");
                    return;
                }

                if (_statusId == 0)
                {
                    var employmentStatus = new EmploymentStatus(name);
                    _employmentStatusService.Create(employmentStatus);
                    MessageBox.Show("Employment status created successfully!");
                }
                else
                {
                    var employmentStatus = new EmploymentStatus(name);
                    employmentStatus.SetId(_statusId);

                    _employmentStatusService.Update(employmentStatus);
                    MessageBox.Show("Employment status updated successfully!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(14, labelTitle, label2, btnClose, btnSave);
            GeneralHelper.ApplyRegularFont(14, txtEmpStatus);
        }

        private void FormEmploymentStatus_Load(object sender, EventArgs e)
        {
            resizeControls();

            if (_isUpdate)
            {
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
