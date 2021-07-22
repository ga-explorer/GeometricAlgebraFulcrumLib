using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Geometry.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Processing.Products.Iterators
{
    public sealed class GaProductKVectorsIterator<T>
    {
        public static GaProductKVectorsIterator<T> Create(GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            return new(
                mv1.Processor,
                mv1.Storage.GetKVectorStoragesDictionary(),
                mv2.Storage.GetKVectorStoragesDictionary()
            );
        }

        public static GaProductKVectorsIterator<T> Create(IGaScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<uint, IGasKVector<T>> storageList1, IReadOnlyDictionary<uint, IGasKVector<T>> storageList2)
        {
            return new(
                scalarProcessor,
                storageList1,
                storageList2
            );
        }


        private readonly GaProductTermsIterator<T> _termsIterator 
            = GaProductTermsIterator<T>.Create();


        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public IReadOnlyDictionary<uint, IGasKVector<T>> StoragesDictionary1 { get; }

        public IReadOnlyDictionary<uint, IGasKVector<T>> StoragesDictionary2 { get; }


        private GaProductKVectorsIterator([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] IReadOnlyDictionary<uint, IGasKVector<T>> storageList1, [NotNull] IReadOnlyDictionary<uint, IGasKVector<T>> storageList2)
        {
            ScalarProcessor = scalarProcessor;

            StoragesDictionary1 = storageList1;
            StoragesDictionary2 = storageList2;
        }


        private IEnumerable<IGasKVector<T>> GetOpKVectors2(uint grade1)
        {
            return StoragesDictionary2.Values.Where(storage => 
                grade1 + storage.Grade <= GaSpaceUtils.MaxVSpaceDimension
            );
        }

        private IEnumerable<IGasKVector<T>> GetESpKVector2(uint grade1)
        {
            if (StoragesDictionary2.TryGetValue(grade1, out var storage2))
                yield return storage2;
        }

        private IEnumerable<IGasKVector<T>> GetELcpKVectors2(uint grade1)
        {
            return StoragesDictionary2.Values.Where(storage => 
                storage.Grade >= grade1
            );
        }

        private IEnumerable<IGasKVector<T>> GetERcpKVectors2(uint grade1)
        {
            return StoragesDictionary2.Values.Where(storage => 
                grade1 >= storage.Grade
            );
        }
        

        public IGasKVector<T> GetOpKVector(IGasKVector<T> storage1, IGasKVector<T> storage2)
        {
            var grade = storage1.Grade + storage2.Grade;

            if (grade > GaSpaceUtils.MaxVSpaceDimension)
                return ScalarProcessor.CreateZeroScalar();

            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddIdScalarPairs(
                _termsIterator.GetOpIdScalarPairs()
            );

            return storage.GetKVectorStorage();
        }

        public T GetESpScalar(IGasKVector<T> storage1, IGasKVector<T> storage2)
        {
            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            return ScalarProcessor.Add(
                _termsIterator.GetESpScalars()
            );
        }

        public IGasKVector<T> GetELcpKVector(IGasKVector<T> storage1, IGasKVector<T> storage2)
        {
            if (storage2.Grade < storage1.Grade)
                return ScalarProcessor.CreateZeroScalar();

            var grade = storage2.Grade - storage1.Grade;

            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddIdScalarPairs(
                _termsIterator.GetELcpIdScalarPairs()
            );

            return storage.GetKVectorStorage();
        }

        public IGasKVector<T> GetERcpKVector(IGasKVector<T> storage1, IGasKVector<T> storage2)
        {
            if (storage1.Grade < storage2.Grade)
                return ScalarProcessor.CreateZeroScalar();

            var grade = storage1.Grade - storage2.Grade;

            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddIdScalarPairs(
                _termsIterator.GetERcpIdScalarPairs()
            );

            return storage.GetKVectorStorage();
        }

        public IGasKVector<T> GetEHipKVector(IGasKVector<T> storage1, IGasKVector<T> storage2)
        {
            if (storage1.Grade < 1 || storage2.Grade < 1)
                return ScalarProcessor.CreateZeroScalar();

            var grade = (uint) Math.Abs(storage1.Grade - storage2.Grade);
            
            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddIdScalarPairs(
                _termsIterator.GetERcpIdScalarPairs()
            );

            return storage.GetKVectorStorage();
        }

        public IGasKVector<T> GetEFdpKVector(IGasKVector<T> storage1, IGasKVector<T> storage2)
        {
            var grade = (uint) Math.Abs(storage1.Grade - storage2.Grade);
            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddIdScalarPairs(
                _termsIterator.GetEFdpIdScalarPairs()
            );

            return storage.GetKVectorStorage();
        }


        public T GetSpScalar(IGasKVector<T> storage1, IGasKVector<T> storage2, GaSignature basesSignature)
        {
            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            return ScalarProcessor.Add(
                _termsIterator.GetSpScalars(basesSignature)
            );
        }

        public IGasKVector<T> GetLcpKVector(IGasKVector<T> storage1, IGasKVector<T> storage2, GaSignature basesSignature)
        {
            if (storage2.Grade < storage1.Grade)
                return ScalarProcessor.CreateZeroScalar();

            var grade = storage2.Grade - storage1.Grade;

            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddIdScalarPairs(
                _termsIterator.GetLcpIdScalarPairs(basesSignature)
            );

            return storage.GetKVectorStorage();
        }

        public IGasKVector<T> GetRcpKVector(IGasKVector<T> storage1, IGasKVector<T> storage2, GaSignature basesSignature)
        {
            if (storage1.Grade < storage2.Grade)
                return ScalarProcessor.CreateZeroScalar();

            var grade = storage1.Grade - storage2.Grade;

            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddIdScalarPairs(
                _termsIterator.GetRcpIdScalarPairs(basesSignature)
            );

            return storage.GetKVectorStorage();
        }

        public IGasKVector<T> GetHipKVector(IGasKVector<T> storage1, IGasKVector<T> storage2, GaSignature basesSignature)
        {
            if (storage1.Grade < 1 || storage2.Grade < 1)
                return ScalarProcessor.CreateZeroScalar();

            var grade = (uint) Math.Abs(storage1.Grade - storage2.Grade);
            
            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddIdScalarPairs(
                _termsIterator.GetRcpIdScalarPairs(basesSignature)
            );

            return storage.GetKVectorStorage();
        }

        public IGasKVector<T> GetFdpKVector(IGasKVector<T> storage1, IGasKVector<T> storage2, GaSignature basesSignature)
        {
            var grade = (uint) Math.Abs(storage1.Grade - storage2.Grade);
            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddIdScalarPairs(
                _termsIterator.GetFdpIdScalarPairs(basesSignature)
            );

            return storage.GetKVectorStorage();
        }


        public IEnumerable<IGasKVector<T>> GetOpKVectors()
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

        public IEnumerable<IGasKVector<T>> GetELcpKVectors()
        {
            foreach (var storage1 in StoragesDictionary1.Values)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetELcpKVectors2(grade1))
                    yield return GetELcpKVector(storage1, storage2);
            }
        }

        public IEnumerable<IGasKVector<T>> GetERcpKVectors()
        {
            foreach (var storage1 in StoragesDictionary1.Values)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetERcpKVectors2(grade1))
                    yield return GetERcpKVector(storage1, storage2);
            }
        }

        public IEnumerable<IGasKVector<T>> GetEHipKVectors()
        {
            foreach (var storage1 in StoragesDictionary1.Values.Where(s => s.Grade > 0))
            foreach (var storage2 in StoragesDictionary2.Values.Where(s => s.Grade > 0))
                yield return GetEHipKVector(storage1, storage2);
        }

        public IEnumerable<IGasKVector<T>> GetEFdpKVectors()
        {
            foreach (var storage1 in StoragesDictionary1.Values)
            foreach (var storage2 in StoragesDictionary2.Values)
                yield return GetEFdpKVector(storage1, storage2);
        }


        public IEnumerable<IGasKVector<T>> GetELcpKVectors(GaSignature basesSignature)
        {
            foreach (var storage1 in StoragesDictionary1.Values)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetELcpKVectors2(grade1))
                    yield return GetLcpKVector(storage1, storage2, basesSignature);
            }
        }

        public IEnumerable<IGasKVector<T>> GetERcpKVectors(GaSignature basesSignature)
        {
            foreach (var storage1 in StoragesDictionary1.Values)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetERcpKVectors2(grade1))
                    yield return GetRcpKVector(storage1, storage2, basesSignature);
            }
        }

        public IEnumerable<IGasKVector<T>> GetEHipKVectors(GaSignature basesSignature)
        {
            foreach (var storage1 in StoragesDictionary1.Values.Where(s => s.Grade > 0))
            foreach (var storage2 in StoragesDictionary2.Values.Where(s => s.Grade > 0))
                yield return GetHipKVector(storage1, storage2, basesSignature);
        }

        public IEnumerable<IGasKVector<T>> GetEFdpKVectors(GaSignature basesSignature)
        {
            foreach (var storage1 in StoragesDictionary1.Values)
            foreach (var storage2 in StoragesDictionary2.Values)
                yield return GetFdpKVector(storage1, storage2, basesSignature);
        }
    }
}