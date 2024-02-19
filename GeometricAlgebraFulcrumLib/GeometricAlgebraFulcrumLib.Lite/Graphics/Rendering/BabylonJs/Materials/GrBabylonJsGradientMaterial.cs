using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Materials;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.GradientMaterial
/// </summary>
public sealed class GrBabylonJsGradientMaterial :
    GrBabylonJsMaterial
{
    public sealed class GradientMaterialProperties :
        MaterialProperties
    {
        public GrBabylonJsColor3Value? BottomColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("bottomColor");
            set => SetAttributeValue("bottomColor", value);
        }

        public GrBabylonJsColor3Value? TopColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("topColor");
            set => SetAttributeValue("topColor", value);
        }

        public GrBabylonJsFloat32Value? BottomColorAlpha
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("bottomColorAlpha");
            set => SetAttributeValue("bottomColorAlpha", value);
        }

        public GrBabylonJsFloat32Value? TopColorAlpha
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("topColorAlpha");
            set => SetAttributeValue("topColorAlpha", value);
        }

        public GrBabylonJsFloat32Value? Offset
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("offset");
            set => SetAttributeValue("offset", value);
        }

        public GrBabylonJsFloat32Value? Scale
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("scale");
            set => SetAttributeValue("scale", value);
        }

        public GrBabylonJsFloat32Value? Smoothness
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("smoothness");
            set => SetAttributeValue("smoothness", value);
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


        public GradientMaterialProperties()
        {
        }

        public GradientMaterialProperties(GradientMaterialProperties properties)
        {
            SetAttributeValues(properties);
        }
    }


    protected override string ConstructorName
        => "new BABYLON.GradientMaterial";

    public GradientMaterialProperties Properties { get; private set; }
        = new GradientMaterialProperties();
    
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


    public GrBabylonJsGradientMaterial SetProperties(GradientMaterialProperties properties)
    {
        Properties = new GradientMaterialProperties(properties);

        return this;
    }
}