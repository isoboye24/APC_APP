using APC.Domain.Entities;
using APC.Domain.Interfaces;
using APC.Helper;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC
{
    public partial class FormCountry : Form
    {
        private readonly ICountryService _countryService;

        private int _countryId = 0;
        private bool _isUpdate = false;

        public FormCountry(ICountryService countryService)
        {
            InitializeComponent();
            _countryService = countryService;
        }

        /// <summary>
        ///  Drag
        /// </summary>
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);
        
        private void panel1_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void FormCountry_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LoadForEdit(int id, string name, bool isUpdate)
        {
            _countryId = id;
            txtCountry.Text = name;
            _isUpdate = isUpdate;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var name = txtCountry.Text.Trim();

                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Please enter country");
                    return;
                }

                if (_countryId == 0)
                {
                    var country = new Country(name);
                    _countryService.Create(country);
                    MessageBox.Show("Country created successfully!");
                }
                else
                {
                    var country = new Country(name);
                    country.SetId(_countryId);

                    _countryService.Update(country);
                    MessageBox.Show("Country updated successfully!");
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
            GeneralHelper.ApplyRegularFont(14, txtCountry);
        }

        private void FormCountry_Load(object sender, EventArgs e)
        {

            resizeControls();

            if (_isUpdate)
            {
                labelTitle.Text = "Edit Country";
            }
            else
            {
                labelTitle.Text = "Add Country";
            }
        }        
    }
}
