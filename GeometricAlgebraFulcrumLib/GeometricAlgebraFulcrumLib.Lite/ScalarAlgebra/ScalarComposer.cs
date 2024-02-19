using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

public sealed class ScalarComposer<T> :
    IScalarAlgebraElement<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarComposer<T> Create(IScalarProcessor<T> scalarProcessor)
    {
        return new ScalarComposer<T>(scalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarComposer<T> Create(IScalarProcessor<T> scalarProcessor, T scalarValue)
    {
        return new ScalarComposer<T>(scalarProcessor, scalarValue);
    }


    private T _scalarValue;

    public T ScalarValue
    {
        get => _scalarValue;
        set
        {
            if (!ScalarProcessor.IsValid(value))
                throw new InvalidOperationException();

            _scalarValue = value;
        }
    }

    public IScalarProcessor<T> ScalarProcessor { get; }

    public bool IsZero
        => ScalarProcessor.IsZero(_scalarValue);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScalarComposer(IScalarProcessor<T> scalarProcessor)
    {
        ScalarProcessor = scalarProcessor;
        _scalarValue = scalarProcessor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScalarComposer(IScalarProcessor<T> scalarProcessor, T scalarValue)
    {
        ScalarProcessor = scalarProcessor;

        if (!ScalarProcessor.IsValid(scalarValue))
            throw new InvalidOperationException();

        _scalarValue = scalarValue;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Clear()
    {
        _scalarValue = ScalarProcessor.ScalarZero;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalarValue()
    {
        return _scalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetScalar()
    {
        return ScalarProcessor.CreateScalar(_scalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalarValue(T scalarValue)
    {
        if (!ScalarProcessor.IsValid(scalarValue))
            throw new InvalidOperationException();

        _scalarValue = scalarValue;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(Scalar<T> scalar)
    {
        return SetScalarValue(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalarValueNegative(T scalarValue)
    {
        return SetScalarValue(
            ScalarProcessor.Negative(scalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalarNegative(Scalar<T> scalar)
    {
        return SetScalarValue(
            ScalarProcessor.Negative(scalar.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalarValue(T scalarValue, T scalingFactor)
    {
        return SetScalarValue(
            ScalarProcessor.Times(scalarValue, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(Scalar<T> scalar, T scalingFactor)
    {
        return SetScalarValue(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalarValue(T scalarValue)
    {
        if (ScalarProcessor.IsZero(scalarValue))
            return this;

        return SetScalarValue(
            ScalarProcessor.Add(_scalarValue, scalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalarValues(params T[] scalarList)
    {
        foreach (var scalarValue in scalarList)
            AddScalarValue(scalarValue);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalarValues(IEnumerable<T> scalarList)
    {
        foreach (var scalarValue in scalarList)
            AddScalarValue(scalarValue);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(Scalar<T> scalar)
    {
        return AddScalarValue(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(Scalar<T> scalar, T scalingFactor)
    {
        return AddScalarValue(
            ScalarProcessor.Add(scalar.ScalarValue, scalingFactor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalarValue(T scalarValue)
    {
        if (ScalarProcessor.IsZero(scalarValue))
            return this;

        return SetScalarValue(
            ScalarProcessor.Subtract(_scalarValue, scalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalarValues(params T[] scalarList)
    {
        foreach (var scalarValue in scalarList)
            SubtractScalarValue(scalarValue);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalarValues(IEnumerable<T> scalarList)
    {
        foreach (var scalarValue in scalarList)
            SubtractScalarValue(scalarValue);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(Scalar<T> scalar)
    {
        return SubtractScalarValue(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(Scalar<T> scalar, T scalingFactor)
    {
        return SubtractScalarValue(
            ScalarProcessor.Subtract(scalar.ScalarValue, scalingFactor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> MapScalar(Func<T, T> mappingFunction)
    {
        return SetScalarValue(
            mappingFunction(_scalarValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Negative()
    {
        return SetScalarValue(
            ScalarProcessor.Negative(_scalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Times(T scalarFactor)
    {
        return SetScalarValue(
            ScalarProcessor.Times(_scalarValue, scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Divide(T scalarFactor)
    {
        return SetScalarValue(
            ScalarProcessor.Divide(_scalarValue, scalarFactor)
        );
    }
}