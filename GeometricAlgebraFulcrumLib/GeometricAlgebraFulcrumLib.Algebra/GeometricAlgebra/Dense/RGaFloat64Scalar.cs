using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Dense;

public sealed class RGaFloat64Scalar :
    RGaFloat64KVector,
    IFloat64Scalar
{
    public override int Grade 
        => 0;

    private double _scalarValue;

    public double ScalarValue
    {
        get => _scalarValue;
        set
        {
            if (!value.IsValid())
                throw new InvalidOperationException();

            _scalarValue = value;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar(RGaFloat64Processor processor)
        : base(processor)
    {
        _scalarValue = 0d;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar(RGaFloat64Processor processor, double scalarValue)
        : base(processor)
    {
        _scalarValue = scalarValue;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ToScalar()
    {
        return new Float64Scalar(ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return ScalarValue.IsValid();
    }
}