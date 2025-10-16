using APC.BLL;
using APC.DAL.DTO;
using APC.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace APC.AllForms
{
    public partial class FormEventExpenditure : Form
    {
        public FormEventExpenditure()
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

        private int buttonSize = 14;
        private float panelSize;
        public bool isUpdate = false;
        public int eventID;

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public EventExpenditureDetailDTO detail = new EventExpenditureDetailDTO();
        private void FormEventExpenditure_Load(object sender, EventArgs e)
        {
            // Resizeable controls
            #region

            label1.Tag = "resizable";
            label2.Tag = "resizable";
            label3.Tag = "resizable";

            txtAmountSpent.Tag = "resizable";
            txtSummary.Tag = "resizable";

            dateTimePickerEventExpDate.Tag = "resizable";

            btnClose.Tag = "resizable";
            btnSave.Tag = "resizable";
            #endregion

            if (isUpdate)
            {
                labelTitle.Text = "Edit Expenditure";

                txtAmountSpent.Text = detail.AmountSpent.ToString();
                txtSummary.Text = detail.Summary;
                dateTimePickerEventExpDate.Value = detail.ExpenditureDate;
            }
            else
            {
                labelTitle.Text = "Add Expenditure";
            }

        }

        private void iconMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                buttonSize = 18;
                panelSize = 3.05f;
                panel1.Size = new Size(1001, 60);
                iconZoomIn.IconSize = 40;
                iconZoomOut.IconSize = 40;

                ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
            }
            else
            {
                WindowState = FormWindowState.Normal;
                buttonSize = 14;
                panelSize = 1.05f;
                panel1.Size = new Size(1001, 51);
                iconZoomIn.IconSize = 32;
                iconZoomOut.IconSize = 32;

                ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void picMinimize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Minimized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                buttonSize = 14;
                panelSize = 1.05f;
                panel1.Size = new Size(1001, 51);
                iconZoomIn.IconSize = 32;
                iconZoomOut.IconSize = 32;

                ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
            }
        }

        private void iconZoomIn_Click(object sender, EventArgs e)
        {
            buttonSize += 1;
            panelSize += 1.05f;

            ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
        }

        private void iconZoomOut_Click(object sender, EventArgs e)
        {
            buttonSize -= 1;
            panelSize -= 1.05f;
            ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
        }

        EventExpenditureBLL bll = new EventExpenditureBLL();        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAmountSpent.Text.Trim() == "")
            {
                MessageBox.Show("Add amount");
            }
            else if (txtSummary.Text.Trim() == "")
            {
                MessageBox.Show("Add summary");
            }
            else
            {
                if (!isUpdate)
                {
                    EventExpenditureDetailDTO expenditure = new EventExpenditureDetailDTO();
                    expenditure.AmountSpent = Convert.ToDecimal(txtAmountSpent.Text, CultureInfo.InvariantCulture);
                    expenditure.Summary = txtSummary.Text;
                    expenditure.EventID = this.eventID;
                    expenditure.Day = dateTimePickerEventExpDate.Value.Day;
                    expenditure.MonthID = dateTimePickerEventExpDate.Value.Month;
                    expenditure.Year = dateTimePickerEventExpDate.Value.Year;
                    expenditure.ExpenditureDate = dateTimePickerEventExpDate.Value;
                    if (bll.Insert(expenditure))
                    {
                        MessageBox.Show("Event Expenditure was added");
                        txtAmountSpent.Clear();
                        txtSummary.Clear();
                        dateTimePickerEventExpDate.Value = DateTime.Today;
                    }
                }
                else if (isUpdate)
                {
                    if (Convert.ToDecimal(txtAmountSpent.Text, CultureInfo.InvariantCulture) == detail.AmountSpent && txtSummary.Text == detail.Summary
                        && dateTimePickerEventExpDate.Value == detail.ExpenditureDate)
                    {
                        MessageBox.Show("There is no change");
                    }
                    else
                    {
                        detail.AmountSpent = Convert.ToDecimal(txtAmountSpent.Text, CultureInfo.InvariantCulture);
                        detail.EventID = this.eventID;
                        detail.Summary = txtSummary.Text;
                        detail.Day = dateTimePickerEventExpDate.Value.Day;
                        detail.MonthID = dateTimePickerEventExpDate.Value.Month;
                        detail.Year = dateTimePickerEventExpDate.Value.Year;
                        detail.ExpenditureDate = dateTimePickerEventExpDate.Value;
                        if (bll.Update(detail))
                        {
                            MessageBox.Show("Event Expenditure was updated");
                            this.Close();
                        }
                    }
                }
            }
        }
    }
}
