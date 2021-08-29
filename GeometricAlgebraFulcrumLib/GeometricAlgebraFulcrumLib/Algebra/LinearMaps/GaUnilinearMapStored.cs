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

namespace GeometricAlgebraFulcrumLib.Algebra.LinearMaps
{
    public sealed class GaUnilinearMapStored<T> :
        IGaGeneralUnilinearMap<T>, IReadOnlyDictionary<ulong, IGaMultivectorStorage<T>>
    {
        private readonly Dictionary<ulong, IGaMultivectorStorage<T>> _mappedBasisBladesDictionary
            = new Dictionary<ulong, IGaMultivectorStorage<T>>();


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

        public ILaProcessor<T> ScalarsGridProcessor { get; }

        
        public int Count 
            => _mappedBasisBladesDictionary.Count;

        public IGaMultivectorStorage<T> this[ulong id]
        {
            get
            {
                if (id >= GaSpaceDimension)
                    throw new IndexOutOfRangeException(nameof(id));

                return _mappedBasisBladesDictionary.TryGetValue(id, out var value) && !ReferenceEquals(value, null)
                    ? value
                    : ScalarsGridProcessor.CreateKVectorStorage(id.BasisBladeIdToGrade());
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
        
        public IGaMultivectorStorage<T> this[uint grade, ulong index]
        {
            get => this[index.BasisBladeIndexToId(grade)];
            set => this[index.BasisBladeIndexToId(grade)] = value;
        }

        public IEnumerable<ulong> Keys 
            => _mappedBasisBladesDictionary.Keys;

        public IEnumerable<IGaMultivectorStorage<T>> Values 
            => _mappedBasisBladesDictionary.Values;


        internal GaUnilinearMapStored([NotNull] ILaProcessor<T> arrayProcessor, uint vSpaceDimension)
        {
            ScalarsGridProcessor = arrayProcessor;
            VSpaceDimension = vSpaceDimension;
        }


        public bool ContainsKey(ulong id)
        {
            return _mappedBasisBladesDictionary.ContainsKey(id);
        }

        public bool TryGetValue(ulong id, out IGaMultivectorStorage<T> multivector)
        {
            return _mappedBasisBladesDictionary.TryGetValue(id, out multivector);
        }

        public IGaMultivectorStorage<T> MapBasisBlade(ulong id)
        {
            return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedMultivector)
                ? mappedMultivector
                : ScalarsGridProcessor.CreateStorageZeroScalar();
        }

        public IGaMultivectorStorage<T> MapBasisBlade(uint grade, ulong index)
        {
            var id = index.BasisBladeIndexToId(grade);

            return _mappedBasisBladesDictionary.TryGetValue(id, out var mappedMultivector)
                ? mappedMultivector
                : ScalarsGridProcessor.CreateStorageZeroScalar();
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> mv)
        {
            var storage = 
                ScalarsGridProcessor.CreateStorageSparseMultivectorComposer();

            foreach (var (id, scalar) in mv.GetIdScalarRecords())
                if (_mappedBasisBladesDictionary.TryGetValue(id, out var mappedMultivector))
                    storage.AddScaledTerms(
                        scalar, 
                        mappedMultivector.GetIdScalarRecords()
                    );

            storage.RemoveZeroTerms();

            return storage.CreateGaMultivectorSparseStorage();
        }

        public IEnumerator<KeyValuePair<ulong, IGaMultivectorStorage<T>>> GetEnumerator()
        {
            return _mappedBasisBladesDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}