using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D
{
    public sealed class Float64SphericalUnitVector3D : 
        IFloat64SphericalVector3D
    {
        public int VSpaceDimensions 
            => 3;

        public Float64PlanarAngle Theta { get; }

        public Float64PlanarAngle Phi { get; }
        
        public Float64Scalar R 
            => Float64Scalar.One;

        public Float64Scalar X 
            => Theta.Sin() * Phi.Cos();

        public Float64Scalar Y 
            => Theta.Sin() * Phi.Sin();

        public Float64Scalar Z 
            => Theta.Cos();

        public double Item1 
            => X.Value;

        public double Item2 
            => Y.Value;

        public double Item3 
            => Z.Value;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64SphericalUnitVector3D(Float64PlanarAngle theta, Float64PlanarAngle phi)
        {
            Theta = theta.GetAngleInPeriodicRange(Math.PI);
            Phi = phi.GetAngleInPositiveRange();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Theta.IsValid() &&
                   Phi.IsValid();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsUnitVector()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearUnitVector(double epsilon = 1E-12)
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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