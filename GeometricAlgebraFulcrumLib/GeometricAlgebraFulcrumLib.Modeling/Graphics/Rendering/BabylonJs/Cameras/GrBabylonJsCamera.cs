using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Cameras;

public abstract class GrBabylonJsCamera :
    GrBabylonJsObject
{
    public string SceneVariableName
        => ParentScene?.Value.ConstName ?? string.Empty;

    public bool AttachControl { get; set; } = true;

    public override GrBabylonJsObjectOptions? ObjectOptions
        => null;

    public GrBabylonJsSceneValue? ParentScene { get; set; }


    protected GrBabylonJsCamera(string constName)
        : base(constName)
    {
    }

    protected GrBabylonJsCamera(string constName, GrBabylonJsSceneValue scene)
        : base(constName)
    {
        ParentScene = scene;
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

        if (AttachControl)
            composer.AppendLineAtNewLine($"{ConstName}.attachControl(true);");

        return composer.ToString();
    }
}