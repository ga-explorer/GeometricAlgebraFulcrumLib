using System.Collections.Generic;
using GeometricAlgebraLib.Algebra.Basis;
using GeometricAlgebraLib.Processing.Multivectors;
using GeometricAlgebraLib.Processing.Scalars;
using GeometricAlgebraLib.Storage;
using GeometricAlgebraLib.Storage.Composers;

namespace GeometricAlgebraLib.Algebra.Outermorphisms.Stored
{
    public sealed class GaOmStored<T> : 
        IGaOutermorphism<T>
    {
        private readonly Dictionary<ulong, IGaKVectorStorage<T>> _mappedBasisBladesDictionary
            = new Dictionary<ulong, IGaKVectorStorage<T>>();


        public int DomainVSpaceDimension { get; }

        public ulong DomainGaSpaceDimension 
            => 1UL << DomainVSpaceDimension;

        public IGaMultivectorProcessor<T> MultivectorProcessor { get; }

        public IGaScalarProcessor<T> ScalarProcessor 
            => MultivectorProcessor.ScalarProcessor;


        private GaOmStored(IGaMultivectorProcessor<T> processor, int domainVSpaceDimension)
        {
            MultivectorProcessor = processor;
            DomainVSpaceDimension = domainVSpaceDimension;
        }


        public IReadOnlyList<IGaVectorStorage<T>> GetMappedBasisVectors()
        {
            var mappedBasisVectorsList = 
                new List<IGaVectorStorage<T>>(DomainVSpaceDimension);

            for (var index = 0; index < DomainVSpaceDimension; index++)
            {
                mappedBasisVectorsList.Add(
                    _mappedBasisBladesDictionary.TryGetValue(1UL << index, out var mappedKVector)
                        ? mappedKVector.GetVectorPart()
                        : GaVectorTermStorage<T>.CreateZero(ScalarProcessor)
                    );
            }

            return mappedBasisVectorsList;
        }

        public T GetDeterminant()
        {
            var mappedPseudoScalar = 
                MapBasisBlade((1UL << DomainVSpaceDimension) - 1);

            return MultivectorProcessor.Sp(
                mappedPseudoScalar, 
                MultivectorProcessor.PseudoScalarInverse
            );
        }

        public IGaVectorStorage<T> MapBasisVector(int index)
        {
            return _mappedBasisBladesDictionary.TryGetValue(1UL << index, out var mappedKVector)
                ? mappedKVector.GetVectorPart()
                : GaVectorTermStorage<T>.CreateZero(ScalarProcessor);
        }

        public IGaVectorStorage<T> MapBasisVector(ulong index)
        {
            return _mappedBasisBladesDictionary.TryGetValue(1UL << (int) index, out var mappedKVector)
                ? mappedKVector.GetVectorPart()
                : GaVectorTermStorage<T>.CreateZero(ScalarProcessor);
        }

        public IGaKVectorStorage<T> MapBasisBlade(ulong id)
        {
            return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector)
                ? mappedKVector
                : GaKVectorTermStorage<T>.CreateZero(ScalarProcessor, id.BasisBladeGrade());
        }

        public IGaKVectorStorage<T> MapBasisBlade(int grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector)
                ? mappedKVector
                : GaKVectorTermStorage<T>.CreateZero(ScalarProcessor, grade);
        }

        public IGaVectorStorage<T> MapVector(IGaVectorStorage<T> vector)
        {
            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, 1);

            foreach (var (index, scalar) in vector.GetIndexScalarPairs())
                if (_mappedBasisBladesDictionary.TryGetValue(1UL << (int) index, out var mappedKVector))
                    storage.AddLeftScaledTerms(
                        scalar, 
                        mappedKVector.GetIndexScalarPairs()
                    );

            storage.RemoveZeroTerms();

            return storage.GetVectorStorage();
        }

        public IGaKVectorStorage<T> MapKVector(IGaKVectorStorage<T> kVector)
        {
            var grade = kVector.Grade;
            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);

            foreach (var (index, scalar) in kVector.GetIndexScalarPairs())
                if (_mappedBasisBladesDictionary.TryGetValue(GaBasisUtils.BasisBladeId(grade, index), out var mappedKVector))
                    storage.AddLeftScaledTerms(
                        scalar, 
                        mappedKVector.GetIndexScalarPairs()
                    );

            storage.RemoveZeroTerms();

            return storage.GetKVectorStorage();
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> mv)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(ScalarProcessor);

            foreach (var (id, scalar) in mv.GetIdScalarPairs())
                if (_mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector))
                    storage.AddLeftScaledTerms(
                        scalar, 
                        mappedKVector.GetIdScalarPairs()
                    );

            storage.RemoveZeroTerms();

            return storage.GetCompactStorage();
        }
        
    }
}