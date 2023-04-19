using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Dictionary;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors
{
    public sealed partial class RGaFloat64HigherKVector :
        RGaFloat64KVector
    {
        private readonly IReadOnlyDictionary<ulong, double> _idScalarDictionary;


        public override string MultivectorClassName
            => $"Generic {Grade}-Vector";

        public override int Count
            => _idScalarDictionary.Count;

        public override IEnumerable<int> KVectorGrades
        {
            get
            {
                if (!IsZero) yield return Grade;
            }
        }

        public override int Grade { get; }

        public override bool IsZero
            => _idScalarDictionary.Count == 0;

        public override IEnumerable<RGaBasisBlade> BasisBlades
            => _idScalarDictionary.Keys.Select(Processor.CreateBasisBlade);

        public override IEnumerable<ulong> Ids 
            => _idScalarDictionary.Keys;

        public override IEnumerable<double> Scalars
            => _idScalarDictionary.Values;

        public override IEnumerable<KeyValuePair<ulong, double>> IdScalarPairs
            => _idScalarDictionary;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaFloat64HigherKVector(RGaFloat64Processor metric, int grade)
            : base(metric)
        {
            if (grade < 3)
                throw new ArgumentOutOfRangeException(nameof(grade));

            _idScalarDictionary = new EmptyDictionary<ulong, double>();

            Grade = grade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaFloat64HigherKVector(RGaFloat64Processor metric, KeyValuePair<ulong, double> basisScalarPair)
            : base(metric)
        {
            var grade = basisScalarPair.Key.Grade();

            if (grade < 3)
                throw new ArgumentOutOfRangeException(nameof(grade));

            _idScalarDictionary =
                new SingleItemDictionary<ulong, double>(basisScalarPair);

            Grade = grade;

            Debug.Assert(IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaFloat64HigherKVector(RGaFloat64Processor metric, int grade, IReadOnlyDictionary<ulong, double> indexScalarDictionary)
            : base(metric)
        {
            if (grade < 3)
                throw new ArgumentOutOfRangeException(nameof(grade));

            _idScalarDictionary = indexScalarDictionary;

            Grade = grade;

            Debug.Assert(IsValid());
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _idScalarDictionary.IsValidKVectorDictionary(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IReadOnlyDictionary<ulong, double> GetIdScalarDictionary()
        {
            return _idScalarDictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(ulong key)
        {
            return !IsZero && _idScalarDictionary.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar GetScalarPart()
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Vector GetVectorPart()
        {
            return Processor.CreateZeroVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Bivector GetBivectorPart()
        {
            return Processor.CreateZeroBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64HigherKVector GetHigherKVectorPart(int grade)
        {
            return grade == Grade
                ? this
                : Processor.CreateZeroHigherKVector(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64HigherKVector GetPart(Func<ulong, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Key)
                ).ToDictionary();

            return Processor.CreateHigherKVector(Grade, idScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64HigherKVector GetPart(Func<double, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Value)
                ).ToDictionary();

            return Processor.CreateHigherKVector(Grade, idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64HigherKVector GetPart(Func<ulong, double, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Key, p.Value)
                ).ToDictionary();

            return Processor.CreateHigherKVector(Grade, idScalarDictionary);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetScalarTermScalar()
        {
            return 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetTermScalar(ulong basisBlade)
        {
            if (basisBlade.Grade() != Grade)
                throw new IndexOutOfRangeException(nameof(basisBlade));

            return _idScalarDictionary.TryGetValue(basisBlade, out var scalar)
                ? scalar
                : 0d;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalarTermScalar(out double scalar)
        {
            scalar = 0d;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetTermScalar(ulong basisBlade, out double scalar)
        {
            if (basisBlade.Grade() == Grade && _idScalarDictionary.TryGetValue(basisBlade, out scalar))
                return true;

            scalar = 0d;
            return false;
        }


        public override IEnumerable<KeyValuePair<RGaBasisBlade, double>> BasisScalarPairs
        {
            get
            {
                return _idScalarDictionary.Select(p =>
                    new KeyValuePair<RGaBasisBlade, double>(
                        Processor.CreateBasisBlade(p.Key),
                        p.Value
                    )
                );
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector Simplify()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return IdScalarPairs
                .OrderBy(p => p.Key)
                .Select(p => $"'{p.Value:G}'{p.Key}")
                .Concatenate(" + ");
        }
    }
}
