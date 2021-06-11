using System.Collections.Generic;

namespace DataStructuresLib.Sequences.Periodic1D
{
    /// <summary>
    /// This interface represents a periodic sequence of values of finite size.
    /// </summary>
    public interface IPeriodicSequence1D<out T> 
        : IReadOnlyList<T>
    {
        bool IsBasic { get; }

        bool IsOperator { get; }
    }
}