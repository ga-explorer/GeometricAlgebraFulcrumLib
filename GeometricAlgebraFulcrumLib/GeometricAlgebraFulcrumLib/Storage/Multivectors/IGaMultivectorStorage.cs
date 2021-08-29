using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public interface IGaMultivectorStorage
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

        IEnumerable<GradeIndexRecord> GetGradeIndexRecords();

        IEnumerable<GaBasisBlade> GetBasisBlades();
    }

    public interface IGaMultivectorStorage<T> 
        : IGaMultivectorStorage
    {
        bool TryGetScalar(out T value);

        bool TryGetTermScalar(ulong id, out T value);

        bool TryGetTermScalar(uint grade, ulong index, out T value);
        
        bool TryGetTerm(ulong id, out GaBasisTerm<T> term);

        bool TryGetTerm(uint grade, ulong index, out GaBasisTerm<T> term);


        ILaVectorEvenStorage<T> GetScalarPartList();

        ILaVectorEvenStorage<T> GetVectorPartList();

        ILaVectorEvenStorage<T> GetBivectorPartList();

        ILaVectorEvenStorage<T> GetKVectorPartList(uint grade);


        bool TryGetScalarPart(out IGaScalarStorage<T> scalar);

        bool TryGetVectorPart(out IGaVectorStorage<T> vector);

        bool TryGetBivectorPart(out IGaBivectorStorage<T> bivector);

        bool TryGetKVectorPart(uint grade, out IGaKVectorStorage<T> kVector);

        
        ILaVectorEvenStorage<T> GetIdScalarList();

        ILaVectorGradedStorage<T> GetGradeIndexScalarList();

        ILaVectorEvenStorage<T> GetIndexScalarList(uint grade);


        bool TryGetScalarPartList(out ILaVectorEvenStorage<T> indexScalarList);

        bool TryGetVectorPartList(out ILaVectorEvenStorage<T> indexScalarList);

        bool TryGetBivectorPartList(out ILaVectorEvenStorage<T> indexScalarList);

        bool TryGetKVectorPartList(uint grade, out ILaVectorEvenStorage<T> indexScalarList);


        IEnumerable<T> GetScalars();

        IEnumerable<IndexScalarRecord<T>> GetIdScalarRecords();

        IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords(uint grade);

        IEnumerable<GradeIndexScalarRecord<T>> GetGradeIndexScalarRecords();


        IEnumerable<GaBasisTerm<T>> GetTerms();

        IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, bool> idSelection);

        IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection);

        IEnumerable<GaBasisTerm<T>> GetTerms(Func<T, bool> scalarSelection);

        IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection);

        IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection);
        

        LaVectorTreeStorage<T> GetBinaryTree(int treeDepth);

        IGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity, IScalarProcessor<T> scalarProcessor);


        IGaMultivectorSparseStorage<T> ToSparseMultivector();

        IGaMultivectorGradedStorage<T> ToGradedMultivector();


        IGaVectorStorage<T> GetVectorPart();

        IGaVectorStorage<T> GetVectorPart(Func<T, bool> scalarSelection);

        IGaVectorStorage<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection);

        IGaVectorStorage<T> GetVectorPart(Func<ulong, bool> indexSelection);


        IGaBivectorStorage<T> GetBivectorPart();

        IGaBivectorStorage<T> GetBivectorPart(Func<T, bool> scalarSelection);

        IGaBivectorStorage<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection);

        IGaBivectorStorage<T> GetBivectorPart(Func<ulong, bool> indexSelection);


        IGaKVectorStorage<T> GetKVectorPart(uint grade);

        IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection);

        IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection);

        IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection);


        IGaMultivectorStorage<T> GetMultivectorPart(Func<ulong, bool> idSelection);

        IGaMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection);

        IGaMultivectorStorage<T> GetMultivectorPart(Func<T, bool> scalarSelection);

        IGaMultivectorStorage<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection);

        IGaMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection);


        Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>> SplitVectorPart(Func<ulong, bool> indexSelection);

        Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection);

        Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>> SplitVectorPart(Func<T, bool> scalarSelection);
    }
}