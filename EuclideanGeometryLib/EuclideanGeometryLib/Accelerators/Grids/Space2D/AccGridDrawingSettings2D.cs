using System.Drawing;

namespace EuclideanGeometryLib.Accelerators.Grids.Space2D
{
    public sealed class AccGridDrawingSettings2D
    {
        public bool DrawGeometricObjects { get; set; } = true;

        public bool DrawGridLines { get; set; } = true;

        public bool DrawActiveCells { get; set; } = true;

        public Color GeometricObjectsColor { get; set; }
            = Color.Black;

        public Color GridLinesColor { get; set; }
            = Color.DarkSalmon;

        public Color ActiveCellsFillColor { get; set; }
            = Color.DarkSalmon;

        public double ActiveCellsFillOpacity { get; set; } = 0.1d;

        public Color MarkerLineColor { get; set; }
            = Color.Black;

        public Color MarkerFillColor { get; set; } 
            = Color.White;

        public int MarkerPixelSize { get; set; } = 7;
    }
}
