﻿using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Maps.Space2D;

public interface IAffineMap2D :
    IGeometricElement
{
    bool SwapsHandedness { get; }
        
    Float64Vector2D MapPoint(IFloat64Vector2D point);

    Float64Vector2D MapVector(IFloat64Vector2D vector);

    Float64Vector2D MapNormal(IFloat64Vector2D normal);

    SquareMatrix3 GetSquareMatrix3();

    double[,] GetArray2D();

    IAffineMap2D GetInverseAffineMap();
}