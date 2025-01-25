using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateLathe-2
/// https://doc.babylonjs.com/features/featuresDeepDive/mesh/creation/param/lathe
/// </summary>
public sealed class GrBabylonJsLathe :
    GrBabylonJsMesh
{
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateLathe";

    public GrBabylonJsLatheOptions Options { get; private set; }
        = new GrBabylonJsLatheOptions();

    public override GrBabylonJsObjectOptions ObjectOptions
        => Options;


    public GrBabylonJsLathe(string constName)
        : base(constName)
    {
        UseLetDeclaration = true;
    }

    public GrBabylonJsLathe(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
        UseLetDeclaration = true;
    }


    public GrBabylonJsLathe SetOptions(GrBabylonJsLatheOptions options)
    {
        Options = new GrBabylonJsLatheOptions(options);

        return this;
    }

    public GrBabylonJsLathe SetProperties(GrBabylonJsMeshProperties properties)
    {
        Properties = new GrBabylonJsMeshProperties(properties);

        return this;
    }
}