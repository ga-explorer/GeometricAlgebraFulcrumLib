using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Terms;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public sealed class GaMultivectorGradedStorageComposer<T> 
        : GaMultivectorStorageComposerBase<T>
    {
        public Dictionary<uint, GaKVectorStorageComposer<T>> GradeKVectorStorageComposerDictionary { get; }
            = new();


        public override T this[ulong id]
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                return GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var kVectorStorage) 
                    ? kVectorStorage[index] 
                    : ScalarProcessor.ZeroScalar;
            }
            set
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                var composer = GetOrCreateKVectorStorageComposer(grade);

                composer[index] = value;
            }
        }

        public override T this[uint grade, ulong index]
        {
            get => GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var kVectorStorage) 
                ? kVectorStorage[index] 
                : ScalarProcessor.ZeroScalar;
            set
            {
                var composer = GetOrCreateKVectorStorageComposer(grade);

                composer[index] = value;
            }
        }


        internal GaMultivectorGradedStorageComposer(IGaScalarProcessor<T> scalarProcessor)
            : base(scalarProcessor)
        {
        }


        private GaKVectorStorageComposer<T> GetOrCreateKVectorStorageComposer(uint grade)
        {
            if (GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
                return storage;

            storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);

            GradeKVectorStorageComposerDictionary.Add(grade, storage);

            return storage;
        }


        public override bool IsEmpty()
        {
            return GradeKVectorStorageComposerDictionary.Count == 0 || 
                   GradeKVectorStorageComposerDictionary.Values.All(d => d.IsEmpty());
        }

        public override GaMultivectorStorageComposerBase<T> Clear()
        {
            foreach (var storage in GradeKVectorStorageComposerDictionary.Values)
                storage.Clear();

            return this;
        }


        public GaMultivectorGradedStorageComposer<T> SetKVectorComposer(GaKVectorStorageComposer<T> composer)
        {
            var grade = composer.Grade;

            if (GradeKVectorStorageComposerDictionary.ContainsKey(grade))
                GradeKVectorStorageComposerDictionary[grade] = composer;
            else
                GradeKVectorStorageComposerDictionary.Add(grade, composer);

            return this;
        }


        public GaMultivectorGradedStorageComposer<T> AddKVector(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.AddTerms(indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<T> AddKVector(uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.AddTerms(indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<T> AddKVector(IGasKVector<T> storage)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(storage.Grade);

            kVectorStorage.AddTerms(storage.GetIndexScalarPairs());

            return this;
        }

        public GaMultivectorGradedStorageComposer<T> AddKVectors(IEnumerable<KeyValuePair<uint, Dictionary<ulong, T>>> storagesList)
        {
            foreach (var (grade, storage) in storagesList)
                AddKVector(grade, storage);

            return this;
        }

        public GaMultivectorGradedStorageComposer<T> AddKVectors(IEnumerable<IGasKVector<T>> storagesList)
        {
            foreach (var storage in storagesList)
                AddKVector(storage);

            return this;
        }

        public GaMultivectorGradedStorageComposer<T> AddLeftScaledKVector(T scalingFactor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.AddLeftScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<T> AddLeftScaledKVector(T scalingFactor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.AddLeftScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<T> AddLeftScaledKVector(T scalingFactor, GasKVectorBase<T> storage)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(storage.Grade);

            kVectorStorage.AddLeftScaledTerms(scalingFactor, storage.GetIndexScalarPairs());

            return this;
        }

        public GaMultivectorGradedStorageComposer<T> AddRightScaledKVector(T scalingFactor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.AddRightScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<T> AddRightScaledKVector(T scalingFactor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.AddRightScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<T> AddRightScaledKVector(T scalingFactor, GasKVectorBase<T> storage)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(storage.Grade);

            kVectorStorage.AddRightScaledTerms(scalingFactor, storage.GetIndexScalarPairs());

            return this;
        }


        public GaMultivectorGradedStorageComposer<T> SubtractKVector(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.SubtractTerms(indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<T> SubtractKVector(uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.SubtractTerms(indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<T> SubtractKVector(GasKVectorBase<T> storage)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(storage.Grade);

            kVectorStorage.SubtractTerms(storage.GetIndexScalarPairs());

            return this;
        }

        public GaMultivectorGradedStorageComposer<T> SubtractLeftScaledKVector(T scalingFactor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.SubtractLeftScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<T> SubtractLeftScaledKVector(T scalingFactor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.SubtractLeftScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<T> SubtractLeftScaledKVector(T scalingFactor, GasKVectorBase<T> storage)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(storage.Grade);

            kVectorStorage.SubtractLeftScaledTerms(scalingFactor, storage.GetIndexScalarPairs());

            return this;
        }

        public GaMultivectorGradedStorageComposer<T> SubtractRightScaledKVector(T scalingFactor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.SubtractRightScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<T> SubtractRightScaledKVector(T scalingFactor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.SubtractRightScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<T> SubtractRightScaledKVector(T scalingFactor, GasKVectorBase<T> storage)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(storage.Grade);

            kVectorStorage.SubtractRightScaledTerms(scalingFactor, storage.GetIndexScalarPairs());

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> SetTerm(GaTerm<T> term)
        {
            term.BasisBlade.GetGradeIndex(out var grade, out var index);

            this[grade, index] = term.Scalar;

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> SetTerms(IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                term.BasisBlade.GetGradeIndex(out var grade, out var index);

                this[grade, index] = term.Scalar;
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> SetTermsToNegative()
        {
            foreach (var storage in GradeKVectorStorageComposerDictionary.Values)
                storage.SetTermsToNegative();

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SetTermsToNegative(IEnumerable<ulong> idsList)
        {
            foreach (var id in idsList)
            {
                var grade = id.BasisBladeGrade();

                if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage)) 
                    continue;

                var index = id.BasisBladeIndex();

                if (storage.IndexScalarsDictionary.TryGetValue(index, out var scalar))
                    storage[index] = ScalarProcessor.Negative(scalar);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SetTermsToNegative(
            IEnumerable<Tuple<uint, ulong>> indicesList)
        {
            foreach (var (grade, index) in indicesList)
            {
                if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage)) 
                    continue;

                if (storage.IndexScalarsDictionary.TryGetValue(index, out var scalar))
                    storage[index] = ScalarProcessor.Negative(scalar);
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<ulong, T> mappingFunc)
        {
            foreach (var id in idsList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                this[grade, index] = mappingFunc(id);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList, Func<ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in idsList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                this[grade, index] = mappingFunc(id);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var id in idsList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                this[grade, index] = mappingFunc(grade, index);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SetComputedTerms(
            IEnumerable<Tuple<uint, ulong>> idsList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in idsList) 
                this[grade, index] = mappingFunc(grade, index);

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var id in idsList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                this[grade, index] = mappingFunc(id, scalar);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SetComputedTerms(
            IEnumerable<Tuple<uint, ulong>> idsList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var (grade, index) in idsList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var scalar = this[grade, index];

                this[grade, index] = mappingFunc(id, scalar);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SetComputedTerms(IEnumerable<ulong> idsList,
            Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var id in idsList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[id];

                this[grade, index] = mappingFunc(grade, index, scalar);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SetComputedTerms(
            IEnumerable<Tuple<uint, ulong>> idsList, Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var (grade, index) in idsList)
            {
                var scalar = this[grade, index];

                this[grade, index] = mappingFunc(grade, index, scalar);
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> LeftScaleTerms(T scalingFactor)
        {
            foreach (var storage in GradeKVectorStorageComposerDictionary.Values)
                storage.LeftScaleTerms(scalingFactor);

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> LeftScaleTerms(IEnumerable<ulong> indexList, T scalingFactor)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                this[grade, index] = ScalarProcessor.Times(scalingFactor, scalar);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> LeftScaleTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, T scalingFactor)
        {
            foreach (var (grade, index) in indexList)
            {
                var scalar = this[grade, index];

                this[grade, index] = ScalarProcessor.Times(scalingFactor, scalar);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> RightScaleTerms(T scalingFactor)
        {
            foreach (var storage in GradeKVectorStorageComposerDictionary.Values)
                storage.RightScaleTerms(scalingFactor);

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> RightScaleTerms(IEnumerable<ulong> indexList, T scalingFactor)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                this[grade, index] = ScalarProcessor.Times(scalar, scalingFactor);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> RightScaleTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, T scalingFactor)
        {
            foreach (var (grade, index) in indexList)
            {
                var scalar = this[grade, index];

                this[grade, index] = ScalarProcessor.Times(scalar, scalingFactor);
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                var (grade, index) = term.BasisBlade.GetGradeIndex();

                this[grade, index] = ScalarProcessor.Times(scalingFactor, term.Scalar);
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> SetRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                var (grade, index) = term.BasisBlade.GetGradeIndex();

                this[grade, index] = ScalarProcessor.Times(term.Scalar, scalingFactor);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> AddTerm(ulong id, T scalar)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage.AddTerm(index, scalar);

                return this;
            }

            this[grade, index] = scalar;

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> AddTerm(uint grade, ulong index, T scalar)
        {
            if (GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage.AddTerm(index, scalar);

                return this;
            }

            this[grade, index] = scalar;

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> AddTerm(GaTerm<T> term)
        {
            term.BasisBlade.GetGradeIndex(out var grade, out var index);

            if (GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage.AddTerm(index, term.Scalar);

                return this;
            }

            this[grade, index] = term.Scalar;

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                AddTerm(grade, index, mappingFunc(id));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> AddComputedTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                AddTerm(grade, index, mappingFunc(id));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> AddComputedTerms(IEnumerable<ulong> indexList,
            Func<uint, ulong, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                AddTerm(grade, index, mappingFunc(grade, index));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> AddComputedTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
                AddTerm(grade, index, mappingFunc(grade, index));

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                AddTerm(grade, index, mappingFunc(id, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> AddComputedTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var scalar = this[grade, index];

                AddTerm(grade, index, mappingFunc(id, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> AddComputedTerms(IEnumerable<ulong> indexList,
            Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                AddTerm(grade, index, mappingFunc(grade, index, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> AddComputedTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var scalar = this[grade, index];

                AddTerm(grade, index, mappingFunc(grade, index, scalar));
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                term.BasisBlade.GetGradeIndex(out var grade, out var index);

                AddTerm(grade, index, ScalarProcessor.Times(scalingFactor, term.Scalar));
            }

            return this;
        }
        

        public override GaMultivectorStorageComposerBase<T> AddRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                term.BasisBlade.GetGradeIndex(out var grade, out var index);

                AddTerm(grade, index, ScalarProcessor.Times(term.Scalar, scalingFactor));
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> SubtractTerm(ulong id, T scalar)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage.SubtractTerm(index, scalar);

                return this;
            }

            this[grade, index] = ScalarProcessor.Negative(scalar);

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SubtractTerm(uint grade, ulong index, T scalar)
        {
            if (GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage.SubtractTerm(index, scalar);

                return this;
            }

            this[grade, index] = ScalarProcessor.Negative(scalar);

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SubtractTerm(GaTerm<T> term)
        {
            term.BasisBlade.GetGradeIndex(out var grade, out var index);

            if (GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage.SubtractTerm(index, term.Scalar);

                return this;
            }

            this[grade, index] = ScalarProcessor.Negative(term.Scalar);

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                SubtractTerm(grade, index, mappingFunc(id));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SubtractComputedTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                SubtractTerm(grade, index, mappingFunc(id));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SubtractComputedTerms(IEnumerable<ulong> indexList,
            Func<uint, ulong, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                SubtractTerm(grade, index, mappingFunc(grade, index));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SubtractComputedTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
                SubtractTerm(grade, index, mappingFunc(grade, index));

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                SubtractTerm(grade, index, mappingFunc(id, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SubtractComputedTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var scalar = this[grade, index];

                SubtractTerm(grade, index, mappingFunc(id, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SubtractComputedTerms(IEnumerable<ulong> indexList,
            Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                SubtractTerm(grade, index, mappingFunc(grade, index, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SubtractComputedTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var scalar = this[grade, index];

                SubtractTerm(grade, index, mappingFunc(grade, index, scalar));
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                term.BasisBlade.GetGradeIndex(out var grade, out var index);

                SubtractTerm(grade, index, ScalarProcessor.Times(scalingFactor, term.Scalar));
            }

            return this;
        }
        

        public override GaMultivectorStorageComposerBase<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                term.BasisBlade.GetGradeIndex(out var grade, out var index);

                SubtractTerm(grade, index, ScalarProcessor.Times(term.Scalar, scalingFactor));
            }

            return this;
        }


        public override bool RemoveTerm(ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
                return false;

            var flag = storage.RemoveTerm(index);

            if (storage.Count == 0)
                GradeKVectorStorageComposerDictionary.Remove(grade);

            return flag;
        }

        public override bool RemoveTerm(uint grade, ulong index)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
                return false;

            var flag = storage.RemoveTerm(index);

            if (storage.Count == 0)
                GradeKVectorStorageComposerDictionary.Remove(grade);

            return flag;
        }

        public override GaMultivectorStorageComposerBase<T> RemoveTerms(params ulong[] indexList)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                if (GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
                    storage.RemoveTerm(index);
            }

            var gradesArray = 
                GradeKVectorStorageComposerDictionary
                    .Where(pair => pair.Value.Count == 0)
                    .Select(pair => pair.Key)
                    .ToArray();

            foreach (var grade in gradesArray)
                GradeKVectorStorageComposerDictionary.Remove(grade);

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> RemoveZeroTerms()
        {
            foreach (var kVectorsStorage in GradeKVectorStorageComposerDictionary.Values) 
                kVectorsStorage.RemoveZeroTerms();

            var keysArray = 
                GradeKVectorStorageComposerDictionary
                    .Where(p => p.Value.Count == 0)
                    .Select(p => p.Key)
                    .ToArray();

            foreach (var key in keysArray)
                GradeKVectorStorageComposerDictionary.Remove(key);

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> RemoveNearZeroTerms()
        {
            foreach (var kVectorsStorage in GradeKVectorStorageComposerDictionary.Values) 
                kVectorsStorage.RemoveNearZeroTerms();

            var keysArray = 
                GradeKVectorStorageComposerDictionary
                    .Where(p => p.Value.Count == 0)
                    .Select(p => p.Key)
                    .ToArray();

            foreach (var key in keysArray)
                GradeKVectorStorageComposerDictionary.Remove(key);

            return this;
        }

        public GaMultivectorGradedStorageComposer<T> RemoveKVector(uint grade)
        {
            GradeKVectorStorageComposerDictionary.Remove(grade);

            return this;
        }


        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        public void SetKVector(uint grade, IReadOnlyList<T> scalarValuesList)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);

                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            for (var index = 0; index < scalarValuesList.Count; index++)
                storage.SetTerm(
                    (ulong)index, 
                    scalarValuesList[index]
                );

            if (storage.IsEmpty())
                GradeKVectorStorageComposerDictionary.Remove(grade);
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        public void SetKVector(uint grade, T scalingFactor, IReadOnlyList<T> scalarValuesList)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);

                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            for (var index = 0; index < scalarValuesList.Count; index++)
                storage.SetTerm(
                    (ulong)index, 
                    ScalarProcessor.Times(scalingFactor, scalarValuesList[index])
                );

            if (storage.IsEmpty())
                GradeKVectorStorageComposerDictionary.Remove(grade);
        }

        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexScalarPairs"></param>
        /// <returns></returns>
        public void SetKVector(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);

                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.SetTerms(indexScalarPairs);

            if (storage.IsEmpty())
                GradeKVectorStorageComposerDictionary.Remove(grade);
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="indexScalarPairs"></param>
        /// <returns></returns>
        public void SetKVector(uint grade, T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);

                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.SetLeftScaledTerms(scalingFactor, indexScalarPairs);

            if (storage.IsEmpty())
                GradeKVectorStorageComposerDictionary.Remove(grade);
        }

        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="kVector"></param>
        /// <returns></returns>
        public void SetKVector(IGasKVector<T> kVector)
        {
            var grade = kVector.Grade;

            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);

                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.SetTerms(kVector.GetIndexScalarPairs());

            if (storage.IsEmpty())
                GradeKVectorStorageComposerDictionary.Remove(grade);
        }

        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexScalarsDictionary"></param>
        /// <returns></returns>
        public void SetKVectorStorage(uint grade, Dictionary<ulong, T> indexScalarsDictionary)
        {
            if (GradeKVectorStorageComposerDictionary.ContainsKey(grade))
                GradeKVectorStorageComposerDictionary[grade].SetStorage(indexScalarsDictionary);
            else
            {
                var composer = new GaKVectorStorageComposer<T>(ScalarProcessor, grade, indexScalarsDictionary);

                GradeKVectorStorageComposerDictionary.Add(grade, composer);
            }
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="scalingFactor"></param>
        /// <param name="kVector"></param>
        /// <returns></returns>
        public void SetKVector(T scalingFactor, IGasKVector<T> kVector)
        {
            var grade = kVector.Grade;

            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);

                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.SetLeftScaledTerms(
                scalingFactor, 
                kVector.GetIndexScalarPairs()
            );

            if (storage.IsEmpty())
                GradeKVectorStorageComposerDictionary.Remove(grade);
        }


        public GaMultivectorGradedStorageComposer<T> SetKVectors(IEnumerable<KeyValuePair<uint, Dictionary<ulong, T>>> storagesList)
        {
            foreach (var (grade, storage) in storagesList)
                SetKVector(grade, storage);

            return this;
        }

        /// <summary>
        /// Set some terms using the given k-vectors data
        /// </summary>
        /// <param name="kVectorsList"></param>
        /// <returns></returns>
        public void SetKVectors(IEnumerable<IGasKVector<T>> kVectorsList)
        {
            foreach (var storage in kVectorsList)
                SetKVector(storage);
        }

        /// <summary>
        /// Set some terms using the given k-vectors data scaled by a scaling factor
        /// </summary>
        /// <param name="scaledKVectorsList"></param>
        /// <returns></returns>
        public void SetKVectors(IEnumerable<Tuple<T, IGasKVector<T>>> scaledKVectorsList)
        {
            foreach (var (scalingFactor, kVector) in scaledKVectorsList)
                SetKVector(scalingFactor, kVector);
        }



        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        public void AddKVector(uint grade, IReadOnlyList<T> scalarValuesList)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);
                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.AddTerms(scalarValuesList);
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        public void AddKVector(uint grade, T scalingFactor, IReadOnlyList<T> scalarValuesList)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);
                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.AddLeftScaledTerms(scalingFactor, scalarValuesList);
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="indexScalarPairs"></param>
        /// <returns></returns>
        public void AddLeftScaledKVector(uint grade, T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);
                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.AddLeftScaledTerms(scalingFactor, indexScalarPairs);
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="scalingFactor"></param>
        /// <param name="kVector"></param>
        /// <returns></returns>
        public void AddKVector(T scalingFactor, IGasKVector<T> kVector)
        {
            var grade = kVector.Grade;

            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);
                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.AddLeftScaledTerms(
                scalingFactor, 
                kVector.GetIndexScalarPairs()
            );
        }

        /// <summary>
        /// Set some terms using the given k-vectors data scaled by a scaling factor
        /// </summary>
        /// <param name="scaledKVectorsList"></param>
        /// <returns></returns>
        public void AddLeftScaledKVectors(IEnumerable<Tuple<T, IGasKVector<T>>> scaledKVectorsList)
        {
            foreach (var (scalingFactor, storage) in scaledKVectorsList)
                AddLeftScaledKVector(storage.Grade, scalingFactor, storage.GetIndexScalarPairs());
        }


        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        public void SubtractKVector(uint grade, IReadOnlyList<T> scalarValuesList)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);
                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.SubtractTerms(scalarValuesList);
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        public void SubtractKVector(uint grade, T scalingFactor, IReadOnlyList<T> scalarValuesList)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);
                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.SubtractLeftScaledTerms(scalingFactor, scalarValuesList);
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="indexScalarPairs"></param>
        /// <returns></returns>
        public void SubtractLeftScaledKVector(uint grade, T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);
                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.SubtractLeftScaledTerms(scalingFactor, indexScalarPairs);
        }

        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="kVector"></param>
        /// <returns></returns>
        public void SubtractKVector(IGasKVector<T> kVector)
        {
            var grade = kVector.Grade;

            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);
                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.SubtractTerms(kVector.GetIndexScalarPairs());
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="scalingFactor"></param>
        /// <param name="kVector"></param>
        /// <returns></returns>
        public void SubtractKVector(T scalingFactor, IGasKVector<T> kVector)
        {
            var grade = kVector.Grade;

            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);
                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.SubtractLeftScaledTerms(scalingFactor, kVector.GetIndexScalarPairs());
        }


        public GaMultivectorGradedStorageComposer<T> SubtractKVectors(IEnumerable<KeyValuePair<uint, Dictionary<ulong, T>>> storagesList)
        {
            foreach (var (grade, storage) in storagesList)
                SubtractKVector(grade, storage);

            return this;
        }

        /// <summary>
        /// Set some terms using the given k-vectors data
        /// </summary>
        /// <param name="kVectorsList"></param>
        /// <returns></returns>
        public void SubtractKVectors(IEnumerable<IGasKVector<T>> kVectorsList)
        {
            foreach (var kVector in kVectorsList)
                SubtractKVector(kVector);
        }

        /// <summary>
        /// Set some terms using the given k-vectors data scaled by a scaling factor
        /// </summary>
        /// <param name="scaledKVectorsList"></param>
        /// <returns></returns>
        public void SubtractLeftScaledKVectors(IEnumerable<Tuple<T, IGasKVector<T>>> scaledKVectorsList)
        {
            foreach (var (scalingFactor, storage) in scaledKVectorsList)
                SubtractLeftScaledKVector(storage.Grade, scalingFactor, storage.GetIndexScalarPairs());
        }


        /// <summary>
        /// Remove the given terms if possible, else set to zero
        /// </summary>
        /// <param name="idsList"></param>
        public void RemoveTerms(IEnumerable<ulong> idsList)
        {
            foreach (var id in idsList)
                RemoveTerm(id);
        }

        /// <summary>
        /// Remove the given terms if possible, else set to zero
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexList"></param>
        public void RemoveTerms(uint grade, IEnumerable<ulong> indexList)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var composer))
                return;

            composer.RemoveTerms(indexList);
        }
        
        /// <summary>
        /// Remove the given terms if possible, else set to zero
        /// </summary>
        /// <param name="grade"></param>
        public void RemoveTermsOfGrade(uint grade)
        {
            if (GradeKVectorStorageComposerDictionary.ContainsKey(grade))
                GradeKVectorStorageComposerDictionary.Remove(grade);
        }


        /// <summary>
        /// Remove the given term if zero
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nearZeroFlag"></param>
        /// <returns></returns>
        public void RemoveTermIfZero(ulong id, bool nearZeroFlag = false)
        {
            var (grade, index) = id.BasisBladeGradeIndex();

            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var composer))
                return;

            composer.RemoveTermIfZero(index, nearZeroFlag);
        }

        /// <summary>
        /// Remove the given term if zero
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <param name="nearZeroFlag"></param>
        /// <returns></returns>
        public void RemoveTermIfZero(uint grade, ulong index, bool nearZeroFlag = false)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var composer))
                return;

            composer.RemoveTermIfZero(index, nearZeroFlag);
        }
        
        /// <summary>
        /// Remove all terms from storage where their scalar values is near zero
        /// </summary>
        public void RemoveZeroTermsOfGrade(uint grade, bool nearZeroFlag = false)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var composer))
                return;

            composer.RemoveZeroTerms(nearZeroFlag);
        }


        public override IGasMultivector<T> GetCompactMultivector()
        {
            var gradesCount = 
                GradeKVectorStorageComposerDictionary.Count;

            if (gradesCount == 0)
                return ScalarProcessor.CreateZeroScalar();

            if (gradesCount == 1)
                GradeKVectorStorageComposerDictionary.First().Value.GetCompactMultivector();

            return CreateMultivectorGradedStorage();
        }

        public override IGasTermsMultivector<T> GetCompactTermsStorage()
        {
            var idScalarDictionary = new Dictionary<ulong, T>();

            foreach (var (grade, composer) in GradeKVectorStorageComposerDictionary)
            {
                var indexScalarDictionary = composer.IndexScalarsDictionary;

                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    var id = GaBasisUtils.BasisBladeId(grade, index);

                    idScalarDictionary.Add(id, scalar);
                }
            }

            var termsCount = 
                idScalarDictionary.Count;

            if (termsCount == 0)
                return ScalarProcessor.CreateZeroScalar();

            if (termsCount > 1) 
                return ScalarProcessor.CreateTermsMultivector(idScalarDictionary);

            var (termId, termScalar) = 
                idScalarDictionary.First();

            return termId == 0UL
                ? ScalarProcessor.CreateScalar(termScalar)
                : ScalarProcessor.CreateKVector(termId, termScalar);
        }

        public override IGasGradedMultivector<T> GetCompactGradedMultivector()
        {
            var gradesCount = GradeKVectorStorageComposerDictionary.Count;

            if (gradesCount == 0)
                return ScalarProcessor.CreateZeroScalar();

            if (gradesCount == 1)
                GradeKVectorStorageComposerDictionary.First().Value.GetCompactMultivector();

            return CreateMultivectorGradedStorage();
        }

        public override IGasMultivector<T> GetMultivectorStorage()
        {
            return CreateMultivectorGradedStorage();
        }
        
        public IGasGradedMultivector<T> CreateMultivectorGradedStorage()
        {
            var kVectorsDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, composer) in GradeKVectorStorageComposerDictionary)
                kVectorsDictionary.Add(grade, composer.IndexScalarsDictionary);

            return ScalarProcessor.CreateGradedMultivector(kVectorsDictionary);
        }

        public IGasVector<T> CreateVectorStorage()
        {
            var indexScalarsDictionary =
                GradeKVectorStorageComposerDictionary.TryGetValue(1, out var composer)
                    ? composer.IndexScalarsDictionary
                    : new Dictionary<ulong, T>();

            return ScalarProcessor.CreateVector(indexScalarsDictionary);
        }

        public IGasBivector<T> CreateBivectorStorage()
        {
            var indexScalarsDictionary =
                GradeKVectorStorageComposerDictionary.TryGetValue(1, out var composer)
                    ? composer.IndexScalarsDictionary
                    : new Dictionary<ulong, T>();

            return ScalarProcessor.CreateBivector(indexScalarsDictionary);
        }

        public IGasKVector<T> CreateKVectorStorage(uint grade)
        {
            var indexScalarsDictionary =
                GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var composer)
                    ? composer.IndexScalarsDictionary
                    : new Dictionary<ulong, T>();

            return ScalarProcessor.CreateKVector(grade, indexScalarsDictionary);
        }

        
        public override IGasMultivector<T> GetMultivectorCopy()
        {
            return GetGradedMultivectorCopy();
        }

        public override IGasMultivector<T> GetMultivectorCopy(Func<T, T> scalarMapping)
        {
            var kVectorsDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, composer) in GradeKVectorStorageComposerDictionary)
            {
                var indexScalarDictionary = composer.IndexScalarsDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => scalarMapping(pair.Value)
                );

                kVectorsDictionary.Add(grade, indexScalarDictionary);
            }

            return ScalarProcessor.CreateGradedMultivector(kVectorsDictionary
            );
        }

        public override IGasGradedMultivector<T> GetGradedMultivectorCopy()
        {
            var kVectorsDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, composer) in GradeKVectorStorageComposerDictionary)
            {
                var indexScalarDictionary = composer.IndexScalarsDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                );

                kVectorsDictionary.Add(grade, indexScalarDictionary);
            }

            return ScalarProcessor.CreateGradedMultivector(kVectorsDictionary
            );
        }

        public override IGasTermsMultivector<T> GetTermsMultivectorCopy()
        {
            var idScalarDictionary = new Dictionary<ulong, T>();

            foreach (var (grade, composer) in GradeKVectorStorageComposerDictionary)
            {
                var indexScalarDictionary = composer.IndexScalarsDictionary;

                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    var id = GaBasisUtils.BasisBladeId(grade, index);

                    idScalarDictionary.Add(id, scalar);
                }
            }

            return ScalarProcessor.CreateTermsMultivector(idScalarDictionary);
        }

        public override GasTreeMultivector<T> GetTreeMultivectorCopy()
        {
            var idScalarDictionary = new Dictionary<ulong, T>();

            foreach (var (grade, composer) in GradeKVectorStorageComposerDictionary)
            {
                var indexScalarDictionary = composer.IndexScalarsDictionary;

                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    var id = GaBasisUtils.BasisBladeId(grade, index);

                    idScalarDictionary.Add(id, scalar);
                }
            }

            return GaStorageFactory.CreateTreeMultivector(
                ScalarProcessor, 
                idScalarDictionary
            );
        }

        public override IGasScalar<T> GetScalarStorage()
        {
            return ScalarProcessor.CreateScalar(this[0, 0]);
        }

        public override IGasVector<T> GetVectorStorageCopy()
        {
            var indexScalarsDictionary =
                GradeKVectorStorageComposerDictionary.TryGetValue(1, out var composer)
                    ? composer.IndexScalarsDictionary.ToDictionary(
                        pair => pair.Key, 
                        pair => pair.Value
                    )
                    : new Dictionary<ulong, T>();

            return ScalarProcessor.CreateVector(indexScalarsDictionary);
        }

        public override IGasBivector<T> GetBivectorStorageCopy(uint grade)
        {
            var indexScalarsDictionary =
                GradeKVectorStorageComposerDictionary.TryGetValue(2, out var composer)
                    ? composer.IndexScalarsDictionary.ToDictionary(
                        pair => pair.Key, 
                        pair => pair.Value
                    )
                    : new Dictionary<ulong, T>();

            return ScalarProcessor.CreateBivector(indexScalarsDictionary);
        }

        public override IGasKVector<T> GetKVectorStorageCopy(uint grade)
        {
            var indexScalarsDictionary =
                GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var composer)
                    ? composer.IndexScalarsDictionary.ToDictionary(
                        pair => pair.Key, 
                        pair => pair.Value
                    )
                    : new Dictionary<ulong, T>();

            return ScalarProcessor.CreateKVector(grade, 
                indexScalarsDictionary
            );
        }
    }
}