using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Animations;

public sealed class GrBabylonJsAnimationGroupProperties :
    GrBabylonJsObjectProperties
{
    public GrBabylonJsBooleanValue? IsAdditive
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isAdditive");
        set => SetAttributeValue("isAdditive", value);
    }

    public GrBabylonJsBooleanValue? LoopAnimation
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("loopAnimation");
        set => SetAttributeValue("loopAnimation", value);
    }
        
    public GrBabylonJsFloat32Value? SpeedRatio
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("speedRatio");
        set => SetAttributeValue("speedRatio", value);
    }
        

    public GrBabylonJsAnimationGroupProperties()
    {
    }

    public GrBabylonJsAnimationGroupProperties(GrBabylonJsAnimationGroupProperties properties)
    {
        SetAttributeValues(properties);
    }
}