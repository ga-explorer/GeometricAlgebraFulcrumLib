using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.SubSpaces
{
    public sealed record LinFloat64NullSubspace :
        ILinFloat64Subspace
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64NullSubspace Create(int dimensions)
        {
            return new LinFloat64NullSubspace(dimensions);
        }


        public int VSpaceDimensions { get; }

        public int SubspaceDimensions 
            => 0;

        public IEnumerable<LinFloat64Vector> BasisVectors 
            => Enumerable.Empty<LinFloat64Vector>();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64NullSubspace(int dimensions)
        {
            if (dimensions < 1)
                throw new ArgumentOutOfRangeException(nameof(dimensions));

            VSpaceDimensions = dimensions;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NearContains(LinFloat64Vector vector, double epsilon = 1E-12D)
        {
            return vector.IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NearContains(ILinFloat64Subspace subspace, double epsilon = 1E-12)
        {
            return subspace.VSpaceDimensions == 0;
        }
    }
}