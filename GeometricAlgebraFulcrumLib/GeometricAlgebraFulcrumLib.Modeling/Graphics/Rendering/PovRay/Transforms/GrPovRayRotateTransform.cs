using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Transforms;

/// <summary>
/// http://www.povray.org/documentation/3.7.0/r3_3.html#r3_3_1_12_3
/// </summary>
public sealed class GrPovRayRotateTransform : 
    GrPovRayTransform
{
    /// <summary>
    /// The Euler X->Y->Z Rotation angles in degrees
    /// </summary>
    public GrPovRayAngleValue AngleX { get; }

    public GrPovRayAngleValue AngleY { get; }

    public GrPovRayAngleValue AngleZ { get; }


    internal GrPovRayRotateTransform(GrPovRayAngleValue angleX, GrPovRayAngleValue angleY, GrPovRayAngleValue angleZ)
    {
        AngleX = angleX;
        AngleY = angleY;
        AngleZ = angleZ;
    }


    public override string GetPovRayCode()
    {
        return $"rotate <{AngleX.GetPovRayCode()}, {AngleY.GetPovRayCode()}, {AngleZ.GetPovRayCode()}>";
    }
}