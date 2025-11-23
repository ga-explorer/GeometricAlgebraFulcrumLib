using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space2D
{
    public static class LinFloat64FrameInterpolationUtils
    {
        /// <summary>
        /// Interpolate between two vectors in 2D where the length
        /// is linearly interpolated and the angle is rotationally
        /// interpolated counter-clockwise
        /// </summary>
        /// <param name="t"></param>
        /// <param name="u1"></param>
        /// <param name="u2"></param>
        /// <returns></returns>
        public static LinFloat64Vector2D InterpolateCcw(this double t, LinFloat64Vector2D u1, LinFloat64Vector2D u2)
        {
            var length = double.Lerp(
                u1.L2Norm(),
                u2.L2Norm(),
                t
            );

            // Step 1: angles
            var theta1 = Math.Atan2(u1.Y, u1.X);
            var theta2 = Math.Atan2(u2.Y, u2.X);

            // Step 2: CCW difference
            var delta = theta2 - theta1;
            
            // force CCW
            if (delta < 0)
                delta += 2 * Math.PI; 

            // Step 3: interpolated angle
            var theta = theta1 + t * delta;

            // Step 4: return interpolated vector
            return LinFloat64Vector2D.Create(
                length * Math.Cos(theta), 
                length * Math.Sin(theta)
            );
        }

    }
}
