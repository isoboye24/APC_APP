using APC.DAL.DTO;
using APC.Domain.Interfaces;
using System;
using System.Drawing;
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
            if (txtNationality.Text.Trim() == "")
            {
                MessageBox.Show("Nationality is empty");
            }
            else
            {
                if (!isUpdate)
                {
                    NationalityDetailDTO nationality = new NationalityDetailDTO();
                    nationality.Nationality = txtNationality.Text;
                    DualNationalityDetailDTO dualNationality = new DualNationalityDetailDTO();
                    dualNationality.DualNationalityName = txtNationality.Text;
                    if (bll.Insert(nationality) && dualNationalityBLL.Insert(dualNationality))
                    {
                        MessageBox.Show("Nationality was added");
                        txtNationality.Clear();
                    }
                }
                else if (isUpdate)
                {
                    if (detail.NationalityID == 0)
                    {
                        MessageBox.Show("Please select a nationality from the table");
                    }
                    else
                    {
                        detail.Nationality = txtNationality.Text;
                        dualNatDetail.DualNationalityID = detail.NationalityID;
                        dualNatDetail.DualNationalityName = txtNationality.Text;
                        if (bll.Update(detail) && dualNationalityBLL.Update(dualNatDetail))
                        {
                            MessageBox.Show("Nationality was update");
                            this.Close();
                        }
                    }
                }

            }
        }

        private void FormNationality_Load(object sender, EventArgs e)
        {
            #region
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            txtNationality.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnSave.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            #endregion

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
