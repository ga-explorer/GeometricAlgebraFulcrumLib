using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

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


        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}