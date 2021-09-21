using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Geometry.Conformal
{
    public class ConformalIpnsHyperPlane<T> :
        ConformalIpnsVectorBase<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalIpnsHyperPlane<T> operator *(ConformalIpnsHyperPlane<T> mv, T s)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalIpnsHyperPlane<T>(
                processor,
                processor.Times(mv.VectorStorage, s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalIpnsHyperPlane<T> operator *(T s, ConformalIpnsHyperPlane<T> mv)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalIpnsHyperPlane<T>(
                processor,
                processor.Times(s, mv.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalIpnsHyperPlane<T> operator /(ConformalIpnsHyperPlane<T> mv, T s)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalIpnsHyperPlane<T>(
                processor,
                processor.Divide(mv.VectorStorage, s)
            );
        }


        internal ConformalIpnsHyperPlane([NotNull] GeometricAlgebraConformalProcessor<T> conformalProcessor, [NotNull] VectorStorage<T> vectorStorage)
            : base(conformalProcessor, vectorStorage)
        {
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> GetNormal()
        {
            return VectorStorage
                .FilterVectorByIndex(index => index < ConformalProcessor.VSpaceDimension - 2)
                .CreateVector(ConformalProcessor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ConformalOpnsHyperPlane<T> ToOpnsHyperPlane()
        {
            return new ConformalOpnsHyperPlane<T>(
                ConformalProcessor,
                ConformalProcessor
                    .UnDual(VectorStorage)
                    .GetKVectorPart(ConformalProcessor.VSpaceDimension - 1)
            );
        }
    }
}