using System.Collections.Generic;

namespace DataStructuresLib.Collections;

public interface IReadOnlyCollection2D<out T> 
    : IReadOnlyCollection<T>
{
    int Count1 { get; }

    int Count2 { get; }
}