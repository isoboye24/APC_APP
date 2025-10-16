using APC.BLL;
using APC.DAL.DTO;
using APC.Utility;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace APC.AllForms
{
    public partial class FormEventDetailsBoard : Form
    {
        public FormEventDetailsBoard()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.FormEventDetailsBoard_Load);
        }

        /// <summary>
        ///  Drag
        /// </summary>
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);

        public EventsDetailDTO detail = new EventsDetailDTO();
        public bool isView = false;
        private int buttonSize = 14;
        private float panelSize;

        EventExpenditureDetailDTO eventExpDetail = new EventExpenditureDetailDTO();
        EventExpenditureDTO eventExpDTO = new EventExpenditureDTO();
        EventExpenditureBLL eventExpBLL = new EventExpenditureBLL();

        EventSalesDetailDTO eventSalesDetail = new EventSalesDetailDTO();
        EventSalesBLL eventSalesBLL = new EventSalesBLL();
        EventSalesDTO eventSalesDTO = new EventSalesDTO();

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void FormEventDetails_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void picClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
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
                ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
            }
            else
            {
                WindowState = FormWindowState.Normal;
                buttonSize = 14;
                panelSize = 1.05f;
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

        private void FormEventDetailsBoard_Load(object sender, EventArgs e)
        {
            if (LoginInfo.AccessLevel != 4)
            {
                btnDeleteExpReport.Hide();
            }

            // Resizeable
            #region
            labelTitle.Tag = "resizable";
            label1.Tag = "resizable";
            labelEventSummary.Tag = "resizable";
            label4.Tag = "resizable";

            txtImageCaption.Tag = "resizable";

            btnAdd.Tag = "resizable";
            btnView.Tag = "resizable";
            btnUpdate.Tag = "resizable";
            btnDelete.Tag = "resizable";
            btnClose.Tag = "resizable";

            #endregion

            labelTitle.Text = detail.EventTitle;
            labelEventSummary.Text = detail.Summary;

            // Event Expenditure
            #region
            eventExpDTO = eventExpBLL.Select(detail.EventID);
            cmbMonthExpReport.DataSource = eventExpDTO.Months;
            General.ComboBoxProps(cmbMonthExpReport, "MonthName", "MonthID");

            dataGridEventExpenditures.DataSource = eventExpDTO.EventExpenditures;
            dataGridEventExpenditures.Columns[0].Visible = false;
            dataGridEventExpenditures.Columns[1].Visible = false;
            dataGridEventExpenditures.Columns[2].HeaderText = "Summary";
            dataGridEventExpenditures.Columns[3].HeaderText = "Amount Spent (€)";
            dataGridEventExpenditures.Columns[4].Visible = false;
            dataGridEventExpenditures.Columns[5].HeaderText = "Day";
            dataGridEventExpenditures.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridEventExpenditures.Columns[6].Visible = false;
            dataGridEventExpenditures.Columns[7].HeaderText = "Month";
            dataGridEventExpenditures.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridEventExpenditures.Columns[8].HeaderText = "Year";
            dataGridEventExpenditures.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            foreach (DataGridViewColumn column in dataGridEventExpenditures.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            }

            // limit the amount of summary characters
            if (dataGridEventExpenditures.Columns.Count > 2)
            {
                foreach (DataGridViewRow row in dataGridEventExpenditures.Rows)
                {
                    if (row.Cells[2].Value != null)
                    {
                        string text = row.Cells[2].Value.ToString();
                        if (text.Length > 60)
                        {
                            row.Cells[2].Value = text.Substring(0, 60) + "...";
                        }
                    }
                }
            }

            #endregion

            // Event Sales
            #region
            eventSalesDTO = eventSalesBLL.Select(detail.EventID);
            cmbMonthEventSales.DataSource = eventSalesDTO.Months;
            General.ComboBoxProps(cmbMonthEventSales, "MonthName", "MonthID");

            dataGridEventSales.DataSource = eventSalesDTO.EventSales;
            dataGridEventSales.Columns[0].Visible = false;
            dataGridEventSales.Columns[1].Visible = false;
            dataGridEventSales.Columns[2].HeaderText = "Summary";
            dataGridEventSales.Columns[3].HeaderText = "Amount Spent (€)";
            dataGridEventSales.Columns[4].Visible = false;
            dataGridEventSales.Columns[5].HeaderText = "Day";
            dataGridEventSales.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridEventSales.Columns[6].Visible = false;
            dataGridEventSales.Columns[7].HeaderText = "Month";
            dataGridEventSales.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridEventSales.Columns[8].HeaderText = "Year";
            dataGridEventSales.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            foreach (DataGridViewColumn column in dataGridEventSales.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            }

            if (dataGridEventSales.Columns.Count > 2)
            {
                foreach (DataGridViewRow row in dataGridEventSales.Rows)
                {
                    if (row.Cells[2].Value != null)
                    {
                        string text = row.Cells[2].Value.ToString();
                        if (text.Length > 60)
                        {
                            row.Cells[2].Value = text.Substring(0, 60) + "...";
                        }
                    }
                }
            }

            #endregion

            

            Counts();
        }

        // ---------------------------------------------------------------
        // ---------------- Event Expenditure ACTIONS ---------------
        // ---------------------------------------------------------------
        private void btnCloseEventExp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnViewExpReport_Click(object sender, EventArgs e)
        {
            if (eventExpDetail.EventExpenditureID == 0)
            {
                MessageBox.Show("Please choose an event expenditure from the table");
            }
            else
            {
                FormViewEventExpenditure open = new FormViewEventExpenditure();
                open.detail = detail;
                open.eventExpDetail = eventExpDetail;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void btnAddExpReport_Click(object sender, EventArgs e)
        {
            FormEventExpenditure open = new FormEventExpenditure();
            open.eventID = detail.EventID;
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }
        private void btnUpdateExpReport_Click(object sender, EventArgs e)
        {
            if (eventExpDetail.EventExpenditureID == 0)
            {
                MessageBox.Show("Please choose an event expenditure from the table");
            }
            else
            {
                FormEventExpenditure open = new FormEventExpenditure();
                open.detail = eventExpDetail;
                open.eventID = detail.EventID;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void dataGridEventExpenditures_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            eventExpDetail = new EventExpenditureDetailDTO();
            eventExpDetail.EventExpenditureID = Convert.ToInt32(dataGridEventExpenditures.Rows[e.RowIndex].Cells[0].Value);
            eventExpDetail.EventID = Convert.ToInt32(dataGridEventExpenditures.Rows[e.RowIndex].Cells[1].Value);
            eventExpDetail.Summary = dataGridEventExpenditures.Rows[e.RowIndex].Cells[2].Value.ToString();
            eventExpDetail.AmountSpent = Convert.ToDecimal(dataGridEventExpenditures.Rows[e.RowIndex].Cells[3].Value);
            eventExpDetail.ExpenditureDate = Convert.ToDateTime(dataGridEventExpenditures.Rows[e.RowIndex].Cells[4].Value);
            eventExpDetail.Day = Convert.ToInt32(dataGridEventExpenditures.Rows[e.RowIndex].Cells[5].Value);
            eventExpDetail.MonthID = Convert.ToInt32(dataGridEventExpenditures.Rows[e.RowIndex].Cells[6].Value);
            eventExpDetail.MonthName = dataGridEventExpenditures.Rows[e.RowIndex].Cells[7].Value.ToString();
            eventExpDetail.Year = Convert.ToInt32(dataGridEventExpenditures.Rows[e.RowIndex].Cells[8].Value);
        }

        private void ClearFilters()
        {
            txtSummaryExpReport.Clear();
            cmbMonthExpReport.SelectedIndex = -1;
            eventExpBLL = new EventExpenditureBLL();
            eventExpDTO = eventExpBLL.Select(detail.EventID);
            dataGridEventExpenditures.DataSource = eventExpDTO.EventExpenditures;

            txtSummaryEventSales.Clear();
            cmbMonthEventSales.SelectedIndex = -1;
            eventSalesBLL = new EventSalesBLL();
            eventSalesDTO = eventSalesBLL.Select(detail.EventID);
            dataGridEventSales.DataSource = eventSalesDTO.EventSales;

            Counts();
        }

        private void Counts()
        {
            int pluralRowExp = dataGridEventExpenditures.RowCount;
            labelTotalAmountEventExp.Text = "Total : " + eventExpBLL.SelectTotalAmountEventExp(detail.EventID) + " €";
            labelTotalRowsEventExp.Text = "Row" + (pluralRowExp > 1 ? "s " : " ") + pluralRowExp.ToString();

            int pluralRowSales = dataGridEventSales.RowCount;
            labelTotalEventSales.Text = "Total : " + eventSalesBLL.SelectTotalAmountEventSold(detail.EventID) + " €";
            labelTotalRowsEventSales.Text = "Row" + (pluralRowSales > 1 ? "s " : " ") + pluralRowSales.ToString();
        }

        private void txtSummaryExpReport_TextChanged(object sender, EventArgs e)
        {
            List<EventExpenditureDetailDTO> list = eventExpDTO.EventExpenditures;
            list = list.Where(x => x.Summary.Contains(txtSummaryExpReport.Text)).ToList();
            dataGridEventExpenditures.DataSource = list;
        }

        private void btnSearchExpReport_Click(object sender, EventArgs e)
        {
            List<EventExpenditureDetailDTO> list = eventExpDTO.EventExpenditures;
            if (cmbMonthExpReport.SelectedIndex != -1)
            {
                list = list.Where(x => x.MonthID == Convert.ToInt32(cmbMonthExpReport.SelectedValue)).ToList();
            }            
            else
            {
                MessageBox.Show("Unknown search!");
            }
            dataGridEventExpenditures.DataSource = list;
        }

        private void btnClearExpReport_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void btnDeleteExpReport_Click(object sender, EventArgs e)
        {
            if (eventExpDetail.EventExpenditureID == 0)
            {
                MessageBox.Show("Please choose an event expenditure from the table");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (eventExpBLL.Delete(eventExpDetail))
                    {
                        MessageBox.Show("Event Expenditure was deleted");
                        ClearFilters();
                    }
                }
            }
        }


        // ---------------------------------------------------------------
        // ---------------- Event SALES ACTIONS ---------------
        // ---------------------------------------------------------------
        private void btnCloseEventSales_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        private void btnAddEventSales_Click(object sender, EventArgs e)
        {
            FormEventSales open = new FormEventSales();
            open.eventID = detail.EventID;
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private void btnUpdateEventSales_Click(object sender, EventArgs e)
        {
            if (eventSalesDetail.EventSalesID == 0)
            {
                MessageBox.Show("Please choose an event sales from the table");
            }
            else
            {
                FormEventSales open = new FormEventSales();
                open.detail = eventSalesDetail;
                open.eventID = detail.EventID;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void dataGridEventSales_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            eventSalesDetail = new EventSalesDetailDTO();
            eventSalesDetail.EventSalesID = Convert.ToInt32(dataGridEventSales.Rows[e.RowIndex].Cells[0].Value);
            eventSalesDetail.EventID = Convert.ToInt32(dataGridEventSales.Rows[e.RowIndex].Cells[1].Value);
            eventSalesDetail.Summary = dataGridEventSales.Rows[e.RowIndex].Cells[2].Value.ToString();
            eventSalesDetail.AmountSold = Convert.ToDecimal(dataGridEventSales.Rows[e.RowIndex].Cells[3].Value);
            eventSalesDetail.SalesDate = Convert.ToDateTime(dataGridEventSales.Rows[e.RowIndex].Cells[4].Value);
            eventSalesDetail.Day = Convert.ToInt32(dataGridEventSales.Rows[e.RowIndex].Cells[5].Value);
            eventSalesDetail.MonthID = Convert.ToInt32(dataGridEventSales.Rows[e.RowIndex].Cells[6].Value);
            eventSalesDetail.MonthName = dataGridEventSales.Rows[e.RowIndex].Cells[7].Value.ToString();
            eventSalesDetail.Year = Convert.ToInt32(dataGridEventSales.Rows[e.RowIndex].Cells[8].Value);
        }

        private void txtSummaryEventSales_TextChanged(object sender, EventArgs e)
        {
            List<EventSalesDetailDTO> list = eventSalesDTO.EventSales;
            list = list.Where(x => x.Summary.Contains(txtSummaryEventSales.Text)).ToList();
            dataGridEventSales.DataSource = list;
        }
        private void btnSearchEventSales_Click(object sender, EventArgs e)
        {
            List<EventSalesDetailDTO> list = eventSalesDTO.EventSales;
            if (cmbMonthEventSales.SelectedIndex != -1)
            {
                list = list.Where(x => x.MonthID == Convert.ToInt32(cmbMonthEventSales.SelectedValue)).ToList();
            }
            else
            {
                MessageBox.Show("Unknown search!");
            }
            dataGridEventSales.DataSource = list;
        }

        private void btnClearEventSales_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }


        private void btnViewEventSales_Click(object sender, EventArgs e)
        {
            if (eventSalesDetail.EventSalesID == 0)
            {
                MessageBox.Show("Please choose an event sales from the table");
            }
            else
            {
                FormViewEventSales open = new FormViewEventSales();
                open.detail = detail;
                open.eventSalesDetail = eventSalesDetail;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void btnDeleteEventSales_Click(object sender, EventArgs e)
        {
            if (eventSalesDetail.EventSalesID == 0)
            {
                MessageBox.Show("Please choose an event sales from the table");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (eventSalesBLL.Delete(eventSalesDetail))
                    {
                        MessageBox.Show("Event sales was deleted");
                        ClearFilters();
                    }
                }
            }
        }

        // ---------------------------------------------------------------
        // ---------------- Event RECEIPTS ACTIONS ---------------
        // ---------------------------------------------------------------

        private void btnAddReceipt_Click(object sender, EventArgs e)
        {

        }

    }
}
