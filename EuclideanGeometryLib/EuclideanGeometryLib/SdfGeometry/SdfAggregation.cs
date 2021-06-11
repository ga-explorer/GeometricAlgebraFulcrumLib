using System.Collections.Generic;

namespace EuclideanGeometryLib.SdfGeometry
{
    public abstract class SdfAggregation : SignedDistanceFunction
    {
        public List<ISdfGeometry3D> Surfaces { get; }
            = new List<ISdfGeometry3D>();
    }
}
