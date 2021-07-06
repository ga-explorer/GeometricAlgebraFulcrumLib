using System;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraLib.Processing.Multivectors;
using GeometricAlgebraLib.Processing.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Geometry.Metric
{
    public sealed class GaMetricSubspace<T> 
        : IGaMetricGeometry<T>, IGaSubspace<T>
    {
        public static GaMetricSubspace<T> Create(IGaMultivectorProcessor<T> processor, IGaKVectorStorage<T> storage)
        {
            return new GaMetricSubspace<T>(processor, storage);
        }

        public static GaMetricSubspace<T> CreateFromPseudoScalar(IGaMultivectorProcessor<T> processor)
        {
            return new GaMetricSubspace<T>(
                processor,
                GaKVectorTermStorage<T>.CreatePseudoScalar(
                    processor.ScalarProcessor, 
                    processor.BasisSet.VSpaceDimension
                )
            );
        }


        public IGaMultivectorProcessor<T> MultivectorProcessor { get; }

        public bool IsValid 
            => true;

        public bool IsInvalid 
            => false;

        public IGaScalarProcessor<T> ScalarProcessor 
            => MultivectorProcessor.ScalarProcessor;

        public IGaKVectorStorage<T> BladeStorage { get; }

        public T BladeScalarProductSquared { get; }


        private GaMetricSubspace([NotNull] IGaMultivectorProcessor<T> processor, [NotNull] IGaKVectorStorage<T> storage)
        {
            BladeStorage = storage;
            BladeScalarProductSquared = processor.Sp(storage);
            MultivectorProcessor = processor;
        }


        public IGaMultivectorStorage<T> Project(IGaMultivectorStorage<T> storage)
        {
            return MultivectorProcessor.Lcp(
                MultivectorProcessor.Lcp(storage, BladeStorage),
                BladeStorage
            ).Divide(BladeScalarProductSquared);
        }

        //TODO: Implement all cases in table 7.1 page 201 in "Geometric Algebra for Computer Science"
        public IGaMultivectorStorage<T> Reflect(IGaMultivectorStorage<T> storage)
        {
            return MultivectorProcessor.Gp(
                BladeStorage,
                storage.GetGradeInvolution(),
                BladeStorage
            ).Divide(BladeScalarProductSquared);
        }

        public IGaMultivectorStorage<T> Rotate(IGaMultivectorStorage<T> storage)
        {
            if (BladeStorage.Grade.IsOdd())
                throw new InvalidOperationException();

            //Debug.Assert(ScalarProcessor.IsOne(BladeScalarProductSquared));

            return MultivectorProcessor.Gp(
                BladeStorage,
                storage,
                BladeStorage.GetReverse()
            );
        }

        public IGaMultivectorStorage<T> VersorProduct(IGaMultivectorStorage<T> storage)
        {
            return MultivectorProcessor.Gp(
                BladeStorage,
                storage,
                BladeStorage
            ).Divide(BladeScalarProductSquared);
        }

        public IGaMultivectorStorage<T> Complement(IGaMultivectorStorage<T> storage)
        {
            return MultivectorProcessor.Lcp(storage, BladeStorage);
        }
    }
}