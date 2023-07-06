using GraphicsComposerLib.Rendering.BabylonJs.Values;
using TextComposerLib;

namespace GraphicsComposerLib.Rendering.BabylonJs.GUI
{
    /// <summary>
    /// https://doc.babylonjs.com/typedoc/classes/BABYLON.GUI.MultiLine
    /// </summary>
    public sealed class GrBabylonJsGuiMultiLine :
        GrBabylonJsGuiControl
    {
        public sealed class GuiMultiLineProperties :
            GuiControlProperties
        {
            public GrBabylonJsInt32ArrayValue? Dash
            {
                get => GetAttributeValueOrNull<GrBabylonJsInt32ArrayValue>("dash");
                set => SetAttributeValue("dash", value);
            }

            public GrBabylonJsFloat32Value? LineWidth
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("lineWidth");
                set => SetAttributeValue("lineWidth", value);
            }


            public GuiMultiLineProperties()
            {
            }

            public GuiMultiLineProperties(GuiMultiLineProperties properties)
            {
                SetAttributeValues(properties);
            }
        }


        protected override string ConstructorName 
            => "new BABYLON.GUI.MultiLine";

        public GuiMultiLineProperties Properties { get; private set; }
            = new GuiMultiLineProperties();

        public override GrBabylonJsObjectProperties ObjectProperties 
            => Properties;
    

        public GrBabylonJsGuiMultiLine(string constName, IGrBabylonJsGuiControlContainer parentContainer) 
            : base(constName, parentContainer)
        {
        }


        protected override IEnumerable<string> GetConstructorArguments()
        {
            yield return ConstName.DoubleQuote();
        }
    
        public GrBabylonJsGuiMultiLine SetProperties(GuiMultiLineProperties properties)
        {
            Properties = new GuiMultiLineProperties(properties);

            return this;
        }
    }
}