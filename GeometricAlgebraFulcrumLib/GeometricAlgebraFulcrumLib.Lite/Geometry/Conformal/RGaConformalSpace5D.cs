using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Visualizer;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal
{
    /// <summary>
    /// 5D Conformal Geometric Algebra for 3D Euclidean Space
    /// </summary>
    public sealed class RGaConformalSpace5D :
        RGaConformalSpace
    {
        public static RGaConformalSpace5D Instance { get; } 
            = new RGaConformalSpace5D();


        public RGaConformalBlade E3 { get; }

        public RGaConformalBlade E13 { get; }

        public RGaConformalBlade E23 { get; }

        public RGaConformalVisualizer Visualizer { get; }


        private RGaConformalSpace5D() 
            : base(5)
        {
            E3 = new RGaConformalBlade(this, ConformalProcessor.CreateTermVector(4));

            E13 = new RGaConformalBlade(this, ConformalProcessor.CreateTermBivector(2, 4));
            E23 = new RGaConformalBlade(this, ConformalProcessor.CreateTermBivector(3, 4));

            Visualizer = new RGaConformalVisualizer(this);
        }
    }
}
