using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Terms;
using GeometricAlgebraFulcrumLib.Structures.Even;
using GeometricAlgebraFulcrumLib.Structures.Graded;
using GeometricAlgebraFulcrumLib.TextComposers;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public abstract record GaStorageKVectorBase<T> 
        : IGaStorageKVector<T>
    {
        public IGaEvenDictionary<T> IndexScalarDictionary { get; }

        public uint VSpaceDimension 
            => (uint) MaxBasisBladeId.LastOneBitPosition() + 1;

        public ulong MaxBasisBladeId { get; }

        public int GradesCount 
            => 1;

        public abstract uint Grade { get; }

        public ulong FirstIndex 
            => IndexScalarDictionary.GetFirstKey();

        public ulong LastIndex 
            => IndexScalarDictionary.GetLastKey();

        public int TermsCount 
            => IndexScalarDictionary.Count;

        public T FirstScalar 
            => IndexScalarDictionary.GetFirstValue();

        public T LastScalar 
            => IndexScalarDictionary.GetLastValue();


        protected GaStorageKVectorBase([NotNull] IGaEvenDictionary<T> indexScalarDictionary, ulong maxBasisBladeId)
        {
            IndexScalarDictionary = indexScalarDictionary;
            MaxBasisBladeId = maxBasisBladeId;
        }


        public bool ContainsTerm(ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return Grade == grade && IndexScalarDictionary.ContainsKey(index);
        }

        public bool ContainsTerm(uint grade, ulong index)
        {
            return grade == Grade && IndexScalarDictionary.ContainsKey(index);
        }

        public bool ContainsScalarPart()
        {
            return Grade == 0;
        }

        public bool ContainsVectorPart()
        {
            return Grade == 1;
        }

        public bool ContainsBivectorPart()
        {
            return Grade == 2;
        }

        public bool ContainsKVectorPart(uint grade)
        {
            return Grade == grade;
        }

        public bool ContainsTermWithIndex(ulong index)
        {
            return IndexScalarDictionary.ContainsKey(index);
        }

        public bool IsEmpty()
        {
            return IndexScalarDictionary.IsEmpty();
        }


        public bool IsScalar()
        {
            return Grade == 0;
        }

        public bool IsVector()
        {
            return Grade == 1;
        }

        public bool IsBivector()
        {
            return Grade == 2;
        }

        public bool IsKVector()
        {
            return true;
        }

        public bool IsKVector(uint grade)
        {
            return Grade == grade;
        }


        public IEnumerable<uint> GetGrades()
        {
            yield return Grade;
        }

        public ulong GetStoredGradesBitPattern()
        {
            return 1UL << (int) Grade;
        }

        public bool TryGetTermScalarByIndex(ulong index, out T value)
        {
            if (IndexScalarDictionary.TryGetValue(index, out value))
                return true;

            value = default;
            return false;
        }

        public bool TryGetTermScalar(ulong id, out T value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (Grade == grade && IndexScalarDictionary.TryGetValue(index, out value))
                return true;

            value = default;
            return false;
        }

        public bool TryGetTermScalar(uint grade, ulong index, out T value)
        {
            if (grade == Grade) 
                return IndexScalarDictionary.TryGetValue(index, out value);

            value = default;
            return false;
        }


        public abstract bool TryGetTermByIndex(int index, out GaTerm<T> term);

        public abstract bool TryGetTermByIndex(ulong index, out GaTerm<T> term);

        public abstract bool TryGetTerm(ulong id, out GaTerm<T> term);

        public abstract bool TryGetTerm(uint grade, ulong index, out GaTerm<T> term);

        public IGaEvenDictionary<T> GetScalarPartDictionary()
        {
            return Grade == 0 && !IndexScalarDictionary.IsEmpty()
                ? IndexScalarDictionary
                : GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaEvenDictionary<T> GetVectorPartDictionary()
        {
            return Grade == 1 && !IndexScalarDictionary.IsEmpty()
                ? IndexScalarDictionary
                : GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaEvenDictionary<T> GetBivectorPartDictionary()
        {
            return Grade == 2 && !IndexScalarDictionary.IsEmpty()
                ? IndexScalarDictionary
                : GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaEvenDictionary<T> GetKVectorPartDictionary(uint grade)
        {
            return Grade == grade && !IndexScalarDictionary.IsEmpty()
                ? IndexScalarDictionary
                : GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public bool TryGetScalarPart(out IGaStorageScalar<T> scalar)
        {
            if (Grade == 0)
            {
                scalar = (this is IGaStorageScalar<T> scalarStorage)
                    ? scalarStorage
                    : GaStorageScalar<T>.Create(IndexScalarDictionary);

                return true;
            }

            scalar = null;
            return false;
        }

        public bool TryGetVectorPart(out IGaStorageVector<T> vector)
        {
            if (Grade == 1)
            {
                vector =  (this is IGaStorageVector<T> vectorStorage)
                    ? vectorStorage
                    : IndexScalarDictionary.CreateStorageVector();

                return true;
            }

            vector = null;
            return false;
        }

        public bool TryGetBivectorPart(out IGaStorageBivector<T> bivector)
        {
            if (Grade == 2)
            {
                bivector =  (this is IGaStorageBivector<T> bivectorStorage)
                    ? bivectorStorage
                    : GaStorageBivector<T>.Create(IndexScalarDictionary);

                return true;
            }

            bivector = null;
            return false;
        }

        public bool TryGetKVectorPart(uint grade, out IGaStorageKVector<T> kVector)
        {
            if (Grade == 2)
            {
                kVector = this;
                return true;
            }

            kVector = null;
            return false;
        }


        public IEnumerable<IGaStorageKVector<T>> GetKVectorStorages()
        {
            yield return this;
        }

        public IReadOnlyDictionary<uint, IGaStorageKVector<T>> GetKVectorStoragesDictionary()
        {
            return new Dictionary<uint, IGaStorageKVector<T>>(){{Grade, this}};
        }

        public bool TryGetVectorPartDictionary(out IGaEvenDictionary<T> indexScalarDictionary)
        {
            if (Grade == 1 && !IndexScalarDictionary.IsEmpty())
            {
                indexScalarDictionary = IndexScalarDictionary;
                return true;
            }

            indexScalarDictionary = GaEvenDictionaryEmpty<T>.DefaultDictionary;
            return false;
        }

        public bool TryGetBivectorPartDictionary(out IGaEvenDictionary<T> indexScalarDictionary)
        {
            if (Grade == 2 && !IndexScalarDictionary.IsEmpty())
            {
                indexScalarDictionary = IndexScalarDictionary;
                return true;
            }

            indexScalarDictionary = GaEvenDictionaryEmpty<T>.DefaultDictionary;
            return false;
        }

        public bool TryGetKVectorPartDictionary(uint grade, out IGaEvenDictionary<T> indexScalarDictionary)
        {
            if (Grade == grade && !IndexScalarDictionary.IsEmpty())
            {
                indexScalarDictionary = IndexScalarDictionary;
                return true;
            }

            indexScalarDictionary = GaEvenDictionaryEmpty<T>.DefaultDictionary;
            return false;
        }

        public IGaEvenDictionary<T> GetIdScalarDictionary()
        {
            return IndexScalarDictionary.MapKeys(
                index => GaBasisUtils.BasisBladeId(Grade, index)
            );
        }

        public IGaGradedDictionary<T> GetGradeIndexScalarDictionary()
        {
            return IndexScalarDictionary.CreateGradedDictionarySingleKey(Grade);
        }

        public bool TryGetKVectorDictionary(uint grade, out IGaEvenDictionary<T> indexScalarDictionary)
        {
            if (grade == Grade)
            {
                indexScalarDictionary = IndexScalarDictionary;
                return true;
            }

            indexScalarDictionary = null;
            return false;
        }

        public IEnumerable<ulong> GetIds()
        {
            return IndexScalarDictionary.Keys.Select(
                index => GaBasisUtils.BasisBladeId(Grade, index)
            );
        }

        public IEnumerable<ulong> GetIndices()
        {
            return IndexScalarDictionary.Keys;
        }
        
        public IEnumerable<Tuple<uint, ulong>> GetGradeIndexTuples()
        {
            return IndexScalarDictionary.Keys.Select(index => new Tuple<uint, ulong>(Grade, index));
        }

        public abstract IEnumerable<IGaBasisBlade> GetBasisBlades();

        public IEnumerable<T> GetScalars()
        {
            return IndexScalarDictionary.Values;
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetIdScalarPairs()
        {
            return IndexScalarDictionary.Select(
                pair => new KeyValuePair<ulong, T>(
                    GaBasisUtils.BasisBladeId(Grade, pair.Key),
                    pair.Value
                )
            );
        }

        public IEnumerable<Tuple<ulong, T>> GetIdScalarTuples()
        {
            return IndexScalarDictionary.Select(
                pair => new Tuple<ulong, T>(
                    GaBasisUtils.BasisBladeId(Grade, pair.Key),
                    pair.Value
                )
            );
        }

        public IEnumerable<Tuple<uint, ulong, T>> GetGradeIndexScalarTuples()
        {
            return IndexScalarDictionary
                .Select(pair => new Tuple<uint, ulong, T>(Grade, pair.Key, pair.Value));
        }

        public abstract IEnumerable<GaTerm<T>> GetTerms();
        
        public abstract IEnumerable<GaTerm<T>> GetTerms(Func<ulong, bool> idSelection);
        
        public abstract IEnumerable<GaTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection);
        
        public abstract IEnumerable<GaTerm<T>> GetTerms(Func<T, bool> scalarSelection);
        
        public abstract IEnumerable<GaTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection);
        
        public abstract IEnumerable<GaTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection);

        public abstract IGaStorageKVector<T> GetComputedCopy(Func<T, T> mappingFunc);

        public abstract IGaStorageKVector<T> GetComputedCopy(Func<ulong, T, T> mappingFunc);

        public abstract IGaStorageKVector<T> GetComputedCopy(Func<uint, ulong, T, T> mappingFunc);


        //public IGaStorageKVector<T> GetKVectorStorage()
        //{
        //    return ScalarProcessor.CreateStorageKVector(Grade,
        //        IndexScalarDictionary
        //    );
        //}

        //public IGaStorageKVector<T> GetKVectorStorageCopy()
        //{
        //    return ScalarProcessor.CreateStorageKVector(Grade,
        //        IndexScalarDictionary.CopyToDictionary()
        //    );
        //}

        //public IGaStorageKVector<T> GetKVectorStorageCopy(Func<T, T> scalarMapping)
        //{
        //    return ScalarProcessor.CreateStorageKVector(Grade,
        //        IndexScalarDictionary.CopyToDictionary(scalarMapping)
        //    );
        //}

        public IGaStorageMultivectorGraded<T> GetGradedMultivectorCopy()
        {
            var gradeIndexScalarDictionary = 
                IndexScalarDictionary.GetCopy().CreateGradedDictionarySingleKey(Grade);

            return GaStorageMultivectorGraded<T>.Create(
                gradeIndexScalarDictionary
            );
        }

        public IGaStorageMultivectorSparse<T> GetSparseMultivectorCopy()
        {
            var idScalarDictionary =
                IndexScalarDictionary.MapKeys(GaBasisUtils.BasisBladeIndex);

            return GaStorageMultivectorSparse<T>.Create(
                idScalarDictionary, 
                MaxBasisBladeId
            );
        }
        

        public GaEvenDictionaryTree<T> GetBinaryTree(int treeDepth)
        {
            if (treeDepth < VSpaceDimension)
                throw new InvalidOperationException();

            var dict = GetIdScalarPairs()
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                );

            return new GaEvenDictionaryTree<T>(treeDepth, dict);
        }

        public IGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity, IGaScalarProcessor<T> scalarProcessor)
        {
            //return GaGbtKVectorStorageStack1<T>.Create(capacity, treeDepth, this);
            //return GaGbtMultivectorStorageGradedStack1<T>.Create(capacity, treeDepth, this);
            return GaGbtMultivectorStorageUniformStack1<T>.Create(
                capacity, 
                treeDepth, 
                scalarProcessor,
                this
            );
        }


        public IGaStorageMultivector<T> GetCompactStorage()
        {
            return this;
        }

        public IGaStorageMultivectorGraded<T> GetCompactGradedStorage()
        {
            return this;
        }


        public IGaStorageMultivectorSparse<T> ToSparseMultivector()
        {
            var idScalarDictionary =
                IndexScalarDictionary.MapKeys(GaBasisUtils.BasisBladeIndex);

            return GaStorageMultivectorSparse<T>.Create(
                idScalarDictionary, 
                MaxBasisBladeId
            );
        }

        public IGaStorageMultivectorGraded<T> ToGradedMultivector()
        {
            return this;
        }

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