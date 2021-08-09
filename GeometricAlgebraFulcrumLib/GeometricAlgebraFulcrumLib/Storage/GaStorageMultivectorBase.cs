using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Terms;
using GeometricAlgebraFulcrumLib.Structures.Even;
using GeometricAlgebraFulcrumLib.Structures.Graded;
using GeometricAlgebraFulcrumLib.TextComposers;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public abstract record GaStorageMultivectorBase<T>
        : IGaStorageMultivector<T>
    {
        public abstract bool IsUniform { get; }

        public abstract bool IsGraded { get; }

        public abstract uint VSpaceDimension { get; }

        public ulong MaxBasisBladeId { get; }

        public abstract int GradesCount { get; }

        public abstract int TermsCount { get; }
        

        protected GaStorageMultivectorBase(ulong maxBasisBladeId)
        {
            MaxBasisBladeId = maxBasisBladeId;
        }

        
        public abstract bool ContainsKey(ulong key);

        public abstract bool TryGetValue(ulong key, out T value);


        public abstract bool ContainsBivectorPart();
        
        public abstract bool ContainsKVectorPart(uint grade);

        public abstract bool IsEmpty();
        
        public abstract bool IsScalar();

        public abstract bool IsVector();

        public abstract bool IsBivector();

        public abstract bool IsKVector();

        public abstract bool IsKVector(uint grade);


        public abstract IEnumerable<uint> GetGrades();


        public abstract bool ContainsTerm(ulong id);

        public abstract bool ContainsTerm(uint grade, ulong index);
        
        public abstract bool ContainsScalarPart();
        
        public abstract bool ContainsVectorPart();


        public abstract bool TryGetTermScalar(ulong id, out T value);

        public abstract bool TryGetTermScalar(uint grade, ulong index, out T value);


        public abstract bool TryGetTerm(ulong id, out GaTerm<T> term);

        public abstract bool TryGetTerm(uint grade, ulong index, out GaTerm<T> term);
        
        public abstract IGaEvenDictionary<T> GetScalarPartDictionary();
        
        public abstract IGaEvenDictionary<T> GetVectorPartDictionary();
        
        public abstract IGaEvenDictionary<T> GetBivectorPartDictionary();
        
        public abstract IGaEvenDictionary<T> GetKVectorPartDictionary(uint grade);

        public abstract bool TryGetScalarPart(out IGaStorageScalar<T> scalar);
        
        public abstract bool TryGetVectorPart(out IGaStorageVector<T> vector);
        
        public abstract bool TryGetBivectorPart(out IGaStorageBivector<T> bivector);
        
        public abstract bool TryGetKVectorPart(uint grade, out IGaStorageKVector<T> kVector);


        public abstract IEnumerable<IGaStorageKVector<T>> GetKVectorStorages();

        public abstract IReadOnlyDictionary<uint, IGaStorageKVector<T>> GetKVectorStoragesDictionary();
        
        public abstract bool TryGetVectorPartDictionary(out IGaEvenDictionary<T> indexScalarDictionary);
        
        public abstract bool TryGetBivectorPartDictionary(out IGaEvenDictionary<T> indexScalarDictionary);
        
        public abstract bool TryGetKVectorPartDictionary(uint grade, out IGaEvenDictionary<T> indexScalarDictionary);

        public abstract IGaEvenDictionary<T> GetIdScalarDictionary();

        public abstract IGaGradedDictionary<T> GetGradeIndexScalarDictionary();


        public abstract IEnumerable<ulong> GetIds();

        public abstract IEnumerable<Tuple<uint, ulong>> GetGradeIndexTuples();

        public abstract IEnumerable<IGaBasisBlade> GetBasisBlades();

        public abstract IEnumerable<T> GetScalars();

        public abstract IEnumerable<KeyValuePair<ulong, T>> GetIdScalarPairs();

        public abstract IEnumerable<Tuple<ulong, T>> GetIdScalarTuples();
        
        public abstract IEnumerable<Tuple<uint, ulong, T>> GetGradeIndexScalarTuples();
        
        public abstract IEnumerable<GaTerm<T>> GetTerms();
        
        public abstract IEnumerable<GaTerm<T>> GetTerms(Func<ulong, bool> idSelection);
        
        public abstract IEnumerable<GaTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection);
        
        public abstract IEnumerable<GaTerm<T>> GetTerms(Func<T, bool> scalarSelection);
        
        public abstract IEnumerable<GaTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection);
        
        public abstract IEnumerable<GaTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection);

        public abstract IGaStorageMultivector<T> CopyToMultivectorStorage();

        public abstract IGaStorageMultivectorGraded<T> GetGradedMultivectorCopy();

        public IGaStorageMultivectorSparse<T> GetSparseMultivectorCopy()
        {
            return GaStorageMultivectorSparse<T>.Create(
                GetIdScalarPairs().ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                )
            );
        }

        public IGaStorageMultivectorSparse<T> GetTreeMultivectorCopy()
        {
            return GaStorageMultivectorSparse<T>.Create(
                GetIdScalarPairs().ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                ).CreateEvenDictionaryTree()
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


        public abstract GaEvenDictionaryTree<T> GetBinaryTree(int treeDepth);

        public abstract IGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity, IGaScalarProcessor<T> scalarProcessor);


        public abstract IGaStorageMultivector<T> GetCompactStorage();

        public abstract IGaStorageMultivectorGraded<T> GetCompactGradedStorage();

        public abstract IGaStorageMultivectorSparse<T> ToSparseMultivector();

        public abstract IGaStorageMultivectorGraded<T> ToGradedMultivector();

        public abstract IGaStorageVector<T> GetVectorPart();

        public abstract IGaStorageVector<T> GetVectorPart(Func<T, bool> scalarSelection);

        public abstract IGaStorageVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection);

        public abstract IGaStorageVector<T> GetVectorPart(Func<ulong, bool> indexSelection);

        public abstract IGaStorageBivector<T> GetBivectorPart();

        public abstract IGaStorageBivector<T> GetBivectorPart(Func<T, bool> scalarSelection);
        
        public abstract IGaStorageBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection);
        
        public abstract IGaStorageBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection);

        public abstract IGaStorageKVector<T> GetKVectorPart(uint grade);

        public abstract IGaStorageKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection);
        
        public abstract IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection);
        
        public abstract IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection);

        public abstract IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection);
        
        public abstract IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection);
        
        public abstract IGaStorageMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection);
        
        public abstract IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection);
        
        public abstract IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection);

        public abstract Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<ulong, bool> indexSelection);

        public abstract Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection);

        public abstract Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<T, bool> scalarSelection);
 
        public override string ToString()
        {
            return this.GetMultivectorText();
        }
    }
}