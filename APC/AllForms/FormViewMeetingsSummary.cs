using APC.Applications.DTO;
using APC.Helper;
using System;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormViewMeetingsSummary : Form
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

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(12, btnClose);
            GeneralHelper.ApplyRegularFont(10, labelWordsCount, label1);
            GeneralHelper.ApplyRegularFont(12, txtSummary);
        }

        public void loadForView(GeneralMeetingDTO generalMeetingDTO)
        {
            _generalMeetingDTO = generalMeetingDTO;
        }

        private void FormViewMeetingsSummary_Load(object sender, EventArgs e)
        {
            resizeControls();

            this.Text = "Summary of meeting on " + _generalMeetingDTO.GeneralMeetingDate.ToString("dd.MM.yyy");
            txtSummary.Text = _generalMeetingDTO.Summary;

            string[] words;
            if (_generalMeetingDTO.Summary != null)
            {
                words = _generalMeetingDTO.Summary.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                labelWordsCount.Text = words.Length.ToString();
            }
            else
            {
                labelWordsCount.Text = 0.ToString();
            }
        }
    }
}
