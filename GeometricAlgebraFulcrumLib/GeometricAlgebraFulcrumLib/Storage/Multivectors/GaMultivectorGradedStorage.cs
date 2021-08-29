using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public sealed record GaMultivectorGradedStorage<T>
        : GaMultivectorStorageBase<T>, IGaMultivectorGradedStorage<T>
    {
        public static GaMultivectorGradedStorage<T> ZeroMultivector { get; }
            = new GaMultivectorGradedStorage<T>(LaVectorEmptyGradedStorage<T>.EmptyList);


        public static GaMultivectorGradedStorage<T> Create(ILaVectorGradedStorage<T> gradeIndexScalarList)
        {
            return new GaMultivectorGradedStorage<T>(
                gradeIndexScalarList
            );
        }


        public ILaVectorGradedStorage<T> GradeIndexScalarList { get; }

        private uint? _vSpaceDimension;
        public override uint MinVSpaceDimension 
            => _vSpaceDimension 
                ??= GradeIndexScalarList.GetMinVSpaceDimensionOfMultivector();

        public override int GradesCount 
            => GradeIndexScalarList.GradesCount;

        public override int TermsCount 
            => GradeIndexScalarList.GetGradeStorageRecords().Sum(p => p.Storage.GetSparseCount());
        
        public override bool IsUniform => false;

        public override bool IsGraded => true;


        private GaMultivectorGradedStorage([NotNull] ILaVectorGradedStorage<T> gradeIndexScalarList)
        {
            GradeIndexScalarList = gradeIndexScalarList;
        }

        
        public IGaMultivectorGradedStorage<T> GetGradedMultivectorCopy()
        {
            var gradeIndexScalarList = 
                GradeIndexScalarList.GetCopy();

            return new GaMultivectorGradedStorage<T>(
                gradeIndexScalarList
            );
        }
        
        public IGaMultivectorGradedStorage<T2> MapGradedMultivectorScalars<T2>(Func<T, T2> scalarMapping)
        {
            var gradeIndexScalarList =
                GradeIndexScalarList.MapScalars(scalarMapping);

            return new GaMultivectorGradedStorage<T2>(
                gradeIndexScalarList
            );
        }

        public IGaMultivectorGradedStorage<T2> MapGradedMultivectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            var gradeIndexScalarList =
                GradeIndexScalarList.MapScalars(
                    (grade, index, scalar) => 
                        idScalarMapping(index.BasisBladeIndexToId(grade), scalar)
                );

            return new GaMultivectorGradedStorage<T2>(
                gradeIndexScalarList
            );
        }
        
        public IGaMultivectorGradedStorage<T2> MapGradedMultivectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            var gradeIndexScalarList =
                GradeIndexScalarList.MapScalars(
                    (_, index, scalar) => 
                        indexScalarMapping(index, scalar)
                );

            return new GaMultivectorGradedStorage<T2>(
                gradeIndexScalarList
            );
        }

        public IGaMultivectorGradedStorage<T2> MapGradedMultivectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            var gradeIndexScalarList =
                GradeIndexScalarList.MapScalars(gradeIndexScalarMapping);

            return new GaMultivectorGradedStorage<T2>(
                gradeIndexScalarList
            );
        }


        public override bool ContainsVectorPart()
        {
            return GradeIndexScalarList.ContainsGrade(1);
        }

        public override bool TryGetScalar(out T value)
        {
            return GradeIndexScalarList.TryGetScalar(0, 0, out value);
        }

        public override bool ContainsBivectorPart()
        {
            return GradeIndexScalarList.ContainsGrade(2);
        }

        public override bool ContainsKVectorPart(uint grade)
        {
            return GradeIndexScalarList.ContainsGrade(grade);
        }


        public override bool IsEmpty()
        {
            return GradeIndexScalarList.IsEmpty();
        }

        public override bool IsScalar()
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarList.GetGradeStorageRecords())
            {
                if (grade == 0)
                    continue;

                if (!indexScalarDictionary.IsEmpty())
                    return false;
            }

            return true;
        }

        public override bool IsVector()
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarList.GetGradeStorageRecords())
            {
                if (grade == 1)
                    continue;

                if (!indexScalarDictionary.IsEmpty())
                    return false;
            }

            return true;
        }

        public override bool IsBivector()
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarList.GetGradeStorageRecords())
            {
                if (grade == 2)
                    continue;

                if (!indexScalarDictionary.IsEmpty())
                    return false;
            }

            return true;
        }

        public override bool IsKVector()
        {
            return GradeIndexScalarList.GetEvenStorages().Count(pair => !pair.IsEmpty()) < 2;
        }

        public override bool IsKVector(uint grade)
        {
            foreach (var (g, indexScalarDictionary) in GradeIndexScalarList.GetGradeStorageRecords())
            {
                if (g == grade)
                    continue;

                if (!indexScalarDictionary.IsEmpty())
                    return false;
            }

            return true;
        }

        public override ulong GetMinId()
        {
            return GradeIndexScalarList
                .GetEvenKeys(GaBasisBladeUtils.BasisBladeGradeIndexToId)
                .Min();
        }

        public override ulong GetMaxId()
        {
            return GradeIndexScalarList
                .GetEvenKeys(GaBasisBladeUtils.BasisBladeGradeIndexToId)
                .Max();
        }

        public override ulong GetMinId(uint grade)
        {
            return GradeIndexScalarList.GetMinKey(grade).BasisBladeIndexToId(grade);
        }

        public override ulong GetMaxId(uint grade)
        {
            return GradeIndexScalarList.GetMaxKey(grade).BasisBladeIndexToId(grade);
        }

        public override uint GetMinGrade()
        {
            return GradeIndexScalarList.GetMinGrade();
        }

        public override uint GetMaxGrade()
        {
            return GradeIndexScalarList.GetMaxGrade();
        }

        public override ulong GetMinIndex(uint grade)
        {
            return GradeIndexScalarList.GetMinKey(grade);
        }

        public override ulong GetMaxIndex(uint grade)
        {
            return GradeIndexScalarList.GetMaxKey(grade);
        }

        public override IEnumerable<uint> GetGrades()
        {
            return GradeIndexScalarList.GetGrades();
        }


        public override bool ContainsTerm(ulong id)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            return GradeIndexScalarList.ContainsIndex(grade, index);
        }

        public override bool ContainsTerm(uint grade, ulong index)
        {
            return GradeIndexScalarList.ContainsIndex(grade, index);
        }

        public override bool ContainsScalarPart()
        {
            return GradeIndexScalarList.ContainsGrade(0);
        }


        public override bool TryGetTermScalar(ulong id, out T value)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            if (GradeIndexScalarList.TryGetEvenStorage(grade, out var storage))
                return storage.TryGetScalar(index, out value);

            value = default;
            return false;
        }

        public override bool TryGetTermScalar(uint grade, ulong index, out T value)
        {
            if (GradeIndexScalarList.TryGetEvenStorage(grade, out var storage))
                return storage.TryGetScalar(index, out value);

            value = default;
            return false;
        }


        public override bool TryGetKVectorPart(uint grade, out IGaKVectorStorage<T> kVector)
        {
            if (GradeIndexScalarList.TryGetEvenStorage(grade, out var evenDictionary))
            {
                kVector = GaKVectorStorage<T>.Create(grade, evenDictionary);
                return true;
            }

            kVector = null;
            return false;
        }

        public override bool TryGetScalarPartList(out ILaVectorEvenStorage<T> indexScalarList)
        {
            return GradeIndexScalarList.TryGetEvenStorage(0, out indexScalarList);
        }

        public override bool TryGetVectorPartList(out ILaVectorEvenStorage<T> indexScalarDictionary)
        {
            if (GradeIndexScalarList.TryGetEvenStorage(1U, out indexScalarDictionary) && !indexScalarDictionary.IsEmpty())
                return true;

            indexScalarDictionary = LaVectorEmptyStorage<T>.ZeroStorage;
            return false;
        }

        public override bool TryGetBivectorPartList(out ILaVectorEvenStorage<T> indexScalarDictionary)
        {
            if (GradeIndexScalarList.TryGetEvenStorage(2U, out indexScalarDictionary) && !indexScalarDictionary.IsEmpty())
                return true;

            indexScalarDictionary = LaVectorEmptyStorage<T>.ZeroStorage;
            return false;

        }

        public override bool TryGetKVectorPartList(uint grade, out ILaVectorEvenStorage<T> indexScalarDictionary)
        {
            if (GradeIndexScalarList.TryGetEvenStorage(grade, out indexScalarDictionary) && !indexScalarDictionary.IsEmpty())
                return true;

            indexScalarDictionary = LaVectorEmptyStorage<T>.ZeroStorage;
            return false;

        }

        public override ILaVectorEvenStorage<T> GetIdScalarList()
        {
            return GradeIndexScalarList
                .GetGradeStorageRecords()
                .SelectMany(storage => 
                    storage.Storage.GetIndexScalarRecords().Select(pair => 
                        new KeyValuePair<ulong, T>(
                            pair.Index.BasisBladeIndexToId(storage.Grade), 
                            pair.Scalar
                        )
                    )
                ).ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                ).CreateLaVectorStorage();
        }

        public override ILaVectorGradedStorage<T> GetGradeIndexScalarList()
        {
            return GradeIndexScalarList;
        }

        public override ILaVectorEvenStorage<T> GetIndexScalarList(uint grade)
        {
            return GradeIndexScalarList.TryGetEvenStorage(grade, out var indexScalarList)
                ? indexScalarList
                : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        
        public override bool TryGetTerm(ulong id, out GaBasisTerm<T> term)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            if (TryGetTermScalar(grade, index, out var value))
            {
                term = value.CreateBasisTerm(grade, index);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(uint grade, ulong index, out GaBasisTerm<T> term)
        {
            if (TryGetTermScalar(grade, index, out var value))
            {
                term = value.CreateBasisTerm(grade, index);
                return true;
            }

            term = null;
            return false;
        }

        public override ILaVectorEvenStorage<T> GetScalarPartList()
        {
            return 
                GradeIndexScalarList.TryGetEvenStorage(0, out var evenDictionary) && 
                evenDictionary.TryGetScalar(0UL, out var scalar)
                    ? scalar.CreateLaVectorZeroIndexStorage()
                    : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        public override ILaVectorEvenStorage<T> GetVectorPartList()
        {
            return 
                GradeIndexScalarList.TryGetEvenStorage(1U, out var evenDictionary) && 
                !evenDictionary.IsEmpty()
                    ? evenDictionary
                    : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        public override ILaVectorEvenStorage<T> GetBivectorPartList()
        {
            return 
                GradeIndexScalarList.TryGetEvenStorage(2U, out var evenDictionary) && 
                !evenDictionary.IsEmpty()
                    ? evenDictionary
                    : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        public override ILaVectorEvenStorage<T> GetKVectorPartList(uint grade)
        {
            return 
                GradeIndexScalarList.TryGetEvenStorage(grade, out var evenDictionary) && 
                !evenDictionary.IsEmpty()
                    ? evenDictionary
                    : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        public override bool TryGetScalarPart(out IGaScalarStorage<T> scalar)
        {
            if (GradeIndexScalarList.TryGetEvenStorage(0U, out var evenDictionary) && evenDictionary.TryGetScalar(0, out var s))
            {
                scalar = GaScalarStorage<T>.Create(s);
                return true;
            }

            scalar = null;
            return false;
        }

        public override bool TryGetVectorPart(out IGaVectorStorage<T> vector)
        {
            if (GradeIndexScalarList.TryGetEvenStorage(1U, out var evenDictionary))
            {
                vector = GaVectorStorage<T>.Create(evenDictionary);
                return true;
            }

            vector = null;
            return false;
        }

        public override bool TryGetBivectorPart(out IGaBivectorStorage<T> bivector)
        {
            if (GradeIndexScalarList.TryGetEvenStorage(2U, out var evenDictionary))
            {
                bivector = GaBivectorStorage<T>.Create(evenDictionary);
                return true;
            }

            bivector = null;
            return false;
        }


        public override IEnumerable<ulong> GetIds()
        {
            return GradeIndexScalarList
                .GetGradeStorageRecords()
                .SelectMany(pair => 
                    pair.Storage.GetIndices().Select(
                        index => index.BasisBladeIndexToId(pair.Grade)
                    )
                );
        }

        public override IEnumerable<GradeIndexRecord> GetGradeIndexRecords()
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarList.GetGradeStorageRecords())
            foreach (var index in indexScalarDictionary.GetIndices())
                yield return new GradeIndexRecord(grade, index);
        }

        public override IEnumerable<GaBasisBlade> GetBasisBlades()
        {
            return GradeIndexScalarList
                .GetGradeStorageRecords()
                .SelectMany(storage => 
                    storage.Storage.GetIndexScalarRecords().Select(pair => 
                        storage.Grade.CreateBasisBlade(pair.Index)
                    )
                );
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms()
        {
            return GradeIndexScalarList
                .GetGradeStorageRecords()
                .SelectMany(storage => 
                    storage.Storage.GetIndexScalarRecords().Select(pair => 
                        pair.Scalar.CreateBasisTerm(storage.Grade, pair.Index)
                    )
                );
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, bool> idSelection)
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarList.GetGradeStorageRecords())
            {
                foreach (var (index, scalar) in indexScalarDictionary.GetIndexScalarRecords())
                {
                    var id = index.BasisBladeIndexToId(grade);

                    if (idSelection(id))
                        yield return scalar.CreateBasisTerm(grade, index);
                }
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarList.GetGradeStorageRecords())
            {
                foreach (var (index, scalar) in indexScalarDictionary.GetIndexScalarRecords())
                {
                    if (gradeIndexSelection(grade, index))
                        yield return scalar.CreateBasisTerm(grade, index);
                }
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<T, bool> scalarSelection)
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarList.GetGradeStorageRecords())
            {
                foreach (var (index, scalar) in indexScalarDictionary.GetIndexScalarRecords())
                {
                    if (scalarSelection(scalar))
                        yield return scalar.CreateBasisTerm(grade, index);
                }
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarList.GetGradeStorageRecords())
            {
                foreach (var (index, scalar) in indexScalarDictionary.GetIndexScalarRecords())
                {
                    var id = index.BasisBladeIndexToId(grade);

                    if (idScalarSelection(id, scalar))
                        yield return scalar.CreateBasisTerm(grade, index);
                }
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarList.GetGradeStorageRecords())
            {
                foreach (var (index, scalar) in indexScalarDictionary.GetIndexScalarRecords())
                {
                    if (gradeIndexScalarSelection(grade, index, scalar))
                        yield return scalar.CreateBasisTerm(grade, index);
                }
            }
        }

        public override IEnumerable<IndexScalarRecord<T>> GetIdScalarRecords()
        {
            return GradeIndexScalarList
                .GetGradeStorageRecords()
                .SelectMany(storage => 
                    storage.Storage.GetIndexScalarRecords().Select(pair => 
                        new IndexScalarRecord<T>(
                            pair.Index.BasisBladeIndexToId(storage.Grade), 
                            pair.Scalar
                        )
                    )
                );
        }

        public override IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords(uint grade)
        {
            return GradeIndexScalarList.GetEvenStorage(grade).GetIndexScalarRecords();
        }

        public override IEnumerable<GradeIndexScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            return GradeIndexScalarList
                .GetGradeStorageRecords()
                .SelectMany(storage => 
                    storage.Storage.GetIndexScalarRecords().Select(pair => 
                        new GradeIndexScalarRecord<T>(storage.Grade, pair.Index, pair.Scalar)
                    )
                );
        }

        public override IEnumerable<T> GetScalars()
        {
            return GradeIndexScalarList.GetEvenStorages()
                .SelectMany(storage => storage.GetScalars());
        }


        public override IGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity, IScalarProcessor<T> scalarProcessor)
        {
            //return GaGbtMultivectorStorageGradedStack1<T>.Create(
            //    capacity, 
            //    treeDepth,
            //    this
            //);
            return GaGbtMultivectorStorageUniformStack1<T>.Create(
                capacity, 
                treeDepth,
                scalarProcessor,
                this
            );
        }
        
        /// <summary>
        /// Construct a binary tree representation of this storage
        /// </summary>
        /// <returns></returns>
        public override LaVectorTreeStorage<T> GetBinaryTree(int treeDepth)
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


        public override IGaMultivectorSparseStorage<T> ToSparseMultivector()
        {
            return GaMultivectorSparseStorage<T>.Create(
                GradeIndexScalarList.ToEvenList()
            );
        }

        public override IGaMultivectorGradedStorage<T> ToGradedMultivector()
        {
            return this;
        }

        public override IGaVectorStorage<T> GetVectorPart()
        {
            if (!GradeIndexScalarList.TryGetEvenStorage(1, out var indexScalarDictionary))
                return GaVectorStorage<T>.ZeroVector;
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaVectorStorage<T>.ZeroVector
                : GaVectorStorage<T>.Create(indexScalarDictionary);
        }

        public override IGaVectorStorage<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            if (!GradeIndexScalarList.TryGetEvenStorage(1, out var indexScalarDictionary))
                return GaVectorStorage<T>.ZeroVector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByScalar(scalarSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaVectorStorage<T>.ZeroVector
                : GaVectorStorage<T>.Create(indexScalarDictionary);
        }

        public override IGaVectorStorage<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            if (!GradeIndexScalarList.TryGetEvenStorage(1, out var indexScalarDictionary))
                return GaVectorStorage<T>.ZeroVector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByIndexScalar(indexScalarSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaVectorStorage<T>.ZeroVector
                : GaVectorStorage<T>.Create(indexScalarDictionary);
        }

        public override IGaVectorStorage<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            if (!GradeIndexScalarList.TryGetEvenStorage(1, out var indexScalarDictionary))
                return GaVectorStorage<T>.ZeroVector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByIndex(indexSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaVectorStorage<T>.ZeroVector
                : GaVectorStorage<T>.Create(indexScalarDictionary);
        }

        public override IGaBivectorStorage<T> GetBivectorPart()
        {
            if (!GradeIndexScalarList.TryGetEvenStorage(2, out var indexScalarDictionary))
                return GaBivectorStorage<T>.ZeroBivector;
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaBivectorStorage<T>.ZeroBivector
                : GaBivectorStorage<T>.Create(indexScalarDictionary);
        }

        public override IGaBivectorStorage<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            if (!GradeIndexScalarList.TryGetEvenStorage(2, out var indexScalarDictionary))
                return GaBivectorStorage<T>.ZeroBivector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByScalar(scalarSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaBivectorStorage<T>.ZeroBivector
                : GaBivectorStorage<T>.Create(indexScalarDictionary);
        }

        public override IGaBivectorStorage<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            if (!GradeIndexScalarList.TryGetEvenStorage(2, out var indexScalarDictionary))
                return GaBivectorStorage<T>.ZeroBivector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByIndexScalar(indexScalarSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaBivectorStorage<T>.ZeroBivector
                : GaBivectorStorage<T>.Create(indexScalarDictionary);
        }

        public override IGaBivectorStorage<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            if (!GradeIndexScalarList.TryGetEvenStorage(2, out var indexScalarDictionary))
                return GaBivectorStorage<T>.ZeroBivector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByIndex(indexSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaBivectorStorage<T>.ZeroBivector
                : GaBivectorStorage<T>.Create(indexScalarDictionary);
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade)
        {
            if (!GradeIndexScalarList.TryGetEvenStorage(grade, out var indexScalarDictionary))
                return GaKVectorStorage<T>.ZeroKVector(grade);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaKVectorStorage<T>.ZeroKVector(grade)
                : GaKVectorStorage<T>.Create(grade, indexScalarDictionary);
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            if (!GradeIndexScalarList.TryGetEvenStorage(grade, out var indexScalarDictionary))
                return GaKVectorStorage<T>.ZeroKVector(grade);

            indexScalarDictionary = 
                indexScalarDictionary.FilterByScalar(scalarSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaKVectorStorage<T>.ZeroKVector(grade)
                : GaKVectorStorage<T>.Create(grade, indexScalarDictionary);
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            if (!GradeIndexScalarList.TryGetEvenStorage(grade, out var indexScalarDictionary))
                return GaKVectorStorage<T>.ZeroKVector(grade);

            indexScalarDictionary = 
                indexScalarDictionary.FilterByIndexScalar(indexScalarSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaKVectorStorage<T>.ZeroKVector(grade)
                : GaKVectorStorage<T>.Create(grade, indexScalarDictionary);
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            if (!GradeIndexScalarList.TryGetEvenStorage(grade, out var indexScalarDictionary))
                return GaKVectorStorage<T>.ZeroKVector(grade);

            indexScalarDictionary = 
                indexScalarDictionary.FilterByIndex(indexSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaKVectorStorage<T>.ZeroKVector(grade)
                : GaKVectorStorage<T>.Create(grade, indexScalarDictionary);
        }
        
        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarList) in GradeIndexScalarList.GetGradeStorageRecords())
            {
                var indexScalarDictionary = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarList.GetIndexScalarRecords())
                {
                    var id = index.BasisBladeIndexToId(grade);

                    if (idSelection(id))
                        indexScalarDictionary.Add(index, scalar);
                }

                if (indexScalarDictionary.Count > 0)
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
            }

            var gradeIndexScalarList =
                gradeIndexScalarDictionary.CreateLaVectorGradedStorage();

            return new GaMultivectorGradedStorage<T>(
                gradeIndexScalarList
            );
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarList) in GradeIndexScalarList.GetGradeStorageRecords())
            {
                var indexScalarDictionary = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarList.GetIndexScalarRecords())
                {
                    if (gradeIndexSelection(grade, index))
                        indexScalarDictionary.Add(index, scalar);
                }

                if (indexScalarDictionary.Count > 0)
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
            }

            var gradeIndexScalarList =
                gradeIndexScalarDictionary.CreateLaVectorGradedStorage();

            return new GaMultivectorGradedStorage<T>(
                gradeIndexScalarList
            );
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarList) in GradeIndexScalarList.GetGradeStorageRecords())
            {
                var indexScalarDictionary = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarList.GetIndexScalarRecords())
                {
                    if (scalarSelection(scalar))
                        indexScalarDictionary.Add(index, scalar);
                }

                if (indexScalarDictionary.Count > 0)
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
            }

            var gradeIndexScalarList =
                gradeIndexScalarDictionary.CreateLaVectorGradedStorage();

            return new GaMultivectorGradedStorage<T>(
                gradeIndexScalarList
            );
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarList) in GradeIndexScalarList.GetGradeStorageRecords())
            {
                var indexScalarDictionary = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarList.GetIndexScalarRecords())
                {
                    var id = index.BasisBladeIndexToId(grade);

                    if (idScalarSelection(id, scalar))
                        indexScalarDictionary.Add(index, scalar);
                }

                if (indexScalarDictionary.Count > 0)
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
            }

            var gradeIndexScalarList =
                gradeIndexScalarDictionary.CreateLaVectorGradedStorage();

            return new GaMultivectorGradedStorage<T>(
                gradeIndexScalarList
            );
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarList) in GradeIndexScalarList.GetGradeStorageRecords())
            {
                var indexScalarDictionary = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarList.GetIndexScalarRecords())
                {
                    if (gradeIndexScalarSelection(grade, index, scalar))
                        indexScalarDictionary.Add(index, scalar);
                }

                if (indexScalarDictionary.Count > 0)
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
            }

            var gradeIndexScalarList =
                gradeIndexScalarDictionary.CreateLaVectorGradedStorage();

            return new GaMultivectorGradedStorage<T>(
                gradeIndexScalarList
            );
        }

        public override Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            if (!TryGetVectorPartList(out var indexScalarDictionary))
                return new Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>>(
                    GaVectorStorage<T>.ZeroVector,
                    GaVectorStorage<T>.ZeroVector
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in indexScalarDictionary.GetIndexScalarRecords())
            {
                if (indexSelection(index))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>>(
                indexScalarDictionary1.CreateGaVectorStorage(),
                indexScalarDictionary2.CreateGaVectorStorage()
            );
        }

        public override Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            if (!TryGetVectorPartList(out var indexScalarDictionary))
                return new Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>>(
                    GaVectorStorage<T>.ZeroVector,
                    GaVectorStorage<T>.ZeroVector
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in indexScalarDictionary.GetIndexScalarRecords())
            {
                if (indexScalarSelection(index, scalar))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>>(
                indexScalarDictionary1.CreateGaVectorStorage(),
                indexScalarDictionary2.CreateGaVectorStorage()
            );
        }

        public override Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>> SplitVectorPart(Func<T, bool> scalarSelection)
        {
            if (!TryGetVectorPartList(out var indexScalarDictionary))
                return new Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>>(
                    GaVectorStorage<T>.ZeroVector,
                    GaVectorStorage<T>.ZeroVector
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in indexScalarDictionary.GetIndexScalarRecords())
            {
                if (scalarSelection(scalar))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>>(
                indexScalarDictionary1.CreateGaVectorStorage(),
                indexScalarDictionary2.CreateGaVectorStorage()
            );
        }
    }
}