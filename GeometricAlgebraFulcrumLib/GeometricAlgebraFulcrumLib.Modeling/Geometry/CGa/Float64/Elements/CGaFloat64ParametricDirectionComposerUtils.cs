using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public static class CGaFloat64ParametricDirectionComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineDirectionLine(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            vectorCurve.TimeRange,
            t => cgaGeometricSpace.DefineDirectionLine(
                vectorCurve.GetValue(t).ToXGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineDirectionLine(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineDirectionLine(
                vectorCurve.GetValue(t).ToXGaFloat64Vector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineDirectionPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            bivectorCurve.TimeRange,
            t => cgaGeometricSpace.DefineDirectionPlane(
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineDirectionPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineDirectionPlane(
                bivectorCurve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineDirectionPlaneFromNormal(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            normalCurve.TimeRange,
            t => cgaGeometricSpace.DefineDirectionPlane(
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineDirectionPlaneFromNormal(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineDirectionPlane(
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }


}