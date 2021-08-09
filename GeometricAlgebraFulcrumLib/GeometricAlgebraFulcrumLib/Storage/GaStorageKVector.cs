using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Terms;
using GeometricAlgebraFulcrumLib.Structures.Even;

namespace GeometricAlgebraFulcrumLib.Storage
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
                _ => new GaStorageKVector<T>(grade, GaEvenDictionaryEmpty<T>.DefaultDictionary, 0UL)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Create(ulong id, T scalar)
        {
            var (grade, index) 
                = id.BasisBladeGradeIndex();

            var evenDictionary = 
                scalar.CreateEvenDictionarySingleKey(index);
            
            return grade switch
            {
                0 => GaStorageScalar<T>.Create(evenDictionary),
                1 => GaStorageVector<T>.Create(evenDictionary),
                2 => GaStorageBivector<T>.Create(evenDictionary),
                _ => new GaStorageKVector<T>(grade, evenDictionary, evenDictionary.GetMaxBasisBladeId(grade))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Create(uint grade, ulong index, T scalar)
        {
            var evenDictionary = 
                scalar.CreateEvenDictionarySingleKey(index);
            
            return grade switch
            {
                0 => GaStorageScalar<T>.Create(evenDictionary),
                1 => GaStorageVector<T>.Create(evenDictionary),
                2 => GaStorageBivector<T>.Create(evenDictionary),
                _ => new GaStorageKVector<T>(grade, evenDictionary, evenDictionary.GetMaxBasisBladeId(grade))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Create(uint grade, params T[] indexScalarList)
        {
            var evenDictionary = 
                indexScalarList.CreateEvenDictionaryList();

            return grade switch
            {
                0 => GaStorageScalar<T>.Create(evenDictionary),
                1 => GaStorageVector<T>.Create(evenDictionary),
                2 => GaStorageBivector<T>.Create(evenDictionary),
                _ => new GaStorageKVector<T>(grade, evenDictionary, evenDictionary.GetMaxBasisBladeId(grade))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Create(uint grade, IReadOnlyList<T> indexScalarList)
        {
            var evenDictionary = 
                indexScalarList.CreateEvenDictionaryList();
            
            return grade switch
            {
                0 => GaStorageScalar<T>.Create(evenDictionary),
                1 => GaStorageVector<T>.Create(evenDictionary),
                2 => GaStorageBivector<T>.Create(evenDictionary),
                _ => new GaStorageKVector<T>(grade, evenDictionary, evenDictionary.GetMaxBasisBladeId(grade))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Create(uint grade, IEnumerable<T> indexScalarList)
        {
            var evenDictionary = 
                indexScalarList.CreateEvenDictionaryList();
            
            return grade switch
            {
                0 => GaStorageScalar<T>.Create(evenDictionary),
                1 => GaStorageVector<T>.Create(evenDictionary),
                2 => GaStorageBivector<T>.Create(evenDictionary),
                _ => new GaStorageKVector<T>(grade, evenDictionary, evenDictionary.GetMaxBasisBladeId(grade))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Create(uint grade, Dictionary<ulong, T> indexScalarDictionary)
        {
            var evenDictionary =
                indexScalarDictionary.CreateEvenDictionary();
            
            return grade switch
            {
                0 => GaStorageScalar<T>.Create(evenDictionary),
                1 => GaStorageVector<T>.Create(evenDictionary),
                2 => GaStorageBivector<T>.Create(evenDictionary),
                _ => new GaStorageKVector<T>(grade, evenDictionary, evenDictionary.GetMaxBasisBladeId(grade))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Create(uint grade, IGaEvenDictionary<T> evenDictionary)
        {
            return grade switch
            {
                0 => GaStorageScalar<T>.Create(evenDictionary),
                1 => GaStorageVector<T>.Create(evenDictionary),
                2 => GaStorageBivector<T>.Create(evenDictionary),
                _ => new GaStorageKVector<T>(grade, evenDictionary, evenDictionary.GetMaxBasisBladeId(grade))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IGaStorageKVector<T> Create(uint grade, IGaEvenDictionary<T> evenDictionary, ulong maxBasisBladeId)
        {
            return grade switch
            {
                0 => GaStorageScalar<T>.Create(evenDictionary),
                1 => GaStorageVector<T>.Create(evenDictionary, maxBasisBladeId),
                2 => GaStorageBivector<T>.Create(evenDictionary, maxBasisBladeId),
                _ => new GaStorageKVector<T>(grade, evenDictionary, maxBasisBladeId)
            };
        }


        public override uint Grade { get; }


        private GaStorageKVector(uint grade, [NotNull] IGaEvenDictionary<T> indexScalarDictionary, ulong maxBasisBladeId)
            : base(indexScalarDictionary, maxBasisBladeId)
        {
            if (grade < 3 || grade > GaSpaceUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(grade));

            Grade = grade;
        }


        public override bool TryGetTermByIndex(int index, out GaTerm<T> term)
        {
            var i = (ulong) index;

            if (IndexScalarDictionary.TryGetValue(i, out var value))
            {
                term = GaTerm<T>.CreateGraded(Grade, i, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTermByIndex(ulong index, out GaTerm<T> term)
        {
            if (IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<T>.CreateGraded(Grade, index, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(ulong id, out GaTerm<T> term)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (grade == Grade && IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<T>.CreateGraded(Grade, index, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(uint grade, ulong index, out GaTerm<T> term)
        {
            if (grade == Grade && IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<T>.CreateGraded(Grade, index, value);
                return true;
            }

            term = null;
            return false;
        }


        public override IGaStorageKVector<T> GetComputedCopy(Func<T, T> mappingFunc)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(mappingFunc);

            return new GaStorageKVector<T>(
                Grade,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaStorageKVector<T> GetComputedCopy(Func<ulong, T, T> mappingFunc)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(mappingFunc);

            return new GaStorageKVector<T>(
                Grade,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaStorageKVector<T> GetComputedCopy(Func<uint, ulong, T, T> mappingFunc)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(
                    (index, scalar) => mappingFunc(Grade, index, scalar)
                );

            return new GaStorageKVector<T>(
                Grade,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }


        public GaStorageKVector<T> GetStorageCopy()
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.GetCopy();

            return new GaStorageKVector<T>(
                Grade,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public GaStorageKVector<T2> MapScalars<T2>(Func<T, T2> scalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(scalarMapping);

            return new GaStorageKVector<T2>(
                Grade,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public GaStorageKVector<T2> MapScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(
                    (index, scalar) => 
                        idScalarMapping(GaBasisUtils.BasisBladeId(Grade, index), scalar)
                );

            return new GaStorageKVector<T2>(
                Grade,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public GaStorageKVector<T2> MapScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(indexScalarMapping);

            return new GaStorageKVector<T2>(
                Grade,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public GaStorageKVector<T2> MapScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(
                    (index, scalar) => 
                        gradeIndexScalarMapping(Grade, index, scalar)
                );

            return new GaStorageKVector<T2>(
                Grade,
                indexScalarDictionary,
                MaxBasisBladeId
            );
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
                : GaStorageKVector<T>.ZeroKVector(grade);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            if (grade != Grade)
                return GaStorageKVector<T>.ZeroKVector(grade);

            var indexScalarDictionary = 
                IndexScalarDictionary
                    .Where(pair => scalarSelection(pair.Value))
                    .CopyToDictionary()
                    .CreateEvenDictionary();

            return GaStorageKVector<T>.Create(Grade, indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            if (grade != Grade)
                return GaStorageKVector<T>.ZeroKVector(grade);

            var indexScalarDictionary = 
                IndexScalarDictionary
                    .Where(pair => indexScalarSelection(pair.Key, pair.Value))
                    .CopyToDictionary()
                    .CreateEvenDictionary();

            return GaStorageKVector<T>.Create(Grade, indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            if (grade != Grade)
                return GaStorageKVector<T>.ZeroKVector(grade);

            var indexScalarDictionary = 
                IndexScalarDictionary
                    .Where(pair => indexSelection(pair.Key))
                    .CopyToDictionary()
                    .CreateEvenDictionary();

            return GaStorageKVector<T>.Create(Grade, indexScalarDictionary);
        }


        public GaStorageKVector<T> GetPart(Func<T, bool> scalarFilter)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.FilterByValue(scalarFilter);

            return new GaStorageKVector<T>(
                Grade,
                indexScalarDictionary,
                indexScalarDictionary.GetMaxBasisBladeId(Grade)
            );
        }

        public GaStorageKVector<T> GetPart(Func<ulong, T, bool> indexScalarFilter)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.FilterByKeyValue(indexScalarFilter);

            return new GaStorageKVector<T>(
                Grade,
                indexScalarDictionary,
                indexScalarDictionary.GetMaxBasisBladeId(Grade)
            );
        }

        public GaStorageKVector<T> GetPart(Func<ulong, bool> indexFilter)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.FilterByKey(indexFilter);

            return new GaStorageKVector<T>(
                Grade,
                indexScalarDictionary,
                indexScalarDictionary.GetMaxBasisBladeId(Grade)
            );
        }
        

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary
                    .Where(pair => idSelection(GaBasisUtils.BasisBladeId(Grade, pair.Key)))
                    .CopyToDictionary()
                    .CreateEvenDictionary();

            return GaStorageKVector<T>.Create(Grade, indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary
                    .Where(pair => gradeIndexSelection(Grade, pair.Key))
                    .CopyToDictionary()
                    .CreateEvenDictionary();

            return GaStorageKVector<T>.Create(Grade, indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary
                    .Where(pair => scalarSelection(pair.Value))
                    .CopyToDictionary()
                    .CreateEvenDictionary();

            return GaStorageKVector<T>.Create(Grade, indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary
                    .Where(pair => idScalarSelection(GaBasisUtils.BasisBladeId(Grade, pair.Key), pair.Value))
                    .CopyToDictionary()
                    .CreateEvenDictionary();

            return GaStorageKVector<T>.Create(Grade, indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary
                    .Where(pair => gradeIndexScalarSelection(Grade, pair.Key, pair.Value))
                    .CopyToDictionary()
                    .CreateEvenDictionary();

            return GaStorageKVector<T>.Create(Grade, indexScalarDictionary);
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

            foreach (var (index, scalar) in IndexScalarDictionary)
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

            foreach (var (index, scalar) in IndexScalarDictionary)
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

            foreach (var (index, scalar) in IndexScalarDictionary)
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


        
        public override IEnumerable<IGaBasisBlade> GetBasisBlades()
        {
            return IndexScalarDictionary.Select(pair => 
                (IGaBasisBlade) Grade.CreateGradedBasisBlade(pair.Key)
            );
        }
        
        public override IEnumerable<GaTerm<T>> GetTerms()
        {
            return IndexScalarDictionary.Select(pair => 
                GaTerm<T>.CreateGraded(Grade, pair.Key, pair.Value)
            );
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<ulong, bool> idSelection)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                var id = GaBasisUtils.BasisBladeId(Grade, index);

                if (idSelection(id))
                    yield return GaTerm<T>.CreateGraded(Grade, index, scalar);
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                if (gradeIndexSelection(Grade, index))
                    yield return GaTerm<T>.CreateGraded(Grade, index, scalar);
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<T, bool> scalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                if (scalarSelection(scalar))
                    yield return GaTerm<T>.CreateGraded(Grade, index, scalar);
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                var id = GaBasisUtils.BasisBladeId(Grade, index);

                if (idScalarSelection(id, scalar))
                    yield return GaTerm<T>.CreateGraded(Grade, index, scalar);
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                if (gradeIndexScalarSelection(Grade, index, scalar))
                    yield return GaTerm<T>.CreateGraded(Grade, index, scalar);
            }
        }
    }
}
