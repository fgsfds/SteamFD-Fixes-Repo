using System.Net;

namespace FixesTests.Helpers
{
    internal class FileChecker
    {
        public static string? CheckOnlineFile(Uri url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";

            try
            {
                using var response = (HttpWebResponse)request.GetResponse();

                if (response is null ||
                    response.StatusCode is not HttpStatusCode.OK)
                {
                    return $"Error: {url}";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex.Message + ": " + url;
            }
        }
    }
}
