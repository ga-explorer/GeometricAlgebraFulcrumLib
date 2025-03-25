using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;

public abstract class GrPovRayTransformablePigment : 
    GrPovRayPigment,
    IGrPovRayTransformableCodeElement
{
    public Float64InvertibleAffineMap3D AffineMap { get; } 
        = Float64InvertibleAffineMap3D.Create();

    public IFloat64AffineMap3D Transform 
        => AffineMap;

    //public GrPovRayTransformList Transforms { get; }
    //    = new GrPovRayTransformList();


    protected GrPovRayTransformablePigment(string basePigmentName) 
        : base(basePigmentName)
    {
    }
}