using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateGround-2
/// </summary>
public sealed class GrBabylonJsGround :
    GrBabylonJsMesh
{
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateGround";

    public GrBabylonJsGroundOptions Options { get; private set; }
        = new GrBabylonJsGroundOptions();

    public override GrBabylonJsObjectOptions ObjectOptions 
        => Options;


    public GrBabylonJsGround(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsGround(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsGround SetOptions(GrBabylonJsGroundOptions options)
    {
        Options = new GrBabylonJsGroundOptions(options);

        return this;
    }

    public GrBabylonJsGround SetProperties(GrBabylonJsMeshProperties properties)
    {
        Properties = new GrBabylonJsMeshProperties(properties);

        return this;
    }
}