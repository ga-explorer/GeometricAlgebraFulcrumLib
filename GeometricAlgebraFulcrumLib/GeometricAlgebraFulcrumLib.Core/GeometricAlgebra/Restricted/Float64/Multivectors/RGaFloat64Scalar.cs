using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.Dictionary;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;

public sealed partial class RGaFloat64Scalar :
    RGaFloat64KVector,
    IFloat64Scalar
{
    private readonly double _scalar;
    
    public double ScalarValue 
        => _scalar;

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

        _scalar = scalar;
        IsZero = scalar.IsZero();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaFloat64Scalar(RGaFloat64Processor metric)
        : base(metric)
    {
        _scalar = 0d;
        IsZero = true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaFloat64Scalar(RGaFloat64Processor metric, IReadOnlyDictionary<ulong, double> idScalarDictionary)
        : base(metric)
    {
        _scalar = idScalarDictionary.TryGetValue(0UL, out var scalar)
            ? scalar : 0d;

        Debug.Assert(
            _scalar.IsValid()
        );

        IsZero = _scalar.IsZero();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return ScalarValue.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ToScalar()
    {
        return Float64Scalar.Create(_scalar);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IReadOnlyDictionary<ulong, double> GetIdScalarDictionary()
    {
        return IsZero
            ? new EmptyDictionary<ulong, double>()
            : new SingleItemDictionary<ulong, double>(0, _scalar);
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
        return Processor.VectorZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector GetVectorPart(Func<int, bool> filterFunc)
    {
        return Processor.VectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector GetVectorPart(Func<double, bool> filterFunc)
    {
        return Processor.VectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector GetVectorPart(Func<int, double, bool> filterFunc)
    {
        return Processor.VectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Bivector GetBivectorPart()
    {
        return Processor.BivectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64HigherKVector GetHigherKVectorPart(int grade)
    {
        return Processor.HigherKVectorZero(grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar GetPart(Func<ulong, bool> filterFunc)
    {
        return IsZero || filterFunc(0) 
            ? this 
            : Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar GetPart(Func<double, bool> filterFunc)
    {
        return IsZero || filterFunc(ScalarValue) 
            ? this 
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar GetPart(Func<ulong, double, bool> filterFunc)
    {
        return IsZero || filterFunc(0, ScalarValue) 
            ? this 
            : Processor.ScalarZero;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double Scalar()
    {
        return _scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetBasisBladeScalar(ulong basisBladeId)
    {
        return basisBladeId == 0UL
            ? _scalar
            : 0d;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetScalarValue(out double scalar)
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
    public override bool TryGetBasisBladeScalarValue(ulong basisBlade, out double scalar)
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
        return Equals(_scalar, other._scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is RGaFloat64Scalar other && Equals(other);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return _scalar.GetHashCode();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return IsZero ? string.Empty : $"({ScalarValue:G})<>";
    }
}