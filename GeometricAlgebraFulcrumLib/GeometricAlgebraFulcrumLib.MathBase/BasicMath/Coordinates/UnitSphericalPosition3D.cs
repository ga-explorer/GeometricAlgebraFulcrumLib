using System.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Coordinates
{
    public sealed class UnitSphericalPosition3D : ISphericalPosition3D
    {
        public Float64PlanarAngle Theta { get; }

        public Float64PlanarAngle Phi { get; }

        public double R => 1;

        public bool IsValid()
        {
            return !double.IsNaN(Theta) &&
                   !double.IsNaN(Phi);
        }

        public double X => Theta.Sin() * Phi.Cos();

        public double Y => Theta.Sin() * Phi.Sin();

        public double Z => Theta.Cos();

        public double Item1 => X;

        public double Item2 => Y;

        public double Item3 => Z;


        public UnitSphericalPosition3D(Float64PlanarAngle theta, Float64PlanarAngle phi)
        {
            Theta = theta.ClampPeriodic(Math.PI);
            Phi = phi.ClampPositive();
        }


        public override string ToString()
        {
            return new StringBuilder()
                .Append("Unit Spherical Position< theta: ")
                .Append(Theta.ToString())
                .Append(", phi: ")
                .Append(Phi.ToString())
                .Append(" >")
                .ToString();
        }
    }
}