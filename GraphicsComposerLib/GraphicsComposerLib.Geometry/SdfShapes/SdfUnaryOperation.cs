namespace GraphicsComposerLib.Geometry.SdfShapes
{
    public abstract class SdfUnaryOperation : ScalarDistanceFunction
    {
        public ISdfGeometry3D Surface { get; set; }
    }
}
