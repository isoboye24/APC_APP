using APC.BLL;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace APC.AllForms
{
    public partial class FormDocumentList : Form
    {
        public FormDocumentList()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormDocument open = new FormDocument();
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.DocumentID==0)
            {
                MessageBox.Show("Please select a document from the table");                
            }
            else
            {
                FormDocument open = new FormDocument();
                open.detail = detail;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }            
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (detail.DocumentID == 0)
            {
                MessageBox.Show("Please select a document from the table");
            }
            else
            {
                if (detail.DocumentType != "Word Document")
                {
                    ReadFiles.CopyDocument(detail.DocumentPath, detail.DocumentName);
                    ClearFilters();
                }
                else if (detail.DocumentType == "Word Document")
                {
                    DialogResult result = MessageBox.Show("Open document \"" + detail.DocumentName + " ?\"", "Warning!", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        FormViewDocument open = new FormViewDocument();
                        open.detail = detail;
                        this.Hide();
                        open.ShowDialog();
                        this.Visible = true;
                        ClearFilters();
                    }
                }                
            }           
        }
        DocumentBLL bll = new DocumentBLL();
        DocumentDTO dto = new DocumentDTO();
        private void FormDocumentList_Load(object sender, EventArgs e)
        {
            label2.Tag = "resizable";
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label3.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label4.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label6.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            txtDocName.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtDocType.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtYear.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            cmbMonth.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnAdd.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnDelete.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnUpdate.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnView.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSearch.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnClear.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            dto = bll.Select();
            cmbMonth.DataSource = dto.Months;
            General.ComboBoxProps(cmbMonth, "MonthName", "MonthID");

            dataGridView1.DataSource = dto.Documents;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Document Name";
            dataGridView1.Columns[2].HeaderText = "Document Type";
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].HeaderText = "Date";
            dataGridView1.Columns[8].Visible = false;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            }

            labelDocCount.Text = bll.SelectDocCount().ToString();
            if (LoginInfo.AccessLevel != 4)
            {
                btnDelete.Hide();
            }
        }
        private void FillDataGrid()
        {
            bll = new DocumentBLL();
            dto = bll.Select();
            dataGridView1.DataSource = dto.Documents;
            labelDocCount.Text = bll.SelectDocCount().ToString();
        }
        private void ClearFilters()
        {
            txtDocName.Clear();
            txtDocType.Clear();
            txtYear.Clear();
            cmbMonth.SelectedIndex = -1;
            FillDataGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void txtDocName_TextChanged(object sender, EventArgs e)
        {
            List<DocumentDetailDTO> list = dto.Documents;
            list = list.Where(x => x.DocumentName.Contains(txtDocName.Text.Trim())).ToList();
            dataGridView1.DataSource = list;
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            List<DocumentDetailDTO> list = dto.Documents;
            list = list.Where(x => x.Year.Contains(txtYear.Text.Trim())).ToList();
            dataGridView1.DataSource = list;
        }

        private void txtDocType_TextChanged(object sender, EventArgs e)
        {
            List<DocumentDetailDTO> list = dto.Documents;
            list = list.Where(x => x.DocumentType.Contains(txtDocType.Text.Trim())).ToList();
            dataGridView1.DataSource = list;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<DocumentDetailDTO> list = dto.Documents;
            if (cmbMonth.SelectedIndex != -1)
            {
                list = list.Where(x => x.MonthID == Convert.ToInt32(cmbMonth.SelectedValue)).ToList();
            }           
            dataGridView1.DataSource = list;
        }
        DocumentDetailDTO detail = new DocumentDetailDTO();
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail = new DocumentDetailDTO();
            detail.DocumentID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            detail.DocumentName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            detail.DocumentType = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();            
            detail.Day = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
            detail.MonthID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
            detail.MonthName = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            detail.Year = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            detail.Date = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            detail.DocumentPath = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (detail.DocumentID == 0)
            {
                MessageBox.Show("Please choose a document from the table");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?","Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (bll.Delete(detail))
                    {
                        MessageBox.Show("Document was deleted");
                        ClearFilters();
                    }
                }
            }
        }
    }
}
