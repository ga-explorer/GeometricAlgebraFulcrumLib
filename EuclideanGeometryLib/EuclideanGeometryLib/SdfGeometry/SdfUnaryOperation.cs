namespace EuclideanGeometryLib.SdfGeometry
{
    public abstract class SdfUnaryOperation : SignedDistanceFunction
    {
        public ISdfGeometry3D Surface { get; set; }
    }
}
