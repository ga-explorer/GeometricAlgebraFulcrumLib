using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Arrays.Float64;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Rotation
{
    public sealed class LinFloat64VectorToAxisRotation :
        LinFloat64VectorToVectorRotationBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToAxisRotation Create(LinFloat64Vector u, LinSignedBasisVector vAxis)
        {
            return new LinFloat64VectorToAxisRotation(
                u,
                vAxis.Index,
                vAxis.IsNegative
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToAxisRotation Create(LinFloat64Vector u, int vAxisIndex, bool vAxisNegative)
        {
            return new LinFloat64VectorToAxisRotation(
                u,
                vAxisIndex,
                vAxisNegative
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToAxisRotation CreateToPositiveAxis(LinFloat64Vector u, int vAxisIndex)
        {
            return new LinFloat64VectorToAxisRotation(
                u,
                vAxisIndex,
                false
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToAxisRotation CreateToNegativeAxis(LinFloat64Vector u, int vAxisIndex)
        {
            return new LinFloat64VectorToAxisRotation(
                u,
                vAxisIndex,
                true
            );
        }


        public LinSignedBasisVector TargetAxis { get; }

        public override LinFloat64Vector SourceVector { get; }

        public override LinFloat64Vector TargetOrthogonalVector { get; }

        public override LinFloat64Vector TargetVector { get; }

        public override double AngleCos { get; }

        public override Float64PlanarAngle Angle
            => AngleCos.ArcCos();


        private LinFloat64VectorToAxisRotation(LinFloat64Vector sourceVector, int targetAxisIndex, bool targetAxisNegative)
        {
            Debug.Assert(
                sourceVector.IsNearUnit()
            );

            SourceVector = sourceVector;
            TargetAxis = new LinSignedBasisVector(targetAxisIndex, targetAxisNegative);
            TargetVector = TargetAxis.ToVector();

            AngleCos = SourceVector.VectorDot(TargetAxis).Clamp(-1d, 1d);

            Debug.Assert(
                !AngleCos.IsNearMinusOne()
            );

            //TargetOrthogonalVector = (TargetVector - AngleCos * SourceVector) / (1d + AngleCos);

            TargetOrthogonalVector =
                LinFloat64VectorComposer
                    .Create()
                    .SetVector(SourceVector, -AngleCos)
                    .AddTerm(TargetAxis.Index, TargetAxis.Sign.ToFloat64())
                    .Times(1d / (1d + AngleCos))
                    .GetVector();
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return
                SourceVector.IsNearUnit() &&
                !AngleCos.IsNearMinusOne();
        }
    
        public override LinFloat64Vector ProjectOnRotationPlane(LinFloat64Vector vector)
        {
            var xuDot = vector.VectorDot(SourceVector);
            var xvDot = vector.VectorDot(TargetAxis);
            var bivectorNormSquaredInv = 1d / (1d - AngleCos * AngleCos);

            var uScalar = (xuDot - xvDot * AngleCos) * bivectorNormSquaredInv;
            var vScalar = (xvDot - xuDot * AngleCos) * bivectorNormSquaredInv;

            return LinFloat64VectorComposer
                .Create()
                .SetVector(SourceVector, uScalar)
                .AddTerm(TargetAxis.Index, TargetAxis.IsNegative ? -vScalar : vScalar)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Vector MapBasisVector(int basisIndex)
        {
            Debug.Assert(
                basisIndex >= 0
            );

            var r = TargetOrthogonalVector[basisIndex];
            var s = SourceVector[basisIndex];
            var rsPlus = r + s;
            var rsMinus = r - s;

            return LinFloat64VectorComposer
                .Create()
                .SetVector(SourceVector, -rsPlus)
                .AddTerm(basisIndex, 1d)
                .SubtractTerm(TargetAxis.Index, TargetAxis.IsNegative ? -rsMinus : rsMinus)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Vector MapVector(LinFloat64Vector vector)
        {
            //var r = vector.VectorDot(TargetOrthogonalVector);
            //var s = vector.VectorDot(SourceVector);

            //return vector - (r + s) * SourceVector - (r - s) * TargetVector;
            
            var (r, s) = vector.VectorDot(TargetOrthogonalVector, SourceVector);
            var rsPlus = r + s;
            var rsMinus = r - s;
        
            return LinFloat64VectorComposer
                .Create()
                .SetVector(vector)
                .AddVector(SourceVector, -rsPlus)
                .SubtractTerm(TargetAxis.Index, TargetAxis.IsNegative ? -rsMinus : rsMinus)
                .GetVector();
        }
    
        public override LinFloat64Vector MapVectorProjection(LinFloat64Vector vector)
        {
            var (r, s) = vector.VectorDot(TargetOrthogonalVector, SourceVector);
            
            var uScalar = r / (AngleCos - 1d);
            var vScalar = s - uScalar * AngleCos;

            return LinFloat64VectorComposer
                .Create()
                .SetVector(SourceVector, uScalar)
                .AddTerm(TargetAxis.Index, TargetAxis.IsNegative ? -vScalar : vScalar)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64SimpleRotationBase GetSimpleVectorRotationInverse()
        {
            return new LinFloat64AxisToVectorRotation(
                TargetAxis.Index,
                TargetAxis.IsNegative,
                SourceVector
            );
        }
    }
}