using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.Algebra.ComplexAlgebra
{
    public static class CaMathematicaUtils
    {
        public static ScalarProcessorOfWolframExpr ScalarProcessor
            => ScalarProcessorOfWolframExpr.Instance;

        //public static MatrixAlgebraMathematicaProcessor MatrixProcessor
        //    => MatrixAlgebraMathematicaProcessor.Instance;

        public static LaTeXComposerOfWolframExpr LaTeXComposer
            => LaTeXComposerOfWolframExpr.DefaultComposer;

        public static TextComposerExpr TextComposer
            => TextComposerExpr.DefaultComposer;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComplexNumber<Expr> ToSymbolic(this Complex mv)
        {
            return ScalarProcessor.CreateComplexNumber(
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
            return TextComposer.GetNumberText(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetLaTeX(this ComplexNumber<Expr> mv)
        {
            return LaTeXComposer.GetNumberText(mv);
        }

    }
}
