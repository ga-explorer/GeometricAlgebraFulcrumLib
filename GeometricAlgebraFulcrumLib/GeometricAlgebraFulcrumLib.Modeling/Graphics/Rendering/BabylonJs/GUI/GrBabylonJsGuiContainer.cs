using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.GUI.Container
/// </summary>
public abstract class GrBabylonJsGuiContainer :
    GrBabylonJsGuiControl,
    IGrBabylonJsGuiControlContainer
{
    public GrBabylonJsGuiControlList ControlList { get; } 
        = new GrBabylonJsGuiControlList();


    protected GrBabylonJsGuiContainer(string constName, IGrBabylonJsGuiControlContainer parentContainer) 
        : base(constName, parentContainer)
    {
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