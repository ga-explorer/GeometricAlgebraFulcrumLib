using System;
using System.Text;

namespace NumericalGeometryLib.BasicMath.Coordinates
{
    public sealed class SphericalPosition3D : ISphericalPosition3D
    {
        public PlanarAngle Theta { get; }

        public PlanarAngle Phi { get; }

        public double R { get; }

        public bool IsValid()
        {
            return !double.IsNaN(R) &&
                   !double.IsNaN(Theta) &&
                   !double.IsNaN(Phi);
        }

        public double X => R * Theta.Sin() * Phi.Cos();

        public double Y => R * Theta.Sin() * Phi.Sin();

        public double Z => R * Theta.Cos();

        public double Item1 => X;

        public double Item2 => Y;

        public double Item3 => Z;


        public SphericalPosition3D(PlanarAngle theta, PlanarAngle phi)
        {
            Theta = theta.ClampPeriodic(Math.PI);
            Phi = phi.ClampPositive();
            R = 1;
        }

        public SphericalPosition3D(PlanarAngle theta, PlanarAngle phi, double r)
        {
            Theta = theta.ClampPeriodic(Math.PI);
            Phi = phi.ClampPositive();
            R = r > 0 ? r : 0;
        }


        public override string ToString()
        {
            return new StringBuilder()
                .Append("Spherical Position< theta: ")
                .Append(Theta.ToString())
                .Append(", phi: ")
                .Append(Phi.ToString())
                .Append(", r: ")
                .Append(R.ToString("G"))
                .Append(" >")
                .ToString();
        }
    }
}
