using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.SubSpaces.Space3D
{
    public sealed class LinFloat64LineSubspace3D :
        ILinFloat64Subspace3D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64LineSubspace3D CreateFromVector(Float64Vector3D vector)
        {
            var length =
                vector.ENorm();

            var unitVector =
                length.IsNearOne() ? vector : vector / length;

            return new LinFloat64LineSubspace3D(unitVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64LineSubspace3D CreateFromUnitVector(Float64Vector3D vector)
        {
            return new LinFloat64LineSubspace3D(vector);
        }


        public int VSpaceDimensions
            => 3;

        public int SubspaceDimensions
            => 1;

        public Float64Vector3D BasisVector { get; }

        public IEnumerable<Float64Vector3D> BasisVectors
        {
            get
            {
                yield return BasisVector;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64LineSubspace3D(Float64Vector3D vector)
        {
            Debug.Assert(
                vector.IsNearUnit()
            );

            BasisVector = vector;
        }


        public Float64PlanarAngle GetVectorProjectionPolarAngle(IFloat64Vector3D vector)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NearContains(IFloat64Vector3D vector, double epsilon = 1E-12D)
        {
            return vector.IsNearZero(epsilon) ||
                   vector.IsNearParallelToUnit(BasisVector, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NearContains(ILinFloat64Subspace3D subspace, double epsilon = 1E-12)
        {
            return subspace.VSpaceDimensions <= VSpaceDimensions &&
                   subspace.BasisVectors.All(v => NearContains(v, epsilon));
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