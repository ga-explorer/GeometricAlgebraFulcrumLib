using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Reflectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
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