//using System.Diagnostics.CodeAnalysis;

//namespace GraphicsComposerLib.Rendering.KonvaJs.Values;

//public abstract class SparseCodeAttributeValue :
//    IGrKonvaJsCodeElement
//{
//    public string ValueText { get; }

//    public abstract bool IsEmpty { get; }


//    protected SparseCodeAttributeValue(string valueText)
//    {
//        ValueText = valueText;
//    }


//    public abstract string GetCode();

//    public override string ToString()
//    {
//        return GetCode();
//    }
//}

//public abstract class SparseCodeAttributeValue<T> :
//    SparseCodeAttributeValue
//{
//    public T Value { get; }

//    public override bool IsEmpty 
//        => string.IsNullOrEmpty(ValueText) && Value is null;


//    protected SparseCodeAttributeValue(string valueText) 
//        : base(valueText)
//    {
//    }

//    protected SparseCodeAttributeValue([NotNull] T value)
//        : base(string.Empty)
//    {
//        Value = value;
//    }



//}