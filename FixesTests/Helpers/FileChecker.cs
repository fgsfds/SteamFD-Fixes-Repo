using System.Security.Cryptography;

namespace FixesTests.Helpers
{
    internal class FileChecker
    {
        public static async Task<string?> CheckOnlineFileAsync(HttpClient client, Uri url, string? zipMD5)
        {
            try
            {
                using var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

                if (!response.IsSuccessStatusCode)
                {
                    return $"Error while checking URL: {url}";
                }

                if (zipMD5 is null)
                {
                    return null;
                }

                string hash;

                if (response.Content.Headers.ContentMD5 is not null)
                {
                    hash = BitConverter.ToString(response.Content.Headers.ContentMD5).Replace("-", string.Empty);
                }
                else
                {
                    //if can't get md5 from the response, download zip
                    var currentDir = Directory.GetCurrentDirectory();
                    var fileName = Path.GetFileName(url.ToString());
                    var pathToFile = Path.Combine(currentDir, fileName);

                    using (var file = new FileStream(pathToFile, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        using var source = await response.Content.ReadAsStreamAsync();

                        await source.CopyToAsync(file);
                    }

                    response.Dispose();

                    using (var md5 = MD5.Create())
                    {
                        using var stream = File.OpenRead(pathToFile);

                        hash = Convert.ToHexString(md5.ComputeHash(stream));
                    }
                }

                if (!zipMD5.Equals(hash))
                {
                    return $"MD5 of the ZIP archive doesn't match: {url}";
                }
            }
            catch (Exception ex)
            {
                return ex.Message + ": " + url;
            }

            return null;
        }
    }
}
