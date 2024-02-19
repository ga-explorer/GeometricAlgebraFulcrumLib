using DataStructuresLib.AttributeSet;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.GUI;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

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


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.ToString() 
            : ValueText;
    }
}