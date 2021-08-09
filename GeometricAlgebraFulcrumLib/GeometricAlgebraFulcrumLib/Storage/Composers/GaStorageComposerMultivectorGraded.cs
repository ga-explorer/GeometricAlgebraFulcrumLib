using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Terms;
using GeometricAlgebraFulcrumLib.Structures.Even;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public sealed class GaStorageComposerMultivectorGraded<T> 
        : GaStorageComposerMultivectorBase<T>
    {
        public Dictionary<uint, GaStorageComposerKVector<T>> GradeKVectorStorageComposerDictionary { get; }
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


        internal GaStorageComposerMultivectorGraded(IGaScalarProcessor<T> scalarProcessor)
            : base(scalarProcessor)
        {
        }

        internal GaStorageComposerMultivectorGraded(IGaScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, T> idScalarDictionary)
            : base(scalarProcessor)
        {
            SetTerms(idScalarDictionary);
        }

        internal GaStorageComposerMultivectorGraded(IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> idScalarPairs)
            : base(scalarProcessor)
        {
            AddTerms(idScalarPairs);
        }

        internal GaStorageComposerMultivectorGraded(IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<ulong, T>> idScalarTuples)
            : base(scalarProcessor)
        {
            AddTerms(idScalarTuples);
        }

        internal GaStorageComposerMultivectorGraded(IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<uint, ulong, T>> gradeIndexScalarTuples)
            : base(scalarProcessor)
        {
            AddTerms(gradeIndexScalarTuples);
        }


        private GaStorageComposerKVector<T> GetOrCreateKVectorStorageComposer(uint grade)
        {
            if (GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
                return storage;

            storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);

            GradeKVectorStorageComposerDictionary.Add(grade, storage);

            return storage;
        }


        public override bool IsEmpty()
        {
            return GradeKVectorStorageComposerDictionary.Count == 0 || 
                   GradeKVectorStorageComposerDictionary.Values.All(d => d.IsEmpty());
        }

        public override IGaStorageComposerMultivector<T> Clear()
        {
            foreach (var storage in GradeKVectorStorageComposerDictionary.Values)
                storage.Clear();

            return this;
        }

        public override IGaStorageComposerMultivector<T> SetScalar(T scalar)
        {
            return SetTerm(0, 0, scalar);
        }


        public GaStorageComposerMultivectorGraded<T> SetKVectorComposer(GaStorageComposerKVector<T> composer)
        {
            var grade = composer.Grade;

            if (GradeKVectorStorageComposerDictionary.ContainsKey(grade))
                GradeKVectorStorageComposerDictionary[grade] = composer;
            else
                GradeKVectorStorageComposerDictionary.Add(grade, composer);

            return this;
        }


        public override IGaStorageComposerMultivector<T> AddKVector(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.AddTerms(indexScalarPairs);

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddKVector(uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.AddTerms(indexScalarPairs);

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddKVector(IGaStorageKVector<T> storage)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(storage.Grade);

            kVectorStorage.AddTerms(storage.IndexScalarDictionary);

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddKVectors(IEnumerable<KeyValuePair<uint, Dictionary<ulong, T>>> storagesList)
        {
            foreach (var (grade, storage) in storagesList)
                AddKVector(grade, storage);

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddKVectors(IEnumerable<KeyValuePair<uint, IGaEvenDictionary<T>>> storagesList)
        {
            foreach (var (grade, storage) in storagesList)
                AddKVector(grade, storage);

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddKVectors(IEnumerable<IGaStorageKVector<T>> storagesList)
        {
            foreach (var storage in storagesList)
                AddKVector(storage);

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddLeftScaledKVector(T scalingFactor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.AddLeftScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddLeftScaledKVector(T scalingFactor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.AddLeftScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddLeftScaledKVector(T scalingFactor, GaStorageKVectorBase<T> storage)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(storage.Grade);

            kVectorStorage.AddLeftScaledTerms(scalingFactor, storage.IndexScalarDictionary);

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddRightScaledKVector(T scalingFactor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.AddRightScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddRightScaledKVector(T scalingFactor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.AddRightScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddRightScaledKVector(T scalingFactor, GaStorageKVectorBase<T> storage)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(storage.Grade);

            kVectorStorage.AddRightScaledTerms(scalingFactor, storage.IndexScalarDictionary);

            return this;
        }


        public override IGaStorageComposerMultivector<T> SubtractKVector(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.SubtractTerms(indexScalarPairs);

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractKVector(uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.SubtractTerms(indexScalarPairs);

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractKVector(GaStorageKVectorBase<T> storage)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(storage.Grade);

            kVectorStorage.SubtractTerms(storage.IndexScalarDictionary);

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractLeftScaledKVector(T scalingFactor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.SubtractLeftScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractLeftScaledKVector(T scalingFactor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.SubtractLeftScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractLeftScaledKVector(T scalingFactor, GaStorageKVectorBase<T> storage)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(storage.Grade);

            kVectorStorage.SubtractLeftScaledTerms(scalingFactor, storage.IndexScalarDictionary);

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractRightScaledKVector(T scalingFactor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.SubtractRightScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractRightScaledKVector(T scalingFactor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(grade);

            kVectorStorage.SubtractRightScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractRightScaledKVector(T scalingFactor, GaStorageKVectorBase<T> storage)
        {
            var kVectorStorage = GetOrCreateKVectorStorageComposer(storage.Grade);

            kVectorStorage.SubtractRightScaledTerms(scalingFactor, storage.IndexScalarDictionary);

            return this;
        }


        public override IGaStorageComposerMultivector<T> SetTerm(GaTerm<T> term)
        {
            term.BasisBlade.GetGradeIndex(out var grade, out var index);

            this[grade, index] = term.Scalar;

            return this;
        }


        public override IGaStorageComposerMultivector<T> SetTerms(IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                term.BasisBlade.GetGradeIndex(out var grade, out var index);

                this[grade, index] = term.Scalar;
            }

            return this;
        }


        public override IGaStorageComposerMultivector<T> SetTermsToNegative()
        {
            foreach (var storage in GradeKVectorStorageComposerDictionary.Values)
                storage.SetTermsToNegative();

            return this;
        }

        public override IGaStorageComposerMultivector<T> SetTermsToNegative(IEnumerable<ulong> idsList)
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

        public override IGaStorageComposerMultivector<T> SetTermsToNegative(IEnumerable<Tuple<uint, ulong>> indicesList)
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


        public override IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<ulong, T> mappingFunc)
        {
            foreach (var id in idsList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                this[grade, index] = mappingFunc(id);
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList, Func<ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in idsList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                this[grade, index] = mappingFunc(id);
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var id in idsList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                this[grade, index] = mappingFunc(grade, index);
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in idsList) 
                this[grade, index] = mappingFunc(grade, index);

            return this;
        }

        public override IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var id in idsList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                this[grade, index] = mappingFunc(id, scalar);
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var (grade, index) in idsList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var scalar = this[grade, index];

                this[grade, index] = mappingFunc(id, scalar);
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var id in idsList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[id];

                this[grade, index] = mappingFunc(grade, index, scalar);
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList, Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var (grade, index) in idsList)
            {
                var scalar = this[grade, index];

                this[grade, index] = mappingFunc(grade, index, scalar);
            }

            return this;
        }


        public override IGaStorageComposerMultivector<T> LeftScaleTerms(T scalingFactor)
        {
            foreach (var storage in GradeKVectorStorageComposerDictionary.Values)
                storage.LeftScaleTerms(scalingFactor);

            return this;
        }

        public override IGaStorageComposerMultivector<T> LeftScaleTerms(IEnumerable<ulong> indexList, T scalingFactor)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                this[grade, index] = ScalarProcessor.Times(scalingFactor, scalar);
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> LeftScaleTerms(IEnumerable<Tuple<uint, ulong>> indexList, T scalingFactor)
        {
            foreach (var (grade, index) in indexList)
            {
                var scalar = this[grade, index];

                this[grade, index] = ScalarProcessor.Times(scalingFactor, scalar);
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> RightScaleTerms(T scalingFactor)
        {
            foreach (var storage in GradeKVectorStorageComposerDictionary.Values)
                storage.RightScaleTerms(scalingFactor);

            return this;
        }

        public override IGaStorageComposerMultivector<T> RightScaleTerms(IEnumerable<ulong> indexList, T scalingFactor)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                this[grade, index] = ScalarProcessor.Times(scalar, scalingFactor);
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> RightScaleTerms(IEnumerable<Tuple<uint, ulong>> indexList, T scalingFactor)
        {
            foreach (var (grade, index) in indexList)
            {
                var scalar = this[grade, index];

                this[grade, index] = ScalarProcessor.Times(scalar, scalingFactor);
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddScalar(T scalar)
        {
            return AddTerm(0, scalar);
        }


        public override IGaStorageComposerMultivector<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                var (grade, index) = term.BasisBlade.GetGradeIndex();

                this[grade, index] = ScalarProcessor.Times(scalingFactor, term.Scalar);
            }

            return this;
        }


        public override IGaStorageComposerMultivector<T> SetRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                var (grade, index) = term.BasisBlade.GetGradeIndex();

                this[grade, index] = ScalarProcessor.Times(term.Scalar, scalingFactor);
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddTerm(ulong id, T scalar)
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

        public override IGaStorageComposerMultivector<T> AddTerm(uint grade, ulong index, T scalar)
        {
            if (GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage.AddTerm(index, scalar);

                return this;
            }

            this[grade, index] = scalar;

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddTerm(GaTerm<T> term)
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


        public override IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                AddTerm(grade, index, mappingFunc(id));
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                AddTerm(grade, index, mappingFunc(id));
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                AddTerm(grade, index, mappingFunc(grade, index));
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
                AddTerm(grade, index, mappingFunc(grade, index));

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                AddTerm(grade, index, mappingFunc(id, scalar));
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var scalar = this[grade, index];

                AddTerm(grade, index, mappingFunc(id, scalar));
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                AddTerm(grade, index, mappingFunc(grade, index, scalar));
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var scalar = this[grade, index];

                AddTerm(grade, index, mappingFunc(grade, index, scalar));
            }

            return this;
        }


        public override IGaStorageComposerMultivector<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                term.BasisBlade.GetGradeIndex(out var grade, out var index);

                AddTerm(grade, index, ScalarProcessor.Times(scalingFactor, term.Scalar));
            }

            return this;
        }
        

        public override IGaStorageComposerMultivector<T> AddRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                term.BasisBlade.GetGradeIndex(out var grade, out var index);

                AddTerm(grade, index, ScalarProcessor.Times(term.Scalar, scalingFactor));
            }

            return this;
        }


        public override IGaStorageComposerMultivector<T> SubtractScalar(T scalar)
        {
            return SubtractTerm(0, 0, scalar);
        }
        
        public override IGaStorageComposerMultivector<T> SubtractTerm(ulong id, T scalar)
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

        public override IGaStorageComposerMultivector<T> SubtractTerm(uint grade, ulong index, T scalar)
        {
            if (GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage.SubtractTerm(index, scalar);

                return this;
            }

            this[grade, index] = ScalarProcessor.Negative(scalar);

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractTerm(GaTerm<T> term)
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


        public override IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                SubtractTerm(grade, index, mappingFunc(id));
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                SubtractTerm(grade, index, mappingFunc(id));
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                SubtractTerm(grade, index, mappingFunc(grade, index));
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
                SubtractTerm(grade, index, mappingFunc(grade, index));

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                SubtractTerm(grade, index, mappingFunc(id, scalar));
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var scalar = this[grade, index];

                SubtractTerm(grade, index, mappingFunc(id, scalar));
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[grade, index];

                SubtractTerm(grade, index, mappingFunc(grade, index, scalar));
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var scalar = this[grade, index];

                SubtractTerm(grade, index, mappingFunc(grade, index, scalar));
            }

            return this;
        }


        public override IGaStorageComposerMultivector<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                term.BasisBlade.GetGradeIndex(out var grade, out var index);

                SubtractTerm(grade, index, ScalarProcessor.Times(scalingFactor, term.Scalar));
            }

            return this;
        }
        

        public override IGaStorageComposerMultivector<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
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

        public override IGaStorageComposerMultivector<T> RemoveTerms(params ulong[] indexList)
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

        public override IGaStorageComposerMultivector<T> RemoveZeroTerms()
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

        public override IGaStorageComposerMultivector<T> RemoveNearZeroTerms()
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

        public override IGaStorageComposerMultivector<T> RemoveKVector(uint grade)
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
        public override IGaStorageComposerMultivector<T> SetKVector(uint grade, IReadOnlyList<T> scalarValuesList)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);

                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            for (var index = 0; index < scalarValuesList.Count; index++)
                storage.SetTerm(
                    (ulong)index, 
                    scalarValuesList[index]
                );

            if (storage.IsEmpty())
                GradeKVectorStorageComposerDictionary.Remove(grade);

            return this;
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        public override IGaStorageComposerMultivector<T> SetKVector(uint grade, T scalingFactor, IReadOnlyList<T> scalarValuesList)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);

                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            for (var index = 0; index < scalarValuesList.Count; index++)
                storage.SetTerm(
                    (ulong)index, 
                    ScalarProcessor.Times(scalingFactor, scalarValuesList[index])
                );

            if (storage.IsEmpty())
                GradeKVectorStorageComposerDictionary.Remove(grade);
            
            return this;
        }

        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexScalarPairs"></param>
        /// <returns></returns>
        public override IGaStorageComposerMultivector<T> SetKVector(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);

                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.SetTerms(indexScalarPairs);

            if (storage.IsEmpty())
                GradeKVectorStorageComposerDictionary.Remove(grade);
            
            return this;
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="indexScalarPairs"></param>
        /// <returns></returns>
        public override IGaStorageComposerMultivector<T> SetKVector(uint grade, T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);

                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.SetLeftScaledTerms(scalingFactor, indexScalarPairs);

            if (storage.IsEmpty())
                GradeKVectorStorageComposerDictionary.Remove(grade);
            
            return this;
        }

        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="kVector"></param>
        /// <returns></returns>
        public override IGaStorageComposerMultivector<T> SetKVector(IGaStorageKVector<T> kVector)
        {
            var grade = kVector.Grade;

            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);

                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.SetTerms(kVector.IndexScalarDictionary);

            if (storage.IsEmpty())
                GradeKVectorStorageComposerDictionary.Remove(grade);
            
            return this;
        }

        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexScalarsDictionary"></param>
        /// <returns></returns>
        public override IGaStorageComposerMultivector<T> SetKVectorStorage(uint grade, Dictionary<ulong, T> indexScalarsDictionary)
        {
            if (GradeKVectorStorageComposerDictionary.ContainsKey(grade))
                GradeKVectorStorageComposerDictionary[grade].SetStorage(indexScalarsDictionary);
            else
            {
                var composer = new GaStorageComposerKVector<T>(ScalarProcessor, grade, indexScalarsDictionary);

                GradeKVectorStorageComposerDictionary.Add(grade, composer);
            }
            
            return this;
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="scalingFactor"></param>
        /// <param name="kVector"></param>
        /// <returns></returns>
        public override IGaStorageComposerMultivector<T> SetKVector(T scalingFactor, IGaStorageKVector<T> kVector)
        {
            var grade = kVector.Grade;

            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);

                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.SetLeftScaledTerms(
                scalingFactor, 
                kVector.IndexScalarDictionary
            );

            if (storage.IsEmpty())
                GradeKVectorStorageComposerDictionary.Remove(grade);
            
            return this;
        }


        public override IGaStorageComposerMultivector<T> SetKVectors(IEnumerable<KeyValuePair<uint, Dictionary<ulong, T>>> storagesList)
        {
            foreach (var (grade, storage) in storagesList)
                SetKVector(grade, storage);

            return this;
        }

        public override IGaStorageComposerMultivector<T> SetKVectors(IEnumerable<KeyValuePair<uint, IGaEvenDictionary<T>>> storagesList)
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
        public override IGaStorageComposerMultivector<T> SetKVectors(IEnumerable<IGaStorageKVector<T>> kVectorsList)
        {
            foreach (var storage in kVectorsList)
                SetKVector(storage);
            
            return this;
        }

        /// <summary>
        /// Set some terms using the given k-vectors data scaled by a scaling factor
        /// </summary>
        /// <param name="scaledKVectorsList"></param>
        /// <returns></returns>
        public override IGaStorageComposerMultivector<T> SetKVectors(IEnumerable<Tuple<T, IGaStorageKVector<T>>> scaledKVectorsList)
        {
            foreach (var (scalingFactor, kVector) in scaledKVectorsList)
                SetKVector(scalingFactor, kVector);
            
            return this;
        }



        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        public override IGaStorageComposerMultivector<T> AddKVector(uint grade, IReadOnlyList<T> scalarValuesList)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);
                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.AddTerms(scalarValuesList);
            
            return this;
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        public override IGaStorageComposerMultivector<T> AddKVector(uint grade, T scalingFactor, IReadOnlyList<T> scalarValuesList)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);
                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.AddLeftScaledTerms(scalingFactor, scalarValuesList);
            
            return this;
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="indexScalarPairs"></param>
        /// <returns></returns>
        public override IGaStorageComposerMultivector<T> AddLeftScaledKVector(uint grade, T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);
                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.AddLeftScaledTerms(scalingFactor, indexScalarPairs);
            
            return this;
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="scalingFactor"></param>
        /// <param name="kVector"></param>
        /// <returns></returns>
        public override IGaStorageComposerMultivector<T> AddKVector(T scalingFactor, IGaStorageKVector<T> kVector)
        {
            var grade = kVector.Grade;

            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);
                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.AddLeftScaledTerms(
                scalingFactor, 
                kVector.IndexScalarDictionary
            );
            
            return this;
        }

        /// <summary>
        /// Set some terms using the given k-vectors data scaled by a scaling factor
        /// </summary>
        /// <param name="scaledKVectorsList"></param>
        /// <returns></returns>
        public override IGaStorageComposerMultivector<T> AddLeftScaledKVectors(IEnumerable<Tuple<T, IGaStorageKVector<T>>> scaledKVectorsList)
        {
            foreach (var (scalingFactor, storage) in scaledKVectorsList)
                AddLeftScaledKVector(storage.Grade, scalingFactor, storage.IndexScalarDictionary);
            
            return this;
        }


        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        public override IGaStorageComposerMultivector<T> SubtractKVector(uint grade, IReadOnlyList<T> scalarValuesList)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);
                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.SubtractTerms(scalarValuesList);
            
            return this;
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        public override IGaStorageComposerMultivector<T> SubtractKVector(uint grade, T scalingFactor, IReadOnlyList<T> scalarValuesList)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);
                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.SubtractLeftScaledTerms(scalingFactor, scalarValuesList);
            
            return this;
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="indexScalarPairs"></param>
        /// <returns></returns>
        public override IGaStorageComposerMultivector<T> SubtractLeftScaledKVector(uint grade, T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);
                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.SubtractLeftScaledTerms(scalingFactor, indexScalarPairs);

            return this;
        }

        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="kVector"></param>
        /// <returns></returns>
        public override IGaStorageComposerMultivector<T> SubtractKVector(IGaStorageKVector<T> kVector)
        {
            var grade = kVector.Grade;

            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);
                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.SubtractTerms(kVector.IndexScalarDictionary);
            
            return this;
        }

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="scalingFactor"></param>
        /// <param name="kVector"></param>
        /// <returns></returns>
        public override IGaStorageComposerMultivector<T> SubtractKVector(T scalingFactor, IGaStorageKVector<T> kVector)
        {
            var grade = kVector.Grade;

            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var storage))
            {
                storage = new GaStorageComposerKVector<T>(ScalarProcessor, grade);
                GradeKVectorStorageComposerDictionary.Add(grade, storage);
            }

            storage.SubtractLeftScaledTerms(scalingFactor, kVector.IndexScalarDictionary);
            
            return this;
        }

        
        public override IGaStorageComposerMultivector<T> SubtractKVectors(IEnumerable<KeyValuePair<uint, IGaEvenDictionary<T>>> storagesList)
        {
            foreach (var (grade, storage) in storagesList)
                SubtractKVector(grade, storage);

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractKVectors(IEnumerable<KeyValuePair<uint, Dictionary<ulong, T>>> storagesList)
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
        public override IGaStorageComposerMultivector<T> SubtractKVectors(IEnumerable<IGaStorageKVector<T>> kVectorsList)
        {
            foreach (var kVector in kVectorsList)
                SubtractKVector(kVector);
            
            return this;
        }

        /// <summary>
        /// Set some terms using the given k-vectors data scaled by a scaling factor
        /// </summary>
        /// <param name="scaledKVectorsList"></param>
        /// <returns></returns>
        public override IGaStorageComposerMultivector<T> SubtractLeftScaledKVectors(IEnumerable<Tuple<T, IGaStorageKVector<T>>> scaledKVectorsList)
        {
            foreach (var (scalingFactor, storage) in scaledKVectorsList)
                SubtractLeftScaledKVector(storage.Grade, scalingFactor, storage.IndexScalarDictionary);
            
            return this;
        }


        /// <summary>
        /// Remove the given terms if possible, else set to zero
        /// </summary>
        /// <param name="idsList"></param>
        public override IGaStorageComposerMultivector<T> RemoveTerms(IEnumerable<ulong> idsList)
        {
            foreach (var id in idsList)
                RemoveTerm(id);
            
            return this;
        }

        /// <summary>
        /// Remove the given terms if possible, else set to zero
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexList"></param>
        public override IGaStorageComposerMultivector<T> RemoveTerms(uint grade, IEnumerable<ulong> indexList)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var composer))
                return this;

            composer.RemoveTerms(indexList);
            
            return this;
        }
        
        /// <summary>
        /// Remove the given terms if possible, else set to zero
        /// </summary>
        /// <param name="grade"></param>
        public override IGaStorageComposerMultivector<T> RemoveTermsOfGrade(uint grade)
        {
            if (GradeKVectorStorageComposerDictionary.ContainsKey(grade))
                GradeKVectorStorageComposerDictionary.Remove(grade);
            
            return this;
        }


        /// <summary>
        /// Remove the given term if zero
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nearZeroFlag"></param>
        /// <returns></returns>
        public override IGaStorageComposerMultivector<T> RemoveTermIfZero(ulong id, bool nearZeroFlag = false)
        {
            var (grade, index) = id.BasisBladeGradeIndex();

            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var composer))
                return this;

            composer.RemoveTermIfZero(index, nearZeroFlag);
            
            return this;
        }

        /// <summary>
        /// Remove the given term if zero
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <param name="nearZeroFlag"></param>
        /// <returns></returns>
        public override IGaStorageComposerMultivector<T> RemoveTermIfZero(uint grade, ulong index, bool nearZeroFlag = false)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var composer))
                return this;

            composer.RemoveTermIfZero(index, nearZeroFlag);
            
            return this;
        }
        
        /// <summary>
        /// Remove all terms from storage where their scalar values is near zero
        /// </summary>
        public override IGaStorageComposerMultivector<T> RemoveZeroTermsOfGrade(uint grade, bool nearZeroFlag = false)
        {
            if (!GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var composer))
                return this;

            composer.RemoveZeroTerms(nearZeroFlag);
            
            return this;
        }
        
        
        public override GaStorageScalar<T> GetScalar()
        {
            return ScalarProcessor.CreateStorageScalar(this[0, 0]);
        }

        public override GaStorageVector<T> GetVector()
        {
            var indexScalarsDictionary =
                GradeKVectorStorageComposerDictionary.TryGetValue(1, out var composer)
                    ? composer.IndexScalarsDictionary
                    : new Dictionary<ulong, T>();

            return ScalarProcessor.CreateStorageVector(indexScalarsDictionary);
        }

        public override GaStorageVector<T> GetVector(bool copyFlag)
        {
            var indexScalarsDictionary =
                GradeKVectorStorageComposerDictionary.TryGetValue(1, out var composer)
                    ? composer.IndexScalarsDictionary.ToDictionary(
                        pair => pair.Key, 
                        pair => pair.Value
                    )
                    : new Dictionary<ulong, T>();

            return ScalarProcessor.CreateStorageVector(indexScalarsDictionary);
        }

        public override GaStorageBivector<T> GetBivector()
        {
            var indexScalarsDictionary =
                GradeKVectorStorageComposerDictionary.TryGetValue(1, out var composer)
                    ? composer.IndexScalarsDictionary
                    : new Dictionary<ulong, T>();

            return ScalarProcessor.CreateStorageBivector(indexScalarsDictionary);
        }

        public override GaStorageBivector<T> GetBivector(bool copyFlag)
        {
            var indexScalarsDictionary =
                GradeKVectorStorageComposerDictionary.TryGetValue(2, out var composer)
                    ? composer.IndexScalarsDictionary.ToDictionary(
                        pair => pair.Key, 
                        pair => pair.Value
                    )
                    : new Dictionary<ulong, T>();

            return ScalarProcessor.CreateStorageBivector(indexScalarsDictionary);
        }

        public override IGaStorageKVector<T> GetKVector(uint grade)
        {
            var indexScalarsDictionary =
                GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var composer)
                    ? composer.IndexScalarsDictionary
                    : new Dictionary<ulong, T>();

            return ScalarProcessor.CreateStorageKVector(grade, indexScalarsDictionary);
        }

        public override IGaStorageKVector<T> GetKVector(uint grade, bool copyFlag)
        {
            var indexScalarsDictionary =
                GradeKVectorStorageComposerDictionary.TryGetValue(grade, out var composer)
                    ? composer.IndexScalarsDictionary.ToDictionary(
                        pair => pair.Key, 
                        pair => pair.Value
                    )
                    : new Dictionary<ulong, T>();

            return ScalarProcessor.CreateStorageKVector(grade, 
                indexScalarsDictionary
            );
        }

        public override IGaStorageMultivector<T> GetMultivector()
        {
            var gradesCount = 
                GradeKVectorStorageComposerDictionary.Count;

            if (gradesCount == 0)
                return ScalarProcessor.CreateStorageZeroScalar();

            if (gradesCount == 1)
                GradeKVectorStorageComposerDictionary.First().Value.GetMultivector();

            return GetGradedMultivector();
        }

        public override IGaStorageMultivector<T> GetMultivector(bool copyFlag)
        {
            return GetGradedMultivector(copyFlag);
        }

        public override IGaStorageMultivectorGraded<T> GetGradedMultivector()
        {
            var kVectorsDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, composer) in GradeKVectorStorageComposerDictionary)
                kVectorsDictionary.Add(grade, composer.IndexScalarsDictionary);

            return ScalarProcessor.CreateStorageGradedMultivector(kVectorsDictionary);
        }

        public override IGaStorageMultivectorGraded<T> GetGradedMultivector(bool copyFlag)
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

            return ScalarProcessor.CreateStorageGradedMultivector(kVectorsDictionary
            );
        }

        public override GaStorageMultivectorSparse<T> GetSparseMultivector()
        {
            var sparseComposer = 
                new GaStorageComposerMultivectorSparse<T>(ScalarProcessor);

            foreach (var (grade, composer) in GradeKVectorStorageComposerDictionary)
            {
                var indexScalarDictionary = composer.IndexScalarsDictionary;

                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    var id = GaBasisUtils.BasisBladeId(grade, index);

                    sparseComposer.AddTerm(id, scalar);
                }
            }

            return sparseComposer.GetSparseMultivector();
        }

        public override GaStorageMultivectorSparse<T> GetSparseMultivector(bool copyFlag)
        {
            return GetSparseMultivector();
        }

        public override GaStorageMultivectorSparse<T> GetTreeMultivector()
        {
            var sparseComposer = 
                new GaStorageComposerMultivectorSparse<T>(ScalarProcessor);

            foreach (var (grade, composer) in GradeKVectorStorageComposerDictionary)
            {
                var indexScalarDictionary = composer.IndexScalarsDictionary;

                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    var id = GaBasisUtils.BasisBladeId(grade, index);

                    sparseComposer.AddTerm(id, scalar);
                }
            }

            return sparseComposer.GetTreeMultivector();
        }

        public override GaStorageMultivectorSparse<T> GetTreeMultivector(bool copyFlag)
        {
            return GetTreeMultivector();
        }

        public override GaStorageMultivectorSparse<T> GetTreeMultivector(int treeDepth)
        {
            var sparseComposer = 
                new GaStorageComposerMultivectorSparse<T>(ScalarProcessor);

            foreach (var (grade, composer) in GradeKVectorStorageComposerDictionary)
            {
                var indexScalarDictionary = composer.IndexScalarsDictionary;

                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    var id = GaBasisUtils.BasisBladeId(grade, index);

                    sparseComposer.AddTerm(id, scalar);
                }
            }

            return sparseComposer.GetTreeMultivector(treeDepth);
        }

        public override GaStorageMultivectorSparse<T> GetTreeMultivector(int treeDepth, bool copyFlag)
        {
            return GetTreeMultivector(treeDepth);
        }
    }
}