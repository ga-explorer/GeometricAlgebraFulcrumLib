using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateIcoSphere-2
/// </summary>
public sealed class GrBabylonJsIcoSphere :
    GrBabylonJsMesh
{
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateIcoSphere";

    public GrBabylonJsIcoSphereOptions Options { get; private set; }
        = new GrBabylonJsIcoSphereOptions();

    public override GrBabylonJsObjectOptions ObjectOptions
        => Options;


    public GrBabylonJsIcoSphere(string constName)
        : base(constName)
    {
    }

    public GrBabylonJsIcoSphere(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
    }


    public GrBabylonJsIcoSphere SetOptions(GrBabylonJsIcoSphereOptions options)
    {
        Options = new GrBabylonJsIcoSphereOptions(options);

        return this;
    }

    public GrBabylonJsIcoSphere SetProperties(GrBabylonJsMeshProperties properties)
    {
        Properties = new GrBabylonJsMeshProperties(properties);

        return this;
    }
}