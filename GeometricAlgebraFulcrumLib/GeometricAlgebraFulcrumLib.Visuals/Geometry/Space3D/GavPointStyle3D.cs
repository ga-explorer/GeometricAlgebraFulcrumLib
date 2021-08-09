using System.Drawing;

namespace GeometricAlgebraFulcrumLib.Visuals.Geometry.Space3D
{
    public sealed class GavPointStyle3D
    {
        public Color Color { get; }

        public double Radius { get; }


        public GavPointStyle3D(Color color, double radius)
        {
            Color = color;
            Radius = radius;
        }
    }
}