﻿using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Surfaces;

public class GrComputedParametricSurface3D :
    IGraphicsParametricSurface3D
{
    public Func<double, double, LinFloat64Vector3D> GetPointFunc { get; }

    public Func<double, double, LinFloat64Vector3D> GetNormalFunc { get; }


    public GrComputedParametricSurface3D(Func<double, double, LinFloat64Vector3D> getPointFunc)
    {
        GetPointFunc = getPointFunc;
        GetNormalFunc = null;
    }

    public GrComputedParametricSurface3D(Func<double, double, LinFloat64Vector3D> getPointFunc, Func<double, double, LinFloat64Vector3D> getNormalFunc)
    {
        GetPointFunc = getPointFunc;
        GetNormalFunc = getNormalFunc;
    }


    public bool IsValid()
    {
        return true;
    }

    public LinFloat64Vector3D GetPoint(double parameterValue1, double parameterValue2)
    {
        return GetPointFunc(parameterValue1, parameterValue2);
    }

    public LinFloat64Vector3D GetNormal(double parameterValue1, double parameterValue2)
    {
        if (GetNormalFunc is not null)
            return GetNormalFunc(parameterValue1, parameterValue2);

        const double zeroEpsilon = 1e-7;

        var p1 = GetPointFunc(parameterValue1 - zeroEpsilon, parameterValue2);
        var p2 = GetPointFunc(parameterValue1 + zeroEpsilon, parameterValue2);

        var v1 = (p2 - p1) / (2 * zeroEpsilon);

        p1 = GetPointFunc(parameterValue1, parameterValue2 - zeroEpsilon);
        p2 = GetPointFunc(parameterValue1, parameterValue2 + zeroEpsilon);

        var v2 = (p2 - p1) / (2 * zeroEpsilon);

        return v1.VectorCross(v2);
    }

    public LinFloat64Vector3D GetUnitNormal(double parameterValue1, double parameterValue2)
    {
        return GetNormal(parameterValue1, parameterValue2).ToUnitLinVector3D();
    }

    public GrParametricSurfaceLocalFrame3D GetFrame(double parameterValue1, double parameterValue2)
    {
        return new GrParametricSurfaceLocalFrame3D(this, parameterValue1, parameterValue2);
    }
}