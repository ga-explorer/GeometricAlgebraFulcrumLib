using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.PeriodicLists;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.PeriodicLists2D;

/// <summary>
/// A 2D periodic list is like an array that can be indexed using any two integers,
/// positive or negative, such that it's a periodic 2D block of values
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface IPeriodicReadOnlyList2D<out TValue> :
    IPeriodicReadOnlyList<TValue>, IReadOnlyList2D<TValue>
{
    TValue[,] ToArray2D();
}