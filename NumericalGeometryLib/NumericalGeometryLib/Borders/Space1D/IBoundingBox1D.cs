using NumericalGeometryLib.Borders.Space1D.Immutable;
using NumericalGeometryLib.Borders.Space1D.Mutable;

namespace NumericalGeometryLib.Borders.Space1D
{
    public interface IBoundingBox1D
    {
        double MinValue { get; }

        double MaxValue { get; }

        BoundingBox1D GetBoundingBox();

        MutableBoundingBox1D GetMutableBoundingBox();
    }
}