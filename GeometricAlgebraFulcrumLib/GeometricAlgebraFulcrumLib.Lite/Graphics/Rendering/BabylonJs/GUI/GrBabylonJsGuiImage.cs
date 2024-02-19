using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;
using TextComposerLib;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.GUI;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.GUI.Image
/// </summary>
public sealed class GrBabylonJsGuiImage :
    GrBabylonJsGuiControl
{
    public sealed class GuiImageProperties :
        GuiControlProperties
    {
        public GrBabylonJsBooleanValue? AutoScale
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("autoScale");
            set => SetAttributeValue("autoScale", value);
        }

        public GrBabylonJsFloat32Value? CellWidth
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("cellWidth");
            set => SetAttributeValue("cellWidth", value);
        }

        public GrBabylonJsFloat32Value? CellHeight
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("cellHeight");
            set => SetAttributeValue("cellHeight", value);
        }

        public GrBabylonJsInt32Value? CellId
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("cellId");
            set => SetAttributeValue("cellId", value);
        }

        public GrBabylonJsBooleanValue? DetectPointerOnOpaqueOnly
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("detectPointerOnOpaqueOnly");
            set => SetAttributeValue("detectPointerOnOpaqueOnly", value);
        }

        public GrBabylonJsBooleanValue? PopulateNinePatchSlicesFromImage
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("populateNinePatchSlicesFromImage");
            set => SetAttributeValue("populateNinePatchSlicesFromImage", value);
        }

        public GrBabylonJsCodeValue? DomImage
        {
            get => GetAttributeValueOrNull<GrBabylonJsCodeValue>("domImage");
            set => SetAttributeValue("domImage", value);
        }

        public GrBabylonJsFloat32Value? SliceBottom
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("sliceBottom");
            set => SetAttributeValue("sliceBottom", value);
        }

        public GrBabylonJsFloat32Value? SliceTop
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("sliceTop");
            set => SetAttributeValue("sliceTop", value);
        }

        public GrBabylonJsFloat32Value? SliceLeft
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("sliceLeft");
            set => SetAttributeValue("sliceLeft", value);
        }

        public GrBabylonJsFloat32Value? SliceRight
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("sliceRight");
            set => SetAttributeValue("sliceRight", value);
        }

        public GrBabylonJsStringValue? Source
        {
            get => GetAttributeValueOrNull<GrBabylonJsStringValue>("source");
            set => SetAttributeValue("source", value);
        }

        public GrBabylonJsFloat32Value? SourceTop
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("sourceTop");
            set => SetAttributeValue("sourceTop", value);
        }

        public GrBabylonJsFloat32Value? SourceLeft
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("sourceLeft");
            set => SetAttributeValue("sourceLeft", value);
        }

        public GrBabylonJsFloat32Value? SourceWidth
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("sourceWidth");
            set => SetAttributeValue("sourceWidth", value);
        }

        public GrBabylonJsFloat32Value? SourceHeight
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("sourceHeight");
            set => SetAttributeValue("sourceHeight", value);
        }

        public GrBabylonJsImageStretchValue? Stretch
        {
            get => GetAttributeValueOrNull<GrBabylonJsImageStretchValue>("stretch");
            set => SetAttributeValue("stretch", value);
        }


        public GuiImageProperties()
        {

        }

        public GuiImageProperties(GuiImageProperties properties)
        {
            SetAttributeValues(properties);
        }
            
    }


    protected override string ConstructorName
        => "new BABYLON.GUI.Image";

    public GuiImageProperties Properties { get; private set; }
        = new GuiImageProperties();

    public override GrBabylonJsObjectProperties ObjectProperties
        => Properties;

    public GrBabylonJsStringValue Url { get; set; }


    public GrBabylonJsGuiImage(string constName, IGrBabylonJsGuiControlContainer parentContainer)
        : base(constName, parentContainer)
    {
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();

        if (Url.IsNullOrEmpty()) yield break;
        yield return Url.GetCode();
    }

    public GrBabylonJsGuiImage SetProperties(GuiImageProperties properties)
    {
        Properties = new GuiImageProperties(properties);

        return this;
    }
}