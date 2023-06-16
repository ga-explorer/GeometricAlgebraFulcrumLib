using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GraphicsComposerLib.Rendering.Visuals.Space2D
{
    public sealed class GrVisualLineVector2D :
        GrVisualElement2D
    {
        public IFloat64Tuple2D Position { get; set; }

        public IFloat64Tuple2D Direction { get; set; }


        public GrVisualLineVector2D(string name) 
            : base(name)
        {
        }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}