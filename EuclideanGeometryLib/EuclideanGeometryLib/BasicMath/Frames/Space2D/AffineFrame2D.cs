using System.Diagnostics;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicOperations;

namespace EuclideanGeometryLib.BasicMath.Frames.Space2D
{
    /// <summary>
    /// This class represents a directions frame of 2 orthonormal vectors U, V where
    /// the components are double precision numbers
    /// </summary>
    public class AffineFrame2D
    {
        /// <summary>
        /// Create a right-handed orthonormal affine frame from the given origin and vector
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="uDirection"></param>
        /// <returns></returns>
        public static AffineFrame2D CreateRightHanded(ITuple2D origin, ITuple2D uDirection)
        {
            Debug.Assert(!origin.IsInvalid && !uDirection.IsInvalid);

            var s = uDirection.GetLength();

            Debug.Assert(!s.IsAlmostZero());

            s = 1.0d / s;
            return new AffineFrame2D(
                origin.X, origin.Y,
                uDirection.X * s, uDirection.Y * s, 
                true
            );
        }

        /// <summary>
        /// Create a left-handed orthonormal affine frame from the given origin and vector
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="uDirection"></param>
        /// <returns></returns>
        public static AffineFrame2D CreateLeftHanded(ITuple2D origin, ITuple2D uDirection)
        {
            Debug.Assert(!origin.IsInvalid && !uDirection.IsInvalid);

            var s = uDirection.GetLength();

            Debug.Assert(!s.IsAlmostZero());

            s = 1.0d / s;
            return new AffineFrame2D(
                origin.X, origin.Y,
                uDirection.X * s, uDirection.Y * s,
                true
            );
        }



        public double OriginX { get; }

        public double OriginY { get; }

        public double UDirectionX { get; }

        public double UDirectionY { get; }

        public double VDirectionX { get; }

        public double VDirectionY { get; }

        public Tuple2D UDirection
            => new Tuple2D(UDirectionX, UDirectionY);

        public Tuple2D VDirection
            => new Tuple2D(VDirectionX, VDirectionY);

        public bool IsRightHanded
            => VectorAlgebraUtils.Determinant(UDirection, VDirection) > 0.0d;

        public bool IsLeftHanded
            => VectorAlgebraUtils.Determinant(UDirection, VDirection) < 0.0d;

        public bool HasNaNComponent
            => double.IsNaN(OriginX) || double.IsNaN(OriginY) || 
               double.IsNaN(UDirectionX) || double.IsNaN(UDirectionY) ||
               double.IsNaN(VDirectionX) || double.IsNaN(VDirectionY);


        private AffineFrame2D(double ox, double oy, double ux, double uy, bool rightHanded)
        {
            Debug.Assert((ux * ux + uy * uy).IsAlmostEqual(1.0d));

            OriginX = ox;
            OriginY = oy;
            UDirectionX = ux;
            UDirectionY = uy;

            if (rightHanded)
            {
                VDirectionX = -uy;
                VDirectionY = ux;
            }
            else
            {
                VDirectionX = uy;
                VDirectionY = -ux;
            }
        }
    }
}
