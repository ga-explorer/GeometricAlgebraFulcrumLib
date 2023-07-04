using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.SubSpaces.Space3D
{
    public sealed record LinFloat64NullSubspace3D :
        ILinFloat64Subspace3D
    {
        public static LinFloat64NullSubspace3D Instance { get; }
            = new LinFloat64NullSubspace3D();


        public int VSpaceDimensions
            => 3;

        public int SubspaceDimensions
            => 0;

        public IEnumerable<Float64Vector3D> BasisVectors
            => Enumerable.Empty<Float64Vector3D>();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64NullSubspace3D()
        {
        }


        public Float64PlanarAngle GetVectorProjectionPolarAngle(IFloat64Vector3D vector)
        {
            return Float64PlanarAngle.Angle0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NearContains(IFloat64Vector3D vector, double epsilon = 1E-12D)
        {
            return vector.ENorm().IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NearContains(ILinFloat64Subspace3D subspace, double epsilon = 1E-12)
        {
            return subspace.SubspaceDimensions == 0;
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
        
        public Float64Vector3D GetVectorProjection(IFloat64Vector3D vector)
        {
            throw new NotImplementedException();
        }

        public Float64Vector3D GetVectorRejection(IFloat64Vector3D vector)
        {
            throw new NotImplementedException();
        }

    }
}