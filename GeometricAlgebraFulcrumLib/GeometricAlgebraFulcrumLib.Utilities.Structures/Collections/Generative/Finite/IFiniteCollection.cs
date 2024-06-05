namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.Generative.Finite;

/// <summary>
/// This interface represents a finite collection of elements. The elements
/// are indexed by integers that can be negative or positive integers
/// </summary>
public interface IFiniteCollection : IGenerativeCollection
{
    /// <summary>
    /// The total number of elements in this collection
    /// </summary>
    int Count { get; }

    /// <summary>
    /// The smallest integer index in this collection
    /// </summary>
    int MinIndex { get; }

    /// <summary>
    /// The largest integer index in this collection
    /// </summary>
    int MaxIndex { get; }
}

/// <summary>
/// This interface represents a generic finite collection of elements of a certain type.
/// The elements are indexed by integers that can be negative or positive integers
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IFiniteCollection<out T> : IFiniteCollection, IGenerativeCollection<T>, IEnumerable<T>
{
}