using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;

public abstract class GrBabylonJsMaterial :
    GrBabylonJsObject,
    IGrVisualElementMaterial3D
{
    public string MaterialName
        => ConstName;

    public GrBabylonJsSceneValue ParentScene { get; set; }

    public string SceneVariableName
        => ParentScene.Value.ConstName;

    public override GrBabylonJsObjectOptions? ObjectOptions
        => null;


    protected GrBabylonJsMaterial(string constName)
        : base(constName)
    {
    }

    protected GrBabylonJsMaterial(string constName, GrBabylonJsSceneValue scene)
        : base(constName)
    {
        ParentScene = scene;
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();

        if (ParentScene.IsNullOrEmpty()) yield break;
        yield return ParentScene.Value.ConstName;
    }
}