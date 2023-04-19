using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;
using TextComposerLib;

namespace GraphicsComposerLib.Rendering.BabylonJs.GUI
{
    /// <summary>
    /// https://doc.babylonjs.com/typedoc/classes/BABYLON.GUI.TextBlock
    /// </summary>
    public sealed class GrBabylonJsGuiTextBlock :
        GrBabylonJsGuiControl
    {
        public sealed class GuiTextBlockProperties :
            GuiControlProperties
        {
            public GrBabylonJsFloat32Value? LineSpacing { get; set; }
        
            public GrBabylonJsStringValue? Text { get; set; }

            public GrBabylonJsHorizontalAlignmentValue? TextHorizontalAlignment { get; set; }

            public GrBabylonJsVerticalAlignmentValue? TextVerticalAlignment { get; set; }

            public GrBabylonJsBooleanValue? TextWrapping { get; set; }

            public GrBabylonJsBooleanValue? LineThrough { get; set; }

            public GrBabylonJsBooleanValue? ResizeToFit { get; set; }

            public GrBabylonJsBooleanValue? Underline { get; set; }

            public GrBabylonJsStringValue? WordDivider { get; set; }
        
            public GrBabylonJsFloat32Value? OutlineWidth { get; set; }
        
            public GrBabylonJsGuiColorValue? OutlineColor { get; set; }
        

            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                foreach (var pair in base.GetNameValuePairs())
                    yield return pair;

                yield return LineSpacing.GetNameValueCodePair("lineSpacing");
                yield return Text.GetNameValueCodePair("text");
                yield return TextHorizontalAlignment.GetNameValueCodePair("textHorizontalAlignment");
                yield return TextVerticalAlignment.GetNameValueCodePair("textVerticalAlignment");
                yield return TextWrapping.GetNameValueCodePair("textWrapping");
                yield return LineThrough.GetNameValueCodePair("lineThrough");
                yield return ResizeToFit.GetNameValueCodePair("resizeToFit");
                yield return Underline.GetNameValueCodePair("underline");
                yield return WordDivider.GetNameValueCodePair("wordDivider");
                yield return OutlineWidth.GetNameValueCodePair("outlineWidth");
                yield return OutlineColor.GetNameValueCodePair("outlineColor");
            }
        }


        protected override string ConstructorName 
            => "new BABYLON.GUI.TextBlock";

        public GuiTextBlockProperties? Properties { get; private set; }
            = new GuiTextBlockProperties();

        public override GrBabylonJsObjectProperties? ObjectProperties 
            => Properties;

        public GrBabylonJsStringValue? Text { get; set; }


        public GrBabylonJsGuiTextBlock(string constName, IGrBabylonJsGuiControlContainer parentContainer) 
            : base(constName, parentContainer)
        {
        }


        protected override IEnumerable<string> GetConstructorArguments()
        {
            yield return ConstName.DoubleQuote();

            if (Text is null || Text.IsEmpty) yield break;
            yield return Text.GetCode();
        }
    
        public GrBabylonJsGuiTextBlock SetProperties([NotNull] GuiTextBlockProperties? properties)
        {
            Properties = properties;

            return this;
        }
    }
}