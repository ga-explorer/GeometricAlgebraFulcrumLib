using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.SubSpaces.Space4D
{
    public sealed record LinFloat64NullSubspace4D :
        ILinFloat64Subspace4D
    {
        public static LinFloat64NullSubspace4D Instance { get; }
            = new LinFloat64NullSubspace4D();


        public int VSpaceDimensions
            => 4;

        public int SubspaceDimensions
            => 0;

        public IEnumerable<Float64Vector4D> BasisVectors
            => Enumerable.Empty<Float64Vector4D>();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64NullSubspace4D()
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NearContains(IFloat64Vector4D vector, double epsilon = 1E-12D)
        {
            return vector.ENorm().IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NearContains(ILinFloat64Subspace4D subspace, double epsilon = 1E-12)
        {
            return subspace.SubspaceDimensions == 0;
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}