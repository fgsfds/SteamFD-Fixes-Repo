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
                    if (fix.Url is null)
                    {
                        continue;
                    }

                    if (!fix.Url.EndsWith(".zip"))
                    {
                        Trace.TraceError($"{fixes.GameName}, url is not a zip: {fix.Url}");
                        isFailed = true;

                        continue;
                    }

                    if (fix.Url.Contains("/blob/"))
                    {
                        Trace.TraceError($"{fixes.GameName}, invalid Url: {fix.Url}");
                        isFailed = true;

                        continue;
                    }

                    Uri url;

                    if (!branchName.Equals("master"))
                    {
                        url = new Uri(fix.Url.Replace("/master/", $"/{branchName}/"));
                    }
                    else
                    {
                        url = new Uri(fix.Url);
                    }

                    var fileCheckResult = await FileChecker.CheckOnlineFileAsync(client, url, fix.MD5);

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