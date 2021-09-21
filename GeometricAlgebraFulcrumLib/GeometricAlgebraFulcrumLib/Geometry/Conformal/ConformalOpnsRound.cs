using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Geometry.Conformal
{
    public class ConformalOpnsRound<T> :
        IKVectorStorageContainer<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalOpnsRound<T> operator *(ConformalOpnsRound<T> mv, T s)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalOpnsRound<T>(
                processor,
                processor.Times(mv.BladeStorage, s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalOpnsRound<T> operator *(T s, ConformalOpnsRound<T> mv)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalOpnsRound<T>(
                processor,
                processor.Times(s, mv.BladeStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalOpnsRound<T> operator /(ConformalOpnsRound<T> mv, T s)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalOpnsRound<T>(
                processor,
                processor.Divide(mv.BladeStorage, s)
            );
        }
        
        
        public GeometricAlgebraConformalProcessor<T> ConformalProcessor { get; }

        public KVectorStorage<T> BladeStorage { get; }


        internal ConformalOpnsRound([NotNull] GeometricAlgebraConformalProcessor<T> conformalProcessor, [NotNull] KVectorStorage<T> bladeStorage)
        {
            ConformalProcessor = conformalProcessor;
            BladeStorage = bladeStorage;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Square()
        {
            return ConformalProcessor.Sp(BladeStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ConformalIpnsRound<T> ToIpnsRound()
        {
            return new ConformalIpnsRound<T>(
                ConformalProcessor,
                ConformalProcessor
                    .Dual(BladeStorage)
                    .GetKVectorPart(ConformalProcessor.VSpaceDimension - BladeStorage.Grade)
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
