using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.GUI
{
    /// <summary>
    /// https://doc.babylonjs.com/typedoc/classes/BABYLON.GUI.StackPanel
    /// </summary>
    public sealed class GrBabylonJsGuiStackPanel : 
        GrBabylonJsGuiContainer
    {
        public class GuiStackPanelProperties :
            GuiContainerProperties
        {
            public GrBabylonJsBooleanValue? IgnoreLayoutWarnings { get; set; }

            public GrBabylonJsBooleanValue? IsVertical { get; set; }

            public GrBabylonJsFloat32Value? Spacing { get; set; }


            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                foreach (var pair in base.GetNameValuePairs())
                    yield return pair;

                yield return IgnoreLayoutWarnings.GetNameValueCodePair("ignoreLayoutWarnings");
                yield return IsVertical.GetNameValueCodePair("isVertical");
                yield return Spacing.GetNameValueCodePair("spacing");
            }
        }


        protected override string ConstructorName
            => "new BABYLON.GUI.StackPanel";

        public GuiStackPanelProperties? Properties { get; private set; }

        public override GrBabylonJsObjectProperties? ObjectProperties 
            => Properties;


        public GrBabylonJsGuiStackPanel(string constName, IGrBabylonJsGuiControlContainer parentContainer) 
            : base(constName, parentContainer)
        {
        }


        public GrBabylonJsGuiStackPanel SetProperties([NotNull] GuiStackPanelProperties? properties)
        {
            Properties = properties;

            return this;
        }
    }
}