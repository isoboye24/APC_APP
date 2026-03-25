using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.BLL;
using APC.DAL.DTO;
using APC.Helper;
using APC.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormViewSpecialContribution : Form
    {
        private readonly ISpecialContributorService _specialContributorService;
        private readonly IMemberService _memberService;

        private Applications.DTO.SpecialContributionDTO _specialContributionsDTO;
        private List<Applications.DTO.SpecialContributorDTO> _specialContributorsDTO;

        public FormViewSpecialContribution(Applications.DTO.SpecialContributionDTO dto, IMemberService memberService, ISpecialContributorService specialContributorService, List<Applications.DTO.SpecialContributorDTO> specialContributorsDTO)
        {
            InitializeComponent();
            _specialContributionsDTO = dto;
            _memberService = memberService;
            _specialContributorService = specialContributorService;
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

        private int buttonSize = 14;
        private float panelSize;

        private void loadContributors()
        {
            dataGridView1.DataSource = _specialContributorService.GetAllByContributionId(_specialContributionsDTO.SpecialContributionId);
            SpecialContributorHelper.ConfigureSpecialContributorGrid(dataGridView1, SpecialContributorHelper.SpecialContributorGridType.Basic);
        }

        private void FormViewSpecialContribution_Load(object sender, EventArgs e)
        {
            #region
            cmbContributionStatus.Items.Add("Incomplete");
            cmbContributionStatus.Items.Add("Completed");
            cmbContributionStatus.Items.Add("Extra");

            labelName.Text = _specialContributionsDTO.FirstName;
            labelSurname.Text = _specialContributionsDTO.LastName;
            labelTitle.Text = _specialContributionsDTO.Title;
            labelStartDate.Text = _specialContributionsDTO.FormattedContributionStartDate;
            labelEndDate.Text = _specialContributionsDTO.FormattedContributionEndDate;
            if (DateTime.Today > _specialContributionsDTO.ContributionEndDate)
            {
                labelEnded.Text = "Ended on";
            }
            else if (DateTime.Today > _specialContributionsDTO.ContributionEndDate)
            {
                labelEnded.Text = "Ends Today";
            }
            else
            {
                labelEnded.Text = "Ends on";
            }
            labelSummary.Text = _specialContributionsDTO.Summary;
            

            string imagePath = Application.StartupPath + "\\images\\" + _specialContributionsDTO.ImagePath;
            picSupervisor.ImageLocation = imagePath;

            labelTotalContributors.Text = dataGridView1.RowCount.ToString();
            #endregion

            loadContributors();

            refreshCounts();
        }

        private void refreshCounts()
        {
            labelTotalContributors.Text = "Rows : " + dataGridView1.RowCount.ToString();
            labelTotalExpectedAmt.Text = "Total Amt. Exp. : " + _specialContributionsDTO.FormattedAmountExpected;
            labelTotalContributedAmt.Text = "Total Amt. Cont. : " + _specialContributionsDTO.FormattedTotalContributedAmount;
            labelTotalBalance.Text = "Bal : " + Math.Abs(_specialContributionsDTO.AmountExpected - _specialContributionsDTO.TotalContributedAmount) + " € (" + _specialContributionsDTO.Status + ")";
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

            loadContributors();

            refreshCounts();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new FormSpecialContributor(_specialContributorService, _memberService);
            form.ShowDialog();
            ClearFilters();
        }

        private Applications.DTO.SpecialContributorDTO GetSelectedSpecialContributor()
        {
            if (dataGridView1.CurrentRow == null)
                return null;

            return dataGridView1.CurrentRow.DataBoundItem as Applications.DTO.SpecialContributorDTO;
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedSpecialContributor();
            if (selected == null)
            {
                MessageBox.Show("Please select a contributor from the table");
                return;
            }

            var form = new FormSpecialContributor(_specialContributorService, _memberService);
            form.loadForEdit(selected, true);
            form.ShowDialog();

            ClearFilters();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = GeneralHelper.isNumber(e, (TextBox)sender);
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            string search = txtName.Text.Trim().ToLower();
            var filtered = _specialContributorsDTO.Where(x => x.FirstName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridView1.DataSource = filtered;
        }

        private void txtSurname_TextChanged(object sender, EventArgs e)
        {
            string search = txtSurname.Text.Trim().ToLower();
            var filtered = _specialContributorsDTO.Where(x => x.LastName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridView1.DataSource = filtered;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var filtered = _specialContributorsDTO.AsQueryable();

            if (txtAmount.Text.Trim().Length == 0 && cmbContributionStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select either a status or give an amount");
                return;
            }

            if (txtAmount.Text.Trim().Length != 0)
            {
                decimal amount = Convert.ToDecimal(txtAmount.Text.Trim());
                if (rbEqual.Checked)
                {
                    filtered = filtered.Where(x => x.AmountContributed == amount);
                }
                else if (rbLess.Checked)
                {
                    filtered = filtered.Where(x => x.AmountContributed < amount);
                }
                else if (rbMore.Checked)
                {
                    filtered = filtered.Where(x => x.AmountContributed > amount);
                }
                else
                {
                    MessageBox.Show("Please select any of the less or equal or more than buttons");
                }
            }

            if (cmbContributionStatus.SelectedIndex != -1)
            {
                if (cmbContributionStatus.SelectedIndex == 0)
                {
                    filtered = filtered.Where(x => x.PaymentStatus == "Incomplete");
                }
                if (cmbContributionStatus.SelectedIndex == 1)
                {
                    filtered = filtered.Where(x => x.PaymentStatus == "Completed");
                }
                if (cmbContributionStatus.SelectedIndex == 2)
                {
                    filtered = filtered.Where(x => x.PaymentStatus == "Extra");
                }
            }

            dataGridView1.DataSource = filtered.ToList();
        }

        private void btnViewSummary_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedSpecialContributor();
            if (selected == null)
            {
                MessageBox.Show("Please select a contributor.");
                return;
            }

            var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _specialContributorService.Delete(selected.SpecialContributorId);
                ClearFilters();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedSpecialContributor();
            if (selected == null)
            {
                MessageBox.Show("Please select a contributor from the table");
                return;
            }

            var form = new FormViewSpecialContributor(selected, _memberService);
            form.ShowDialog();

            ClearFilters();
        }
    }
}
