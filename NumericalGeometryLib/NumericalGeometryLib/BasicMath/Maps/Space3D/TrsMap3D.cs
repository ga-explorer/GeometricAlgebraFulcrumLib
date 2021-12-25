using System;
using System.Diagnostics;
using System.Numerics;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space3D
{
    //TODO: Review and integrate this class
    /// <summary>
    /// Represents a Stretch then Rotate then Translate map
    /// </summary>
    public sealed class TrsMap3D : IAffineMap3D
    {
        

        public static TrsMap3D CreateFromMatrix(SquareMatrix4 c)
        {
            var newMap = new TrsMap3D();

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
        public SquareMatrix3 StretchMatrix { get; set; }

        //TODO: Complete this and compare with GMac
        public SquareMatrix3 RotationMatrix
        {
            get
            {
                var qr = Math.Cos(RotationAngle);
                var sinAngle = Math.Sin(RotationAngle);
                var qi = RotationVector.X * sinAngle;
                var qj = RotationVector.Y * sinAngle;
                var qk = RotationVector.Z * sinAngle;

                return new SquareMatrix3()
                {
                    Scalar00 = 1.0d - 2.0d * (qj * qj + qk * qk),
                    Scalar10 = 2.0d * (qi * qj - qk * qr),
                    Scalar20 = 2.0d * (qi * qk + qj * qr),

                    Scalar01 = 2.0d * (qi * qj + qk * qr),
                    Scalar11 = 1.0d - 2.0d * (qi * qi + qk * qk),
                    Scalar21 = 2.0d * (qj * qk - qi * qr),

                    Scalar02 = 2.0d * (qi * qk - qj* qr),
                    Scalar12 = 2.0d * (qj * qk + qi * qr),
                    Scalar22 = 1.0d - 2.0d * (qi * qi + qj * qj)
                };
            }
        }

        public bool SwapsHandedness { get; }

        public SquareMatrix4 ToSquareMatrix4()
        {
            Debug.Assert(RotationVector.IsUnitVector());

            return new SquareMatrix4
            {
                UpperLeftBlock3X3 = RotationMatrix * StretchMatrix,
                UpperRightBlock3X1 = TranslationVector,
                [15] = 1.0d
            };
        }

        public Matrix4x4 ToMatrix4x4()
        {
            return ToSquareMatrix4().ToMatrix4x4();
        }

        public double[,] ToArray2D()
        {
            throw new NotImplementedException();
        }

        public Tuple3D MapPoint(ITuple3D point)
        {
            throw new NotImplementedException();
        }

        public Tuple3D MapVector(ITuple3D point)
        {
            throw new NotImplementedException();
        }

        public Tuple3D MapNormal(ITuple3D normal)
        {
            throw new NotImplementedException();
        }

        public IAffineMap3D InverseMap()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
