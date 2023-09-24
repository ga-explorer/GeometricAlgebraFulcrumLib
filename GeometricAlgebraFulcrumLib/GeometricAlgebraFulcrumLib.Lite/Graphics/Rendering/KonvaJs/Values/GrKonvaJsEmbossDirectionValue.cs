using DataStructuresLib.AttributeSet;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsEmbossDirectionValue :
    SparseCodeAttributeValue<GrKonvaJsEmbossDirection>
{
    public static implicit operator GrKonvaJsEmbossDirectionValue(string valueText)
    {
        return new GrKonvaJsEmbossDirectionValue(valueText);
    }

    public static implicit operator GrKonvaJsEmbossDirectionValue(GrKonvaJsEmbossDirection value)
    {
        return new GrKonvaJsEmbossDirectionValue(value);
    }


    private GrKonvaJsEmbossDirectionValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsEmbossDirectionValue(GrKonvaJsEmbossDirection value)
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