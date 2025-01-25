using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateDashedLines-2
/// </summary>
public sealed class GrBabylonJsDashedLines :
    GrBabylonJsLinesMesh
{
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateDashedLines";

    public GrBabylonJsDashedLinesOptions Options { get; private set; }
        = new GrBabylonJsDashedLinesOptions();

    public override GrBabylonJsObjectOptions ObjectOptions 
        => Options;


    public GrBabylonJsDashedLines(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsDashedLines(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsDashedLines SetOptions(GrBabylonJsDashedLinesOptions options)
    {
        Options = new GrBabylonJsDashedLinesOptions(options);

        return this;
    }

    public GrBabylonJsDashedLines SetProperties(GrBabylonJsLinesMeshProperties properties)
    {
        Properties = new GrBabylonJsLinesMeshProperties(properties);

        return this;
    }

}