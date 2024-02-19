using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Samples.Graphics.BasicMath;

public static class Rotations
{
    public static void Execute()
    {
        var axisArray = new []
        {
            LinUnitBasisVector3D.PositiveX,
            LinUnitBasisVector3D.NegativeX,
            LinUnitBasisVector3D.PositiveY,
            LinUnitBasisVector3D.NegativeY,
            LinUnitBasisVector3D.PositiveZ,
            LinUnitBasisVector3D.NegativeZ
        };

        var vectorsArray = new []
        {
            Float64Vector3D.Create(1, 2, 3).ToUnitVector()
        };

        foreach (var vector in vectorsArray)
        {
            Console.WriteLine("Axis to vector rotations:");
            Console.WriteLine();

            foreach (var axis in axisArray)
            {
                var matrix = SquareMatrix3.CreateAxisToVectorRotationMatrix3D(axis, vector);

                Console.WriteLine($"Axis: {axis.ToVector3D()}");
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

                Console.WriteLine($"Axis: {axis.ToVector3D()}");
                Console.WriteLine($"Vector: {vector}");
                Console.WriteLine($"Matrix: {matrix}");
                Console.WriteLine($"Matrix determinant: {matrix.Determinant}");
                Console.WriteLine();
            }

        }
    }
}