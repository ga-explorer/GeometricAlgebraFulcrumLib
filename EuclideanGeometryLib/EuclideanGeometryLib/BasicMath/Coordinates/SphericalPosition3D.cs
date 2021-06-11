using System;
using System.Text;

namespace EuclideanGeometryLib.BasicMath.Coordinates
{
    public sealed class SphericalPosition3D : ISphericalPosition3D
    {
        public double Theta { get; }

        public double Phi { get; }

        public double R { get; }

        public double ThetaInDegrees 
            => Theta.RadiansToDegrees();

        public double PhiInDegrees 
            => Phi.RadiansToDegrees();

        public bool IsValid
            => !double.IsNaN(R) &&
               !double.IsNaN(Theta) &&
               !double.IsNaN(Phi);

        public bool IsInvalid
            => double.IsNaN(R) ||
               double.IsNaN(Theta) ||
               double.IsNaN(Phi);

        public double X 
            => R * Math.Sin(Theta) * Math.Cos(Phi);

        public double Y 
            => R * Math.Sin(Theta) * Math.Sin(Phi);

        public double Z 
            => R * Math.Cos(Theta);

        public double Item1
            => X;

        public double Item2
            => Y;

        public double Item3
            => Z;


        public SphericalPosition3D(double theta, double phi)
        {
            Theta = theta.ClampPeriodic(Math.PI);
            Phi = phi.ClampAngle();
            R = 1;
        }

        public SphericalPosition3D(double theta, double phi, double r)
        {
            Theta = theta.ClampPeriodic(Math.PI);
            Phi = phi.ClampAngle();
            R = r > 0 ? r : 0;
        }


        public override string ToString()
        {
            return new StringBuilder()
                .Append("Spherical Position< theta: ")
                .Append(ThetaInDegrees.ToString("G"))
                .Append(", phi: ")
                .Append(PhiInDegrees.ToString("G"))
                .Append(", r: ")
                .Append(R.ToString("G"))
                .Append(" >")
                .ToString();
        }
    }
}
