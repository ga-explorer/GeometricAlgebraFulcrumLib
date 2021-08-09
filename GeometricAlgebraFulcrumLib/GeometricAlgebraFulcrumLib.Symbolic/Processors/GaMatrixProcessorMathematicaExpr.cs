using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Symbolic.Processors
{
    public sealed class GaMatrixProcessorMathematicaExpr
        : IGaMatrixProcessor<Expr, Expr>
    {
        public static GaMatrixProcessorMathematicaExpr DefaultProcessor { get; }
            = new GaMatrixProcessorMathematicaExpr();


        public IGaScalarProcessor<Expr> ScalarProcessor { get; set; }
            = GaScalarProcessorMathematicaExpr.DefaultProcessor;

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


        public Expr Add(Expr scalar1, Expr scalar2)
        {
            return ScalarProcessor.Add(scalar1, scalar2);
        }

        public Expr Add(params Expr[] scalarsList)
        {
            return ScalarProcessor.Add(scalarsList);
        }

        public Expr Add(IEnumerable<Expr> scalarsList)
        {
            return ScalarProcessor.Add(scalarsList);
        }

        public Expr Subtract(Expr scalar1, Expr scalar2)
        {
            return ScalarProcessor.Subtract(scalar1, scalar2);
        }

        public Expr Times(Expr scalar1, Expr scalar2)
        {
            return ScalarProcessor.Times(scalar1, scalar2);
        }

        public Expr Times(params Expr[] scalarsList)
        {
            return ScalarProcessor.Times(scalarsList);
        }

        public Expr Times(IEnumerable<Expr> scalarsList)
        {
            return ScalarProcessor.Times(scalarsList);
        }

        public Expr NegativeTimes(Expr scalar1, Expr scalar2)
        {
            return ScalarProcessor.NegativeTimes(scalar1, scalar2);
        }

        public Expr NegativeTimes(params Expr[] scalarsList)
        {
            return ScalarProcessor.NegativeTimes(scalarsList);
        }

        public Expr NegativeTimes(IEnumerable<Expr> scalarsList)
        {
            return ScalarProcessor.NegativeTimes(scalarsList);
        }

        public Expr Divide(Expr scalar1, Expr scalar2)
        {
            return ScalarProcessor.Divide(scalar1, scalar2);
        }

        public Expr NegativeDivide(Expr scalar1, Expr scalar2)
        {
            return ScalarProcessor.NegativeDivide(scalar1, scalar2);
        }

        public Expr Positive(Expr scalar)
        {
            return ScalarProcessor.Positive(scalar);
        }

        public Expr Negative(Expr scalar)
        {
            return ScalarProcessor.Negative(scalar);
        }

        public Expr Inverse(Expr scalar)
        {
            return ScalarProcessor.Inverse(scalar);
        }


        public int GetRowsCount(Expr matrix)
        {
            var dimensionsExpr = Mfs.Dimensions[matrix].Simplify();

            return (int) dimensionsExpr[0].AsInt64();
        }

        public int GetColumnsCount(Expr matrix)
        {
            var dimensionsExpr = Mfs.Dimensions[matrix].Simplify();

            return (int) dimensionsExpr[1].AsInt64();
        }

        public Tuple<int, int> GetSize(Expr matrix)
        {
            var dimensionsExpr = Mfs.Dimensions[matrix].Simplify();

            return new Tuple<int, int>(
                (int) dimensionsExpr[0].AsInt64(),
                (int) dimensionsExpr[1].AsInt64()
            );
        }

        public Expr[,] GetArray(Expr matrix)
        {
            var (rowsCount, columnsCount) = GetSize(matrix);

            var array = new Expr[rowsCount, columnsCount];

            for (var i = 0; i < rowsCount; i++)
            {
                var rowExpr = Mfs.Part[matrix, i.ToExpr()];

                for (var j = 0; j < columnsCount; j++)
                {
                    array[i, j] = Mfs.Part[rowExpr, j.ToExpr()].Simplify();
                }
            }

            return array;
        }

        public Expr CreateMatrix(Expr[,] array)
        {
            var rowsCount = array.GetLength(0);
            var columnsCount = array.GetLength(1);

            var rowExprArray = new Expr[rowsCount];
            for (var i = 0; i < rowsCount; i++)
            {
                var scalarExprArray = new Expr[columnsCount];
                
                for (var j = 0; j < columnsCount; j++)
                    scalarExprArray[j] = array[i, j] ?? Expr.INT_ZERO;

                rowExprArray[i] = Mfs.ListExpr(scalarExprArray);
            }

            return Mfs.ListExpr(rowExprArray).Simplify();
        }

        public Expr CreateRowVectorMatrix(Expr[] array)
        {
            var rowExprArray = new Expr[1];
            var scalarExprArray = new Expr[array.Length];
            
            for (var j = 0; j < array.Length; j++)
                scalarExprArray[j] = array[j] ?? Expr.INT_ZERO;

            rowExprArray[0] = Mfs.ListExpr(scalarExprArray);

            return Mfs.ListExpr(rowExprArray).Simplify();
        }

        public Expr CreateRowVectorMatrix(Expr[,] array, int rowIndex)
        {
            var count = array.GetLength(1);
            var rowExprArray = new Expr[1];
            var scalarExprArray = new Expr[count];
            
            for (var j = 0; j < count; j++)
                scalarExprArray[j] = 
                    array[rowIndex, j] ?? Expr.INT_ZERO;

            rowExprArray[0] = Mfs.ListExpr(scalarExprArray);

            return Mfs.ListExpr(rowExprArray).Simplify();
        }

        public Expr CreateColumnVectorMatrix(Expr[] array)
        {
            var rowExprArray = new Expr[array.Length];
            for (var i = 0; i < array.Length; i++)
            {
                var scalarExprArray = new Expr[1];
                
                scalarExprArray[0] = 
                    array[i] ?? Expr.INT_ZERO;

                rowExprArray[i] = Mfs.ListExpr(scalarExprArray);
            }

            return Mfs.ListExpr(rowExprArray).Simplify();
        }

        public Expr CreateColumnVectorMatrix(Expr[,] array, int columnIndex)
        {
            var count = array.GetLength(0);
            var rowExprArray = new Expr[count];
            for (var i = 0; i < count; i++)
            {
                var scalarExprArray = new Expr[1];
                
                scalarExprArray[0] = 
                    array[i, columnIndex] ?? Expr.INT_ZERO;

                rowExprArray[i] = Mfs.ListExpr(scalarExprArray);
            }

            return Mfs.ListExpr(rowExprArray).Simplify();
        }

        public Expr CreateZeroMatrix(int size)
        {
            var sizeExpr = size.ToExpr();

            return Mfs.ConstantArray[
                Expr.INT_ZERO,
                Mfs.List[sizeExpr, sizeExpr]
            ].Evaluate();
        }

        public Expr CreateZeroMatrix(int rowsCount, int columnsCount)
        {
            return Mfs.ConstantArray[
                Expr.INT_ZERO,
                Mfs.List[rowsCount.ToExpr(), columnsCount.ToExpr()]
            ].Evaluate();
        }

        public Expr CreateUnityMatrix(int size)
        {
            return Mfs.IdentityMatrix[size.ToExpr()].Simplify();
        }

        public Expr AddMatrixScalar(Expr matrix, Expr scalar)
        {
            return Mfs.Plus[matrix, scalar].Simplify();
        }

        public Expr AddScalarMatrix(Expr scalar, Expr matrix)
        {
            return Mfs.Plus[scalar, matrix].Simplify();
        }

        public Expr AddMatrices(Expr matrix1, Expr matrix2)
        {
            return Mfs.Plus[matrix1, matrix2].Simplify();
        }

        public Expr SubtractMatrixScalar(Expr matrix, Expr scalar)
        {
            return Mfs.Plus[matrix, scalar].Simplify();
        }

        public Expr SubtractScalarMatrix(Expr scalar, Expr matrix)
        {
            return Mfs.Plus[scalar, matrix].Simplify();
        }

        public Expr SubtractMatrices(Expr matrix1, Expr matrix2)
        {
            return Mfs.Plus[matrix1, matrix2].Simplify();
        }

        public Expr ScalarMatrixProduct(Expr scalar, Expr matrix)
        {
            return Mfs.Times[scalar, matrix].Simplify();
        }

        public Expr MatrixScalarProduct(Expr matrix, Expr scalar)
        {
            return Mfs.Times[matrix, scalar].Simplify();
        }

        public Expr TimesScalars(Expr matrix1, Expr matrix2)
        {
            return Mfs.Times[matrix1, matrix2].Simplify();
        }

        public Expr DivideScalars(Expr matrix1, Expr matrix2)
        {
            return Mfs.Divide[matrix1, matrix2].Simplify();
        }

        public Expr DivideMatrixScalar(Expr matrix, Expr scalar)
        {
            return Mfs.Divide[matrix, scalar].Simplify();
        }

        public Expr DivideScalarMatrix(Expr scalar, Expr matrix)
        {
            return Mfs.Divide[scalar, matrix].Simplify();
        }

        public Expr MatrixProduct(Expr matrix1, Expr matrix2)
        {
            return Mfs.Dot[matrix1, matrix2].Simplify();
        }

        public Expr MapScalars(Expr matrix1, Expr matrix2, Func<Expr, Expr, Expr> mappingFunc)
        {
            var (rowsCount, columnsCount) = GetSize(matrix1);

            var rowExprArray = new Expr[rowsCount];

            for (var i = 0; i < rowsCount; i++)
            {
                var rowExpr1 = Mfs.Part[matrix1, i.ToExpr()];
                var rowExpr2 = Mfs.Part[matrix2, i.ToExpr()];
                var scalarExprArray = new Expr[columnsCount];

                for (var j = 0; j < columnsCount; j++)
                {
                    scalarExprArray[j] = mappingFunc(
                        Mfs.Part[rowExpr1, j.ToExpr()],
                        Mfs.Part[rowExpr2, j.ToExpr()]
                    );
                }

                rowExprArray[i] = Mfs.ListExpr(scalarExprArray);
            }

            return Mfs.ListExpr(rowExprArray).Simplify();
        }

        public Expr MapScalars(Expr matrix, Func<Expr, Expr> mappingFunc)
        {
            var (rowsCount, columnsCount) = GetSize(matrix);

            var rowExprArray = new Expr[rowsCount];

            for (var i = 0; i < rowsCount; i++)
            {
                var rowExpr = Mfs.Part[matrix, i.ToExpr()];
                var scalarExprArray = new Expr[columnsCount];

                for (var j = 0; j < columnsCount; j++)
                {
                    scalarExprArray[j] = mappingFunc(
                        Mfs.Part[rowExpr, j.ToExpr()]
                    );
                }

                rowExprArray[i] = Mfs.ListExpr(scalarExprArray);
            }

            return Mfs.ListExpr(rowExprArray).Simplify();
        }

        public Expr NegativeMatrix(Expr matrix)
        {
            return Mfs.Minus[matrix].Simplify();
        }

        public Expr AdjointMatrix(Expr matrix)
        {
            return Mfs.Transpose[matrix].Simplify();
        }

        public Expr InverseMatrix(Expr matrix)
        {
            return Mfs.Inverse[matrix].Simplify();
        }

        public Expr Abs(Expr scalar)
        {
            return ScalarProcessor.Abs(scalar);
        }

        public Expr Sqrt(Expr scalar)
        {
            return ScalarProcessor.Sqrt(scalar);
        }

        public Expr SqrtOfAbs(Expr scalar)
        {
            return ScalarProcessor.SqrtOfAbs(scalar);
        }

        public Expr Exp(Expr scalar)
        {
            return ScalarProcessor.Exp(scalar);
        }

        public Expr Log(Expr scalar)
        {
            return ScalarProcessor.Log(scalar);
        }

        public Expr Log2(Expr scalar)
        {
            return ScalarProcessor.Log2(scalar);
        }

        public Expr Log10(Expr scalar)
        {
            return ScalarProcessor.Log10(scalar);
        }

        public Expr Log(Expr scalar, Expr baseScalar)
        {
            return ScalarProcessor.Log(scalar, baseScalar);
        }

        public Expr Cos(Expr scalar)
        {
            return ScalarProcessor.Cos(scalar);
        }

        public Expr Sin(Expr scalar)
        {
            return ScalarProcessor.Sin(scalar);
        }

        public Expr Tan(Expr scalar)
        {
            return ScalarProcessor.Tan(scalar);
        }

        public Expr ArcCos(Expr scalar)
        {
            return ScalarProcessor.ArcCos(scalar);
        }

        public Expr ArcSin(Expr scalar)
        {
            return ScalarProcessor.ArcSin(scalar);
        }

        public Expr ArcTan(Expr scalar)
        {
            return ScalarProcessor.ArcTan(scalar);
        }

        public Expr ArcTan2(Expr scalarX, Expr scalarY)
        {
            return ScalarProcessor.ArcTan2(scalarX, scalarY);
        }

        public Expr Cosh(Expr scalar)
        {
            return ScalarProcessor.Cosh(scalar);
        }

        public Expr Sinh(Expr scalar)
        {
            return ScalarProcessor.Sinh(scalar);
        }

        public Expr Tanh(Expr scalar)
        {
            return ScalarProcessor.Tanh(scalar);
        }

        public bool IsValid(Expr scalar)
        {
            return ScalarProcessor.IsValid(scalar);
        }

        public bool IsZero(Expr scalar)
        {
            return ScalarProcessor.IsZero(scalar);
        }

        public bool IsZero(Expr scalar, bool nearZeroFlag)
        {
            return ScalarProcessor.IsZero(scalar, nearZeroFlag);
        }

        public bool IsNearZero(Expr scalar)
        {
            return ScalarProcessor.IsNearZero(scalar);
        }

        public bool IsPositive(Expr scalar)
        {
            return ScalarProcessor.IsPositive(scalar);
        }

        public bool IsNegative(Expr scalar)
        {
            return ScalarProcessor.IsNegative(scalar);
        }

        public bool IsNotNearPositive(Expr scalar)
        {
            return ScalarProcessor.IsNotNearPositive(scalar);
        }

        public bool IsNotNearNegative(Expr scalar)
        {
            return ScalarProcessor.IsNotNearNegative(scalar);
        }

        public Expr TextToScalar(string text)
        {
            return ScalarProcessor.TextToScalar(text);
        }

        public Expr IntegerToScalar(int value)
        {
            return ScalarProcessor.IntegerToScalar(value);
        }

        public Expr Float64ToScalar(double value)
        {
            return ScalarProcessor.Float64ToScalar(value);
        }

        public Expr GetRandomScalar(Random randomGenerator, double minValue, double maxValue)
        {
            return ScalarProcessor.GetRandomScalar(randomGenerator, minValue, maxValue);
        }

        public string ToText(Expr scalar)
        {
            return ScalarProcessor.ToText(scalar);
        }

        public Expr InverseAdjointMatrix(Expr matrix)
        {
            return Mfs.Transpose[Mfs.Inverse[matrix]].Simplify();
        }

        public int EigenDecomposition(Expr matrix, out Tuple<Expr, Expr[]>[] realPairs, out Tuple<Expr, Expr[]>[] imagPairs)
        {
            var sysExpr = Mfs.Eigensystem[matrix].Evaluate();

            var realEigenValues = Mfs.Re[sysExpr.Args[0]].Evaluate().Args;
            var imagEigenValues = Mfs.Im[sysExpr.Args[0]].Evaluate().Args;

            var realEigenVectors = Mfs.Re[sysExpr.Args[1]].Evaluate().Args;
            var imagEigenVectors = Mfs.Im[sysExpr.Args[1]].Evaluate().Args;

            var count = realEigenValues.Length;

            realPairs = new Tuple<Expr, Expr[]>[count];
            imagPairs = new Tuple<Expr, Expr[]>[count];

            for (var i = 0; i < count; i++)
            {
                realPairs[i] = new Tuple<Expr, Expr[]>(
                    realEigenValues[i],
                    realEigenVectors[i].Args
                );

                imagPairs[i] = new Tuple<Expr, Expr[]>(
                    imagEigenValues[i],
                    imagEigenVectors[i].Args
                );
            }

            return count;
        }

        public Tuple<Expr, Expr>[] EigenValues(Expr matrix)
        {
            var sysExpr = Mfs.Eigensystem[matrix].Evaluate();

            var realEigenValues = Mfs.Re[sysExpr.Args[0]].Evaluate().Args;
            var imagEigenValues = Mfs.Im[sysExpr.Args[0]].Evaluate().Args;

            var count = realEigenValues.Length;

            var eigenValueTuples = new Tuple<Expr, Expr>[count];

            for (var i = 0; i < count; i++)
            {
                eigenValueTuples[i] = new Tuple<Expr, Expr>(
                    realEigenValues[i],
                    imagEigenValues[i]
                );
            }

            return eigenValueTuples;
        }

        public Tuple<Expr[], Expr[]>[] EigenVectors(Expr matrix)
        {
            var sysExpr = Mfs.Eigenvectors[matrix].Evaluate();

            var count = sysExpr.Length;
            
            var realEigenVectors = Mfs.Re[sysExpr].Evaluate().Args;
            var imagEigenVectors = Mfs.Im[sysExpr].Evaluate().Args;

            var eigenVectorTuples = new Tuple<Expr[], Expr[]>[count];

            for (var i = 0; i < count; i++)
            {
                eigenVectorTuples[i] = new Tuple<Expr[], Expr[]>(
                    realEigenVectors[i].Args,
                    imagEigenVectors[i].Args
                );
            }

            return eigenVectorTuples;
        }
    }
}