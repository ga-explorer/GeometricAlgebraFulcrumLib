using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.NormalMaterial
/// </summary>
public sealed class GrBabylonJsNormalMaterial :
    GrBabylonJsMaterial
{
    protected override string ConstructorName
        => "new BABYLON.NormalMaterial";

    public GrBabylonJsNormalMaterialProperties Properties { get; private set; }
        = new GrBabylonJsNormalMaterialProperties();
    
    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;
    

    public GrBabylonJsNormalMaterial(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsNormalMaterial(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsNormalMaterial SetProperties(GrBabylonJsNormalMaterialProperties properties)
    {
        Properties = new GrBabylonJsNormalMaterialProperties(properties);

        return this;
    }
}