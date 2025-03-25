using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Trivectors3D.Float64;

public sealed class LinFloat64Trivector3DConstantTrajectory :
    LinFloat64Trivector3DTrajectory
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3DConstantTrajectory Create(Float64ScalarRange timeRange, bool isPeriodic, LinFloat64Trivector3D point)
    {
        return new LinFloat64Trivector3DConstantTrajectory(
            timeRange,
            isPeriodic,
            point,
            LinFloat64Trivector3D.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3DConstantTrajectory Create(Float64ScalarRange timeRange, bool isPeriodic, LinFloat64Trivector3D point, LinFloat64Trivector3D tangent)
    {
        return new LinFloat64Trivector3DConstantTrajectory(
            timeRange,
            isPeriodic,
            point,
            tangent
        );
    }


    public LinFloat64Trivector3D Point { get; }

    public LinFloat64Trivector3D Tangent { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64Trivector3DConstantTrajectory(Float64ScalarRange timeRange, bool isPeriodic, LinFloat64Trivector3D point, LinFloat64Trivector3D tangent)
        : base(timeRange, isPeriodic)
    {
        Point = point;
        Tangent = tangent;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return Point.IsValid() &&
               Tangent.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Trivector3D GetValue(double parameterValue)
    {
        return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Trivector3D GetDerivative1Value(double parameterValue)
    {
        return Tangent;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Trivector3D GetDerivative2Value(double parameterValue)
    {
        return LinFloat64Trivector3D.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal GetDualScalarCurve()
    {
        return Float64ScalarSignal.FiniteConstant(
            TimeRange, 
            Point.Dual3D().Scalar.ScalarValue
        );
    }
}