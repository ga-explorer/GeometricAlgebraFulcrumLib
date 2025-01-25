using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.GradientMaterial
/// </summary>
public sealed class GrBabylonJsGradientMaterial :
    GrBabylonJsMaterial
{
    protected override string ConstructorName
        => "new BABYLON.GradientMaterial";

    public GrBabylonJsGradientMaterialProperties Properties { get; private set; }
        = new GrBabylonJsGradientMaterialProperties();
    
    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;


    public GrBabylonJsGradientMaterial(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsGradientMaterial(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsGradientMaterial SetProperties(GrBabylonJsGradientMaterialProperties properties)
    {
        Properties = new GrBabylonJsGradientMaterialProperties(properties);

        return this;
    }
}