using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Factories;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Structures;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public sealed record GaStorageMultivectorGraded<T>
        : GaStorageMultivectorBase<T>, IGaStorageMultivectorGraded<T>
    {
        public static GaStorageMultivectorGraded<T> ZeroMultivector { get; }
            = new GaStorageMultivectorGraded<T>(GaListGradedEmpty<T>.EmptyList);


        public static GaStorageMultivectorGraded<T> Create(IGaListGraded<T> gradeIndexScalarList)
        {
            return new GaStorageMultivectorGraded<T>(
                gradeIndexScalarList
            );
        }


        public IGaListGraded<T> GradeIndexScalarList { get; }

        private uint? _vSpaceDimension;
        public override uint MinVSpaceDimension 
            => _vSpaceDimension 
                ??= GradeIndexScalarList.GetMinVSpaceDimensionOfMultivector();

        public override int GradesCount 
            => GradeIndexScalarList.GradesCount;

        public override int TermsCount 
            => GradeIndexScalarList.GetGradeListRecords().Sum(p => p.List.GetSparseCount());
        
        public override bool IsUniform => false;

        public override bool IsGraded => true;


        private GaStorageMultivectorGraded([NotNull] IGaListGraded<T> gradeIndexScalarList)
        {
            GradeIndexScalarList = gradeIndexScalarList;
        }

        
        public IGaStorageMultivectorGraded<T> GetGradedMultivectorCopy()
        {
            var gradeIndexScalarList = 
                GradeIndexScalarList.GetCopy();

            return new GaStorageMultivectorGraded<T>(
                gradeIndexScalarList
            );
        }
        
        public IGaStorageMultivectorGraded<T2> MapGradedMultivectorScalars<T2>(Func<T, T2> scalarMapping)
        {
            var gradeIndexScalarList =
                GradeIndexScalarList.MapValues(scalarMapping);

            return new GaStorageMultivectorGraded<T2>(
                gradeIndexScalarList
            );
        }

        public IGaStorageMultivectorGraded<T2> MapGradedMultivectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            var gradeIndexScalarList =
                GradeIndexScalarList.MapValues(
                    (grade, index, scalar) => 
                        idScalarMapping(index.BasisBladeIndexToId(grade), scalar)
                );

            return new GaStorageMultivectorGraded<T2>(
                gradeIndexScalarList
            );
        }
        
        public IGaStorageMultivectorGraded<T2> MapGradedMultivectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            var gradeIndexScalarList =
                GradeIndexScalarList.MapValues(
                    (_, index, scalar) => 
                        indexScalarMapping(index, scalar)
                );

            return new GaStorageMultivectorGraded<T2>(
                gradeIndexScalarList
            );
        }

        public IGaStorageMultivectorGraded<T2> MapGradedMultivectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            var gradeIndexScalarList =
                GradeIndexScalarList.MapValues(gradeIndexScalarMapping);

            return new GaStorageMultivectorGraded<T2>(
                gradeIndexScalarList
            );
        }


        public override bool ContainsVectorPart()
        {
            return GradeIndexScalarList.ContainsGrade(1);
        }

        public override bool TryGetScalar(out T value)
        {
            return GradeIndexScalarList.TryGetValue(0, 0, out value);
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
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarList.GetGradeListRecords())
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
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarList.GetGradeListRecords())
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
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarList.GetGradeListRecords())
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
            return GradeIndexScalarList.GetLists().Count(pair => !pair.IsEmpty()) < 2;
        }

        public override bool IsKVector(uint grade)
        {
            foreach (var (g, indexScalarDictionary) in GradeIndexScalarList.GetGradeListRecords())
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

            return GradeIndexScalarList.ContainsKey(grade, index);
        }

        public override bool ContainsTerm(uint grade, ulong index)
        {
            return GradeIndexScalarList.ContainsKey(grade, index);
        }

        public override bool ContainsScalarPart()
        {
            return GradeIndexScalarList.ContainsGrade(0);
        }


        public override bool TryGetTermScalar(ulong id, out T value)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            if (GradeIndexScalarList.TryGetList(grade, out var storage))
                return storage.TryGetValue(index, out value);

            value = default;
            return false;
        }

        public override bool TryGetTermScalar(uint grade, ulong index, out T value)
        {
            if (GradeIndexScalarList.TryGetList(grade, out var storage))
                return storage.TryGetValue(index, out value);

            value = default;
            return false;
        }


        public override bool TryGetKVectorPart(uint grade, out IGaStorageKVector<T> kVector)
        {
            if (GradeIndexScalarList.TryGetList(grade, out var evenDictionary))
            {
                kVector = GaStorageKVector<T>.Create(grade, evenDictionary);
                return true;
            }

            kVector = null;
            return false;
        }

        public override bool TryGetScalarPartList(out IGaListEven<T> indexScalarList)
        {
            return GradeIndexScalarList.TryGetList(0, out indexScalarList);
        }

        public override bool TryGetVectorPartList(out IGaListEven<T> indexScalarDictionary)
        {
            if (GradeIndexScalarList.TryGetList(1U, out indexScalarDictionary) && !indexScalarDictionary.IsEmpty())
                return true;

            indexScalarDictionary = GaListEvenEmpty<T>.EmptyList;
            return false;
        }

        public override bool TryGetBivectorPartList(out IGaListEven<T> indexScalarDictionary)
        {
            if (GradeIndexScalarList.TryGetList(2U, out indexScalarDictionary) && !indexScalarDictionary.IsEmpty())
                return true;

            indexScalarDictionary = GaListEvenEmpty<T>.EmptyList;
            return false;

        }

        public override bool TryGetKVectorPartList(uint grade, out IGaListEven<T> indexScalarDictionary)
        {
            if (GradeIndexScalarList.TryGetList(grade, out indexScalarDictionary) && !indexScalarDictionary.IsEmpty())
                return true;

            indexScalarDictionary = GaListEvenEmpty<T>.EmptyList;
            return false;

        }

        public override IGaListEven<T> GetIdScalarList()
        {
            return GradeIndexScalarList
                .GetGradeListRecords()
                .SelectMany(storage => 
                    storage.List.GetKeyValueRecords().Select(pair => 
                        new KeyValuePair<ulong, T>(
                            pair.Key.BasisBladeIndexToId(storage.Grade), 
                            pair.Value
                        )
                    )
                ).ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                ).CreateEvenList();
        }

        public override IGaListGraded<T> GetGradeIndexScalarList()
        {
            return GradeIndexScalarList;
        }

        public override IGaListEven<T> GetIndexScalarList(uint grade)
        {
            return GradeIndexScalarList.TryGetList(grade, out var indexScalarList)
                ? indexScalarList
                : GaListEvenEmpty<T>.EmptyList;
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

        public override IGaListEven<T> GetScalarPartList()
        {
            return 
                GradeIndexScalarList.TryGetList(0, out var evenDictionary) && 
                evenDictionary.TryGetValue(0UL, out var scalar)
                    ? scalar.CreateEvenListSingleKeyZero()
                    : GaListEvenEmpty<T>.EmptyList;
        }

        public override IGaListEven<T> GetVectorPartList()
        {
            return 
                GradeIndexScalarList.TryGetList(1U, out var evenDictionary) && 
                !evenDictionary.IsEmpty()
                    ? evenDictionary
                    : GaListEvenEmpty<T>.EmptyList;
        }

        public override IGaListEven<T> GetBivectorPartList()
        {
            return 
                GradeIndexScalarList.TryGetList(2U, out var evenDictionary) && 
                !evenDictionary.IsEmpty()
                    ? evenDictionary
                    : GaListEvenEmpty<T>.EmptyList;
        }

        public override IGaListEven<T> GetKVectorPartList(uint grade)
        {
            return 
                GradeIndexScalarList.TryGetList(grade, out var evenDictionary) && 
                !evenDictionary.IsEmpty()
                    ? evenDictionary
                    : GaListEvenEmpty<T>.EmptyList;
        }

        public override bool TryGetScalarPart(out IGaStorageScalar<T> scalar)
        {
            if (GradeIndexScalarList.TryGetList(0U, out var evenDictionary) && evenDictionary.TryGetValue(0, out var s))
            {
                scalar = GaStorageScalar<T>.Create(s);
                return true;
            }

            scalar = null;
            return false;
        }

        public override bool TryGetVectorPart(out IGaStorageVector<T> vector)
        {
            if (GradeIndexScalarList.TryGetList(1U, out var evenDictionary))
            {
                vector = GaStorageVector<T>.Create(evenDictionary);
                return true;
            }

            vector = null;
            return false;
        }

        public override bool TryGetBivectorPart(out IGaStorageBivector<T> bivector)
        {
            if (GradeIndexScalarList.TryGetList(2U, out var evenDictionary))
            {
                bivector = GaStorageBivector<T>.Create(evenDictionary);
                return true;
            }

            bivector = null;
            return false;
        }


        public override IEnumerable<ulong> GetIds()
        {
            return GradeIndexScalarList
                .GetGradeListRecords()
                .SelectMany(pair => 
                    pair.List.GetKeys().Select(
                        index => index.BasisBladeIndexToId(pair.Grade)
                    )
                );
        }

        public override IEnumerable<GaRecordGradeKey> GetGradeIndexRecords()
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarList.GetGradeListRecords())
            foreach (var index in indexScalarDictionary.GetKeys())
                yield return new GaRecordGradeKey(grade, index);
        }

        public override IEnumerable<GaBasisBlade> GetBasisBlades()
        {
            return GradeIndexScalarList
                .GetGradeListRecords()
                .SelectMany(storage => 
                    storage.List.GetKeyValueRecords().Select(pair => 
                        storage.Grade.CreateBasisBlade(pair.Key)
                    )
                );
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms()
        {
            return GradeIndexScalarList
                .GetGradeListRecords()
                .SelectMany(storage => 
                    storage.List.GetKeyValueRecords().Select(pair => 
                        pair.Value.CreateBasisTerm(storage.Grade, pair.Key)
                    )
                );
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, bool> idSelection)
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarList.GetGradeListRecords())
            {
                foreach (var (index, scalar) in indexScalarDictionary.GetKeyValueRecords())
                {
                    var id = index.BasisBladeIndexToId(grade);

                    if (idSelection(id))
                        yield return scalar.CreateBasisTerm(grade, index);
                }
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarList.GetGradeListRecords())
            {
                foreach (var (index, scalar) in indexScalarDictionary.GetKeyValueRecords())
                {
                    if (gradeIndexSelection(grade, index))
                        yield return scalar.CreateBasisTerm(grade, index);
                }
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<T, bool> scalarSelection)
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarList.GetGradeListRecords())
            {
                foreach (var (index, scalar) in indexScalarDictionary.GetKeyValueRecords())
                {
                    if (scalarSelection(scalar))
                        yield return scalar.CreateBasisTerm(grade, index);
                }
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarList.GetGradeListRecords())
            {
                foreach (var (index, scalar) in indexScalarDictionary.GetKeyValueRecords())
                {
                    var id = index.BasisBladeIndexToId(grade);

                    if (idScalarSelection(id, scalar))
                        yield return scalar.CreateBasisTerm(grade, index);
                }
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarList.GetGradeListRecords())
            {
                foreach (var (index, scalar) in indexScalarDictionary.GetKeyValueRecords())
                {
                    if (gradeIndexScalarSelection(grade, index, scalar))
                        yield return scalar.CreateBasisTerm(grade, index);
                }
            }
        }

        public override IEnumerable<GaRecordKeyValue<T>> GetIdScalarRecords()
        {
            return GradeIndexScalarList
                .GetGradeListRecords()
                .SelectMany(storage => 
                    storage.List.GetKeyValueRecords().Select(pair => 
                        new GaRecordKeyValue<T>(
                            pair.Key.BasisBladeIndexToId(storage.Grade), 
                            pair.Value
                        )
                    )
                );
        }

        public override IEnumerable<GaRecordKeyValue<T>> GetIndexScalarRecords(uint grade)
        {
            return GradeIndexScalarList.GetList(grade).GetKeyValueRecords();
        }

        public override IEnumerable<GaRecordGradeKeyValue<T>> GetGradeIndexScalarRecords()
        {
            return GradeIndexScalarList
                .GetGradeListRecords()
                .SelectMany(storage => 
                    storage.List.GetKeyValueRecords().Select(pair => 
                        new GaRecordGradeKeyValue<T>(storage.Grade, pair.Key, pair.Value)
                    )
                );
        }

        public override IEnumerable<T> GetScalars()
        {
            return GradeIndexScalarList.GetLists()
                .SelectMany(storage => storage.GetValues());
        }


        public override IGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity, IGaScalarProcessor<T> scalarProcessor)
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
        public override GaListEvenTree<T> GetBinaryTree(int treeDepth)
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


        public override IGaStorageMultivectorSparse<T> ToSparseMultivector()
        {
            return GaStorageMultivectorSparse<T>.Create(
                GradeIndexScalarList.ToEvenList()
            );
        }

        public override IGaStorageMultivectorGraded<T> ToGradedMultivector()
        {
            return this;
        }

        public override IGaStorageVector<T> GetVectorPart()
        {
            if (!GradeIndexScalarList.TryGetList(1, out var indexScalarDictionary))
                return GaStorageVector<T>.ZeroVector;
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaStorageVector<T>.ZeroVector
                : GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageVector<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            if (!GradeIndexScalarList.TryGetList(1, out var indexScalarDictionary))
                return GaStorageVector<T>.ZeroVector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByValue(scalarSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaStorageVector<T>.ZeroVector
                : GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            if (!GradeIndexScalarList.TryGetList(1, out var indexScalarDictionary))
                return GaStorageVector<T>.ZeroVector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByKeyValue(indexScalarSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaStorageVector<T>.ZeroVector
                : GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageVector<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            if (!GradeIndexScalarList.TryGetList(1, out var indexScalarDictionary))
                return GaStorageVector<T>.ZeroVector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByKey(indexSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaStorageVector<T>.ZeroVector
                : GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageBivector<T> GetBivectorPart()
        {
            if (!GradeIndexScalarList.TryGetList(2, out var indexScalarDictionary))
                return GaStorageBivector<T>.ZeroBivector;
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaStorageBivector<T>.ZeroBivector
                : GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            if (!GradeIndexScalarList.TryGetList(2, out var indexScalarDictionary))
                return GaStorageBivector<T>.ZeroBivector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByValue(scalarSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaStorageBivector<T>.ZeroBivector
                : GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            if (!GradeIndexScalarList.TryGetList(2, out var indexScalarDictionary))
                return GaStorageBivector<T>.ZeroBivector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByKeyValue(indexScalarSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaStorageBivector<T>.ZeroBivector
                : GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            if (!GradeIndexScalarList.TryGetList(2, out var indexScalarDictionary))
                return GaStorageBivector<T>.ZeroBivector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByKey(indexSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaStorageBivector<T>.ZeroBivector
                : GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade)
        {
            if (!GradeIndexScalarList.TryGetList(grade, out var indexScalarDictionary))
                return GaStorageKVector<T>.ZeroKVector(grade);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaStorageKVector<T>.ZeroKVector(grade)
                : GaStorageKVector<T>.Create(grade, indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            if (!GradeIndexScalarList.TryGetList(grade, out var indexScalarDictionary))
                return GaStorageKVector<T>.ZeroKVector(grade);

            indexScalarDictionary = 
                indexScalarDictionary.FilterByValue(scalarSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaStorageKVector<T>.ZeroKVector(grade)
                : GaStorageKVector<T>.Create(grade, indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            if (!GradeIndexScalarList.TryGetList(grade, out var indexScalarDictionary))
                return GaStorageKVector<T>.ZeroKVector(grade);

            indexScalarDictionary = 
                indexScalarDictionary.FilterByKeyValue(indexScalarSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaStorageKVector<T>.ZeroKVector(grade)
                : GaStorageKVector<T>.Create(grade, indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            if (!GradeIndexScalarList.TryGetList(grade, out var indexScalarDictionary))
                return GaStorageKVector<T>.ZeroKVector(grade);

            indexScalarDictionary = 
                indexScalarDictionary.FilterByKey(indexSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? GaStorageKVector<T>.ZeroKVector(grade)
                : GaStorageKVector<T>.Create(grade, indexScalarDictionary);
        }
        
        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarList) in GradeIndexScalarList.GetGradeListRecords())
            {
                var indexScalarDictionary = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarList.GetKeyValueRecords())
                {
                    var id = index.BasisBladeIndexToId(grade);

                    if (idSelection(id))
                        indexScalarDictionary.Add(index, scalar);
                }

                if (indexScalarDictionary.Count > 0)
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
            }

            var gradeIndexScalarList =
                gradeIndexScalarDictionary.CreateGradedList();

            return new GaStorageMultivectorGraded<T>(
                gradeIndexScalarList
            );
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarList) in GradeIndexScalarList.GetGradeListRecords())
            {
                var indexScalarDictionary = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarList.GetKeyValueRecords())
                {
                    if (gradeIndexSelection(grade, index))
                        indexScalarDictionary.Add(index, scalar);
                }

                if (indexScalarDictionary.Count > 0)
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
            }

            var gradeIndexScalarList =
                gradeIndexScalarDictionary.CreateGradedList();

            return new GaStorageMultivectorGraded<T>(
                gradeIndexScalarList
            );
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarList) in GradeIndexScalarList.GetGradeListRecords())
            {
                var indexScalarDictionary = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarList.GetKeyValueRecords())
                {
                    if (scalarSelection(scalar))
                        indexScalarDictionary.Add(index, scalar);
                }

                if (indexScalarDictionary.Count > 0)
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
            }

            var gradeIndexScalarList =
                gradeIndexScalarDictionary.CreateGradedList();

            return new GaStorageMultivectorGraded<T>(
                gradeIndexScalarList
            );
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarList) in GradeIndexScalarList.GetGradeListRecords())
            {
                var indexScalarDictionary = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarList.GetKeyValueRecords())
                {
                    var id = index.BasisBladeIndexToId(grade);

                    if (idScalarSelection(id, scalar))
                        indexScalarDictionary.Add(index, scalar);
                }

                if (indexScalarDictionary.Count > 0)
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
            }

            var gradeIndexScalarList =
                gradeIndexScalarDictionary.CreateGradedList();

            return new GaStorageMultivectorGraded<T>(
                gradeIndexScalarList
            );
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarList) in GradeIndexScalarList.GetGradeListRecords())
            {
                var indexScalarDictionary = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarList.GetKeyValueRecords())
                {
                    if (gradeIndexScalarSelection(grade, index, scalar))
                        indexScalarDictionary.Add(index, scalar);
                }

                if (indexScalarDictionary.Count > 0)
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
            }

            var gradeIndexScalarList =
                gradeIndexScalarDictionary.CreateGradedList();

            return new GaStorageMultivectorGraded<T>(
                gradeIndexScalarList
            );
        }

        public override Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            if (!TryGetVectorPartList(out var indexScalarDictionary))
                return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                    GaStorageVector<T>.ZeroVector,
                    GaStorageVector<T>.ZeroVector
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in indexScalarDictionary.GetKeyValueRecords())
            {
                if (indexSelection(index))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                indexScalarDictionary1.CreateStorageVector(),
                indexScalarDictionary2.CreateStorageVector()
            );
        }

        public override Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            if (!TryGetVectorPartList(out var indexScalarDictionary))
                return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                    GaStorageVector<T>.ZeroVector,
                    GaStorageVector<T>.ZeroVector
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in indexScalarDictionary.GetKeyValueRecords())
            {
                if (indexScalarSelection(index, scalar))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                indexScalarDictionary1.CreateStorageVector(),
                indexScalarDictionary2.CreateStorageVector()
            );
        }

        public override Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<T, bool> scalarSelection)
        {
            if (!TryGetVectorPartList(out var indexScalarDictionary))
                return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                    GaStorageVector<T>.ZeroVector,
                    GaStorageVector<T>.ZeroVector
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in indexScalarDictionary.GetKeyValueRecords())
            {
                if (scalarSelection(scalar))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                indexScalarDictionary1.CreateStorageVector(),
                indexScalarDictionary2.CreateStorageVector()
            );
        }
    }
}