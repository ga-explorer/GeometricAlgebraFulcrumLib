using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Structures;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;
using GeometricAlgebraFulcrumLib.TextComposers;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public abstract record GaStorageKVectorBase<T> 
        : IGaStorageKVector<T>
    {
        public abstract IGaListGradedSingleGrade<T> SingleGradeIndexScalarList { get; }

        public IGaListGraded<T> GradeIndexScalarList 
            => SingleGradeIndexScalarList;
        
        public IGaListEven<T> IndexScalarList 
            => SingleGradeIndexScalarList.EvenList;

        public abstract uint MinVSpaceDimension { get; }

        public int GradesCount 
            => 1;

        public uint Grade
            => SingleGradeIndexScalarList.Grade;

        
        public int TermsCount 
            => IndexScalarList.GetSparseCount();

        
        public ulong GetMinIndex()
        {
            return IndexScalarList.GetMinKey();
        }

        public ulong GetMaxIndex()
        {
            return IndexScalarList.GetMaxKey();
        }

        public bool ContainsTerm(ulong id)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            return Grade == grade && IndexScalarList.ContainsKey(index);
        }

        public bool ContainsTerm(uint grade, ulong index)
        {
            return grade == Grade && IndexScalarList.ContainsKey(index);
        }

        public bool ContainsScalarPart()
        {
            return Grade == 0 && !SingleGradeIndexScalarList.IsEmpty();
        }

        public bool ContainsVectorPart()
        {
            return Grade == 1 && !SingleGradeIndexScalarList.IsEmpty();
        }

        public bool ContainsBivectorPart()
        {
            return Grade == 2 && !SingleGradeIndexScalarList.IsEmpty();
        }

        public bool ContainsKVectorPart(uint grade)
        {
            return Grade == grade && !SingleGradeIndexScalarList.IsEmpty();
        }

        public bool ContainsTermWithIndex(ulong index)
        {
            return SingleGradeIndexScalarList.ContainsKey(Grade, index);
        }

        public bool IsEmpty()
        {
            return IndexScalarList.IsEmpty();
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

        public ulong GetMinId()
        {
            return IndexScalarList.GetMinKey().BasisBladeIndexToId(Grade);
        }

        public ulong GetMaxId()
        {
            return IndexScalarList.GetMaxKey().BasisBladeIndexToId(Grade);
        }

        public ulong GetMinId(uint grade)
        {
            return grade == Grade
                ? IndexScalarList.GetMinKey()
                : throw new InvalidOperationException();
        }

        public ulong GetMaxId(uint grade)
        {
            return grade == Grade
                ? IndexScalarList.GetMaxKey()
                : throw new InvalidOperationException();
        }

        public uint GetMinGrade()
        {
            return Grade;
        }

        public uint GetMaxGrade()
        {
            return Grade;
        }

        public ulong GetMinIndex(uint grade)
        {
            return IndexScalarList.GetMinKey();
        }

        public ulong GetMaxIndex(uint grade)
        {
            return IndexScalarList.GetMaxKey();
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
            if (IndexScalarList.TryGetValue(index, out value))
                return true;

            value = default;
            return false;
        }

        public abstract bool TryGetScalar(out T value);

        public bool TryGetTermScalar(ulong id, out T value)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            if (Grade == grade && IndexScalarList.TryGetValue(index, out value))
                return true;

            value = default;
            return false;
        }

        public bool TryGetTermScalar(uint grade, ulong index, out T value)
        {
            if (grade == Grade) 
                return IndexScalarList.TryGetValue(index, out value);

            value = default;
            return false;
        }


        public abstract bool TryGetTermByIndex(int index, out GaBasisTerm<T> term);

        public abstract bool TryGetTermByIndex(ulong index, out GaBasisTerm<T> term);


        public IGaStorageKVector<T> GetKVectorCopy()
        {
            return this switch
            {
                GaStorageScalar<T> scalar => scalar.GetScalarCopy(),
                GaStorageVector<T> vector => vector.GetVectorCopy(),
                GaStorageBivector<T> bivector => bivector.GetBivectorCopy(),
                _ => 
                    GaStorageKVector<T>.Create(
                        Grade, 
                        IndexScalarList.GetCopy()
                    )
            };
        }

        public IGaStorageKVector<T2> MapKVectorScalars<T2>(Func<T, T2> scalarMapping)
        {
            return this switch
            {
                GaStorageScalar<T> scalar => scalar.MapScalar(scalarMapping),
                GaStorageVector<T> vector => vector.MapVectorScalars(scalarMapping),
                GaStorageBivector<T> bivector => bivector.MapBivectorScalars(scalarMapping),
                _ => 
                    GaStorageKVector<T2>.Create(
                        Grade,
                        IndexScalarList.MapValues(scalarMapping)
                    )
            };
        }

        public IGaStorageKVector<T2> MapKVectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            return this switch
            {
                GaStorageScalar<T> scalar => scalar.MapScalarById(idScalarMapping),
                GaStorageVector<T> vector => vector.MapVectorScalarsById(idScalarMapping),
                GaStorageBivector<T> bivector => bivector.MapBivectorScalarsById(idScalarMapping),
                _ => 
                    GaStorageKVector<T2>.Create(
                        Grade,
                        IndexScalarList.MapValues(
                            (index, scalar) => idScalarMapping(index.BasisBladeIndexToId(Grade), scalar)
                        )
                    )
            };
        }

        public IGaStorageKVector<T2> MapKVectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            return this switch
            {
                GaStorageScalar<T> scalar => scalar.MapScalarByIndex(indexScalarMapping),
                GaStorageVector<T> vector => vector.MapVectorScalarsByIndex(indexScalarMapping),
                GaStorageBivector<T> bivector => bivector.MapBivectorScalarsByIndex(indexScalarMapping),
                _ => 
                    GaStorageKVector<T2>.Create(
                        Grade,
                        IndexScalarList.MapValues(indexScalarMapping)
                    )
            };
        }

        public IGaStorageKVector<T2> MapKVectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return this switch
            {
                GaStorageScalar<T> scalar => scalar.MapScalarByGradeIndex(gradeIndexScalarMapping),
                GaStorageVector<T> vector => vector.MapVectorScalarsByGradeIndex(gradeIndexScalarMapping),
                GaStorageBivector<T> bivector => bivector.MapBivectorScalarsByGradeIndex(gradeIndexScalarMapping),
                _ => 
                    GaStorageKVector<T2>.Create(
                        Grade,
                        IndexScalarList.MapValues(
                            (index, scalar) => gradeIndexScalarMapping(Grade, index, scalar)
                        )
                    )
            };
        }


        
        public IGaStorageMultivectorGraded<T> GetGradedMultivectorCopy()
        {
            return GetKVectorCopy();
        }

        public IGaStorageMultivectorGraded<T2> MapGradedMultivectorScalars<T2>(Func<T, T2> scalarMapping)
        {
            return MapKVectorScalars(scalarMapping);
        }

        public IGaStorageMultivectorGraded<T2> MapGradedMultivectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            return MapKVectorScalarsByIndex(
                (index, scalar) => 
                    idScalarMapping(index.BasisBladeIndexToId(Grade), scalar)
                );
        }

        public IGaStorageMultivectorGraded<T2> MapGradedMultivectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            return MapKVectorScalarsByIndex(indexScalarMapping);
        }

        public IGaStorageMultivectorGraded<T2> MapGradedMultivectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return MapKVectorScalarsByIndex(
                (index, scalar) => 
                    gradeIndexScalarMapping(Grade, index, scalar)
            );
        }


        public IGaStorageKVector<T> FilterKVectorByScalar(Func<T, bool> scalarFilter)
        {
            return this switch
            {
                GaStorageScalar<T> scalar => scalar.FilterScalarByScalar(scalarFilter),
                GaStorageVector<T> vector => vector.FilterVectorByScalar(scalarFilter),
                GaStorageBivector<T> bivector => bivector.FilterBivectorByScalar(scalarFilter),
                _ => 
                    GaStorageKVector<T>.Create(
                        Grade,
                        IndexScalarList.FilterByValue(scalarFilter)
                    )
            };
        }

        public IGaStorageKVector<T> FilterKVectorByIndexScalar(Func<ulong, T, bool> indexScalarFilter)
        {
            return this switch
            {
                GaStorageScalar<T> scalar => scalar.FilterScalarByIndexScalar(indexScalarFilter),
                GaStorageVector<T> vector => vector.FilterVectorByIndexScalar(indexScalarFilter),
                GaStorageBivector<T> bivector => bivector.FilterBivectorByIndexScalar(indexScalarFilter),
                _ => 
                    GaStorageKVector<T>.Create(
                        Grade,
                        IndexScalarList.FilterByKeyValue(indexScalarFilter)
                    )
            };
        }

        public IGaStorageKVector<T> FilterKVectorByIndex(Func<ulong, bool> indexFilter)
        {
            return this switch
            {
                GaStorageScalar<T> scalar => scalar.FilterScalarByIndex(indexFilter),
                GaStorageVector<T> vector => vector.FilterVectorByIndex(indexFilter),
                GaStorageBivector<T> bivector => bivector.FilterBivectorByIndex(indexFilter),
                _ => 
                    GaStorageKVector<T>.Create(
                        Grade,
                        IndexScalarList.FilterByKey(indexFilter)
                    )
            };
        }


        public abstract bool TryGetTerm(ulong id, out GaBasisTerm<T> term);

        public abstract bool TryGetTerm(uint grade, ulong index, out GaBasisTerm<T> term);

        public IGaListEven<T> GetScalarPartList()
        {
            return Grade == 0 && !IndexScalarList.IsEmpty()
                ? IndexScalarList
                : GaListEvenEmpty<T>.EmptyList;
        }

        public IGaListEven<T> GetVectorPartList()
        {
            return Grade == 1 && !IndexScalarList.IsEmpty()
                ? IndexScalarList
                : GaListEvenEmpty<T>.EmptyList;
        }

        public IGaListEven<T> GetBivectorPartList()
        {
            return Grade == 2 && !IndexScalarList.IsEmpty()
                ? IndexScalarList
                : GaListEvenEmpty<T>.EmptyList;
        }

        public IGaListEven<T> GetKVectorPartList(uint grade)
        {
            return Grade == grade && !IndexScalarList.IsEmpty()
                ? IndexScalarList
                : GaListEvenEmpty<T>.EmptyList;
        }

        public bool TryGetScalarPart(out IGaStorageScalar<T> scalar)
        {
            if (Grade == 0 && this is IGaStorageScalar<T> scalarStorage)
            {
                scalar = scalarStorage;
                return true;
            }

            scalar = null;
            return false;
        }

        public bool TryGetVectorPart(out IGaStorageVector<T> vector)
        {
            if (Grade == 1 && this is IGaStorageVector<T> vectorStorage)
            {
                vector = vectorStorage;
                return true;
            }

            vector = null;
            return false;
        }

        public bool TryGetBivectorPart(out IGaStorageBivector<T> bivector)
        {
            if (Grade == 2 && this is IGaStorageBivector<T> bivectorStorage)
            {
                bivector = bivectorStorage;
                return true;
            }

            bivector = null;
            return false;
        }

        public bool TryGetKVectorPart(uint grade, out IGaStorageKVector<T> kVector)
        {
            if (Grade == grade)
            {
                kVector = this;
                return true;
            }

            kVector = null;
            return false;
        }


        public bool TryGetScalarPartList(out IGaListEven<T> indexScalarList)
        {
            if (Grade == 0 && !IndexScalarList.IsEmpty())
            {
                indexScalarList = IndexScalarList;
                return true;
            }

            indexScalarList = null;
            return false;
        }

        public bool TryGetVectorPartList(out IGaListEven<T> indexScalarList)
        {
            if (Grade == 1 && !IndexScalarList.IsEmpty())
            {
                indexScalarList = IndexScalarList;
                return true;
            }

            indexScalarList = null;
            return false;
        }

        public bool TryGetBivectorPartList(out IGaListEven<T> indexScalarList)
        {
            if (Grade == 2 && !IndexScalarList.IsEmpty())
            {
                indexScalarList = IndexScalarList;
                return true;
            }

            indexScalarList = GaListEvenEmpty<T>.EmptyList;
            return false;
        }

        public bool TryGetKVectorPartList(uint grade, out IGaListEven<T> indexScalarList)
        {
            if (Grade == grade && !IndexScalarList.IsEmpty())
            {
                indexScalarList = IndexScalarList;
                return true;
            }

            indexScalarList = GaListEvenEmpty<T>.EmptyList;
            return false;
        }

        public IGaListEven<T> GetIdScalarList()
        {
            return IndexScalarList.MapKeys(
                index => index.BasisBladeIndexToId(Grade)
            );
        }

        public IGaListGraded<T> GetGradeIndexScalarList()
        {
            return SingleGradeIndexScalarList;
        }

        public IGaListEven<T> GetIndexScalarList(uint grade)
        {
            return grade == Grade
                ? IndexScalarList 
                : GaListEvenEmpty<T>.EmptyList;
        }

        public IEnumerable<ulong> GetIds()
        {
            return IndexScalarList.GetKeys().Select(
                index => index.BasisBladeIndexToId(Grade)
            );
        }

        public IEnumerable<ulong> GetIndices()
        {
            return IndexScalarList.GetKeys();
        }
        
        public IEnumerable<GaRecordGradeKey> GetGradeIndexRecords()
        {
            return IndexScalarList.GetKeys().Select(index => new GaRecordGradeKey(Grade, index));
        }

        public abstract IEnumerable<GaBasisBlade> GetBasisBlades();

        public IEnumerable<T> GetScalars()
        {
            return IndexScalarList.GetValues();
        }

        public IEnumerable<GaRecordKeyValue<T>> GetIdScalarRecords()
        {
            return IndexScalarList.GetKeyValueRecords().Select(
                pair => new GaRecordKeyValue<T>(
                    pair.Key.BasisBladeIndexToId(Grade),
                    pair.Value
                )
            );
        }

        public IEnumerable<GaRecordKeyValue<T>> GetIndexScalarRecords(uint grade)
        {
            return grade == Grade
                ? IndexScalarList.GetKeyValueRecords()
                : Enumerable.Empty<GaRecordKeyValue<T>>();
        }

        public IEnumerable<GaRecordGradeKeyValue<T>> GetGradeIndexScalarRecords()
        {
            return SingleGradeIndexScalarList.GetGradeKeyValueRecords();
        }

        public abstract IEnumerable<GaBasisTerm<T>> GetTerms();
        
        public abstract IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, bool> idSelection);
        
        public abstract IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection);
        
        public abstract IEnumerable<GaBasisTerm<T>> GetTerms(Func<T, bool> scalarSelection);
        
        public abstract IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection);
        
        public abstract IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection);


        public GaListEvenTree<T> GetBinaryTree(int treeDepth)
        {
            if (treeDepth < MinVSpaceDimension)
                throw new InvalidOperationException();

            var dict = GetIdScalarRecords()
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                );

            return new GaListEvenTree<T>(treeDepth, dict);
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


        public IGaStorageMultivectorSparse<T> ToSparseMultivector()
        {
            var idScalarDictionary =
                IndexScalarList.MapKeys(GaBasisBladeUtils.BasisBladeIdToIndex);

            return GaStorageMultivectorSparse<T>.Create(
                idScalarDictionary
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