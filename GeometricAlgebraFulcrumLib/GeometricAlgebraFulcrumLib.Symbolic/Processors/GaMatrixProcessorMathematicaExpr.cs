using System;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Utils;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Symbolic.Processors
{
    public sealed class GaMatrixProcessorMathematicaExpr
        : IGaMatrixProcessorSymbolic<Expr>
    {
        public static GaMatrixProcessorMathematicaExpr DefaultProcessor { get; }
            = new GaMatrixProcessorMathematicaExpr();


        public IGaScalarProcessor<Expr> ScalarProcessor { get; set; }
            = GaScalarProcessorMathematicaExpr.DefaultProcessor;

        public int RoundingPlaces { get; set; }
            = 13;

        public double ZeroEpsilon 
            => Math.Pow(10, -RoundingPlaces);

        public bool IsNumeric 
            => false;

        public bool IsSymbolic 
            => true;


        private GaMatrixProcessorMathematicaExpr()
        {
        }


        public Expr GetZeroScalar()
        {
            return Expr.INT_ZERO;
        }

        public Expr GetOneScalar()
        {
            return Expr.INT_ONE;
        }

        public Expr GetMinusOneScalar()
        {
            return Expr.INT_MINUSONE;
        }

        public Expr GetPiScalar()
        {
            return MathematicaInterface.DefaultCasConstants.ExprPi;
        }

        public Expr[] GetZeroScalarArray1D(int count)
        {
            var exprArray = new Expr[count];

            for (var i = 0; i < count; i++)
                exprArray[i] = Expr.INT_ZERO;

            return exprArray;
        }

        public Expr[,] GetZeroScalarArray2D(int count)
        {
            var exprArray = new Expr[count, count];

            for (var i = 0; i < count; i++)
            for (var j = 0; j < count; j++)
                exprArray[i, j] = Expr.INT_ZERO;

            return exprArray;
        }

        public Expr[,] GetZeroScalarArray2D(int count1, int count2)
        {
            var exprArray = new Expr[count1, count2];

            for (var i = 0; i < count1; i++)
            for (var j = 0; j < count2; j++)
                exprArray[i, j] = Expr.INT_ZERO;

            return exprArray;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Add(Expr scalar1, Expr scalar2)
        {
            return ScalarProcessor.Add(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Subtract(Expr scalar1, Expr scalar2)
        {
            return ScalarProcessor.Subtract(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Times(Expr scalar1, Expr scalar2)
        {
            return ScalarProcessor.Times(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr NegativeTimes(Expr scalar1, Expr scalar2)
        {
            return ScalarProcessor.NegativeTimes(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Divide(Expr scalar1, Expr scalar2)
        {
            return ScalarProcessor.Divide(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr NegativeDivide(Expr scalar1, Expr scalar2)
        {
            return ScalarProcessor.NegativeDivide(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Positive(Expr scalar)
        {
            return ScalarProcessor.Positive(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Negative(Expr scalar)
        {
            return ScalarProcessor.Negative(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Inverse(Expr scalar)
        {
            return ScalarProcessor.Inverse(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetDenseRowsCount(Expr matrix)
        {
            var dimensionsExpr = Mfs.Dimensions[matrix].Simplify();

            return (int) dimensionsExpr[0].AsInt64();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetDenseColumnsCount(Expr matrix)
        {
            var dimensionsExpr = Mfs.Dimensions[matrix].Simplify();

            return (int) dimensionsExpr[1].AsInt64();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<int> GetDenseSize(Expr matrix)
        {
            var dimensionsExpr = Mfs.Dimensions[matrix].Simplify();

            return new Pair<int>(
                (int) dimensionsExpr[0].AsInt64(),
                (int) dimensionsExpr[1].AsInt64()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr GetScalar(Expr matrix, int rowIndex, int colIndex)
        {
            var rowExpr = Mfs.Part[matrix, rowIndex.ToExpr()];

            return Mfs.Part[rowExpr, colIndex.ToExpr()].Simplify();
        }

        public IGaListEven<Expr> MatrixRowToVector(Expr matrix, int rowIndex)
        {
            var columnsCount = GetDenseColumnsCount(matrix);

            var array = new Expr[columnsCount];
            var rowExpr = Mfs.Part[matrix, rowIndex.ToExpr()];

            for (var j = 0; j < columnsCount; j++) 
                array[j] = Mfs.Part[rowExpr, j.ToExpr()].Simplify();

            return array.CreateEvenListDense();
        }

        public IGaListEven<Expr> MatrixColumnToVector(Expr matrix, int colIndex)
        {
            var rowsCount = GetDenseRowsCount(matrix);

            var array = new Expr[rowsCount];

            var colIndexExpr = colIndex.ToExpr();
            for (var i = 0; i < rowsCount; i++)
            {
                var rowExpr = Mfs.Part[matrix, i.ToExpr()];

                array[i] = Mfs.Part[rowExpr, colIndexExpr].Simplify();
            }

            return array.CreateEvenListDense();
        }

        public IGaGridEven<Expr> MatrixToArray(Expr matrix)
        {
            var (rowsCount, columnsCount) = GetDenseSize(matrix);

            var array = new Expr[rowsCount, columnsCount];

            for (var i = 0; i < rowsCount; i++)
            {
                var rowExpr = Mfs.Part[matrix, i.ToExpr()];

                for (var j = 0; j < columnsCount; j++)
                {
                    array[i, j] = Mfs.Part[rowExpr, j.ToExpr()].Simplify();
                }
            }

            return array.CreateEvenGridDense();
        }

        public Expr CreateMatrix(IGaGridEven<Expr> array)
        {
            return array.ToArray().ArrayToMatrixExpr().Simplify();
        }

        public Expr CreateRowVectorMatrix(IGaListEven<Expr> array)
        {
            var rowExprArray = new Expr[1];
            var scalarExprArray = new Expr[array.GetSparseCount()];
            
            for (var j = 0; j < array.GetSparseCount(); j++)
                scalarExprArray[j] = array.GetValue((ulong) j) ?? Expr.INT_ZERO;

            rowExprArray[0] = Mfs.ListExpr(scalarExprArray);

            return Mfs.ListExpr(rowExprArray).Simplify();
        }

        public Expr CreateRowVectorMatrix(IGaGridEven<Expr> array, int rowIndex)
        {
            var count = array.GetDenseCount2();
            var rowExprArray = new Expr[1];
            var scalarExprArray = new Expr[count];
            
            for (var j = 0; j < count; j++)
                scalarExprArray[j] = 
                    array.GetValue(rowIndex, j) ?? Expr.INT_ZERO;

            rowExprArray[0] = Mfs.ListExpr(scalarExprArray);

            return Mfs.ListExpr(rowExprArray).Simplify();
        }

        public Expr CreateColumnVectorMatrix(IGaListEven<Expr> array)
        {
            var rowExprArray = new Expr[array.GetSparseCount()];
            for (var i = 0; i < array.GetSparseCount(); i++)
            {
                var scalarExprArray = new Expr[1];
                
                scalarExprArray[0] = 
                    array.GetValue(i, Expr.INT_ZERO);

                rowExprArray[i] = Mfs.ListExpr(scalarExprArray);
            }

            return Mfs.ListExpr(rowExprArray).Simplify();
        }

        public Expr CreateColumnVectorMatrix(IGaGridEven<Expr> array, int columnIndex)
        {
            var count = array.GetDenseCount1();
            var rowExprArray = new Expr[count];
            for (var i = 0; i < count; i++)
            {
                var scalarExprArray = new Expr[1];
                
                scalarExprArray[0] = 
                    array.GetValue(i, columnIndex) ?? Expr.INT_ZERO;

                rowExprArray[i] = Mfs.ListExpr(scalarExprArray);
            }

            return Mfs.ListExpr(rowExprArray).Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr CreateZeroMatrix(int size)
        {
            var sizeExpr = size.ToExpr();

            return Mfs.ConstantArray[
                Expr.INT_ZERO,
                Mfs.List[sizeExpr, sizeExpr]
            ].Evaluate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr CreateZeroMatrix(int rowsCount, int columnsCount)
        {
            return Mfs.ConstantArray[
                Expr.INT_ZERO,
                Mfs.List[rowsCount.ToExpr(), columnsCount.ToExpr()]
            ].Evaluate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr CreateUnityMatrix(int size)
        {
            return Mfs.IdentityMatrix[size.ToExpr()].Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr AddMatrixScalar(Expr matrix, Expr scalar)
        {
            return Mfs.Plus[matrix, scalar].Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr AddScalarMatrix(Expr scalar, Expr matrix)
        {
            return Mfs.Plus[scalar, matrix].Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr AddMatrices(Expr matrix1, Expr matrix2)
        {
            return Mfs.Plus[matrix1, matrix2].Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr SubtractMatrixScalar(Expr matrix, Expr scalar)
        {
            return Mfs.Plus[matrix, scalar].Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr SubtractScalarMatrix(Expr scalar, Expr matrix)
        {
            return Mfs.Plus[scalar, matrix].Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr SubtractMatrices(Expr matrix1, Expr matrix2)
        {
            return Mfs.Plus[matrix1, matrix2].Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr TimesScalarMatrix(Expr scalar, Expr matrix)
        {
            return Mfs.Times[scalar, matrix].Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr TimesMatrixScalar(Expr matrix, Expr scalar)
        {
            return Mfs.Times[matrix, scalar].Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr TimesMatrixItems(Expr matrix1, Expr matrix2)
        {
            return Mfs.Times[matrix1, matrix2].Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr DivideMatrixItems(Expr matrix1, Expr matrix2)
        {
            return Mfs.Divide[matrix1, matrix2].Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr DivideMatrixScalar(Expr matrix, Expr scalar)
        {
            return Mfs.Divide[matrix, scalar].Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr DivideScalarMatrix(Expr scalar, Expr matrix)
        {
            return Mfs.Divide[scalar, matrix].Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr TimesMatrices(Expr matrix1, Expr matrix2)
        {
            return Mfs.Dot[matrix1, matrix2].Simplify();
        }

        public Expr MapMatrixItems(Expr matrix, Func<int, int, Expr, Expr> mappingFunc)
        {
            var (rowsCount, columnsCount) = GetDenseSize(matrix);

            var rowExprArray = new Expr[rowsCount];

            for (var i = 0; i < rowsCount; i++)
            {
                var rowExpr = Mfs.Part[matrix, i.ToExpr()];
                var scalarExprArray = new Expr[columnsCount];

                for (var j = 0; j < columnsCount; j++)
                {
                    scalarExprArray[j] = mappingFunc(
                        i, 
                        j, 
                        Mfs.Part[rowExpr, j.ToExpr()]
                    );
                }

                rowExprArray[i] = Mfs.ListExpr(scalarExprArray);
            }

            return Mfs.ListExpr(rowExprArray).Simplify();
        }

        public Expr MapMatrixItems(Expr matrix1, Expr matrix2, Func<Expr, Expr, Expr> mappingFunc)
        {
            var (rowsCount, columnsCount) = GetDenseSize(matrix1);

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

        public Expr MapMatrixItems(Expr matrix, Func<Expr, Expr> mappingFunc)
        {
            var (rowsCount, columnsCount) = GetDenseSize(matrix);

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr MatrixNegative(Expr matrix)
        {
            return Mfs.Minus[matrix].Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr MatrixTranspose(Expr matrix)
        {
            return Mfs.Transpose[matrix].Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr MatrixInverse(Expr matrix)
        {
            return Mfs.Inverse[matrix].Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Abs(Expr scalar)
        {
            return ScalarProcessor.Abs(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Sqrt(Expr scalar)
        {
            return ScalarProcessor.Sqrt(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr SqrtOfAbs(Expr scalar)
        {
            return ScalarProcessor.SqrtOfAbs(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Exp(Expr scalar)
        {
            return ScalarProcessor.Exp(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Log(Expr scalar)
        {
            return ScalarProcessor.Log(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Log2(Expr scalar)
        {
            return ScalarProcessor.Log2(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Log10(Expr scalar)
        {
            return ScalarProcessor.Log10(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Log(Expr scalar, Expr baseScalar)
        {
            return ScalarProcessor.Log(scalar, baseScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Cos(Expr scalar)
        {
            return ScalarProcessor.Cos(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Sin(Expr scalar)
        {
            return ScalarProcessor.Sin(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Tan(Expr scalar)
        {
            return ScalarProcessor.Tan(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr ArcCos(Expr scalar)
        {
            return ScalarProcessor.ArcCos(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr ArcSin(Expr scalar)
        {
            return ScalarProcessor.ArcSin(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr ArcTan(Expr scalar)
        {
            return ScalarProcessor.ArcTan(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr ArcTan2(Expr scalarX, Expr scalarY)
        {
            return ScalarProcessor.ArcTan2(scalarX, scalarY);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Cosh(Expr scalar)
        {
            return ScalarProcessor.Cosh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Sinh(Expr scalar)
        {
            return ScalarProcessor.Sinh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Tanh(Expr scalar)
        {
            return ScalarProcessor.Tanh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(Expr scalar)
        {
            return ScalarProcessor.IsValid(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(Expr scalar)
        {
            return ScalarProcessor.IsZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(Expr scalar, bool nearZeroFlag)
        {
            return ScalarProcessor.IsZero(scalar, nearZeroFlag);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(Expr scalar)
        {
            return ScalarProcessor.IsNearZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(Expr scalar)
        {
            return !ScalarProcessor.IsZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(Expr scalar, bool nearZeroFlag)
        {
            return !ScalarProcessor.IsZero(scalar, nearZeroFlag);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearZero(Expr scalar)
        {
            return !ScalarProcessor.IsNearZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(Expr scalar)
        {
            return ScalarProcessor.IsPositive(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(Expr scalar)
        {
            return ScalarProcessor.IsNegative(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotPositive(Expr scalar)
        {
            return !ScalarProcessor.IsPositive(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNegative(Expr scalar)
        {
            return !ScalarProcessor.IsNegative(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(Expr scalar)
        {
            return ScalarProcessor.IsNotNearPositive(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(Expr scalar)
        {
            return ScalarProcessor.IsNotNearNegative(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr TextToScalar(string text)
        {
            return ScalarProcessor.TextToScalar(text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr IntegerToScalar(int value)
        {
            return ScalarProcessor.IntegerToScalar(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr Float64ToScalar(double value)
        {
            return ScalarProcessor.Float64ToScalar(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr GetRandomScalar(Random randomGenerator, double minValue, double maxValue)
        {
            return ScalarProcessor.GetRandomScalar(randomGenerator, minValue, maxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToText(Expr scalar)
        {
            return ScalarProcessor.ToText(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr MatrixInverseTranspose(Expr matrix)
        {
            return Mfs.Transpose[Mfs.Inverse[matrix]].Simplify();
        }

        public int MatrixEigenDecomposition(Expr matrix, out Tuple<Expr, IGaListEven<Expr>>[] realPairs, out Tuple<Expr, IGaListEven<Expr>>[] imagPairs)
        {
            var sysExpr = Mfs.Eigensystem[matrix].Evaluate();

            var realEigenValues = Mfs.Re[sysExpr.Args[0]].Evaluate().Args;
            var imagEigenValues = Mfs.Im[sysExpr.Args[0]].Evaluate().Args;

            var realEigenVectors = Mfs.Re[sysExpr.Args[1]].Evaluate().Args;
            var imagEigenVectors = Mfs.Im[sysExpr.Args[1]].Evaluate().Args;

            var count = realEigenValues.Length;

            realPairs = new Tuple<Expr, IGaListEven<Expr>>[count];
            imagPairs = new Tuple<Expr, IGaListEven<Expr>>[count];

            for (var i = 0; i < count; i++)
            {
                realPairs[i] = new Tuple<Expr, IGaListEven<Expr>>(
                    realEigenValues[i],
                    realEigenVectors[i].Args.CreateEvenListDenseArray()
                );

                imagPairs[i] = new Tuple<Expr, IGaListEven<Expr>>(
                    imagEigenValues[i],
                    imagEigenVectors[i].Args.CreateEvenListDenseArray()
                );
            }

            return count;
        }

        public Pair<Expr>[] MatrixEigenValues(Expr matrix)
        {
            var sysExpr = Mfs.Eigensystem[matrix].Evaluate();

            var realEigenValues = Mfs.Re[sysExpr.Args[0]].Evaluate().Args;
            var imagEigenValues = Mfs.Im[sysExpr.Args[0]].Evaluate().Args;

            var count = realEigenValues.Length;

            var eigenValueTuples = new Pair<Expr>[count];

            for (var i = 0; i < count; i++)
            {
                eigenValueTuples[i] = new Pair<Expr>(
                    realEigenValues[i],
                    imagEigenValues[i]
                );
            }

            return eigenValueTuples;
        }

        public Pair<IGaListEven<Expr>>[] MatrixEigenVectors(Expr matrix)
        {
            var sysExpr = Mfs.Eigenvectors[matrix].Evaluate();

            var count = sysExpr.Length;
            
            var realEigenVectors = Mfs.Re[sysExpr].Evaluate().Args;
            var imagEigenVectors = Mfs.Im[sysExpr].Evaluate().Args;

            var eigenVectorTuples = new Pair<IGaListEven<Expr>>[count];

            for (var i = 0; i < count; i++)
            {
                eigenVectorTuples[i] = new Pair<IGaListEven<Expr>>(
                    realEigenVectors[i].Args.CreateEvenListDenseArray(),
                    imagEigenVectors[i].Args.CreateEvenListDenseArray()
                );
            }

            return eigenVectorTuples;
        }
    }
}