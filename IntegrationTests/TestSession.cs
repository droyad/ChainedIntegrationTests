using System;
using System.Transactions;

namespace IntegrationTests
{
    public class TestSession : IDisposable
    {
        [ThreadStatic]
        private static TestSession _current;

        private readonly TransactionScope _trn;

        public TestSession()
        {
            if (_current != null)
                return;

            _trn = new TransactionScope();
            _current = this;
        }

        public void Dispose()
        {
            if (_current != this)
                return;

            _current = null;
            _trn.Dispose();
        }
    }
}