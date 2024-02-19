using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Samples.Geometry
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/Snellius%E2%80%93Pothenot_problem
    /// https://github.com/jorgeven98/Resection-Problem-3D/blob/main/pothenot_GA.ipynb
    /// </summary>
    public static class SnelliusPothenotProblemSamples
    {
        public static Tuple<Triplet<Float64Vector3D>, Triplet<Float64PlanarAngle>> GetAngles(Float64Vector3D pointA, Float64Vector3D pointB, Float64Vector3D pointC, Float64Vector3D pointP)
        {
            var ua = Float64Vector2D.Create(pointA.X - pointP.X, pointA.Y - pointP.Y);
            var ub = Float64Vector2D.Create(pointB.X - pointP.X, pointB.Y - pointP.Y);
            var uc = Float64Vector2D.Create(pointC.X - pointP.X, pointC.Y - pointP.Y);

            var thetaUa = ua.GetPolarAngle();
            var thetaUb = ub.GetPolarAngle();
            var thetaUc = uc.GetPolarAngle();

            Console.WriteLine(
                $"Angles ----> A: {thetaUa}, B: {thetaUb}, C: {thetaUc}"
            );

            var alpha = thetaUa - thetaUb;
            var beta = thetaUb - thetaUc;

            var a = pointA;
            var b = pointB;
            var c = pointC;

            // If P is aligned with A and B or B and C, a new assignment of
            // the points is necessary.
            if (beta.IsZeroOrFullRotation())
            {
                alpha = thetaUb - thetaUa;
                beta = thetaUa - thetaUc;

                Console.WriteLine(
                    "The angle beta = 0 ---> The position of A and B will be interchanged."
                );

                (a, b) = (b, a);
            }

            if (alpha.IsZeroOrFullRotation())
            {
                alpha = thetaUa - thetaUc;
                beta = thetaUc - thetaUb;

                Console.WriteLine(
                    "The angle alpha = 0 ---> The position of C and B will be interchanged."
                );

                (c, b) = (b, c);
            }

            // if we are working in 2 dimensions, gamma is not used.
            //var gamma = Float64PlanarAngle.Angle0;

            var gamma = Math.Atan2(
                pointA.Z - pointP.Z,
                ua.Norm()
            );

            return new Tuple<Triplet<Float64Vector3D>, Triplet<Float64PlanarAngle>>(
                new Triplet<Float64Vector3D>(a, b, c),
                new Triplet<Float64PlanarAngle>(alpha, beta, gamma)
            );
        }


        public static void Example1()
        {
            // Just for initializing GA lookup tables before computations start
            var ga = RGaConformalSpace4D.Instance;

            var data = SnelliusPothenotData2D.Create(
                Float64Vector2D.Create(-4, 2),
                Float64Vector2D.Create(0, 10),
                Float64Vector2D.Create(10, 8),
                Float64Vector2D.Create(2, 2)
            );

            //var data = SnelliusPothenotData2D.Create(
            //    Float64Vector2D.Create(-7, -1),
            //    Float64Vector2D.Create(1, 5),
            //    Float64Vector2D.Create(15.6, 6),
            //    Float64Vector2D.Create(3.12, -18.5)
            //);

            //var data = SnelliusPothenotData2D.Create(
            //    Float64Vector2D.Create(5, 6), 
            //    Float64Vector2D.Create(0, 1),
            //    Float64Vector2D.Create(3, 12),
            //    Float64Vector2D.Create(8, -3)
            //);

            var p1 = data.SolveUsingVGa();
            var p2 = data.SolveUsingCGaCassini();
            var p3 = data.SolveUsingCGaCollins();

            Console.WriteLine(data);
            Console.WriteLine($"VGA method result        : {p1.ToTupleString()}");
            Console.WriteLine($"CGA Cassini method result: {p2.ToTupleString()}");
            Console.WriteLine($"CGA Collins method result: {p3.ToTupleString()}");
            Console.WriteLine();
        }

    }
}
