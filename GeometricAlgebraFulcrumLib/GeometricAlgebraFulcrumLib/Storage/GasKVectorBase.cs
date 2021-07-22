using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Terms;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Trees;
using GeometricAlgebraFulcrumLib.TextComposers;


namespace GeometricAlgebraFulcrumLib.Storage
{
    public abstract class GasKVectorBase<T> 
        : IGasKVector<T>
    {
        public Dictionary<ulong, T> IndexScalarDictionary { get; }
        
        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public uint VSpaceDimension 
            => (uint) MaxBasisBladeId.LastOneBitPosition() + 1;

        public ulong MaxBasisBladeId { get; }

        public int GradesCount 
            => 1;

        public abstract uint Grade { get; }

        public int TermsCount 
            => IndexScalarDictionary.Count;

        public T this[ulong index]
        {
            get => IndexScalarDictionary.TryGetValue(index, out var scalar) 
                ? scalar : ScalarProcessor.ZeroScalar;
            set => IndexScalarDictionary[index] = value;
        }


        protected GasKVectorBase([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] Dictionary<ulong, T> indexScalarDictionary, ulong maxBasisBladeId)
        {
            ScalarProcessor = scalarProcessor;
            IndexScalarDictionary = indexScalarDictionary;
            MaxBasisBladeId = maxBasisBladeId;
        }


        public bool ContainsTerm(ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return Grade == grade && IndexScalarDictionary.ContainsKey(index);
        }

        public bool ContainsTerm(uint grade, ulong index)
        {
            return grade == Grade && IndexScalarDictionary.ContainsKey(index);
        }

        public bool ContainsTermsOfGrade(uint grade)
        {
            return Grade == grade;
        }

        public bool ContainsTermWithIndex(ulong index)
        {
            return IndexScalarDictionary.ContainsKey(index);
        }

        public bool IsEmpty()
        {
            return IndexScalarDictionary.Count == 0;
        }

        public bool IsZero()
        {
            return IndexScalarDictionary.Count == 0 ||
                   IndexScalarDictionary.Values.All(scalar => ScalarProcessor.IsZero(scalar));
        }

        public bool IsNearZero()
        {
            return IndexScalarDictionary.Count == 0 ||
                   IndexScalarDictionary.Values.All(scalar => ScalarProcessor.IsNearZero(scalar));
        }

        public bool IsZero(bool nearZeroFlag)
        {
            return IndexScalarDictionary.Count == 0 ||
                   (nearZeroFlag && IndexScalarDictionary.Values.All(scalar => ScalarProcessor.IsNearZero(scalar))) ||
                   (!nearZeroFlag && IndexScalarDictionary.Values.All(scalar => ScalarProcessor.IsZero(scalar)));
        }

        public abstract bool IsScalar();

        public abstract bool IsVector();

        public abstract bool IsBivector();

        public bool IsKVector()
        {
            return true;
        }

        public bool IsKVector(uint grade)
        {
            return Grade == grade;
        }


        public IEnumerable<uint> GetGrades()
        {
            yield return Grade;
        }

        public ulong GetStoredGradesBitPattern()
        {
            return 1UL << (int) Grade;
        }

        public T GetTermScalarByIndex(ulong index)
        {
            return IndexScalarDictionary.TryGetValue(index, out var scalar) 
                ? scalar 
                : ScalarProcessor.ZeroScalar;
        }

        public T GetTermScalar(ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return grade == Grade && IndexScalarDictionary.TryGetValue(index, out var scalar) 
                ? scalar 
                : ScalarProcessor.ZeroScalar;
        }

        public T GetTermScalar(uint grade, ulong index)
        {
            if (grade == Grade && IndexScalarDictionary.TryGetValue(index, out var scalar))
                return scalar;

            return ScalarProcessor.ZeroScalar;
        }


        public bool TryGetTermScalarByIndex(ulong index, out T value)
        {
            if (IndexScalarDictionary.TryGetValue(index, out value))
                return true;

            value = ScalarProcessor.ZeroScalar;
            return false;
        }

        public bool TryGetTermScalar(ulong id, out T value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (Grade == grade && IndexScalarDictionary.TryGetValue(index, out value))
                return true;

            value = ScalarProcessor.ZeroScalar;
            return false;
        }

        public bool TryGetTermScalar(uint grade, ulong index, out T value)
        {
            if (grade == Grade) 
                return IndexScalarDictionary.TryGetValue(index, out value);

            value = default;
            return false;
        }


        public abstract GaTerm<T> GetTermByIndex(int index);

        public abstract GaTerm<T> GetTermByIndex(ulong index);

        public abstract GaTerm<T> GetTerm(ulong id);

        public abstract GaTerm<T> GetTerm(uint grade, ulong index);


        public abstract bool TryGetTermByIndex(int index, out GaTerm<T> term);

        public abstract bool TryGetTermByIndex(ulong index, out GaTerm<T> term);

        public abstract bool TryGetTerm(ulong id, out GaTerm<T> term);

        public abstract bool TryGetTerm(uint grade, ulong index, out GaTerm<T> term);


        public IEnumerable<IGasKVector<T>> GetKVectorStorages()
        {
            yield return this;
        }

        public IReadOnlyDictionary<uint, IGasKVector<T>> GetKVectorStoragesDictionary()
        {
            return new Dictionary<uint, IGasKVector<T>>(){{Grade, this}};
        }

        public abstract IReadOnlyDictionary<ulong, T> GetIdScalarDictionary();

        public IReadOnlyDictionary<uint, Dictionary<ulong, T>> GetGradeIndexScalarDictionary()
        {
            return new Dictionary<uint, Dictionary<ulong, T>>()
            {
                {Grade, IndexScalarDictionary}
            };
        }

        public bool TryGetKVectorStorage(uint grade, out IGasKVector<T> storage)
        {
            if (Grade == grade)
            {
                storage = this;
                return true;
            }

            storage = null;
            return false;
        }

        public bool TryGetKVectorStorageDictionary(uint grade, out IReadOnlyDictionary<ulong, T> indexScalarDictionary)
        {
            if (grade == Grade)
            {
                indexScalarDictionary = IndexScalarDictionary;
                return true;
            }

            indexScalarDictionary = null;
            return false;
        }

        public abstract IEnumerable<ulong> GetIds();

        public IEnumerable<ulong> GetIndices()
        {
            return IndexScalarDictionary.Keys;
        }
        
        public IEnumerable<Tuple<uint, ulong>> GetGradeIndexTuples()
        {
            return IndexScalarDictionary.Keys.Select(index => new Tuple<uint, ulong>(Grade, index));
        }

        public abstract IEnumerable<IGaBasisBlade> GetBasisBlades();

        public IEnumerable<T> GetScalars()
        {
            return IndexScalarDictionary.Values;
        }

        public abstract IEnumerable<KeyValuePair<ulong, T>> GetIdScalarPairs();

        public abstract IEnumerable<Tuple<ulong, T>> GetIdScalarTuples();

        public IEnumerable<KeyValuePair<ulong, T>> GetIndexScalarPairs()
        {
            return IndexScalarDictionary;
        }

        public IEnumerable<Tuple<ulong, T>> GetIndexScalarTuples()
        {
            return IndexScalarDictionary
                .Select(pair => new Tuple<ulong, T>(pair.Key, pair.Value));
        }

        public IReadOnlyDictionary<ulong, T> GetIndexScalarDictionary()
        {
            return IndexScalarDictionary;
        }

        public IEnumerable<Tuple<uint, ulong, T>> GetGradeIndexScalarTuples()
        {
            return IndexScalarDictionary
                .Select(pair => new Tuple<uint, ulong, T>(Grade, pair.Key, pair.Value));
        }

        public abstract IEnumerable<GaTerm<T>> GetTerms();

        public abstract IEnumerable<GaTerm<T>> GetNotZeroTerms();

        public abstract IEnumerable<GaTerm<T>> GetNotNearZeroTerms();

        public IEnumerable<GaTerm<T>> GetNotZeroTerms(bool nearZeroFlag)
        {
            return nearZeroFlag
                ? GetNotNearZeroTerms()
                : GetNotZeroTerms();
        }

        public abstract IEnumerable<GaTerm<T>> GetZeroTerms();

        public abstract IEnumerable<GaTerm<T>> GetNearZeroTerms();

        public IEnumerable<GaTerm<T>> GetZeroTerms(bool nearZeroFlag)
        {
            return nearZeroFlag
                ? GetNearZeroTerms()
                : GetZeroTerms();
        }


        public abstract GasKVectorBase<T> GetLeftScaledCopy(T scalingFactor);

        public abstract GasKVectorBase<T> GetRightScaledCopy(T scalingFactor);

        public abstract GasKVectorBase<T> GetComputedCopy(Func<T, T> mappingFunc);

        public abstract GasKVectorBase<T> GetComputedCopy(Func<ulong, T, T> mappingFunc);

        public abstract GasKVectorBase<T> GetComputedCopy(Func<ulong, T> mappingFunc);

        public abstract GasKVectorBase<T> GetComputedCopy(Func<uint, ulong, T, T> mappingFunc);

        public abstract GasKVectorBase<T> GetComputedCopy(Func<uint, ulong, T> mappingFunc);

        
        public IGasKVector<T> GetKVectorStorage()
        {
            return ScalarProcessor.CreateKVector(Grade,
                IndexScalarDictionary
            );
        }

        public IGasKVector<T> GetKVectorStorageCopy()
        {
            return ScalarProcessor.CreateKVector(Grade,
                IndexScalarDictionary.CopyToDictionary()
            );
        }

        public IGasKVector<T> GetKVectorStorageCopy(Func<T, T> scalarMapping)
        {
            return ScalarProcessor.CreateKVector(Grade,
                IndexScalarDictionary.CopyToDictionary(scalarMapping)
            );
        }

        public IGasGradedMultivector<T> GetGradedMultivectorCopy()
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.CopyToDictionary();

            var gradeIndexScalarDictionary = 
                new Dictionary<uint, Dictionary<ulong, T>>()
                {
                    {Grade, indexScalarDictionary}
                };

            return new GasGradedMultivector<T>(
                ScalarProcessor,
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public IGasTermsMultivector<T> GetTermsMultivectorCopy()
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(ScalarProcessor);

            composer.SetTerms(GetIdScalarPairs());

            return composer.CreateMultivectorTermsStorage();
        }

        public GasTreeMultivector<T> GetTreeMultivectorCopy()
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(ScalarProcessor);

            composer.SetTerms(GetIdScalarPairs());

            return composer.GetTreeMultivectorCopy();
        }


        public GaBinaryTree<T> GetBinaryTree(int treeDepth)
        {
            if (treeDepth < VSpaceDimension)
                throw new InvalidOperationException();

            var dict = GetIdScalarPairs()
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                );

            return new GaBinaryTree<T>(treeDepth, dict);
        }

        public IGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity)
        {
            //return GaGbtKVectorStorageStack1<T>.Create(capacity, treeDepth, this);
            //return GaGbtMultivectorStorageGradedStack1<T>.Create(capacity, treeDepth, this);
            return GaGbtMultivectorStorageUniformStack1<T>.Create(
                capacity, 
                treeDepth, 
                this
            );
        }

        public abstract IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<ulong, T, T2> idScalarMapping);

        public abstract IGasMultivector<T2> GetCopy<T2>(
            IGaScalarProcessor<T2> scalarProcessor, Func<uint, ulong, T, T2> gradeIndexScalarMapping);

        public abstract IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<T, T2> scalarMapping);


        public IGasMultivector<T> GetCompactStorage()
        {
            return this;
        }

        public IGasGradedMultivector<T> GetCompactGradedStorage()
        {
            return this;
        }


        public abstract IGasMultivector<T> GetCopy();
        
        public abstract IGasMultivector<T> GetCopy(Func<T, T> scalarMapping);

        public abstract IGasMultivector<T> GetNegative();

        public IGasMultivector<T> GetNegative(Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(Grade)
                ? GetNegative()
                : this;
        }

        public IGasMultivector<T> GetReverse()
        {
            return Grade.GradeHasNegativeReverse()
                ? GetNegative()
                : this;
        }

        public IGasMultivector<T> GetGradeInvolution()
        {
            return Grade.GradeHasNegativeGradeInvolution()
                ? GetNegative()
                : this;
        }

        public IGasMultivector<T> GetCliffordConjugate()
        {
            return Grade.GradeHasNegativeCliffordConjugate()
                ? GetNegative()
                : this;
        }

        public IGasTermsMultivector<T> ToTermsMultivector()
        {
            return IndexScalarDictionary.Count switch
            {
                0 => ScalarProcessor.CreateZeroKVector(Grade),
                1 => ScalarProcessor.CreateKVector(Grade, IndexScalarDictionary.First()),
                _ => ScalarProcessor.CreateTermsMultivector(IndexScalarDictionary.ToDictionary(
                        pair => GaBasisUtils.BasisBladeId(Grade, pair.Key),
                        pair => pair.Value
                    )
                )
            };
        }

        public IGasGradedMultivector<T> ToGradedMultivector()
        {
            return this;
        }

        public abstract IGasScalar<T> GetScalarPart();

        public abstract IGasScalar<T> GetScalarPart(Func<T, T> scalarMapping);

        public abstract IGasVector<T> GetVectorPart();

        public abstract IGasVector<T> GetVectorPart(Func<T, T> scalarMapping);

        public abstract IGasVector<T> GetVectorPart(Func<ulong, T, T> indexScalarMapping);

        public abstract IGasVector<T> GetVectorPart(Func<T, bool> scalarSelection);

        public abstract IGasVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection);

        public abstract IGasVector<T> GetVectorPart(Func<ulong, bool> indexSelection);

        public abstract IGasBivector<T> GetBivectorPart();

        public abstract IGasBivector<T> GetBivectorPart(Func<T, T> scalarMapping);

        public abstract IGasBivector<T> GetBivectorPart(Func<ulong, T, T> indexScalarMapping);
        
        public abstract IGasBivector<T> GetBivectorPart(Func<T, bool> scalarSelection);
        
        public abstract IGasBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection);
        
        public abstract IGasBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection);

        public abstract IGasKVector<T> GetKVectorPart(uint grade);

        public abstract IGasKVector<T> GetKVectorPart(uint grade, Func<T, T> scalarMapping);

        public abstract IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, T> indexScalarMapping);
        
        public abstract IGasKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection);
        
        public abstract IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection);
        
        public abstract IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection);

        public abstract IGasMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection);
        
        public abstract IGasMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection);
        
        public abstract IGasMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection);
        
        public abstract IGasMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection);
        
        public abstract IGasMultivector<T> GetMultivectorPart(
            Func<uint, ulong, T, bool> gradeIndexScalarSelection);

        public abstract Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<ulong, bool> indexSelection);

        public abstract Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection);

        public abstract Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<T, bool> scalarSelection);

        public override string ToString()
        {
            return this.GetMultivectorText();
        }
    }
}