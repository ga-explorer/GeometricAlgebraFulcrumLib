using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public abstract class GrBabylonJsBaseTexture :
    GrBabylonJsObject
{
    public GrBabylonJsSceneValue? ParentScene { get; set; }

    public string SceneVariableName
        => ParentScene?.Value.ConstName ?? string.Empty;

    public override GrBabylonJsObjectOptions? ObjectOptions
        => null;


    protected GrBabylonJsBaseTexture(string constName)
        : base(constName)
    {
    }

    protected GrBabylonJsBaseTexture(string constName, GrBabylonJsSceneValue scene)
        : base(constName)
    {
        ParentScene = scene;
    }
}