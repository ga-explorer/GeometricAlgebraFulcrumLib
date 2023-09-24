using System;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Samples.EuclideanGeometry
{
    public static class Euclidean3DNumericSample
    {
        public static void Execute()
        {
            var n = 3;
            var randGen = new Random();

            var processor =
                XGaFloat64Processor.Euclidean;
                //ScalarProcessorFloat64.DefaultProcessor.CreateXGafGeometricAlgebraEuclideanProcessor(n);

            var v = processor.CreateVector(
                randGen.NextDouble(), 
                randGen.NextDouble(),
                randGen.NextDouble()
            );

            v = v.DivideByENorm();

            var u = processor.CreateVector(
                randGen.NextDouble(), 
                randGen.NextDouble(),
                randGen.NextDouble()
            );

            u = u.DivideByENorm();


            var rotor = 
                v.CreatePureRotor(u);

            var rotorMv = rotor.Multivector;
            var rotorMvReverse = rotor.Multivector.Reverse();

            var rotorMatrix =
                rotor.GetMultivectorMapArray(3, 3);

            
            var rotorMatrix1 =
                n.CreateLinUnilinearMap(
                    (int index) =>
                        rotorMv.EGp(processor.CreateTermVector(index)).GetVectorPart().ToLinVector()
                    ).ToArray(n, n);

            var rotorMatrix2 =
                n.CreateLinUnilinearMap(
                        (int index) =>
                            processor.CreateTermVector(index).EGp(rotorMvReverse).GetVectorPart().ToLinVector()
                    ).ToArray(n, n);

            var rotorMatrix21 = 
                rotorMatrix2.Times(rotorMatrix1);

            var vMatrix = v.VectorToColumnArray2D(n);
            var uMatrix = u.VectorToColumnArray2D(n);

            var u1 = rotor.OmMap(v);
            var u2 = rotorMatrix.Times(vMatrix);
            var u3 = rotorMatrix21.Times(vMatrix);
            var u4 = rotorMatrix2.Times(rotorMatrix1, vMatrix);

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
            Console.WriteLine(u1.ToString());
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