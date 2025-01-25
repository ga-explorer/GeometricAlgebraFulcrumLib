namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Transforms;

public sealed class GrPovRayNamedTransform : 
    GrPovRayTransform
{
    public string Identifier { get; }


    internal GrPovRayNamedTransform(string identifier)
    {
        Identifier = identifier;
    }


    public override string GetPovRayCode()
    {
        return "transform " + Identifier;
    }
}