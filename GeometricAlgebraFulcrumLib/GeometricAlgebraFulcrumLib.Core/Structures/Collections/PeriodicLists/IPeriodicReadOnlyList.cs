namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections.PeriodicLists;

/// <summary>
/// A periodic list is like an array that can be indexed using any integer,
/// positive or negative, such that it's a periodic list of values
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface IPeriodicReadOnlyList<out TValue> : 
    IReadOnlyList<TValue>
{
}