using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.ProductIterators
{
    public sealed class MultivectorStorageKVectorsIterator<T>
    {
        public static MultivectorStorageKVectorsIterator<T> Create(Multivector<T> mv1, Multivector<T> mv2)
        {
            return new(
                mv1.GeometricProcessor,
                mv1.MultivectorStorage.GetKVectorStorages().ToArray(),
                mv2.MultivectorStorage.GetKVectorStorages().ToArray()
            );
        }

        public static MultivectorStorageKVectorsIterator<T> Create(IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyList<KVectorStorage<T>> storageList1, IReadOnlyList<KVectorStorage<T>> storageList2)
        {
            return new MultivectorStorageKVectorsIterator<T>(
                scalarProcessor,
                storageList1,
                storageList2
            );
        }


        private readonly MultivectorStorageTermsIterator<T> _termsIterator;


        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public IReadOnlyList<KVectorStorage<T>> Storages1 { get; }

        public IReadOnlyList<KVectorStorage<T>> Storages2 { get; }


        private MultivectorStorageKVectorsIterator([NotNull] IScalarAlgebraProcessor<T> scalarProcessor, [NotNull] IReadOnlyList<KVectorStorage<T>> storageList1, [NotNull] IReadOnlyList<KVectorStorage<T>> storageList2)
        {
            _termsIterator = MultivectorStorageTermsIterator<T>.Create(scalarProcessor);
            ScalarProcessor = scalarProcessor;
            Storages1 = storageList1;
            Storages2 = storageList2;
        }


        private IEnumerable<KVectorStorage<T>> GetOpKVectors2(uint grade1)
        {
            return Storages2
                .Where(storage => 
                    grade1 + storage.Grade <= GeometricAlgebraSpaceUtils.MaxVSpaceDimension
                );
        }

        private IEnumerable<KVectorStorage<T>> GetESpKVector2(uint grade1)
        {
            return Storages2.Where(s => s.Grade == grade1);
        }

        private IEnumerable<KVectorStorage<T>> GetELcpKVectors2(uint grade1)
        {
            return Storages2.Where(storage => 
                storage.Grade >= grade1
            );
        }

        private IEnumerable<KVectorStorage<T>> GetERcpKVectors2(uint grade1)
        {
            return Storages2.Where(storage => 
                grade1 >= storage.Grade
            );
        }
        

        public KVectorStorage<T> GetOpKVector(KVectorStorage<T> storage1, KVectorStorage<T> storage2)
        {
            var grade = storage1.Grade + storage2.Grade;

            if (grade > GeometricAlgebraSpaceUtils.MaxVSpaceDimension)
                return KVectorStorage<T>.ZeroScalar;

            var storage = ScalarProcessor.CreateVectorStorageComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetOpIdScalarRecords()
            );

            return storage.CreateKVectorStorage(grade);
        }

        public T GetESpScalar(KVectorStorage<T> storage1, KVectorStorage<T> storage2)
        {
            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            return ScalarProcessor.Add(
                _termsIterator.GetESpScalars()
            );
        }

        public KVectorStorage<T> GetELcpKVector(KVectorStorage<T> storage1, KVectorStorage<T> storage2)
        {
            if (storage2.Grade < storage1.Grade)
                return KVectorStorage<T>.ZeroScalar;

            var grade = storage2.Grade - storage1.Grade;

            var storage = ScalarProcessor.CreateVectorStorageComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetELcpIdScalarRecords()
            );

            return storage.CreateKVectorStorage(grade);
        }

        public KVectorStorage<T> GetERcpKVector(KVectorStorage<T> storage1, KVectorStorage<T> storage2)
        {
            if (storage1.Grade < storage2.Grade)
                return KVectorStorage<T>.ZeroScalar;

            var grade = storage1.Grade - storage2.Grade;

            var storage = ScalarProcessor.CreateVectorStorageComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetERcpIdScalarRecords()
            );

            return storage.CreateKVectorStorage(grade);
        }

        public KVectorStorage<T> GetEHipKVector(KVectorStorage<T> storage1, KVectorStorage<T> storage2)
        {
            if (storage1.Grade < 1 || storage2.Grade < 1)
                return KVectorStorage<T>.ZeroScalar;

            var grade = (uint) Math.Abs(storage1.Grade - storage2.Grade);
            
            var storage = ScalarProcessor.CreateVectorStorageComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetERcpIdScalarRecords()
            );

            return storage.CreateKVectorStorage(grade);
        }

        public KVectorStorage<T> GetEFdpKVector(KVectorStorage<T> storage1, KVectorStorage<T> storage2)
        {
            var grade = (uint) Math.Abs(storage1.Grade - storage2.Grade);
            var storage = ScalarProcessor.CreateVectorStorageComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetEFdpIdScalarRecords()
            );

            return storage.CreateKVectorStorage(grade);
        }


        public T GetSpScalar(KVectorStorage<T> storage1, KVectorStorage<T> storage2, GeometricAlgebraBasisSet basisSet)
        {
            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            return ScalarProcessor.Add(
                _termsIterator.GetSpScalars(basisSet)
            );
        }

        public KVectorStorage<T> GetLcpKVector(KVectorStorage<T> storage1, KVectorStorage<T> storage2, GeometricAlgebraBasisSet basisSet)
        {
            if (storage2.Grade < storage1.Grade)
                return KVectorStorage<T>.ZeroScalar;

            var grade = storage2.Grade - storage1.Grade;

            var storage = ScalarProcessor.CreateVectorStorageComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetLcpIdScalarRecords(basisSet)
            );

            return storage.CreateKVectorStorage(grade);
        }

        public KVectorStorage<T> GetRcpKVector(KVectorStorage<T> storage1, KVectorStorage<T> storage2, GeometricAlgebraBasisSet basisSet)
        {
            if (storage1.Grade < storage2.Grade)
                return KVectorStorage<T>.ZeroScalar;

            var grade = storage1.Grade - storage2.Grade;

            var storage = ScalarProcessor.CreateVectorStorageComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetRcpIdScalarRecords(basisSet)
            );

            return storage.CreateKVectorStorage(grade);
        }

        public KVectorStorage<T> GetHipKVector(KVectorStorage<T> storage1, KVectorStorage<T> storage2, GeometricAlgebraBasisSet basisSet)
        {
            if (storage1.Grade < 1 || storage2.Grade < 1)
                return KVectorStorage<T>.ZeroScalar;

            var grade = (uint) Math.Abs(storage1.Grade - storage2.Grade);
            
            var storage = ScalarProcessor.CreateVectorStorageComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetRcpIdScalarRecords(basisSet)
            );

            return storage.CreateKVectorStorage(grade);
        }

        public KVectorStorage<T> GetFdpKVector(KVectorStorage<T> storage1, KVectorStorage<T> storage2, GeometricAlgebraBasisSet basisSet)
        {
            var grade = (uint) Math.Abs(storage1.Grade - storage2.Grade);
            var storage = ScalarProcessor.CreateVectorStorageComposer();

            _termsIterator.Storage1 = storage1;
            _termsIterator.Storage2 = storage2;

            storage.AddTerms(
                _termsIterator.GetFdpIdScalarRecords(basisSet)
            );

            return storage.CreateKVectorStorage(grade);
        }


        public IEnumerable<KVectorStorage<T>> GetOpKVectors()
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

        public IEnumerable<KVectorStorage<T>> GetELcpKVectors()
        {
            foreach (var storage1 in Storages1)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetELcpKVectors2(grade1))
                    yield return GetELcpKVector(storage1, storage2);
            }
        }

        public IEnumerable<KVectorStorage<T>> GetERcpKVectors()
        {
            foreach (var storage1 in Storages1)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetERcpKVectors2(grade1))
                    yield return GetERcpKVector(storage1, storage2);
            }
        }

        public IEnumerable<KVectorStorage<T>> GetEHipKVectors()
        {
            foreach (var storage1 in Storages1.Where(s => s.Grade > 0))
            foreach (var storage2 in Storages2.Where(s => s.Grade > 0))
                yield return GetEHipKVector(storage1, storage2);
        }

        public IEnumerable<KVectorStorage<T>> GetEFdpKVectors()
        {
            foreach (var storage1 in Storages1)
            foreach (var storage2 in Storages2)
                yield return GetEFdpKVector(storage1, storage2);
        }


        public IEnumerable<KVectorStorage<T>> GetELcpKVectors(GeometricAlgebraBasisSet basisSet)
        {
            foreach (var storage1 in Storages1)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetELcpKVectors2(grade1))
                    yield return GetLcpKVector(storage1, storage2, basisSet);
            }
        }

        public IEnumerable<KVectorStorage<T>> GetERcpKVectors(GeometricAlgebraBasisSet basisSet)
        {
            foreach (var storage1 in Storages1)
            {
                var grade1 = storage1.Grade;

                foreach (var storage2 in GetERcpKVectors2(grade1))
                    yield return GetRcpKVector(storage1, storage2, basisSet);
            }
        }

        public IEnumerable<KVectorStorage<T>> GetEHipKVectors(GeometricAlgebraBasisSet basisSet)
        {
            foreach (var storage1 in Storages1.Where(s => s.Grade > 0))
            foreach (var storage2 in Storages2.Where(s => s.Grade > 0))
                yield return GetHipKVector(storage1, storage2, basisSet);
        }

        public IEnumerable<KVectorStorage<T>> GetEFdpKVectors(GeometricAlgebraBasisSet basisSet)
        {
            foreach (var storage1 in Storages1)
            foreach (var storage2 in Storages2)
                yield return GetFdpKVector(storage1, storage2, basisSet);
        }
    }
}