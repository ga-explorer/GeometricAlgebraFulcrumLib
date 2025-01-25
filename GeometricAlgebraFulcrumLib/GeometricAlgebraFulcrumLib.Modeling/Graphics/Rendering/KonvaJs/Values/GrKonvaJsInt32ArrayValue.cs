using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsInt32ArrayValue :
    SparseCodeAttributeValue<IReadOnlyList<int>>
{
    internal static GrKonvaJsInt32ArrayValue Create(IReadOnlyList<int> value)
    {
        return new GrKonvaJsInt32ArrayValue(value);
    }


    public static implicit operator GrKonvaJsInt32ArrayValue(string valueText)
    {
        return new GrKonvaJsInt32ArrayValue(valueText);
    }

    public static implicit operator GrKonvaJsInt32ArrayValue(int[] value)
    {
        return new GrKonvaJsInt32ArrayValue(value);
    }
    

    private GrKonvaJsInt32ArrayValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsInt32ArrayValue(IReadOnlyList<int> value)
        : base(value)
    {
    }


    public override string GetAttributeValueCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetKonvaJsCode() 
            : ValueText;
    }
}