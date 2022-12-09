using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Conformal
{
    public class ConformalIpnsFlat<T> :
        IKVectorStorageContainer<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalIpnsFlat<T> operator *(ConformalIpnsFlat<T> mv, T s)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalIpnsFlat<T>(
                processor,
                processor.Times(mv.BladeStorage, s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalIpnsFlat<T> operator *(T s, ConformalIpnsFlat<T> mv)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalIpnsFlat<T>(
                processor,
                processor.Times(s, mv.BladeStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalIpnsFlat<T> operator /(ConformalIpnsFlat<T> mv, T s)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalIpnsFlat<T>(
                processor,
                processor.Divide(mv.BladeStorage, s)
            );
        }


        public GeometricAlgebraConformalProcessor<T> ConformalProcessor { get; }

        public KVectorStorage<T> BladeStorage { get; }


        internal ConformalIpnsFlat([NotNull] GeometricAlgebraConformalProcessor<T> conformalProcessor, [NotNull] KVectorStorage<T> bladeStorage)
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
        public ConformalOpnsFlat<T> ToOpnsFlat()
        {
            return new ConformalOpnsFlat<T>(
                ConformalProcessor,
                ConformalProcessor
                    .UnDual(BladeStorage)
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