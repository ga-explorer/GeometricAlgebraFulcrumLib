using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraLib.Multivectors.Basis;
using GeometricAlgebraLib.Multivectors.Terms;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage.Composers;
using GeometricAlgebraLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraLib.Storage.GuidedBinaryTraversal.Products;
using GeometricAlgebraLib.Storage.Trees;

namespace GeometricAlgebraLib.Storage
{
    public abstract class GaMultivectorStorageBase<TScalar>
        : IGaMultivectorStorage<TScalar>
    {
        public IGaScalarProcessor<TScalar> ScalarProcessor { get; }

        public abstract bool IsUniform { get; }

        public abstract bool IsGraded { get; }

        public abstract int VSpaceDimension { get; }

        public ulong MaxBasisBladeId { get; }

        public abstract int GradesCount { get; }

        public abstract int TermsCount { get; }

        public abstract TScalar this[ulong id] { get; set; }

        public abstract TScalar this[int grade, ulong index] { get; set; }


        protected GaMultivectorStorageBase([NotNull] IGaScalarProcessor<TScalar> scalarProcessor, ulong maxBasisBladeId)
        {
            ScalarProcessor = scalarProcessor;
            MaxBasisBladeId = maxBasisBladeId;
        }

        
        public abstract bool ContainsKey(ulong key);

        public abstract bool TryGetValue(ulong key, out TScalar value);


        public abstract bool ContainsTermsOfGrade(int grade);

        public abstract bool IsEmpty();

        public abstract bool IsZero();

        public abstract bool IsNearZero();

        public bool IsZero(bool nearZeroFlag)
        {
            return nearZeroFlag
                ? IsNearZero()
                : IsZero();
        }

        public abstract bool IsScalar();

        public abstract bool IsVector();

        public abstract bool IsBivector();

        public abstract bool IsKVector();

        public abstract bool IsKVector(int grade);


        public abstract IEnumerable<int> GetGrades();


        public abstract bool ContainsTerm(ulong id);

        public abstract bool ContainsTerm(int grade, ulong index);


        public abstract TScalar GetTermScalar(ulong id);

        public abstract TScalar GetTermScalar(int grade, ulong index);


        public abstract bool TryGetTermScalar(ulong id, out TScalar value);

        public abstract bool TryGetTermScalar(int grade, ulong index, out TScalar value);


        public abstract GaTerm<TScalar> GetTerm(ulong id);

        public abstract GaTerm<TScalar> GetTerm(int grade, ulong index);


        public abstract bool TryGetTerm(ulong id, out GaTerm<TScalar> term);

        public abstract bool TryGetTerm(int grade, ulong index, out GaTerm<TScalar> term);


        public abstract IEnumerable<IGaKVectorStorage<TScalar>> GetKVectorStorages();

        public abstract IReadOnlyDictionary<int, IGaKVectorStorage<TScalar>> GetKVectorStoragesDictionary();

        public abstract IReadOnlyDictionary<ulong, TScalar> GetIdScalarDictionary();

        public abstract IReadOnlyDictionary<int, Dictionary<ulong, TScalar>> GetGradeIndexScalarDictionary();

        public abstract bool TryGetKVectorStorage(int grade, out IGaKVectorStorage<TScalar> storage);

        public abstract bool TryGetKVectorStorageDictionary(int grade,
            out IReadOnlyDictionary<ulong, TScalar> indexScalarDictionary);


        public abstract IEnumerable<ulong> GetIds();

        public abstract IEnumerable<Tuple<int, ulong>> GetGradeIndexTuples();

        public abstract IEnumerable<IGaBasisBlade> GetBasisBlades();

        public abstract IEnumerable<TScalar> GetScalars();

        public abstract IEnumerable<KeyValuePair<ulong, TScalar>> GetIdScalarPairs();

        public abstract IEnumerable<Tuple<ulong, TScalar>> GetIdScalarTuples();
        
        public abstract IEnumerable<Tuple<int, ulong, TScalar>> GetGradeIndexScalarTuples();
        
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


        public abstract IGaMultivectorStorage<TScalar> CopyToMultivectorStorage();

        public abstract GaMultivectorGradedStorage<TScalar> GetMultivectorGradedStorageCopy();

        public GaMultivectorTermsStorage<TScalar> GetMultivectorTermsStorageCopy()
        {
            return GaMultivectorTermsStorage<TScalar>.Create(
                ScalarProcessor, 
                GetIdScalarPairs().ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                )
            );
        }

        public GaMultivectorTreeStorage<TScalar> GetMultivectorTreeStorageCopy()
        {
            var idScalarDictionary = GetIdScalarPairs().ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

            return GaMultivectorTreeStorage<TScalar>.Create(
                ScalarProcessor, 
                idScalarDictionary
            );
        }

        
        /// <summary>
        /// Create a bit pattern where each active grades is a 1
        /// </summary>
        /// <returns></returns>
        public ulong GetStoredGradesBitPattern()
        {
            return GetGrades().Aggregate(
                0UL, 
                (current, grade) => current | 1UL << grade
            );
        }


        public abstract GaBinaryTree<TScalar> GetBinaryTree(int treeDepth);

        public abstract IGaGbtMultivectorStorageStack1<TScalar> CreateGbtStack(int treeDepth, int capacity);
        
        public abstract IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<ulong, TScalar, TScalar2> idScalarMapping);

        public abstract IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<int, ulong, TScalar, TScalar2> gradeIndexScalarMapping);

        public abstract IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<TScalar, TScalar2> scalarMapping);



        public abstract IGaMultivectorStorage<TScalar> GetCompactStorage();

        public abstract IGaMultivectorGradedStorage<TScalar> GetCompactGradedStorage();

        public abstract IGaMultivectorStorage<TScalar> GetStorageCopy();

        public abstract IGaMultivectorStorage<TScalar> GetStorageCopy(Func<TScalar, TScalar> scalarMapping);

        public abstract IGaMultivectorStorage<TScalar> GetNegative();

        public abstract IGaMultivectorStorage<TScalar> GetNegative(Predicate<int> gradeToNegativePredicate);

        public abstract IGaMultivectorStorage<TScalar> GetReverse();

        public abstract IGaMultivectorStorage<TScalar> GetGradeInvolution();

        public abstract IGaMultivectorStorage<TScalar> GetCliffordConjugate();

        public abstract IGaMultivectorTermsStorage<TScalar> GetTermsStorage();

        public abstract IGaMultivectorGradedStorage<TScalar> GetGradedStorage();

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
            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            composer.SetRightScaledTerms(scalar2, GetIdScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public IGaMultivectorStorage<TScalar> Divide(TScalar scalar2)
        {
            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            var scalarInv2 = ScalarProcessor.Inverse(scalar2);

            composer.SetRightScaledTerms(scalarInv2, GetIdScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public IGaMultivectorStorage<TScalar> Op(IGaMultivectorStorage<TScalar> mv2)
        {
            if (mv2 is IGaScalarStorage<TScalar> scalarStorage2)
                return Times(scalarStorage2.Scalar);

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