using System;
using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Constants;
using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Samples.BasicMath
{
    public static class Rotations
    {
        public static void Execute()
        {
            var axisArray = new []
            {
                Axis3D.PositiveX,
                Axis3D.NegativeX,
                Axis3D.PositiveY,
                Axis3D.NegativeY,
                Axis3D.PositiveZ,
                Axis3D.NegativeZ
            };

            var vectorsArray = new []
            {
                new Tuple3D(1, 2, 3).ToUnitVector()
            };

            foreach (var vector in vectorsArray)
            {
                Console.WriteLine("Axis to vector rotations:");
                Console.WriteLine();

                foreach (var axis in axisArray)
                {
                    var matrix = SquareMatrix3.CreateAxisToVectorRotationMatrix3D(axis, vector);

                    Console.WriteLine($"Axis: {axis.GetVector3D()}");
                    Console.WriteLine($"Vector: {vector}");
                    Console.WriteLine($"Matrix: {matrix}");
                    Console.WriteLine($"Matrix determinant: {matrix.Determinant}");
                    Console.WriteLine();
                }

                Console.WriteLine("Vector to axis rotations:");
                Console.WriteLine();

                foreach (var axis in axisArray)
                {
                    var matrix = SquareMatrix3.CreateVectorToAxisRotationMatrix3D(vector, axis);

                    Console.WriteLine($"Axis: {axis.GetVector3D()}");
                    Console.WriteLine($"Vector: {vector}");
                    Console.WriteLine($"Matrix: {matrix}");
                    Console.WriteLine($"Matrix determinant: {matrix.Determinant}");
                    Console.WriteLine();
                }

            }
        }
    }
}
