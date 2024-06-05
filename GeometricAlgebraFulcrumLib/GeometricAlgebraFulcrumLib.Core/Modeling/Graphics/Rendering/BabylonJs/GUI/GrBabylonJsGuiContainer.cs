using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.GUI;

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
        public GrBabylonJsBooleanValue? LogLayoutCycleErrors
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("logLayoutCycleErrors");
            set => SetAttributeValue("logLayoutCycleErrors", value);
        }

        public GrBabylonJsInt32Value? MaxLayoutCycle
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("maxLayoutCycle");
            set => SetAttributeValue("maxLayoutCycle", value);
        }

        public GrBabylonJsBooleanValue? AdaptHeightToChildren
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("adaptHeightToChildren");
            set => SetAttributeValue("adaptHeightToChildren", value);
        }

        public GrBabylonJsBooleanValue? AdaptWidthToChildren
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("adaptWidthToChildren");
            set => SetAttributeValue("adaptWidthToChildren", value);
        }

        public GrBabylonJsBooleanValue? RenderToIntermediateTexture
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("renderToIntermediateTexture");
            set => SetAttributeValue("renderToIntermediateTexture", value);
        }

        public GrBabylonJsGuiColorValue? BackgroundColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsGuiColorValue>("backgroundColor");
            set => SetAttributeValue("backgroundColor", value);
        }


        protected GuiContainerProperties()
        {

        }

        protected GuiContainerProperties(GuiContainerProperties properties)
        {
            SetAttributeValues(properties);
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
        {
            var declarationKeyword = UseLetDeclaration ? "let" : "const";

            composer.Append($"{declarationKeyword} {ConstName} = ");
        }

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