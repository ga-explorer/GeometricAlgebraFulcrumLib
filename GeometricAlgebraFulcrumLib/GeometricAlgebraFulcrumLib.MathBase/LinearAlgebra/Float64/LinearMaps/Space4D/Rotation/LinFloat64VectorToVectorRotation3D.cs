using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation
{
    public sealed class LinFloat64VectorToVectorRotation4D :
        LinFloat64VectorToVectorRotationBase4D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotation4D CreateIdentity()
        {
            var u = Float64Vector4D.E1;

            return new LinFloat64VectorToVectorRotation4D(u, u);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotation4D CreateIdentity(IFloat64Vector4D sourceVector)
        {
            return new LinFloat64VectorToVectorRotation4D(
                sourceVector.ToTuple4D(), 
                sourceVector.ToTuple4D()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotation4D Create(IFloat64Vector4D sourceVector, IFloat64Vector4D targetVector)
        {
            return new LinFloat64VectorToVectorRotation4D(
                sourceVector.ToTuple4D(), 
                targetVector.ToTuple4D()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotation4D Create(Float64Vector4D sourceVector, Float64Vector4D targetVector, Float64PlanarAngle angle)
        {
            if (angle.Radians.IsNearZero())
                return new LinFloat64VectorToVectorRotation4D(sourceVector, sourceVector);

            // Compute a rotated version of v in the u-v rotational plane by the given angle
            var vFinal = sourceVector.RotateToUnitVector(targetVector, angle);

            return new LinFloat64VectorToVectorRotation4D(sourceVector, vFinal);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotation4D CreateFromComplexEigenPair(Complex eigenValue, MathNet.Numerics.LinearAlgebra.Vector<Complex> eigenVector)
        {
            var angle = Math.Atan2(
                eigenValue.Imaginary,
                eigenValue.Real
            );

            //TODO: Why is this the correct one, but not the reverse??!!
            var u = eigenVector.Imaginary().ToTuple4D();
            var v = eigenVector.Real().ToTuple4D();

            return Create(u, v, angle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotation4D CreateFromComplexEigenPair(double realValue, double imagValue, double[] realVector, double[] imagVector)
        {
            var angle = Math.Atan2(
                imagValue,
                realValue
            );

            //TODO: Why is this the correct one, but not the reverse??!!
            var u = imagVector.ToTuple4D();
            var v = realVector.ToTuple4D();

            return Create(u, v, angle);
        }


        public override Float64Vector4D SourceVector { get; }

        public override Float64Vector4D TargetOrthogonalVector { get; }

        public override Float64Vector4D TargetVector { get; }

        public override double AngleCos { get; }

        public override Float64PlanarAngle Angle
            => AngleCos.ArcCos();


        internal LinFloat64VectorToVectorRotation4D(Triplet<double[]> rotationVectors)
        {
            var (sourceVector, targetOrthogonalVector, targetVector) = 
                rotationVectors;

            Debug.Assert(
                sourceVector.Length == targetVector.Length &&
                sourceVector.GetVectorNormSquared().IsNearOne() &&
                targetVector.GetVectorNormSquared().IsNearOne()
            );

            SourceVector = sourceVector.ToTuple4D();
            TargetVector = targetVector.ToTuple4D();

            AngleCos = TargetVector.ESp(SourceVector).Clamp(-1d, 1d);

            Debug.Assert(
                !AngleCos.IsNearMinusOne()
            );

            TargetOrthogonalVector = targetOrthogonalVector.ToTuple4D();

            Debug.Assert(
                (TargetOrthogonalVector - (TargetVector - AngleCos * SourceVector) / (1d + AngleCos)).ENormSquared().IsNearZero()
            );
        }

        private LinFloat64VectorToVectorRotation4D(Float64Vector4D sourceVector, Float64Vector4D targetVector)
        {
            Debug.Assert(
                sourceVector.IsNearUnit() &&
                targetVector.IsNearUnit()
            );

            SourceVector = sourceVector;
            TargetVector = targetVector;

            AngleCos = TargetVector.ESp(SourceVector).Clamp(-1d, 1d);

            Debug.Assert(
                !AngleCos.IsNearMinusOne()
            );

            TargetOrthogonalVector = (TargetVector - AngleCos * SourceVector) / (1d + AngleCos);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return
                SourceVector.IsNearUnit() &&
                TargetVector.IsNearUnit() &&
                !AngleCos.IsNearMinusOne();
        }
        
        public override Float64Vector4D ProjectOnRotationPlane(Float64Vector4D vector)
        {
            var (xuDot, xvDot) = vector.ESp(SourceVector, TargetVector);
            var bivectorNormSquaredInv = 1d / (1d - AngleCos * AngleCos);

            var uScalar = (xuDot - xvDot * AngleCos) * bivectorNormSquaredInv;
            var vScalar = (xvDot - xuDot * AngleCos) * bivectorNormSquaredInv;
            
            return Float64Vector4DComposer
                .Create()
                .SetVector(SourceVector, uScalar)
                .AddVector(TargetVector, vScalar)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector4D MapBasisVector(int basisIndex)
        {
            Debug.Assert(
                basisIndex >= 0
            );

            //var r = vector.ESp(TargetOrthogonalVector);
            //var s = vector.ESp(SourceVector);

            //return vector - (r + s) * SourceVector - (r - s) * TargetVector;
            
            var r = TargetOrthogonalVector[basisIndex];
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
        public override Float64Vector4D MapVector(IFloat64Vector4D vector)
        {
            //var r = vector.ESp(TargetOrthogonalVector);
            //var s = vector.ESp(SourceVector);

            //return vector - (r + s) * SourceVector - (r - s) * TargetVector;
            
            var (r, s) = vector.ESp(TargetOrthogonalVector, SourceVector);
            var rsPlus = r + s;
            var rsMinus = r - s;
            
            return Float64Vector4DComposer
                .Create()
                .SetVector(vector)
                .AddVector(SourceVector, -rsPlus)
                .AddVector(TargetVector, -rsMinus)
                .GetVector();
        }

        public override Float64Vector4D MapVectorProjection(Float64Vector4D vector)
        {
            var (r, s) = vector.ESp(TargetOrthogonalVector, SourceVector);
            
            var uScalar = r / (AngleCos - 1d);
            var vScalar = s - uScalar * AngleCos;
            
            return Float64Vector4DComposer
                .Create()
                .SetVector(SourceVector, uScalar)
                .AddVector(TargetVector, vScalar)
                .GetVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorToVectorRotation4D GetVectorToVectorRotationInverse()
        {
            return new LinFloat64VectorToVectorRotation4D(
                TargetVector,
                SourceVector
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64SimpleRotationBase4D GetSimpleVectorRotationInverse()
        {
            return GetVectorToVectorRotationInverse();
        }
    }
}
