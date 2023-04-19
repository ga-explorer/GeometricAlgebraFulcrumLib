using System.Runtime.CompilerServices;
using System.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Coordinates
{
    public sealed class PolarPosition2D : 
        IPolarPosition2D
    {
        public Float64PlanarAngle Theta { get; }

        public double R { get; }
        
        public double X 
            => R * Math.Cos(Theta);

        public double Y 
            => R * Math.Sin(Theta);

        public double Item1 
            => X;

        public double Item2 
            => Y;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolarPosition2D(double r, Float64PlanarAngle theta)
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
                Theta = Float64PlanarAngle.Angle0;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolarPosition2D(Float64PlanarAngle theta)
        {
            R = 1;
            Theta = theta.ClampPositive();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return !double.IsNaN(R) && !double.IsNaN(Theta);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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