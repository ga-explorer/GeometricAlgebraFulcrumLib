using System;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Samples.Algebra.LinearAlgebra;

public static class RotationSamples
{
    public static void Execute()
    {
        var axisArray = new[]
        {
            LinUnitBasisVector3D.PositiveX,
            LinUnitBasisVector3D.NegativeX,
            LinUnitBasisVector3D.PositiveY,
            LinUnitBasisVector3D.NegativeY,
            LinUnitBasisVector3D.PositiveZ,
            LinUnitBasisVector3D.NegativeZ
        };

        var vectorsArray = new[]
        {
            LinFloat64Vector3D.Create(1, 2, 3).ToUnitLinVector3D()
        };

        foreach (var vector in vectorsArray)
        {
            Console.WriteLine("Axis to vector rotations:");
            Console.WriteLine();

            foreach (var axis in axisArray)
            {
                var matrix = SquareMatrix3.CreateAxisToVectorRotationMatrix3D(axis, vector);

                Console.WriteLine($"Axis: {axis.ToLinVector3D()}");
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

                Console.WriteLine($"Axis: {axis.ToLinVector3D()}");
                Console.WriteLine($"Vector: {vector}");
                Console.WriteLine($"Matrix: {matrix}");
                Console.WriteLine($"Matrix determinant: {matrix.Determinant}");
                Console.WriteLine();
            }

        }
    }
}