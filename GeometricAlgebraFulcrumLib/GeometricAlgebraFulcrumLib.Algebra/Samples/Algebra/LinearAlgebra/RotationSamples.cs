using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Algebra.Samples.Algebra.LinearAlgebra;

public static class RotationSamples
{
    public static void Execute()
    {
        var axisArray = new[]
        {
            LinBasisVector3D.Px,
            LinBasisVector3D.Nx,
            LinBasisVector3D.Py,
            LinBasisVector3D.Ny,
            LinBasisVector3D.Pz,
            LinBasisVector3D.Nz
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

    public static void ValidateVectorToVectorRotationAxisAngle()
    {
        const double zeroEpsilon = 1e-6;

        var randGen = new Random(10);

        for (var i = 0; i < 1000000; i++)
        {
            // General case
            var v1 = randGen.GetUnitLinVector3D();
            var v2 = randGen.GetUnitLinVector3D();

            var (axis, angle) =
                v1.VectorToVectorRotationAxisAngle(v2);

            var v3 = v1.RotateVectorUsingAxisAngle(axis, angle);

            Debug.Assert(axis.IsNearOrthogonalTo(v1, zeroEpsilon));
            Debug.Assert(axis.IsNearOrthogonalTo(v2, zeroEpsilon));
            Debug.Assert(axis.IsNearUnitVector(zeroEpsilon));
            Debug.Assert(v2.IsNearEqual(v3, zeroEpsilon));


            // Special case: v2 = v1
            v2 = v1;
            (axis, angle) =
                v1.VectorToVectorRotationAxisAngle(v2);

            v3 = v1.RotateVectorUsingAxisAngle(axis, angle);

            Debug.Assert(axis.IsNearOrthogonalTo(v1, zeroEpsilon));
            Debug.Assert(axis.IsNearOrthogonalTo(v2, zeroEpsilon));
            Debug.Assert(axis.IsNearUnitVector(zeroEpsilon));
            Debug.Assert(v2.IsNearEqual(v3, zeroEpsilon));
            

            // Special case: v2 = -v1
            v2 = -v1;
            (axis, angle) =
                v1.VectorToVectorRotationAxisAngle(v2);

            v3 = v1.RotateVectorUsingAxisAngle(axis, angle);

            Debug.Assert(axis.IsNearOrthogonalTo(v1, zeroEpsilon));
            Debug.Assert(axis.IsNearOrthogonalTo(v2, zeroEpsilon));
            Debug.Assert(axis.IsNearUnitVector(zeroEpsilon));
            Debug.Assert(v2.IsNearEqual(v3, zeroEpsilon));
        }
    }

    public static void ValidateVectorToVectorRotationQuaternion()
    {
        var randGen = new Random(10);

        for (var i = 0; i < 1000000; i++)
        {
            // General case
            var v1 = randGen.GetUnitLinVector3D();
            var v2 = randGen.GetUnitLinVector3D();

            var quaternion = v1.VectorToVectorRotationQuaternion(v2);

            Debug.Assert(
                quaternion.IsNearNormalized()
            );

            var v3 = v1.RotateVectorUsingQuaternion(quaternion);

            Debug.Assert(
                v2.IsNearEqual(v3)
            );


            // Special case: v2 = v1
            v2 = v1;
            quaternion = v1.VectorToVectorRotationQuaternion(v2);

            Debug.Assert(
                quaternion.IsNearNormalized()
            );

            v3 = v1.RotateVectorUsingQuaternion(quaternion);

            Debug.Assert(
                v2.IsNearEqual(v3)
            );


            // Special case: v2 = -v1
            v2 = -v1;
            quaternion = v1.VectorToVectorRotationQuaternion(v2);

            Debug.Assert(
                quaternion.IsNearNormalized()
            );

            v3 = v1.RotateVectorUsingQuaternion(quaternion);

            Debug.Assert(
                v2.IsNearEqual(v3)
            );
        }
    }
}