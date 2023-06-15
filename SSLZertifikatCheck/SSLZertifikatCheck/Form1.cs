
using SSLZertifikatCheck.i18n;
using System.Globalization;
using System.Drawing;

namespace SSLZertifikatCheck
{
    public partial class Form1 : Form
    {
        private Rectangle recButtonAddUrl;
        private Rectangle recButtonRefresh;
        private Rectangle recButtonSearchForFile;
        private Rectangle recButtonInsertFromFile;
        private Rectangle recLbnError;
        private Rectangle recLbnFileInfo;
        private Rectangle recTxtError;
        private Rectangle recTxtAddUrl;
        private Rectangle recTxtAddFile;
        private Rectangle recdatagridView;
        private Rectangle reclbnPercentage;
        private Rectangle recprogressbar;
        private Rectangle recload;
        private Size recForm;
        public static Form1 Current { get; private set; }
        public Form1()
        {
            if (Current == null)
            {
                Current = this;
            }
            InitializeComponent();
            // Add websites to Datagridview that the user saved from previous session. 
            DatagridHelper.AddValuesToDatagridview(dataGridView);
            // Application 
            Languages();
            txtfile.ReadOnly = true;
            txtError.ReadOnly = true;
            pictureBox1.Enabled = false;
            pictureBox1.Visible = false;
        }
        private async void btnAddUrl_Click(object sender, EventArgs e)
        {
            ProgressBarStatus(0, 1);
            // check internet conncetion before we try to request values from userinput
            
            pictureBox1.Enabled = true;
            // Start loading gif
            ShowWaitingIndicator(true);

            string rawInput = txtUrl.Text.ToLower().Trim();

            TextBox error = txtError;
            btnAddUrl.Enabled = false;
            // Now we check if the Userinput is empty oder null.
            if (!string.IsNullOrEmpty(rawInput))
            {
                var dch = new RowData()
                {
                    RawUrl = this.txtUrl.Text,
                    CorrectedUrl = UrlHelper.ChangedInput(rawInput)
                };
                // Check if we can extract values from userinput

                ConnectionStatus connectionState = await Ssl.PopulateWithSslDataAsync(dch, error);
                string errorMessage = Resources.DefaultErrorMessage;

                if (connectionState == ConnectionStatus.NoIssue || connectionState == ConnectionStatus.CertificateOrNotSecure)
                {
                    if (connectionState == ConnectionStatus.NoIssue)
                    { 
                        dataGridView.Rows.Add(dch.SubjectName, dch.StartDate, dch.ExpirationDate, dch.Days, dch.RawUrl, dch.IsNotValid);
                        SettingsHelper.SaveSettings(dch);
                        ProgressBarStatus(1, 1);
                    }
                    // We add values to datagridview and saves the vaues. Progressbar will be 100% because of only one Input
                    else  
                    { 
                        AddToLog(error, dch.CorrectedUrl, errorMessage, connectionState);
                        dataGridView.Rows.Add(dch.SubjectName, dch.StartDate, dch.ExpirationDate, dch.Days, dch.RawUrl, dch.IsNotValid);
                        SettingsHelper.SaveSettings(dch);
                        ProgressBarStatus(1, 1);
                    }
                }
                else
                { 
                     AddToLog(error , dch.CorrectedUrl, errorMessage,   connectionState);
                }
            }
            Application.DoEvents();
            btnAddUrl.Enabled = true;
            // Stop loading gif
            ShowWaitingIndicator(false);  
        }

        public void ShowWaitingIndicator(bool show)
        {
            if (pictureBox1.InvokeRequired)
            {
                pictureBox1.Invoke(new Action(() => { pictureBox1.Visible = show; }));
            }
            else
            {
                pictureBox1.Visible = show;
            }
        }
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh.Enabled = false;
            lbnpercentage.Text = "0%";
            progressBar1.Value = 0;
            pictureBox1.Enabled = true;
            ShowWaitingIndicator(true);
            // check internet conncetion before we try to request values from Datagrid column or file 
            // We create a list where we will add datagrid values and or file values to one list before making 
            List<string> websitesFromUrlColumn = new List<string>();
            TextBox error = txtError;
            await DatagridHelper.AddWebsitesToListAsync(websitesFromUrlColumn, dataGridView);
            // Now we'll clear Datagridview values,Settings and Log textbox
            DatagridHelper.ClearRowAndSettings(dataGridView, error);
            //We'll add values to Datagridview, Settings and if an error occurs than to the Log textbox
            await DatagridHelper.AddRefreshedWebsitesToDatagridAsync(websitesFromUrlColumn, dataGridView, error);
            ShowWaitingIndicator(false);
            Application.DoEvents();
            btnRefresh.Enabled = true; 
        }
        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DatagridHelper.AddColorToDataGrid(dataGridView);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DatagridHelper.OpenFileDialog(txtfile);
        }
        private void btnInsertFromFile_Click(object sender, EventArgs e)
        {
            btnInsertFromFile.Enabled = false;
            string file = txtfile.Text;
            if (!string.IsNullOrWhiteSpace(file))
            {
                DatagridHelper.AddWebsitesFromFile(file);
                btnRefresh_Click(sender, e);
            }
            Application.DoEvents();
            btnInsertFromFile.Enabled = true;
        }
        private void resizeControl(Rectangle r, Control c)
        {
            float xRatio = (float)(this.Width) / (float)(recForm.Width);
            float yRatio = (float)(this.Height) / (float)(recForm.Height);

            int newX = (int)(r.Location.X * xRatio);
            int newY = (int)(r.Location.Y * yRatio);

            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);

            c.Location = new Point(newX, newY);
            c.Size = new Size(newWidth, newHeight);
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            resizeControl(recButtonRefresh, btnRefresh);
            resizeControl(recTxtError, txtError);
            resizeControl(recdatagridView, dataGridView);
            resizeControl(recLbnError, lbnError);
            resizeControl(reclbnPercentage, lbnpercentage);
            resizeControl(recprogressbar, progressBar1);
            resizeControl(recButtonAddUrl, btnAddUrl);
            resizeControl(recButtonInsertFromFile, btnInsertFromFile);
            resizeControl(recButtonSearchForFile, btnSearch);
            resizeControl(recLbnFileInfo, lbnTextForFile);
            resizeControl(recTxtAddFile, txtfile);
            resizeControl(recTxtAddUrl, txtUrl);
            resizeControl(recload, pictureBox1);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            recForm = this.Size;

            recTxtError = new Rectangle(txtError.Location.X, txtError.Location.Y, txtError.Width, txtError.Height);
            recTxtAddUrl = new Rectangle(txtUrl.Location.X, txtUrl.Location.Y, txtUrl.Width, txtUrl.Height);
            recTxtAddFile = new Rectangle(txtfile.Location.X, txtfile.Location.Y, txtfile.Width, txtfile.Height);

            recdatagridView = new Rectangle(dataGridView.Location.X, dataGridView.Location.Y, dataGridView.Width, dataGridView.Height);

            recLbnError = new Rectangle(lbnError.Location.X, lbnError.Location.Y, lbnError.Width, lbnError.Height);
            recLbnFileInfo = new Rectangle(lbnTextForFile.Location.X, lbnTextForFile.Location.Y, lbnTextForFile.Width, lbnTextForFile.Height);
            reclbnPercentage = new Rectangle(lbnpercentage.Location.X, lbnpercentage.Location.Y, lbnpercentage.Width, lbnpercentage.Height);
            recprogressbar = new Rectangle(progressBar1.Location.X, progressBar1.Location.Y, progressBar1.Width, progressBar1.Height);
            recload = new Rectangle(pictureBox1.Location.X, pictureBox1.Location.Y, pictureBox1.Width, pictureBox1.Height);

            recButtonRefresh = new Rectangle(btnRefresh.Location.X, btnRefresh.Location.Y, btnRefresh.Width, btnRefresh.Height);
            recButtonSearchForFile = new Rectangle(btnSearch.Location.X, btnSearch.Location.Y, btnSearch.Width, btnSearch.Height);
            recButtonAddUrl = new Rectangle(btnAddUrl.Location.X, btnAddUrl.Location.Y, btnAddUrl.Width, btnAddUrl.Height);
            recButtonInsertFromFile = new Rectangle(btnInsertFromFile.Location.X, btnInsertFromFile.Location.Y, btnInsertFromFile.Width, btnInsertFromFile.Height);
        }
        
        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            int indexcount = totalRowcount(dataGridView);
            lbnpercentage.Text = "0%";
            progressBar1.Value = 0;
            foreach (DataGridViewRow row in dataGridView.SelectedRows)
            {   
                int index = row.Index + 1;
                if (indexcount == index)
                {
                    continue;
                }
                try
                {
                    Settings.Default.Name.RemoveAt(index);
                    Settings.Default.Startdate.RemoveAt(index);
                    Settings.Default.Expirationdate.RemoveAt(index);
                    Settings.Default.Days.RemoveAt(index);
                    Settings.Default.RawUrl.RemoveAt(index);
                    Settings.Default.Url.RemoveAt(index);
                    Settings.Default.IsValid.RemoveAt(index);
                    dataGridView.Rows.Remove(row);
                    Settings.Default.Save(); 
                }
                catch
                {

                }
            }
        }
        private void dataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            btnRefresh_Click(sender, e);
        } 
        public static int totalRowcount(DataGridView dt)
        {
            int counter = 0;
            foreach (DataGridViewRow row in dt.Rows)
            {
                counter = dt.Rows.Count;
            }
            return counter;
        }
        public void ProgressBarStatus(int currentnumber, int totalNumber)
        {
            this.progressBar1.Maximum = totalNumber;
            this.progressBar1.Value = currentnumber;

            float currentnumberValue = (float)currentnumber;
            float totalnumberValue = (float)totalNumber;
            float percentage = (currentnumberValue / totalnumberValue) * 100;

            this.lbnpercentage.Refresh();
            int roundednumber = (int)Math.Round(percentage, 0);
            this.lbnpercentage.Text = roundednumber.ToString() + " %";
        }
        public void Languages()
        {
            btnAddUrl.Text = Resources.AddUrl;
            btnInsertFromFile.Text = Resources.AddFile;
            btnSearch.Text = Resources.SearchForFile;
            btnRefresh.Text = Resources.Refresh;
            lbnTextForFile.Text = Resources.LbnTextfileInfo;
            dataGridView.Columns[1].HeaderText = Resources.Startdate;
            dataGridView.Columns[2].HeaderText = Resources.Expirationdate;
            dataGridView.Columns[3].HeaderText = Resources.Days;
        }
        public static void AddToLog(TextBox logTextBox, string dataColumnHelper, string errorMessage, ConnectionStatus status)
        {
            string localDate = DateTime.Now.ToString("HH:mm:ss");
            string errorTyp = "";
            string defaultMessage = $"[ {localDate} ] Error: " + " URL: " + dataColumnHelper.ToUpper() + "  ";
            if (status == ConnectionStatus.HttpError)
            {
                errorTyp = Resources.HttpMessage;
            }
            else if (status == ConnectionStatus.WrongUrl)
            {
                errorTyp = Resources.UnknownHost;
            }
            else if (status == ConnectionStatus.AlreadyExist)
            {
                errorTyp = Resources.AlreadyExist;
            }
            else if (status == ConnectionStatus.CertificateOrNotSecure)
            {
                errorTyp =   Resources.CertNotValidOrSiteIsSecure;
            }
            else
            {
                errorTyp = errorMessage;
            }
            logTextBox.AppendText(defaultMessage + errorTyp + Environment.NewLine);
        }

    }
}
