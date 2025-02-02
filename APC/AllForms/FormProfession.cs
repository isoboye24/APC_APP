using APC.BLL;
using APC.DAL.DTO;
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
        public FormProfession()
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
        private void FormProfession_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ProfessionBLL bll = new ProfessionBLL();
        public ProfessionDetailDTO detail = new ProfessionDetailDTO();
        public bool isUpdate = false;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProfession.Text.Trim()=="")
            {
                MessageBox.Show("Profession is empty");
            }
            else
            {
                if (!isUpdate)
                {
                    ProfessionDetailDTO profession = new ProfessionDetailDTO();
                    profession.Profession = txtProfession.Text;
                    if (bll.Insert(profession))
                    {
                        MessageBox.Show("Profession was added");
                        txtProfession.Clear();
                    }
                }
                else if (isUpdate)
                {
                    if (detail.Profession==txtProfession.Text.Trim())
                    {
                        MessageBox.Show("There is no change");
                    }
                    else
                    {
                        detail.Profession = txtProfession.Text;
                        if (bll.Update(detail))
                        {
                            MessageBox.Show("Profession was updated");
                            this.Close();
                        }
                    }
                }
            }
        }

        private void FormProfession_Load(object sender, EventArgs e)
        {
            #region
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            txtProfession.Font = new Font("Segoe UI", 14, FontStyle.Regular);            
            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSave.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            #endregion
            if (isUpdate)
            {
                txtProfession.Text = detail.Profession;
                labelTitle.Text = "Edit Profession";
            }
            else
            {
                labelTitle.Text = "Add Profession";
            }
        }        
    }
}
