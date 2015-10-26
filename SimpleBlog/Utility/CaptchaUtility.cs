using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace SimpleBlog.Utility
{
    class CaptchaResponse
    {
       [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public IList<string> ErrorCodes { get; set; }
    }

    public static class CaptchaUtility
    {
        /// <summary>
        /// Sends a request out to Google to ensure the captcha
        /// response is valid.
        /// </summary>
        /// <param name="response">The data posted from the form</param>
        /// <param name="secret">The secret API key</param>
        public static bool Verify(string response, string secret)
        {
            string postString = "secret=" + secret;
            postString += "&response=" + response;

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] postData = encoding.GetBytes(postString);

            string googleRecaptchaUrl = "https://www.google.com/recaptcha/api/siteverify";

            var captchaRequest = (HttpWebRequest)WebRequest.Create(googleRecaptchaUrl);

            captchaRequest.Method = "POST";
            // Always be honest about your UserAgent.
            captchaRequest.UserAgent = "SimpleBlog";
            captchaRequest.ContentType = "application/x-www-form-urlencoded";
            captchaRequest.ContentLength = postData.Length;

            //Send the data
            Stream resultStream = captchaRequest.GetRequestStream();
            resultStream.Write(postData, 0, postData.Length);
            resultStream.Close();

            //Get the response
            var webResponse = captchaRequest.GetResponse();
            resultStream = webResponse.GetResponseStream();
            StreamReader responseReader = new StreamReader(resultStream);
            string captchaResults = responseReader.ReadToEnd();

            responseReader.Close();
            resultStream.Close();
            webResponse.Close();

            CaptchaResponse processedResponse = JsonConvert.DeserializeObject<CaptchaResponse>(captchaResults);

            return processedResponse.Success;
        }
    }
}