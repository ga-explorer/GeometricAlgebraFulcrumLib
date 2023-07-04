using System.Diagnostics;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Frames.Space2D
{
    /// <summary>
    /// This class represents a directions frame of 2 orthonormal vectors U, V where
    /// the components are double precision numbers
    /// </summary>
    public class LinearFrame2D
    {
        /// <summary>
        /// Create a set of 2 right-handed orthonormal direction vectors from the given vector
        /// </summary>
        /// <returns></returns>
        public static LinearFrame2D CreateRightHanded(IFloat64Vector2D uDirection)
        {
            var s = uDirection.ENorm();

            Debug.Assert(!s.IsAlmostZero());

            s = 1.0d / s;
            return new LinearFrame2D(uDirection.X * s, uDirection.Y * s, true);
        }

        /// <summary>
        /// Create a set of 2 left-handed orthonormal direction vectors from the given vector
        /// </summary>
        /// <param name="uDirection"></param>
        /// <returns></returns>
        public static LinearFrame2D CreateLeftHanded(IFloat64Vector2D uDirection)
        {
            var s = uDirection.ENorm();

            Debug.Assert(!s.IsAlmostZero());

            s = 1.0d / s;
            return new LinearFrame2D(uDirection.X * s, uDirection.Y * s, false);
        }



        public double UDirectionX { get; }

        public double UDirectionY { get; }

        public double VDirectionX { get; }

        public double VDirectionY { get; }

        public Float64Vector2D UDirection
        {
            get { return Float64Vector2D.Create((Float64Scalar)UDirectionX, (Float64Scalar)UDirectionY); }
        }

        public Float64Vector2D VDirection
        {
            get { return Float64Vector2D.Create((Float64Scalar)VDirectionX, (Float64Scalar)VDirectionY); }
        }

        public bool IsRightHanded
        {
            get { return UDirection.Determinant(VDirection) > 0.0d; }
        }

        public bool IsLeftHanded
        {
            get { return UDirection.Determinant(VDirection) < 0.0d; }
        }

        public bool HasNaNComponent
        {
            get
            {
                return double.IsNaN(UDirectionX) || double.IsNaN(UDirectionY) ||
                       double.IsNaN(VDirectionX) || double.IsNaN(VDirectionY);
            }
        }


        public LinearFrame2D(IFloat64Vector2D uDirection, IFloat64Vector2D vDirection)
        {
            UDirectionX = uDirection.X;
            UDirectionY = uDirection.Y;

            VDirectionX = vDirection.X;
            VDirectionY = vDirection.Y;

            Debug.Assert(!HasNaNComponent);
        }

        private LinearFrame2D(double ux, double uy, bool rightHanded)
        {
            Debug.Assert((ux * ux + uy * uy).IsAlmostEqual(1.0d));

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

            Debug.Assert(!HasNaNComponent);
        }
    }
}