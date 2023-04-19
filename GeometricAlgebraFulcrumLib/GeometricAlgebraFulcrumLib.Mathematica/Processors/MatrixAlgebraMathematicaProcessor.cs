using System;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Processors.MatrixAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.Processors
{
    public sealed class MatrixAlgebraMathematicaProcessor
        : IMatrixAlgebraSymbolicProcessor<Expr>
    {
        public static MatrixAlgebraMathematicaProcessor DefaultProcessor { get; }
            = new MatrixAlgebraMathematicaProcessor();


        public IScalarProcessor<Expr> ScalarProcessor { get; set; }
            = ScalarProcessorExpr.DefaultProcessor;

        public int RoundingPlaces { get; set; }
            = 13;

        public double ZeroEpsilon 
            => Math.Pow(10, -RoundingPlaces);

        public bool IsNumeric 
            => false;

        public bool IsSymbolic 
            => true;
        
        public Expr ScalarZero 
            => Expr.INT_ZERO;

        public Expr ScalarOne 
            => Expr.INT_ONE;

        public Expr ScalarMinusOne 
            => Expr.INT_MINUSONE;

        public Expr ScalarTwo { get; } 
            = 2.ToExpr();

        public Expr ScalarMinusTwo { get; } 
            = (-2).ToExpr();

        public Expr ScalarTen { get; } 
            = 10.ToExpr();

        public Expr ScalarMinusTen { get; } 
            = (-10).ToExpr();

        public Expr ScalarPi 
            => MathematicaInterface.DefaultCasConstants.ExprPi;

        public Expr ScalarTwoPi { get; }
            = MathematicaInterface.DefaultCas["2 * Pi"];

        public Expr ScalarPiOver2 { get; }
            = MathematicaInterface.DefaultCas["Pi / 2"];

        public Expr ScalarE 
            => MathematicaInterface.DefaultCasConstants.ExprE;

        public Expr ScalarDegreeToRadian { get; }
            = MathematicaInterface.DefaultCas["Pi / 180"];

        public Expr ScalarRadianToDegree { get; }
            = MathematicaInterface.DefaultCas["180 / Pi"];


        private MatrixAlgebraMathematicaProcessor()
        {
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
        public Expr Times(IntegerSign sign, Expr scalar)
        {
            if (sign.IsZero) return ScalarZero;

            return sign.IsPositive
                ? scalar 
                : Negative(scalar);
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
        public Expr Sign(Expr scalar)
        {
            return ScalarProcessor.Sign(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr UnitStep(Expr scalar)
        {
            return ScalarProcessor.UnitStep(scalar);
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
            var rowExpr = Mfs.Part[matrix, (rowIndex + 1).ToExpr()];

            return Mfs.Part[rowExpr, (colIndex + 1).ToExpr()].Simplify();
        }

        public ILinVectorStorage<Expr> MatrixRowToVector(Expr matrix, int rowIndex)
        {
            var columnsCount = GetDenseColumnsCount(matrix);

            var array = new Expr[columnsCount];
            var rowExpr = Mfs.Part[matrix, rowIndex.ToExpr()];

            for (var j = 0; j < columnsCount; j++) 
                array[j] = Mfs.Part[rowExpr, j.ToExpr()].Simplify();

            return array.CreateLinVectorDenseStorage();
        }

        public ILinVectorStorage<Expr> MatrixColumnToVector(Expr matrix, int colIndex)
        {
            var rowsCount = GetDenseRowsCount(matrix);

            var array = new Expr[rowsCount];

            var colIndexExpr = colIndex.ToExpr();
            for (var i = 0; i < rowsCount; i++)
            {
                var rowExpr = Mfs.Part[matrix, i.ToExpr()];

                array[i] = Mfs.Part[rowExpr, colIndexExpr].Simplify();
            }

            return array.CreateLinVectorDenseStorage();
        }

        public ILinMatrixStorage<Expr> MatrixToArray(Expr matrix)
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

            return array.CreateLinMatrixDenseStorage();
        }

        public Expr CreateMatrix(ILinMatrixStorage<Expr> array)
        {
            return array.ToArray().ToMatrixExpr().Simplify();
        }

        public Expr CreateRowVectorMatrix(ILinVectorStorage<Expr> array)
        {
            var rowExprArray = new Expr[1];
            var scalarExprArray = new Expr[array.GetSparseCount()];
            
            for (var j = 0; j < array.GetSparseCount(); j++)
                scalarExprArray[j] = array.GetScalar((ulong) j) ?? Expr.INT_ZERO;

            rowExprArray[0] = Mfs.ListExpr(scalarExprArray);

            return Mfs.ListExpr(rowExprArray).Simplify();
        }

        public Expr CreateRowVectorMatrix(ILinMatrixStorage<Expr> array, int rowIndex)
        {
            var count = array.GetDenseCount2();
            var rowExprArray = new Expr[1];
            var scalarExprArray = new Expr[count];
            
            for (var j = 0; j < count; j++)
                scalarExprArray[j] = 
                    array.GetScalar(rowIndex, j, Expr.INT_ZERO);

            rowExprArray[0] = Mfs.ListExpr(scalarExprArray);

            return Mfs.ListExpr(rowExprArray).Simplify();
        }

        public Expr CreateColumnVectorMatrix(ILinVectorStorage<Expr> array)
        {
            var rowExprArray = new Expr[array.GetSparseCount()];
            for (var i = 0; i < array.GetSparseCount(); i++)
            {
                var scalarExprArray = new Expr[1];
                
                scalarExprArray[0] = 
                    array.GetScalar(i, Expr.INT_ZERO);

                rowExprArray[i] = Mfs.ListExpr(scalarExprArray);
            }

            return Mfs.ListExpr(rowExprArray).Simplify();
        }

        public Expr CreateColumnVectorMatrix(ILinMatrixStorage<Expr> array, int columnIndex)
        {
            var count = array.GetDenseCount1();
            var rowExprArray = new Expr[count];
            for (var i = 0; i < count; i++)
            {
                var scalarExprArray = new Expr[1];
                
                scalarExprArray[0] = 
                    array.GetScalar(i, columnIndex, Expr.INT_ZERO);

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
        public Expr Power(Expr baseScalar, Expr scalar)
        {
            return ScalarProcessor.Power(baseScalar, scalar);
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
        public Expr LogE(Expr scalar)
        {
            return ScalarProcessor.LogE(scalar);
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
        public Expr Log(Expr baseScalar, Expr scalar)
        {
            return ScalarProcessor.Log(baseScalar, scalar);
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
        public Expr Sinc(Expr scalar)
        {
            return ScalarProcessor.Sinc(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(Expr scalar)
        {
            return ScalarProcessor.IsValid(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsFiniteNumber(Expr scalar)
        {
            return ScalarProcessor.IsFiniteNumber(scalar);
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
        public Expr GetScalarFromText(string text)
        {
            return ScalarProcessor.GetScalarFromText(text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr GetScalarFromNumber(int value)
        {
            return ScalarProcessor.GetScalarFromNumber(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr GetScalarFromNumber(uint value)
        {
            return ScalarProcessor.GetScalarFromNumber(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr GetScalarFromNumber(long value)
        {
            return ScalarProcessor.GetScalarFromNumber(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr GetScalarFromNumber(ulong value)
        {
            return ScalarProcessor.GetScalarFromNumber(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr GetScalarFromNumber(float value)
        {
            return ScalarProcessor.GetScalarFromNumber(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr GetScalarFromNumber(double value)
        {
            return ScalarProcessor.GetScalarFromNumber(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr GetScalarFromRational(long numerator, long denominator)
        {
            return ScalarProcessor.GetScalarFromRational(numerator, denominator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expr GetScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
        {
            return ScalarProcessor.GetScalarFromRandom(randomGenerator, minValue, maxValue);
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

        public int MatrixEigenDecomposition(Expr matrix, out Tuple<Expr, ILinVectorStorage<Expr>>[] realPairs, out Tuple<Expr, ILinVectorStorage<Expr>>[] imagPairs)
        {
            var sysExpr = Mfs.Eigensystem[matrix].Evaluate();

            var realEigenValues = Mfs.Re[sysExpr.Args[0]].Evaluate().Args;
            var imagEigenValues = Mfs.Im[sysExpr.Args[0]].Evaluate().Args;

            var realEigenVectors = Mfs.Re[sysExpr.Args[1]].Evaluate().Args;
            var imagEigenVectors = Mfs.Im[sysExpr.Args[1]].Evaluate().Args;

            var count = realEigenValues.Length;

            realPairs = new Tuple<Expr, ILinVectorStorage<Expr>>[count];
            imagPairs = new Tuple<Expr, ILinVectorStorage<Expr>>[count];

            for (var i = 0; i < count; i++)
            {
                realPairs[i] = new Tuple<Expr, ILinVectorStorage<Expr>>(
                    realEigenValues[i],
                    realEigenVectors[i].Args.CreateLinVectorArrayStorage()
                );

                imagPairs[i] = new Tuple<Expr, ILinVectorStorage<Expr>>(
                    imagEigenValues[i],
                    imagEigenVectors[i].Args.CreateLinVectorArrayStorage()
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

        public Pair<ILinVectorStorage<Expr>>[] MatrixEigenVectors(Expr matrix)
        {
            var sysExpr = Mfs.Eigenvectors[matrix].Evaluate();

            var count = sysExpr.Length;
            
            var realEigenVectors = Mfs.Re[sysExpr].Evaluate().Args;
            var imagEigenVectors = Mfs.Im[sysExpr].Evaluate().Args;

            var eigenVectorTuples = new Pair<ILinVectorStorage<Expr>>[count];

            for (var i = 0; i < count; i++)
            {
                eigenVectorTuples[i] = new Pair<ILinVectorStorage<Expr>>(
                    realEigenVectors[i].Args.CreateLinVectorArrayStorage(),
                    imagEigenVectors[i].Args.CreateLinVectorArrayStorage()
                );
            }

            return eigenVectorTuples;
        }
    }
}