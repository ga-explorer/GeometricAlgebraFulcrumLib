using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.SimpleMaterial
/// </summary>
public sealed class GrBabylonJsSimpleMaterial :
    GrBabylonJsMaterial
{
    protected override string ConstructorName
        => "new BABYLON.SimpleMaterial";

    public GrBabylonJsSimpleMaterialProperties Properties { get; private set; }
        = new GrBabylonJsSimpleMaterialProperties();
    
    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;


    public GrBabylonJsSimpleMaterial(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsSimpleMaterial(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsSimpleMaterial SetProperties(GrBabylonJsSimpleMaterialProperties properties)
    {
        Properties = new GrBabylonJsSimpleMaterialProperties(properties);

        return this;
    }
}