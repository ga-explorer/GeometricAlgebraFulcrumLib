using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Symbolic.Mathematica;
using GeometricAlgebraLib.Symbolic.Mathematica.ExprFactory;
using GeometricAlgebraLib.SymbolicExpressions;
using GeometricAlgebraLib.SymbolicExpressions.Context;
using Wolfram.NETLink;

namespace GeometricAlgebraLib.Symbolic.Processors
{
    public sealed class GaScalarProcessorMathematicaExpr
        : IGaSymbolicScalarProcessor<Expr>
    {
        public static GaScalarProcessorMathematicaExpr DefaultProcessor { get; }
            = new();

        public int RoundingPlaces { get; set; }
            = 13;

        public double ZeroEpsilon 
            => Math.Pow(10, -RoundingPlaces);
        
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
        
        public Expr Add(Expr scalar1, Expr scalar2)
        {
            return PostProcessScalar(Mfs.Plus[
                PreProcessScalar(scalar1), 
                PreProcessScalar(scalar2)
            ]);
        }

        public Expr Add(params Expr[] scalarsList)
        {
            return PostProcessScalar(Mfs.SumExpr(
                scalarsList.Select(PreProcessScalar).ToArray()
            ));
        }

        public Expr Add(IEnumerable<Expr> scalarsList)
        {
            return PostProcessScalar(Mfs.SumExpr(
                scalarsList.Select(PreProcessScalar).ToArray()
            ));
        }

        public Expr Subtract(Expr scalar1, Expr scalar2)
        {
            return PostProcessScalar(Mfs.Subtract[
                PreProcessScalar(scalar1), 
                PreProcessScalar(scalar2)
            ]);
        }

        public Expr Times(Expr scalar1, Expr scalar2)
        {
            return PostProcessScalar(Mfs.Times[
                PreProcessScalar(scalar1), 
                PreProcessScalar(scalar2)
            ]);
        }

        public Expr Times(params Expr[] scalarsList)
        {
            return PostProcessScalar(Mfs.ProductExpr(
                scalarsList.Select(PreProcessScalar).ToArray()
            ));
        }

        public Expr Times(IEnumerable<Expr> scalarsList)
        {
            return PostProcessScalar(Mfs.ProductExpr(
                scalarsList.Select(PreProcessScalar).ToArray()
            ));
        }

        public Expr NegativeTimes(Expr scalar1, Expr scalar2)
        {
            return PostProcessScalar(Mfs.Minus[Mfs.Times[
                PreProcessScalar(scalar1), 
                PreProcessScalar(scalar2)
            ]]);
        }

        public Expr NegativeTimes(params Expr[] scalarsList)
        {
            return PostProcessScalar(Mfs.ProductExpr(
                true, 
                scalarsList.Select(PreProcessScalar).ToArray()
            ));
        }

        public Expr NegativeTimes(IEnumerable<Expr> scalarsList)
        {
            return PostProcessScalar(Mfs.ProductExpr(
                true, 
                scalarsList.Select(PreProcessScalar).ToArray()
            ));
        }

        public Expr Divide(Expr scalar1, Expr scalar2)
        {
            return PostProcessScalar(Mfs.Divide[
                PreProcessScalar(scalar1), 
                PreProcessScalar(scalar2)
            ]);
        }

        public Expr NegativeDivide(Expr scalar1, Expr scalar2)
        {
            return PostProcessScalar(Mfs.Minus[Mfs.Divide[
                PreProcessScalar(scalar1), 
                PreProcessScalar(scalar2)
            ]]);
        }

        public Expr Positive(Expr scalar)
        {
            return PostProcessScalar(
                PreProcessScalar(scalar)
            );
        }

        public Expr Negative(Expr scalar)
        {
            return PostProcessScalar(Mfs.Minus[
                PreProcessScalar(scalar)
            ]);
        }

        public Expr Inverse(Expr scalar)
        {
            return PostProcessScalar(Mfs.Divide[
                Expr.INT_ONE, 
                PreProcessScalar(scalar)
            ]);
        }

        public Expr Abs(Expr scalar)
        {
            return PostProcessScalar(Mfs.Abs[
                PreProcessScalar(scalar)
            ]);
        }

        public Expr Sqrt(Expr scalar)
        {
            return PostProcessScalar(Mfs.Sqrt[
                PreProcessScalar(scalar)
            ]);
        }

        public Expr SqrtOfAbs(Expr scalar)
        {
            return PostProcessScalar(Mfs.Sqrt[Mfs.Abs[
                PreProcessScalar(scalar)
            ]]);
        }

        public Expr Exp(Expr scalar)
        {
            return PostProcessScalar(Mfs.Exp[
                PreProcessScalar(scalar)
            ]);
        }

        public Expr Log(Expr scalar)
        {
            return PostProcessScalar(Mfs.Log[
                PreProcessScalar(scalar)
            ]);
        }

        public Expr Log2(Expr scalar)
        {
            return PostProcessScalar(Mfs.Log2[
                PreProcessScalar(scalar)
            ]);
        }

        public Expr Log10(Expr scalar)
        {
            return PostProcessScalar(Mfs.Log2[
                PreProcessScalar(scalar)
            ]);
        }

        public Expr Log(Expr scalar, Expr baseScalar)
        {
            return PostProcessScalar(Mfs.Log[
                PreProcessScalar(scalar),
                PreProcessScalar(baseScalar)
            ]);
        }

        public Expr Cos(Expr scalar)
        {
            return PostProcessScalar(Mfs.Cos[
                PreProcessScalar(scalar)
            ]);
        }

        public Expr Sin(Expr scalar)
        {
            return PostProcessScalar(Mfs.Sin[
                PreProcessScalar(scalar)
            ]);
        }

        public Expr Tan(Expr scalar)
        {
            return PostProcessScalar(Mfs.Tan[
                PreProcessScalar(scalar)
            ]);
        }

        public Expr ArcCos(Expr scalar)
        {
            return PostProcessScalar(Mfs.ArcCos[
                PreProcessScalar(scalar)
            ]);
        }

        public Expr ArcSin(Expr scalar)
        {
            return PostProcessScalar(Mfs.ArcSin[
                PreProcessScalar(scalar)
            ]);
        }

        public Expr ArcTan(Expr scalar)
        {
            return PostProcessScalar(Mfs.ArcTan[
                PreProcessScalar(scalar)
            ]);
        }

        public Expr ArcTan2(Expr scalarX, Expr scalarY)
        {
            return PostProcessScalar(Mfs.ArcTan[
                PreProcessScalar(scalarX), 
                PreProcessScalar(scalarY)
            ]);
        }

        public bool IsValid(Expr scalar)
        {
            return true;
        }

        public bool IsZero(Expr scalar)
        {
            return scalar.IsZero();
        }

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

        public Expr TextToScalar(string text)
        {
            return text.ToExpr();
        }

        public Expr IntegerToScalar(int value)
        {
            return value.ToExpr();
        }

        public Expr Float64ToScalar(double value)
        {
            return value.ToExpr();
        }

        public Expr GetRandomScalar(Random randomGenerator, double minValue, double maxValue)
        {
            var value = minValue + (maxValue - minValue) * randomGenerator.NextDouble();

            return value.ToExpr(); 
        }

        public string ToText(Expr scalar)
        {
            return scalar.ToString();
        }

        public Expr Simplify(Expr scalar)
        {
            return scalar.Simplify();
        }

        public Expr GetSymbol(string symbolNameText)
        {
            return symbolNameText.ToSymbolExpr();
        }

        public Expr SymbolicExpressionToScalar(ISymbolicExpression expression)
        {
            return expression.ToString().ToExpr();
        }

        public ISymbolicExpression ScalarToSymbolicExpression(SymbolicContext context, Expr scalar)
        {
            return context.ToSymbolicExpression(scalar);
        }
    }
}