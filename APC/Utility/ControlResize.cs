using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace APC.Utility
{
    public static class ControlResize
    {
        public static void ResizeTaggedControls(Control parent, float newFontSize, float resizeFactor = 1.1f)
        {
            foreach (Control c in parent.Controls)
            {
                if (c.Tag != null && c.Tag.ToString().Equals("resizable", StringComparison.OrdinalIgnoreCase))
                {
                    // Resize text-based controls
                    if (c is Label || c is Button || c is TextBox || c is ComboBox)
                    {
                        ResizeFont(c, newFontSize);
                        ResizeSize(c, resizeFactor);
                    }

                    // Resize FontAwesome IconButton
                    else if (c is IconButton iconButton)
                    {
                        ResizeFont(iconButton, newFontSize);
                        iconButton.IconSize = (int)(iconButton.IconSize * resizeFactor);
                        ResizeSize(iconButton, resizeFactor);
                    }

                    // Resize FontAwesome IconPictureBox
                    else if (c is IconPictureBox iconPicture)
                    {
                        iconPicture.IconSize = (int)(iconPicture.IconSize * resizeFactor);
                        ResizeSize(iconPicture, resizeFactor);
                    }

                    // Resize DataGridView (columns + rows)
                    else if (c is DataGridView grid)
                    {
                        ResizeFont(grid, newFontSize);
                        ResizeDataGridView(grid, resizeFactor);
                    }

                    // Resize container controls (Panel, GroupBox, FlowLayoutPanel)
                    else if (c is Panel || c is GroupBox || c is FlowLayoutPanel)
                    {
                        ResizeSize(c, resizeFactor);
                    }

                    // Resize TableLayoutPanel — important part
                    else if (c is TableLayoutPanel table)
                    {
                        ResizeTableLayoutPanel(table, resizeFactor);
                    }
                }

                // Recurse into children
                if (c.HasChildren)
                    ResizeTaggedControls(c, newFontSize, resizeFactor);
            }
        }

        private static void ResizeFont(Control control, float newFontSize)
        {
            try
            {
                control.Font = new Font("Segoe UI", newFontSize, control.Font.Style);
            }
            catch { }
        }

        private static void ResizeSize(Control control, float resizeFactor)
        {
            control.Width = (int)(control.Width * resizeFactor);
            control.Height = (int)(control.Height * resizeFactor);
        }

        private static void ResizeDataGridView(DataGridView grid, float resizeFactor)
        {
            try
            {
                // Update fonts for all key sections
                var newFont = new Font("Segoe UI", grid.Font.Size * resizeFactor, grid.Font.Style);
                grid.Font = newFont;
                grid.DefaultCellStyle.Font = newFont;
                grid.ColumnHeadersDefaultCellStyle.Font = newFont;
                grid.RowHeadersDefaultCellStyle.Font = newFont;

                // Resize column widths
                foreach (DataGridViewColumn col in grid.Columns)
                {
                    col.Width = (int)(col.Width * resizeFactor);
                }

                // Resize row heights
                foreach (DataGridViewRow row in grid.Rows)
                {
                    row.Height = (int)(row.Height * resizeFactor);
                }

                // Optionally auto-adjust layout to prevent text clipping
                grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                grid.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
            }
            catch
            {
                // Ignore grids that are uninitialized
            }
        }


        // Resize TableLayoutPanel Rows and Columns
        private static void ResizeTableLayoutPanel(TableLayoutPanel table, float resizeFactor)
        {
            foreach (RowStyle row in table.RowStyles)
            {
                if (row.SizeType == SizeType.Absolute)
                    row.Height = row.Height * resizeFactor;
            }

            foreach (ColumnStyle col in table.ColumnStyles)
            {
                if (col.SizeType == SizeType.Absolute)
                    col.Width = col.Width * resizeFactor;
            }

            // Also resize the table itself
            ResizeSize(table, resizeFactor);
        }

        public static void ResizeAllOpenForms(float newFontSize, float resizeFactor = 1.1f)
        {
            foreach (Form form in Application.OpenForms)
                ResizeTaggedControls(form, newFontSize, resizeFactor);
        }
    }
}
