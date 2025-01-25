using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateBox-2
/// </summary>
public sealed class GrBabylonJsBox :
    GrBabylonJsMesh
{
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateBox";

    public GrBabylonJsBoxOptions Options { get; private set; }
        = new GrBabylonJsBoxOptions();

    public override GrBabylonJsObjectOptions ObjectOptions
        => Options;


    public GrBabylonJsBox(string constName)
        : base(constName)
    {
    }

    public GrBabylonJsBox(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
    }


    public GrBabylonJsBox SetOptions(GrBabylonJsBoxOptions options)
    {
        Options = new GrBabylonJsBoxOptions(options);

        return this;
    }

    public GrBabylonJsBox SetProperties(GrBabylonJsMeshProperties properties)
    {
        Properties = new GrBabylonJsMeshProperties(properties);

        return this;
    }
}