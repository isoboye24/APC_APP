using APC.Domain.Entities;
using APC.Domain.Interfaces;
using APC.Helper;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC
{
    public partial class FormMaritalStatus : Form
    {
        private readonly IMaritalStatusService _maritalStatusService;

        private int _statusId;
        private bool _isUpdate = false;

        public FormMaritalStatus(IMaritalStatusService maritalStatusService)
        {
            InitializeComponent();
            _maritalStatusService = maritalStatusService;
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
        private void FormMaritalStatus_MouseDown(object sender, MouseEventArgs e)
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
            txtMaritalStatus.Text = name;
            _isUpdate = isUpdate;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var name = txtMaritalStatus.Text.Trim();

                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Please enter marital status");
                    return;
                }

                if (_statusId == 0)
                {
                    var maritalStatus = new MaritalStatus(name);
                    _maritalStatusService.Create(maritalStatus);
                    MessageBox.Show("Marital status created successfully!");
                }
                else
                {
                    var maritalStatus = new MaritalStatus(name);
                    maritalStatus.SetId(_statusId);

                    _maritalStatusService.Update(maritalStatus);
                    MessageBox.Show("Marital status updated successfully!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(14, labelTitle, label2, btnClose, btnSave);
            GeneralHelper.ApplyRegularFont(14, txtMaritalStatus);
        }

        private void FormMaritalStatus_Load(object sender, EventArgs e)
        {
            resizeControls();

            if (_isUpdate)
            {
                labelTitle.Text = "Edit Marital Status";
            }
            else
            {
                labelTitle.Text = "Add Marital Status";
            }
        }        
    }
}
