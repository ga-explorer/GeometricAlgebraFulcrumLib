using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.NormalMaterial
/// </summary>
public sealed class GrBabylonJsNormalMaterial :
    GrBabylonJsMaterial
{
    public sealed class NormalMaterialProperties :
        MaterialProperties
    {
        public GrBabylonJsColor3Value? DiffuseColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("diffuseColor");
            set => SetAttributeValue("diffuseColor", value);
        }
        
        public GrBabylonJsTextureValue? DiffuseTexture
        {
            get => GetAttributeValueOrNull<GrBabylonJsTextureValue>("diffuseTexture");
            set => SetAttributeValue("diffuseTexture", value);
        }
        
        public GrBabylonJsInt32Value? MaxSimultaneousLights
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("disableLighting");
            set => SetAttributeValue("disableLighting", value);
        }

        public GrBabylonJsBooleanValue? DisableLighting
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("maxSimultaneousLights");
            set => SetAttributeValue("maxSimultaneousLights", value);
        }


        public NormalMaterialProperties()
        {
        }

        public NormalMaterialProperties(NormalMaterialProperties properties)
        {
            SetAttributeValues(properties);
        }
    }


    protected override string ConstructorName
        => "new BABYLON.NormalMaterial";

    public NormalMaterialProperties Properties { get; private set; }
        = new NormalMaterialProperties();
    
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


    public GrBabylonJsNormalMaterial SetProperties(NormalMaterialProperties properties)
    {
        Properties = new NormalMaterialProperties(properties);

        return this;
    }
}