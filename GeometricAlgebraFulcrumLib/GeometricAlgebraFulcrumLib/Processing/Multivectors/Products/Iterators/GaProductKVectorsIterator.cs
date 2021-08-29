using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Iterators
{
    public sealed class GaProductKVectorsIterator<T>
    {
        public static GaProductKVectorsIterator<T> Create(GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            return new(
                mv1.Processor,
                mv1.MultivectorStorage.GetKVectorStorages().ToArray(),
                mv2.MultivectorStorage.GetKVectorStorages().ToArray()
            );
        }

        public static GaProductKVectorsIterator<T> Create(IScalarProcessor<T> scalarProcessor, IReadOnlyList<IGaKVectorStorage<T>> storageList1, IReadOnlyList<IGaKVectorStorage<T>> storageList2)
        {
            return new GaProductKVectorsIterator<T>(
                scalarProcessor,
                storageList1,
                storageList2
            );
        }


        private readonly GaProductTermsIterator<T> _termsIterator;


        public IScalarProcessor<T> ScalarProcessor { get; }

        public IReadOnlyList<IGaKVectorStorage<T>> Storages1 { get; }

        public IReadOnlyList<IGaKVectorStorage<T>> Storages2 { get; }


        private GaProductKVectorsIterator([NotNull] IScalarProcessor<T> scalarProcessor, [NotNull] IReadOnlyList<IGaKVectorStorage<T>> storageList1, [NotNull] IReadOnlyList<IGaKVectorStorage<T>> storageList2)
        {
            _termsIterator = GaProductTermsIterator<T>.Create(scalarProcessor);
            ScalarProcessor = scalarProcessor;
            Storages1 = storageList1;
            Storages2 = storageList2;
        }


        private IEnumerable<IGaKVectorStorage<T>> GetOpKVectors2(uint grade1)
        {
            return Storages2
                .Where(storage => 
                    grade1 + storage.Grade <= GaSpaceUtils.MaxVSpaceDimension
                );
        }

        private IEnumerable<IGaKVectorStorage<T>> GetESpKVector2(uint grade1)
        {
            return Storages2.Where(s => s.Grade == grade1);
        }

        private IEnumerable<IGaKVectorStorage<T>> GetELcpKVectors2(uint grade1)
        {
            return Storages2.Where(storage => 
                storage.Grade >= grade1
            );
        }

        private IEnumerable<IGaKVectorStorage<T>> GetERcpKVectors2(uint grade1)
        {
            return Storages2.Where(storage => 
                grade1 >= storage.Grade
            );
        }
        

        public IGaKVectorStorage<T> GetOpKVector(IGaKVectorStorage<T> storage1, IGaKVectorStorage<T> storage2)
        {
            var grade = storage1.Grade + storage2.Grade;

            if (grade > GaSpaceUtils.MaxVSpaceDimension)
                return ScalarProcessor.CreateStorageZeroScalar();

            var storage = ScalarProcessor.CreateKVectorStorageComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetOpIdScalarRecords()
            );

            return storage.CreateGaKVectorStorage(grade);
        }

        public T GetESpScalar(IGaKVectorStorage<T> storage1, IGaKVectorStorage<T> storage2)
        {
            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            return ScalarProcessor.Add(
                _termsIterator.GetESpScalars()
            );
        }

        public IGaKVectorStorage<T> GetELcpKVector(IGaKVectorStorage<T> storage1, IGaKVectorStorage<T> storage2)
        {
            if (storage2.Grade < storage1.Grade)
                return ScalarProcessor.CreateStorageZeroScalar();

            var grade = storage2.Grade - storage1.Grade;

            var storage = ScalarProcessor.CreateKVectorStorageComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetELcpIdScalarRecords()
            );

            return storage.CreateGaKVectorStorage(grade);
        }

        public IGaKVectorStorage<T> GetERcpKVector(IGaKVectorStorage<T> storage1, IGaKVectorStorage<T> storage2)
        {
            if (storage1.Grade < storage2.Grade)
                return ScalarProcessor.CreateStorageZeroScalar();

            var grade = storage1.Grade - storage2.Grade;

            var storage = ScalarProcessor.CreateKVectorStorageComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetERcpIdScalarRecords()
            );

            return storage.CreateGaKVectorStorage(grade);
        }

        public IGaKVectorStorage<T> GetEHipKVector(IGaKVectorStorage<T> storage1, IGaKVectorStorage<T> storage2)
        {
            if (storage1.Grade < 1 || storage2.Grade < 1)
                return ScalarProcessor.CreateStorageZeroScalar();

            var grade = (uint) Math.Abs(storage1.Grade - storage2.Grade);
            
            var storage = ScalarProcessor.CreateKVectorStorageComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetERcpIdScalarRecords()
            );

            return storage.CreateGaKVectorStorage(grade);
        }

        public IGaKVectorStorage<T> GetEFdpKVector(IGaKVectorStorage<T> storage1, IGaKVectorStorage<T> storage2)
        {
            var grade = (uint) Math.Abs(storage1.Grade - storage2.Grade);
            var storage = ScalarProcessor.CreateKVectorStorageComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetEFdpIdScalarRecords()
            );

            return storage.CreateGaKVectorStorage(grade);
        }


        public T GetSpScalar(IGaKVectorStorage<T> storage1, IGaKVectorStorage<T> storage2, GaSignature basesSignature)
        {
            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            return ScalarProcessor.Add(
                _termsIterator.GetSpScalars(basesSignature)
            );
        }

        public IGaKVectorStorage<T> GetLcpKVector(IGaKVectorStorage<T> storage1, IGaKVectorStorage<T> storage2, GaSignature basesSignature)
        {
            if (storage2.Grade < storage1.Grade)
                return ScalarProcessor.CreateStorageZeroScalar();

            var grade = storage2.Grade - storage1.Grade;

            var storage = ScalarProcessor.CreateKVectorStorageComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetLcpIdScalarRecords(basesSignature)
            );

            return storage.CreateGaKVectorStorage(grade);
        }

        public IGaKVectorStorage<T> GetRcpKVector(IGaKVectorStorage<T> storage1, IGaKVectorStorage<T> storage2, GaSignature basesSignature)
        {
            if (storage1.Grade < storage2.Grade)
                return ScalarProcessor.CreateStorageZeroScalar();

            var grade = storage1.Grade - storage2.Grade;

            var storage = ScalarProcessor.CreateKVectorStorageComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetRcpIdScalarRecords(basesSignature)
            );

            return storage.CreateGaKVectorStorage(grade);
        }

        public IGaKVectorStorage<T> GetHipKVector(IGaKVectorStorage<T> storage1, IGaKVectorStorage<T> storage2, GaSignature basesSignature)
        {
            if (storage1.Grade < 1 || storage2.Grade < 1)
                return ScalarProcessor.CreateStorageZeroScalar();

            var grade = (uint) Math.Abs(storage1.Grade - storage2.Grade);
            
            var storage = ScalarProcessor.CreateKVectorStorageComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetRcpIdScalarRecords(basesSignature)
            );

            return storage.CreateGaKVectorStorage(grade);
        }

        public IGaKVectorStorage<T> GetFdpKVector(IGaKVectorStorage<T> storage1, IGaKVectorStorage<T> storage2, GaSignature basesSignature)
        {
            var grade = (uint) Math.Abs(storage1.Grade - storage2.Grade);
            var storage = ScalarProcessor.CreateKVectorStorageComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetFdpIdScalarRecords(basesSignature)
            );

            return storage.CreateGaKVectorStorage(grade);
        }


        public IEnumerable<IGaKVectorStorage<T>> GetOpKVectors()
        {
            foreach (var storage1 in Storages1)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetOpKVectors2(grade1))
                    yield return GetOpKVector(storage1, storage2);
            }
        }

        public IEnumerable<T> GetESpScalars()
        {
            foreach (var storage1 in Storages1)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetESpKVector2(grade1))
                    yield return GetESpScalar(storage1, storage2);
            }
        }

        public IEnumerable<IGaKVectorStorage<T>> GetELcpKVectors()
        {
            foreach (var storage1 in Storages1)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetELcpKVectors2(grade1))
                    yield return GetELcpKVector(storage1, storage2);
            }
        }

        public IEnumerable<IGaKVectorStorage<T>> GetERcpKVectors()
        {
            foreach (var storage1 in Storages1)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetERcpKVectors2(grade1))
                    yield return GetERcpKVector(storage1, storage2);
            }
        }

        public IEnumerable<IGaKVectorStorage<T>> GetEHipKVectors()
        {
            foreach (var storage1 in Storages1.Where(s => s.Grade > 0))
            foreach (var storage2 in Storages2.Where(s => s.Grade > 0))
                yield return GetEHipKVector(storage1, storage2);
        }

        public IEnumerable<IGaKVectorStorage<T>> GetEFdpKVectors()
        {
            foreach (var storage1 in Storages1)
            foreach (var storage2 in Storages2)
                yield return GetEFdpKVector(storage1, storage2);
        }


        public IEnumerable<IGaKVectorStorage<T>> GetELcpKVectors(GaSignature basesSignature)
        {
            foreach (var storage1 in Storages1)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetELcpKVectors2(grade1))
                    yield return GetLcpKVector(storage1, storage2, basesSignature);
            }
        }

        public IEnumerable<IGaKVectorStorage<T>> GetERcpKVectors(GaSignature basesSignature)
        {
            foreach (var storage1 in Storages1)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetERcpKVectors2(grade1))
                    yield return GetRcpKVector(storage1, storage2, basesSignature);
            }
        }

        public IEnumerable<IGaKVectorStorage<T>> GetEHipKVectors(GaSignature basesSignature)
        {
            foreach (var storage1 in Storages1.Where(s => s.Grade > 0))
            foreach (var storage2 in Storages2.Where(s => s.Grade > 0))
                yield return GetHipKVector(storage1, storage2, basesSignature);
        }

        public IEnumerable<IGaKVectorStorage<T>> GetEFdpKVectors(GaSignature basesSignature)
        {
            foreach (var storage1 in Storages1)
            foreach (var storage2 in Storages2)
                yield return GetFdpKVector(storage1, storage2, basesSignature);
        }
    }
}