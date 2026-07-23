using System;
using System.Drawing;
using System.Windows.Forms;

namespace APC.Utility
{
    public static class FontManagerDashboard
    {
        /// <summary> /////////////////
        /// Dashboard 
        /// </summary> /////////////// 
        /// 
        // Normal sizes
        private const float DashboardNormalOtherFontSize = 9;
        private const float DashboardNormalLabelFontSize = 14;
        private const float DashboardNormalValueFontSize = 20;
        private const float DashboardNormalButtonFontSize = 12;
        private const float LogoFontSize = 40;

        // Maximized sizes
        private const float DashboardMaximizedOtherFontSize = 10;
        private const float DashboardMaximizedLabelFontSize = 18;
        private const float DashboardMaximizedValueFontSize = 24;
        private const float DashboardMaximizedButtonFontSize = 14;

        /// <summary> /////////////////
        /// General 
        /// </summary> /////////////// 
        /// 
        // Normal sizes
        private const float NormalValueFontSize = 20;
        private const float NormalFontSize = 14;
        private const float NormalButtonFontSize = 14;
        private const float NormalStatusValueFontSize = 10;
        private const float NormalStatusFontSize = 9;
        private const float NormalRadioFontSize = 12;

        // Maximized sizes
        private const float MaximizedValueFontSize = 24;
        private const float MaximizedFontSize = 18;
        private const float MaximizedButtonFontSize = 18;
        private const float MaximizedStatusValueFontSize = 12;
        private const float MaximizedStatusFontSize = 11;
        private const float MaximizedRadioFontSize = 14;


        public static void Apply(
            Control parent,
            FormWindowState windowState)
        {
            bool isMaximized =
                windowState == FormWindowState.Maximized;

            float generalButtonFontSize = isMaximized
                ? MaximizedButtonFontSize
                : NormalButtonFontSize;

            float statusValueFontSize = isMaximized
                ? MaximizedStatusValueFontSize
                : NormalStatusValueFontSize;

            float statusFontSize = isMaximized
                ? MaximizedStatusFontSize
                : NormalStatusFontSize;
            
            float normalValueFontSize = isMaximized
                ? MaximizedValueFontSize
                : NormalValueFontSize;
            
            float normalFontSize = isMaximized
                ? MaximizedFontSize
                : NormalFontSize;

            float radioFontSize = isMaximized
                ? MaximizedRadioFontSize
                : NormalRadioFontSize;


            float dashboardOtherFontSize = isMaximized
                ? DashboardMaximizedOtherFontSize
                : DashboardNormalOtherFontSize;

            float logoFontSize = isMaximized
                ? LogoFontSize
                : LogoFontSize;

            float dashboardLabelFontSize = isMaximized
                ? DashboardMaximizedLabelFontSize
                : DashboardNormalLabelFontSize;

            float dashboardValueFontSize = isMaximized
                ? DashboardMaximizedValueFontSize
                : DashboardNormalValueFontSize;

            float dashboardButtonFontSize = isMaximized
                ? DashboardMaximizedButtonFontSize
                : DashboardNormalButtonFontSize;


            ApplyToControls(
                parent,
                dashboardOtherFontSize,
                logoFontSize,
                dashboardLabelFontSize,
                dashboardValueFontSize,
                dashboardButtonFontSize,

                generalButtonFontSize,
                normalFontSize,
                normalValueFontSize,
                statusValueFontSize,
                statusFontSize,
                radioFontSize
                );
        }

        private static void ApplyToControls(
            Control parent,
            float dashboardOtherFontSize,
            float logoFontSize,
            float dashboardLabelFontSize,
            float dashboardValueFontSize,
            float dashboardButtonFontSize,

            float generalButtonFontSize,
            float normalFontSize,
            float normalValueFontSize,
            float statusValueFontSize,
            float statusFontSize,
            float radioFontSize
            )
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Label)
                {
                    if (control.Name.StartsWith(
                        "dashboard",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        SetFont(
                            control,
                            dashboardLabelFontSize,
                            FontStyle.Bold);
                    }
                    else if (control.Name.StartsWith(
                        "logo",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        SetFont(
                            control,
                            logoFontSize,
                            FontStyle.Bold);
                    }
                    else if (control.Name.StartsWith(
                        "dv",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        SetFont(
                            control,
                            dashboardValueFontSize,
                            FontStyle.Bold);
                    }
                    else if (control.Name.StartsWith(
                        "dashboardOther",
                        StringComparison.OrdinalIgnoreCase))                    
                    {
                        SetFont(
                            control,
                            dashboardOtherFontSize,
                            FontStyle.Bold);
                    }



                    else if (control.Name.StartsWith(
                        "label",
                        StringComparison.OrdinalIgnoreCase))                    
                    {
                        SetFont(
                            control,
                            normalFontSize,
                            FontStyle.Regular);
                    }
                    else if (control.Name.StartsWith(
                        "status",
                        StringComparison.OrdinalIgnoreCase))                    
                    {
                        SetFont(
                            control,
                            statusFontSize,
                            FontStyle.Regular);
                    }
                    else if (control.Name.StartsWith(
                        "sv",
                        StringComparison.OrdinalIgnoreCase))                    
                    {
                        SetFont(
                            control,
                            statusValueFontSize,
                            FontStyle.Regular);
                    }
                    else if (control.Name.StartsWith(
                        "nv",
                        StringComparison.OrdinalIgnoreCase))                    
                    {
                        SetFont(
                            control,
                            normalValueFontSize,
                            FontStyle.Regular);
                    }
                }
                else if (control is Button)
                {
                    if (control.Name.StartsWith(
                        "dashboardBtn",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        SetFont(
                            control,
                            dashboardButtonFontSize,
                            FontStyle.Bold);
                    }
                    else if (control.Name.StartsWith(
                        "btn",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        SetFont(
                            control,
                            generalButtonFontSize,
                            FontStyle.Bold);
                    }
                }

                else if (
                    control is TextBox
                    || control is ComboBox
                    || control is DateTimePicker
                    )
                {
                    SetFont(
                            control,
                            normalFontSize,
                            FontStyle.Regular
                            );
                }
                else if (
                    control is RadioButton
                    )
                {
                    SetFont(
                            control,
                            radioFontSize,
                            FontStyle.Bold
                            );
                }

                // Recursively process nested controls
                if (control.HasChildren)
                {
                    ApplyToControls(
                        control,
                        dashboardOtherFontSize,
                        logoFontSize,
                        dashboardLabelFontSize,
                        dashboardValueFontSize,
                        dashboardButtonFontSize,
                        generalButtonFontSize,
                        normalFontSize,
                        normalValueFontSize,
                        statusValueFontSize,
                        statusFontSize,
                        radioFontSize
                        );
                }
            }
        }

        private static void SetFont(
            Control control,
            float fontSize,
            FontStyle fontStyle)
        {
            control.Font = new Font(
                control.Font.FontFamily,
                fontSize,
                fontStyle);
        }
    }
}