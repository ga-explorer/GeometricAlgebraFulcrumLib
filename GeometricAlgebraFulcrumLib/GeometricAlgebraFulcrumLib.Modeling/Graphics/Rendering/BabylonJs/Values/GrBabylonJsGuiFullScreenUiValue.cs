using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsGuiFullScreenUiValue :
    SparseCodeAttributeValue<GrBabylonJsGuiFullScreenUi>,
    IGrBabylonJsGuiControlContainer
{
    public static implicit operator GrBabylonJsGuiFullScreenUiValue(string valueText)
    {
        return new GrBabylonJsGuiFullScreenUiValue(valueText);
    }

    public static implicit operator GrBabylonJsGuiFullScreenUiValue(GrBabylonJsGuiFullScreenUi value)
    {
        return new GrBabylonJsGuiFullScreenUiValue(value);
    }


    public GrBabylonJsGuiFullScreenUiValue ParentUi 
        => this;

    public GrBabylonJsGuiControlList ControlList 
        => Value.ControlList;


    private GrBabylonJsGuiFullScreenUiValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsGuiFullScreenUiValue(GrBabylonJsGuiFullScreenUi value)
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