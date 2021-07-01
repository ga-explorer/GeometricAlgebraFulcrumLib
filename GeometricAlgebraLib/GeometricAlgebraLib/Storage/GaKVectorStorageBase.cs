using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib;
using DataStructuresLib.Extensions;
using GeometricAlgebraLib.Multivectors.Basis;
using GeometricAlgebraLib.Multivectors.Terms;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage.Composers;
using GeometricAlgebraLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraLib.Storage.GuidedBinaryTraversal.Products;
using GeometricAlgebraLib.Storage.Trees;
using GaBasisUtils = GeometricAlgebraLib.Multivectors.Basis.GaBasisUtils;

namespace GeometricAlgebraLib.Storage
{
    public abstract class GaKVectorStorageBase<TScalar> 
        : IGaKVectorStorage<TScalar>
    {
        public Dictionary<ulong, TScalar> IndexScalarDictionary { get; }
        
        public IGaScalarProcessor<TScalar> ScalarProcessor { get; }

        public int VSpaceDimension 
            => MaxBasisBladeId.LastOneBitPosition() + 1;

        public ulong MaxBasisBladeId { get; }

        public int GradesCount 
            => 1;

        public abstract int Grade { get; }

        public int TermsCount 
            => IndexScalarDictionary.Count;

        public TScalar this[ulong index]
        {
            get => IndexScalarDictionary.TryGetValue(index, out var scalar) 
                ? scalar : ScalarProcessor.ZeroScalar;
            set => IndexScalarDictionary[index] = value;
        }


        protected GaKVectorStorageBase([NotNull] IGaScalarProcessor<TScalar> scalarProcessor, [NotNull] Dictionary<ulong, TScalar> indexScalarDictionary, ulong maxBasisBladeId)
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

        public bool ContainsTerm(int grade, ulong index)
        {
            return grade == Grade && IndexScalarDictionary.ContainsKey(index);
        }

        public bool ContainsTermsOfGrade(int grade)
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

        public bool IsKVector(int grade)
        {
            return Grade == grade;
        }


        public IEnumerable<int> GetGrades()
        {
            yield return Grade;
        }

        public ulong GetStoredGradesBitPattern()
        {
            return 1UL << Grade;
        }

        public TScalar GetTermScalarByIndex(ulong index)
        {
            return IndexScalarDictionary.TryGetValue(index, out var scalar) 
                ? scalar 
                : ScalarProcessor.ZeroScalar;
        }

        public TScalar GetTermScalar(ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return grade == Grade && IndexScalarDictionary.TryGetValue(index, out var scalar) 
                ? scalar 
                : ScalarProcessor.ZeroScalar;
        }

        public TScalar GetTermScalar(int grade, ulong index)
        {
            if (grade == Grade && IndexScalarDictionary.TryGetValue(index, out var scalar))
                return scalar;

            return ScalarProcessor.ZeroScalar;
        }


        public bool TryGetTermScalarByIndex(ulong index, out TScalar value)
        {
            if (IndexScalarDictionary.TryGetValue(index, out value))
                return true;

            value = ScalarProcessor.ZeroScalar;
            return false;
        }

        public bool TryGetTermScalar(ulong id, out TScalar value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (Grade == grade && IndexScalarDictionary.TryGetValue(index, out value))
                return true;

            value = ScalarProcessor.ZeroScalar;
            return false;
        }

        public bool TryGetTermScalar(int grade, ulong index, out TScalar value)
        {
            if (grade == Grade) 
                return IndexScalarDictionary.TryGetValue(index, out value);

            value = default;
            return false;
        }


        public abstract GaTerm<TScalar> GetTermByIndex(int index);

        public abstract GaTerm<TScalar> GetTermByIndex(ulong index);

        public abstract GaTerm<TScalar> GetTerm(ulong id);

        public abstract GaTerm<TScalar> GetTerm(int grade, ulong index);


        public abstract bool TryGetTermByIndex(int index, out GaTerm<TScalar> term);

        public abstract bool TryGetTermByIndex(ulong index, out GaTerm<TScalar> term);

        public abstract bool TryGetTerm(ulong id, out GaTerm<TScalar> term);

        public abstract bool TryGetTerm(int grade, ulong index, out GaTerm<TScalar> term);


        public IEnumerable<IGaKVectorStorage<TScalar>> GetKVectorStorages()
        {
            yield return this;
        }

        public IReadOnlyDictionary<int, IGaKVectorStorage<TScalar>> GetKVectorStoragesDictionary()
        {
            return new Dictionary<int, IGaKVectorStorage<TScalar>>(){{Grade, this}};
        }

        public abstract IReadOnlyDictionary<ulong, TScalar> GetIdScalarDictionary();

        public IReadOnlyDictionary<int, Dictionary<ulong, TScalar>> GetGradeIndexScalarDictionary()
        {
            return new Dictionary<int, Dictionary<ulong, TScalar>>()
            {
                {Grade, IndexScalarDictionary}
            };
        }

        public bool TryGetKVectorStorage(int grade, out IGaKVectorStorage<TScalar> storage)
        {
            if (Grade == grade)
            {
                storage = this;
                return true;
            }

            storage = null;
            return false;
        }

        public bool TryGetKVectorStorageDictionary(int grade, out IReadOnlyDictionary<ulong, TScalar> indexScalarDictionary)
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
        
        public IEnumerable<Tuple<int, ulong>> GetGradeIndexTuples()
        {
            return IndexScalarDictionary.Keys.Select(index => new Tuple<int, ulong>(Grade, index));
        }

        public abstract IEnumerable<IGaBasisBlade> GetBasisBlades();

        public IEnumerable<TScalar> GetScalars()
        {
            return IndexScalarDictionary.Values;
        }

        public abstract IEnumerable<KeyValuePair<ulong, TScalar>> GetIdScalarPairs();

        public abstract IEnumerable<Tuple<ulong, TScalar>> GetIdScalarTuples();

        public IEnumerable<KeyValuePair<ulong, TScalar>> GetIndexScalarPairs()
        {
            return IndexScalarDictionary;
        }

        public IEnumerable<Tuple<ulong, TScalar>> GetIndexScalarTuples()
        {
            return IndexScalarDictionary
                .Select(pair => new Tuple<ulong, TScalar>(pair.Key, pair.Value));
        }

        public IReadOnlyDictionary<ulong, TScalar> GetIndexScalarDictionary()
        {
            return IndexScalarDictionary;
        }

        public IEnumerable<Tuple<int, ulong, TScalar>> GetGradeIndexScalarTuples()
        {
            return IndexScalarDictionary
                .Select(pair => new Tuple<int, ulong, TScalar>(Grade, pair.Key, pair.Value));
        }

        public abstract IEnumerable<GaTerm<TScalar>> GetTerms();

        public abstract IEnumerable<GaTerm<TScalar>> GetNotZeroTerms();

        public abstract IEnumerable<GaTerm<TScalar>> GetNotNearZeroTerms();

        public IEnumerable<GaTerm<TScalar>> GetNotZeroTerms(bool nearZeroFlag)
        {
            return nearZeroFlag
                ? GetNotNearZeroTerms()
                : GetNotZeroTerms();
        }

        public abstract IEnumerable<GaTerm<TScalar>> GetZeroTerms();

        public abstract IEnumerable<GaTerm<TScalar>> GetNearZeroTerms();

        public IEnumerable<GaTerm<TScalar>> GetZeroTerms(bool nearZeroFlag)
        {
            return nearZeroFlag
                ? GetNearZeroTerms()
                : GetZeroTerms();
        }


        public abstract GaKVectorStorageBase<TScalar> GetLeftScaledCopy(TScalar scalingFactor);

        public abstract GaKVectorStorageBase<TScalar> GetRightScaledCopy(TScalar scalingFactor);

        public abstract GaKVectorStorageBase<TScalar> GetComputedCopy(Func<TScalar, TScalar> mappingFunc);

        public abstract GaKVectorStorageBase<TScalar> GetComputedCopy(Func<ulong, TScalar, TScalar> mappingFunc);

        public abstract GaKVectorStorageBase<TScalar> GetComputedCopy(Func<ulong, TScalar> mappingFunc);

        public abstract GaKVectorStorageBase<TScalar> GetComputedCopy(Func<int, ulong, TScalar, TScalar> mappingFunc);

        public abstract GaKVectorStorageBase<TScalar> GetComputedCopy(Func<int, ulong, TScalar> mappingFunc);

        
        public IGaKVectorStorage<TScalar> GetKVectorStorage()
        {
            return GaKVectorStorage<TScalar>.Create(
                ScalarProcessor,
                Grade,
                IndexScalarDictionary
            );
        }

        public IGaKVectorStorage<TScalar> GetKVectorStorageCopy()
        {
            return GaKVectorStorage<TScalar>.Create(
                ScalarProcessor,
                Grade,
                IndexScalarDictionary.CopyToDictionary()
            );
        }

        public IGaKVectorStorage<TScalar> GetKVectorStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            return GaKVectorStorage<TScalar>.Create(
                ScalarProcessor,
                Grade,
                IndexScalarDictionary.CopyToDictionary(scalarMapping)
            );
        }

        public GaMultivectorGradedStorage<TScalar> GetMultivectorGradedStorageCopy()
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.CopyToDictionary();

            return GaMultivectorGradedStorage<TScalar>.CreateKVector(
                ScalarProcessor, 
                Grade,
                indexScalarDictionary
            );
        }

        public GaMultivectorTermsStorage<TScalar> GetMultivectorTermsStorageCopy()
        {
            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerms(GetIdScalarPairs());

            return composer.CreateMultivectorTermsStorage();
        }

        public GaMultivectorTreeStorage<TScalar> GetMultivectorTreeStorageCopy()
        {
            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerms(GetIdScalarPairs());

            return composer.GetMultivectorTreeStorageCopy();
        }


        public GaBinaryTree<TScalar> GetBinaryTree(int treeDepth)
        {
            if (treeDepth < VSpaceDimension)
                throw new InvalidOperationException();

            var dict = GetIdScalarPairs()
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                );

            return new GaBinaryTree<TScalar>(treeDepth, dict);
        }

        public IGaGbtMultivectorStorageStack1<TScalar> CreateGbtStack(int treeDepth, int capacity)
        {
            //return GaGbtKVectorStorageStack1<T>.Create(capacity, treeDepth, this);
            //return GaGbtMultivectorStorageGradedStack1<T>.Create(capacity, treeDepth, this);
            return GaGbtMultivectorStorageUniformStack1<TScalar>.Create(
                capacity, 
                treeDepth, 
                this
            );
        }

        public abstract IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<ulong, TScalar, TScalar2> idScalarMapping);

        public abstract IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<int, ulong, TScalar, TScalar2> gradeIndexScalarMapping);

        public abstract IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<TScalar, TScalar2> scalarMapping);


        public IGaMultivectorStorage<TScalar> GetCompactStorage()
        {
            return this;
        }

        public IGaMultivectorGradedStorage<TScalar> GetCompactGradedStorage()
        {
            return this;
        }


        public abstract IGaMultivectorStorage<TScalar> GetStorageCopy();
        
        public abstract IGaMultivectorStorage<TScalar> GetStorageCopy(Func<TScalar, TScalar> scalarMapping);

        public abstract IGaMultivectorStorage<TScalar> GetNegative();

        public IGaMultivectorStorage<TScalar> GetNegative(Predicate<int> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(Grade)
                ? GetNegative()
                : this;
        }

        public IGaMultivectorStorage<TScalar> GetReverse()
        {
            return Grade.GradeHasNegativeReverse()
                ? GetNegative()
                : this;
        }

        public IGaMultivectorStorage<TScalar> GetGradeInvolution()
        {
            return Grade.GradeHasNegativeGradeInvolution()
                ? GetNegative()
                : this;
        }

        public IGaMultivectorStorage<TScalar> GetCliffordConjugate()
        {
            return Grade.GradeHasNegativeCliffordConjugate()
                ? GetNegative()
                : this;
        }

        public IGaMultivectorTermsStorage<TScalar> GetTermsStorage()
        {
            return IndexScalarDictionary.Count switch
            {
                0 => GaKVectorTermStorageBase<TScalar>.CreateZeroKVector(ScalarProcessor, Grade),
                1 => GaKVectorTermStorageBase<TScalar>.CreateKVector(ScalarProcessor, Grade, IndexScalarDictionary.First()),
                _ => GaMultivectorTermsStorage<TScalar>.Create(
                    ScalarProcessor, 
                    IndexScalarDictionary.ToDictionary(
                        pair => GaBasisUtils.BasisBladeId(Grade, pair.Key),
                        pair => pair.Value
                    )
                )
            };
        }

        public IGaMultivectorGradedStorage<TScalar> GetGradedStorage()
        {
            return this;
        }

        public abstract IGaScalarStorage<TScalar> GetScalarPart();

        public abstract IGaScalarStorage<TScalar> GetScalarPart(Func<TScalar, TScalar> scalarMapping);

        public abstract IGaVectorStorage<TScalar> GetVectorPart();

        public abstract IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, TScalar> scalarMapping);

        public abstract IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping);

        public abstract IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, bool> scalarSelection);

        public abstract IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, bool> indexScalarSelection);

        public abstract IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, bool> indexSelection);

        public abstract IGaBivectorStorage<TScalar> GetBivectorPart();

        public abstract IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, TScalar> scalarMapping);

        public abstract IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping);
        public abstract IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, bool> scalarSelection);
        
        public abstract IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, bool> indexScalarSelection);
        
        public abstract IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, bool> indexSelection);

        public abstract IGaKVectorStorage<TScalar> GetKVectorPart(int grade);

        public abstract IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, TScalar> scalarMapping);

        public abstract IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, TScalar> indexScalarMapping);
        
        public abstract IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, bool> scalarSelection);
        
        public abstract IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, bool> indexScalarSelection);
        
        public abstract IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, bool> indexSelection);

        public abstract IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, TScalar> scalarMapping);

        public abstract IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<ulong, bool> idSelection);
        
        public abstract IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<int, ulong, bool> gradeIndexSelection);
        
        public abstract IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, bool> scalarSelection);
        
        public abstract IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<ulong, TScalar, bool> idScalarSelection);
        
        public abstract IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<int, ulong, TScalar, bool> gradeIndexScalarSelection);

        public abstract Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<ulong, bool> indexSelection);

        public abstract Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<ulong, TScalar, bool> indexScalarSelection);

        public abstract Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<TScalar, bool> scalarSelection);
        
        public IGaMultivectorStorage<TScalar> Add(IGaMultivectorStorage<TScalar> mv2)
        {
            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerms(GetIdScalarPairs());

            composer.AddTerms(mv2.GetIdScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public IGaMultivectorStorage<TScalar> Add(TScalar scalar2)
        {
            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerms(GetIdScalarPairs());

            composer.AddTerm(0, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        
        public IGaMultivectorStorage<TScalar> Subtract(IGaMultivectorStorage<TScalar> mv2)
        {
            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerms(GetIdScalarPairs());

            composer.SubtractTerms(mv2.GetIdScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public IGaMultivectorStorage<TScalar> Subtract(TScalar scalar2)
        {
            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerms(GetIdScalarPairs());

            composer.SubtractTerm(0, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public IGaMultivectorStorage<TScalar> Times(TScalar scalar2)
        {
            var composer = new GaKVectorStorageComposer<TScalar>(ScalarProcessor, Grade);

            composer.SetRightScaledTerms(scalar2, GetIndexScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public IGaMultivectorStorage<TScalar> Divide(TScalar scalar2)
        {
            var composer = new GaKVectorStorageComposer<TScalar>(ScalarProcessor, Grade);

            var scalarInv2 = ScalarProcessor.Inverse(scalar2);

            composer.SetRightScaledTerms(scalarInv2, GetIndexScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public IGaKVectorStorage<TScalar> Op(IGaKVectorStorage<TScalar> mv2)
        {
            var grade1 = Grade;
            var grade2 = mv2.Grade;
            var composer = new GaKVectorStorageComposer<TScalar>(
                ScalarProcessor, 
                grade1 + grade2
            );

            var idScalarDictionary2 = mv2.GetIdScalarDictionary();

            foreach (var (index1, scalar1) in IndexScalarDictionary)
            {
                var id1 = GaBasisUtils.BasisBladeId(grade1, index1);

                foreach (var (id2, scalar2) in idScalarDictionary2)
                {
                    if (!GaBasisUtils.IsNonZeroOp(id1, id2))
                        continue;

                    var id = id1 ^ id2;
                    var scalar = GaBasisUtils.IsNegativeEGp(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    composer.AddTerm(id.BasisBladeIndex(), scalar);
                }    
            }

            composer.RemoveZeroTerms();

            return composer.GetKVectorStorage();
        }

        public IGaMultivectorStorage<TScalar> Op(IGaMultivectorStorage<TScalar> mv2)
        {
            if (mv2 is IGaScalarStorage<TScalar> scalarStorage2)
                return Times(scalarStorage2.Scalar);

            if (mv2 is IGaKVectorStorage<TScalar> kVectorStorage2)
                return Op(kVectorStorage2);

            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            composer.AddTerms(
                GaGbtProductsStack2<TScalar>.Create(this, mv2)
                    .GetOpIdScalarPairs()
            );

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
    }
}