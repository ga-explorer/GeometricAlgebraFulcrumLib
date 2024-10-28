using System;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using Wolfram.NETLink;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Mathematica.Algebra;

public static class RGaMathematicaUtils
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
    public static RGaMultivector<Expr> ToSymbolic(this RGaMultivector<double> mv)
    {
        return mv.MapScalars(
            EuclideanProcessor,
            number => number.ToExpr()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<double> ToNumeric(this RGaMultivector<Expr> mv, RGaProcessor<double> processor)
    {
        return mv.MapScalars(
            processor,
            number => number.ToNumber()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<Expr> SimplifyScalar(this RGaScalar<Expr> mv)
    {
        return mv.Processor.Scalar(
            mv.ScalarValue.Simplify()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<Expr> SimplifyScalar(this RGaScalar<Expr> mv, Expr assumeExpr)
    {
        return mv.Processor.Scalar(
            mv.ScalarValue.Simplify()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<Expr> SimplifyScalars(this RGaVector<Expr> mv)
    {
        return mv.MapScalars(
            scalar => scalar.Simplify()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<Expr> SimplifyScalars(this RGaVector<Expr> mv, Expr assumeExpr)
    {
        return mv.MapScalars(
            scalar => scalar.Simplify(assumeExpr)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<Expr> SimplifyScalars(this RGaBivector<Expr> mv)
    {
        return mv.MapScalars(
            scalar => scalar.Simplify()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<Expr> SimplifyScalars(this RGaBivector<Expr> mv, Expr assumeExpr)
    {
        return mv.MapScalars(
            scalar => scalar.Simplify(assumeExpr)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<Expr> SimplifyScalars(this RGaKVector<Expr> mv)
    {
        return mv.MapScalars(
            scalar => scalar.Simplify()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<Expr> SimplifyScalars(this RGaKVector<Expr> mv, Expr assumeExpr)
    {
        return mv.MapScalars(
            scalar => scalar.Simplify(assumeExpr)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<Expr> SimplifyScalars(this RGaMultivector<Expr> mv)
    {
        return mv.MapScalars(
            scalar => scalar.Simplify()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<Expr> SimplifyScalars(this RGaMultivector<Expr> mv, Expr assumeExpr)
    {
        return mv.MapScalars(
            scalar => scalar.Simplify(assumeExpr)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaPureRotor<Expr> SimplifyScalars(this RGaPureRotor<Expr> rotor)
    {
        return rotor
            .Multivector
            .MapScalars(scalar => scalar.Simplify())
            .CreatePureRotor();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaPureRotor<Expr> SimplifyScalars(this RGaPureRotor<Expr> rotor, Expr assumeExpr)
    {
        return rotor
            .Multivector
            .MapScalars(scalar => scalar.Simplify(assumeExpr))
            .CreatePureRotor();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<Expr> TrigExpandScalar(this RGaScalar<Expr> v)
    {
        return v.Processor.Scalar(
            v.ScalarValue.TrigExpand()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<Expr> TrigExpandScalars(this RGaVector<Expr> v)
    {
        return v.MapScalars(s => s.TrigExpand());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<Expr> TrigExpandScalars(this RGaBivector<Expr> v)
    {
        return v.MapScalars(s => s.TrigExpand());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<Expr> TrigExpandScalars(this RGaKVector<Expr> v)
    {
        return v.MapScalars(s => s.TrigExpand());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<Expr> TrigExpandScalars(this RGaMultivector<Expr> v)
    {
        return v.MapScalars(s => s.TrigExpand());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<Expr> TrigReduceScalar(this RGaScalar<Expr> v)
    {
        return v.Processor.Scalar(
            v.ScalarValue.TrigReduce()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<Expr> TrigReduceScalars(this RGaVector<Expr> v)
    {
        return v.MapScalars(s => s.TrigReduce());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<Expr> TrigReduceScalars(this RGaBivector<Expr> v)
    {
        return v.MapScalars(s => s.TrigReduce());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<Expr> TrigReduceScalars(this RGaKVector<Expr> v)
    {
        return v.MapScalars(s => s.TrigReduce());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<Expr> TrigReduceScalars(this RGaMultivector<Expr> v)
    {
        return v.MapScalars(s => s.TrigReduce());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<Expr> CollectScalar(this RGaScalar<Expr> mv, Expr symbolExpr)
    {
        return mv.MapScalar(s => s.Collect(symbolExpr));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<Expr> CollectScalars(this RGaVector<Expr> vector, Expr t)
    {
        return vector.MapScalars(scalar =>
            Mfs.Collect[scalar, t].Evaluate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<Expr> CollectScalars(this RGaBivector<Expr> vector, Expr t)
    {
        return vector.MapScalars(scalar =>
            Mfs.Collect[scalar, t].Evaluate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<Expr> CollectScalars(this RGaKVector<Expr> vector, Expr t)
    {
        return vector.MapScalars(scalar =>
            Mfs.Collect[scalar, t].Evaluate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<Expr> CollectScalars(this RGaMultivector<Expr> vector, Expr t)
    {
        return vector.MapScalars(scalar =>
            Mfs.Collect[scalar, t].Evaluate()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<Expr> SimplifyCollectScalar(this RGaScalar<Expr> vector, Expr t)
    {
        return vector.MapScalar(scalar =>
            Mfs.Collect[Mfs.Simplify[scalar], t].Evaluate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<Expr> SimplifyCollectScalars(this RGaVector<Expr> vector, Expr t)
    {
        return vector.MapScalars(scalar =>
            Mfs.Collect[Mfs.Simplify[scalar], t].Evaluate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<Expr> SimplifyCollectScalars(this RGaBivector<Expr> vector, Expr t)
    {
        return vector.MapScalars(scalar =>
            Mfs.Collect[Mfs.Simplify[scalar], t].Evaluate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<Expr> SimplifyCollectScalars(this RGaKVector<Expr> vector, Expr t)
    {
        return vector.MapScalars(scalar =>
            Mfs.Collect[Mfs.Simplify[scalar], t].Evaluate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<Expr> SimplifyCollectScalars(this RGaMultivector<Expr> vector, Expr t)
    {
        return vector.MapScalars(scalar =>
            Mfs.Collect[Mfs.Simplify[scalar], t].Evaluate()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<Expr> FullSimplifyScalar(this RGaScalar<Expr> mv)
    {
        return mv.MapScalar(scalar => scalar.FullSimplify());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<Expr> FullSimplifyScalars(this RGaScalar<Expr> mv, Expr assumptionsExpr)
    {
        return mv.MapScalar(scalar => scalar.FullSimplify(assumptionsExpr));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<Expr> FullSimplifyScalars(this RGaVector<Expr> mv)
    {
        return mv.MapScalars(scalar => scalar.FullSimplify());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<Expr> FullSimplifyScalars(this RGaVector<Expr> mv, Expr assumptionsExpr)
    {
        return mv.MapScalars(scalar => scalar.FullSimplify(assumptionsExpr));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<Expr> FullSimplifyScalars(this RGaBivector<Expr> mv)
    {
        return mv.MapScalars(scalar => scalar.FullSimplify());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<Expr> FullSimplifyScalars(this RGaBivector<Expr> mv, Expr assumptionsExpr)
    {
        return mv.MapScalars(scalar => scalar.FullSimplify(assumptionsExpr));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<Expr> FullSimplifyScalars(this RGaKVector<Expr> mv)
    {
        return mv.MapScalars(scalar => scalar.FullSimplify());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<Expr> FullSimplifyScalars(this RGaKVector<Expr> mv, Expr assumptionsExpr)
    {
        return mv.MapScalars(scalar => scalar.FullSimplify(assumptionsExpr));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<Expr> FullSimplifyScalars(this RGaMultivector<Expr> mv)
    {
        return mv.MapScalars(scalar => scalar.FullSimplify());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<Expr> FullSimplifyScalars(this RGaMultivector<Expr> mv, Expr assumptionsExpr)
    {
        return mv.MapScalars(scalar => scalar.FullSimplify(assumptionsExpr));
    }

    public static Expr AsListExpr(this RGaMultivector<Expr> vector)
    {
        var n = vector.VSpaceDimensions;
        var listExprArgs = new object[n];

        for (var i = 0; i < n; i++)
            listExprArgs[i] = vector.Scalar(i).ScalarValue;

        return Mfs.List[listExprArgs];
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<Expr> DifferentiateScalar(this RGaScalar<Expr> mv, string variableName, int degree = 1)
    {
        return mv.MapScalar(scalar => scalar.DifferentiateScalar(variableName, degree));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<Expr> DifferentiateScalar(this RGaScalar<Expr> mv, Expr variableExpr, int degree = 1)
    {
        return mv.MapScalar(scalar => scalar.DifferentiateScalar(variableExpr, degree));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<Expr> DifferentiateScalar(this RGaScalar<Expr> mv, Scalar<Expr> variableExpr, int degree = 1)
    {
        return mv.MapScalar(scalar => scalar.DifferentiateScalar(variableExpr.ScalarValue, degree));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<Expr> DifferentiateScalar(this RGaScalar<Expr> mv, IScalar<Expr> variableExpr, int degree = 1)
    {
        return mv.MapScalar(scalar => scalar.DifferentiateScalar(variableExpr.ScalarValue, degree));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<Expr> DifferentiateScalars(this RGaVector<Expr> vector, string variableName, int degree = 1)
    {
        return vector.MapScalars(
            s => s.DifferentiateScalar(variableName, degree)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<Expr> DifferentiateScalars(this RGaVector<Expr> vector, Expr variableExpr, int degree = 1)
    {
        return vector.MapScalars(
            s => s.DifferentiateScalar(variableExpr, degree)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<Expr> DifferentiateScalars(this RGaVector<Expr> vector, Scalar<Expr> variableExpr, int degree = 1)
    {
        return vector.MapScalars(
            s => s.DifferentiateScalar(variableExpr.ScalarValue, degree)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<Expr> DifferentiateScalars(this RGaVector<Expr> vector, IScalar<Expr> variableExpr, int degree = 1)
    {
        return vector.MapScalars(
            s => s.DifferentiateScalar(variableExpr.ScalarValue, degree)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<Expr> DifferentiateScalars(this RGaBivector<Expr> vector, string variableName, int degree = 1)
    {
        return vector.MapScalars(
            s => s.DifferentiateScalar(variableName, degree)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<Expr> DifferentiateScalars(this RGaBivector<Expr> vector, Expr variableExpr, int degree = 1)
    {
        return vector.MapScalars(
            s => s.DifferentiateScalar(variableExpr, degree)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<Expr> DifferentiateScalars(this RGaKVector<Expr> vector, string variableName, int degree = 1)
    {
        return vector.MapScalars(
            s => s.DifferentiateScalar(variableName, degree)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<Expr> DifferentiateScalars(this RGaKVector<Expr> vector, Expr variableExpr, int degree = 1)
    {
        return vector.MapScalars(
            s => s.DifferentiateScalar(variableExpr, degree)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<Expr> DifferentiateScalars(this RGaMultivector<Expr> vector, string variableName, int degree = 1)
    {
        return vector.MapScalars(
            s => s.DifferentiateScalar(variableName, degree)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<Expr> DifferentiateScalars(this RGaMultivector<Expr> vector, Expr variableExpr, int degree = 1)
    {
        return vector.MapScalars(
            s => s.DifferentiateScalar(variableExpr, degree)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<Expr> IntegrateScalars(this RGaMultivector<Expr> mv, string variableName)
    {
        var variableExpr = variableName.ToExpr();

        return mv.MapScalars(
            scalar => Mfs.Integrate[scalar, variableExpr].FullSimplify()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<Expr> ArcLength(this RGaMultivector<Expr> vector, string variableName, Expr tMin, Expr tMax)
    {
        return Mfs.ArcLength[
            vector.AsListExpr(),
            Mfs.List[variableName.ToExpr(), tMin, tMax]
        ].ScalarFromValue(vector.ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<Expr> HilbertTransformScalars(this RGaMultivector<Expr> mv, string timeVariableName, string freqVariableName)
    {
        var timeVariableExpr = timeVariableName.ToExpr();
        var freqVariableExpr = freqVariableName.ToExpr();

        return mv.MapScalars(
            scalar =>
                Mfs
                    .HilbertTransform[scalar, timeVariableExpr, timeVariableExpr]
                    .FullSimplify(Mfs.Greater[freqVariableExpr, Expr.INT_ZERO])
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<Expr> Vector(params Expr[] scalarArray)
    {
        return EuclideanProcessor.Vector(scalarArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<Expr> Vector(params string[] scalarTextArray)
    {
        return EuclideanProcessor.Vector(
            scalarTextArray.Select(t => t.ToExpr()).ToArray()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<Expr> CreateBasisVector(int index)
    {
        return EuclideanProcessor.VectorTerm(index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IRGaOutermorphism<Expr> CreateVectorsLinearMap(int basisVectorsCount, Func<int, LinVector<Expr>> basisVectorMapFunc)
    {
        return ScalarProcessor.CreateLinUnilinearMap(
            basisVectorsCount,
            basisVectorMapFunc
        ).ToOutermorphism(EuclideanProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IRGaOutermorphism<Expr> CreateVectorsLinearMap(int basisVectorsCount, Func<LinVector<Expr>, LinVector<Expr>> basisVectorMapFunc)
    {
        return ScalarProcessor.CreateLinUnilinearMap(
            basisVectorsCount,
            basisVectorMapFunc
        ).ToOutermorphism(EuclideanProcessor);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr ToVectorExpr(this RGaVector<Expr> vector, int vectorSize)
    {
        return vector.VectorToArray1D(vectorSize).ToVectorExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr ToVectorExpr(this RGaBivector<Expr> vector, int vectorSize)
    {
        return vector.BivectorToArray1D(vectorSize).ToVectorExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr ToRowVectorMatrixExpr(this RGaVector<Expr> vector, int vectorSize)
    {
        return vector.VectorToArray1D(vectorSize).ToRowVectorMatrixExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr ToRowVectorMatrixExpr(this RGaBivector<Expr> vector, int vectorSize)
    {
        return vector.BivectorToArray1D(vectorSize).ToRowVectorMatrixExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr ToColumnVectorMatrixExpr(this RGaVector<Expr> vector, int vectorSize)
    {
        return vector.VectorToArray1D(vectorSize).ToColumnVectorMatrixExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr ToColumnVectorMatrixExpr(this RGaBivector<Expr> vector, int vectorSize)
    {
        return vector.BivectorToArray1D(vectorSize).ToColumnVectorMatrixExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr BivectorToMatrix(this RGaBivector<Expr> bivector, int arraySize)
    {
        return bivector.BivectorToArray2D(arraySize).ToMatrixExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr ScalarPlusBivectorToMatrix(this RGaMultivector<Expr> multivector, int arraySize)
    {
        return multivector.ScalarPlusBivectorToArray2D(arraySize).ToMatrixExpr();
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Expr KVectorToRowVectorMatrix(this RGaMultivector<Expr> kVectorStorage, uint arraySize)
    //{
    //    return MatrixProcessor.CreateRowVectorMatrix(
    //        ScalarProcessor.KVectorToArrayVector(kVectorStorage, vSpaceDimensions)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Expr KVectorToColumnVectorMatrix(this RGaMultivector<Expr> kVectorStorage, uint vSpaceDimensions)
    //{
    //    return MatrixProcessor.CreateColumnVectorMatrix(
    //        ScalarProcessor.KVectorToArrayVector(kVectorStorage, vSpaceDimensions)
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr MultivectorToVectorExpr(this RGaMultivector<Expr> multivector, int vectorSize)
    {
        return multivector.MultivectorToArray1D(vectorSize).ToVectorExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr MultivectorToRowVectorMatrixExpr(this RGaMultivector<Expr> multivector, int vectorSize)
    {
        return multivector.MultivectorToArray1D(vectorSize).ToRowVectorMatrixExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr MultivectorToColumnVectorMatrix(this RGaMultivector<Expr> multivector, int vectorSize)
    {
        return multivector.MultivectorToArray1D(vectorSize).ToColumnVectorMatrixExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetText(this RGaMultivector<Expr> mv)
    {
        return TextComposer.GetMultivectorText(mv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetLaTeX(this RGaMultivector<Expr> mv)
    {
        return LaTeXComposer.GetMultivectorText(mv);
    }

    public static RGaVector<Float64Signal> GetSampledSignal(this RGaVector<Expr> vector, RGaProcessor<Float64Signal> processor, Expr t, double samplingRate, int sampleCount)
    {
        var composer = processor.CreateComposer();

        foreach (var (id, exprScalar) in vector.IdScalarPairs)
        {
            composer.SetTerm(
                id,
                exprScalar.GetSampledSignal(t, samplingRate, sampleCount)
            );
        }

        return composer.GetVector();
    }

    public static RGaBivector<Float64Signal> GetSampledSignal(this RGaBivector<Expr> vector, RGaProcessor<Float64Signal> processor, Expr t, double samplingRate, int sampleCount)
    {
        var composer = processor.CreateComposer();

        foreach (var (id, exprScalar) in vector.IdScalarPairs)
        {
            composer.SetTerm(
                id,
                exprScalar.GetSampledSignal(t, samplingRate, sampleCount)
            );
        }

        return composer.GetBivector();
    }

}