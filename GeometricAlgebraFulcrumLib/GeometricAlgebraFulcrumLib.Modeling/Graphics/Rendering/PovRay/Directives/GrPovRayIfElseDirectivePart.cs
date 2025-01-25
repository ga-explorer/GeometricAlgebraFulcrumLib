using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Directives;

public sealed class GrPovRayIfElseDirectivePart
{
    public GrPovRayIfElseDirectivePartKind Kind { get; private set; }

    public GrPovRayBooleanValue Condition { get; private set; }

    public GrPovRayStatementList Statements { get; } 
        = new GrPovRayStatementList();


    internal GrPovRayIfElseDirectivePart(GrPovRayIfElseDirectivePartKind kind, GrPovRayBooleanValue condition)
    {
        Kind = kind;
        Condition = condition;
    }
}