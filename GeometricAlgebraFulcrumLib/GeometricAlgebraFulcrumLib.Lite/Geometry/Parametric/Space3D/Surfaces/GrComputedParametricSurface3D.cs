using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Surfaces;

public class GrComputedParametricSurface3D :
    IGraphicsParametricSurface3D
{
    public Func<double, double, Float64Vector3D> GetPointFunc { get; }

    public Func<double, double, Float64Vector3D> GetNormalFunc { get; }


    public GrComputedParametricSurface3D(Func<double, double, Float64Vector3D> getPointFunc)
    {
        GetPointFunc = getPointFunc;
        GetNormalFunc = null;
    }
        
    public GrComputedParametricSurface3D(Func<double, double, Float64Vector3D> getPointFunc, Func<double, double, Float64Vector3D> getNormalFunc)
    {
        GetPointFunc = getPointFunc;
        GetNormalFunc = getNormalFunc;
    }


    public bool IsValid()
    {
        return true;
    }

    public Float64Vector3D GetPoint(double parameterValue1, double parameterValue2)
    {
        return GetPointFunc(parameterValue1, parameterValue2);
    }

    public Float64Vector3D GetNormal(double parameterValue1, double parameterValue2)
    {
        if (GetNormalFunc is not null)
            return GetNormalFunc(parameterValue1, parameterValue2);

        const double epsilon = 1e-7;

        var p1 = GetPointFunc(parameterValue1 - epsilon, parameterValue2);
        var p2 = GetPointFunc(parameterValue1 + epsilon, parameterValue2);

        var v1 = (p2 - p1) / (2 * epsilon);

        p1 = GetPointFunc(parameterValue1, parameterValue2 - epsilon);
        p2 = GetPointFunc(parameterValue1, parameterValue2 + epsilon);

        var v2 = (p2 - p1) / (2 * epsilon);

        return v1.VectorCross(v2);
    }

    public Float64Vector3D GetUnitNormal(double parameterValue1, double parameterValue2)
    {
        return GetNormal(parameterValue1, parameterValue2).ToUnitVector();
    }
        
    public GrParametricSurfaceLocalFrame3D GetFrame(double parameterValue1, double parameterValue2)
    {
        return new GrParametricSurfaceLocalFrame3D(this, parameterValue1, parameterValue2);
    }
}