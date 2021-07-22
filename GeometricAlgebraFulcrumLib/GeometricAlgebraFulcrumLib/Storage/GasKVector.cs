using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Terms;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Storage
{
    /// <summary>
    /// Can store the scalar coefficients of a k-vector of any dimension.
    /// The scalars are assumed to be of immutable type such as T, complex, etc.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class GasKVector<T> 
        : GasKVectorBase<T>
    {
        public override uint Grade { get; }


        internal GasKVector([NotNull] IGaScalarProcessor<T> scalarProcessor, uint grade, [NotNull] Dictionary<ulong, T> indexScalarDictionary, ulong maxBasisBladeId)
            : base(scalarProcessor, indexScalarDictionary, maxBasisBladeId)
        {
            if (grade < 3 || grade > GaSpaceUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(grade));

            Grade = grade;
        }


        public override GaTerm<T> GetTermByIndex(int index)
        {
            var i = (ulong) index;

            return GaTerm<T>.CreateGraded(
                Grade,
                i,
                IndexScalarDictionary.TryGetValue(i, out var scalar)
                    ? scalar
                    : ScalarProcessor.ZeroScalar
            );
        }

        public override GaTerm<T> GetTermByIndex(ulong index)
        {
            return GaTerm<T>.CreateGraded(
                Grade,
                index,
                IndexScalarDictionary.TryGetValue(index, out var scalar)
                    ? scalar
                    : ScalarProcessor.ZeroScalar
            );
        }

        public override GaTerm<T> GetTerm(ulong id)
        {
            Debug.Assert(id.BasisBladeGrade() == Grade);

            var index = id.BasisBladeIndex();

            return GaTerm<T>.CreateGraded(
                Grade,
                index,
                IndexScalarDictionary.TryGetValue(index, out var scalar)
                    ? scalar
                    : ScalarProcessor.ZeroScalar
            );
        }

        public override GaTerm<T> GetTerm(uint grade, ulong index)
        {
            Debug.Assert(grade == Grade);

            return GaTerm<T>.CreateGraded(
                Grade,
                index,
                IndexScalarDictionary.TryGetValue(index, out var scalar)
                    ? scalar
                    : ScalarProcessor.ZeroScalar
            );
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


        public override GasKVectorBase<T> GetLeftScaledCopy(T scalingFactor)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.CopyToDictionary(
                    scalar => ScalarProcessor.Times(scalingFactor, scalar)
                );

            return new GasKVector<T>(
                ScalarProcessor,
                Grade,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GasKVectorBase<T> GetRightScaledCopy(T scalingFactor)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => ScalarProcessor.Times(pair.Value, scalingFactor)
                );

            return new GasKVector<T>(
                ScalarProcessor,
                Grade,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GasKVectorBase<T> GetComputedCopy(Func<T, T> mappingFunc)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.CopyToDictionary(mappingFunc);

            return new GasKVector<T>(
                ScalarProcessor,
                Grade,
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

            return new GasKVector<T>(
                ScalarProcessor,
                Grade,
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

            return new GasKVector<T>(
                ScalarProcessor,
                Grade,
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

            return new GasKVector<T>(
                ScalarProcessor,
                Grade,
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

            return new GasKVector<T>(
                ScalarProcessor,
                Grade,
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }


        public override IGasMultivector<T> GetCopy()
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.CopyToDictionary();

            return new GasKVector<T>(
                ScalarProcessor, 
                Grade, 
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T> GetCopy(Func<T, T> scalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.CopyToDictionary(scalarMapping);

            return new GasKVector<T>(
                ScalarProcessor, 
                Grade, 
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<ulong, T, T2> idScalarMapping)
        {
            return new GasKVector<T2>(
                scalarProcessor,
                Grade,
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
            return new GasKVector<T2>(
                scalarProcessor,
                Grade,
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => gradeIndexScalarMapping(Grade, pair.Key, pair.Value)
                ),
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<T, T2> scalarMapping)
        {
            return new GasKVector<T2>(
                scalarProcessor,
                Grade,
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

            return new GasKVector<T>(
                ScalarProcessor, 
                Grade, 
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGasScalar<T> GetScalarPart()
        {
            return Grade == 0 
                ? ScalarProcessor.CreateScalar(GetTermScalarByIndex(0))
                : ScalarProcessor.CreateZeroScalar();
        }

        public override IGasScalar<T> GetScalarPart(Func<T, T> scalarMapping)
        {
            return Grade == 0 
                ? ScalarProcessor.CreateScalar(scalarMapping(GetTermScalar(0)))
                : ScalarProcessor.CreateZeroScalar();
        }

        public override IGasVector<T> GetVectorPart()
        {
            return Grade == 1
                ? ScalarProcessor.CreateVector(IndexScalarDictionary)
                : ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<T, T> scalarMapping)
        {
            return Grade == 1
                ? ScalarProcessor.CreateVector(IndexScalarDictionary.CopyToDictionary(scalarMapping)
                )
                : ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, T, T> indexScalarMapping)
        {
            return Grade == 1
                ? ScalarProcessor.CreateVector(IndexScalarDictionary.ToDictionary(
                        pair => pair.Key,
                        pair => indexScalarMapping(pair.Key, pair.Value)
                    )
                )
                : ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            if (Grade != 1)
                return ScalarProcessor.CreateZeroVector();

            return ScalarProcessor.CreateVector(IndexScalarDictionary
                    .Where(pair => scalarSelection(pair.Value))
                    .CopyToDictionary()
            );
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            if (Grade != 1)
                return ScalarProcessor.CreateZeroVector();

            return ScalarProcessor.CreateVector(IndexScalarDictionary
                    .Where(pair => indexScalarSelection(pair.Key, pair.Value))
                    .CopyToDictionary()
            );
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            if (Grade != 1)
                return ScalarProcessor.CreateZeroVector();

            return ScalarProcessor.CreateVector(IndexScalarDictionary
                    .Where(pair => indexSelection(pair.Key))
                    .CopyToDictionary()
            );
        }

        public override IGasBivector<T> GetBivectorPart()
        {
            return Grade == 2
                ? ScalarProcessor.CreateBivector(IndexScalarDictionary)
                : ScalarProcessor.CreateZeroBivector();
        }

        public override IGasBivector<T> GetBivectorPart(Func<T, T> scalarMapping)
        {
            return Grade == 2
                ? ScalarProcessor.CreateBivector(IndexScalarDictionary.CopyToDictionary(scalarMapping)
                )
                : ScalarProcessor.CreateZeroBivector();
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, T, T> indexScalarMapping)
        {
            return Grade == 2
                ? ScalarProcessor.CreateBivector(IndexScalarDictionary.ToDictionary(
                        pair => pair.Key,
                        pair => indexScalarMapping(pair.Key, pair.Value)
                    )
                )
                : ScalarProcessor.CreateZeroBivector();
        }

        public override IGasBivector<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            if (Grade != 2)
                return ScalarProcessor.CreateZeroBivector();

            var indexScalarDictionary =
                IndexScalarDictionary
                    .Where(pair => scalarSelection(pair.Value))
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value
                    );

            return indexScalarDictionary.Count == 0
                ? ScalarProcessor.CreateZeroBivector()
                : ScalarProcessor.CreateBivector(indexScalarDictionary);
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            if (Grade != 2)
                return ScalarProcessor.CreateZeroBivector();

            var indexScalarDictionary =
                IndexScalarDictionary
                    .Where(pair => indexScalarSelection(pair.Key, pair.Value))
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value
                    );

            return indexScalarDictionary.Count == 0
                ? ScalarProcessor.CreateZeroBivector()
                : ScalarProcessor.CreateBivector(indexScalarDictionary);
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            if (Grade != 2)
                return ScalarProcessor.CreateZeroBivector();

            var indexScalarDictionary =
                IndexScalarDictionary
                    .Where(pair => indexSelection(pair.Key))
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value
                    );

            return indexScalarDictionary.Count == 0
                ? ScalarProcessor.CreateZeroBivector()
                : ScalarProcessor.CreateBivector(indexScalarDictionary);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade)
        {
            return grade == Grade
                ? this
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<T, T> scalarMapping)
        {
            if (grade != Grade) 
                return ScalarProcessor.CreateZeroKVector(grade);

            return grade switch
            {
                0 => GetScalarPart(scalarMapping),
                1 => GetVectorPart(scalarMapping),
                2 => GetBivectorPart(scalarMapping),
                _ => new GasKVector<T>(
                    ScalarProcessor,
                    Grade,
                    IndexScalarDictionary.CopyToDictionary(scalarMapping),
                    MaxBasisBladeId
                )
            };
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, T> indexScalarMapping)
        {
            if (grade != Grade) 
                return ScalarProcessor.CreateZeroKVector(grade);

            return grade switch
            {
                0 => ScalarProcessor.CreateScalar(indexScalarMapping(0, GetTermScalarByIndex(0))),
                1 => GetVectorPart(indexScalarMapping),
                2 => GetBivectorPart(indexScalarMapping),
                _ => new GasKVector<T>(
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

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            if (Grade != grade)
                return ScalarProcessor.CreateZeroKVector(grade);

            var indexScalarDictionary =
                IndexScalarDictionary
                    .Where(pair => scalarSelection(pair.Value))
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value
                    );

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroKVector(grade)
                : ScalarProcessor.CreateKVector(grade, indexScalarDictionary);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            if (Grade != grade)
                return ScalarProcessor.CreateZeroKVector(grade);

            var indexScalarDictionary =
                IndexScalarDictionary
                    .Where(pair => indexScalarSelection(pair.Key, pair.Value))
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value
                    );

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroKVector(grade)
                : ScalarProcessor.CreateKVector(grade, indexScalarDictionary);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            if (Grade != grade)
                return ScalarProcessor.CreateZeroKVector(grade);

            var indexScalarDictionary =
                IndexScalarDictionary
                    .Where(pair => indexSelection(pair.Key))
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value
                    );

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroKVector(grade)
                : ScalarProcessor.CreateKVector(grade, indexScalarDictionary);
        }
        

        public override IGasMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var indexScalarDictionary = IndexScalarDictionary
                .Where(pair => idSelection(GaBasisUtils.BasisBladeId(Grade, pair.Key)))
                .CopyToDictionary();

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroKVector(Grade)
                : ScalarProcessor.CreateKVector(Grade, indexScalarDictionary); 
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var indexScalarDictionary = IndexScalarDictionary
                .Where(pair => gradeIndexSelection(Grade, pair.Key))
                .CopyToDictionary();

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroKVector(Grade)
                : ScalarProcessor.CreateKVector(Grade, indexScalarDictionary); 
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary = IndexScalarDictionary
                .Where(pair => scalarSelection(pair.Value))
                .CopyToDictionary();

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroKVector(Grade)
                : ScalarProcessor.CreateKVector(Grade, indexScalarDictionary); 
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var indexScalarDictionary = IndexScalarDictionary
                .Where(pair => idScalarSelection(GaBasisUtils.BasisBladeId(Grade, pair.Key), pair.Value))
                .CopyToDictionary();

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroKVector(Grade)
                : ScalarProcessor.CreateKVector(Grade, indexScalarDictionary); 
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var indexScalarDictionary = IndexScalarDictionary
                .Where(pair => gradeIndexScalarSelection(Grade, pair.Key, pair.Value))
                .CopyToDictionary();

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroKVector(Grade)
                : ScalarProcessor.CreateKVector(Grade, indexScalarDictionary); 
        }

        public override Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            if (Grade != 1)
                return new Tuple<IGasVector<T>, IGasVector<T>>(
                    ScalarProcessor.CreateZeroVector(),
                    ScalarProcessor.CreateZeroVector()
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

            return new Tuple<IGasVector<T>, IGasVector<T>>(
                ScalarProcessor.CreateVector(indexScalarDictionary1),
                ScalarProcessor.CreateVector(indexScalarDictionary2)
            );
        }

        public override Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            if (Grade != 1)
                return new Tuple<IGasVector<T>, IGasVector<T>>(
                    ScalarProcessor.CreateZeroVector(),
                    ScalarProcessor.CreateZeroVector()
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

            return new Tuple<IGasVector<T>, IGasVector<T>>(
                ScalarProcessor.CreateVector(indexScalarDictionary1),
                ScalarProcessor.CreateVector(indexScalarDictionary2)
            );
        }

        public override Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<T, bool> scalarSelection)
        {
            if (Grade != 1)
                return new Tuple<IGasVector<T>, IGasVector<T>>(
                    ScalarProcessor.CreateZeroVector(),
                    ScalarProcessor.CreateZeroVector()
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

            return new Tuple<IGasVector<T>, IGasVector<T>>(
                ScalarProcessor.CreateVector(indexScalarDictionary1),
                ScalarProcessor.CreateVector(indexScalarDictionary2)
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
            return IndexScalarDictionary.Keys.Select(index => GaBasisUtils.BasisBladeId(Grade, index));
        }

        public override IEnumerable<IGaBasisBlade> GetBasisBlades()
        {
            return IndexScalarDictionary.Select(pair => 
                (IGaBasisBlade) Grade.CreateGradedBasisBlade(pair.Key)
            );
        }

        public override IEnumerable<KeyValuePair<ulong, T>> GetIdScalarPairs()
        {
            return IndexScalarDictionary
                .Select(pair => new KeyValuePair<ulong, T>(
                    GaBasisUtils.BasisBladeId(Grade, pair.Key),
                    pair.Value
                ));
        }

        public override IEnumerable<Tuple<ulong, T>> GetIdScalarTuples()
        {
            return IndexScalarDictionary
                .Select(pair => new Tuple<ulong, T>(
                    GaBasisUtils.BasisBladeId(Grade, pair.Key),
                    pair.Value
                ));
        }

        public override IReadOnlyDictionary<ulong, T> GetIdScalarDictionary()
        {
            return IndexScalarDictionary.ToDictionary(
                pair => GaBasisUtils.BasisBladeId(Grade, pair.Key),
                pair => pair.Value
            );
        }

        public override IEnumerable<GaTerm<T>> GetTerms()
        {
            return IndexScalarDictionary.Select(pair => 
                GaTerm<T>.CreateGraded(Grade, pair.Key, pair.Value)
            );
        }

        public override IEnumerable<GaTerm<T>> GetNotZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => !ScalarProcessor.IsZero(pair.Value))
                .Select(pair => 
                    GaTerm<T>.CreateGraded(Grade, pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<T>> GetNotNearZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => !ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => 
                    GaTerm<T>.CreateGraded(Grade, pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<T>> GetZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => ScalarProcessor.IsZero(pair.Value))
                .Select(pair => 
                    GaTerm<T>.CreateGraded(Grade, pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<T>> GetNearZeroTerms()
        {
            return IndexScalarDictionary
                .Where(pair => ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => 
                    GaTerm<T>.CreateGraded(Grade, pair.Key, pair.Value)
                );
        }
    }
}
