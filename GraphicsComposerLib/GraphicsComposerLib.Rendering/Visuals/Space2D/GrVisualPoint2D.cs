using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GraphicsComposerLib.Rendering.Visuals.Space2D
{
    public sealed class GrVisualPoint2D :
        GrVisualElement2D
    {
        public IFloat64Tuple2D Position { get; set; }


        public GrVisualPoint2D(string name) 
            : base(name)
        {
        }
    }
}
