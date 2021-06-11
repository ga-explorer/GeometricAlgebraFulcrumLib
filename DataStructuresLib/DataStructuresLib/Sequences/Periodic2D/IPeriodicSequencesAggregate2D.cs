using System.Collections.Generic;

namespace DataStructuresLib.Sequences.Periodic2D
{
    /// <summary>
    /// This interface represents a 2D periodic sequence of values of finite size.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPeriodicSequencesAggregate2D<T> 
        : IPeriodicSequence2D<T>
    {
        IReadOnlyList<IPeriodicSequence2D<T>> BaseSequences { get; }
    }
}