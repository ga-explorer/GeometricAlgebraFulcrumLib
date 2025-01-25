using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Directives;

public sealed class GrPovRayMacroDirective : 
    GrPovRayDirective
{
    public string Name { get; }

    public List<string> Parameters { get; } 
        = new List<string>();

    public GrPovRayStatementList Statements { get; }
        = new GrPovRayStatementList();


    internal GrPovRayMacroDirective(string name)
    {
        Name = name;
    }


    public override string GetPovRayCode()
    {
        throw new NotImplementedException();
    }
}