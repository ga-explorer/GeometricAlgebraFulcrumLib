using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Processing.Matrices
{
    public static class GaMatrixProcessorUtils
    {
        public static T[,] MapScalars<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T scalar2, Func<T, T, T> scalarMapping)
        {
            scalar2 ??= scalarProcessor.ZeroScalar;

            var rowsCount = scalarsArray1.GetLength(0);
            var colsCount = scalarsArray1.GetLength(1);

            var array = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
            {
                array[i, j] = scalarMapping(
                    scalarsArray1[i, j] ?? scalarProcessor.ZeroScalar,
                    scalar2
                );
            }

            return array;
        }

        public static T[,] MapScalars<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarsArray2, Func<T, T, T> scalarMapping)
        {
            scalar1 ??= scalarProcessor.ZeroScalar;

            var rowsCount = scalarsArray2.GetLength(0);
            var colsCount = scalarsArray2.GetLength(1);

            var array = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
            {
                array[i, j] = scalarMapping(
                    scalar1,
                    scalarsArray2[i, j] ?? scalarProcessor.ZeroScalar
                );
            }

            return array;
        }

        public static T[,] MapScalars<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T[,] scalarsArray2, Func<T, T, T> scalarMapping)
        {
            var rowsCount = scalarsArray1.GetLength(0);
            var colsCount = scalarsArray1.GetLength(1);

            var array = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
            {
                array[i, j] = scalarMapping(
                    scalarsArray1[i, j] ?? scalarProcessor.ZeroScalar,
                    scalarsArray2[i, j] ?? scalarProcessor.ZeroScalar
                );
            }

            return array;
        }

        public static T[,] MapNotZeroScalars<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray, Func<T, T> scalarMapping)
        {
            var rowsCount = scalarsArray.GetLength(0);
            var colsCount = scalarsArray.GetLength(1);

            var array = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
            {
                var scalar = scalarsArray[i, j];

                if (ReferenceEquals(scalar, null) || scalarProcessor.IsZero(scalar))
                {
                    array[i, j] = scalarProcessor.ZeroScalar;

                    continue;
                }

                array[i, j] = scalarMapping(
                    scalarsArray[i, j]
                );
            }

            return array;
        }

        public static T[,] MapScalars<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray, Func<T, T> scalarMapping)
        {
            var rowsCount = scalarsArray.GetLength(0);
            var colsCount = scalarsArray.GetLength(1);

            var array = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
            {
                array[i, j] = scalarMapping(
                    scalarsArray[i, j] ?? scalarProcessor.ZeroScalar
                );
            }

            return array;
        }

        public static T[,] Add<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalarsArray2, 
                scalarProcessor.Add
            );
        }

        public static T[,] Add<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalar1, 
                scalarsArray2, 
                scalarProcessor.Add
            );
        }

        public static T[,] Add<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T scalar2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalar2,
                scalarProcessor.Add
            );
        }

        public static T[,] Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalarsArray2, 
                scalarProcessor.Subtract
            );
        }

        public static T[,] Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalar1, 
                scalarsArray2, 
                scalarProcessor.Subtract
            );
        }

        public static T[,] Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T scalar2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalar2,
                scalarProcessor.Subtract
            );
        }

        public static T[,] Times<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalarsArray2, 
                scalarProcessor.Times
            );
        }

        public static T[,] Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalar1, 
                scalarsArray2, 
                scalarProcessor.Times
            );
        }

        public static T[,] Times<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T scalar2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalar2,
                scalarProcessor.Times
            );
        }

        public static T[,] Divide<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalarsArray2, 
                scalarProcessor.Divide
            );
        }

        public static T[,] Divide<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalar1, 
                scalarsArray2, 
                scalarProcessor.Divide
            );
        }

        public static T[,] Divide<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T scalar2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalar2,
                scalarProcessor.Divide
            );
        }
        
        public static T[,] NotZeroScalarsInverse<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapNotZeroScalars(
                scalarsArray, 
                scalarProcessor.Inverse
            );
        }
        
        public static T[,] ScalarsNegative<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Negative
            );
        }

        public static T[,] ScalarsCos<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Cos
            );
        }

        public static T[,] ScalarsSin<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Sin
            );
        }

        public static T[,] ScalarsTan<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Tan
            );
        }

        public static T[,] ScalarsExp<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Exp
            );
        }

        public static T[,] ScalarsLog<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Log
            );
        }

        public static T[,] ScalarsLog10<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Log10
            );
        }

        public static T[,] ScalarsLog2<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Log2
            );
        }

        public static T[,] ScalarsAbs<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Abs
            );
        }

        public static T[,] ScalarsSqrt<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Sqrt
            );
        }

        public static T[,] ScalarsSqrtOfAbs<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.SqrtOfAbs
            );
        }

        
        public static IGaVectorStorage<T> ColumnToVectorStorage<T>(this T[,] scalarsArray, int colIndex, IGaScalarProcessor<T> scalarProcessor)
        {
            var rowsCount = scalarsArray.GetLength(0);

            var composer = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            for (var i = 0; i < rowsCount; i++)
            {
                var scalar = scalarsArray[i, colIndex] ?? scalarProcessor.ZeroScalar;

                if (!scalarProcessor.IsZero(scalar))
                    composer.SetTerm((ulong) i, scalar);
            }

            return composer.GetVectorStorage();
        }

        public static IGaVectorStorage<T> RowToVectorStorage<T>(this T[,] scalarsArray, int rowIndex, IGaScalarProcessor<T> scalarProcessor)
        {
            var colsCount = scalarsArray.GetLength(1);

            var composer = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            for (var j = 0; j < colsCount; j++)
            {
                var scalar = scalarsArray[rowIndex, j] ?? scalarProcessor.ZeroScalar;

                if (!scalarProcessor.IsZero(scalar))
                    composer.SetTerm((ulong) j, scalar);
            }

            return composer.GetVectorStorage();
        }

        public static Dictionary<ulong, IGaVectorStorage<T>> ColumnsToVectorStoragesDictionary<T>(this T[,] scalarsArray, IGaScalarProcessor<T> scalarProcessor)
        {
            var rowsCount = scalarsArray.GetLength(0);
            var colsCount = scalarsArray.GetLength(1);

            var vectorsDictionary = 
                new Dictionary<ulong, IGaVectorStorage<T>>();

            for (var j = 0; j < colsCount; j++)
            {
                var composer = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

                for (var i = 0; i < rowsCount; i++)
                {
                    var scalar = scalarsArray[i, j] ?? scalarProcessor.ZeroScalar;

                    if (!scalarProcessor.IsZero(scalar))
                        composer.SetTerm((ulong) i, scalar);
                }

                vectorsDictionary.Add((ulong) j, composer.GetVectorStorage());
            }

            return vectorsDictionary;
        }

        public static IGaVectorStorage<T>[] ColumnsToVectorStoragesArray<T>(this T[,] scalarsArray, IGaScalarProcessor<T> scalarProcessor)
        {
            var rowsCount = scalarsArray.GetLength(0);
            var colsCount = scalarsArray.GetLength(1);

            var vectorsArray = 
                new IGaVectorStorage<T>[colsCount];

            for (var j = 0; j < colsCount; j++)
            {
                var composer = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

                for (var i = 0; i < rowsCount; i++)
                {
                    var scalar = scalarsArray[i, j] ?? scalarProcessor.ZeroScalar;

                    if (!scalarProcessor.IsZero(scalar))
                        composer.SetTerm((ulong) i, scalar);
                }

                vectorsArray[j] = composer.GetVectorStorage();
            }

            return vectorsArray;
        }


        public static GaEuclideanSimpleRotor<T> ComplexEigenPairToEuclideanSimpleRotor<T>(this IGaScalarProcessor<T> scalarProcessor, T realValue, T imagValue, T[] realVector, T[] imagVector)
        {
            //var scalar = scalarProcessor.Add(
            //    scalarProcessor.Times(realValue, realValue),
            //    scalarProcessor.Times(imagValue, imagValue)
            //);

            var angle = scalarProcessor.ArcTan2(
                realValue, 
                imagValue
            );

            var blade = scalarProcessor.VectorsOp(
                realVector, 
                imagVector
            );

            return GaEuclideanSimpleRotor<T>.Create(angle, blade);

            //Console.WriteLine($"Eigen value real part: {realValue.GetLaTeXDisplayEquation()}");
            //Console.WriteLine();

            //Console.WriteLine($"Eigen value imag part: {imagValue.GetLaTeXDisplayEquation()}");
            //Console.WriteLine();

            //Console.WriteLine($"Eigen value length: {scalar.GetLaTeXDisplayEquation()}");
            //Console.WriteLine();

            //Console.WriteLine($"Eigen value angle: {angle.GetLaTeXDisplayEquation()}");
            //Console.WriteLine();

            //Console.WriteLine("Eigen vector real part:");
            //Console.WriteLine(realVector.TermsToLaTeX().GetLaTeXDisplayEquation());
            //Console.WriteLine();

            //Console.WriteLine("Eigen vector imag part:");
            //Console.WriteLine(imagVector.TermsToLaTeX().GetLaTeXDisplayEquation());
            //Console.WriteLine();

            //Console.WriteLine("Blade:");
            //Console.WriteLine(blade.ToLaTeXEquationsArray("B", @"\mu"));
            //Console.WriteLine();

            //Console.WriteLine("Final rotor:");
            //Console.WriteLine(rotor.ToLaTeXEquationsArray("R", @"\mu"));
            //Console.WriteLine();

            //Console.WriteLine($"Is simple rotor? {rotor.IsSimpleRotor()}");
            //Console.WriteLine();

            //Console.WriteLine();

            //return rotor;
        }
    }
}