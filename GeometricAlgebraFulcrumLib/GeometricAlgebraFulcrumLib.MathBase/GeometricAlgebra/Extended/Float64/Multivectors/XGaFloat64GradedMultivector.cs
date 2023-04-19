using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Dictionary;
using DataStructuresLib.Extensions;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors
{
    public sealed partial class XGaFloat64GradedMultivector :
        XGaFloat64Multivector
    {
        private readonly IReadOnlyDictionary<int, XGaFloat64KVector> _gradeKVectorDictionary;


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

        public IEnumerable<XGaFloat64KVector> KVectors
            => _gradeKVectorDictionary.Values;

        public override IEnumerable<KeyValuePair<IIndexSet, double>> IdScalarPairs
            => _gradeKVectorDictionary.Values.SelectMany(
                kv => kv.IdScalarPairs
            );

        public override IEnumerable<KeyValuePair<XGaBasisBlade, double>> BasisScalarPairs
            => _gradeKVectorDictionary.Values.SelectMany(
                kv => kv.BasisScalarPairs
            );


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64GradedMultivector(XGaFloat64Processor processor)
            : base(processor)
        {
            _gradeKVectorDictionary = new EmptyDictionary<int, XGaFloat64KVector>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64GradedMultivector(XGaFloat64Processor processor, KeyValuePair<int, XGaFloat64KVector> gradeKVectorPair)
            : base(processor)
        {
            _gradeKVectorDictionary =
                new SingleItemDictionary<int, XGaFloat64KVector>(gradeKVectorPair);

            Debug.Assert(
                Metric.IsValidMultivectorDictionary(_gradeKVectorDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64GradedMultivector(XGaFloat64Processor processor, IReadOnlyDictionary<int, XGaFloat64KVector> kVectorDictionary)
            : base(processor)
        {
            _gradeKVectorDictionary = kVectorDictionary;

            Debug.Assert(
                Metric.IsValidMultivectorDictionary(_gradeKVectorDictionary)
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
        public override IReadOnlyDictionary<IIndexSet, double> GetIdScalarDictionary()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetMaxGrade()
        {
            return IsZero ? 0 : _gradeKVectorDictionary.Keys.Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(IIndexSet id)
        {
            if (IsZero) return false;

            var grade = id.Count;

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
        public override XGaFloat64Multivector Simplify()
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

        public XGaFloat64Multivector MapKVectors(Func<XGaFloat64KVector, XGaFloat64KVector> kVectorMapping, bool simplify = true)
        {
            var kVectorDictionary = new Dictionary<int, XGaFloat64KVector>();

            foreach (var (grade, kVector) in _gradeKVectorDictionary)
            {
                var kVector1 = kVectorMapping(kVector);

                if (!kVector1.IsZero)
                    kVectorDictionary.Add(grade, kVector1);
            }

            if (kVectorDictionary.Count == 0)
                return simplify
                    ? Processor.CreateZeroScalar()
                    : new XGaFloat64GradedMultivector(
                        Processor,
                        new EmptyDictionary<int, XGaFloat64KVector>()
                    );

            if (kVectorDictionary.Count == 1)
                return new XGaFloat64GradedMultivector(
                    Processor,
                    new SingleItemDictionary<int, XGaFloat64KVector>(kVectorDictionary.First())
                );

            var mv = new XGaFloat64GradedMultivector(
                Processor, 
                kVectorDictionary
            );

            return simplify ? mv.Simplify() : mv;
        }

        public XGaFloat64Multivector MapKVectors(Func<int, XGaFloat64KVector, XGaFloat64KVector> kVectorMapping, bool simplify = true)
        {
            var kVectorDictionary = new Dictionary<int, XGaFloat64KVector>();

            foreach (var (grade, kVector) in _gradeKVectorDictionary)
            {
                var kVector1 = kVectorMapping(grade, kVector);

                if (!kVector1.IsZero)
                    kVectorDictionary.Add(grade, kVector1);
            }

            if (kVectorDictionary.Count == 0)
                return simplify
                    ? Processor.CreateZeroScalar()
                    : new XGaFloat64GradedMultivector(
                        Processor,
                        new EmptyDictionary<int, XGaFloat64KVector>()
                    );

            if (kVectorDictionary.Count == 1)
                return new XGaFloat64GradedMultivector(
                    Processor,
                    new SingleItemDictionary<int, XGaFloat64KVector>(kVectorDictionary.First())
                );

            var mv = new XGaFloat64GradedMultivector(
                Processor, 
                kVectorDictionary
            );

            return simplify ? mv.Simplify() : mv;
        }


        public override IEnumerable<XGaFloat64KVector> GetKVectorParts()
        {
            return _gradeKVectorDictionary.Values;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetScalarTermScalar()
        {
            if (_gradeKVectorDictionary.TryGetValue(0, out var kVector))
                return ((XGaFloat64Scalar)kVector).ScalarValue;

            return 0d;
        }

        public override double GetTermScalar(IIndexSet basisBladeId)
        {
            var grade = basisBladeId.Count;

            if (grade == 0)
                return GetScalarTermScalar();

            return _gradeKVectorDictionary.TryGetValue(grade, out var kVector)
                ? kVector.GetTermScalar(basisBladeId)
                : 0d;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalarTermScalar(out double scalar)
        {
            if (_gradeKVectorDictionary.TryGetValue(0, out var kVector))
                return kVector.TryGetScalarTermScalar(out scalar);

            scalar = 0d;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetTermScalar(IIndexSet basisBlade, out double scalar)
        {
            if (_gradeKVectorDictionary.TryGetValue(basisBlade.Count, out var kVector))
                return kVector.TryGetTermScalar(basisBlade, out scalar);

            scalar = 0d;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetKVector(int grade, out XGaFloat64KVector kVector)
        {
            return _gradeKVectorDictionary.TryGetValue(grade, out kVector);
        }


        public override IEnumerable<XGaBasisBlade> BasisBlades
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _gradeKVectorDictionary
                    .Values
                    .SelectMany(kv => kv.BasisBlades);
            }
        }

        public override IEnumerable<IIndexSet> Ids
            => _gradeKVectorDictionary.Values.SelectMany(kv => kv.Ids);

        public override IEnumerable<double> Scalars
            => _gradeKVectorDictionary
                .Values
                .SelectMany(kv => kv.Scalars);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar GetScalarPart()
        {
            if (_gradeKVectorDictionary.TryGetValue(0, out var kVector))
                return (XGaFloat64Scalar)kVector;

            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector GetVectorPart()
        {
            if (_gradeKVectorDictionary.TryGetValue(1, out var kVector))
                return (XGaFloat64Vector)kVector;

            return Processor.CreateZeroVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector GetBivectorPart()
        {
            if (_gradeKVectorDictionary.TryGetValue(2, out var kVector))
                return (XGaFloat64Bivector)kVector;

            return Processor.CreateZeroBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64HigherKVector GetHigherKVectorPart(int grade)
        {
            if (grade < 3)
                throw new ArgumentOutOfRangeException(nameof(grade));

            return _gradeKVectorDictionary.TryGetValue(grade, out var kVector)
                ? (XGaFloat64HigherKVector)kVector
                : Processor.CreateZeroHigherKVector(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector GetKVectorPart(int grade)
        {
            if (grade < 0)
                throw new ArgumentOutOfRangeException(nameof(grade));

            return _gradeKVectorDictionary.TryGetValue(grade, out var kVector)
                ? kVector
                : Processor.CreateZeroKVector(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivector GetPart(Func<IIndexSet, bool> filterFunc)
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
        public XGaFloat64GradedMultivector GetPart(Func<double, bool> filterFunc)
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
        public XGaFloat64GradedMultivector GetPart(Func<IIndexSet, double, bool> filterFunc)
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

        public override XGaFloat64Multivector GetEvenPart()
        {
            if (IsZero)
                return Processor.CreateZeroScalar();

            var composer = Processor.CreateComposer();

            if (_gradeKVectorDictionary.TryGetValue(0, out var scalarPart))
                composer.SetScalar((XGaFloat64Scalar)scalarPart);

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

        public override XGaFloat64Multivector GetEvenPart(int maxGrade)
        {
            if (maxGrade < 0 || IsZero)
                return Processor.CreateZeroScalar();

            var composer = Processor.CreateComposer();

            if (_gradeKVectorDictionary.TryGetValue(0, out var scalarPart))
                composer.SetScalar((XGaFloat64Scalar)scalarPart);

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

        public override XGaFloat64Multivector GetOddPart()
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

        public override XGaFloat64Multivector GetOddPart(int maxGrade)
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