using System.Collections.Generic;

namespace GraphicsComposerLib.Geometry.SdfShapes
{
    public abstract class SdfAggregation : ScalarDistanceFunction
    {
        public List<ISdfGeometry3D> Surfaces { get; }
            = new List<ISdfGeometry3D>();
    }
}
