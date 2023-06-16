using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation
{
    public sealed class LinFloat64OrthogonalVectorToVectorRotation4D :
        LinFloat64VectorToVectorRotationBase4D
    {
        public override Float64Vector4D SourceVector { get; }

        public override Float64Vector4D TargetOrthogonalVector
            => TargetVector;

        public override Float64Vector4D TargetVector { get; }

        public override double AngleCos
            => 0d;

        public override Float64PlanarAngle Angle
            => Float64PlanarAngle.Angle90;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64OrthogonalVectorToVectorRotation4D(IFloat64Tuple4D sourceVector, IFloat64Tuple4D targetVector)
        {
            Debug.Assert(
                sourceVector.IsNearUnit() &&
                targetVector.IsNearUnit() &&
                targetVector.IsNearOrthogonalTo(sourceVector)
            );

            SourceVector = sourceVector.ToTuple4D();
            TargetVector = targetVector.ToTuple4D();
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
        public override Float64Vector4D ProjectOnRotationPlane(Float64Vector4D vector)
        {
            var (xuDot, xvDot) = 
                vector.ESp(SourceVector, TargetVector);
            
            return Float64Vector4DComposer
                .Create()
                .SetVector(SourceVector, xuDot)
                .AddVector(TargetVector, xvDot)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector4D MapBasisVector(int basisIndex)
        {
            Debug.Assert(
                basisIndex >= 0
            );
            
            var r = TargetVector[basisIndex];
            var s = SourceVector[basisIndex];
            var rsPlus = r + s;
            var rsMinus = r - s;
            
            return Float64Vector4DComposer
                .Create()
                .SetVector(SourceVector, -rsPlus)
                .AddVector(TargetVector, -rsMinus)
                .AddTerm(basisIndex, 1d)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector4D MapVector(IFloat64Tuple4D vector)
        {
            var (r, s) = vector.ESp(TargetVector, SourceVector);
            var rsPlus = r + s;
            var rsMinus = r - s;
            
            return Float64Vector4DComposer
                .Create()
                .SetVector(vector)
                .AddVector(SourceVector, -rsPlus)
                .AddVector(TargetVector, -rsMinus)
                .GetVector();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector4D MapVectorProjection(Float64Vector4D vector)
        {
            var (r, s) = vector.ESp(TargetOrthogonalVector, SourceVector);
            
            return Float64Vector4DComposer
                .Create()
                .SetVector(TargetVector, s)
                .SubtractVector(SourceVector, r)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64SimpleRotationBase4D GetSimpleVectorRotationInverse()
        {
            return new LinFloat64OrthogonalVectorToVectorRotation4D(
                TargetVector,
                SourceVector
            );
        }
    }
}