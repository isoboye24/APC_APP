using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Xceed.Document.NET;

namespace APC
{
    public class GeneralHelper
    {
        static string connectingString = "Server=localhost\\sqlexpress;Database=APC;integrated security=True;encrypt=True;trustservercertificate=True;";

        public static void CreateChart(System.Windows.Forms.DataVisualization.Charting.Chart chart, string query,
             SeriesChartType chartType, string seriesName, string chartArea)
        {
            using (SqlConnection con = new SqlConnection(connectingString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);

                chart.DataSource = dt;
                chart.Series.Clear();

                System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series(seriesName);
                series.XValueMember = dt.Columns[0].ColumnName;
                series.YValueMembers = dt.Columns[1].ColumnName;
                series.ChartType = chartType;
                chart.Series.Add(series);
                chart.DataBind();

                CustomizeChart(series, chartType, chartArea);
            }
        }
        private static void CustomizeChart(System.Windows.Forms.DataVisualization.Charting.Series serie, SeriesChartType chartType, string chartArea)
        {
            switch (chartType)
            {
                case SeriesChartType.Pie:
                    foreach (DataPoint point in serie.Points)
                    {
                        point.Label = string.Format("{0} ({1:P})", point.AxisLabel,
                            point.YValues[0] / serie.Points.Sum(x => x.YValues[0]));
                    }
                    serie.IsValueShownAsLabel = true;
                    serie.LabelForeColor = Color.Yellow;
                    serie.Color = Color.Navy;
                    serie.ChartArea = chartArea;
                    break;

                case SeriesChartType.Column:
                    serie.IsValueShownAsLabel = true;
                    break;
            }
        }

        public static bool isNumber(KeyPressEventArgs e, TextBox txt)
        {
            // Allow control keys (Backspace, Delete, etc.)
            if (char.IsControl(e.KeyChar))
            {
                return false;
            }

            // Allow digits
            if (char.IsDigit(e.KeyChar))
            {
                return false;
            }

            // Allow one decimal point (.)
            if (e.KeyChar == '.' && !txt.Text.Contains('.'))
            {
                return false;
            }

            return true;
        }

        public static void ValueCount(Label property, int value, int horizontalPoint, int verticalPoint)
        {
            if (value.ToString().Length < 2)
            {
                property.Text = value.ToString();
                property.Location = new Point(horizontalPoint, verticalPoint);
            }
            else if (value.ToString().Length > 1 && value.ToString().Length < 3)
            {
                property.Text = value.ToString();
                property.Location = new Point(horizontalPoint - 10, verticalPoint);
            }
            else if (value.ToString().Length > 2 && value.ToString().Length < 4)
            {
                property.Text = value.ToString();
                property.Location = new Point(horizontalPoint - 30, verticalPoint);
            }
            else if (value.ToString().Length > 3 && value.ToString().Length < 5)
            {
                property.Text = value.ToString();
                property.Location = new Point(horizontalPoint - 50, verticalPoint);
            }
            else if (value.ToString().Length > 4 && value.ToString().Length < 6)
            {
                property.Text = value.ToString();
                property.Location = new Point(horizontalPoint - 70, verticalPoint);
            }
            else if (value.ToString().Length > 5 && value.ToString().Length < 7)
            {
                property.Text = value.ToString();
                property.Location = new Point(horizontalPoint - 90, verticalPoint);
            }
            else if (value.ToString().Length > 6 && value.ToString().Length < 8)
            {
                property.Text = value.ToString();
                property.Location = new Point(horizontalPoint - 110, verticalPoint);
            }
            else
            {
                MessageBox.Show("The digit of "+ property.Name +" is too big. The panel needs adjustment.");
                property.Text = "########";
                property.Location = new Point(horizontalPoint - 140, verticalPoint);
            }
        }

        public static void ValueCount(Label property, string value, int horizontalPoint, int verticalPoint)
        {
            if (value.Length < 2)
            {
                property.Text = value;
                property.Location = new Point(horizontalPoint, verticalPoint);
            }
            else if (value.Length > 1 && value.Length < 3)
            {
                property.Text = value;
                property.Location = new Point(horizontalPoint - 10, verticalPoint);
            }
            else if (value.Length > 2 && value.Length < 4)
            {
                property.Text = value;
                property.Location = new Point(horizontalPoint - 30, verticalPoint);
            }
            else if (value.Length > 3 && value.Length < 5)
            {
                property.Text = value;
                property.Location = new Point(horizontalPoint - 50, verticalPoint);
            }
            else if (value.Length > 4 && value.Length < 6)
            {
                property.Text = value;
                property.Location = new Point(horizontalPoint - 70, verticalPoint);
            }
            else if (value.Length > 5 && value.Length < 7)
            {
                property.Text = value;
                property.Location = new Point(horizontalPoint - 90, verticalPoint);
            }
            else if (value.Length > 6 && value.Length < 8)
            {
                property.Text = value.ToString();
                property.Location = new Point(horizontalPoint - 110, verticalPoint);
            }
            else
            {
                MessageBox.Show("The digit of " + property.Name + " is too big. The panel needs adjustment.");
                property.Text = "########";
                property.Location = new Point(horizontalPoint - 140, verticalPoint);
            }
        }
        public static void ValueCountInDecimal(Label property, decimal value, int horizontalPoint, int verticalPoint)
        {
            if (value.ToString("0.00").Length < 5)
            {
                property.Text = value.ToString("0.00");
                property.Location = new Point(horizontalPoint, verticalPoint + 40);
            }
            else if (value.ToString("0.00").Length > 4 && value.ToString("0.00").Length < 6)
            {
                property.Text = value.ToString("0.00");
                property.Location = new Point(horizontalPoint - 20, verticalPoint);
            }
            else if (value.ToString("0.00").Length > 5 && value.ToString("0.00").Length < 7)
            {
                property.Text = value.ToString("0.00");
                property.Location = new Point(horizontalPoint - 40, verticalPoint);
            }
            else if (value.ToString("0.00").Length > 6 && value.ToString("0.00").Length < 8)
            {
                property.Text = value.ToString("0.00");
                property.Location = new Point(horizontalPoint - 60, verticalPoint);
            }
            else if (value.ToString("0.00").Length > 7 && value.ToString("0.00").Length < 9)
            {
                property.Text = value.ToString("0.00");
                property.Location = new Point(horizontalPoint - 80, verticalPoint);
            }
            else if (value.ToString("0.00").Length > 8 && value.ToString("0.00").Length < 10)
            {
                property.Text = value.ToString("0.00");
                property.Location = new Point(horizontalPoint - 100, verticalPoint);
            }
            else if (value.ToString("0.00").Length > 9 && value.ToString("0.00").Length < 11)
            {
                property.Text = value.ToString("0.00");
                property.Location = new Point(horizontalPoint - 120, verticalPoint);
            }
            else if (value.ToString("0.00").Length > 10 && value.ToString("0.00").Length < 12)
            {
                property.Text = value.ToString("0.00");
                property.Location = new Point(horizontalPoint - 140, verticalPoint);
            }
            else if (value.ToString("0.00").Length > 11 && value.ToString("0.00").Length < 13)
            {
                property.Text = value.ToString("0.00");
                property.Location = new Point(horizontalPoint - 160, verticalPoint);
            }            
            else if (value.ToString("0.00").Length > 12)
            {
                MessageBox.Show("The digit of " + property.Name + " is too big. The panel needs adjustment.");
                property.Text = "###########";
                property.Location = new Point(horizontalPoint - 180, verticalPoint);
            }
        }
        public static void ComboBoxProps(ComboBox cmb, string name, string ID)
        {
            cmb.DisplayMember = name;
            cmb.ValueMember = ID;
            cmb.SelectedIndex = -1;
        }

        public static string GetDocumentType(string fileExtension)
        {
            if (fileExtension.ToLower() == ".txt")
            {
                return "Text Document";
            }
            else if (fileExtension.ToLower() == ".docx" || fileExtension.ToLower() == ".doc")
            {
                return "Word Document";
            }
            else if (fileExtension.ToLower() == ".pdf")
            {
                return "PDF Document";
            }
            else if (fileExtension.ToLower() == ".html")
            {
                return "HTML Document";
            }
            else if (fileExtension.ToLower() == ".xlsx")
            {
                return "Excel Document";
            }
            else if (fileExtension.ToLower() == ".ppt")
            {
                return "PowerPoint Document";
            }
            else
            {
                return "Unknown Document";
            }
        }

        public static IEnumerable<int> FindMembersAppearingThreeTimes(List<int> list)
        {
            var groupedMembers = list.GroupBy(x => x).Where(group => group.Count() > 2).Select(group => group.Key);
            return groupedMembers;
        }

        public static string ConventIntToMonth(int month)
        {
            if (month == 1)
            {
                return "January";
            }
            else if (month == 2)
            {
                return "February";
            }
            else if (month == 3)
            {
                return "March";
            }
            else if (month == 4)
            {
                return "April";
            }
            else if (month == 5)
            {
                return "May";
            }
            else if (month == 6)
            {
                return "June";
            }
            else if (month == 7)
            {
                return "July";
            }
            else if (month == 8)
            {
                return "August";
            }
            else if (month == 9)
            {
                return "September";
            }
            else if (month == 10)
            {
                return "October";
            }
            else if (month == 11)
            {
                return "November";
            }
            else if (month == 12)
            {
                return "December";
            }
            else
            {
                return "Unknown month";
            }
        }

        public static bool IsDirectoryWritable(string dirPath)
        {
            try
            {
                string testFile = Path.Combine(dirPath, Path.GetRandomFileName());
                using (FileStream fs = File.Create(testFile, 1, FileOptions.DeleteOnClose)) { }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static decimal CalculateYearlyDue(DateTime registrationDate, int targetYear)
        {
            const decimal monthlyDue = 10m;
            int registrationYear = registrationDate.Year;
            int registrationMonth = registrationDate.Month;

            int monthsToPay = 0;

            if (registrationYear < targetYear)
            {
                // Registered before the target year → pays all 12 months
                monthsToPay = 12;
            }
            else if (registrationYear == targetYear)
            {
                // Registered in target year → starts paying from next month
                monthsToPay = 12 - registrationMonth;
                if (monthsToPay < 0) monthsToPay = 0; // just in case
            }
            else
            {
                // Registered after target year → pays nothing yet
                monthsToPay = 0;
            }

            return monthsToPay * monthlyDue;
        }

        public static void ApplyBoldFont11(params Control[] controls)
        {
            foreach (var control in controls)
            {
                control.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Bold);
            }
        }
        public static void ApplyRegularFont11(params Control[] controls)
        {
            foreach (var control in controls)
            {
                control.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
            }
        }
        public static void ApplyBoldFont12(params Control[] controls)
        {
            foreach (var control in controls)
            {
                control.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            }
        }
        public static void ApplyRegularFont12(params Control[] controls)
        {
            foreach (var control in controls)
            {
                control.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Regular);
            }
        }

        public static void ApplyBoldFont14(params Control[] controls)
        {
            foreach (var control in controls)
            {
                control.Font = new System.Drawing.Font("Segoe UI", 14, FontStyle.Bold);
            }
        }
        public static void ApplyRegularFont14(params Control[] controls)
        {
            foreach (var control in controls)
            {
                control.Font = new System.Drawing.Font("Segoe UI", 14, FontStyle.Regular);
            }
        }
        public static void ApplyBoldFont16(params Control[] controls)
        {
            foreach (var control in controls)
            {
                control.Font = new System.Drawing.Font("Segoe UI", 16, FontStyle.Bold);
            }
        }
        public static void ApplyRegularFont16(params Control[] controls)
        {
            foreach (var control in controls)
            {
                control.Font = new System.Drawing.Font("Segoe UI", 16, FontStyle.Regular);
            }
        }

        public static void SetVisibleColumns(DataGridView grid, params string[] visibleColumns)
        {
            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.Visible = visibleColumns.Contains(column.Name);
                column.HeaderCell.Style.Font = new System.Drawing.Font("Segoe UI", 16, FontStyle.Bold);
            }
        }

        public static void RenameColumns(DataGridView grid, Dictionary<string, string> mappings)
        {
            foreach (var map in mappings)
            {
                if (grid.Columns.Contains(map.Key))
                    grid.Columns[map.Key].HeaderText = map.Value;
            }
        }
    }
}
