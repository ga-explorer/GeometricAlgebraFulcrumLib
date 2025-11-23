using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Rays;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.SpacePartitions;

public interface IFloat64SpacePartition<in TBox, in TVector> 
    where TBox    : IFloat64BoundingBox<TBox, TVector>
    where TVector : ILinFloat64Vector
{
    void QueryCandidates(TBox query, List<int> outIndices);

    void QueryPoint(TVector point, List<int> outIndices);

    void QueryRay(IFloat64Ray<TVector> ray, List<int> outIndices);

    int? QueryNearest(TVector point, double maxDistance = double.PositiveInfinity);

    void QueryIntersections(TBox query, List<int> outIndices);
}