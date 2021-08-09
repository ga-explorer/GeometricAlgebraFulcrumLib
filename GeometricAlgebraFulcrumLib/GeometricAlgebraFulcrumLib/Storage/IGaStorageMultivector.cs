using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Terms;
using GeometricAlgebraFulcrumLib.Structures.Even;
using GeometricAlgebraFulcrumLib.Structures.Graded;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public interface IGaStorageMultivector
    {
        uint VSpaceDimension { get; }

        ulong MaxBasisBladeId { get; }

        int TermsCount { get; }

        int GradesCount { get; }


        bool ContainsTerm(ulong id);

        bool ContainsTerm(uint grade, ulong index);

        bool ContainsScalarPart();

        bool ContainsVectorPart();

        bool ContainsBivectorPart();

        bool ContainsKVectorPart(uint grade);


        bool IsEmpty();
        
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

    public interface IGaStorageMultivector<T> 
        : IGaStorageMultivector
    {
        bool TryGetTermScalar(ulong id, out T value);

        bool TryGetTermScalar(uint grade, ulong index, out T value);
        
        bool TryGetTerm(ulong id, out GaTerm<T> term);

        bool TryGetTerm(uint grade, ulong index, out GaTerm<T> term);


        IGaEvenDictionary<T> GetScalarPartDictionary();

        IGaEvenDictionary<T> GetVectorPartDictionary();

        IGaEvenDictionary<T> GetBivectorPartDictionary();

        IGaEvenDictionary<T> GetKVectorPartDictionary(uint grade);


        bool TryGetScalarPart(out IGaStorageScalar<T> scalar);

        bool TryGetVectorPart(out IGaStorageVector<T> vector);

        bool TryGetBivectorPart(out IGaStorageBivector<T> bivector);

        bool TryGetKVectorPart(uint grade, out IGaStorageKVector<T> kVector);


        IEnumerable<IGaStorageKVector<T>> GetKVectorStorages();

        IReadOnlyDictionary<uint, IGaStorageKVector<T>> GetKVectorStoragesDictionary();

        bool TryGetVectorPartDictionary(out IGaEvenDictionary<T> indexScalarDictionary);

        bool TryGetBivectorPartDictionary(out IGaEvenDictionary<T> indexScalarDictionary);

        bool TryGetKVectorPartDictionary(uint grade, out IGaEvenDictionary<T> indexScalarDictionary);


        IEnumerable<T> GetScalars();

        IEnumerable<KeyValuePair<ulong, T>> GetIdScalarPairs();

        IEnumerable<Tuple<ulong, T>> GetIdScalarTuples();

        IEnumerable<Tuple<uint, ulong, T>> GetGradeIndexScalarTuples();

        IGaEvenDictionary<T> GetIdScalarDictionary();

        IGaGradedDictionary<T> GetGradeIndexScalarDictionary();


        IEnumerable<GaTerm<T>> GetTerms();

        IEnumerable<GaTerm<T>> GetTerms(Func<ulong, bool> idSelection);

        IEnumerable<GaTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection);

        IEnumerable<GaTerm<T>> GetTerms(Func<T, bool> scalarSelection);

        IEnumerable<GaTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection);

        IEnumerable<GaTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection);
        

        GaEvenDictionaryTree<T> GetBinaryTree(int treeDepth);

        IGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity, IGaScalarProcessor<T> scalarProcessor);


        IGaStorageMultivectorGraded<T> GetGradedMultivectorCopy();

        IGaStorageMultivectorSparse<T> GetSparseMultivectorCopy();


        IGaStorageMultivectorSparse<T> ToSparseMultivector();

        IGaStorageMultivectorGraded<T> ToGradedMultivector();


        IGaStorageVector<T> GetVectorPart();

        IGaStorageVector<T> GetVectorPart(Func<T, bool> scalarSelection);

        IGaStorageVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection);

        IGaStorageVector<T> GetVectorPart(Func<ulong, bool> indexSelection);

        IGaStorageBivector<T> GetBivectorPart();

        IGaStorageBivector<T> GetBivectorPart(Func<T, bool> scalarSelection);

        IGaStorageBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection);

        IGaStorageBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection);

        IGaStorageKVector<T> GetKVectorPart(uint grade);

        IGaStorageKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection);

        IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection);

        IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection);

        IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection);

        IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection);

        IGaStorageMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection);

        IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection);

        IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection);

        Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<ulong, bool> indexSelection);

        Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection);

        Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<T, bool> scalarSelection);
    }
}