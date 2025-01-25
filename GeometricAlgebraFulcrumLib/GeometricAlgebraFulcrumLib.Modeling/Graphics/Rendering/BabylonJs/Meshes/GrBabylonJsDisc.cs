using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateDisc-2
/// </summary>
public sealed class GrBabylonJsDisc :
    GrBabylonJsMesh
{
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateDisc";

    public GrBabylonJsDiscOptions Options { get; private set; }
        = new GrBabylonJsDiscOptions();

    public override GrBabylonJsObjectOptions ObjectOptions 
        => Options;


    public GrBabylonJsDisc(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsDisc(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsDisc SetOptions(GrBabylonJsDiscOptions options)
    {
        Options = new GrBabylonJsDiscOptions(options);

        return this;
    }

    public GrBabylonJsDisc SetProperties(GrBabylonJsMeshProperties properties)
    {
        Properties = new GrBabylonJsMeshProperties(properties);

        return this;
    }
}