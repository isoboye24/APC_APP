namespace APC.AllForms
{
    partial class FormDocumentList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabDocumentPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDocNameDocument = new System.Windows.Forms.TextBox();
            this.txtDocTypeDocument = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddDocument = new System.Windows.Forms.Button();
            this.btnUpdateDocument = new System.Windows.Forms.Button();
            this.btnDeleteDocument = new System.Windows.Forms.Button();
            this.btnViewDocument = new System.Windows.Forms.Button();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.labelDocCount = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbYearDocument = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearchDocument = new System.Windows.Forms.Button();
            this.btnClearDocument = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbMonthDocument = new System.Windows.Forms.ComboBox();
            this.tabReceiptPage = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabDocumentPage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabDocumentPage);
            this.tabControl1.Controls.Add(this.tabReceiptPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1276, 738);
            this.tabControl1.TabIndex = 0;
            // 
            // tabDocumentPage
            // 
            this.tabDocumentPage.Controls.Add(this.tableLayoutPanel1);
            this.tabDocumentPage.Location = new System.Drawing.Point(4, 40);
            this.tabDocumentPage.Name = "tabDocumentPage";
            this.tabDocumentPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabDocumentPage.Size = new System.Drawing.Size(1268, 694);
            this.tabDocumentPage.TabIndex = 0;
            this.tabDocumentPage.Text = "Documents    ";
            this.tabDocumentPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 96F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1262, 688);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtDocNameDocument, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtDocTypeDocument, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(29, 15);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1203, 41);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 28);
            this.label2.TabIndex = 0;
            this.label2.Text = "Document Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDocNameDocument
            // 
            this.txtDocNameDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDocNameDocument.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocNameDocument.Location = new System.Drawing.Point(217, 4);
            this.txtDocNameDocument.Margin = new System.Windows.Forms.Padding(4);
            this.txtDocNameDocument.Name = "txtDocNameDocument";
            this.txtDocNameDocument.Size = new System.Drawing.Size(526, 32);
            this.txtDocNameDocument.TabIndex = 2;
            this.txtDocNameDocument.TextChanged += new System.EventHandler(this.txtDocNameDocument_TextChanged);
            // 
            // txtDocTypeDocument
            // 
            this.txtDocTypeDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDocTypeDocument.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocTypeDocument.Location = new System.Drawing.Point(977, 4);
            this.txtDocTypeDocument.Margin = new System.Windows.Forms.Padding(4);
            this.txtDocTypeDocument.Name = "txtDocTypeDocument";
            this.txtDocTypeDocument.Size = new System.Drawing.Size(222, 32);
            this.txtDocTypeDocument.TabIndex = 2;
            this.txtDocTypeDocument.TextChanged += new System.EventHandler(this.txtDocTypeDocument_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(760, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Document Type";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.tableLayoutPanel3.Controls.Add(this.btnAddDocument, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnUpdateDocument, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnDeleteDocument, 7, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnViewDocument, 5, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel9, 8, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(29, 577);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1203, 107);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // btnAddDocument
            // 
            this.btnAddDocument.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnAddDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddDocument.FlatAppearance.BorderSize = 0;
            this.btnAddDocument.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDocument.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddDocument.ForeColor = System.Drawing.Color.White;
            this.btnAddDocument.Location = new System.Drawing.Point(256, 9);
            this.btnAddDocument.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddDocument.Name = "btnAddDocument";
            this.btnAddDocument.Size = new System.Drawing.Size(148, 45);
            this.btnAddDocument.TabIndex = 0;
            this.btnAddDocument.Text = "Add";
            this.btnAddDocument.UseVisualStyleBackColor = false;
            this.btnAddDocument.Click += new System.EventHandler(this.btnAddDocument_Click);
            // 
            // btnUpdateDocument
            // 
            this.btnUpdateDocument.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnUpdateDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpdateDocument.FlatAppearance.BorderSize = 0;
            this.btnUpdateDocument.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateDocument.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateDocument.ForeColor = System.Drawing.Color.White;
            this.btnUpdateDocument.Location = new System.Drawing.Point(436, 9);
            this.btnUpdateDocument.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdateDocument.Name = "btnUpdateDocument";
            this.btnUpdateDocument.Size = new System.Drawing.Size(148, 45);
            this.btnUpdateDocument.TabIndex = 1;
            this.btnUpdateDocument.Text = "Edit";
            this.btnUpdateDocument.UseVisualStyleBackColor = false;
            this.btnUpdateDocument.Click += new System.EventHandler(this.btnUpdateDocument_Click);
            // 
            // btnDeleteDocument
            // 
            this.btnDeleteDocument.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnDeleteDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteDocument.FlatAppearance.BorderSize = 0;
            this.btnDeleteDocument.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteDocument.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteDocument.ForeColor = System.Drawing.Color.White;
            this.btnDeleteDocument.Location = new System.Drawing.Point(796, 9);
            this.btnDeleteDocument.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteDocument.Name = "btnDeleteDocument";
            this.btnDeleteDocument.Size = new System.Drawing.Size(148, 45);
            this.btnDeleteDocument.TabIndex = 2;
            this.btnDeleteDocument.Text = "Delete";
            this.btnDeleteDocument.UseVisualStyleBackColor = false;
            this.btnDeleteDocument.Click += new System.EventHandler(this.btnDeleteDocument_Click);
            // 
            // btnViewDocument
            // 
            this.btnViewDocument.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnViewDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnViewDocument.FlatAppearance.BorderSize = 0;
            this.btnViewDocument.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewDocument.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewDocument.ForeColor = System.Drawing.Color.White;
            this.btnViewDocument.Location = new System.Drawing.Point(616, 9);
            this.btnViewDocument.Margin = new System.Windows.Forms.Padding(4);
            this.btnViewDocument.Name = "btnViewDocument";
            this.btnViewDocument.Size = new System.Drawing.Size(148, 45);
            this.btnViewDocument.TabIndex = 3;
            this.btnViewDocument.Text = "View";
            this.btnViewDocument.UseVisualStyleBackColor = false;
            this.btnViewDocument.Click += new System.EventHandler(this.btnViewDocument_Click);
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.Controls.Add(this.labelDocCount, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(952, 62);
            this.tableLayoutPanel9.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(247, 41);
            this.tableLayoutPanel9.TabIndex = 5;
            // 
            // labelDocCount
            // 
            this.labelDocCount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelDocCount.AutoSize = true;
            this.labelDocCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDocCount.Location = new System.Drawing.Point(224, 9);
            this.labelDocCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDocCount.Name = "labelDocCount";
            this.labelDocCount.Size = new System.Drawing.Size(19, 23);
            this.labelDocCount.TabIndex = 0;
            this.labelDocCount.Text = "0";
            this.labelDocCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(29, 132);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(1203, 437);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting_1);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 10;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 7F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 4F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.Controls.Add(this.cmbYearDocument, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.btnSearchDocument, 6, 0);
            this.tableLayoutPanel6.Controls.Add(this.btnClearDocument, 8, 0);
            this.tableLayoutPanel6.Controls.Add(this.label4, 3, 0);
            this.tableLayoutPanel6.Controls.Add(this.cmbMonthDocument, 4, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(29, 70);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1203, 54);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // cmbYearDocument
            // 
            this.cmbYearDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbYearDocument.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbYearDocument.FormattingEnabled = true;
            this.cmbYearDocument.Location = new System.Drawing.Point(111, 4);
            this.cmbYearDocument.Margin = new System.Windows.Forms.Padding(4);
            this.cmbYearDocument.Name = "cmbYearDocument";
            this.cmbYearDocument.Size = new System.Drawing.Size(182, 36);
            this.cmbYearDocument.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 13);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 28);
            this.label3.TabIndex = 0;
            this.label3.Text = "Year";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSearchDocument
            // 
            this.btnSearchDocument.BackColor = System.Drawing.Color.DarkOrange;
            this.btnSearchDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearchDocument.FlatAppearance.BorderSize = 0;
            this.btnSearchDocument.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchDocument.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchDocument.ForeColor = System.Drawing.Color.Black;
            this.btnSearchDocument.Location = new System.Drawing.Point(679, 4);
            this.btnSearchDocument.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearchDocument.Name = "btnSearchDocument";
            this.btnSearchDocument.Size = new System.Drawing.Size(134, 46);
            this.btnSearchDocument.TabIndex = 0;
            this.btnSearchDocument.Text = "Search";
            this.btnSearchDocument.UseVisualStyleBackColor = false;
            this.btnSearchDocument.Click += new System.EventHandler(this.btnSearchDocument_Click);
            // 
            // btnClearDocument
            // 
            this.btnClearDocument.BackColor = System.Drawing.Color.Crimson;
            this.btnClearDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearDocument.FlatAppearance.BorderSize = 0;
            this.btnClearDocument.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDocument.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearDocument.ForeColor = System.Drawing.Color.Black;
            this.btnClearDocument.Location = new System.Drawing.Point(825, 4);
            this.btnClearDocument.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearDocument.Name = "btnClearDocument";
            this.btnClearDocument.Size = new System.Drawing.Size(134, 46);
            this.btnClearDocument.TabIndex = 0;
            this.btnClearDocument.Text = "Clear";
            this.btnClearDocument.UseVisualStyleBackColor = false;
            this.btnClearDocument.Click += new System.EventHandler(this.btnClearDocument_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(324, 13);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 28);
            this.label4.TabIndex = 0;
            this.label4.Text = "Month";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbMonthDocument
            // 
            this.cmbMonthDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbMonthDocument.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMonthDocument.FormattingEnabled = true;
            this.cmbMonthDocument.Location = new System.Drawing.Point(435, 4);
            this.cmbMonthDocument.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMonthDocument.Name = "cmbMonthDocument";
            this.cmbMonthDocument.Size = new System.Drawing.Size(229, 36);
            this.cmbMonthDocument.TabIndex = 3;
            // 
            // tabReceiptPage
            // 
            this.tabReceiptPage.Location = new System.Drawing.Point(4, 40);
            this.tabReceiptPage.Name = "tabReceiptPage";
            this.tabReceiptPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabReceiptPage.Size = new System.Drawing.Size(1268, 694);
            this.tabReceiptPage.TabIndex = 1;
            this.tabReceiptPage.Text = "Receipts    ";
            this.tabReceiptPage.UseVisualStyleBackColor = true;
            // 
            // FormDocumentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 738);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1291, 776);
            this.Name = "FormDocumentList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Documents";
            this.Load += new System.EventHandler(this.FormDocumentList_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabDocumentPage.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabDocumentPage;
        private System.Windows.Forms.TabPage tabReceiptPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDocNameDocument;
        private System.Windows.Forms.TextBox txtDocTypeDocument;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnAddDocument;
        private System.Windows.Forms.Button btnUpdateDocument;
        private System.Windows.Forms.Button btnDeleteDocument;
        private System.Windows.Forms.Button btnViewDocument;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Label labelDocCount;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearchDocument;
        private System.Windows.Forms.Button btnClearDocument;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbMonthDocument;
        private System.Windows.Forms.ComboBox cmbYearDocument;
    }
}