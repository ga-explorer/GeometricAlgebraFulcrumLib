using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors
{
    public sealed partial class RGaFloat64Scalar :
        RGaFloat64KVector
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

        public override IEnumerable<RGaBasisBlade> BasisBlades
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (!IsZero) yield return Processor.BasisScalar;
            }
        }

        public override IEnumerable<ulong> Ids
        {
            get
            {
                if (!IsZero) yield return 0UL;
            }
        }

        public override IEnumerable<double> Scalars
        {
            get
            {
                if (!IsZero) yield return ScalarValue;
            }
        }

        public override IEnumerable<KeyValuePair<RGaBasisBlade, double>> BasisScalarPairs
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (!IsZero)
                    yield return new KeyValuePair<RGaBasisBlade, double>(
                        Processor.BasisScalar,
                        ScalarValue
                    );
            }
        }

        public override IEnumerable<KeyValuePair<ulong, double>> IdScalarPairs
        {
            get
            {
                if (!IsZero)
                    yield return new KeyValuePair<ulong, double>(
                        0UL,
                        ScalarValue
                    );
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaFloat64Scalar(RGaFloat64Processor metric, double scalar)
            : base(metric)
        {
            Debug.Assert(
                scalar.IsValid()
            );

            Scalar = scalar;
            IsZero = scalar.IsZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaFloat64Scalar(RGaFloat64Processor metric)
            : base(metric)
        {
            Scalar = 0d;
            IsZero = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaFloat64Scalar(RGaFloat64Processor metric, IReadOnlyDictionary<ulong, double> idScalarDictionary)
            : base(metric)
        {
            Scalar = idScalarDictionary.TryGetValue(0UL, out var scalar)
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
        public override IReadOnlyDictionary<ulong, double> GetIdScalarDictionary()
        {
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(ulong key)
        {
            return key == 0UL && !IsZero;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar GetScalarPart()
        {
            return this;
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
            return Processor.CreateZeroHigherKVector(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar GetPart(Func<ulong, bool> filterFunc)
        {
            return IsZero || filterFunc(0) 
                ? this 
                : Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar GetPart(Func<double, bool> filterFunc)
        {
            return IsZero || filterFunc(ScalarValue) 
                ? this 
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar GetPart(Func<ulong, double, bool> filterFunc)
        {
            return IsZero || filterFunc(0, ScalarValue) 
                ? this 
                : Processor.CreateZeroScalar();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetScalarTermScalar()
        {
            return Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetTermScalar(ulong basisBladeId)
        {
            return basisBladeId == 0UL
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
        public override bool TryGetTermScalar(ulong basisBlade, out double scalar)
        {
            if (basisBlade == 0UL)
            {
                scalar = ScalarValue;
                return true;
            }

            scalar = 0d;
            return false;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector Simplify()
        {
            return this;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool Equals(RGaFloat64Scalar other)
        {
            return Equals(Scalar, other.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is RGaFloat64Scalar other && Equals(other);
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