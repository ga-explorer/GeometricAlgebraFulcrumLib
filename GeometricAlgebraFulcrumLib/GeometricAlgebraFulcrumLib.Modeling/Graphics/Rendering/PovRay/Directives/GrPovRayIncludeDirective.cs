namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Directives;

/// <summary>
/// http://www.povray.org/documentation/3.7.0/r3_3.html#r3_3_2_1
/// </summary>
public sealed class GrPovRayIncludeDirective : 
    GrPovRayDirective
{
    public string FileName { get; }


    internal GrPovRayIncludeDirective(string fileName)
    {
        FileName = fileName;
    }


    public override string GetPovRayCode()
    {
        return $"#include \"{FileName}.inc\"";
    }
}