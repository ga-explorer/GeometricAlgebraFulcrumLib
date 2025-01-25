using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

public sealed class GrBabylonJsPlane :
    GrBabylonJsMesh
{
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreatePlane";

    public GrBabylonJsPlaneOptions Options { get; private set; }
        = new GrBabylonJsPlaneOptions();

    public override GrBabylonJsObjectOptions ObjectOptions 
        => Options;


    public GrBabylonJsPlane(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsPlane(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsPlane SetOptions(GrBabylonJsPlaneOptions options)
    {
        Options = new GrBabylonJsPlaneOptions(options);

        return this;
    }

    public GrBabylonJsPlane SetProperties(GrBabylonJsMeshProperties properties)
    {
        Properties = new GrBabylonJsMeshProperties(properties);

        return this;
    }
}