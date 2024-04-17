using Microsoft.AspNetCore.Routing;
using System.Diagnostics.Metrics;
using TestTasks__API_.Domain.Core.Models;
using TestTasks__API_.Domain.Interfaces;

namespace TestTasks__API_
{
    public class TransientCounterService : ITransientCounter
    {
        protected internal IScopedCounter Counter { get; }

        public TransientCounterService(IScopedCounter counter)
        {
            Counter = counter;
        }

        public Task<int> GetValueAsync()
        {
            return Counter.GetValueAsync();
        }
    }
}