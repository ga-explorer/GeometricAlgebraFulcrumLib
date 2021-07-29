using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Algebra.Outermorphisms.Stored
{
    public sealed class GaOmStored<T> :
        IGaOutermorphism<T>, IReadOnlyDictionary<ulong, IGasKVector<T>>
    {
        private readonly Dictionary<ulong, IGasKVector<T>> _mappedBasisBladesDictionary
            = new Dictionary<ulong, IGasKVector<T>>();


        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public IGasKVector<T> MappedPseudoScalar
        {
            get
            {
                var id = (1UL << (int) VSpaceDimension) - 1;

                return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector)
                    ? mappedKVector
                    : ScalarProcessor.CreateZeroKVector(VSpaceDimension);
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

        public IGasKVector<T> this[ulong id]
        {
            get
            {
                if (id >= GaSpaceDimension)
                    throw new IndexOutOfRangeException(nameof(id));

                return _mappedBasisBladesDictionary.TryGetValue(id, out var value) && !ReferenceEquals(value, null)
                    ? value
                    : ScalarProcessor.CreateZeroKVector(id.BasisBladeGrade());
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
        
        public IGasKVector<T> this[uint grade, ulong index]
        {
            get => this[GaBasisUtils.BasisBladeId(grade, index)];
            set => this[GaBasisUtils.BasisBladeId(grade, index)] = value;
        }

        public IEnumerable<ulong> Keys 
            => _mappedBasisBladesDictionary.Keys;

        public IEnumerable<IGasKVector<T>> Values 
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

        public bool TryGetValue(ulong id, out IGasKVector<T> kVector)
        {
            return _mappedBasisBladesDictionary.TryGetValue(id, out kVector);
        }

        public IReadOnlyList<IGasVector<T>> GetMappedBasisVectors()
        {
            var mappedBasisVectorsList = 
                new List<IGasVector<T>>((int) VSpaceDimension);

            for (var index = 0; index < VSpaceDimension; index++)
            {
                mappedBasisVectorsList.Add(
                    _mappedBasisBladesDictionary.TryGetValue(1UL << index, out var mappedKVector)
                        ? mappedKVector.GetVectorPart()
                        : ScalarProcessor.CreateZeroVector()
                    );
            }

            return mappedBasisVectorsList;
        }


        public IGasVector<T> MapBasisVector(ulong index)
        {
            return _mappedBasisBladesDictionary.TryGetValue(1UL << (int) index, out var mappedKVector)
                ? mappedKVector.GetVectorPart()
                : ScalarProcessor.CreateZeroVector();
        }

        public IGasBivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            if (index1 == index2)
                return ScalarProcessor.CreateZeroBivector();

            var id = (1UL << (int) index1) | (1UL << (int) index2);

            if (_mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector))
                return index1 < index2
                    ? mappedKVector.GetBivectorPart()
                    : mappedKVector.GetNegativeBivectorPart();

            return ScalarProcessor.CreateZeroBivector();
        }

        public IGasKVector<T> MapBasisBlade(ulong id)
        {
            return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector)
                ? mappedKVector
                : ScalarProcessor.CreateZeroKVector(id.BasisBladeGrade());
        }

        public IGasKVector<T> MapBasisBlade(uint grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector)
                ? mappedKVector
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public IGasVector<T> MapVector(IGasVector<T> vector)
        {
            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, 1);

            foreach (var (index, scalar) in vector.GetIndexScalarPairs())
                if (_mappedBasisBladesDictionary.TryGetValue(1UL << (int) index, out var mappedKVector))
                    storage.AddLeftScaledTerms(
                        scalar, 
                        mappedKVector.GetIndexScalarPairs()
                    );

            storage.RemoveZeroTerms();

            return storage.GetVectorStorage();
        }

        public IGasBivector<T> MapBivector(IGasBivector<T> bivector)
        {
            var storage = new GaBivectorStorageComposer<T>(ScalarProcessor);

            foreach (var (index, scalar) in bivector.GetIndexScalarPairs())
                if (_mappedBasisBladesDictionary.TryGetValue(GaBasisUtils.BasisBladeId(2, index), out var mappedKVector))
                    storage.AddLeftScaledTerms(
                        scalar, 
                        mappedKVector.GetIndexScalarPairs()
                    );

            storage.RemoveZeroTerms();

            return storage.GetBivectorStorage();
        }

        public IGasKVector<T> MapKVector(IGasKVector<T> kVector)
        {
            var grade = kVector.Grade;
            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);

            foreach (var (index, scalar) in kVector.GetIndexScalarPairs())
                if (_mappedBasisBladesDictionary.TryGetValue(GaBasisUtils.BasisBladeId(grade, index), out var mappedKVector))
                    storage.AddLeftScaledTerms(
                        scalar, 
                        mappedKVector.GetIndexScalarPairs()
                    );

            storage.RemoveZeroTerms();

            return storage.GetKVectorStorage();
        }

        public IGasMultivector<T> MapMultivector(IGasMultivector<T> mv)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(ScalarProcessor);

            foreach (var (id, scalar) in mv.GetIdScalarPairs())
                if (_mappedBasisBladesDictionary.TryGetValue(id, out var mappedKVector))
                    storage.AddLeftScaledTerms(
                        scalar, 
                        mappedKVector.GetIdScalarPairs()
                    );

            storage.RemoveZeroTerms();

            return storage.GetCompactMultivector();
        }

        public IEnumerator<KeyValuePair<ulong, IGasKVector<T>>> GetEnumerator()
        {
            return _mappedBasisBladesDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}