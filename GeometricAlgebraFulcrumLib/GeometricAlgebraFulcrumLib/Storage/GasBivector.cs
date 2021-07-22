using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Combinations;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Terms;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Storage
{
    /// <summary>
    /// Can store the scalar coefficients of a vector of any dimension.
    /// The scalars are assumed to be of immutable type such as T, complex, etc.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class GasBivector<T> 
        : GasKVectorBase<T>, IGasBivector<T>
    {
        public override uint Grade 
            => 2;


        internal GasBivector([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] Dictionary<ulong, T> indexScalarDictionary, ulong maxBasisBladeId)
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


        public T GetTermScalar(int basisVectorIndex1, int basisVectorIndex2)
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

        public T GetTermScalar(ulong basisVectorIndex1, ulong basisVectorIndex2)
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


        public override GaTerm<T> GetTermByIndex(int index)
        {
            var i = (ulong) index;

            return GaTerm<T>.CreateBivector(
                i, 
                IndexScalarDictionary.TryGetValue(i, out var scalar) 
                    ? scalar 
                    : ScalarProcessor.ZeroScalar
            );
        }

        public override GaTerm<T> GetTermByIndex(ulong index)
        {
            return GaTerm<T>.CreateBivector(
                index, 
                IndexScalarDictionary.TryGetValue(index, out var scalar) 
                    ? scalar 
                    : ScalarProcessor.ZeroScalar
            );
        }

        public override GaTerm<T> GetTerm(ulong id)
        {
            Debug.Assert(id.BasisBladeGrade() == 2);

            var index = BinaryCombinationsUtilsUInt64.CombinadicPatternToIndex(id);

            return GaTerm<T>.CreateBivector(
                index, 
                IndexScalarDictionary.TryGetValue(index, out var scalar) 
                    ? scalar 
                    : ScalarProcessor.ZeroScalar
            );
        }

        public override GaTerm<T> GetTerm(uint grade, ulong index)
        {
            Debug.Assert(grade == 2);

            return GaTerm<T>.CreateBivector(
                index, 
                IndexScalarDictionary.TryGetValue(index, out var scalar) 
                    ? scalar 
                    : ScalarProcessor.ZeroScalar
            );
        }

        public GaTerm<T> GetTerm(int basisVectorIndex1, int basisVectorIndex2)
        {
            if (basisVectorIndex1 > basisVectorIndex2)
            {
                var index = 
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

                return GaTerm<T>.CreateBivector(
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

                return GaTerm<T>.CreateBivector(
                    index, 
                    IndexScalarDictionary.TryGetValue(index, out var scalar) 
                        ? scalar 
                        : ScalarProcessor.ZeroScalar
                );
            }
        }

        public GaTerm<T> GetTerm(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            if (basisVectorIndex1 > basisVectorIndex2)
            {
                var index = 
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

                return GaTerm<T>.CreateBivector(
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

                return GaTerm<T>.CreateBivector(
                    index, 
                    IndexScalarDictionary.TryGetValue(index, out var scalar) 
                        ? scalar 
                        : ScalarProcessor.ZeroScalar
                );
            }
        }


        public bool TryGetTermScalar(int basisVectorIndex1, int basisVectorIndex2, out T scalar)
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

        public bool TryGetTermScalar(ulong basisVectorIndex1, ulong basisVectorIndex2, out T scalar)
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


        public override bool TryGetTermByIndex(int index, out GaTerm<T> term)
        {
            var i = (ulong) index;

            if (IndexScalarDictionary.TryGetValue(i, out var value))
            {
                term = GaTerm<T>.CreateBivector(i, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTermByIndex(ulong index, out GaTerm<T> term)
        {
            if (IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<T>.CreateBivector(index, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(ulong id, out GaTerm<T> term)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (Grade == grade && IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<T>.CreateBivector(index, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(uint grade, ulong index, out GaTerm<T> term)
        {
            if (Grade == grade && IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<T>.CreateBivector(index, value);
                return true;
            }

            term = null;
            return false;
        }

        public bool TryGetTerm(int basisVectorIndex1, int basisVectorIndex2, out GaTerm<T> term)
        {
            if (basisVectorIndex1 > basisVectorIndex2)
            {
                var index = 
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

                if (IndexScalarDictionary.TryGetValue(index, out var scalar))
                {
                    term = GaTerm<T>.CreateBivector(
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
                    term = GaTerm<T>.CreateBivector(
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

        public bool TryGetTerm(ulong basisVectorIndex1, ulong basisVectorIndex2, out GaTerm<T> term)
        {
            if (basisVectorIndex1 > basisVectorIndex2)
            {
                var index = 
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

                if (IndexScalarDictionary.TryGetValue(index, out var scalar))
                {
                    term = GaTerm<T>.CreateBivector(
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
                    term = GaTerm<T>.CreateBivector(
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

        public IEnumerable<Tuple<ulong, ulong>> GetTermsBasisVectorsIndices(Func<uint, ulong, bool> filterFunc)
        {
            return IndexScalarDictionary
                .Keys
                .Where(index => filterFunc(Grade, index))
                .Select(BinaryCombinationsUtilsUInt64.IndexToCombinadic);
        }

        public IEnumerable<Tuple<ulong, ulong>> GetTermsBasisVectorsIndices(Func<ulong, T, bool> filterFunc)
        {
            return IndexScalarDictionary
                .Where(pair => filterFunc(pair.Key, pair.Value))
                .Select(pair => pair.Key)
                .Select(BinaryCombinationsUtilsUInt64.IndexToCombinadic);
        }

        public IEnumerable<Tuple<ulong, ulong>> GetTermsBasisVectorsIndices(Func<uint, ulong, T, bool> filterFunc)
        {
            return IndexScalarDictionary
                .Where(pair => filterFunc(Grade, pair.Key, pair.Value))
                .Select(pair => pair.Key)
                .Select(BinaryCombinationsUtilsUInt64.IndexToCombinadic);
        }

        public IEnumerable<Tuple<ulong, ulong>> GetTermsBasisVectorsIndices(Func<T, bool> filterFunc)
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


        public override GasKVectorBase<T> GetLeftScaledCopy(T scalingFactor)
        {
            var indexScalarDictionary = IndexScalarDictionary.ToDictionary(
                pair => pair.Key, 
                pair => ScalarProcessor.Times(scalingFactor, pair.Value)
            );

            return new GasBivector<T>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GasKVectorBase<T> GetRightScaledCopy(T scalingFactor)
        {
            var indexScalarDictionary = IndexScalarDictionary.ToDictionary(
                pair => pair.Key, 
                pair => ScalarProcessor.Times(pair.Value, scalingFactor)
            );

            return new GasBivector<T>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GasKVectorBase<T> GetComputedCopy(Func<T, T> mappingFunc)
        {
            var indexScalarDictionary = IndexScalarDictionary.ToDictionary(
                pair => pair.Key, 
                pair => mappingFunc(pair.Value)
            );

            return new GasBivector<T>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GasKVectorBase<T> GetComputedCopy(Func<ulong, T, T> mappingFunc)
        {
            var indexScalarDictionary = IndexScalarDictionary.ToDictionary(
                pair => pair.Key, 
                pair => mappingFunc(pair.Key, pair.Value)
            );

            return new GasBivector<T>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GasKVectorBase<T> GetComputedCopy(Func<ulong, T> mappingFunc)
        {
            var indexScalarDictionary = IndexScalarDictionary.ToDictionary(
                pair => pair.Key, 
                pair => mappingFunc(pair.Key)
            );

            return new GasBivector<T>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GasKVectorBase<T> GetComputedCopy(Func<uint, ulong, T, T> mappingFunc)
        {
            var indexScalarDictionary = IndexScalarDictionary.ToDictionary(
                pair => pair.Key, 
                pair => mappingFunc(Grade, pair.Key, pair.Value)
            );

            return new GasBivector<T>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GasKVectorBase<T> GetComputedCopy(Func<uint, ulong, T> mappingFunc)
        {
            var indexScalarDictionary = IndexScalarDictionary.ToDictionary(
                pair => pair.Key, 
                pair => mappingFunc(Grade, pair.Key)
            );

            return new GasBivector<T>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }


        public override IGasMultivector<T> GetCopy()
        {
            return GetBivectorPart();
        }

        public override IGasMultivector<T> GetCopy(Func<T, T> scalarMapping)
        {
            var indexScalarDictionary = IndexScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => scalarMapping(pair.Value)
            );

            return new GasBivector<T>(
                ScalarProcessor, 
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<ulong, T, T2> idScalarMapping)
        {
            return new GasBivector<T2>(
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

        public override IGasMultivector<T2> GetCopy<T2>(
            IGaScalarProcessor<T2> scalarProcessor, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return new GasBivector<T2>(
                scalarProcessor,
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => gradeIndexScalarMapping(Grade, pair.Key, pair.Value)
                ),
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<T, T2> scalarMapping)
        {
            return new GasBivector<T2>(
                scalarProcessor,
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => scalarMapping(pair.Value)
                ),
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T> GetNegative()
        {
            var indexScalarDictionary = IndexScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => ScalarProcessor.Negative(pair.Value)
            );

            return new GasBivector<T>(
                ScalarProcessor, 
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGasScalar<T> GetScalarPart()
        {
            return ScalarProcessor.CreateZeroScalar();
        }

        public override IGasScalar<T> GetScalarPart(Func<T, T> scalarMapping)
        {
            return ScalarProcessor.CreateZeroScalar();
        }

        public override IGasVector<T> GetVectorPart()
        {
            return ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<T, T> scalarMapping)
        {
            return ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, T, T> indexScalarMapping)
        {
            return ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            return ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            return ScalarProcessor.CreateZeroVector();
        }

        public override IGasBivector<T> GetBivectorPart()
        {
            return this;
        }

        public override IGasBivector<T> GetBivectorPart(Func<T, T> scalarMapping)
        {
            return new GasBivector<T>(
                ScalarProcessor,
                IndexScalarDictionary.CopyToDictionary(scalarMapping),
                MaxBasisBladeId
            );
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, T, T> indexScalarMapping)
        {
            return new GasBivector<T>(
                ScalarProcessor,
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => indexScalarMapping(pair.Key, pair.Value)
                ),
                MaxBasisBladeId
            );
        }

        public override IGasBivector<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            return ScalarProcessor.CreateBivector(IndexScalarDictionary
                    .Where(pair => scalarSelection(pair.Value))
                    .CopyToDictionary()
            );
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return ScalarProcessor.CreateBivector(IndexScalarDictionary
                    .Where(pair => indexScalarSelection(pair.Key, pair.Value))
                    .CopyToDictionary()
            );
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            return ScalarProcessor.CreateBivector(IndexScalarDictionary
                    .Where(pair => indexSelection(pair.Key))
                    .CopyToDictionary()
            );
        }

        public override IGasKVector<T> GetKVectorPart(uint grade)
        {
            return grade == 2
                ? this
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<T, T> scalarMapping)
        {
            return grade == 2
                ? GetBivectorPart(scalarMapping)
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, T> indexScalarMapping)
        {
            return grade == 2
                ? GetBivectorPart(indexScalarMapping)
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            return grade == 2
                ? GetBivectorPart(scalarSelection)
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            return grade == 2
                ? GetBivectorPart(indexScalarSelection)
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            return grade == 2
                ? GetBivectorPart(indexSelection)
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            return ScalarProcessor.CreateBivector(IndexScalarDictionary
                    .Where(pair => idSelection(GaBasisUtils.BasisBladeId(2, pair.Key)))
                    .CopyToDictionary()
            );
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            return ScalarProcessor.CreateBivector(IndexScalarDictionary
                    .Where(pair => gradeIndexSelection(2, pair.Key))
                    .CopyToDictionary()
            );
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            return GetBivectorPart(scalarSelection);
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            return ScalarProcessor.CreateBivector(IndexScalarDictionary
                    .Where(pair => idScalarSelection(GaBasisUtils.BasisBladeId(2, pair.Key), pair.Value))
                    .CopyToDictionary()
            );
        }

        public override IGasMultivector<T> GetMultivectorPart(
            Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            return ScalarProcessor.CreateBivector(IndexScalarDictionary
                    .Where(pair => gradeIndexScalarSelection(2, pair.Key, pair.Value))
                    .CopyToDictionary()
            );
        }

        public override Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            return new Tuple<IGasVector<T>, IGasVector<T>>(
                ScalarProcessor.CreateZeroVector(),
                ScalarProcessor.CreateZeroVector()
            );
        }

        public override Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return new Tuple<IGasVector<T>, IGasVector<T>>(
                ScalarProcessor.CreateZeroVector(),
                ScalarProcessor.CreateZeroVector()
            );
        }

        public override Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<T, bool> scalarSelection)
        {
            return new Tuple<IGasVector<T>, IGasVector<T>>(
                ScalarProcessor.CreateZeroVector(),
                ScalarProcessor.CreateZeroVector()
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
                (IGaBasisBlade)pair.Key.CreateBasisBivector()
            );
        }

        public override IEnumerable<KeyValuePair<ulong, T>> GetIdScalarPairs()
        {
            return IndexScalarDictionary
                .Select(pair =>
                    new KeyValuePair<ulong, T>(
                        BinaryCombinationsUtilsUInt64.IndexToCombinadicPattern(pair.Key),
                        pair.Value
                    )
                );
        }

        public override IEnumerable<Tuple<ulong, T>> GetIdScalarTuples()
        {
            return IndexScalarDictionary
                .Select(pair =>
                    new Tuple<ulong, T>(
                        BinaryCombinationsUtilsUInt64.IndexToCombinadicPattern(pair.Key),
                        pair.Value
                    )
                );
        }

        public override IReadOnlyDictionary<ulong, T> GetIdScalarDictionary()
        {
            return IndexScalarDictionary.ToDictionary(
                pair => BinaryCombinationsUtilsUInt64.IndexToCombinadicPattern(pair.Key),
                pair => pair.Value
            );
        }

        public override IEnumerable<GaTerm<T>> GetTerms()
        {
            return IndexScalarDictionary.Select(pair => 
                GaTerm<T>.CreateBivector(pair.Key, pair.Value)
            );
        }

        public override IEnumerable<GaTerm<T>> GetNotZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => !ScalarProcessor.IsZero(pair.Value))
                .Select(pair => 
                    GaTerm<T>.CreateBivector(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<T>> GetNotNearZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => !ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => 
                    GaTerm<T>.CreateBivector(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<T>> GetZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => ScalarProcessor.IsZero(pair.Value))
                .Select(pair => 
                    GaTerm<T>.CreateBivector(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<T>> GetNearZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => 
                    GaTerm<T>.CreateBivector(pair.Key, pair.Value)
                );
        }


        public IGasBivector<T> GetBivectorStorage()
        {
            return this;
        }

        public IGasBivector<T> GetBivectorStorageCopy()
        {
            return new GasBivector<T>(
                ScalarProcessor,
                IndexScalarDictionary.CopyToDictionary(),
                MaxBasisBladeId
            );
        }

        public IGasBivector<T> GetBivectorStorageCopy(Func<T, T> scalarMapping)
        {
            return new GasBivector<T>(
                ScalarProcessor,
                IndexScalarDictionary.CopyToDictionary(scalarMapping),
                MaxBasisBladeId
            );
        }

        public IEnumerable<Tuple<ulong, ulong, T>> GetBasisVectorsIndexScalarTuples()
        {
            return IndexScalarDictionary.Select(
                pair =>
                {
                    var (index, scalar) = pair;
                    var (index1, index2) = 
                        BinaryCombinationsUtilsUInt64.IndexToCombinadic(index);

                    return new Tuple<ulong, ulong, T>(index1, index2, scalar);
                }
            );
        }

        public IGasBivector<T> Add(IGasBivector<T> mv2)
        {
            var composer = new GaBivectorStorageComposer<T>(ScalarProcessor);

            composer.SetTerms(GetIndexScalarPairs());

            composer.AddTerms(mv2.GetIndexScalarPairs());

            return composer.GetBivectorStorage();
        }

        public IGasBivector<T> Subtract(IGasBivector<T> mv2)
        {
            var composer = new GaBivectorStorageComposer<T>(ScalarProcessor);

            composer.SetTerms(GetIndexScalarPairs());

            composer.SubtractTerms(mv2.GetIndexScalarPairs());

            return composer.GetBivectorStorage();
        }
    }
}