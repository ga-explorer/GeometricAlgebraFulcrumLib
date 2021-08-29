using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    /// <summary>
    /// Can store the scalar coefficients of a k-vector of any dimension.
    /// The scalars are assumed to be of immutable type such as T, complex, etc.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed record GaKVectorStorage<T> 
        : GaKVectorStorageBase<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> ZeroKVector(uint grade)
        {
            return grade switch
            {
                0 => GaScalarStorage<T>.ZeroScalar,
                1 => GaVectorStorage<T>.ZeroVector,
                2 => GaBivectorStorage<T>.ZeroBivector,
                _ => new GaKVectorStorage<T>(grade)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Create(ulong id, T scalar)
        {
            var (grade, index) = id.BasisBladeIdToGradeIndex();

            return grade switch
            {
                0 => GaScalarStorage<T>.Create(scalar),
                1 => GaVectorStorage<T>.Create(index, scalar),
                2 => GaBivectorStorage<T>.Create(index, scalar),
                _ => new GaKVectorStorage<T>(new LaVectorSingleGradeIndexStorage<T>(grade, index, scalar))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Create(uint grade, ulong index, T scalar)
        {
            return grade switch
            {
                0 => GaScalarStorage<T>.Create(scalar),
                1 => GaVectorStorage<T>.Create(index, scalar),
                2 => GaBivectorStorage<T>.Create(index, scalar),
                _ => new GaKVectorStorage<T>(new LaVectorSingleGradeIndexStorage<T>(grade, index, scalar))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Create(uint grade, params T[] indexScalarList)
        {
            if (grade == 0)
            {
                if (indexScalarList.Length > 1)
                    throw new InvalidOperationException();

                return indexScalarList.Length == 1
                    ? GaScalarStorage<T>.Create(indexScalarList[0])
                    : GaScalarStorage<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => GaVectorStorage<T>.Create(indexScalarList),
                2 => GaBivectorStorage<T>.Create(indexScalarList),
                _ => indexScalarList.Length switch
                {
                    0 => ZeroKVector(grade),
                    1 => new GaKVectorStorage<T>(new LaVectorSingleGradeIndexStorage<T>(grade, indexScalarList[0])),
                    _ => new GaKVectorStorage<T>(indexScalarList.CreateLaVectorDenseStorage().CreateLaVectorSingleGradeStorage(grade))
                }
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Create(uint grade, IReadOnlyList<T> indexScalarList)
        {
            if (grade == 0)
            {
                if (indexScalarList.Count > 1)
                    throw new InvalidOperationException();

                return indexScalarList.Count == 1
                    ? GaScalarStorage<T>.Create(indexScalarList[0])
                    : GaScalarStorage<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => GaVectorStorage<T>.Create(indexScalarList),
                2 => GaBivectorStorage<T>.Create(indexScalarList),
                _ => indexScalarList.Count switch
                {
                    0 => ZeroKVector(grade),
                    1 => new GaKVectorStorage<T>(new LaVectorSingleGradeIndexStorage<T>(grade, indexScalarList[0])),
                    _ => new GaKVectorStorage<T>(indexScalarList.CreateLaVectorDenseStorage().CreateLaVectorSingleGradeStorage(grade))
                }
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Create(uint grade, IEnumerable<T> indexScalarList)
        {
            return Create(grade, indexScalarList.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Create(uint grade, Dictionary<ulong, T> indexScalarDictionary)
        {
            if (grade == 0)
            {
                if (indexScalarDictionary.Count == 0)
                    return GaScalarStorage<T>.ZeroScalar;

                if (indexScalarDictionary.Count > 1)
                    throw new InvalidOperationException();

                var (index, scalar) = indexScalarDictionary.First();

                if (index > 0)
                    throw new InvalidOperationException();

                return GaScalarStorage<T>.Create(scalar);
            }
            
            return grade switch
            {
                1 => GaVectorStorage<T>.Create(indexScalarDictionary),
                2 => GaBivectorStorage<T>.Create(indexScalarDictionary),
                _ => indexScalarDictionary.Count switch
                {
                    0 => ZeroKVector(grade),
                    1 => new GaKVectorStorage<T>(grade, indexScalarDictionary.First()),
                    _ => new GaKVectorStorage<T>(indexScalarDictionary.CreateLaVectorSparseStorage().CreateLaVectorSingleGradeStorage(grade))
                }
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Create(uint grade, ILaVectorSingleIndexEvenStorage<T> indexScalarList)
        {
            return grade switch
            {
                0 => GaScalarStorage<T>.Create(indexScalarList),
                1 => GaVectorStorage<T>.Create(indexScalarList),
                2 => GaBivectorStorage<T>.Create(indexScalarList),
                _ => indexScalarList.IsEmpty()
                    ? ZeroKVector(grade)
                    : new GaKVectorStorage<T>(grade, indexScalarList)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Create(uint grade, ILaVectorEvenStorage<T> indexScalarList)
        {
            if (grade == 0)
            {
                if (indexScalarList.IsEmpty())
                    return GaScalarStorage<T>.ZeroScalar;

                if (indexScalarList.GetSparseCount() > 1 || !indexScalarList.TryGetScalar(0, out var scalar))
                    throw new InvalidOperationException();

                return GaScalarStorage<T>.Create(scalar);
            }

            return grade switch
            {
                1 => GaVectorStorage<T>.Create(indexScalarList),
                2 => GaBivectorStorage<T>.Create(indexScalarList),
                _ => indexScalarList.IsEmpty()
                    ? ZeroKVector(grade)
                    : new GaKVectorStorage<T>(grade, indexScalarList)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Create(ILaVectorSingleGradeStorage<T> singleGradeIndexScalarList)
        {
            return singleGradeIndexScalarList.Grade switch
            {
                0 => GaScalarStorage<T>.Create(singleGradeIndexScalarList),
                1 => GaVectorStorage<T>.Create(singleGradeIndexScalarList),
                2 => GaBivectorStorage<T>.Create(singleGradeIndexScalarList),
                _ => new GaKVectorStorage<T>(singleGradeIndexScalarList)
            };
        }


        public override ILaVectorSingleGradeStorage<T> SingleGradeIndexScalarList { get; }


        private uint? _vSpaceDimension;
        public override uint MinVSpaceDimension 
            => _vSpaceDimension 
                ??= IndexScalarList.GetMinVSpaceDimensionOfKVector(Grade);
        

        private GaKVectorStorage(uint grade)
        {
            if (grade < 3)
                throw new ArgumentException();

            SingleGradeIndexScalarList = 
                new LaVectorEmptySingleGradeStorage<T>(grade);
        }
        
        private GaKVectorStorage(uint grade, KeyValuePair<ulong, T> indexScalarPair)
        {
            if (grade < 3)
                throw new ArgumentException();

            var (index, scalar) = indexScalarPair;

            SingleGradeIndexScalarList = 
                new LaVectorSingleGradeIndexStorage<T>(grade, index, scalar);
        }
        
        private GaKVectorStorage(uint grade, [NotNull] ILaVectorSingleIndexEvenStorage<T> singleKeyList)
        {
            if (grade < 3)
                throw new ArgumentException();

            SingleGradeIndexScalarList = new LaVectorSingleGradeIndexStorage<T>(grade, singleKeyList);
        }
        
        private GaKVectorStorage(uint grade, [NotNull] ILaVectorEvenStorage<T> singleKeyList)
        {
            if (grade < 3)
                throw new ArgumentException();

            SingleGradeIndexScalarList = new LaVectorSingleGradeStorage<T>(grade, singleKeyList);
        }

        private GaKVectorStorage([NotNull] ILaVectorSingleGradeStorage<T> gradeIndexScalarList)
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

            if (IndexScalarList.TryGetScalar(i, out var value))
            {
                term = value.CreateBasisTerm(Grade, index);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTermByIndex(ulong index, out GaBasisTerm<T> term)
        {
            if (IndexScalarList.TryGetScalar(index, out var value))
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

            if (grade == Grade && IndexScalarList.TryGetScalar(index, out var value))
            {
                term = value.CreateBasisTerm(Grade, index);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(uint grade, ulong index, out GaBasisTerm<T> term)
        {
            if (grade == Grade && IndexScalarList.TryGetScalar(index, out var value))
            {
                term = value.CreateBasisTerm(Grade, index);
                return true;
            }

            term = null;
            return false;
        }

        
        public override IGaVectorStorage<T> GetVectorPart()
        {
            return GaVectorStorage<T>.ZeroVector;
        }

        public override IGaVectorStorage<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            return GaVectorStorage<T>.ZeroVector;
        }

        public override IGaVectorStorage<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return GaVectorStorage<T>.ZeroVector;
        }

        public override IGaVectorStorage<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            return GaVectorStorage<T>.ZeroVector;
        }

        public override IGaBivectorStorage<T> GetBivectorPart()
        {
            return GaBivectorStorage<T>.ZeroBivector;
        }

        public override IGaBivectorStorage<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            return GaBivectorStorage<T>.ZeroBivector;
        }

        public override IGaBivectorStorage<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return GaBivectorStorage<T>.ZeroBivector;
        }

        public override IGaBivectorStorage<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            return GaBivectorStorage<T>.ZeroBivector;
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade)
        {
            return grade == Grade 
                ? this 
                : ZeroKVector(grade);
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            if (grade != Grade)
                return ZeroKVector(grade);

            var indexScalarDictionary = 
                IndexScalarList.FilterByScalar(scalarSelection);

            return Create(Grade, indexScalarDictionary);
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            if (grade != Grade)
                return ZeroKVector(grade);

            var indexScalarDictionary = 
                IndexScalarList.FilterByIndexScalar(indexScalarSelection);

            return Create(Grade, indexScalarDictionary);
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            if (grade != Grade)
                return ZeroKVector(grade);

            var indexScalarDictionary = 
                IndexScalarList.FilterByIndex(indexSelection);

            return Create(Grade, indexScalarDictionary);
        }

        

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var indexScalarDictionary = 
                IndexScalarList.FilterByIndex(
                    index => idSelection(index.BasisBladeIndexToId(Grade))
                );

            return Create(Grade, indexScalarDictionary);
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var indexScalarDictionary = 
                IndexScalarList.FilterByIndex(
                    index => gradeIndexSelection(Grade, index)
                );

            return Create(Grade, indexScalarDictionary);
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary = 
                IndexScalarList.FilterByScalar(scalarSelection);

            return Create(Grade, indexScalarDictionary);
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var indexScalarDictionary = 
                IndexScalarList.FilterByIndexScalar(
                    (index, value) => idScalarSelection(
                        index.BasisBladeIndexToId(Grade), 
                        value
                    )
                );

            return Create(Grade, indexScalarDictionary);
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var indexScalarDictionary = 
                IndexScalarList.FilterByIndexScalar(
                    (index, scalar) => gradeIndexScalarSelection(Grade, index, scalar)
                );

            return Create(Grade, indexScalarDictionary);
        }

        public override Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            if (Grade != 1)
                return new Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>>(
                    GaVectorStorage<T>.ZeroVector,
                    GaVectorStorage<T>.ZeroVector
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
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
            if (Grade != 1)
                return new Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>>(
                    GaVectorStorage<T>.ZeroVector,
                    GaVectorStorage<T>.ZeroVector
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
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
            if (Grade != 1)
                return new Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>>(
                    GaVectorStorage<T>.ZeroVector,
                    GaVectorStorage<T>.ZeroVector
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
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


        public override IEnumerable<GaBasisBlade> GetBasisBlades()
        {
            return IndexScalarList.GetIndexScalarRecords().Select(pair => 
                Grade.CreateBasisBlade(pair.Index)
            );
        }
        
        public override IEnumerable<GaBasisTerm<T>> GetTerms()
        {
            return IndexScalarList.GetIndexScalarRecords().Select(pair => 
                pair.Scalar.CreateBasisTerm(Grade, pair.Index)
            );
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, bool> idSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
            {
                var id = index.BasisBladeIndexToId(Grade);

                if (idSelection(id))
                    yield return scalar.CreateBasisTerm(Grade, index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
            {
                if (gradeIndexSelection(Grade, index))
                    yield return scalar.CreateBasisTerm(Grade, index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<T, bool> scalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
            {
                if (scalarSelection(scalar))
                    yield return scalar.CreateBasisTerm(Grade, index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
            {
                var id = index.BasisBladeIndexToId(Grade);

                if (idScalarSelection(id, scalar))
                    yield return scalar.CreateBasisTerm(Grade, index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
            {
                if (gradeIndexScalarSelection(Grade, index, scalar))
                    yield return scalar.CreateBasisTerm(Grade, index);
            }
        }
    }
}
