using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Polynomials;

public class DfMonomialPolynomial :
    DfPolynomial
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfMonomialPolynomial CreateZero()
    {
        return new DfMonomialPolynomial(
            LinFloat64Vector.VectorZero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfMonomialPolynomial Create(LinFloat64Vector scalarCoefficients)
    {
        return new DfMonomialPolynomial(scalarCoefficients);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfMonomialPolynomial Create(params double[] scalarCoefficients)
    {
        return new DfMonomialPolynomial(
            LinFloat64VectorComposer
                .Create()
                .SetTerms(scalarCoefficients)
                .GetVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfMonomialPolynomial Create(IEnumerable<double> scalarCoefficients)
    {
        return new DfMonomialPolynomial(
            LinFloat64VectorComposer
                .Create()
                .SetTerms(scalarCoefficients)
                .GetVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfMonomialPolynomial Create(IEnumerable<Tuple<double, DfMonomialBasis>> scalarBasisList)
    {
        var scalarCoefficients = LinFloat64VectorComposer.Create();

        foreach (var (scalar, basis) in scalarBasisList)
            scalarCoefficients[basis.Degree] += scalar;

        return new DfMonomialPolynomial(scalarCoefficients.GetVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfMonomialPolynomial Create(IEnumerable<Tuple<double, DfMonomialPolynomial>> scalarPolynomialList)
    {
        var scalarCoefficients = LinFloat64VectorComposer.Create();

        foreach (var (scalar1, polynomial) in scalarPolynomialList)
            foreach (var (scalar2, basis) in polynomial.GetScaledBasis())
                scalarCoefficients[basis.Degree] += scalar1 * scalar2;

        return new DfMonomialPolynomial(scalarCoefficients.GetVector());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfMonomialPolynomial operator -(DfMonomialPolynomial p1)
    {
        return p1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfMonomialPolynomial operator +(DfMonomialPolynomial p1, double p2)
    {
        return p1.Add(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfMonomialPolynomial operator +(DfMonomialPolynomial p1, DfMonomialBasis p2)
    {
        return p1.Add(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfMonomialPolynomial operator +(DfMonomialPolynomial p1, DfMonomialPolynomial p2)
    {
        return p1.Add(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfMonomialPolynomial operator -(DfMonomialPolynomial p1, double p2)
    {
        return p1.Subtract(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfMonomialPolynomial operator -(DfMonomialPolynomial p1, DfMonomialBasis p2)
    {
        return p1.Subtract(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfMonomialPolynomial operator -(DfMonomialPolynomial p1, DfMonomialPolynomial p2)
    {
        return p1.Subtract(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfMonomialPolynomial operator *(DfMonomialPolynomial p1, double p2)
    {
        return p1.Times(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfMonomialPolynomial operator *(double p1, DfMonomialPolynomial p2)
    {
        return p2.Times(p1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfMonomialPolynomial operator *(DfMonomialPolynomial p1, DfMonomialBasis p2)
    {
        return p1.Times(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfMonomialPolynomial operator *(DfMonomialBasis p1, DfMonomialPolynomial p2)
    {
        return p2.Times(p1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfMonomialPolynomial operator *(DfMonomialPolynomial p1, DfMonomialPolynomial p2)
    {
        return p2.Times(p1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfMonomialPolynomial operator /(DfMonomialPolynomial p1, double p2)
    {
        return p1.Divide(p2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DfMonomialPolynomial(LinFloat64Vector scalarCoefficients)
        : base(scalarCoefficients)
    {
    }


    public override double GetValue(double t)
    {
        var value = 0d;

        foreach (var (i, c) in ScalarCoefficients.IndexScalarPairs)
            value += c * Math.Pow(t, i);

        return value;
    }

    public override Tuple<bool, DifferentialFunction> TrySimplify()
    {
        return new Tuple<bool, DifferentialFunction>(false, this);
    }

    public override DifferentialFunction Simplify()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction GetDerivative1()
    {
        return GetPolynomialDerivative1().Simplify();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction GetDerivativeN(int order)
    {
        return GetPolynomialDerivativeN(order);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfMonomialPolynomial GetPolynomialDerivative1()
    {
        if (ScalarCoefficients.Count == 0)
            return this;

        var scalarCoefficients =
            LinFloat64VectorComposer
                .Create()
                .AddTerms(
                    ScalarCoefficients
                        .IndexScalarPairs
                        .Select(p => new KeyValuePair<int, double>(
                            p.Key - 1,
                            p.Value * p.Key
                        ))
                );

        return new DfMonomialPolynomial(scalarCoefficients.GetVector());
    }

    public DfMonomialPolynomial GetPolynomialDerivativeN(int order)
    {
        if (order < 0)
            throw new ArgumentOutOfRangeException(nameof(order));

        if (order == 0)
            return this;

        if (order > Degree)
            return CreateZero();

        var derivative = this;
        while (order > 0)
        {
            derivative = derivative.GetPolynomialDerivative1();

            order--;
        }

        return derivative;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Tuple<double, DfMonomialBasis>> GetScaledBasis()
    {
        return ScalarCoefficients.IndexScalarPairs.Select(r =>
            new Tuple<double, DfMonomialBasis>(
                r.Value,
                DfMonomialBasis.Create(r.Key)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfBernsteinPolynomial ToBernsteinPolynomial(int spaceDegree)
    {
        return DfBernsteinPolynomial.Create(
            spaceDegree,
            GetScaledBasis().MapItem2(
                b => b.ToBernsteinPolynomial(spaceDegree)
            )
        );
    }

    public DfChebyshevPolynomial ToChebyshevPolynomial()
    {
        return DfChebyshevPolynomial.Create(
            GetScaledBasis().MapItem2(
                b => b.ToChebyshevPolynomial()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfMonomialPolynomial Negative()
    {
        var scalarCoefficients = -ScalarCoefficients;

        return new DfMonomialPolynomial(scalarCoefficients);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfMonomialPolynomial Add(double p2)
    {
        if (p2.IsNaNOrInfinite())
            throw new ArgumentException(nameof(p2));

        var scalarCoefficients = LinFloat64VectorComposer.Create(ScalarCoefficients);

        scalarCoefficients[0] += p2;

        return new DfMonomialPolynomial(scalarCoefficients.GetVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfMonomialPolynomial Add(DfMonomialBasis p2)
    {
        var scalarCoefficients = LinFloat64VectorComposer.Create(ScalarCoefficients);

        scalarCoefficients[p2.Degree] += 1d;

        return new DfMonomialPolynomial(scalarCoefficients.GetVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfMonomialPolynomial Add(DfMonomialBasis p2, double scalarFactor)
    {
        if (scalarFactor.IsNaNOrInfinite())
            throw new ArgumentException(nameof(scalarFactor));

        var scalarCoefficients = LinFloat64VectorComposer.Create(ScalarCoefficients);

        scalarCoefficients[p2.Degree] += scalarFactor;

        return new DfMonomialPolynomial(scalarCoefficients.GetVector());
    }

    public DfMonomialPolynomial Add(DfMonomialPolynomial p2)
    {
        var scalarCoefficients = LinFloat64VectorComposer.Create(ScalarCoefficients);

        foreach (var (i, c) in p2.ScalarCoefficients.IndexScalarPairs)
            scalarCoefficients[i] += c;

        return new DfMonomialPolynomial(scalarCoefficients.GetVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfMonomialPolynomial Subtract(double p2)
    {
        if (p2.IsNaNOrInfinite())
            throw new ArgumentException(nameof(p2));

        var scalarCoefficients = LinFloat64VectorComposer.Create(ScalarCoefficients);

        scalarCoefficients[0] -= p2;

        return new DfMonomialPolynomial(scalarCoefficients.GetVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfMonomialPolynomial Subtract(DfMonomialBasis p2)
    {
        var scalarCoefficients = LinFloat64VectorComposer.Create(ScalarCoefficients);

        scalarCoefficients[p2.Degree] -= 1d;

        return new DfMonomialPolynomial(scalarCoefficients.GetVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfMonomialPolynomial Subtract(DfMonomialBasis p2, double scalarFactor)
    {
        if (scalarFactor.IsNaNOrInfinite())
            throw new ArgumentException(nameof(scalarFactor));

        var scalarCoefficients = LinFloat64VectorComposer.Create(ScalarCoefficients);

        scalarCoefficients[p2.Degree] -= scalarFactor;

        return new DfMonomialPolynomial(scalarCoefficients.GetVector());
    }

    public DfMonomialPolynomial Subtract(DfMonomialPolynomial p2)
    {
        var scalarCoefficients = LinFloat64VectorComposer.Create(ScalarCoefficients);

        foreach (var (i, c) in p2.ScalarCoefficients.IndexScalarPairs)
            scalarCoefficients[i] -= c;

        return new DfMonomialPolynomial(scalarCoefficients.GetVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfMonomialPolynomial Times(double scalar)
    {
        if (scalar.IsNaNOrInfinite())
            throw new InvalidOperationException();

        var scalarCoefficients =
            ScalarCoefficients * scalar;

        return new DfMonomialPolynomial(scalarCoefficients);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfMonomialPolynomial Times(DfMonomialBasis p2)
    {
        var scalarCoefficients = LinFloat64VectorComposer.Create();

        var i2 = p2.Degree;
        foreach (var (i1, c1) in ScalarCoefficients.IndexScalarPairs)
        {
            var i = i1 + i2;
            var j = Math.Abs(i1 - i2);
            var d = 0.5 * c1;

            scalarCoefficients[i] += d;
            scalarCoefficients[j] += d;
        }

        return new DfMonomialPolynomial(scalarCoefficients.GetVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfMonomialPolynomial Times(DfMonomialBasis p2, double scalingFactor)
    {
        if (scalingFactor.IsNaNOrInfinite())
            throw new NotFiniteNumberException(nameof(scalingFactor));

        var scalarCoefficients = LinFloat64VectorComposer.Create();

        var i2 = p2.Degree;
        foreach (var (i1, c1) in ScalarCoefficients.IndexScalarPairs)
        {
            var i = i1 + i2;
            var j = Math.Abs(i1 - i2);
            var d = 0.5 * c1 * scalingFactor;

            scalarCoefficients[i] += d;
            scalarCoefficients[j] += d;
        }

        return new DfMonomialPolynomial(scalarCoefficients.GetVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfMonomialPolynomial Times(DfMonomialPolynomial p2)
    {
        var scalarCoefficients = LinFloat64VectorComposer.Create();

        foreach (var (i1, c1) in ScalarCoefficients.IndexScalarPairs)
            foreach (var (i2, c2) in p2.ScalarCoefficients.IndexScalarPairs)
            {
                var i = i1 + i2;
                var j = Math.Abs(i1 - i2);
                var d = 0.5 * c1 * c2;

                scalarCoefficients[i] += d;
                scalarCoefficients[j] += d;
            }

        return new DfMonomialPolynomial(scalarCoefficients.GetVector());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfMonomialPolynomial Divide(double scalar)
    {
        return Times(1d / scalar);
    }
}