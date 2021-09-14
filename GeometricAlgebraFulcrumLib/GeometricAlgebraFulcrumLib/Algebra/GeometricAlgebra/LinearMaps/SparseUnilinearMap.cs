using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps
{
    public sealed class SparseUnilinearMap<T> :
        IUnilinearMap<T>, 
        IReadOnlyDictionary<ulong, IMultivectorStorage<T>>
    {
        private readonly Dictionary<ulong, IMultivectorStorage<T>> _mappedBasisBladesDictionary
            = new Dictionary<ulong, IMultivectorStorage<T>>();


        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => LinearProcessor;
        
        public ILinearAlgebraProcessor<T> LinearProcessor { get; }

        public int Count 
            => _mappedBasisBladesDictionary.Count;

        public IMultivectorStorage<T> this[ulong id]
        {
            get =>
                _mappedBasisBladesDictionary.TryGetValue(id, out var value) && !ReferenceEquals(value, null)
                    ? value
                    : LinearProcessor.CreateKVectorBasisStorage(id.BasisBladeIdToGrade());
            set
            {
                if (ReferenceEquals(value, null))
                {
                    _mappedBasisBladesDictionary.Remove(id);
                    return;
                }

                if (_mappedBasisBladesDictionary.ContainsKey(id))
                    _mappedBasisBladesDictionary[id] = value;

                else
                    _mappedBasisBladesDictionary.Add(id, value);
            }
        }
        
        public IMultivectorStorage<T> this[uint grade, ulong index]
        {
            get => this[index.BasisBladeIndexToId(grade)];
            set => this[index.BasisBladeIndexToId(grade)] = value;
        }

        public IEnumerable<ulong> Keys 
            => _mappedBasisBladesDictionary.Keys;

        public IEnumerable<IMultivectorStorage<T>> Values 
            => _mappedBasisBladesDictionary.Values;


        internal SparseUnilinearMap([NotNull] ILinearAlgebraProcessor<T> linearProcessor)
        {
            LinearProcessor = linearProcessor;
        }


        public bool ContainsKey(ulong id)
        {
            return _mappedBasisBladesDictionary.ContainsKey(id);
        }

        public bool TryGetValue(ulong id, out IMultivectorStorage<T> multivector)
        {
            return _mappedBasisBladesDictionary.TryGetValue(id, out multivector);
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        public bool IsInvalid()
        {
            throw new NotImplementedException();
        }

        public IUnilinearMap<T> GetAdjoint()
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapBasisScalar()
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapBasisVector(ulong index)
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapBasisBivector(ulong index)
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapBasisBivector(ulong index1, ulong index2)
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapBasisBlade(ulong id)
        {
            return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedMultivector)
                ? mappedMultivector
                : KVectorStorage<T>.ZeroScalar;
        }

        public IMultivectorStorage<T> MapBasisBlade(uint grade, ulong index)
        {
            var id = index.BasisBladeIndexToId(grade);

            return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedMultivector)
                ? mappedMultivector
                : KVectorStorage<T>.ZeroScalar;
        }

        public IMultivectorStorage<T> MapScalar(T mv)
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapVector(VectorStorage<T> vector)
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapBivector(BivectorStorage<T> bivector)
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapKVector(KVectorStorage<T> kVector)
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapMultivector(MultivectorStorage<T> multivector)
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> MapMultivector(MultivectorGradedStorage<T> multivector)
        {
            throw new NotImplementedException();
        }

        public VectorStorage<T> OmMapVector(VectorStorage<T> mv)
        {
            throw new NotImplementedException();
        }

        public BivectorStorage<T> OmMapBivector(BivectorStorage<T> mv)
        {
            throw new NotImplementedException();
        }

        public KVectorStorage<T> OmMapKVector(KVectorStorage<T> mv)
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> Map(ulong id)
        {
            throw new NotImplementedException();
        }

        public IMultivectorStorage<T> Map(uint grade, ulong index)
        {
            throw new NotImplementedException();
        }

        public MultivectorStorage<T> OmMapMultivector(MultivectorStorage<T> mv)
        {
            var storage = 
                LinearProcessor.CreateVectorStorageComposer();

            foreach (var (id, scalar) in mv.GetIdScalarRecords())
                if (_mappedBasisBladesDictionary.TryGetValue(id, out var mappedMultivector))
                    storage.AddScaledTerms(
                        scalar, 
                        mappedMultivector.GetIdScalarRecords()
                    );

            storage.RemoveZeroTerms();

            return storage.CreateMultivectorSparseStorage();
        }

        public MultivectorGradedStorage<T> OmMapMultivector(MultivectorGradedStorage<T> mv)
        {
            throw new NotImplementedException();
        }

        public ILinMatrixStorage<T> GetMultivectorMappingMatrix()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IdMultivectorStorageRecord<T>> GetMappedBasisBlades()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<ulong, IMultivectorStorage<T>>> GetEnumerator()
        {
            return _mappedBasisBladesDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ILinVectorStorage<T> LinMapBasisVector(ulong index)
        {
            throw new NotImplementedException();
        }

        public ILinVectorStorage<T> LinMapVector(ILinVectorStorage<T> vectorStorage)
        {
            throw new NotImplementedException();
        }

        public ILinMatrixStorage<T> LinMapMatrix(ILinMatrixStorage<T> matrixStorage)
        {
            throw new NotImplementedException();
        }

        public ILinMatrixStorage<T> GetLinMappingMatrix()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndexLinVectorStorageRecord<T>> GetLinMappedBasisVectors()
        {
            throw new NotImplementedException();
        }
    }
}