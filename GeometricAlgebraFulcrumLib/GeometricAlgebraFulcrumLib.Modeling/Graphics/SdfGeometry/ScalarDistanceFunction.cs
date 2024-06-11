using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.ParametricShapes.Volumes;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.SdfGeometry;

public abstract class ScalarDistanceFunction : 
    ISdfGeometry3D
{
    private double _distanceDeltaInv = 1 << 13;
    private double _distanceDelta = 1.0d / (1 << 13);
        

    public double SdfDistanceDelta
    {
        get => _distanceDelta;
        set
        {
            _distanceDelta = value;
            _distanceDeltaInv = 1 / value;
        }
    }

    public double SdfDistanceDeltaInv
    {
        get => _distanceDeltaInv;
        set
        {
            _distanceDeltaInv = value;
            _distanceDelta = 1 / value;
        }
    }

    public double SdfAlpha { get; set; }
        = 1.0d;

    public double SdfDelta { get; set; } 
        = 0.01d;

        

    public bool IsValid()
    {
        throw new NotImplementedException();
    }

    public LinFloat64Vector3D GetPoint(ILinFloat64Vector3D parameterValue)
    {
        return parameterValue.ToLinVector3D();
    }

    public LinFloat64Vector3D GetPoint(double parameterValue1, double parameterValue2, double parameterValue3)
    {
        return LinFloat64Vector3D.Create(parameterValue1, parameterValue2, parameterValue3);
    }

    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public abstract double GetScalarDistance(ILinFloat64Vector3D point);

    public double GetScalarDistance(double parameterValue1, double parameterValue2, double parameterValue3)
    {
        return GetScalarDistance(
            LinFloat64Vector3D.Create(parameterValue1, parameterValue2, parameterValue3)
        );
    }

    public GrParametricVolumeLocalFrame3D GetFrame(ILinFloat64Vector3D parameterValue)
    {
        return new GrParametricVolumeLocalFrame3D(
            parameterValue,
            parameterValue,
            GetScalarDistance(parameterValue)
        );
    }

    public GrParametricVolumeLocalFrame3D GetFrame(double parameterValue1, double parameterValue2, double parameterValue3)
    {
        return new GrParametricVolumeLocalFrame3D(
            parameterValue1,
            parameterValue2,
            parameterValue3,
            LinFloat64Vector3D.Create(parameterValue1, parameterValue2, parameterValue3),
            GetScalarDistance(parameterValue1, parameterValue2, parameterValue3)
        );
    }

    /// <summary>
    /// https://en.wikipedia.org/wiki/Newton%27s_method
    /// </summary>
    /// <param name="ray"></param>
    /// <param name="t0"></param>
    /// <returns></returns>
    public virtual double ComputeSdfRayStep(Line3D ray, double t0)
    {
        var f0 = GetScalarDistance(ray.GetPointAt(t0));
        var f1 = GetScalarDistance(ray.GetPointAt(t0 + _distanceDelta));

        return _distanceDelta * f0 / (f0 - f1);
    }

    /// <summary>
    /// http://iquilezles.org/www/articles/normalsSDF/normalsSDF.htm
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public virtual LinFloat64Vector3D ComputeSdfNormal(ILinFloat64Vector3D point)
    {
        var d1 = GetScalarDistance(LinFloat64Vector3D.Create(point.X + SdfDistanceDelta,
            point.Y - SdfDistanceDelta,
            point.Z - SdfDistanceDelta));

        var d2 = GetScalarDistance(LinFloat64Vector3D.Create(point.X - SdfDistanceDelta,
            point.Y - SdfDistanceDelta,
            point.Z + SdfDistanceDelta));

        var d3 = GetScalarDistance(LinFloat64Vector3D.Create(point.X - SdfDistanceDelta,
            point.Y + SdfDistanceDelta,
            point.Z - SdfDistanceDelta));

        var d4 = GetScalarDistance(LinFloat64Vector3D.Create(point.X + SdfDistanceDelta,
            point.Y + SdfDistanceDelta,
            point.Z + SdfDistanceDelta));


        return LinFloat64Vector3D.CreateUnitVector(
            d4 + d1 - d2 - d3,
            d4 - d1 - d2 + d3,
            d4 - d1 + d2 - d3
        );
    }
}