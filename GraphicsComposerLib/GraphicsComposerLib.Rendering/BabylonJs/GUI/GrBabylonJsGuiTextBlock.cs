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
            public GrBabylonJsFloat32Value? LineSpacing
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("lineSpacing");
                set => SetAttributeValue("lineSpacing", value);
            }

            public GrBabylonJsStringValue? Text
            {
                get => GetAttributeValueOrNull<GrBabylonJsStringValue>("text");
                set => SetAttributeValue("text", value);
            }

            public GrBabylonJsHorizontalAlignmentValue? TextHorizontalAlignment
            {
                get => GetAttributeValueOrNull<GrBabylonJsHorizontalAlignmentValue>("textHorizontalAlignment");
                set => SetAttributeValue("textHorizontalAlignment", value);
            }

            public GrBabylonJsVerticalAlignmentValue? TextVerticalAlignment
            {
                get => GetAttributeValueOrNull<GrBabylonJsVerticalAlignmentValue>("textVerticalAlignment");
                set => SetAttributeValue("textVerticalAlignment", value);
            }

            public GrBabylonJsBooleanValue? TextWrapping
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("textWrapping");
                set => SetAttributeValue("textWrapping", value);
            }

            public GrBabylonJsBooleanValue? LineThrough
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("lineThrough");
                set => SetAttributeValue("lineThrough", value);
            }

            public GrBabylonJsBooleanValue? ResizeToFit
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("resizeToFit");
                set => SetAttributeValue("resizeToFit", value);
            }

            public GrBabylonJsBooleanValue? Underline
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("underline");
                set => SetAttributeValue("underline", value);
            }

            public GrBabylonJsStringValue? WordDivider
            {
                get => GetAttributeValueOrNull<GrBabylonJsStringValue>("wordDivider");
                set => SetAttributeValue("wordDivider", value);
            }

            public GrBabylonJsFloat32Value? OutlineWidth
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("outlineWidth");
                set => SetAttributeValue("outlineWidth", value);
            }

            public GrBabylonJsGuiColorValue? OutlineColor
            {
                get => GetAttributeValueOrNull<GrBabylonJsGuiColorValue>("outlineColor");
                set => SetAttributeValue("outlineColor", value);
            }


            public GuiTextBlockProperties()
            {
            }

            public GuiTextBlockProperties(GuiTextBlockProperties properties)
            {
                SetAttributeValues(properties);
            }
            
        }


        protected override string ConstructorName
            => "new BABYLON.GUI.TextBlock";

        public GuiTextBlockProperties Properties { get; private set; }
            = new GuiTextBlockProperties();

        public override GrBabylonJsObjectProperties ObjectProperties
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

        public GrBabylonJsGuiTextBlock SetProperties(GuiTextBlockProperties properties)
        {
            Properties = new GuiTextBlockProperties(properties);

            return this;
        }
    }
}