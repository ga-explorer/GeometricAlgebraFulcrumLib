using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Bernstein;

public class RationalBernsteinPolynomial2D
{
    private const double Epsilon = 1e-14;

        
    public BernsteinPolynomial2D Numerator { get; }

    public BernsteinPolynomial2D Denominator { get; }


    public RationalBernsteinPolynomial2D(BernsteinPolynomial2D num, BernsteinPolynomial2D den, double tolerance = Float64Utils.DefaultTolerance)
    {
        if (num == null || den == null) 
            throw new ArgumentNullException();

        Numerator = num;
        Denominator = den;
    }


    public double EvaluateAt(double t, double s)
    {
        var d = Denominator.EvaluateAt(t, s);

        if (Math.Abs(d) <= Epsilon * (1 + Math.Abs(Numerator.EvaluateAt(t, s))))
            throw new DivideByZeroException("Denominator too close to zero at t.");

        return Numerator.EvaluateAt(t, s) / d;
    }
}