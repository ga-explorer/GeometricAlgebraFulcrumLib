using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Terms;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public sealed class GaMultivectorTermsStorageComposer<T>
        : GaMultivectorStorageComposerBase<T>
    {
        public Dictionary<ulong, T> IdScalarsDictionary { get; private set; }

        public int Count 
            => IdScalarsDictionary.Count;

        public override T this[ulong id]
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

        public override T this[uint grade, ulong index]
        {
            get
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                return IdScalarsDictionary.TryGetValue(id, out var scalar)
                    ? scalar
                    : ScalarProcessor.ZeroScalar;
            }

            set
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                if (IdScalarsDictionary.ContainsKey(id))
                    IdScalarsDictionary[id] = value;
                else
                    IdScalarsDictionary.Add(id, value);
            }
        }


        internal GaMultivectorTermsStorageComposer([NotNull] IGaScalarProcessor<T> scalarProcessor)
            : base(scalarProcessor)
        {
            IdScalarsDictionary = new Dictionary<ulong, T>();
        }

        internal GaMultivectorTermsStorageComposer([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] Dictionary<ulong, T> idScalarDictionary)
            : base(scalarProcessor)
        {
            IdScalarsDictionary = idScalarDictionary;
        }


        public override bool IsEmpty()
        {
            return IdScalarsDictionary.Count == 0;
        }

        public override GaMultivectorStorageComposerBase<T> Clear()
        {
            IdScalarsDictionary.Clear();

            return this;
        }
        
        public bool ContainsId(ulong id)
        {
            return IdScalarsDictionary.ContainsKey(id);
        }

        public bool TryGetScalar(ulong id, out T scalar)
        {
            return IdScalarsDictionary.TryGetValue(id, out scalar);
        }


        public GaMultivectorTermsStorageComposer<T> AddKVectorTerms(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            foreach (var (index, scalar) in indexScalarPairs)
                AddTerm(grade, index, scalar);

            return this;
        }

        public GaMultivectorTermsStorageComposer<T> AddKVectorTerms(uint grade, IEnumerable<Tuple<ulong, T>> indexScalarTuples)
        {
            foreach (var (index, scalar) in indexScalarTuples)
                AddTerm(grade, index, scalar);

            return this;
        }


        public GaMultivectorTermsStorageComposer<T> SubtractKVectorTerms(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            foreach (var (index, scalar) in indexScalarPairs)
                SubtractTerm(grade, index, scalar);

            return this;
        }

        public GaMultivectorTermsStorageComposer<T> SubtractKVectorTerms(uint grade, IEnumerable<Tuple<ulong, T>> indexScalarTuples)
        {
            foreach (var (index, scalar) in indexScalarTuples)
                SubtractTerm(grade, index, scalar);

            return this;
        }


        public override IGasMultivector<T> GetCompactMultivector()
        {
            return GetCompactTermsStorage();
        }

        public override IGasTermsMultivector<T> GetCompactTermsStorage()
        {
            var termsCount = 
                IdScalarsDictionary.Count;

            if (termsCount == 0)
                return ScalarProcessor.CreateZeroScalar();

            if (termsCount > 1) 
                return CreateMultivectorTermsStorage();

            var (id, scalar) = 
                IdScalarsDictionary.First();

            return id == 0UL
                ? ScalarProcessor.CreateScalar(scalar)
                : ScalarProcessor.CreateKVector(id, scalar);
        }

        public override IGasGradedMultivector<T> GetCompactGradedMultivector()
        {
            var composer = new GaMultivectorGradedStorageComposer<T>(ScalarProcessor);

            composer.SetTerms(IdScalarsDictionary);

            return composer.GetCompactGradedMultivector();
        }

        public override IGasMultivector<T> GetMultivectorStorage()
        {
            return CreateMultivectorTermsStorage();
        }

        public IGasTermsMultivector<T> CreateMultivectorTermsStorage()
        {
            return ScalarProcessor.CreateTermsMultivector(IdScalarsDictionary);
        }


        public override IGasMultivector<T> GetMultivectorCopy()
        {
            return GetTermsMultivectorCopy();
        }

        public override IGasMultivector<T> GetMultivectorCopy(Func<T, T> scalarMapping)
        {
            return ScalarProcessor.CreateTermsMultivector(IdScalarsDictionary.CopyToDictionary(scalarMapping)
            );
        }

        public override IGasGradedMultivector<T> GetGradedMultivectorCopy()
        {
            var composer = new GaMultivectorGradedStorageComposer<T>(ScalarProcessor);

            composer.SetTerms(IdScalarsDictionary);

            return composer.CreateMultivectorGradedStorage();
        }

        public override IGasTermsMultivector<T> GetTermsMultivectorCopy()
        {
            return ScalarProcessor.CreateTermsMultivector(IdScalarsDictionary.CopyToDictionary()
            );
        }

        public override GasTreeMultivector<T> GetTreeMultivectorCopy()
        {
            return GaStorageFactory.CreateTreeMultivector(
                ScalarProcessor, 
                IdScalarsDictionary.CopyToDictionary()
            );
        }

        public override IGasScalar<T> GetScalarStorage()
        {
            return ScalarProcessor.CreateScalar(this[0]);
        }

        public override IGasVector<T> GetVectorStorageCopy()
        {
            var indexScalarsDictionary =
                IdScalarsDictionary
                    .Where(pair => pair.Key.BasisBladeGrade() == 1)
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(),
                        pair => pair.Value
                    );

            return ScalarProcessor.CreateVector(indexScalarsDictionary);
        }

        public override IGasBivector<T> GetBivectorStorageCopy(uint grade)
        {
            var indexScalarsDictionary =
                IdScalarsDictionary
                    .Where(pair => pair.Key.BasisBladeGrade() == 2)
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(),
                        pair => pair.Value
                    );

            return ScalarProcessor.CreateBivector(indexScalarsDictionary);
        }

        public override IGasKVector<T> GetKVectorStorageCopy(uint grade)
        {
            var indexScalarsDictionary =
                IdScalarsDictionary
                    .Where(pair => pair.Key.BasisBladeGrade() == grade)
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(),
                        pair => pair.Value
                    );

            return ScalarProcessor.CreateKVector(grade, 
                indexScalarsDictionary
            );
        }


        public override GaMultivectorStorageComposerBase<T> SetTerm(GaTerm<T> term)
        {
            this[term.BasisBlade.Id] = term.Scalar;

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> SetTerms(IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                this[term.BasisBlade.Id] = term.Scalar;

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> SetTermsToNegative()
        {
            foreach (var (id, scalar) in IdScalarsDictionary)
                IdScalarsDictionary[id] = ScalarProcessor.Negative(scalar);

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SetTermsToNegative(IEnumerable<ulong> idsList)
        {
            foreach (var id in idsList)
                if (IdScalarsDictionary.TryGetValue(id, out var scalar))
                    IdScalarsDictionary[id] = ScalarProcessor.Negative(scalar);

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SetTermsToNegative(IEnumerable<Tuple<uint, ulong>> indicesList)
        {
            foreach (var (grade, index) in indicesList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                if (IdScalarsDictionary.TryGetValue(id, out var scalar))
                    IdScalarsDictionary[id] = ScalarProcessor.Negative(scalar);
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<ulong, T> mappingFunc)
        {
            foreach (var id in idsList)
                this[id] = mappingFunc(id);

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList, Func<ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in idsList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                this[id] = mappingFunc(id);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var id in idsList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                this[id] = mappingFunc(grade, index);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SetComputedTerms(
            IEnumerable<Tuple<uint, ulong>> idsList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in idsList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                this[id] = mappingFunc(grade, index);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var id in idsList)
            {
                var scalar = this[id];

                this[id] = mappingFunc(id, scalar);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SetComputedTerms(
            IEnumerable<Tuple<uint, ulong>> idsList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var (grade, index) in idsList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var scalar = this[id];

                this[id] = mappingFunc(id, scalar);
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

                this[id] = mappingFunc(grade, index, scalar);
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SetComputedTerms(
            IEnumerable<Tuple<uint, ulong>> idsList, Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var (grade, index) in idsList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var scalar = this[id];

                this[id] = mappingFunc(grade, index, scalar);
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> LeftScaleTerms(T scalingFactor)
        {
            foreach (var (id, scalar) in IdScalarsDictionary)
                SetTerm(id, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> LeftScaleTerms(IEnumerable<ulong> indexList, T scalingFactor)
        {
            foreach (var id in indexList)
                SetTerm(id, ScalarProcessor.Times(scalingFactor, this[id]));

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> LeftScaleTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, T scalingFactor)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var scalar = this[id];

                SetTerm(id, ScalarProcessor.Times(scalingFactor, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> RightScaleTerms(T scalingFactor)
        {
            foreach (var (id, scalar) in IdScalarsDictionary)
                SetTerm(id, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> RightScaleTerms(IEnumerable<ulong> indexList, T scalingFactor)
        {
            foreach (var id in indexList)
                SetTerm(id, ScalarProcessor.Times(this[id], scalingFactor));

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> RightScaleTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, T scalingFactor)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var scalar = this[id];

                SetTerm(id, ScalarProcessor.Times(scalar, scalingFactor));
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                this[term.BasisBlade.Id] = ScalarProcessor.Times(scalingFactor, term.Scalar);

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SetRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                this[term.BasisBlade.Id] = ScalarProcessor.Times(term.Scalar, scalingFactor);

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> AddTerm(ulong id, T scalar)
        {
            if (IdScalarsDictionary.TryGetValue(id, out var oldValue))
            {
                IdScalarsDictionary[id] = ScalarProcessor.Add(oldValue, scalar);
                
                return this;
            }

            IdScalarsDictionary.Add(id, scalar);

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> AddTerm(uint grade, ulong index, T scalar)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            if (IdScalarsDictionary.TryGetValue(id, out var oldValue))
            {
                IdScalarsDictionary[id] = ScalarProcessor.Add(oldValue, scalar);
                
                return this;
            }

            IdScalarsDictionary.Add(id, scalar);

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> AddTerm(GaTerm<T> term)
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


        public override GaMultivectorStorageComposerBase<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var id in indexList)
                AddTerm(id, mappingFunc(id));

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> AddComputedTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                AddTerm(id, mappingFunc(id));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> AddComputedTerms(IEnumerable<ulong> indexList,
            Func<uint, ulong, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                AddTerm(id, mappingFunc(grade, index));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> AddComputedTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                AddTerm(id, mappingFunc(grade, index));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                var scalar = this[id];

                AddTerm(id, mappingFunc(id, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> AddComputedTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var scalar = this[id];

                AddTerm(id, mappingFunc(id, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> AddComputedTerms(IEnumerable<ulong> indexList,
            Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[id];

                AddTerm(id, mappingFunc(grade, index, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> AddComputedTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var scalar = this[id];

                AddTerm(id, mappingFunc(grade, index, scalar));
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList) 
                AddTerm(term.BasisBlade.Id, ScalarProcessor.Times(scalingFactor, term.Scalar));

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> AddRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList) 
                AddTerm(term.BasisBlade.Id, ScalarProcessor.Times(term.Scalar, scalingFactor));

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> SubtractTerm(ulong id, T scalar)
        {
            if (IdScalarsDictionary.TryGetValue(id, out var oldValue))
            {
                IdScalarsDictionary[id] = ScalarProcessor.Subtract(oldValue, scalar);
                
                return this;
            }

            IdScalarsDictionary.Add(id, ScalarProcessor.Negative(scalar));

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SubtractTerm(uint grade, ulong index, T scalar)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            if (IdScalarsDictionary.TryGetValue(id, out var oldValue))
            {
                IdScalarsDictionary[id] = ScalarProcessor.Subtract(oldValue, scalar);
                
                return this;
            }

            IdScalarsDictionary.Add(id, ScalarProcessor.Negative(scalar));

            return this;
        }
        
        public override GaMultivectorStorageComposerBase<T> SubtractTerm(GaTerm<T> term)
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


        public override GaMultivectorStorageComposerBase<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var id in indexList)
                SubtractTerm(id, mappingFunc(id));

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SubtractComputedTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                SubtractTerm(id, mappingFunc(id));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SubtractComputedTerms(IEnumerable<ulong> indexList,
            Func<uint, ulong, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                SubtractTerm(id, mappingFunc(grade, index));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SubtractComputedTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                SubtractTerm(id, mappingFunc(grade, index));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                var scalar = this[id];

                SubtractTerm(id, mappingFunc(id, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SubtractComputedTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var scalar = this[id];

                SubtractTerm(id, mappingFunc(id, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SubtractComputedTerms(IEnumerable<ulong> indexList,
            Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                var scalar = this[id];

                SubtractTerm(id, mappingFunc(grade, index, scalar));
            }

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SubtractComputedTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var scalar = this[id];

                SubtractTerm(id, mappingFunc(grade, index, scalar));
            }

            return this;
        }


        public override GaMultivectorStorageComposerBase<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList) 
                SubtractTerm(term.BasisBlade.Id, ScalarProcessor.Times(scalingFactor, term.Scalar));

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList) 
                SubtractTerm(term.BasisBlade.Id, ScalarProcessor.Times(term.Scalar, scalingFactor));

            return this;
        }


        public override bool RemoveTerm(ulong id)
        {
            return IdScalarsDictionary.Remove(id);
        }

        public override bool RemoveTerm(uint grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            return IdScalarsDictionary.Remove(id);
        }

        public override GaMultivectorStorageComposerBase<T> RemoveTerms(params ulong[] indexList)
        {
            foreach (var key in indexList)
                IdScalarsDictionary.Remove(key);

            return this;
        }

        public override GaMultivectorStorageComposerBase<T> RemoveZeroTerms()
        {
            var idsArray = IdScalarsDictionary
                .Where(pair => ScalarProcessor.IsZero(pair.Value))
                .Select(pair => pair.Key)
                .ToArray();

            return RemoveTerms(idsArray);
        }

        public override GaMultivectorStorageComposerBase<T> RemoveNearZeroTerms()
        {
            var idsArray = IdScalarsDictionary
                .Where(pair => ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => pair.Key)
                .ToArray();

            return RemoveTerms(idsArray);
        }
    }
}