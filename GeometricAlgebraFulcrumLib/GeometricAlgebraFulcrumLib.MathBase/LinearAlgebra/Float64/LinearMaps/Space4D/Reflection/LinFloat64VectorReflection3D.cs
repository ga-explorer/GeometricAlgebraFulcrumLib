using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection
{
    public sealed class LinFloat64VectorReflection4D :
        ILinFloat64UnilinearMap4D
    {
        public Float64Vector4D ReflectionVector { get; }

        public int VSpaceDimensions 
            => 3;

        public bool SwapsHandedness 
            => true;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorReflection4D(Float64Vector4D vector)
        {
            ReflectionVector = vector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return ReflectionVector.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsIdentity()
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearIdentity(double epsilon = 1E-12)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector4D MapBasisVector(int basisIndex)
        {
            Debug.Assert(basisIndex >= 0);

            var s = 2d * ReflectionVector[basisIndex];

            return Float64Vector4DComposer
                .Create()
                .SetVector(ReflectionVector, s)
                .SubtractTerm(basisIndex, 1d)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector4D MapVector(IFloat64Tuple4D vector)
        {
            var s = 2d * vector.ESp(ReflectionVector);

            return Float64Vector4DComposer
                .Create()
                .SetVector(ReflectionVector, s)
                .SubtractVector(vector)
                .GetVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorReflection4D GetVectorReflectionInverse()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinFloat64UnilinearMap4D GetInverseMap()
        {
            return GetVectorReflectionInverse();
        }
    }
}
