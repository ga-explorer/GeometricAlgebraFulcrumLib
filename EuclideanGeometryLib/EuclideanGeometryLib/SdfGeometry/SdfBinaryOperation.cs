namespace EuclideanGeometryLib.SdfGeometry
{
    public abstract class SdfBinaryOperation : SignedDistanceFunction
    {
        public ISdfGeometry3D Surface1 { get; set; }

        public ISdfGeometry3D Surface2 { get; set; }
    }
}
