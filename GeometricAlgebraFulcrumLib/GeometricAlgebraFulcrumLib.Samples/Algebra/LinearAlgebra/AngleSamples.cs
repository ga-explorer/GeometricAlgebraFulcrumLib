using System;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;

namespace GeometricAlgebraFulcrumLib.Samples.Algebra.LinearAlgebra
{
    public static class AngleSamples
    {
        public static void RangeExample1()
        {
            for (var angleValue = -1000d; angleValue <= 1000d; angleValue += 10d)
            {
                var angle = LinFloat64DirectedAngle.CreateFromDegrees(angleValue);

                Console.WriteLine($"{angleValue:G} => {angle}");
            }
        }

        public static void ValidationExample1()
        {
            for (var angleInDegrees = 0d; angleInDegrees <= 360; angleInDegrees += 1)
            {
                var angle = LinFloat64PolarAngle.CreateFromDegrees(angleInDegrees);
                var halfAngle = LinFloat64PolarAngle.CreateFromDegrees(angleInDegrees / 2d);
                var doubleAngle = LinFloat64PolarAngle.CreateFromDegrees(angleInDegrees * 2d);
                var tripleAngle = LinFloat64PolarAngle.CreateFromDegrees(angleInDegrees * 3d);

                var (angleCos, angleSin) = angle;
                var (halfAngleCos, halfAngleSin) = halfAngle;
                var (doubleAngleCos, doubleAngleSin) = doubleAngle;
                var (tripleAngleCos, tripleAngleSin) = tripleAngle;

                var (angleCos1, angleSin1) = angle.ToPolarAngle();
                var (halfAngleCos1, halfAngleSin1) = angle.HalfPolarAngle();
                var (doubleAngleCos1, doubleAngleSin1) = angle.DoublePolarAngle();
                var (tripleAngleCos1, tripleAngleSin1) = angle.TriplePolarAngle();

                var (angleCos2, angleSin2) = angle.ToDirectedAngle();
                var (halfAngleCos2, halfAngleSin2) = angle.HalfDirectedAngle();
                var (doubleAngleCos2, doubleAngleSin2) = angle.DoubleDirectedAngle();
                var (tripleAngleCos2, tripleAngleSin2) = angle.TripleDirectedAngle();

                Debug.Assert(
                    angleCos1.IsNearEqual(angleCos)
                );

                Debug.Assert(
                    angleSin1.IsNearEqual(angleSin)
                );

                Debug.Assert(
                    halfAngleCos1.IsNearEqual(halfAngleCos)
                );

                Debug.Assert(
                    halfAngleSin1.IsNearEqual(halfAngleSin)
                );

                Debug.Assert(
                    doubleAngleCos1.IsNearEqual(doubleAngleCos)
                );

                Debug.Assert(
                    doubleAngleSin1.IsNearEqual(doubleAngleSin)
                );

                Debug.Assert(
                    tripleAngleCos1.IsNearEqual(tripleAngleCos)
                );

                Debug.Assert(
                    tripleAngleSin1.IsNearEqual(tripleAngleSin)
                );


                Debug.Assert(
                    angleCos2.IsNearEqual(angleCos)
                );

                Debug.Assert(
                    angleSin2.IsNearEqual(angleSin)
                );

                Debug.Assert(
                    halfAngleCos2.IsNearEqual(halfAngleCos)
                );

                Debug.Assert(
                    halfAngleSin2.IsNearEqual(halfAngleSin)
                );

                Debug.Assert(
                    doubleAngleCos2.IsNearEqual(doubleAngleCos)
                );

                Debug.Assert(
                    doubleAngleSin2.IsNearEqual(doubleAngleSin)
                );

                Debug.Assert(
                    tripleAngleCos2.IsNearEqual(tripleAngleCos)
                );

                Debug.Assert(
                    tripleAngleSin2.IsNearEqual(tripleAngleSin)
                );

                var rangeArray = new[]
                {
                    LinAngleRange.Symmetric360,
                    LinAngleRange.Symmetric180,
                    LinAngleRange.Positive360,
                    LinAngleRange.Negative360
                };

                foreach (var range in rangeArray)
                {
                    (angleCos1, angleSin1) = angle.ToPolarAngle();
                    (halfAngleCos1, halfAngleSin1) = angle.HalfPolarAngle(range);
                    (doubleAngleCos1, doubleAngleSin1) = angle.DoublePolarAngle(range);
                    (tripleAngleCos1, tripleAngleSin1) = angle.TriplePolarAngle(range);

                    (angleCos2, angleSin2) = angle.ToDirectedAngle(range);
                    (halfAngleCos2, halfAngleSin2) = angle.HalfDirectedAngle(range);
                    (doubleAngleCos2, doubleAngleSin2) = angle.DoubleDirectedAngle(range);
                    (tripleAngleCos2, tripleAngleSin2) = angle.TripleDirectedAngle(range);


                    Debug.Assert(
                        angleCos1.IsNearEqual(angleCos)
                    );

                    Debug.Assert(
                        angleSin1.IsNearEqual(angleSin)
                    );

                    Debug.Assert(
                        halfAngleCos1.IsNearEqual(halfAngleCos)
                    );

                    Debug.Assert(
                        halfAngleSin1.IsNearEqual(halfAngleSin)
                    );

                    Debug.Assert(
                        doubleAngleCos1.IsNearEqual(doubleAngleCos)
                    );

                    Debug.Assert(
                        doubleAngleSin1.IsNearEqual(doubleAngleSin)
                    );

                    Debug.Assert(
                        tripleAngleCos1.IsNearEqual(tripleAngleCos)
                    );

                    Debug.Assert(
                        tripleAngleSin1.IsNearEqual(tripleAngleSin)
                    );


                    Debug.Assert(
                        angleCos2.IsNearEqual(angleCos)
                    );

                    Debug.Assert(
                        angleSin2.IsNearEqual(angleSin)
                    );

                    Debug.Assert(
                        halfAngleCos2.IsNearEqual(halfAngleCos)
                    );

                    Debug.Assert(
                        halfAngleSin2.IsNearEqual(halfAngleSin)
                    );

                    Debug.Assert(
                        doubleAngleCos2.IsNearEqual(doubleAngleCos)
                    );

                    Debug.Assert(
                        doubleAngleSin2.IsNearEqual(doubleAngleSin)
                    );

                    Debug.Assert(
                        tripleAngleCos2.IsNearEqual(tripleAngleCos)
                    );

                    Debug.Assert(
                        tripleAngleSin2.IsNearEqual(tripleAngleSin)
                    );

                }
            }
        }

        public static void QuadrantValidationExample()
        {
            var randomGen = new Random(10);

            for (var i = 0; i < 1000; i++)
            {
                var angle = (randomGen.NextDouble() * 2 - 1) * 360;

                var q1 = GetQuadrant1(angle.DegreesToRadians());
                var q2 = GetQuadrant2(angle.DegreesToRadians());

                Debug.Assert(q1 == q2);
            }

            return;

            int GetQuadrant1(double angle)
            {
                if (angle >= 0)
                {
                    return angle switch
                    {
                        <= Math.PI / 2 => 0,
                        <= Math.PI => 1,
                        <= Math.PI * 3 / 2 => 2,
                        _ => 3
                    };
                }

                return -angle switch
                {
                    <= Math.PI / 2 => 3,
                    <= Math.PI => 2,
                    <= Math.PI * 3 / 2 => 1,
                    _ => 0
                };
            }

            int GetQuadrant2(double angle)
            {
                return ((int)Math.Floor(2 * angle / Math.PI) % 4 + 4) % 4;
            }
        }
    }
}
