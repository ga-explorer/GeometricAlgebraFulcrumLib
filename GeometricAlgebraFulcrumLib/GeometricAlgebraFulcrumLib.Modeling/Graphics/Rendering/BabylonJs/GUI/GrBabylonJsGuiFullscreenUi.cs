using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;

public class GrBabylonJsGuiFullScreenUi :
    GrBabylonJsAdvancedDynamicTexture,
    IGrBabylonJsGuiControlContainer
{
    protected override string ConstructorName 
        => "BABYLON.GUI.AdvancedDynamicTexture.CreateFullscreenUI";

    public GrBabylonJsBooleanValue IsForeground { get; set; }

    public GrBabylonJsBooleanValue AdaptiveScaling { get; set; }

    public GrBabylonJsGuiFullScreenUiValue ParentUi 
        => this;

    public GrBabylonJsGuiControlList ControlList { get; } 
        = new GrBabylonJsGuiControlList();


    public GrBabylonJsGuiFullScreenUi(string constName) 
        : base(constName)
    {
    }

    public GrBabylonJsGuiFullScreenUi(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();

        if (IsForeground.IsNullOrEmpty()) yield break;
        yield return IsForeground.GetAttributeValueCode();

        if (ParentScene.IsNullOrEmpty()) yield break;
        yield return ParentScene.GetAttributeValueCode();

        if (SamplingMode.IsNullOrEmpty()) yield break;
        yield return SamplingMode.GetAttributeValueCode();
        
        if (AdaptiveScaling.IsNullOrEmpty()) yield break;
        yield return AdaptiveScaling.GetAttributeValueCode();
    }


    public override string GetBabylonJsCode()
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
                    .AppendAtNewLine(control.GetBabylonJsCode())
                    .AppendLineAtNewLine($"{ConstName}.addControl({control.ConstName});")
                    .AppendLine();
        }

        return composer.ToString();
    }
}