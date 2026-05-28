using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.BLL;
using APC.DAL.DTO;
using APC.Helper;
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
using static APC.Helper.EventsHelper;

namespace APC.AllForms
{
    public partial class FormEventDetailsBoard : BaseForm
    {
        private readonly IEventsService _eventsService;
        private readonly IEventExpenditureService _eventExpenditureService;
        private readonly IEventSalesService _eventSalesService;
        private readonly IEventReceiptService _eventReceiptService;
        private readonly IEventImagesService _eventImagesService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMonthService _monthService;

        private EventDTO _eventDTO;
        private  List<Applications.DTO.EventExpenditureDTO> _eventExpenditureDTOs;
        private  List<Applications.DTO.EventSalesDTO> _eventSalesDTOs;
        private  List<Applications.DTO.EventImageDTO> _eventImageDTOs;
        public FormEventDetailsBoard(IEventsService eventsService, IEventExpenditureService eventExpenditureService, IEventSalesService eventSalesService,
            IEventReceiptService eventReceiptService, IEventImagesService eventImagesService, ICurrentUserService currentUserService)
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.FormEventDetailsBoard_Load);

            _eventsService = eventsService;
            _eventExpenditureService = eventExpenditureService;
            _eventSalesService = eventSalesService;
            _eventReceiptService = eventReceiptService;
            _eventImagesService = eventImagesService;
            _currentUserService = currentUserService;
        }

        /// <summary>
        ///  Drag
        /// </summary>
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);

        private int buttonSize = 14;
        private int tableHeaderSize = 16;
        private float panelSize;

        EventReceiptsDetailDTO eventReceiptDetail = new EventReceiptsDetailDTO();
        EventReceiptsBLL eventReceiptsBLL = new EventReceiptsBLL();
        EventReceiptsDTO eventReceiptsDTO = new EventReceiptsDTO();

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

        public void loadForView(EventDTO eventDTO)
        {
            _eventDTO = eventDTO;
        }

        private void iconMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                buttonSize = 18;
                panelSize = 3.05f;
                AppZoomState.CurrentFontSize = buttonSize;
                AppZoomState.CurrentScale = panelSize;

                ControlResize.ResizeTaggedControls(dataGridEventImages, buttonSize, panelSize);
            }
            else
            {
                WindowState = FormWindowState.Normal;
                buttonSize = 14;
                panelSize = 1.05f;
                AppZoomState.CurrentFontSize = buttonSize;
                AppZoomState.CurrentScale = panelSize;

                ControlResize.ResizeTaggedControls(dataGridEventImages, buttonSize, panelSize);
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
                AppZoomState.CurrentFontSize = buttonSize;
                AppZoomState.CurrentScale = panelSize;

                ControlResize.ResizeTaggedControls(dataGridEventImages, buttonSize, panelSize);
            }
        }

        private void iconZoomIn_Click(object sender, EventArgs e)
        {
            buttonSize += 1;
            panelSize += 1.05f;
            AppZoomState.CurrentFontSize = buttonSize;
            AppZoomState.CurrentScale = panelSize;

            ControlResize.ResizeTaggedControls(dataGridEventImages, buttonSize, panelSize);
        }

        private void iconZoomOut_Click(object sender, EventArgs e)
        {
            buttonSize -= 1; 
            panelSize -= 1.05f;
            AppZoomState.CurrentFontSize = buttonSize;
            AppZoomState.CurrentScale = panelSize;

            ControlResize.ResizeTaggedControls(dataGridEventImages, buttonSize, panelSize);
        }

        private void ResizeableControls()
        {
            labelTitle.Tag = "resizable";
            label1.Tag = "resizable";
            labelEventSummary.Tag = "resizable";
            label4.Tag = "resizable";
            labelTotalRowsEventImage.Tag = "resizable";
            label14.Tag = "resizable";
            label2.Tag = "resizable";
            label3.Tag = "resizable";
            label5.Tag = "resizable";
            label6.Tag = "resizable";
            label7.Tag = "resizable";
            label8.Tag = "resizable";
            label9.Tag = "resizable";
            labelTotalAmountEventExp.Tag = "resizable";
            labelTotalAmountEventReceipt.Tag = "resizable";
            labelTotalEventSales.Tag = "resizable";
            labelTotalRowsEventExp.Tag = "resizable";
            labelTotalRowsEventImage.Tag = "resizable";
            labelTotalRowsEventReceipt.Tag = "resizable";
            labelTotalRowsEventSales.Tag = "resizable";
            

            txtEventImageCaption.Tag = "resizable";
            txtSummaryEventSales.Tag = "resizable";
            txtEventReceiptCaption.Tag = "resizable";
            txtSummaryExpReport.Tag = "resizable";


            cmbMonthEventReceipt.Tag = "resizable";
            cmbMonthEventSales.Tag = "resizable";
            cmbMonthExpReport.Tag = "resizable";


            btnAddEventImages.Tag = "resizable";
            btnViewEventImages.Tag = "resizable";
            btnUpdateEventImages.Tag = "resizable";
            btnDeleteEventImages.Tag = "resizable";
            btnCloseEventImages.Tag = "resizable";
            btnClearEventImage.Tag = "resizable";
            btnAddEventReceipt.Tag = "resizable";
            btnAddExpReport.Tag = "resizable";
            btnClear.Tag = "resizable";
            btnClearEventSales.Tag = "resizable";
            btnAddEventSales.Tag = "resizable";
            btnClearExpReport.Tag = "resizable";
            btnCloseEventExp.Tag = "resizable";
            btnCloseEventReceipt.Tag = "resizable";
            btnCloseEventSales.Tag = "resizable";
            btnDeleteEventReceipt.Tag = "resizable";
            btnDeleteEventSales.Tag = "resizable";
            btnDeleteExpReport.Tag = "resizable";
            btnSearch.Tag = "resizable";
            btnSearchEventSales.Tag = "resizable";
            btnSearchExpReport.Tag = "resizable";
            btnUpdateEventReceipt.Tag = "resizable";
            btnUpdateEventSales.Tag = "resizable";
            btnUpdateExpReport.Tag = "resizable";
            btnViewEventReceipt.Tag = "resizable";
            btnViewEventSales.Tag = "resizable";
            btnViewExpReport.Tag = "resizable";


            dataGridEventExpenditures.Tag = "resizable";
            dataGridEventImages.Tag = "resizable";
            dataGridEventSales.Tag = "resizable";
            dataGridViewEventReceipt.Tag = "resizable";


            tableLayoutPanel1.Tag = "resizable";
        }

        private void loadEventExpenditure()
        {
            dataGridEventExpenditures.DataSource = _eventExpenditureService.GetByEvent(_eventDTO.EventsId);
            _eventExpenditureDTOs = _eventExpenditureService.GetByEvent(_eventDTO.EventsId);
            ConfigureEventsGrid(dataGridEventExpenditures, EventsGridType.Expenditure);
        }

        private void loadEventSales()
        {
            dataGridEventSales.DataSource = _eventSalesService.GetByEvent(_eventDTO.EventsId);
            _eventSalesDTOs = _eventSalesService.GetByEvent(_eventDTO.EventsId);
            ConfigureEventsGrid(dataGridEventSales, EventsGridType.Sales);
        }
        
        private void loadEventImages()
        {
            dataGridEventImages.DataSource = _eventImagesService.GetByEvent(_eventDTO.EventsId);
            _eventImageDTOs = _eventImagesService.GetByEvent(_eventDTO.EventsId);
            ConfigureEventsGrid(dataGridEventImages, EventsGridType.Images);
        }

        private void FormEventDetailsBoard_Load(object sender, EventArgs e)
        {
            if (_currentUserService.AccessLevel != 4)
            {
                btnDeleteExpReport.Hide();
            }

            labelTitle.Text = _eventDTO.Title;
            labelEventSummary.Text = _eventDTO.Summary;

            // Event Expenditure
            #region
            cmbMonthExpReport.DataSource = _monthService.GetAll();
            GeneralHelper.ComboBoxProps(cmbMonthExpReport, "MonthName", "MonthID");

            loadEventExpenditure();

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
            cmbMonthEventSales.DataSource = _monthService.GetAll();
            GeneralHelper.ComboBoxProps(cmbMonthEventSales, "MonthName", "MonthID");

            loadEventSales();

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


            loadEventImages();

            // Event Receipts
            #region
            eventReceiptsDTO = eventReceiptsBLL.Select(detail.EventID);
            cmbMonthEventReceipt.DataSource = eventReceiptsDTO.Months;
            GeneralHelper.ComboBoxProps(cmbMonthEventReceipt, "MonthName", "MonthID");

            dataGridViewEventReceipt.DataSource = eventReceiptsDTO.EventReceipts;
            dataGridViewEventReceipt.Columns[0].Visible = false;
            dataGridViewEventReceipt.Columns[1].Visible = false;
            dataGridViewEventReceipt.Columns[2].Visible = false;
            dataGridViewEventReceipt.Columns[3].Visible = false;
            dataGridViewEventReceipt.Columns[4].HeaderText = "No.";
            dataGridViewEventReceipt.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewEventReceipt.Columns[5].HeaderText = "Picture Caption";
            dataGridViewEventReceipt.Columns[6].HeaderText = "Amt. on Receipt";
            dataGridViewEventReceipt.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewEventReceipt.Columns[7].HeaderText = "Day";
            dataGridViewEventReceipt.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewEventReceipt.Columns[8].Visible = false;
            dataGridViewEventReceipt.Columns[9].HeaderText = "Month";
            dataGridViewEventReceipt.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewEventReceipt.Columns[10].HeaderText = "Year";
            dataGridViewEventReceipt.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewEventReceipt.Columns[11].Visible = false;

            foreach (DataGridViewColumn column in dataGridViewEventReceipt.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", tableHeaderSize, FontStyle.Bold);
            }
            #endregion

            Counts();


            ResizeableControls();
        }

        // ---------------------------------------------------------------
        // ---------------- EVENT Expenditure ACTIONS ---------------
        // ---------------------------------------------------------------
        private void btnCloseEventExp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddExpReport_Click(object sender, EventArgs e)
        {
            var form = new FormEventExpenditure(_eventExpenditureService);
            form.loadEventData(_eventDTO);
            form.ShowDialog();

            ClearFilters();

        }

        private Applications.DTO.EventExpenditureDTO GetSelectedEventExpenditure()
        {
            if (dataGridEventExpenditures.CurrentRow == null)
                return null;

            return dataGridEventExpenditures.CurrentRow.DataBoundItem as Applications.DTO.EventExpenditureDTO;
        }

        private void btnViewExpReport_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedEventExpenditure();
            if (selected == null)
            {
                MessageBox.Show("Please select an expenditure from the table");
                return;
            }

            var form = new FormViewEventExpenditure(_eventExpenditureService);
            form.loadForView(selected);
            form.loadEventData(_eventDTO);
            form.ShowDialog();

            ClearFilters();
        }

        
        private void btnUpdateExpReport_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedEventExpenditure();
            if (selected == null)
            {
                MessageBox.Show("Please select an expenditure from the table");
                return;
            }

            var form = new FormEventExpenditure(_eventExpenditureService);
            form.loadForEdit(selected, true);
            form.loadEventData(_eventDTO);
            form.ShowDialog();

            ClearFilters();
        }

        private void ClearFilters()
        {
            txtSummaryExpReport.Clear();
            cmbMonthExpReport.SelectedIndex = -1;
            loadEventExpenditure();

            txtSummaryEventSales.Clear();
            cmbMonthEventSales.SelectedIndex = -1;
            loadEventSales();

            txtEventImageCaption.Clear();
            loadEventImages();

            txtEventReceiptCaption.Clear();
            cmbMonthEventReceipt.SelectedIndex = -1;
            eventReceiptsBLL = new EventReceiptsBLL();
            eventReceiptsDTO = eventReceiptsBLL.Select(detail.EventID);
            dataGridViewEventReceipt.DataSource = eventReceiptsDTO.EventReceipts;

            Counts();
        }

        private void Counts()
        {
            int pluralRowExp = dataGridEventExpenditures.RowCount;
            labelTotalAmountEventExp.Text = "Total : " + _eventExpenditureService.GetTotalAmountSpentByEvent(_eventDTO.EventsId) + " €";
            labelTotalRowsEventExp.Text = "Row" + (pluralRowExp > 1 ? "s " : " ") + pluralRowExp.ToString();

            int pluralRowSales = dataGridEventSales.RowCount;
            labelTotalEventSales.Text = "Total : " + _eventSalesService.GetSalesAmountByEvent(_eventDTO.EventsId) + " €";
            labelTotalRowsEventSales.Text = "Row" + (pluralRowSales > 1 ? "s " : " ") + pluralRowSales.ToString();

            labelTotalRowsEventImage.Text = "Total : " + dataGridEventImages.RowCount.ToString();

            int pluralRowReceipts = dataGridViewEventReceipt.RowCount;
            labelTotalRowsEventReceipt.Text = "Row" + (pluralRowReceipts > 1 ? "s " : " ") + pluralRowReceipts.ToString();
            labelTotalAmountEventReceipt.Text = "Total : " + eventReceiptsBLL.SelectTotalAmountOnEventReceipt(_eventDTO.EventsId) + " €";
        }

        private void txtSummaryExpReport_TextChanged(object sender, EventArgs e)
        {
            string search = txtSummaryExpReport.Text.Trim().ToLower();
            var filtered = _eventExpenditureDTOs.Where(x => x.Summary.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridEventExpenditures.DataSource = filtered;
        }

        private void btnSearchExpReport_Click(object sender, EventArgs e)
        {
            var filtered = _eventExpenditureDTOs.AsQueryable();

            if (cmbMonthExpReport.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a year");
                return;
            }
            else
            {
                int search = Convert.ToInt32(cmbMonthExpReport.SelectedValue);
                filtered = filtered.Where(x => x.ExpenditureDate.Month == search);            
            }

            dataGridEventExpenditures.DataSource = filtered.ToList();
        }

        private void btnClearExpReport_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void btnDeleteExpReport_Click(object sender, EventArgs e)
        {
            if (_currentUserService.AccessLevel != 4)
            {
                this.Close();
            }
            else
            {
                var selected = GetSelectedEventExpenditure();
                if (selected == null)
                {
                    MessageBox.Show("Please select an expenditure.");
                    return;
                }

                var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    _eventExpenditureService.Delete(selected.EventExpenditureId);
                    ClearFilters();
                }                
            }
        }


        // ---------------------------------------------------------------
        // ---------------- EVENT SALES ACTIONS ---------------
        // ---------------------------------------------------------------
        private void btnCloseEventSales_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        private void btnAddEventSales_Click(object sender, EventArgs e)
        {
            var form = new FormEventSales(_eventSalesService);
            form.loadEventData(_eventDTO);
            form.ShowDialog();

            ClearFilters();
        }

        private Applications.DTO.EventSalesDTO GetSelectedEventSale()
        {
            if (dataGridEventSales.CurrentRow == null)
                return null;

            return dataGridEventSales.CurrentRow.DataBoundItem as Applications.DTO.EventSalesDTO;
        }

        private void btnUpdateEventSales_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedEventSale();
            if (selected == null)
            {
                MessageBox.Show("Please select a sale from the table");
                return;
            }

            var form = new FormEventSales(_eventSalesService);
            form.loadForEdit(selected, true);
            form.loadEventData(_eventDTO);
            form.ShowDialog();

            ClearFilters();
        }

        private void txtSummaryEventSales_TextChanged(object sender, EventArgs e)
        {
            string search = txtSummaryEventSales.Text.Trim().ToLower();
            var filtered = _eventSalesDTOs.Where(x => x.Summary.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridEventSales.DataSource = filtered;
        }

        private void btnSearchEventSales_Click(object sender, EventArgs e)
        {
            var filtered = _eventSalesDTOs.AsQueryable();

            if (cmbMonthEventSales.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a year");
                return;
            }
            else
            {
                int search = Convert.ToInt32(cmbMonthEventSales.SelectedValue);
                filtered = filtered.Where(x => x.SalesDate.Month == search);
            }

            dataGridEventSales.DataSource = filtered.ToList();
        }

        private void btnClearEventSales_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void btnViewEventSales_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedEventSale();
            if (selected == null)
            {
                MessageBox.Show("Please select a sale from the table");
                return;
            }

            var form = new FormViewEventSales(_eventSalesService);
            form.loadForView(selected);
            form.loadEventData(_eventDTO);
            form.ShowDialog();

            ClearFilters();
        }

        private void btnDeleteEventSales_Click(object sender, EventArgs e)
        {
            if (_currentUserService.AccessLevel != 4)
            {
                this.Close();
            }
            else
            {
                var selected = GetSelectedEventSale();
                if (selected == null)
                {
                    MessageBox.Show("Please select a sale.");
                    return;
                }

                var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    _eventSalesService.Delete(selected.EventSalesId);
                    ClearFilters();
                }                
            }
        }

        // ---------------------------------------------------------------
        // ---------------- EVENT PICTURES ACTIONS ---------------
        // ---------------------------------------------------------------
        private void btnAddEventImages_Click(object sender, EventArgs e)
        {
            var form = new FormEventSingleImage(_eventImagesService);
            form.loadEventData(_eventDTO);
            form.ShowDialog();

            ClearFilters();
        }

        private Applications.DTO.EventImageDTO GetSelectedEventImage()
        {
            if (dataGridEventImages.CurrentRow == null)
                return null;

            return dataGridEventSales.CurrentRow.DataBoundItem as Applications.DTO.EventImageDTO;
        }

        private void btnUpdateEventImages_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedEventImage();
            if (selected == null)
            {
                MessageBox.Show("Please select an image from the table");
                return;
            }

            var form = new FormEventSingleImage(_eventImagesService);
            form.loadForEdit(selected, true);
            form.loadEventData(_eventDTO);
            form.ShowDialog();

            ClearFilters();
        }

        private void btnViewEventImages_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedEventImage();
            if (selected == null)
            {
                MessageBox.Show("Please select an image from the table");
                return;
            }

            var form = new FormViewSingleImage(_eventImagesService);
            form.loadForView(selected);
            form.loadEventData(_eventDTO);
            form.ShowDialog();

            ClearFilters();
        }

        private void btnDeleteEventImages_Click(object sender, EventArgs e)
        {
            if (_currentUserService.AccessLevel != 4)
            {
                this.Close();
            }
            else
            {
                var selected = GetSelectedEventImage();
                if (selected == null)
                {
                    MessageBox.Show("Please select a sale.");
                    return;
                }

                var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    _eventImagesService.Delete(selected.EventImageId);
                    ClearFilters();
                }
            }
        }

        private void txtImageCaption_TextChanged(object sender, EventArgs e)
        {
            string search = txtEventImageCaption.Text.Trim().ToLower();
            var filtered = _eventImageDTOs.Where(x => x.ImageCaption.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridEventImages.DataSource = filtered;
        }

        private void dataGridEventImages_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var selected = GetSelectedEventImage();

            string imagePath = System.IO.Path.Combine(Application.StartupPath, "images", selected.ImagePath);
            if (System.IO.File.Exists(imagePath))
                picImage2.ImageLocation = imagePath;
            else
                picImage2.Image = null;
        }

        private void btnClearEventImage_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        // ---------------------------------------------------------------
        // ---------------- Event RECEIPTS ACTIONS ---------------
        // ---------------------------------------------------------------

        private void btnAddEventReceipt_Click(object sender, EventArgs e)
        {
            FormEventReceipt open = new FormEventReceipt();
            open.detail = detail;
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private void btnCloseEventReceipt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdateEventReceipt_Click(object sender, EventArgs e)
        {
            if (eventReceiptDetail.EventReceiptID == 0)
            {
                MessageBox.Show("Please choose an event receipt from the table");
            }
            else
            {
                FormEventReceipt open = new FormEventReceipt();
                open.detailReceipt = eventReceiptDetail;
                open.detail = detail;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void dataGridViewEventReceipt_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewEventReceipt.Rows.Count)
                return;

            var row = dataGridViewEventReceipt.Rows[e.RowIndex];
            if (row == null || row.IsNewRow)
                return;

            eventReceiptDetail = new EventReceiptsDetailDTO();
            eventReceiptDetail.EventReceiptID = Convert.ToInt32(row.Cells[0].Value ?? 0);
            eventReceiptDetail.EventID = Convert.ToInt32(row.Cells[1].Value ?? 0);
            eventReceiptDetail.Summary = row.Cells[2].Value?.ToString() ?? string.Empty;
            eventReceiptDetail.ImagePath = row.Cells[3].Value?.ToString() ?? string.Empty;
            eventReceiptDetail.Counter = Convert.ToInt32(row.Cells[4].Value ?? 0);
            eventReceiptDetail.ImageCaption = row.Cells[5].Value?.ToString() ?? string.Empty;
            eventReceiptDetail.AmountSpent = Convert.ToDecimal(row.Cells[6].Value ?? 0);
            eventReceiptDetail.Day = Convert.ToInt32(row.Cells[7].Value ?? 0);
            eventReceiptDetail.MonthID = Convert.ToInt32(row.Cells[8].Value ?? 0);
            eventReceiptDetail.MonthName = row.Cells[9].Value?.ToString() ?? string.Empty;
            eventReceiptDetail.Year = Convert.ToInt32(row.Cells[10].Value ?? 0);
            eventReceiptDetail.ReceiptDate = Convert.ToDateTime(row.Cells[11].Value ?? 0);

            // ✅ Only set image if file path is valid
            string imagePath = System.IO.Path.Combine(Application.StartupPath, "images", eventReceiptDetail.ImagePath);
            if (System.IO.File.Exists(imagePath))
                picEventReceipt.ImageLocation = imagePath;
            else
                picEventReceipt.Image = null;

            
        }

        private void txtEventReceiptCaption_TextChanged(object sender, EventArgs e)
        {
            List<EventReceiptsDetailDTO> list = eventReceiptsDTO.EventReceipts;
            list = list.Where(x => x.ImageCaption.Contains(txtEventReceiptCaption.Text)).ToList();
            dataGridViewEventReceipt.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<EventReceiptsDetailDTO> list = eventReceiptsDTO.EventReceipts;
            if (cmbMonthEventReceipt.SelectedIndex != -1)
            {
                list = list.Where(x => x.MonthID == Convert.ToInt32(cmbMonthEventReceipt.SelectedValue)).ToList();
            }
            else
            {
                MessageBox.Show("Unknown search!");
            }
            dataGridViewEventReceipt.DataSource = list;
        }

        private void btnViewEventReceipt_Click(object sender, EventArgs e)
        {
            if (eventReceiptDetail.EventReceiptID == 0)
            {
                MessageBox.Show("Please choose an event receipt from the table");
            }
            else
            {
                FormViewEventReceipt open = new FormViewEventReceipt();
                open.detailReceipt = eventReceiptDetail;
                open.detail = detail;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void btnDeleteEventReceipt_Click(object sender, EventArgs e)
        {
            if (AuthenticationDTO.AccessLevel != 4)
            {
                this.Close();
            }
            else
            {
                if (eventReceiptDetail.EventReceiptID == 0)
                {
                    MessageBox.Show("Please choose an event receipt from the table");
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        if (eventReceiptsBLL.Delete(eventReceiptDetail))
                        {
                            MessageBox.Show("The receipt was deleted");
                            ClearFilters();
                        }
                    }
                }
            }
        }
    }
}
