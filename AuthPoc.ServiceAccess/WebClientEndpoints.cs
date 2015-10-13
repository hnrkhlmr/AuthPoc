using System.Configuration;

namespace AuthPoc.ServiceAccess
{
    public class WebClientEndpoints
    {
        public static string UserInfoBaseUrl { get { return GetApiBaseUrlFor("UserInfo"); } }
        public static string LaserpekareApiBaseUrl { get { return GetApiBaseUrlFor("Laserpekare"); } }
        public static string RegionsApiBaseUrl { get { return GetApiBaseUrlFor("Regions"); } }
        public static string YttrandeApiBaseUrl { get { return GetApiBaseUrlFor("Yttrande"); } }
        public static string YttrandedokumentApiBaseUrl { get { return GetApiBaseUrlFor("Yttrandedokument"); } }
        public static string JobTitlesApiBaseUrl { get { return GetApiBaseUrlFor("JobTitles"); } }
        public static string EmployersApiBaseUrl { get { return GetApiBaseUrlFor("Employers"); } }
        public static string DepartmentsApiBaseUrl { get { return GetApiBaseUrlFor("Departments"); } }
        public static string AddressesApiBaseUrl { get { return GetApiBaseUrlFor("Addresses"); } }
        public static string EmailsBaseUrl { get { return GetApiBaseUrlFor("Emails"); } }
        public static string ValuesApiBaseUrl { get { return GetApiBaseUrlFor("Values"); } }
        public static string AccountApiBaseUrl { get { return GetApiBaseUrlFor("Account"); } }

        public static string BaseUrl { get { return GetBaseUrlFor(string.Empty); } }

        #region Private helpers

        private static string GetApiBaseUrlFor(string endpoint) 
        {
            return GetBaseUrlFor(string.Format("/api/{0}", endpoint));
        }

        private static string GetBaseUrlFor(string endpoint)
        {
            var baseUrl = ConfigurationManager.AppSettings.Get("WebApiBaseUrl").TrimEnd('/');

            return string.IsNullOrEmpty(endpoint) 
                ? baseUrl 
                : string.Format("{0}/{1}", baseUrl, endpoint.TrimStart('/'));
        }

        #endregion
    }
}
