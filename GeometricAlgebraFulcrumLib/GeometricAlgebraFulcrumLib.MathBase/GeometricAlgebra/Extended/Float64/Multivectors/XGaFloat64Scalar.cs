using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors
{
    public sealed partial class XGaFloat64Scalar :
        XGaFloat64KVector
    {
        public override string MultivectorClassName
            => "Generic Scalar";

        public double ScalarValue
            => Scalar;

        public double Scalar { get; }

        public override int Count
            => IsZero ? 0 : 1;

        public override IEnumerable<int> KVectorGrades
        {
            get
            {
                if (!IsZero) yield return 0;
            }
        }

        public override int Grade
            => 0;

        public override bool IsZero { get; }

        public bool IsOne
            => ScalarValue.IsOne();
    
        public bool IsMinusOne
            => ScalarValue.IsMinusOne();

        public override IEnumerable<XGaBasisBlade> BasisBlades
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (!IsZero) yield return Processor.BasisScalar;
            }
        }

        public override IEnumerable<IIndexSet> Ids
        {
            get
            {
                if (!IsZero) yield return EmptyIndexSet.Instance;
            }
        }

        public override IEnumerable<double> Scalars
        {
            get
            {
                if (!IsZero) yield return ScalarValue;
            }
        }

        public override IEnumerable<KeyValuePair<XGaBasisBlade, double>> BasisScalarPairs
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (!IsZero)
                    yield return new KeyValuePair<XGaBasisBlade, double>(
                        Metric.BasisScalar,
                        ScalarValue
                    );
            }
        }

        public override IEnumerable<KeyValuePair<IIndexSet, double>> IdScalarPairs
        {
            get
            {
                if (!IsZero)
                    yield return new KeyValuePair<IIndexSet, double>(
                        EmptyIndexSet.Instance,
                        ScalarValue
                    );
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64Scalar(XGaFloat64Processor metric, double scalar)
            : base(metric)
        {
            Debug.Assert(
                scalar.IsValid()
            );

            Scalar = scalar;
            IsZero = scalar.IsZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64Scalar(XGaFloat64Processor metric)
            : base(metric)
        {
            Scalar = 0d;
            IsZero = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64Scalar(XGaFloat64Processor metric, IReadOnlyDictionary<IIndexSet, double> idScalarDictionary)
            : base(metric)
        {
            Scalar = idScalarDictionary.TryGetValue(EmptyIndexSet.Instance, out var scalar)
                ? scalar : 0d;

            Debug.Assert(
                Scalar.IsValid()
            );

            IsZero = Scalar.IsZero();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return ScalarValue.IsValid();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IReadOnlyDictionary<IIndexSet, double> GetIdScalarDictionary()
        {
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(IIndexSet key)
        {
            return key.IsEmptySet && !IsZero;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar GetScalarPart()
        {
            return this;
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
            return Processor.CreateZeroHigherKVector(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar GetPart(Func<IIndexSet, bool> filterFunc)
        {
            return IsZero || filterFunc(EmptyIndexSet.Instance) 
                ? this 
                : Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar GetPart(Func<double, bool> filterFunc)
        {
            return IsZero || filterFunc(ScalarValue) 
                ? this 
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar GetPart(Func<IIndexSet, double, bool> filterFunc)
        {
            return IsZero || filterFunc(EmptyIndexSet.Instance, ScalarValue) 
                ? this 
                : Processor.CreateZeroScalar();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetScalarTermScalar()
        {
            return Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetTermScalar(IIndexSet basisBladeId)
        {
            return basisBladeId.IsEmptySet
                ? Scalar
                : 0d;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalarTermScalar(out double scalar)
        {
            if (!IsZero)
            {
                scalar = ScalarValue;
                return true;
            }

            scalar = 0d;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetTermScalar(IIndexSet basisBlade, out double scalar)
        {
            if (basisBlade.IsEmptySet)
            {
                scalar = ScalarValue;
                return true;
            }

            scalar = 0d;
            return false;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Simplify()
        {
            return this;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool Equals(XGaFloat64Scalar other)
        {
            return Equals(Scalar, other.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is XGaFloat64Scalar other && Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return Scalar.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return IsZero ? string.Empty : $"'{ScalarValue:G}'<>";
        }
    }
}