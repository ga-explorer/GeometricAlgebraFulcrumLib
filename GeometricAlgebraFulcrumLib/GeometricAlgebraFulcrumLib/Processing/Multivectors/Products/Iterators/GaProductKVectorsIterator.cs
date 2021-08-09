using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Storage.Factories;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Iterators
{
    public sealed class GaProductKVectorsIterator<T>
    {
        public static GaProductKVectorsIterator<T> Create(GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            return new(
                mv1.Processor,
                mv1.MultivectorStorage.GetKVectorStoragesDictionary(),
                mv2.MultivectorStorage.GetKVectorStoragesDictionary()
            );
        }

        public static GaProductKVectorsIterator<T> Create(IGaScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<uint, IGaStorageKVector<T>> storageList1, IReadOnlyDictionary<uint, IGaStorageKVector<T>> storageList2)
        {
            return new GaProductKVectorsIterator<T>(
                scalarProcessor,
                storageList1,
                storageList2
            );
        }


        private readonly GaProductTermsIterator<T> _termsIterator;


        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public IReadOnlyDictionary<uint, IGaStorageKVector<T>> StoragesDictionary1 { get; }

        public IReadOnlyDictionary<uint, IGaStorageKVector<T>> StoragesDictionary2 { get; }


        private GaProductKVectorsIterator([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] IReadOnlyDictionary<uint, IGaStorageKVector<T>> storageList1, [NotNull] IReadOnlyDictionary<uint, IGaStorageKVector<T>> storageList2)
        {
            _termsIterator = GaProductTermsIterator<T>.Create(scalarProcessor);
            ScalarProcessor = scalarProcessor;
            StoragesDictionary1 = storageList1;
            StoragesDictionary2 = storageList2;
        }


        private IEnumerable<IGaStorageKVector<T>> GetOpKVectors2(uint grade1)
        {
            return StoragesDictionary2.Values.Where(storage => 
                grade1 + storage.Grade <= GaSpaceUtils.MaxVSpaceDimension
            );
        }

        private IEnumerable<IGaStorageKVector<T>> GetESpKVector2(uint grade1)
        {
            if (StoragesDictionary2.TryGetValue(grade1, out var storage2))
                yield return storage2;
        }

        private IEnumerable<IGaStorageKVector<T>> GetELcpKVectors2(uint grade1)
        {
            return StoragesDictionary2.Values.Where(storage => 
                storage.Grade >= grade1
            );
        }

        private IEnumerable<IGaStorageKVector<T>> GetERcpKVectors2(uint grade1)
        {
            return StoragesDictionary2.Values.Where(storage => 
                grade1 >= storage.Grade
            );
        }
        

        public IGaStorageKVector<T> GetOpKVector(IGaStorageKVector<T> storage1, IGaStorageKVector<T> storage2)
        {
            var grade = storage1.Grade + storage2.Grade;

            if (grade > GaSpaceUtils.MaxVSpaceDimension)
                return ScalarProcessor.CreateStorageZeroScalar();

            var storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddIdScalarPairs(
                _termsIterator.GetOpIdScalarPairs()
            );

            return storage.GetKVector();
        }

        public T GetESpScalar(IGaStorageKVector<T> storage1, IGaStorageKVector<T> storage2)
        {
            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            return ScalarProcessor.Add(
                _termsIterator.GetESpScalars()
            );
        }

        public IGaStorageKVector<T> GetELcpKVector(IGaStorageKVector<T> storage1, IGaStorageKVector<T> storage2)
        {
            if (storage2.Grade < storage1.Grade)
                return ScalarProcessor.CreateStorageZeroScalar();

            var grade = storage2.Grade - storage1.Grade;

            var storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddIdScalarPairs(
                _termsIterator.GetELcpIdScalarPairs()
            );

            return storage.GetKVector();
        }

        public IGaStorageKVector<T> GetERcpKVector(IGaStorageKVector<T> storage1, IGaStorageKVector<T> storage2)
        {
            if (storage1.Grade < storage2.Grade)
                return ScalarProcessor.CreateStorageZeroScalar();

            var grade = storage1.Grade - storage2.Grade;

            var storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddIdScalarPairs(
                _termsIterator.GetERcpIdScalarPairs()
            );

            return storage.GetKVector();
        }

        public IGaStorageKVector<T> GetEHipKVector(IGaStorageKVector<T> storage1, IGaStorageKVector<T> storage2)
        {
            if (storage1.Grade < 1 || storage2.Grade < 1)
                return ScalarProcessor.CreateStorageZeroScalar();

            var grade = (uint) Math.Abs(storage1.Grade - storage2.Grade);
            
            var storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddIdScalarPairs(
                _termsIterator.GetERcpIdScalarPairs()
            );

            return storage.GetKVector();
        }

        public IGaStorageKVector<T> GetEFdpKVector(IGaStorageKVector<T> storage1, IGaStorageKVector<T> storage2)
        {
            var grade = (uint) Math.Abs(storage1.Grade - storage2.Grade);
            var storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddIdScalarPairs(
                _termsIterator.GetEFdpIdScalarPairs()
            );

            return storage.GetKVector();
        }


        public T GetSpScalar(IGaStorageKVector<T> storage1, IGaStorageKVector<T> storage2, GaSignature basesSignature)
        {
            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            return ScalarProcessor.Add(
                _termsIterator.GetSpScalars(basesSignature)
            );
        }

        public IGaStorageKVector<T> GetLcpKVector(IGaStorageKVector<T> storage1, IGaStorageKVector<T> storage2, GaSignature basesSignature)
        {
            if (storage2.Grade < storage1.Grade)
                return ScalarProcessor.CreateStorageZeroScalar();

            var grade = storage2.Grade - storage1.Grade;

            var storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddIdScalarPairs(
                _termsIterator.GetLcpIdScalarPairs(basesSignature)
            );

            return storage.GetKVector();
        }

        public IGaStorageKVector<T> GetRcpKVector(IGaStorageKVector<T> storage1, IGaStorageKVector<T> storage2, GaSignature basesSignature)
        {
            if (storage1.Grade < storage2.Grade)
                return ScalarProcessor.CreateStorageZeroScalar();

            var grade = storage1.Grade - storage2.Grade;

            var storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddIdScalarPairs(
                _termsIterator.GetRcpIdScalarPairs(basesSignature)
            );

            return storage.GetKVector();
        }

        public IGaStorageKVector<T> GetHipKVector(IGaStorageKVector<T> storage1, IGaStorageKVector<T> storage2, GaSignature basesSignature)
        {
            if (storage1.Grade < 1 || storage2.Grade < 1)
                return ScalarProcessor.CreateStorageZeroScalar();

            var grade = (uint) Math.Abs(storage1.Grade - storage2.Grade);
            
            var storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddIdScalarPairs(
                _termsIterator.GetRcpIdScalarPairs(basesSignature)
            );

            return storage.GetKVector();
        }

        public IGaStorageKVector<T> GetFdpKVector(IGaStorageKVector<T> storage1, IGaStorageKVector<T> storage2, GaSignature basesSignature)
        {
            var grade = (uint) Math.Abs(storage1.Grade - storage2.Grade);
            var storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddIdScalarPairs(
                _termsIterator.GetFdpIdScalarPairs(basesSignature)
            );

            return storage.GetKVector();
        }


        public IEnumerable<IGaStorageKVector<T>> GetOpKVectors()
        {
            foreach (var storage1 in StoragesDictionary1.Values)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetOpKVectors2(grade1))
                    yield return GetOpKVector(storage1, storage2);
            }
        }

        public IEnumerable<T> GetESpScalars()
        {
            foreach (var storage1 in StoragesDictionary1.Values)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetESpKVector2(grade1))
                    yield return GetESpScalar(storage1, storage2);
            }
        }

        public IEnumerable<IGaStorageKVector<T>> GetELcpKVectors()
        {
            foreach (var storage1 in StoragesDictionary1.Values)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetELcpKVectors2(grade1))
                    yield return GetELcpKVector(storage1, storage2);
            }
        }

        public IEnumerable<IGaStorageKVector<T>> GetERcpKVectors()
        {
            foreach (var storage1 in StoragesDictionary1.Values)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetERcpKVectors2(grade1))
                    yield return GetERcpKVector(storage1, storage2);
            }
        }

        public IEnumerable<IGaStorageKVector<T>> GetEHipKVectors()
        {
            foreach (var storage1 in StoragesDictionary1.Values.Where(s => s.Grade > 0))
            foreach (var storage2 in StoragesDictionary2.Values.Where(s => s.Grade > 0))
                yield return GetEHipKVector(storage1, storage2);
        }

        public IEnumerable<IGaStorageKVector<T>> GetEFdpKVectors()
        {
            foreach (var storage1 in StoragesDictionary1.Values)
            foreach (var storage2 in StoragesDictionary2.Values)
                yield return GetEFdpKVector(storage1, storage2);
        }


        public IEnumerable<IGaStorageKVector<T>> GetELcpKVectors(GaSignature basesSignature)
        {
            foreach (var storage1 in StoragesDictionary1.Values)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetELcpKVectors2(grade1))
                    yield return GetLcpKVector(storage1, storage2, basesSignature);
            }
        }

        public IEnumerable<IGaStorageKVector<T>> GetERcpKVectors(GaSignature basesSignature)
        {
            foreach (var storage1 in StoragesDictionary1.Values)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetERcpKVectors2(grade1))
                    yield return GetRcpKVector(storage1, storage2, basesSignature);
            }
        }

        public IEnumerable<IGaStorageKVector<T>> GetEHipKVectors(GaSignature basesSignature)
        {
            foreach (var storage1 in StoragesDictionary1.Values.Where(s => s.Grade > 0))
            foreach (var storage2 in StoragesDictionary2.Values.Where(s => s.Grade > 0))
                yield return GetHipKVector(storage1, storage2, basesSignature);
        }

        public IEnumerable<IGaStorageKVector<T>> GetEFdpKVectors(GaSignature basesSignature)
        {
            foreach (var storage1 in StoragesDictionary1.Values)
            foreach (var storage2 in StoragesDictionary2.Values)
                yield return GetFdpKVector(storage1, storage2, basesSignature);
        }
    }
}