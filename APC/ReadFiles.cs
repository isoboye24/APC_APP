using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Words.NET;
using OfficeOpenXml;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace APC
{
    public static class ReadFiles
    {
        public static void ReadWord(string filePath, TextBox textbox)
        {
            using (DocX doc = DocX.Load(filePath))
            {
                foreach (var paragraph in doc.Paragraphs)
                {
                    textbox.Text += paragraph.Text + Environment.NewLine;                    
                }
            }
        }
        private static string GetDownloadsFolderPath()
        {
            string downloadsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            downloadsFolderPath = Path.Combine(downloadsFolderPath, "Downloads");
            return downloadsFolderPath;
        }
        public static void CopyDocument(string documentPath, string fileName)
        {
            string filePath = Application.StartupPath + "\\documents\\" + documentPath;
            string downloadsFolderPath = GetDownloadsFolderPath();
            string destinationPath = Path.Combine(downloadsFolderPath, Path.GetFileName(filePath));
            try
            {
                File.Copy(filePath, destinationPath, true);
                MessageBox.Show($"{fileName} downloaded to: {destinationPath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error copying file: {ex.Message}");
            }
        }
    }
}
