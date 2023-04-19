using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GraphicsComposerLib.Rendering.Visuals.Space2D
{
    public sealed class GrVisualLineSegment2D :
        GrVisualElement2D
    {
        public IFloat64Tuple2D Position1 { get; set; }

        public IFloat64Tuple2D Position2 { get; set; }


        public GrVisualLineSegment2D(string name) 
            : base(name)
        {
        }
    }
}