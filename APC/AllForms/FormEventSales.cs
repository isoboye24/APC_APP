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

namespace APC.AllForms
{
    public partial class FormEventSales : Form
    {
        public FormEventSales()
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

        public EventSalesDetailDTO detail = new EventSalesDetailDTO();
        EventSalesBLL bll = new EventSalesBLL();

        private int buttonSize = 14;
        private float panelSize;
        public bool isUpdate = false;
        private bool noChanges;
        public int eventID;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
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

        private void iconZoomOut_Click(object sender, EventArgs e)
        {
            buttonSize -= 1;
            panelSize -= 1.05f;
            ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
        }

        private void iconZoomIn_Click(object sender, EventArgs e)
        {
            buttonSize += 1;
            panelSize += 1.05f;

            ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
        }

        private void FormEventSales_Load(object sender, EventArgs e)
        {
            if (isUpdate)
            {
                labelTitle.Text = "Edit Sales";

                txtAmountSold.Text = detail.AmountSold.ToString();
                txtSummary.Text = detail.Summary;
                dateTimePickerEventSalesDate.Value = detail.SalesDate;
            }
            else
            {
                labelTitle.Text = "Add Sales";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAmountSold.Text.Trim() == "")
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
                    EventSalesDetailDTO sales = new EventSalesDetailDTO();
                    sales.AmountSold = Convert.ToDecimal(txtAmountSold.Text, CultureInfo.InvariantCulture);
                    sales.Summary = txtSummary.Text;
                    sales.EventID = this.eventID;
                    sales.Day = dateTimePickerEventSalesDate.Value.Day;
                    sales.MonthID = dateTimePickerEventSalesDate.Value.Month;
                    sales.Year = dateTimePickerEventSalesDate.Value.Year;
                    sales.SalesDate = dateTimePickerEventSalesDate.Value;
                    if (bll.Insert(sales))
                    {
                        MessageBox.Show("Event Sales was added");
                        txtAmountSold.Clear();
                        txtSummary.Clear();
                        dateTimePickerEventSalesDate.Value = DateTime.Today;
                    }
                }
                else if (isUpdate)
                {
                    noChanges = Convert.ToDecimal(txtAmountSold.Text, CultureInfo.InvariantCulture) == detail.AmountSold && txtSummary.Text == detail.Summary
                        && dateTimePickerEventSalesDate.Value == detail.SalesDate;
                    if (noChanges)
                    {
                        MessageBox.Show("There is no change");
                    }
                    else
                    {
                        detail.AmountSold = Convert.ToDecimal(txtAmountSold.Text, CultureInfo.InvariantCulture);
                        detail.EventID = this.eventID;
                        detail.Summary = txtSummary.Text;
                        detail.Day = dateTimePickerEventSalesDate.Value.Day;
                        detail.MonthID = dateTimePickerEventSalesDate.Value.Month;
                        detail.Year = dateTimePickerEventSalesDate.Value.Year;
                        detail.SalesDate = dateTimePickerEventSalesDate.Value;
                        if (bll.Update(detail))
                        {
                            MessageBox.Show("Event Sales was updated");
                            this.Close();
                        }
                    }
                }
            }
        }
    }
}
