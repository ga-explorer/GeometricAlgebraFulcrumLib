using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public abstract record GaMultivectorStorageBase<T>
        : IGaMultivectorStorage<T>
    {
        public abstract bool IsUniform { get; }

        public abstract bool IsGraded { get; }

        public abstract uint MinVSpaceDimension { get; }

        public abstract int GradesCount { get; }

        public abstract int TermsCount { get; }


        public abstract bool ContainsBivectorPart();
        
        public abstract bool ContainsKVectorPart(uint grade);

        public abstract bool IsEmpty();
        
        public abstract bool IsScalar();

        public abstract bool IsVector();

        public abstract bool IsBivector();

        public abstract bool IsKVector();

        public abstract bool IsKVector(uint grade);
        
        public abstract ulong GetMinId();
        
        public abstract ulong GetMaxId();
        
        public abstract ulong GetMinId(uint grade);
        
        public abstract ulong GetMaxId(uint grade);
        
        public abstract uint GetMinGrade();
        
        public abstract uint GetMaxGrade();
        
        public abstract ulong GetMinIndex(uint grade);
        
        public abstract ulong GetMaxIndex(uint grade);


        public abstract IEnumerable<uint> GetGrades();


        public abstract bool ContainsTerm(ulong id);

        public abstract bool ContainsTerm(uint grade, ulong index);
        
        public abstract bool ContainsScalarPart();
        
        public abstract bool ContainsVectorPart();


        public abstract bool TryGetScalar(out T value);

        public abstract bool TryGetTermScalar(ulong id, out T value);

        public abstract bool TryGetTermScalar(uint grade, ulong index, out T value);


        public abstract bool TryGetTerm(ulong id, out GaBasisTerm<T> term);

        public abstract bool TryGetTerm(uint grade, ulong index, out GaBasisTerm<T> term);
        
        public abstract ILaVectorEvenStorage<T> GetScalarPartList();
        
        public abstract ILaVectorEvenStorage<T> GetVectorPartList();
        
        public abstract ILaVectorEvenStorage<T> GetBivectorPartList();
        
        public abstract ILaVectorEvenStorage<T> GetKVectorPartList(uint grade);

        public abstract bool TryGetScalarPart(out IGaScalarStorage<T> scalar);
        
        public abstract bool TryGetVectorPart(out IGaVectorStorage<T> vector);
        
        public abstract bool TryGetBivectorPart(out IGaBivectorStorage<T> bivector);
        
        public abstract bool TryGetKVectorPart(uint grade, out IGaKVectorStorage<T> kVector);


        public abstract bool TryGetScalarPartList(out ILaVectorEvenStorage<T> indexScalarList);

        public abstract bool TryGetVectorPartList(out ILaVectorEvenStorage<T> indexScalarList);
        
        public abstract bool TryGetBivectorPartList(out ILaVectorEvenStorage<T> indexScalarList);
        
        public abstract bool TryGetKVectorPartList(uint grade, out ILaVectorEvenStorage<T> indexScalarList);

        public abstract ILaVectorEvenStorage<T> GetIdScalarList();

        public abstract ILaVectorGradedStorage<T> GetGradeIndexScalarList();

        public abstract ILaVectorEvenStorage<T> GetIndexScalarList(uint grade);


        public abstract IEnumerable<ulong> GetIds();

        public abstract IEnumerable<GradeIndexRecord> GetGradeIndexRecords();

        public abstract IEnumerable<GaBasisBlade> GetBasisBlades();

        public abstract IEnumerable<T> GetScalars();

        public abstract IEnumerable<IndexScalarRecord<T>> GetIdScalarRecords();
        
        public abstract IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords(uint grade);

        public abstract IEnumerable<GradeIndexScalarRecord<T>> GetGradeIndexScalarRecords();
        
        public abstract IEnumerable<GaBasisTerm<T>> GetTerms();
        
        public abstract IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, bool> idSelection);
        
        public abstract IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection);
        
        public abstract IEnumerable<GaBasisTerm<T>> GetTerms(Func<T, bool> scalarSelection);
        
        public abstract IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection);
        
        public abstract IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection);


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


        public abstract LaVectorTreeStorage<T> GetBinaryTree(int treeDepth);

        public abstract IGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity, IScalarProcessor<T> scalarProcessor);


        public abstract IGaMultivectorSparseStorage<T> ToSparseMultivector();

        public abstract IGaMultivectorGradedStorage<T> ToGradedMultivector();

        public abstract IGaVectorStorage<T> GetVectorPart();

        public abstract IGaVectorStorage<T> GetVectorPart(Func<T, bool> scalarSelection);

        public abstract IGaVectorStorage<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection);

        public abstract IGaVectorStorage<T> GetVectorPart(Func<ulong, bool> indexSelection);

        public abstract IGaBivectorStorage<T> GetBivectorPart();

        public abstract IGaBivectorStorage<T> GetBivectorPart(Func<T, bool> scalarSelection);
        
        public abstract IGaBivectorStorage<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection);
        
        public abstract IGaBivectorStorage<T> GetBivectorPart(Func<ulong, bool> indexSelection);

        public abstract IGaKVectorStorage<T> GetKVectorPart(uint grade);

        public abstract IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection);
        
        public abstract IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection);
        
        public abstract IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection);

        public abstract IGaMultivectorStorage<T> GetMultivectorPart(Func<ulong, bool> idSelection);
        
        public abstract IGaMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection);
        
        public abstract IGaMultivectorStorage<T> GetMultivectorPart(Func<T, bool> scalarSelection);
        
        public abstract IGaMultivectorStorage<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection);
        
        public abstract IGaMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection);

        public abstract Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>> SplitVectorPart(Func<ulong, bool> indexSelection);

        public abstract Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection);

        public abstract Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>> SplitVectorPart(Func<T, bool> scalarSelection);
 
        public override string ToString()
        {
            return this.GetMultivectorText();
        }
    }
}