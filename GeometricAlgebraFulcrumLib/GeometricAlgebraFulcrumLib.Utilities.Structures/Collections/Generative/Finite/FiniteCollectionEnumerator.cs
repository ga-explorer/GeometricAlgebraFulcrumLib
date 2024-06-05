﻿using System.Collections;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.Generative.Finite;

public sealed class FiniteCollectionEnumerator<T> : IEnumerator<T>
{
    private readonly IFiniteCollection<T> _finiteCollection;
    private int _currentIndex;


    internal FiniteCollectionEnumerator(IFiniteCollection<T> finiteCollection)
    {
        _finiteCollection = finiteCollection;
        _currentIndex = finiteCollection.MinIndex - 1;
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
        _currentIndex = _finiteCollection.MinIndex - 1;
        Current = default;
    }

    public T Current { get; private set; }

    object IEnumerator.Current => Current;
}