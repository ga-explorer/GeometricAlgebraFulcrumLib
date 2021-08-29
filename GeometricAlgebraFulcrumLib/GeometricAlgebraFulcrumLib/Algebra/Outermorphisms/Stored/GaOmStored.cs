using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Algebra.Outermorphisms.Stored
{
    public sealed class GaOmStored<T> :
        IGaOutermorphism<T>, IReadOnlyDictionary<ulong, IGaKVectorStorage<T>>
    {
        private readonly Dictionary<ulong, IGaKVectorStorage<T>> _mappedBasisBladesDictionary
            = new Dictionary<ulong, IGaKVectorStorage<T>>();


        public ILaProcessor<T> ScalarsGridProcessor { get; }

        public IGaKVectorStorage<T> MappedPseudoScalar
        {
            get
            {
                var id = (VSpaceDimension.ToGaSpaceDimension()) - 1;

                return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector)
                    ? mappedKVector
                    : ScalarsGridProcessor.CreateZeroKVectorStorage(VSpaceDimension);
            }
        }


        public IGaSpace Space { get; }
        public uint VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => VSpaceDimension.ToGaSpaceDimension();

        public ulong MaxBasisBladeId 
            => (VSpaceDimension.ToGaSpaceDimension()) - 1UL;

        public uint GradesCount 
            => VSpaceDimension + 1;

        public IEnumerable<uint> Grades 
            => GradesCount.GetRange();

        public int Count 
            => _mappedBasisBladesDictionary.Count;

        public IGaKVectorStorage<T> this[ulong id]
        {
            get
            {
                if (id >= GaSpaceDimension)
                    throw new IndexOutOfRangeException(nameof(id));

                return _mappedBasisBladesDictionary.TryGetValue(id, out var value) && !ReferenceEquals(value, null)
                    ? value
                    : ScalarsGridProcessor.CreateZeroKVectorStorage(id.BasisBladeIdToGrade());
            }
            set
            {
                if (id >= GaSpaceDimension)
                    throw new IndexOutOfRangeException(nameof(id));

                if (ReferenceEquals(value, null))
                {
                    _mappedBasisBladesDictionary.Remove(id);
                    return;
                }

                if (value.Grade != id.BasisBladeIdToGrade())
                    throw new InvalidOperationException();

                if (_mappedBasisBladesDictionary.ContainsKey(id))
                    _mappedBasisBladesDictionary[id] = value;

                else
                    _mappedBasisBladesDictionary.Add(id, value);
            }
        }
        
        public IGaKVectorStorage<T> this[uint grade, ulong index]
        {
            get => this[index.BasisBladeIndexToId(grade)];
            set => this[index.BasisBladeIndexToId(grade)] = value;
        }

        public IEnumerable<ulong> Keys 
            => _mappedBasisBladesDictionary.Keys;

        public IEnumerable<IGaKVectorStorage<T>> Values 
            => _mappedBasisBladesDictionary.Values;


        internal GaOmStored([NotNull] ILaProcessor<T> arrayProcessor, uint vSpaceDimension)
        {
            ScalarsGridProcessor = arrayProcessor;
            VSpaceDimension = vSpaceDimension;
        }


        public bool ContainsKey(ulong id)
        {
            return _mappedBasisBladesDictionary.ContainsKey(id);
        }

        public bool TryGetValue(ulong id, out IGaKVectorStorage<T> kVector)
        {
            return _mappedBasisBladesDictionary.TryGetValue(id, out kVector);
        }

        public IReadOnlyList<IGaVectorStorage<T>> GetMappedBasisVectors()
        {
            var mappedBasisVectorsList = 
                new List<IGaVectorStorage<T>>((int) VSpaceDimension);

            for (var index = 0; index < VSpaceDimension; index++)
            {
                mappedBasisVectorsList.Add(
                    _mappedBasisBladesDictionary.TryGetValue(index.BasisVectorIndexToId(), out var mappedKVector)
                        ? mappedKVector.GetVectorPart()
                        : GaVectorStorage<T>.ZeroVector
                    );
            }

            return mappedBasisVectorsList;
        }


        public IGaVectorStorage<T> MapBasisVector(ulong index)
        {
            return _mappedBasisBladesDictionary.TryGetValue(index.BasisVectorIndexToId(), out var mappedKVector)
                ? mappedKVector.GetVectorPart()
                : GaVectorStorage<T>.ZeroVector;
        }

        public IGaBivectorStorage<T> MapBasisBivector(ulong index1, ulong index2)
        {
            if (index1 == index2)
                return GaBivectorStorage<T>.ZeroBivector;

            var id = GaBasisBivectorUtils.BasisBivectorId(index1, index2);

            if (_mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector))
                return index1 < index2
                    ? mappedKVector.GetBivectorPart()
                    : ScalarsGridProcessor.GetNegativeBivectorPart(mappedKVector);

            return GaBivectorStorage<T>.ZeroBivector;
        }

        public IGaKVectorStorage<T> MapBasisBlade(ulong id)
        {
            return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector)
                ? mappedKVector
                : ScalarsGridProcessor.CreateZeroKVectorStorage(id.BasisBladeIdToGrade());
        }

        public IGaKVectorStorage<T> MapBasisBlade(uint grade, ulong index)
        {
            var id = index.BasisBladeIndexToId(grade);

            return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector)
                ? mappedKVector
                : ScalarsGridProcessor.CreateZeroKVectorStorage(grade);
        }

        public IGaVectorStorage<T> MapVector(IGaVectorStorage<T> vector)
        {
            var storage = ScalarsGridProcessor.CreateKVectorStorageComposer(1);

            foreach (var (index, scalar) in vector.IndexScalarList.GetIndexScalarRecords())
                if (_mappedBasisBladesDictionary.TryGetValue(index.BasisVectorIndexToId(), out var mappedKVector))
                    storage.AddScaledTerms(
                        scalar, 
                        mappedKVector.IndexScalarList.GetIndexScalarRecords()
                    );

            storage.RemoveZeroTerms();

            return storage.CreateGaVectorStorage();
        }

        public IGaBivectorStorage<T> MapBivector(IGaBivectorStorage<T> bivector)
        {
            var storage = ScalarsGridProcessor.CreateKVectorStorageComposer();

            foreach (var (index, scalar) in bivector.IndexScalarList.GetIndexScalarRecords())
                if (_mappedBasisBladesDictionary.TryGetValue(index.BasisBivectorIndexToId(), out var mappedKVector))
                    storage.AddScaledTerms(
                        scalar, 
                        mappedKVector.IndexScalarList.GetIndexScalarRecords()
                    );

            storage.RemoveZeroTerms();

            return storage.CreateGaBivectorStorage();
        }

        public IGaKVectorStorage<T> MapKVector(IGaKVectorStorage<T> kVector)
        {
            var grade = kVector.Grade;
            var storage = ScalarsGridProcessor.CreateKVectorStorageComposer();

            foreach (var (index, scalar) in kVector.IndexScalarList.GetIndexScalarRecords())
                if (_mappedBasisBladesDictionary.TryGetValue(index.BasisBladeIndexToId(grade), out var mappedKVector))
                    storage.AddScaledTerms(
                        scalar, 
                        mappedKVector.IndexScalarList.GetIndexScalarRecords()
                    );

            storage.RemoveZeroTerms();

            return storage.CreateGaKVectorStorage(grade);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> mv)
        {
            var storage = ScalarsGridProcessor.CreateStorageSparseMultivectorComposer();

            foreach (var (id, scalar) in mv.GetIdScalarRecords())
                if (_mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector))
                    storage.AddScaledTerms(
                        scalar, 
                        mappedKVector.GetIdScalarRecords()
                    );

            storage.RemoveZeroTerms();

            return storage.CreateGaMultivectorSparseStorage();
        }

        public IEnumerator<KeyValuePair<ulong, IGaKVectorStorage<T>>> GetEnumerator()
        {
            return _mappedBasisBladesDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}