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
                    // Resize text-based controls (added RadioButton + CheckBox)
                    if (c is Label || c is Button || c is TextBox || c is ComboBox ||
                        c is RadioButton || c is CheckBox)
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

                    // Resize TableLayoutPanel
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
                if (grid == null || grid.Columns.Count == 0)
                    return; // Nothing to resize yet

                // Create a new scaled font
                float newFontSize = grid.Font.Size * resizeFactor;
                var newFont = new Font(grid.Font.FontFamily, newFontSize, grid.Font.Style);

                // Apply to all parts of the grid
                grid.Font = newFont;
                grid.DefaultCellStyle.Font = newFont;
                grid.ColumnHeadersDefaultCellStyle.Font = newFont;
                grid.RowHeadersDefaultCellStyle.Font = newFont;

                // Resize column widths
                foreach (DataGridViewColumn col in grid.Columns)
                {
                    col.Width = Math.Max(20, (int)(col.Width * resizeFactor)); // Minimum width
                }

                // Resize row heights
                foreach (DataGridViewRow row in grid.Rows)
                {
                    try
                    {
                        row.Height = Math.Max(18, (int)(row.Height * resizeFactor));
                    }
                    catch { /* some virtual rows may throw */ }
                }

                // Optional: auto resize to fit content
                grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                grid.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
            }
            catch (Exception ex)
            {
                // Log or safely ignore — prevents crash during form load
                Console.WriteLine("ResizeDataGridView failed: " + ex.Message);
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

        // 🟦 New: Smooth animation across all forms
        public static void SmoothResizeAll(float targetFontSize, float targetScale, int durationMs = 250)
        {
            // Capture the current values at start
            float startFontSize = CurrentFontSize;
            float startScale = CurrentScale;

            int steps = 10;
            int interval = durationMs / steps;
            int currentStep = 0;

            Timer timer = new Timer { Interval = interval };
            timer.Tick += (s, e) =>
            {
                currentStep++;
                float progress = (float)currentStep / steps;
                float eased = (float)Math.Sin(progress * Math.PI / 2); // smooth easing

                // Interpolate between start and target
                float fontSize = startFontSize + (targetFontSize - startFontSize) * eased;
                float scale = startScale + (targetScale - startScale) * eased;

                // Apply resize using absolute values, not cumulative scaling
                foreach (Form form in Application.OpenForms)
                    ResizeTaggedControls(form, fontSize, scale);

                if (currentStep >= steps)
                {
                    timer.Stop();
                    timer.Dispose();

                    // Save new global zoom state
                    CurrentFontSize = targetFontSize;
                    CurrentScale = targetScale;

                    // Refresh all DataGridViews once
                    foreach (Form form in Application.OpenForms)
                        RefreshDataGrids(form);
                }
            };

            timer.Start();
        }

        public static float CurrentFontSize { get; private set; } = 14f;
        public static float CurrentScale { get; private set; } = 1.0f;

        private static void RefreshDataGrids(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is DataGridView grid)
                {
                    grid.AutoResizeColumnHeadersHeight();
                    grid.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
                    grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    grid.Refresh();
                }

                if (c.HasChildren)
                    RefreshDataGrids(c);
            }
        }
    }
}
