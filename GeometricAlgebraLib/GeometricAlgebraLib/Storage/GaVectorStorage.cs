using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraLib.Algebra.Basis;
using GeometricAlgebraLib.Algebra.Multivectors.Terms;
using GeometricAlgebraLib.Processing.Scalars;
using GeometricAlgebraLib.Storage.Composers;
using GaBasisUtils = GeometricAlgebraLib.Algebra.Basis.GaBasisUtils;

namespace GeometricAlgebraLib.Storage
{
    /// <summary>
    /// Can store the scalar coefficients of a vector of any dimension.
    /// The scalars are assumed to be of immutable type such as T, complex, etc.
    /// </summary>
    /// <typeparam name="TScalar"></typeparam>
    public sealed class GaVectorStorage<TScalar> 
        : GaKVectorStorageBase<TScalar>, IGaVectorStorage<TScalar>
    {
        //public static GaVectorStorage<TScalar> CreateZero(IGaScalarProcessor<TScalar> scalarProcessor)
        //{
        //    return new(
        //        scalarProcessor,
        //        new Dictionary<ulong, TScalar>(),
        //        0UL
        //    );
        //}

        //public static GaVectorStorage<TScalar> CreateBasisVector(IGaScalarProcessor<TScalar> scalarProcessor, int index)
        //{
        //    var indexScalarDictionary = 
        //        new Dictionary<ulong, TScalar>() {{(ulong) index, scalarProcessor.OneScalar}};

        //    return new GaVectorStorage<TScalar>(
        //        scalarProcessor,
        //        indexScalarDictionary,
        //        1UL << index
        //    );
        //}

        //public static GaVectorStorage<TScalar> CreateBasisVector(IGaScalarProcessor<TScalar> scalarProcessor, ulong index)
        //{
        //    var indexScalarDictionary = 
        //        new Dictionary<ulong, TScalar>() {{index, scalarProcessor.OneScalar}};

        //    return new GaVectorStorage<TScalar>(
        //        scalarProcessor,
        //        indexScalarDictionary,
        //        1UL << (int) index
        //    );
        //}

        //public static GaVectorStorage<TScalar> CreateTerm(IGaScalarProcessor<TScalar> scalarProcessor, int index, TScalar scalar)
        //{
        //    var indexScalarDictionary = 
        //        new Dictionary<ulong, TScalar>() {{(ulong) index, scalar}};

        //    return new GaVectorStorage<TScalar>(
        //        scalarProcessor,
        //        indexScalarDictionary,
        //        1UL << index
        //    );
        //}

        //public static GaVectorStorage<TScalar> CreateTerm(IGaScalarProcessor<TScalar> scalarProcessor, ulong index, TScalar scalar)
        //{
        //    var indexScalarDictionary = 
        //        new Dictionary<ulong, TScalar>() {{index, scalar}};

        //    return new GaVectorStorage<TScalar>(
        //        scalarProcessor,
        //        indexScalarDictionary,
        //        1UL << (int)index
        //    );
        //}

        public static GaVectorStorage<TScalar> CreateOnesVector(IGaScalarProcessor<TScalar> scalarProcessor, int termsCount)
        {
            return new GaVectorStorage<TScalar>(
                scalarProcessor,
                Enumerable.Range(0, termsCount).ToDictionary(
                    i => (ulong) i,
                    _ => scalarProcessor.OneScalar
                ),
                1UL << (termsCount - 1)
            );
        }

        public static GaVectorStorage<TScalar> CreateUnitOnesVector(IGaScalarProcessor<TScalar> scalarProcessor, int termsCount)
        {
            var length = scalarProcessor.Sqrt(termsCount);

            return new GaVectorStorage<TScalar>(
                scalarProcessor,
                Enumerable.Range(0, termsCount).ToDictionary(
                    i => (ulong) i,
                    _ => scalarProcessor.Divide(scalarProcessor.OneScalar, length)
                ),
                1UL << (termsCount - 1)
            );
        }

        public static GaVectorStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, params TScalar[] scalarArray)
        {
            var indexScalarDictionary = new Dictionary<ulong, TScalar>();

            for (var i = 0; i < scalarArray.Length; i++)
                indexScalarDictionary.Add((ulong) i, scalarArray[i]);

            return new GaVectorStorage<TScalar>(
                scalarProcessor,
                indexScalarDictionary,
                1UL << scalarArray.Length
            );
        }

        public static GaVectorStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, IReadOnlyList<TScalar> scalarList)
        {
            var indexScalarDictionary = new Dictionary<ulong, TScalar>();

            for (var i = 0; i < scalarList.Count; i++)
                indexScalarDictionary.Add((ulong) i, scalarList[i]);

            return new GaVectorStorage<TScalar>(
                scalarProcessor,
                indexScalarDictionary,
                1UL << scalarList.Count
            );
        }

        public static GaVectorStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, Dictionary<ulong, TScalar> indexScalarDictionary)
        {
            return new(
                scalarProcessor,
                indexScalarDictionary,
                indexScalarDictionary.Keys.GetMaxBasisBladeId(1)
            );
        }


        public override int Grade => 1;


        private GaVectorStorage([NotNull] IGaScalarProcessor<TScalar> scalarProcessor, [NotNull] Dictionary<ulong, TScalar> indexScalarDictionary, ulong maxBasisBladeId)
            : base(scalarProcessor, indexScalarDictionary, maxBasisBladeId)
        {
        }


        public override GaTerm<TScalar> GetTermByIndex(int index)
        {
            var i = (ulong) index;

            return GaTerm<TScalar>.CreateVector(
                i, 
                IndexScalarDictionary.TryGetValue(i, out var scalar)
                    ? scalar : ScalarProcessor.ZeroScalar
            );
        }

        public override GaTerm<TScalar> GetTermByIndex(ulong index)
        {
            return GaTerm<TScalar>.CreateVector(
                index, 
                IndexScalarDictionary.TryGetValue(index, out var scalar)
                    ? scalar : ScalarProcessor.ZeroScalar
            );
        }

        public override GaTerm<TScalar> GetTerm(ulong id)
        {
            Debug.Assert(id.BasisBladeGrade() == 1);

            var index = id.BasisVectorIndex();

            return GaTerm<TScalar>.CreateVector(
                index, 
                IndexScalarDictionary.TryGetValue(index, out var scalar)
                    ? scalar : ScalarProcessor.ZeroScalar
            );
        }

        public override GaTerm<TScalar> GetTerm(int grade, ulong index)
        {
            Debug.Assert(grade == 1);

            return GaTerm<TScalar>.CreateVector(
                index, 
                IndexScalarDictionary.TryGetValue(index, out var scalar)
                    ? scalar : ScalarProcessor.ZeroScalar
            );
        }


        public override bool TryGetTermByIndex(int index, out GaTerm<TScalar> term)
        {
            var i = (ulong) index;

            if (IndexScalarDictionary.TryGetValue(i, out var value))
            {
                term = GaTerm<TScalar>.CreateVector(i, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTermByIndex(ulong index, out GaTerm<TScalar> term)
        {
            if (IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<TScalar>.CreateVector(index, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(ulong id, out GaTerm<TScalar> term)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (grade == 1 && IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<TScalar>.CreateVector(index, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(int grade, ulong index, out GaTerm<TScalar> term)
        {
            if (grade == 1 && IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<TScalar>.CreateVector(index, value);
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

            return new GaVectorStorage<TScalar>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GaKVectorStorageBase<TScalar> GetRightScaledCopy(TScalar scalingFactor)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.CopyToDictionary(
                    scalar => ScalarProcessor.Times(scalar, scalingFactor)
                );

            return new GaVectorStorage<TScalar>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GaKVectorStorageBase<TScalar> GetComputedCopy(Func<TScalar, TScalar> mappingFunc)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.CopyToDictionary(mappingFunc);

            return new GaVectorStorage<TScalar>(
                ScalarProcessor,
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

            return new GaVectorStorage<TScalar>(
                ScalarProcessor,
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

            return new GaVectorStorage<TScalar>(
                ScalarProcessor,
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

            return new GaVectorStorage<TScalar>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GaKVectorStorageBase<TScalar> GetComputedCopy(Func<int, ulong, TScalar> mappingFunc)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => mappingFunc(Grade, pair.Key)
                );

            return new GaVectorStorage<TScalar>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }


        public override IGaMultivectorStorage<TScalar> GetStorageCopy()
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.CopyToDictionary();

            return new GaVectorStorage<TScalar>(
                ScalarProcessor, 
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar> GetStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.CopyToDictionary(scalarMapping);

            return new GaVectorStorage<TScalar>(
                ScalarProcessor, 
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<ulong, TScalar, TScalar2> idScalarMapping)
        {
            return new GaVectorStorage<TScalar2>(
                scalarProcessor,
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => idScalarMapping(
                        GaBasisUtils.BasisBladeId(Grade, pair.Key), 
                        pair.Value
                    )
                ),
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<int, ulong, TScalar, TScalar2> gradeIndexScalarMapping)
        {
            return new GaVectorStorage<TScalar2>(
                scalarProcessor,
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => gradeIndexScalarMapping(Grade, pair.Key, pair.Value)
                ),
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<TScalar, TScalar2> scalarMapping)
        {
            return new GaVectorStorage<TScalar2>(
                scalarProcessor,
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

            return new GaVectorStorage<TScalar>(
                ScalarProcessor, 
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaScalarStorage<TScalar> GetScalarPart()
        {
            return GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaScalarStorage<TScalar> GetScalarPart(Func<TScalar, TScalar> scalarMapping)
        {
            return GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart()
        {
            return this;
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            return new GaVectorStorage<TScalar>(
                ScalarProcessor,
                IndexScalarDictionary.CopyToDictionary(scalarMapping),
                MaxBasisBladeId
            );
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            return new GaVectorStorage<TScalar>(
                ScalarProcessor,
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => indexScalarMapping(pair.Key, pair.Value)
                    ),
                MaxBasisBladeId
            );
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, bool> scalarSelection)
        {
            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor,
                IndexScalarDictionary
                    .Where(pair => scalarSelection(pair.Value))
                    .CopyToDictionary()
            );
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor,
                IndexScalarDictionary
                    .Where(pair => indexScalarSelection(pair.Key, pair.Value))
                    .CopyToDictionary()
            );
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor,
                IndexScalarDictionary
                    .Where(pair => indexSelection(pair.Key))
                    .CopyToDictionary()
            );
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart()
        {
            return GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            return GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            return GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, bool> scalarSelection)
        {
            return GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            return GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            return GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade)
        {
            return grade == 1
                ? this
                : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, TScalar> scalarMapping)
        {
            return grade == 1
                ? GetVectorPart(scalarMapping)
                : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            return grade == 1
                ? GetVectorPart(indexScalarMapping)
                : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, bool> scalarSelection)
        {
            return grade == 1
                ? GetVectorPart(scalarSelection)
                : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, bool> indexScalarSelection)
        {
            return grade == 1
                ? GetVectorPart(indexScalarSelection)
                : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, bool> indexSelection)
        {
            return grade == 1
                ? GetVectorPart(indexSelection)
                : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade);
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            return GetVectorPart(scalarMapping);
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor,
                IndexScalarDictionary
                    .Where(pair => idSelection(pair.Key))
                    .CopyToDictionary()
            );
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<int, ulong, bool> gradeIndexSelection)
        {
            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor,
                IndexScalarDictionary
                    .Where(pair => gradeIndexSelection(1, pair.Key))
                    .CopyToDictionary()
            );
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, bool> scalarSelection)
        {
            return GetVectorPart(scalarSelection);
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<ulong, TScalar, bool> idScalarSelection)
        {
            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor,
                IndexScalarDictionary
                    .Where(pair => idScalarSelection(GaBasisUtils.BasisBladeId(1, pair.Key), pair.Value))
                    .CopyToDictionary()
            );
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<int, ulong, TScalar, bool> gradeIndexScalarSelection)
        {
            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor,
                IndexScalarDictionary
                    .Where(pair => gradeIndexScalarSelection(1, pair.Key, pair.Value))
                    .CopyToDictionary()
            );
        }

        public override Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
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
                Create(ScalarProcessor, indexScalarDictionary1),
                Create(ScalarProcessor, indexScalarDictionary2)
            );
        }

        public override Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
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
                Create(ScalarProcessor, indexScalarDictionary1),
                Create(ScalarProcessor, indexScalarDictionary2)
            );
        }

        public override Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<TScalar, bool> scalarSelection)
        {
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
                Create(ScalarProcessor, indexScalarDictionary1),
                Create(ScalarProcessor, indexScalarDictionary2)
            );
        }

        public override bool IsScalar()
        {
            return false;
        }

        public override bool IsVector()
        {
            return true;
        }

        public override bool IsBivector()
        {
            return false;
        }

        public override IEnumerable<ulong> GetIds()
        {
            return IndexScalarDictionary.Keys.Select(index => 1UL << (int)index);
        }

        public override IEnumerable<IGaBasisBlade> GetBasisBlades()
        {
            return IndexScalarDictionary.Keys.Select(index => 
                (IGaBasisBlade)new GaBasisVector(index)
            );
        }

        public override IEnumerable<KeyValuePair<ulong, TScalar>> GetIdScalarPairs()
        {
            return IndexScalarDictionary
                .Select(pair => new KeyValuePair<ulong, TScalar>(1UL << (int)pair.Key, pair.Value));
        }

        public override IEnumerable<Tuple<ulong, TScalar>> GetIdScalarTuples()
        {
            return IndexScalarDictionary
                .Select(pair => new Tuple<ulong, TScalar>(1UL << (int)pair.Key, pair.Value));
        }

        public override IReadOnlyDictionary<ulong, TScalar> GetIdScalarDictionary()
        {
            return IndexScalarDictionary.ToDictionary(
                pair => 1UL << (int)pair.Key,
                pair => pair.Value
            );
        }

        public override IEnumerable<GaTerm<TScalar>> GetTerms()
        {
            return IndexScalarDictionary
                .Select(pair => GaTerm<TScalar>.CreateVector(pair.Key, pair.Value));
        }

        public override IEnumerable<GaTerm<TScalar>> GetNotZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => !ScalarProcessor.IsZero(pair.Value))
                .Select(pair => GaTerm<TScalar>.CreateVector(pair.Key, pair.Value));
        }

        public override IEnumerable<GaTerm<TScalar>> GetNotNearZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => !ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => GaTerm<TScalar>.CreateVector(pair.Key, pair.Value));
        }

        public override IEnumerable<GaTerm<TScalar>> GetZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => ScalarProcessor.IsZero(pair.Value))
                .Select(pair => GaTerm<TScalar>.CreateVector(pair.Key, pair.Value));
        }

        public override IEnumerable<GaTerm<TScalar>> GetNearZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => GaTerm<TScalar>.CreateVector(pair.Key, pair.Value));
        }

        
        public IGaVectorStorage<TScalar> GetVectorStorage()
        {
            return this;
        }

        public IGaVectorStorage<TScalar> GetVectorStorageCopy()
        {
            return IndexScalarDictionary.Count switch
            {
                0 => GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                1 => GaVectorTermStorage<TScalar>.Create(ScalarProcessor, IndexScalarDictionary.First()),
                _ => new GaVectorStorage<TScalar>(ScalarProcessor, IndexScalarDictionary.CopyToDictionary(), MaxBasisBladeId)
            };
        }

        public IGaVectorStorage<TScalar> GetVectorStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            return IndexScalarDictionary.Count switch
            {
                0 => GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                1 => GaVectorTermStorage<TScalar>.Create(ScalarProcessor, IndexScalarDictionary.First(scalarMapping)),
                _ => new GaVectorStorage<TScalar>(ScalarProcessor, IndexScalarDictionary.CopyToDictionary(scalarMapping), MaxBasisBladeId)
            };
        }

        public IGaVectorStorage<TScalar> Add(IGaVectorStorage<TScalar> mv2)
        {
            var composer = new GaVectorStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerms(GetIndexScalarPairs());

            composer.AddTerms(mv2.GetIndexScalarPairs());

            return composer.GetVectorStorage();
        }

        public IGaVectorStorage<TScalar> Subtract(IGaVectorStorage<TScalar> mv2)
        {
            var composer = new GaVectorStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerms(GetIndexScalarPairs());

            composer.SubtractTerms(mv2.GetIndexScalarPairs());

            return composer.GetVectorStorage();
        }

        public IGaBivectorStorage<TScalar> Op(IGaVectorStorage<TScalar> mv2)
        {
            var storage = new GaBivectorStorageComposer<TScalar>(ScalarProcessor);

            foreach (var (index1, scalar1) in IndexScalarDictionary)
            {
                foreach (var (index2, scalar2) in mv2.GetIndexScalarDictionary())
                {
                    if (index1 == index2)
                        continue;

                    storage.AddTerm(
                        index1, 
                        index2, 
                        ScalarProcessor.Times(scalar1, scalar2)
                    );
                }
            }

            storage.RemoveZeroTerms();

            return storage.GetBivectorStorage();
        }

    }
}