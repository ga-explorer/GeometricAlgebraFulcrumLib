using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraLib.Frames;
using GeometricAlgebraLib.Multivectors.Terms;
using GeometricAlgebraLib.Processors.Scalars;

namespace GeometricAlgebraLib.Storage.Composers
{
    public sealed class GaMultivectorTermsStorageComposer<TScalar>
        : GaMultivectorStorageComposerBase<TScalar>
    {
        public Dictionary<ulong, TScalar> IdScalarsDictionary { get; private set; }

        public int Count 
            => IdScalarsDictionary.Count;

        public override TScalar this[ulong id]
        {
            get => IdScalarsDictionary.TryGetValue(id, out var scalar)
                ? scalar 
                : ScalarProcessor.ZeroScalar;

            set
            {
                if (IdScalarsDictionary.ContainsKey(id))
                    IdScalarsDictionary[id] = value;
                else
                    IdScalarsDictionary.Add(id, value);
            }
        }

        public override TScalar this[int grade, ulong index]
        {
            get
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);

                return IdScalarsDictionary.TryGetValue(id, out var scalar)
                    ? scalar
                    : ScalarProcessor.ZeroScalar;
            }

            set
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);

                if (IdScalarsDictionary.ContainsKey(id))
                    IdScalarsDictionary[id] = value;
                else
                    IdScalarsDictionary.Add(id, value);
            }
        }


        internal GaMultivectorTermsStorageComposer([NotNull] IGaScalarProcessor<TScalar> scalarProcessor)
            : base(scalarProcessor)
        {
            IdScalarsDictionary = new Dictionary<ulong, TScalar>();
        }

        internal GaMultivectorTermsStorageComposer([NotNull] IGaScalarProcessor<TScalar> scalarProcessor, [NotNull] Dictionary<ulong, TScalar> idScalarDictionary)
            : base(scalarProcessor)
        {
            IdScalarsDictionary = idScalarDictionary;
        }


        public override bool IsEmpty()
        {
            return IdScalarsDictionary.Count == 0;
        }

        public override GaMultivectorStorageComposerBase<TScalar> Clear()
        {
            IdScalarsDictionary.Clear();

            return this;
        }
        
        public bool ContainsId(ulong id)
        {
            return IdScalarsDictionary.ContainsKey(id);
        }

        public bool TryGetScalar(ulong id, out TScalar scalar)
        {
            return IdScalarsDictionary.TryGetValue(id, out scalar);
        }


        public GaMultivectorTermsStorageComposer<TScalar> AddKVectorTerms(int grade, IEnumerable<KeyValuePair<ulong, TScalar>> indexScalarPairs)
        {
            foreach (var (index, scalar) in indexScalarPairs)
                AddTerm(grade, index, scalar);

            return this;
        }

        public GaMultivectorTermsStorageComposer<TScalar> AddKVectorTerms(int grade, IEnumerable<Tuple<ulong, TScalar>> indexScalarTuples)
        {
            foreach (var (index, scalar) in indexScalarTuples)
                AddTerm(grade, index, scalar);

            return this;
        }


        public GaMultivectorTermsStorageComposer<TScalar> SubtractKVectorTerms(int grade, IEnumerable<KeyValuePair<ulong, TScalar>> indexScalarPairs)
        {
            foreach (var (index, scalar) in indexScalarPairs)
                SubtractTerm(grade, index, scalar);

            return this;
        }

        public GaMultivectorTermsStorageComposer<TScalar> SubtractKVectorTerms(int grade, IEnumerable<Tuple<ulong, TScalar>> indexScalarTuples)
        {
            foreach (var (index, scalar) in indexScalarTuples)
                SubtractTerm(grade, index, scalar);

            return this;
        }


        public override IGaMultivectorStorage<TScalar> GetCompactStorage()
        {
            var termsCount = IdScalarsDictionary.Count;

            if (termsCount == 0)
                return GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);

            if (termsCount > 1) 
                return CreateMultivectorTermsStorage();

            var (id, scalar) = IdScalarsDictionary.First();

            return id == 0UL
                ? GaScalarTermStorage<TScalar>.Create(ScalarProcessor, scalar)
                : GaKVectorTermStorage<TScalar>.Create(ScalarProcessor, id, scalar);
        }

        public override IGaMultivectorGradedStorage<TScalar> GetCompactGradedStorage()
        {
            var composer = new GaMultivectorGradedStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerms(IdScalarsDictionary);

            return composer.GetCompactGradedStorage();
        }

        public override GaMultivectorStorageBase<TScalar> GetMultivectorStorage()
        {
            return CreateMultivectorTermsStorage();
        }

        public GaMultivectorTermsStorage<TScalar> CreateMultivectorTermsStorage()
        {
            return GaMultivectorTermsStorage<TScalar>.Create(
                ScalarProcessor, 
                IdScalarsDictionary
            );
        }


        public override IGaMultivectorStorage<TScalar> GetStorageCopy()
        {
            return GetMultivectorTermsStorageCopy();
        }

        public override IGaMultivectorStorage<TScalar> GetStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            return GaMultivectorTermsStorage<TScalar>.Create(
                ScalarProcessor, 
                IdScalarsDictionary.CopyToDictionary(scalarMapping)
            );
        }

        public override GaMultivectorGradedStorage<TScalar> GetMultivectorGradedStorageCopy()
        {
            var composer = new GaMultivectorGradedStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerms(IdScalarsDictionary);

            return composer.CreateMultivectorGradedStorage();
        }

        public override GaMultivectorTermsStorage<TScalar> GetMultivectorTermsStorageCopy()
        {
            return GaMultivectorTermsStorage<TScalar>.Create(
                ScalarProcessor, 
                IdScalarsDictionary.CopyToDictionary()
            );
        }

        public override GaMultivectorTreeStorage<TScalar> GetMultivectorTreeStorageCopy()
        {
            return GaMultivectorTreeStorage<TScalar>.Create(
                ScalarProcessor, 
                IdScalarsDictionary.CopyToDictionary()
            );
        }

        public override GaScalarTermStorage<TScalar> GetScalarStorage()
        {
            return GaScalarTermStorage<TScalar>.Create(ScalarProcessor, this[0]);
        }

        public override GaVectorStorage<TScalar> GetVectorStorageCopy()
        {
            var indexScalarsDictionary =
                IdScalarsDictionary
                    .Where(pair => pair.Key.BasisBladeGrade() == 1)
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(),
                        pair => pair.Value
                    );

            return GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarsDictionary);
        }

        public override GaBivectorStorage<TScalar> GetBivectorStorageCopy(int grade)
        {
            var indexScalarsDictionary =
                IdScalarsDictionary
                    .Where(pair => pair.Key.BasisBladeGrade() == 2)
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(),
                        pair => pair.Value
                    );

            return GaBivectorStorage<TScalar>.Create(ScalarProcessor, indexScalarsDictionary);
        }

        public override GaKVectorStorage<TScalar> GetKVectorStorageCopy(int grade)
        {
            var indexScalarsDictionary =
                IdScalarsDictionary
                    .Where(pair => pair.Key.BasisBladeGrade() == grade)
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(),
                        pair => pair.Value
                    );

            return GaKVectorStorage<TScalar>.Create(
                ScalarProcessor, 
                grade, 
                indexScalarsDictionary
            );
        }


        public override GaMultivectorStorageComposerBase<TScalar> SetTerm(GaTerm<TScalar> term)
        {
            this[term.BasisBlade.Id] = term.Scalar;

            return this;
        }


        public override GaMultivectorStorageComposerBase<TScalar> SetTerms(IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList)
                this[term.BasisBlade.Id] = term.Scalar;

            return this;
        }


        public override GaMultivectorStorageComposerBase<TScalar> SetTermsToNegative()
        {
            foreach (var (id, scalar) in IdScalarsDictionary)
                IdScalarsDictionary[id] = ScalarProcessor.Negative(scalar);

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SetTermsToNegative(IEnumerable<ulong> idsList)
        {
            foreach (var id in idsList)
                if (IdScalarsDictionary.TryGetValue(id, out var scalar))
                    IdScalarsDictionary[id] = ScalarProcessor.Negative(scalar);

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SetTermsToNegative(IEnumerable<Tuple<int, ulong>> indicesList)
        {
            foreach (var (grade, index) in indicesList)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);

                if (IdScalarsDictionary.TryGetValue(id, out var scalar))
                    IdScalarsDictionary[id] = ScalarProcessor.Negative(scalar);
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<ulong> idsList, Func<ulong, TScalar> mappingFunc)
        {
            foreach (var id in idsList)
                this[id] = mappingFunc(id);

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<Tuple<int, ulong>> idsList, Func<ulong, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in idsList)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);

                this[id] = mappingFunc(id);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<ulong> idsList, Func<int, ulong, TScalar> mappingFunc)
        {
            foreach (var id in idsList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                this[id] = mappingFunc(grade, index);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<Tuple<int, ulong>> idsList, Func<int, ulong, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in idsList)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);

                this[id] = mappingFunc(grade, index);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<ulong> idsList, Func<ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var id in idsList)
            {
                var scalar = this[id];

                this[id] = mappingFunc(id, scalar);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<Tuple<int, ulong>> idsList, Func<ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in idsList)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);
                var scalar = this[id];

                this[id] = mappingFunc(id, scalar);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<ulong> idsList, Func<int, ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var id in idsList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[id];

                this[id] = mappingFunc(grade, index, scalar);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<Tuple<int, ulong>> idsList, Func<int, ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in idsList)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);
                var scalar = this[id];

                this[id] = mappingFunc(grade, index, scalar);
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<TScalar> LeftScaleTerms(TScalar scalingFactor)
        {
            foreach (var (id, scalar) in IdScalarsDictionary)
                SetTerm(id, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> LeftScaleTerms(IEnumerable<ulong> indexList, TScalar scalingFactor)
        {
            foreach (var id in indexList)
                SetTerm(id, ScalarProcessor.Times(scalingFactor, this[id]));

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> LeftScaleTerms(IEnumerable<Tuple<int, ulong>> indexList, TScalar scalingFactor)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);
                var scalar = this[id];

                SetTerm(id, ScalarProcessor.Times(scalingFactor, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> RightScaleTerms(TScalar scalingFactor)
        {
            foreach (var (id, scalar) in IdScalarsDictionary)
                SetTerm(id, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> RightScaleTerms(IEnumerable<ulong> indexList, TScalar scalingFactor)
        {
            foreach (var id in indexList)
                SetTerm(id, ScalarProcessor.Times(this[id], scalingFactor));

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> RightScaleTerms(IEnumerable<Tuple<int, ulong>> indexList, TScalar scalingFactor)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);
                var scalar = this[id];

                SetTerm(id, ScalarProcessor.Times(scalar, scalingFactor));
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<TScalar> SetLeftScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList)
                this[term.BasisBlade.Id] = ScalarProcessor.Times(scalingFactor, term.Scalar);

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SetRightScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList)
                this[term.BasisBlade.Id] = ScalarProcessor.Times(term.Scalar, scalingFactor);

            return this;
        }


        public override GaMultivectorStorageComposerBase<TScalar> AddTerm(ulong id, TScalar scalar)
        {
            if (IdScalarsDictionary.TryGetValue(id, out var oldValue))
            {
                IdScalarsDictionary[id] = ScalarProcessor.Add(oldValue, scalar);
                
                return this;
            }

            IdScalarsDictionary.Add(id, scalar);

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> AddTerm(int grade, ulong index, TScalar scalar)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            if (IdScalarsDictionary.TryGetValue(id, out var oldValue))
            {
                IdScalarsDictionary[id] = ScalarProcessor.Add(oldValue, scalar);
                
                return this;
            }

            IdScalarsDictionary.Add(id, scalar);

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> AddTerm(GaTerm<TScalar> term)
        {
            var id = term.BasisBlade.Id;
            var scalar = term.Scalar;

            if (IdScalarsDictionary.TryGetValue(id, out var oldValue))
            {
                IdScalarsDictionary[id] = ScalarProcessor.Add(oldValue, scalar);
                
                return this;
            }

            IdScalarsDictionary.Add(id, scalar);

            return this;
        }


        public override GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, TScalar> mappingFunc)
        {
            foreach (var id in indexList)
                AddTerm(id, mappingFunc(id));

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<ulong, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);

                AddTerm(id, mappingFunc(id));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<ulong> indexList, Func<int, ulong, TScalar> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                AddTerm(id, mappingFunc(grade, index));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<int, ulong, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);

                AddTerm(id, mappingFunc(grade, index));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var id in indexList)
            {
                var scalar = this[id];

                AddTerm(id, mappingFunc(id, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);
                var scalar = this[id];

                AddTerm(id, mappingFunc(id, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<ulong> indexList, Func<int, ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[id];

                AddTerm(id, mappingFunc(grade, index, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<int, ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);
                var scalar = this[id];

                AddTerm(id, mappingFunc(grade, index, scalar));
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<TScalar> AddLeftScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList) 
                AddTerm(term.BasisBlade.Id, ScalarProcessor.Times(scalingFactor, term.Scalar));

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> AddRightScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList) 
                AddTerm(term.BasisBlade.Id, ScalarProcessor.Times(term.Scalar, scalingFactor));

            return this;
        }


        public override GaMultivectorStorageComposerBase<TScalar> SubtractTerm(ulong id, TScalar scalar)
        {
            if (IdScalarsDictionary.TryGetValue(id, out var oldValue))
            {
                IdScalarsDictionary[id] = ScalarProcessor.Subtract(oldValue, scalar);
                
                return this;
            }

            IdScalarsDictionary.Add(id, ScalarProcessor.Negative(scalar));

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SubtractTerm(int grade, ulong index, TScalar scalar)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            if (IdScalarsDictionary.TryGetValue(id, out var oldValue))
            {
                IdScalarsDictionary[id] = ScalarProcessor.Subtract(oldValue, scalar);
                
                return this;
            }

            IdScalarsDictionary.Add(id, ScalarProcessor.Negative(scalar));

            return this;
        }
        
        public override GaMultivectorStorageComposerBase<TScalar> SubtractTerm(GaTerm<TScalar> term)
        {
            var id = term.BasisBlade.Id;
            var scalar = term.Scalar;

            if (IdScalarsDictionary.TryGetValue(id, out var oldValue))
            {
                IdScalarsDictionary[id] = ScalarProcessor.Subtract(oldValue, scalar);
                
                return this;
            }

            IdScalarsDictionary.Add(id, ScalarProcessor.Negative(scalar));

            return this;
        }


        public override GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, TScalar> mappingFunc)
        {
            foreach (var id in indexList)
                SubtractTerm(id, mappingFunc(id));

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<ulong, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);

                SubtractTerm(id, mappingFunc(id));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<int, ulong, TScalar> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                SubtractTerm(id, mappingFunc(grade, index));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<int, ulong, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);

                SubtractTerm(id, mappingFunc(grade, index));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var id in indexList)
            {
                var scalar = this[id];

                SubtractTerm(id, mappingFunc(id, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);
                var scalar = this[id];

                SubtractTerm(id, mappingFunc(id, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<int, ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[id];

                SubtractTerm(id, mappingFunc(grade, index, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<int, ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);
                var scalar = this[id];

                SubtractTerm(id, mappingFunc(grade, index, scalar));
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<TScalar> SubtractLeftScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList) 
                SubtractTerm(term.BasisBlade.Id, ScalarProcessor.Times(scalingFactor, term.Scalar));

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> SubtractRightScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList) 
                SubtractTerm(term.BasisBlade.Id, ScalarProcessor.Times(term.Scalar, scalingFactor));

            return this;
        }


        public override bool RemoveTerm(ulong id)
        {
            return IdScalarsDictionary.Remove(id);
        }

        public override bool RemoveTerm(int grade, ulong index)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            return IdScalarsDictionary.Remove(id);
        }

        public override GaMultivectorStorageComposerBase<TScalar> RemoveTerms(params ulong[] indexList)
        {
            foreach (var key in indexList)
                IdScalarsDictionary.Remove(key);

            return this;
        }

        public override GaMultivectorStorageComposerBase<TScalar> RemoveZeroTerms()
        {
            var idsArray = IdScalarsDictionary
                .Where(pair => ScalarProcessor.IsZero(pair.Value))
                .Select(pair => pair.Key)
                .ToArray();

            return RemoveTerms(idsArray);
        }

        public override GaMultivectorStorageComposerBase<TScalar> RemoveNearZeroTerms()
        {
            var idsArray = IdScalarsDictionary
                .Where(pair => ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => pair.Key)
                .ToArray();

            return RemoveTerms(idsArray);
        }
    }
}