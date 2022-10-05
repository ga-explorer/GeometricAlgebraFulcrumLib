using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Rendering.BabylonJs.GUI;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.GUI.Container
/// </summary>
public abstract class GrBabylonJsGuiContainer :
    GrBabylonJsGuiControl,
    IGrBabylonJsGuiControlContainer
{
    public abstract class GuiContainerProperties :
        GuiControlProperties
    {
        public GrBabylonJsBooleanValue? LogLayoutCycleErrors { get; set; }

        public GrBabylonJsInt32Value? MaxLayoutCycle { get; set; }

        public GrBabylonJsBooleanValue? AdaptHeightToChildren { get; set; }

        public GrBabylonJsBooleanValue? AdaptWidthToChildren { get; set; }

        public GrBabylonJsBooleanValue? RenderToIntermediateTexture { get; set; }

        public GrBabylonJsGuiColorValue? BackgroundColor { get; set; }


        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            foreach (var pair in base.GetNameValuePairs())
                yield return pair;

            yield return LogLayoutCycleErrors.GetNameValueCodePair("logLayoutCycleErrors");
            yield return MaxLayoutCycle.GetNameValueCodePair("maxLayoutCycle");
            yield return AdaptHeightToChildren.GetNameValueCodePair("adaptHeightToChildren");
            yield return AdaptWidthToChildren.GetNameValueCodePair("adaptWidthToChildren");
            yield return RenderToIntermediateTexture.GetNameValueCodePair("renderToIntermediateTexture");
            yield return BackgroundColor.GetNameValueCodePair("background");
        }
    }

    
    public GrBabylonJsGuiControlList ControlList { get; } 
        = new GrBabylonJsGuiControlList();


    protected GrBabylonJsGuiContainer(string constName, IGrBabylonJsGuiControlContainer parentContainer) 
        : base(constName, parentContainer)
    {
    }

    public override string GetCode()
    {
        var composer = new LinearTextComposer();
        
        if (!string.IsNullOrEmpty(ConstName))
            composer.Append($"const {ConstName} = ");

        var constructorCode = GetConstructorCode();
        var propertiesCode = GetPropertiesCode();

        composer
            .AppendLine(constructorCode)
            .AppendAtNewLine(propertiesCode);

        if (ControlList.Count > 0)
        {
            composer.AppendEmptyLines(1);

            foreach (var control in ControlList)
                composer
                    .AppendAtNewLine(control.GetCode())
                    .AppendLineAtNewLine($"{ConstName}.addControl({control.ConstName});")
                    .AppendLine();
        }

        return composer.ToString();
    }
}