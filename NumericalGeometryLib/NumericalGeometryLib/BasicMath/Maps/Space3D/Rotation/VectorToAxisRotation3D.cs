using System.Diagnostics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Constants;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space3D.Rotation;

public sealed class VectorToAxisRotation3D :
    VectorToVectorRotationLinearMap3D
{
    public Axis3D TargetAxis { get; }

    public override Float64Tuple3D SourceVector { get; }

    public override Float64Tuple3D TargetOrthogonalVector { get; }

    public override Float64Tuple3D TargetVector { get; }

    public override double AngleCos { get; }

    public override PlanarAngle Angle
        => AngleCos.ArcCos();


    public VectorToAxisRotation3D(IFloat64Tuple3D u, Axis3D vAxis)
    {
        SourceVector = u.ToUnitVector();
        TargetAxis = vAxis;
        TargetVector = TargetAxis.GetVector3D();

        AngleCos = SourceVector.GetComponent(TargetAxis);

        Debug.Assert(
            !(AngleCos + 1d).IsNearZero()
        );

        TargetOrthogonalVector = (TargetVector - AngleCos * SourceVector) / (1d + AngleCos);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return SourceVector.IsValid() &&
               !(AngleCos + 1d).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Tuple3D MapAxis(Axis3D axis)
    {
        var r = axis.VectorDot(TargetOrthogonalVector);
        var s = axis.VectorDot(SourceVector);

        return axis.GetVector3D() - (r + s) * SourceVector - (r - s) * TargetVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Tuple3D MapVector(IFloat64Tuple3D vector)
    {
        var r = vector.VectorDot(TargetOrthogonalVector);
        var s = vector.VectorDot(SourceVector);

        return vector - (r + s) * SourceVector - (r - s) * TargetVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override SimpleRotationLinearMap3D GetInverseSimpleVectorRotation()
    {
        return new AxisToVectorRotation3D(
            TargetAxis,
            SourceVector
        );
    }
}