using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Collections;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Arrays.Generic
{
    public static class ScalarArrayUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarArrayRow<T> GetRow<T>(this T[,] scalarArray, int rowIndex)
        {
            return new ScalarArrayRow<T>(scalarArray, rowIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarArrayColumn<T> GetColumn<T>(this T[,] scalarArray, int colIndex)
        {
            return new ScalarArrayColumn<T>(scalarArray, colIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarArrayDiagonal<T> GetDiagonal<T>(this T[,] scalarArray)
        {
            return new ScalarArrayDiagonal<T>(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ScalarArrayRow<T>> GetRows<T>(this T[,] scalarArray)
        {
            return scalarArray
                .GetLength(1)
                .GetRange(i =>
                    new ScalarArrayRow<T>(scalarArray, i)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ScalarArrayColumn<T>> GetColumns<T>(this T[,] scalarArray)
        {
            return scalarArray
                .GetLength(1)
                .GetRange(i =>
                    new ScalarArrayColumn<T>(scalarArray, i)
                );
        }


        public static IReadOnlyList<T> CombineRows<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray, IReadOnlyList<T> scalarList, Func<T, IReadOnlyList<T>, IReadOnlyList<T>> scalingFunc, Func<IReadOnlyList<T>, IReadOnlyList<T>, IReadOnlyList<T>> reducingFunc)
        {
            var colCount = scalarArray.GetLength(0);
            var rowCount = scalarArray.GetLength(1);

            IReadOnlyList<T> vector = new RepeatedItemReadOnlyList<T>(
                scalarProcessor.ScalarZero,
                colCount
            );

            rowCount = Math.Min(
                scalarList.Count,
                rowCount
            );

            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                var scalingFactor = scalarList[rowIndex];
                var rowVector = scalarArray.GetRow(rowIndex);
                var scaledVector = scalingFunc(scalingFactor, rowVector);

                vector = reducingFunc(vector, scaledVector);
            }

            return vector;
        }

        public static IReadOnlyList<T> CombineRows<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray, IEnumerable<KeyValuePair<int, T>> rowIndexScalarRecords, Func<T, IReadOnlyList<T>, IReadOnlyList<T>> scalingFunc, Func<IReadOnlyList<T>, IReadOnlyList<T>, IReadOnlyList<T>> reducingFunc)
        {
            var colCount = scalarArray.GetLength(0);
            var rowCount = scalarArray.GetLength(1);

            IReadOnlyList<T> vector = new RepeatedItemReadOnlyList<T>(
                scalarProcessor.ScalarZero,
                colCount
            );

            foreach (var (rowIndex, scalingFactor) in rowIndexScalarRecords)
            {
                if (rowIndex >= rowCount) continue;

                var rowVector = scalarArray.GetRow(rowIndex);
                var scaledVector = scalingFunc(scalingFactor, rowVector);

                vector = reducingFunc(vector, scaledVector);
            }

            return vector;
        }

        public static IReadOnlyList<T> CombineColumns<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray, IReadOnlyList<T> scalarList, Func<T, IReadOnlyList<T>, IReadOnlyList<T>> scalingFunc, Func<IReadOnlyList<T>, IReadOnlyList<T>, IReadOnlyList<T>> reducingFunc)
        {
            var colCount = scalarArray.GetLength(0);
            var rowCount = scalarArray.GetLength(1);

            IReadOnlyList<T> vector = new RepeatedItemReadOnlyList<T>(
                scalarProcessor.ScalarZero,
                rowCount
            );

            colCount = Math.Min(
                scalarList.Count,
                colCount
            );

            for (var columnIndex = 0; columnIndex < colCount; columnIndex++)
            {
                var scalingFactor = scalarList[columnIndex];
                var columnVector = scalarArray.GetColumn(columnIndex);
                var scaledVector = scalingFunc(scalingFactor, columnVector);

                vector = reducingFunc(vector, scaledVector);
            }

            return vector;
        }

        public static IReadOnlyList<T> CombineColumns<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray, IEnumerable<KeyValuePair<int, T>> colIndexScalarRecords, Func<T, IReadOnlyList<T>, IReadOnlyList<T>> scalingFunc, Func<IReadOnlyList<T>, IReadOnlyList<T>, IReadOnlyList<T>> reducingFunc)
        {
            var colCount = scalarArray.GetLength(0);
            var rowCount = scalarArray.GetLength(1);

            IReadOnlyList<T> vector = new RepeatedItemReadOnlyList<T>(
                scalarProcessor.ScalarZero,
                rowCount
            );

            foreach (var (columnIndex, scalingFactor) in colIndexScalarRecords)
            {
                if (columnIndex >= colCount) continue;

                var columnVector = scalarArray.GetColumn(columnIndex);
                var scaledVector = scalingFunc(scalingFactor, columnVector);

                vector = reducingFunc(vector, scaledVector);
            }

            return vector;
        }


        public static T[,] CreateClarkeRotationArray3D<T>(IScalarProcessor<T> processor)
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

            return processor.Times(scalar, clarkeArray);
        }

        public static T[,] CreateClarkeRotationArray5D<T>(IScalarProcessor<T> processor)
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

            return processor.Times(scalar, clarkeArray);
        }

        public static T[,] CreateClarkeRotationArray6D<T>(IScalarProcessor<T> processor)
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

            var scalar = processor.SqrtRational(2, 6);

            return processor.Times(scalar, clarkeArray);
        }

        /// <summary>
        /// See the paper "Generalized Clarke Components for Polyphase Networks", 1969
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="vectorsCount"></param>
        /// <returns></returns>
        private static T[,] CreateClarkeRotationArrayOdd<T>(IScalarProcessor<T> processor, int vectorsCount)
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
        private static T[,] CreateClarkeRotationArrayEven<T>(IScalarProcessor<T> processor, int vectorsCount)
        {
            var clarkeArray = new T[vectorsCount, vectorsCount];

            var m = vectorsCount;
            var s = processor.SqrtRational(2, m); //$"Sqrt[2 / {m}]";

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

        public static T[,] CreateClarkeRotationArray<T>(this IScalarProcessor<T> processor, int vectorsCount)
        {
            return vectorsCount % 2 == 0
                ? CreateClarkeRotationArrayEven(processor, vectorsCount)
                : CreateClarkeRotationArrayOdd(processor, vectorsCount);
        }


        public static T[,] MapScalars<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray1, T scalar2, Func<T, T, T> scalarMapping)
        {
            scalar2 ??= scalarProcessor.ScalarZero;

            var rowsCount = scalarArray1.GetLength(0);
            var colsCount = scalarArray1.GetLength(1);

            var array = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
                for (var j = 0; j < colsCount; j++)
                {
                    array[i, j] = scalarMapping(
                        scalarArray1[i, j] ?? scalarProcessor.ScalarZero,
                        scalar2
                    );
                }

            return array;
        }

        public static T[,] MapScalars<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarArray2, Func<T, T, T> scalarMapping)
        {
            scalar1 ??= scalarProcessor.ScalarZero;

            var rowsCount = scalarArray2.GetLength(0);
            var colsCount = scalarArray2.GetLength(1);

            var array = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
                for (var j = 0; j < colsCount; j++)
                {
                    array[i, j] = scalarMapping(
                        scalar1,
                        scalarArray2[i, j] ?? scalarProcessor.ScalarZero
                    );
                }

            return array;
        }

        public static T[,] MapScalars<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray1, T[,] scalarArray2, Func<T, T, T> scalarMapping)
        {
            var rowsCount = scalarArray1.GetLength(0);
            var colsCount = scalarArray1.GetLength(1);

            var array = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
                for (var j = 0; j < colsCount; j++)
                {
                    array[i, j] = scalarMapping(
                        scalarArray1[i, j] ?? scalarProcessor.ScalarZero,
                        scalarArray2[i, j] ?? scalarProcessor.ScalarZero
                    );
                }

            return array;
        }

        public static T[,] MapNotZeroScalars<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray, Func<T, T> scalarMapping)
        {
            var rowsCount = scalarArray.GetLength(0);
            var colsCount = scalarArray.GetLength(1);

            var array = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
                for (var j = 0; j < colsCount; j++)
                {
                    var scalar = scalarArray[i, j];

                    if (ReferenceEquals(scalar, null) || scalarProcessor.IsZero(scalar))
                    {
                        array[i, j] = scalarProcessor.ScalarZero;

                        continue;
                    }

                    array[i, j] = scalarMapping(
                        scalarArray[i, j]
                    );
                }

            return array;
        }

        public static T[,] MapScalars<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray, Func<T, T> scalarMapping)
        {
            var rowsCount = scalarArray.GetLength(0);
            var colsCount = scalarArray.GetLength(1);

            var array = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
                for (var j = 0; j < colsCount; j++)
                {
                    array[i, j] = scalarMapping(
                        scalarArray[i, j] ?? scalarProcessor.ScalarZero
                    );
                }

            return array;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Add<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray1, T[,] scalarArray2)
        {
            return scalarProcessor.MapScalars(
                scalarArray1,
                scalarArray2,
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Add<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarArray2)
        {
            return scalarProcessor.MapScalars(
                scalar1,
                scalarArray2,
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Add<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray1, T scalar2)
        {
            return scalarProcessor.MapScalars(
                scalarArray1,
                scalar2,
                scalarProcessor.Add
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Subtract<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray1, T[,] scalarArray2)
        {
            return scalarProcessor.MapScalars(
                scalarArray1,
                scalarArray2,
                scalarProcessor.Subtract
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarArray2)
        {
            return scalarProcessor.MapScalars(
                scalar1,
                scalarArray2,
                scalarProcessor.Subtract
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Subtract<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray1, T scalar2)
        {
            return scalarProcessor.MapScalars(
                scalarArray1,
                scalar2,
                scalarProcessor.Subtract
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Times<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray1, T[,] scalarArray2)
        {
            return scalarProcessor.MapScalars(
                scalarArray1,
                scalarArray2,
                scalarProcessor.Times
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Times<T>(this IScalarProcessor<T> scalarProcessor, params T[][,] scalarArrays)
        {
            return scalarArrays.Skip(1).Aggregate(
                scalarArrays[0],
                scalarProcessor.Times
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Times<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T[,]> scalarArrays)
        {
            return scalarArrays.Skip(1).Aggregate(
                scalarArrays[0],
                scalarProcessor.Times
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarArray2)
        {
            return scalarProcessor.MapScalars(
                scalar1,
                scalarArray2,
                scalarProcessor.Times
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Times<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray1, T scalar2)
        {
            return scalarProcessor.MapScalars(
                scalarArray1,
                scalar2,
                scalarProcessor.Times
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Divide<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray1, T[,] scalarArray2)
        {
            return scalarProcessor.MapScalars(
                scalarArray1,
                scalarArray2,
                scalarProcessor.Divide
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Divide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarArray2)
        {
            return scalarProcessor.MapScalars(
                scalar1,
                scalarArray2,
                scalarProcessor.Divide
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Divide<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray1, T scalar2)
        {
            return scalarProcessor.MapScalars(
                scalarArray1,
                scalar2,
                scalarProcessor.Divide
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] NotZeroScalarsInverse<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
        {
            return scalarProcessor.MapNotZeroScalars(
                scalarArray,
                scalarProcessor.Inverse
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsNegative<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
        {
            return scalarProcessor.MapScalars(
                scalarArray,
                scalarProcessor.Negative
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsCos<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
        {
            return scalarProcessor.MapScalars(
                scalarArray,
                scalarProcessor.Cos
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsSin<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
        {
            return scalarProcessor.MapScalars(
                scalarArray,
                scalarProcessor.Sin
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsTan<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
        {
            return scalarProcessor.MapScalars(
                scalarArray,
                scalarProcessor.Tan
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsExp<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
        {
            return scalarProcessor.MapScalars(
                scalarArray,
                scalarProcessor.Exp
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsLog<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
        {
            return scalarProcessor.MapScalars(
                scalarArray,
                scalarProcessor.LogE
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsLog10<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
        {
            return scalarProcessor.MapScalars(
                scalarArray,
                scalarProcessor.Log10
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsLog2<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
        {
            return scalarProcessor.MapScalars(
                scalarArray,
                scalarProcessor.Log2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsAbs<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
        {
            return scalarProcessor.MapScalars(
                scalarArray,
                scalarProcessor.Abs
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsSqrt<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
        {
            return scalarProcessor.MapScalars(
                scalarArray,
                scalarProcessor.Sqrt
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsSqrtOfAbs<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
        {
            return scalarProcessor.MapScalars(
                scalarArray,
                scalarProcessor.SqrtOfAbs
            );
        }


        //public static VectorStorage<T> ColumnToVectorStorage<T>(this T[,] scalarArray, int colIndex, IScalarProcessor<T> scalarProcessor)
        //{
        //    var rowsCount = scalarArray.GetLength(0);

        //    var composer = scalarProcessor.CreateVectorStorageComposer(rowsCount);

        //    for (var i = 0; i < rowsCount; i++)
        //    {
        //        var scalar = scalarArray[i, colIndex] ?? scalarProcessor.ScalarZero;

        //        if (!scalarProcessor.IsZero(scalar))
        //            composer.SetTerm((ulong)i, scalar);
        //    }

        //    return composer.CreateVectorStorage();
        //}

        //public static VectorStorage<T> RowToVectorStorage<T>(this T[,] scalarArray, int rowIndex, IScalarProcessor<T> scalarProcessor)
        //{
        //    var colsCount = scalarArray.GetLength(1);

        //    var composer = scalarProcessor.CreateVectorStorageComposer(1);

        //    for (var j = 0; j < colsCount; j++)
        //    {
        //        var scalar = scalarArray[rowIndex, j] ?? scalarProcessor.ScalarZero;

        //        if (!scalarProcessor.IsZero(scalar))
        //            composer.SetTerm((ulong)j, scalar);
        //    }

        //    return composer.CreateVectorStorage();
        //}

        //public static Dictionary<ulong, VectorStorage<T>> ColumnsToVectorStoragesDictionary<T>(this T[,] scalarArray, IScalarProcessor<T> scalarProcessor)
        //{
        //    var rowsCount = scalarArray.GetLength(0);
        //    var colsCount = scalarArray.GetLength(1);

        //    var vectorsDictionary =
        //        new Dictionary<ulong, VectorStorage<T>>();

        //    for (var j = 0; j < colsCount; j++)
        //    {
        //        var composer = scalarProcessor.CreateVectorStorageComposer(1);

        //        for (var i = 0; i < rowsCount; i++)
        //        {
        //            var scalar = scalarArray[i, j] ?? scalarProcessor.ScalarZero;

        //            if (!scalarProcessor.IsZero(scalar))
        //                composer.SetTerm((ulong)i, scalar);
        //        }

        //        vectorsDictionary.Add((ulong)j, composer.CreateVectorStorage());
        //    }

        //    return vectorsDictionary;
        //}

        //public static VectorStorage<T>[] ColumnsToVectorStoragesArray<T>(this T[,] scalarArray, IScalarProcessor<T> scalarProcessor)
        //{
        //    var rowsCount = scalarArray.GetLength(0);
        //    var colsCount = scalarArray.GetLength(1);

        //    var vectorsArray =
        //        new VectorStorage<T>[colsCount];

        //    for (var j = 0; j < colsCount; j++)
        //    {
        //        var composer = scalarProcessor.CreateVectorStorageComposer(1);

        //        for (var i = 0; i < rowsCount; i++)
        //        {
        //            var scalar = scalarArray[i, j] ?? scalarProcessor.ScalarZero;

        //            if (!scalarProcessor.IsZero(scalar))
        //                composer.SetTerm((ulong)i, scalar);
        //        }

        //        vectorsArray[j] = composer.CreateVectorStorage();
        //    }

        //    return vectorsArray;
        //}

        //public static GaVector<T>[] ColumnsToVectorsArray<T>(this T[,] scalarArray, IScalarProcessor<T> scalarProcessor)
        //{
        //    var rowsCount = scalarArray.GetLength(0);
        //    var colsCount = scalarArray.GetLength(1);

        //    var vectorsArray =
        //        new GaVector<T>[colsCount];

        //    for (var j = 0; j < colsCount; j++)
        //    {
        //        var composer = scalarProcessor.CreateVectorStorageComposer(1);

        //        for (var i = 0; i < rowsCount; i++)
        //        {
        //            var scalar = scalarArray[i, j] ?? scalarProcessor.ScalarZero;

        //            if (!scalarProcessor.IsZero(scalar))
        //                composer.SetTerm((ulong)i, scalar);
        //        }

        //        vectorsArray[j] = composer.CreateVector();
        //    }

        //    return vectorsArray;
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> matrix, int index)
        {
            return index >= 0 &&
                   index < matrix.Count
                ? matrix[index] ?? scalarProcessor.ScalarZero
                : scalarProcessor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IScalarProcessor<T> scalarProcessor, T[,] matrix, int rowIndex, int colIndex)
        {
            return rowIndex >= 0 &&
                   colIndex >= 0 &&
                   rowIndex < matrix.GetLength(0) &&
                   colIndex < matrix.GetLength(1)
                ? matrix[rowIndex, colIndex] ?? scalarProcessor.ScalarZero
                : scalarProcessor.ScalarZero;
        }


        public static IReadOnlyList<T> MapScalars<T1, T>(this IReadOnlyList<T1> matrix1, Func<T1, T> scalarMapping)
        {
            var count = matrix1.Count;
            var newMatrix = new T[count];

            for (var i = 0; i < count; i++)
                newMatrix[i] = scalarMapping(matrix1[i]);

            return newMatrix;
        }

        public static IReadOnlyList<T> MapScalars<T1, T>(this IReadOnlyList<T1> matrix1, Func<int, T1, T> scalarMapping)
        {
            var count = matrix1.Count;
            var newMatrix = new T[count];

            for (var i = 0; i < count; i++)
                newMatrix[i] = scalarMapping(i, matrix1[i]);

            return newMatrix;
        }

        public static IReadOnlyList<T> MapScalars<T>(this IReadOnlyList<T> matrix1, IReadOnlyList<T> matrix2, T initScalar, Func<T, T, T> scalarMapping)
        {
            var count1 = matrix1.Count;
            var count2 = matrix2.Count;
            var count = Math.Max(count1, count2);

            var newMatrix = new T[count];

            for (var i = 0; i < count; i++)
            {
                var s1 = i < count1
                    ? matrix1[i]
                    : initScalar;

                var s2 = i < count2
                    ? matrix2[i]
                    : initScalar;

                newMatrix[i] = scalarMapping(s1, s2);
            }

            return newMatrix;
        }

        public static IReadOnlyList<T> MapScalars<T1, T2, T>(this IReadOnlyList<T1> matrix1, IReadOnlyList<T2> matrix2, Func<T1> initFunc1, Func<T2> initFunc2, Func<T1, T2, T> scalarMapping)
        {
            var count1 = matrix1.Count;
            var count2 = matrix2.Count;
            var count = Math.Max(count1, count2);

            var newMatrix = new T[count];

            for (var i = 0; i < count; i++)
            {
                var s1 = i < count1
                    ? matrix1[i]
                    : initFunc1();

                var s2 = i < count2
                    ? matrix2[i]
                    : initFunc2();

                newMatrix[i] = scalarMapping(s1, s2);
            }

            return newMatrix;
        }

        public static T[,] MapScalars<T1, T>(this T1[,] matrix1, Func<T1, T> scalarMapping)
        {
            var rowsCount = matrix1.GetLength(0);
            var colsCount = matrix1.GetLength(1);
            var newMatrix = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
                for (var j = 0; j < colsCount; j++)
                    newMatrix[i, j] = scalarMapping(matrix1[i, j]);

            return newMatrix;
        }

        public static T[,] MapScalars<T1, T>(this T1[,] matrix1, Func<int, int, T1, T> scalarMapping)
        {
            var rowsCount = matrix1.GetLength(0);
            var colsCount = matrix1.GetLength(1);
            var newMatrix = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
                for (var j = 0; j < colsCount; j++)
                    newMatrix[i, j] = scalarMapping(i, j, matrix1[i, j]);

            return newMatrix;
        }

        public static T[,] MapScalars<T>(this T[,] matrix1, T[,] matrix2, T initScalar, Func<T, T, T> scalarMapping)
        {
            var rowCount1 = matrix1.GetLength(0);
            var colCount1 = matrix1.GetLength(1);

            var rowCount2 = matrix2.GetLength(0);
            var colCount2 = matrix1.GetLength(2);

            var rowCount = Math.Max(rowCount1, rowCount2);
            var colCount = Math.Max(colCount1, colCount2);

            var newMatrix = new T[rowCount, colCount];

            for (var j = 0; j < colCount; j++)
                for (var i = 0; i < rowCount; i++)
                {
                    var s1 = i < rowCount1 && j < colCount1
                        ? matrix1[i, j]
                        : initScalar;

                    var s2 = i < rowCount2 && j < colCount2
                        ? matrix2[i, j]
                        : initScalar;

                    newMatrix[i, j] = scalarMapping(s1, s2);
                }

            return newMatrix;
        }

        public static T[,] MapScalars<T1, T2, T>(this T1[,] matrix1, T2[,] matrix2, Func<T1> initFunc1, Func<T2> initFunc2, Func<T1, T2, T> scalarMapping)
        {
            var rowCount1 = matrix1.GetLength(0);
            var colCount1 = matrix1.GetLength(1);

            var rowCount2 = matrix2.GetLength(0);
            var colCount2 = matrix1.GetLength(2);

            var rowCount = Math.Max(rowCount1, rowCount2);
            var colCount = Math.Max(colCount1, colCount2);

            var newMatrix = new T[rowCount, colCount];

            for (var j = 0; j < colCount; j++)
                for (var i = 0; i < rowCount; i++)
                {
                    var s1 = i < rowCount1 && j < colCount1
                        ? matrix1[i, j]
                        : initFunc1();

                    var s2 = i < rowCount2 && j < colCount2
                        ? matrix2[i, j]
                        : initFunc2();

                    newMatrix[i, j] = scalarMapping(s1, s2);
                }

            return newMatrix;
        }

        public static T[,] MapScalars<T>(this T[,] matrix1, T[,] matrix2, T initScalar, Func<T, T, T, T> accumulatorFunc)
        {
            var rowsCount1 = matrix1.GetLength(0);
            var colsCount1 = matrix1.GetLength(1);

            var rowsCount2 = matrix2.GetLength(0);
            var colsCount2 = matrix2.GetLength(1);

            var innerCount = Math.Max(colsCount1, rowsCount2);

            var newMatrix = new T[rowsCount1, colsCount2];

            for (var i = 0; i < rowsCount1; i++)
            {
                for (var j = 0; j < colsCount2; j++)
                {
                    var scalar = initScalar;

                    for (var k = 0; k < innerCount; k++)
                    {
                        var aik = i < rowsCount1 && k < colsCount1
                            ? matrix1[i, k]
                            : initScalar;

                        var bkj = k < rowsCount2 && j < colsCount2
                            ? matrix2[k, j]
                            : initScalar;

                        scalar = accumulatorFunc(scalar, aik, bkj);
                    }

                    newMatrix[i, j] = scalar;
                }
            }

            return newMatrix;
        }

        public static T[,] MapScalars<T1, T2, T>(this T1[,] matrix1, T2[,] matrix2, Func<T1> initFunc1, Func<T2> initFunc2, Func<T> accumulatorInitFunc, Func<T, T1, T2, T> accumulatorFunc)
        {
            var rowsCount1 = matrix1.GetLength(0);
            var colsCount1 = matrix1.GetLength(1);

            var rowsCount2 = matrix2.GetLength(0);
            var colsCount2 = matrix2.GetLength(1);

            var innerCount = Math.Max(colsCount1, rowsCount2);

            var newMatrix = new T[rowsCount1, colsCount2];

            for (var i = 0; i < rowsCount1; i++)
            {
                for (var j = 0; j < colsCount2; j++)
                {
                    var accumulator = accumulatorInitFunc();

                    for (var k = 0; k < innerCount; k++)
                    {
                        var aik = i < rowsCount1 && k < colsCount1
                            ? matrix1[i, k]
                            : initFunc1();

                        var bkj = k < rowsCount2 && j < colsCount2
                            ? matrix2[k, j]
                            : initFunc2();

                        accumulator = accumulatorFunc(accumulator, aik, bkj);
                    }

                    newMatrix[i, j] = accumulator;
                }
            }

            return newMatrix;
        }


        public static IReadOnlyList<T> MatrixProduct<T>(this IScalarProcessor<T> scalarProcessor, T[,] matrix1, IReadOnlyList<T> matrix2)
        {
            var m = matrix1.GetLength(0);
            var n = matrix1.GetLength(1);

            if (n != matrix2.Count)
                throw new InvalidOperationException();

            var matrix = new T[m];

            for (var i = 0; i < m; i++)
            {
                var d = scalarProcessor.ScalarZero;

                for (var j = 0; j < n; j++)
                    d = scalarProcessor.Add(
                        d,
                        scalarProcessor.Times(
                            matrix1[i, j],
                            matrix2[j]
                        )
                    );

                matrix[i] = d;
            }

            return matrix;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] MatrixProduct<T>(this IScalarProcessor<T> scalarProcessor, T[,] matrix1, T[,] matrix2)
        {
            return matrix1.MapScalars(
                matrix2,
                scalarProcessor.ScalarZero,
                (accumulator, a, b) =>
                    scalarProcessor.Add(accumulator, scalarProcessor.Times(a, b))
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> Abs<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
        {
            return v1.MapScalars(scalarProcessor.Abs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> Sqrt<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
        {
            return v1.MapScalars(scalarProcessor.Sqrt);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> SqrtOfAbs<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
        {
            return v1.MapScalars(scalarProcessor.SqrtOfAbs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> Exp<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
        {
            return v1.MapScalars(scalarProcessor.Exp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> LogE<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
        {
            return v1.MapScalars(scalarProcessor.LogE);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> Log2<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
        {
            return v1.MapScalars(scalarProcessor.Log2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> Log10<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
        {
            return v1.MapScalars(scalarProcessor.Log10);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> Log<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1, T scalar)
        {
            return v1.MapScalars(s => scalarProcessor.Log(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> Log<T>(this IScalarProcessor<T> scalarProcessor, T scalar, IReadOnlyList<T> v1)
        {
            return v1.MapScalars(s => scalarProcessor.Log(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> Power<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1, T scalar)
        {
            return v1.MapScalars(s => scalarProcessor.Power(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> Power<T>(this IScalarProcessor<T> scalarProcessor, T scalar, IReadOnlyList<T> v1)
        {
            return v1.MapScalars(s => scalarProcessor.Power(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> Cos<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
        {
            return v1.MapScalars(scalarProcessor.Cos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> Sin<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
        {
            return v1.MapScalars(scalarProcessor.Sin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> Tan<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
        {
            return v1.MapScalars(scalarProcessor.Tan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> ArcCos<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
        {
            return v1.MapScalars(scalarProcessor.ArcCos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> ArcSin<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
        {
            return v1.MapScalars(scalarProcessor.ArcSin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> ArcTan<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
        {
            return v1.MapScalars(scalarProcessor.ArcTan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> Cosh<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
        {
            return v1.MapScalars(scalarProcessor.Cosh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> Sinh<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
        {
            return v1.MapScalars(scalarProcessor.Sinh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> Tanh<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
        {
            return v1.MapScalars(scalarProcessor.Tanh);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Abs<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Abs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Sqrt<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Sqrt);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] SqrtOfAbs<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.SqrtOfAbs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Exp<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Exp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] LogE<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.LogE);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Log2<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Log2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Log10<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Log10);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Log<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1, T scalar)
        {
            return v1.MapScalars(s => scalarProcessor.Log(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Log<T>(this IScalarProcessor<T> scalarProcessor, T scalar, T[,] v1)
        {
            return v1.MapScalars(s => scalarProcessor.Log(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Power<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1, T scalar)
        {
            return v1.MapScalars(s => scalarProcessor.Power(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Power<T>(this IScalarProcessor<T> scalarProcessor, T scalar, T[,] v1)
        {
            return v1.MapScalars(s => scalarProcessor.Power(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Cos<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Cos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Sin<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Sin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Tan<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Tan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ArcCos<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.ArcCos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ArcSin<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.ArcSin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ArcTan<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.ArcTan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Cosh<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Cosh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Sinh<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Sinh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Tanh<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Tanh);
        }
    }
}