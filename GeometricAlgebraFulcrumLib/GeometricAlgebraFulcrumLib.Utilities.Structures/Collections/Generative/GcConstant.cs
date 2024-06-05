namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.Generative;

public sealed class GcConstant<T> : GenerativeCollection<T>
{
    public static GcConstant<T> Create(T defaultValue)
    {
        return new GcConstant<T>(defaultValue);
    }


    public T this[int index] => DefaultValue;


    private GcConstant(T defaultValue)
    {
        DefaultValue = defaultValue;
    }


    public override T GetItem(int index)
    {
        return DefaultValue;
    }
}