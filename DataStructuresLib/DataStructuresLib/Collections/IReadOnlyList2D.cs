using System.Collections.Generic;

namespace DataStructuresLib.Collections
{
    public interface IReadOnlyList2D<out T> : 
        IReadOnlyList<T>, 
        IReadOnlyCollection2D<T>
    {
        T this[int index1, int index2] { get; }
    }
}