using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Dictionary;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;
using Open.Collections;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors
{
    /// <summary>
    /// This is not intended to be an efficient implementation, but a correct
    /// reference implementation for validation and performance comparison.
    /// </summary>
    public sealed partial class XGaFloat64UniformMultivector :
        XGaFloat64Multivector
    {
        private readonly IReadOnlyDictionary<IIndexSet, double> _idScalarDictionary;


        public override string MultivectorClassName
            => "Generic Uniform Multivector";

        public override int Count
            => _idScalarDictionary.Count;

        public override IEnumerable<int> KVectorGrades
            => _idScalarDictionary
                .Keys
                .Select(k => k.Count)
                .Distinct();

        public override bool IsZero
            => _idScalarDictionary.Count == 0;

        public override IEnumerable<XGaBasisBlade> BasisBlades
            => _idScalarDictionary.Keys.Select(Metric.CreateBasisBlade);

        public override IEnumerable<IIndexSet> Ids
            => _idScalarDictionary.Keys;

        public override IEnumerable<double> Scalars
            => _idScalarDictionary.Values;

        public override IEnumerable<KeyValuePair<IIndexSet, double>> IdScalarPairs
            => _idScalarDictionary;

        public override IEnumerable<KeyValuePair<XGaBasisBlade, double>> BasisScalarPairs
            => _idScalarDictionary.Select(p =>
                new KeyValuePair<XGaBasisBlade, double>(
                    Metric.CreateBasisBlade(p.Key),
                    p.Value
                )
            );



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64UniformMultivector(XGaFloat64Processor processor)
            : base(processor)
        {
            _idScalarDictionary =
                new EmptyDictionary<IIndexSet, double>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64UniformMultivector(XGaFloat64Processor processor, KeyValuePair<IIndexSet, double> basisScalarPair)
            : base(processor)
        {
            _idScalarDictionary =
                new SingleItemDictionary<IIndexSet, double>(basisScalarPair);

            Debug.Assert(
                _idScalarDictionary.IsValidMultivectorDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64UniformMultivector(XGaFloat64Processor processor, IReadOnlyDictionary<IIndexSet, double> idScalarDictionary)
            : base(processor)
        {
            _idScalarDictionary =
                idScalarDictionary;

            Debug.Assert(
                _idScalarDictionary.IsValidMultivectorDictionary()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _idScalarDictionary.IsValidMultivectorDictionary();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsScalar()
        {
            return IsZero || _idScalarDictionary.Keys.All(id => id.IsEmptySet);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsVector()
        {
            return IsZero || _idScalarDictionary.Keys.All(id => id.Count == 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsBivector()
        {
            return IsZero || _idScalarDictionary.Keys.All(id => id.Count == 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsKVector(int grade)
        {
            return IsZero || _idScalarDictionary.Keys.All(id => id.Count == grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsOdd()
        {
            return !IsZero && _idScalarDictionary.Keys.All(
                id => id.Grade().IsOdd()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsOdd(int maxGrade)
        {
            return !IsZero && _idScalarDictionary.Keys.All(
                id => id.Grade().IsOdd(maxGrade)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEven()
        {
            return !IsZero && _idScalarDictionary.Keys.All(
                id => id.Grade().IsEven()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEven(int maxGrade)
        {
            return !IsZero && _idScalarDictionary.Keys.All(
                id => id.Grade().IsEven(maxGrade)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsScalarPart()
        {
            return _idScalarDictionary.Keys.Any(b => b.Count == 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsVectorPart()
        {
            return _idScalarDictionary.Keys.Any(b => b.Count == 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsBivectorPart()
        {
            return _idScalarDictionary.Keys.Any(b => b.Count == 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKVectorPart(int grade)
        {
            return _idScalarDictionary.Keys.Any(b => b.Count == grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsOddPart()
        {
            return !IsZero && _idScalarDictionary.Keys.Any(
                id => id.Grade().IsOdd()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsOddPart(int maxGrade)
        {
            return !IsZero && _idScalarDictionary.Keys.Any(
                id => id.Grade().IsOdd(maxGrade)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsEvenPart()
        {
            return !IsZero && _idScalarDictionary.Keys.Any(
                id => id.Grade().IsEven()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsEvenPart(int maxGrade)
        {
            return !IsZero && _idScalarDictionary.Keys.Any(
                id => id.Grade().IsEven(maxGrade)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IReadOnlyDictionary<IIndexSet, double> GetIdScalarDictionary()
        {
            return _idScalarDictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetMaxGrade()
        {
            return IsZero ? 0 : _idScalarDictionary.Keys.Max(id => id.Count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(IIndexSet key)
        {
            return !IsZero && _idScalarDictionary.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetKVectorCount()
        {
            return _idScalarDictionary
                .Keys
                .Select(k => k.Count)
                .Distinct()
                .Count();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Simplify()
        {
            return this;
        }


        public override IEnumerable<XGaFloat64KVector> GetKVectorParts()
        {
            if (_idScalarDictionary.Count == 0)
                yield break;

            if (_idScalarDictionary.Count == 1)
            {
                yield return Processor.CreateKVector(
                    _idScalarDictionary.First()
                );

                yield break;
            }

            var gradeGroup =
                _idScalarDictionary.GroupBy(
                    basisScalarPair => basisScalarPair.Key.Count
                );

            foreach (var gradeBasisScalarPairGroups in gradeGroup)
            {
                var grade = gradeBasisScalarPairGroups.Key;

                if (grade == 0)
                {
                    yield return Processor.CreateScalar(
                        gradeBasisScalarPairGroups.First().Value
                    );

                    continue;
                }

                var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

                idScalarDictionary.AddRange(gradeBasisScalarPairGroups);

                yield return Processor.CreateKVector(
                    grade,
                    idScalarDictionary
                );
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetScalarTermScalar()
        {
            return _idScalarDictionary.TryGetValue(EmptyIndexSet.Instance, out var scalar)
                ? scalar
                : 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetTermScalar(IIndexSet basisBladeId)
        {
            return _idScalarDictionary.TryGetValue(basisBladeId, out var scalar)
                ? scalar
                : 0d;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalarTermScalar(out double scalar)
        {
            if (_idScalarDictionary.TryGetValue(EmptyIndexSet.Instance, out scalar))
                return true;

            scalar = 0d;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetTermScalar(IIndexSet basisBlade, out double scalar)
        {
            if (_idScalarDictionary.TryGetValue(basisBlade, out scalar))
                return true;

            scalar = 0d;
            return false;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar GetScalarPart()
        {
            return _idScalarDictionary.TryGetValue(EmptyIndexSet.Instance, out var scalarValue)
                ? Processor.CreateScalar(scalarValue)
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector GetVectorPart()
        {
            return Processor
                .CreateComposer()
                .SetTerms(_idScalarDictionary.Where(p => p.Key.Count == 1))
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector GetBivectorPart()
        {
            return Processor
                .CreateComposer()
                .SetTerms(_idScalarDictionary.Where(p => p.Key.Count == 2))
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64HigherKVector GetHigherKVectorPart(int grade)
        {
            if (grade < 3)
                throw new ArgumentOutOfRangeException(nameof(grade));

            return (XGaFloat64HigherKVector)Processor
                .CreateComposer()
                .SetTerms(_idScalarDictionary.Where(p => p.Key.Count == grade))
                .GetKVector(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64UniformMultivector GetPart(Func<IIndexSet, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarPairs = 
                IdScalarPairs.Where(
                    p => filterFunc(p.Key)
                ).ToDictionary();

            return Processor
                .CreateComposer()
                .SetTerms(idScalarPairs)
                .GetUniformMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64UniformMultivector GetPart(Func<double, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarPairs = 
                IdScalarPairs.Where(
                    p => filterFunc(p.Value)
                ).ToDictionary();

            return Processor
                .CreateComposer()
                .SetTerms(idScalarPairs)
                .GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64UniformMultivector GetPart(Func<IIndexSet, double, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarPairs = 
                IdScalarPairs.Where(
                    p => filterFunc(p.Key, p.Value)
                ).ToDictionary();

            return Processor
                .CreateComposer()
                .SetTerms(idScalarPairs)
                .GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector GetKVectorPart(int grade)
        {
            if (grade < 0)
                throw new ArgumentOutOfRangeException(nameof(grade));

            return grade switch
            {
                0 => GetScalarPart(),
                1 => GetVectorPart(),
                2 => GetBivectorPart(),
                _ => Processor
                    .CreateComposer()
                    .SetTerms(_idScalarDictionary.Where(p => p.Key.Count == grade))
                    .GetKVector(grade)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector GetEvenPart()
        {
            if (IsZero) return this;

            var idScalarDictionary =
                _idScalarDictionary
                    .Where(p => p.Key.Count.IsEven())
                    .ToDictionary(p => p.Key, p => p.Value);

            return Processor.CreateUniformMultivector(
                idScalarDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector GetEvenPart(int maxGrade)
        {
            if (IsZero) return this;

            var idScalarDictionary =
                _idScalarDictionary
                    .Where(p => p.Key.Count.IsEven(maxGrade))
                    .ToDictionary(p => p.Key, p => p.Value);

            return Processor.CreateUniformMultivector(
                idScalarDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector GetOddPart()
        {
            if (IsZero) return this;

            var idScalarDictionary =
                _idScalarDictionary
                    .Where(p => p.Key.Count.IsOdd())
                    .ToDictionary(p => p.Key, p => p.Value);

            return Processor.CreateUniformMultivector(
                idScalarDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector GetOddPart(int maxGrade)
        {
            if (IsZero) return this;

            var idScalarDictionary =
                _idScalarDictionary
                    .Where(p => p.Key.Count.IsOdd(maxGrade))
                    .ToDictionary(p => p.Key, p => p.Value);

            return Processor.CreateUniformMultivector(
                idScalarDictionary
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return BasisScalarPairs
                .OrderBy(p => p.Key)
                .Select(p => $"'{p.Value:G}'{p.Key}")
                .Concatenate(" + ");
        }
    }
}