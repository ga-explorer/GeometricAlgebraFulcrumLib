using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;
using TextComposerLib;

namespace GraphicsComposerLib.Rendering.BabylonJs.GUI;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.GUI.Image
/// </summary>
public sealed class GrBabylonJsGuiImage :
    GrBabylonJsGuiControl
{
    public sealed class GuiImageProperties :
        GuiControlProperties
    {
        public GrBabylonJsBooleanValue? AutoScale { get; set; }
        
        public GrBabylonJsFloat32Value? CellWidth { get; set; }

        public GrBabylonJsFloat32Value? CellHeight { get; set; }

        public GrBabylonJsInt32Value? CellId { get; set; }

        public GrBabylonJsBooleanValue? DetectPointerOnOpaqueOnly { get; set; }

        public GrBabylonJsBooleanValue? PopulateNinePatchSlicesFromImage { get; set; }

        public GrBabylonJsCodeValue? DomImage { get; set; }

        public GrBabylonJsFloat32Value? SliceBottom { get; set; }
        
        public GrBabylonJsFloat32Value? SliceTop { get; set; }

        public GrBabylonJsFloat32Value? SliceLeft { get; set; }
        
        public GrBabylonJsFloat32Value? SliceRight { get; set; }
        
        public GrBabylonJsStringValue? Source { get; set; }

        public GrBabylonJsFloat32Value? SourceTop { get; set; }

        public GrBabylonJsFloat32Value? SourceLeft { get; set; }

        public GrBabylonJsFloat32Value? SourceWidth { get; set; }

        public GrBabylonJsFloat32Value? SourceHeight { get; set; }
        
        public GrBabylonJsImageStretchValue? Stretch { get; set; }
        

        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            foreach (var pair in base.GetNameValuePairs())
                yield return pair;

            yield return AutoScale.GetNameValueCodePair("autoScale");
            yield return CellWidth.GetNameValueCodePair("cellWidth");
            yield return CellHeight.GetNameValueCodePair("cellHeight");
            yield return CellId.GetNameValueCodePair("cellId");
            yield return DetectPointerOnOpaqueOnly.GetNameValueCodePair("detectPointerOnOpaqueOnly");
            yield return PopulateNinePatchSlicesFromImage.GetNameValueCodePair("populateNinePatchSlicesFromImage");
            yield return DomImage.GetNameValueCodePair("domImage");
            yield return SliceBottom.GetNameValueCodePair("sliceBottom");
            yield return SliceTop.GetNameValueCodePair("sliceTop");
            yield return SliceLeft.GetNameValueCodePair("sliceLeft");
            yield return SliceRight.GetNameValueCodePair("sliceRight");
            yield return Source.GetNameValueCodePair("source");
            yield return SourceTop.GetNameValueCodePair("sourceTop");
            yield return SourceLeft.GetNameValueCodePair("sourceLeft");
            yield return SourceWidth.GetNameValueCodePair("sourceWidth");
            yield return SourceHeight.GetNameValueCodePair("sourceHeight");
            yield return Stretch.GetNameValueCodePair("stretch");
        }
    }


    protected override string ConstructorName 
        => "new BABYLON.GUI.Image";

    public GuiImageProperties? Properties { get; private set; }
        = new GuiImageProperties();

    public override GrBabylonJsObjectProperties? ObjectProperties 
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
    
    public GrBabylonJsGuiImage SetProperties([NotNull] GuiImageProperties? properties)
    {
        Properties = properties;

        return this;
    }
}