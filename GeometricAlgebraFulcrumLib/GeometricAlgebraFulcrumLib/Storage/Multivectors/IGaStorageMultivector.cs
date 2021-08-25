using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Structures;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public interface IGaStorageMultivector
    {
        uint MinVSpaceDimension { get; }

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


        ulong GetMinId();

        ulong GetMaxId();
        
        ulong GetMinId(uint grade);

        ulong GetMaxId(uint grade);

        uint GetMinGrade();

        uint GetMaxGrade();
        
        ulong GetMinIndex(uint grade);

        ulong GetMaxIndex(uint grade);


        IEnumerable<ulong> GetIds();

        IEnumerable<uint> GetGrades();

        ulong GetStoredGradesBitPattern();

        IEnumerable<GaRecordGradeKey> GetGradeIndexRecords();

        IEnumerable<GaBasisBlade> GetBasisBlades();
    }

    public interface IGaStorageMultivector<T> 
        : IGaStorageMultivector
    {
        bool TryGetScalar(out T value);

        bool TryGetTermScalar(ulong id, out T value);

        bool TryGetTermScalar(uint grade, ulong index, out T value);
        
        bool TryGetTerm(ulong id, out GaBasisTerm<T> term);

        bool TryGetTerm(uint grade, ulong index, out GaBasisTerm<T> term);


        IGaListEven<T> GetScalarPartList();

        IGaListEven<T> GetVectorPartList();

        IGaListEven<T> GetBivectorPartList();

        IGaListEven<T> GetKVectorPartList(uint grade);


        bool TryGetScalarPart(out IGaStorageScalar<T> scalar);

        bool TryGetVectorPart(out IGaStorageVector<T> vector);

        bool TryGetBivectorPart(out IGaStorageBivector<T> bivector);

        bool TryGetKVectorPart(uint grade, out IGaStorageKVector<T> kVector);

        
        IGaListEven<T> GetIdScalarList();

        IGaListGraded<T> GetGradeIndexScalarList();

        IGaListEven<T> GetIndexScalarList(uint grade);


        bool TryGetScalarPartList(out IGaListEven<T> indexScalarList);

        bool TryGetVectorPartList(out IGaListEven<T> indexScalarList);

        bool TryGetBivectorPartList(out IGaListEven<T> indexScalarList);

        bool TryGetKVectorPartList(uint grade, out IGaListEven<T> indexScalarList);


        IEnumerable<T> GetScalars();

        IEnumerable<GaRecordKeyValue<T>> GetIdScalarRecords();

        IEnumerable<GaRecordKeyValue<T>> GetIndexScalarRecords(uint grade);

        IEnumerable<GaRecordGradeKeyValue<T>> GetGradeIndexScalarRecords();


        IEnumerable<GaBasisTerm<T>> GetTerms();

        IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, bool> idSelection);

        IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection);

        IEnumerable<GaBasisTerm<T>> GetTerms(Func<T, bool> scalarSelection);

        IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection);

        IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection);
        

        GaListEvenTree<T> GetBinaryTree(int treeDepth);

        IGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity, IGaScalarProcessor<T> scalarProcessor);


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