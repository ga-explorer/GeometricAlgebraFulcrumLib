namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsTextureProperties :
    GrBabylonJsBaseTextureProperties
{
    public GrBabylonJsTextureProperties()
    {
    }

    public GrBabylonJsTextureProperties(GrBabylonJsTextureProperties properties)
    {
        SetAttributeValues(properties);
    }
}