using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

public sealed class Float64ScalarComposer
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarComposer Create()
    {
        return new Float64ScalarComposer();
    }


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

    public bool IsZero
        => _scalarValue.IsZero();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarComposer()
    {
        _scalarValue = 0d;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarComposer Clear()
    {
        _scalarValue = 0d;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetScalarValue()
    {
        return _scalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarComposer SetScalarValue(double scalarValue)
    {
        if (!scalarValue.IsValid())
            throw new InvalidOperationException();

        _scalarValue = scalarValue;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarComposer SetScalarValueNegative(double scalarValue)
    {
        return SetScalarValue(-scalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarComposer SetScalarValue(double scalarValue, double scalingFactor)
    {
        return SetScalarValue(scalarValue * scalingFactor);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarComposer AddScalarValue(double scalarValue)
    {
        return SetScalarValue(_scalarValue + scalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarComposer AddScalarValues(params double[] scalarList)
    {
        foreach (var scalarValue in scalarList)
            AddScalarValue(scalarValue);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarComposer AddScalarValues(IEnumerable<double> scalarList)
    {
        foreach (var scalarValue in scalarList)
            AddScalarValue(scalarValue);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarComposer SubtractScalarValue(double scalarValue)
    {
        return SetScalarValue(_scalarValue - scalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarComposer SubtractScalarValues(params double[] scalarList)
    {
        foreach (var scalarValue in scalarList)
            SubtractScalarValue(scalarValue);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarComposer SubtractScalarValues(IEnumerable<double> scalarList)
    {
        foreach (var scalarValue in scalarList)
            SubtractScalarValue(scalarValue);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarComposer MapScalar(Func<double, double> mappingFunction)
    {
        return SetScalarValue(
            mappingFunction(_scalarValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarComposer Negative()
    {
        return SetScalarValue(-_scalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarComposer Times(double scalarFactor)
    {
        return SetScalarValue(_scalarValue * scalarFactor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarComposer Divide(double scalarFactor)
    {
        return SetScalarValue(_scalarValue / scalarFactor);
    }
}