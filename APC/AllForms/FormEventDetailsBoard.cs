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

        

        private void btnAddReceipt_Click(object sender, EventArgs e)
        {

        }

        private void ClearFilters()
        {
            txtYearExpReport.Clear();
            cmbMonthExpReport.SelectedIndex = -1;
            eventExpBLL = new EventExpenditureBLL();
            eventExpDTO = eventExpBLL.Select(detail.EventID);
            dataGridEventExpenditures.DataSource = eventExpDTO.EventExpenditures;

            //Counts();
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

        private void FormEventDetailsBoard_Load(object sender, EventArgs e)
        {
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

            if (LoginInfo.AccessLevel != 4)
            {
                btnDeleteExpReport.Hide();
            }
        }
    }
}
