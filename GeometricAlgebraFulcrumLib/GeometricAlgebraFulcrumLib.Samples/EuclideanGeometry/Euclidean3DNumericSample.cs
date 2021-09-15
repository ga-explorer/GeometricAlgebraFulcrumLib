using System;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Samples.EuclideanGeometry
{
    public static class Euclidean3DNumericSample
    {
        public static void Execute()
        {
            var n = 3U;
            var randGen = new Random();
            
            var processor = 
                ScalarAlgebraFloat64Processor.DefaultProcessor.CreateGeometricAlgebraEuclideanProcessor(n);

            VectorStorage<double> v = ScalarAlgebraFloat64ProcessorUtils.CreateVector(
                randGen.NextDouble(), 
                randGen.NextDouble(),
                randGen.NextDouble()
            );

            v = processor.DivideByENorm(v);

            VectorStorage<double> u = ScalarAlgebraFloat64ProcessorUtils.CreateVector(
                randGen.NextDouble(), 
                randGen.NextDouble(),
                randGen.NextDouble()
            );

            u = processor.DivideByENorm(u);


            var rotor = 
                processor.CreateEuclideanRotor(v, u);

            var rotorMv = rotor.Multivector;
            var rotorMvReverse = processor.Reverse(rotor.Multivector);

            var rotorMatrix =
                rotor.GetMatrix(3, 3);

            
            var rotorMatrix1 =
                processor.CreateComputedOutermorphism((int) n,
                        basisVector =>
                            processor.EGp(rotorMv, basisVector).GetVectorPart()
                    )
                    .GetMatrix((int) n, (int) n);

            var rotorMatrix2 =
                processor.CreateComputedOutermorphism((int) n,
                        basisVector =>
                            processor.EGp(basisVector, rotorMvReverse).GetVectorPart()
                    )
                    .GetMatrix((int) n, (int) n);

            var rotorMatrix21 = 
                rotorMatrix2 * rotorMatrix1;

            var vMatrix = v.VectorToColumnVectorMatrix(n);
            var uMatrix = u.VectorToColumnVectorMatrix(n);

            var u1 = rotor.OmMapVector(v);
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
            //    GeoFloat64Utils.MatrixProcessor.MatrixProduct(
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