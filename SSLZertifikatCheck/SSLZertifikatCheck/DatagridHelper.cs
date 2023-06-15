using SSLZertifikatCheck.i18n;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSLZertifikatCheck
{
    internal class DatagridHelper
    {

        static List<string> urlFromUserFile = new List<string>();
        public async static Task AddWebsitesToListAsync(List<string> websitesFromUrlColumn, DataGridView dataGridView)
        {
            List<Task> ts = new List<Task>();
            string website = string.Empty;

            // add Websites from URL Datgridcolumns
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // Websites are in the fifth column
                website = row.Cells[4].Value?.ToString();
                if (website != null)
                {
                   websitesFromUrlColumn.Add(website);
                }
            }
            // Add Urls from file. This will be false if User clicks on Refresh. Only true if user click on add from file button
            if (urlFromUserFile.Count > 0)
            {
                foreach (string websiteFromFile in urlFromUserFile)
                {
                   ts.Add( Task.Run(() => websitesFromUrlColumn.Add(websiteFromFile)));
                }
                // Now we will clear the list. Because the list is in Datagridviewhelper the values wouldnt get delete if the user clicks on refresh after the user has added values. 
                urlFromUserFile.Clear();
            }
            await Task.WhenAll(ts);
        }
        public static void AddWebsitesFromFile(string file)
        {
            urlFromUserFile = File.ReadAllLines(file).ToList();
        }
        public static void OpenFileDialog(TextBox textBox)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = Resources.Textfilefilter;

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox.Text = openFileDialog1.FileName;
            }
        }
        public static void ClearRowAndSettings(DataGridView dataGridView, TextBox error)
        { 
           dataGridView.Rows.Clear();
           Settings.Default.Reset();
           error.Clear();
        }
        public async static Task AddRefreshedWebsitesToDatagridAsync(List<string> websitesFromUrlColumn, DataGridView dataGridView, TextBox error)
        {
            // counter for Progressbar
            int count = 0;
            Form1.Current.ShowWaitingIndicator(true);
            // We'll go through each column until the name is urldt
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                
                if (column.Name.ToLower() == "urldt")
                {
                    // Now we will stay here until we went through every value in websitesFromUrlColumn
                    try
                    {
                        // websitesFromUrlColumn also contains values from file if the user has clicked on add from file button
                        foreach (string item in websitesFromUrlColumn)
                        {
                            count++; 
                            if (!string.IsNullOrWhiteSpace(item))
                            {
                                var dch = new RowData()
                                {
                                    RawUrl = item,
                                    CorrectedUrl = UrlHelper.ChangedInput(item)
                                };
                                // we start to add values from websitesFromUrlColumn to Datagridview and settings if we can extract SSL-values
                                if (await Ssl.PopulateWithSslDataAsync(dch, error) == ConnectionStatus.NoIssue)
                                {
                                    // Update Progressbar
                                    Form1.Current.ProgressBarStatus(count, websitesFromUrlColumn.Count); 
                                    dataGridView.Rows.Add(dch.SubjectName, dch.StartDate, dch.ExpirationDate, dch.Days, dch.RawUrl, dch.IsNotValid);
                                    SettingsHelper.SaveSettings(dch); 
                                }
                                else
                                {
                                    Form1.Current.ProgressBarStatus(count, websitesFromUrlColumn.Count);
                                }
                            }
                            else
                            {
                                Form1.Current.ProgressBarStatus(count, websitesFromUrlColumn.Count);
                                continue;
                            }

                        }
                    }
                    catch
                    {

                    }
                    finally
                    {
                        Form1.Current.ShowWaitingIndicator(false);
                    }
                }
            }

        }
        public static void AddValuesToDatagridview(DataGridView dataGridView)
        {
            int settingsQuantity = Settings.Default.Name.Count;
            // Has to be atleast two becauce an empty datagridview still has one value which is an empty string
            if (settingsQuantity >= 2)
            {
                for (int i = 1; i < settingsQuantity; i++)
                {
                    // will be false for the first value 
                    bool notNulltest = !string.IsNullOrWhiteSpace(Settings.Default.Name[i]) && !string.IsNullOrWhiteSpace(Settings.Default.Startdate[i])
                        && !string.IsNullOrWhiteSpace(Settings.Default.Expirationdate[i])
                        && !string.IsNullOrWhiteSpace(Settings.Default.Days[i])
                        && !string.IsNullOrWhiteSpace(Settings.Default.Url[i]) &&
                          !string.IsNullOrWhiteSpace(Settings.Default.IsValid[i]) &&
                        !string.IsNullOrWhiteSpace(Settings.Default.RawUrl[i]);
                    if (notNulltest)
                    {
                        int counter = 0;
                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            bool checkIfWebsiteTwice = false;
                            try
                            {
                                // Check if value already in datagridview
                                checkIfWebsiteTwice = (bool)(row.Cells[4]?.Value?.ToString().Contains(Settings.Default.Url[i]));
                            }
                            catch { }

                            if (checkIfWebsiteTwice)
                            {
                                counter++;
                            }
                        }
                        if (counter == 0)
                        {
                            dataGridView.Rows.Add(Settings.Default.Name[i],
                            Settings.Default.Startdate[i], Settings.Default.Expirationdate[i],
                            Settings.Default.Days[i], Settings.Default.RawUrl[i], Settings.Default.IsValid[i]);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        public static void AddColorToDataGrid(DataGridView dataGridView)
        {
            long diffrence = 0;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                int currentRow = row.Index;
                string num = row.Cells[3].Value?.ToString();
                bool notNull = row.Cells[5].Value != null;

                if (notNull)
                {
                    string isNotValidBool = row.Cells[5].Value?.ToString();
                    // if the site exist but has certificate Issues or the site is not safe then it row will be red  
                    if (isNotValidBool == "true")
                    {
                        dataGridView.Rows[currentRow].DefaultCellStyle.BackColor = Color.FromArgb(243, 80, 80);// red
                        continue;
                    }
                }
                if (num != null)
                {
                    bool checkInput = long.TryParse(num, out diffrence);

                    if (diffrence <= 0 && checkInput)
                    {
                        dataGridView.Rows[currentRow].DefaultCellStyle.BackColor = Color.FromArgb(243, 80, 80);// red
                    }
                    if (!checkInput)
                    {
                        dataGridView.Rows[currentRow].DefaultCellStyle.BackColor = Color.White; // if days has convert issues.For instance  Days == 23r oder 5z  
                        continue;
                    }
                    if (diffrence > 30)
                    {
                        dataGridView.Rows[currentRow].DefaultCellStyle.BackColor = Color.FromArgb(215, 255, 241);// green 
                    }
                    if (diffrence <= 30 && diffrence > 0)
                    {
                        dataGridView.Rows[currentRow].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 11);// yellow
                    } 
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
