using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Styles;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Filters;

public class GrKonvaFilterEmboss :
    GrKonvaFilter
{
    public override string FilterName 
        => "Emboss";

    
    public GrKonvaJsBooleanValue? Blend
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsBooleanValue>("EmbossBlend");
        set => ParentStyle.SetAttributeValue("EmbossBlend", value);
    }

    public GrKonvaJsFloat32Value? Strength
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("EmbossStrength");
        set => ParentStyle.SetAttributeValue("EmbossStrength", value);
    }
        
    public GrKonvaJsFloat32Value? WhiteLevel
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("EmbossWhiteLevel");
        set => ParentStyle.SetAttributeValue("EmbossWhiteLevel", value);
    }
        
    public GrKonvaJsEmbossDirectionValue? Direction
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsEmbossDirectionValue>("EmbossDirection");
        set => ParentStyle.SetAttributeValue("EmbossDirection", value);
    }


    public GrKonvaFilterEmboss(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}