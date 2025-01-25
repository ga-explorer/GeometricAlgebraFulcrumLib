namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Directives;

public sealed class GrPovRayIfElseDirective : 
    GrPovRayDirective
{
    public List<GrPovRayIfElseDirectivePart> Parts { get; private set; }


    internal GrPovRayIfElseDirective()
    {
        Parts = new List<GrPovRayIfElseDirectivePart>();
    }

    public override string GetPovRayCode()
    {
        throw new NotImplementedException();
    }
}