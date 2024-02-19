using System.Collections.Generic;

namespace DataStructuresLib.Collections;

public interface IReadOnlyList3D<out T> 
    : IReadOnlyList<T>, IReadOnlyCollection3D<T>
{
    T this[int index1, int index2, int index3] { get; }
}