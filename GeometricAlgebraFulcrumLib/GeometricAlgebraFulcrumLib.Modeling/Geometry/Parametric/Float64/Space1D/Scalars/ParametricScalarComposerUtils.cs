using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Angles;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Bivectors;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Quaternions;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space4D.Curves;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;

public static class ParametricScalarComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricScalar ToParametricScalar(this IFloat64ParametricScalar curve, Func<double, double> vectorMapping)
    {
        return ComputedParametricScalar.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricScalar ToParametricScalar(this IParametricPolarAngle curve, Func<LinFloat64Angle, double> vectorMapping)
    {
        return ComputedParametricScalar.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetAngle(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricScalar ToParametricScalar(this IFloat64ParametricCurve2D curve, Func<LinFloat64Vector2D, double> vectorMapping)
    {
        return ComputedParametricScalar.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetPoint(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricScalar ToParametricScalar(this IParametricCurve3D curve, Func<LinFloat64Vector3D, double> vectorMapping)
    {
        return ComputedParametricScalar.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetPoint(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricScalar ToParametricScalar(this IParametricBivector3D curve, Func<LinFloat64Bivector3D, double> vectorMapping)
    {
        return ComputedParametricScalar.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetBivector(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricScalar ToParametricScalar(this IParametricCurve4D curve, Func<LinFloat64Vector4D, double> vectorMapping)
    {
        return ComputedParametricScalar.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetPoint(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricScalar ToParametricScalar(this IParametricQuaternion curve, Func<LinFloat64Quaternion, double> vectorMapping)
    {
        return ComputedParametricScalar.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetQuaternion(t))
        );
    }




}