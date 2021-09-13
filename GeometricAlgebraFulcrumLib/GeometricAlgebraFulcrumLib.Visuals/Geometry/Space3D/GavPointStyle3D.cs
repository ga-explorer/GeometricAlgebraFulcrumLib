using System.Drawing;

namespace GeometricAlgebraFulcrumLib.Visuals.Geometry.Space3D
{
    public sealed class GeovPointStyle3D
    {
        public Color Color { get; }

        public double Radius { get; }


        public GeovPointStyle3D(Color color, double radius)
        {
            Color = color;
            Radius = radius;
        }
    }
}