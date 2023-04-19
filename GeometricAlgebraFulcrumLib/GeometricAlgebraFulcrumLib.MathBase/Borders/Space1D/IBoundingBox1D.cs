using GeometricAlgebraFulcrumLib.MathBase.Borders.Space1D.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space1D.Mutable;

namespace GeometricAlgebraFulcrumLib.MathBase.Borders.Space1D
{
    public interface IBoundingBox1D
    {
        double MinValue { get; }

        double MaxValue { get; }

        BoundingBox1D GetBoundingBox();

        MutableBoundingBox1D GetMutableBoundingBox();
    }
}