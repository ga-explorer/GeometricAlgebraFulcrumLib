using System;
using System.Text;

namespace NumericalGeometryLib.BasicMath.Coordinates
{
    public sealed class PolarPosition2D : IPolarPosition2D
    {
        public PlanarAngle Theta { get; }

        public double R { get; }
        
        public double X 
            => R * Math.Cos(Theta);

        public double Y 
            => R * Math.Sin(Theta);

        public double Item1 
            => X;

        public double Item2 
            => Y;


        public bool IsValid()
        {
            return !double.IsNaN(R) && !double.IsNaN(Theta);
        }


        public PolarPosition2D(double r, PlanarAngle theta)
        {
            if (r > 0)
            {
                R = r;
                Theta = theta.ClampPositive();
            }
            else if (r < 0)
            {
                R = -r;
                Theta = theta.ClampNegative();
            }
            else
            {
                R = 0;
                Theta = PlanarAngle.Angle0;
            }
        }

        public PolarPosition2D(PlanarAngle theta)
        {
            R = 1;
            Theta = theta.ClampPositive();
        }
        
        
        public override string ToString()
        {
            return new StringBuilder()
                .Append("Polar Position< r: ")
                .Append(R.ToString("G"))
                .Append(", theta: ")
                .Append(Theta.ToString())
                .Append(" >")
                .ToString();
        }
    }
}