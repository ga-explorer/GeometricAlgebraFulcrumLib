using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.CSG;

public abstract class GrPovRayCsgObject :
    GrPovRayObject,
    IGrPovRayCsgObject
{
    private static readonly string[] CsgOpNames = new[]
    {
        "union",
        "merge",
        "intersection",
        "difference"
    };


    public abstract GrPovRayCsgOperation CsgOperation { get; }

    public string CsgOperationName
        => CsgOpNames[(int)CsgOperation];
    

    protected abstract string GetObjectsCode();

    public override string GetPovRayCode()
    {
        return new LinearTextComposer()
            .AppendLine(CsgOperationName + " {")
            .IncreaseIndentation()
            .AppendAtNewLine(GetObjectsCode())
            .AppendAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}")
            .ToString();
    }
}