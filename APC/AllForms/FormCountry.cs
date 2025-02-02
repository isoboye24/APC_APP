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
using System.Xml.Linq;

namespace APC
{
    public partial class FormCountry : Form
    {
        public FormCountry()
        {
            InitializeComponent();
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
        CountryBLL bll = new CountryBLL();
        public CountryDetailDTO detail = new CountryDetailDTO();
        public bool isUpdate = false;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCountry.Text.Trim()=="")
            {
                MessageBox.Show("Country is empty");
            }
            else
            {
                if (!isUpdate)
                {
                    CountryDetailDTO country = new CountryDetailDTO();
                    country.CountryName = txtCountry.Text;
                    if (bll.Insert(country))
                    {
                        MessageBox.Show("Country was added");
                        txtCountry.Clear();
                    }
                }
                else
                {
                    if (detail.CountryName == txtCountry.Text.Trim())
                    {
                        MessageBox.Show("There is no change");
                    }
                    else if(isUpdate)
                    {
                        detail.CountryName = txtCountry.Text;
                        if (bll.Update(detail))
                        {
                            MessageBox.Show("Country was updated");
                            this.Close();
                        }
                    }
                }
            }
        }

        private void FormCountry_Load(object sender, EventArgs e)
        {
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            txtCountry.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSave.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            if (isUpdate)
            {
                txtCountry.Text = detail.CountryName;
                labelTitle.Text = "Edit Country";
            }
            else
            {
                labelTitle.Text = "Add Country";
            }
        }        
    }
}
