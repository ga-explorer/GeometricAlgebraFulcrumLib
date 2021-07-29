using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearMaps
{
    public sealed class GaUnilinearMapStored<T> :
        IGaGeneralUnilinearMap<T>, IReadOnlyDictionary<ulong, IGasMultivector<T>>
    {
        private readonly Dictionary<ulong, IGasMultivector<T>> _mappedBasisBladesDictionary
            = new Dictionary<ulong, IGasMultivector<T>>();


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

        public IGasMultivector<T> this[ulong id]
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

                if (_mappedBasisBladesDictionary.ContainsKey(id))
                    _mappedBasisBladesDictionary[id] = value;

                else
                    _mappedBasisBladesDictionary.Add(id, value);
            }
        }
        
        public IGasMultivector<T> this[uint grade, ulong index]
        {
            get => this[GaBasisUtils.BasisBladeId(grade, index)];
            set => this[GaBasisUtils.BasisBladeId(grade, index)] = value;
        }

        public IEnumerable<ulong> Keys 
            => _mappedBasisBladesDictionary.Keys;

        public IEnumerable<IGasMultivector<T>> Values 
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

        public bool TryGetValue(ulong id, out IGasMultivector<T> multivector)
        {
            return _mappedBasisBladesDictionary.TryGetValue(id, out multivector);
        }

        public IGasMultivector<T> MapBasisBlade(ulong id)
        {
            return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedMultivector)
                ? mappedMultivector
                : ScalarProcessor.CreateZeroScalar();
        }

        public IGasMultivector<T> MapBasisBlade(uint grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedMultivector)
                ? mappedMultivector
                : ScalarProcessor.CreateZeroScalar();
        }

        public IGasMultivector<T> MapMultivector(IGasMultivector<T> mv)
        {
            var storage = new GaMultivectorTermsStorageComposer<T>(ScalarProcessor);

            foreach (var (id, scalar) in mv.GetIdScalarPairs())
                if (_mappedBasisBladesDictionary.TryGetValue(id, out var mappedMultivector))
                    storage.AddLeftScaledTerms(
                        scalar, 
                        mappedMultivector.GetIdScalarPairs()
                    );

            storage.RemoveZeroTerms();

            return storage.GetCompactMultivector();
        }

        public IEnumerator<KeyValuePair<ulong, IGasMultivector<T>>> GetEnumerator()
        {
            return _mappedBasisBladesDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}