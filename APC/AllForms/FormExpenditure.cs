using APC.Applications.Interfaces;
using APC.Domain.Entities;
using APC.Helper;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormExpenditure : Form
    {
        private readonly IExpenditureService _expenditureService;

        private Applications.DTO.ExpenditureDTO _expenditureDTO;
        private bool _isUpdate = false;

        public FormExpenditure(IExpenditureService expenditureService)
        {
            InitializeComponent();
            _expenditureService = expenditureService;
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
        

        public void loadForEdit(Applications.DTO.ExpenditureDTO expenditureDTO, bool isUpdate)
        {
            _expenditureDTO = expenditureDTO;
            _isUpdate = isUpdate;
        }

        private void controlsFont()
        {
            GeneralHelper.ApplyBoldFont(14, labelTitle, label1, label2, label3, btnClose, btnSave);

            GeneralHelper.ApplyRegularFont(16, txtSummary, txtAmountSpent, dateTimePickerExpDate);
        }

        private void FormExpenditure_Load(object sender, EventArgs e)
        {
            controlsFont();

            if (_isUpdate)
            {
                labelTitle.Text = "Edit Expenditure";
                txtAmountSpent.Text = _expenditureDTO.AmountSpent.ToString();
                txtSummary.Text = _expenditureDTO.Summary;
                dateTimePickerExpDate.Value = _expenditureDTO.ExpenditureDate;
            }
            else
            {
                labelTitle.Text = "Add Expenditure";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string summary = txtSummary.Text.Trim();
                decimal amount = Convert.ToDecimal(txtAmountSpent.Text.Trim());
                DateTime date = dateTimePickerExpDate.Value;

                if (_expenditureDTO.ExpenditureId == 0)
                {
                    var expenditure = new Expenditure(amount, summary, date);
                    _expenditureService.Create(expenditure);
                    MessageBox.Show("Expenditure created successfully!");
                }
                else
                {
                    var expenditure = new Expenditure(amount, summary, date);

                    _expenditureService.Update(expenditure);
                    MessageBox.Show("Expenditure updated successfully!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtAmountSpent_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = GeneralHelper.isNumber(e, (TextBox)sender);
        }
        

    }
}
