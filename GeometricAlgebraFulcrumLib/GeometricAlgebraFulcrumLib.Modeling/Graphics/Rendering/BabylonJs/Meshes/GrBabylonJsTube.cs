using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateTube-2
/// https://doc.babylonjs.com/features/featuresDeepDive/mesh/creation/param/tube
/// </summary>
public sealed class GrBabylonJsTube :
    GrBabylonJsMesh
{
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateTube";

    public GrBabylonJsTubeOptions Options { get; private set; }
        = new GrBabylonJsTubeOptions();

    public override GrBabylonJsObjectOptions ObjectOptions
        => Options;


    public GrBabylonJsTube(string constName)
        : base(constName)
    {
        UseLetDeclaration = true;
    }

    public GrBabylonJsTube(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
        UseLetDeclaration = true;
    }


    public GrBabylonJsTube SetOptions(GrBabylonJsTubeOptions options)
    {
        Options = new GrBabylonJsTubeOptions(options);

        return this;
    }

    public GrBabylonJsTube SetProperties(GrBabylonJsMeshProperties properties)
    {
        Properties = new GrBabylonJsMeshProperties(properties);

        return this;
    }
}