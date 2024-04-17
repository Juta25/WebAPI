using System.Diagnostics.Metrics;
using TestTasks__API_.Domain.Core.Models;
using TestTasks__API_.Domain.Interfaces;

namespace TestTasks__API_
{
    public class ScopedCounter : IScopedCounter
    {
        static Random Rnd = new Random();
        private int _value;

        public ScopedCounter()
        {
            _value = Rnd.Next(0, 1000000);
        }

        public Task<int> GetValueAsync()
        {
            return Task.FromResult(_value);
        }
    }
}