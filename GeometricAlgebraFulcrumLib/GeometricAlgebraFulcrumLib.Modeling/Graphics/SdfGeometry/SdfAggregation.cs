namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.SdfGeometry;

public abstract class SdfAggregation : ScalarDistanceFunction
{
    public List<ISdfGeometry3D> Surfaces { get; }
        = new List<ISdfGeometry3D>();
}