﻿namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

public interface IInt32IndexedValue<out T>
{
    public int Index { get; }

    public T Value { get; }
}