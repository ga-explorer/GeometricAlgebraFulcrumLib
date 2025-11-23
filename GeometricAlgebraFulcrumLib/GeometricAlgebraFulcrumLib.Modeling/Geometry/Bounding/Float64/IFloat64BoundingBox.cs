using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Rays;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

public interface IFloat64BoundingBox<TBox, TVector>
    where TVector : ILinFloat64Vector
{
    static abstract TBox Empty(int vSpaceDimensions);

    static abstract TBox Containing(IEnumerable<TBox> boxes);
    

    int VSpaceDimensions { get; }

    bool IsEmpty { get; }

    TVector MinPoint { get; }

    TVector MaxPoint { get; }

    TVector Center { get; }

    TVector Size { get; }

    
    bool ContainsPoint(TVector point);

    bool IntersectsBox(TBox other);

    bool IntersectsRay(IFloat64Ray<TVector> ray);

    bool TryIntersectRay(IFloat64Ray<TVector> ray, out double tMin, out double tMax, double tolerance = Float64Utils.DefaultTolerance, double geometricTolerance = 1e-9);

    double DistanceSquaredToPoint(TVector point);

}