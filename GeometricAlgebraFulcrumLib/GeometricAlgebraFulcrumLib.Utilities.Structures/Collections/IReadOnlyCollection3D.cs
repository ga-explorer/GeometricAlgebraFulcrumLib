﻿namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections;

public interface IReadOnlyCollection3D<out T> : IReadOnlyCollection<T>
{
    int Count1 { get; }

    int Count2 { get; }

    int Count3 { get; }
}