using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Angles;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Bivectors;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Quaternions;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space4D.Curves;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;

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