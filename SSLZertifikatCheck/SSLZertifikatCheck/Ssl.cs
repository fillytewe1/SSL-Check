using SSLZertifikatCheck.i18n;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SSLZertifikatCheck
{
    internal class Ssl
    {
        static string subjectnameErrorCert = "";
        static string StartErrorCert;
        static string EndErrorCert;
        static string DaysErrorCert;
        static string IsExpired = "false";

        public async static Task<ConnectionStatus> PopulateWithSslDataAsync(RowData dataColumnHelper, TextBox error) 
        {
            bool access = false;
            string localDate = DateTime.Now.ToString("HH:mm:ss");
            // Check if the corrected Url already exist in UserSettings
            if (Settings.Default.Url.Contains(dataColumnHelper.CorrectedUrl))
            { 
                return ConnectionStatus.AlreadyExist;
            }
            try
            {
                // Check if we have an input with an different port than 443. So we split the input for instance https://astaro.blackpoint.de:4443/

                int portnumber = 443;
                string[] UrlSplitForPort = dataColumnHelper.CorrectedUrl.Split(':');
                if (UrlSplitForPort.Length > 1)
                {
                    int temp;
                    bool success = int.TryParse(UrlSplitForPort[1], out temp);
                    if (success)
                    {
                        portnumber = temp;
                    }
                }
                // Start
                var clients = new System.Net.Sockets.TcpClient(UrlSplitForPort[0], portnumber);

                using (var sslStream = new SslStream(clients.GetStream(), true))
                {
                    await sslStream.AuthenticateAsClientAsync(UrlSplitForPort[0]);

                    var serverCertificate = sslStream.RemoteCertificate;
                    DateTime dateTimeStart = DateTime.Parse(serverCertificate?.GetEffectiveDateString());
                    DateTime dateTimeEnd = DateTime.Parse(serverCertificate?.GetExpirationDateString());
                    TimeSpan difference = dateTimeEnd.Subtract(DateTime.Now);
                    double differenceInDays = difference.Days;

                    dataColumnHelper.SubjectName = serverCertificate?.Subject?.Split(',')[0];
                    dataColumnHelper.StartDate = serverCertificate?.GetEffectiveDateString();
                    dataColumnHelper.ExpirationDate = serverCertificate?.GetExpirationDateString();
                    dataColumnHelper.Days = differenceInDays.ToString();
                    dataColumnHelper.IsNotValid = IsExpired;
                    IsExpired = "false";

                    return ConnectionStatus.NoIssue;
                }
            }
            catch (Exception e)
            {
                try
                {
                    // if connection has failed we try again with HttpWebRequest and HttpWebResponse
                    string url = "";
                    // We check if the url exist even if it starts with http. 
                    if (dataColumnHelper.CorrectedUrl.StartsWith("http://"))
                    {
                        url = dataColumnHelper.CorrectedUrl;
                    }
                    else
                    {
                        // we have to add https for HttpWebRequest
                        url = "https://" + dataColumnHelper.CorrectedUrl;
                    }

                    HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    myHttpWebRequest.MaximumAutomaticRedirections = 1;
                    myHttpWebRequest.AllowAutoRedirect = true;
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;
                    myHttpWebRequest.Credentials = CredentialCache.DefaultCredentials;
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                    ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);

                    HttpWebResponse myHttpWebResponse = (HttpWebResponse)(await myHttpWebRequest.GetResponseAsync());
                    access = true;
                }
                catch
                {

                }
                if (e.HResult == -2146233087 || IsExpired == "true")
                {
                    // if https site can be reached then we'll add the values 
                    dataColumnHelper.SubjectName = subjectnameErrorCert;
                    dataColumnHelper.StartDate = StartErrorCert;
                    dataColumnHelper.ExpirationDate = EndErrorCert;
                    dataColumnHelper.Days = DaysErrorCert;
                    dataColumnHelper.IsNotValid = IsExpired;
                    IsExpired = "false";
                    // This means the certificate has issues. For instance: the site is not safe even though we have the values. 
                    return ConnectionStatus.CertificateOrNotSecure;
                }
                else
                {
                    // The second attempt with HttpWebRequest has failed.
                    if (dataColumnHelper.CorrectedUrl.StartsWith("http://") && access)
                    { 
                        return ConnectionStatus.HttpError;
                    }
                    if (e.HResult == -2147467259)
                    { 
                        return ConnectionStatus.WrongUrl;
                    }
                    else
                    { 
                        return ConnectionStatus.Unknown;
                    }
                } 
            }
        }

        private static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
        {
            bool result = true;
            // 
            /*
             * policyErrors can be :
             * 
                1.None	                        0	 No SSL policy errors.
               

                2.RemoteCertificateChainErrors	4	 ChainStatus has returned a non empty array.
               

                3.RemoteCertificateNameMismatch	2	 Certificate name mismatch.
               

                4.RemoteCertificateNotAvailable	1	 Certificate not available.
               
             */
            if (policyErrors != SslPolicyErrors.None || chain.ChainElements.Count != 3)
            {
                // Proof we can extract values but something is wrong with the URL. For instance : website might not be safe
                DateTime dateTimeStart = DateTime.Parse(cert?.GetEffectiveDateString());
                DateTime dateTimeEnd = DateTime.Parse(cert?.GetExpirationDateString());
                TimeSpan difference = dateTimeEnd.Subtract(DateTime.Now);
                double differenceInDays = difference.Days;

                subjectnameErrorCert = cert?.Subject?.Split(',')[0];
                StartErrorCert = cert?.GetEffectiveDateString();
                EndErrorCert = cert?.GetExpirationDateString();
                DaysErrorCert = differenceInDays.ToString();
                IsExpired = "true";

                return false;
            }
            return result;
        }
    }
}
