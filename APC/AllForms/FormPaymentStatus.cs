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

namespace APC.AllForms
{
    public partial class FormPaymentStatus : Form
    {
        public FormPaymentStatus()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);

        private void FormPaymentStatus_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        PaymentStatusBLL bll = new PaymentStatusBLL();
        PaymentStatusDetailDTO dto = new PaymentStatusDetailDTO();
        public PaymentStatusDetailDTO detail = new PaymentStatusDetailDTO();
        public bool isUpdate = false;

        private void FormPaymentStatus_Load(object sender, EventArgs e)
        {            
            if (isUpdate)
            {
                txtPaymentStatus.Text = detail.PaymentStatusName;
                labelTitle.Text = "Edit " + detail.PaymentStatusName;
            }
            else
            {
                labelTitle.Text = "Add Payment Status";
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPaymentStatus.Text.Trim() == "")
            {
                MessageBox.Show("Please enter payment status");
            }
            else
            {
                if (!isUpdate)
                {
                    PaymentStatusDetailDTO status = new PaymentStatusDetailDTO();
                    status.PaymentStatusName = txtPaymentStatus.Text.Trim();
                    if (bll.Insert(status))
                    {
                        MessageBox.Show("Payment status added successfully");
                        txtPaymentStatus.Clear();
                    }
                }
                else if(isUpdate)
                {                    
                    detail.PaymentStatusName = txtPaymentStatus.Text.Trim();
                    if (bll.Update(detail))
                    {
                        MessageBox.Show("Payment status updated successfully");
                        this.Close();
                    }
                }
            }
        }
    }
}
