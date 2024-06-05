using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Angles;

public static class ParametricAngleUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricPolarAngle MapAngles(this IParametricPolarAngle angle, Func<LinFloat64PolarAngle, LinFloat64PolarAngle> angleMapping)
    {
        return ComputedParametricPolarAngle.Create(
            angle.ParameterRange,
            t => angleMapping(angle.GetAngle(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IParametricCurve3D CreatePolarCurve(this IParametricPolarAngle angle, double radius, LinFloat64Vector3D center, LinFloat64Vector3D direction1, LinFloat64Vector3D direction2)
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