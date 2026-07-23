using APC.Applications.DTO;
using APC.Helper;
using APC.Utility;
using System;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormViewMeetingsSummary : BaseFormDashboard
    {
        private GeneralMeetingDTO _generalMeetingDTO;
        public FormViewMeetingsSummary()
        {
            InitializeComponent();
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void loadForView(GeneralMeetingDTO generalMeetingDTO)
        {
            _generalMeetingDTO = generalMeetingDTO;
        }

        private void FormViewMeetingsSummary_Load(object sender, EventArgs e)
        {
            this.Text = "Summary of meeting on " + _generalMeetingDTO.GeneralMeetingDate.ToString("dd.MM.yyy");
            txtSummary.Text = _generalMeetingDTO.Summary;

            string[] words;
            if (_generalMeetingDTO.Summary != null)
            {
                words = _generalMeetingDTO.Summary.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                svLabelWordsCount.Text = words.Length.ToString();
            }
            else
            {
                svLabelWordsCount.Text = 0.ToString();
            }
        }
    }
}
