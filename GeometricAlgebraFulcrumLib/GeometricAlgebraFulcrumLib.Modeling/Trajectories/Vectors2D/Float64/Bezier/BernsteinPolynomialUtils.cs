//using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

//namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier;

//public static class BernsteinPolynomialUtils
//{
    

//    public static (BernsteinPolynomial xPrime, BernsteinPolynomial yPrime, BernsteinPolynomial speed) GetPhDerivatives(BernsteinPolynomial u, BernsteinPolynomial v, BernsteinPolynomial w)
//    {
//        var u2 = u.Square();
//        var v2 = v.Square();
//        var xPrime = w * (u2 - v2);
//        var yPrime = 2 * w * u * v;
//        var speed = (u2 + v2) * w;
        
//        return (xPrime, yPrime, speed);
//    }

//    public static RationalBernsteinPolynomial GetPhCurvature(BernsteinPolynomial u, BernsteinPolynomial v, BernsteinPolynomial w, double tolerance = Float64Utils.DefaultTolerance)
//    {
//        var uPrime = u.Derivative();
//        var vPrime = v.Derivative();
//        var numerator = 2 * (u * vPrime - v * uPrime);
//        var denom = w * (u.Square() + v.Square()).Square();

//        return new RationalBernsteinPolynomial(numerator, denom.Simplify(tolerance));
//    }

//    public static RationalBernsteinPolynomial GetPhEnergyDensity(BernsteinPolynomial u, BernsteinPolynomial v, BernsteinPolynomial w, double tolerance = Float64Utils.DefaultTolerance)
//    {
//        var uPrime = u.Derivative();
//        var vPrime = v.Derivative();

//        var numerator = (u * vPrime - v * uPrime).Square().Times(4);
//        var sum = u.Square() + v.Square();
//        var denom = w * (sum * sum.Square());

//        return new RationalBernsteinPolynomial(numerator, denom.Simplify(tolerance));
//    }
//}