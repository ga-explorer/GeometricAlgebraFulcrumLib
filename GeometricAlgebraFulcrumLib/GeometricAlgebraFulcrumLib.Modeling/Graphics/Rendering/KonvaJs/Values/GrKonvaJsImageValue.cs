using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsImageValue :
    SparseCodeAttributeValue<GrKonvaJsImage>
{
    public static implicit operator GrKonvaJsImageValue(string valueText)
    {
        return new GrKonvaJsImageValue(valueText);
    }

    public static implicit operator GrKonvaJsImageValue(GrKonvaJsImage value)
    {
        return new GrKonvaJsImageValue(value);
    }
    

    private GrKonvaJsImageValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsImageValue(GrKonvaJsImage value)
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