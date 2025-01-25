using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateRibbon-2
/// https://doc.babylonjs.com/features/featuresDeepDive/mesh/creation/param/ribbon
/// </summary>
public sealed class GrBabylonJsRibbon :
    GrBabylonJsMesh
{
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateRibbon";

    public GrBabylonJsRibbonOptions Options { get; private set; }
        = new GrBabylonJsRibbonOptions();

    public override GrBabylonJsObjectOptions ObjectOptions
        => Options;


    public GrBabylonJsRibbon(string constName)
        : base(constName)
    {
        UseLetDeclaration = true;
    }

    public GrBabylonJsRibbon(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
        UseLetDeclaration = true;
    }


    public GrBabylonJsRibbon SetOptions(GrBabylonJsRibbonOptions options)
    {
        Options = new GrBabylonJsRibbonOptions(options);

        return this;
    }

    public GrBabylonJsRibbon SetProperties(GrBabylonJsMeshProperties properties)
    {
        Properties = new GrBabylonJsMeshProperties(properties);

        return this;
    }
}