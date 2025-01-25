using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateLines-2
/// </summary>
public sealed class GrBabylonJsLines :
    GrBabylonJsLinesMesh
{
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateLines";

    public GrBabylonJsLinesOptions Options { get; private set; }
        = new GrBabylonJsLinesOptions();

    public override GrBabylonJsObjectOptions ObjectOptions 
        => Options;


    public GrBabylonJsLines(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsLines(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsLines SetOptions(GrBabylonJsLinesOptions options)
    {
        Options = new GrBabylonJsLinesOptions(options);

        return this;
    }

    public GrBabylonJsLines SetProperties(GrBabylonJsLinesMeshProperties properties)
    {
        Properties = new GrBabylonJsLinesMeshProperties(properties);

        return this;
    }
}