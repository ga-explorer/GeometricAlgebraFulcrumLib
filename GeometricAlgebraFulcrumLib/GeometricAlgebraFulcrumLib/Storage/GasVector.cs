using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Terms;
using GeometricAlgebraFulcrumLib.Processing.Scalars;


namespace GeometricAlgebraFulcrumLib.Storage
{
    /// <summary>
    /// Can store the scalar coefficients of a vector of any dimension.
    /// The scalars are assumed to be of immutable type such as T, complex, etc.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class GasVector<T> 
        : GasKVectorBase<T>, IGasVector<T>
    {
        public override uint Grade => 1;


        internal GasVector([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] Dictionary<ulong, T> indexScalarDictionary, ulong maxBasisBladeId)
            : base(scalarProcessor, indexScalarDictionary, maxBasisBladeId)
        {
        }


        public override GaTerm<T> GetTermByIndex(int index)
        {
            var i = (ulong) index;

            return GaTerm<T>.CreateVector(
                i, 
                IndexScalarDictionary.TryGetValue(i, out var scalar)
                    ? scalar : ScalarProcessor.ZeroScalar
            );
        }

        public override GaTerm<T> GetTermByIndex(ulong index)
        {
            return GaTerm<T>.CreateVector(
                index, 
                IndexScalarDictionary.TryGetValue(index, out var scalar)
                    ? scalar : ScalarProcessor.ZeroScalar
            );
        }

        public override GaTerm<T> GetTerm(ulong id)
        {
            Debug.Assert(id.BasisBladeGrade() == 1);

            var index = id.BasisVectorIndex();

            return GaTerm<T>.CreateVector(
                index, 
                IndexScalarDictionary.TryGetValue(index, out var scalar)
                    ? scalar : ScalarProcessor.ZeroScalar
            );
        }

        public override GaTerm<T> GetTerm(uint grade, ulong index)
        {
            Debug.Assert(grade == 1);

            return GaTerm<T>.CreateVector(
                index, 
                IndexScalarDictionary.TryGetValue(index, out var scalar)
                    ? scalar : ScalarProcessor.ZeroScalar
            );
        }


        public override bool TryGetTermByIndex(int index, out GaTerm<T> term)
        {
            var i = (ulong) index;

            if (IndexScalarDictionary.TryGetValue(i, out var value))
            {
                term = GaTerm<T>.CreateVector(i, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTermByIndex(ulong index, out GaTerm<T> term)
        {
            if (IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<T>.CreateVector(index, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(ulong id, out GaTerm<T> term)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (grade == 1 && IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<T>.CreateVector(index, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(uint grade, ulong index, out GaTerm<T> term)
        {
            if (grade == 1 && IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<T>.CreateVector(index, value);
                return true;
            }

            term = null;
            return false;
        }


        public override GasKVectorBase<T> GetLeftScaledCopy(T scalingFactor)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.CopyToDictionary(
                    scalar => ScalarProcessor.Times(scalingFactor, scalar)
                );

            return new GasVector<T>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GasKVectorBase<T> GetRightScaledCopy(T scalingFactor)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.CopyToDictionary(
                    scalar => ScalarProcessor.Times(scalar, scalingFactor)
                );

            return new GasVector<T>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GasKVectorBase<T> GetComputedCopy(Func<T, T> mappingFunc)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.CopyToDictionary(mappingFunc);

            return new GasVector<T>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GasKVectorBase<T> GetComputedCopy(Func<ulong, T, T> mappingFunc)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => mappingFunc(pair.Key, pair.Value)
                );

            return new GasVector<T>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GasKVectorBase<T> GetComputedCopy(Func<ulong, T> mappingFunc)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => mappingFunc(pair.Key)
                );

            return new GasVector<T>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GasKVectorBase<T> GetComputedCopy(Func<uint, ulong, T, T> mappingFunc)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => mappingFunc(Grade, pair.Key, pair.Value)
                );

            return new GasVector<T>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GasKVectorBase<T> GetComputedCopy(Func<uint, ulong, T> mappingFunc)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => mappingFunc(Grade, pair.Key)
                );

            return new GasVector<T>(
                ScalarProcessor,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }


        public override IGasMultivector<T> GetCopy()
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.CopyToDictionary();

            return new GasVector<T>(
                ScalarProcessor, 
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T> GetCopy(Func<T, T> scalarMapping)
        {
            return GetVectorPart(scalarMapping);
        }

        public override IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<ulong, T, T2> idScalarMapping)
        {
            return new GasVector<T2>(
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
            return new GasVector<T2>(
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
            return new GasVector<T2>(
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
            var indexScalarDictionary = 
                IndexScalarDictionary.CopyToDictionary(ScalarProcessor.Negative);

            return new GasVector<T>(
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
            return this;
        }

        public override IGasVector<T> GetVectorPart(Func<T, T> scalarMapping)
        {
            return new GasVector<T>(
                ScalarProcessor,
                IndexScalarDictionary.CopyToDictionary(scalarMapping),
                MaxBasisBladeId
            );
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, T, T> indexScalarMapping)
        {
            return new GasVector<T>(
                ScalarProcessor,
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => indexScalarMapping(pair.Key, pair.Value)
                    ),
                MaxBasisBladeId
            );
        }

        public override IGasVector<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            return ScalarProcessor.CreateVector(IndexScalarDictionary
                    .Where(pair => scalarSelection(pair.Value))
                    .CopyToDictionary()
            );
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return ScalarProcessor.CreateVector(IndexScalarDictionary
                    .Where(pair => indexScalarSelection(pair.Key, pair.Value))
                    .CopyToDictionary()
            );
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            return ScalarProcessor.CreateVector(IndexScalarDictionary
                    .Where(pair => indexSelection(pair.Key))
                    .CopyToDictionary()
            );
        }

        public override IGasBivector<T> GetBivectorPart()
        {
            return ScalarProcessor.CreateZeroBivector();
        }

        public override IGasBivector<T> GetBivectorPart(Func<T, T> scalarMapping)
        {
            return ScalarProcessor.CreateZeroBivector();
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, T, T> indexScalarMapping)
        {
            return ScalarProcessor.CreateZeroBivector();
        }

        public override IGasBivector<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            return ScalarProcessor.CreateZeroBivector();
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return ScalarProcessor.CreateZeroBivector();
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            return ScalarProcessor.CreateZeroBivector();
        }

        public override IGasKVector<T> GetKVectorPart(uint grade)
        {
            return grade == 1
                ? this
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<T, T> scalarMapping)
        {
            return grade == 1
                ? GetVectorPart(scalarMapping)
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, T> indexScalarMapping)
        {
            return grade == 1
                ? GetVectorPart(indexScalarMapping)
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            return grade == 1
                ? GetVectorPart(scalarSelection)
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            return grade == 1
                ? GetVectorPart(indexScalarSelection)
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            return grade == 1
                ? GetVectorPart(indexSelection)
                : ScalarProcessor.CreateZeroKVector(grade);
        }
        

        public override IGasMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            return ScalarProcessor.CreateVector(IndexScalarDictionary
                    .Where(pair => idSelection(pair.Key))
                    .CopyToDictionary()
            );
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            return ScalarProcessor.CreateVector(IndexScalarDictionary
                    .Where(pair => gradeIndexSelection(1, pair.Key))
                    .CopyToDictionary()
            );
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            return GetVectorPart(scalarSelection);
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            return ScalarProcessor.CreateVector(IndexScalarDictionary
                    .Where(pair => idScalarSelection(GaBasisUtils.BasisBladeId(1, pair.Key), pair.Value))
                    .CopyToDictionary()
            );
        }

        public override IGasMultivector<T> GetMultivectorPart(
            Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            return ScalarProcessor.CreateVector(IndexScalarDictionary
                    .Where(pair => gradeIndexScalarSelection(1, pair.Key, pair.Value))
                    .CopyToDictionary()
            );
        }

        public override Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                if (indexSelection(index))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGasVector<T>, IGasVector<T>>(
                ScalarProcessor.CreateVector(indexScalarDictionary1),
                ScalarProcessor.CreateVector(indexScalarDictionary2)
            );
        }

        public override Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                if (indexScalarSelection(index, scalar))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGasVector<T>, IGasVector<T>>(
                ScalarProcessor.CreateVector(indexScalarDictionary1),
                ScalarProcessor.CreateVector(indexScalarDictionary2)
            );
        }

        public override Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                if (scalarSelection(scalar))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGasVector<T>, IGasVector<T>>(
                ScalarProcessor.CreateVector(indexScalarDictionary1),
                ScalarProcessor.CreateVector(indexScalarDictionary2)
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
                (IGaBasisBlade) index.CreateBasisVector()
            );
        }

        public override IEnumerable<KeyValuePair<ulong, T>> GetIdScalarPairs()
        {
            return IndexScalarDictionary
                .Select(pair => new KeyValuePair<ulong, T>(1UL << (int)pair.Key, pair.Value));
        }

        public override IEnumerable<Tuple<ulong, T>> GetIdScalarTuples()
        {
            return IndexScalarDictionary
                .Select(pair => new Tuple<ulong, T>(1UL << (int)pair.Key, pair.Value));
        }

        public override IReadOnlyDictionary<ulong, T> GetIdScalarDictionary()
        {
            return IndexScalarDictionary.ToDictionary(
                pair => 1UL << (int)pair.Key,
                pair => pair.Value
            );
        }

        public override IEnumerable<GaTerm<T>> GetTerms()
        {
            return IndexScalarDictionary
                .Select(pair => GaTerm<T>.CreateVector(pair.Key, pair.Value));
        }

        public override IEnumerable<GaTerm<T>> GetNotZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => !ScalarProcessor.IsZero(pair.Value))
                .Select(pair => GaTerm<T>.CreateVector(pair.Key, pair.Value));
        }

        public override IEnumerable<GaTerm<T>> GetNotNearZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => !ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => GaTerm<T>.CreateVector(pair.Key, pair.Value));
        }

        public override IEnumerable<GaTerm<T>> GetZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => ScalarProcessor.IsZero(pair.Value))
                .Select(pair => GaTerm<T>.CreateVector(pair.Key, pair.Value));
        }

        public override IEnumerable<GaTerm<T>> GetNearZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => GaTerm<T>.CreateVector(pair.Key, pair.Value));
        }

        
        public IGasVector<T> GetVectorStorage()
        {
            return this;
        }

        public IGasVector<T> GetVectorStorageCopy()
        {
            return IndexScalarDictionary.Count switch
            {
                0 => ScalarProcessor.CreateZeroVector(),
                1 => ScalarProcessor.CreateVector(IndexScalarDictionary.First()),
                _ => new GasVector<T>(ScalarProcessor, IndexScalarDictionary.CopyToDictionary(), MaxBasisBladeId)
            };
        }

        public IGasVector<T> GetVectorStorageCopy(Func<T, T> scalarMapping)
        {
            return IndexScalarDictionary.Count switch
            {
                0 => ScalarProcessor.CreateZeroVector(),
                1 => ScalarProcessor.CreateVector(IndexScalarDictionary.First(scalarMapping)),
                _ => new GasVector<T>(ScalarProcessor, IndexScalarDictionary.CopyToDictionary(scalarMapping), MaxBasisBladeId)
            };
        }
    }
}