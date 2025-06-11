using System.Text;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;

public sealed class LinFloat64PolarVector2D :
    ILinFloat64PolarVector2D
{
    public int VSpaceDimensions
        => 2;

    public LinFloat64PolarAngle Theta { get; }

    public double R { get; }

    public double X
        => R * Theta.Cos();

    public double Y
        => R * Theta.Sin();

    public double Item1
        => X;

    public double Item2
        => Y;


    
    public LinFloat64PolarVector2D(double r, LinFloat64PolarAngle theta)
    {
        if (r > 0)
        {
            R = r;
            Theta = theta;
        }
        else if (r < 0)
        {
            R = -r;
            Theta = theta.OppositeAngle();
        }
        else
        {
            R = 0d;
            Theta = LinFloat64PolarAngle.Angle0;
        }
    }

    
    public LinFloat64PolarVector2D(LinFloat64PolarAngle theta)
    {
        R = 1d;
        Theta = theta;
    }


    
    public bool IsValid()
    {
        return R.IsValid() &&
               R >= 0 &&
               Theta.IsValid();
    }

    
    public bool IsUnitVector()
    {
        return R.IsOne();
    }

    
    public bool IsNearUnitVector(double zeroEpsilon = 1E-12)
    {
        return R.IsNearOne(zeroEpsilon);
    }

    
    public override string ToString()
    {
        return new StringBuilder()
            .Append("Polar Position< r: ")
            .Append(R.ToString("G"))
            .Append(", theta: ")
            .Append(Theta)
            .Append(" >")
            .ToString();
    }
}