using System;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.Expression;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Mathematica.Text;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica
{
    public static class GaSymbolicUtils
    {
        public static MathematicaScalarProcessor ScalarProcessor
            => MathematicaScalarProcessor.DefaultProcessor;
        
        public static LaMathematicaProcessor MatrixProcessor
            => LaMathematicaProcessor.DefaultProcessor;

        public static IGaProcessorEuclidean<Expr> EuclideanProcessor { get; }
            = ScalarProcessor.CreateGaEuclideanProcessor(63);

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
        public static IGaMultivectorStorage<Expr> ToSymbolic(this IGaMultivectorStorage<double> storage)
        {
            return storage.MapScalars(number => number.ToExpr());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<double> ToNumeric(this IGaMultivectorStorage<Expr> storage)
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

        public static Expr[] SimplifyScalars(this Expr[] array)
        {
            var count = array.Length;

            var newArray = new Expr[count];

            for (var i = 0; i < count; i++)
                newArray[i] = array[i].Simplify();

            return newArray;
        }

        public static Expr[] SimplifyScalars(this Expr[] array, Expr assumptionsExpr)
        {
            var count = array.Length;

            var newArray = new Expr[count];

            for (var i = 0; i < count; i++)
                newArray[i] = array[i].Simplify(assumptionsExpr);

            return newArray;
        }

        public static Expr[,] SimplifyScalars(this Expr[,] array)
        {
            var rowsCount = array.GetLength(0);
            var colsCount = array.GetLength(1);

            var newArray = new Expr[rowsCount, colsCount];

            for (var j = 0; j < colsCount; j++)
            for (var i = 0; i < rowsCount; i++)
                newArray[i, j] = array[i, j].Simplify();

            return newArray;
        }

        public static Expr[,] SimplifyScalars(this Expr[,] array, Expr assumptionsExpr)
        {
            var rowsCount = array.GetLength(0);
            var colsCount = array.GetLength(1);

            var newArray = new Expr[rowsCount, colsCount];

            for (var j = 0; j < colsCount; j++)
            for (var i = 0; i < rowsCount; i++)
                newArray[i, j] = array[i, j].Simplify(assumptionsExpr);

            return newArray;
        }

        public static Expr[] FullSimplifyScalars(this Expr[] array)
        {
            var count = array.Length;

            var newArray = new Expr[count];

            for (var i = 0; i < count; i++)
                newArray[i] = array[i].FullSimplify();

            return newArray;
        }

        public static Expr[] FullSimplifyScalars(this Expr[] array, Expr assumptionsExpr)
        {
            var count = array.Length;

            var newArray = new Expr[count];

            for (var i = 0; i < count; i++)
                newArray[i] = array[i].FullSimplify(assumptionsExpr);

            return newArray;
        }

        public static Expr[,] FullSimplifyScalars(this Expr[,] array)
        {
            var rowsCount = array.GetLength(0);
            var colsCount = array.GetLength(1);

            var newArray = new Expr[rowsCount, colsCount];

            for (var j = 0; j < colsCount; j++)
            for (var i = 0; i < rowsCount; i++)
                newArray[i, j] = array[i, j].FullSimplify();

            return newArray;
        }

        public static Expr[,] FullSimplifyScalars(this Expr[,] array, Expr assumptionsExpr)
        {
            var rowsCount = array.GetLength(0);
            var colsCount = array.GetLength(1);

            var newArray = new Expr[rowsCount, colsCount];

            for (var j = 0; j < colsCount; j++)
            for (var i = 0; i < rowsCount; i++)
                newArray[i, j] = array[i, j].FullSimplify(assumptionsExpr);

            return newArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<Expr> FullSimplifyScalars(this ILaVectorEvenStorage<Expr> storage)
        {
            return storage.MapScalars(scalar => scalar.FullSimplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<Expr> FullSimplifyScalars(this ILaVectorEvenStorage<Expr> storage, Expr assumptionsExpr)
        {
            return storage.MapScalars(scalar => scalar.FullSimplify(assumptionsExpr));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<Expr> FullSimplifyScalars(this ILaMatrixEvenStorage<Expr> storage)
        {
            return storage.MapScalars(scalar => scalar.FullSimplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<Expr> FullSimplifyScalars(this ILaMatrixEvenStorage<Expr> storage, Expr assumptionsExpr)
        {
            return storage.MapScalars(scalar => scalar.FullSimplify(assumptionsExpr));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<Expr> SimplifyScalars(this ILaVectorEvenStorage<Expr> storage)
        {
            return storage.MapScalars(scalar => scalar.Simplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<Expr> SimplifyScalars(this ILaVectorEvenStorage<Expr> storage, Expr assumptionsExpr)
        {
            return storage.MapScalars(scalar => scalar.Simplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<Expr> SimplifyScalars(this ILaMatrixEvenStorage<Expr> storage)
        {
            return storage.MapScalars(scalar => scalar.Simplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<Expr> SimplifyScalars(this ILaMatrixEvenStorage<Expr> storage, Expr assumptionsExpr)
        {
            return storage.MapScalars(scalar => scalar.Simplify(assumptionsExpr));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<Expr> SimplifyScalars(this IGaMultivectorStorage<Expr> storage)
        {
            return storage.MapScalars(scalar => scalar.Simplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<Expr> SimplifyScalars(this IGaMultivectorStorage<Expr> storage, Expr assumptionsExpr)
        {
            return storage.MapScalars(scalar => scalar.Simplify(assumptionsExpr));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<Expr> FullSimplifyScalars(this IGaMultivectorStorage<Expr> storage)
        {
            return storage.MapScalars(scalar => scalar.FullSimplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<Expr> FullSimplifyScalars(this IGaMultivectorStorage<Expr> storage, Expr assumptionsExpr)
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
        public static ILaVectorEvenStorage<Expr> DifferentiateScalars(this ILaVectorEvenStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.MapScalars(
                scalar => Mfs.D[scalar, variableExpr].FullSimplify()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorGradedStorage<Expr> DifferentiateScalars(this ILaVectorGradedStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.MapScalars(
                scalar => Mfs.D[scalar, variableExpr].FullSimplify()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<Expr> DifferentiateScalars(this ILaMatrixEvenStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.MapScalars(
                scalar => Mfs.D[scalar, variableExpr].FullSimplify()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixGradedStorage<Expr> DifferentiateScalars(this ILaMatrixGradedStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.MapScalars(
                scalar => Mfs.D[scalar, variableExpr].FullSimplify()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<Expr> DifferentiateScalars(this IGaMultivectorStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.MapScalars(
                scalar => Mfs.D[scalar, variableExpr].FullSimplify()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<Expr> DifferentiateScalars(this IGaVectorStorage<Expr> storage, string variableName)
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
        public static ILaVectorEvenStorage<Expr> IntegrateScalars(this ILaVectorEvenStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.MapScalars(
                scalar => Mfs.Integrate[scalar, variableExpr].FullSimplify()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorGradedStorage<Expr> IntegrateScalars(this ILaVectorGradedStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.MapScalars(
                scalar => Mfs.Integrate[scalar, variableExpr].FullSimplify()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<Expr> IntegrateScalars(this ILaMatrixEvenStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.MapScalars(
                scalar => Mfs.Integrate[scalar, variableExpr].FullSimplify()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixGradedStorage<Expr> IntegrateScalars(this ILaMatrixGradedStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.MapScalars(
                scalar => Mfs.Integrate[scalar, variableExpr].FullSimplify()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<Expr> IntegrateScalars(this IGaMultivectorStorage<Expr> storage, string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return storage.MapScalars(
                scalar => Mfs.Integrate[scalar, variableExpr].FullSimplify()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<Expr> IntegrateScalars(this IGaVectorStorage<Expr> storage, string variableName)
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
        public static IGaMultivectorStorage<Expr> HilbertTransformScalars(this IGaMultivectorStorage<Expr> storage, string timeVariableName, string freqVariableName)
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
        public static IGaVectorStorage<Expr> HilbertTransformScalars(this IGaVectorStorage<Expr> storage, string timeVariableName, string freqVariableName)
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

        //public static Expr GetScalarExpr(this GaRandomGenerator randomGenerator)
        //{
        //    return randomGenerator.GetScalar().ToExpr();
        //}

        //public static Expr GetScalarExpr(this GaRandomGenerator randomGenerator, double maxLimit)
        //{
        //    return randomGenerator.GetScalar(maxLimit).ToExpr();
        //}

        //public static Expr GetScalarExpr(this GaRandomGenerator randomGenerator, double minLimit, double maxLimit)
        //{
        //    return randomGenerator.GetScalar(minLimit, maxLimit).ToExpr();
        //}


        //public static Expr GetIntegerExpr(this GaRandomGenerator randomGenerator)
        //{
        //    return randomGenerator.GetInteger().ToExpr();
        //}

        //public static Expr GetIntegerExpr(this GaRandomGenerator randomGenerator, int maxLimit)
        //{
        //    return randomGenerator.GetInteger(maxLimit).ToExpr();
        //}

        //public static Expr GetIntegerExpr(this GaRandomGenerator randomGenerator, int minLimit, int maxLimit)
        //{
        //    return randomGenerator.GetInteger(minLimit, maxLimit).ToExpr();
        //}


        //public static MathematicaScalar GetSymbolicInteger(this GaRandomGenerator randomGenerator)
        //{
        //    return MathematicaScalar.Create(Cas, randomGenerator.GetInteger());
        //}

        //public static MathematicaScalar GetSymbolicInteger(this GaRandomGenerator randomGenerator, int maxLimit)
        //{
        //    return MathematicaScalar.Create(Cas, randomGenerator.GetInteger(maxLimit));
        //}

        //public static MathematicaScalar GetSymbolicInteger(this GaRandomGenerator randomGenerator, int minLimit, int maxLimit)
        //{
        //    return MathematicaScalar.Create(Cas, randomGenerator.GetInteger(minLimit, maxLimit));
        //}


        //public static MathematicaScalar GetSymbolicScalar(this GaRandomGenerator randomGenerator)
        //{
        //    return MathematicaScalar.Create(Cas, randomGenerator.GetScalar());
        //}

        //public static MathematicaScalar GetSymbolicScalar(this GaRandomGenerator randomGenerator, double maxLimit)
        //{
        //    return MathematicaScalar.Create(Cas, randomGenerator.GetScalar(maxLimit));
        //}

        //public static MathematicaScalar GetSymbolicScalar(this GaRandomGenerator randomGenerator, double minLimit, double maxLimit)
        //{
        //    return MathematicaScalar.Create(Cas, randomGenerator.GetScalar(minLimit, maxLimit));
        //}


        //public static GaSymMultivector GetSymMultivectorFull(this GaRandomGenerator randomGenerator, int vSpaceDim)
        //{
        //    var gaSpaceDim = 1UL << vSpaceDim;
        //    var mv = GaSymMultivector.CreateZero(vSpaceDim);

        //    for (var basisBladeId = 0UL; basisBladeId < gaSpaceDim; basisBladeId++)
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

        //    return mv;
        //}

        //public static GaSymMultivector GetSymMultivectorFull(this GaRandomGenerator randomGenerator, int vSpaceDim, double maxValue)
        //{
        //    var gaSpaceDim = 1UL << vSpaceDim;
        //    var mv = GaSymMultivector.CreateZero(vSpaceDim);

        //    for (var basisBladeId = 0UL; basisBladeId < gaSpaceDim; basisBladeId++)
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(maxValue));

        //    return mv;
        //}

        //public static GaSymMultivector GetSymMultivectorFull(this GaRandomGenerator randomGenerator, int vSpaceDim, double minValue, double maxValue)
        //{
        //    var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();

        //    var mv = GaSymMultivector.CreateZero(vSpaceDim);

        //    for (var basisBladeId = 0UL; basisBladeId < gaSpaceDim; basisBladeId++)
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(minValue, maxValue));

        //    return mv;
        //}

        //public static GaSymMultivector GetSymMultivectorByTerms(this GaRandomGenerator randomGenerator, int vSpaceDim, params ulong[] basisBladeIDs)
        //{
        //    var mv = GaSymMultivector.CreateZero(vSpaceDim);

        //    foreach (var basisBladeId in basisBladeIDs)
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

        //    return mv;
        //}

        //public static GaSymMultivector GetSymMultivectorByTerms(this GaRandomGenerator randomGenerator, int vSpaceDim, IEnumerable<ulong> basisBladeIDs)
        //{
        //    var mv = GaSymMultivector.CreateZero(vSpaceDim);

        //    foreach (var basisBladeId in basisBladeIDs)
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

        //    return mv;
        //}

        //public static GaSymMultivector GetSymMultivectorByGrades(this GaRandomGenerator randomGenerator, int vSpaceDim, params int[] grades)
        //{
        //    var mv = GaSymMultivector.CreateZero(vSpaceDim);

        //    var basisBladeIDs =
        //        GaFrameUtils.BasisBladeIDsOfGrades(
        //            mv.VSpaceDimension,
        //            grades
        //        );

        //    foreach (var basisBladeId in basisBladeIDs)
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

        //    return mv;
        //}

        //public static GaSymMultivector GetSymMultivector(this GaRandomGenerator randomGenerator, int vSpaceDim)
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

        //public static GaSymMultivector GetSymMultivector(this GaRandomGenerator randomGenerator, int vSpaceDim, string baseCoefName)
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
        //    return GaSymMultivector.CreateSymbolic(vSpaceDim, baseCoefName, basisBladeIDs);
        //}


        //public static GaSymMultivector GetSymTerm(this GaRandomGenerator randomGenerator, int vSpaceDim, string baseCoefName)
        //{
        //    var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();

        //    //Randomly select the number of terms in the multivector
        //    var basisBladeId = (ulong)randomGenerator.GetInteger((int)gaSpaceDim - 1);

        //    //Generate the multivector's symbolic coefficients
        //    return GaSymMultivector.CreateSymbolicTerm(vSpaceDim, baseCoefName, basisBladeId);
        //}


        //public static GaSymMultivector GetSymVector(this GaRandomGenerator randomGenerator, int gaSpaceDim)
        //{
        //    var mv = GaSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

        //    return mv;
        //}

        //public static GaSymMultivector GetSymVector(this GaRandomGenerator randomGenerator, int gaSpaceDim, double maxValue)
        //{
        //    var mv = GaSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(maxValue));

        //    return mv;
        //}

        //public static GaSymMultivector GetSymVector(this GaRandomGenerator randomGenerator, int gaSpaceDim, double minValue, double maxValue)
        //{
        //    var mv = GaSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(minValue, maxValue));

        //    return mv;
        //}


        //public static GaSymMultivector GetSymIntegerVector(this GaRandomGenerator randomGenerator, int gaSpaceDim)
        //{
        //    var mv = GaSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetIntegerExpr());

        //    return mv;
        //}

        //public static GaSymMultivector GetSymIntegerVector(this GaRandomGenerator randomGenerator, int gaSpaceDim, int maxLimit)
        //{
        //    var mv = GaSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetIntegerExpr(maxLimit));

        //    return mv;
        //}

        //public static GaSymMultivector GetSymIntegerVector(this GaRandomGenerator randomGenerator, int gaSpaceDim, int minLimit, int maxLimit)
        //{
        //    var mv = GaSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetIntegerExpr(minLimit, maxLimit));

        //    return mv;
        //}


        //public static IEnumerable<GaSymMultivector> GetSymIntegerLidVectors(this GaRandomGenerator randomGenerator, int gaSpaceDim, int count)
        //{
        //    var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

        //    if (count < 1 || count > vSpaceDim)
        //        yield break;

        //    var mv = GaSymMultivector.CreateScalar(gaSpaceDim, Expr.INT_ONE);
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

        //public static IEnumerable<GaSymMultivector> GetSymIntegerLidVectors(this GaRandomGenerator randomGenerator, int gaSpaceDim, int count, int maxValue)
        //{
        //    var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

        //    if (count < 1 || count > vSpaceDim)
        //        yield break;

        //    var mv = GaSymMultivector.CreateScalar(gaSpaceDim, Expr.INT_ONE);
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

        //public static IEnumerable<GaSymMultivector> GetSymIntegerLidVectors(this GaRandomGenerator randomGenerator, int gaSpaceDim, int count, int minValue, int maxValue)
        //{
        //    var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

        //    if (count < 1 || count > vSpaceDim)
        //        yield break;

        //    var mv = GaSymMultivector.CreateScalar(gaSpaceDim, Expr.INT_ONE);
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


        //public static GaSymMultivector GetSymKVector(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade)
        //{
        //    var mv = GaSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

        //    return mv;
        //}

        //public static GaSymMultivector GetSymKVector(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade, double maxValue)
        //{
        //    var mv = GaSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(maxValue));

        //    return mv;
        //}

        //public static GaSymMultivector GetSymKVector(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade, double minValue, double maxValue)
        //{
        //    var mv = GaSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(minValue, maxValue));

        //    return mv;
        //}


        //public static GaSymMultivector GetSymIntegerKVector(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade)
        //{
        //    var mv = GaSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetIntegerExpr());

        //    return mv;
        //}

        //public static GaSymMultivector GetSymIntegerKVector(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade, int maxValue)
        //{
        //    var mv = GaSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetIntegerExpr(maxValue));

        //    return mv;
        //}

        //public static GaSymMultivector GetSymIntegerKVector(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade, int minValue, int maxValue)
        //{
        //    var mv = GaSymMultivector.CreateZero(gaSpaceDim);

        //    foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
        //        mv.SetTermCoef(basisBladeId, randomGenerator.GetIntegerExpr(minValue, maxValue));

        //    return mv;
        //}


        //public static GaSymMultivector GetSymTerm(this GaRandomGenerator randomGenerator, int vSpaceDim, ulong basisBladeId)
        //{
        //    var mv = GaSymMultivector.CreateZero(vSpaceDim);

        //    mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

        //    return mv;
        //}

        //public static GaSymMultivector GetSymTerm(this GaRandomGenerator randomGenerator, int vSpaceDim, ulong basisBladeId, double maxValue)
        //{
        //    var mv = GaSymMultivector.CreateZero(vSpaceDim);

        //    mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(maxValue));

        //    return mv;
        //}

        //public static GaSymMultivector GetSymTerm(this GaRandomGenerator randomGenerator, int vSpaceDim, ulong basisBladeId, double minValue, double maxValue)
        //{
        //    var mv = GaSymMultivector.CreateZero(vSpaceDim);

        //    mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(minValue, maxValue));

        //    return mv;
        //}

        //public static GaSymMultivector GetSymTerm(this GaRandomGenerator randomGenerator, int vSpaceDim, int grade, ulong index)
        //{
        //    var mv = GaSymMultivector.CreateZero(vSpaceDim);

        //    var basisBladeId = GaFrameUtils.BasisBladeId(grade, index);

        //    mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

        //    return mv;
        //}

        //public static GaSymMultivector GetSymTerm(this GaRandomGenerator randomGenerator, int vSpaceDim, int grade, ulong index, double maxValue)
        //{
        //    var mv = GaSymMultivector.CreateZero(vSpaceDim);

        //    var basisBladeId = GaFrameUtils.BasisBladeId(grade, index);

        //    mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(maxValue));

        //    return mv;
        //}

        //public static GaSymMultivector GetSymTerm(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade, ulong index, double minValue, double maxValue)
        //{
        //    var mv = GaSymMultivector.CreateZero(gaSpaceDim);

        //    var basisBladeId = GaFrameUtils.BasisBladeId(grade, index);

        //    mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(minValue, maxValue));

        //    return mv;
        //}


        //public static GaSymMultivector GetSymBlade(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade)
        //{
        //    var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

        //    if (grade < 0 || grade > vSpaceDim)
        //        return GaSymMultivector.CreateZero(gaSpaceDim);

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

        //public static GaSymMultivector GetSymBlade(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade, double maxValue)
        //{
        //    var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

        //    if (grade < 0 || grade > vSpaceDim)
        //        return GaSymMultivector.CreateZero(gaSpaceDim);

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

        //public static GaSymMultivector GetSymBlade(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade, double minValue, double maxValue)
        //{
        //    var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

        //    if (grade < 0 || grade > vSpaceDim)
        //        return GaSymMultivector.CreateZero(gaSpaceDim);

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


        //public static GaSymMultivector GetSymIntegerBlade(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade)
        //{
        //    var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

        //    if (grade < 0 || grade > vSpaceDim)
        //        return GaSymMultivector.CreateZero(gaSpaceDim);

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

        //public static GaSymMultivector GetSymIntegerBlade(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade, int maxValue)
        //{
        //    var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

        //    if (grade < 0 || grade > vSpaceDim)
        //        return GaSymMultivector.CreateZero(gaSpaceDim);

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

        //public static GaSymMultivector GetSymIntegerBlade(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade, int minValue, int maxValue)
        //{
        //    var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

        //    if (grade < 0 || grade > vSpaceDim)
        //        return GaSymMultivector.CreateZero(gaSpaceDim);

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


        //public static GaSymMultivector GetSymNonNullVector(this GaRandomGenerator randomGenerator, GaSymFrame frame)
        //{
        //    GaSymMultivector mv;

        //    do
        //        mv = randomGenerator.GetSymVector(frame.VSpaceDimension);
        //    while (!frame.Norm2(mv).IsZero());

        //    return mv;
        //}


        //public static GaSymMultivector GetSymVersor(this GaRandomGenerator randomGenerator, GaSymFrame frame, int vectorsCount)
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
        public static IGaVectorStorage<Expr> CreateVector(params Expr[] scalarArray)
        {
            return ScalarProcessor.CreateGaVectorStorage(scalarArray
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<Expr> CreateVector(params string[] scalarTextArray)
        {
            return ScalarProcessor.CreateGaVectorStorage(scalarTextArray.Select(t => t.ToExpr()).ToArray()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<Expr> CreateBasisVector(int index)
        {
            return ScalarProcessor.CreateGaVectorStorage(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaOutermorphism<Expr> CreateVectorsLinearMap(int basisVectorsCount, Func<IGaVectorStorage<Expr>, IGaVectorStorage<Expr>> basisVectorMapFunc)
        {
            return EuclideanProcessor.CreateComputedOutermorphism(
                basisVectorsCount,
                basisVectorMapFunc
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr VectorToRowVectorMatrix(this IGaVectorStorage<Expr> vectorStorage, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                ScalarProcessor.VectorToArrayVector(vectorStorage, vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr VectorToColumnVectorMatrix(this IGaVectorStorage<Expr> vectorStorage, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                ScalarProcessor.VectorToArrayVector(vectorStorage, vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr BivectorToRowVectorMatrix(this IGaBivectorStorage<Expr> bivectorStorage, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                ScalarProcessor.BivectorToArrayVector(bivectorStorage, vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr BivectorToColumnVectorMatrix(this IGaBivectorStorage<Expr> bivectorStorage, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                ScalarProcessor.BivectorToArrayVector(bivectorStorage, vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr BivectorToMatrix(this IGaBivectorStorage<Expr> bivectorStorage, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateMatrix(
                ScalarProcessor.BivectorToArray(bivectorStorage, vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ScalarPlusBivectorToMatrix(this IGaMultivectorStorage<Expr> multivectorStorage, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateMatrix(
                ScalarProcessor.ScalarPlusBivectorToArray(multivectorStorage, vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr KVectorToRowVectorMatrix(this IGaVectorStorage<Expr> kVectorStorage, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                ScalarProcessor.KVectorToArrayVector(kVectorStorage, vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr KVectorToColumnVectorMatrix(this IGaVectorStorage<Expr> kVectorStorage, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                ScalarProcessor.KVectorToArrayVector(kVectorStorage, vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr MultivectorToRowVectorMatrix(this IGaMultivectorStorage<Expr> multivectorStorage, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                ScalarProcessor.MultivectorToArrayVector(multivectorStorage, vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr MultivectorToColumnVectorMatrix(this IGaMultivectorStorage<Expr> multivectorStorage, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                ScalarProcessor.MultivectorToArrayVector(multivectorStorage, vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ArrayToMatrix(this Expr[,] array)
        {
            return MatrixProcessor.CreateMatrix(array.CreateEvenGridDense());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ArrayToMatrix(this ILaMatrixEvenStorage<Expr> array)
        {
            return MatrixProcessor.CreateMatrix(array);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr GetMatrix(this IGaOutermorphism<Expr> linearMap, int rowsCount, int columnsCount)
        {
            return MatrixProcessor.CreateMatrix(
                linearMap.GetVectorsMappingArray(rowsCount, columnsCount)
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
        public static string GetText(this IGaMultivectorStorage<Expr> mv)
        {
            return TextComposer.GetMultivectorText(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetLaTeX(this IGaMultivectorStorage<Expr> mv)
        {
            return LaTeXComposer.GetMultivectorText(mv);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AttachMathematicaExpressionEvaluator(this SymbolicContext context)
        {
            context.ExpressionEvaluator = 
                new MathematicaSymbolicExpressionEvaluator(context);
        }
    }
}
