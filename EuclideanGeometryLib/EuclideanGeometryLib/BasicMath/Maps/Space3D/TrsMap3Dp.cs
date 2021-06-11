using System;
using System.Diagnostics;
using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.BasicMath.Maps.Space3D
{
    //TODO: Review and integrate this class
    /// <summary>
    /// Represents a Stretch then Rotate then Translate map
    /// </summary>
    public sealed class TrsMap3Dp : IAffineMap3D
    {
        

        public static TrsMap3Dp CreateFromMatrix(Matrix4X4 c)
        {
            var newMap = new TrsMap3Dp();

            //Extract translation part
            newMap.TranslationVector = new Tuple3D(c[3], c[7], c[11]);

            //Separate upper left 3x3 block into rotation of stretch
            var m = c.UpperLeftBlock3X3;
            var r = m.NearestOrthogonalMatrix();
            newMap.StretchMatrix = r.Transpose() * m;

            return newMap;
        }

        //TODO: Implement this using GA and compare with classical method

        /// <summary>
        /// The translation vector
        /// </summary>
        public Tuple3D TranslationVector { get; set; }

        /// <summary>
        /// The direction of the rotation axis; this must be a unit vector
        /// </summary>
        public Tuple3D RotationVector { get; set; }

        /// <summary>
        /// The angle of the rotation
        /// </summary>
        public double RotationAngle { get; set; }

        /// <summary>
        /// The symmetric stretch matrix
        /// </summary>
        public Matrix3X3 StretchMatrix { get; set; }

        //TODO: Complete this and compare with GMac
        public Matrix3X3 RotationMatrix
        {
            get
            {
                var qr = Math.Cos(RotationAngle);
                var sinAngle = Math.Sin(RotationAngle);
                var qi = RotationVector.X * sinAngle;
                var qj = RotationVector.Y * sinAngle;
                var qk = RotationVector.Z * sinAngle;

                return new Matrix3X3()
                {
                    [0] = 1.0d - 2.0d * (qj * qj + qk * qk),
                    [1] = 2.0d * (qi * qj - qk * qr),
                    [2] = 2.0d * (qi * qk + qj * qr),

                    [3] = 2.0d * (qi * qj + qk * qr),
                    [4] = 1.0d - 2.0d * (qi * qi + qk * qk),
                    [5] = 2.0d * (qj * qk - qi * qr),

                    [6] = 2.0d * (qi * qk - qj* qr),
                    [7] = 2.0d * (qj * qk + qi * qr),
                    [8] = 1.0d - 2.0d * (qi * qi + qj * qj)
                };
            }
        }

        public bool SwapsHandedness { get; }

        public Matrix4X4 ToMatrix()
        {
            Debug.Assert(RotationVector.IsUnitVector());

            return new Matrix4X4
            {
                UpperLeftBlock3X3 = RotationMatrix * StretchMatrix,
                UpperRightBlock3X1 = TranslationVector,
                [15] = 1.0d
            };
        }

        public ITuple3D MapPoint(ITuple3D point)
        {
            throw new NotImplementedException();
        }

        public ITuple3D MapVector(ITuple3D point)
        {
            throw new NotImplementedException();
        }

        public ITuple3D MapNormal(ITuple3D normal)
        {
            throw new NotImplementedException();
        }

        public IAffineMap3D InverseMap()
        {
            throw new NotImplementedException();
        }
    }
}
