using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;

namespace GeometricAlgebraFulcrumLib.Core.Samples.Modeling.Geometry
{
    public static class PlanarAngleSamples
    {
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
