﻿using System;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Generic;
using GeometricAlgebraFulcrumLib.Processing.Implementations.Float64;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Samples.EuclideanGeometry
{
    public static class Euclidean3DNumericSample
    {
        public static void Execute()
        {
            var n = 3U;
            var randGen = new Random();
            var processor = GaProcessorGenericOrthonormal<double>.CreateEuclidean(
                GaScalarProcessorFloat64.DefaultProcessor, 
                n
            );

            IGasVector<double> v = GaFloat64Utils.CreateVector(
                randGen.NextDouble(), 
                randGen.NextDouble(),
                randGen.NextDouble()
            );

            v = v.Divide(v.ENorm()).GetVectorPart();

            IGasVector<double> u = GaFloat64Utils.CreateVector(
                randGen.NextDouble(), 
                randGen.NextDouble(),
                randGen.NextDouble()
            );

            u = u.Divide(u.ENorm()).GetVectorPart();


            var rotor = 
                GaEuclideanSimpleRotor<double>.Create(processor, v, u);

            var rotorMv = rotor.Rotor;
            var rotorMvReverse = rotor.Rotor.GetReverse();

            var rotorMatrix =
                rotor.GetMatrix(3, 3);

            
            var rotorMatrix1 =
                processor.CreateComputedOutermorphism((int) n,
                        basisVector =>
                            rotorMv.EGp(basisVector).GetVectorPart()
                    )
                    .GetMatrix((int) n, (int) n);

            var rotorMatrix2 =
                processor.CreateComputedOutermorphism((int) n,
                        basisVector =>
                            basisVector.EGp(rotorMvReverse).GetVectorPart()
                    )
                    .GetMatrix((int) n, (int) n);

            var rotorMatrix21 = 
                rotorMatrix2 * rotorMatrix1;

            var vMatrix = v.VectorToColumnVectorMatrix(n);
            var uMatrix = u.VectorToColumnVectorMatrix(n);

            var u1 = rotor.MapVector(v);
            var u2 = rotorMatrix * vMatrix;
            var u3 = rotorMatrix21 * vMatrix;
            var u4 = rotorMatrix2 * (rotorMatrix1 * vMatrix);

            Console.WriteLine("Rotor Matrix:");
            Console.WriteLine(rotorMatrix.ToString());
            Console.WriteLine();

            Console.WriteLine("Rotor Matrix 1:");
            Console.WriteLine(rotorMatrix1.ToString());
            Console.WriteLine();

            Console.WriteLine("Rotor Matrix 2:");
            Console.WriteLine(rotorMatrix2.ToString());
            Console.WriteLine();

            Console.WriteLine("Rotor Matrix2 * Matrix1:");
            Console.WriteLine(rotorMatrix21.ToString());
            Console.WriteLine();

            Console.WriteLine("v Vector Matrix:");
            Console.WriteLine(vMatrix.ToString());
            Console.WriteLine();

            Console.WriteLine("u Vector Matrix:");
            Console.WriteLine(uMatrix.ToString());
            Console.WriteLine();

            Console.WriteLine("Rotated Vector 1:");
            Console.WriteLine(u1.GetText());
            Console.WriteLine();

            Console.WriteLine("Rotated Vector 2:");
            Console.WriteLine(u2.ToString());
            Console.WriteLine();

            Console.WriteLine("Rotated Vector 3:");
            Console.WriteLine(u3.ToString());
            Console.WriteLine();

            Console.WriteLine("Rotated Vector 4:");
            Console.WriteLine(u4.ToString());
            Console.WriteLine();

            //Console.WriteLine($"v = {v.GetText()}");
            //Console.WriteLine();

            //Console.WriteLine($"u = {u.GetText()}");
            //Console.WriteLine();

            //Console.WriteLine("Rotation Array =");
            //Console.WriteLine(rotorArray.GetText());
            //Console.WriteLine();

            //var vMatrix = v.VectorToColumnVectorMatrix(3);
            //var uMatrix = u.VectorToColumnVectorMatrix(3);
            //var rotorMatrix = rotorArray.ArrayToMatrix();

            //var uMatrix1 =
            //    GaFloat64Utils.MatrixProcessor.MatrixProduct(
            //        rotorMatrix,
            //        vMatrix
            //    );


            //Console.WriteLine("u Matrix =");
            //Console.WriteLine(uMatrix.ToArray().GetText());
            //Console.WriteLine();

            //Console.WriteLine("u Matrix 1 =");
            //Console.WriteLine(uMatrix1.ToArray().GetText());
            //Console.WriteLine();
        }
    }
}