using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Materials;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.HandleMaterial
/// </summary>
public sealed class GrBabylonJsHandleMaterial :
    GrBabylonJsMaterial
{
    public sealed class HandleMaterialProperties :
        MaterialProperties
    {
        public GrBabylonJsFloat32Value? AnimationLength
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("animationLength");
            set => SetAttributeValue("animationLength", value);
        }

        public GrBabylonJsColor3Value? BaseColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("baseColor");
            set => SetAttributeValue("baseColor", value);
        }

        public GrBabylonJsFloat32Value? BaseScale
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("baseScale");
            set => SetAttributeValue("baseScale", value);
        }

        public GrBabylonJsFloat32Value? DragScale
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("dragScale");
            set => SetAttributeValue("dragScale", value);
        }

        public GrBabylonJsColor3Value? HoverColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("hoverColor");
            set => SetAttributeValue("hoverColor", value);
        }

        public GrBabylonJsFloat32Value? HoverScale
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("hoverScale");
            set => SetAttributeValue("hoverScale", value);
        }

        public GrBabylonJsFloat32Value? Hover
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("hover");
            set => SetAttributeValue("hover", value);
        }

        public GrBabylonJsFloat32Value? Drag
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("drag");
            set => SetAttributeValue("drag", value);
        }


        public HandleMaterialProperties()
        {

        }

        public HandleMaterialProperties(HandleMaterialProperties properties)
        {
            SetAttributeValues(properties);
        }
    }


    protected override string ConstructorName
        => "new BABYLON.HandleMaterial";

    public HandleMaterialProperties Properties { get; private set; }
        = new HandleMaterialProperties();
    
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


    public GrBabylonJsHandleMaterial SetProperties(HandleMaterialProperties properties)
    {
        Properties = new HandleMaterialProperties(properties);

        return this;
    }
}