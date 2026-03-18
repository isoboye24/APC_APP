using APC.Applications.Services;
using APC.BLL;
using APC.DAL.DTO;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using APC.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace APC
{
    public partial class FormProfession : Form
    {
        private readonly IProfessionService _professionService;

        private int _professionId = 0;
        private bool _isUpdate = false;
        public FormProfession(IProfessionService professionService)
        {
            InitializeComponent();
            _professionService = professionService;
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
        private void FormProfession_MouseDown(object sender, MouseEventArgs e)
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
            _professionId = id;
            txtProfession.Text = name;
            _isUpdate = isUpdate;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var name = txtProfession.Text.Trim();

                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Please enter profession");
                    return;
                }

                if (_professionId == 0)
                {
                    var profession = new Profession(name);
                    _professionService.Create(profession);
                    MessageBox.Show("Profession created successfully!");
                }
                else
                {
                    var profession = new Profession(name);
                    profession.SetId(_professionId);

                    _professionService.Update(profession);
                    MessageBox.Show("Profession updated successfully!");
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
            GeneralHelper.ApplyRegularFont(14, txtProfession);
        }

        private void FormProfession_Load(object sender, EventArgs e)
        {
            if (_isUpdate)
            {
                labelTitle.Text = "Edit Profession";
            }
            else
            {
                labelTitle.Text = "Add Profession";
            }
        }        
    }
}
