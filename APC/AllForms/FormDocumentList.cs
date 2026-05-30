using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static APC.Helper.DocumentHelper;

namespace APC.AllForms
{
    public partial class FormDocumentList : Form
    {
        private readonly IDocumentService _documentService;
        private readonly IMonthService _monthService;
        private readonly ICurrentUserService _currentUserService;

        private List<DocumentDTO> _documentDTOs;

        private DateTime currDate = DateTime.Today;

        public FormDocumentList(IDocumentService documentService, IMonthService monthService, ICurrentUserService currentUserService)
        {
            InitializeComponent();
            _documentService = documentService;
            _monthService = monthService;
            _currentUserService = currentUserService;
        }

        private void ControlsFont()
        {
            GeneralHelper.ApplyBoldFont(14, label1, label2, label3, label4, btnAddDocument, btnDeleteDocument, btnUpdateDocument, btnViewDocument,
                btnSearchDocument, btnClearDocument);
            GeneralHelper.ApplyRegularFont(14, txtDocNameDocument, txtDocTypeDocument, cmbYearDocument, cmbMonthDocument);
        }

        private void loadDocuments()
        {
            dataGridView1.DataSource = _documentService.GetDocumentByYear(currDate.Year);
            _documentDTOs = _documentService.GetAll();
            ConfigureDocumentGrid(dataGridView1, DocumentGridType.Basic);
        }

        private void FormDocumentList_Load(object sender, EventArgs e)
        {
            ControlsFont();

            cmbMonthDocument.DataSource = _monthService.GetAll();
            GeneralHelper.ComboBoxProps(cmbMonthDocument, "MonthName", "MonthID");
            
            cmbYearDocument.DataSource = _documentService.GetOnlyYears();
            GeneralHelper.ComboBoxProps(cmbYearDocument, "YearInText", "YearInValue");

            loadDocuments();

            Count();

            if (_currentUserService.AccessLevel != 4)
            {
                btnDeleteDocument.Hide();
            }

        }
        private void Count()
        {
            labelDocCount.Text = dataGridView1.RowCount.ToString();
        }
        
        private void ClearFilters()
        {
            txtDocNameDocument.Clear();
            txtDocTypeDocument.Clear();
            cmbMonthDocument.SelectedIndex = -1;
            cmbYearDocument.SelectedIndex = -1;
            loadDocuments();
            Count();
        }

        private void btnAddDocument_Click(object sender, EventArgs e)
        {
            var form = new FormDocument(_documentService);
            form.ShowDialog();

            ClearFilters();
        }

        private DocumentDTO GetSelectedDocument()
        {
            if (dataGridView1.CurrentRow == null)
                return null;

            return dataGridView1.CurrentRow.DataBoundItem as DocumentDTO;
        }

        private void btnUpdateDocument_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedDocument();
            if (selected == null)
            {
                MessageBox.Show("Please select a document from the table");
                return;
            }

            var form = new FormDocument(_documentService);
            form.loadForEdit(selected, true);
            form.ShowDialog();

            ClearFilters();
        }

        private void btnViewDocument_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedDocument();
            if (selected == null)
            {
                MessageBox.Show("Please select a document from the table");
                return;
            }
            else if (selected.DocumentType == "Word Document")
            {
                DialogResult result = MessageBox.Show("Open document \"" + selected.DocumentName + " ?\"", "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    var form = new FormViewDocument();
                    form.loadForView(selected);
                    form.ShowDialog();
                }
            }
            else
            {
                // Place the file in the Download folder to be opened
            }

            ClearFilters();
        }

        private void btnDeleteDocument_Click(object sender, EventArgs e)
        {
            if (_currentUserService.AccessLevel != 4)
            {
                this.Close();
            }
            else
            {
                var selected = GetSelectedDocument();
                if (selected == null)
                {
                    MessageBox.Show("Please select a document.");
                    return;
                }

                var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    _documentService.Delete(selected.DocumentId);
                    ClearFilters();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void btnSearchDocument_Click(object sender, EventArgs e)
        {
            var filtered = _documentDTOs.AsQueryable();

            if (cmbMonthDocument.SelectedIndex == -1 && cmbYearDocument.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a month or year");
                return;
            }
            else
            {
                int searchedMonth = Convert.ToInt32(cmbMonthDocument.SelectedValue);
                int searchedYear = Convert.ToInt32(cmbMonthDocument.SelectedValue);

                if (cmbMonthDocument.SelectedIndex == -1 && cmbYearDocument.SelectedIndex != -1)
                {
                    filtered = filtered.Where(x => x.Date.Year == searchedYear);
                }
                else if (cmbMonthDocument.SelectedIndex != -1 && cmbYearDocument.SelectedIndex == -1)
                {
                    filtered = filtered.Where(x => x.Date.Month == searchedMonth);
                }
                else
                {
                    filtered = filtered.Where(x => x.Date.Month == searchedMonth && x.Date.Year == searchedYear);
                }
            }

            dataGridView1.DataSource = filtered.ToList();
        }

        private void dataGridView1_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.Value != null)
            {
                if (e.Value.ToString() == "Excel Document")
                {
                    e.CellStyle.ForeColor = Color.Black;
                    e.CellStyle.BackColor = Color.DarkOrange;
                }
                else if (e.Value.ToString() == "PDF Document")
                {
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.BackColor = Color.Purple;
                }
                else if (e.Value.ToString() == "PowerPoint Document")
                {
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.BackColor = Color.Brown;
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void txtDocNameDocument_TextChanged(object sender, EventArgs e)
        {
            string search = txtDocNameDocument.Text.Trim().ToLower();
            var filtered = _documentDTOs.Where(x => x.DocumentName.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridView1.DataSource = filtered;
        }

        private void txtDocTypeDocument_TextChanged(object sender, EventArgs e)
        {
            string search = txtDocTypeDocument.Text.Trim().ToLower();
            var filtered = _documentDTOs.Where(x => x.DocumentType.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridView1.DataSource = filtered;
        }
    }
}
