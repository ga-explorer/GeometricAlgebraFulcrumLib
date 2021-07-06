using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Combinations;
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
    public sealed class GaBivectorStorage<TScalar> 
        : GaKVectorStorageBase<TScalar>, IGaBivectorStorage<TScalar>
    {
        //public static GaBivectorStorage<TScalar> CreateZero(IGaScalarProcessor<TScalar> scalarProcessor)
        //{
        //    return new GaBivectorStorage<TScalar>(
        //        scalarProcessor,
        //        new Dictionary<ulong, TScalar>(),
        //        0UL
        //    );
        //}

        //public static GaBivectorStorage<TScalar> CreateTerm(IGaScalarProcessor<TScalar> scalarProcessor, int index, TScalar scalar)
        //{
        //    var indexScalarDictionary = 
        //        new Dictionary<ulong, TScalar>() {{(ulong) index, scalar}};

        //    return new GaBivectorStorage<TScalar>(
        //        scalarProcessor,
        //        indexScalarDictionary,
        //        GaFrameUtils.BasisBladeId(2, (ulong) index)
        //    );
        //}

        //public static GaBivectorStorage<TScalar> CreateTerm(IGaScalarProcessor<TScalar> scalarProcessor, ulong index, TScalar scalar)
        //{
        //    var indexScalarDictionary = 
        //        new Dictionary<ulong, TScalar>() {{index, scalar}};

        //    return new GaBivectorStorage<TScalar>(
        //        scalarProcessor,
        //        indexScalarDictionary,
        //        GaFrameUtils.BasisBladeId(2, index)
        //    );
        //}

        //public static GaBivectorStorage<TScalar> CreateBasisBivector(IGaScalarProcessor<TScalar> scalarProcessor, int basisVectorIndex1, int basisVectorIndex2)
        //{
        //    var index = basisVectorIndex1 > basisVectorIndex2
        //        ? BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1)
        //        : BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

        //    var indexScalarDictionary = 
        //        new Dictionary<ulong, TScalar>() {{index, scalarProcessor.OneScalar}};

        //    return new GaBivectorStorage<TScalar>(
        //        scalarProcessor,
        //        indexScalarDictionary,
        //        GaFrameUtils.BasisBladeId(2, index)
        //    );
        //}

        //public static GaBivectorStorage<TScalar> CreateBasisBivector(IGaScalarProcessor<TScalar> scalarProcessor, ulong basisVectorIndex1, ulong basisVectorIndex2)
        //{
        //    var index = basisVectorIndex1 > basisVectorIndex2
        //        ? BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1)
        //        : BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

        //    var indexScalarDictionary = 
        //        new Dictionary<ulong, TScalar>() {{index, scalarProcessor.OneScalar}};

        //    return new GaBivectorStorage<TScalar>(
        //        scalarProcessor,
        //        indexScalarDictionary,
        //        GaFrameUtils.BasisBladeId(2, index)
        //    );
        //}

        //public static GaBivectorStorage<TScalar> CreateTerm(IGaScalarProcessor<TScalar> scalarProcessor, int basisVectorIndex1, int basisVectorIndex2, TScalar scalar)
        //{
        //    var index = basisVectorIndex1 > basisVectorIndex2
        //        ? BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1)
        //        : BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

        //    var indexScalarDictionary = 
        //        new Dictionary<ulong, TScalar>() {{index, scalar}};

        //    return new GaBivectorStorage<TScalar>(
        //        scalarProcessor,
        //        indexScalarDictionary,
        //        GaFrameUtils.BasisBladeId(2, index)
        //    );
        //}

        //public static GaBivectorStorage<TScalar> CreateTerm(IGaScalarProcessor<TScalar> scalarProcessor, ulong basisVectorIndex1, ulong basisVectorIndex2, TScalar scalar)
        //{
        //    var index = basisVectorIndex1 > basisVectorIndex2
        //        ? BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1)
        //        : BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

        //    var indexScalarDictionary = 
        //        new Dictionary<ulong, TScalar>() {{index, scalar}};

        //    return new GaBivectorStorage<TScalar>(
        //        scalarProcessor,
        //        indexScalarDictionary,
        //        GaFrameUtils.BasisBladeId(2, index)
        //    );
        //}

        public static GaBivectorStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, Dictionary<ulong, TScalar> indexScalarDictionary)
        {
            return new GaBivectorStorage<TScalar>(
                scalarProcessor,
                indexScalarDictionary,
                indexScalarDictionary.Keys.GetMaxBasisBladeId(2)
            );
        }


        public override int Grade => 2;


        private GaBivectorStorage([NotNull] IGaScalarProcessor<TScalar> scalarProcessor, [NotNull] Dictionary<ulong, TScalar> indexScalarDictionary, ulong maxBasisBladeId)
            : base(scalarProcessor, indexScalarDictionary, maxBasisBladeId)
        {
        }
        

        public bool ContainsTerm(int basisVectorIndex1, int basisVectorIndex2)
        {
            var index = basisVectorIndex1 > basisVectorIndex2
                ? BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1)
                : BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

            return IndexScalarDictionary.ContainsKey(index);
        }

        public bool ContainsTerm(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            var index = basisVectorIndex1 > basisVectorIndex2
                ? BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1)
                : BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

            return IndexScalarDictionary.ContainsKey(index);
        }


        public TScalar GetTermScalar(int basisVectorIndex1, int basisVectorIndex2)
        {
            if (basisVectorIndex1 > basisVectorIndex2)
            {
                var index =
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

                return IndexScalarDictionary.TryGetValue(index, out var scalar)
                    ? ScalarProcessor.Negative(scalar)
                    : ScalarProcessor.ZeroScalar;
            }
            else
            {
                var index =
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

                return IndexScalarDictionary.TryGetValue(index, out var scalar)
                    ? scalar
                    : ScalarProcessor.ZeroScalar;
            }
        }

        public TScalar GetTermScalar(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            if (basisVectorIndex1 > basisVectorIndex2)
            {
                var index =
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

                return IndexScalarDictionary.TryGetValue(index, out var scalar)
                    ? ScalarProcessor.Negative(scalar)
                    : ScalarProcessor.ZeroScalar;
            }
            else
            {
                var index =
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

                return IndexScalarDictionary.TryGetValue(index, out var scalar)
                    ? scalar
                    : ScalarProcessor.ZeroScalar;
            }
        }


        public override GaTerm<TScalar> GetTermByIndex(int index)
        {
            var i = (ulong) index;

            return GaTerm<TScalar>.CreateBivector(
                i, 
                IndexScalarDictionary.TryGetValue(i, out var scalar) 
                    ? scalar 
                    : ScalarProcessor.ZeroScalar
            );
        }

        public override GaTerm<TScalar> GetTermByIndex(ulong index)
        {
            return GaTerm<TScalar>.CreateBivector(
                index, 
                IndexScalarDictionary.TryGetValue(index, out var scalar) 
                    ? scalar 
                    : ScalarProcessor.ZeroScalar
            );
        }

        public override GaTerm<TScalar> GetTerm(ulong id)
        {
            Debug.Assert(id.BasisBladeGrade() == 2);

            var index = BinaryCombinationsUtilsUInt64.CombinadicPatternToIndex(id);

            return GaTerm<TScalar>.CreateBivector(
                index, 
                IndexScalarDictionary.TryGetValue(index, out var scalar) 
                    ? scalar 
                    : ScalarProcessor.ZeroScalar
            );
        }

        public override GaTerm<TScalar> GetTerm(int grade, ulong index)
        {
            Debug.Assert(grade == 2);

            return GaTerm<TScalar>.CreateBivector(
                index, 
                IndexScalarDictionary.TryGetValue(index, out var scalar) 
                    ? scalar 
                    : ScalarProcessor.ZeroScalar
            );
        }

        public GaTerm<TScalar> GetTerm(int basisVectorIndex1, int basisVectorIndex2)
        {
            if (basisVectorIndex1 > basisVectorIndex2)
            {
                var index = 
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

                return GaTerm<TScalar>.CreateBivector(
                    index, 
                    IndexScalarDictionary.TryGetValue(index, out var scalar) 
                        ? ScalarProcessor.Negative(scalar)
                        : ScalarProcessor.ZeroScalar
                );
            }
            else
            {
                var index = 
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

                return GaTerm<TScalar>.CreateBivector(
                    index, 
                    IndexScalarDictionary.TryGetValue(index, out var scalar) 
                        ? scalar 
                        : ScalarProcessor.ZeroScalar
                );
            }
        }

        public GaTerm<TScalar> GetTerm(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            if (basisVectorIndex1 > basisVectorIndex2)
            {
                var index = 
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

                return GaTerm<TScalar>.CreateBivector(
                    index, 
                    IndexScalarDictionary.TryGetValue(index, out var scalar) 
                        ? ScalarProcessor.Negative(scalar)
                        : ScalarProcessor.ZeroScalar
                );
            }
            else
            {
                var index = 
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

                return GaTerm<TScalar>.CreateBivector(
                    index, 
                    IndexScalarDictionary.TryGetValue(index, out var scalar) 
                        ? scalar 
                        : ScalarProcessor.ZeroScalar
                );
            }
        }


        public bool TryGetTermScalar(int basisVectorIndex1, int basisVectorIndex2, out TScalar scalar)
        {
            if (basisVectorIndex1 > basisVectorIndex2)
            {
                var index = 
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

                if (IndexScalarDictionary.TryGetValue(index, out scalar))
                {
                    scalar = ScalarProcessor.Negative(scalar);
                    return true;
                }
            }
            else
            {
                var index = 
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

                if (IndexScalarDictionary.TryGetValue(index, out scalar))
                    return true;
            }

            scalar = ScalarProcessor.ZeroScalar;
            return false;
        }

        public bool TryGetTermScalar(ulong basisVectorIndex1, ulong basisVectorIndex2, out TScalar scalar)
        {
            if (basisVectorIndex1 > basisVectorIndex2)
            {
                var index = 
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

                if (IndexScalarDictionary.TryGetValue(index, out scalar))
                {
                    scalar = ScalarProcessor.Negative(scalar);
                    return true;
                }
            }
            else
            {
                var index = 
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

                if (IndexScalarDictionary.TryGetValue(index, out scalar))
                    return true;
            }

            scalar = ScalarProcessor.ZeroScalar;
            return false;
        }


        public override bool TryGetTermByIndex(int index, out GaTerm<TScalar> term)
        {
            var i = (ulong) index;

            if (IndexScalarDictionary.TryGetValue(i, out var value))
            {
                term = GaTerm<TScalar>.CreateBivector(i, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTermByIndex(ulong index, out GaTerm<TScalar> term)
        {
            if (IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<TScalar>.CreateBivector(index, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(ulong id, out GaTerm<TScalar> term)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (Grade == grade && IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<TScalar>.CreateBivector(index, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(int grade, ulong index, out GaTerm<TScalar> term)
        {
            if (Grade == grade && IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<TScalar>.CreateBivector(index, value);
                return true;
            }

            term = null;
            return false;
        }

        public bool TryGetTerm(int basisVectorIndex1, int basisVectorIndex2, out GaTerm<TScalar> term)
        {
            if (basisVectorIndex1 > basisVectorIndex2)
            {
                var index = 
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

                if (IndexScalarDictionary.TryGetValue(index, out var scalar))
                {
                    term = GaTerm<TScalar>.CreateBivector(
                        basisVectorIndex2,
                        basisVectorIndex1,
                        ScalarProcessor.Negative(scalar)
                    );

                    return true;
                }
            }
            else
            {
                var index = 
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

                if (IndexScalarDictionary.TryGetValue(index, out var scalar))
                {
                    term = GaTerm<TScalar>.CreateBivector(
                        basisVectorIndex1,
                        basisVectorIndex2,
                        scalar
                    );

                    return true;
                }
            }

            term = null;
            return false;
        }

        public bool TryGetTerm(ulong basisVectorIndex1, ulong basisVectorIndex2, out GaTerm<TScalar> term)
        {
            if (basisVectorIndex1 > basisVectorIndex2)
            {
                var index = 
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

                if (IndexScalarDictionary.TryGetValue(index, out var scalar))
                {
                    term = GaTerm<TScalar>.CreateBivector(
                        basisVectorIndex2,
                        basisVectorIndex1,
                        ScalarProcessor.Negative(scalar)
                    );

                    return true;
                }
            }
            else
            {
                var index = 
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

                if (IndexScalarDictionary.TryGetValue(index, out var scalar))
                {
                    term = GaTerm<TScalar>.CreateBivector(
                        basisVectorIndex1,
                        basisVectorIndex2,
                        scalar
                    );

                    return true;
                }
            }

            term = null;
            return false;
        }


        public IEnumerable<Tuple<ulong, ulong>> GetTermsBasisVectorsIndices()
        {
            return IndexScalarDictionary
                .Keys
                .Select(BinaryCombinationsUtilsUInt64.IndexToCombinadic);
        }

        public IEnumerable<Tuple<ulong, ulong>> GetTermsBasisVectorsIndices(Func<ulong, bool> filterFunc)
        {
            return IndexScalarDictionary
                .Keys
                .Where(filterFunc)
                .Select(BinaryCombinationsUtilsUInt64.IndexToCombinadic);
        }

        public IEnumerable<Tuple<ulong, ulong>> GetTermsBasisVectorsIndices(Func<int, ulong, bool> filterFunc)
        {
            return IndexScalarDictionary
                .Keys
                .Where(index => filterFunc(Grade, index))
                .Select(BinaryCombinationsUtilsUInt64.IndexToCombinadic);
        }

        public IEnumerable<Tuple<ulong, ulong>> GetTermsBasisVectorsIndices(Func<ulong, TScalar, bool> filterFunc)
        {
            return IndexScalarDictionary
                .Where(pair => filterFunc(pair.Key, pair.Value))
                .Select(pair => pair.Key)
                .Select(BinaryCombinationsUtilsUInt64.IndexToCombinadic);
        }

        public IEnumerable<Tuple<ulong, ulong>> GetTermsBasisVectorsIndices(Func<int, ulong, TScalar, bool> filterFunc)
        {
            return IndexScalarDictionary
                .Where(pair => filterFunc(Grade, pair.Key, pair.Value))
                .Select(pair => pair.Key)
                .Select(BinaryCombinationsUtilsUInt64.IndexToCombinadic);
        }

        public IEnumerable<Tuple<ulong, ulong>> GetTermsBasisVectorsIndices(Func<TScalar, bool> filterFunc)
        {
            return IndexScalarDictionary
                .Where(pair => filterFunc(pair.Value))
                .Select(pair => pair.Key)
                .Select(BinaryCombinationsUtilsUInt64.IndexToCombinadic);
        }

        public IEnumerable<Tuple<ulong, ulong>> GetZeroTermsBasisVectorsIndices()
        {
            return IndexScalarDictionary
                .Where(p => ScalarProcessor.IsZero(p.Value))
                .Select(p => p.Key)
                .Select(BinaryCombinationsUtilsUInt64.IndexToCombinadic);
        }

        public IEnumerable<Tuple<ulong, ulong>> GetZeroTermsBasisVectorsIndices(bool nearZeroFlag)
        {
            return nearZeroFlag
                ? GetNearZeroTermsBasisVectorsIndices() 
                : GetZeroTermsBasisVectorsIndices();
        }

        public IEnumerable<Tuple<ulong, ulong>> GetNearZeroTermsBasisVectorsIndices()
        {
            return IndexScalarDictionary
                .Where(p => ScalarProcessor.IsNearZero(p.Value))
                .Select(p => p.Key)
                .Select(BinaryCombinationsUtilsUInt64.IndexToCombinadic);
        }


        public override GaKVectorStorageBase<TScalar> GetLeftScaledCopy(TScalar scalingFactor)
        {
            var indexScalarDictionary = IndexScalarDictionary.ToDictionary(
                pair => pair.Key, 
                pair => ScalarProcessor.Times(scalingFactor, pair.Value)
            );

            return new GaBivectorStorage<TScalar>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GaKVectorStorageBase<TScalar> GetRightScaledCopy(TScalar scalingFactor)
        {
            var indexScalarDictionary = IndexScalarDictionary.ToDictionary(
                pair => pair.Key, 
                pair => ScalarProcessor.Times(pair.Value, scalingFactor)
            );

            return new GaBivectorStorage<TScalar>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GaKVectorStorageBase<TScalar> GetComputedCopy(Func<TScalar, TScalar> mappingFunc)
        {
            var indexScalarDictionary = IndexScalarDictionary.ToDictionary(
                pair => pair.Key, 
                pair => mappingFunc(pair.Value)
            );

            return new GaBivectorStorage<TScalar>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GaKVectorStorageBase<TScalar> GetComputedCopy(Func<ulong, TScalar, TScalar> mappingFunc)
        {
            var indexScalarDictionary = IndexScalarDictionary.ToDictionary(
                pair => pair.Key, 
                pair => mappingFunc(pair.Key, pair.Value)
            );

            return new GaBivectorStorage<TScalar>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GaKVectorStorageBase<TScalar> GetComputedCopy(Func<ulong, TScalar> mappingFunc)
        {
            var indexScalarDictionary = IndexScalarDictionary.ToDictionary(
                pair => pair.Key, 
                pair => mappingFunc(pair.Key)
            );

            return new GaBivectorStorage<TScalar>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GaKVectorStorageBase<TScalar> GetComputedCopy(Func<int, ulong, TScalar, TScalar> mappingFunc)
        {
            var indexScalarDictionary = IndexScalarDictionary.ToDictionary(
                pair => pair.Key, 
                pair => mappingFunc(Grade, pair.Key, pair.Value)
            );

            return new GaBivectorStorage<TScalar>(
                ScalarProcessor,
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

            return new GaBivectorStorage<TScalar>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }


        public override IGaMultivectorStorage<TScalar> GetStorageCopy()
        {
            var indexScalarDictionary = IndexScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

            return new GaBivectorStorage<TScalar>(
                ScalarProcessor, 
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar> GetStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            var indexScalarDictionary = IndexScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => scalarMapping(pair.Value)
            );

            return new GaBivectorStorage<TScalar>(
                ScalarProcessor, 
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<ulong, TScalar, TScalar2> idScalarMapping)
        {
            return new GaBivectorStorage<TScalar2>(
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
            return new GaBivectorStorage<TScalar2>(
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
            return new GaBivectorStorage<TScalar2>(
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
            var indexScalarDictionary = IndexScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => ScalarProcessor.Negative(pair.Value)
            );

            return new GaBivectorStorage<TScalar>(
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
            return GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            return GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            return GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, bool> scalarSelection)
        {
            return GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            return GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            return GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart()
        {
            return this;
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            return new GaBivectorStorage<TScalar>(
                ScalarProcessor,
                IndexScalarDictionary.CopyToDictionary(scalarMapping),
                MaxBasisBladeId
            );
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            return new GaBivectorStorage<TScalar>(
                ScalarProcessor,
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => indexScalarMapping(pair.Key, pair.Value)
                ),
                MaxBasisBladeId
            );
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, bool> scalarSelection)
        {
            return GaBivectorStorage<TScalar>.Create(
                ScalarProcessor,
                IndexScalarDictionary
                    .Where(pair => scalarSelection(pair.Value))
                    .CopyToDictionary()
            );
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            return GaBivectorStorage<TScalar>.Create(
                ScalarProcessor,
                IndexScalarDictionary
                    .Where(pair => indexScalarSelection(pair.Key, pair.Value))
                    .CopyToDictionary()
            );
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            return GaBivectorStorage<TScalar>.Create(
                ScalarProcessor,
                IndexScalarDictionary
                    .Where(pair => indexSelection(pair.Key))
                    .CopyToDictionary()
            );
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade)
        {
            return grade == 2
                ? this
                : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, TScalar> scalarMapping)
        {
            return grade == 2
                ? GetBivectorPart(scalarMapping)
                : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            return grade == 2
                ? GetBivectorPart(indexScalarMapping)
                : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, bool> scalarSelection)
        {
            return grade == 2
                ? GetBivectorPart(scalarSelection)
                : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, bool> indexScalarSelection)
        {
            return grade == 2
                ? GetBivectorPart(indexScalarSelection)
                : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, bool> indexSelection)
        {
            return grade == 2
                ? GetBivectorPart(indexSelection)
                : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade);
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            return GetBivectorPart(scalarMapping);
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            return GaBivectorStorage<TScalar>.Create(
                ScalarProcessor,
                IndexScalarDictionary
                    .Where(pair => idSelection(GaBasisUtils.BasisBladeId(2, pair.Key)))
                    .CopyToDictionary()
            );
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<int, ulong, bool> gradeIndexSelection)
        {
            return GaBivectorStorage<TScalar>.Create(
                ScalarProcessor,
                IndexScalarDictionary
                    .Where(pair => gradeIndexSelection(2, pair.Key))
                    .CopyToDictionary()
            );
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, bool> scalarSelection)
        {
            return GetBivectorPart(scalarSelection);
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<ulong, TScalar, bool> idScalarSelection)
        {
            return GaBivectorStorage<TScalar>.Create(
                ScalarProcessor,
                IndexScalarDictionary
                    .Where(pair => idScalarSelection(GaBasisUtils.BasisBladeId(2, pair.Key), pair.Value))
                    .CopyToDictionary()
            );
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<int, ulong, TScalar, bool> gradeIndexScalarSelection)
        {
            return GaBivectorStorage<TScalar>.Create(
                ScalarProcessor,
                IndexScalarDictionary
                    .Where(pair => gradeIndexScalarSelection(2, pair.Key, pair.Value))
                    .CopyToDictionary()
            );
        }

        public override Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            return new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(
                GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor)
            );
        }

        public override Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            return new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(
                GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor)
            );
        }

        public override Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<TScalar, bool> scalarSelection)
        {
            return new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(
                GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor)
            );
        }


        public override bool IsScalar()
        {
            return false;
        }

        public override bool IsVector()
        {
            return false;
        }

        public override bool IsBivector()
        {
            return true;
        }

        public override IEnumerable<ulong> GetIds()
        {
            return IndexScalarDictionary.Keys.Select(
                BinaryCombinationsUtilsUInt64.IndexToCombinadicPattern
            );
        }

        public override IEnumerable<IGaBasisBlade> GetBasisBlades()
        {
            return IndexScalarDictionary.Select(pair => 
                (IGaBasisBlade)new GaBasisBivector(pair.Key)
            );
        }

        public override IEnumerable<KeyValuePair<ulong, TScalar>> GetIdScalarPairs()
        {
            return IndexScalarDictionary
                .Select(pair =>
                    new KeyValuePair<ulong, TScalar>(
                        BinaryCombinationsUtilsUInt64.IndexToCombinadicPattern(pair.Key),
                        pair.Value
                    )
                );
        }

        public override IEnumerable<Tuple<ulong, TScalar>> GetIdScalarTuples()
        {
            return IndexScalarDictionary
                .Select(pair =>
                    new Tuple<ulong, TScalar>(
                        BinaryCombinationsUtilsUInt64.IndexToCombinadicPattern(pair.Key),
                        pair.Value
                    )
                );
        }

        public override IReadOnlyDictionary<ulong, TScalar> GetIdScalarDictionary()
        {
            return IndexScalarDictionary.ToDictionary(
                pair => BinaryCombinationsUtilsUInt64.IndexToCombinadicPattern(pair.Key),
                pair => pair.Value
            );
        }

        public override IEnumerable<GaTerm<TScalar>> GetTerms()
        {
            return IndexScalarDictionary.Select(pair => 
                GaTerm<TScalar>.CreateBivector(pair.Key, pair.Value)
            );
        }

        public override IEnumerable<GaTerm<TScalar>> GetNotZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => !ScalarProcessor.IsZero(pair.Value))
                .Select(pair => 
                    GaTerm<TScalar>.CreateBivector(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<TScalar>> GetNotNearZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => !ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => 
                    GaTerm<TScalar>.CreateBivector(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<TScalar>> GetZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => ScalarProcessor.IsZero(pair.Value))
                .Select(pair => 
                    GaTerm<TScalar>.CreateBivector(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<TScalar>> GetNearZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => 
                    GaTerm<TScalar>.CreateBivector(pair.Key, pair.Value)
                );
        }


        public IGaBivectorStorage<TScalar> GetBivectorStorage()
        {
            return this;
        }

        public IGaBivectorStorage<TScalar> GetBivectorStorageCopy()
        {
            return new GaBivectorStorage<TScalar>(
                ScalarProcessor,
                IndexScalarDictionary.CopyToDictionary(),
                MaxBasisBladeId
            );
        }

        public IGaBivectorStorage<TScalar> GetBivectorStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            return new GaBivectorStorage<TScalar>(
                ScalarProcessor,
                IndexScalarDictionary.CopyToDictionary(scalarMapping),
                MaxBasisBladeId
            );
        }

        public IEnumerable<Tuple<ulong, ulong, TScalar>> GetBasisVectorsIndexScalarTuples()
        {
            return IndexScalarDictionary.Select(
                pair =>
                {
                    var (index, scalar) = pair;
                    var (index1, index2) = 
                        BinaryCombinationsUtilsUInt64.IndexToCombinadic(index);

                    return new Tuple<ulong, ulong, TScalar>(index1, index2, scalar);
                }
            );
        }

        public IGaBivectorStorage<TScalar> Add(IGaBivectorStorage<TScalar> mv2)
        {
            var composer = new GaBivectorStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerms(GetIndexScalarPairs());

            composer.AddTerms(mv2.GetIndexScalarPairs());

            return composer.GetBivectorStorage();
        }

        public IGaBivectorStorage<TScalar> Subtract(IGaBivectorStorage<TScalar> mv2)
        {
            var composer = new GaBivectorStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerms(GetIndexScalarPairs());

            composer.SubtractTerms(mv2.GetIndexScalarPairs());

            return composer.GetBivectorStorage();
        }
    }
}