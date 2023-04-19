using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms
{
    public static class XGaOutermorphismComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaLinearMapOutermorphism<T> CreateOutermorphism<T>(this XGaProcessor<T> processor, LinUnilinearMap<T> linearMap)
        {
            return new XGaLinearMapOutermorphism<T>(processor, linearMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaLinearMapOutermorphism<T> ToOutermorphism<T>(this LinUnilinearMap<T> linearMap, XGaProcessor<T> processor)
        {
            return new XGaLinearMapOutermorphism<T>(processor, linearMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaDiagonalOutermorphism<T> CreateDiagonalAutomorphism<T>(this XGaVector<T> diagonalVector)
        {
            return new XGaDiagonalOutermorphism<T>(diagonalVector);
        }
        
        
    }
}