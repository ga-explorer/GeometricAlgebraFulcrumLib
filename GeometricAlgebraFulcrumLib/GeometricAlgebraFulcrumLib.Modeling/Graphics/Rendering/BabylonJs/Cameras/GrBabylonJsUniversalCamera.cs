using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Cameras;

public sealed class GrBabylonJsUniversalCamera :
    GrBabylonJsTargetCamera
{
    protected override string ConstructorName
        => "new BABYLON.UniversalCamera";

    public GrBabylonJsUniversalCameraProperties Properties { get; private set; }
        = new GrBabylonJsUniversalCameraProperties();

    public override GrBabylonJsObjectProperties ObjectProperties
        => Properties;

    public GrBabylonJsVector3Value Position { get; set; }


    public GrBabylonJsUniversalCamera(string constName)
        : base(constName)
    {
    }

    public GrBabylonJsUniversalCamera(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
    }


    public GrBabylonJsUniversalCamera SetProperties(GrBabylonJsUniversalCameraProperties properties)
    {
        Properties = new GrBabylonJsUniversalCameraProperties(properties);

        return this;
    }

    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return Position.GetAttributeValueCode();

        if (ParentScene.IsNullOrEmpty()) yield break;
        yield return SceneVariableName;
    }


}