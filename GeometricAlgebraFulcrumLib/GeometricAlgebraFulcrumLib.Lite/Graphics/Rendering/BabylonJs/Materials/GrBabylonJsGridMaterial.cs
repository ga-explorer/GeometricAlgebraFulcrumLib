using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Materials;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.GridMaterial
/// </summary>
public sealed class GrBabylonJsGridMaterial :
    GrBabylonJsMaterial
{
    public sealed class GridMaterialProperties :
        MaterialProperties
    {
        public GrBabylonJsVector3Value? GridOffset
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("gridOffset");
            set => SetAttributeValue("gridOffset", value);
        }

        public GrBabylonJsFloat32Value? GridRatio
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("gridRatio");
            set => SetAttributeValue("gridRatio", value);
        }

        public GrBabylonJsColor3Value? LineColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("lineColor");
            set => SetAttributeValue("lineColor", value);
        }

        public GrBabylonJsColor3Value? MainColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("mainColor");
            set => SetAttributeValue("mainColor", value);
        }

        public GrBabylonJsFloat32Value? MajorUnitFrequency
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("majorUnitFrequency");
            set => SetAttributeValue("majorUnitFrequency", value);
        }

        public GrBabylonJsFloat32Value? MinorUnitVisibility
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("minorUnitVisibility");
            set => SetAttributeValue("minorUnitVisibility", value);
        }

        public GrBabylonJsFloat32Value? Opacity
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("opacity");
            set => SetAttributeValue("opacity", value);
        }

        public GrBabylonJsTextureValue? OpacityTexture
        {
            get => GetAttributeValueOrNull<GrBabylonJsTextureValue>("opacityTexture");
            set => SetAttributeValue("opacityTexture", value);
        }

        public GrBabylonJsBooleanValue? PreMultiplyAlpha
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("preMultiplyAlpha");
            set => SetAttributeValue("preMultiplyAlpha", value);
        }

        public GrBabylonJsBooleanValue? UseMaxLine
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useMaxLine");
            set => SetAttributeValue("useMaxLine", value);
        }


        public GridMaterialProperties()
        {
        }

        public GridMaterialProperties(GridMaterialProperties properties)
        {
            SetAttributeValues(properties);
        }
    }


    protected override string ConstructorName
        => "new BABYLON.GridMaterial";

    public GridMaterialProperties Properties { get; private set; }
        = new GridMaterialProperties();

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


    public GrBabylonJsGridMaterial SetProperties(GridMaterialProperties properties)
    {
        Properties = new GridMaterialProperties(properties);

        return this;
    }
}