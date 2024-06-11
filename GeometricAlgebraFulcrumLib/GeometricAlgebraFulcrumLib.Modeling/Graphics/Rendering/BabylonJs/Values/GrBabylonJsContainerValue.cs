using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsContainerValue :
    SparseCodeAttributeValue<GrBabylonJsGuiContainer>
{
    public static implicit operator GrBabylonJsContainerValue(string valueText)
    {
        return new GrBabylonJsContainerValue(valueText);
    }

    public static implicit operator GrBabylonJsContainerValue(GrBabylonJsGuiContainer value)
    {
        return new GrBabylonJsContainerValue(value);
    }


    private GrBabylonJsContainerValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsContainerValue(GrBabylonJsGuiContainer value)
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