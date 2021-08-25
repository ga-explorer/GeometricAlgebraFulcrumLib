using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Binary;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Utils;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

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

        public static GaProductKVectorsIterator<T> Create(IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<IGaStorageKVector<T>> storageList1, IReadOnlyList<IGaStorageKVector<T>> storageList2)
        {
            return new GaProductKVectorsIterator<T>(
                scalarProcessor,
                storageList1,
                storageList2
            );
        }


        private readonly GaProductTermsIterator<T> _termsIterator;


        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public IReadOnlyList<IGaStorageKVector<T>> Storages1 { get; }

        public IReadOnlyList<IGaStorageKVector<T>> Storages2 { get; }


        private GaProductKVectorsIterator([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] IReadOnlyList<IGaStorageKVector<T>> storageList1, [NotNull] IReadOnlyList<IGaStorageKVector<T>> storageList2)
        {
            _termsIterator = GaProductTermsIterator<T>.Create(scalarProcessor);
            ScalarProcessor = scalarProcessor;
            Storages1 = storageList1;
            Storages2 = storageList2;
        }


        private IEnumerable<IGaStorageKVector<T>> GetOpKVectors2(uint grade1)
        {
            return Storages2
                .Where(storage => 
                    grade1 + storage.Grade <= GaSpaceUtils.MaxVSpaceDimension
                );
        }

        private IEnumerable<IGaStorageKVector<T>> GetESpKVector2(uint grade1)
        {
            return Storages2.Where(s => s.Grade == grade1);
        }

        private IEnumerable<IGaStorageKVector<T>> GetELcpKVectors2(uint grade1)
        {
            return Storages2.Where(storage => 
                storage.Grade >= grade1
            );
        }

        private IEnumerable<IGaStorageKVector<T>> GetERcpKVectors2(uint grade1)
        {
            return Storages2.Where(storage => 
                grade1 >= storage.Grade
            );
        }
        

        public IGaStorageKVector<T> GetOpKVector(IGaStorageKVector<T> storage1, IGaStorageKVector<T> storage2)
        {
            var grade = storage1.Grade + storage2.Grade;

            if (grade > GaSpaceUtils.MaxVSpaceDimension)
                return ScalarProcessor.CreateStorageZeroScalar();

            var storage = ScalarProcessor.CreateStorageKVectorComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetOpIdScalarRecords()
            );

            return storage.CreateStorageKVector(grade);
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

            var storage = ScalarProcessor.CreateStorageKVectorComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetELcpIdScalarRecords()
            );

            return storage.CreateStorageKVector(grade);
        }

        public IGaStorageKVector<T> GetERcpKVector(IGaStorageKVector<T> storage1, IGaStorageKVector<T> storage2)
        {
            if (storage1.Grade < storage2.Grade)
                return ScalarProcessor.CreateStorageZeroScalar();

            var grade = storage1.Grade - storage2.Grade;

            var storage = ScalarProcessor.CreateStorageKVectorComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetERcpIdScalarRecords()
            );

            return storage.CreateStorageKVector(grade);
        }

        public IGaStorageKVector<T> GetEHipKVector(IGaStorageKVector<T> storage1, IGaStorageKVector<T> storage2)
        {
            if (storage1.Grade < 1 || storage2.Grade < 1)
                return ScalarProcessor.CreateStorageZeroScalar();

            var grade = (uint) Math.Abs(storage1.Grade - storage2.Grade);
            
            var storage = ScalarProcessor.CreateStorageKVectorComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetERcpIdScalarRecords()
            );

            return storage.CreateStorageKVector(grade);
        }

        public IGaStorageKVector<T> GetEFdpKVector(IGaStorageKVector<T> storage1, IGaStorageKVector<T> storage2)
        {
            var grade = (uint) Math.Abs(storage1.Grade - storage2.Grade);
            var storage = ScalarProcessor.CreateStorageKVectorComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetEFdpIdScalarRecords()
            );

            return storage.CreateStorageKVector(grade);
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

            var storage = ScalarProcessor.CreateStorageKVectorComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetLcpIdScalarRecords(basesSignature)
            );

            return storage.CreateStorageKVector(grade);
        }

        public IGaStorageKVector<T> GetRcpKVector(IGaStorageKVector<T> storage1, IGaStorageKVector<T> storage2, GaSignature basesSignature)
        {
            if (storage1.Grade < storage2.Grade)
                return ScalarProcessor.CreateStorageZeroScalar();

            var grade = storage1.Grade - storage2.Grade;

            var storage = ScalarProcessor.CreateStorageKVectorComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetRcpIdScalarRecords(basesSignature)
            );

            return storage.CreateStorageKVector(grade);
        }

        public IGaStorageKVector<T> GetHipKVector(IGaStorageKVector<T> storage1, IGaStorageKVector<T> storage2, GaSignature basesSignature)
        {
            if (storage1.Grade < 1 || storage2.Grade < 1)
                return ScalarProcessor.CreateStorageZeroScalar();

            var grade = (uint) Math.Abs(storage1.Grade - storage2.Grade);
            
            var storage = ScalarProcessor.CreateStorageKVectorComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetRcpIdScalarRecords(basesSignature)
            );

            return storage.CreateStorageKVector(grade);
        }

        public IGaStorageKVector<T> GetFdpKVector(IGaStorageKVector<T> storage1, IGaStorageKVector<T> storage2, GaSignature basesSignature)
        {
            var grade = (uint) Math.Abs(storage1.Grade - storage2.Grade);
            var storage = ScalarProcessor.CreateStorageKVectorComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetFdpIdScalarRecords(basesSignature)
            );

            return storage.CreateStorageKVector(grade);
        }


        public IEnumerable<IGaStorageKVector<T>> GetOpKVectors()
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

        public IEnumerable<IGaStorageKVector<T>> GetELcpKVectors()
        {
            foreach (var storage1 in Storages1)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetELcpKVectors2(grade1))
                    yield return GetELcpKVector(storage1, storage2);
            }
        }

        public IEnumerable<IGaStorageKVector<T>> GetERcpKVectors()
        {
            foreach (var storage1 in Storages1)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetERcpKVectors2(grade1))
                    yield return GetERcpKVector(storage1, storage2);
            }
        }

        public IEnumerable<IGaStorageKVector<T>> GetEHipKVectors()
        {
            foreach (var storage1 in Storages1.Where(s => s.Grade > 0))
            foreach (var storage2 in Storages2.Where(s => s.Grade > 0))
                yield return GetEHipKVector(storage1, storage2);
        }

        public IEnumerable<IGaStorageKVector<T>> GetEFdpKVectors()
        {
            foreach (var storage1 in Storages1)
            foreach (var storage2 in Storages2)
                yield return GetEFdpKVector(storage1, storage2);
        }


        public IEnumerable<IGaStorageKVector<T>> GetELcpKVectors(GaSignature basesSignature)
        {
            foreach (var storage1 in Storages1)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetELcpKVectors2(grade1))
                    yield return GetLcpKVector(storage1, storage2, basesSignature);
            }
        }

        public IEnumerable<IGaStorageKVector<T>> GetERcpKVectors(GaSignature basesSignature)
        {
            foreach (var storage1 in Storages1)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetERcpKVectors2(grade1))
                    yield return GetRcpKVector(storage1, storage2, basesSignature);
            }
        }

        public IEnumerable<IGaStorageKVector<T>> GetEHipKVectors(GaSignature basesSignature)
        {
            foreach (var storage1 in Storages1.Where(s => s.Grade > 0))
            foreach (var storage2 in Storages2.Where(s => s.Grade > 0))
                yield return GetHipKVector(storage1, storage2, basesSignature);
        }

        public IEnumerable<IGaStorageKVector<T>> GetEFdpKVectors(GaSignature basesSignature)
        {
            foreach (var storage1 in Storages1)
            foreach (var storage2 in Storages2)
                yield return GetFdpKVector(storage1, storage2, basesSignature);
        }
    }
}