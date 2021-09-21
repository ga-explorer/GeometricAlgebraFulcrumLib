using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Geometry.Frames;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Mathematica.Applications.GaPoT
{
    public static class GeoPoTUtils
    {
        public static T[,] CreateClarkeArray3D<T>(IScalarAlgebraProcessor<T> processor)
        {
            var clarkeArray = new T[3, 3];

            clarkeArray[0, 0] = processor.ScalarOne;
            clarkeArray[0, 1] = processor.Divide(-1, 2);
            clarkeArray[0, 2] = processor.Divide(-1, 2);

            clarkeArray[1, 0] = processor.ScalarZero;
            clarkeArray[1, 1] = processor.Divide(processor.Sqrt(3), 2);
            clarkeArray[1, 2] = processor.Divide(processor.Sqrt(3), -2);

            clarkeArray[2, 0] = processor.Divide(1, processor.Sqrt(2));
            clarkeArray[2, 1] = processor.Divide(1, processor.Sqrt(2));
            clarkeArray[2, 2] = processor.Divide(1, processor.Sqrt(2));

            var scalar = processor.Sqrt(
                processor.Divide(2, 3)
            );

            return LinArrayUtils.Times(processor, scalar, clarkeArray);
        }

        public static T[,] CreateClarkeArray5D<T>(IScalarAlgebraProcessor<T> processor)
        {
            var clarkeArray = new T[5, 5];

            clarkeArray[0, 0] = processor.ScalarOne;
            clarkeArray[0, 1] = processor.CosPiRatio(2, 5);
            clarkeArray[0, 2] = processor.CosPiRatio(4, 5);
            clarkeArray[0, 3] = processor.CosPiRatio(6, 5);
            clarkeArray[0, 4] = processor.CosPiRatio(8, 5);

            clarkeArray[1, 0] = processor.ScalarZero;
            clarkeArray[1, 1] = processor.SinPiRatio(2, 5);
            clarkeArray[1, 2] = processor.SinPiRatio(4, 5);
            clarkeArray[1, 3] = processor.SinPiRatio(6, 5);
            clarkeArray[1, 4] = processor.SinPiRatio(8, 5);

            clarkeArray[2, 0] = processor.ScalarOne;
            clarkeArray[2, 1] = processor.CosPiRatio(4, 5);
            clarkeArray[2, 2] = processor.CosPiRatio(8, 5);
            clarkeArray[2, 3] = processor.CosPiRatio(12, 5);
            clarkeArray[2, 4] = processor.CosPiRatio(16, 5);

            clarkeArray[3, 0] = processor.ScalarZero;
            clarkeArray[3, 1] = processor.SinPiRatio(4, 5);
            clarkeArray[3, 2] = processor.SinPiRatio(8, 5);
            clarkeArray[3, 3] = processor.SinPiRatio(12, 5);
            clarkeArray[3, 4] = processor.SinPiRatio(16, 5);

            clarkeArray[4, 0] = processor.Divide(1, processor.Sqrt(2));
            clarkeArray[4, 1] = processor.Divide(1, processor.Sqrt(2));
            clarkeArray[4, 2] = processor.Divide(1, processor.Sqrt(2));
            clarkeArray[4, 3] = processor.Divide(1, processor.Sqrt(2));
            clarkeArray[4, 4] = processor.Divide(1, processor.Sqrt(2));

            var scalar = processor.Sqrt(
                processor.Divide(2, 5)
            );

            return LinArrayUtils.Times(processor, scalar, clarkeArray);
        }

        public static T[,] CreateClarkeArray6D<T>(IScalarAlgebraProcessor<T> processor)
        {
            var clarkeArray = new T[6, 6];

            clarkeArray[0, 0] = processor.ScalarOne;
            clarkeArray[0, 1] = processor.Rational(-1, 2);
            clarkeArray[0, 2] = processor.Rational(-1, 2);
            clarkeArray[0, 3] = processor.Rational(1, 2);
            clarkeArray[0, 4] = processor.ScalarMinusOne;
            clarkeArray[0, 5] = processor.Rational(1, 2);

            clarkeArray[1, 0] = processor.ScalarZero;
            clarkeArray[1, 1] = processor.Divide(processor.Sqrt(3), 2);
            clarkeArray[1, 2] = processor.NegativeDivide(processor.Sqrt(3), 2);
            clarkeArray[1, 3] = processor.Divide(processor.Sqrt(3), 2);
            clarkeArray[1, 4] = processor.ScalarZero;
            clarkeArray[1, 5] = processor.NegativeDivide(processor.Sqrt(3), 2);

            clarkeArray[2, 0] = processor.ScalarOne;
            clarkeArray[2, 1] = processor.Rational(-1, 2);
            clarkeArray[2, 2] = processor.Rational(-1, 2);
            clarkeArray[2, 3] = processor.Rational(-1, 2);
            clarkeArray[2, 4] = processor.ScalarOne;
            clarkeArray[2, 5] = processor.Rational(-1, 2);

            clarkeArray[3, 0] = processor.ScalarZero;
            clarkeArray[3, 1] = processor.NegativeDivide(processor.Sqrt(3), 2);
            clarkeArray[3, 2] = processor.Divide(processor.Sqrt(3), 2);
            clarkeArray[3, 3] = processor.Divide(processor.Sqrt(3), 2);
            clarkeArray[3, 4] = processor.ScalarZero;
            clarkeArray[3, 5] = processor.NegativeDivide(processor.Sqrt(3), 2);

            clarkeArray[4, 0] = processor.SqrtRational(1, 2);
            clarkeArray[4, 1] = processor.SqrtRational(1, 2);
            clarkeArray[4, 2] = processor.SqrtRational(1, 2);
            clarkeArray[4, 3] = processor.SqrtRational(1, 2);
            clarkeArray[4, 4] = processor.SqrtRational(1, 2);
            clarkeArray[4, 5] = processor.SqrtRational(1, 2);

            clarkeArray[5, 0] = processor.SqrtRational(1, 2);
            clarkeArray[5, 1] = processor.SqrtRational(1, 2);
            clarkeArray[5, 2] = processor.SqrtRational(1, 2);
            clarkeArray[5, 3] = processor.Negative(processor.SqrtRational(1, 2));
            clarkeArray[5, 4] = processor.Negative(processor.SqrtRational(1, 2));
            clarkeArray[5, 5] = processor.Negative(processor.SqrtRational(1, 2));

            var scalar = processor.SqrtRational(2,6);

            return LinArrayUtils.Times(processor, scalar, clarkeArray);
        }

        /// <summary>
        /// See the paper "Generalized Clarke Components for Polyphase Networks", 1969
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="vectorsCount"></param>
        /// <returns></returns>
        private static T[,] CreateClarkeArrayOdd<T>(IGeometricAlgebraProcessor<T> processor, int vectorsCount)
        {
            var clarkeArray = new T[vectorsCount, vectorsCount];

            var m = vectorsCount;
            var s = processor.Sqrt(
                processor.Divide(
                    processor.GetScalarFromNumber(2),
                    processor.GetScalarFromNumber(m)
                )
            ); //$"Sqrt[2 / {m}]";

            // m is odd, fill all rows except the last
            var n = (m - 1) / 2;
            for (var k = 0; k < n; k++)
            {
                var rowIndex1 = 2 * k;
                var rowIndex2 = 2 * k + 1;

                clarkeArray[rowIndex1, 0] = s;
                clarkeArray[rowIndex2, 0] = processor.ScalarZero;
                
                for (var colIndex = 1; colIndex < m; colIndex++)
                {
                    var angle =
                        processor.PiRatio(
                            2 * (k + 1) * colIndex, 
                            m
                        ); // $"2 * Pi * {k + 1} * {i} / {m}";

                    var cosAngle = processor.Times(
                        s,
                        processor.Cos(angle)
                    ); // $"{s} * Cos[{angle}]";
                    
                    var sinAngle = processor.Times(
                        s,
                        processor.Sin(angle)
                    ); // $"{s} * Sin[{angle}]";
                    
                    clarkeArray[rowIndex1, colIndex] = cosAngle;
                    clarkeArray[rowIndex2, colIndex] = sinAngle;
                }
            }

            //Fill the last column
            var v = processor.Divide(
                processor.ScalarOne,
                processor.Sqrt(m)
            ); // $"1 / Sqrt[{m}]";

            for (var colIndex = 0; colIndex < m; colIndex++) 
                clarkeArray[m - 1, colIndex] = v;

            return clarkeArray;
        }

        /// <summary>
        /// See the paper "Generalized Clarke Components for Polyphase Networks", 1969
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="vectorsCount"></param>
        /// <returns></returns>
        private static T[,] CreateClarkeArrayEven<T>(IGeometricAlgebraProcessor<T> processor, int vectorsCount)
        {
            var clarkeArray = new T[vectorsCount, vectorsCount];

            var m = vectorsCount;
            var s = processor.SqrtRational(2,m); //$"Sqrt[2 / {m}]";

            //m is even, fill all rows except the last two
            var n = (m - 1) / 2;
            for (var k = 0; k < n; k++)
            {
                var rowIndex1 = 2 * k;
                var rowIndex2 = 2 * k + 1;
                
                clarkeArray[rowIndex1, 0] = s;
                clarkeArray[rowIndex2, 0] = processor.ScalarZero;
                
                for (var colIndex = 1; colIndex < m; colIndex++)
                {
                    var angle =
                        processor.PiRatio(
                            2 * (k + 1) * colIndex, 
                            m
                        ); // $"2 * Pi * {k + 1} * {i} / {m}";

                    var cosAngle = processor.Times(
                        s,
                        processor.Cos(angle)
                    ); // $"{s} * Cos[{angle}]";
                    
                    var sinAngle = processor.Times(
                        s,
                        processor.Sin(angle)
                    ); // $"{s} * Sin[{angle}]";
                    
                    clarkeArray[rowIndex1, colIndex] = cosAngle;
                    clarkeArray[rowIndex2, colIndex] = sinAngle;
                }
            }

            //Fill the last two rows
            var v0 = processor.Divide(
                processor.ScalarOne,
                processor.Sqrt(m)
            ); // $"1 / Sqrt[{m}]";

            var v1 = processor.Divide(
                processor.ScalarMinusOne,
                processor.Sqrt(m)
            ); // $"-1 / Sqrt[{m}]";

            for (var colIndex = 0; colIndex < m; colIndex++)
            {
                clarkeArray[m - 2, colIndex] = colIndex % 2 == 0 ? v0 : v1;
                clarkeArray[m - 1, colIndex] = v0;
            }

            return clarkeArray;
        }

        public static T[,] CreateClarkeArray<T>(this IGeometricAlgebraProcessor<T> processor, int vectorsCount)
        {
            return vectorsCount % 2 == 0 
                ? CreateClarkeArrayEven(processor, vectorsCount) 
                : CreateClarkeArrayOdd(processor, vectorsCount);
        }

        /// <summary>
        /// See the paper "Generalized Clarke Components for Polyphase Networks", 1969
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="vectorsCount"></param>
        /// <returns></returns>
        public static GeoFreeFrame<T> CreateClarkeFrame<T>(this IGeometricAlgebraProcessor<T> processor, int vectorsCount)
        {
            var clarkeMapArray =
                CreateClarkeArray(processor, vectorsCount);

            return processor.CreateFreeFrame(
                GeoFreeFrameSpecs.CreateUnitBasisSpecs(), 
                clarkeMapArray.ColumnsToVectorStoragesArray(processor)
            );
        }

        public static IOutermorphism<T> CreateClarkeMap<T>(this IGeometricAlgebraProcessor<T> processor, int vectorsCount)
        {
            var clarkeMapArray =
                CreateClarkeArray(processor, vectorsCount);

            var basisVectorImagesDictionary = 
                new Dictionary<ulong, VectorStorage<T>>();

            for (var i = 0; i < vectorsCount; i++)
                basisVectorImagesDictionary.Add(
                    (ulong) i, 
                    clarkeMapArray.ColumnToVectorStorage(i, processor)
                );

            return processor.CreateLinearMapOutermorphism(
                //(uint) vectorsCount,
                basisVectorImagesDictionary
            );
        }

        public static PureRotor<T> CreateSimpleKirchhoffRotor<T>(this IGeometricAlgebraProcessor<T> processor, uint vSpaceDimension)
        {
            var v1 = processor.CreateStorageVectorUnitOnes(
                (int) vSpaceDimension
            );

            var v2 = processor.CreateVectorTermStorage(
                vSpaceDimension - 1,
                processor.ScalarOne
            );

            return processor.CreateEuclideanRotor(v2, v1);
        }
    }
}
