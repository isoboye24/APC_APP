using APC.Applications.Interfaces;
using APC.Domain.Entities;
using APC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC
{
    public partial class FormComments : Form
    {
        private readonly ICommentService _commentService;
        private readonly IMemberService _memberService;

        private Applications.DTO.CommentDTO _commentDTO;

        private bool _isUpdate = false;
        private string imagePath;
        private int _selectedMemberId = 0;

        private List<Applications.DTO.MembersBasicDetailDTO> _memberDTO;

        public FormComments(ICommentService commentService, IMemberService memberService)
        {
            InitializeComponent();
            _commentService = commentService;
            _memberService = memberService;
        }
        /// <summary>
        ///  Drag
        /// </summary>
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);
        
        private void panel1_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void FormComments_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void FormComments_Click(object sender, EventArgs e)
        {

        }
        

        private Applications.DTO.MembersBasicDetailDTO GetSelectedMember()
        {
            if (dataGridView1.CurrentRow == null)
                return null;

            return dataGridView1.CurrentRow.DataBoundItem as Applications.DTO.MembersBasicDetailDTO;
        }

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(14, labelTitle, label1, label2, label4, label5, btnClose, btnSave);
            GeneralHelper.ApplyRegularFont(11, labelCommentDate, labelCommentTime);
            GeneralHelper.ApplyRegularFont(14, txtImagePath, txtName, txtSurname, txtComment, txtSurnameReadOnly);
        }


        private void loadMembers()
        {
            dataGridView1.DataSource = _memberService.GetAll();
            MemberHelper.ConfigureMemberGrid(dataGridView1, MemberHelper.MemberGridType.Basic);
        }

        public void loadForEdit(Applications.DTO.CommentDTO dto, bool isUpdate)
        {
            _commentDTO = dto;
            _selectedMemberId = _commentDTO.MemberId;
            _isUpdate = isUpdate;
        }

        private void FormComments_Load(object sender, EventArgs e)
        {
            resizeControls();

            loadMembers();


            labelCommentDate.Hide();
            labelCommentTime.Hide();

            if (_isUpdate)
            {
                imagePath = Application.StartupPath + "\\images\\" + _commentDTO.ImagePath;
                picProfilePic.ImageLocation = imagePath;

                txtName.Text = _commentDTO.FirstName;
                txtImagePath.Text = _commentDTO.ImagePath;
                txtSurnameReadOnly.Text = _commentDTO.LastName;
                txtComment.Text = _commentDTO.Content;
                labelCommentDate.Text = _commentDTO.FormattedDate;

                labelImagePath.Visible = true;
                txtImagePath.Visible = true;
                labelTitle.Text = "Edit " + _commentDTO.FirstName + " " + _commentDTO.LastName + "'s comment";
            }
            else
            {
                labelImagePath.Hide();
                txtImagePath.Hide();
                labelTitle.Text = "Add Comment";
            }

            var selected = GetSelectedMember();

            txtSurnameReadOnly.Text = selected.LastName;
            txtName.Text = selected.FirstName;
            _selectedMemberId = selected.MemberId;

            imagePath = Application.StartupPath + "\\images\\" + selected.ImagePath;
            picProfilePic.ImageLocation = imagePath;
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var content = txtComment.Text.Trim();

                if (_commentDTO.CommentId == 0)
                {
                    var comment = new Comment(content, _selectedMemberId, DateTime.Today);
                    _commentService.Create(comment);
                    MessageBox.Show("Comment created successfully!");
                }
                else
                {
                    var comment = new Comment(content, _selectedMemberId, _commentDTO.Date);

                    _commentService.Update(comment);
                    MessageBox.Show("Comment updated successfully!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSurname_TextChanged(object sender, EventArgs e)
        {
            string search = txtSurname.Text.Trim().ToLower();
            var filtered = _memberDTO.Where(x => x.LastName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridView1.DataSource = filtered;
        }

        private void iconMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
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
            }
        }
    }
}
