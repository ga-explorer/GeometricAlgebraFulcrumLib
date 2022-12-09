using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps
{
    public sealed class GaSparseUnilinearMap<T> :
        IGaUnilinearMap<T>, 
        IReadOnlyDictionary<ulong, IMultivectorStorage<T>>
    {
        private readonly Dictionary<ulong, IMultivectorStorage<T>> _mappedBasisBladesDictionary
            = new Dictionary<ulong, IMultivectorStorage<T>>();


        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => GeometricProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public int Count 
            => _mappedBasisBladesDictionary.Count;

        public IMultivectorStorage<T> this[ulong id]
        {
            get =>
                _mappedBasisBladesDictionary.TryGetValue(id, out var value) && !ReferenceEquals(value, null)
                    ? value
                    : GeometricProcessor.CreateKVectorStorageBasis(id.BasisBladeIdToGrade());
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


        internal GaSparseUnilinearMap([NotNull] IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            GeometricProcessor = geometricProcessor;
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

        public IGaUnilinearMap<T> GetAdjoint()
        {
            throw new NotImplementedException();
        }

        public GaMultivector<T> MapBasisScalar()
        {
            throw new NotImplementedException();
        }

        public GaMultivector<T> MapBasisVector(ulong index)
        {
            throw new NotImplementedException();
        }

        public GaMultivector<T> MapBasisBivector(ulong index)
        {
            throw new NotImplementedException();
        }

        public GaMultivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            throw new NotImplementedException();
        }

        public GaMultivector<T> MapBasisBlade(ulong id)
        {
            return GeometricProcessor.CreateMultivector(
                _mappedBasisBladesDictionary.TryGetValue(id, out var mappedMultivector)
                    ? mappedMultivector
                    : KVectorStorage<T>.ZeroScalar
            );
        }

        public GaMultivector<T> MapBasisBlade(uint grade, ulong index)
        {
            var id = index.BasisBladeIndexToId(grade);

            return GeometricProcessor.CreateMultivector(
                _mappedBasisBladesDictionary.TryGetValue(id, out var mappedMultivector)
                    ? mappedMultivector
                    : KVectorStorage<T>.ZeroScalar
                );
        }

        public GaMultivector<T> Map(T mv)
        {
            throw new NotImplementedException();
        }

        public GaMultivector<T> Map(GaVector<T> vector)
        {
            throw new NotImplementedException();
        }

        public GaMultivector<T> Map(GaBivector<T> bivector)
        {
            throw new NotImplementedException();
        }

        public GaMultivector<T> Map(GaKVector<T> kVector)
        {
            throw new NotImplementedException();
        }

        public GaMultivector<T> Map(GaMultivector<T> multivector)
        {
            throw new NotImplementedException();
        }

        public VectorStorage<T> OmMap(VectorStorage<T> mv)
        {
            throw new NotImplementedException();
        }

        public BivectorStorage<T> OmMap(BivectorStorage<T> mv)
        {
            throw new NotImplementedException();
        }

        public KVectorStorage<T> OmMap(KVectorStorage<T> mv)
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

        public MultivectorStorage<T> OmMap(MultivectorStorage<T> mv)
        {
            var storage = 
                GeometricProcessor.CreateVectorStorageComposer();

            foreach (var (id, scalar) in mv.GetIdScalarRecords())
                if (_mappedBasisBladesDictionary.TryGetValue(id, out var mappedMultivector))
                    storage.AddScaledTerms(
                        scalar, 
                        mappedMultivector.GetIdScalarRecords()
                    );

            storage.RemoveZeroTerms();

            return storage.CreateMultivectorStorageSparse();
        }

        public MultivectorGradedStorage<T> OmMap(MultivectorGradedStorage<T> mv)
        {
            throw new NotImplementedException();
        }

        public ILinMatrixStorage<T> GetMultivectorMappingMatrixStorage()
        {
            throw new NotImplementedException();
        }

        public LinMatrix<T> GetMultivectorMappingMatrix()
        {
            return GetMultivectorMappingMatrixStorage().CreateLinMatrix(LinearProcessor);
        }

        public IEnumerable<IdMultivectorRecord<T>> GetMappedBasisBlades()
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