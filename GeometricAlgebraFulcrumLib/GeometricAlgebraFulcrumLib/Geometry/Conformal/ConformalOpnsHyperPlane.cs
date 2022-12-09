using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Conformal
{
    public class ConformalOpnsHyperPlane<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalOpnsHyperPlane<T> operator *(ConformalOpnsHyperPlane<T> mv, T s)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalOpnsHyperPlane<T>(
                processor,
                processor.Times(mv.BladeStorage, s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalOpnsHyperPlane<T> operator *(T s, ConformalOpnsHyperPlane<T> mv)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalOpnsHyperPlane<T>(
                processor,
                processor.Times(s, mv.BladeStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalOpnsHyperPlane<T> operator /(ConformalOpnsHyperPlane<T> mv, T s)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalOpnsHyperPlane<T>(
                processor,
                processor.Divide(mv.BladeStorage, s)
            );
        }
        
        
        public GeometricAlgebraConformalProcessor<T> ConformalProcessor { get; }

        public KVectorStorage<T> BladeStorage { get; }


        internal ConformalOpnsHyperPlane([NotNull] GeometricAlgebraConformalProcessor<T> conformalProcessor, [NotNull] KVectorStorage<T> vectorStorage)
        {
            ConformalProcessor = conformalProcessor;
            BladeStorage = vectorStorage;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Square()
        {
            return MultivectorStorageSpUtils.SpSquared(ConformalProcessor, BladeStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ConformalIpnsHyperPlane<T> ToIpnsHyperPlane()
        {
            return new ConformalIpnsHyperPlane<T>(
                ConformalProcessor,
                ConformalProcessor
                    .Dual(BladeStorage)
                    .GetVectorPart()
            );
        }
    }
}