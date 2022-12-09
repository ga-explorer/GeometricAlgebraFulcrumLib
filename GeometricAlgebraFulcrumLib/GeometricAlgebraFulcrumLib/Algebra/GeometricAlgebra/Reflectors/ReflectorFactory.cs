using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Reflectors
{
    public static class ReflectorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureReflector<T> CreatePureReflector<T>(this GaVector<T> vector)
        {
            return PureReflector<T>.Create(vector);
        }
    }
}