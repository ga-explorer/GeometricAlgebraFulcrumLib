using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Factored;

public sealed class FGaFloat64Scalar :
    FGaFloat64KVector,
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
    public FGaFloat64Scalar(FGaFloat64Processor processor)
        : base(processor)
    {
        _scalarValue = 0d;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FGaFloat64Scalar(FGaFloat64Processor processor, double scalarValue)
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