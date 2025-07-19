using System;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using Wolfram.NETLink;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Text;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Rotors;

namespace GeometricAlgebraFulcrumLib.Mathematica.Algebra;

public static class XGaMathematicaUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<Expr> SimplifyCollectScalar(this XGaScalar<Expr> vector, Expr t)
    {
        return vector.MapScalar(scalar =>
            Mfs.Collect[Mfs.Simplify[scalar], t].Evaluate()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<Expr> FullSimplifyScalar(this XGaScalar<Expr> mv)
    {
        return mv.MapScalar(scalar => scalar.FullSimplify());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<Expr> FullSimplifyScalars(this XGaScalar<Expr> mv, Expr assumptionsExpr)
    {
        return mv.MapScalar(scalar => scalar.FullSimplify(assumptionsExpr));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<Expr> FullSimplifyScalars(this XGaVector<Expr> mv, Expr assumptionsExpr)
    {
        return mv.MapScalars(scalar => scalar.FullSimplify(assumptionsExpr));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<Expr> FullSimplifyScalars(this XGaBivector<Expr> mv, Expr assumptionsExpr)
    {
        return mv.MapScalars(scalar => scalar.FullSimplify(assumptionsExpr));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<Expr> DifferentiateScalar(this XGaScalar<Expr> mv, Scalar<Expr> variableExpr, int degree = 1)
    {
        return mv.MapScalar(scalar => scalar.DifferentiateScalar(variableExpr.ScalarValue, degree));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<Expr> DifferentiateScalar(this XGaScalar<Expr> mv, IScalar<Expr> variableExpr, int degree = 1)
    {
        return mv.MapScalar(scalar => scalar.DifferentiateScalar(variableExpr.ScalarValue, degree));
    }



    public static ScalarProcessorOfWolframExpr ScalarProcessor
        => ScalarProcessorOfWolframExpr.Instance;

    //public static MatrixAlgebraMathematicaProcessor MatrixProcessor
    //    => MatrixAlgebraMathematicaProcessor.Instance;

    public static XGaProcessor<Expr> EuclideanProcessor { get; }
        = XGaProcessor<Expr>.CreateEuclidean(ScalarProcessor);

    public static LaTeXComposerOfWolframExpr LaTeXComposer
        => LaTeXComposerOfWolframExpr.DefaultComposer;

    public static TextComposerExpr TextComposer
        => TextComposerExpr.DefaultComposer;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<Expr> ToSymbolic(this XGaMultivector<double> mv)
    {
        return mv.MapScalars(
            EuclideanProcessor,
            number => number.ToExpr()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<double> ToNumeric(this XGaMultivector<Expr> mv, XGaProcessor<double> processor)
    {
        return mv.MapScalars(
            processor,
            number => number.ToNumber()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<Expr> SimplifyScalar(this XGaScalar<Expr> mv)
    {
        return mv.Processor.Scalar(
            mv.ScalarValue.Simplify()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<Expr> SimplifyScalar(this XGaScalar<Expr> mv, Expr assumeExpr)
    {
        return mv.Processor.Scalar(
            mv.ScalarValue.Simplify()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<Expr> SimplifyScalars(this XGaVector<Expr> mv)
    {
        return mv.MapScalars(
            scalar => scalar.Simplify()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<Expr> SimplifyScalars(this XGaVector<Expr> mv, Expr assumeExpr)
    {
        return mv.MapScalars(
            scalar => scalar.Simplify(assumeExpr)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<Expr> SimplifyScalars(this XGaBivector<Expr> mv)
    {
        return mv.MapScalars(
            scalar => scalar.Simplify()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<Expr> SimplifyScalars(this XGaBivector<Expr> mv, Expr assumeExpr)
    {
        return mv.MapScalars(
            scalar => scalar.Simplify(assumeExpr)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<Expr> SimplifyScalars(this XGaKVector<Expr> mv)
    {
        return mv.MapScalars(
            scalar => scalar.Simplify()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<Expr> SimplifyScalars(this XGaKVector<Expr> mv, Expr assumeExpr)
    {
        return mv.MapScalars(
            scalar => scalar.Simplify(assumeExpr)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<Expr> SimplifyScalars(this XGaMultivector<Expr> mv)
    {
        return mv.MapScalars(
            scalar => scalar.Simplify()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<Expr> SimplifyScalars(this XGaMultivector<Expr> mv, Expr assumeExpr)
    {
        return mv.MapScalars(
            scalar => scalar.Simplify(assumeExpr)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureRotor<Expr> SimplifyScalars(this XGaPureRotor<Expr> rotor)
    {
        return rotor
            .Multivector
            .MapScalars(scalar => scalar.Simplify())
            .ScalarBivectorPartsToEuclideanPureRotor();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureRotor<Expr> SimplifyScalars(this XGaPureRotor<Expr> rotor, Expr assumeExpr)
    {
        return rotor
            .Multivector
            .MapScalars(scalar => scalar.Simplify(assumeExpr))
            .ScalarBivectorPartsToEuclideanPureRotor();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<Expr> TrigExpandScalar(this XGaScalar<Expr> v)
    {
        return v.Processor.Scalar(
            v.ScalarValue.TrigExpand()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<Expr> TrigExpandScalars(this XGaVector<Expr> v)
    {
        return v.MapScalars(s => s.TrigExpand());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<Expr> TrigExpandScalars(this XGaBivector<Expr> v)
    {
        return v.MapScalars(s => s.TrigExpand());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<Expr> TrigExpandScalars(this XGaKVector<Expr> v)
    {
        return v.MapScalars(s => s.TrigExpand());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<Expr> TrigExpandScalars(this XGaMultivector<Expr> v)
    {
        return v.MapScalars(s => s.TrigExpand());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<Expr> TrigReduceScalar(this XGaScalar<Expr> v)
    {
        return v.Processor.Scalar(
            v.ScalarValue.TrigReduce()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<Expr> TrigReduceScalars(this XGaVector<Expr> v)
    {
        return v.MapScalars(s => s.TrigReduce());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<Expr> TrigReduceScalars(this XGaBivector<Expr> v)
    {
        return v.MapScalars(s => s.TrigReduce());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<Expr> TrigReduceScalars(this XGaKVector<Expr> v)
    {
        return v.MapScalars(s => s.TrigReduce());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<Expr> TrigReduceScalars(this XGaMultivector<Expr> v)
    {
        return v.MapScalars(s => s.TrigReduce());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<Expr> CollectScalar(this XGaScalar<Expr> mv, Expr symbolExpr)
    {
        return mv.MapScalar(s => s.Collect(symbolExpr));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<Expr> CollectScalars(this XGaVector<Expr> vector, Expr t)
    {
        return vector.MapScalars(scalar =>
            Mfs.Collect[scalar, t].Evaluate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<Expr> CollectScalars(this XGaBivector<Expr> vector, Expr t)
    {
        return vector.MapScalars(scalar =>
            Mfs.Collect[scalar, t].Evaluate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<Expr> CollectScalars(this XGaKVector<Expr> vector, Expr t)
    {
        return vector.MapScalars(scalar =>
            Mfs.Collect[scalar, t].Evaluate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<Expr> CollectScalars(this XGaMultivector<Expr> vector, Expr t)
    {
        return vector.MapScalars(scalar =>
            Mfs.Collect[scalar, t].Evaluate()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<Expr> SimplifyCollectScalars(this XGaScalar<Expr> vector, Expr t)
    {
        return vector.MapScalar(scalar =>
            Mfs.Collect[Mfs.Simplify[scalar], t].Evaluate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<Expr> SimplifyCollectScalars(this XGaVector<Expr> vector, Expr t)
    {
        return vector.MapScalars(scalar =>
            Mfs.Collect[Mfs.Simplify[scalar], t].Evaluate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<Expr> SimplifyCollectScalars(this XGaBivector<Expr> vector, Expr t)
    {
        return vector.MapScalars(scalar =>
            Mfs.Collect[Mfs.Simplify[scalar], t].Evaluate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<Expr> SimplifyCollectScalars(this XGaKVector<Expr> vector, Expr t)
    {
        return vector.MapScalars(scalar =>
            Mfs.Collect[Mfs.Simplify[scalar], t].Evaluate()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<Expr> SimplifyCollectScalars(this XGaMultivector<Expr> vector, Expr t)
    {
        return vector.MapScalars(scalar =>
            Mfs.Collect[Mfs.Simplify[scalar], t].Evaluate()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<Expr> FullSimplify(this XGaScalar<Expr> mv)
    {
        return mv.MapScalar(scalar => scalar.FullSimplify());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<Expr> FullSimplifyScalars(this XGaVector<Expr> mv)
    {
        return mv.MapScalars(scalar => scalar.FullSimplify());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<Expr> FullSimplifyScalars(this XGaBivector<Expr> mv)
    {
        return mv.MapScalars(scalar => scalar.FullSimplify());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<Expr> FullSimplifyScalars(this XGaKVector<Expr> mv)
    {
        return mv.MapScalars(scalar => scalar.FullSimplify());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<Expr> FullSimplifyScalars(this XGaKVector<Expr> mv, Expr assumptionsExpr)
    {
        return mv.MapScalars(scalar => scalar.FullSimplify(assumptionsExpr));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<Expr> FullSimplifyScalars(this XGaMultivector<Expr> mv)
    {
        return mv.MapScalars(scalar => scalar.FullSimplify());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<Expr> FullSimplifyScalars(this XGaMultivector<Expr> mv, Expr assumptionsExpr)
    {
        return mv.MapScalars(scalar => scalar.FullSimplify(assumptionsExpr));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<Expr> FullSimplifyScalars(this CGaBlade<Expr> mv)
    {
        return mv.GeometricSpace.Encode.Blade(
            mv.InternalKVector.FullSimplifyScalars()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<Expr> FullSimplifyScalars(this CGaBlade<Expr> mv, Expr assumptionsExpr)
    {
        return mv.GeometricSpace.Encode.Blade(
            mv.InternalKVector.FullSimplifyScalars(assumptionsExpr)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<Expr> FullSimplifyScalars(this PGaBlade<Expr> mv)
    {
        return mv.GeometricSpace.EncodeBlade(
            mv.InternalKVector.FullSimplifyScalars()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<Expr> FullSimplifyScalars(this PGaBlade<Expr> mv, Expr assumptionsExpr)
    {
        return mv.GeometricSpace.EncodeBlade(
            mv.InternalKVector.FullSimplifyScalars(assumptionsExpr)
        );
    }

    public static Expr AsListExpr(this XGaMultivector<Expr> vector)
    {
        var n = vector.VSpaceDimensions;
        var listExprArgs = new object[n];

        for (var i = 0; i < n; i++)
            listExprArgs[i] = vector.Scalar(i).ScalarValue;

        return Mfs.List[listExprArgs];
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<Expr> DifferentiateScalar(this XGaScalar<Expr> mv, string variableName, int degree = 1)
    {
        return mv.MapScalar(scalar => scalar.DifferentiateScalar(variableName, degree));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<Expr> DifferentiateScalar(this XGaScalar<Expr> mv, Expr variableExpr, int degree = 1)
    {
        return mv.MapScalar(scalar => scalar.DifferentiateScalar(variableExpr, degree));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<Expr> DifferentiateScalars(this XGaVector<Expr> vector, string variableName, int degree = 1)
    {
        return vector.MapScalars(
            s => s.DifferentiateScalar(variableName, degree)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<Expr> DifferentiateScalars(this XGaVector<Expr> vector, Expr variableExpr, int degree = 1)
    {
        return vector.MapScalars(
            s => s.DifferentiateScalar(variableExpr, degree)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<Expr> DifferentiateScalars(this XGaVector<Expr> vector, Scalar<Expr> variableExpr, int degree = 1)
    {
        return vector.MapScalars(
            s => s.DifferentiateScalar(variableExpr.ScalarValue, degree)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<Expr> DifferentiateScalars(this XGaVector<Expr> vector, IScalar<Expr> variableExpr, int degree = 1)
    {
        return vector.MapScalars(
            s => s.DifferentiateScalar(variableExpr.ScalarValue, degree)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<Expr> DifferentiateScalars(this XGaBivector<Expr> vector, string variableName, int degree = 1)
    {
        return vector.MapScalars(
            s => s.DifferentiateScalar(variableName, degree)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<Expr> DifferentiateScalars(this XGaBivector<Expr> vector, Expr variableExpr, int degree = 1)
    {
        return vector.MapScalars(
            s => s.DifferentiateScalar(variableExpr, degree)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<Expr> DifferentiateScalars(this XGaKVector<Expr> vector, string variableName, int degree = 1)
    {
        return vector.MapScalars(
            s => s.DifferentiateScalar(variableName, degree)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<Expr> DifferentiateScalars(this XGaKVector<Expr> vector, Expr variableExpr, int degree = 1)
    {
        return vector.MapScalars(
            s => s.DifferentiateScalar(variableExpr, degree)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<Expr> DifferentiateScalars(this XGaMultivector<Expr> vector, string variableName, int degree = 1)
    {
        return vector.MapScalars(
            s => s.DifferentiateScalar(variableName, degree)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<Expr> DifferentiateScalars(this XGaMultivector<Expr> vector, Expr variableExpr, int degree = 1)
    {
        return vector.MapScalars(
            s => s.DifferentiateScalar(variableExpr, degree)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<Expr> IntegrateScalars(this XGaMultivector<Expr> mv, string variableName)
    {
        var variableExpr = variableName.ToExpr();

        return mv.MapScalars(
            scalar => Mfs.Integrate[scalar, variableExpr].FullSimplify()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<Expr> ArcLength(this XGaMultivector<Expr> vector, string variableName, Expr tMin, Expr tMax)
    {
        return Mfs.ArcLength[
            vector.AsListExpr(),
            Mfs.List[variableName.ToExpr(), tMin, tMax]
        ].ScalarFromValue(vector.ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<Expr> HilbertTransformScalars(this XGaMultivector<Expr> mv, string timeVariableName, string freqVariableName)
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
    public static XGaMultivector<Expr> Vector(params Expr[] scalarArray)
    {
        return EuclideanProcessor.Vector(scalarArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<Expr> Vector(params string[] scalarTextArray)
    {
        return EuclideanProcessor.Vector(
            scalarTextArray.Select(t => t.ToExpr()).ToArray()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<Expr> CreateBasisVector(int index)
    {
        return EuclideanProcessor.VectorTerm(index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IXGaOutermorphism<Expr> CreateVectorsLinearMap(int basisVectorsCount, Func<int, LinVector<Expr>> basisVectorMapFunc)
    {
        return ScalarProcessor.CreateLinUnilinearMap(
            basisVectorsCount,
            basisVectorMapFunc
        ).ToOutermorphism(EuclideanProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IXGaOutermorphism<Expr> CreateVectorsLinearMap(int basisVectorsCount, Func<LinVector<Expr>, LinVector<Expr>> basisVectorMapFunc)
    {
        return ScalarProcessor.CreateLinUnilinearMap(
            basisVectorsCount,
            basisVectorMapFunc
        ).ToOutermorphism(EuclideanProcessor);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr ToVectorExpr(this XGaVector<Expr> vector, int vectorSize)
    {
        return vector.VectorToArray1D(vectorSize).ToVectorExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr ToVectorExpr(this XGaBivector<Expr> vector, int vectorSize)
    {
        return vector.BivectorToArray1D(vectorSize).ToVectorExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr ToRowVectorMatrixExpr(this XGaVector<Expr> vector, int vectorSize)
    {
        return vector.VectorToArray1D(vectorSize).ToRowVectorMatrixExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr ToRowVectorMatrixExpr(this XGaBivector<Expr> vector, int vectorSize)
    {
        return vector.BivectorToArray1D(vectorSize).ToRowVectorMatrixExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr ToColumnVectorMatrixExpr(this XGaVector<Expr> vector, int vectorSize)
    {
        return vector.VectorToArray1D(vectorSize).ToColumnVectorMatrixExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr ToColumnVectorMatrixExpr(this XGaBivector<Expr> vector, int vectorSize)
    {
        return vector.BivectorToArray1D(vectorSize).ToColumnVectorMatrixExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr BivectorToMatrix(this XGaBivector<Expr> bivector, int arraySize)
    {
        return bivector.BivectorToArray2D(arraySize).ToMatrixExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr ScalarPlusBivectorToMatrix(this XGaMultivector<Expr> multivector, int arraySize)
    {
        return multivector.ScalarPlusBivectorToArray2D(arraySize).ToMatrixExpr();
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Expr KVectorToRowVectorMatrix(this XGaMultivector<Expr> kVectorStorage, uint arraySize)
    //{
    //    return MatrixProcessor.CreateRowVectorMatrix(
    //        ScalarProcessor.KVectorToArrayVector(kVectorStorage, vSpaceDimensions)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Expr KVectorToColumnVectorMatrix(this XGaMultivector<Expr> kVectorStorage, uint vSpaceDimensions)
    //{
    //    return MatrixProcessor.CreateColumnVectorMatrix(
    //        ScalarProcessor.KVectorToArrayVector(kVectorStorage, vSpaceDimensions)
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr MultivectorToVectorExpr(this XGaMultivector<Expr> multivector, int vectorSize)
    {
        return multivector.MultivectorToArray1D(vectorSize).ToVectorExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr MultivectorToRowVectorMatrixExpr(this XGaMultivector<Expr> multivector, int vectorSize)
    {
        return multivector.MultivectorToArray1D(vectorSize).ToRowVectorMatrixExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Expr MultivectorToColumnVectorMatrix(this XGaMultivector<Expr> multivector, int vectorSize)
    {
        return multivector.MultivectorToArray1D(vectorSize).ToColumnVectorMatrixExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetText(this XGaMultivector<Expr> mv)
    {
        return TextComposer.GetMultivectorText(mv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetLaTeX(this XGaMultivector<Expr> mv)
    {
        return LaTeXComposer.GetMultivectorText(mv);
    }

    public static XGaVector<Float64SampledTimeSignal> GetSampledSignal(this XGaVector<Expr> vector, XGaProcessor<Float64SampledTimeSignal> processor, Expr t, double samplingRate, int sampleCount)
    {
        var composer = processor.CreateVectorComposer();

        foreach (var (id, exprScalar) in vector.IdScalarPairs)
        {
            composer.SetTerm(
                id,
                exprScalar.GetSampledSignal(t, samplingRate, sampleCount)
            );
        }

        return composer.GetVector();
    }

    public static XGaBivector<Float64SampledTimeSignal> GetSampledSignal(this XGaBivector<Expr> vector, XGaProcessor<Float64SampledTimeSignal> processor, Expr t, double samplingRate, int sampleCount)
    {
        var composer = processor.CreateBivectorComposer();

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