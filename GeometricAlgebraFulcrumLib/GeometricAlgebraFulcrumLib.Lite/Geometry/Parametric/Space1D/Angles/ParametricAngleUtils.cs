using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Angles;

public static class ParametricAngleUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricAngle MapAngles(this IParametricAngle angle, Func<Float64PlanarAngle, Float64PlanarAngle> angleMapping)
    {
        return ComputedParametricAngle.Create(
            angle.ParameterRange,
            t => angleMapping(angle.GetAngle(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IParametricCurve3D CreatePolarCurve(this IParametricAngle angle, double radius, Float64Vector3D center, Float64Vector3D direction1, Float64Vector3D direction2)
    {
        return ComputedParametricCurve3D.Create(
            angle.ParameterRange,
            t =>
            {
                var a = angle.GetAngle(t);

                return center + radius * (a.Cos() * direction1 + a.Sin() * direction2);
            }
        );
    }
}