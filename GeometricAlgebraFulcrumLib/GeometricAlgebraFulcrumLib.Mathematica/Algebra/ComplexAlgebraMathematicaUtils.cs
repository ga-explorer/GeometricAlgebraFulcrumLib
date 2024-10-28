using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.Algebra
{
    public static class ComplexAlgebraMathematicaUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComplexNumber<Expr> ToSymbolic(this Complex mv)
        {
            return ScalarProcessorOfWolframExpr.Instance.CreateComplexNumber(
                mv.Real.ToExpr(),
                mv.Imaginary.ToExpr()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex ToNumeric(this ComplexNumber<Expr> mv)
        {
            return new Complex(
                mv.RealValue.ToNumber(),
                mv.ImaginaryValue.ToNumber()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComplexNumber<Expr> SimplifyScalars(this ComplexNumber<Expr> mv)
        {
            return mv.MapScalars(
                scalar => scalar.Simplify()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComplexNumber<Expr> SimplifyScalars(this ComplexNumber<Expr> mv, Expr assumeExpr)
        {
            return mv.MapScalars(
                scalar => scalar.Simplify(assumeExpr)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComplexNumber<Expr> FullSimplifyScalars(this ComplexNumber<Expr> mv)
        {
            return mv.MapScalars(
                scalar => scalar.FullSimplify()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComplexNumber<Expr> FullSimplifyScalars(this ComplexNumber<Expr> mv, Expr assumeExpr)
        {
            return mv.MapScalars(
                scalar => scalar.FullSimplify(assumeExpr)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetText(this ComplexNumber<Expr> mv)
        {
            return TextComposerExpr.DefaultComposer.GetNumberText(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetLaTeX(this ComplexNumber<Expr> mv)
        {
            return LaTeXComposerOfWolframExpr.DefaultComposer.GetNumberText(mv);
        }

    }
}
