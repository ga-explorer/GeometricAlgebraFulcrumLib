using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Conformal
{
    public abstract class ConformalIpnsVectorBase<T> :
        IVectorStorageContainer<T>
    {
        protected bool AssumeUnitWeight { get; private set; }


        public GeometricAlgebraConformalProcessor<T> ConformalProcessor { get; }

        public VectorStorage<T> VectorStorage { get; }


        internal ConformalIpnsVectorBase([NotNull] GeometricAlgebraConformalProcessor<T> conformalProcessor, [NotNull] VectorStorage<T> vectorStorage)
        {
            ConformalProcessor = conformalProcessor;
            VectorStorage = vectorStorage;
        }

        internal ConformalIpnsVectorBase([NotNull] GeometricAlgebraConformalProcessor<T> conformalProcessor, [NotNull] VectorStorage<T> vectorStorage, bool assumeUnitWeight)
        {
            AssumeUnitWeight = assumeUnitWeight;
            ConformalProcessor = conformalProcessor;
            VectorStorage = vectorStorage;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Square()
        {
            return MultivectorStorageSpUtils.SpSquared(ConformalProcessor, VectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Weight()
        {
            if (AssumeUnitWeight)
                return ConformalProcessor.ScalarOne;

            return ConformalProcessor.Negative(
                ConformalProcessor.Sp(
                    ConformalProcessor.InfinityBasisVector.VectorStorage, 
                    VectorStorage
                )
            );
        }

        public VectorStorage<T> GetUnitWeightVectorStorage()
        {
            if (AssumeUnitWeight)
                return VectorStorage;

            var weight = Weight();

            if (ConformalProcessor.IsZero(weight))
                return VectorStorage;

            if (!ConformalProcessor.IsOne(weight)) 
                return ConformalProcessor.Divide(VectorStorage, weight);

            AssumeUnitWeight = true;
            return VectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasUnitWeight()
        {
            if (AssumeUnitWeight)
                return true;

            return AssumeUnitWeight = ConformalProcessor.IsZero(
                ConformalProcessor.SubtractOne(Weight())
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasZeroWeight()
        {
            return !AssumeUnitWeight && ConformalProcessor.IsZero(Weight());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> GetMultivectorStorage()
        {
            return VectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorStorage()
        {
            return VectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVectorStorage()
        {
            return VectorStorage;
        }

    }
}