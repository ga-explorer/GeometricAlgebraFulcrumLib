using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Dictionary;
using DataStructuresLib.Extensions;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors
{
    public sealed partial class XGaFloat64HigherKVector :
        XGaFloat64KVector
    {
        private readonly IReadOnlyDictionary<IIndexSet, double> _idScalarDictionary;


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

        public override IEnumerable<XGaBasisBlade> BasisBlades
            => _idScalarDictionary.Keys.Select(Metric.CreateBasisBlade);

        public override IEnumerable<IIndexSet> Ids 
            => _idScalarDictionary.Keys;

        public override IEnumerable<double> Scalars
            => _idScalarDictionary.Values;

        public override IEnumerable<KeyValuePair<IIndexSet, double>> IdScalarPairs
            => _idScalarDictionary;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64HigherKVector(XGaFloat64Processor processor, int grade)
            : base(processor)
        {
            if (grade < 3)
                throw new ArgumentOutOfRangeException(nameof(grade));

            _idScalarDictionary = new EmptyDictionary<IIndexSet, double>();

            Grade = grade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64HigherKVector(XGaFloat64Processor processor, KeyValuePair<IIndexSet, double> basisScalarPair)
            : base(processor)
        {
            var grade = basisScalarPair.Key.Count;

            if (grade < 3)
                throw new ArgumentOutOfRangeException(nameof(grade));

            _idScalarDictionary =
                new SingleItemDictionary<IIndexSet, double>(basisScalarPair);

            Grade = grade;

            Debug.Assert(IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64HigherKVector(XGaFloat64Processor processor, int grade, IReadOnlyDictionary<IIndexSet, double> indexScalarDictionary)
            : base(processor)
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
        public override IReadOnlyDictionary<IIndexSet, double> GetIdScalarDictionary()
        {
            return _idScalarDictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(IIndexSet key)
        {
            return !IsZero && _idScalarDictionary.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar GetScalarPart()
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector GetVectorPart()
        {
            return Processor.CreateZeroVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector GetBivectorPart()
        {
            return Processor.CreateZeroBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64HigherKVector GetHigherKVectorPart(int grade)
        {
            return grade == Grade
                ? this
                : Processor.CreateZeroHigherKVector(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64HigherKVector GetPart(Func<IIndexSet, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Key)
                ).ToDictionary();

            return Processor.CreateHigherKVector(Grade, idScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64HigherKVector GetPart(Func<double, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Value)
                ).ToDictionary();

            return Processor.CreateHigherKVector(Grade, idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64HigherKVector GetPart(Func<IIndexSet, double, bool> filterFunc)
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
        public override double GetTermScalar(IIndexSet basisBlade)
        {
            if (basisBlade.Count != Grade)
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
        public override bool TryGetTermScalar(IIndexSet basisBlade, out double scalar)
        {
            if (basisBlade.Count == Grade && _idScalarDictionary.TryGetValue(basisBlade, out scalar))
                return true;

            scalar = 0d;
            return false;
        }


        public override IEnumerable<KeyValuePair<XGaBasisBlade, double>> BasisScalarPairs
        {
            get
            {
                return _idScalarDictionary.Select(p =>
                    new KeyValuePair<XGaBasisBlade, double>(
                        Metric.CreateBasisBlade(p.Key),
                        p.Value
                    )
                );
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Simplify()
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
