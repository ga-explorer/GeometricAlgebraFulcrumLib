using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

public sealed class GrBabylonJsLinesMeshProperties :
    GrBabylonJsMeshProperties
{
    public GrBabylonJsColor3Value? Color
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("color");
        set => SetAttributeValue("color", value);
    }

    public GrBabylonJsFloat32Value? Alpha
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("alpha");
        set => SetAttributeValue("alpha", value);
    }


    public GrBabylonJsLinesMeshProperties()
    {
    }

    public GrBabylonJsLinesMeshProperties(GrBabylonJsLinesMeshProperties properties)
    {
        SetAttributeValues(properties);
    }
}