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
using System.Windows.Media;
using System.Xml.Linq;

namespace APC.AllForms
{
    public partial class FormViewMeetingsSummary : Form
    {
        public FormViewMeetingsSummary()
        {
            InitializeComponent();
        }
        public GeneralAttendanceDetailDTO detail = new GeneralAttendanceDetailDTO();
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormViewMeetingsSummary_Load(object sender, EventArgs e)
        {
            #region
            label1.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            labelWordsCount.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            txtSummary.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            btnClose.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            #endregion

            this.Text = "Summary of meeting on " + detail.Day + "." + detail.MonthID + "." + detail.Year;
            txtSummary.Text = detail.Summary;
            string[] words = detail.Summary.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            labelWordsCount.Text = words.Length.ToString();
        }
    }
}
