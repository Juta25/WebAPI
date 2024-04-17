using Microsoft.AspNetCore.Routing;
using System.Diagnostics.Metrics;
using TestTasks__API_.Domain.Core.Models;
using TestTasks__API_.Domain.Interfaces;

namespace TestTasks__API_
{
    public class ScopedCounterService : IScopedCounter
    {
        protected internal IScopedCounter Counter { get; }
        public ScopedCounterService(IScopedCounter counter)
        {
            Counter = counter;
        }
        public int Value => Counter.Value;
    }
}