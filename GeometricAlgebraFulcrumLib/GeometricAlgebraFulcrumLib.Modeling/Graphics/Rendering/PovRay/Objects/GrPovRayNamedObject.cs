using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects;

public class GrPovRayNamedObject : 
    GrPovRayObject
{
    public string Name { get; }


    internal GrPovRayNamedObject(string name)
    {
        Name = name;
    }


    public override string GetPovRayCode()
    {
        return new LinearTextComposer()
            .AppendLine("object {")
            .IncreaseIndentation()
            .AppendAtNewLine(Name)
            .AppendLineAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendLineAtNewLine("}")
            .ToString();
    }
}