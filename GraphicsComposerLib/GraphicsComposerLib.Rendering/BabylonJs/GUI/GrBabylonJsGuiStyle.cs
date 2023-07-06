using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.GUI
{
    /// <summary>
    /// https://doc.babylonjs.com/typedoc/classes/BABYLON.GUI.Style
    /// </summary>
    public class GrBabylonJsGuiStyle :
        GrBabylonJsObject
    {
        public sealed class GuiStyleProperties :
            GrBabylonJsObjectProperties
        {
            public GrBabylonJsStringValue? FontFamily
            {
                get => GetAttributeValueOrNull<GrBabylonJsStringValue>("fontWeight");
                set => SetAttributeValue("fontWeight", value);
            }

            public GrBabylonJsFloat32Value? FontSize
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("fontSize");
                set => SetAttributeValue("fontSize", value);
            }

            public GrBabylonJsStringValue? FontStyle
            {
                get => GetAttributeValueOrNull<GrBabylonJsStringValue>("fontStyle");
                set => SetAttributeValue("fontStyle", value);
            }

            public GrBabylonJsStringValue? FontWeight
            {
                get => GetAttributeValueOrNull<GrBabylonJsStringValue>("fontWeight");
                set => SetAttributeValue("fontWeight", value);
            }


            public GuiStyleProperties()
            {
            }

            public GuiStyleProperties(GuiStyleProperties properties)
            {
                SetAttributeValues(properties);
            }
        }

        protected override string ConstructorName 
            => "new BABYLON.GUI.Style";

        public GrBabylonJsAdvancedDynamicTextureValue Host { get; set; }

        public GuiStyleProperties Properties { get; private set; }
            = new GuiStyleProperties();

        public override GrBabylonJsObjectOptions? ObjectOptions 
            => null;

        public override GrBabylonJsObjectProperties ObjectProperties 
            => Properties;


        public GrBabylonJsGuiStyle(string constName) 
            : base(constName)
        {
        }


        protected override IEnumerable<string> GetConstructorArguments()
        {
            yield return Host.ToString();
        }

        public GrBabylonJsGuiStyle SetProperties(GuiStyleProperties properties)
        {
            Properties = new GuiStyleProperties(properties);

            return this;
        }
    }
}