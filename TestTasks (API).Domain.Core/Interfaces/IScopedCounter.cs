using TestTasks__API_.Domain.Core.Models;

namespace TestTasks__API_.Domain.Interfaces
{
    public interface IScopedCounter
    {
        int Value { get; }
    }
}