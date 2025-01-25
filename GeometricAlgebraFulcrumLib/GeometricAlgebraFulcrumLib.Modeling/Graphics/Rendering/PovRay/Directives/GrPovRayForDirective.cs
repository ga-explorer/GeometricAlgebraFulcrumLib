using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Directives;

/// <summary>
/// http://www.povray.org/documentation/3.7.0/r3_3.html#r3_3_2_6_3
/// </summary>
public sealed class GrPovRayForDirective :
    GrPovRayDirective
{
    public string LoopVariable { get; }

    public GrPovRayFloat32Value StartValue { get; }

    public GrPovRayFloat32Value EndValue { get; }

    public GrPovRayFloat32Value StepValue { get; } 

    public GrPovRayStatementList Statements { get; } 
        = new GrPovRayStatementList();


    internal GrPovRayForDirective(string loopVariable, GrPovRayFloat32Value startValue, GrPovRayFloat32Value endValue, GrPovRayFloat32Value stepValue)
    {
        LoopVariable = loopVariable;
        StartValue = startValue;
        EndValue = endValue;
        StepValue = stepValue;
    }


    public override string GetPovRayCode()
    {
        return new LinearTextComposer()
            .Append("#for (")
            .Append(LoopVariable)
            .Append(", ")
            .Append(StartValue)
            .Append(", ")
            .Append(EndValue)
            .Append(", ")
            .Append(StepValue)
            .AppendLine(")")
            .IncreaseIndentation()
            .Append(Statements.GetPovRayCode())
            .DecreaseIndentation()
            .AppendLineAtNewLine("#end")
            .ToString();
    }
}