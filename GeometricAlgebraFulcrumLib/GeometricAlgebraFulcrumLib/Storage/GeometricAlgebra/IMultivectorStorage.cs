using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
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

        ulong GetStoredBasisVectorsBitPattern();

        IEnumerable<RGaGradeKvIndexRecord> GetGradeIndexRecords();
    }

    public interface IMultivectorStorage<T> 
        : IMultivectorStorage
    {
        bool TryGetScalar(out T value);

        bool TryGetTermScalar(ulong id, out T value);

        bool TryGetTermScalar(uint grade, ulong index, out T value);
        
        bool TryGetTerm(ulong id, out KeyValuePair<ulong, T> term);

        bool TryGetTerm(uint grade, ulong index, out KeyValuePair<ulong, T> term);


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

        IEnumerable<RGaKvIndexScalarRecord<T>> GetIdScalarRecords();

        IEnumerable<RGaKvIndexScalarRecord<T>> GetIndexScalarRecords(uint grade);

        IEnumerable<RGaGradeKvIndexScalarRecord<T>> GetGradeIndexScalarRecords();


        IEnumerable<KeyValuePair<ulong, T>> GetTerms();

        IEnumerable<KeyValuePair<ulong, T>> GetTerms(Func<ulong, bool> idSelection);

        IEnumerable<KeyValuePair<ulong, T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection);

        IEnumerable<KeyValuePair<ulong, T>> GetTerms(Func<T, bool> scalarSelection);

        IEnumerable<KeyValuePair<ulong, T>> GetTerms(Func<ulong, T, bool> idScalarSelection);

        IEnumerable<KeyValuePair<ulong, T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection);
        

        LinVectorTreeStorage<T> GetBinaryTree(int treeDepth);


        MultivectorStorage<T> ToMultivectorStorage();

        MultivectorGradedStorage<T> ToMultivectorGradedStorage();


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


        Tuple<IMultivectorStorage<T>, IMultivectorStorage<T>> SplitEvenOddParts();

        Tuple<VectorStorage<T>, VectorStorage<T>> SplitVectorPart(Func<ulong, bool> indexSelection);

        Tuple<VectorStorage<T>, VectorStorage<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection);

        Tuple<VectorStorage<T>, VectorStorage<T>> SplitVectorPart(Func<T, bool> scalarSelection);
    }
}