using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraLib.Algebra.Basis;
using GeometricAlgebraLib.Algebra.Multivectors.Terms;
using GeometricAlgebraLib.Processing.Scalars;

namespace GeometricAlgebraLib.Storage.Composers
{
    public sealed class GaMultivectorGradedStorageComposer<TScalar> 
        : GaMultivectorStorageComposerBase<TScalar>
    {
        public Dictionary<int, GaKVectorStorageComposer<TScalar>> GradeKVectorStorageComposerDictionary { get; }
            = new();


        public override TScalar this[ulong id]
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

        public override TScalar this[int grade, ulong index]
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


        internal GaMultivectorGradedStorageComposer(IGaScalarProcessor<TScalar> scalarProcessor)
            : base(scalarProcessor)
        {
        }


        private GaKVectorStorageComposer<TScalar> GetOrCreateKVectorStorageComposer(int grade)
        {
            if (GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
                return storage;

            storage = new GaKVectorStorageComposer<TScalar>(ScalarProcessor, grade);

            GradeKVectorStorageComposerDictionary.Add(grade, storage);

            return storage;
        }


        public override bool IsEmpty()
        {
            return GradeKVectorStorageComposerDictionary.Count == 0 || 
                   GradeKVectorStorageComposerDictionary.Values.All(d => d.IsEmpty());
        }

        public override GaMultivectorStorageComposerBase<TScalar> Clear()
        {
            foreach (var storage in GradeKVectorStorageComposerDictionary.Values)
                storage.Clear();

            return this;
        }


        public GaMultivectorGradedStorageComposer<TScalar> SetKVectorComposer(GaKVectorStorageComposer<TScalar> composer)
        {
            var grade = composer.Grade;

            if (GradeKVectorStorageComposerDictionary.ContainsKey(grade))
                GradeKVectorStorageComposerDictionary[grade] = composer;
            else
                GradeKVectorStorageComposerDictionary.Add(grade, composer);

            return this;
        }


        public GaMultivectorGradedStorageComposer<TScalar> AddKVector(int grade, IEnumerable<KeyValuePair<ulong, TScalar>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.AddTerms(indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<TScalar> AddKVector(int grade, IEnumerable<Tuple<ulong, TScalar>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.AddTerms(indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<TScalar> AddKVector(IGaKVectorStorage<TScalar> storage)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(storage.Grade);

            kVectorStorage.AddTerms(storage.GetIndexScalarPairs());

            return this;
        }

        public GaMultivectorGradedStorageComposer<TScalar> AddKVectors(IEnumerable<KeyValuePair<int, Dictionary<ulong, TScalar>>> storagesList)
        {
            foreach (var (grade, storage) in storagesList)
                AddKVector(grade, storage);

            return this;
        }

        public GaMultivectorGradedStorageComposer<TScalar> AddKVectors(IEnumerable<IGaKVectorStorage<TScalar>> storagesList)
        {
            foreach (var storage in storagesList)
                AddKVector(storage);

            return this;
        }

        public GaMultivectorGradedStorageComposer<TScalar> AddLeftScaledKVector(TScalar scalingFactor, int grade, IEnumerable<KeyValuePair<ulong, TScalar>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.AddLeftScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<TScalar> AddLeftScaledKVector(TScalar scalingFactor, int grade, IEnumerable<Tuple<ulong, TScalar>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.AddLeftScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<TScalar> AddLeftScaledKVector(TScalar scalingFactor, GaKVectorStorageBase<TScalar> storage)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(storage.Grade);

            kVectorStorage.AddLeftScaledTerms(scalingFactor, storage.GetIndexScalarPairs());

            return this;
        }

        public GaMultivectorGradedStorageComposer<TScalar> AddRightScaledKVector(TScalar scalingFactor, int grade, IEnumerable<KeyValuePair<ulong, TScalar>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.AddRightScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<TScalar> AddRightScaledKVector(TScalar scalingFactor, int grade, IEnumerable<Tuple<ulong, TScalar>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.AddRightScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<TScalar> AddRightScaledKVector(TScalar scalingFactor, GaKVectorStorageBase<TScalar> storage)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(storage.Grade);

            kVectorStorage.AddRightScaledTerms(scalingFactor, storage.GetIndexScalarPairs());

            return this;
        }


        public GaMultivectorGradedStorageComposer<TScalar> SubtractKVector(int grade, IEnumerable<KeyValuePair<ulong, TScalar>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.SubtractTerms(indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<TScalar> SubtractKVector(int grade, IEnumerable<Tuple<ulong, TScalar>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.SubtractTerms(indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<TScalar> SubtractKVector(GaKVectorStorageBase<TScalar> storage)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(storage.Grade);

            kVectorStorage.SubtractTerms(storage.GetIndexScalarPairs());

            return this;
        }

        public GaMultivectorGradedStorageComposer<TScalar> SubtractLeftScaledKVector(TScalar scalingFactor, int grade, IEnumerable<KeyValuePair<ulong, TScalar>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.SubtractLeftScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<TScalar> SubtractLeftScaledKVector(TScalar scalingFactor, int grade, IEnumerable<Tuple<ulong, TScalar>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.SubtractLeftScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<TScalar> SubtractLeftScaledKVector(TScalar scalingFactor, GaKVectorStorageBase<TScalar> storage)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(storage.Grade);

            kVectorStorage.SubtractLeftScaledTerms(scalingFactor, storage.GetIndexScalarPairs());

            return this;
        }

        public GaMultivectorGradedStorageComposer<TScalar> SubtractRightScaledKVector(TScalar scalingFactor, int grade, IEnumerable<KeyValuePair<ulong, TScalar>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.SubtractRightScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<TScalar> SubtractRightScaledKVector(TScalar scalingFactor, int grade, IEnumerable<Tuple<ulong, TScalar>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.SubtractRightScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public GaMultivectorGradedStorageComposer<TScalar> SubtractRightScaledKVector(TScalar scalingFactor, GaKVectorStorageBase<TScalar> storage)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(storage.Grade);

            kVectorStorage.SubtractRightScaledTerms(scalingFactor, storage.GetIndexScalarPairs());

            return this;
        }


        public override GaMultivectorStorageComposerBase<TScalar> SetTerm(GaTerm<TScalar> term)
        {
            term.BasisBlade.GetGradeIndex(out var grade, out var index);

            this[grade, index] = term.Scalar;

            return this;
        }


        public override GaMultivectorStorageComposerBase<TScalar> SetTerms(IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList)
            {
                term.BasisBlade.GetGradeIndex(out var grade, out var index);

                this[grade, index] = term.Scalar;
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<TScalar> SetTermsToNegative()
        {
            foreach (var storage in GradeKVectorStorageComposerDictionary.Values)
                storage.SetTermsToNegative();

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SetTermsToNegative(IEnumerable<ulong> idsList)
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

        public override GaMultivectorStorageComposerBase<TScalar> SetTermsToNegative(IEnumerable<Tuple<int, ulong>> indicesList)
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


        public override GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<ulong> idsList, Func<ulong, TScalar> mappingFunc)
        {
            foreach (var id in idsList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                this[grade, index] = mappingFunc(id);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<Tuple<int, ulong>> idsList, Func<ulong, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in idsList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                this[grade, index] = mappingFunc(id);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<ulong> idsList, Func<int, ulong, TScalar> mappingFunc)
        {
            foreach (var id in idsList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                this[grade, index] = mappingFunc(grade, index);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<Tuple<int, ulong>> idsList, Func<int, ulong, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in idsList) 
                this[grade, index] = mappingFunc(grade, index);

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<ulong> idsList, Func<ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var id in idsList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                this[grade, index] = mappingFunc(id, scalar);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<Tuple<int, ulong>> idsList, Func<ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in idsList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var scalar = this[grade, index];

                this[grade, index] = mappingFunc(id, scalar);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<ulong> idsList, Func<int, ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var id in idsList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[id];

                this[grade, index] = mappingFunc(grade, index, scalar);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<Tuple<int, ulong>> idsList, Func<int, ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in idsList)
            {
                var scalar = this[grade, index];

                this[grade, index] = mappingFunc(grade, index, scalar);
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<TScalar> LeftScaleTerms(TScalar scalingFactor)
        {
            foreach (var storage in GradeKVectorStorageComposerDictionary.Values)
                storage.LeftScaleTerms(scalingFactor);

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> LeftScaleTerms(IEnumerable<ulong> indexList, TScalar scalingFactor)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                this[grade, index] = ScalarProcessor.Times(scalingFactor, scalar);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> LeftScaleTerms(IEnumerable<Tuple<int, ulong>> indexList, TScalar scalingFactor)
        {
            foreach (var (grade, index) in indexList)
            {
                var scalar = this[grade, index];

                this[grade, index] = ScalarProcessor.Times(scalingFactor, scalar);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> RightScaleTerms(TScalar scalingFactor)
        {
            foreach (var storage in GradeKVectorStorageComposerDictionary.Values)
                storage.RightScaleTerms(scalingFactor);

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> RightScaleTerms(IEnumerable<ulong> indexList, TScalar scalingFactor)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                this[grade, index] = ScalarProcessor.Times(scalar, scalingFactor);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> RightScaleTerms(IEnumerable<Tuple<int, ulong>> indexList, TScalar scalingFactor)
        {
            foreach (var (grade, index) in indexList)
            {
                var scalar = this[grade, index];

                this[grade, index] = ScalarProcessor.Times(scalar, scalingFactor);
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<TScalar> SetLeftScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList)
            {
                var (grade, index) = term.BasisBlade.GetGradeIndex();

                this[grade, index] = ScalarProcessor.Times(scalingFactor, term.Scalar);
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<TScalar> SetRightScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList)
            {
                var (grade, index) = term.BasisBlade.GetGradeIndex();

                this[grade, index] = ScalarProcessor.Times(term.Scalar, scalingFactor);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> AddTerm(ulong id, TScalar scalar)
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

        public override GaMultivectorStorageComposerBase<TScalar> AddTerm(int grade, ulong index, TScalar scalar)
        {
            if (GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage.AddTerm(index, scalar);

                return this;
            }

            this[grade, index] = scalar;

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> AddTerm(GaTerm<TScalar> term)
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


        public override GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, TScalar> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                AddTerm(grade, index, mappingFunc(id));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<ulong, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                AddTerm(grade, index, mappingFunc(id));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<ulong> indexList, Func<int, ulong, TScalar> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                AddTerm(grade, index, mappingFunc(grade, index));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<int, ulong, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
                AddTerm(grade, index, mappingFunc(grade, index));

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                AddTerm(grade, index, mappingFunc(id, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var scalar = this[grade, index];

                AddTerm(grade, index, mappingFunc(id, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<ulong> indexList, Func<int, ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                AddTerm(grade, index, mappingFunc(grade, index, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<int, ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var scalar = this[grade, index];

                AddTerm(grade, index, mappingFunc(grade, index, scalar));
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<TScalar> AddLeftScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList)
            {
                term.BasisBlade.GetGradeIndex(out var grade, out var index);

                AddTerm(grade, index, ScalarProcessor.Times(scalingFactor, term.Scalar));
            }

            return this;
        }
        

        public override GaMultivectorStorageComposerBase<TScalar> AddRightScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList)
            {
                term.BasisBlade.GetGradeIndex(out var grade, out var index);

                AddTerm(grade, index, ScalarProcessor.Times(term.Scalar, scalingFactor));
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<TScalar> SubtractTerm(ulong id, TScalar scalar)
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

        public override GaMultivectorStorageComposerBase<TScalar> SubtractTerm(int grade, ulong index, TScalar scalar)
        {
            if (GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage.SubtractTerm(index, scalar);

                return this;
            }

            this[grade, index] = ScalarProcessor.Negative(scalar);

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SubtractTerm(GaTerm<TScalar> term)
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


        public override GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, TScalar> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                SubtractTerm(grade, index, mappingFunc(id));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<ulong, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                SubtractTerm(grade, index, mappingFunc(id));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<int, ulong, TScalar> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                SubtractTerm(grade, index, mappingFunc(grade, index));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<int, ulong, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
                SubtractTerm(grade, index, mappingFunc(grade, index));

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                SubtractTerm(grade, index, mappingFunc(id, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var scalar = this[grade, index];

                SubtractTerm(grade, index, mappingFunc(id, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<int, ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                SubtractTerm(grade, index, mappingFunc(grade, index, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<int, ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var scalar = this[grade, index];

                SubtractTerm(grade, index, mappingFunc(grade, index, scalar));
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<TScalar> SubtractLeftScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList)
            {
                term.BasisBlade.GetGradeIndex(out var grade, out var index);

                SubtractTerm(grade, index, ScalarProcessor.Times(scalingFactor, term.Scalar));
            }

            return this;
        }
        

        public override GaMultivectorStorageComposerBase<TScalar> SubtractRightScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList)
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

        public override bool RemoveTerm(int grade, ulong index)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
                return false;

            var flag = storage.RemoveTerm(index);

            if (storage.Count == 0)
                GradeKVectorStorageComposerDictionary.Remove(grade);

            return flag;
        }

        public override GaMultivectorStorageComposerBase<TScalar> RemoveTerms(params ulong[] indexList)
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

        public override GaMultivectorStorageComposerBase<TScalar> RemoveZeroTerms()
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

        public override GaMultivectorStorageComposerBase<TScalar> RemoveNearZeroTerms()
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

        public GaMultivectorGradedStorageComposer<TScalar> RemoveKVector(int grade)
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
        public void SetKVector(int grade, IReadOnlyList<TScalar> scalarValuesList)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        public void SetKVector(int grade, TScalar scalingFactor, IReadOnlyList<TScalar> scalarValuesList)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaKVectorStorageComposer<TScalar>(ScalarProcessor, grade);

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
        public void SetKVector(int grade, IEnumerable<KeyValuePair<ulong, TScalar>> indexScalarPairs)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaKVectorStorageComposer<TScalar>(ScalarProcessor, grade);

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
        public void SetKVector(int grade, TScalar scalingFactor, IEnumerable<KeyValuePair<ulong, TScalar>> indexScalarPairs)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaKVectorStorageComposer<TScalar>(ScalarProcessor, grade);

                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.SetLeftScaledTerms(scalingFactor, indexScalarPairs);

            if (storage.IsEmpty())
                GradeKVectorStorageComposerDictionary.Remove(grade);
        }

        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        public void SetKVector(IGaKVectorStorage<TScalar> storage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexScalarsDictionary"></param>
        /// <returns></returns>
        public void SetKVectorStorage(int grade, Dictionary<ulong, TScalar> indexScalarsDictionary)
        {
            if (GradeKVectorStorageComposerDictionary.ContainsKey(grade))
                GradeKVectorStorageComposerDictionary[grade].SetStorage(indexScalarsDictionary);
            else
            {
                var composer = new GaKVectorStorageComposer<TScalar>(ScalarProcessor, grade, indexScalarsDictionary);

                GradeKVectorStorageComposerDictionary.Add(grade, composer);
            }
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="scalingFactor"></param>
        /// <param name="kVector"></param>
        /// <returns></returns>
        public void SetKVector(TScalar scalingFactor, GaKVectorStorage<TScalar> kVector)
        {
            throw new NotImplementedException();
        }


        public GaMultivectorGradedStorageComposer<TScalar> SetKVectors(IEnumerable<KeyValuePair<int, Dictionary<ulong, TScalar>>> storagesList)
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
        public void SetKVectors(IEnumerable<IGaKVectorStorage<TScalar>> kVectorsList)
        {
            foreach (var storage in kVectorsList)
                SetKVector(storage);
        }

        /// <summary>
        /// Set some terms using the given k-vectors data scaled by a scaling factor
        /// </summary>
        /// <param name="scaledKVectorsList"></param>
        /// <returns></returns>
        public void SetKVectors(IEnumerable<Tuple<TScalar, GaKVectorStorage<TScalar>>> scaledKVectorsList)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        public void AddKVector(int grade, IReadOnlyList<TScalar> scalarValuesList)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        public void AddKVector(int grade, TScalar scalingFactor, IReadOnlyList<TScalar> scalarValuesList)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="indexScalarPairs"></param>
        /// <returns></returns>
        public void AddLeftScaledKVector(int grade, TScalar scalingFactor, IEnumerable<KeyValuePair<ulong, TScalar>> indexScalarPairs)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaKVectorStorageComposer<TScalar>(ScalarProcessor, grade);
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
        public void AddKVector(TScalar scalingFactor, IGaKVectorStorage<TScalar> kVector)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set some terms using the given k-vectors data scaled by a scaling factor
        /// </summary>
        /// <param name="scaledKVectorsList"></param>
        /// <returns></returns>
        public void AddLeftScaledKVectors(IEnumerable<Tuple<TScalar, IGaKVectorStorage<TScalar>>> scaledKVectorsList)
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
        public void SubtractKVector(int grade, IReadOnlyList<TScalar> scalarValuesList)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        public void SubtractKVector(int grade, TScalar scalingFactor, IReadOnlyList<TScalar> scalarValuesList)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="indexScalarPairs"></param>
        /// <returns></returns>
        public void SubtractLeftScaledKVector(int grade, TScalar scalingFactor, IEnumerable<KeyValuePair<ulong, TScalar>> indexScalarPairs)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaKVectorStorageComposer<TScalar>(ScalarProcessor, grade);
                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.SubtractLeftScaledTerms(scalingFactor, indexScalarPairs);
        }

        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="kVector"></param>
        /// <returns></returns>
        public void SubtractKVector(IGaKVectorStorage<TScalar> kVector)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="scalingFactor"></param>
        /// <param name="kVector"></param>
        /// <returns></returns>
        public void SubtractKVector(TScalar scalingFactor, IGaKVectorStorage<TScalar> kVector)
        {
            throw new NotImplementedException();
        }


        public GaMultivectorGradedStorageComposer<TScalar> SubtractKVectors(IEnumerable<KeyValuePair<int, Dictionary<ulong, TScalar>>> storagesList)
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
        public void SubtractKVectors(IEnumerable<IGaKVectorStorage<TScalar>> kVectorsList)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set some terms using the given k-vectors data scaled by a scaling factor
        /// </summary>
        /// <param name="scaledKVectorsList"></param>
        /// <returns></returns>
        public void SubtractLeftScaledKVectors(IEnumerable<Tuple<TScalar, IGaKVectorStorage<TScalar>>> scaledKVectorsList)
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove the given terms if possible, else set to zero
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexList"></param>
        public void RemoveTerms(int grade, IEnumerable<ulong> indexList)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Remove the given terms if possible, else set to zero
        /// </summary>
        /// <param name="grade"></param>
        public void RemoveTermsOfGrade(int grade)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Remove the given term if zero
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nearZeroFlag"></param>
        /// <returns></returns>
        public void RemoveTermIfZero(ulong id, bool nearZeroFlag = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove the given term if zero
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <param name="nearZeroFlag"></param>
        /// <returns></returns>
        public void RemoveTermIfZero(int grade, ulong index, bool nearZeroFlag = false)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Remove all terms from storage where their scalar values is near zero
        /// </summary>
        public void RemoveZeroTermsOfGrade(int grade, bool nearZeroFlag = false)
        {
            throw new NotImplementedException();
        }


        public override IGaMultivectorStorage<TScalar> GetCompactStorage()
        {
            var gradesCount = GradeKVectorStorageComposerDictionary.Count;

            if (gradesCount == 0)
                return GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);

            if (gradesCount == 1)
                GradeKVectorStorageComposerDictionary.First().Value.GetCompactStorage();

            return CreateMultivectorGradedStorage();
        }

        public override IGaMultivectorGradedStorage<TScalar> GetCompactGradedStorage()
        {
            var gradesCount = GradeKVectorStorageComposerDictionary.Count;

            if (gradesCount == 0)
                return GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);

            if (gradesCount == 1)
                GradeKVectorStorageComposerDictionary.First().Value.GetCompactStorage();

            return CreateMultivectorGradedStorage();
        }

        public override GaMultivectorStorageBase<TScalar> GetMultivectorStorage()
        {
            return CreateMultivectorGradedStorage();
        }
        
        public GaMultivectorGradedStorage<TScalar> CreateMultivectorGradedStorage()
        {
            var kVectorsDictionary = new Dictionary<int, Dictionary<ulong, TScalar>>();

            foreach (var (grade, composer) in GradeKVectorStorageComposerDictionary)
                kVectorsDictionary.Add(grade, composer.IndexScalarsDictionary);

            return GaMultivectorGradedStorage<TScalar>.Create(
                ScalarProcessor, 
                kVectorsDictionary
            );
        }

        public GaVectorStorage<TScalar> CreateVectorStorage()
        {
            var indexScalarsDictionary =
                GradeKVectorStorageComposerDictionary.TryGetValue(1, out var composer)
                    ? composer.IndexScalarsDictionary
                    : new Dictionary<ulong, TScalar>();

            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor, 
                indexScalarsDictionary
            );
        }

        public GaBivectorStorage<TScalar> CreateBivectorStorage()
        {
            var indexScalarsDictionary =
                GradeKVectorStorageComposerDictionary.TryGetValue(1, out var composer)
                    ? composer.IndexScalarsDictionary
                    : new Dictionary<ulong, TScalar>();

            return GaBivectorStorage<TScalar>.Create(
                ScalarProcessor, 
                indexScalarsDictionary
            );
        }

        public GaKVectorStorage<TScalar> CreateKVectorStorage(int grade)
        {
            var indexScalarsDictionary =
                GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var composer)
                    ? composer.IndexScalarsDictionary
                    : new Dictionary<ulong, TScalar>();

            return GaKVectorStorage<TScalar>.Create(
                ScalarProcessor, 
                grade, 
                indexScalarsDictionary
            );
        }

        
        public override IGaMultivectorStorage<TScalar> GetStorageCopy()
        {
            return GetMultivectorGradedStorageCopy();
        }

        public override IGaMultivectorStorage<TScalar> GetStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            var kVectorsDictionary = new Dictionary<int, Dictionary<ulong, TScalar>>();

            foreach (var (grade, composer) in GradeKVectorStorageComposerDictionary)
            {
                var indexScalarDictionary = composer.IndexScalarsDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => scalarMapping(pair.Value)
                );

                kVectorsDictionary.Add(grade, indexScalarDictionary);
            }

            return GaMultivectorGradedStorage<TScalar>.Create(
                ScalarProcessor, 
                kVectorsDictionary
            );
        }

        public override GaMultivectorGradedStorage<TScalar> GetMultivectorGradedStorageCopy()
        {
            var kVectorsDictionary = new Dictionary<int, Dictionary<ulong, TScalar>>();

            foreach (var (grade, composer) in GradeKVectorStorageComposerDictionary)
            {
                var indexScalarDictionary = composer.IndexScalarsDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                );

                kVectorsDictionary.Add(grade, indexScalarDictionary);
            }

            return GaMultivectorGradedStorage<TScalar>.Create(
                ScalarProcessor, 
                kVectorsDictionary
            );
        }

        public override GaMultivectorTermsStorage<TScalar> GetMultivectorTermsStorageCopy()
        {
            var idScalarDictionary = new Dictionary<ulong, TScalar>();

            foreach (var (grade, composer) in GradeKVectorStorageComposerDictionary)
            {
                var indexScalarDictionary = composer.IndexScalarsDictionary;

                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    var id = GaBasisUtils.BasisBladeId(grade, index);

                    idScalarDictionary.Add(id, scalar);
                }
            }

            return GaMultivectorTermsStorage<TScalar>.Create(
                ScalarProcessor, 
                idScalarDictionary
            );
        }

        public override GaMultivectorTreeStorage<TScalar> GetMultivectorTreeStorageCopy()
        {
            var idScalarDictionary = new Dictionary<ulong, TScalar>();

            foreach (var (grade, composer) in GradeKVectorStorageComposerDictionary)
            {
                var indexScalarDictionary = composer.IndexScalarsDictionary;

                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    var id = GaBasisUtils.BasisBladeId(grade, index);

                    idScalarDictionary.Add(id, scalar);
                }
            }

            return GaMultivectorTreeStorage<TScalar>.Create(
                ScalarProcessor, 
                idScalarDictionary
            );
        }

        public override GaScalarTermStorage<TScalar> GetScalarStorage()
        {
            return GaScalarTermStorage<TScalar>.Create(ScalarProcessor, this[0, 0]);
        }

        public override GaVectorStorage<TScalar> GetVectorStorageCopy()
        {
            var indexScalarsDictionary =
                GradeKVectorStorageComposerDictionary.TryGetValue(1, out var composer)
                    ? composer.IndexScalarsDictionary.ToDictionary(
                        pair => pair.Key, 
                        pair => pair.Value
                    )
                    : new Dictionary<ulong, TScalar>();

            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor, 
                indexScalarsDictionary
            );
        }

        public override GaBivectorStorage<TScalar> GetBivectorStorageCopy(int grade)
        {
            var indexScalarsDictionary =
                GradeKVectorStorageComposerDictionary.TryGetValue(2, out var composer)
                    ? composer.IndexScalarsDictionary.ToDictionary(
                        pair => pair.Key, 
                        pair => pair.Value
                    )
                    : new Dictionary<ulong, TScalar>();

            return GaBivectorStorage<TScalar>.Create(
                ScalarProcessor, 
                indexScalarsDictionary
            );
        }

        public override GaKVectorStorage<TScalar> GetKVectorStorageCopy(int grade)
        {
            var indexScalarsDictionary =
                GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var composer)
                    ? composer.IndexScalarsDictionary.ToDictionary(
                        pair => pair.Key, 
                        pair => pair.Value
                    )
                    : new Dictionary<ulong, TScalar>();

            return GaKVectorStorage<TScalar>.Create(
                ScalarProcessor, 
                grade, 
                indexScalarsDictionary
            );
        }
    }
}