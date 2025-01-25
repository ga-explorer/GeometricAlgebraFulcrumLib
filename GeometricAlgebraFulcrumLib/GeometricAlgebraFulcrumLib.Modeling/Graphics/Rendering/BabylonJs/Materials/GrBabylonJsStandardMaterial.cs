using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.StandardMaterial
/// </summary>
public sealed class GrBabylonJsStandardMaterial :
    GrBabylonJsMaterial
{
    protected override string ConstructorName
        => "new BABYLON.StandardMaterial";

    public GrBabylonJsStandardMaterialProperties Properties { get; private set; }
        = new GrBabylonJsStandardMaterialProperties();

    public override GrBabylonJsObjectProperties ObjectProperties
        => Properties;


    public GrBabylonJsStandardMaterial(string constName)
        : base(constName)
    {
    }

    public GrBabylonJsStandardMaterial(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
    }


    public GrBabylonJsStandardMaterial SetProperties(GrBabylonJsStandardMaterialProperties properties)
    {
        Properties = new GrBabylonJsStandardMaterialProperties(properties);

        return this;
    }
}