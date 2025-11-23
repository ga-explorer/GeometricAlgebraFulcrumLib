using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;

public partial class CGaFloat64GeometricSpace
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement DirectionLine(Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            vectorCurve.TimeRange,
            t => DirectionLine(
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement DirectionLine(Float64ScalarRange parameterRange, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => DirectionLine(
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement DirectionPlane(IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            bivectorCurve.TimeRange,
            t => DirectionPlane(
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement DirectionPlane(Float64ScalarRange parameterRange, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => DirectionPlane(
                bivectorCurve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement DirectionPlaneFromNormal(Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            normalCurve.TimeRange,
            t => DirectionPlane(
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement DirectionPlaneFromNormal(Float64ScalarRange parameterRange, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => DirectionPlane(
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }


}