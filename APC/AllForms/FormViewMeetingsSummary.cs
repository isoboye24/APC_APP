using APC.Helper;
using System;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormViewMeetingsSummary : Form
    {
        private Applications.DTO.GeneralMeetingDTO _generalMeetingDTO;
        public FormViewMeetingsSummary(Applications.DTO.GeneralMeetingDTO generalMeetingDTO)
        {
            InitializeComponent();
            _generalMeetingDTO = generalMeetingDTO;
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(12, btnClose);
            GeneralHelper.ApplyRegularFont(10, labelWordsCount, label1);
            GeneralHelper.ApplyRegularFont(12, txtSummary);
        }

        private void FormViewMeetingsSummary_Load(object sender, EventArgs e)
        {
            resizeControls();

            this.Text = "Summary of meeting on " + _generalMeetingDTO.Day + "." + _generalMeetingDTO.MonthId + "." + _generalMeetingDTO.Year;
            txtSummary.Text = _generalMeetingDTO.Summary;
            string[] words = _generalMeetingDTO.Summary.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            labelWordsCount.Text = words.Length.ToString();
        }
    }
}
