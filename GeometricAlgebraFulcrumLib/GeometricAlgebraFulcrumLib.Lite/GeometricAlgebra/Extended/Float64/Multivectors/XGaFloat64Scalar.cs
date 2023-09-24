using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Dictionary;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors
{
    public sealed partial class XGaFloat64Scalar :
        XGaFloat64KVector
    {
        private readonly double _scalar;

        public override string MultivectorClassName
            => "Generic Scalar";

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
            => ScalarValue().IsOne();
    
        public bool IsMinusOne
            => ScalarValue().IsMinusOne();

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
                if (!IsZero) yield return ScalarValue();
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
                        ScalarValue()
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
                        ScalarValue()
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

            _scalar = scalar;
            IsZero = scalar.IsZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64Scalar(XGaFloat64Processor metric)
            : base(metric)
        {
            _scalar = 0d;
            IsZero = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64Scalar(XGaFloat64Processor metric, IReadOnlyDictionary<IIndexSet, double> idScalarDictionary)
            : base(metric)
        {
            _scalar = idScalarDictionary.TryGetValue(EmptyIndexSet.Instance, out var scalar)
                ? scalar : 0d;

            Debug.Assert(
                _scalar.IsValid()
            );

            IsZero = _scalar.IsZero();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return ScalarValue().IsValid();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IReadOnlyDictionary<IIndexSet, double> GetIdScalarDictionary()
        {
            return IsZero
                ? new EmptyDictionary<IIndexSet, double>()
                : new SingleItemDictionary<IIndexSet, double>(EmptyIndexSet.Instance, _scalar);
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
            return IsZero || filterFunc(ScalarValue()) 
                ? this 
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar GetPart(Func<IIndexSet, double, bool> filterFunc)
        {
            return IsZero || filterFunc(EmptyIndexSet.Instance, ScalarValue()) 
                ? this 
                : Processor.CreateZeroScalar();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ScalarValue()
        {
            return _scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Scalar()
        {
            return _scalar;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetBasisBladeScalar(IIndexSet basisBladeId)
        {
            return basisBladeId.IsEmptySet
                ? _scalar
                : 0d;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalarValue(out double scalar)
        {
            if (!IsZero)
            {
                scalar = ScalarValue();
                return true;
            }

            scalar = 0d;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetBasisBladeScalarValue(IIndexSet basisBlade, out double scalar)
        {
            if (basisBlade.IsEmptySet)
            {
                scalar = ScalarValue();
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
            return Equals(_scalar, other._scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is XGaFloat64Scalar other && Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return _scalar.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return IsZero ? string.Empty : $"'{ScalarValue():G}'<>";
        }
    }
}