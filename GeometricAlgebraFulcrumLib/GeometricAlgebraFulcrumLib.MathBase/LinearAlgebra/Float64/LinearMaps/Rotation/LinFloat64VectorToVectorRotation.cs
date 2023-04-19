using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Arrays.Float64;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Rotation
{
    public sealed class LinFloat64VectorToVectorRotation :
        LinFloat64VectorToVectorRotationBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotation CreateIdentity()
        {
            var u = 0.CreateLinVector();

            return new LinFloat64VectorToVectorRotation(u, u);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotation CreateIdentity(ILinSignedBasisVector basisVector)
        {
            var u = basisVector.ToVector();

            return new LinFloat64VectorToVectorRotation(u, u);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotation CreateIdentity(LinFloat64Vector sourceVector)
        {
            return new LinFloat64VectorToVectorRotation(sourceVector, sourceVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotation Create(LinFloat64Vector sourceVector, LinFloat64Vector targetVector)
        {
            return new LinFloat64VectorToVectorRotation(sourceVector, targetVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotation Create(LinFloat64Vector sourceVector, LinFloat64Vector targetVector, Float64PlanarAngle angle)
        {
            if (angle.Radians.IsNearZero())
                return new LinFloat64VectorToVectorRotation(sourceVector, sourceVector);

            // Compute a rotated version of v in the u-v rotational plane by the given angle
            var vFinal = sourceVector.RotateToUnitVector(targetVector, angle);

            return new LinFloat64VectorToVectorRotation(sourceVector, vFinal);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotation CreateFromComplexEigenPair(Complex eigenValue, MathNet.Numerics.LinearAlgebra.Vector<Complex> eigenVector)
        {
            var angle = Math.Atan2(
                eigenValue.Imaginary,
                eigenValue.Real
            );

            //TODO: Why is this the correct one, but not the reverse??!!
            var u = eigenVector.Imaginary().ToArray().CreateUnitLinVector();
            var v = eigenVector.Real().ToArray().CreateUnitLinVector();

            return Create(u, v, angle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotation CreateFromComplexEigenPair(double realValue, double imagValue, double[] realVector, double[] imagVector)
        {
            var angle = Math.Atan2(
                imagValue,
                realValue
            );

            //TODO: Why is this the correct one, but not the reverse??!!
            var u = imagVector.CreateUnitLinVector();
            var v = realVector.CreateUnitLinVector();

            return Create(u, v, angle);
        }


        public override LinFloat64Vector SourceVector { get; }

        public override LinFloat64Vector TargetOrthogonalVector { get; }

        public override LinFloat64Vector TargetVector { get; }

        public override double AngleCos { get; }

        public override Float64PlanarAngle Angle
            => AngleCos.ArcCos();


        internal LinFloat64VectorToVectorRotation(Triplet<double[]> rotationVectors)
        {
            var (sourceVector, targetOrthogonalVector, targetVector) = 
                rotationVectors;

            Debug.Assert(
                sourceVector.Length == targetVector.Length &&
                sourceVector.GetVectorNormSquared().IsNearOne() &&
                targetVector.GetVectorNormSquared().IsNearOne()
            );

            SourceVector = sourceVector.CreateLinVector();
            TargetVector = targetVector.CreateLinVector();

            AngleCos = TargetVector.VectorDot(SourceVector).Clamp(-1d, 1d);

            Debug.Assert(
                !AngleCos.IsNearMinusOne()
            );

            TargetOrthogonalVector = targetOrthogonalVector.CreateLinVector();

            Debug.Assert(
                (TargetOrthogonalVector - (TargetVector - AngleCos * SourceVector) / (1d + AngleCos)).ENormSquared().IsNearZero()
            );
        }

        private LinFloat64VectorToVectorRotation(LinFloat64Vector sourceVector, LinFloat64Vector targetVector)
        {
            Debug.Assert(
                sourceVector.IsNearUnit() &&
                targetVector.IsNearUnit()
            );

            SourceVector = sourceVector;
            TargetVector = targetVector;

            AngleCos = TargetVector.VectorDot(SourceVector).Clamp(-1d, 1d);

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
        
        public override LinFloat64Vector ProjectOnRotationPlane(LinFloat64Vector vector)
        {
            var (xuDot, xvDot) = vector.VectorDot(SourceVector, TargetVector);
            var bivectorNormSquaredInv = 1d / (1d - AngleCos * AngleCos);

            var uScalar = (xuDot - xvDot * AngleCos) * bivectorNormSquaredInv;
            var vScalar = (xvDot - xuDot * AngleCos) * bivectorNormSquaredInv;
            
            return LinFloat64VectorComposer
                .Create()
                .SetVector(SourceVector, uScalar)
                .AddVector(TargetVector, vScalar)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Vector MapBasisVector(int basisIndex)
        {
            Debug.Assert(
                basisIndex >= 0
            );

            //var r = vector.VectorDot(TargetOrthogonalVector);
            //var s = vector.VectorDot(SourceVector);

            //return vector - (r + s) * SourceVector - (r - s) * TargetVector;
            
            var r = TargetOrthogonalVector[basisIndex];
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
                .AddVector(TargetVector, -rsMinus)
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
                .AddVector(TargetVector, vScalar)
                .GetVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorToVectorRotation GetVectorToVectorRotationInverse()
        {
            return new LinFloat64VectorToVectorRotation(
                TargetVector,
                SourceVector
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64SimpleRotationBase GetSimpleVectorRotationInverse()
        {
            return GetVectorToVectorRotationInverse();
        }
    }
}
