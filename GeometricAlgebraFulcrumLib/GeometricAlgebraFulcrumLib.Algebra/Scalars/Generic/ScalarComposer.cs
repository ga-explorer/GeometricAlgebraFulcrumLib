using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

public sealed partial class ScalarComposer<T> :
    IScalar<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarComposer<T> Create(IScalarProcessor<T> scalarProcessor)
    {
        return new ScalarComposer<T>(scalarProcessor);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarComposer<T> Create(Scalar<T> scalar)
    {
        return new ScalarComposer<T>(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarComposer<T> Create(IScalar<T> scalar)
    {
        return new ScalarComposer<T>(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarComposer<T> Create(IScalarProcessor<T> scalarProcessor, T scalarValue)
    {
        return new ScalarComposer<T>(scalarProcessor, scalarValue);
    }


    private Scalar<T> _scalar;
    public Scalar<T> Scalar
    {
        get => _scalar;
        set
        {
            if (!value.IsValid())
                throw new InvalidOperationException();

            _scalar = value;
        }
    }

    public T ScalarValue 
        => _scalar.ScalarValue;

    public IScalarProcessor<T> ScalarProcessor 
        => _scalar.ScalarProcessor;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScalarComposer(IScalarProcessor<T> scalarProcessor)
    {
        _scalar = scalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScalarComposer(IScalar<T> scalar)
    {
        if (!scalar.IsValid())
            throw new InvalidOperationException();

        _scalar = scalar.ToScalar();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScalarComposer(IScalarProcessor<T> scalarProcessor, T scalarValue)
    {
        if (!scalarProcessor.IsValid(scalarValue))
            throw new InvalidOperationException();

        _scalar = ScalarProcessor.ScalarFromValue(scalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ToScalar()
    {
        return _scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Clear()
    {
        _scalar = ScalarProcessor.Zero;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalarValue()
    {
        return _scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetScalar()
    {
        return _scalar;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(int scalarValue)
    {
        return SetScalar(
            ScalarProcessor.ScalarFromNumber(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(uint scalarValue)
    {
        return SetScalar(
            ScalarProcessor.ScalarFromNumber(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(long scalarValue)
    {
        return SetScalar(
            ScalarProcessor.ScalarFromNumber(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(ulong scalarValue)
    {
        return SetScalar(
            ScalarProcessor.ScalarFromNumber(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(float scalarValue)
    {
        return SetScalar(
            ScalarProcessor.ScalarFromNumber(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(double scalarValue)
    {
        return SetScalar(
            ScalarProcessor.ScalarFromNumber(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(string scalarValue)
    {
        return SetScalar(
            ScalarProcessor.ScalarFromText(scalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(T scalarValue)
    {
        if (!ScalarProcessor.IsValid(scalarValue))
            throw new InvalidOperationException();

        _scalar = ScalarProcessor.ScalarFromValue(scalarValue);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(Scalar<T> scalar)
    {
        return SetScalar(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(IScalar<T> scalar)
    {
        return SetScalar(scalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalarNegative(int scalarValue)
    {
        return SetScalar(
            ScalarProcessor.Negative(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalarNegative(uint scalarValue)
    {
        return SetScalar(
            ScalarProcessor.Negative(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalarNegative(long scalarValue)
    {
        return SetScalar(
            ScalarProcessor.Negative(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalarNegative(ulong scalarValue)
    {
        return SetScalar(
            ScalarProcessor.Negative(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalarNegative(float scalarValue)
    {
        return SetScalar(
            ScalarProcessor.Negative(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalarNegative(double scalarValue)
    {
        return SetScalar(
            ScalarProcessor.Negative(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalarNegative(string scalarValue)
    {
        return SetScalar(
            ScalarProcessor.Negative(scalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalarNegative(T scalarValue)
    {
        return SetScalar(
            ScalarProcessor.Negative(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalarNegative(Scalar<T> scalar)
    {
        return SetScalar(
            scalar.Negative()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalarNegative(IScalar<T> scalar)
    {
        return SetScalar(
            scalar.Negative()
        );
    }

    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public ScalarComposer<T> SetScalar(double scalarValue, int scalingFactor)
    //{
    //    return SetScalar(
    //        ScalarProcessor.Times(scalarValue, scalingFactor)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public ScalarComposer<T> SetScalar(double scalarValue, uint scalingFactor)
    //{
    //    return SetScalar(
    //        ScalarProcessor.Times(scalarValue, scalingFactor)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public ScalarComposer<T> SetScalar(double scalarValue, long scalingFactor)
    //{
    //    return SetScalar(
    //        ScalarProcessor.Times(scalarValue, scalingFactor)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public ScalarComposer<T> SetScalar(double scalarValue, ulong scalingFactor)
    //{
    //    return SetScalar(
    //        ScalarProcessor.Times(scalarValue, scalingFactor)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public ScalarComposer<T> SetScalar(double scalarValue, float scalingFactor)
    //{
    //    return SetScalar(
    //        ScalarProcessor.Times(scalarValue, scalingFactor)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public ScalarComposer<T> SetScalar(double scalarValue, double scalingFactor)
    //{
    //    return SetScalar(
    //        ScalarProcessor.Times(scalarValue, scalingFactor)
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public ScalarComposer<T> SetScalar(double scalarValue, string scalingFactor)
    //{
    //    return SetScalar(
    //        ScalarProcessor.Times(scalarValue, scalingFactor)
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(double scalarValue, T scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalarValue, scalingFactor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(double scalarValue, Scalar<T> scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalarValue, scalingFactor.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(double scalarValue, IScalar<T> scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalarValue, scalingFactor.ScalarValue)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(T scalarValue, int scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalarValue, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(T scalarValue, uint scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalarValue, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(T scalarValue, long scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalarValue, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(T scalarValue, ulong scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalarValue, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(T scalarValue, float scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalarValue, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(T scalarValue, double scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalarValue, scalingFactor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(T scalarValue, string scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalarValue, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(T scalarValue, T scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalarValue, scalingFactor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(T scalarValue, Scalar<T> scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalarValue, scalingFactor.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(T scalarValue, IScalar<T> scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalarValue, scalingFactor.ScalarValue)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(Scalar<T> scalar, int scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(Scalar<T> scalar, uint scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(Scalar<T> scalar, long scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(Scalar<T> scalar, ulong scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(Scalar<T> scalar, float scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(Scalar<T> scalar, double scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(Scalar<T> scalar, string scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(Scalar<T> scalar, T scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(Scalar<T> scalar, IScalar<T> scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor.ScalarValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(IScalar<T> scalar, int scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(IScalar<T> scalar, uint scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(IScalar<T> scalar, long scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(IScalar<T> scalar, ulong scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(IScalar<T> scalar, float scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(IScalar<T> scalar, double scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(IScalar<T> scalar, string scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(IScalar<T> scalar, T scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SetScalar(IScalar<T> scalar, IScalar<T> scalingFactor)
    {
        return SetScalar(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor.ScalarValue)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(int scalarValue)
    {
        return AddScalar(
            ScalarProcessor.ScalarFromNumber(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(uint scalarValue)
    {
        return AddScalar(
            ScalarProcessor.ScalarFromNumber(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(long scalarValue)
    {
        return AddScalar(
            ScalarProcessor.ScalarFromNumber(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(ulong scalarValue)
    {
        return AddScalar(
            ScalarProcessor.ScalarFromNumber(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(float scalarValue)
    {
        return AddScalar(
            ScalarProcessor.ScalarFromNumber(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(double scalarValue)
    {
        return AddScalar(
            ScalarProcessor.ScalarFromNumber(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(string scalarValue)
    {
        return AddScalar(
            ScalarProcessor.ScalarFromText(scalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(T scalarValue)
    {
        if (ScalarProcessor.IsZero(scalarValue))
            return this;

        return SetScalar(
            ScalarProcessor.Add(_scalar.ScalarValue, scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(Scalar<T> scalar)
    {
        return AddScalar(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(IScalar<T> scalar)
    {
        return AddScalar(scalar.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalars(params T[] scalarList)
    {
        foreach (var scalarValue in scalarList)
            AddScalar(scalarValue);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalars(IEnumerable<T> scalarList)
    {
        foreach (var scalarValue in scalarList)
            AddScalar(scalarValue);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalars(params IScalar<T>[] scalarList)
    {
        foreach (var scalar in scalarList)
            AddScalar(scalar);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalars(IEnumerable<IScalar<T>> scalarList)
    {
        foreach (var scalar in scalarList)
            AddScalar(scalar);

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(T scalar, int scalingFactor)
    {
        return AddScalar(
            ScalarProcessor.Times(scalar, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(T scalar, uint scalingFactor)
    {
        return AddScalar(
            ScalarProcessor.Times(scalar, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(T scalar, long scalingFactor)
    {
        return AddScalar(
            ScalarProcessor.Times(scalar, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(T scalar, ulong scalingFactor)
    {
        return AddScalar(
            ScalarProcessor.Times(scalar, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(T scalar, float scalingFactor)
    {
        return AddScalar(
            ScalarProcessor.Times(scalar, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(T scalar, double scalingFactor)
    {
        return AddScalar(
            ScalarProcessor.Times(scalar, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(T scalar, string scalingFactor)
    {
        return AddScalar(
            ScalarProcessor.Times(scalar, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(T scalar, T scalingFactor)
    {
        return AddScalar(
            ScalarProcessor.Times(scalar, scalingFactor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(IScalar<T> scalar, int scalingFactor)
    {
        return AddScalar(
            scalar.Times(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(IScalar<T> scalar, uint scalingFactor)
    {
        return AddScalar(
            scalar.Times(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(IScalar<T> scalar, long scalingFactor)
    {
        return AddScalar(
            scalar.Times(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(IScalar<T> scalar, ulong scalingFactor)
    {
        return AddScalar(
            scalar.Times(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(IScalar<T> scalar, float scalingFactor)
    {
        return AddScalar(
            scalar.Times(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(IScalar<T> scalar, double scalingFactor)
    {
        return AddScalar(
            scalar.Times(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(IScalar<T> scalar, string scalingFactor)
    {
        return AddScalar(
            scalar.Times(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(IScalar<T> scalar, T scalingFactor)
    {
        return AddScalar(
            scalar.Times(scalingFactor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> AddScalar(IScalar<T> scalar, IScalar<T> scalingFactor)
    {
        return AddScalar(
            scalar.Times(scalingFactor)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(int scalarValue)
    {
        return SubtractScalar(
            ScalarProcessor.ScalarFromNumber(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(uint scalarValue)
    {
        return SubtractScalar(
            ScalarProcessor.ScalarFromNumber(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(long scalarValue)
    {
        return SubtractScalar(
            ScalarProcessor.ScalarFromNumber(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(ulong scalarValue)
    {
        return SubtractScalar(
            ScalarProcessor.ScalarFromNumber(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(float scalarValue)
    {
        return SubtractScalar(
            ScalarProcessor.ScalarFromNumber(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(double scalarValue)
    {
        return SubtractScalar(
            ScalarProcessor.ScalarFromNumber(scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(string scalarValue)
    {
        return SubtractScalar(
            ScalarProcessor.ScalarFromText(scalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(T scalarValue)
    {
        if (ScalarProcessor.IsZero(scalarValue))
            return this;

        return SetScalar(
            ScalarProcessor.Subtract(_scalar.ScalarValue, scalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(Scalar<T> scalar)
    {
        return SubtractScalar(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(IScalar<T> scalar)
    {
        return SubtractScalar(scalar.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalars(params T[] scalarList)
    {
        foreach (var scalarValue in scalarList)
            SubtractScalar(scalarValue);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalars(IEnumerable<T> scalarList)
    {
        foreach (var scalarValue in scalarList)
            SubtractScalar(scalarValue);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalars(params IScalar<T>[] scalarList)
    {
        foreach (var scalar in scalarList)
            SubtractScalar(scalar);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalars(IEnumerable<IScalar<T>> scalarList)
    {
        foreach (var scalar in scalarList)
            SubtractScalar(scalar);

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(T scalar, int scalingFactor)
    {
        return SubtractScalar(
            ScalarProcessor.Times(scalar, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(T scalar, uint scalingFactor)
    {
        return SubtractScalar(
            ScalarProcessor.Times(scalar, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(T scalar, long scalingFactor)
    {
        return SubtractScalar(
            ScalarProcessor.Times(scalar, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(T scalar, ulong scalingFactor)
    {
        return SubtractScalar(
            ScalarProcessor.Times(scalar, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(T scalar, float scalingFactor)
    {
        return SubtractScalar(
            ScalarProcessor.Times(scalar, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(T scalar, double scalingFactor)
    {
        return SubtractScalar(
            ScalarProcessor.Times(scalar, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(T scalar, string scalingFactor)
    {
        return SubtractScalar(
            ScalarProcessor.Times(scalar, scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(T scalar, T scalingFactor)
    {
        return SubtractScalar(
            ScalarProcessor.Times(scalar, scalingFactor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(IScalar<T> scalar, int scalingFactor)
    {
        return SubtractScalar(
            scalar.Times(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(IScalar<T> scalar, uint scalingFactor)
    {
        return SubtractScalar(
            scalar.Times(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(IScalar<T> scalar, long scalingFactor)
    {
        return SubtractScalar(
            scalar.Times(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(IScalar<T> scalar, ulong scalingFactor)
    {
        return SubtractScalar(
            scalar.Times(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(IScalar<T> scalar, float scalingFactor)
    {
        return SubtractScalar(
            scalar.Times(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(IScalar<T> scalar, double scalingFactor)
    {
        return SubtractScalar(
            scalar.Times(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(IScalar<T> scalar, string scalingFactor)
    {
        return SubtractScalar(
            scalar.Times(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(IScalar<T> scalar, T scalingFactor)
    {
        return SubtractScalar(
            scalar.Times(scalingFactor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> SubtractScalar(IScalar<T> scalar, IScalar<T> scalingFactor)
    {
        return SubtractScalar(
            scalar.Times(scalingFactor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> MapScalar(Func<T, T> mappingFunction)
    {
        return SetScalar(
            mappingFunction(_scalar.ScalarValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Negative()
    {
        return SetScalar(
            _scalar.Negative()
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Times(int scalarFactor)
    {
        return SetScalar(
            _scalar.Times(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Times(uint scalarFactor)
    {
        return SetScalar(
            _scalar.Times(scalarFactor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Times(long scalarFactor)
    {
        return SetScalar(
            _scalar.Times(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Times(ulong scalarFactor)
    {
        return SetScalar(
            _scalar.Times(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Times(float scalarFactor)
    {
        return SetScalar(
            _scalar.Times(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Times(double scalarFactor)
    {
        return SetScalar(
            _scalar.Times(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Times(string scalarFactor)
    {
        return SetScalar(
            _scalar.Times(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Times(T scalarFactor)
    {
        return SetScalar(
            _scalar.Times(scalarFactor)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Divide(int scalarFactor)
    {
        return SetScalar(
            _scalar.Divide(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Divide(uint scalarFactor)
    {
        return SetScalar(
            _scalar.Divide(scalarFactor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Divide(long scalarFactor)
    {
        return SetScalar(
            _scalar.Divide(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Divide(ulong scalarFactor)
    {
        return SetScalar(
            _scalar.Divide(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Divide(float scalarFactor)
    {
        return SetScalar(
            _scalar.Divide(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Divide(double scalarFactor)
    {
        return SetScalar(
            _scalar.Divide(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Divide(string scalarFactor)
    {
        return SetScalar(
            _scalar.Divide(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarComposer<T> Divide(T scalarFactor)
    {
        return SetScalar(
            _scalar.Divide(scalarFactor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return _scalar.ToString();
    }
}