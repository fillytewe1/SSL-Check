using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSLZertifikatCheck
{
    internal class SettingsHelper
    {
        // Save Values to Settings only if they have been added to Datagridview 
        public static void SaveSettings(RowData dch)
        {
            Settings.Default.Name.Add(dch.SubjectName);
            Settings.Default.Startdate.Add(dch.StartDate);
            Settings.Default.Expirationdate.Add(dch.ExpirationDate);
            Settings.Default.Days.Add(dch.Days);
            Settings.Default.Url.Add(dch.CorrectedUrl);
            Settings.Default.RawUrl.Add(dch.RawUrl);
            Settings.Default.IsValid.Add(dch.IsNotValid);
            Settings.Default.Save();
        }
    }
}
