using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors
{
    public interface IMultivectorStorage
    {
        uint MinVSpaceDimension { get; }

        int TermsCount { get; }

        int GradesCount { get; }

        bool IsEven { get; }

        bool IsGraded { get; }


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

        IEnumerable<BasisBlade> GetBasisBlades();
    }

    public interface IMultivectorStorage<T> 
        : IMultivectorStorage
    {
        bool TryGetScalar(out T value);

        bool TryGetTermScalar(ulong id, out T value);

        bool TryGetTermScalar(uint grade, ulong index, out T value);
        
        bool TryGetTerm(ulong id, out BasisTerm<T> term);

        bool TryGetTerm(uint grade, ulong index, out BasisTerm<T> term);


        ILinVectorStorage<T> GetScalarPartList();

        ILinVectorStorage<T> GetVectorPartList();

        ILinVectorStorage<T> GetBivectorPartList();

        ILinVectorStorage<T> GetKVectorPartList(uint grade);


        bool TryGetVectorPart(out VectorStorage<T> vector);

        bool TryGetBivectorPart(out BivectorStorage<T> bivector);

        bool TryGetKVectorPart(uint grade, out KVectorStorage<T> kVector);

        
        ILinVectorStorage<T> GetLinVectorIdScalarStorage();

        ILinVectorGradedStorage<T> GetLinVectorGradedStorage();

        ILinVectorStorage<T> GetLinVectorIndexScalarStorage(uint grade);


        bool TryGetScalarPartList(out ILinVectorStorage<T> indexScalarList);

        bool TryGetVectorPartList(out ILinVectorStorage<T> indexScalarList);

        bool TryGetBivectorPartList(out ILinVectorStorage<T> indexScalarList);

        bool TryGetKVectorPartList(uint grade, out ILinVectorStorage<T> indexScalarList);


        IEnumerable<T> GetScalars();

        IEnumerable<IndexScalarRecord<T>> GetIdScalarRecords();

        IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords(uint grade);

        IEnumerable<GradeIndexScalarRecord<T>> GetGradeIndexScalarRecords();


        IEnumerable<BasisTerm<T>> GetTerms();

        IEnumerable<BasisTerm<T>> GetTerms(Func<ulong, bool> idSelection);

        IEnumerable<BasisTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection);

        IEnumerable<BasisTerm<T>> GetTerms(Func<T, bool> scalarSelection);

        IEnumerable<BasisTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection);

        IEnumerable<BasisTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection);
        

        LinVectorTreeStorage<T> GetBinaryTree(int treeDepth);

        IGeoGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity, IScalarAlgebraProcessor<T> scalarProcessor);


        MultivectorStorage<T> ToMultivectorStorage();

        MultivectorGradedStorage<T> ToGradedMultivectorStorage();


        VectorStorage<T> GetVectorPart();

        VectorStorage<T> GetVectorPart(Func<T, bool> scalarSelection);

        VectorStorage<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection);

        VectorStorage<T> GetVectorPart(Func<ulong, bool> indexSelection);


        BivectorStorage<T> GetBivectorPart();

        BivectorStorage<T> GetBivectorPart(Func<T, bool> scalarSelection);

        BivectorStorage<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection);

        BivectorStorage<T> GetBivectorPart(Func<ulong, bool> indexSelection);


        KVectorStorage<T> GetKVectorPart(uint grade);

        KVectorStorage<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection);

        KVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection);

        KVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection);


        IMultivectorStorage<T> GetMultivectorPart(Func<ulong, bool> idSelection);

        IMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection);

        IMultivectorStorage<T> GetMultivectorPart(Func<T, bool> scalarSelection);

        IMultivectorStorage<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection);

        IMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection);


        Tuple<VectorStorage<T>, VectorStorage<T>> SplitVectorPart(Func<ulong, bool> indexSelection);

        Tuple<VectorStorage<T>, VectorStorage<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection);

        Tuple<VectorStorage<T>, VectorStorage<T>> SplitVectorPart(Func<T, bool> scalarSelection);
    }
}