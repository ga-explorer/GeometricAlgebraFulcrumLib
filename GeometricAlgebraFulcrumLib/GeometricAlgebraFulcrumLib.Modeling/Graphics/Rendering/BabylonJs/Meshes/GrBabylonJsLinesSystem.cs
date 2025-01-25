using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateLineSystem-2
/// </summary>
public sealed class GrBabylonJsLineSystem :
    GrBabylonJsLinesMesh
{
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateLineSystem";

    public GrBabylonJsLinesSystemOptions Options { get; private set; }
        = new GrBabylonJsLinesSystemOptions();

    public override GrBabylonJsObjectOptions ObjectOptions 
        => Options;


    public GrBabylonJsLineSystem(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsLineSystem(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsLineSystem SetOptions(GrBabylonJsLinesSystemOptions options)
    {
        Options = new GrBabylonJsLinesSystemOptions(options);

        return this;
    }

    public GrBabylonJsLineSystem SetProperties(GrBabylonJsLinesMeshProperties properties)
    {
        Properties = new GrBabylonJsLinesMeshProperties(properties);

        return this;
    }
}