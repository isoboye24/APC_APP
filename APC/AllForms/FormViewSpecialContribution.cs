using APC.BLL;
using APC.DAL.DTO;
using APC.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormViewSpecialContribution : Form
    {
        public FormViewSpecialContribution()
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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public SpecialContributionDetailDTO detail = new SpecialContributionDetailDTO();
        private int buttonSize = 14;
        private float panelSize;

        SpecialContributorDetailDTO contributorDetail = new SpecialContributorDetailDTO();
        SpecialContributorsBLL contributorBLL = new SpecialContributorsBLL();
        SpecialContributorDTO contributorDTO = new SpecialContributorDTO();
        private void FormViewSpecialContribution_Load(object sender, EventArgs e)
        {
            #region
            cmbContributionStatus.Items.Add("Incomplete");
            cmbContributionStatus.Items.Add("Completed");
            cmbContributionStatus.Items.Add("Extra");

            labelName.Text = detail.SupervisorName;
            labelSurname.Text = detail.SupervisorSurname;
            labelTitle.Text = detail.ContributionTitle;
            labelStartDate.Text = detail.StartDate;
            labelEndDate.Text = detail.EndDate;
            if (DateTime.Today > detail.ContributionEndDate)
            {
                labelEnded.Text = "Ended on";
            }
            else if (DateTime.Today > detail.ContributionEndDate)
            {
                labelEnded.Text = "Ends Today";
            }
            else
            {
                labelEnded.Text = "Ends on";
            }
            labelSummary.Text = detail.Summary;
            

            string imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
            picSupervisor.ImageLocation = imagePath;

            labelTotalContributors.Text = dataGridView1.RowCount.ToString();
            #endregion

            contributorDTO = contributorBLL.Select(detail.SpecialContributionID);

            #region
            dataGridView1.DataSource = contributorDTO.SpecialContributors;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].HeaderText = "No.";
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].HeaderText = "Name";
            dataGridView1.Columns[4].HeaderText = "Surname";
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].HeaderText = "Expected";
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].HeaderText = "Contributed";
            dataGridView1.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[10].HeaderText = "Balance";
            dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[11].HeaderText = "Status";
            dataGridView1.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].HeaderText = "Date";
            dataGridView1.Columns[13].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[15].Visible = false;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            }
            #endregion

            refreshCounts();
        }

        private void refreshCounts()
        {
            labelTotalContributors.Text = "Rows : " + dataGridView1.RowCount.ToString();
            labelTotalExpectedAmt.Text = "Total Amt. Exp. : " + detail.AmountExpectedWithCurrency;
            labelTotalContributedAmt.Text = "Total Amt. Cont. : " + detail.AmountContributedWithCurrency;
            labelTotalBalance.Text = "Bal : " + Math.Abs(detail.AmountExpected - detail.AmountContributed) + " € (" + detail.Status + ")";
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearFilters()
        {
            txtName.Clear();
            txtSurname.Clear();
            txtAmount.Clear();
            cmbContributionStatus.SelectedIndex = -1;

            contributorBLL = new SpecialContributorsBLL();
            contributorDTO = contributorBLL.Select(detail.SpecialContributionID);
            dataGridView1.DataSource = contributorDTO.SpecialContributors;

            refreshCounts();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormSpecialContributor open = new FormSpecialContributor();
            open.specialContributionID = detail.SpecialContributionID;
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (contributorDetail.ContributorID == 0)
            {
                MessageBox.Show("Please select a contributor from the table.");
            }
            else
            {
                FormSpecialContributor open = new FormSpecialContributor();
                open.specialContributionID = detail.SpecialContributionID;
                open.detail = contributorDetail;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
                
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count)
                return;

            var row = dataGridView1.Rows[e.RowIndex];
            if (row == null || row.IsNewRow)
                return;

            contributorDetail = new SpecialContributorDetailDTO();
            contributorDetail.ContributorID = Convert.ToInt32(row.Cells[0].Value ?? 0);
            contributorDetail.MemberID = Convert.ToInt32(row.Cells[1].Value ?? 0);
            contributorDetail.Counter = Convert.ToInt32(row.Cells[2].Value ?? 0);
            contributorDetail.Name = row.Cells[3].Value?.ToString() ?? string.Empty;
            contributorDetail.Surname = row.Cells[4].Value?.ToString() ?? string.Empty;
            contributorDetail.ImagePath = row.Cells[5].Value?.ToString() ?? string.Empty;
            contributorDetail.AmountExpected = Convert.ToDecimal(row.Cells[6].Value ?? 0);
            contributorDetail.AmountExpectedWithCurrency = row.Cells[7].Value?.ToString() ?? string.Empty;
            contributorDetail.AmountContributed = Convert.ToDecimal(row.Cells[8].Value ?? 0);
            contributorDetail.AmountContributedWithCurrency = row.Cells[9].Value?.ToString() ?? string.Empty;
            contributorDetail.Balance = row.Cells[10].Value?.ToString() ?? string.Empty;
            contributorDetail.AmountContributedStatus = row.Cells[11].Value?.ToString() ?? string.Empty;
            contributorDetail.ContributedDate = Convert.ToDateTime(row.Cells[12].Value ?? 0);
            contributorDetail.Date = row.Cells[13].Value?.ToString() ?? string.Empty;
            contributorDetail.ContributionID = Convert.ToInt32(row.Cells[14].Value ?? 0);
            contributorDetail.Summary = row.Cells[15].Value?.ToString() ?? string.Empty;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e, (TextBox)sender);
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            List<SpecialContributorDetailDTO> list = contributorDTO.SpecialContributors;
            list = list.Where(x => x.Name.Contains(txtName.Text.Trim())).ToList();
            dataGridView1.DataSource = list;
        }

        private void txtSurname_TextChanged(object sender, EventArgs e)
        {
            List<SpecialContributorDetailDTO> list = contributorDTO.SpecialContributors;
            list = list.Where(x => x.Surname.Contains(txtSurname.Text.Trim())).ToList();
            dataGridView1.DataSource = list;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<SpecialContributorDetailDTO> list = contributorDTO.SpecialContributors;
            if (txtAmount.Text.Trim() != "")
            {
                if (rbEqual.Checked)
                {
                    list = list.Where(x => x.AmountContributed == Convert.ToInt32(txtAmount.Text.Trim())).ToList();
                }
                else if (rbLess.Checked)
                {
                    list = list.Where(x => x.AmountContributed < Convert.ToInt32(txtAmount.Text.Trim())).ToList();
                }
                else if (rbMore.Checked)
                {
                    list = list.Where(x => x.AmountContributed > Convert.ToInt32(txtAmount.Text.Trim())).ToList();
                }
                else
                {
                    MessageBox.Show("Please select a criterion from the monthly dues group");
                }

                dataGridView1.DataSource = list;
            }
            else
            {
                dataGridView1.DataSource = list;
            }

            if (cmbContributionStatus.SelectedIndex != -1)
            {
                if (cmbContributionStatus.SelectedIndex == 0)
                {
                    list = list.Where(x => x.AmountContributedStatus == "Incomplete").ToList();
                }
                if (cmbContributionStatus.SelectedIndex == 1)
                {
                    list = list.Where(x => x.AmountContributedStatus == "Completed").ToList();
                }
                if (cmbContributionStatus.SelectedIndex == 2)
                {
                    list = list.Where(x => x.AmountContributedStatus == "Extra").ToList();
                }

                dataGridView1.DataSource = list;
            }
            
        }

        private void btnViewSummary_Click(object sender, EventArgs e)
        {
            if (contributorDetail.ContributorID == 0)
            {
                MessageBox.Show("Please select a contributor from the table.");
            }
            else
            {
                FormViewSpecialContributor open = new FormViewSpecialContributor();
                open.detail = contributorDetail;
                open.contributionDetail = detail;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (contributorDetail.ContributorID == 0)
            {
                MessageBox.Show("Please choose a contributor from the table.");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (contributorBLL.Delete(contributorDetail))
                    {
                        MessageBox.Show("Contributor was deleted successfully!");
                        ClearFilters();
                    }
                }
            }
        }
    }
}
