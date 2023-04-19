using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Arrays.Float64;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Rotation
{
    public sealed class LinFloat64OrthogonalVectorToVectorRotation :
        LinFloat64VectorToVectorRotationBase
    {
        public override LinFloat64Vector SourceVector { get; }

        public override LinFloat64Vector TargetOrthogonalVector
            => TargetVector;

        public override LinFloat64Vector TargetVector { get; }

        public override double AngleCos
            => 0d;

        public override Float64PlanarAngle Angle
            => Float64PlanarAngle.Angle90;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64OrthogonalVectorToVectorRotation(LinFloat64Vector sourceVector, LinFloat64Vector targetVector)
        {
            Debug.Assert(
                sourceVector.IsNearUnit() &&
                targetVector.IsNearUnit() &&
                targetVector.IsNearOrthogonalTo(sourceVector)
            );

            SourceVector = sourceVector;
            TargetVector = targetVector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return
                SourceVector.IsNearUnit() &&
                TargetVector.IsNearUnit() &&
                TargetVector.IsNearOrthogonalTo(SourceVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsIdentity()
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsNearIdentity(double epsilon = 1e-12d)
        {
            return false;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Vector ProjectOnRotationPlane(LinFloat64Vector vector)
        {
            var (xuDot, xvDot) = vector.VectorDot(SourceVector, TargetVector);
            
            return LinFloat64VectorComposer
                .Create()
                .SetVector(SourceVector, xuDot)
                .AddVector(TargetVector, xvDot)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Vector MapBasisVector(int basisIndex)
        {
            Debug.Assert(
                basisIndex >= 0
            );
            
            var r = TargetVector[basisIndex];
            var s = SourceVector[basisIndex];
            var rsPlus = r + s;
            var rsMinus = r - s;
            
            return LinFloat64VectorComposer
                .Create()
                .SetVector(SourceVector, -rsPlus)
                .AddVector(TargetVector, -rsMinus)
                .AddTerm(basisIndex, 1d)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Vector MapVector(LinFloat64Vector vector)
        {
            var (r, s) = vector.VectorDot(TargetVector, SourceVector);
            var rsPlus = r + s;
            var rsMinus = r - s;
            
            return LinFloat64VectorComposer
                .Create()
                .SetVector(vector)
                .AddVector(SourceVector, -rsPlus)
                .AddVector(TargetVector, -rsMinus)
                .GetVector();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Vector MapVectorProjection(LinFloat64Vector vector)
        {
            var (r, s) = vector.VectorDot(TargetOrthogonalVector, SourceVector);
            
            return LinFloat64VectorComposer
                .Create()
                .SetVector(TargetVector, s)
                .SubtractVector(SourceVector, r)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64SimpleRotationBase GetSimpleVectorRotationInverse()
        {
            return new LinFloat64OrthogonalVectorToVectorRotation(
                TargetVector,
                SourceVector
            );
        }
    }
}