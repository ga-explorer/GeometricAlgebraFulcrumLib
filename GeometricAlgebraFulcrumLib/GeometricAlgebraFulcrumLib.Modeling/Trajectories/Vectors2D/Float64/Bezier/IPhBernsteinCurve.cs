using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Bernstein;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier;

public interface IPhBernsteinCurve
{
    double Tolerance { get; }

    BernsteinPolynomial S { get; }

    BernsteinPolynomial SDt { get; }

    RationalBernsteinPolynomial EnergyDensity { get; }
    
    RationalBernsteinPolynomial Kappa1 { get; }
    

    double GetArcLength(double t);

    double GetArcSpeed(double t);

    double GetCurvature(double t);

    double GetEnergyDensity(double t);

    double GetEnergy(double t);
}