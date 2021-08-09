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

namespace GeometricAlgebraFulcrumLib.Algebra.LinearMaps
{
    public sealed class GaUnilinearMapStored<T> :
        IGaGeneralUnilinearMap<T>, IReadOnlyDictionary<ulong, IGaStorageMultivector<T>>
    {
        private readonly Dictionary<ulong, IGaStorageMultivector<T>> _mappedBasisBladesDictionary
            = new Dictionary<ulong, IGaStorageMultivector<T>>();


        public uint VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => 1UL << (int) VSpaceDimension;

        public ulong MaxBasisBladeId 
            => (1UL << (int) VSpaceDimension) - 1UL;

        public uint GradesCount 
            => VSpaceDimension + 1;

        public IEnumerable<uint> Grades 
            => GradesCount.GetRange();

        public IGaScalarProcessor<T> ScalarProcessor { get; }

        
        public int Count 
            => _mappedBasisBladesDictionary.Count;

        public IGaStorageMultivector<T> this[ulong id]
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

                if (_mappedBasisBladesDictionary.ContainsKey(id))
                    _mappedBasisBladesDictionary[id] = value;

                else
                    _mappedBasisBladesDictionary.Add(id, value);
            }
        }
        
        public IGaStorageMultivector<T> this[uint grade, ulong index]
        {
            get => this[GaBasisUtils.BasisBladeId(grade, index)];
            set => this[GaBasisUtils.BasisBladeId(grade, index)] = value;
        }

        public IEnumerable<ulong> Keys 
            => _mappedBasisBladesDictionary.Keys;

        public IEnumerable<IGaStorageMultivector<T>> Values 
            => _mappedBasisBladesDictionary.Values;


        internal GaUnilinearMapStored([NotNull] IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            ScalarProcessor = scalarProcessor;
            VSpaceDimension = vSpaceDimension;
        }


        public bool ContainsKey(ulong id)
        {
            return _mappedBasisBladesDictionary.ContainsKey(id);
        }

        public bool TryGetValue(ulong id, out IGaStorageMultivector<T> multivector)
        {
            return _mappedBasisBladesDictionary.TryGetValue(id, out multivector);
        }

        public IGaStorageMultivector<T> MapBasisBlade(ulong id)
        {
            return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedMultivector)
                ? mappedMultivector
                : ScalarProcessor.CreateStorageZeroScalar();
        }

        public IGaStorageMultivector<T> MapBasisBlade(uint grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedMultivector)
                ? mappedMultivector
                : ScalarProcessor.CreateStorageZeroScalar();
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivector<T> mv)
        {
            var storage = new GaStorageComposerMultivectorSparse<T>(ScalarProcessor);

            foreach (var (id, scalar) in mv.GetIdScalarPairs())
                if (_mappedBasisBladesDictionary.TryGetValue(id, out var mappedMultivector))
                    storage.AddLeftScaledTerms(
                        scalar, 
                        mappedMultivector.GetIdScalarPairs()
                    );

            storage.RemoveZeroTerms();

            return storage.GetMultivector();
        }

        public IEnumerator<KeyValuePair<ulong, IGaStorageMultivector<T>>> GetEnumerator()
        {
            return _mappedBasisBladesDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}