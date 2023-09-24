using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Angles;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Bivectors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Quaternions;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space4D.Curves;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;

public static class ParametricAngleComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricAngle ToParametricAngle(this IParametricScalar curve, Func<double, Float64PlanarAngle> vectorMapping)
    {
        return ComputedParametricAngle.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetValue(t))
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricAngle ToParametricAngle(this IParametricAngle curve, Func<Float64PlanarAngle, Float64PlanarAngle> vectorMapping)
    {
        return ComputedParametricAngle.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetAngle(t))
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricAngle ToParametricAngle(this IParametricCurve2D curve, Func<Float64Vector2D, Float64PlanarAngle> vectorMapping)
    {
        return ComputedParametricAngle.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetPoint(t))
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricAngle ToParametricAngle(this IParametricCurve3D curve, Func<Float64Vector3D, Float64PlanarAngle> vectorMapping)
    {
        return ComputedParametricAngle.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetPoint(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricAngle ToParametricAngle(this IParametricBivector3D curve, Func<Float64Bivector3D, Float64PlanarAngle> vectorMapping)
    {
        return ComputedParametricAngle.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetBivector(t))
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricAngle ToParametricAngle(this IParametricCurve4D curve, Func<Float64Vector4D, Float64PlanarAngle> vectorMapping)
    {
        return ComputedParametricAngle.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetPoint(t))
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricAngle ToParametricAngle(this IParametricQuaternion curve, Func<Float64Quaternion, Float64PlanarAngle> vectorMapping)
    {
        return ComputedParametricAngle.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetQuaternion(t))
        );
    }

    
        

}