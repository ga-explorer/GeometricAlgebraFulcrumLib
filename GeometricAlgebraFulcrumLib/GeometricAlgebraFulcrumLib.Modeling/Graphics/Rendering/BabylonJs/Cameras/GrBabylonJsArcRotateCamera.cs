using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Cameras;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.ArcRotateCamera
/// </summary>
public sealed class GrBabylonJsArcRotateCamera :
    GrBabylonJsTargetCamera
{
    protected override string ConstructorName
        => "new BABYLON.ArcRotateCamera";

    public GrBabylonJsArcRotateCameraProperties Properties { get; private set; }
        = new GrBabylonJsArcRotateCameraProperties();

    public override GrBabylonJsObjectProperties ObjectProperties
        => Properties;

    public GrBabylonJsAngleValue Alpha { get; set; }

    public GrBabylonJsAngleValue Beta { get; set; }

    public GrBabylonJsFloat32Value Radius { get; set; }

    public GrBabylonJsVector3Value Target { get; set; }

    public GrBabylonJsBooleanValue? SetActiveOnSceneIfNoneActive { get; set; }


    public GrBabylonJsArcRotateCamera(string constName)
        : base(constName)
    {
    }

    public GrBabylonJsArcRotateCamera(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
    }


    public GrBabylonJsArcRotateCamera SetProperties(GrBabylonJsArcRotateCameraProperties properties)
    {
        Properties = new GrBabylonJsArcRotateCameraProperties(properties);

        return this;
    }

    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();
        yield return Alpha.GetAttributeValueCode();
        yield return Beta.GetAttributeValueCode();
        yield return Radius.GetAttributeValueCode();
        yield return Target.GetAttributeValueCode();

        if (ParentScene.IsNullOrEmpty()) yield break;
        yield return SceneVariableName;

        if (SetActiveOnSceneIfNoneActive is null || SetActiveOnSceneIfNoneActive.IsEmpty) yield break;
        yield return SetActiveOnSceneIfNoneActive.GetAttributeValueCode();
    }

}