using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Mutable;

namespace GeometricAlgebraFulcrumLib.MathBase.Differential.Functions.Polynomials
{
    public class DfMonomialBasis :
        DfPolynomialBasis
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfMonomialBasis Create(int power)
        {
            return new DfMonomialBasis(power);
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DfMonomialBasis(int degree) 
            : base(degree)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetValue(double t)
        {
            return Math.Pow(t, Degree);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Tuple<bool, DifferentialFunction> TrySimplify()
        {
            return new Tuple<bool, DifferentialFunction>(false, this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction Simplify()
        {
            return this;
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
        public DfMonomialPolynomial GetPolynomialDerivative1()
        {
            if (Degree == 0)
                return DfMonomialPolynomial.CreateZero();

            var scalarCoefficients = new Float64SparseTuple
            {
                [Degree - 1] = Degree
            };

            return DfMonomialPolynomial.Create(scalarCoefficients);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfMonomialPolynomial GetPolynomialDerivativeN(int order)
        {
            if (order < 0)
                throw new ArgumentOutOfRangeException(nameof(order));

            if (order == 0)
                return ToMonomialPolynomial();

            if (Degree < order)
                return DfMonomialPolynomial.CreateZero();

            var degree = Degree;
            var scalar = 1d;
            while (order > 0)
            {
                scalar *= degree;

                degree--;
                order--;
            }

            var scalarCoefficients = new Float64SparseTuple
            {
                [degree] = scalar
            };

            return DfMonomialPolynomial.Create(scalarCoefficients);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfMonomialPolynomial ToMonomialPolynomial()
        {
            var scalarCoefficients = new Float64SparseTuple
            {
                [Degree] = 1d
            };

            return DfMonomialPolynomial.Create(scalarCoefficients);
        }

        public DfBernsteinPolynomial ToBernsteinPolynomial(int spaceDegree)
        {
            if (spaceDegree < 0 || spaceDegree < Degree)
                throw new ArgumentOutOfRangeException(nameof(spaceDegree));

            var scalarCoefficients = new Float64SparseTuple();

            var n = spaceDegree;
            var k = Degree;
            var r = 
                FactoredRationalInt64.Create().DivideCombination(n, k);

            for (var j = k; j <= n; j++)
            {
                var degree = n - 2 * k;

                var rational = 
                    r.GetCopy().TimesCombination(j, k);

                if (k.IsOdd())
                    rational.SetNegative();
            
                scalarCoefficients[degree] = 0.5d * n * rational.ToFloat64();
            }

            return DfBernsteinPolynomial.Create(spaceDegree, scalarCoefficients);
        }
    
        public DfChebyshevPolynomial ToChebyshevPolynomial()
        {
            var scalarCoefficients = new Float64SparseTuple();

            var n = Degree;
            for (var j = 0; j <= n; j++)
            {
                if ((n - j).IsOdd()) continue;

                var rational = 
                    FactoredRationalInt64
                        .Create()
                        .TimesCombination(n, (n - j) / 2)
                        .TimesPower(2, 1 - n);

                if (j == 0)
                    rational.DivideValue(2);
            
                scalarCoefficients[j] = rational.ToFloat64();
            }

            return DfChebyshevPolynomial.Create(scalarCoefficients);
        }
    }
}