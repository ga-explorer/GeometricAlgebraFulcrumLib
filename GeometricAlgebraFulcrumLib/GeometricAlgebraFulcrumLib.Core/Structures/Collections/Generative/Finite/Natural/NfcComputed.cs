namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections.Generative.Finite.Natural;

/// <summary>
/// This class represents a collection of elements computed from their indices
/// </summary>
/// <typeparam name="T"></typeparam>
public class NfcComputed<T> : NaturalFiniteCollection<T>
{
    public NfcComputed<T> Create(int itemsCount, Func<int, T> mapFunction)
    {
        return new NfcComputed<T>(itemsCount, mapFunction);
    }


    private int _valuesCount;


    public Func<int, T> ValueFunction { get; set; }

    public override int Count => _valuesCount;

    public T this[int index] => ValueFunction == null ? DefaultValue : ValueFunction(index);


    private NfcComputed(int itemsCount, Func<int, T> valueFunction)
    {
        _valuesCount = itemsCount;
        ValueFunction = valueFunction;
    }


    public NfcComputed<T> Reset(int itemsCount, Func<int, T> valueFunction)
    {
        _valuesCount = itemsCount;
        ValueFunction = valueFunction;

        return this;
    }

    public override T GetItem(int index)
    {
        return ValueFunction == null ? DefaultValue : ValueFunction(index);
    }
}