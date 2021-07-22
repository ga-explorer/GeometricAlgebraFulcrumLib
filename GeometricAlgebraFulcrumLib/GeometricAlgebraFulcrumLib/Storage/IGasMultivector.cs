using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Terms;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Trees;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public interface IGasMultivector
    {
        uint VSpaceDimension { get; }

        ulong MaxBasisBladeId { get; }

        int TermsCount { get; }

        int GradesCount { get; }


        bool ContainsTerm(ulong id);

        bool ContainsTerm(uint grade, ulong index);

        bool ContainsTermsOfGrade(uint grade);
        
        bool IsEmpty();

        bool IsZero();

        bool IsNearZero();

        bool IsZero(bool nearZeroFlag);

        bool IsScalar();

        bool IsVector();

        bool IsBivector();

        bool IsKVector();

        bool IsKVector(uint grade);


        IEnumerable<ulong> GetIds();

        IEnumerable<uint> GetGrades();

        ulong GetStoredGradesBitPattern();

        IEnumerable<Tuple<uint, ulong>> GetGradeIndexTuples();

        IEnumerable<IGaBasisBlade> GetBasisBlades();
    }

    public interface IGasMultivector<T> 
        : IGasMultivector
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }

        T GetTermScalar(ulong id);

        T GetTermScalar(uint grade, ulong index);

        bool TryGetTermScalar(ulong id, out T value);

        bool TryGetTermScalar(uint grade, ulong index, out T value);

        GaTerm<T> GetTerm(ulong id);

        GaTerm<T> GetTerm(uint grade, ulong index);

        bool TryGetTerm(ulong id, out GaTerm<T> term);

        bool TryGetTerm(uint grade, ulong index, out GaTerm<T> term);


        IEnumerable<IGasKVector<T>> GetKVectorStorages();

        IReadOnlyDictionary<uint, IGasKVector<T>> GetKVectorStoragesDictionary();

        bool TryGetKVectorStorage(uint grade, out IGasKVector<T> storage);

        bool TryGetKVectorStorageDictionary(uint grade, out IReadOnlyDictionary<ulong, T> indexScalarDictionary);


        IEnumerable<T> GetScalars();

        IEnumerable<KeyValuePair<ulong, T>> GetIdScalarPairs();

        IEnumerable<Tuple<ulong, T>> GetIdScalarTuples();

        IEnumerable<Tuple<uint, ulong, T>> GetGradeIndexScalarTuples();

        IReadOnlyDictionary<ulong, T> GetIdScalarDictionary();

        IReadOnlyDictionary<uint, Dictionary<ulong, T>> GetGradeIndexScalarDictionary();

        IEnumerable<GaTerm<T>> GetTerms();

        IEnumerable<GaTerm<T>> GetNotZeroTerms();

        IEnumerable<GaTerm<T>> GetNotNearZeroTerms();

        IEnumerable<GaTerm<T>> GetNotZeroTerms(bool nearZeroFlag);

        IEnumerable<GaTerm<T>> GetZeroTerms();

        IEnumerable<GaTerm<T>> GetNearZeroTerms();

        IEnumerable<GaTerm<T>> GetZeroTerms(bool nearZeroFlag);


        GaBinaryTree<T> GetBinaryTree(int treeDepth);

        IGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity);


        IGasGradedMultivector<T> GetGradedMultivectorCopy();

        IGasTermsMultivector<T> GetTermsMultivectorCopy();

        GasTreeMultivector<T> GetTreeMultivectorCopy();


        IGasMultivector<T> GetCopy();
        
        IGasMultivector<T> GetCopy(Func<T, T> scalarMapping);

        IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<ulong, T, T2> idScalarMapping);

        IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<uint, ulong, T, T2> gradeIndexScalarMapping);

        IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<T, T2> scalarMapping);


        IGasMultivector<T> GetNegative();

        IGasMultivector<T> GetNegative(Predicate<uint> gradeToNegativePredicate);

        IGasMultivector<T> GetReverse();

        IGasMultivector<T> GetGradeInvolution();

        IGasMultivector<T> GetCliffordConjugate();


        IGasTermsMultivector<T> ToTermsMultivector();

        IGasGradedMultivector<T> ToGradedMultivector();


        IGasScalar<T> GetScalarPart();

        IGasScalar<T> GetScalarPart(Func<T, T> scalarMapping);


        IGasVector<T> GetVectorPart();

        IGasVector<T> GetVectorPart(Func<T, T> scalarMapping);

        IGasVector<T> GetVectorPart(Func<ulong, T, T> indexScalarMapping);

        IGasVector<T> GetVectorPart(Func<T, bool> scalarSelection);

        IGasVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection);

        IGasVector<T> GetVectorPart(Func<ulong, bool> indexSelection);


        IGasBivector<T> GetBivectorPart();

        IGasBivector<T> GetBivectorPart(Func<T, T> scalarMapping);

        IGasBivector<T> GetBivectorPart(Func<ulong, T, T> indexScalarMapping);

        IGasBivector<T> GetBivectorPart(Func<T, bool> scalarSelection);

        IGasBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection);

        IGasBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection);


        IGasKVector<T> GetKVectorPart(uint grade);

        IGasKVector<T> GetKVectorPart(uint grade, Func<T, T> scalarMapping);

        IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, T> indexScalarMapping);

        IGasKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection);

        IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection);

        IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection);


        IGasMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection);

        IGasMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection);

        IGasMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection);

        IGasMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection);

        IGasMultivector<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection);


        Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<ulong, bool> indexSelection);

        Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection);

        Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<T, bool> scalarSelection);
    }
}