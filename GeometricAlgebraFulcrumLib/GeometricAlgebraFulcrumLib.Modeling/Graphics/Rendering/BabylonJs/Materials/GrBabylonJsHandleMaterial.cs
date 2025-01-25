using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.HandleMaterial
/// </summary>
public sealed class GrBabylonJsHandleMaterial :
    GrBabylonJsMaterial
{
    protected override string ConstructorName
        => "new BABYLON.HandleMaterial";

    public GrBabylonJsHandleMaterialProperties Properties { get; private set; }
        = new GrBabylonJsHandleMaterialProperties();
    
    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;


    public GrBabylonJsHandleMaterial(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsHandleMaterial(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsHandleMaterial SetProperties(GrBabylonJsHandleMaterialProperties properties)
    {
        Properties = new GrBabylonJsHandleMaterialProperties(properties);

        return this;
    }
}