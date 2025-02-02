using APC.BLL;
using APC.DAL.DTO;
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

namespace APC.AllForms
{
    public partial class FormEvenImages : Form
    {
        public FormEvenImages()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);
        
        private void panel1_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormEventSingleImage open = new FormEventSingleImage();
            open.eventID = detail.EventID;
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            FillDataGrid();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (imageDetail.EventImageID == 0)
            {
                MessageBox.Show("Please choose an image from the table");
            }
            else
            {
                FormEventSingleImage open = new FormEventSingleImage();
                open.detail = imageDetail;
                open.isUdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                FillDataGrid();
            }            
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (imageDetail.EventImageID == 0)
            {
                MessageBox.Show("Please choose an image from the table");
            }
            else
            {
                FormEventSingleImage open = new FormEventSingleImage();
                open.detail = imageDetail;
                open.isView = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                FillDataGrid();
            }            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public EventsDetailDTO detail = new EventsDetailDTO();
        public bool isView = false;
        EventImageBLL bll = new EventImageBLL();
        EventImageDTO dto = new EventImageDTO();
        private void FormEvenImages_Load(object sender, EventArgs e)
        {
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            txtImageCaption.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnAdd.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnDelete.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnUpdate.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnView.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            dto = bll.SelectSpecificImage(detail.EventID);

            dataGridView1.DataSource = dto.EventImages;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].HeaderText = "No.";
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[7].HeaderText = "Picture Caption";
            dataGridView1.Columns[8].Visible = false;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            }
            if (LoginInfo.AccessLevel != 4)
            {
                btnClose.Hide();
                btnDelete.Text = "Close";
            }
        }
        private void FillDataGrid()
        {
            bll = new EventImageBLL();
            dto = bll.SelectSpecificImage(detail.EventID);
            dataGridView1.DataSource = dto.EventImages;
            txtImageCaption.Clear();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        EventImageDetailDTO imageDetail = new EventImageDetailDTO();
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            imageDetail = new EventImageDetailDTO();
            imageDetail.EventImageID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            imageDetail.EventID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            imageDetail.EventTitle = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            imageDetail.EventYear = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
            imageDetail.Summary = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            imageDetail.ImagePath = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            imageDetail.Counter = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
            imageDetail.ImageCaption = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            imageDetail.isEventDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
            string imagePath = Application.StartupPath + "\\images\\" + imageDetail.ImagePath;
            picImage.ImageLocation = imagePath;
        }

        private void txtImageCaption_TextChanged(object sender, EventArgs e)
        {
            List<EventImageDetailDTO> list = dto.EventImages;
            list = list.Where(x => x.ImageCaption.Contains(txtImageCaption.Text)).ToList();
            dataGridView1.DataSource = list;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (LoginInfo.AccessLevel != 4)
            {
                this.Close();
            }
            else
            {
                if (imageDetail.EventImageID == 0)
                {
                    MessageBox.Show("Please choose an image from the table");
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        if (bll.Delete(imageDetail))
                        {
                            MessageBox.Show("The picture was deleted");
                            FillDataGrid();
                        }
                    }
                }
            }            
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
