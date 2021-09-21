using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Geometry.Conformal
{
    public class ConformalOpnsHyperSphere<T> :
        IKVectorStorageContainer<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalOpnsHyperSphere<T> operator *(ConformalOpnsHyperSphere<T> mv, T s)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalOpnsHyperSphere<T>(
                processor,
                processor.Times(mv.BladeStorage, s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalOpnsHyperSphere<T> operator *(T s, ConformalOpnsHyperSphere<T> mv)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalOpnsHyperSphere<T>(
                processor,
                processor.Times(s, mv.BladeStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalOpnsHyperSphere<T> operator /(ConformalOpnsHyperSphere<T> mv, T s)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalOpnsHyperSphere<T>(
                processor,
                processor.Divide(mv.BladeStorage, s)
            );
        }
        
        
        public GeometricAlgebraConformalProcessor<T> ConformalProcessor { get; }

        public KVectorStorage<T> BladeStorage { get; }


        internal ConformalOpnsHyperSphere([NotNull] GeometricAlgebraConformalProcessor<T> conformalProcessor, [NotNull] KVectorStorage<T> vectorStorage)
        {
            ConformalProcessor = conformalProcessor;
            BladeStorage = vectorStorage;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Square()
        {
            return ConformalProcessor.Sp(BladeStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ConformalIpnsHyperSphere<T> ToIpnsHyperSphere()
        {
            return new ConformalIpnsHyperSphere<T>(
                ConformalProcessor,
                ConformalProcessor
                    .Dual(BladeStorage)
                    .GetVectorPart()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> GetMultivectorStorage()
        {
            return BladeStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorStorage()
        {
            return BladeStorage;
        }
    }
}