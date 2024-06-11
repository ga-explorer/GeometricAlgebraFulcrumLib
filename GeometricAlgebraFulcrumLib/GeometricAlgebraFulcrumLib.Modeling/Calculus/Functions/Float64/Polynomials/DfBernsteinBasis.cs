using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Polynomials;

public class DfBernsteinBasis :
    DfPolynomialBasis
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidDegree(int spaceDegree, int degree)
    {
        return spaceDegree >= 0 &&
               degree >= 0 && degree <= spaceDegree;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfBernsteinBasis Create(int spaceDegree, int degree)
    {
        return new DfBernsteinBasis(spaceDegree, degree);
    }


    private readonly double _binomialCoefficient;


    public int SpaceDegree { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DfBernsteinBasis(int spaceDegree, int degree)
        : base(degree)
    {
        if (spaceDegree < 0)
            throw new ArgumentOutOfRangeException(nameof(spaceDegree));

        if (degree > spaceDegree)
            throw new ArgumentOutOfRangeException(nameof(degree));

        _binomialCoefficient = spaceDegree.GetBinomialCoefficient(degree);

        SpaceDegree = spaceDegree;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        return _binomialCoefficient *
               Math.Pow(t, Degree) *
               Math.Pow(1 - t, SpaceDegree - Degree);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction GetDerivative1()
    {
        return GetPolynomialDerivative1();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction GetDerivativeN(int order)
    {
        return GetPolynomialDerivativeN(order);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfBernsteinPolynomial GetPolynomialDerivative1()
    {
        var n = SpaceDegree;
        var v = Degree;

        var scalarBasisTuples = new List<Tuple<double, DfBernsteinBasis>>(3);

        if (IsValidDegree(SpaceDegree, v - 1))
            scalarBasisTuples.Add(
                new Tuple<double, DfBernsteinBasis>(
                    n - v + 1,
                    Create(n, v - 1)
                )
            );

        scalarBasisTuples.Add(
            new Tuple<double, DfBernsteinBasis>(
                2 * v - n,
                Create(n, v)
            )
        );

        if (IsValidDegree(SpaceDegree, v - 1))
            scalarBasisTuples.Add(
                new Tuple<double, DfBernsteinBasis>(
                    v + 1,
                    Create(n, v + 1)
                )
            );

        return DfBernsteinPolynomial.Create(SpaceDegree, scalarBasisTuples);
    }

    public DfBernsteinPolynomial GetPolynomialDerivativeN(int order)
    {
        if (order < 0)
            throw new ArgumentOutOfRangeException(nameof(order));

        if (order == 0)
            return ToBernsteinPolynomial();

        if (order > Degree)
            return DfBernsteinPolynomial.CreateZero(SpaceDegree);

        var derivative = ToBernsteinPolynomial();

        while (order > 0)
        {
            derivative = derivative.GetPolynomialDerivative1();

            order--;
        }

        return derivative;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfBernsteinPolynomial ToBernsteinPolynomial()
    {
        return DfBernsteinPolynomial.Create(
            SpaceDegree,
            new[]
            {
                new Tuple<double, DfBernsteinBasis>(1d, this)
            }
        );
    }

    public DfMonomialPolynomial ToMonomialPolynomial()
    {
        var scalarCoefficients = LinFloat64VectorComposer.Create();

        var n = SpaceDegree;
        var v = Degree;
        for (var k = v; k <= n; k++)
        {
            var rational =
                FactoredRationalInt64
                    .Create()
                    .TimesCombination(n, k)
                    .TimesCombination(k, v);

            if ((k - v).IsOdd())
                rational.SetNegative();

            scalarCoefficients[k] = rational.ToFloat64();
        }

        return DfMonomialPolynomial.Create(
            scalarCoefficients.GetVector()
        );
    }

}