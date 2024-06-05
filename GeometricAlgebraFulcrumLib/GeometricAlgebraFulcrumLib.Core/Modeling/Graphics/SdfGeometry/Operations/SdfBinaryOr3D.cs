﻿using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.SdfGeometry.Operations;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfBinaryOr3D : SdfBinaryOperation
{
    public override double GetScalarDistance(ILinFloat64Vector3D point)
    {
        return Math.Min(
            Surface1.GetScalarDistance(point),
            Surface2.GetScalarDistance(point)
        );
    }
}