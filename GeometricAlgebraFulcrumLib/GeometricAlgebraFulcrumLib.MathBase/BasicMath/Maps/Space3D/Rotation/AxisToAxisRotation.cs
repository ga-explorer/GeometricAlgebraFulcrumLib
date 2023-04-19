using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Constants;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D.Rotation
{
    public sealed class AxisToAxisRotation3D :
        VectorToVectorRotationLinearMap3D
    {
        public Axis3D SourceAxis { get; }

        public Axis3D TargetAxis { get; }

        public override Float64Tuple3D SourceVector { get; }

        public override Float64Tuple3D TargetOrthogonalVector
            => TargetVector;

        public override Float64Tuple3D TargetVector { get; }

        public override double AngleCos
            => 0d;

        public override Float64PlanarAngle Angle
            => Float64PlanarAngle.Angle90;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AxisToAxisRotation3D(Axis3D uAxis, Axis3D vAxis)
        {
            Debug.Assert(
                uAxis != vAxis
            );

            SourceAxis = uAxis;
            TargetAxis = vAxis;

            SourceVector = SourceAxis.GetVector3D();
            TargetVector = TargetAxis.GetVector3D();

            Debug.Assert(
                !(SourceVector + TargetVector).IsExactZeroVector()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return !(SourceVector + TargetVector).IsExactZeroVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Tuple3D MapAxis(Axis3D axis)
        {
            var r = axis.GetComponent(TargetAxis);
            var s = axis.GetComponent(SourceAxis);

            return axis.GetVector3D() - (r + s) * SourceVector - (r - s) * TargetVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Tuple3D MapVector(IFloat64Tuple3D vector)
        {
            var r = vector.GetComponent(TargetAxis);
            var s = vector.GetComponent(SourceAxis);

            return vector - (r + s) * SourceVector - (r - s) * TargetVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override SimpleRotationLinearMap3D GetInverseSimpleVectorRotation()
        {
            return new AxisToAxisRotation3D(
                TargetAxis,
                SourceAxis
            );
        }
    }
}