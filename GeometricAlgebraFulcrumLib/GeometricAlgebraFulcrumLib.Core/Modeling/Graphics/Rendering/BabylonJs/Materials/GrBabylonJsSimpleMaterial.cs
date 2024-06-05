using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Materials;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.SimpleMaterial
/// </summary>
public sealed class GrBabylonJsSimpleMaterial :
    GrBabylonJsMaterial
{
    public sealed class SimpleMaterialProperties :
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
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("maxSimultaneousLights");
            set => SetAttributeValue("maxSimultaneousLights", value);
        }

        public GrBabylonJsBooleanValue? DisableLighting
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("disableLighting");
            set => SetAttributeValue("disableLighting", value);
        }


        public SimpleMaterialProperties()
        {
        }

        public SimpleMaterialProperties(SimpleMaterialProperties properties)
        {
            SetAttributeValues(properties);
        }
    }


    protected override string ConstructorName
        => "new BABYLON.SimpleMaterial";

    public SimpleMaterialProperties Properties { get; private set; }
        = new SimpleMaterialProperties();
    
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


    public GrBabylonJsSimpleMaterial SetProperties(SimpleMaterialProperties properties)
    {
        Properties = new SimpleMaterialProperties(properties);

        return this;
    }
}