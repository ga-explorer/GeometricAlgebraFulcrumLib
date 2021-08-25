using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Structures;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;
using GeometricAlgebraFulcrumLib.TextComposers;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public abstract record GaStorageMultivectorBase<T>
        : IGaStorageMultivector<T>
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
        
        public abstract IGaListEven<T> GetScalarPartList();
        
        public abstract IGaListEven<T> GetVectorPartList();
        
        public abstract IGaListEven<T> GetBivectorPartList();
        
        public abstract IGaListEven<T> GetKVectorPartList(uint grade);

        public abstract bool TryGetScalarPart(out IGaStorageScalar<T> scalar);
        
        public abstract bool TryGetVectorPart(out IGaStorageVector<T> vector);
        
        public abstract bool TryGetBivectorPart(out IGaStorageBivector<T> bivector);
        
        public abstract bool TryGetKVectorPart(uint grade, out IGaStorageKVector<T> kVector);


        public abstract bool TryGetScalarPartList(out IGaListEven<T> indexScalarList);

        public abstract bool TryGetVectorPartList(out IGaListEven<T> indexScalarList);
        
        public abstract bool TryGetBivectorPartList(out IGaListEven<T> indexScalarList);
        
        public abstract bool TryGetKVectorPartList(uint grade, out IGaListEven<T> indexScalarList);

        public abstract IGaListEven<T> GetIdScalarList();

        public abstract IGaListGraded<T> GetGradeIndexScalarList();

        public abstract IGaListEven<T> GetIndexScalarList(uint grade);


        public abstract IEnumerable<ulong> GetIds();

        public abstract IEnumerable<GaRecordGradeKey> GetGradeIndexRecords();

        public abstract IEnumerable<GaBasisBlade> GetBasisBlades();

        public abstract IEnumerable<T> GetScalars();

        public abstract IEnumerable<GaRecordKeyValue<T>> GetIdScalarRecords();
        
        public abstract IEnumerable<GaRecordKeyValue<T>> GetIndexScalarRecords(uint grade);

        public abstract IEnumerable<GaRecordGradeKeyValue<T>> GetGradeIndexScalarRecords();
        
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


        public abstract GaListEvenTree<T> GetBinaryTree(int treeDepth);

        public abstract IGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity, IGaScalarProcessor<T> scalarProcessor);


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