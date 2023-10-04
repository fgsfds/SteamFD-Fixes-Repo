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
            var result = FixesProvider.DeserializeFixesXml();

            Assert.IsNotNull(result);

            var doesFixEsists = result.Any(x => x.GameId == 108710);

            Assert.IsTrue(doesFixEsists);

            List<string> fileCheckResults = new();

            foreach (var fixes in result)
            {
                foreach (var fix in fixes.Fixes)
                {
                    if (fix.Url.Contains("/blob/"))
                    {
                        fileCheckResults.Add("Invalid Url: " + fix.Url);
                        continue;
                    }

                    var fileCheckResult = FileChecker.CheckOnlineFile(new Uri(fix.Url));

                    if (fileCheckResult is not null)
                    {
                        fileCheckResults.Add(fileCheckResult);
                        continue;
                    }

                    Trace.WriteLine(fix.Url + " is OK");
                }
            }

            if (fileCheckResults.Any())
            {
                foreach (var fileCheckResult in fileCheckResults)
                {
                    Trace.WriteLine(fileCheckResult);
                }
            }

            Assert.IsFalse(fileCheckResults.Any());
        }
    }
}