using System;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Samples.EuclideanGeometry
{
    public static class QuaternionSamples
    {
        public static void Example1()
        {
            var axis = LinUnitBasisVector3D.PositiveX;
            var angle = Float64PlanarAngle.Angle120;

            var q1 = Float64Quaternion.CreateFromAxisAngle(axis, angle);

            Console.WriteLine(q1);
            Console.WriteLine();

            var u = Float64Vector3D.E2;
            var v = q1.RotateVector(u);

            Console.WriteLine(u);
            Console.WriteLine(v);
            Console.WriteLine();
        }
    }
}
