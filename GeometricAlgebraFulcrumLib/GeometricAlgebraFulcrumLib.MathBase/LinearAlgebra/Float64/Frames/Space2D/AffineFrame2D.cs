using System.Diagnostics;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Frames.Space2D
{
    /// <summary>
    /// This class represents a directions frame of 2 orthonormal vectors U, V where
    /// the components are double precision numbers
    /// </summary>
    public class AffineFrame2D
    {
        public static AffineFrame2D Create(IFloat64Tuple2D origin, IFloat64Tuple2D uDirection, IFloat64Tuple2D vDirection)
        {
            return new AffineFrame2D(
                origin.X,
                origin.Y,
                uDirection.X,
                uDirection.Y,
                vDirection.X,
                vDirection.Y
            );
        }

        /// <summary>
        /// Create a right-handed orthonormal affine frame from the given origin and vector
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="uDirection"></param>
        /// <returns></returns>
        public static AffineFrame2D CreateRightHanded(IFloat64Tuple2D origin, IFloat64Tuple2D uDirection)
        {
            Debug.Assert(origin.IsValid() && uDirection.IsValid());

            var s = uDirection.ENorm();

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
        public static AffineFrame2D CreateLeftHanded(IFloat64Tuple2D origin, IFloat64Tuple2D uDirection)
        {
            var s = uDirection.ENorm();

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

        public Float64Vector2D Origin
            => new Float64Vector2D(OriginX, OriginY);

        public Float64Vector2D UDirection
            => new Float64Vector2D(UDirectionX, UDirectionY);

        public Float64Vector2D VDirection
            => new Float64Vector2D(VDirectionX, VDirectionY);

        public bool IsRightHanded
            => UDirection.Determinant(VDirection) > 0.0d;

        public bool IsLeftHanded
            => UDirection.Determinant(VDirection) < 0.0d;


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

        private AffineFrame2D(double ox, double oy, double ux, double uy, double vx, double vy)
        {
            OriginX = ox;
            OriginY = oy;

            UDirectionX = ux;
            UDirectionY = uy;

            VDirectionX = vx;
            VDirectionY = vy;
        }


        public bool IsValid()
        {
            return !double.IsNaN(OriginX) &&
                   !double.IsNaN(OriginY) &&
                   !double.IsNaN(UDirectionX) &&
                   !double.IsNaN(UDirectionY) &&
                   !double.IsNaN(VDirectionX) &&
                   !double.IsNaN(VDirectionY);
        }
    }
}
