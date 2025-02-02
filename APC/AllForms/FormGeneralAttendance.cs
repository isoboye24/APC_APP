using APC.BLL;
using APC.DAL;
using APC.DAL.DTO;
using OfficeOpenXml.Drawing.Chart;
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
    public partial class FormGeneralAttendance : Form
    {
        public FormGeneralAttendance()
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        GeneralAttendanceBLL bll = new GeneralAttendanceBLL();
        public GeneralAttendanceDetailDTO detail = new GeneralAttendanceDetailDTO();
        public bool isUpdate = false;
        private void btnSave_Click(object sender, EventArgs e)
        {
            int day = dateTimePickerGenAttDate.Value.Day;
            int month = dateTimePickerGenAttDate.Value.Month;
            int year = dateTimePickerGenAttDate.Value.Year;
            bool newMeeting = bll.CheckMeeting(day, month, year);
            if (newMeeting)
            {
                MessageBox.Show("There is a meeting with the same date");
            }
            else if (!isUpdate)
            {
                GeneralAttendanceDetailDTO generalAttendance = new GeneralAttendanceDetailDTO();
                generalAttendance.Day = dateTimePickerGenAttDate.Value.Day;
                generalAttendance.MonthID = dateTimePickerGenAttDate.Value.Month;
                generalAttendance.Year = dateTimePickerGenAttDate.Value.Year.ToString();
                generalAttendance.Summary = txtSummary.Text.Trim();
                generalAttendance.TotalMembersPresent = 0;
                generalAttendance.TotalMembersAbsent = 0;
                generalAttendance.TotalDuesPaid = 0;
                generalAttendance.TotalDuesExpected = 0;
                generalAttendance.TotalDuesBalance = generalAttendance.TotalDuesExpected - generalAttendance.TotalDuesPaid;
                generalAttendance.AttendanceDate = dateTimePickerGenAttDate.Value;
                
                if (bll.Insert(generalAttendance))
                {
                    MessageBox.Show("Meeting was created");
                    dateTimePickerGenAttDate.Value = DateTime.Today;
                    txtSummary.Clear();
                }
            }
            else if (isUpdate)
            {
                if (dateTimePickerGenAttDate.Value == detail.AttendanceDate && detail.Summary == txtSummary.Text.Trim())
                {
                    MessageBox.Show("There is no change");
                }
                else
                {
                    detail.Day = dateTimePickerGenAttDate.Value.Day;
                    detail.MonthID = dateTimePickerGenAttDate.Value.Month;
                    detail.Year = dateTimePickerGenAttDate.Value.Year.ToString();
                    detail.Summary = txtSummary.Text.Trim();
                    detail.AttendanceDate = dateTimePickerGenAttDate.Value;
                    if (bll.Update(detail))
                    {
                        MessageBox.Show("Attendance was updated");
                        this.Close();
                    }
                }
            }
        }

        private void FormGeneralAttendance_Load(object sender, EventArgs e)
        {
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            dateTimePickerGenAttDate.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtSummary.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSave.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            if (isUpdate)
            {
                labelTitle.Text = "Edit meeting on " + detail.Day + "." + detail.MonthID +"."+ detail.Year;
                dateTimePickerGenAttDate.Value = detail.AttendanceDate;
                txtSummary.Text = detail.Summary;
            }
            else
            {
                labelTitle.Text = "Add Meeting";
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
