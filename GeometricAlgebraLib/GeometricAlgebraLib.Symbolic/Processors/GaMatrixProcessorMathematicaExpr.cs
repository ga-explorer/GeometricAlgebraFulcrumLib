using System;
using GeometricAlgebraLib.Processing.Matrices;
using GeometricAlgebraLib.Processing.Scalars;
using GeometricAlgebraLib.Symbolic.Mathematica;
using GeometricAlgebraLib.Symbolic.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraLib.Symbolic.Processors
{
    public sealed class GaMatrixProcessorMathematicaExpr
        : IGaMatrixProcessor<Expr, Expr>
    {
        public static GaMatrixProcessorMathematicaExpr DefaultProcessor { get; }
            = new GaMatrixProcessorMathematicaExpr();


        public IGaScalarProcessor<Expr> ScalarProcessor 
            => GaScalarProcessorMathematicaExpr.DefaultProcessor;

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
                    scalarExprArray[j] = 
                        array[i, j] 
                        ?? ScalarProcessor.ZeroScalar;

                rowExprArray[i] = Mfs.ListExpr(scalarExprArray);
            }

            return Mfs.ListExpr(rowExprArray).Simplify();
        }

        public Expr CreateRowVectorMatrix(Expr[] array)
        {
            var rowExprArray = new Expr[1];
            var scalarExprArray = new Expr[array.Length];
            
            for (var j = 0; j < array.Length; j++)
                scalarExprArray[j] = 
                    array[j] ?? ScalarProcessor.ZeroScalar;

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
                    array[rowIndex, j] ?? ScalarProcessor.ZeroScalar;

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
                    array[i] ?? ScalarProcessor.ZeroScalar;

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
                    array[i, columnIndex] ?? ScalarProcessor.ZeroScalar;

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

        public Expr Negative(Expr matrix)
        {
            return Mfs.Minus[matrix].Simplify();
        }

        public Expr Adjoint(Expr matrix)
        {
            return Mfs.Transpose[matrix].Simplify();
        }

        public Expr Inverse(Expr matrix)
        {
            return Mfs.Inverse[matrix].Simplify();
        }

        public Expr InverseAdjoint(Expr matrix)
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