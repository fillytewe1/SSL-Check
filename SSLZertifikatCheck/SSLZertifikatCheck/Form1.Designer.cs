namespace SSLZertifikatCheck
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnAddUrl = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.NameDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExperationDateDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DaysDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.URLDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isValidDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtError = new System.Windows.Forms.TextBox();
            this.txtfile = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lbnError = new System.Windows.Forms.Label();
            this.btnInsertFromFile = new System.Windows.Forms.Button();
            this.lbnTextForFile = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lbnpercentage = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddUrl
            // 
            resources.ApplyResources(this.btnAddUrl, "btnAddUrl");
            this.btnAddUrl.Name = "btnAddUrl";
            this.btnAddUrl.UseVisualStyleBackColor = true;
            this.btnAddUrl.Click += new System.EventHandler(this.btnAddUrl_Click);
            // 
            // txtUrl
            // 
            resources.ApplyResources(this.txtUrl, "txtUrl");
            this.txtUrl.Name = "txtUrl";
            // 
            // dataGridView
            // 
            resources.ApplyResources(this.dataGridView, "dataGridView");
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameDt,
            this.StartDt,
            this.ExperationDateDt,
            this.DaysDt,
            this.URLDt,
            this.isValidDt});
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 25;
            this.dataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView_CellFormatting);
            this.dataGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView_UserDeletedRow);
            this.dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_KeyDown);
            // 
            // NameDt
            // 
            resources.ApplyResources(this.NameDt, "NameDt");
            this.NameDt.Name = "NameDt";
            // 
            // StartDt
            // 
            resources.ApplyResources(this.StartDt, "StartDt");
            this.StartDt.Name = "StartDt";
            // 
            // ExperationDateDt
            // 
            resources.ApplyResources(this.ExperationDateDt, "ExperationDateDt");
            this.ExperationDateDt.Name = "ExperationDateDt";
            // 
            // DaysDt
            // 
            resources.ApplyResources(this.DaysDt, "DaysDt");
            this.DaysDt.Name = "DaysDt";
            // 
            // URLDt
            // 
            resources.ApplyResources(this.URLDt, "URLDt");
            this.URLDt.Name = "URLDt";
            // 
            // isValidDt
            // 
            resources.ApplyResources(this.isValidDt, "isValidDt");
            this.isValidDt.Name = "isValidDt";
            // 
            // btnRefresh
            // 
            resources.ApplyResources(this.btnRefresh, "btnRefresh");
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtError
            // 
            resources.ApplyResources(this.txtError, "txtError");
            this.txtError.Name = "txtError";
            // 
            // txtfile
            // 
            resources.ApplyResources(this.txtfile, "txtfile");
            this.txtfile.Name = "txtfile";
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lbnError
            // 
            resources.ApplyResources(this.lbnError, "lbnError");
            this.lbnError.Name = "lbnError";
            // 
            // btnInsertFromFile
            // 
            resources.ApplyResources(this.btnInsertFromFile, "btnInsertFromFile");
            this.btnInsertFromFile.Name = "btnInsertFromFile";
            this.btnInsertFromFile.UseVisualStyleBackColor = true;
            this.btnInsertFromFile.Click += new System.EventHandler(this.btnInsertFromFile_Click);
            // 
            // lbnTextForFile
            // 
            resources.ApplyResources(this.lbnTextForFile, "lbnTextForFile");
            this.lbnTextForFile.Name = "lbnTextForFile";
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // lbnpercentage
            // 
            resources.ApplyResources(this.lbnpercentage, "lbnpercentage");
            this.lbnpercentage.Name = "lbnpercentage";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::SSLZertifikatCheck.i18n.Resources.gif_barCircle;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbnpercentage);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lbnTextForFile);
            this.Controls.Add(this.btnInsertFromFile);
            this.Controls.Add(this.lbnError);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtfile);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.btnAddUrl);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnAddUrl;
        private TextBox txtUrl;
        private DataGridView dataGridView;
        private Button btnRefresh;
        private TextBox txtError;
        private TextBox txtfile;
        private Button btnSearch;
        private Label lbnError;
        private Button btnInsertFromFile;
        private Label lbnTextForFile;
        private ProgressBar progressBar1;
        private Label lbnpercentage;
        private DataGridViewTextBoxColumn NameDt;
        private DataGridViewTextBoxColumn StartDt;
        private DataGridViewTextBoxColumn ExperationDateDt;
        private DataGridViewTextBoxColumn DaysDt;
        private DataGridViewTextBoxColumn URLDt;
        private DataGridViewTextBoxColumn isValidDt;
        private PictureBox pictureBox1;
    }
}