using System;
using System.Diagnostics;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Frames.Space3D
{
    /// <summary>
    /// This class represents a directions frame of 3 orthonormal vectors U, V, W where
    /// the components are double precision numbers
    /// </summary>
    public class LinearFrame3D
    {
        /// <summary>
        /// Create a set of 3 right-handed orthonormal direction vectors from the given vector
        /// </summary>
        /// <param name="uDirection"></param>
        /// <returns></returns>
        public static LinearFrame3D CreateRightHanded(ITuple3D uDirection)
        {
            var s = uDirection.GetLength();

            Debug.Assert(!s.IsAlmostZero());

            s = 1.0d / s;
            return new LinearFrame3D(
                uDirection.X * s, 
                uDirection.Y * s, 
                uDirection.Z * s, 
                true
            );
        }

        /// <summary>
        /// Create a set of 3 left-handed orthonormal direction vectors from the given vector
        /// </summary>
        /// <param name="uDirection"></param>
        /// <returns></returns>
        public static LinearFrame3D CreateLeftHanded(ITuple3D uDirection)
        {
            var s = uDirection.GetLength();

            Debug.Assert(!s.IsAlmostZero());

            s = 1.0d / s;
            return new LinearFrame3D(
                uDirection.X * s, 
                uDirection.Y * s, 
                uDirection.Z * s, 
                false
            );
        }


        public double UDirectionX { get; }

        public double UDirectionY { get; }

        public double UDirectionZ { get; }

        public double VDirectionX { get; }

        public double VDirectionY { get; }

        public double VDirectionZ { get; }

        public double WDirectionX { get; }

        public double WDirectionY { get; }

        public double WDirectionZ { get; }

        public Tuple3D UDirection 
            => new Tuple3D(UDirectionX, UDirectionY, UDirectionZ);

        public Tuple3D VDirection 
            => new Tuple3D(VDirectionX, VDirectionY, VDirectionZ);

        public Tuple3D WDirection 
            => new Tuple3D(WDirectionX, WDirectionY, WDirectionZ);

        public bool IsRightHanded 
            => VectorUtils.Determinant(UDirection, VDirection, WDirection) > 0.0d;

        public bool IsLeftHanded 
            => VectorUtils.Determinant(UDirection, VDirection, WDirection) < 0.0d;

        public bool HasNaNComponent =>
            double.IsNaN(UDirectionX) ||
            double.IsNaN(UDirectionY) ||
            double.IsNaN(UDirectionZ) ||
            double.IsNaN(VDirectionX) ||
            double.IsNaN(VDirectionY) ||
            double.IsNaN(VDirectionZ) ||
            double.IsNaN(WDirectionX) ||
            double.IsNaN(WDirectionY) ||
            double.IsNaN(WDirectionZ);


        public LinearFrame3D(ITuple3D uDirection, ITuple3D vDirection, ITuple3D wDirection)
        {
            UDirectionX = uDirection.X;
            UDirectionY = uDirection.Y;
            UDirectionZ = uDirection.Z;

            VDirectionX = vDirection.X;
            VDirectionY = vDirection.Y;
            VDirectionZ = vDirection.Z;

            WDirectionX = wDirection.X;
            WDirectionY = wDirection.Y;
            WDirectionZ = wDirection.Z;

            Debug.Assert(!HasNaNComponent);
        }

        private LinearFrame3D(double ux, double uy, double uz, bool rightHanded)
        {
            Debug.Assert(
                (ux * ux + uy * uy + uz * uz).IsAlmostEqual(1.0d)
            );

            UDirectionX = ux;
            UDirectionY = uy;
            UDirectionZ = uz;

            double vLength;
            var absUx = Math.Abs(ux);
            var absUy = Math.Abs(uy);
            var absUz = Math.Abs(uz);

            if (absUx < absUy && absUx < absUz)
            {
                //Ux is the smallest component in magnitude, make it zero in V
                vLength = uy * uy + uz * uz;
                VDirectionX = 0.0d;
                VDirectionY = -uz / vLength;
                VDirectionZ = uy / vLength;
            }
            else if (absUy < absUx && absUy < absUz)
            {
                //Uy is the smallest component in magnitude, make it zero in V
                vLength = ux * ux + uz * uz;
                VDirectionX = uz / vLength;
                VDirectionY = 0.0d;
                VDirectionZ = -ux / vLength;
            }
            else
            {
                //Uz is the smallest component in magnitude, make it zero in V
                vLength = ux * ux + uy * uy;
                VDirectionX = -uy / vLength;
                VDirectionY = ux / vLength;
                VDirectionZ = 0.0d;
            }

            //Compute W as the cross product of U and V
            if (rightHanded)
            {
                WDirectionX = UDirectionY * VDirectionZ - UDirectionZ * VDirectionY;
                WDirectionY = UDirectionZ * VDirectionX - UDirectionX * VDirectionZ;
                WDirectionZ = UDirectionX * VDirectionY - UDirectionY * VDirectionX;
            }
            else
            {
                WDirectionX = UDirectionZ * VDirectionY - UDirectionY * VDirectionZ;
                WDirectionY = UDirectionX * VDirectionZ - UDirectionZ * VDirectionX;
                WDirectionZ = UDirectionY * VDirectionX - UDirectionX * VDirectionY;
            }

            Debug.Assert(!HasNaNComponent);
        }
    }
}
