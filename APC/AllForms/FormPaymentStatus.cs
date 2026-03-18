using APC.Applications.Services;
using APC.BLL;
using APC.DAL.DTO;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
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
        private readonly IPaymentStatusService _paymentStatusService;

        private int _paymentStatusId = 0;
        private bool _isUpdate = false;

        public FormPaymentStatus(IPaymentStatusService paymentStatusService)
        {
            InitializeComponent();
            _paymentStatusService = paymentStatusService;
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

        public void LoadForEdit(int id, string name, bool isUpdate)
        {
            _paymentStatusId = id;
            txtPaymentStatus.Text = name;
            _isUpdate = isUpdate;
        }

        private void FormPaymentStatus_Load(object sender, EventArgs e)
        {            
            if (_isUpdate)
            {
                labelTitle.Text = "Edit Payment Status";
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
            try
            {
                var name = txtPaymentStatus.Text.Trim();

                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Please enter payment status");
                    return;
                }

                if (_paymentStatusId == 0)
                {
                    var paymentStatus = new PaymentStatus(name);
                    _paymentStatusService.Create(paymentStatus);
                    MessageBox.Show("Payment status created successfully!");
                }
                else
                {
                    var paymentStatus = new PaymentStatus(name);
                    paymentStatus.SetId(_paymentStatusId);

                    _paymentStatusService.Update(paymentStatus);
                    MessageBox.Show("Payment status updated successfully!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
