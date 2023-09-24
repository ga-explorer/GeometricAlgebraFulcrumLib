using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.SubSpaces.SpaceND
{
    public sealed class LinFloat64PlaneSubspace :
        ILinFloat64Subspace
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64PlaneSubspace CreateFromSpanningVectors(Float64Vector vector1, Float64Vector vector2)
        {
            var u = vector1.ToUnitVector();
            var v = vector2.RejectOnUnitVector(u).DivideByENorm();

            return new LinFloat64PlaneSubspace(u, v);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64PlaneSubspace CreateFromUnitVectors(Float64Vector vector1, Float64Vector vector2)
        {
            return new LinFloat64PlaneSubspace(
                vector1,
                vector2.RejectOnUnitVector(vector1).DivideByENorm()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64PlaneSubspace CreateFromOrthogonalVectors(Float64Vector vector1, Float64Vector vector2)
        {
            return new LinFloat64PlaneSubspace(
                vector1.ToUnitVector(),
                vector2.ToUnitVector()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64PlaneSubspace CreateFromOrthonormalVectors(Float64Vector vector1, Float64Vector vector2)
        {
            return new LinFloat64PlaneSubspace(
                vector1,
                vector2
            );
        }


        public int VSpaceDimensions
            => BasisVector1.VSpaceDimensions;

        public int SubspaceDimensions
            => 2;

        public IEnumerable<Float64Vector> BasisVectors
        {
            get
            {
                yield return BasisVector1;
                yield return BasisVector2;
            }
        }

        public Float64Vector BasisVector1 { get; }

        public Float64Vector BasisVector2 { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64PlaneSubspace(Float64Vector vector1, Float64Vector vector2)
        {
            BasisVector1 = vector1;
            BasisVector2 = vector2;

            Debug.Assert(IsValid());
        }


        public bool NearContains(Float64Vector vector, double epsilon = 1E-12D)
        {
            if (vector.IsNearZero(epsilon))
                return true;

            // Project vector on subspace plane and compare with original vector

            var (xuDot, xvDot) = vector.VectorDot(BasisVector1, BasisVector2);

            var diffNorm = (vector - (xuDot * BasisVector1 + xvDot * BasisVector2)).ENormSquared();

            return diffNorm < epsilon;

            //var rank = Matrix<double>.Build.DenseOfColumnArrays(
            //    vector,
            //    BasisVector1,
            //    BasisVector2
            //).Rank();

            //Debug.Assert(
            //    rank is 2 or 3
            //);

            //return rank == 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NearContains(ILinFloat64Subspace subspace, double epsilon = 1E-12)
        {
            return subspace.VSpaceDimensions <= VSpaceDimensions &&
                   subspace.BasisVectors.All(v => NearContains(v, epsilon));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return BasisVector1.IsValid() &&
                   BasisVector2.IsValid() &&
                   BasisVector1.IsNearOrthonormalWith(BasisVector2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector GetVectorProjection(Float64Vector vector)
        {
            var (u1Dot, u2Dot) = 
                vector.VectorDot(BasisVector1, BasisVector2);

            return Float64VectorComposer
                .Create()
                .SetVector(BasisVector1, u1Dot)
                .AddVector(BasisVector2, u2Dot)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64PlanarAngle GetVectorProjectionPolarAngle(Float64Vector vector)
        {
            var (u1Dot, u2Dot) = 
                vector.VectorDot(BasisVector1, BasisVector2);

            return Math.Atan2(u2Dot, u1Dot).RadiansToAngle().GetAngleInPositiveRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector GetVectorRejection(Float64Vector vector)
        {
            var (u1Dot, u2Dot) = 
                vector.VectorDot(BasisVector1, BasisVector2);

            return Float64VectorComposer
                .Create()
                .SetVector(vector)
                .AddVector(BasisVector1, -u1Dot)
                .AddVector(BasisVector2, -u2Dot)
                .GetVector();
        }
    }
}