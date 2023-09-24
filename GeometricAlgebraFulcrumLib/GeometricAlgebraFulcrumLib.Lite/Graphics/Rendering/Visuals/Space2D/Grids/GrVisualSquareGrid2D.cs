using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space2D.Images;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space2D.Grids
{
    public sealed class GrVisualSquareGrid2D :
        GrVisualLineGridImage2D
    {
        public IFloat64Vector2D Origin { get; set; } 
            = Float64Vector2D.Create(-12, -12);

        public double UnitSize { get; set; } 
            = 1;

        public int UnitCountX { get; set; } 
            = 24;

        public int UnitCountZ { get; set; } 
            = 24;

        public double SizeX 
            => UnitCountX * UnitSize;

        public double SizeZ 
            => UnitCountZ * UnitSize;

        public double Opacity { get; set; } 
            = 0.2;


        public GrVisualSquareGrid2D(string name) 
            : base(name)
        {
        }
    }
}