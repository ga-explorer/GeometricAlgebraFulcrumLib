using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Geometry;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Symbolic.Applications.GaPoT
{
    public static class GaPoTUtils
    {
        public static T[,] CreateClarkeArray3D<T>(IGaScalarProcessor<T> scalarProcessor)
        {
            var clarkeArray = new T[3, 3];

            clarkeArray[0, 0] = scalarProcessor.OneScalar;
            clarkeArray[0, 1] = scalarProcessor.Divide(-1, 2);
            clarkeArray[0, 2] = scalarProcessor.Divide(-1, 2);

            clarkeArray[1, 0] = scalarProcessor.ZeroScalar;
            clarkeArray[1, 1] = scalarProcessor.Divide(scalarProcessor.Sqrt(3), 2);
            clarkeArray[1, 2] = scalarProcessor.Divide(scalarProcessor.Sqrt(3), -2);

            clarkeArray[2, 0] = scalarProcessor.Divide(1, scalarProcessor.Sqrt(2));
            clarkeArray[2, 1] = scalarProcessor.Divide(1, scalarProcessor.Sqrt(2));
            clarkeArray[2, 2] = scalarProcessor.Divide(1, scalarProcessor.Sqrt(2));

            var scalar = scalarProcessor.Sqrt(
                scalarProcessor.Divide(2, 3)
            );

            return scalarProcessor.Times(scalar, clarkeArray);
        }

        public static T[,] CreateClarkeArray5D<T>(IGaScalarProcessor<T> scalarProcessor)
        {
            var clarkeArray = new T[5, 5];

            clarkeArray[0, 0] = scalarProcessor.OneScalar;
            clarkeArray[0, 1] = scalarProcessor.CosPiRatio(2, 5);
            clarkeArray[0, 2] = scalarProcessor.CosPiRatio(4, 5);
            clarkeArray[0, 3] = scalarProcessor.CosPiRatio(6, 5);
            clarkeArray[0, 4] = scalarProcessor.CosPiRatio(8, 5);

            clarkeArray[1, 0] = scalarProcessor.ZeroScalar;
            clarkeArray[1, 1] = scalarProcessor.SinPiRatio(2, 5);
            clarkeArray[1, 2] = scalarProcessor.SinPiRatio(4, 5);
            clarkeArray[1, 3] = scalarProcessor.SinPiRatio(6, 5);
            clarkeArray[1, 4] = scalarProcessor.SinPiRatio(8, 5);

            clarkeArray[2, 0] = scalarProcessor.OneScalar;
            clarkeArray[2, 1] = scalarProcessor.CosPiRatio(4, 5);
            clarkeArray[2, 2] = scalarProcessor.CosPiRatio(8, 5);
            clarkeArray[2, 3] = scalarProcessor.CosPiRatio(12, 5);
            clarkeArray[2, 4] = scalarProcessor.CosPiRatio(16, 5);

            clarkeArray[3, 0] = scalarProcessor.ZeroScalar;
            clarkeArray[3, 1] = scalarProcessor.SinPiRatio(4, 5);
            clarkeArray[3, 2] = scalarProcessor.SinPiRatio(8, 5);
            clarkeArray[3, 3] = scalarProcessor.SinPiRatio(12, 5);
            clarkeArray[3, 4] = scalarProcessor.SinPiRatio(16, 5);

            clarkeArray[4, 0] = scalarProcessor.Divide(1, scalarProcessor.Sqrt(2));
            clarkeArray[4, 1] = scalarProcessor.Divide(1, scalarProcessor.Sqrt(2));
            clarkeArray[4, 2] = scalarProcessor.Divide(1, scalarProcessor.Sqrt(2));
            clarkeArray[4, 3] = scalarProcessor.Divide(1, scalarProcessor.Sqrt(2));
            clarkeArray[4, 4] = scalarProcessor.Divide(1, scalarProcessor.Sqrt(2));

            var scalar = scalarProcessor.Sqrt(
                scalarProcessor.Divide(2, 5)
            );

            return scalarProcessor.Times(scalar, clarkeArray);
        }

        public static T[,] CreateClarkeArray6D<T>(IGaScalarProcessor<T> scalarProcessor)
        {
            var clarkeArray = new T[6, 6];

            clarkeArray[0, 0] = scalarProcessor.OneScalar;
            clarkeArray[0, 1] = scalarProcessor.Rational(-1, 2);
            clarkeArray[0, 2] = scalarProcessor.Rational(-1, 2);
            clarkeArray[0, 3] = scalarProcessor.Rational(1, 2);
            clarkeArray[0, 4] = scalarProcessor.MinusOneScalar;
            clarkeArray[0, 5] = scalarProcessor.Rational(1, 2);

            clarkeArray[1, 0] = scalarProcessor.ZeroScalar;
            clarkeArray[1, 1] = scalarProcessor.Divide(scalarProcessor.Sqrt(3), 2);
            clarkeArray[1, 2] = scalarProcessor.NegativeDivide(scalarProcessor.Sqrt(3), 2);
            clarkeArray[1, 3] = scalarProcessor.Divide(scalarProcessor.Sqrt(3), 2);
            clarkeArray[1, 4] = scalarProcessor.ZeroScalar;
            clarkeArray[1, 5] = scalarProcessor.NegativeDivide(scalarProcessor.Sqrt(3), 2);

            clarkeArray[2, 0] = scalarProcessor.OneScalar;
            clarkeArray[2, 1] = scalarProcessor.Rational(-1, 2);
            clarkeArray[2, 2] = scalarProcessor.Rational(-1, 2);
            clarkeArray[2, 3] = scalarProcessor.Rational(-1, 2);
            clarkeArray[2, 4] = scalarProcessor.OneScalar;
            clarkeArray[2, 5] = scalarProcessor.Rational(-1, 2);

            clarkeArray[3, 0] = scalarProcessor.ZeroScalar;
            clarkeArray[3, 1] = scalarProcessor.NegativeDivide(scalarProcessor.Sqrt(3), 2);
            clarkeArray[3, 2] = scalarProcessor.Divide(scalarProcessor.Sqrt(3), 2);
            clarkeArray[3, 3] = scalarProcessor.Divide(scalarProcessor.Sqrt(3), 2);
            clarkeArray[3, 4] = scalarProcessor.ZeroScalar;
            clarkeArray[3, 5] = scalarProcessor.NegativeDivide(scalarProcessor.Sqrt(3), 2);

            clarkeArray[4, 0] = scalarProcessor.SqrtRational(1, 2);
            clarkeArray[4, 1] = scalarProcessor.SqrtRational(1, 2);
            clarkeArray[4, 2] = scalarProcessor.SqrtRational(1, 2);
            clarkeArray[4, 3] = scalarProcessor.SqrtRational(1, 2);
            clarkeArray[4, 4] = scalarProcessor.SqrtRational(1, 2);
            clarkeArray[4, 5] = scalarProcessor.SqrtRational(1, 2);

            clarkeArray[5, 0] = scalarProcessor.SqrtRational(1, 2);
            clarkeArray[5, 1] = scalarProcessor.SqrtRational(1, 2);
            clarkeArray[5, 2] = scalarProcessor.SqrtRational(1, 2);
            clarkeArray[5, 3] = scalarProcessor.Negative(scalarProcessor.SqrtRational(1, 2));
            clarkeArray[5, 4] = scalarProcessor.Negative(scalarProcessor.SqrtRational(1, 2));
            clarkeArray[5, 5] = scalarProcessor.Negative(scalarProcessor.SqrtRational(1, 2));

            var scalar = scalarProcessor.SqrtRational(2,6);

            return scalarProcessor.Times(scalar, clarkeArray);
        }

        /// <summary>
        /// See the paper "Generalized Clarke Components for Polyphase Networks", 1969
        /// </summary>
        /// <param name="scalarProcessor"></param>
        /// <param name="vectorsCount"></param>
        /// <returns></returns>
        private static T[,] CreateClarkeArrayOdd<T>(IGaScalarProcessor<T> scalarProcessor, int vectorsCount)
        {
            var clarkeArray = new T[vectorsCount, vectorsCount];

            var m = vectorsCount;
            var s = scalarProcessor.Sqrt(
                scalarProcessor.Divide(
                    scalarProcessor.IntegerToScalar(2),
                    scalarProcessor.IntegerToScalar(m)
                )
            ); //$"Sqrt[2 / {m}]";

            // m is odd, fill all rows except the last
            var n = (m - 1) / 2;
            for (var k = 0; k < n; k++)
            {
                var rowIndex1 = 2 * k;
                var rowIndex2 = 2 * k + 1;

                clarkeArray[rowIndex1, 0] = s;
                clarkeArray[rowIndex2, 0] = scalarProcessor.ZeroScalar;
                
                for (var colIndex = 1; colIndex < m; colIndex++)
                {
                    var angle =
                        scalarProcessor.PiRatio(
                            2 * (k + 1) * colIndex, 
                            m
                        ); // $"2 * Pi * {k + 1} * {i} / {m}";

                    var cosAngle = scalarProcessor.Times(
                        s,
                        scalarProcessor.Cos(angle)
                    ); // $"{s} * Cos[{angle}]";
                    
                    var sinAngle = scalarProcessor.Times(
                        s,
                        scalarProcessor.Sin(angle)
                    ); // $"{s} * Sin[{angle}]";
                    
                    clarkeArray[rowIndex1, colIndex] = cosAngle;
                    clarkeArray[rowIndex2, colIndex] = sinAngle;
                }
            }

            //Fill the last column
            var v = scalarProcessor.Divide(
                scalarProcessor.OneScalar,
                scalarProcessor.Sqrt(m)
            ); // $"1 / Sqrt[{m}]";

            for (var colIndex = 0; colIndex < m; colIndex++) 
                clarkeArray[m - 1, colIndex] = v;

            return clarkeArray;
        }

        /// <summary>
        /// See the paper "Generalized Clarke Components for Polyphase Networks", 1969
        /// </summary>
        /// <param name="scalarProcessor"></param>
        /// <param name="vectorsCount"></param>
        /// <returns></returns>
        private static T[,] CreateClarkeArrayEven<T>(IGaScalarProcessor<T> scalarProcessor, int vectorsCount)
        {
            var clarkeArray = new T[vectorsCount, vectorsCount];

            var m = vectorsCount;
            var s = scalarProcessor.SqrtRational(2,m); //$"Sqrt[2 / {m}]";

            //m is even, fill all rows except the last two
            var n = (m - 1) / 2;
            for (var k = 0; k < n; k++)
            {
                var rowIndex1 = 2 * k;
                var rowIndex2 = 2 * k + 1;
                
                clarkeArray[rowIndex1, 0] = s;
                clarkeArray[rowIndex2, 0] = scalarProcessor.ZeroScalar;
                
                for (var colIndex = 1; colIndex < m; colIndex++)
                {
                    var angle =
                        scalarProcessor.PiRatio(
                            2 * (k + 1) * colIndex, 
                            m
                        ); // $"2 * Pi * {k + 1} * {i} / {m}";

                    var cosAngle = scalarProcessor.Times(
                        s,
                        scalarProcessor.Cos(angle)
                    ); // $"{s} * Cos[{angle}]";
                    
                    var sinAngle = scalarProcessor.Times(
                        s,
                        scalarProcessor.Sin(angle)
                    ); // $"{s} * Sin[{angle}]";
                    
                    clarkeArray[rowIndex1, colIndex] = cosAngle;
                    clarkeArray[rowIndex2, colIndex] = sinAngle;
                }
            }

            //Fill the last two rows
            var v0 = scalarProcessor.Divide(
                scalarProcessor.OneScalar,
                scalarProcessor.Sqrt(m)
            ); // $"1 / Sqrt[{m}]";

            var v1 = scalarProcessor.Divide(
                scalarProcessor.MinusOneScalar,
                scalarProcessor.Sqrt(m)
            ); // $"-1 / Sqrt[{m}]";

            for (var colIndex = 0; colIndex < m; colIndex++)
            {
                clarkeArray[m - 2, colIndex] = colIndex % 2 == 0 ? v0 : v1;
                clarkeArray[m - 1, colIndex] = v0;
            }

            return clarkeArray;
        }

        public static T[,] CreateClarkeArray<T>(this IGaScalarProcessor<T> scalarProcessor, int vectorsCount)
        {
            return vectorsCount % 2 == 0 
                ? CreateClarkeArrayEven(scalarProcessor, vectorsCount) 
                : CreateClarkeArrayOdd(scalarProcessor, vectorsCount);
        }

        /// <summary>
        /// See the paper "Generalized Clarke Components for Polyphase Networks", 1969
        /// </summary>
        /// <param name="scalarProcessor"></param>
        /// <param name="vectorsCount"></param>
        /// <returns></returns>
        public static GaEuclideanVectorsFrame<T> CreateClarkeFrame<T>(this IGaScalarProcessor<T> scalarProcessor, int vectorsCount)
        {
            var clarkeMapArray =
                CreateClarkeArray(scalarProcessor, vectorsCount);

            return GaEuclideanVectorsFrame<T>.Create(
                scalarProcessor, 
                clarkeMapArray.ColumnsToVectorStoragesArray(scalarProcessor)
            );
        }

        public static GaVectorsLinearMap<T> CreateClarkeMap<T>(this IGaScalarProcessor<T> scalarProcessor, int vectorsCount)
        {
            var clarkeMapArray =
                CreateClarkeArray(scalarProcessor, vectorsCount);

            var basisVectorImagesDictionary = 
                new Dictionary<ulong, IGaVectorStorage<T>>();

            for (var i = 0; i < vectorsCount; i++)
                basisVectorImagesDictionary.Add(
                    (ulong) i, 
                    clarkeMapArray.ColumnToVectorStorage(i, scalarProcessor)
                );

            return GaVectorsLinearMap<T>.Create(
                scalarProcessor,
                basisVectorImagesDictionary
            );
        }

        public static GaEuclideanSimpleRotor<T> CreateSimpleKirchhoffRotor<T>(this IGaScalarProcessor<T> scalarProcessor, int vSpaceDimension)
        {
            var v1 = GaVectorStorage<T>.CreateUnitOnesVector(
                scalarProcessor, 
                vSpaceDimension
            );

            var v2 = GaVectorTermStorage<T>.Create(
                scalarProcessor, 
                vSpaceDimension - 1,
                scalarProcessor.OneScalar
            );

            return GaEuclideanSimpleRotor<T>.Create(v2, v1);
        }
    }
}
