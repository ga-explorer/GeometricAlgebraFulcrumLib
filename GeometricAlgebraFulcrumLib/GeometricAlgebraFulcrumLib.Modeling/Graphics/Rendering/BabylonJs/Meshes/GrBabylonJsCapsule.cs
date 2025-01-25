using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateCapsule-2
/// </summary>
public sealed class GrBabylonJsCapsule :
    GrBabylonJsMesh
{
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateCapsule";

    public GrBabylonJsCapsuleOptions Options { get; private set; }
        = new GrBabylonJsCapsuleOptions();

    public override GrBabylonJsObjectOptions ObjectOptions
        => Options;


    public GrBabylonJsCapsule(string constName)
        : base(constName)
    {
    }

    public GrBabylonJsCapsule(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
    }


    public GrBabylonJsCapsule SetOptions(GrBabylonJsCapsuleOptions options)
    {
        Options = new GrBabylonJsCapsuleOptions(options);

        return this;
    }

    public GrBabylonJsCapsule SetProperties(GrBabylonJsMeshProperties properties)
    {
        Properties = new GrBabylonJsMeshProperties(properties);

        return this;
    }
}