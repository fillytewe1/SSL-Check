using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSLZertifikatCheck
{
    public class RowData
    {
        public string SubjectName { get; set; }
        public string StartDate { get; set; }
        public string ExpirationDate { get; set; }
        public string Days { get; set; } 
        public string RawUrl { get; set; }
        public string CorrectedUrl { get; set; }
        public string IsNotValid { get; set; }
    }
}
