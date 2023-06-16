using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Dictionary;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using Open.Collections;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors
{
    /// <summary>
    /// This is not intended to be an efficient implementation, but a correct
    /// reference implementation for validation and performance comparison.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed partial class RGaUniformMultivector<T> :
        RGaMultivector<T>
    {
        private readonly IReadOnlyDictionary<ulong, T> _idScalarDictionary;


        public override string MultivectorClassName
            => "Generic Uniform Multivector";

        public override int Count
            => _idScalarDictionary.Count;

        public override IEnumerable<int> KVectorGrades
            => _idScalarDictionary
                .Keys
                .Select(k => k.Grade())
                .Distinct();

        public override bool IsZero
            => _idScalarDictionary.Count == 0;

        public override IEnumerable<RGaBasisBlade> BasisBlades
            => _idScalarDictionary.Keys.Select(Processor.CreateBasisBlade);

        public override IEnumerable<ulong> Ids
            => _idScalarDictionary.Keys;

        public override IEnumerable<T> Scalars
            => _idScalarDictionary.Values;

        public override IEnumerable<KeyValuePair<ulong, T>> IdScalarPairs
            => _idScalarDictionary;

        public override IEnumerable<KeyValuePair<RGaBasisBlade, T>> BasisScalarPairs
            => _idScalarDictionary.Select(p =>
                new KeyValuePair<RGaBasisBlade, T>(
                    Processor.CreateBasisBlade(p.Key),
                    p.Value
                )
            );



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaUniformMultivector(RGaProcessor<T> processor)
            : base(processor)
        {
            _idScalarDictionary =
                new EmptyDictionary<ulong, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaUniformMultivector(RGaProcessor<T> processor, KeyValuePair<ulong, T> basisScalarPair)
            : base(processor)
        {
            _idScalarDictionary =
                new SingleItemDictionary<ulong, T>(basisScalarPair);

            Debug.Assert(
                _idScalarDictionary.IsValidMultivectorDictionary(ScalarProcessor)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaUniformMultivector(RGaProcessor<T> processor, IReadOnlyDictionary<ulong, T> idScalarDictionary)
            : base(processor)
        {
            _idScalarDictionary =
                idScalarDictionary;

            Debug.Assert(
                _idScalarDictionary.IsValidMultivectorDictionary(ScalarProcessor)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _idScalarDictionary.IsValidMultivectorDictionary(ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsScalar()
        {
            return IsZero || _idScalarDictionary.Keys.All(id => id == 0UL);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsVector()
        {
            return IsZero || _idScalarDictionary.Keys.All(id => id.Grade() == 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsBivector()
        {
            return IsZero || _idScalarDictionary.Keys.All(id => id.Grade() == 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsKVector(int grade)
        {
            return IsZero || _idScalarDictionary.Keys.All(id => id.Grade() == grade);
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
            return _idScalarDictionary.Keys.Any(b => b.Grade() == 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsVectorPart()
        {
            return _idScalarDictionary.Keys.Any(b => b.Grade() == 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsBivectorPart()
        {
            return _idScalarDictionary.Keys.Any(b => b.Grade() == 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKVectorPart(int grade)
        {
            return _idScalarDictionary.Keys.Any(b => b.Grade() == grade);
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
        public override IReadOnlyDictionary<ulong, T> GetIdScalarDictionary()
        {
            return _idScalarDictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetMaxGrade()
        {
            return IsZero ? 0 : _idScalarDictionary.Keys.Max(id => id.Grade());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(ulong key)
        {
            return !IsZero && _idScalarDictionary.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetKVectorCount()
        {
            return _idScalarDictionary
                .Keys
                .Select(k => k.Grade())
                .Distinct()
                .Count();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> Simplify()
        {
            return this;
        }


        public override IEnumerable<RGaKVector<T>> GetKVectorParts()
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
                    basisScalarPair => basisScalarPair.Key.Grade()
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

                var idScalarDictionary = new Dictionary<ulong, T>();

                idScalarDictionary.AddRange(gradeBasisScalarPairGroups);

                yield return Processor.CreateKVector(
                    grade,
                    idScalarDictionary
                );
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetScalarTermScalar()
        {
            return _idScalarDictionary.TryGetValue(0UL, out var scalar)
                ? ScalarProcessor.CreateScalar(scalar)
                : ScalarProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetTermScalar(ulong basisBladeId)
        {
            return _idScalarDictionary.TryGetValue(basisBladeId, out var scalar)
                ? ScalarProcessor.CreateScalar(scalar)
                : ScalarProcessor.CreateScalarZero();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalarTermScalar(out T scalar)
        {
            if (_idScalarDictionary.TryGetValue(0UL, out scalar))
                return true;

            scalar = ScalarProcessor.ScalarZero;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetTermScalar(ulong basisBlade, out T scalar)
        {
            if (_idScalarDictionary.TryGetValue(basisBlade, out scalar))
                return true;

            scalar = ScalarProcessor.ScalarZero;
            return false;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> GetScalarPart()
        {
            return _idScalarDictionary.TryGetValue(0UL, out var scalarValue)
                ? Processor.CreateScalar(scalarValue)
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaVector<T> GetVectorPart()
        {
            return Processor
                .CreateComposer()
                .SetTerms(_idScalarDictionary.Where(p => p.Key.Grade() == 1))
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaBivector<T> GetBivectorPart()
        {
            return Processor
                .CreateComposer()
                .SetTerms(_idScalarDictionary.Where(p => p.Key.Grade() == 2))
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaHigherKVector<T> GetHigherKVectorPart(int grade)
        {
            if (grade < 3)
                throw new ArgumentOutOfRangeException(nameof(grade));

            return (RGaHigherKVector<T>)Processor
                .CreateComposer()
                .SetTerms(_idScalarDictionary.Where(p => p.Key.Grade() == grade))
                .GetKVector(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaKVector<T> GetKVectorPart(int grade)
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
                    .SetTerms(_idScalarDictionary.Where(p => p.Key.Grade() == grade))
                    .GetKVector(grade)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaUniformMultivector<T> GetPart(Func<ulong, bool> filterFunc)
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
        public RGaUniformMultivector<T> GetPart(Func<T, bool> filterFunc)
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
        public RGaUniformMultivector<T> GetPart(Func<ulong, T, bool> filterFunc)
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
        public override RGaMultivector<T> GetEvenPart()
        {
            if (IsZero) return this;

            var idScalarDictionary =
                _idScalarDictionary
                    .Where(p => p.Key.Grade().IsEven())
                    .ToDictionary(p => p.Key, p => p.Value);

            return Processor.CreateUniformMultivector(
                idScalarDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> GetEvenPart(int maxGrade)
        {
            if (IsZero) return this;

            var idScalarDictionary =
                _idScalarDictionary
                    .Where(p => p.Key.Grade().IsEven(maxGrade))
                    .ToDictionary(p => p.Key, p => p.Value);

            return Processor.CreateUniformMultivector(
                idScalarDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> GetOddPart()
        {
            if (IsZero) return this;

            var idScalarDictionary =
                _idScalarDictionary
                    .Where(p => p.Key.Grade().IsOdd())
                    .ToDictionary(p => p.Key, p => p.Value);

            return Processor.CreateUniformMultivector(
                idScalarDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> GetOddPart(int maxGrade)
        {
            if (IsZero) return this;

            var idScalarDictionary =
                _idScalarDictionary
                    .Where(p => p.Key.Grade().IsOdd(maxGrade))
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