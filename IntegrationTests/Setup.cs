using Api.Domain;
using NUnit.Framework;

namespace IntegrationTests
{
    [SetUpFixture]
    public class Setup
    {
        [OneTimeSetUp]
        public void BeforeRunningAnyTests()
        {
            using (var db = new DB())
                db.Database.CreateIfNotExists();
        }
    }
}