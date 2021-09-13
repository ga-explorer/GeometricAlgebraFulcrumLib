using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors
{
    /// <summary>
    /// Can store the scalar coefficients of a k-vector of any dimension.
    /// The scalars are assumed to be of immutable type such as T, complex, etc.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public record KVectorStorage<T> 
        : IMultivectorGradedStorage<T>
    {
        public static KVectorStorage<T> ZeroScalar { get; }
            = new KVectorStorage<T>(0);

        public static VectorStorage<T> ZeroVector { get; }
            = new VectorStorage<T>();

        public static BivectorStorage<T> ZeroBivector { get; }
            = new BivectorStorage<T>();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorZero(uint grade)
        {
            return grade switch
            {
                0 => ZeroScalar,
                1 => ZeroVector,
                2 => ZeroBivector,
                _ => new KVectorStorage<T>(grade)
            };
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorScalar(T scalar)
        {
            return new KVectorStorage<T>(
                new LinVectorSingleScalarGradedStorage<T>(0, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVector(ulong id, T scalar)
        {
            var (grade, index) = id.BasisBladeIdToGradeIndex();

            return grade switch
            {
                0 => index == 0 ? CreateKVectorScalar(scalar) : throw new InvalidOperationException(),
                1 => VectorStorage<T>.CreateVector(index, scalar),
                2 => BivectorStorage<T>.CreateBivector(index, scalar),
                _ => new KVectorStorage<T>(new LinVectorSingleScalarGradedStorage<T>(grade, index, scalar))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVector(uint grade, ulong index, T scalar)
        {
            return grade switch
            {
                0 => index == 0 ? CreateKVectorScalar(scalar) : throw new InvalidOperationException(),
                1 => VectorStorage<T>.CreateVector(index, scalar),
                2 => BivectorStorage<T>.CreateBivector(index, scalar),
                _ => new KVectorStorage<T>(new LinVectorSingleScalarGradedStorage<T>(grade, index, scalar))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVector(uint grade, params T[] indexScalarList)
        {
            return grade switch
            {
                0 => indexScalarList.Length > 1
                    ? throw new InvalidOperationException()
                    : indexScalarList.Length == 1
                        ? new KVectorStorage<T>(new LinVectorSingleScalarGradedStorage<T>(0, indexScalarList[0]))
                        : ZeroScalar,

                1 => VectorStorage<T>.CreateVector(indexScalarList),

                2 => BivectorStorage<T>.CreateBivector(indexScalarList),

                _ => indexScalarList.Length switch
                {
                    0 => CreateKVectorZero(grade),
                    1 => new KVectorStorage<T>(
                        new LinVectorSingleScalarGradedStorage<T>(grade, indexScalarList[0])),
                    _ => new KVectorStorage<T>(indexScalarList.CreateLinVectorDenseStorage()
                        .CreateLinVectorSingleGradeStorage(grade))
                }
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVector(uint grade, IReadOnlyList<T> indexScalarList)
        {
            var count = indexScalarList.Count;

            return grade switch
            {
                0 => count > 1
                    ? throw new InvalidOperationException()
                    : count == 1
                        ? new KVectorStorage<T>(new LinVectorSingleScalarGradedStorage<T>(0, indexScalarList[0]))
                        : ZeroScalar,

                1 => VectorStorage<T>.CreateVector(indexScalarList),

                2 => BivectorStorage<T>.CreateBivector(indexScalarList),

                _ => count switch
                {
                    0 => CreateKVectorZero(grade),
                    1 => new KVectorStorage<T>(
                        new LinVectorSingleScalarGradedStorage<T>(grade, indexScalarList[0])),
                    _ => new KVectorStorage<T>(indexScalarList.CreateLinVectorDenseStorage()
                        .CreateLinVectorSingleGradeStorage(grade))
                }
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVector(uint grade, IEnumerable<T> indexScalarList)
        {
            return CreateKVector(grade, indexScalarList.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVector(uint grade, Dictionary<ulong, T> indexScalarDictionary)
        {
            if (grade == 0)
            {
                if (indexScalarDictionary.Count == 0)
                    return ZeroScalar;

                if (indexScalarDictionary.Count > 1)
                    throw new InvalidOperationException();

                var (index, scalar) = indexScalarDictionary.First();

                if (index > 0)
                    throw new InvalidOperationException();

                return CreateKVectorScalar(scalar);
            }
            
            return grade switch
            {
                1 => VectorStorage<T>.CreateVector(indexScalarDictionary),
                2 => BivectorStorage<T>.CreateBivector(indexScalarDictionary),
                _ => indexScalarDictionary.Count switch
                {
                    0 => CreateKVectorZero(grade),
                    1 => new KVectorStorage<T>(grade, indexScalarDictionary.First()),
                    _ => new KVectorStorage<T>(indexScalarDictionary.CreateLinVectorSparseStorage().CreateLinVectorSingleGradeStorage(grade))
                }
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVector(uint grade, ILinVectorSingleScalarStorage<T> indexScalarList)
        {
            return grade switch
            {
                1 => VectorStorage<T>.CreateVector(indexScalarList),
                2 => BivectorStorage<T>.CreateBivector(indexScalarList),
                _ => indexScalarList.IsEmpty()
                    ? CreateKVectorZero(grade)
                    : new KVectorStorage<T>(grade, indexScalarList)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVector(uint grade, ILinVectorStorage<T> indexScalarList)
        {
            if (grade == 0)
            {
                if (indexScalarList.IsEmpty())
                    return ZeroScalar;

                if (indexScalarList.GetSparseCount() > 1 || !indexScalarList.TryGetScalar(0, out var scalar))
                    throw new InvalidOperationException();

                return CreateKVectorScalar(scalar);
            }

            return grade switch
            {
                1 => VectorStorage<T>.CreateVector(indexScalarList),
                2 => BivectorStorage<T>.CreateBivector(indexScalarList),
                _ => indexScalarList.IsEmpty()
                    ? CreateKVectorZero(grade)
                    : new KVectorStorage<T>(grade, indexScalarList)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVector(ILinVectorSingleGradeStorage<T> singleGradeIndexScalarList)
        {
            return singleGradeIndexScalarList.Grade switch
            {
                1 => VectorStorage<T>.CreateVector(singleGradeIndexScalarList),
                2 => BivectorStorage<T>.CreateBivector(singleGradeIndexScalarList),
                _ => new KVectorStorage<T>(singleGradeIndexScalarList)
            };
        }


        protected uint? VSpaceDimension { get; set; }

        protected ILinVectorSingleGradeStorage<T> SingleGradeVectorStorage { get; }

        public virtual uint MinVSpaceDimension 
            => VSpaceDimension 
                ??= GetLinVectorIndexScalarStorage().GetMinVSpaceDimensionOfKVector(Grade);

        public int GradesCount 
            => 1;

        public uint Grade
            => GetLinVectorSingleGradeStorage().Grade;
        
        public int TermsCount 
            => GetLinVectorIndexScalarStorage().GetSparseCount();

        public bool IsEven 
            => false;

        public bool IsGraded 
            => true;


        protected KVectorStorage(uint grade)
        {
            SingleGradeVectorStorage = 
                new LinVectorEmptySingleGradeStorage<T>(grade);
        }
        
        protected KVectorStorage(uint grade, KeyValuePair<ulong, T> indexScalarPair)
        {
            var (index, scalar) = indexScalarPair;

            SingleGradeVectorStorage = 
                new LinVectorSingleScalarGradedStorage<T>(grade, index, scalar);
        }
        
        protected KVectorStorage(uint grade, [NotNull] ILinVectorSingleScalarStorage<T> singleScalarVectorStorage)
        {
            SingleGradeVectorStorage = new LinVectorSingleScalarGradedStorage<T>(grade, singleScalarVectorStorage);
        }
        
        protected KVectorStorage(uint grade, [NotNull] ILinVectorStorage<T> singleScalarVectorStorage)
        {
            SingleGradeVectorStorage = new LinVectorSingleGradeStorage<T>(grade, singleScalarVectorStorage);
        }

        protected KVectorStorage([NotNull] ILinVectorSingleGradeStorage<T> gradeIndexScalarList)
        {
            SingleGradeVectorStorage = 
                gradeIndexScalarList;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex()
        {
            return GetLinVectorIndexScalarStorage().GetMinIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex()
        {
            return GetLinVectorIndexScalarStorage().GetMaxIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsTerm(ulong id)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            return Grade == grade && GetLinVectorIndexScalarStorage().ContainsIndex(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsTerm(uint grade, ulong index)
        {
            return grade == Grade && GetLinVectorIndexScalarStorage().ContainsIndex(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsScalarPart()
        {
            return Grade == 0 && !GetLinVectorSingleGradeStorage().IsEmpty();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsVectorPart()
        {
            return Grade == 1 && !GetLinVectorSingleGradeStorage().IsEmpty();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsBivectorPart()
        {
            return Grade == 2 && !GetLinVectorSingleGradeStorage().IsEmpty();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKVectorPart(uint grade)
        {
            return Grade == grade && !GetLinVectorSingleGradeStorage().IsEmpty();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsTermWithIndex(ulong index)
        {
            return GetLinVectorSingleGradeStorage().ContainsIndex(Grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return GetLinVectorIndexScalarStorage().IsEmpty();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsScalar()
        {
            return Grade == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsVector()
        {
            return Grade == 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsBivector()
        {
            return Grade == 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsKVector()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsKVector(uint grade)
        {
            return Grade == grade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinId()
        {
            return GetLinVectorIndexScalarStorage().GetMinIndex().BasisBladeIndexToId(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxId()
        {
            return GetLinVectorIndexScalarStorage().GetMaxIndex().BasisBladeIndexToId(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinId(uint grade)
        {
            return grade == Grade
                ? GetLinVectorIndexScalarStorage().GetMinIndex()
                : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxId(uint grade)
        {
            return grade == Grade
                ? GetLinVectorIndexScalarStorage().GetMaxIndex()
                : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetMinGrade()
        {
            return Grade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetMaxGrade()
        {
            return Grade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex(uint grade)
        {
            return GetLinVectorIndexScalarStorage().GetMinIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex(uint grade)
        {
            return GetLinVectorIndexScalarStorage().GetMaxIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            yield return Grade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetStoredGradesBitPattern()
        {
            return 1UL << (int) Grade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetTermScalarByIndex(ulong index, out T value)
        {
            if (GetLinVectorIndexScalarStorage().TryGetScalar(index, out value))
                return true;

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetTermScalar(ulong id, out T value)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            if (Grade == grade && GetLinVectorIndexScalarStorage().TryGetScalar(index, out value))
                return true;

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetTermScalar(uint grade, ulong index, out T value)
        {
            if (grade == Grade) 
                return GetLinVectorIndexScalarStorage().TryGetScalar(index, out value);

            value = default;
            return false;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords()
        {
            return GetLinVectorSingleGradeStorage().VectorStorage.GetIndexScalarRecords();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorCopy()
        {
            return this switch
            {
                VectorStorage<T> vector => vector.GetVectorCopy(),
                BivectorStorage<T> bivector => bivector.GetBivectorCopy(),
                _ => 
                    KVectorStorage<T>.CreateKVector(
                        Grade, 
                        GetLinVectorIndexScalarStorage().GetCopy()
                    )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T2> MapKVectorScalars<T2>(Func<T, T2> scalarMapping)
        {
            return this switch
            {
                VectorStorage<T> vector => vector.MapVectorScalars(scalarMapping),
                BivectorStorage<T> bivector => bivector.MapBivectorScalars(scalarMapping),
                _ => 
                    KVectorStorage<T2>.CreateKVector(
                        Grade,
                        GetLinVectorIndexScalarStorage().MapScalars(scalarMapping)
                    )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T2> MapKVectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            return this switch
            {
                VectorStorage<T> vector => vector.MapVectorScalarsById(idScalarMapping),
                BivectorStorage<T> bivector => bivector.MapBivectorScalarsById(idScalarMapping),
                _ => 
                    KVectorStorage<T2>.CreateKVector(
                        Grade,
                        GetLinVectorIndexScalarStorage().MapScalars(
                            (index, scalar) => idScalarMapping(index.BasisBladeIndexToId(Grade), scalar)
                        )
                    )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T2> MapKVectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            return this switch
            {
                VectorStorage<T> vector => vector.MapVectorScalarsByIndex(indexScalarMapping),
                BivectorStorage<T> bivector => bivector.MapBivectorScalarsByIndex(indexScalarMapping),
                _ => 
                    KVectorStorage<T2>.CreateKVector(
                        Grade,
                        GetLinVectorIndexScalarStorage().MapScalars(indexScalarMapping)
                    )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T2> MapKVectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return this switch
            {
                VectorStorage<T> vector => vector.MapVectorScalarsByGradeIndex(gradeIndexScalarMapping),
                BivectorStorage<T> bivector => bivector.MapBivectorScalarsByGradeIndex(gradeIndexScalarMapping),
                _ => 
                    KVectorStorage<T2>.CreateKVector(
                        Grade,
                        GetLinVectorIndexScalarStorage().MapScalars(
                            (index, scalar) => gradeIndexScalarMapping(Grade, index, scalar)
                        )
                    )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorGradedStorage<T> GetGradedMultivectorCopy()
        {
            return GetKVectorCopy();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorGradedStorage<T2> MapGradedMultivectorScalars<T2>(Func<T, T2> scalarMapping)
        {
            return MapKVectorScalars(scalarMapping);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorGradedStorage<T2> MapGradedMultivectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            return MapKVectorScalarsByIndex(
                (index, scalar) => 
                    idScalarMapping(index.BasisBladeIndexToId(Grade), scalar)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorGradedStorage<T2> MapGradedMultivectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            return MapKVectorScalarsByIndex(indexScalarMapping);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorGradedStorage<T2> MapGradedMultivectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return MapKVectorScalarsByIndex(
                (index, scalar) => 
                    gradeIndexScalarMapping(Grade, index, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> FilterKVectorByScalar(Func<T, bool> scalarFilter)
        {
            return this switch
            {
                VectorStorage<T> vector => vector.FilterVectorByScalar(scalarFilter),
                BivectorStorage<T> bivector => bivector.FilterBivectorByScalar(scalarFilter),
                _ => 
                    KVectorStorage<T>.CreateKVector(
                        Grade,
                        GetLinVectorIndexScalarStorage().FilterByScalar(scalarFilter)
                    )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> FilterKVectorByIndexScalar(Func<ulong, T, bool> indexScalarFilter)
        {
            return this switch
            {
                VectorStorage<T> vector => vector.FilterVectorByIndexScalar(indexScalarFilter),
                BivectorStorage<T> bivector => bivector.FilterBivectorByIndexScalar(indexScalarFilter),
                _ => 
                    KVectorStorage<T>.CreateKVector(
                        Grade,
                        GetLinVectorIndexScalarStorage().FilterByIndexScalar(indexScalarFilter)
                    )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> FilterKVectorByIndex(Func<ulong, bool> indexFilter)
        {
            return this switch
            {
                VectorStorage<T> vector => vector.FilterVectorByIndex(indexFilter),
                BivectorStorage<T> bivector => bivector.FilterBivectorByIndex(indexFilter),
                _ => 
                    KVectorStorage<T>.CreateKVector(
                        Grade,
                        GetLinVectorIndexScalarStorage().FilterByIndex(indexFilter)
                    )
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetScalarPartList()
        {
            return Grade == 0 && !GetLinVectorIndexScalarStorage().IsEmpty()
                ? GetLinVectorIndexScalarStorage()
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetVectorPartList()
        {
            return Grade == 1 && !GetLinVectorIndexScalarStorage().IsEmpty()
                ? GetLinVectorIndexScalarStorage()
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetBivectorPartList()
        {
            return Grade == 2 && !GetLinVectorIndexScalarStorage().IsEmpty()
                ? GetLinVectorIndexScalarStorage()
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetKVectorPartList(uint grade)
        {
            return Grade == grade && !GetLinVectorIndexScalarStorage().IsEmpty()
                ? GetLinVectorIndexScalarStorage()
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetVectorPart(out VectorStorage<T> vector)
        {
            if (Grade == 1 && this is VectorStorage<T> vectorStorage)
            {
                vector = vectorStorage;
                return true;
            }

            vector = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetBivectorPart(out BivectorStorage<T> bivector)
        {
            if (Grade == 2 && this is BivectorStorage<T> bivectorStorage)
            {
                bivector = bivectorStorage;
                return true;
            }

            bivector = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetKVectorPart(uint grade, out KVectorStorage<T> kVector)
        {
            if (Grade == grade)
            {
                kVector = this;
                return true;
            }

            kVector = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalarPartList(out ILinVectorStorage<T> indexScalarList)
        {
            if (Grade == 0 && !GetLinVectorIndexScalarStorage().IsEmpty())
            {
                indexScalarList = GetLinVectorIndexScalarStorage();
                return true;
            }

            indexScalarList = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetVectorPartList(out ILinVectorStorage<T> indexScalarList)
        {
            if (Grade == 1 && !GetLinVectorIndexScalarStorage().IsEmpty())
            {
                indexScalarList = GetLinVectorIndexScalarStorage();
                return true;
            }

            indexScalarList = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetBivectorPartList(out ILinVectorStorage<T> indexScalarList)
        {
            if (Grade == 2 && !GetLinVectorIndexScalarStorage().IsEmpty())
            {
                indexScalarList = GetLinVectorIndexScalarStorage();
                return true;
            }

            indexScalarList = LinVectorEmptyStorage<T>.EmptyStorage;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetKVectorPartList(uint grade, out ILinVectorStorage<T> indexScalarList)
        {
            if (Grade == grade && !GetLinVectorIndexScalarStorage().IsEmpty())
            {
                indexScalarList = GetLinVectorIndexScalarStorage();
                return true;
            }

            indexScalarList = LinVectorEmptyStorage<T>.EmptyStorage;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetLinVectorIndexScalarStorage()
        {
            return GetLinVectorSingleGradeStorage().VectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetLinVectorIdScalarStorage()
        {
            return GetLinVectorIndexScalarStorage().GetPermutation(
                index => index.BasisBladeIndexToId(Grade)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T> GetLinVectorGradedStorage()
        {
            return GetLinVectorSingleGradeStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetLinVectorIndexScalarStorage(uint grade)
        {
            return grade == Grade
                ? GetLinVectorIndexScalarStorage() 
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIds()
        {
            return GetLinVectorIndexScalarStorage().GetIndices().Select(
                index => index.BasisBladeIndexToId(Grade)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices()
        {
            return GetLinVectorIndexScalarStorage().GetIndices();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeIndexRecord> GetGradeIndexRecords()
        {
            return GetLinVectorIndexScalarStorage().GetIndices().Select(index => new GradeIndexRecord(Grade, index));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return GetLinVectorIndexScalarStorage().GetScalars();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexScalarRecord<T>> GetIdScalarRecords()
        {
            return GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Select(
                pair => new IndexScalarRecord<T>(
                    pair.Index.BasisBladeIndexToId(Grade),
                    pair.Scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords(uint grade)
        {
            return grade == Grade
                ? GetLinVectorIndexScalarStorage().GetIndexScalarRecords()
                : Enumerable.Empty<IndexScalarRecord<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeIndexScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            return GetLinVectorSingleGradeStorage().GetGradeIndexScalarRecords();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorTreeStorage<T> GetBinaryTree(int treeDepth)
        {
            if (treeDepth < MinVSpaceDimension)
                throw new InvalidOperationException();

            var dict = GetIdScalarRecords()
                .ToDictionary(
                    pair => pair.Index,
                    pair => pair.Scalar
                );

            return new LinVectorTreeStorage<T>(treeDepth, dict);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            //return GeoGbtKVectorStorageStack1<T>.Create(capacity, treeDepth, this);
            //return GeoGbtMultivectorStorageGradedStack1<T>.Create(capacity, treeDepth, this);
            return GeoGbtMultivectorStorageUniformStack1<T>.Create(
                capacity, 
                treeDepth, 
                scalarProcessor,
                this
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MultivectorStorage<T> ToMultivectorStorage()
        {
            var idScalarDictionary =
                GetLinVectorIndexScalarStorage().GetPermutation(BasisBladeUtils.BasisBladeIdToIndex);

            return MultivectorStorage<T>.Create(
                idScalarDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MultivectorGradedStorage<T> ToGradedMultivectorStorage()
        {
            return SingleGradeVectorStorage.CreateMultivectorGradedStorage();
        }


        public virtual bool TryGetScalar(out T value)
        {
            value = default;
            return false;
        }

        public virtual bool TryGetTermByIndex(int index, out BasisTerm<T> term)
        {
            var i = (ulong) index;

            if (GetLinVectorIndexScalarStorage().TryGetScalar(i, out var value))
            {
                term = value.CreateBasisTerm(Grade, index);
                return true;
            }

            term = null;
            return false;
        }

        public virtual bool TryGetTermByIndex(ulong index, out BasisTerm<T> term)
        {
            if (GetLinVectorIndexScalarStorage().TryGetScalar(index, out var value))
            {
                term = value.CreateBasisTerm(Grade, index);
                return true;
            }

            term = null;
            return false;
        }

        public virtual bool TryGetTerm(ulong id, out BasisTerm<T> term)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            if (grade == Grade && GetLinVectorIndexScalarStorage().TryGetScalar(index, out var value))
            {
                term = value.CreateBasisTerm(Grade, index);
                return true;
            }

            term = null;
            return false;
        }

        public virtual bool TryGetTerm(uint grade, ulong index, out BasisTerm<T> term)
        {
            if (grade == Grade && GetLinVectorIndexScalarStorage().TryGetScalar(index, out var value))
            {
                term = value.CreateBasisTerm(Grade, index);
                return true;
            }

            term = null;
            return false;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual ILinVectorSingleGradeStorage<T> GetLinVectorSingleGradeStorage()
        {
            return SingleGradeVectorStorage;
        }

        public virtual VectorStorage<T> GetVectorPart()
        {
            return VectorStorage<T>.ZeroVector;
        }

        public virtual VectorStorage<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            return VectorStorage<T>.ZeroVector;
        }

        public virtual VectorStorage<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return VectorStorage<T>.ZeroVector;
        }

        public virtual VectorStorage<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            return VectorStorage<T>.ZeroVector;
        }

        public virtual BivectorStorage<T> GetBivectorPart()
        {
            return BivectorStorage<T>.ZeroBivector;
        }

        public virtual BivectorStorage<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            return BivectorStorage<T>.ZeroBivector;
        }

        public virtual BivectorStorage<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return BivectorStorage<T>.ZeroBivector;
        }

        public virtual BivectorStorage<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            return BivectorStorage<T>.ZeroBivector;
        }

        public virtual KVectorStorage<T> GetKVectorPart(uint grade)
        {
            return grade == Grade 
                ? this 
                : CreateKVectorZero(grade);
        }

        public virtual KVectorStorage<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            if (grade != Grade)
                return CreateKVectorZero(grade);

            var indexScalarDictionary = 
                GetLinVectorIndexScalarStorage().FilterByScalar(scalarSelection);

            return CreateKVector(Grade, indexScalarDictionary);
        }

        public virtual KVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            if (grade != Grade)
                return CreateKVectorZero(grade);

            var indexScalarDictionary = 
                GetLinVectorIndexScalarStorage().FilterByIndexScalar(indexScalarSelection);

            return CreateKVector(Grade, indexScalarDictionary);
        }

        public virtual KVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            if (grade != Grade)
                return CreateKVectorZero(grade);

            var indexScalarDictionary = 
                GetLinVectorIndexScalarStorage().FilterByIndex(indexSelection);

            return CreateKVector(Grade, indexScalarDictionary);
        }

        

        public virtual IMultivectorStorage<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var indexScalarDictionary = 
                GetLinVectorIndexScalarStorage().FilterByIndex(
                    index => idSelection(index.BasisBladeIndexToId(Grade))
                );

            return CreateKVector(Grade, indexScalarDictionary);
        }

        public virtual IMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var indexScalarDictionary = 
                GetLinVectorIndexScalarStorage().FilterByIndex(
                    index => gradeIndexSelection(Grade, index)
                );

            return CreateKVector(Grade, indexScalarDictionary);
        }

        public virtual IMultivectorStorage<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary = 
                GetLinVectorIndexScalarStorage().FilterByScalar(scalarSelection);

            return CreateKVector(Grade, indexScalarDictionary);
        }

        public virtual IMultivectorStorage<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var indexScalarDictionary = 
                GetLinVectorIndexScalarStorage().FilterByIndexScalar(
                    (index, value) => idScalarSelection(
                        index.BasisBladeIndexToId(Grade), 
                        value
                    )
                );

            return CreateKVector(Grade, indexScalarDictionary);
        }

        public virtual IMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var indexScalarDictionary = 
                GetLinVectorIndexScalarStorage().FilterByIndexScalar(
                    (index, scalar) => gradeIndexScalarSelection(Grade, index, scalar)
                );

            return CreateKVector(Grade, indexScalarDictionary);
        }

        public virtual Tuple<VectorStorage<T>, VectorStorage<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            if (Grade != 1)
                return new Tuple<VectorStorage<T>, VectorStorage<T>>(
                    VectorStorage<T>.ZeroVector,
                    VectorStorage<T>.ZeroVector
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                if (indexSelection(index))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<VectorStorage<T>, VectorStorage<T>>(
                indexScalarDictionary1.CreateVectorStorage(),
                indexScalarDictionary2.CreateVectorStorage()
            );
        }

        public virtual Tuple<VectorStorage<T>, VectorStorage<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            if (Grade != 1)
                return new Tuple<VectorStorage<T>, VectorStorage<T>>(
                    VectorStorage<T>.ZeroVector,
                    VectorStorage<T>.ZeroVector
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                if (indexScalarSelection(index, scalar))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<VectorStorage<T>, VectorStorage<T>>(
                indexScalarDictionary1.CreateVectorStorage(),
                indexScalarDictionary2.CreateVectorStorage()
            );
        }

        public virtual Tuple<VectorStorage<T>, VectorStorage<T>> SplitVectorPart(Func<T, bool> scalarSelection)
        {
            if (Grade != 1)
                return new Tuple<VectorStorage<T>, VectorStorage<T>>(
                    VectorStorage<T>.ZeroVector,
                    VectorStorage<T>.ZeroVector
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                if (scalarSelection(scalar))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<VectorStorage<T>, VectorStorage<T>>(
                indexScalarDictionary1.CreateVectorStorage(),
                indexScalarDictionary2.CreateVectorStorage()
            );
        }


        public virtual IEnumerable<BasisBlade> GetBasisBlades()
        {
            return GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Select(pair => 
                Grade.CreateBasisBlade(pair.Index)
            );
        }
        
        public virtual IEnumerable<BasisTerm<T>> GetTerms()
        {
            return GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Select(pair => 
                pair.Scalar.CreateBasisTerm(Grade, pair.Index)
            );
        }

        public virtual IEnumerable<BasisTerm<T>> GetTerms(Func<ulong, bool> idSelection)
        {
            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                var id = index.BasisBladeIndexToId(Grade);

                if (idSelection(id))
                    yield return scalar.CreateBasisTerm(Grade, index);
            }
        }

        public virtual IEnumerable<BasisTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                if (gradeIndexSelection(Grade, index))
                    yield return scalar.CreateBasisTerm(Grade, index);
            }
        }

        public virtual IEnumerable<BasisTerm<T>> GetTerms(Func<T, bool> scalarSelection)
        {
            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                if (scalarSelection(scalar))
                    yield return scalar.CreateBasisTerm(Grade, index);
            }
        }

        public virtual IEnumerable<BasisTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                var id = index.BasisBladeIndexToId(Grade);

                if (idScalarSelection(id, scalar))
                    yield return scalar.CreateBasisTerm(Grade, index);
            }
        }

        public virtual IEnumerable<BasisTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                if (gradeIndexScalarSelection(Grade, index, scalar))
                    yield return scalar.CreateBasisTerm(Grade, index);
            }
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return this.GetMultivectorText();
        }
    }
}
