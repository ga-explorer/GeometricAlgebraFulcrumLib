using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.SubSpaces.Space4D
{
    public sealed class LinFloat64PlaneSubspace4D :
        ILinFloat64Subspace4D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64PlaneSubspace4D CreateFromVectors(IFloat64Vector4D vector1, IFloat64Vector4D vector2)
        {
            var u = vector1.ToUnitVector();
            var v = vector2.RejectOnUnitVector(u).ToUnitVector();

            return new LinFloat64PlaneSubspace4D(u, v);

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64PlaneSubspace4D CreateFromUnitVectors(IFloat64Vector4D vector1, IFloat64Vector4D vector2)
        {
            return new LinFloat64PlaneSubspace4D(
                vector1.ToTuple4D(),
                vector2.RejectOnUnitVector(vector1).ToUnitVector()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64PlaneSubspace4D CreateFromOrthogonalVectors(IFloat64Vector4D vector1, IFloat64Vector4D vector2)
        {
            return new LinFloat64PlaneSubspace4D(
                vector1.ToUnitVector(),
                vector2.ToUnitVector()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64PlaneSubspace4D CreateFromOrthonormalVectors(IFloat64Vector4D vector1, IFloat64Vector4D vector2)
        {
            return new LinFloat64PlaneSubspace4D(
                vector1.ToTuple4D(),
                vector2.ToTuple4D()
            );
        }


        public int VSpaceDimensions
            => 4;

        public int SubspaceDimensions
            => 2;

        public IEnumerable<Float64Vector4D> BasisVectors
        {
            get
            {
                yield return BasisVector1;
                yield return BasisVector2;
            }
        }

        public Float64Vector4D BasisVector1 { get; }

        public Float64Vector4D BasisVector2 { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64PlaneSubspace4D(Float64Vector4D vector1, Float64Vector4D vector2)
        {
            Debug.Assert(
                vector1.IsNearOrthonormalWith(vector2)
            );

            BasisVector1 = vector1;
            BasisVector2 = vector2;
        }


        public bool NearContains(IFloat64Vector4D vector, double epsilon = 1E-12D)
        {
            if (vector.IsNearZero(epsilon))
                return true;

            // Project vector on subspace plane and compare with original vector

            var (xuDot, xvDot) = vector.ESp(BasisVector1, BasisVector2);

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
        public bool NearContains(ILinFloat64Subspace4D subspace, double epsilon = 1E-12)
        {
            return subspace.VSpaceDimensions <= VSpaceDimensions &&
                   subspace.BasisVectors.All(v => NearContains(v, epsilon));
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}