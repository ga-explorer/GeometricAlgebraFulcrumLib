using DataStructuresLib.Basic;
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
            public GrBabylonJsInt32ArrayValue? Dash { get; set; }

            public GrBabylonJsFloat32Value? LineWidth { get; set; }
        

            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                foreach (var pair in base.GetNameValuePairs())
                    yield return pair;

                yield return Color.GetNameValueCodePair("color");
                yield return Dash.GetNameValueCodePair("dash");
                yield return LineWidth.GetNameValueCodePair("lineWidth");
            }
        }


        protected override string ConstructorName 
            => "new BABYLON.GUI.MultiLine";

        public GuiMultiLineProperties? Properties { get; private set; }
            = new GuiMultiLineProperties();

        public override GrBabylonJsObjectProperties? ObjectProperties 
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
            Properties = properties;

            return this;
        }
    }
}