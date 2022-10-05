using GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Basic
{
    public sealed class GrVisualPoint3D :
        GrVisualElement3D
    {
        public GrVisualThickSurfaceStyle3D Style { get; set; }

        public ITuple3D Position { get; set; } 
            = Tuple3D.Zero;


        public GrVisualPoint3D(string name) 
            : base(name)
        {
        }
    }
}
