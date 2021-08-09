using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Symbolic.Processors
{
    public sealed class GaScalarProcessorMathematicaExpr
        : IGaScalarProcessorSymbolic<Expr>
    {
        public static GaScalarProcessorMathematicaExpr DefaultProcessor { get; }
            = new GaScalarProcessorMathematicaExpr();


        public int RoundingPlaces { get; set; }
            = 13;

        public double ZeroEpsilon 
            => Math.Pow(10, -RoundingPlaces);

        public bool IsNumeric => false;

        public bool IsSymbolic => true;

        public Expr ZeroScalar 
            => Expr.INT_ZERO;
        
        public Expr OneScalar 
            => Expr.INT_ONE;

        public Expr MinusOneScalar 
            => Expr.INT_MINUSONE;

        public Expr PiScalar 
            => MathematicaInterface.DefaultCasConstants.ExprPi;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr PreProcessScalar([NotNull] Expr scalar)
        {
            return scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr PostProcessScalar([NotNull] Expr scalar)
        {
            return scalar.Simplify();
            //return Mfs.Round[Mfs.N[scalar], ZeroEpsilon.ToExpr()].Simplify();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Add(Expr scalar1, Expr scalar2)
        {
            return PostProcessScalar(Mfs.Plus[
                PreProcessScalar(scalar1), 
                PreProcessScalar(scalar2)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Add(params Expr[] scalarsList)
        {
            return PostProcessScalar(Mfs.SumExpr(
                scalarsList.Select(PreProcessScalar).ToArray()
            ));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Add(IEnumerable<Expr> scalarsList)
        {
            return PostProcessScalar(Mfs.SumExpr(
                scalarsList.Select(PreProcessScalar).ToArray()
            ));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Subtract(Expr scalar1, Expr scalar2)
        {
            return PostProcessScalar(Mfs.Subtract[
                PreProcessScalar(scalar1), 
                PreProcessScalar(scalar2)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Times(Expr scalar1, Expr scalar2)
        {
            return PostProcessScalar(Mfs.Times[
                PreProcessScalar(scalar1), 
                PreProcessScalar(scalar2)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Times(params Expr[] scalarsList)
        {
            return PostProcessScalar(Mfs.ProductExpr(
                scalarsList.Select(PreProcessScalar).ToArray()
            ));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Times(IEnumerable<Expr> scalarsList)
        {
            return PostProcessScalar(Mfs.ProductExpr(
                scalarsList.Select(PreProcessScalar).ToArray()
            ));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr NegativeTimes(Expr scalar1, Expr scalar2)
        {
            return PostProcessScalar(Mfs.Minus[Mfs.Times[
                PreProcessScalar(scalar1), 
                PreProcessScalar(scalar2)
            ]]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr NegativeTimes(params Expr[] scalarsList)
        {
            return PostProcessScalar(Mfs.ProductExpr(
                true, 
                scalarsList.Select(PreProcessScalar).ToArray()
            ));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr NegativeTimes(IEnumerable<Expr> scalarsList)
        {
            return PostProcessScalar(Mfs.ProductExpr(
                true, 
                scalarsList.Select(PreProcessScalar).ToArray()
            ));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Divide(Expr scalar1, Expr scalar2)
        {
            return PostProcessScalar(Mfs.Divide[
                PreProcessScalar(scalar1), 
                PreProcessScalar(scalar2)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr NegativeDivide(Expr scalar1, Expr scalar2)
        {
            return PostProcessScalar(Mfs.Minus[Mfs.Divide[
                PreProcessScalar(scalar1), 
                PreProcessScalar(scalar2)
            ]]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Positive(Expr scalar)
        {
            return PostProcessScalar(
                PreProcessScalar(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Negative(Expr scalar)
        {
            return PostProcessScalar(Mfs.Minus[
                PreProcessScalar(scalar)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Inverse(Expr scalar)
        {
            return PostProcessScalar(Mfs.Divide[
                Expr.INT_ONE, 
                PreProcessScalar(scalar)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Abs(Expr scalar)
        {
            return PostProcessScalar(Mfs.Abs[
                PreProcessScalar(scalar)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Sqrt(Expr scalar)
        {
            return PostProcessScalar(Mfs.Sqrt[
                PreProcessScalar(scalar)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr SqrtOfAbs(Expr scalar)
        {
            return PostProcessScalar(Mfs.Sqrt[Mfs.Abs[
                PreProcessScalar(scalar)
            ]]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Exp(Expr scalar)
        {
            return PostProcessScalar(Mfs.Exp[
                PreProcessScalar(scalar)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Log(Expr scalar)
        {
            return PostProcessScalar(Mfs.Log[
                PreProcessScalar(scalar)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Log2(Expr scalar)
        {
            return PostProcessScalar(Mfs.Log2[
                PreProcessScalar(scalar)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Log10(Expr scalar)
        {
            return PostProcessScalar(Mfs.Log2[
                PreProcessScalar(scalar)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Log(Expr scalar, Expr baseScalar)
        {
            return PostProcessScalar(Mfs.Log[
                PreProcessScalar(scalar),
                PreProcessScalar(baseScalar)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Cos(Expr scalar)
        {
            return PostProcessScalar(Mfs.Cos[
                PreProcessScalar(scalar)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Sin(Expr scalar)
        {
            return PostProcessScalar(Mfs.Sin[
                PreProcessScalar(scalar)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Tan(Expr scalar)
        {
            return PostProcessScalar(Mfs.Tan[
                PreProcessScalar(scalar)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr ArcCos(Expr scalar)
        {
            return PostProcessScalar(Mfs.ArcCos[
                PreProcessScalar(scalar)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr ArcSin(Expr scalar)
        {
            return PostProcessScalar(Mfs.ArcSin[
                PreProcessScalar(scalar)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr ArcTan(Expr scalar)
        {
            return PostProcessScalar(Mfs.ArcTan[
                PreProcessScalar(scalar)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr ArcTan2(Expr scalarX, Expr scalarY)
        {
            return PostProcessScalar(Mfs.ArcTan[
                PreProcessScalar(scalarX), 
                PreProcessScalar(scalarY)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Cosh(Expr scalar)
        {
            return PostProcessScalar(Mfs.Cosh[
                PreProcessScalar(scalar)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Sinh(Expr scalar)
        {
            return PostProcessScalar(Mfs.Sinh[
                PreProcessScalar(scalar)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Tanh(Expr scalar)
        {
            return PostProcessScalar(Mfs.Tanh[
                PreProcessScalar(scalar)
            ]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(Expr scalar)
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(Expr scalar)
        {
            return scalar.IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(Expr scalar, bool nearZeroFlag)
        {
            if (scalar.IsZero())
                return true;

            if (!nearZeroFlag)
                return false;

            if (!scalar.NumberQ())
                return false;

            var number = 
                scalar.ToNumber();

            return number > -ZeroEpsilon && number < ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(Expr scalar)
        {
            if (scalar.IsZero())
                return true;

            if (!scalar.NumberQ())
                return false;

            var number = 
                scalar.ToNumber();

            return number > -ZeroEpsilon && number < ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(Expr scalar)
        {
            if (!scalar.NumberQ())
                return false;

            var number = 
                scalar.ToNumber();

            return number > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(Expr scalar)
        {
            if (!scalar.NumberQ())
                return false;

            var number = 
                scalar.ToNumber();

            return number < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(Expr scalar)
        {
            if (!scalar.NumberQ())
                return false;

            var number = 
                scalar.ToNumber();

            return number < -ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(Expr scalar)
        {
            if (!scalar.NumberQ())
                return false;

            var number = 
                scalar.ToNumber();

            return number > ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr TextToScalar(string text)
        {
            return text.ToExpr();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr IntegerToScalar(int value)
        {
            return value.ToExpr();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Float64ToScalar(double value)
        {
            return value.ToExpr();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr GetRandomScalar(Random randomGenerator, double minValue, double maxValue)
        {
            var value = minValue + (maxValue - minValue) * randomGenerator.NextDouble();

            return value.ToExpr(); 
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToText(Expr scalar)
        {
            return scalar.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Simplify(Expr scalar)
        {
            return scalar.Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr GetSymbol(string symbolNameText)
        {
            return symbolNameText.ToSymbolExpr();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr SymbolicExpressionToScalar(ISymbolicExpression expression)
        {
            return expression.ToString().ToExpr();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression ScalarToSymbolicExpression(SymbolicContext context, Expr scalar)
        {
            return context.ToSymbolicExpression(scalar);
        }
    }
}