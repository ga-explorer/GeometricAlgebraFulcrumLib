using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Dictionary;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using Open.Collections;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors
{
    /// <summary>
    /// This is not intended to be an efficient implementation, but a correct
    /// reference implementation for validation and performance comparison.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed partial class XGaUniformMultivector<T> :
        XGaMultivector<T>
    {
        private readonly IReadOnlyDictionary<IIndexSet, T> _idScalarDictionary;


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
            => _idScalarDictionary.Keys.Select(Processor.CreateBasisBlade);

        public override IEnumerable<IIndexSet> Ids
            => _idScalarDictionary.Keys;

        public override IEnumerable<T> Scalars
            => _idScalarDictionary.Values;

        public override IEnumerable<KeyValuePair<IIndexSet, T>> IdScalarPairs
            => _idScalarDictionary;

        public override IEnumerable<KeyValuePair<XGaBasisBlade, T>> BasisScalarPairs
            => _idScalarDictionary.Select(p =>
                new KeyValuePair<XGaBasisBlade, T>(
                    Processor.CreateBasisBlade(p.Key),
                    p.Value
                )
            );



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaUniformMultivector(XGaProcessor<T> processor)
            : base(processor)
        {
            _idScalarDictionary =
                new EmptyDictionary<IIndexSet, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaUniformMultivector(XGaProcessor<T> processor, KeyValuePair<IIndexSet, T> basisScalarPair)
            : base(processor)
        {
            _idScalarDictionary =
                new SingleItemDictionary<IIndexSet, T>(basisScalarPair);

            Debug.Assert(
                _idScalarDictionary.IsValidMultivectorDictionary(Processor)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaUniformMultivector(XGaProcessor<T> processor, IReadOnlyDictionary<IIndexSet, T> idScalarDictionary)
            : base(processor)
        {
            _idScalarDictionary =
                idScalarDictionary;

            Debug.Assert(
                _idScalarDictionary.IsValidMultivectorDictionary(Processor)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _idScalarDictionary.IsValidMultivectorDictionary(Processor);
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
        public IReadOnlyDictionary<IIndexSet, T> GetIdScalarDictionary()
        {
            return _idScalarDictionary;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetMinGrade()
        {
            return IsZero ? 0 : _idScalarDictionary.Keys.Min(id => id.Count);
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
        public override XGaMultivector<T> Simplify()
        {
            return this;
        }


        public override IEnumerable<XGaKVector<T>> GetKVectorParts()
        {
            if (_idScalarDictionary.Count == 0)
                yield break;

            if (_idScalarDictionary.Count == 1)
            {
                yield return Processor.CreateTermKVector(
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

                var idScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

                idScalarDictionary.AddRange(gradeBasisScalarPairGroups);

                yield return Processor.CreateKVector(
                    grade,
                    idScalarDictionary
                );
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> Scalar()
        {
            return _idScalarDictionary.TryGetValue(EmptyIndexSet.Instance, out var scalar)
                ? ScalarProcessor.CreateScalar(scalar)
                : ScalarProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetBasisBladeScalar(IIndexSet basisBladeId)
        {
            return _idScalarDictionary.TryGetValue(basisBladeId, out var scalar)
                ? ScalarProcessor.CreateScalar(scalar)
                : ScalarProcessor.CreateScalarZero();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalarValue(out T scalar)
        {
            if (_idScalarDictionary.TryGetValue(EmptyIndexSet.Instance, out scalar))
                return true;

            scalar = ScalarProcessor.ScalarZero;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetBasisBladeScalarValue(IIndexSet basisBlade, out T scalar)
        {
            if (_idScalarDictionary.TryGetValue(basisBlade, out scalar))
                return true;

            scalar = ScalarProcessor.ScalarZero;
            return false;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> GetScalarPart()
        {
            return _idScalarDictionary.TryGetValue(EmptyIndexSet.Instance, out var scalarValue)
                ? Processor.CreateScalar(scalarValue)
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> GetVectorPart()
        {
            return Processor
                .CreateComposer()
                .SetTerms(_idScalarDictionary.Where(p => p.Key.Count == 1))
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> GetBivectorPart()
        {
            return Processor
                .CreateComposer()
                .SetTerms(_idScalarDictionary.Where(p => p.Key.Count == 2))
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> GetHigherKVectorPart(int grade)
        {
            if (grade < 3)
                throw new ArgumentOutOfRangeException(nameof(grade));

            return (XGaHigherKVector<T>)Processor
                .CreateComposer()
                .SetTerms(_idScalarDictionary.Where(p => p.Key.Count == grade))
                .GetKVector(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivector<T> GetPart(Func<IIndexSet, bool> filterFunc)
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
        public XGaUniformMultivector<T> GetPart(Func<T, bool> filterFunc)
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
        public XGaUniformMultivector<T> GetPart(Func<IIndexSet, T, bool> filterFunc)
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
        public override XGaKVector<T> GetKVectorPart(int grade)
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
        public override XGaKVector<T> GetFirstKVectorPart()
        {
            return GetKVectorPart(GetMinGrade());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> GetEvenPart()
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
        public override XGaMultivector<T> GetEvenPart(int maxGrade)
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
        public override XGaMultivector<T> GetOddPart()
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
        public override XGaMultivector<T> GetOddPart(int maxGrade)
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