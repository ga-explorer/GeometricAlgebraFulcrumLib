using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces
{
    public sealed class GrVisualRingSurface3D :
        GrVisualSurface3D
    {
        public IFloat64Tuple3D Center { get; set; }

        public IFloat64Tuple3D Normal { get; set; }

        public double MinRadius { get; set; }

        public double MaxRadius { get; set; }


        public GrVisualRingSurface3D(string name) 
            : base(name)
        {
        }
    }
}