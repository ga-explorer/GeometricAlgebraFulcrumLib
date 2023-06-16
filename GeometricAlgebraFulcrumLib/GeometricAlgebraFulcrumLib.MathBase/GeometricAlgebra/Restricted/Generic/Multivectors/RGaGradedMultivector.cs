using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Dictionary;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors
{
    public sealed partial class RGaGradedMultivector<T> :
        RGaMultivector<T>
    {
        private readonly IReadOnlyDictionary<int, RGaKVector<T>> _gradeKVectorDictionary;


        public override string MultivectorClassName
            => "Generic Graded Multivector";

        public override int Count
            => _gradeKVectorDictionary.Count == 0
                ? 0
                : _gradeKVectorDictionary.Values.Sum(kv => kv.Count);

        public override IEnumerable<int> KVectorGrades
            => _gradeKVectorDictionary.Keys;

        public int KVectorCount
            => _gradeKVectorDictionary.Count;

        public override bool IsZero
            => _gradeKVectorDictionary.Count == 0;

        public IEnumerable<RGaKVector<T>> KVectors
            => _gradeKVectorDictionary.Values;

        public override IEnumerable<KeyValuePair<ulong, T>> IdScalarPairs
            => _gradeKVectorDictionary.Values.SelectMany(
                kv => kv.IdScalarPairs
            );

        public override IEnumerable<KeyValuePair<RGaBasisBlade, T>> BasisScalarPairs
            => _gradeKVectorDictionary.Values.SelectMany(
                kv => kv.BasisScalarPairs
            );


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaGradedMultivector(RGaProcessor<T> processor)
            : base(processor)
        {
            _gradeKVectorDictionary = new EmptyDictionary<int, RGaKVector<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaGradedMultivector(RGaProcessor<T> processor, KeyValuePair<int, RGaKVector<T>> gradeKVectorPair)
            : base(processor)
        {
            _gradeKVectorDictionary =
                new SingleItemDictionary<int, RGaKVector<T>>(gradeKVectorPair);

            Debug.Assert(
                Processor.IsValidMultivectorDictionary(_gradeKVectorDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaGradedMultivector(RGaProcessor<T> processor, IReadOnlyDictionary<int, RGaKVector<T>> kVectorDictionary)
            : base(processor)
        {
            _gradeKVectorDictionary = kVectorDictionary;

            Debug.Assert(
                Processor.IsValidMultivectorDictionary(_gradeKVectorDictionary)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsScalar()
        {
            return IsZero || _gradeKVectorDictionary.Keys.All(g => g == 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsVector()
        {
            return IsZero || _gradeKVectorDictionary.Keys.All(g => g == 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsBivector()
        {
            return IsZero || _gradeKVectorDictionary.Keys.All(g => g == 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsKVector(int grade)
        {
            return IsZero || _gradeKVectorDictionary.Keys.All(g => g == grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsOdd()
        {
            return !IsZero && _gradeKVectorDictionary.Keys.All(k => k.IsOdd());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsOdd(int maxGrade)
        {
            return !IsZero && _gradeKVectorDictionary.Keys.All(k => k.IsOdd(maxGrade));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEven()
        {
            return !IsZero && _gradeKVectorDictionary.Keys.All(k => k.IsEven());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEven(int maxGrade)
        {
            return !IsZero && _gradeKVectorDictionary.Keys.All(k => k.IsEven(maxGrade));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IReadOnlyDictionary<ulong, T> GetIdScalarDictionary()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetMaxGrade()
        {
            return IsZero ? 0 : _gradeKVectorDictionary.Keys.Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(ulong id)
        {
            if (IsZero) return false;

            var grade = id.Grade();

            return _gradeKVectorDictionary.TryGetValue(grade, out var kVector) &&
                   kVector.ContainsKey(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsScalarPart()
        {
            return _gradeKVectorDictionary.ContainsKey(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsVectorPart()
        {
            return _gradeKVectorDictionary.ContainsKey(1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsBivectorPart()
        {
            return _gradeKVectorDictionary.ContainsKey(2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKVectorPart(int grade)
        {
            return _gradeKVectorDictionary.ContainsKey(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsOddPart()
        {
            return !IsZero && _gradeKVectorDictionary.Keys.Any(k => k.IsOdd());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsOddPart(int maxGrade)
        {
            return !IsZero && _gradeKVectorDictionary.Keys.Any(k => k.IsOdd(maxGrade));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsEvenPart()
        {
            return !IsZero && _gradeKVectorDictionary.Keys.Any(k => k.IsEven());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsEvenPart(int maxGrade)
        {
            return !IsZero && _gradeKVectorDictionary.Keys.Any(k => k.IsEven(maxGrade));
        }

        public override int GetKVectorCount()
        {
            return _gradeKVectorDictionary.Count;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> Simplify()
        {
            return KVectorCount switch
            {
                0 => Processor.CreateZeroScalar(),
                1 => _gradeKVectorDictionary.Values.First().Simplify(),
                _ => this
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return Processor.IsValidMultivectorDictionary(_gradeKVectorDictionary);
        }
        
        public override IEnumerable<RGaKVector<T>> GetKVectorParts()
        {
            return _gradeKVectorDictionary.Values;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetScalarTermScalar()
        {
            if (_gradeKVectorDictionary.TryGetValue(0, out var kVector))
                return ((RGaScalar<T>)kVector).ScalarValue.CreateScalar(ScalarProcessor);

            return ScalarProcessor.CreateScalarZero();
        }

        public override Scalar<T> GetTermScalar(ulong basisBladeId)
        {
            var grade = basisBladeId.Grade();

            if (grade == 0)
                return GetScalarTermScalar();

            return _gradeKVectorDictionary.TryGetValue(grade, out var kVector)
                ? kVector.GetTermScalar(basisBladeId)
                : ScalarProcessor.CreateScalarZero();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalarTermScalar(out T scalar)
        {
            if (_gradeKVectorDictionary.TryGetValue(0, out var kVector))
                return kVector.TryGetScalarTermScalar(out scalar);

            scalar = ScalarProcessor.ScalarZero;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetTermScalar(ulong basisBlade, out T scalar)
        {
            if (_gradeKVectorDictionary.TryGetValue(basisBlade.Grade(), out var kVector))
                return kVector.TryGetTermScalar(basisBlade, out scalar);

            scalar = ScalarProcessor.ScalarZero;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetKVector(int grade, out RGaKVector<T> kVector)
        {
            return _gradeKVectorDictionary.TryGetValue(grade, out kVector);
        }


        public override IEnumerable<RGaBasisBlade> BasisBlades
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _gradeKVectorDictionary
                    .Values
                    .SelectMany(kv => kv.BasisBlades);
            }
        }

        public override IEnumerable<ulong> Ids
            => _gradeKVectorDictionary.Values.SelectMany(kv => kv.Ids);

        public override IEnumerable<T> Scalars
            => _gradeKVectorDictionary
                .Values
                .SelectMany(kv => kv.Scalars);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> GetScalarPart()
        {
            if (_gradeKVectorDictionary.TryGetValue(0, out var kVector))
                return (RGaScalar<T>)kVector;

            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaVector<T> GetVectorPart()
        {
            if (_gradeKVectorDictionary.TryGetValue(1, out var kVector))
                return (RGaVector<T>)kVector;

            return Processor.CreateZeroVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaBivector<T> GetBivectorPart()
        {
            if (_gradeKVectorDictionary.TryGetValue(2, out var kVector))
                return (RGaBivector<T>)kVector;

            return Processor.CreateZeroBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaHigherKVector<T> GetHigherKVectorPart(int grade)
        {
            if (grade < 3)
                throw new ArgumentOutOfRangeException(nameof(grade));

            return _gradeKVectorDictionary.TryGetValue(grade, out var kVector)
                ? (RGaHigherKVector<T>)kVector
                : Processor.CreateZeroHigherKVector(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaKVector<T> GetKVectorPart(int grade)
        {
            if (grade < 0)
                throw new ArgumentOutOfRangeException(nameof(grade));

            return _gradeKVectorDictionary.TryGetValue(grade, out var kVector)
                ? kVector
                : Processor.CreateZeroKVector(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaGradedMultivector<T> GetPart(Func<ulong, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarPairs = 
                IdScalarPairs.Where(
                    p => filterFunc(p.Key)
                ).ToDictionary();

            return Processor
                .CreateComposer()
                .SetTerms(idScalarPairs)
                .GetMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaGradedMultivector<T> GetPart(Func<T, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarPairs = 
                IdScalarPairs.Where(
                    p => filterFunc(p.Value)
                ).ToDictionary();

            return Processor
                .CreateComposer()
                .SetTerms(idScalarPairs)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaGradedMultivector<T> GetPart(Func<ulong, T, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarPairs = 
                IdScalarPairs.Where(
                    p => filterFunc(p.Key, p.Value)
                ).ToDictionary();

            return Processor
                .CreateComposer()
                .SetTerms(idScalarPairs)
                .GetMultivector();
        }

        public override RGaMultivector<T> GetEvenPart()
        {
            if (IsZero)
                return Processor.CreateZeroScalar();

            var composer = Processor.CreateComposer();

            if (_gradeKVectorDictionary.TryGetValue(0, out var scalarPart))
                composer.SetScalar((RGaScalar<T>)scalarPart);

            if (_gradeKVectorDictionary.TryGetValue(2, out var bivectorPart))
                composer.SetMultivector(bivectorPart);

            var kVectors =
                _gradeKVectorDictionary.Values.Where(
                    kv => kv.Grade > 3 && kv.Grade.IsEven()
                );

            foreach (var kVector in kVectors)
                composer.SetMultivector(kVector);

            return composer.GetSimpleMultivector();
        }

        public override RGaMultivector<T> GetEvenPart(int maxGrade)
        {
            if (maxGrade < 0 || IsZero)
                return Processor.CreateZeroScalar();

            var composer = Processor.CreateComposer();

            if (_gradeKVectorDictionary.TryGetValue(0, out var scalarPart))
                composer.SetScalar((RGaScalar<T>)scalarPart);

            if (maxGrade < 2)
                return composer.GetSimpleMultivector();

            if (_gradeKVectorDictionary.TryGetValue(2, out var bivectorPart))
                composer.SetMultivector(bivectorPart);

            var kVectors =
                _gradeKVectorDictionary.Values.Where(
                    kv => kv.Grade > 3 && kv.Grade.IsEven(maxGrade)
                );

            foreach (var kVector in kVectors)
                composer.SetMultivector(kVector);

            return composer.GetSimpleMultivector();
        }

        public override RGaMultivector<T> GetOddPart()
        {
            if (IsZero)
                return Processor.CreateZeroScalar();

            var composer = Processor.CreateComposer();

            if (_gradeKVectorDictionary.TryGetValue(1, out var vectorPart))
                composer.SetMultivector(vectorPart);

            var kVectors =
                _gradeKVectorDictionary.Values.Where(
                    kv => kv.Grade > 2 && kv.Grade.IsOdd()
                );

            foreach (var kVector in kVectors)
                composer.SetMultivector(kVector);

            return composer.GetSimpleMultivector();
        }

        public override RGaMultivector<T> GetOddPart(int maxGrade)
        {
            if (maxGrade < 1 || IsZero)
                return Processor.CreateZeroScalar();

            var composer = Processor.CreateComposer();

            if (_gradeKVectorDictionary.TryGetValue(1, out var vectorPart))
                composer.SetMultivector(vectorPart);

            var kVectors =
                _gradeKVectorDictionary.Values.Where(
                    kv => kv.Grade > 2 && kv.Grade.IsOdd(maxGrade)
                );

            foreach (var kVector in kVectors)
                composer.SetMultivector(kVector);

            return composer.GetSimpleMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return KVectors
                .OrderBy(p => p.Grade)
                .SelectMany(kVector =>
                    kVector
                        .BasisScalarPairs
                        .OrderBy(p => p.Key.Id)
                        .Select(p => $"'{p.Value:G}'{p.Key}")
                )
                .Concatenate(" + ");
        }
    }
}