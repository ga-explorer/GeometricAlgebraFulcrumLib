using System;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;

namespace GraphicsComposerLib.Samples.BasicMath
{
    public static class Angles
    {
        public static void Execute()
        {
            for (var angleValue = -1000d; angleValue <= 1000d; angleValue += 10d)
            {
                var angle = Float64PlanarAngle.CreateFromDegrees(angleValue).ClampNegative();

                Console.WriteLine($"{angleValue:G} => {angle}");
            }
        }
    }
}