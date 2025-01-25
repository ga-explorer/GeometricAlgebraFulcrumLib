using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Transforms;

public sealed class GrPovRayTranslateTransform : 
    GrPovRayTransform
{
    public GrPovRayVector3Value Direction { get; }

    
    internal GrPovRayTranslateTransform(GrPovRayFloat32Value directionX, GrPovRayFloat32Value directionY, GrPovRayFloat32Value directionZ)
    {
        Direction = GrPovRayVector3Value.Create(directionX, directionY, directionZ);
    }

    internal GrPovRayTranslateTransform(GrPovRayVector3Value direction)
    {
        Direction = direction;
    }


    public override string GetPovRayCode()
    {
        return "translate " + Direction.GetAttributeValueCode();
    }
}