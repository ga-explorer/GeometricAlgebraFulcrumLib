using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.GeometricAlgebra;

public static class LinMathematicaUtils
{
    public static ScalarProcessorOfWolframExpr ScalarProcessor
        => ScalarProcessorOfWolframExpr.DefaultProcessor;
        
    //public static MatrixAlgebraMathematicaProcessor MatrixProcessor
    //    => MatrixAlgebraMathematicaProcessor.DefaultProcessor;

    public static RGaProcessor<Expr> EuclideanProcessor { get; }
        = RGaProcessor<Expr>.CreateEuclidean(ScalarProcessor);

    public static LaTeXComposerExpr LaTeXComposer
        => LaTeXComposerExpr.DefaultComposer;

    public static TextComposerExpr TextComposer
        => TextComposerExpr.DefaultComposer;
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<Expr> ToSymbolic(this Float64Vector mv)
    {
        var indexScalarDictionary = mv.ToDictionary(
            p => p.Key,
            p => p.Value.ToExpr()
        );

        return ScalarProcessor.CreateLinVector(indexScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector ToNumeric(this LinVector<Expr> mv)
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