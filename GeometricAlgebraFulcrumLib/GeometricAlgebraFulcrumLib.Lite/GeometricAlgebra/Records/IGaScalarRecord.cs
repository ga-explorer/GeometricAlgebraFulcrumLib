﻿namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Records;

public interface IGaScalarRecord<out T>
{
    /// <summary>
    /// The Scalar Value
    /// </summary>
    T Scalar { get; }
}