using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Directives;

public sealed class GrPovRayWhileDirective : 
    GrPovRayDirective
{
    public GrPovRayBooleanValue LoopCondition { get; }

    public GrPovRayStatementList Statements { get; } 
        = new GrPovRayStatementList();


    internal GrPovRayWhileDirective(GrPovRayBooleanValue loopCondition)
    {
        LoopCondition = loopCondition;
    }


    public override string GetPovRayCode()
    {
        throw new NotImplementedException();
    }
}