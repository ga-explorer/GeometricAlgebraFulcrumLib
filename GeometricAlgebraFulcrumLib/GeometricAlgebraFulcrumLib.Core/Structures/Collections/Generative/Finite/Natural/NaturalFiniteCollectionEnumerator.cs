using System.Collections;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections.Generative.Finite.Natural;

public sealed class NaturalFiniteCollectionEnumerator<T> : IEnumerator<T>
{
    private readonly NaturalFiniteCollection<T> _finiteCollection;
    private int _currentIndex;


    internal NaturalFiniteCollectionEnumerator(NaturalFiniteCollection<T> finiteCollection)
    {
        _finiteCollection = finiteCollection;
        _currentIndex = -1;
        Current = default;
    }


    public void Dispose()
    {
            
    }

    public bool MoveNext()
    {
        _currentIndex++;

        if (_currentIndex > _finiteCollection.MaxIndex)
            return false;

        Current = _finiteCollection.GetItem(_currentIndex);

        return true;
    }

    public void Reset()
    {
        _currentIndex = -1;
        Current = default;
    }

    public T Current { get; private set; }

    object IEnumerator.Current => Current;
}