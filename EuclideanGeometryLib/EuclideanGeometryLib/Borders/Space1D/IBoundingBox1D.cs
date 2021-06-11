using EuclideanGeometryLib.Borders.Space1D.Immutable;
using EuclideanGeometryLib.Borders.Space1D.Mutable;

namespace EuclideanGeometryLib.Borders.Space1D
{
    public interface IBoundingBox1D
    {
        double MinValue { get; }

        double MaxValue { get; }

        BoundingBox1D GetBoundingBox();

        MutableBoundingBox1D GetMutableBoundingBox();
    }
}