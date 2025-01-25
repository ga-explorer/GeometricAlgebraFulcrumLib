using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateSphere-2
/// </summary>
public sealed class GrBabylonJsSphere :
    GrBabylonJsMesh
{
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateSphere";

    public GrBabylonJsSphereOptions Options { get; private set; }
        = new GrBabylonJsSphereOptions();

    public override GrBabylonJsObjectOptions ObjectOptions
        => Options;


    public GrBabylonJsSphere(string constName)
        : base(constName)
    {
    }

    public GrBabylonJsSphere(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
    }


    public GrBabylonJsSphere SetOptions(GrBabylonJsSphereOptions options)
    {
        Options = options;

        return this;
    }

    public GrBabylonJsSphere SetProperties(GrBabylonJsMeshProperties properties)
    {
        Properties = properties;

        return this;
    }
}