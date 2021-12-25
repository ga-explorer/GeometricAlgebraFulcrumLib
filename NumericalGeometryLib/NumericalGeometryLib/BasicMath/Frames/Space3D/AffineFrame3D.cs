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
    public class AffineFrame3D
    {
        /// <summary>
        /// Create a set of 3 right-handed orthonormal direction vectors from the given vector
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="uDirection"></param>
        /// <returns></returns>
        public static AffineFrame3D CreateRightHanded(ITuple3D origin, ITuple3D uDirection)
        {
            var s = uDirection.GetLength();

            Debug.Assert(!s.IsAlmostZero());

            s = 1.0d / s;
            return new AffineFrame3D(
                origin.X, origin.Y, origin.Z,
                uDirection.X * s, uDirection.Y * s, uDirection.Z * s, 
                true
            );
        }

        /// <summary>
        /// Create a set of 3 left-handed orthonormal direction vectors from the given vector
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="uDirection"></param>
        /// <returns></returns>
        public static AffineFrame3D CreateLeftHanded(ITuple3D origin, ITuple3D uDirection)
        {
            var s = uDirection.GetLength();

            Debug.Assert(!s.IsAlmostZero());

            s = 1.0d / s;
            return new AffineFrame3D(
                origin.X, origin.Y, origin.Z,
                uDirection.X * s, uDirection.Y * s, uDirection.Z * s, 
                false
            );
        }


        public double OriginX { get; }

        public double OriginY { get; }

        public double OriginZ { get; }

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
        {
            get { return new Tuple3D(UDirectionX, UDirectionY, UDirectionZ); }
        }

        public Tuple3D VDirection
        {
            get { return new Tuple3D(VDirectionX, VDirectionY, VDirectionZ); }
        }

        public Tuple3D WDirection
        {
            get { return new Tuple3D(WDirectionX, WDirectionY, WDirectionZ); }
        }

        public bool IsRightHanded
        {
            get { return VectorAlgebraUtils.Determinant(UDirection, VDirection, WDirection) > 0.0d; }
        }

        public bool IsLeftHanded
        {
            get { return VectorAlgebraUtils.Determinant(UDirection, VDirection, WDirection) < 0.0d; }
        }

        public bool HasNaNComponent
        {
            get
            {
                return double.IsNaN(OriginX) ||
                       double.IsNaN(OriginY) ||
                       double.IsNaN(OriginZ) ||
                       double.IsNaN(UDirectionX) ||
                       double.IsNaN(UDirectionY) ||
                       double.IsNaN(UDirectionZ) ||
                       double.IsNaN(VDirectionX) ||
                       double.IsNaN(VDirectionY) ||
                       double.IsNaN(VDirectionZ) ||
                       double.IsNaN(WDirectionX) ||
                       double.IsNaN(WDirectionY) ||
                       double.IsNaN(WDirectionZ);
            }
        }


        public AffineFrame3D(ITuple3D origin, ITuple3D uDirection, ITuple3D vDirection, ITuple3D wDirection)
        {
            OriginX = origin.X;
            OriginY = origin.Y;
            OriginZ = origin.Z;

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

        private AffineFrame3D(double ox, double oy, double oz, double ux, double uy, double uz, bool rightHanded)
        {
            Debug.Assert(
                (ux * ux + uy * uy + uz * uz).IsAlmostEqual(1.0d)
            );

            OriginX = ox;
            OriginY = oy;
            OriginZ = oz;

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