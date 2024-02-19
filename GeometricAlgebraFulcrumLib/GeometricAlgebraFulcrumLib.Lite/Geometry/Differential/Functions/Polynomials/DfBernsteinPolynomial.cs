using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.Combinations;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions.Polynomials;

public class DfBernsteinPolynomial :
    DfPolynomial
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfBernsteinPolynomial CreateZero(int spaceDegree)
    {
        return new DfBernsteinPolynomial(
            spaceDegree,
            Float64Vector.Create()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfBernsteinPolynomial Create(int spaceDegree, Float64Vector scalarCoefficients)
    {
        if (scalarCoefficients.LastIndex > spaceDegree)
            return new DfBernsteinPolynomial(
                spaceDegree,
                scalarCoefficients.GetCopyByIndex(i => i <= spaceDegree)
            );

        return new DfBernsteinPolynomial(
            spaceDegree,
            scalarCoefficients
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfBernsteinPolynomial Create(int spaceDegree, IEnumerable<Tuple<double, DfBernsteinBasis>> scalarBasisList)
    {
        var scalarCoefficients = Float64VectorComposer.Create();

        foreach (var (scalar, basis) in scalarBasisList)
        {
            if (basis.SpaceDegree != spaceDegree)
                throw new InvalidOperationException();

            scalarCoefficients[basis.Degree] += scalar;
        }

        return new DfBernsteinPolynomial(spaceDegree, scalarCoefficients.GetVector());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfBernsteinPolynomial Create(int spaceDegree, IEnumerable<Tuple<double, DfBernsteinPolynomial>> scalarPolynomialList)
    {
        var scalarCoefficients = Float64VectorComposer.Create();

        foreach (var (scalar1, polynomial) in scalarPolynomialList)
        {
            if (polynomial.SpaceDegree != spaceDegree)
                throw new InvalidOperationException();

            foreach (var (scalar2, basis) in polynomial.GetScaledBasis())
                scalarCoefficients[basis.Degree] += scalar1 * scalar2;
        }

        return new DfBernsteinPolynomial(spaceDegree, scalarCoefficients.GetVector());
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfBernsteinPolynomial operator -(DfBernsteinPolynomial p1)
    {
        return p1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfBernsteinPolynomial operator +(DfBernsteinPolynomial p1, double p2)
    {
        return p1.Add(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfBernsteinPolynomial operator +(DfBernsteinPolynomial p1, DfBernsteinBasis p2)
    {
        return p1.Add(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfBernsteinPolynomial operator +(DfBernsteinPolynomial p1, DfBernsteinPolynomial p2)
    {
        return p1.Add(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfBernsteinPolynomial operator -(DfBernsteinPolynomial p1, double p2)
    {
        return p1.Subtract(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfBernsteinPolynomial operator -(DfBernsteinPolynomial p1, DfBernsteinBasis p2)
    {
        return p1.Subtract(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfBernsteinPolynomial operator -(DfBernsteinPolynomial p1, DfBernsteinPolynomial p2)
    {
        return p1.Subtract(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfBernsteinPolynomial operator *(DfBernsteinPolynomial p1, double p2)
    {
        return p1.Times(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfBernsteinPolynomial operator *(double p1, DfBernsteinPolynomial p2)
    {
        return p2.Times(p1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfBernsteinPolynomial operator *(DfBernsteinPolynomial p1, DfBernsteinBasis p2)
    {
        return p1.Times(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfBernsteinPolynomial operator *(DfBernsteinBasis p1, DfBernsteinPolynomial p2)
    {
        return p2.Times(p1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfBernsteinPolynomial operator *(DfBernsteinPolynomial p1, DfBernsteinPolynomial p2)
    {
        return p2.Times(p1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfBernsteinPolynomial operator /(DfBernsteinPolynomial p1, double p2)
    {
        return p1.Divide(p2);
    }


    public int SpaceDegree { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DfBernsteinPolynomial(int spaceDegree, Float64Vector scalarCoefficients) 
        : base(scalarCoefficients)
    {
        if (spaceDegree < 0)
            throw new ArgumentOutOfRangeException(nameof(spaceDegree));

        if (scalarCoefficients.LastIndex > spaceDegree)
            throw new InvalidOperationException();

        SpaceDegree = spaceDegree;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Tuple<double, DfBernsteinBasis>> GetScaledBasis()
    {
        return ScalarCoefficients.IndexScalarPairs.Select(r => 
            new Tuple<double, DfBernsteinBasis>(
                r.Value,
                DfBernsteinBasis.Create(SpaceDegree, r.Key)
            )
        );
    }

    public override double GetValue(double t)
    {
        var value = 0d;

        foreach (var (degree, scalar) in ScalarCoefficients.IndexScalarPairs)
        {
            value += 
                scalar *
                SpaceDegree.GetBinomialCoefficient(degree) *
                Math.Pow(t, degree) * 
                Math.Pow(1 - t, SpaceDegree - degree);
        }

        return value;
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
        var scalarPolynomialList = 
            GetScaledBasis().MapItem2(t => t.GetPolynomialDerivative1());

        return Create(SpaceDegree, scalarPolynomialList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfBernsteinPolynomial GetPolynomialDerivativeN(int order)
    {
        if (order < 0)
            throw new ArgumentOutOfRangeException(nameof(order));

        if (order == 0)
            return this;
        
        if (order > Degree)
            return CreateZero(SpaceDegree);

        var scalarPolynomialList = 
            GetScaledBasis().MapItem2(t => t.GetPolynomialDerivativeN(order));

        return Create(SpaceDegree, scalarPolynomialList);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfMonomialPolynomial ToMonomialPolynomial()
    {
        return DfMonomialPolynomial.Create(
            GetScaledBasis().MapItem2(
                b => b.ToMonomialPolynomial()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfBernsteinPolynomial Negative()
    {
        var scalarCoefficients = -ScalarCoefficients;

        return new DfBernsteinPolynomial(SpaceDegree, scalarCoefficients);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfBernsteinPolynomial Add(double p2)
    {
        if (p2.IsNaNOrInfinite())
            throw new ArgumentException(nameof(p2));

        var scalarCoefficients = 
            Float64VectorComposer.Create(ScalarCoefficients);

        scalarCoefficients[0] += p2;
        
        return new DfBernsteinPolynomial(SpaceDegree, scalarCoefficients.GetVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfBernsteinPolynomial Add(DfBernsteinBasis p2)
    {
        var scalarCoefficients = Float64VectorComposer.Create(ScalarCoefficients);

        scalarCoefficients[p2.Degree] += 1d;

        return new DfBernsteinPolynomial(SpaceDegree, scalarCoefficients.GetVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfBernsteinPolynomial Add(DfBernsteinBasis p2, double scalarFactor)
    {
        if (scalarFactor.IsNaNOrInfinite())
            throw new ArgumentException(nameof(scalarFactor));

        var scalarCoefficients = Float64VectorComposer.Create(ScalarCoefficients);

        scalarCoefficients[p2.Degree] += scalarFactor;

        return new DfBernsteinPolynomial(SpaceDegree, scalarCoefficients.GetVector());
    }

    public DfBernsteinPolynomial Add(DfBernsteinPolynomial p2)
    {
        var scalarCoefficients = Float64VectorComposer.Create(ScalarCoefficients);

        foreach (var (i, c) in p2.ScalarCoefficients.IndexScalarPairs)
            scalarCoefficients[i] += c;

        return new DfBernsteinPolynomial(SpaceDegree, scalarCoefficients.GetVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfBernsteinPolynomial Subtract(double p2)
    {
        if (p2.IsNaNOrInfinite())
            throw new ArgumentException(nameof(p2));

        var scalarCoefficients = Float64VectorComposer.Create(ScalarCoefficients);

        scalarCoefficients[0] -= p2;

        return new DfBernsteinPolynomial(SpaceDegree, scalarCoefficients.GetVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfBernsteinPolynomial Subtract(DfBernsteinBasis p2)
    {
        var scalarCoefficients = Float64VectorComposer.Create(ScalarCoefficients);

        scalarCoefficients[p2.Degree] -= 1d;

        return new DfBernsteinPolynomial(SpaceDegree, scalarCoefficients.GetVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfBernsteinPolynomial Subtract(DfBernsteinBasis p2, double scalarFactor)
    {
        if (scalarFactor.IsNaNOrInfinite())
            throw new ArgumentException(nameof(scalarFactor));

        var scalarCoefficients = Float64VectorComposer.Create(ScalarCoefficients);

        scalarCoefficients[p2.Degree] -= scalarFactor;

        return new DfBernsteinPolynomial(SpaceDegree, scalarCoefficients.GetVector());
    }

    public DfBernsteinPolynomial Subtract(DfBernsteinPolynomial p2)
    {
        var scalarCoefficients = Float64VectorComposer.Create(ScalarCoefficients);

        foreach (var (i, c) in p2.ScalarCoefficients.IndexScalarPairs)
            scalarCoefficients[i] -= c;

        return new DfBernsteinPolynomial(SpaceDegree, scalarCoefficients.GetVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfBernsteinPolynomial Times(double scalar)
    {
        if (scalar.IsNaNOrInfinite())
            throw new InvalidOperationException();

        var scalarCoefficients = 
            ScalarCoefficients * scalar;

        return new DfBernsteinPolynomial(SpaceDegree, scalarCoefficients);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfBernsteinPolynomial Times(DfBernsteinBasis p2)
    {
        var scalarCoefficients = Float64VectorComposer.Create();

        var i2 = p2.Degree;
        foreach (var (i1, c1) in ScalarCoefficients.IndexScalarPairs)
        {
            var i = i1 + i2;
            var j = Math.Abs(i1 - i2);
            var d = 0.5 * c1;

            scalarCoefficients[i] += d;
            scalarCoefficients[j] += d;
        }

        return new DfBernsteinPolynomial(SpaceDegree, scalarCoefficients.GetVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfBernsteinPolynomial Times(DfBernsteinBasis p2, double scalingFactor)
    {
        if (scalingFactor.IsNaNOrInfinite())
            throw new NotFiniteNumberException(nameof(scalingFactor));

        var scalarCoefficients = Float64VectorComposer.Create();

        var i2 = p2.Degree;
        foreach (var (i1, c1) in ScalarCoefficients.IndexScalarPairs)
        {
            var i = i1 + i2;
            var j = Math.Abs(i1 - i2);
            var d = 0.5 * c1 * scalingFactor;

            scalarCoefficients[i] += d;
            scalarCoefficients[j] += d;
        }

        return new DfBernsteinPolynomial(SpaceDegree, scalarCoefficients.GetVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfBernsteinPolynomial Times(DfBernsteinPolynomial p2)
    {
        var scalarCoefficients = Float64VectorComposer.Create();

        foreach (var (i1, c1) in ScalarCoefficients.IndexScalarPairs)
        foreach (var (i2, c2) in p2.ScalarCoefficients.IndexScalarPairs)
        {
            var i = i1 + i2;
            var j = Math.Abs(i1 - i2);
            var d = 0.5 * c1 * c2;

            scalarCoefficients[i] += d;
            scalarCoefficients[j] += d;
        }

        return new DfBernsteinPolynomial(SpaceDegree, scalarCoefficients.GetVector());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfBernsteinPolynomial Divide(double scalar)
    {
        return Times(1d / scalar);
    }

}