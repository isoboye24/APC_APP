using APC.AllForms;
using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Applications.Services;
using APC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static APC.Helper.EventsHelper;

namespace APC
{
    public partial class FormEventsList : Form
    {
        private readonly IEventsService _eventsService;
        private readonly IEventExpenditureService _eventExpenditureService;
        private readonly IEventSalesService _eventSalesService;
        private readonly IEventReceiptService _eventReceiptService;
        private readonly IEventImagesService _eventImagesService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMonthService _monthService;

        private List<EventDTO> _eventDTOs;

        private int currYear = DateTime.Today.Year;

        public FormEventsList(IEventsService eventsService, IEventExpenditureService eventExpenditureService, IEventSalesService eventSalesService,
            IEventReceiptService eventReceiptService, IEventImagesService eventImagesService, ICurrentUserService currentUserService, 
            IMonthService monthService)
        {
            InitializeComponent();
            _eventsService = eventsService;
            _eventExpenditureService = eventExpenditureService;
            _eventSalesService = eventSalesService;
            _eventReceiptService = eventReceiptService;
            _eventImagesService = eventImagesService;
            _currentUserService = currentUserService;
            _monthService = monthService;
        }        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new FormEvent(_eventsService);
            form.ShowDialog();

            ClearFilters();
        }

        private EventDTO GetSelectedEvent()
        {
            if (dataGridView1.CurrentRow == null)
                return null;

            return dataGridView1.CurrentRow.DataBoundItem as EventDTO;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedEvent();
            if (selected == null)
            {
                MessageBox.Show("Please select an event from the table");
                return;
            }

            var form = new FormEvent(_eventsService);
            form.loadForEdit(selected, true);
            form.ShowDialog();

            ClearFilters();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedEvent();
            if (selected == null)
            {
                MessageBox.Show("Please select an event from the table");
                return;
            }

            var form = new FormEventDetailsBoard(_eventsService, _eventExpenditureService, _eventSalesService,
                    _eventReceiptService, _eventImagesService, _currentUserService, _monthService);
            form.loadForView(selected);
            form.ShowDialog();

            ClearFilters();      
        }

        private void controlsFont()
        {
            GeneralHelper.ApplyBoldFont(14, label5, label2, btnAdd, btnUpdate, btnView);

            GeneralHelper.ApplyRegularFont(14, cmbEventYear, txtEventTitle);

            label1.Tag = "resizable";
            label3.Tag = "resizable";
            label4.Tag = "resizable";
            label5.Tag = "resizable";
            label6.Tag = "resizable";
            labelOverallBalance.Tag = "resizable";
            labelOverallSold.Tag = "resizable";
            labelOverallSpent.Tag = "resizable";
        }

        private void loadEvents()
        {
            dataGridView1.DataSource = _eventsService.GetAnnualEvents(currYear);
            _eventDTOs = _eventsService.GetAll();
            ConfigureEventsGrid(dataGridView1, EventsGridType.Basic);
        }

        private void FormEventsList_Load(object sender, EventArgs e)
        {
            cmbEventYear.DataSource = _eventsService.GetEventYearsOnly();
            GeneralHelper.ComboBoxProps(cmbEventYear, "YearInText", "YearInValue");

            controlsFont();

            loadEvents();
        }
        private void ClearFilters()
        {
            cmbEventYear.SelectedIndex = -1;
            txtEventTitle.Clear();

            loadEvents();
        }

        private void txtEventTitle_TextChanged(object sender, EventArgs e)
        {
            string search = txtEventTitle.Text.Trim().ToLower();
            var filtered = _eventDTOs.Where(x => x.Title.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridView1.DataSource = filtered;
        }

        private void txtEventYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = GeneralHelper.isNumber(e, (TextBox)sender);
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var selected = GetSelectedEvent();
            if (selected == null)
            {
                return;
            }

            string imagePath = Path.Combine(Application.StartupPath, "images", selected.CoverImagePath);
            picViewEventCoverImage.ImageLocation = imagePath;

            label3.Text = selected.Title + " " + selected.EventsDate.Year;

            decimal totalSales = _eventSalesService.GetSalesAmountByEvent(selected.EventsId);
            decimal totalExpenditures = _eventExpenditureService.GetTotalAmountSpentByEvent(selected.EventsId);

            labelOverallSold.Text = totalSales.ToString();
            labelOverallSpent.Text = totalExpenditures.ToString();
            labelOverallBalance.Text = (totalSales - totalExpenditures).ToString();
        }

        private void btnSearchEvent_Click(object sender, EventArgs e)
        {
            if (cmbEventYear.SelectedIndex != -1)
            {
                int year = Convert.ToInt32(cmbEventYear.SelectedValue);
                var filtered = _eventDTOs.Where(x => x.EventsDate.Year == year).ToList();
                dataGridView1.DataSource = filtered;
            }
        }

        private void btnClearEvent_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }
    }
}
