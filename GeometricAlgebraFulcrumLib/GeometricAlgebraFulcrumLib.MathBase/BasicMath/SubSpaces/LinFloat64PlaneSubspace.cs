using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Arrays.Float64;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.SubSpaces
{
    public sealed class LinFloat64PlaneSubspace :
        ILinFloat64Subspace
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64PlaneSubspace CreateFromVectors(LinFloat64Vector vector1, LinFloat64Vector vector2)
        {
            var u = vector1.ToUnitVector();
            var v = vector2.RejectOnUnitVector(u).DivideByENorm();
        
            return new LinFloat64PlaneSubspace(u, v);
        
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64PlaneSubspace CreateFromUnitVectors(LinFloat64Vector vector1, LinFloat64Vector vector2)
        {
            return new LinFloat64PlaneSubspace(
                vector1, 
                vector2.RejectOnUnitVector(vector1).DivideByENorm()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64PlaneSubspace CreateFromOrthogonalVectors(LinFloat64Vector vector1, LinFloat64Vector vector2)
        {
            return new LinFloat64PlaneSubspace(
                vector1.ToUnitVector(), 
                vector2.ToUnitVector()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64PlaneSubspace CreateFromOrthonormalVectors(LinFloat64Vector vector1, LinFloat64Vector vector2)
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

        public IEnumerable<LinFloat64Vector> BasisVectors
        {
            get
            {
                yield return BasisVector1;
                yield return BasisVector2;
            }
        }

        public LinFloat64Vector BasisVector1 { get; }

        public LinFloat64Vector BasisVector2 { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64PlaneSubspace(LinFloat64Vector vector1, LinFloat64Vector vector2)
        {
            Debug.Assert(
                vector1.IsNearOrthonormalWith(vector2)
            );

            BasisVector1 = vector1;
            BasisVector2 = vector2;
        }


        public bool NearContains(LinFloat64Vector vector, double epsilon = 1E-12D)
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
    }
}