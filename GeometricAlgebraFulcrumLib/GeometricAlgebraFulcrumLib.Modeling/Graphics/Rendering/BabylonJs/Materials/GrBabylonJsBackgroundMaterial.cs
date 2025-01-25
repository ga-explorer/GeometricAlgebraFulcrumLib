using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.BackgroundMaterial
/// </summary>
public sealed class GrBabylonJsBackgroundMaterial :
    GrBabylonJsMaterial
{
    protected override string ConstructorName
        => "new BABYLON.BackgroundMaterial";

    public GrBabylonJsBackgroundMaterialProperties Properties { get; private set; }
        = new GrBabylonJsBackgroundMaterialProperties();

    public override GrBabylonJsObjectProperties ObjectProperties
        => Properties;


    public GrBabylonJsBackgroundMaterial(string constName)
        : base(constName)
    {
    }

    public GrBabylonJsBackgroundMaterial(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
    }


    public GrBabylonJsBackgroundMaterial SetProperties(GrBabylonJsBackgroundMaterialProperties properties)
    {
        Properties = new GrBabylonJsBackgroundMaterialProperties(properties);

        return this;
    }
}