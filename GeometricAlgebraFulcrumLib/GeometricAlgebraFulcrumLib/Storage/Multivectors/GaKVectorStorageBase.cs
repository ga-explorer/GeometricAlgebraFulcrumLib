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
    public abstract record GaKVectorStorageBase<T> 
        : IGaKVectorStorage<T>
    {
        public abstract ILaVectorSingleGradeStorage<T> SingleGradeIndexScalarList { get; }

        public ILaVectorGradedStorage<T> GradeIndexScalarList 
            => SingleGradeIndexScalarList;
        
        public ILaVectorEvenStorage<T> IndexScalarList 
            => SingleGradeIndexScalarList.EvenStorage;

        public abstract uint MinVSpaceDimension { get; }

        public int GradesCount 
            => 1;

        public uint Grade
            => SingleGradeIndexScalarList.Grade;

        
        public int TermsCount 
            => IndexScalarList.GetSparseCount();

        
        public ulong GetMinIndex()
        {
            return IndexScalarList.GetMinIndex();
        }

        public ulong GetMaxIndex()
        {
            return IndexScalarList.GetMaxIndex();
        }

        public bool ContainsTerm(ulong id)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            return Grade == grade && IndexScalarList.ContainsIndex(index);
        }

        public bool ContainsTerm(uint grade, ulong index)
        {
            return grade == Grade && IndexScalarList.ContainsIndex(index);
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
            return SingleGradeIndexScalarList.ContainsIndex(Grade, index);
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
            return IndexScalarList.GetMinIndex().BasisBladeIndexToId(Grade);
        }

        public ulong GetMaxId()
        {
            return IndexScalarList.GetMaxIndex().BasisBladeIndexToId(Grade);
        }

        public ulong GetMinId(uint grade)
        {
            return grade == Grade
                ? IndexScalarList.GetMinIndex()
                : throw new InvalidOperationException();
        }

        public ulong GetMaxId(uint grade)
        {
            return grade == Grade
                ? IndexScalarList.GetMaxIndex()
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
            return IndexScalarList.GetMinIndex();
        }

        public ulong GetMaxIndex(uint grade)
        {
            return IndexScalarList.GetMaxIndex();
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
            if (IndexScalarList.TryGetScalar(index, out value))
                return true;

            value = default;
            return false;
        }

        public abstract bool TryGetScalar(out T value);

        public bool TryGetTermScalar(ulong id, out T value)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            if (Grade == grade && IndexScalarList.TryGetScalar(index, out value))
                return true;

            value = default;
            return false;
        }

        public bool TryGetTermScalar(uint grade, ulong index, out T value)
        {
            if (grade == Grade) 
                return IndexScalarList.TryGetScalar(index, out value);

            value = default;
            return false;
        }


        public abstract bool TryGetTermByIndex(int index, out GaBasisTerm<T> term);

        public abstract bool TryGetTermByIndex(ulong index, out GaBasisTerm<T> term);


        public IGaKVectorStorage<T> GetKVectorCopy()
        {
            return this switch
            {
                GaScalarStorage<T> scalar => scalar.GetScalarCopy(),
                GaVectorStorage<T> vector => vector.GetVectorCopy(),
                GaBivectorStorage<T> bivector => bivector.GetBivectorCopy(),
                _ => 
                    GaKVectorStorage<T>.Create(
                        Grade, 
                        IndexScalarList.GetCopy()
                    )
            };
        }

        public IGaKVectorStorage<T2> MapKVectorScalars<T2>(Func<T, T2> scalarMapping)
        {
            return this switch
            {
                GaScalarStorage<T> scalar => scalar.MapScalar(scalarMapping),
                GaVectorStorage<T> vector => vector.MapVectorScalars(scalarMapping),
                GaBivectorStorage<T> bivector => bivector.MapBivectorScalars(scalarMapping),
                _ => 
                    GaKVectorStorage<T2>.Create(
                        Grade,
                        IndexScalarList.MapScalars(scalarMapping)
                    )
            };
        }

        public IGaKVectorStorage<T2> MapKVectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            return this switch
            {
                GaScalarStorage<T> scalar => scalar.MapScalarById(idScalarMapping),
                GaVectorStorage<T> vector => vector.MapVectorScalarsById(idScalarMapping),
                GaBivectorStorage<T> bivector => bivector.MapBivectorScalarsById(idScalarMapping),
                _ => 
                    GaKVectorStorage<T2>.Create(
                        Grade,
                        IndexScalarList.MapScalars(
                            (index, scalar) => idScalarMapping(index.BasisBladeIndexToId(Grade), scalar)
                        )
                    )
            };
        }

        public IGaKVectorStorage<T2> MapKVectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            return this switch
            {
                GaScalarStorage<T> scalar => scalar.MapScalarByIndex(indexScalarMapping),
                GaVectorStorage<T> vector => vector.MapVectorScalarsByIndex(indexScalarMapping),
                GaBivectorStorage<T> bivector => bivector.MapBivectorScalarsByIndex(indexScalarMapping),
                _ => 
                    GaKVectorStorage<T2>.Create(
                        Grade,
                        IndexScalarList.MapScalars(indexScalarMapping)
                    )
            };
        }

        public IGaKVectorStorage<T2> MapKVectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return this switch
            {
                GaScalarStorage<T> scalar => scalar.MapScalarByGradeIndex(gradeIndexScalarMapping),
                GaVectorStorage<T> vector => vector.MapVectorScalarsByGradeIndex(gradeIndexScalarMapping),
                GaBivectorStorage<T> bivector => bivector.MapBivectorScalarsByGradeIndex(gradeIndexScalarMapping),
                _ => 
                    GaKVectorStorage<T2>.Create(
                        Grade,
                        IndexScalarList.MapScalars(
                            (index, scalar) => gradeIndexScalarMapping(Grade, index, scalar)
                        )
                    )
            };
        }


        
        public IGaMultivectorGradedStorage<T> GetGradedMultivectorCopy()
        {
            return GetKVectorCopy();
        }

        public IGaMultivectorGradedStorage<T2> MapGradedMultivectorScalars<T2>(Func<T, T2> scalarMapping)
        {
            return MapKVectorScalars(scalarMapping);
        }

        public IGaMultivectorGradedStorage<T2> MapGradedMultivectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            return MapKVectorScalarsByIndex(
                (index, scalar) => 
                    idScalarMapping(index.BasisBladeIndexToId(Grade), scalar)
                );
        }

        public IGaMultivectorGradedStorage<T2> MapGradedMultivectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            return MapKVectorScalarsByIndex(indexScalarMapping);
        }

        public IGaMultivectorGradedStorage<T2> MapGradedMultivectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return MapKVectorScalarsByIndex(
                (index, scalar) => 
                    gradeIndexScalarMapping(Grade, index, scalar)
            );
        }


        public IGaKVectorStorage<T> FilterKVectorByScalar(Func<T, bool> scalarFilter)
        {
            return this switch
            {
                GaScalarStorage<T> scalar => scalar.FilterScalarByScalar(scalarFilter),
                GaVectorStorage<T> vector => vector.FilterVectorByScalar(scalarFilter),
                GaBivectorStorage<T> bivector => bivector.FilterBivectorByScalar(scalarFilter),
                _ => 
                    GaKVectorStorage<T>.Create(
                        Grade,
                        IndexScalarList.FilterByScalar(scalarFilter)
                    )
            };
        }

        public IGaKVectorStorage<T> FilterKVectorByIndexScalar(Func<ulong, T, bool> indexScalarFilter)
        {
            return this switch
            {
                GaScalarStorage<T> scalar => scalar.FilterScalarByIndexScalar(indexScalarFilter),
                GaVectorStorage<T> vector => vector.FilterVectorByIndexScalar(indexScalarFilter),
                GaBivectorStorage<T> bivector => bivector.FilterBivectorByIndexScalar(indexScalarFilter),
                _ => 
                    GaKVectorStorage<T>.Create(
                        Grade,
                        IndexScalarList.FilterByIndexScalar(indexScalarFilter)
                    )
            };
        }

        public IGaKVectorStorage<T> FilterKVectorByIndex(Func<ulong, bool> indexFilter)
        {
            return this switch
            {
                GaScalarStorage<T> scalar => scalar.FilterScalarByIndex(indexFilter),
                GaVectorStorage<T> vector => vector.FilterVectorByIndex(indexFilter),
                GaBivectorStorage<T> bivector => bivector.FilterBivectorByIndex(indexFilter),
                _ => 
                    GaKVectorStorage<T>.Create(
                        Grade,
                        IndexScalarList.FilterByIndex(indexFilter)
                    )
            };
        }


        public abstract bool TryGetTerm(ulong id, out GaBasisTerm<T> term);

        public abstract bool TryGetTerm(uint grade, ulong index, out GaBasisTerm<T> term);

        public ILaVectorEvenStorage<T> GetScalarPartList()
        {
            return Grade == 0 && !IndexScalarList.IsEmpty()
                ? IndexScalarList
                : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        public ILaVectorEvenStorage<T> GetVectorPartList()
        {
            return Grade == 1 && !IndexScalarList.IsEmpty()
                ? IndexScalarList
                : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        public ILaVectorEvenStorage<T> GetBivectorPartList()
        {
            return Grade == 2 && !IndexScalarList.IsEmpty()
                ? IndexScalarList
                : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        public ILaVectorEvenStorage<T> GetKVectorPartList(uint grade)
        {
            return Grade == grade && !IndexScalarList.IsEmpty()
                ? IndexScalarList
                : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        public bool TryGetScalarPart(out IGaScalarStorage<T> scalar)
        {
            if (Grade == 0 && this is IGaScalarStorage<T> scalarStorage)
            {
                scalar = scalarStorage;
                return true;
            }

            scalar = null;
            return false;
        }

        public bool TryGetVectorPart(out IGaVectorStorage<T> vector)
        {
            if (Grade == 1 && this is IGaVectorStorage<T> vectorStorage)
            {
                vector = vectorStorage;
                return true;
            }

            vector = null;
            return false;
        }

        public bool TryGetBivectorPart(out IGaBivectorStorage<T> bivector)
        {
            if (Grade == 2 && this is IGaBivectorStorage<T> bivectorStorage)
            {
                bivector = bivectorStorage;
                return true;
            }

            bivector = null;
            return false;
        }

        public bool TryGetKVectorPart(uint grade, out IGaKVectorStorage<T> kVector)
        {
            if (Grade == grade)
            {
                kVector = this;
                return true;
            }

            kVector = null;
            return false;
        }


        public bool TryGetScalarPartList(out ILaVectorEvenStorage<T> indexScalarList)
        {
            if (Grade == 0 && !IndexScalarList.IsEmpty())
            {
                indexScalarList = IndexScalarList;
                return true;
            }

            indexScalarList = null;
            return false;
        }

        public bool TryGetVectorPartList(out ILaVectorEvenStorage<T> indexScalarList)
        {
            if (Grade == 1 && !IndexScalarList.IsEmpty())
            {
                indexScalarList = IndexScalarList;
                return true;
            }

            indexScalarList = null;
            return false;
        }

        public bool TryGetBivectorPartList(out ILaVectorEvenStorage<T> indexScalarList)
        {
            if (Grade == 2 && !IndexScalarList.IsEmpty())
            {
                indexScalarList = IndexScalarList;
                return true;
            }

            indexScalarList = LaVectorEmptyStorage<T>.ZeroStorage;
            return false;
        }

        public bool TryGetKVectorPartList(uint grade, out ILaVectorEvenStorage<T> indexScalarList)
        {
            if (Grade == grade && !IndexScalarList.IsEmpty())
            {
                indexScalarList = IndexScalarList;
                return true;
            }

            indexScalarList = LaVectorEmptyStorage<T>.ZeroStorage;
            return false;
        }

        public ILaVectorEvenStorage<T> GetIdScalarList()
        {
            return IndexScalarList.MapIndices(
                index => index.BasisBladeIndexToId(Grade)
            );
        }

        public ILaVectorGradedStorage<T> GetGradeIndexScalarList()
        {
            return SingleGradeIndexScalarList;
        }

        public ILaVectorEvenStorage<T> GetIndexScalarList(uint grade)
        {
            return grade == Grade
                ? IndexScalarList 
                : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        public IEnumerable<ulong> GetIds()
        {
            return IndexScalarList.GetIndices().Select(
                index => index.BasisBladeIndexToId(Grade)
            );
        }

        public IEnumerable<ulong> GetIndices()
        {
            return IndexScalarList.GetIndices();
        }
        
        public IEnumerable<GradeIndexRecord> GetGradeIndexRecords()
        {
            return IndexScalarList.GetIndices().Select(index => new GradeIndexRecord(Grade, index));
        }

        public abstract IEnumerable<GaBasisBlade> GetBasisBlades();

        public IEnumerable<T> GetScalars()
        {
            return IndexScalarList.GetScalars();
        }

        public IEnumerable<IndexScalarRecord<T>> GetIdScalarRecords()
        {
            return IndexScalarList.GetIndexScalarRecords().Select(
                pair => new IndexScalarRecord<T>(
                    pair.Index.BasisBladeIndexToId(Grade),
                    pair.Scalar
                )
            );
        }

        public IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords(uint grade)
        {
            return grade == Grade
                ? IndexScalarList.GetIndexScalarRecords()
                : Enumerable.Empty<IndexScalarRecord<T>>();
        }

        public IEnumerable<GradeIndexScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            return SingleGradeIndexScalarList.GetGradeIndexScalarRecords();
        }

        public abstract IEnumerable<GaBasisTerm<T>> GetTerms();
        
        public abstract IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, bool> idSelection);
        
        public abstract IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection);
        
        public abstract IEnumerable<GaBasisTerm<T>> GetTerms(Func<T, bool> scalarSelection);
        
        public abstract IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection);
        
        public abstract IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection);


        public LaVectorTreeStorage<T> GetBinaryTree(int treeDepth)
        {
            if (treeDepth < MinVSpaceDimension)
                throw new InvalidOperationException();

            var dict = GetIdScalarRecords()
                .ToDictionary(
                    pair => pair.Index,
                    pair => pair.Scalar
                );

            return new LaVectorTreeStorage<T>(treeDepth, dict);
        }

        public IGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity, IScalarProcessor<T> scalarProcessor)
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


        public IGaMultivectorSparseStorage<T> ToSparseMultivector()
        {
            var idScalarDictionary =
                IndexScalarList.MapIndices(GaBasisBladeUtils.BasisBladeIdToIndex);

            return GaMultivectorSparseStorage<T>.Create(
                idScalarDictionary
            );
        }

        public IGaMultivectorGradedStorage<T> ToGradedMultivector()
        {
            return this;
        }

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