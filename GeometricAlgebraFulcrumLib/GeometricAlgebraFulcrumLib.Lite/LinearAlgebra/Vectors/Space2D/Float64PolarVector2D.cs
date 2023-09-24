using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D
{
    public sealed class Float64PolarVector2D : 
        IFloat64PolarVector2D
    {
        public int VSpaceDimensions 
            => 2;

        public Float64PlanarAngle Theta { get; }
        
        public Float64Scalar R { get; }
        
        public Float64Scalar X 
            => R * Theta.Cos();

        public Float64Scalar Y 
            => R * Theta.Sin();

        public double Item1 
            => X.Value;

        public double Item2 
            => Y.Value;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64PolarVector2D(Float64Scalar r, Float64PlanarAngle theta)
        {
            if (r.Value > 0)
            {
                R = r;
                Theta = theta.GetAngleInPositiveRange();
            }
            else if (r.Value < 0)
            {
                R = -r;
                Theta = theta.ClampNegative();
            }
            else
            {
                R = Float64Scalar.Zero;
                Theta = Float64PlanarAngle.Angle0;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64PolarVector2D(Float64PlanarAngle theta)
        {
            R = Float64Scalar.One;
            Theta = theta.GetAngleInPositiveRange();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return R.IsValid() && 
                   R.Value >= 0 &&
                   Theta.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsUnitVector()
        {
            return R.IsOne();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearUnitVector(double epsilon = 1E-12)
        {
            return R.IsNearOne(epsilon);
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