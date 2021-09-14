using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class VectorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> OmMapUsing<T>(this Vector<T> vector, IOutermorphism<T> om)
        {
            var processor = vector.GeometricProcessor;

            return new Vector<T>(
                processor,
                om.OmMapVector(vector.VectorStorage)
            );
        }
    }
}