using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using MathNet.Numerics;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions.Polynomials;

/// <summary>
/// Chebyshev Type1 Basis Polynomials
/// </summary>
public class DfChebyshevBasis :
    DfPolynomialBasis
{
    /// <summary>
    /// This iterative method is less efficient for large degrees
    /// </summary>
    /// <param name="degree"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    private static double GetChebyshevValue1(int degree, double x)
    {
        var t0 = 1d;
        var t1 = x;
        x = 2d * x;

        for (var i = 2; i <= degree; i++)
        {
            var t2 = x * t1 - t0;

            t0 = t1;
            t1 = t2;
        }

        return t1;
    }

    /// <summary>
    /// This iterative method is more efficient for higher degrees
    /// </summary>
    /// <param name="degree"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static double GetChebyshevValue(int degree, double x)
    {
        if (degree < 0) throw new ArgumentOutOfRangeException(nameof(degree));
        if (degree == 0) return 1d;
        if (degree == 1) return x;
        if (degree == 2) return 2 * x * x - 1;

        if (x == 1d) return 1d;
        if (x == -1d) return (degree & 1) == 0 ? 1d : -1d;

        var degreeStack = new Stack<int>();
        var degreeValueDictionary = new SortedDictionary<int, double>
        {
            { 0, 1d },
            { 1, x },
            { 2, 2 * x * x - 1 },
            { degree, 0d }
        };

        degreeStack.Push(degree);

        while (degreeStack.Count > 0)
        {
            var n = degreeStack.Pop();

            if ((n & 1) == 0)
            {
                // Even n
                var m = n >> 1;

                if (m > 1 && !degreeValueDictionary.ContainsKey(m))
                {
                    degreeValueDictionary.Add(m, 0d);
                    degreeStack.Push(m);
                }
            }
            else
            {
                // Odd n
                var m = n + 1 >> 1;

                if (m > 1 && !degreeValueDictionary.ContainsKey(m))
                {
                    degreeValueDictionary.Add(m, 0d);
                    degreeStack.Push(m);
                }

                m--;
                if (m > 1 && !degreeValueDictionary.ContainsKey(m))
                {
                    degreeValueDictionary.Add(m, 0d);
                    degreeStack.Push(m);
                }
            }
        }

        //Console.WriteLine(
        //    degreeValueDictionary.Select(n => n.Key.ToString()).Concatenate(", ")
        //);

        var y = 0d;
        var degreeList =
            degreeValueDictionary.Keys.Skip(3).ToImmutableArray();

        foreach (var n in degreeList)
        {
            if (n.IsEven())
            {
                var m = n >> 1;
                var t1 = degreeValueDictionary[m];

                y = 2d * t1 * t1 - 1d;
            }
            else
            {
                var m = n + 1 >> 1;
                var t1 = degreeValueDictionary[m];
                var t2 = degreeValueDictionary[m - 1];

                y = 2d * t1 * t2 - x;
            }

            degreeValueDictionary[n] = y;
        }

        return y;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfChebyshevBasis Create(int degree)
    {
        return new DfChebyshevBasis(degree);
    }
    
    public static DfChebyshevPolynomial CreateDerivativePolynomial(int degree)
    {
        var n = degree;

        var scalarCoefficients = LinFloat64VectorComposer.Create();

        for (var k = 0; k <= n - 1; k++)
        {
            if ((n - 1 - k).IsOdd()) continue;

            scalarCoefficients[k] +=
                (k == 0 ? 0.5d : 1) * 2d * n;
        }

        return DfChebyshevPolynomial.Create(scalarCoefficients.GetVector());
    }

    public static DfChebyshevPolynomial CreateDerivativePolynomial(int degree, DfAffinePolynomial varToUnitMap, DfAffinePolynomial unitToVarMap)
    {
        var n = degree;

        var scalarCoefficients = LinFloat64VectorComposer.Create();

        for (var k = 0; k <= n - 1; k++)
        {
            if ((n - 1 - k).IsOdd()) continue;

            scalarCoefficients[k] +=
                (k == 0 ? 0.5d : 1) * 2d * n;
        }

        return DfChebyshevPolynomial.Create(
            scalarCoefficients.GetVector(),
            varToUnitMap,
            unitToVarMap
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DfChebyshevBasis(int degree)
        : base(degree < 0 ? 0 : degree)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double x)
    {
        return GetChebyshevValue(Degree, x);
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
    public DfChebyshevPolynomial GetPolynomialDerivative1()
    {
        return CreateDerivativePolynomial(Degree);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfChebyshevPolynomial GetPolynomialDerivative1(DfAffinePolynomial varToUnitMap, DfAffinePolynomial unitToVarMap)
    {
        return CreateDerivativePolynomial(
            Degree,
            varToUnitMap,
            unitToVarMap
        );
    }
    
    public DfChebyshevPolynomial GetPolynomialDerivativeN(int order)
    {
        if (order < 0)
            throw new ArgumentOutOfRangeException(nameof(order));

        if (order == 0)
            return ToChebyshevPolynomial();

        if (order > Degree)
            return DfChebyshevPolynomial.CreateZero();

        var derivative = ToChebyshevPolynomial();

        while (order > 0)
        {
            derivative = derivative.GetPolynomialDerivative1();

            order--;
        }

        return derivative;
    }
    
    public DfChebyshevPolynomial GetPolynomialDerivativeN(int order, DfAffinePolynomial varToUnitMap, DfAffinePolynomial unitToVarMap)
    {
        if (order < 0)
            throw new ArgumentOutOfRangeException(nameof(order));

        if (order == 0)
            return ToChebyshevPolynomial(varToUnitMap, unitToVarMap);

        if (order > Degree)
            return DfChebyshevPolynomial.CreateZero();

        var derivative = 
            ToChebyshevPolynomial(varToUnitMap, unitToVarMap);

        while (order > 0)
        {
            derivative = derivative.GetPolynomialDerivative1();

            order--;
        }

        return derivative;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfChebyshevPolynomial ToChebyshevPolynomial()
    {
        var scalarCoefficients = LinFloat64VectorComposer.Create(Degree, 1);

        return DfChebyshevPolynomial.Create(scalarCoefficients.GetVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfChebyshevPolynomial ToChebyshevPolynomial(DfAffinePolynomial varToUnitMap, DfAffinePolynomial unitToVarMap)
    {
        var scalarCoefficients = LinFloat64VectorComposer.Create(Degree, 1);

        return DfChebyshevPolynomial.Create(
            scalarCoefficients.GetVector(),
            varToUnitMap,
            unitToVarMap
        );
    }

    
    public DfMonomialPolynomial ToMonomialPolynomial()
    {
        var scalarCoefficients = LinFloat64VectorComposer.Create();

        var n = Degree;
        for (var  k = 0; k < n / 2; k++)
        {
            var degree = n - 2 * k;

            var rational = FactoredRationalInt64.Create();

            rational
                .TimesPower(2, degree)
                .TimesFactorial(n - k - 1)
                .DivideFactorial(degree)
                .DivideFactorial(k);

            if (k.IsOdd())
                rational.SetNegative();
            
            scalarCoefficients[degree] = 0.5d * n * rational.ToFloat64();
        }

        return DfMonomialPolynomial.Create(scalarCoefficients.GetVector());
    }

}