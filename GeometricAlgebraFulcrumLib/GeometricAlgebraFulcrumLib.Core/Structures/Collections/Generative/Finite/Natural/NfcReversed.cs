namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections.Generative.Finite.Natural;

/// <summary>
/// This class represents a reversal of a base collection
/// </summary>
/// <typeparam name="T"></typeparam>
public class NfcReversed<T> : NaturalFiniteCollection<T>
{
    public static NfcReversed<T> Create(FiniteCollection<T> baseCollection)
    {
        return new NfcReversed<T>(baseCollection);
    }

    public FiniteCollection<T> BaseCollection { get; private set; }

    public override int Count => BaseCollection.Count;


    private NfcReversed(FiniteCollection<T> baseCollection)
    {
        BaseCollection = baseCollection;
    }


    public override T GetItem(int index)
    {
        return BaseCollection.GetItem(BaseCollection.MaxIndex - index);
    }

    public NfcReversed<T> Reset(FiniteCollection<T> baseCollection)
    {
        BaseCollection = baseCollection;

        return this;
    }
}