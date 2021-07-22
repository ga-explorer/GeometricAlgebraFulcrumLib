using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Terms;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Trees;
using GeometricAlgebraFulcrumLib.TextComposers;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public abstract class GasMultivectorBase<T>
        : IGasMultivector<T>
    {
        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public abstract bool IsUniform { get; }

        public abstract bool IsGraded { get; }

        public abstract uint VSpaceDimension { get; }

        public ulong MaxBasisBladeId { get; }

        public abstract int GradesCount { get; }

        public abstract int TermsCount { get; }

        public abstract T this[ulong id] { get; set; }

        public abstract T this[uint grade, ulong index] { get; set; }


        protected GasMultivectorBase([NotNull] IGaScalarProcessor<T> scalarProcessor, ulong maxBasisBladeId)
        {
            ScalarProcessor = scalarProcessor;
            MaxBasisBladeId = maxBasisBladeId;
        }

        
        public abstract bool ContainsKey(ulong key);

        public abstract bool TryGetValue(ulong key, out T value);


        public abstract bool ContainsTermsOfGrade(uint grade);

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

        public abstract bool IsKVector(uint grade);


        public abstract IEnumerable<uint> GetGrades();


        public abstract bool ContainsTerm(ulong id);

        public abstract bool ContainsTerm(uint grade, ulong index);


        public abstract T GetTermScalar(ulong id);

        public abstract T GetTermScalar(uint grade, ulong index);


        public abstract bool TryGetTermScalar(ulong id, out T value);

        public abstract bool TryGetTermScalar(uint grade, ulong index, out T value);


        public abstract GaTerm<T> GetTerm(ulong id);

        public abstract GaTerm<T> GetTerm(uint grade, ulong index);


        public abstract bool TryGetTerm(ulong id, out GaTerm<T> term);

        public abstract bool TryGetTerm(uint grade, ulong index, out GaTerm<T> term);


        public abstract IEnumerable<IGasKVector<T>> GetKVectorStorages();

        public abstract IReadOnlyDictionary<uint, IGasKVector<T>> GetKVectorStoragesDictionary();

        public abstract IReadOnlyDictionary<ulong, T> GetIdScalarDictionary();

        public abstract IReadOnlyDictionary<uint, Dictionary<ulong, T>> GetGradeIndexScalarDictionary();

        public abstract bool TryGetKVectorStorage(uint grade, out IGasKVector<T> storage);

        public abstract bool TryGetKVectorStorageDictionary(uint grade,
            out IReadOnlyDictionary<ulong, T> indexScalarDictionary);


        public abstract IEnumerable<ulong> GetIds();

        public abstract IEnumerable<Tuple<uint, ulong>> GetGradeIndexTuples();

        public abstract IEnumerable<IGaBasisBlade> GetBasisBlades();

        public abstract IEnumerable<T> GetScalars();

        public abstract IEnumerable<KeyValuePair<ulong, T>> GetIdScalarPairs();

        public abstract IEnumerable<Tuple<ulong, T>> GetIdScalarTuples();
        
        public abstract IEnumerable<Tuple<uint, ulong, T>> GetGradeIndexScalarTuples();
        
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


        public abstract IGasMultivector<T> CopyToMultivectorStorage();

        public abstract IGasGradedMultivector<T> GetGradedMultivectorCopy();

        public IGasTermsMultivector<T> GetTermsMultivectorCopy()
        {
            return ScalarProcessor.CreateTermsMultivector(GetIdScalarPairs().ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                )
            );
        }

        public GasTreeMultivector<T> GetTreeMultivectorCopy()
        {
            var idScalarDictionary = GetIdScalarPairs().ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

            return GaStorageFactory.CreateTreeMultivector(
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
                (current, grade) => current | 1UL << (int) grade
            );
        }


        public abstract GaBinaryTree<T> GetBinaryTree(int treeDepth);

        public abstract IGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity);
        
        public abstract IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<ulong, T, T2> idScalarMapping);

        public abstract IGasMultivector<T2> GetCopy<T2>(
            IGaScalarProcessor<T2> scalarProcessor, Func<uint, ulong, T, T2> gradeIndexScalarMapping);

        public abstract IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<T, T2> scalarMapping);



        public abstract IGasMultivector<T> GetCompactStorage();

        public abstract IGasGradedMultivector<T> GetCompactGradedStorage();

        public abstract IGasMultivector<T> GetCopy();

        public abstract IGasMultivector<T> GetCopy(Func<T, T> scalarMapping);

        public abstract IGasMultivector<T> GetNegative();

        public abstract IGasMultivector<T> GetNegative(Predicate<uint> gradeToNegativePredicate);

        public abstract IGasMultivector<T> GetReverse();

        public abstract IGasMultivector<T> GetGradeInvolution();

        public abstract IGasMultivector<T> GetCliffordConjugate();

        public abstract IGasTermsMultivector<T> ToTermsMultivector();

        public abstract IGasGradedMultivector<T> ToGradedMultivector();

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