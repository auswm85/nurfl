using System;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Nurfl.Core
{
    /// <summary>
    /// NurflRequest is a wrapper for the device request.
    /// </summary>
    public class NurflRequest
    {
        /// <summary>
        /// Current HttpRequest of application
        /// </summary>
        public HttpRequest Request { get; set; }

        /// <summary>
        /// User Agent string
        /// </summary>
        public string UserAgent { get; set; }

        public NurflRequest()
        {
            Request = (HttpContext.Current.Request != null) ? HttpContext.Current.Request : null;
            UserAgent = (Request.UserAgent != null) ? Request.UserAgent : string.Empty;
        }

        public NurflRequest(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            Request = request;
            UserAgent = (Request.UserAgent != null) ? Request.UserAgent : string.Empty;
        }

        public NurflRequest(string userAgent)
        {
            if(string.IsNullOrEmpty(userAgent.Trim()))
                throw new ArgumentNullException("userAgent");

            Request = (HttpContext.Current.Request != null) ? HttpContext.Current.Request : null;
            UserAgent = userAgent;
        }

        /// <summary>
        /// Retrieves device information from tera wurfl webservice
        /// </summary>
        /// <param name="baseUrl">base tera wurfl webservice url</param>
        /// <param name="parameters">(optional) capabilities to return. If left blank will return all capabilities</param>
        /// <returns>Nurfl.Core.Device</returns>
        public Device Get(string baseUrl, params string[] parameters)
        {
            if (string.IsNullOrEmpty(baseUrl) || !baseUrl.IsValidUrl())
                throw new ArgumentException("invalid webservice url");  //bad url that's a no no

            string requestUrl = BuildRequestUrl(baseUrl, parameters);
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader reader = null;
            Device device = null;
            string json = string.Empty;

            try
            {
                request = WebRequest.Create(requestUrl) as HttpWebRequest;
                request.KeepAlive = false;
                request.Method = "GET";
                request.Timeout = 10 * 1000; //10 second timeout

                response = request.GetResponse() as HttpWebResponse;

                if (request.HaveResponse && response != null)
                {
                    reader = new StreamReader(response.GetResponseStream());
                    json = reader.ReadToEnd();

                    if (!string.IsNullOrEmpty(json))
                        device = JsonConvert.DeserializeObject<Device>(json);
                }
            }
            catch(WebException wex)
            {
                // Try to retrieve more information about the network error  
                if (wex.Response != null)
                {
                    using (HttpWebResponse errorResponse = (HttpWebResponse)wex.Response)
                    {
                       //TODO: implement error handling or logging
                    }
                }  
            }
            finally 
            {
                if (response != null)
                    response.Close();
            }
    
            return device;
        }

        /// <summary>
        /// Builds request url for webservice.
        /// </summary>
        /// <param name="baseUrl">base tera wurfl webservice url</param>
        /// <param name="parameters">(optional) capabilities to return. If left blank will return all capabilities</param>
        /// <returns>string</returns>
        private string BuildRequestUrl(string baseUrl, params string[] parameters)
        {
            string requestUrl = baseUrl;

            if (!string.IsNullOrEmpty(UserAgent))
                requestUrl = requestUrl.AppendUrlParam("ua", HttpUtility.UrlEncode(UserAgent));// append user agent

            if (parameters.Length > 0)
                requestUrl = requestUrl.AppendUrlParam("search", HttpUtility.UrlEncode(string.Join("|", parameters))); //append parameters to search

            requestUrl = requestUrl.AppendUrlParam("format", "json"); //we want some delicious json

            return requestUrl;
        }
    }
}
