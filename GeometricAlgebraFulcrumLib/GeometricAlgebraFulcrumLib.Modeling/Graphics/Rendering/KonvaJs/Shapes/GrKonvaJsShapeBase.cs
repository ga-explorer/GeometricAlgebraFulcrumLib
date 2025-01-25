using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Containers;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.Shape.html
/// </summary>
public abstract class GrKonvaJsShapeBase :
    GrKonvaJsNode,
    IGrKonvaJsGroupObject
{
    public abstract GrKonvaJsShapeBaseProperties? ShapeBaseProperties { get; }
    
    public override GrKonvaJsNodeProperties? NodeProperties
        => ShapeBaseProperties;


    protected GrKonvaJsShapeBase(string constName)
        : base(constName)
    {
    }


    public override string GetKonvaJsCode()
    {
        var composer = new LinearTextComposer();

        var constructorCode = GetConstructorCode();
        var propertiesCode = GetPropertiesCode();

        if (!string.IsNullOrEmpty(ConstName))
        {
            var declarationKeyword = UseLetDeclaration ? "let" : "const";

            composer.Append($"{declarationKeyword} {ConstName} = ");
        }

        composer
            .AppendLine(constructorCode)
            .AppendLine(propertiesCode);
        
        if (DrawAfterCreation)
            composer.AppendLineAtNewLine($"{ConstName}.draw();");

        return composer.ToString();
    }
}