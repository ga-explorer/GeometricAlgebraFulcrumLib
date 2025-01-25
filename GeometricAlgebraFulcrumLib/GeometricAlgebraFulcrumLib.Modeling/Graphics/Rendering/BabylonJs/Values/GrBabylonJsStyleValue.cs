using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsStyleValue :
    SparseCodeAttributeValue<GrBabylonJsGuiStyle>
{
    public static implicit operator GrBabylonJsStyleValue(string valueText)
    {
        return new GrBabylonJsStyleValue(valueText);
    }

    public static implicit operator GrBabylonJsStyleValue(GrBabylonJsGuiStyle value)
    {
        return new GrBabylonJsStyleValue(value);
    }


    private GrBabylonJsStyleValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsStyleValue(GrBabylonJsGuiStyle value)
        : base(value)
    {
    }


    public override string GetAttributeValueCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.ToString() 
            : ValueText;
    }
}