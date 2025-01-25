using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsControlValue :
    SparseCodeAttributeValue<GrBabylonJsGuiControl>
{
    public static implicit operator GrBabylonJsControlValue(string valueText)
    {
        return new GrBabylonJsControlValue(valueText);
    }

    public static implicit operator GrBabylonJsControlValue(GrBabylonJsGuiControl value)
    {
        return new GrBabylonJsControlValue(value);
    }


    private GrBabylonJsControlValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsControlValue(GrBabylonJsGuiControl value)
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