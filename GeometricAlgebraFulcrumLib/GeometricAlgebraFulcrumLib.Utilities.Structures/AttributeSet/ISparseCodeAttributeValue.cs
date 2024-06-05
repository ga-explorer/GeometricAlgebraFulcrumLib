namespace GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

public interface ISparseCodeAttributeValue
{
    string ValueText { get; }

    bool IsEmpty { get; }

    string GetCode();
}

public interface ISparseCodeAttributeValue<out T> :
    ISparseCodeAttributeValue
{
    public T Value { get; }
}
