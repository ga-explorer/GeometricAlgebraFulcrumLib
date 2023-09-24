namespace GeometricAlgebraFulcrumLib.Lite.Graphics.SdfGeometry
{
    public abstract class SdfUnaryOperation : ScalarDistanceFunction
    {
        public ISdfGeometry3D Surface { get; set; }
    }
}
