using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateTorus-2
/// https://doc.babylonjs.com/features/featuresDeepDive/mesh/creation/set/torus
/// </summary>
public sealed class GrBabylonJsTorus :
    GrBabylonJsMesh
{
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateTorus";

    public GrBabylonJsTorusOptions Options { get; private set; }
        = new GrBabylonJsTorusOptions();

    public override GrBabylonJsObjectOptions ObjectOptions 
        => Options;


    public GrBabylonJsTorus(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsTorus(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsTorus SetOptions(GrBabylonJsTorusOptions options)
    {
        Options = new GrBabylonJsTorusOptions(options);

        return this;
    }

    public GrBabylonJsTorus SetProperties(GrBabylonJsMeshProperties properties)
    {
        Properties = new GrBabylonJsMeshProperties(properties);

        return this;
    }
}