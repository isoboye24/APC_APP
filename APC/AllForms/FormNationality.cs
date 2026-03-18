using APC.Domain.Entities;
using APC.Domain.Interfaces;
using APC.Helper;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormNationality : Form
    {
        private readonly INationalityService _nationalityService;

        private int _nationalityId;
        private bool _isUpdate = false;

        public FormNationality(INationalityService nationalityService)
        {
            InitializeComponent();
            _nationalityService = nationalityService;
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
        private void FormNationality_MouseDown(object sender, MouseEventArgs e)
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
            _nationalityId = id;
            txtNationality.Text = name;
            _isUpdate = isUpdate;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var name = txtNationality.Text.Trim();

                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Please enter nationality");
                    return;
                }

                if (_nationalityId == 0)
                {
                    var nationality = new Nationality(name);
                    _nationalityService.Create(nationality);
                    MessageBox.Show("Nationality created successfully!");
                }
                else
                {
                    var nationality = new Nationality(name);
                    nationality.SetId(_nationalityId);

                    _nationalityService.Update(nationality);
                    MessageBox.Show("Nationality updated successfully!");
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
            GeneralHelper.ApplyRegularFont(14, txtNationality);
        }

        private void FormNationality_Load(object sender, EventArgs e)
        {
            resizeControls();

            if (_isUpdate)
            {
                labelTitle.Text = "Edit Nationality";
            }
            else
            {
                labelTitle.Text = "Add Nationality";
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
