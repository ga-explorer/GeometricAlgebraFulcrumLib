using System.Numerics;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space3D
{
    /// <summary>
    /// TODO: Study how to implement a TransformCache for this class
    /// This class represents a Linear map using 4x4 homogeneous matrices internally
    /// </summary>
    public sealed class AffineMap3D : 
        IAffineMap3D
    {
        private SquareMatrix4 _matrix;

        private SquareMatrix4 _invMatrix;


        public double this[int i, int j, bool useInvMatrix] 
            => useInvMatrix ? _invMatrix[i, j] : _matrix[i, j];

        public double this[int i, int j] 
            => _matrix[i, j];

        public bool IsIdentity 
            => _matrix.IsIdentity;

        public bool ContainsScaling 
            => _matrix.ContainsScaling;

        public bool SwapsHandedness 
            => _matrix.Determinant < 0;


        public AffineMap3D()
        {
            _matrix = SquareMatrix4.CreateIdentityMatrix();
            _invMatrix = SquareMatrix4.CreateIdentityMatrix();
        }

        public AffineMap3D(SquareMatrix4 matrix)
        {
            _matrix = new SquareMatrix4(matrix);
            _invMatrix = matrix.Inverse();
        }

        public AffineMap3D(SquareMatrix4 matrix, SquareMatrix4 invMatrix)
        {
            _matrix = new SquareMatrix4(matrix);
            _invMatrix = new SquareMatrix4(invMatrix);
        }


        public AffineMap3D SelfTranspose()
        {
            _matrix.SelfTranspose();
            _invMatrix.SelfTranspose();

            return this;
        }

        public AffineMap3D SelfInverse()
        {
            (_matrix, _invMatrix) = (_invMatrix, _matrix);

            return this;
        }

        public AffineMap3D SelfInverseTranspose()
        {
            (_matrix, _invMatrix) = (_invMatrix, _matrix);

            _matrix.SelfTranspose();
            _invMatrix.SelfTranspose();

            return this;
        }


        public AffineMap3D ResetToTranslation(double dx, double dy, double dz)
        {
            _matrix = SquareMatrix4.CreateTranslationMatrix3D(dx, dy, dz);
            _invMatrix = SquareMatrix4.CreateTranslationMatrix3D(-dx, -dy, -dz);

            return this;
        }

        public AffineMap3D PrependTranslation(double dx, double dy, double dz)
        {
            _matrix = SquareMatrix4.CreateTranslationMatrix3D(dx, dy, dz) * _matrix;
            _invMatrix = _invMatrix * SquareMatrix4.CreateTranslationMatrix3D(-dx, -dy, -dz);

            return this;
        }

        public AffineMap3D AppendTranslation(double dx, double dy, double dz)
        {
            _matrix = _matrix * SquareMatrix4.CreateTranslationMatrix3D(dx, dy, dz);
            _invMatrix = SquareMatrix4.CreateTranslationMatrix3D(-dx, -dy, -dz) * _invMatrix;

            return this;
        }

        public AffineMap3D ResetToScaling(double sx, double sy, double sz)
        {
            _matrix = SquareMatrix4.CreateScalingMatrix3D(sx, sy, sz);
            _invMatrix = SquareMatrix4.CreateScalingMatrix3D(1.0d / sx, 1.0d / sy, 1.0d / sz);

            return this;
        }

        public AffineMap3D PrependScaling(double sx, double sy, double sz)
        {
            _matrix = SquareMatrix4.CreateScalingMatrix3D(sx, sy, sz) * _matrix;
            _invMatrix = _invMatrix * SquareMatrix4.CreateScalingMatrix3D(1.0d / sx, 1.0d / sy, 1.0d / sz);

            return this;
        }

        public AffineMap3D AppendScaling(double sx, double sy, double sz)
        {
            _matrix = _matrix * SquareMatrix4.CreateScalingMatrix3D(sx, sy, sz);
            _invMatrix = SquareMatrix4.CreateScalingMatrix3D(1.0d / sx, 1.0d / sy, 1.0d / sz) * _invMatrix;

            return this;
        }

        public AffineMap3D ResetToScaling(double s)
        {
            _matrix = SquareMatrix4.CreateScalingMatrix3D(s);
            _invMatrix = SquareMatrix4.CreateScalingMatrix3D(1.0d / s);

            return this;
        }

        public AffineMap3D PrependScaling(double s)
        {
            _matrix = SquareMatrix4.CreateScalingMatrix3D(s) * _matrix;
            _invMatrix = _invMatrix * SquareMatrix4.CreateScalingMatrix3D(1.0d / s);

            return this;
        }

        public AffineMap3D AppendScaling(double s)
        {
            _matrix = _matrix * SquareMatrix4.CreateScalingMatrix3D(s);
            _invMatrix = SquareMatrix4.CreateScalingMatrix3D(1.0d / s) * _invMatrix;

            return this;
        }

        //TODO: Complete all other maps here

        public SquareMatrix4 ToSquareMatrix4()
        {
            return new SquareMatrix4(_matrix);
        }

        public Matrix4x4 ToMatrix4x4()
        {
            return _matrix.ToMatrix4x4();
        }

        public double[,] ToArray2D()
        {
            throw new System.NotImplementedException();
        }

        public SquareMatrix4 ToMatrix(bool useInvMatrix)
        {
            return new SquareMatrix4(useInvMatrix ? _invMatrix : _matrix);
        }

        public Tuple3D MapPoint(ITuple3D point)
        {
            var pointX = _matrix[0] * point.X + _matrix[4] * point.Y + _matrix[8] * point.Z + _matrix[12];
            var pointY = _matrix[1] * point.X + _matrix[5] * point.Y + _matrix[9] * point.Z + _matrix[13];
            var pointZ = _matrix[2] * point.X + _matrix[6] * point.Y + _matrix[10] * point.Z + _matrix[14];
            var pointW = _matrix[3] * point.X + _matrix[7] * point.Y + _matrix[11] * point.Z + _matrix[15];

            if ((pointW - 1.0d).IsAlmostZero())
                return new Tuple3D(pointX, pointY, pointZ);

            var s = 1.0d / pointW;
            return new Tuple3D(pointX * s, pointY * s, pointZ * s);
        }

        public Tuple3D MapVector(ITuple3D vector)
        {
            return new Tuple3D(
                _matrix[0] * vector.X + _matrix[4] * vector.Y + _matrix[8] * vector.Z,
                _matrix[1] * vector.X + _matrix[5] * vector.Y + _matrix[9] * vector.Z,
                _matrix[2] * vector.X + _matrix[6] * vector.Y + _matrix[10] * vector.Z
            );
        }

        public Tuple3D MapNormal(ITuple3D normal)
        {
            return new Tuple3D(
                _invMatrix[0] * normal.X + _invMatrix[1] * normal.Y + _invMatrix[2] * normal.Z,
                _invMatrix[4] * normal.X + _invMatrix[5] * normal.Y + _invMatrix[6] * normal.Z,
                _invMatrix[8] * normal.X + _invMatrix[9] * normal.Y + _invMatrix[10] * normal.Z
            );
        }

        public IAffineMap3D InverseMap()
        {
            return new AffineMap3D(_invMatrix, _matrix);
        }

        //TODO: Implement equality tests and IsIdentity
        public bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}
