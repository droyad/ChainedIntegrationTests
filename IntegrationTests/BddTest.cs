using System.Transactions;
using Api;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Testing;
using NUnit.Framework;
using TestStack.BDDfy;

namespace IntegrationTests
{
    public abstract class BddTest
    {
        public static Browser Browser { get; } = new Browser(new MyNancyBootstrapper());

        public virtual void Execute()
        {
            using (new TestSession())
            {
                this.BDDfy();
            }
        }

    }
}