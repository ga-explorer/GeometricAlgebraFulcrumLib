using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraLib.Frames;
using GeometricAlgebraLib.Multivectors.Bases;
using GeometricAlgebraLib.Multivectors.Terms;
using GeometricAlgebraLib.Processors.Scalars;

namespace GeometricAlgebraLib.Storage
{
    /// <summary>
    /// Can store the scalar coefficients of a k-vector of any dimension.
    /// The scalars are assumed to be of immutable type such as T, complex, etc.
    /// </summary>
    /// <typeparam name="TScalar"></typeparam>
    public sealed class GaKVectorStorage<TScalar> 
        : GaKVectorStorageBase<TScalar>
    {
        public static GaKVectorStorage<TScalar> CreateScalar(IGaScalarProcessor<TScalar> scalarProcessor, TScalar scalar)
        {
            return new(
                scalarProcessor, 
                0, 
                new Dictionary<ulong, TScalar>() {{0, scalar}},
                0UL
            );
        }

        public static GaKVectorStorage<TScalar> CreateScalarOne(IGaScalarProcessor<TScalar> scalarProcessor)
        {
            return new(
                scalarProcessor, 
                0, 
                new Dictionary<ulong, TScalar>() {{0, scalarProcessor.OneScalar}},
                0UL
            );
        }

        public static GaKVectorStorage<TScalar> CreateScalarZero(IGaScalarProcessor<TScalar> scalarProcessor)
        {
            return new(
                scalarProcessor, 
                0, 
                new Dictionary<ulong, TScalar>() {{0, scalarProcessor.ZeroScalar}},
                0UL
            );
        }

        public static GaKVectorStorage<TScalar> CreateTerm(IGaScalarProcessor<TScalar> scalarProcessor, ulong id, TScalar scalar)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return new GaKVectorStorage<TScalar>(
                scalarProcessor, 
                grade, 
                new Dictionary<ulong, TScalar>() {{index, scalar}},
                id
            );
        }

        public static GaKVectorStorage<TScalar> CreateTerm(IGaScalarProcessor<TScalar> scalarProcessor, int grade, ulong index, TScalar scalar)
        {
            return new(
                scalarProcessor, 
                grade, 
                new Dictionary<ulong, TScalar>() {{index, scalar}},
                GaFrameUtils.BasisBladeId(grade, index)
            );
        }

        public static GaKVectorStorage<TScalar> CreateVector(IGaScalarProcessor<TScalar> scalarProcessor, Dictionary<ulong, TScalar> indexScalarDictionary)
        {
            return new(
                scalarProcessor, 
                1, 
                indexScalarDictionary,
                indexScalarDictionary.Keys.GetMaxBasisBladeId(1)
            );
        }

        public static GaKVectorStorage<TScalar> CreateVectorZero(IGaScalarProcessor<TScalar> scalarProcessor)
        {
            return new(
                scalarProcessor, 
                1, 
                new Dictionary<ulong, TScalar>(),
                0UL
            );
        }

        public static GaKVectorStorage<TScalar> CreateBivector(IGaScalarProcessor<TScalar> scalarProcessor, Dictionary<ulong, TScalar> indexScalarDictionary)
        {
            return new(
                scalarProcessor, 
                2, 
                indexScalarDictionary,
                indexScalarDictionary.Keys.GetMaxBasisBladeId(2)
            );
        }

        public static GaKVectorStorage<TScalar> CreateBivectorZero(IGaScalarProcessor<TScalar> scalarProcessor)
        {
            return new(
                scalarProcessor, 
                2, 
                new Dictionary<ulong, TScalar>(),
                0UL
            );
        }

        public static GaKVectorStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, int grade, Dictionary<ulong, TScalar> indexScalarDictionary)
        {
            return new(
                scalarProcessor, 
                grade, 
                indexScalarDictionary,
                indexScalarDictionary.Keys.GetMaxBasisBladeId(grade)
            );
        }


        public override int Grade { get; }


        private GaKVectorStorage([NotNull] IGaScalarProcessor<TScalar> scalarProcessor, int grade, [NotNull] Dictionary<ulong, TScalar> indexScalarDictionary, ulong maxBasisBladeId)
            : base(scalarProcessor, indexScalarDictionary, maxBasisBladeId)
        {
            if (grade < 0 || grade > GaFrameUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(grade));

            Grade = grade;
        }


        public override GaTerm<TScalar> GetTermByIndex(int index)
        {
            var i = (ulong) index;

            return GaTerm<TScalar>.CreateGraded(
                Grade,
                i,
                IndexScalarDictionary.TryGetValue(i, out var scalar)
                    ? scalar
                    : ScalarProcessor.ZeroScalar
            );
        }

        public override GaTerm<TScalar> GetTermByIndex(ulong index)
        {
            return GaTerm<TScalar>.CreateGraded(
                Grade,
                index,
                IndexScalarDictionary.TryGetValue(index, out var scalar)
                    ? scalar
                    : ScalarProcessor.ZeroScalar
            );
        }

        public override GaTerm<TScalar> GetTerm(ulong id)
        {
            Debug.Assert(id.BasisBladeGrade() == Grade);

            var index = id.BasisBladeIndex();

            return GaTerm<TScalar>.CreateGraded(
                Grade,
                index,
                IndexScalarDictionary.TryGetValue(index, out var scalar)
                    ? scalar
                    : ScalarProcessor.ZeroScalar
            );
        }

        public override GaTerm<TScalar> GetTerm(int grade, ulong index)
        {
            Debug.Assert(grade == Grade);

            return GaTerm<TScalar>.CreateGraded(
                Grade,
                index,
                IndexScalarDictionary.TryGetValue(index, out var scalar)
                    ? scalar
                    : ScalarProcessor.ZeroScalar
            );
        }


        public override bool TryGetTermByIndex(int index, out GaTerm<TScalar> term)
        {
            var i = (ulong) index;

            if (IndexScalarDictionary.TryGetValue(i, out var value))
            {
                term = GaTerm<TScalar>.CreateGraded(Grade, i, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTermByIndex(ulong index, out GaTerm<TScalar> term)
        {
            if (IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<TScalar>.CreateGraded(Grade, index, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(ulong id, out GaTerm<TScalar> term)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (grade == Grade && IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<TScalar>.CreateGraded(Grade, index, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(int grade, ulong index, out GaTerm<TScalar> term)
        {
            if (grade == Grade && IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<TScalar>.CreateGraded(Grade, index, value);
                return true;
            }

            term = null;
            return false;
        }


        public override GaKVectorStorageBase<TScalar> GetLeftScaledCopy(TScalar scalingFactor)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.CopyToDictionary(
                    scalar => ScalarProcessor.Times(scalingFactor, scalar)
                );

            return new GaKVectorStorage<TScalar>(
                ScalarProcessor,
                Grade,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GaKVectorStorageBase<TScalar> GetRightScaledCopy(TScalar scalingFactor)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => ScalarProcessor.Times(pair.Value, scalingFactor)
                );

            return new GaKVectorStorage<TScalar>(
                ScalarProcessor,
                Grade,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GaKVectorStorageBase<TScalar> GetComputedCopy(Func<TScalar, TScalar> mappingFunc)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.CopyToDictionary(mappingFunc);

            return new GaKVectorStorage<TScalar>(
                ScalarProcessor,
                Grade,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GaKVectorStorageBase<TScalar> GetComputedCopy(Func<ulong, TScalar, TScalar> mappingFunc)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => mappingFunc(pair.Key, pair.Value)
                );

            return new GaKVectorStorage<TScalar>(
                ScalarProcessor,
                Grade,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GaKVectorStorageBase<TScalar> GetComputedCopy(Func<ulong, TScalar> mappingFunc)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => mappingFunc(pair.Key)
                );

            return new GaKVectorStorage<TScalar>(
                ScalarProcessor,
                Grade,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GaKVectorStorageBase<TScalar> GetComputedCopy(Func<int, ulong, TScalar, TScalar> mappingFunc)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => mappingFunc(Grade, pair.Key, pair.Value)
                );

            return new GaKVectorStorage<TScalar>(
                ScalarProcessor,
                Grade,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GaKVectorStorageBase<TScalar> GetComputedCopy(Func<int, ulong, TScalar> mappingFunc)
        {
            var indexScalarDictionary = IndexScalarDictionary.ToDictionary(
                pair => pair.Key, 
                pair => mappingFunc(Grade, pair.Key)
            );

            return new GaKVectorStorage<TScalar>(
                ScalarProcessor,
                Grade,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }


        public override IGaMultivectorStorage<TScalar> GetStorageCopy()
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.CopyToDictionary();

            return new GaKVectorStorage<TScalar>(
                ScalarProcessor, 
                Grade, 
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar> GetStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.CopyToDictionary(scalarMapping);

            return new GaKVectorStorage<TScalar>(
                ScalarProcessor, 
                Grade, 
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<ulong, TScalar, TScalar2> idScalarMapping)
        {
            return new GaKVectorStorage<TScalar2>(
                scalarProcessor,
                Grade,
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => idScalarMapping(
                        GaFrameUtils.BasisBladeId(Grade, pair.Key), 
                        pair.Value
                    )
                ),
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<int, ulong, TScalar, TScalar2> gradeIndexScalarMapping)
        {
            return new GaKVectorStorage<TScalar2>(
                scalarProcessor,
                Grade,
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => gradeIndexScalarMapping(Grade, pair.Key, pair.Value)
                ),
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<TScalar, TScalar2> scalarMapping)
        {
            return new GaKVectorStorage<TScalar2>(
                scalarProcessor,
                Grade,
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => scalarMapping(pair.Value)
                ),
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar> GetNegative()
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.CopyToDictionary(ScalarProcessor.Negative);

            return new GaKVectorStorage<TScalar>(
                ScalarProcessor, 
                Grade, 
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaScalarStorage<TScalar> GetScalarPart()
        {
            return Grade == 0 
                ? GaScalarTermStorage<TScalar>.Create(ScalarProcessor, GetTermScalarByIndex(0))
                : GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaScalarStorage<TScalar> GetScalarPart(Func<TScalar, TScalar> scalarMapping)
        {
            return Grade == 0 
                ? GaScalarTermStorage<TScalar>.Create(ScalarProcessor, scalarMapping(GetTermScalar(0)))
                : GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart()
        {
            return Grade == 1
                ? GaVectorStorage<TScalar>.Create(ScalarProcessor, IndexScalarDictionary)
                : GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            return Grade == 1
                ? GaVectorStorage<TScalar>.Create(
                    ScalarProcessor, 
                    IndexScalarDictionary.CopyToDictionary(scalarMapping)
                )
                : GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            return Grade == 1
                ? GaVectorStorage<TScalar>.Create(
                    ScalarProcessor, 
                    IndexScalarDictionary.ToDictionary(
                        pair => pair.Key,
                        pair => indexScalarMapping(pair.Key, pair.Value)
                    )
                )
                : GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, bool> scalarSelection)
        {
            if (Grade != 1)
                return GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);

            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor,
                IndexScalarDictionary
                    .Where(pair => scalarSelection(pair.Value))
                    .CopyToDictionary()
            );
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            if (Grade != 1)
                return GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);

            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor,
                IndexScalarDictionary
                    .Where(pair => indexScalarSelection(pair.Key, pair.Value))
                    .CopyToDictionary()
            );
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            if (Grade != 1)
                return GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);

            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor,
                IndexScalarDictionary
                    .Where(pair => indexSelection(pair.Key))
                    .CopyToDictionary()
            );
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart()
        {
            return Grade == 2
                ? GaBivectorStorage<TScalar>.Create(ScalarProcessor, IndexScalarDictionary)
                : GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            return Grade == 2
                ? GaBivectorStorage<TScalar>.Create(
                    ScalarProcessor, 
                    IndexScalarDictionary.CopyToDictionary(scalarMapping)
                )
                : GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            return Grade == 2
                ? GaBivectorStorage<TScalar>.Create(
                    ScalarProcessor, 
                    IndexScalarDictionary.ToDictionary(
                        pair => pair.Key,
                        pair => indexScalarMapping(pair.Key, pair.Value)
                    )
                )
                : GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, bool> scalarSelection)
        {
            throw new NotImplementedException();
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            throw new NotImplementedException();
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            throw new NotImplementedException();
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade)
        {
            return grade == Grade
                ? this
                : GaKVectorTermStorageBase<TScalar>.CreateZeroKVector(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, TScalar> scalarMapping)
        {
            if (grade != Grade) 
                return GaKVectorTermStorageBase<TScalar>.CreateZeroKVector(ScalarProcessor, grade);

            return grade switch
            {
                0 => GetScalarPart(scalarMapping),
                1 => GetVectorPart(scalarMapping),
                2 => GetBivectorPart(scalarMapping),
                _ => new GaKVectorStorage<TScalar>(
                    ScalarProcessor,
                    Grade,
                    IndexScalarDictionary.CopyToDictionary(scalarMapping),
                    MaxBasisBladeId
                )
            };
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            if (grade != Grade) 
                return GaKVectorTermStorageBase<TScalar>.CreateZeroKVector(ScalarProcessor, grade);

            return grade switch
            {
                0 => GaScalarTermStorage<TScalar>.Create(ScalarProcessor, indexScalarMapping(0, GetTermScalarByIndex(0))),
                1 => GetVectorPart(indexScalarMapping),
                2 => GetBivectorPart(indexScalarMapping),
                _ => new GaKVectorStorage<TScalar>(
                    ScalarProcessor,
                    Grade,
                    IndexScalarDictionary.ToDictionary(
                        pair => pair.Key,
                        pair => indexScalarMapping(pair.Key, pair.Value)
                    ),
                    MaxBasisBladeId
                )
            };
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, bool> scalarSelection)
        {
            throw new NotImplementedException();
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, bool> indexScalarSelection)
        {
            throw new NotImplementedException();
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, bool> indexSelection)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<int, ulong, bool> gradeIndexSelection)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, bool> scalarSelection)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<ulong, TScalar, bool> idScalarSelection)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<int, ulong, TScalar, bool> gradeIndexScalarSelection)
        {
            throw new NotImplementedException();
        }

        public override Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            if (Grade != 1)
                return new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(
                    GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                    GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor)
                );

            var indexScalarDictionary1 = new Dictionary<ulong, TScalar>();
            var indexScalarDictionary2 = new Dictionary<ulong, TScalar>();

            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                if (indexSelection(index))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(
                GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary1),
                GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary2)
            );
        }

        public override Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            if (Grade != 1)
                return new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(
                    GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                    GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor)
                );

            var indexScalarDictionary1 = new Dictionary<ulong, TScalar>();
            var indexScalarDictionary2 = new Dictionary<ulong, TScalar>();

            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                if (indexScalarSelection(index, scalar))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(
                GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary1),
                GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary2)
            );
        }

        public override Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<TScalar, bool> scalarSelection)
        {
            if (Grade != 1)
                return new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(
                    GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                    GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor)
                );

            var indexScalarDictionary1 = new Dictionary<ulong, TScalar>();
            var indexScalarDictionary2 = new Dictionary<ulong, TScalar>();

            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                if (scalarSelection(scalar))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(
                GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary1),
                GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary2)
            );
        }


        public override bool IsScalar()
        {
            return Grade == 0;
        }

        public override bool IsVector()
        {
            return Grade == 1;
        }

        public override bool IsBivector()
        {
            return Grade == 2;
        }

        public override IEnumerable<ulong> GetIds()
        {
            return IndexScalarDictionary.Keys.Select(index => GaFrameUtils.BasisBladeId(Grade, index));
        }

        public override IEnumerable<IGaBasis> GetBasisBlades()
        {
            return IndexScalarDictionary.Select(pair => 
                (IGaBasis)new GaBasisGraded(Grade, pair.Key)
            );
        }

        public override IEnumerable<KeyValuePair<ulong, TScalar>> GetIdScalarPairs()
        {
            return IndexScalarDictionary
                .Select(pair => new KeyValuePair<ulong, TScalar>(
                    GaFrameUtils.BasisBladeId(Grade, pair.Key),
                    pair.Value
                ));
        }

        public override IEnumerable<Tuple<ulong, TScalar>> GetIdScalarTuples()
        {
            return IndexScalarDictionary
                .Select(pair => new Tuple<ulong, TScalar>(
                    GaFrameUtils.BasisBladeId(Grade, pair.Key),
                    pair.Value
                ));
        }

        public override IReadOnlyDictionary<ulong, TScalar> GetIdScalarDictionary()
        {
            return IndexScalarDictionary.ToDictionary(
                pair => GaFrameUtils.BasisBladeId(Grade, pair.Key),
                pair => pair.Value
            );
        }

        public override IEnumerable<GaTerm<TScalar>> GetTerms()
        {
            return IndexScalarDictionary.Select(pair => 
                GaTerm<TScalar>.CreateGraded(Grade, pair.Key, pair.Value)
            );
        }

        public override IEnumerable<GaTerm<TScalar>> GetNotZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => !ScalarProcessor.IsZero(pair.Value))
                .Select(pair => 
                    GaTerm<TScalar>.CreateGraded(Grade, pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<TScalar>> GetNotNearZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => !ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => 
                    GaTerm<TScalar>.CreateGraded(Grade, pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<TScalar>> GetZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => ScalarProcessor.IsZero(pair.Value))
                .Select(pair => 
                    GaTerm<TScalar>.CreateGraded(Grade, pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<TScalar>> GetNearZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => 
                    GaTerm<TScalar>.CreateGraded(Grade, pair.Key, pair.Value)
                );
        }
    }
}
