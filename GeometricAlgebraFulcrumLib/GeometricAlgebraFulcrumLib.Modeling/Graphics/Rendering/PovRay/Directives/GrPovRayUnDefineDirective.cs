namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Directives;

/// <summary>
/// http://www.povray.org/documentation/3.7.0/r3_3.html#r3_3_2_2_4
/// </summary>
public sealed class GrPovRayUnDefineDirective : 
    GrPovRayDirective
{
    public string Name { get; }


    internal GrPovRayUnDefineDirective(string name)
    {
        Name = name;
    }


    public override string GetPovRayCode()
    {
        return $"#undef {Name}";
    }
}