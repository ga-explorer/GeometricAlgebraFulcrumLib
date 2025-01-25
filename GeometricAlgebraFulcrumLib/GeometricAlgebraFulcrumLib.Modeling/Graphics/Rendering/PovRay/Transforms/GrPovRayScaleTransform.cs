using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Transforms;

/// <summary>
/// http://www.povray.org/documentation/3.7.0/r3_3.html#r3_3_1_12_2
/// </summary>
public sealed class GrPovRayScaleTransform : 
    GrPovRayTransform
{
    public GrPovRayVector3Value FactorVector { get; }


    internal GrPovRayScaleTransform(GrPovRayFloat32Value factor)
    {
        FactorVector = GrPovRayVector3Value.Create(factor, factor, factor);
    }
    
    internal GrPovRayScaleTransform(GrPovRayFloat32Value factorX, GrPovRayFloat32Value factorY, GrPovRayFloat32Value factorZ)
    {
        FactorVector = GrPovRayVector3Value.Create(factorX, factorY, factorZ);
    }

    internal GrPovRayScaleTransform(GrPovRayVector3Value factorVector)
    {
        FactorVector = factorVector;
    }


    public override string GetPovRayCode()
    {
        return "scale " + FactorVector.GetAttributeValueCode();
    }
}