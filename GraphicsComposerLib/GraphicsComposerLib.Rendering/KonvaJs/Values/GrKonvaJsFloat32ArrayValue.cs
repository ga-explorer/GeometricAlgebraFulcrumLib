using DataStructuresLib.AttributeSet;

namespace GraphicsComposerLib.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsFloat32ArrayValue :
    SparseCodeAttributeValue<IReadOnlyList<float>>
{
    internal static GrKonvaJsFloat32ArrayValue Create(IReadOnlyList<float> value)
    {
        return new GrKonvaJsFloat32ArrayValue(value);
    }


    public static implicit operator GrKonvaJsFloat32ArrayValue(string valueText)
    {
        return new GrKonvaJsFloat32ArrayValue(valueText);
    }

    public static implicit operator GrKonvaJsFloat32ArrayValue(float[] value)
    {
        return new GrKonvaJsFloat32ArrayValue(value);
    }
    

    private GrKonvaJsFloat32ArrayValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsFloat32ArrayValue(IReadOnlyList<float> value)
        : base(value)
    {
    }


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetKonvaJsCode() 
            : ValueText;
    }
}