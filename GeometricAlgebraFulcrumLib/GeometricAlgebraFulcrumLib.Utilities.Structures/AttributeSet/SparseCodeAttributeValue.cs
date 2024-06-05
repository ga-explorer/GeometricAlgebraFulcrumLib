namespace GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

public abstract class SparseCodeAttributeValue :
    ISparseCodeAttributeValue
{
    public string ValueText { get; }

    public abstract bool IsEmpty { get; }


    protected SparseCodeAttributeValue(string valueText)
    {
        ValueText = valueText;
    }


    public abstract string GetCode();

    public override string ToString()
    {
        return GetCode();
    }
}

public abstract class SparseCodeAttributeValue<T> : 
    SparseCodeAttributeValue,
    ISparseCodeAttributeValue<T>
{
    public T Value { get; }
        
    public override bool IsEmpty 
        => string.IsNullOrEmpty(ValueText) && Value is null;


    protected SparseCodeAttributeValue(string valueText) 
        : base(valueText)
    {
        Value = default;
    }
        
    protected SparseCodeAttributeValue(T value) 
        : base(string.Empty)
    {
        Value = value;
    }
}