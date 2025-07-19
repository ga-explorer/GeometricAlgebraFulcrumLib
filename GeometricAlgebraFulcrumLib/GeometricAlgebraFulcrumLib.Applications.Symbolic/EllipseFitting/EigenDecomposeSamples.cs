using Avalonia;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.EllipseFitting
{
    public static class EigenDecomposeSamples
    {
        public static void Example3X3()
        {
            var matrix = new double[,]
            {
                { 5, 1, 2 },
                { 1, 6, 1 },
                { 2, 1, 7 }
            };

            var eig = new JacobiSymmetricEigenDecomposer(matrix);
            eig.EigenDecompose();

            Console.WriteLine(eig.ToString());
            Console.WriteLine();

            Console.WriteLine(DenseMatrix.OfArray(matrix).Evd().EigenValues.ToString());
            Console.WriteLine();
        }

        public static void Example4X4()
        {
            var matrix = new double[,]
            {
                { 4, 1, 2, 1 },
                { 1, 5, 1, 2 },
                { 2, 1, 6, 1 },
                { 1, 2, 1, 7 }
            };

            var eig = new JacobiSymmetricEigenDecomposer(matrix);
            eig.EigenDecompose();

            Console.WriteLine(eig.ToString());
            Console.WriteLine();

            Console.WriteLine(DenseMatrix.OfArray(matrix).Evd().EigenValues[0].ToString("G"));
            Console.WriteLine();
        }

        private static List<double[,]> CloneMatrix(double[,] matrix, int count)
        {
            var m = matrix.GetLength(0);
            var n = matrix.GetLength(1);
            var matrixList = new List<double[,]>(count);

            for (var k = 0; k < count; k++)
            {
                var matrixClone = new double[m, n];

                for (var i = 0; i < m; i++)
                for (var j = 0; j < n; j++)
                    matrixClone[i, j] = matrix[i, j];

                matrixList.Add(matrixClone);
            }

            return matrixList;
        }

        private static List<double[,]> GetRandomMatrices(int size, int count)
        {
            var randGen = new Random(10);

            var matrixList = new List<double[,]>(count);

            for (var k = 0; k < count; k++)
            {
                var matrix = new double[size, size];

                for (var i = 0; i < size; i++)
                {
                    for (var j = 0; j <= i; j++)
                    {
                        var s = randGen.Next(21);
                        matrix[i, j] = s;
                        matrix[j, i] = s;
                    }
                }

                matrixList.Add(matrix);
            }

            return matrixList;
        }

        public static void Example2()
        {
            const int size = 6;
            const int n = 1000000;

            //var matrix = new double[,]
            //{
            //    { 4, 1, 2, 1 },
            //    { 1, 5, 1, 2 },
            //    { 2, 1, 6, 1 },
            //    { 1, 2, 1, 7 }
            //};

            var matrixList = GetRandomMatrices(size, n);
            var t1 = DateTime.Now;
            for (var k = 0; k < n; k++)
            {
                //var eig = new JacobiEigen4X4Generated(matrixList[k]);
                var eig = new JacobiSymmetricEigenDecomposer(matrixList[k]);

                eig.EigenDecompose();
            }
            Console.WriteLine(DateTime.Now - t1);

            //matrixList = GetRandomMatrices(4, n);
            //t1 = DateTime.Now;
            //for (var k = 0; k < n; k++)
            //{
            //    var eig = new JacobiEigen4X4Generated(matrixList[k]);
            //    //var eig = new JacobiSymmetricEigenDecomposer(matrixList[k]);

            //    eig.EigenDecompose();
            //}
            //Console.WriteLine(DateTime.Now - t1);

            matrixList = GetRandomMatrices(size, n);
            t1 = DateTime.Now;
            for (var k = 0; k < n; k++)
            {
                var evd = DenseMatrix.OfArray(matrixList[k]).Evd(Symmetricity.Symmetric);

                var v = evd.EigenValues;
                var v2 = evd.EigenVectors;
            }

            Console.WriteLine(DateTime.Now - t1);
        }
    }
}
