using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GraphicsComposerLib.Rendering.Visuals.Space2D
{
    public sealed class GrVisualCircle2D :
        GrVisualElement2D
    {
        public IFloat64Tuple2D Center { get; set; }

        public double Radius { get; set; }


        public GrVisualCircle2D(string name) 
            : base(name)
        {
        }
    }
}