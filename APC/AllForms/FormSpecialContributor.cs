using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Applications.Services;
using APC.BLL;
using APC.DAL.DAO;
using APC.DAL.DTO;
using APC.Domain.Entities;
using APC.Helper;
using APC.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormSpecialContributor : Form
    {
        private readonly ISpecialContributorService _specialContributorService;
        private readonly IMemberService _memberService;

        private Applications.DTO.SpecialContributorDTO _specialContributorDTO;
        private List<MembersBasicDetailDTO> _memberDTO;

        public int _memberId = 0;
        public bool _isUpdate = false;
        private int buttonSize = 14;
        private float panelSize;



        public FormSpecialContributor(ISpecialContributorService specialContributorService, IMemberService memberService)
        {
            InitializeComponent();
            _specialContributorService = specialContributorService;
            _memberService = memberService;
        }

        /// <summary>
        ///  Drag
        /// </summary>
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void loadForEdit(Applications.DTO.SpecialContributorDTO dto, bool isUpdate)
        {
            _specialContributorDTO = dto;
            _isUpdate = isUpdate;
        }

        private void loadMembers()
        {
            dataGridView1.DataSource = _memberService.GetAll();
            MemberHelper.ConfigureMemberGrid(dataGridView1, MemberHelper.MemberGridType.Basic);
        }

        private MembersBasicDetailDTO GetSelectedMember()
        {
            if (dataGridView1.CurrentRow == null)
                return null;

            return dataGridView1.CurrentRow.DataBoundItem as MembersBasicDetailDTO;
        }

        private void FormSpecialContributor_Load(object sender, EventArgs e)
        {
            loadMembers();

            var selectedMember = GetSelectedMember();
            _memberId = selectedMember.MemberId;

            if (_isUpdate)
            {
                labelTitle.Text = "Edit Contributor " + _specialContributorDTO.FirstName + " " + _specialContributorDTO.LastName;
                txtAmount.Text = _specialContributorDTO.AmountContributed.ToString();
                txtSummary.Text = _specialContributorDTO.Summary;
                dateTimePickerContributedDate.Value = _specialContributorDTO.ContributedDate;

                string imagePath = Application.StartupPath + "\\images\\" + _specialContributorDTO.ImagePath;
                picContributor.ImageLocation = imagePath;

                labelName.Text = _specialContributorDTO.FirstName;
                labelSurname.Text = _specialContributorDTO.LastName;
            }
            else
            {
                labelTitle.Text = "Add Contributor";
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var selected = GetSelectedMember();
            if (selected.MemberId != 0)
            {
                string imagePath = Path.Combine(Application.StartupPath, "images", selected.ImagePath);
                picContributor.ImageLocation = imagePath;

                labelName.Text = selected.FirstName;
                labelSurname.Text = selected.LastName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                decimal amountPaid = Convert.ToDecimal(txtAmount.Text.Trim());
                string summary = txtSummary.Text.Trim();
                DateTime date = dateTimePickerContributedDate.Value;

                if (_specialContributorDTO.SpecialContributorId == 0)
                {
                    var contributor = new SpecialContributor(_memberId, amountPaid, date, summary, _specialContributorDTO.SpecialContributionId);
                    _specialContributorService.Create(contributor);
                    MessageBox.Show("Contributor created successfully!");
                }
                else
                {
                    var contributor = new SpecialContributor(_memberId, amountPaid, _specialContributorDTO.ContributedDate, summary, _specialContributorDTO.SpecialContributionId);

                    _specialContributorService.Update(contributor);
                    MessageBox.Show("Contributor updated successfully!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
