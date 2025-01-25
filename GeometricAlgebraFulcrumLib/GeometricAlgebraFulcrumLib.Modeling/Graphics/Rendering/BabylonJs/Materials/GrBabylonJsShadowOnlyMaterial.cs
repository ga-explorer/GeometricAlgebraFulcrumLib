using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.ShadowOnlyMaterial
/// </summary>
public sealed class GrBabylonJsShadowOnlyMaterial :
    GrBabylonJsMaterial
{
    protected override string ConstructorName
        => "new BABYLON.ShadowOnlyMaterial";

    public GrBabylonJsShadowOnlyMaterialProperties Properties { get; private set; }
        = new GrBabylonJsShadowOnlyMaterialProperties();
    
    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;


    public GrBabylonJsShadowOnlyMaterial(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsShadowOnlyMaterial(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsShadowOnlyMaterial SetProperties(GrBabylonJsShadowOnlyMaterialProperties properties)
    {
        Properties = new GrBabylonJsShadowOnlyMaterialProperties(properties);

        return this;
    }
}