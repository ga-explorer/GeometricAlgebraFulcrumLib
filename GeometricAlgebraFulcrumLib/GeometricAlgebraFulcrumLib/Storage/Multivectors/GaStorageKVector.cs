using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Factories;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    /// <summary>
    /// Can store the scalar coefficients of a k-vector of any dimension.
    /// The scalars are assumed to be of immutable type such as T, complex, etc.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed record GaStorageKVector<T> 
        : GaStorageKVectorBase<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> ZeroKVector(uint grade)
        {
            return grade switch
            {
                0 => GaStorageScalar<T>.ZeroScalar,
                1 => GaStorageVector<T>.ZeroVector,
                2 => GaStorageBivector<T>.ZeroBivector,
                _ => new GaStorageKVector<T>(grade)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Create(ulong id, T scalar)
        {
            var (grade, index) = id.BasisBladeIdToGradeIndex();

            return grade switch
            {
                0 => GaStorageScalar<T>.Create(scalar),
                1 => GaStorageVector<T>.Create(index, scalar),
                2 => GaStorageBivector<T>.Create(index, scalar),
                _ => new GaStorageKVector<T>(new GaListGradedSingleGradeKey<T>(grade, index, scalar))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Create(uint grade, ulong index, T scalar)
        {
            return grade switch
            {
                0 => GaStorageScalar<T>.Create(scalar),
                1 => GaStorageVector<T>.Create(index, scalar),
                2 => GaStorageBivector<T>.Create(index, scalar),
                _ => new GaStorageKVector<T>(new GaListGradedSingleGradeKey<T>(grade, index, scalar))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Create(uint grade, params T[] indexScalarList)
        {
            if (grade == 0)
            {
                if (indexScalarList.Length > 1)
                    throw new InvalidOperationException();

                return indexScalarList.Length == 1
                    ? GaStorageScalar<T>.Create(indexScalarList[0])
                    : GaStorageScalar<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => GaStorageVector<T>.Create(indexScalarList),
                2 => GaStorageBivector<T>.Create(indexScalarList),
                _ => indexScalarList.Length switch
                {
                    0 => ZeroKVector(grade),
                    1 => new GaStorageKVector<T>(new GaListGradedSingleGradeKey<T>(grade, indexScalarList[0])),
                    _ => new GaStorageKVector<T>(indexScalarList.CreateEvenListDense().CreateGradedListSingleGrade(grade))
                }
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Create(uint grade, IReadOnlyList<T> indexScalarList)
        {
            if (grade == 0)
            {
                if (indexScalarList.Count > 1)
                    throw new InvalidOperationException();

                return indexScalarList.Count == 1
                    ? GaStorageScalar<T>.Create(indexScalarList[0])
                    : GaStorageScalar<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => GaStorageVector<T>.Create(indexScalarList),
                2 => GaStorageBivector<T>.Create(indexScalarList),
                _ => indexScalarList.Count switch
                {
                    0 => ZeroKVector(grade),
                    1 => new GaStorageKVector<T>(new GaListGradedSingleGradeKey<T>(grade, indexScalarList[0])),
                    _ => new GaStorageKVector<T>(indexScalarList.CreateEvenListDense().CreateGradedListSingleGrade(grade))
                }
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Create(uint grade, IEnumerable<T> indexScalarList)
        {
            return Create(grade, indexScalarList.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Create(uint grade, Dictionary<ulong, T> indexScalarDictionary)
        {
            if (grade == 0)
            {
                if (indexScalarDictionary.Count == 0)
                    return GaStorageScalar<T>.ZeroScalar;

                if (indexScalarDictionary.Count > 1)
                    throw new InvalidOperationException();

                var (index, scalar) = indexScalarDictionary.First();

                if (index > 0)
                    throw new InvalidOperationException();

                return GaStorageScalar<T>.Create(scalar);
            }
            
            return grade switch
            {
                1 => GaStorageVector<T>.Create(indexScalarDictionary),
                2 => GaStorageBivector<T>.Create(indexScalarDictionary),
                _ => indexScalarDictionary.Count switch
                {
                    0 => ZeroKVector(grade),
                    1 => new GaStorageKVector<T>(grade, indexScalarDictionary.First()),
                    _ => new GaStorageKVector<T>(indexScalarDictionary.CreateEvenListSparse().CreateGradedListSingleGrade(grade))
                }
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Create(uint grade, IGaListEvenSingleKey<T> indexScalarList)
        {
            return grade switch
            {
                0 => GaStorageScalar<T>.Create(indexScalarList),
                1 => GaStorageVector<T>.Create(indexScalarList),
                2 => GaStorageBivector<T>.Create(indexScalarList),
                _ => indexScalarList.IsEmpty()
                    ? ZeroKVector(grade)
                    : new GaStorageKVector<T>(grade, indexScalarList)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Create(uint grade, IGaListEven<T> indexScalarList)
        {
            if (grade == 0)
            {
                if (indexScalarList.IsEmpty())
                    return GaStorageScalar<T>.ZeroScalar;

                if (indexScalarList.GetSparseCount() > 1 || !indexScalarList.TryGetValue(0, out var scalar))
                    throw new InvalidOperationException();

                return GaStorageScalar<T>.Create(scalar);
            }

            return grade switch
            {
                1 => GaStorageVector<T>.Create(indexScalarList),
                2 => GaStorageBivector<T>.Create(indexScalarList),
                _ => indexScalarList.IsEmpty()
                    ? ZeroKVector(grade)
                    : new GaStorageKVector<T>(grade, indexScalarList)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Create(IGaListGradedSingleGrade<T> singleGradeIndexScalarList)
        {
            return singleGradeIndexScalarList.Grade switch
            {
                0 => GaStorageScalar<T>.Create(singleGradeIndexScalarList),
                1 => GaStorageVector<T>.Create(singleGradeIndexScalarList),
                2 => GaStorageBivector<T>.Create(singleGradeIndexScalarList),
                _ => new GaStorageKVector<T>(singleGradeIndexScalarList)
            };
        }


        public override IGaListGradedSingleGrade<T> SingleGradeIndexScalarList { get; }


        private uint? _vSpaceDimension;
        public override uint MinVSpaceDimension 
            => _vSpaceDimension 
                ??= IndexScalarList.GetMinVSpaceDimensionOfKVector(Grade);
        

        private GaStorageKVector(uint grade)
        {
            if (grade < 3)
                throw new ArgumentException();

            SingleGradeIndexScalarList = 
                new GaListGradedSingleGradeEmpty<T>(grade);
        }
        
        private GaStorageKVector(uint grade, KeyValuePair<ulong, T> indexScalarPair)
        {
            if (grade < 3)
                throw new ArgumentException();

            var (index, scalar) = indexScalarPair;

            SingleGradeIndexScalarList = 
                new GaListGradedSingleGradeKey<T>(grade, index, scalar);
        }
        
        private GaStorageKVector(uint grade, [NotNull] IGaListEvenSingleKey<T> singleKeyList)
        {
            if (grade < 3)
                throw new ArgumentException();

            SingleGradeIndexScalarList = new GaListGradedSingleGradeKey<T>(grade, singleKeyList);
        }
        
        private GaStorageKVector(uint grade, [NotNull] IGaListEven<T> singleKeyList)
        {
            if (grade < 3)
                throw new ArgumentException();

            SingleGradeIndexScalarList = new GaListGradedSingleGrade<T>(grade, singleKeyList);
        }

        private GaStorageKVector([NotNull] IGaListGradedSingleGrade<T> gradeIndexScalarList)
        {
            if (gradeIndexScalarList.Grade < 3)
                throw new ArgumentException();

            SingleGradeIndexScalarList = 
                gradeIndexScalarList;
        }


        public override bool TryGetScalar(out T value)
        {
            value = default;
            return false;
        }

        public override bool TryGetTermByIndex(int index, out GaBasisTerm<T> term)
        {
            var i = (ulong) index;

            if (IndexScalarList.TryGetValue(i, out var value))
            {
                term = value.CreateBasisTerm(Grade, index);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTermByIndex(ulong index, out GaBasisTerm<T> term)
        {
            if (IndexScalarList.TryGetValue(index, out var value))
            {
                term = value.CreateBasisTerm(Grade, index);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(ulong id, out GaBasisTerm<T> term)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            if (grade == Grade && IndexScalarList.TryGetValue(index, out var value))
            {
                term = value.CreateBasisTerm(Grade, index);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(uint grade, ulong index, out GaBasisTerm<T> term)
        {
            if (grade == Grade && IndexScalarList.TryGetValue(index, out var value))
            {
                term = value.CreateBasisTerm(Grade, index);
                return true;
            }

            term = null;
            return false;
        }

        
        public override IGaStorageVector<T> GetVectorPart()
        {
            return GaStorageVector<T>.ZeroVector;
        }

        public override IGaStorageVector<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            return GaStorageVector<T>.ZeroVector;
        }

        public override IGaStorageVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return GaStorageVector<T>.ZeroVector;
        }

        public override IGaStorageVector<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            return GaStorageVector<T>.ZeroVector;
        }

        public override IGaStorageBivector<T> GetBivectorPart()
        {
            return GaStorageBivector<T>.ZeroBivector;
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            return GaStorageBivector<T>.ZeroBivector;
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return GaStorageBivector<T>.ZeroBivector;
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            return GaStorageBivector<T>.ZeroBivector;
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade)
        {
            return grade == Grade 
                ? this 
                : ZeroKVector(grade);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            if (grade != Grade)
                return ZeroKVector(grade);

            var indexScalarDictionary = 
                IndexScalarList.FilterByValue(scalarSelection);

            return Create(Grade, indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            if (grade != Grade)
                return ZeroKVector(grade);

            var indexScalarDictionary = 
                IndexScalarList.FilterByKeyValue(indexScalarSelection);

            return Create(Grade, indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            if (grade != Grade)
                return ZeroKVector(grade);

            var indexScalarDictionary = 
                IndexScalarList.FilterByKey(indexSelection);

            return Create(Grade, indexScalarDictionary);
        }

        

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var indexScalarDictionary = 
                IndexScalarList.FilterByKey(
                    index => idSelection(index.BasisBladeIndexToId(Grade))
                );

            return Create(Grade, indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var indexScalarDictionary = 
                IndexScalarList.FilterByKey(
                    index => gradeIndexSelection(Grade, index)
                );

            return Create(Grade, indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary = 
                IndexScalarList.FilterByValue(scalarSelection);

            return Create(Grade, indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var indexScalarDictionary = 
                IndexScalarList.FilterByKeyValue(
                    (index, value) => idScalarSelection(
                        index.BasisBladeIndexToId(Grade), 
                        value
                    )
                );

            return Create(Grade, indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var indexScalarDictionary = 
                IndexScalarList.FilterByKeyValue(
                    (index, scalar) => gradeIndexScalarSelection(Grade, index, scalar)
                );

            return Create(Grade, indexScalarDictionary);
        }

        public override Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            if (Grade != 1)
                return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                    GaStorageVector<T>.ZeroVector,
                    GaStorageVector<T>.ZeroVector
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
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
            if (Grade != 1)
                return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                    GaStorageVector<T>.ZeroVector,
                    GaStorageVector<T>.ZeroVector
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
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
            if (Grade != 1)
                return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                    GaStorageVector<T>.ZeroVector,
                    GaStorageVector<T>.ZeroVector
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
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


        public override IEnumerable<GaBasisBlade> GetBasisBlades()
        {
            return IndexScalarList.GetKeyValueRecords().Select(pair => 
                Grade.CreateBasisBlade(pair.Key)
            );
        }
        
        public override IEnumerable<GaBasisTerm<T>> GetTerms()
        {
            return IndexScalarList.GetKeyValueRecords().Select(pair => 
                pair.Value.CreateBasisTerm(Grade, pair.Key)
            );
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, bool> idSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
            {
                var id = index.BasisBladeIndexToId(Grade);

                if (idSelection(id))
                    yield return scalar.CreateBasisTerm(Grade, index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
            {
                if (gradeIndexSelection(Grade, index))
                    yield return scalar.CreateBasisTerm(Grade, index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<T, bool> scalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
            {
                if (scalarSelection(scalar))
                    yield return scalar.CreateBasisTerm(Grade, index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
            {
                var id = index.BasisBladeIndexToId(Grade);

                if (idScalarSelection(id, scalar))
                    yield return scalar.CreateBasisTerm(Grade, index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
            {
                if (gradeIndexScalarSelection(Grade, index, scalar))
                    yield return scalar.CreateBasisTerm(Grade, index);
            }
        }
    }
}
