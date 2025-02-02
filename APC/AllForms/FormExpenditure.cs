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
    public partial class FormExpenditure : Form
    {
        public FormExpenditure()
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ExpenditureBLL bll = new ExpenditureBLL();
        public ExpenditureDetailDTO detail = new ExpenditureDetailDTO();
        public bool isUpdate = false;
        private void btnSave_Click(object sender, EventArgs e)
        {            
            if (txtAmountSpent.Text.Trim()=="")
            {
                MessageBox.Show("Add amount");
            }
            else if(txtSummary.Text.Trim() == "")
            {
                MessageBox.Show("Add summary");
            }
            else
            {
                if (!isUpdate)
                {
                    ExpenditureDetailDTO expenditure = new ExpenditureDetailDTO();
                    expenditure.AmountSpent = Convert.ToDecimal(txtAmountSpent.Text);
                    expenditure.Summary = txtSummary.Text;
                    expenditure.Day = dateTimePickerExpDate.Value.Day;
                    expenditure.MonthID = dateTimePickerExpDate.Value.Month;
                    expenditure.Year = dateTimePickerExpDate.Value.Year.ToString();
                    expenditure.ExpenditureDate = dateTimePickerExpDate.Value;
                    if (bll.Insert(expenditure))
                    {
                        MessageBox.Show("Expenditure was added");
                        txtAmountSpent.Clear();
                        txtSummary.Clear();
                        dateTimePickerExpDate.Value = DateTime.Today;
                    }
                }
                else if(isUpdate)
                {
                    if (detail.AmountSpent == Convert.ToDecimal(txtAmountSpent.Text) && detail.Summary == txtSummary.Text
                        && detail.ExpenditureDate == dateTimePickerExpDate.Value)
                    {
                        MessageBox.Show("There is no change");
                    }
                    else
                    {
                        detail.AmountSpent = Convert.ToDecimal(txtAmountSpent.Text);
                        detail.Summary = txtSummary.Text;
                        detail.Day = dateTimePickerExpDate.Value.Day;
                        detail.MonthID = dateTimePickerExpDate.Value.Month;
                        detail.Year = dateTimePickerExpDate.Value.Year.ToString();
                        detail.ExpenditureDate = dateTimePickerExpDate.Value;
                        if (bll.Update(detail))
                        {
                            MessageBox.Show("Expenditure was updated");
                            this.Close();
                        }
                    }
                }
            }
        }

        private void txtAmountSpent_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void FormExpenditure_Load(object sender, EventArgs e)
        {
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label3.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            txtSummary.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtAmountSpent.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            dateTimePickerExpDate.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSave.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            if (isUpdate)
            {
                labelTitle.Text = "Edit Expenditure";
                txtAmountSpent.Text = detail.AmountSpent.ToString();
                txtSummary.Text = detail.Summary;
                dateTimePickerExpDate.Value = detail.ExpenditureDate;
            }
            else
            {
                labelTitle.Text = "Add Expenditure";
            }
        }        
    }
}
