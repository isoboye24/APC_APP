using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace APC.Helper
{
    public class GeneralHelper
    {

        public static string Capitalize(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return char.ToUpper(value[0]) + value.Substring(1).ToLower();
        }

        public static bool isNumber(KeyPressEventArgs e, TextBox txt)
        {
            // Allow control keys (Backspace, Delete, etc.)
            if (char.IsControl(e.KeyChar))
            {
                return false;
            }

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

        public static void ApplyRegularFont(int size, params Control[] controls)
        {
            foreach (var control in controls)
            {
                control.Font = new System.Drawing.Font("Segoe UI", size, FontStyle.Regular);
            }
        }

        public static void ApplyBoldFont(int size, params Control[] controls)
        {
            foreach (var control in controls)
            {
                control.Font = new System.Drawing.Font("Segoe UI", size, FontStyle.Bold);
            }
        }

        public static void ApplyItalicFont(int size, params Control[] controls)
        {
            foreach (var control in controls)
            {
                control.Font = new System.Drawing.Font("Segoe UI", size, FontStyle.Italic);
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

        // Generic reflection mapping pattern
        public static T MapFromGrid<T>(DataGridView grid, int rowIndex) where T : new()
        {
            var row = grid.Rows[rowIndex];
            var obj = new T();

            foreach (var prop in typeof(T).GetProperties())
            {
                if (!grid.Columns.Contains(prop.Name))
                    continue;

                var value = row.Cells[prop.Name].Value;

                if (value == null || value == DBNull.Value)
                    continue;

                var convertedValue = Convert.ChangeType(value, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

                prop.SetValue(obj, convertedValue);
            }

            return obj;
        }

        public static void ApplyRankingColors(DataGridView grid, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= grid.Rows.Count)
                return;

            var row = grid.Rows[e.RowIndex];

            row.DefaultCellStyle.ForeColor = Color.Black;

            switch (e.RowIndex)
            {
                case 0:
                    row.DefaultCellStyle.BackColor = Color.DarkOrange;
                    break;
                case 1:
                    row.DefaultCellStyle.BackColor = Color.GreenYellow;
                    break;
                case 2:
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                    break;
                default:
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                    break;
            }
        }

    }
}
