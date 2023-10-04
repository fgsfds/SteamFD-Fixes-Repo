using FixesTests.Helpers;
using System.Diagnostics;

namespace FixesTests
{
    [TestClass]
    public class FixesTests
    {
        [TestMethod]
        public void DeserializeXmlAndCheckIfFilesExist()
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

            var result = FixesProvider.DeserializeFixesXml();

            Assert.IsNotNull(result);

            var doesFixExist = result.Any(x => x.GameId == 108710);

            Assert.IsTrue(doesFixExist);

            var isFailed = false;

            foreach (var fixes in result)
            {
                foreach (var fix in fixes.Fixes)
                {
                    if (!fix.Url.EndsWith(".zip"))
                    {
                        Trace.WriteLine("Url is not a zip: " + fix.Url);
                        isFailed = true;

                        continue;
                    }

                    if (fix.Url.Contains("/blob/"))
                    {
                        Trace.WriteLine("Invalid Url: " + fix.Url);
                        isFailed = true;

                        continue;
                    }

                    Uri url;

                    if (branchName is not null &&
                        !branchName.Equals("master"))
                    {
                        url = new Uri(fix.Url.Replace("/master/", $"/{branchName}/"));
                    }
                    else
                    {
                        url = new Uri(fix.Url);
                    }

                    var fileCheckResult = FileChecker.CheckOnlineFile(url);

                    if (fileCheckResult is not null)
                    {
                        Trace.WriteLine(fileCheckResult);
                        isFailed = true;

                        continue;
                    }

                    Trace.WriteLine(url + " is OK");
                }
            }

            Assert.IsFalse(isFailed);
        }
    }
}