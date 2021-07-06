using System;
using System.Collections.Generic;
using GeometricAlgebraLib.Algebra.Basis;
using GeometricAlgebraLib.Algebra.Multivectors.Terms;
using GeometricAlgebraLib.Processing.Scalars;
using GeometricAlgebraLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraLib.Storage.Trees;

namespace GeometricAlgebraLib.Storage
{
    public interface IGaMultivectorStorage
    {
        int VSpaceDimension { get; }

        ulong MaxBasisBladeId { get; }

        int TermsCount { get; }

        int GradesCount { get; }


        bool ContainsTerm(ulong id);

        bool ContainsTerm(int grade, ulong index);

        bool ContainsTermsOfGrade(int grade);
        
        bool IsEmpty();

        bool IsZero();

        bool IsNearZero();

        bool IsZero(bool nearZeroFlag);

        bool IsScalar();

        bool IsVector();

        bool IsBivector();

        bool IsKVector();

        bool IsKVector(int grade);


        IEnumerable<ulong> GetIds();

        IEnumerable<int> GetGrades();

        ulong GetStoredGradesBitPattern();

        IEnumerable<Tuple<int, ulong>> GetGradeIndexTuples();

        IEnumerable<IGaBasisBlade> GetBasisBlades();
    }

    public interface IGaMultivectorStorage<TScalar> 
        : IGaMultivectorStorage
    {
        IGaScalarProcessor<TScalar> ScalarProcessor { get; }

        TScalar GetTermScalar(ulong id);

        TScalar GetTermScalar(int grade, ulong index);

        bool TryGetTermScalar(ulong id, out TScalar value);

        bool TryGetTermScalar(int grade, ulong index, out TScalar value);

        GaTerm<TScalar> GetTerm(ulong id);

        GaTerm<TScalar> GetTerm(int grade, ulong index);

        bool TryGetTerm(ulong id, out GaTerm<TScalar> term);

        bool TryGetTerm(int grade, ulong index, out GaTerm<TScalar> term);


        IEnumerable<IGaKVectorStorage<TScalar>> GetKVectorStorages();

        IReadOnlyDictionary<int, IGaKVectorStorage<TScalar>> GetKVectorStoragesDictionary();

        bool TryGetKVectorStorage(int grade, out IGaKVectorStorage<TScalar> storage);

        bool TryGetKVectorStorageDictionary(int grade, out IReadOnlyDictionary<ulong, TScalar> indexScalarDictionary);


        IEnumerable<TScalar> GetScalars();

        IEnumerable<KeyValuePair<ulong, TScalar>> GetIdScalarPairs();

        IEnumerable<Tuple<ulong, TScalar>> GetIdScalarTuples();

        IEnumerable<Tuple<int, ulong, TScalar>> GetGradeIndexScalarTuples();

        IReadOnlyDictionary<ulong, TScalar> GetIdScalarDictionary();

        IReadOnlyDictionary<int, Dictionary<ulong, TScalar>> GetGradeIndexScalarDictionary();

        IEnumerable<GaTerm<TScalar>> GetTerms();

        IEnumerable<GaTerm<TScalar>> GetNotZeroTerms();

        IEnumerable<GaTerm<TScalar>> GetNotNearZeroTerms();

        IEnumerable<GaTerm<TScalar>> GetNotZeroTerms(bool nearZeroFlag);

        IEnumerable<GaTerm<TScalar>> GetZeroTerms();

        IEnumerable<GaTerm<TScalar>> GetNearZeroTerms();

        IEnumerable<GaTerm<TScalar>> GetZeroTerms(bool nearZeroFlag);


        GaBinaryTree<TScalar> GetBinaryTree(int treeDepth);

        IGaGbtMultivectorStorageStack1<TScalar> CreateGbtStack(int treeDepth, int capacity);


        GaMultivectorGradedStorage<TScalar> GetMultivectorGradedStorageCopy();

        GaMultivectorTermsStorage<TScalar> GetMultivectorTermsStorageCopy();

        GaMultivectorTreeStorage<TScalar> GetMultivectorTreeStorageCopy();

        IGaMultivectorStorage<TScalar> GetStorageCopy();
        
        IGaMultivectorStorage<TScalar> GetStorageCopy(Func<TScalar, TScalar> scalarMapping);

        IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<ulong, TScalar, TScalar2> idScalarMapping);

        IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<int, ulong, TScalar, TScalar2> gradeIndexScalarMapping);

        IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<TScalar, TScalar2> scalarMapping);

        IGaMultivectorStorage<TScalar> GetNegative();

        IGaMultivectorStorage<TScalar> GetNegative(Predicate<int> gradeToNegativePredicate);

        IGaMultivectorStorage<TScalar> GetReverse();

        IGaMultivectorStorage<TScalar> GetGradeInvolution();

        IGaMultivectorStorage<TScalar> GetCliffordConjugate();

        IGaMultivectorTermsStorage<TScalar> GetTermsStorage();

        IGaMultivectorGradedStorage<TScalar> GetGradedStorage();


        IGaScalarStorage<TScalar> GetScalarPart();

        IGaScalarStorage<TScalar> GetScalarPart(Func<TScalar, TScalar> scalarMapping);

        IGaVectorStorage<TScalar> GetVectorPart();

        IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, TScalar> scalarMapping);

        IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping);

        IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, bool> scalarSelection);

        IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, bool> indexScalarSelection);

        IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, bool> indexSelection);

        IGaBivectorStorage<TScalar> GetBivectorPart();

        IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, TScalar> scalarMapping);

        IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping);

        IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, bool> scalarSelection);

        IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, bool> indexScalarSelection);

        IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, bool> indexSelection);

        IGaKVectorStorage<TScalar> GetKVectorPart(int grade);

        IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, TScalar> scalarMapping);

        IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, TScalar> indexScalarMapping);

        IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, bool> scalarSelection);

        IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, bool> indexScalarSelection);

        IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, bool> indexSelection);

        IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, TScalar> scalarMapping);

        IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<ulong, bool> idSelection);

        IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<int, ulong, bool> gradeIndexSelection);

        IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, bool> scalarSelection);

        IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<ulong, TScalar, bool> idScalarSelection);

        IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<int, ulong, TScalar, bool> gradeIndexScalarSelection);

        Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<ulong, bool> indexSelection);

        Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<ulong, TScalar, bool> indexScalarSelection);

        Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<TScalar, bool> scalarSelection);


        IGaMultivectorStorage<TScalar> Add(IGaMultivectorStorage<TScalar> mv2);

        IGaMultivectorStorage<TScalar> Add(TScalar scalar2);

        IGaMultivectorStorage<TScalar> Subtract(IGaMultivectorStorage<TScalar> mv2);

        IGaMultivectorStorage<TScalar> Subtract(TScalar scalar2);

        IGaMultivectorStorage<TScalar> Times(TScalar scalar2);

        IGaMultivectorStorage<TScalar> Divide(TScalar scalar2);

        IGaMultivectorStorage<TScalar> Op(IGaMultivectorStorage<TScalar> mv2);
    }
}