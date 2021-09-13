using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Composite;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.Expression;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Mathematica.Text;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica
{
    public static class MathematicaUtils
    {
        public static ScalarAlgebraMathematicaProcessor ScalarProcessor
            => ScalarAlgebraMathematicaProcessor.DefaultProcessor;
        
        public static LinearAlgebraMathematicaProcessor MatrixProcessor
            => LinearAlgebraMathematicaProcessor.DefaultProcessor;

        public static IGeometricAlgebraEuclideanProcessor<Expr> EuclideanProcessor { get; }
            = ScalarProcessor.CreateGeometricAlgebraEuclideanProcessor(63);

        public static MathematicaLaTeXComposer LaTeXComposer
            => MathematicaLaTeXComposer.DefaultComposer;

        public static MathematicaTextComposer TextComposer
            => MathematicaTextComposer.DefaultComposer;


        public static MathematicaInterface Cas 
            => MathematicaInterface.DefaultCas;

        public static MathematicaEvaluator Evaluator 
            => Cas.Evaluator;

        public static MathematicaConstants Constants 
            => Cas.Constants;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrZero(this MathematicaScalar scalar)
        {
            return ReferenceEquals(scalar, null) || scalar.IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEqualZero(this MathematicaScalar scalar)
        {
            return ReferenceEquals(scalar, null) || scalar.IsEqualZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MathematicaScalar ToMathematicaScalar(this Expr e)
        {
            return ReferenceEquals(e, null)
                ? Constants.Zero
                : MathematicaScalar.Create(Cas, e);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MathematicaScalar ToMathematicaScalar(this double e)
        {
            return MathematicaScalar.Create(Cas, e.ToExpr());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MathematicaScalar ToMathematicaScalar(this int e)
        {
            return MathematicaScalar.Create(Cas, e.ToExpr());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<Expr> ToSymbolic(this IMultivectorStorage<double> storage)
        {
            return storage.MapScalars(number => number.ToExpr());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<double> ToNumeric(this IMultivectorStorage<Expr> storage)
        {
            return storage.MapScalars(number => number.ToNumber());
        }


        public static double ToNumber(this Expr value)
        {
            if (ReferenceEquals(value, null))
                return 0.0d;

            if (!value.NumberQ())
                return 0.0d;

            var exprText = value.ToString();
            if (exprText == "0")
                return 0.0d;

            var textValue =
                Cas.Connection.EvaluateToOutputForm(Mfs.CForm[Mfs.N[value]]);

            return double.TryParse(textValue, out var doubleValue)
                ? doubleValue : 0.0d;
        }

        public static double ToNumber(this MathematicaScalar value)
        {
            if (ReferenceEquals(value, null))
                return 0.0d;

            if (!value.Expression.NumberQ())
                return 0.0d;

            var exprText = value.ExpressionText;
            if (exprText == "0")
                return 0.0d;

            var textValue =
                value.CasConnection.EvaluateToOutputForm(Mfs.CForm[Mfs.N[value.Expression]]);

            return double.TryParse(textValue, out var doubleValue)
                ? doubleValue : 0.0d;
        }

        public static bool IsNullOrNearNumericZero(this Expr value, double epsilon)
        {
            if (ReferenceEquals(value, null))
                return true;

            if (!value.NumberQ())
                return false;

            var exprText = value.ToString();
            if (exprText == "0")
                return true;

            var textValue =
                Cas.Connection.EvaluateToOutputForm(Mfs.CForm[Mfs.N[value]]);

            if (!double.TryParse(textValue, out var doubleValue))
                return false;

            return Math.Abs(doubleValue) <= epsilon;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr[] SimplifyScalars(this Expr[] array)
        {
            return array.MapScalars(s => s.Simplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr[] SimplifyScalars(this Expr[] array, Expr assumptionsExpr)
        {
            return array.MapScalars(s => s.Simplify(assumptionsExpr));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr[,] SimplifyScalars(this Expr[,] array)
        {
            return array.MapScalars(s => s.Simplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr[,] SimplifyScalars(this Expr[,] array, Expr assumptionsExpr)
        {
            return array.MapScalars(s => s.Simplify(assumptionsExpr));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<Expr> SimplifyScalars(this ILinVectorStorage<Expr> storage)
        {
            return storage.MapScalars(scalar => scalar.Simplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<Expr> SimplifyScalars(this ILinVectorStorage<Expr> storage, Expr assumptionsExpr)
        {
            return storage.MapScalars(scalar => scalar.Simplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<Expr> SimplifyScalars(this ILinMatrixStorage<Expr> storage)
        {
            return storage.MapScalars(scalar => scalar.Simplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<Expr> SimplifyScalars(this ILinMatrixStorage<Expr> storage, Expr assumptionsExpr)
        {
            return storage.MapScalars(scalar => scalar.Simplify(assumptionsExpr));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<Expr> SimplifyScalars(this IMultivectorStorage<Expr> storage)
        {
            return storage.MapScalars(scalar => scalar.Simplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<Expr> SimplifyScalars(this IMultivectorStorage<Expr> storage, Expr assumptionsExpr)
        {
            return storage.MapScalars(scalar => scalar.Simplify(assumptionsExpr));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<Expr> SimplifyScalars(this Multivector<Expr> storage, Expr assumptionsExpr)
        {
            return storage.MapScalars(scalar => scalar.Simplify(assumptionsExpr));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr[] FullSimplifyScalars(this Expr[] array)
        {
            return array.MapScalars(s => s.FullSimplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr[] FullSimplifyScalars(this Expr[] array, Expr assumptionsExpr)
        {
            return array.MapScalars(s => s.FullSimplify(assumptionsExpr));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr[,] FullSimplifyScalars(this Expr[,] array)
        {
            return array.MapScalars(s => s.FullSimplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr[,] FullSimplifyScalars(this Expr[,] array, Expr assumptionsExpr)
        {
            return array.MapScalars(s => s.FullSimplify(assumptionsExpr));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<Expr> FullSimplifyScalars(this ILinVectorStorage<Expr> storage)
        {
            return storage.MapScalars(scalar => scalar.FullSimplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<Expr> FullSimplifyScalars(this ILinVectorStorage<Expr> storage, Expr assumptionsExpr)
        {
            return storage.MapScalars(scalar => scalar.FullSimplify(assumptionsExpr));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<Expr> FullSimplifyScalars(this ILinMatrixStorage<Expr> storage)
        {
            return storage.MapScalars(scalar => scalar.FullSimplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<Expr> FullSimplifyScalars(this ILinMatrixStorage<Expr> storage, Expr assumptionsExpr)
        {
            return storage.MapScalars(scalar => scalar.FullSimplify(assumptionsExpr));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<Expr> FullSimplifyScalars(this IMultivectorStorage<Expr> storage)
        {
            return storage.MapScalars(scalar => scalar.FullSimplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<Expr> FullSimplifyScalars(this IMultivectorStorage<Expr> storage, Expr assumptionsExpr)
        {
            return storage.MapScalars(scalar => scalar.FullSimplify(assumptionsExpr));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr DifferentiateScalar(this Expr scalar, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return Mfs
                .D[scalar, variableExpr]
                .FullSimplify();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<Expr> DifferentiateScalars(this ILinVectorStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.MapScalars(
                scalar => Mfs.D[scalar, variableExpr].FullSimplify()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorGradedStorage<Expr> DifferentiateScalars(this ILinVectorGradedStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.MapScalars(
                scalar => Mfs.D[scalar, variableExpr].FullSimplify()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<Expr> DifferentiateScalars(this ILinMatrixStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.MapScalars(
                scalar => Mfs.D[scalar, variableExpr].FullSimplify()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixGradedStorage<Expr> DifferentiateScalars(this ILinMatrixGradedStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.MapScalars(
                scalar => Mfs.D[scalar, variableExpr].FullSimplify()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<Expr> DifferentiateScalars(this IMultivectorStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.MapScalars(
                scalar => Mfs.D[scalar, variableExpr].FullSimplify()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<Expr> DifferentiateScalars(this VectorStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.GetVectorPart(
                scalar => Mfs.D[scalar, variableExpr].FullSimplify()
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr IntegrateScalar(this Expr scalar, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return Mfs
                .Integrate[scalar, variableExpr]
                .FullSimplify();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<Expr> IntegrateScalars(this ILinVectorStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.MapScalars(
                scalar => Mfs.Integrate[scalar, variableExpr].FullSimplify()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorGradedStorage<Expr> IntegrateScalars(this ILinVectorGradedStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.MapScalars(
                scalar => Mfs.Integrate[scalar, variableExpr].FullSimplify()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<Expr> IntegrateScalars(this ILinMatrixStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.MapScalars(
                scalar => Mfs.Integrate[scalar, variableExpr].FullSimplify()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixGradedStorage<Expr> IntegrateScalars(this ILinMatrixGradedStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.MapScalars(
                scalar => Mfs.Integrate[scalar, variableExpr].FullSimplify()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<Expr> IntegrateScalars(this IMultivectorStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.MapScalars(
                scalar => Mfs.Integrate[scalar, variableExpr].FullSimplify()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<Expr> IntegrateScalars(this VectorStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.GetVectorPart(
                scalar => Mfs.Integrate[scalar, variableExpr].FullSimplify()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr HilbertTransformScalar(this Expr scalar, string timeVariableName, string freqVariableName)
        {
            var timeVariableExpr = timeVariableName.ToExpr();
            var freqVariableExpr = freqVariableName.ToExpr();

            return Mfs
                .HilbertTransform[scalar, timeVariableExpr, timeVariableExpr]
                .FullSimplify(Mfs.Greater[freqVariableExpr, Expr.INT_ZERO]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<Expr> HilbertTransformScalars(this IMultivectorStorage<Expr> storage, string timeVariableName, string freqVariableName)
        {
            var timeVariableExpr = timeVariableName.ToExpr();
            var freqVariableExpr = freqVariableName.ToExpr();

            return storage.MapScalars(
                scalar => 
                    Mfs
                        .HilbertTransform[scalar, timeVariableExpr, timeVariableExpr]
                        .FullSimplify(Mfs.Greater[freqVariableExpr, Expr.INT_ZERO])
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<Expr> HilbertTransformScalars(this VectorStorage<Expr> storage, string timeVariableName, string freqVariableName)
        {
            var timeVariableExpr = timeVariableName.ToExpr();
            var freqVariableExpr = freqVariableName.ToExpr();

            return storage.GetVectorPart(
                scalar => 
                    Mfs
                        .HilbertTransform[scalar, timeVariableExpr, timeVariableExpr]
                        .FullSimplify(Mfs.Greater[freqVariableExpr, Expr.INT_ZERO])
            );
        }

        //public static Expr GetScalarExpr(this GeoRandomGenerator randomGenerator)
        //{
        //    return randomGenerator.GetScalar().ToExpr();
        //}

        //public static Expr GetScalarExpr(this GeoRandomGenerator randomGenerator, double maxLimit)
        //{
        //    return randomGenerator.GetScalar(maxLimit).ToExpr();
        //}

        //public static Expr GetScalarExpr(this GeoRandomGenerator randomGenerator, double minLimit, double maxLimit)
        //{
        //    return randomGenerator.GetScalar(minLimit, maxLimit).ToExpr();
        //}


        //public static Expr GetIntegerExpr(this GeoRandomGenerator randomGenerator)
        //{
        //    return randomGenerator.GetInteger().ToExpr();
        //}

        //public static Expr GetIntegerExpr(this GeoRandomGenerator randomGenerator, int maxLimit)
        //{
        //    return randomGenerator.GetInteger(maxLimit).ToExpr();
        //}

        //public static Expr GetIntegerExpr(this GeoRandomGenerator randomGenerator, int minLimit, int maxLimit)
        //{
        //    return randomGenerator.GetInteger(minLimit, maxLimit).ToExpr();
        //}


        //public static MathematicaScalar GetSymbolicInteger(this GeoRandomGenerator randomGenerator)
        //{
        //    return MathematicaScalar.Create(Cas, randomGenerator.GetInteger());
        //}

        //public static MathematicaScalar GetSymbolicInteger(this GeoRandomGenerator randomGenerator, int maxLimit)
        //{
        //    return MathematicaScalar.Create(Cas, randomGenerator.GetInteger(maxLimit));
        //}

        //public static MathematicaScalar GetSymbolicInteger(this GeoRandomGenerator randomGenerator, int minLimit, int maxLimit)
        //{
        //    return MathematicaScalar.Create(Cas, randomGenerator.GetInteger(minLimit, maxLimit));
        //}


        //public static MathematicaScalar GetSymbolicScalar(this GeoRandomGenerator randomGenerator)
        //{
        //    return MathematicaScalar.Create(Cas, randomGenerator.GetScalar());
        //}

        //public static MathematicaScalar GetSymbolicScalar(this GeoRandomGenerator randomGenerator, double maxLimit)
        //{
        //    return MathematicaScalar.Create(Cas, randomGenerator.GetScalar(maxLimit));
        //}

        //public static MathematicaScalar GetSymbolicScalar(this GeoRandomGenerator randomGenerator, double minLimit, double maxLimit)
        //{
        //    return MathematicaScalar.Create(Cas, randomGenerator.GetScalar(minLimit, maxLimit));
        //}


        //public static GeoSymMultivector GetSymMultivectorFull(this GeoRandomGenerator randomGenerator, int vSpaceDim)
        //{
        //    var gaSpaceDim = 1UL << vSpaceDim;
        //    var mv = GeoSymMultivector.CreateZero(vSpaceDim);

        //    for (var basisBladeId = 0UL; basisBladeId < gaSpaceDim; basisBladeId++)
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymMultivectorFull(this GeoRandomGenerator randomGenerator, int vSpaceDim, double maxValue)
        //{
        //    var gaSpaceDim = 1UL << vSpaceDim;
        //    var mv = GeoSymMultivector.CreateZero(vSpaceDim);

        //    for (var basisBladeId = 0UL; basisBladeId < gaSpaceDim; basisBladeId++)
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(maxValue));

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymMultivectorFull(this GeoRandomGenerator randomGenerator, int vSpaceDim, double minValue, double maxValue)
        //{
        //    var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();

        //    var mv = GeoSymMultivector.CreateZero(vSpaceDim);

        //    for (var basisBladeId = 0UL; basisBladeId < gaSpaceDim; basisBladeId++)
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(minValue, maxValue));

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymMultivectorByTerms(this GeoRandomGenerator randomGenerator, int vSpaceDim, params ulong[] basisBladeIDs)
        //{
        //    var mv = GeoSymMultivector.CreateZero(vSpaceDim);

        //    foreach (var basisBladeId in basisBladeIDs)
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymMultivectorByTerms(this GeoRandomGenerator randomGenerator, int vSpaceDim, IEnumerable<ulong> basisBladeIDs)
        //{
        //    var mv = GeoSymMultivector.CreateZero(vSpaceDim);

        //    foreach (var basisBladeId in basisBladeIDs)
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymMultivectorByGrades(this GeoRandomGenerator randomGenerator, int vSpaceDim, params int[] grades)
        //{
        //    var mv = GeoSymMultivector.CreateZero(vSpaceDim);

        //    var basisBladeIDs =
        //        GeoFrameUtils.BasisBladeIDsOfGrades(
        //            mv.VSpaceDimension,
        //            grades
        //        );

        //    foreach (var basisBladeId in basisBladeIDs)
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymMultivector(this GeoRandomGenerator randomGenerator, int vSpaceDim)
        //{
        //    var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();

        //    //Randomly select the number of terms in the multivector
        //    var termsCount = randomGenerator.GetInteger((int)gaSpaceDim - 1);

        //    //Randomly select the terms basis blades in the multivectors
        //    var basisBladeIDs = randomGenerator
        //        .GetRangePermutation((int)gaSpaceDim - 1)
        //        .Take(termsCount)
        //        .Select(id => (ulong)id);

        //    //Randomly generate the multivector's coefficients
        //    return randomGenerator.GetSymMultivectorByTerms(vSpaceDim, basisBladeIDs);
        //}

        //public static GeoSymMultivector GetSymMultivector(this GeoRandomGenerator randomGenerator, int vSpaceDim, string baseCoefName)
        //{
        //    var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();

        //    //Randomly select the number of terms in the multivector
        //    var termsCount = randomGenerator.GetInteger((int)gaSpaceDim - 1);

        //    //Randomly select the terms basis blades in the multivectors
        //    var basisBladeIDs = randomGenerator
        //        .GetRangePermutation((int)gaSpaceDim - 1)
        //        .Take(termsCount)
        //        .Select(id => (ulong)id);

        //    //Generate the multivector's symbolic coefficients
        //    return GeoSymMultivector.CreateSymbolic(vSpaceDim, baseCoefName, basisBladeIDs);
        //}


        //public static GeoSymMultivector GetSymTerm(this GeoRandomGenerator randomGenerator, int vSpaceDim, string baseCoefName)
        //{
        //    var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();

        //    //Randomly select the number of terms in the multivector
        //    var basisBladeId = (ulong)randomGenerator.GetInteger((int)gaSpaceDim - 1);

        //    //Generate the multivector's symbolic coefficients
        //    return GeoSymMultivector.CreateSymbolicTerm(vSpaceDim, baseCoefName, basisBladeId);
        //}


        //public static GeoSymMultivector GetSymVector(this GeoRandomGenerator randomGenerator, int gaSpaceDim)
        //{
        //    var mv = GeoSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GeoFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymVector(this GeoRandomGenerator randomGenerator, int gaSpaceDim, double maxValue)
        //{
        //    var mv = GeoSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GeoFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(maxValue));

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymVector(this GeoRandomGenerator randomGenerator, int gaSpaceDim, double minValue, double maxValue)
        //{
        //    var mv = GeoSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GeoFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(minValue, maxValue));

        //    return mv;
        //}


        //public static GeoSymMultivector GetSymIntegerVector(this GeoRandomGenerator randomGenerator, int gaSpaceDim)
        //{
        //    var mv = GeoSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GeoFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetIntegerExpr());

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymIntegerVector(this GeoRandomGenerator randomGenerator, int gaSpaceDim, int maxLimit)
        //{
        //    var mv = GeoSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GeoFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetIntegerExpr(maxLimit));

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymIntegerVector(this GeoRandomGenerator randomGenerator, int gaSpaceDim, int minLimit, int maxLimit)
        //{
        //    var mv = GeoSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GeoFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetIntegerExpr(minLimit, maxLimit));

        //    return mv;
        //}


        //public static IEnumerable<GeoSymMultivector> GetSymIntegerLidVectors(this GeoRandomGenerator randomGenerator, int gaSpaceDim, int count)
        //{
        //    var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

        //    if (count < 1 || count > vSpaceDim)
        //        yield break;

        //    var mv = GeoSymMultivector.CreateScalar(gaSpaceDim, Expr.INT_ONE);
        //    while (count > 0)
        //    {
        //        var v = randomGenerator.GetSymIntegerVector(gaSpaceDim);
        //        var mv1 = mv.Op(v);

        //        if (mv1.IsZero()) continue;

        //        mv = mv1;
        //        count--;

        //        yield return v;
        //    }
        //}

        //public static IEnumerable<GeoSymMultivector> GetSymIntegerLidVectors(this GeoRandomGenerator randomGenerator, int gaSpaceDim, int count, int maxValue)
        //{
        //    var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

        //    if (count < 1 || count > vSpaceDim)
        //        yield break;

        //    var mv = GeoSymMultivector.CreateScalar(gaSpaceDim, Expr.INT_ONE);
        //    while (count > 0)
        //    {
        //        var v = randomGenerator.GetSymIntegerVector(gaSpaceDim, maxValue);
        //        var mv1 = mv.Op(v);

        //        if (mv1.IsZero()) continue;

        //        mv = mv1;
        //        count--;

        //        yield return v;
        //    }
        //}

        //public static IEnumerable<GeoSymMultivector> GetSymIntegerLidVectors(this GeoRandomGenerator randomGenerator, int gaSpaceDim, int count, int minValue, int maxValue)
        //{
        //    var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

        //    if (count < 1 || count > vSpaceDim)
        //        yield break;

        //    var mv = GeoSymMultivector.CreateScalar(gaSpaceDim, Expr.INT_ONE);
        //    while (count > 0)
        //    {
        //        var v = randomGenerator.GetSymIntegerVector(gaSpaceDim, minValue, maxValue);
        //        var mv1 = mv.Op(v);

        //        if (mv1.IsZero()) continue;

        //        mv = mv1;
        //        count--;

        //        yield return v;
        //    }
        //}


        //public static GeoSymMultivector GetSymKVector(this GeoRandomGenerator randomGenerator, int gaSpaceDim, int grade)
        //{
        //    var mv = GeoSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GeoFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymKVector(this GeoRandomGenerator randomGenerator, int gaSpaceDim, int grade, double maxValue)
        //{
        //    var mv = GeoSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GeoFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(maxValue));

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymKVector(this GeoRandomGenerator randomGenerator, int gaSpaceDim, int grade, double minValue, double maxValue)
        //{
        //    var mv = GeoSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GeoFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(minValue, maxValue));

        //    return mv;
        //}


        //public static GeoSymMultivector GetSymIntegerKVector(this GeoRandomGenerator randomGenerator, int gaSpaceDim, int grade)
        //{
        //    var mv = GeoSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GeoFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetIntegerExpr());

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymIntegerKVector(this GeoRandomGenerator randomGenerator, int gaSpaceDim, int grade, int maxValue)
        //{
        //    var mv = GeoSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GeoFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetIntegerExpr(maxValue));

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymIntegerKVector(this GeoRandomGenerator randomGenerator, int gaSpaceDim, int grade, int minValue, int maxValue)
        //{
        //    var mv = GeoSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GeoFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetIntegerExpr(minValue, maxValue));

        //    return mv;
        //}


        //public static GeoSymMultivector GetSymTerm(this GeoRandomGenerator randomGenerator, int vSpaceDim, ulong basisBladeId)
        //{
        //    var mv = GeoSymMultivector.CreateZero(vSpaceDim);

        //    mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymTerm(this GeoRandomGenerator randomGenerator, int vSpaceDim, ulong basisBladeId, double maxValue)
        //{
        //    var mv = GeoSymMultivector.CreateZero(vSpaceDim);

        //    mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(maxValue));

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymTerm(this GeoRandomGenerator randomGenerator, int vSpaceDim, ulong basisBladeId, double minValue, double maxValue)
        //{
        //    var mv = GeoSymMultivector.CreateZero(vSpaceDim);

        //    mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(minValue, maxValue));

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymTerm(this GeoRandomGenerator randomGenerator, int vSpaceDim, int grade, ulong index)
        //{
        //    var mv = GeoSymMultivector.CreateZero(vSpaceDim);

        //    var basisBladeId = GeoFrameUtils.BasisBladeId(grade, index);

        //    mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymTerm(this GeoRandomGenerator randomGenerator, int vSpaceDim, int grade, ulong index, double maxValue)
        //{
        //    var mv = GeoSymMultivector.CreateZero(vSpaceDim);

        //    var basisBladeId = GeoFrameUtils.BasisBladeId(grade, index);

        //    mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(maxValue));

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymTerm(this GeoRandomGenerator randomGenerator, int gaSpaceDim, int grade, ulong index, double minValue, double maxValue)
        //{
        //    var mv = GeoSymMultivector.CreateZero(gaSpaceDim);

        //    var basisBladeId = GeoFrameUtils.BasisBladeId(grade, index);

        //    mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(minValue, maxValue));

        //    return mv;
        //}


        //public static GeoSymMultivector GetSymBlade(this GeoRandomGenerator randomGenerator, int gaSpaceDim, int grade)
        //{
        //    var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

        //    if (grade < 0 || grade > vSpaceDim)
        //        return GeoSymMultivector.CreateZero(gaSpaceDim);

        //    if (grade <= 1 || grade >= vSpaceDim - 1)
        //        return randomGenerator.GetSymKVector(gaSpaceDim, grade);

        //    var mv = randomGenerator.GetSymVector(gaSpaceDim);
        //    grade--;

        //    while (grade > 0)
        //    {
        //        var v = randomGenerator.GetSymVector(gaSpaceDim);
        //        var mv1 = mv.Op(v);

        //        if (mv1.IsZero()) continue;

        //        mv = mv1;
        //        grade--;
        //    }

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymBlade(this GeoRandomGenerator randomGenerator, int gaSpaceDim, int grade, double maxValue)
        //{
        //    var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

        //    if (grade < 0 || grade > vSpaceDim)
        //        return GeoSymMultivector.CreateZero(gaSpaceDim);

        //    if (grade <= 1 || grade >= vSpaceDim - 1)
        //        return randomGenerator.GetSymKVector(gaSpaceDim, grade, maxValue);

        //    var mv = randomGenerator.GetSymVector(gaSpaceDim, maxValue);
        //    grade--;

        //    while (grade > 0)
        //    {
        //        var v = randomGenerator.GetSymVector(gaSpaceDim, maxValue);
        //        var mv1 = mv.Op(v);

        //        if (mv1.IsZero()) continue;

        //        mv = mv1;
        //        grade--;
        //    }

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymBlade(this GeoRandomGenerator randomGenerator, int gaSpaceDim, int grade, double minValue, double maxValue)
        //{
        //    var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

        //    if (grade < 0 || grade > vSpaceDim)
        //        return GeoSymMultivector.CreateZero(gaSpaceDim);

        //    if (grade <= 1 || grade >= vSpaceDim - 1)
        //        return randomGenerator.GetSymKVector(gaSpaceDim, grade, minValue, maxValue);

        //    var mv = randomGenerator.GetSymVector(gaSpaceDim, minValue, maxValue);
        //    grade--;

        //    while (grade > 0)
        //    {
        //        var v = randomGenerator.GetSymVector(gaSpaceDim, minValue, maxValue);
        //        var mv1 = mv.Op(v);

        //        if (mv1.IsZero()) continue;

        //        mv = mv1;
        //        grade--;
        //    }

        //    return mv;
        //}


        //public static GeoSymMultivector GetSymIntegerBlade(this GeoRandomGenerator randomGenerator, int gaSpaceDim, int grade)
        //{
        //    var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

        //    if (grade < 0 || grade > vSpaceDim)
        //        return GeoSymMultivector.CreateZero(gaSpaceDim);

        //    if (grade <= 1 || grade >= vSpaceDim - 1)
        //        return randomGenerator.GetSymIntegerKVector(gaSpaceDim, grade);

        //    var mv = randomGenerator.GetSymIntegerVector(gaSpaceDim);
        //    grade--;

        //    while (grade > 0)
        //    {
        //        var v = randomGenerator.GetSymIntegerVector(gaSpaceDim);
        //        var mv1 = mv.Op(v);

        //        if (mv1.IsZero()) continue;

        //        mv = mv1;
        //        grade--;
        //    }

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymIntegerBlade(this GeoRandomGenerator randomGenerator, int gaSpaceDim, int grade, int maxValue)
        //{
        //    var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

        //    if (grade < 0 || grade > vSpaceDim)
        //        return GeoSymMultivector.CreateZero(gaSpaceDim);

        //    if (grade <= 1 || grade >= vSpaceDim - 1)
        //        return randomGenerator.GetSymIntegerKVector(gaSpaceDim, grade, maxValue);

        //    var mv = randomGenerator.GetSymIntegerVector(gaSpaceDim, maxValue);
        //    grade--;

        //    while (grade > 0)
        //    {
        //        var v = randomGenerator.GetSymIntegerVector(gaSpaceDim, maxValue);
        //        var mv1 = mv.Op(v);

        //        if (mv1.IsZero()) continue;

        //        mv = mv1;
        //        grade--;
        //    }

        //    return mv;
        //}

        //public static GeoSymMultivector GetSymIntegerBlade(this GeoRandomGenerator randomGenerator, int gaSpaceDim, int grade, int minValue, int maxValue)
        //{
        //    var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

        //    if (grade < 0 || grade > vSpaceDim)
        //        return GeoSymMultivector.CreateZero(gaSpaceDim);

        //    if (grade <= 1 || grade >= vSpaceDim - 1)
        //        return randomGenerator.GetSymIntegerKVector(gaSpaceDim, grade, minValue, maxValue);

        //    var mv = randomGenerator.GetSymIntegerVector(gaSpaceDim, minValue, maxValue);
        //    grade--;

        //    while (grade > 0)
        //    {
        //        var v = randomGenerator.GetSymIntegerVector(gaSpaceDim, minValue, maxValue);
        //        var mv1 = mv.Op(v);

        //        if (mv1.IsZero()) continue;

        //        mv = mv1;
        //        grade--;
        //    }

        //    return mv;
        //}


        //public static GeoSymMultivector GetSymNonNullVector(this GeoRandomGenerator randomGenerator, GeoSymFrame frame)
        //{
        //    GeoSymMultivector mv;

        //    do
        //        mv = randomGenerator.GetSymVector(frame.VSpaceDimension);
        //    while (!frame.Norm2(mv).IsZero());

        //    return mv;
        //}


        //public static GeoSymMultivector GetSymVersor(this GeoRandomGenerator randomGenerator, GeoSymFrame frame, int vectorsCount)
        //{
        //    var mv = randomGenerator.GetSymNonNullVector(frame);
        //    vectorsCount--;

        //    while (vectorsCount > 0)
        //    {
        //        mv = frame.Gp[mv, randomGenerator.GetSymNonNullVector(frame)];
        //        vectorsCount--;
        //    }

        //    return mv;
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<Expr> CreateVector(params Expr[] scalarArray)
        {
            return ScalarProcessor.CreateVectorStorage(scalarArray
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<Expr> CreateVector(params string[] scalarTextArray)
        {
            return ScalarProcessor.CreateVectorStorage(scalarTextArray.Select(t => t.ToExpr()).ToArray()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<Expr> CreateBasisVector(int index)
        {
            return ScalarProcessor.CreateVectorBasisStorage(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOutermorphism<Expr> CreateVectorsLinearMap(int basisVectorsCount, Func<VectorStorage<Expr>, VectorStorage<Expr>> basisVectorMapFunc)
        {
            return EuclideanProcessor.CreateComputedOutermorphism(
                basisVectorsCount,
                basisVectorMapFunc
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr VectorToRowVectorMatrix(this VectorStorage<Expr> vectorStorage, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                ScalarProcessor.VectorToArrayVector(vectorStorage, vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr VectorToColumnVectorMatrix(this VectorStorage<Expr> vectorStorage, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                ScalarProcessor.VectorToArrayVector(vectorStorage, vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr BivectorToRowVectorMatrix(this BivectorStorage<Expr> bivectorStorage, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                ScalarProcessor.BivectorToArrayVector(bivectorStorage, vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr BivectorToColumnVectorMatrix(this BivectorStorage<Expr> bivectorStorage, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                ScalarProcessor.BivectorToArrayVector(bivectorStorage, vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr BivectorToMatrix(this BivectorStorage<Expr> bivectorStorage, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateMatrix(
                ScalarProcessor.BivectorToArray(bivectorStorage, vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ScalarPlusBivectorToMatrix(this IMultivectorStorage<Expr> multivectorStorage, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateMatrix(
                ScalarProcessor.ScalarPlusBivectorToArray(multivectorStorage, vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr KVectorToRowVectorMatrix(this VectorStorage<Expr> kVectorStorage, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                ScalarProcessor.KVectorToArrayVector(kVectorStorage, vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr KVectorToColumnVectorMatrix(this VectorStorage<Expr> kVectorStorage, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                ScalarProcessor.KVectorToArrayVector(kVectorStorage, vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr MultivectorToRowVectorMatrix(this IMultivectorStorage<Expr> multivectorStorage, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                ScalarProcessor.MultivectorToArrayVector(multivectorStorage, vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr MultivectorToColumnVectorMatrix(this IMultivectorStorage<Expr> multivectorStorage, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                ScalarProcessor.MultivectorToArrayVector(multivectorStorage, vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ArrayToMatrix(this Expr[,] array)
        {
            return MatrixProcessor.CreateMatrix(array.CreateLinMatrixDenseStorage());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ArrayToMatrix(this ILinMatrixStorage<Expr> array)
        {
            return MatrixProcessor.CreateMatrix(array);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr GetMatrix(this IOutermorphism<Expr> linearMap, int rowsCount, int columnsCount)
        {
            return MatrixProcessor.CreateMatrix(
                linearMap.GetVectorOmMappingMatrix(rowsCount, columnsCount)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr MatrixProduct(this Expr matrix1, Expr matrix2)
        {
            return MatrixProcessor.TimesMatrices(matrix1, matrix2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr MatrixDeterminant(this Expr[,] array)
        {
            return Mfs.Det[array.ArrayToMatrixExpr()];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetText(this Expr[,] array)
        {
            return TextComposer.GetArrayText(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetText(this IMultivectorStorage<Expr> mv)
        {
            return TextComposer.GetMultivectorText(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetLaTeX(this IMultivectorStorage<Expr> mv)
        {
            return LaTeXComposer.GetMultivectorText(mv);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AttachMathematicaExpressionEvaluator(this SymbolicContext context)
        {
            context.SymbolicEvaluator = 
                new MathematicaExpressionEvaluator(context);
        }


        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this bool value)
        {
            return new Expr(ExpressionType.Boolean, value ? "True" : "False");
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this int value)
        {
            return new Expr(value);
        }
        
        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this uint value)
        {
            return new Expr(value);
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this long value)
        {
            return new Expr(value);
        }
        
        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this ulong value)
        {
            return new Expr(value);
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this float value)
        {
            return new Expr(value);
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this double value)
        {
            return new Expr(value);
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given text expression using the Mathematica interface evaluator
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this string value)
        {
            return MathematicaInterface.DefaultCas.Connection.EvaluateToExpr(value);
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given text expression using the Mathematica interface evaluator
        /// </summary>
        /// <param name="value"></param>
        /// <param name="mathematicaInterface"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this string value, MathematicaInterface mathematicaInterface)
        {
            return mathematicaInterface.Connection.EvaluateToExpr(value);
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given symbol name
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToSymbolExpr(this string symbolName)
        {
            return new Expr(ExpressionType.Symbol, symbolName);
        }

        public static Expr[,] MatrixExprToArray(this Expr matrix)
        {
            var dimensionsExpr = Mfs.Dimensions[matrix].Simplify();

            var rowsCount = (int) dimensionsExpr[0].AsInt64();
            var colsCount = (int) dimensionsExpr[1].AsInt64();

            var array = new Expr[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            {
                var rowExpr = Mfs.Part[matrix, i.ToExpr()];

                for (var j = 0; j < colsCount; j++)
                {
                    array[i, j] = Mfs.Part[rowExpr, j.ToExpr()].Simplify();
                }
            }

            return array;
        }

        public static Expr ArrayToMatrixExpr(this Expr[,] exprArray)
        {
            var rowsCount = exprArray.GetLength(0);
            var colsCount = exprArray.GetLength(1);

            var rowsExprArray = new Expr[rowsCount];
            
            for (var i = 0; i < rowsCount; i++)
            {
                var rowItems = new Expr[colsCount];

                for (var j = 0; j < colsCount; j++)
                    rowItems[j] = exprArray[i, j];

                rowsExprArray[i] = Mfs.ListExpr(rowItems);
            }
            
            return Mfs.ListExpr(rowsExprArray);
        }
        
        /// <summary>
        /// Create a list of Mathematica Expr objects from the given symbol names
        /// </summary>
        /// <param name="symbolNames"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Expr> ToSymbolExprList(this IEnumerable<string> symbolNames)
        {
            return symbolNames.Select(symbolName => new Expr(ExpressionType.Symbol, symbolName));
        }

        /// <summary>
        /// Construct an Expr object from a head expression and some arguments
        /// </summary>
        /// <param name="funcNameSymbol"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ApplyTo(this Expr funcNameSymbol, params object[] args)
        {
            return new Expr(funcNameSymbol, args);
        }

        /// <summary>
        /// Construct an Expr object from a head symbol string and some arguments
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ApplyTo(this string funcName, params object[] args)
        {
            return new Expr(new Expr(ExpressionType.Symbol, funcName), args);
        }

        /// <summary>
        /// Get a list of all sub-expressions of the given expression using depth-first order. The original
        /// expression is the first on the list
        /// </summary>
        /// <param name="rootExpr"></param>
        /// <returns></returns>
        public static IEnumerable<Expr> SubExpressions(this Expr rootExpr)
        {
            if (ReferenceEquals(rootExpr, null))
                throw new ArgumentNullException(nameof(rootExpr));

            yield return rootExpr;

            if (rootExpr.Args.Length == 0)
                yield break;

            var stack = new Stack<Expr>(rootExpr.Args);

            while (stack.Count > 0)
            {
                var expr = stack.Pop();

                yield return expr;

                if (expr.Args.Length == 0)
                    continue;

                foreach (var subExpr in expr.Args)
                    stack.Push(subExpr);
            }
        }

        /// <summary>
        /// Get a list of all sub-expressions of the given expression using depth-first order. 
        /// The original expression is not included in the list
        /// </summary>
        /// <param name="rootExpr"></param>
        /// <returns></returns>
        public static IEnumerable<Expr> ProperSubExpressions(this Expr rootExpr)
        {
            if (ReferenceEquals(rootExpr, null))
                throw new ArgumentNullException(nameof(rootExpr));

            if (rootExpr.Args.Length == 0)
                yield break;

            var stack = new Stack<Expr>(rootExpr.Args);

            while (stack.Count > 0)
            {
                var expr = stack.Pop();

                yield return expr;

                if (expr.Args.Length == 0)
                    continue;

                foreach (var subExpr in expr.Args)
                    stack.Push(subExpr);
            }
        }

        /// <summary>
        /// Get a list of all sub-expressions of the given expression using depth-first order. 
        /// The sub-expression is accepted for output and traversal if skipFunc is false for that sub-expression.
        /// If a sub-expression is skiped so is all its sub-expressions
        /// </summary>
        /// <param name="rootExpr"></param>
        /// <param name="skipFunc"></param>
        /// <returns></returns>
        public static IEnumerable<Expr> SubExpressions(this Expr rootExpr, Func<Expr, bool> skipFunc)
        {
            if (ReferenceEquals(rootExpr, null))
                throw new ArgumentNullException(nameof(rootExpr));

            if (skipFunc(rootExpr))
                yield break;

            yield return rootExpr;

            var stack = new Stack<Expr>(rootExpr.Args);

            while (stack.Count > 0)
            {
                var expr = stack.Pop();

                if (skipFunc(rootExpr))
                    continue;

                yield return expr;

                foreach (var subExpr in expr.Args)
                    stack.Push(subExpr);
            }
        }

        /// <summary>
        /// Get a list of all sub-expressions of the given expression using depth-first order. 
        /// The root expression is not included in the list.
        /// The sub-expression is accepted for output and traversal if skipFunc is false for that sub-expression.
        /// If a sub-expression is skiped so is all its sub-expressions
        /// </summary>
        /// <param name="rootExpr"></param>
        /// <param name="skipFunc"></param>
        /// <returns></returns>
        public static IEnumerable<Expr> ProperSubExpressions(this Expr rootExpr, Func<Expr, bool> skipFunc)
        {
            if (ReferenceEquals(rootExpr, null))
                throw new ArgumentNullException(nameof(rootExpr));

            if (skipFunc(rootExpr))
                yield break;

            var stack = new Stack<Expr>(rootExpr.Args);

            while (stack.Count > 0)
            {
                var expr = stack.Pop();

                if (skipFunc(rootExpr))
                    continue;

                yield return expr;

                foreach (var subExpr in expr.Args)
                    stack.Push(subExpr);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr N(this Expr expr)
        {
            return MathematicaInterface.DefaultCas[
                Mfs.N[expr]
            ];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr Round(this Expr expr, int places)
        {
            return MathematicaInterface.DefaultCas[
                Mfs.Round[Mfs.N[expr], Math.Pow(10, -places).ToExpr()]
            ];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr Simplify(this Expr expr)
        {
            return expr.AtomQ() 
                ? expr 
                : MathematicaInterface.DefaultCas[Mfs.Simplify[expr]];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr Simplify(this Expr expr, Expr assumptionsExpr)
        {
            return MathematicaInterface.DefaultCas[
                Mfs.Simplify[expr, assumptionsExpr]
            ];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr Simplify(this Expr expr, MathematicaInterface casInterface)
        {
            return expr.AtomQ() 
                ? expr 
                : casInterface[Mfs.Simplify[expr]];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr SimplifyToExpr(this string exprText)
        {
            return $"Simplify[{exprText}]".ToExpr();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr FullSimplifyToExpr(this string exprText)
        {
            return $"FullSimplify[{exprText}]".ToExpr();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ReplaceAll(this Expr inputExpr, string subExprText1, string subExprText2)
        {
            return Mfs.ReplaceAll[
                inputExpr,
                Mfs.Rule[subExprText1.ToExpr(), subExprText2.ToExpr()]
            ].FullSimplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ReplaceAll(this Expr inputExpr, Expr subExpr1, Expr subExpr2)
        {
            return Mfs.ReplaceAll[
                inputExpr,
                Mfs.Rule[subExpr1, subExpr2]
            ].FullSimplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr FullSimplify(this Expr expr)
        {
            return expr.AtomQ() 
                ? expr 
                : MathematicaInterface.DefaultCas[Mfs.FullSimplify[expr]];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr FullSimplify(this Expr expr, Expr assumptionsExpr)
        {
            return MathematicaInterface.DefaultCas[
                Mfs.FullSimplify[expr, assumptionsExpr]
            ];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr FullSimplify(this Expr expr, MathematicaInterface casInterface)
        {
            return expr.AtomQ() 
                ? expr 
                : casInterface[Mfs.FullSimplify[expr]];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr Evaluate(this Expr expr)
        {
            return expr.AtomQ() 
                ? expr 
                : MathematicaInterface.DefaultCas[expr];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string EvaluateToText(this Expr expr)
        {
            return expr.AtomQ() 
                ? expr.ToString() 
                : MathematicaInterface.DefaultCasConnection.EvaluateToString(expr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr Expand(this Expr expr)
        {
            return expr.AtomQ() 
                ? expr 
                : MathematicaInterface.DefaultCas[Mfs.Expand[expr]];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr Expand(this Expr expr, MathematicaInterface casInterface)
        {
            return expr.AtomQ() 
                ? expr 
                : casInterface[Mfs.Expand[expr]];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero(this Expr expr)
        {
            if (!expr.NumberQ())
                return false;

            var exprText = expr.ToString();

            return exprText == "0" || exprText == "0.";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualZero(this Expr expr, MathematicaInterface casInterface)
        {
            return expr.IsZero() ||
                   casInterface.EvalTrueQ(Mfs.Equal[expr, Expr.INT_ZERO]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrZero(this Expr expr)
        {
            return ReferenceEquals(expr, null) ||
                   expr.IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEqualZero(this Expr expr, MathematicaInterface casInterface)
        {
            return ReferenceEquals(expr, null) || 
                   expr.IsZero() || 
                   casInterface.EvalTrueQ(Mfs.Equal[expr, Expr.INT_ZERO]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOne(this Expr expr)
        {
            if (!expr.NumberQ())
                return false;

            var exprText = expr.ToString();

            return exprText == "1" || exprText == "1.";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMinusOne(this Expr expr)
        {
            if (!expr.NumberQ())
                return false;

            var exprText = expr.ToString();

            return exprText == "-1" || exprText == "-1.";
        }


        /// <summary>
        /// Evaluates the given expression and if the result is 'True' it returns true
        /// If the result is anything else it returns false and raises no errors
        /// </summary>
        /// <param name="casInterface"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EvalTrueQ(this MathematicaInterface casInterface, Expr e)
        {
            return casInterface.Connection.EvaluateToExpr(e).ToString() == "True";
        }

        /// <summary>
        /// Evaluates the given expression and if the result is 'False' it returns true
        /// If the result is anything else it returns false and raises no errors
        /// </summary>
        /// <param name="casInterface"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EvalFalseQ(this MathematicaInterface casInterface, Expr e)
        {
            return casInterface.Connection.EvaluateToExpr(e).ToString() == "False";
        }

        /// <summary>
        /// Evaluates the given expression and if the result is 'True' it returns true
        /// If the result is 'False' it returns false
        /// If the result is anything else it raises an error
        /// </summary>
        /// <param name="casInterface"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EvalIsTrue(this MathematicaInterface casInterface, Expr e)
        {
            var result = casInterface.Connection.EvaluateToExpr(e).ToString();

            return result switch
            {
                "True" => true,
                "False" => false,
                _ => throw new InvalidOperationException("Expression did not evaluate to a constant boolean value")
            };
        }

        /// <summary>
        /// Evaluates the given expression and if the result is 'False' it returns true
        /// If the result is 'True' it returns false
        /// If the result is anything else it raises an error
        /// </summary>
        /// <param name="casInterface"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EvalIsFalse(this MathematicaInterface casInterface, Expr e)
        {
            var result = casInterface.Connection.EvaluateToExpr(e).ToString();

            return result switch
            {
                "True" => false,
                "False" => true,
                _ => throw new InvalidOperationException("Expression did not evaluate to a constant boolean value")
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Expr CreateElementExpr(List<Expr> items, Expr domainNameSymbol)
        {
            if (items.Count == 1)
                return Mfs.Element[items[0], domainNameSymbol];

            return
                items.Count > 1
                ? Mfs.Element[Mfs.Alternatives[items.Cast<object>().ToArray()], domainNameSymbol]
                : null;
        }

        public static Expr CreateAssumeExpr(this MathematicaInterface parentCas, Dictionary<string, MathematicaAtomicType> varTypes)
        {
            var complexesList = new List<Expr>();
            var realsList = new List<Expr>();
            var rationalsList = new List<Expr>();
            var integersList = new List<Expr>();
            var booleansList = new List<Expr>();

            foreach (var (key, value) in varTypes)
            {
                switch (value)
                {
                    case MathematicaAtomicType.Complex:
                        complexesList.Add(key.ToSymbolExpr());
                        break;

                    case MathematicaAtomicType.Real:
                        realsList.Add(key.ToSymbolExpr());
                        break;

                    case MathematicaAtomicType.Rational:
                        rationalsList.Add(key.ToSymbolExpr());
                        break;

                    case MathematicaAtomicType.Integer:
                        integersList.Add(key.ToSymbolExpr());
                        break;

                    case MathematicaAtomicType.Boolean:
                        booleansList.Add(key.ToSymbolExpr());
                        break;
                }
            }

            var domainElementsExpr = new List<Expr>(4);

            if (complexesList.Count > 0)
                domainElementsExpr.Add(CreateElementExpr(complexesList, DomainSymbols.Complexes));

            if (realsList.Count > 0)
                domainElementsExpr.Add(CreateElementExpr(realsList, DomainSymbols.Reals));

            if (rationalsList.Count > 0)
                domainElementsExpr.Add(CreateElementExpr(rationalsList, DomainSymbols.Rationals));

            if (integersList.Count > 0)
                domainElementsExpr.Add(CreateElementExpr(integersList, DomainSymbols.Integers));

            if (booleansList.Count > 0)
                domainElementsExpr.Add(CreateElementExpr(booleansList, DomainSymbols.Booleans));

            if (domainElementsExpr.Count == 0)
                return null;

            var expr = domainElementsExpr.Count == 1
                ? parentCas[domainElementsExpr[0]]
                : parentCas[Mfs.And[domainElementsExpr.Cast<object>().ToArray()]];

            return expr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBooleanScalar(this MathematicaScalar sc, Expr assumptionsExpr)
        {
            var cond = MathematicaCondition.CreateIsDomainMemberTest(sc, DomainSymbols.Booleans, assumptionsExpr);

            return cond.IsConstantTrue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsIntegerScalar(this MathematicaScalar sc, Expr assumptionsExpr)
        {
            var cond = MathematicaCondition.CreateIsDomainMemberTest(sc, DomainSymbols.Integers, assumptionsExpr);

            return cond.IsConstantTrue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRealScalar(this MathematicaScalar sc, Expr assumptionsExpr)
        {
            var cond = MathematicaCondition.CreateIsDomainMemberTest(sc, DomainSymbols.Reals, assumptionsExpr);

            return cond.IsConstantTrue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsComplexScalar(this MathematicaScalar sc, Expr assumptionsExpr)
        {
            var cond = MathematicaCondition.CreateIsDomainMemberTest(sc, DomainSymbols.Complexes, assumptionsExpr);

            return cond.IsConstantTrue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRationalScalar(this MathematicaScalar sc, Expr assumptionsExpr)
        {
            var cond = MathematicaCondition.CreateIsDomainMemberTest(sc, DomainSymbols.Rationals, assumptionsExpr);

            return cond.IsConstantTrue();
        }


        /// <summary>
        /// Convert the given Mathematica Expr object into a SteExpression object
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static SteExpression ToSimpleTextExpression(this Expr expr)
        {
            var isNumber = expr.NumberQ();
            var isSymbol = expr.SymbolQ();

            if (isNumber)
            {
                return isSymbol
                    ? SteExpression.CreateSymbolicNumber(expr.ToString())
                    : SteExpression.CreateLiteralNumber(expr.ToString());
            }

            if (isSymbol)
                return SteExpression.CreateVariable(expr.ToString());

            if (expr.Args.Length == 0)
                return SteExpression.CreateFunction(expr.ToString());

            var args = new SteExpression[expr.Args.Length];

            for (var i = 0; i < expr.Args.Length; i++)
                args[i] = ToSimpleTextExpression(expr.Args[i]);

            return SteExpression.CreateFunction(expr.Head.ToString(), args);
        }

        public static ISymbolicExpression ToSymbolicExpression(this Expr expr, SymbolicContext context)
        {
            var isNumber = expr.NumberQ();
            var isSymbol = expr.SymbolQ();

            if (isNumber)
                return context.GetOrDefineSymbolicNumber(
                    expr.ToString(), 
                    expr.ToNumber()
                );

            if (isSymbol)
                return context.GetVariable(expr.ToString());

            if (expr.Args.Length == 0)
                return SymbolicFunction.CreateNonAssociative(
                    context, 
                    expr.Head.ToString()
                );

            var args = expr.Args.Select(
                argExpr => ToSymbolicExpression(argExpr, context)
            );
            
            var functionName = expr.Head.ToString();
            return functionName switch
            {
                "Minus" => context.FunctionHeadSpecsFactory.Negative.CreateFunction(args),

                "Plus" => context.FunctionHeadSpecsFactory.Plus.CreateFunction(args),
                "Subtract" => context.FunctionHeadSpecsFactory.Subtract.CreateFunction(args),
                "Times" => context.FunctionHeadSpecsFactory.Times.CreateFunction(args),
                "Divide" => context.FunctionHeadSpecsFactory.Divide.CreateFunction(args),
                
                _ => SymbolicFunction.CreateNonAssociative(context, functionName, args)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ISymbolicExpression ToSymbolicExpression(this SymbolicContext context, Expr expr)
        {
            return ToSymbolicExpression(expr, context);
        }

        /// <summary>
        /// Convert this symbolic expression into a Mathematica expression object
        /// </summary>
        /// <param name="symbolicExpr"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this SteExpression symbolicExpr)
        {
            return MathematicaInterface.DefaultCas[symbolicExpr.ToString()];
        }

        /// <summary>
        /// Convert this symbolic expression into a Mathematica expression object
        /// </summary>
        /// <param name="symbolicExpr"></param>
        /// <param name="cas"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this SteExpression symbolicExpr, MathematicaInterface cas)
        {
            return cas[symbolicExpr.ToString()];
        }

        /// <summary>
        /// Convert this symbolic expression into a Mathematica expression object
        /// </summary>
        /// <param name="symbolicExpr"></param>
        /// <param name="cas"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr SimplifyToExpr(this SteExpression symbolicExpr, MathematicaInterface cas)
        {
            return cas[Mfs.Simplify[symbolicExpr.ToString()]];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetLaTeX(this Expr expr)
        {
            return Mfs.EToString[Mfs.TeXForm[expr]].EvaluateToText().Trim();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetLaTeXInlineEquation(this Expr expr)
        {
            return "$" + Mfs.EToString[Mfs.TeXForm[expr]].EvaluateToText().Trim() + "$";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetLaTeXDisplayEquation(this Expr expr)
        {
            return new StringBuilder()
                .AppendLine(@"\[")
                .AppendLine(Mfs.EToString[Mfs.TeXForm[expr]].EvaluateToText().Trim())
                .AppendLine(@"\]")
                .ToString();
        }
    }
}
