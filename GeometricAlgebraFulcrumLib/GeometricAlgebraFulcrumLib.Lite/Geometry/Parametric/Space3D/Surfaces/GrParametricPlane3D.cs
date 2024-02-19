using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Surfaces;

public class GrParametricPlane3D :
    IGraphicsParametricSurface3D
{
    public Float64Vector3D Point { get; }

    public Float64Vector3D Vector1 { get; }

    public Float64Vector3D Vector2 { get; }

    public Float64Vector3D Normal { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricPlane3D(IFloat64Vector3D point, IFloat64Vector3D vector1, IFloat64Vector3D vector2)
    {
        Point = point.ToVector3D();
        Vector1 = vector1.ToVector3D();
        Vector2 = vector2.ToVector3D();
        Normal = Vector1.VectorUnitCross(Vector2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricPlane3D(IFloat64Vector3D point, IFloat64Vector3D normal)
    {
        Point = point.ToVector3D();
        Vector1 = normal.GetUnitNormal();
        Vector2 = normal.VectorUnitCross(Vector1);
        Normal = normal.ToUnitVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Point.IsValid() &&
               Vector1.IsValid() &&
               Vector2.IsValid() &&
               Normal.IsValid() &&
               Normal.IsNearUnitVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D GetPoint(double parameterValue1, double parameterValue2)
    {
        return Float64Vector3D.Create(Point.X + parameterValue1 * Vector1.X + parameterValue2 * Vector2.X,
            Point.Y + parameterValue1 * Vector1.Y + parameterValue2 * Vector2.Y,
            Point.Z + parameterValue1 * Vector1.Z + parameterValue2 * Vector2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D GetNormal(double parameterValue1, double parameterValue2)
    {
        return Normal;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D GetUnitNormal(double parameterValue1, double parameterValue2)
    {
        return Normal;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricSurfaceLocalFrame3D GetFrame(double parameterValue1, double parameterValue2)
    {
        return new GrParametricSurfaceLocalFrame3D(
            parameterValue1,
            parameterValue2,
            GetPoint(parameterValue1, parameterValue2),
            Normal
        );
    }
}