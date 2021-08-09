using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Terms;
using GeometricAlgebraFulcrumLib.Structures.Even;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public sealed class GaStorageComposerMultivectorSparse<T>
        : GaStorageComposerMultivectorBase<T>
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


        internal GaStorageComposerMultivectorSparse([NotNull] IGaScalarProcessor<T> scalarProcessor)
            : base(scalarProcessor)
        {
            IdScalarsDictionary = new Dictionary<ulong, T>();
        }

        internal GaStorageComposerMultivectorSparse([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] Dictionary<ulong, T> idScalarDictionary)
            : base(scalarProcessor)
        {
            IdScalarsDictionary = idScalarDictionary;
        }


        public override bool IsEmpty()
        {
            return IdScalarsDictionary.Count == 0;
        }

        public override IGaStorageComposerMultivector<T> Clear()
        {
            IdScalarsDictionary.Clear();

            return this;
        }

        public override IGaStorageComposerMultivector<T> SetScalar(T scalar)
        {
            throw new NotImplementedException();
        }

        public bool ContainsId(ulong id)
        {
            return IdScalarsDictionary.ContainsKey(id);
        }

        public bool TryGetScalar(ulong id, out T scalar)
        {
            return IdScalarsDictionary.TryGetValue(id, out scalar);
        }


        public GaStorageComposerMultivectorSparse<T> AddKVectorTerms(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            foreach (var (index, scalar) in indexScalarPairs)
                AddTerm(grade, index, scalar);

            return this;
        }

        public GaStorageComposerMultivectorSparse<T> AddKVectorTerms(uint grade, IEnumerable<Tuple<ulong, T>> indexScalarTuples)
        {
            foreach (var (index, scalar) in indexScalarTuples)
                AddTerm(grade, index, scalar);

            return this;
        }


        public GaStorageComposerMultivectorSparse<T> SubtractKVectorTerms(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            foreach (var (index, scalar) in indexScalarPairs)
                SubtractTerm(grade, index, scalar);

            return this;
        }

        public GaStorageComposerMultivectorSparse<T> SubtractKVectorTerms(uint grade, IEnumerable<Tuple<ulong, T>> indexScalarTuples)
        {
            foreach (var (index, scalar) in indexScalarTuples)
                SubtractTerm(grade, index, scalar);

            return this;
        }


        public override IGaStorageComposerMultivector<T> RemoveZeroTermsOfGrade(uint grade, bool nearZeroFlag = false)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaStorageScalar<T> GetScalar()
        {
            return IdScalarsDictionary.TryGetValue(0, out var scalar) && !ScalarProcessor.IsZero(scalar)
                ? ScalarProcessor.CreateStorageScalar(scalar)
                : GaStorageScalar<T>.ZeroScalar;
        }

        public override GaStorageVector<T> GetVector()
        {
            var indexScalarDictionary =
                IdScalarsDictionary.Where(
                    pair => pair.Key.IsBasicPattern()
                ).ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => pair.Value
                );

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override GaStorageVector<T> GetVector(bool copyFlag)
        {
            var indexScalarDictionary =
                IdScalarsDictionary.Where(
                    pair => pair.Key.IsBasicPattern()
                ).ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => pair.Value
                );

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override GaStorageBivector<T> GetBivector()
        {
            var indexScalarDictionary =
                IdScalarsDictionary.Where(
                    pair => pair.Key.BasisBladeGrade() == 2
                ).ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => pair.Value
                );

            return GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override GaStorageBivector<T> GetBivector(bool copyFlag)
        {
            var indexScalarDictionary =
                IdScalarsDictionary.Where(
                    pair => pair.Key.BasisBladeGrade() == 2
                ).ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => pair.Value
                );

            return GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVector(uint grade)
        {
            var indexScalarDictionary =
                IdScalarsDictionary.Where(
                    pair => pair.Key.BasisBladeGrade() == grade
                ).ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => pair.Value
                );

            return GaStorageKVector<T>.Create(grade, indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVector(uint grade, bool copyFlag)
        {
            var indexScalarDictionary =
                IdScalarsDictionary.Where(
                    pair => pair.Key.BasisBladeGrade() == grade
                ).ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => pair.Value
                );

            return GaStorageKVector<T>.Create(grade, indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivector()
        {
            return GaStorageMultivectorSparse<T>.Create(IdScalarsDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivector(bool copyFlag)
        {
            return GaStorageMultivectorSparse<T>.Create(
                copyFlag
                    ? IdScalarsDictionary.CopyToDictionary()
                    : IdScalarsDictionary
            );
        }

        public override IGaStorageMultivectorGraded<T> GetGradedMultivector()
        {
            return ScalarProcessor
                .CreateStorageComposerGradedMultivector(IdScalarsDictionary)
                .RemoveZeroTerms()
                .GetGradedMultivector();
        }

        public override IGaStorageMultivectorGraded<T> GetGradedMultivector(bool copyFlag)
        {
            return ScalarProcessor
                .CreateStorageComposerGradedMultivector(IdScalarsDictionary)
                .RemoveZeroTerms()
                .GetGradedMultivector();
        }

        public override GaStorageMultivectorSparse<T> GetSparseMultivector()
        {
            return GaStorageMultivectorSparse<T>.Create(IdScalarsDictionary);
        }

        public override GaStorageMultivectorSparse<T> GetSparseMultivector(bool copyFlag)
        {
            return GaStorageMultivectorSparse<T>.Create(
                copyFlag
                    ? IdScalarsDictionary.CopyToDictionary()
                    : IdScalarsDictionary
            );
        }

        public override GaStorageMultivectorSparse<T> GetTreeMultivector()
        {
            return GaStorageMultivectorSparse<T>.Create(
                IdScalarsDictionary.CreateEvenDictionaryTree()
            );
        }

        public override GaStorageMultivectorSparse<T> GetTreeMultivector(bool copyFlag)
        {
            return GaStorageMultivectorSparse<T>.Create(
                IdScalarsDictionary.CreateEvenDictionaryTree()
            );
        }

        public override GaStorageMultivectorSparse<T> GetTreeMultivector(int treeDepth)
        {
            return GaStorageMultivectorSparse<T>.Create(
                IdScalarsDictionary.CreateEvenDictionaryTree(treeDepth)
            );
        }

        public override GaStorageMultivectorSparse<T> GetTreeMultivector(int treeDepth, bool copyFlag)
        {
            return GaStorageMultivectorSparse<T>.Create(
                IdScalarsDictionary.CreateEvenDictionaryTree(treeDepth)
            );
        }
        

        public override IGaStorageComposerMultivector<T> SetTerm(GaTerm<T> term)
        {
            this[term.BasisBlade.Id] = term.Scalar;

            return this;
        }


        public override IGaStorageComposerMultivector<T> SetTerms(IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                this[term.BasisBlade.Id] = term.Scalar;

            return this;
        }


        public override IGaStorageComposerMultivector<T> SetTermsToNegative()
        {
            foreach (var (id, scalar) in IdScalarsDictionary)
                IdScalarsDictionary[id] = ScalarProcessor.Negative(scalar);

            return this;
        }

        public override IGaStorageComposerMultivector<T> SetTermsToNegative(IEnumerable<ulong> idsList)
        {
            foreach (var id in idsList)
                if (IdScalarsDictionary.TryGetValue(id, out var scalar))
                    IdScalarsDictionary[id] = ScalarProcessor.Negative(scalar);

            return this;
        }

        public override IGaStorageComposerMultivector<T> SetTermsToNegative(IEnumerable<Tuple<uint, ulong>> indicesList)
        {
            foreach (var (grade, index) in indicesList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                if (IdScalarsDictionary.TryGetValue(id, out var scalar))
                    IdScalarsDictionary[id] = ScalarProcessor.Negative(scalar);
            }

            return this;
        }


        public override IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<ulong, T> mappingFunc)
        {
            foreach (var id in idsList)
                this[id] = mappingFunc(id);

            return this;
        }

        public override IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList,
            Func<ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in idsList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                this[id] = mappingFunc(id);
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var id in idsList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                this[id] = mappingFunc(grade, index);
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList,
            Func<uint, ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in idsList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                this[id] = mappingFunc(grade, index);
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<ulong> idsList,
            Func<ulong, T, T> mappingFunc)
        {
            foreach (var id in idsList)
            {
                var scalar = this[id];

                this[id] = mappingFunc(id, scalar);
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList,
            Func<ulong, T, T> mappingFunc)
        {
            foreach (var (grade, index) in idsList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);
                var scalar = this[id];

                this[id] = mappingFunc(id, scalar);
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<ulong> idsList,
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

        public override IGaStorageComposerMultivector<T> SetComputedTerms(
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


        public override IGaStorageComposerMultivector<T> LeftScaleTerms(T scalingFactor)
        {
            foreach (var (id, scalar) in IdScalarsDictionary)
                SetTerm(id, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public override IGaStorageComposerMultivector<T> LeftScaleTerms(IEnumerable<ulong> indexList, T scalingFactor)
        {
            foreach (var id in indexList)
                SetTerm(id, ScalarProcessor.Times(scalingFactor, this[id]));

            return this;
        }

        public override IGaStorageComposerMultivector<T> LeftScaleTerms(
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

        public override IGaStorageComposerMultivector<T> RightScaleTerms(T scalingFactor)
        {
            foreach (var (id, scalar) in IdScalarsDictionary)
                SetTerm(id, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public override IGaStorageComposerMultivector<T> RightScaleTerms(IEnumerable<ulong> indexList, T scalingFactor)
        {
            foreach (var id in indexList)
                SetTerm(id, ScalarProcessor.Times(this[id], scalingFactor));

            return this;
        }

        public override IGaStorageComposerMultivector<T> RightScaleTerms(
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

        public override IGaStorageComposerMultivector<T> AddScalar(T scalar)
        {
            throw new NotImplementedException();
        }


        public override IGaStorageComposerMultivector<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                this[term.BasisBlade.Id] = ScalarProcessor.Times(scalingFactor, term.Scalar);

            return this;
        }

        public override IGaStorageComposerMultivector<T> SetRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                this[term.BasisBlade.Id] = ScalarProcessor.Times(term.Scalar, scalingFactor);

            return this;
        }


        public override IGaStorageComposerMultivector<T> AddTerm(ulong id, T scalar)
        {
            if (IdScalarsDictionary.TryGetValue(id, out var oldValue))
            {
                IdScalarsDictionary[id] = ScalarProcessor.Add(oldValue, scalar);
                
                return this;
            }

            IdScalarsDictionary.Add(id, scalar);

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddTerm(uint grade, ulong index, T scalar)
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

        public override IGaStorageComposerMultivector<T> AddTerm(GaTerm<T> term)
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


        public override IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var id in indexList)
                AddTerm(id, mappingFunc(id));

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddComputedTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                AddTerm(id, mappingFunc(id));
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<ulong> indexList,
            Func<uint, ulong, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                AddTerm(id, mappingFunc(grade, index));
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddComputedTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                AddTerm(id, mappingFunc(grade, index));
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                var scalar = this[id];

                AddTerm(id, mappingFunc(id, scalar));
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddComputedTerms(
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

        public override IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<ulong> indexList,
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

        public override IGaStorageComposerMultivector<T> AddComputedTerms(
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


        public override IGaStorageComposerMultivector<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList) 
                AddTerm(term.BasisBlade.Id, ScalarProcessor.Times(scalingFactor, term.Scalar));

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList) 
                AddTerm(term.BasisBlade.Id, ScalarProcessor.Times(term.Scalar, scalingFactor));

            return this;
        }


        public override IGaStorageComposerMultivector<T> SubtractScalar(T scalar)
        {
            return SubtractTerm(0, scalar);
        }
        
        public override IGaStorageComposerMultivector<T> SubtractTerm(ulong id, T scalar)
        {
            if (IdScalarsDictionary.TryGetValue(id, out var oldValue))
            {
                IdScalarsDictionary[id] = ScalarProcessor.Subtract(oldValue, scalar);
                
                return this;
            }

            IdScalarsDictionary.Add(id, ScalarProcessor.Negative(scalar));

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractTerm(uint grade, ulong index, T scalar)
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
        
        public override IGaStorageComposerMultivector<T> SubtractTerm(GaTerm<T> term)
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


        public override IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var id in indexList)
                SubtractTerm(id, mappingFunc(id));

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractComputedTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                SubtractTerm(id, mappingFunc(id));
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<ulong> indexList,
            Func<uint, ulong, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                SubtractTerm(id, mappingFunc(grade, index));
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractComputedTerms(
            IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var (grade, index) in indexList)
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                SubtractTerm(id, mappingFunc(grade, index));
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var id in indexList)
            {
                var scalar = this[id];

                SubtractTerm(id, mappingFunc(id, scalar));
            }

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractComputedTerms(
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

        public override IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<ulong> indexList,
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

        public override IGaStorageComposerMultivector<T> SubtractComputedTerms(
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


        public override IGaStorageComposerMultivector<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList) 
                SubtractTerm(term.BasisBlade.Id, ScalarProcessor.Times(scalingFactor, term.Scalar));

            return this;
        }

        public override IGaStorageComposerMultivector<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList) 
                SubtractTerm(term.BasisBlade.Id, ScalarProcessor.Times(term.Scalar, scalingFactor));

            return this;
        }

        public override IGaStorageComposerMultivector<T> AddKVector(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> AddKVector(uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> AddKVector(IGaStorageKVector<T> storage)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> AddKVectors(IEnumerable<KeyValuePair<uint, Dictionary<ulong, T>>> storagesList)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> AddKVectors(IEnumerable<KeyValuePair<uint, IGaEvenDictionary<T>>> storagesList)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> AddKVectors(IEnumerable<IGaStorageKVector<T>> storagesList)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> AddLeftScaledKVector(T scalingFactor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> AddLeftScaledKVector(T scalingFactor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> AddLeftScaledKVector(T scalingFactor, GaStorageKVectorBase<T> storage)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> AddRightScaledKVector(T scalingFactor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> AddRightScaledKVector(T scalingFactor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> AddRightScaledKVector(T scalingFactor, GaStorageKVectorBase<T> storage)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SubtractKVector(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SubtractKVector(uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SubtractKVector(GaStorageKVectorBase<T> storage)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SubtractLeftScaledKVector(T scalingFactor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SubtractLeftScaledKVector(T scalingFactor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SubtractLeftScaledKVector(T scalingFactor, GaStorageKVectorBase<T> storage)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SubtractRightScaledKVector(T scalingFactor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SubtractRightScaledKVector(T scalingFactor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SubtractRightScaledKVector(T scalingFactor, GaStorageKVectorBase<T> storage)
        {
            throw new NotImplementedException();
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

        public override IGaStorageComposerMultivector<T> RemoveTerms(params ulong[] indexList)
        {
            foreach (var key in indexList)
                IdScalarsDictionary.Remove(key);

            return this;
        }

        public override IGaStorageComposerMultivector<T> RemoveZeroTerms()
        {
            var idsArray = IdScalarsDictionary
                .Where(pair => ScalarProcessor.IsZero(pair.Value))
                .Select(pair => pair.Key)
                .ToArray();

            return RemoveTerms(idsArray);
        }

        public override IGaStorageComposerMultivector<T> RemoveNearZeroTerms()
        {
            var idsArray = IdScalarsDictionary
                .Where(pair => ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => pair.Key)
                .ToArray();

            return RemoveTerms(idsArray);
        }

        public override IGaStorageComposerMultivector<T> RemoveKVector(uint grade)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SetKVector(uint grade, IReadOnlyList<T> scalarValuesList)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SetKVector(uint grade, T scalingFactor, IReadOnlyList<T> scalarValuesList)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SetKVector(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SetKVector(uint grade, T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SetKVector(IGaStorageKVector<T> kVector)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SetKVectorStorage(uint grade, Dictionary<ulong, T> indexScalarsDictionary)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SetKVector(T scalingFactor, IGaStorageKVector<T> kVector)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SetKVectors(IEnumerable<KeyValuePair<uint, Dictionary<ulong, T>>> storagesList)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SetKVectors(IEnumerable<KeyValuePair<uint, IGaEvenDictionary<T>>> storagesList)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SetKVectors(IEnumerable<IGaStorageKVector<T>> kVectorsList)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SetKVectors(IEnumerable<Tuple<T, IGaStorageKVector<T>>> scaledKVectorsList)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> AddKVector(uint grade, IReadOnlyList<T> scalarValuesList)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> AddKVector(uint grade, T scalingFactor, IReadOnlyList<T> scalarValuesList)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> AddLeftScaledKVector(uint grade, T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> AddKVector(T scalingFactor, IGaStorageKVector<T> kVector)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> AddLeftScaledKVectors(IEnumerable<Tuple<T, IGaStorageKVector<T>>> scaledKVectorsList)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SubtractKVector(uint grade, IReadOnlyList<T> scalarValuesList)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SubtractKVector(uint grade, T scalingFactor, IReadOnlyList<T> scalarValuesList)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SubtractLeftScaledKVector(uint grade, T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SubtractKVector(IGaStorageKVector<T> kVector)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SubtractKVector(T scalingFactor, IGaStorageKVector<T> kVector)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SubtractKVectors(IEnumerable<KeyValuePair<uint, IGaEvenDictionary<T>>> storagesList)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SubtractKVectors(IEnumerable<KeyValuePair<uint, Dictionary<ulong, T>>> storagesList)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SubtractKVectors(IEnumerable<IGaStorageKVector<T>> kVectorsList)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> SubtractLeftScaledKVectors(IEnumerable<Tuple<T, IGaStorageKVector<T>>> scaledKVectorsList)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> RemoveTerms(IEnumerable<ulong> idsList)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> RemoveTerms(uint grade, IEnumerable<ulong> indexList)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> RemoveTermsOfGrade(uint grade)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> RemoveTermIfZero(ulong id, bool nearZeroFlag = false)
        {
            throw new NotImplementedException();
        }

        public override IGaStorageComposerMultivector<T> RemoveTermIfZero(uint grade, ulong index, bool nearZeroFlag = false)
        {
            throw new NotImplementedException();
        }
    }
}