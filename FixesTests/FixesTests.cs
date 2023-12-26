using FixesTests.Fixes;
using FixesTests.Helpers;
using System.Diagnostics;

namespace FixesTests
{
    [TestClass]
    public class FixesTests
    {
        [TestMethod]
        public async Task DeserializeXmlAndCheckIfFilesExistAsync()
        {
            Process process = new()
            {
                StartInfo = new ProcessStartInfo("git")
                {
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    Arguments = "rev-parse --abbrev-ref HEAD"
                }
            };
            process.Start();

            var branchName = process.StandardOutput.ReadLine();

            if (branchName is null)
            {
                Assert.Fail("Can't get branch name");
            }

            var result = FixesProvider.DeserializeFixesXml();

            Assert.IsNotNull(result);

            var doesFixExist = result.Any(x => x.GameId == 108710);

            Assert.IsTrue(doesFixExist);

            var isFailed = false;

            using var client = new HttpClient();

            foreach (var fixes in result)
            {
                foreach (var fix in fixes.Fixes)
                {
                    if (fix is not FileFixEntity fileFix)
                    {
                        continue;
                    }

                    if (fileFix.Url is null)
                    {
                        continue;
                    }

                    if (!fileFix.Url.EndsWith(".zip") &&
                        !fileFix.Url.EndsWith(".7z"))
                    {
                        Trace.TraceError($"{fixes.GameName}, url is not a zip or 7zip: {fileFix.Url}");
                        isFailed = true;

                        continue;
                    }

                    if (fileFix.Url.Contains("/blob/"))
                    {
                        Trace.TraceError($"{fixes.GameName}, invalid Url: {fileFix.Url}");
                        isFailed = true;

                        continue;
                    }

                    Uri url;

                    if (!branchName.Equals("master"))
                    {
                        url = new Uri(fileFix.Url.Replace("/master/", $"/{branchName}/"));
                    }
                    else
                    {
                        url = new Uri(fileFix.Url);
                    }

                    var fileCheckResult = await FileChecker.CheckOnlineFileAsync(client, url, fileFix.MD5);

                    if (fileCheckResult is not null)
                    {
                        Trace.TraceError($"{fixes.GameName}, {fileCheckResult}");
                        isFailed = true;

                        continue;
                    }

                    //Trace.WriteLine(url + " is OK");
                }
            }

            Assert.IsFalse(isFailed);
        }
    }
}