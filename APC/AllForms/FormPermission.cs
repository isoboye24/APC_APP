using APC.Applications.Services;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using APC.Helper;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormPermission : Form
    {
        private readonly IPermissionService _permissionService;

        private int _permissionId = 0;
        private bool _isUpdate = false;

        public FormPermission(IPermissionService permissionService)
        {
            InitializeComponent();
            _permissionService = permissionService;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void FormPermission_MouseDown(object sender, MouseEventArgs e)
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
            _permissionId = id;
            txtPermission.Text = name;
            _isUpdate = isUpdate;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var name = txtPermission.Text.Trim();

                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Please enter permission");
                    return;
                }

                if (_permissionId == 0)
                {
                    var permission = new Permission(name);
                    _permissionService.Create(permission);
                    MessageBox.Show("Permission created successfully!");
                }
                else
                {
                    var permission = new Permission(name);
                    permission.SetId(_permissionId);

                    _permissionService.Update(permission);
                    MessageBox.Show("Permission updated successfully!");
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
            GeneralHelper.ApplyRegularFont(14, txtPermission);
        }

        private void FormPermission_Load(object sender, EventArgs e)
        {
            resizeControls();

            if (_isUpdate)
            {
                labelTitle.Text = "Edit Permission";
            }
            else
            {
                labelTitle.Text = "Add Permission";
            }
        }
    }
}
