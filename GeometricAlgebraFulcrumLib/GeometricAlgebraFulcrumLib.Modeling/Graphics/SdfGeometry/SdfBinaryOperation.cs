namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.SdfGeometry;

public abstract class SdfBinaryOperation : ScalarDistanceFunction
{
    public ISdfGeometry3D Surface1 { get; set; }

    public ISdfGeometry3D Surface2 { get; set; }
}