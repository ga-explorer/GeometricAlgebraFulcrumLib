using System.Diagnostics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Constants;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space3D.Rotation;

public sealed class AxisToVectorRotation3D :
    VectorToVectorRotationLinearMap3D
{
    public Axis3D SourceAxis { get; }

    public override Float64Tuple3D SourceVector { get; }

    public override Float64Tuple3D TargetOrthogonalVector { get; }

    public override Float64Tuple3D TargetVector { get; }

    public override double AngleCos { get; }

    public override PlanarAngle Angle
        => AngleCos.ArcCos();


    public AxisToVectorRotation3D(Axis3D uAxis, IFloat64Tuple3D v)
    {
        SourceAxis = uAxis;
        SourceVector = SourceAxis.GetVector3D();
        TargetVector = v.ToUnitVector();

        AngleCos = TargetVector.GetComponent(SourceAxis);

        Debug.Assert(
            !(AngleCos + 1d).IsNearZero()
        );

        TargetOrthogonalVector = (TargetVector - AngleCos * SourceVector) / (1d + AngleCos);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return TargetVector.IsValid() &&
               !(AngleCos + 1d).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Tuple3D MapAxis(Axis3D axis)
    {
        var r = axis.VectorDot(TargetOrthogonalVector);
        var s = axis.GetComponent(SourceAxis);

        return axis.GetVector3D() - (r + s) * SourceVector - (r - s) * TargetVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Tuple3D MapVector(IFloat64Tuple3D vector)
    {
        var r = vector.VectorDot(TargetOrthogonalVector);
        var s = vector.GetComponent(SourceAxis);

        return vector - (r + s) * SourceVector - (r - s) * TargetVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override SimpleRotationLinearMap3D GetInverseSimpleVectorRotation()
    {
        return new VectorToAxisRotation3D(
            TargetVector,
            SourceAxis
        );
    }
}