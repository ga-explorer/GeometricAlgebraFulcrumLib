using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.GridMaterial
/// </summary>
public sealed class GrBabylonJsGridMaterial :
    GrBabylonJsMaterial
{
    protected override string ConstructorName
        => "new BABYLON.GridMaterial";

    public GrBabylonJsGridMaterialProperties Properties { get; private set; }
        = new GrBabylonJsGridMaterialProperties();

    public override GrBabylonJsObjectProperties ObjectProperties
        => Properties;


    public GrBabylonJsGridMaterial(string constName)
        : base(constName)
    {
    }

    public GrBabylonJsGridMaterial(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
    }


    public GrBabylonJsGridMaterial SetProperties(GrBabylonJsGridMaterialProperties properties)
    {
        Properties = new GrBabylonJsGridMaterialProperties(properties);

        return this;
    }
}