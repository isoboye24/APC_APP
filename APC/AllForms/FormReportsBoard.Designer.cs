namespace APC.AllForms
{
    partial class FormReportsBoard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.financialReportPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnUpdateFinReport = new System.Windows.Forms.Button();
            this.btnViewFinReport = new System.Windows.Forms.Button();
            this.btnAddFinReport = new System.Windows.Forms.Button();
            this.btnDeleteFinReport = new System.Windows.Forms.Button();
            this.labelTotalFinReport = new System.Windows.Forms.Label();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtYearFinReport = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panelNoOfChildren = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.labelTotalAmountSpent = new System.Windows.Forms.Label();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.labelTotalAmountRaised = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.labelTotalBalance = new System.Windows.Forms.Label();
            this.dataGridViewFinReport = new System.Windows.Forms.DataGridView();
            this.expenditurePage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddExpReport = new System.Windows.Forms.Button();
            this.btnUpdateExpReport = new System.Windows.Forms.Button();
            this.btnDeleteExpReport = new System.Windows.Forms.Button();
            this.btnViewExpReport = new System.Windows.Forms.Button();
            this.labelTotalExpReport = new System.Windows.Forms.Label();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtYearExpReport = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSearchExpReport = new System.Windows.Forms.Button();
            this.cmbMonthExpReport = new System.Windows.Forms.ComboBox();
            this.btnClearExpReport = new System.Windows.Forms.Button();
            this.dataGridViewExpReport = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.financialReportPage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panelNoOfChildren.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFinReport)).BeginInit();
            this.expenditurePage.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExpReport)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.financialReportPage);
            this.tabControl1.Controls.Add(this.expenditurePage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(957, 600);
            this.tabControl1.TabIndex = 0;
            // 
            // financialReportPage
            // 
            this.financialReportPage.Controls.Add(this.tableLayoutPanel1);
            this.financialReportPage.Location = new System.Drawing.Point(4, 39);
            this.financialReportPage.Name = "financialReportPage";
            this.financialReportPage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.financialReportPage.Size = new System.Drawing.Size(949, 557);
            this.financialReportPage.TabIndex = 0;
            this.financialReportPage.Text = "Financial Report   ";
            this.financialReportPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 98F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel13, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(943, 551);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 9;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21F));
            this.tableLayoutPanel3.Controls.Add(this.btnUpdateFinReport, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnViewFinReport, 5, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnAddFinReport, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnDeleteFinReport, 7, 1);
            this.tableLayoutPanel3.Controls.Add(this.labelTotalFinReport, 8, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(21, 476);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(919, 72);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // btnUpdateFinReport
            // 
            this.btnUpdateFinReport.BackColor = System.Drawing.Color.Indigo;
            this.btnUpdateFinReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpdateFinReport.FlatAppearance.BorderSize = 0;
            this.btnUpdateFinReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateFinReport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateFinReport.ForeColor = System.Drawing.Color.White;
            this.btnUpdateFinReport.Location = new System.Drawing.Point(332, 21);
            this.btnUpdateFinReport.Name = "btnUpdateFinReport";
            this.btnUpdateFinReport.Size = new System.Drawing.Size(113, 30);
            this.btnUpdateFinReport.TabIndex = 3;
            this.btnUpdateFinReport.Text = "Edit";
            this.btnUpdateFinReport.UseVisualStyleBackColor = false;
            this.btnUpdateFinReport.Click += new System.EventHandler(this.btnUpdateFinReport_Click);
            // 
            // btnViewFinReport
            // 
            this.btnViewFinReport.BackColor = System.Drawing.Color.Indigo;
            this.btnViewFinReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnViewFinReport.FlatAppearance.BorderSize = 0;
            this.btnViewFinReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewFinReport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewFinReport.ForeColor = System.Drawing.Color.White;
            this.btnViewFinReport.Location = new System.Drawing.Point(469, 21);
            this.btnViewFinReport.Name = "btnViewFinReport";
            this.btnViewFinReport.Size = new System.Drawing.Size(113, 30);
            this.btnViewFinReport.TabIndex = 3;
            this.btnViewFinReport.Text = "View";
            this.btnViewFinReport.UseVisualStyleBackColor = false;
            this.btnViewFinReport.Click += new System.EventHandler(this.btnViewFinReport_Click);
            // 
            // btnAddFinReport
            // 
            this.btnAddFinReport.BackColor = System.Drawing.Color.Indigo;
            this.btnAddFinReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddFinReport.FlatAppearance.BorderSize = 0;
            this.btnAddFinReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFinReport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddFinReport.ForeColor = System.Drawing.Color.White;
            this.btnAddFinReport.Location = new System.Drawing.Point(195, 21);
            this.btnAddFinReport.Name = "btnAddFinReport";
            this.btnAddFinReport.Size = new System.Drawing.Size(113, 30);
            this.btnAddFinReport.TabIndex = 3;
            this.btnAddFinReport.Text = "Add";
            this.btnAddFinReport.UseVisualStyleBackColor = false;
            this.btnAddFinReport.Click += new System.EventHandler(this.btnAddFinReport_Click);
            // 
            // btnDeleteFinReport
            // 
            this.btnDeleteFinReport.BackColor = System.Drawing.Color.Indigo;
            this.btnDeleteFinReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteFinReport.FlatAppearance.BorderSize = 0;
            this.btnDeleteFinReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteFinReport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteFinReport.ForeColor = System.Drawing.Color.White;
            this.btnDeleteFinReport.Location = new System.Drawing.Point(606, 21);
            this.btnDeleteFinReport.Name = "btnDeleteFinReport";
            this.btnDeleteFinReport.Size = new System.Drawing.Size(113, 30);
            this.btnDeleteFinReport.TabIndex = 3;
            this.btnDeleteFinReport.Text = "Delete";
            this.btnDeleteFinReport.UseVisualStyleBackColor = false;
            this.btnDeleteFinReport.Click += new System.EventHandler(this.btnDeleteFinReport_Click);
            // 
            // labelTotalFinReport
            // 
            this.labelTotalFinReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTotalFinReport.AutoSize = true;
            this.labelTotalFinReport.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalFinReport.Location = new System.Drawing.Point(876, 37);
            this.labelTotalFinReport.Name = "labelTotalFinReport";
            this.labelTotalFinReport.Size = new System.Drawing.Size(40, 17);
            this.labelTotalFinReport.TabIndex = 4;
            this.labelTotalFinReport.Text = "Total:";
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 4;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39F));
            this.tableLayoutPanel13.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel13.Controls.Add(this.txtYearFinReport, 2, 0);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(21, 13);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 1;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(919, 34);
            this.tableLayoutPanel13.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(376, 5);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 21);
            this.label5.TabIndex = 1;
            this.label5.Text = "Year";
            // 
            // txtYearFinReport
            // 
            this.txtYearFinReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtYearFinReport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYearFinReport.Location = new System.Drawing.Point(425, 3);
            this.txtYearFinReport.Name = "txtYearFinReport";
            this.txtYearFinReport.Size = new System.Drawing.Size(131, 29);
            this.txtYearFinReport.TabIndex = 0;
            this.txtYearFinReport.TextChanged += new System.EventHandler(this.txtYearFinReport_TextChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.dataGridViewFinReport, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(21, 53);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(919, 407);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.panelNoOfChildren, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel10, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel7, 1, 4);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(646, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 5;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(270, 401);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // panelNoOfChildren
            // 
            this.panelNoOfChildren.BackColor = System.Drawing.Color.Crimson;
            this.panelNoOfChildren.ColumnCount = 1;
            this.panelNoOfChildren.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelNoOfChildren.Controls.Add(this.label6, 0, 0);
            this.panelNoOfChildren.Controls.Add(this.labelTotalAmountSpent, 0, 1);
            this.panelNoOfChildren.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNoOfChildren.Location = new System.Drawing.Point(8, 143);
            this.panelNoOfChildren.Name = "panelNoOfChildren";
            this.panelNoOfChildren.RowCount = 2;
            this.panelNoOfChildren.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelNoOfChildren.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelNoOfChildren.Size = new System.Drawing.Size(259, 114);
            this.panelNoOfChildren.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(3, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(253, 30);
            this.label6.TabIndex = 0;
            this.label6.Text = "Total Amount Spent";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelTotalAmountSpent
            // 
            this.labelTotalAmountSpent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTotalAmountSpent.AutoSize = true;
            this.labelTotalAmountSpent.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalAmountSpent.ForeColor = System.Drawing.Color.White;
            this.labelTotalAmountSpent.Location = new System.Drawing.Point(213, 64);
            this.labelTotalAmountSpent.Name = "labelTotalAmountSpent";
            this.labelTotalAmountSpent.Size = new System.Drawing.Size(43, 50);
            this.labelTotalAmountSpent.TabIndex = 0;
            this.labelTotalAmountSpent.Text = "0";
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.BackColor = System.Drawing.Color.DarkOrange;
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Controls.Add(this.labelTotalAmountRaised, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(8, 3);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 2;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(259, 114);
            this.tableLayoutPanel10.TabIndex = 13;
            // 
            // labelTotalAmountRaised
            // 
            this.labelTotalAmountRaised.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTotalAmountRaised.AutoSize = true;
            this.labelTotalAmountRaised.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalAmountRaised.ForeColor = System.Drawing.Color.White;
            this.labelTotalAmountRaised.Location = new System.Drawing.Point(213, 64);
            this.labelTotalAmountRaised.Name = "labelTotalAmountRaised";
            this.labelTotalAmountRaised.Size = new System.Drawing.Size(43, 50);
            this.labelTotalAmountRaised.TabIndex = 0;
            this.labelTotalAmountRaised.Text = "0";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(253, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "Total Amount Raised";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.BackColor = System.Drawing.Color.Green;
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.labelTotalBalance, 0, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(8, 283);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(259, 115);
            this.tableLayoutPanel7.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(253, 30);
            this.label3.TabIndex = 0;
            this.label3.Text = "Total Balance";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelTotalBalance
            // 
            this.labelTotalBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTotalBalance.AutoSize = true;
            this.labelTotalBalance.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalBalance.ForeColor = System.Drawing.Color.White;
            this.labelTotalBalance.Location = new System.Drawing.Point(213, 65);
            this.labelTotalBalance.Name = "labelTotalBalance";
            this.labelTotalBalance.Size = new System.Drawing.Size(43, 50);
            this.labelTotalBalance.TabIndex = 0;
            this.labelTotalBalance.Text = "0";
            // 
            // dataGridViewFinReport
            // 
            this.dataGridViewFinReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFinReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFinReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewFinReport.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewFinReport.Name = "dataGridViewFinReport";
            this.dataGridViewFinReport.RowHeadersWidth = 51;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewFinReport.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewFinReport.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewFinReport.RowTemplate.Height = 40;
            this.dataGridViewFinReport.Size = new System.Drawing.Size(637, 401);
            this.dataGridViewFinReport.TabIndex = 2;
            this.dataGridViewFinReport.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFinReport_RowEnter);
            // 
            // expenditurePage
            // 
            this.expenditurePage.Controls.Add(this.tableLayoutPanel5);
            this.expenditurePage.Location = new System.Drawing.Point(4, 39);
            this.expenditurePage.Name = "expenditurePage";
            this.expenditurePage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.expenditurePage.Size = new System.Drawing.Size(949, 557);
            this.expenditurePage.TabIndex = 1;
            this.expenditurePage.Text = "Expenditures";
            this.expenditurePage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 96F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 1, 4);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel8, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.dataGridViewExpReport, 1, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 6;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(943, 551);
            this.tableLayoutPanel5.TabIndex = 6;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 9;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21F));
            this.tableLayoutPanel6.Controls.Add(this.btnAddExpReport, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.btnUpdateExpReport, 3, 1);
            this.tableLayoutPanel6.Controls.Add(this.btnDeleteExpReport, 7, 1);
            this.tableLayoutPanel6.Controls.Add(this.btnViewExpReport, 5, 1);
            this.tableLayoutPanel6.Controls.Add(this.labelTotalExpReport, 8, 2);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(21, 468);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(899, 69);
            this.tableLayoutPanel6.TabIndex = 2;
            // 
            // btnAddExpReport
            // 
            this.btnAddExpReport.BackColor = System.Drawing.Color.Indigo;
            this.btnAddExpReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddExpReport.FlatAppearance.BorderSize = 0;
            this.btnAddExpReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddExpReport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddExpReport.ForeColor = System.Drawing.Color.White;
            this.btnAddExpReport.Location = new System.Drawing.Point(191, 20);
            this.btnAddExpReport.Name = "btnAddExpReport";
            this.btnAddExpReport.Size = new System.Drawing.Size(110, 28);
            this.btnAddExpReport.TabIndex = 0;
            this.btnAddExpReport.Text = "Add";
            this.btnAddExpReport.UseVisualStyleBackColor = false;
            this.btnAddExpReport.Click += new System.EventHandler(this.btnAddExpReport_Click);
            // 
            // btnUpdateExpReport
            // 
            this.btnUpdateExpReport.BackColor = System.Drawing.Color.Indigo;
            this.btnUpdateExpReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpdateExpReport.FlatAppearance.BorderSize = 0;
            this.btnUpdateExpReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateExpReport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateExpReport.ForeColor = System.Drawing.Color.White;
            this.btnUpdateExpReport.Location = new System.Drawing.Point(324, 20);
            this.btnUpdateExpReport.Name = "btnUpdateExpReport";
            this.btnUpdateExpReport.Size = new System.Drawing.Size(110, 28);
            this.btnUpdateExpReport.TabIndex = 1;
            this.btnUpdateExpReport.Text = "Edit";
            this.btnUpdateExpReport.UseVisualStyleBackColor = false;
            this.btnUpdateExpReport.Click += new System.EventHandler(this.btnUpdateExpReport_Click);
            // 
            // btnDeleteExpReport
            // 
            this.btnDeleteExpReport.BackColor = System.Drawing.Color.Indigo;
            this.btnDeleteExpReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteExpReport.FlatAppearance.BorderSize = 0;
            this.btnDeleteExpReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteExpReport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteExpReport.ForeColor = System.Drawing.Color.White;
            this.btnDeleteExpReport.Location = new System.Drawing.Point(590, 20);
            this.btnDeleteExpReport.Name = "btnDeleteExpReport";
            this.btnDeleteExpReport.Size = new System.Drawing.Size(110, 28);
            this.btnDeleteExpReport.TabIndex = 2;
            this.btnDeleteExpReport.Text = "Delete";
            this.btnDeleteExpReport.UseVisualStyleBackColor = false;
            this.btnDeleteExpReport.Click += new System.EventHandler(this.btnDeleteExpReport_Click);
            // 
            // btnViewExpReport
            // 
            this.btnViewExpReport.BackColor = System.Drawing.Color.Indigo;
            this.btnViewExpReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnViewExpReport.FlatAppearance.BorderSize = 0;
            this.btnViewExpReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewExpReport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewExpReport.ForeColor = System.Drawing.Color.White;
            this.btnViewExpReport.Location = new System.Drawing.Point(457, 20);
            this.btnViewExpReport.Name = "btnViewExpReport";
            this.btnViewExpReport.Size = new System.Drawing.Size(110, 28);
            this.btnViewExpReport.TabIndex = 3;
            this.btnViewExpReport.Text = "View";
            this.btnViewExpReport.UseVisualStyleBackColor = false;
            this.btnViewExpReport.Click += new System.EventHandler(this.btnViewExpReport_Click);
            // 
            // labelTotalExpReport
            // 
            this.labelTotalExpReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTotalExpReport.AutoSize = true;
            this.labelTotalExpReport.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalExpReport.Location = new System.Drawing.Point(856, 51);
            this.labelTotalExpReport.Name = "labelTotalExpReport";
            this.labelTotalExpReport.Size = new System.Drawing.Size(40, 17);
            this.labelTotalExpReport.TabIndex = 4;
            this.labelTotalExpReport.Text = "Total:";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 10;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel8.Controls.Add(this.label1, 4, 0);
            this.tableLayoutPanel8.Controls.Add(this.txtYearExpReport, 2, 0);
            this.tableLayoutPanel8.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.btnSearchExpReport, 6, 0);
            this.tableLayoutPanel8.Controls.Add(this.cmbMonthExpReport, 5, 0);
            this.tableLayoutPanel8.Controls.Add(this.btnClearExpReport, 8, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(21, 13);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(899, 44);
            this.tableLayoutPanel8.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(366, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Month";
            // 
            // txtYearExpReport
            // 
            this.txtYearExpReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtYearExpReport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYearExpReport.Location = new System.Drawing.Point(193, 3);
            this.txtYearExpReport.Name = "txtYearExpReport";
            this.txtYearExpReport.Size = new System.Drawing.Size(129, 29);
            this.txtYearExpReport.TabIndex = 0;
            this.txtYearExpReport.TextChanged += new System.EventHandler(this.txtYearExpReport_TextChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(144, 5);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 21);
            this.label4.TabIndex = 1;
            this.label4.Text = "Year";
            // 
            // btnSearchExpReport
            // 
            this.btnSearchExpReport.BackColor = System.Drawing.Color.DarkOrange;
            this.btnSearchExpReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearchExpReport.FlatAppearance.BorderSize = 0;
            this.btnSearchExpReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchExpReport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchExpReport.ForeColor = System.Drawing.Color.Black;
            this.btnSearchExpReport.Location = new System.Drawing.Point(568, 3);
            this.btnSearchExpReport.Name = "btnSearchExpReport";
            this.btnSearchExpReport.Size = new System.Drawing.Size(114, 38);
            this.btnSearchExpReport.TabIndex = 3;
            this.btnSearchExpReport.Text = "Search";
            this.btnSearchExpReport.UseVisualStyleBackColor = false;
            this.btnSearchExpReport.Click += new System.EventHandler(this.btnSearchExpReport_Click);
            // 
            // cmbMonthExpReport
            // 
            this.cmbMonthExpReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbMonthExpReport.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMonthExpReport.FormattingEnabled = true;
            this.cmbMonthExpReport.Location = new System.Drawing.Point(433, 3);
            this.cmbMonthExpReport.Name = "cmbMonthExpReport";
            this.cmbMonthExpReport.Size = new System.Drawing.Size(129, 29);
            this.cmbMonthExpReport.TabIndex = 2;
            // 
            // btnClearExpReport
            // 
            this.btnClearExpReport.BackColor = System.Drawing.Color.IndianRed;
            this.btnClearExpReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearExpReport.FlatAppearance.BorderSize = 0;
            this.btnClearExpReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearExpReport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearExpReport.ForeColor = System.Drawing.Color.Black;
            this.btnClearExpReport.Location = new System.Drawing.Point(690, 3);
            this.btnClearExpReport.Name = "btnClearExpReport";
            this.btnClearExpReport.Size = new System.Drawing.Size(114, 38);
            this.btnClearExpReport.TabIndex = 3;
            this.btnClearExpReport.Text = "Clear";
            this.btnClearExpReport.UseVisualStyleBackColor = false;
            this.btnClearExpReport.Click += new System.EventHandler(this.btnClearExpReport_Click);
            // 
            // dataGridViewExpReport
            // 
            this.dataGridViewExpReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewExpReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExpReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewExpReport.Location = new System.Drawing.Point(21, 63);
            this.dataGridViewExpReport.Name = "dataGridViewExpReport";
            this.dataGridViewExpReport.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewExpReport.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewExpReport.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewExpReport.RowTemplate.Height = 40;
            this.dataGridViewExpReport.Size = new System.Drawing.Size(899, 389);
            this.dataGridViewExpReport.TabIndex = 4;
            this.dataGridViewExpReport.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewExpReport_RowEnter);
            // 
            // FormReportsBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 600);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormReportsBoard";
            this.Text = "Reports";
            this.Load += new System.EventHandler(this.FormReportsBoard_Load);
            this.tabControl1.ResumeLayout(false);
            this.financialReportPage.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panelNoOfChildren.ResumeLayout(false);
            this.panelNoOfChildren.PerformLayout();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFinReport)).EndInit();
            this.expenditurePage.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExpReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage financialReportPage;
        private System.Windows.Forms.TabPage expenditurePage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnUpdateFinReport;
        private System.Windows.Forms.Button btnViewFinReport;
        private System.Windows.Forms.Button btnAddFinReport;
        private System.Windows.Forms.Button btnDeleteFinReport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtYearFinReport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel panelNoOfChildren;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelTotalAmountSpent;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.Label labelTotalAmountRaised;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelTotalBalance;
        private System.Windows.Forms.DataGridView dataGridViewFinReport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button btnAddExpReport;
        private System.Windows.Forms.Button btnUpdateExpReport;
        private System.Windows.Forms.Button btnDeleteExpReport;
        private System.Windows.Forms.Button btnViewExpReport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtYearExpReport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSearchExpReport;
        private System.Windows.Forms.ComboBox cmbMonthExpReport;
        private System.Windows.Forms.Button btnClearExpReport;
        private System.Windows.Forms.DataGridView dataGridViewExpReport;
        private System.Windows.Forms.Label labelTotalFinReport;
        private System.Windows.Forms.Label labelTotalExpReport;
    }
}