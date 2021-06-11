using System;
using System.Text;

namespace EuclideanGeometryLib.BasicMath.Coordinates
{
    public sealed class PolarPosition2D : IPolarPosition2D
    {
        public double Theta { get; }

        public double R { get; }

        public double ThetaInDegrees 
            => Theta.RadiansToDegrees();

        public double X 
            => R * Math.Cos(Theta);

        public double Y 
            => R * Math.Sin(Theta);

        public double Item1
            => X;

        public double Item2
            => Y;


        public bool IsValid 
            => !double.IsNaN(R) && !double.IsNaN(Theta);

        public bool IsInvalid
            => double.IsNaN(R) || double.IsNaN(Theta);


        public PolarPosition2D(double r, double theta)
        {
            if (r > 0)
            {
                R = r;
                Theta = theta.ClampAngle();
            }
            else if (r < 0)
            {
                R = -r;
                Theta = theta.ClampNegativeAngle();
            }
            else
            {
                R = 0;
                Theta = 0;
            }
        }

        public PolarPosition2D(double theta)
        {
            R = 1;
            Theta = theta.ClampAngle();
        }
        
        
        public override string ToString()
        {
            return new StringBuilder()
                .Append("Polar Position< r: ")
                .Append(R.ToString("G"))
                .Append(", theta: ")
                .Append(ThetaInDegrees.ToString("G"))
                .Append(" >")
                .ToString();
        }
    }
}