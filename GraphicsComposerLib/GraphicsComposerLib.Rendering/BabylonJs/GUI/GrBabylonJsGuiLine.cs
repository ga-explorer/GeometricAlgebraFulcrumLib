using DataStructuresLib.Basic;
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
            public GrBabylonJsControlValue? ConnectedControl { get; set; }

            public GrBabylonJsInt32ArrayValue? Dash { get; set; }

            public GrBabylonJsFloat32Value? LineWidth { get; set; }

            public GrBabylonJsFloat32Value? X1 { get; set; }

            public GrBabylonJsFloat32Value? Y1 { get; set; }

            public GrBabylonJsFloat32Value? X2 { get; set; }

            public GrBabylonJsFloat32Value? Y2 { get; set; }
        

            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                foreach (var pair in base.GetNameValuePairs())
                    yield return pair;

                yield return Color.GetNameValueCodePair("color");
                yield return ConnectedControl.GetNameValueCodePair("connectedControl");
                yield return Dash.GetNameValueCodePair("dash");
                yield return LineWidth.GetNameValueCodePair("lineWidth");
                yield return X1.GetNameValueCodePair("x1");
                yield return Y1.GetNameValueCodePair("y1");
                yield return X2.GetNameValueCodePair("x2");
                yield return Y2.GetNameValueCodePair("y2");
            }
        }


        protected override string ConstructorName 
            => "new BABYLON.GUI.Line";

        public GuiLineProperties? Properties { get; private set; }
            = new GuiLineProperties();

        public override GrBabylonJsObjectProperties? ObjectProperties 
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
            Properties = properties;

            return this;
        }
    }
}