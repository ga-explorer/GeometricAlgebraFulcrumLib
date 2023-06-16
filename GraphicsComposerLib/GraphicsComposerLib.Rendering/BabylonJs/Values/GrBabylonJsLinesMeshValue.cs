using GraphicsComposerLib.Rendering.BabylonJs.Meshes;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsLinesMeshValue :
    GrBabylonJsValue<GrBabylonJsLinesMesh>
{
    public static implicit operator GrBabylonJsLinesMeshValue(string valueText)
    {
        return new GrBabylonJsLinesMeshValue(valueText);
    }

    public static implicit operator GrBabylonJsLinesMeshValue(GrBabylonJsLinesMesh value)
    {
        return new GrBabylonJsLinesMeshValue(value);
    }


    private GrBabylonJsLinesMeshValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsLinesMeshValue(GrBabylonJsLinesMesh value)
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