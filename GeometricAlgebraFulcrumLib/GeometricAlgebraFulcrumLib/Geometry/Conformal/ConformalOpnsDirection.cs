using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Geometry.Conformal
{
    public class ConformalOpnsDirection<T> :
        IKVectorStorageContainer<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalOpnsDirection<T> operator *(ConformalOpnsDirection<T> mv, T s)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalOpnsDirection<T>(
                processor,
                processor.Times(mv.BladeStorage, s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalOpnsDirection<T> operator *(T s, ConformalOpnsDirection<T> mv)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalOpnsDirection<T>(
                processor,
                processor.Times(s, mv.BladeStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalOpnsDirection<T> operator /(ConformalOpnsDirection<T> mv, T s)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalOpnsDirection<T>(
                processor,
                processor.Divide(mv.BladeStorage, s)
            );
        }


        public GeometricAlgebraConformalProcessor<T> ConformalProcessor { get; }

        public KVectorStorage<T> BladeStorage { get; }


        internal ConformalOpnsDirection([NotNull] GeometricAlgebraConformalProcessor<T> conformalProcessor, [NotNull] KVectorStorage<T> bladeStorage)
        {
            ConformalProcessor = conformalProcessor;
            BladeStorage = bladeStorage;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Square()
        {
            return MultivectorStorageSpUtils.SpSquared(ConformalProcessor, BladeStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ConformalOpnsDirection<T> ToIpnsDirection()
        {
            return new ConformalOpnsDirection<T>(
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