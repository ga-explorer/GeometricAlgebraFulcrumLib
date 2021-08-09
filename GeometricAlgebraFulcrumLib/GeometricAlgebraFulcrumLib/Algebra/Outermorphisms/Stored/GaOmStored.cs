using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Utils;

namespace GeometricAlgebraFulcrumLib.Algebra.Outermorphisms.Stored
{
    public sealed class GaOmStored<T> :
        IGaOutermorphism<T>, IReadOnlyDictionary<ulong, IGaStorageKVector<T>>
    {
        private readonly Dictionary<ulong, IGaStorageKVector<T>> _mappedBasisBladesDictionary
            = new Dictionary<ulong, IGaStorageKVector<T>>();


        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public IGaStorageKVector<T> MappedPseudoScalar
        {
            get
            {
                var id = (1UL << (int) VSpaceDimension) - 1;

                return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector)
                    ? mappedKVector
                    : ScalarProcessor.CreateStorageZeroKVector(VSpaceDimension);
            }
        }

        public uint VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => 1UL << (int) VSpaceDimension;

        public ulong MaxBasisBladeId 
            => (1UL << (int) VSpaceDimension) - 1UL;

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
                    : ScalarProcessor.CreateStorageZeroKVector(id.BasisBladeGrade());
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

                if (value.Grade != id.BasisBladeGrade())
                    throw new InvalidOperationException();

                if (_mappedBasisBladesDictionary.ContainsKey(id))
                    _mappedBasisBladesDictionary[id] = value;

                else
                    _mappedBasisBladesDictionary.Add(id, value);
            }
        }
        
        public IGaStorageKVector<T> this[uint grade, ulong index]
        {
            get => this[GaBasisUtils.BasisBladeId(grade, index)];
            set => this[GaBasisUtils.BasisBladeId(grade, index)] = value;
        }

        public IEnumerable<ulong> Keys 
            => _mappedBasisBladesDictionary.Keys;

        public IEnumerable<IGaStorageKVector<T>> Values 
            => _mappedBasisBladesDictionary.Values;


        internal GaOmStored([NotNull] IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            ScalarProcessor = scalarProcessor;
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
                    _mappedBasisBladesDictionary.TryGetValue(1UL << index, out var mappedKVector)
                        ? mappedKVector.GetVectorPart()
                        : GaStorageVector<T>.ZeroVector
                    );
            }

            return mappedBasisVectorsList;
        }


        public IGaStorageVector<T> MapBasisVector(ulong index)
        {
            return _mappedBasisBladesDictionary.TryGetValue(1UL << (int) index, out var mappedKVector)
                ? mappedKVector.GetVectorPart()
                : GaStorageVector<T>.ZeroVector;
        }

        public IGaStorageBivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            if (index1 == index2)
                return GaStorageBivector<T>.ZeroBivector;

            var id = (1UL << (int) index1) | (1UL << (int) index2);

            if (_mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector))
                return index1 < index2
                    ? mappedKVector.GetBivectorPart()
                    : ScalarProcessor.GetNegativeBivectorPart(mappedKVector);

            return GaStorageBivector<T>.ZeroBivector;
        }

        public IGaStorageKVector<T> MapBasisBlade(ulong id)
        {
            return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector)
                ? mappedKVector
                : ScalarProcessor.CreateStorageZeroKVector(id.BasisBladeGrade());
        }

        public IGaStorageKVector<T> MapBasisBlade(uint grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector)
                ? mappedKVector
                : ScalarProcessor.CreateStorageZeroKVector(grade);
        }

        public IGaStorageVector<T> MapVector(IGaStorageVector<T> vector)
        {
            var storage = new GaStorageComposerKVector<T>(ScalarProcessor, 1);

            foreach (var (index, scalar) in vector.IndexScalarDictionary)
                if (_mappedBasisBladesDictionary.TryGetValue(1UL << (int) index, out var mappedKVector))
                    storage.AddLeftScaledTerms(
                        scalar, 
                        mappedKVector.IndexScalarDictionary
                    );

            storage.RemoveZeroTerms();

            return storage.GetVector();
        }

        public IGaStorageBivector<T> MapBivector(IGaStorageBivector<T> bivector)
        {
            var storage = new GaStorageComposerBivector<T>(ScalarProcessor);

            foreach (var (index, scalar) in bivector.IndexScalarDictionary)
                if (_mappedBasisBladesDictionary.TryGetValue(GaBasisUtils.BasisBladeId(2, index), out var mappedKVector))
                    storage.AddLeftScaledTerms(
                        scalar, 
                        mappedKVector.IndexScalarDictionary
                    );

            storage.RemoveZeroTerms();

            return storage.GetBivector();
        }

        public IGaStorageKVector<T> MapKVector(IGaStorageKVector<T> kVector)
        {
            var grade = kVector.Grade;
            var storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);

            foreach (var (index, scalar) in kVector.IndexScalarDictionary)
                if (_mappedBasisBladesDictionary.TryGetValue(GaBasisUtils.BasisBladeId(grade, index), out var mappedKVector))
                    storage.AddLeftScaledTerms(
                        scalar, 
                        mappedKVector.IndexScalarDictionary
                    );

            storage.RemoveZeroTerms();

            return storage.GetKVector();
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivector<T> mv)
        {
            var storage = new GaStorageComposerMultivectorSparse<T>(ScalarProcessor);

            foreach (var (id, scalar) in mv.GetIdScalarPairs())
                if (_mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector))
                    storage.AddLeftScaledTerms(
                        scalar, 
                        mappedKVector.GetIdScalarPairs()
                    );

            storage.RemoveZeroTerms();

            return storage.GetMultivector();
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