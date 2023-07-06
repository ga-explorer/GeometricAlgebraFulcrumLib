using GraphicsComposerLib.Rendering.BabylonJs.Values;
using TextComposerLib;

namespace GraphicsComposerLib.Rendering.BabylonJs.GUI
{
    /// <summary>
    /// https://doc.babylonjs.com/typedoc/classes/BABYLON.GUI.Line
    /// </summary>
    public sealed class GrBabylonJsGuiLine :
        GrBabylonJsGuiControl
    {
        public sealed class GuiLineProperties :
            GuiControlProperties
        {
            public GrBabylonJsControlValue? ConnectedControl
            {
                get => GetAttributeValueOrNull<GrBabylonJsControlValue>("connectedControl");
                set => SetAttributeValue("connectedControl", value);
            }

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

            public GrBabylonJsFloat32Value? X1
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("x1");
                set => SetAttributeValue("x1", value);
            }

            public GrBabylonJsFloat32Value? Y1
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("y1");
                set => SetAttributeValue("y1", value);
            }

            public GrBabylonJsFloat32Value? X2
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("x2");
                set => SetAttributeValue("x2", value);
            }

            public GrBabylonJsFloat32Value? Y2
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("y2");
                set => SetAttributeValue("y2", value);
            }


            public GuiLineProperties()
            {
            }

            public GuiLineProperties(GuiLineProperties properties)
            {
                SetAttributeValues(properties);
            }
        }


        protected override string ConstructorName 
            => "new BABYLON.GUI.Line";

        public GuiLineProperties Properties { get; private set; }
            = new GuiLineProperties();

        public override GrBabylonJsObjectProperties ObjectProperties 
            => Properties;
    

        public GrBabylonJsGuiLine(string constName, IGrBabylonJsGuiControlContainer parentContainer) 
            : base(constName, parentContainer)
        {
        }


        protected override IEnumerable<string> GetConstructorArguments()
        {
            yield return ConstName.DoubleQuote();
        }
    
        public GrBabylonJsGuiLine SetProperties(GuiLineProperties properties)
        {
            Properties = new GuiLineProperties(properties);

            return this;
        }
    }
}