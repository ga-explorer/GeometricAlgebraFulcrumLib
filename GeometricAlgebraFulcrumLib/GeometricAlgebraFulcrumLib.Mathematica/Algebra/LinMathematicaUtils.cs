using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using Wolfram.NETLink;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Mathematica.Algebra.LinearAlgebra;

public static class LinMathematicaUtils
{
    public static ScalarProcessorOfWolframExpr ScalarProcessor
        => ScalarProcessorOfWolframExpr.Instance;

    //public static MatrixAlgebraMathematicaProcessor MatrixProcessor
    //    => MatrixAlgebraMathematicaProcessor.Instance;

    public static RGaProcessor<Expr> EuclideanProcessor { get; }
        = RGaProcessor<Expr>.CreateEuclidean(ScalarProcessor);

    public static LaTeXComposerOfWolframExpr LaTeXComposer
        => LaTeXComposerOfWolframExpr.DefaultComposer;

    public static TextComposerExpr TextComposer
        => TextComposerExpr.DefaultComposer;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<Expr> ToSymbolic(this LinFloat64Vector mv)
    {
        var indexScalarDictionary = mv.ToDictionary(
            p => p.Key,
            p => p.Value.ToExpr()
        );

        return ScalarProcessor.CreateLinVector(indexScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector ToNumeric(this LinVector<Expr> mv)
    {
        var indexScalarDictionary = mv.ToDictionary(
            p => p.Key,
            p => p.Value.ToNumber()
        );

        return indexScalarDictionary.CreateLinVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<Expr> SimplifyScalars(this LinVector<Expr> mv)
    {
        return mv.MapScalars(
            scalar => scalar.Simplify()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<Expr> SimplifyScalars(this LinVector<Expr> mv, Expr assumptionsExpr)
    {
        return mv.MapScalars(
            scalar => scalar.Simplify(assumptionsExpr)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<Expr> SimplifyScalars(this LinUnilinearMap<Expr> mv)
    {
        return mv.MapScalars(
            scalar => scalar.Simplify()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<Expr> FullSimplifyScalars(this LinVector<Expr> mv)
    {
        return mv.MapScalars(
            scalar => scalar.FullSimplify()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<Expr> FullSimplifyScalars(this LinVector<Expr> mv, Expr assumptionsExpr)
    {
        return mv.MapScalars(
            scalar => scalar.FullSimplify(assumptionsExpr)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<Expr> FullSimplifyScalars(this LinUnilinearMap<Expr> mv)
    {
        return mv.MapScalars(
            scalar => scalar.FullSimplify()
        );
    }

}