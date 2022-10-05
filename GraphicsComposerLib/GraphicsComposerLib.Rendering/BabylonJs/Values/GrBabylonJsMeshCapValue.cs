using GraphicsComposerLib.Rendering.BabylonJs.Constants;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsMeshCapValue :
    GrBabylonJsValue<GrBabylonJsMeshCap>
{
    public static implicit operator GrBabylonJsMeshCapValue(string valueText)
    {
        return new GrBabylonJsMeshCapValue(valueText);
    }

    public static implicit operator GrBabylonJsMeshCapValue(GrBabylonJsMeshCap value)
    {
        return new GrBabylonJsMeshCapValue(value);
    }


    private GrBabylonJsMeshCapValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsMeshCapValue(GrBabylonJsMeshCap value)
        : base(value)
    {
    }


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetBabylonJsCode() 
            : ValueText;
    }
}