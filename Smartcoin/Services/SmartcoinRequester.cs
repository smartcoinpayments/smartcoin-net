using Smartcoin.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Smartcoin.Services
{
    public class SmartcoinRequester
    {
        public static string Get(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            return DoRequest(request);
        }

        public static string Post(string url, string payload)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            if (!string.IsNullOrWhiteSpace(payload))
            {
                byte[] postData = Encoding.UTF8.GetBytes(payload);

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(postData, 0, postData.Length);
                    stream.Flush();
                    stream.Close();
                }
            }

            return DoRequest(request);
        }

        public static string Delete(string url, string payload = null)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "DELETE";

            if (!string.IsNullOrWhiteSpace(payload))
            {
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] postData = Encoding.UTF8.GetBytes(payload);

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(postData, 0, postData.Length);
                    stream.Flush();
                    stream.Close();
                }
            }

            return DoRequest(request);
        }

        public static string DoRequest(WebRequest req)
        {
            try
            {
                string credentials = String.Format("{0}:{1}", SmartcoinConfiguration.ApiKey, SmartcoinConfiguration.ApiSecret);
                byte[] bytes = Encoding.ASCII.GetBytes(credentials);
                string base64 = Convert.ToBase64String(bytes);
                string authorization = String.Concat("basic ", base64);
                req.Headers.Add("Authorization", authorization);

                using (var res = req.GetResponse())
                {
                    handleError((HttpWebResponse)res);

                    using (var reader = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (SmartcoinException ex)
            {
                throw ex;
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    handleError((HttpWebResponse)ex.Response);
                }

                throw new SmartcoinException(0, "There's was an error in your request");
            }
            catch (Exception ex)
            {
                throw new SmartcoinException(0, ex.Message);
            }
        }

        private static void handleError(HttpWebResponse response)
        {
            switch ((int)response.StatusCode)
            {
                case 400:
                case 402:
                case 404:
                    string json = null;
                    using (var readerStream = new StreamReader(response.GetResponseStream()))
                        json = readerStream.ReadToEnd();

                    var error = SmartcoinError.FromJson(json);
                    throw new SmartcoinException((int)response.StatusCode, error.Error.Message);
                case 401:
                    throw new SmartcoinException(401, "There's an error with your API Keys");
            }
        }
    }
}
