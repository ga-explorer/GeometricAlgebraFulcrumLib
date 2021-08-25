using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.ScalarsGrids;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Utils;

namespace GeometricAlgebraFulcrumLib.Algebra.Outermorphisms.Stored
{
    public sealed class GaOmStored<T> :
        IGaOutermorphism<T>, IReadOnlyDictionary<ulong, IGaStorageKVector<T>>
    {
        private readonly Dictionary<ulong, IGaStorageKVector<T>> _mappedBasisBladesDictionary
            = new Dictionary<ulong, IGaStorageKVector<T>>();


        public IGaScalarsGridProcessor<T> ScalarsGridProcessor { get; }

        public IGaStorageKVector<T> MappedPseudoScalar
        {
            get
            {
                var id = (VSpaceDimension.ToGaSpaceDimension()) - 1;

                return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector)
                    ? mappedKVector
                    : ScalarsGridProcessor.CreateStorageZeroKVector(VSpaceDimension);
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

        public IGaStorageKVector<T> this[ulong id]
        {
            get
            {
                if (id >= GaSpaceDimension)
                    throw new IndexOutOfRangeException(nameof(id));

                return _mappedBasisBladesDictionary.TryGetValue(id, out var value) && !ReferenceEquals(value, null)
                    ? value
                    : ScalarsGridProcessor.CreateStorageZeroKVector(id.BasisBladeIdToGrade());
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
        
        public IGaStorageKVector<T> this[uint grade, ulong index]
        {
            get => this[index.BasisBladeIndexToId(grade)];
            set => this[index.BasisBladeIndexToId(grade)] = value;
        }

        public IEnumerable<ulong> Keys 
            => _mappedBasisBladesDictionary.Keys;

        public IEnumerable<IGaStorageKVector<T>> Values 
            => _mappedBasisBladesDictionary.Values;


        internal GaOmStored([NotNull] IGaScalarsGridProcessor<T> arrayProcessor, uint vSpaceDimension)
        {
            ScalarsGridProcessor = arrayProcessor;
            VSpaceDimension = vSpaceDimension;
        }


        public bool ContainsKey(ulong id)
        {
            return _mappedBasisBladesDictionary.ContainsKey(id);
        }

        public bool TryGetValue(ulong id, out IGaStorageKVector<T> kVector)
        {
            return _mappedBasisBladesDictionary.TryGetValue(id, out kVector);
        }

        public IReadOnlyList<IGaStorageVector<T>> GetMappedBasisVectors()
        {
            var mappedBasisVectorsList = 
                new List<IGaStorageVector<T>>((int) VSpaceDimension);

            for (var index = 0; index < VSpaceDimension; index++)
            {
                mappedBasisVectorsList.Add(
                    _mappedBasisBladesDictionary.TryGetValue(index.BasisVectorIndexToId(), out var mappedKVector)
                        ? mappedKVector.GetVectorPart()
                        : GaStorageVector<T>.ZeroVector
                    );
            }

            return mappedBasisVectorsList;
        }


        public IGaStorageVector<T> MapBasisVector(ulong index)
        {
            return _mappedBasisBladesDictionary.TryGetValue(index.BasisVectorIndexToId(), out var mappedKVector)
                ? mappedKVector.GetVectorPart()
                : GaStorageVector<T>.ZeroVector;
        }

        public IGaStorageBivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            if (index1 == index2)
                return GaStorageBivector<T>.ZeroBivector;

            var id = GaBasisBivectorUtils.BasisBivectorId(index1, index2);

            if (_mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector))
                return index1 < index2
                    ? mappedKVector.GetBivectorPart()
                    : ScalarsGridProcessor.GetNegativeBivectorPart(mappedKVector);

            return GaStorageBivector<T>.ZeroBivector;
        }

        public IGaStorageKVector<T> MapBasisBlade(ulong id)
        {
            return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector)
                ? mappedKVector
                : ScalarsGridProcessor.CreateStorageZeroKVector(id.BasisBladeIdToGrade());
        }

        public IGaStorageKVector<T> MapBasisBlade(uint grade, ulong index)
        {
            var id = index.BasisBladeIndexToId(grade);

            return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector)
                ? mappedKVector
                : ScalarsGridProcessor.CreateStorageZeroKVector(grade);
        }

        public IGaStorageVector<T> MapVector(IGaStorageVector<T> vector)
        {
            var storage = ScalarsGridProcessor.CreateStorageKVectorComposer(1);

            foreach (var (index, scalar) in vector.IndexScalarList.GetKeyValueRecords())
                if (_mappedBasisBladesDictionary.TryGetValue(index.BasisVectorIndexToId(), out var mappedKVector))
                    storage.AddScaledTerms(
                        scalar, 
                        mappedKVector.IndexScalarList.GetKeyValueRecords()
                    );

            storage.RemoveZeroTerms();

            return storage.CreateStorageVector();
        }

        public IGaStorageBivector<T> MapBivector(IGaStorageBivector<T> bivector)
        {
            var storage = ScalarsGridProcessor.CreateStorageKVectorComposer();

            foreach (var (index, scalar) in bivector.IndexScalarList.GetKeyValueRecords())
                if (_mappedBasisBladesDictionary.TryGetValue(index.BasisBivectorIndexToId(), out var mappedKVector))
                    storage.AddScaledTerms(
                        scalar, 
                        mappedKVector.IndexScalarList.GetKeyValueRecords()
                    );

            storage.RemoveZeroTerms();

            return storage.CreateStorageBivector();
        }

        public IGaStorageKVector<T> MapKVector(IGaStorageKVector<T> kVector)
        {
            var grade = kVector.Grade;
            var storage = ScalarsGridProcessor.CreateStorageKVectorComposer();

            foreach (var (index, scalar) in kVector.IndexScalarList.GetKeyValueRecords())
                if (_mappedBasisBladesDictionary.TryGetValue(index.BasisBladeIndexToId(grade), out var mappedKVector))
                    storage.AddScaledTerms(
                        scalar, 
                        mappedKVector.IndexScalarList.GetKeyValueRecords()
                    );

            storage.RemoveZeroTerms();

            return storage.CreateStorageKVector(grade);
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivector<T> mv)
        {
            var storage = ScalarsGridProcessor.CreateStorageSparseMultivectorComposer();

            foreach (var (id, scalar) in mv.GetIdScalarRecords())
                if (_mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector))
                    storage.AddScaledTerms(
                        scalar, 
                        mappedKVector.GetIdScalarRecords()
                    );

            storage.RemoveZeroTerms();

            return storage.CreateStorageSparseMultivector();
        }

        public IEnumerator<KeyValuePair<ulong, IGaStorageKVector<T>>> GetEnumerator()
        {
            return _mappedBasisBladesDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}