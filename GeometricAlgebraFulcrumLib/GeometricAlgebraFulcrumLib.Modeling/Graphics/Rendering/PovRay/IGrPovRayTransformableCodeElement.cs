using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay;

public interface IGrPovRayTransformableCodeElement : 
    IGrPovRayCodeElement
{
    IFloat64AffineMap3D Transform { get; }

    //GrPovRayTransformList Transforms { get; }
}