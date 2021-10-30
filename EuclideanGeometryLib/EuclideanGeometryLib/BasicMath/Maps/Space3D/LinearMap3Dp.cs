using System.Numerics;
using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.BasicMath.Maps.Space3D
{
    /// <summary>
    /// TODO: Study how to implement a TransformCache for this class
    /// This class represents a Linear map using 4x4 homogeneous matrices internally
    /// </summary>
    public sealed class LinearMap3Dp : IAffineMap3D
    {
        private AffineMapMatrix4X4 _matrix;

        private AffineMapMatrix4X4 _invMatrix;


        public double this[int i, int j, bool useInvMatrix] =>
            useInvMatrix ? _invMatrix[i, j] : _matrix[i, j];

        public double this[int i, int j] =>
            _matrix[i, j];

        public bool IsIdentity =>
            _matrix.IsIdentity;

        public bool ContainsScaling
            => _matrix.ContainsScaling;

        public bool SwapsHandedness
            => _matrix.Determinant < 0;


        public LinearMap3Dp()
        {
            _matrix = AffineMapMatrix4X4.CreateIdentityMatrix();
            _invMatrix = AffineMapMatrix4X4.CreateIdentityMatrix();
        }

        public LinearMap3Dp(AffineMapMatrix4X4 matrix)
        {
            _matrix = new AffineMapMatrix4X4(matrix);
            _invMatrix = matrix.Inverse();
        }

        public LinearMap3Dp(AffineMapMatrix4X4 matrix, AffineMapMatrix4X4 invMatrix)
        {
            _matrix = new AffineMapMatrix4X4(matrix);
            _invMatrix = new AffineMapMatrix4X4(invMatrix);
        }


        public LinearMap3Dp SelfTranspose()
        {
            _matrix.SelfTranspose();
            _invMatrix.SelfTranspose();

            return this;
        }

        public LinearMap3Dp SelfInverse()
        {
            (_matrix, _invMatrix) = (_invMatrix, _matrix);

            return this;
        }

        public LinearMap3Dp SelfInverseTranspose()
        {
            (_matrix, _invMatrix) = (_invMatrix, _matrix);

            _matrix.SelfTranspose();
            _invMatrix.SelfTranspose();

            return this;
        }


        public LinearMap3Dp ResetToTranslation(double dx, double dy, double dz)
        {
            _matrix = AffineMapMatrix4X4.CreateTranslationMatrix(dx, dy, dz);
            _invMatrix = AffineMapMatrix4X4.CreateTranslationMatrix(-dx, -dy, -dz);

            return this;
        }

        public LinearMap3Dp PrependTranslation(double dx, double dy, double dz)
        {
            _matrix = AffineMapMatrix4X4.CreateTranslationMatrix(dx, dy, dz) * _matrix;
            _invMatrix = _invMatrix * AffineMapMatrix4X4.CreateTranslationMatrix(-dx, -dy, -dz);

            return this;
        }

        public LinearMap3Dp AppendTranslation(double dx, double dy, double dz)
        {
            _matrix = _matrix * AffineMapMatrix4X4.CreateTranslationMatrix(dx, dy, dz);
            _invMatrix = AffineMapMatrix4X4.CreateTranslationMatrix(-dx, -dy, -dz) * _invMatrix;

            return this;
        }

        public LinearMap3Dp ResetToScaling(double sx, double sy, double sz)
        {
            _matrix = AffineMapMatrix4X4.CreateScalingMatrix(sx, sy, sz);
            _invMatrix = AffineMapMatrix4X4.CreateScalingMatrix(1.0d / sx, 1.0d / sy, 1.0d / sz);

            return this;
        }

        public LinearMap3Dp PrependScaling(double sx, double sy, double sz)
        {
            _matrix = AffineMapMatrix4X4.CreateScalingMatrix(sx, sy, sz) * _matrix;
            _invMatrix = _invMatrix * AffineMapMatrix4X4.CreateScalingMatrix(1.0d / sx, 1.0d / sy, 1.0d / sz);

            return this;
        }

        public LinearMap3Dp AppendScaling(double sx, double sy, double sz)
        {
            _matrix = _matrix * AffineMapMatrix4X4.CreateScalingMatrix(sx, sy, sz);
            _invMatrix = AffineMapMatrix4X4.CreateScalingMatrix(1.0d / sx, 1.0d / sy, 1.0d / sz) * _invMatrix;

            return this;
        }

        public LinearMap3Dp ResetToScaling(double s)
        {
            _matrix = AffineMapMatrix4X4.CreateScalingMatrix(s);
            _invMatrix = AffineMapMatrix4X4.CreateScalingMatrix(1.0d / s);

            return this;
        }

        public LinearMap3Dp PrependScaling(double s)
        {
            _matrix = AffineMapMatrix4X4.CreateScalingMatrix(s) * _matrix;
            _invMatrix = _invMatrix * AffineMapMatrix4X4.CreateScalingMatrix(1.0d / s);

            return this;
        }

        public LinearMap3Dp AppendScaling(double s)
        {
            _matrix = _matrix * AffineMapMatrix4X4.CreateScalingMatrix(s);
            _invMatrix = AffineMapMatrix4X4.CreateScalingMatrix(1.0d / s) * _invMatrix;

            return this;
        }

        //TODO: Complete all other maps here

        public AffineMapMatrix4X4 ToMatrix()
        {
            return new AffineMapMatrix4X4(_matrix);
        }

        public Matrix4x4 ToSystemNumericsMatrix()
        {
            return _matrix.ToSystemNumericsMatrix();
        }

        public double[,] ToArray2D()
        {
            throw new System.NotImplementedException();
        }

        public AffineMapMatrix4X4 ToMatrix(bool useInvMatrix)
        {
            return new AffineMapMatrix4X4(useInvMatrix ? _invMatrix : _matrix);
        }

        public ITuple3D MapPoint(ITuple3D point)
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

        public ITuple3D MapVector(ITuple3D vector)
        {
            return new Tuple3D(
                _matrix[0] * vector.X + _matrix[4] * vector.Y + _matrix[8] * vector.Z,
                _matrix[1] * vector.X + _matrix[5] * vector.Y + _matrix[9] * vector.Z,
                _matrix[2] * vector.X + _matrix[6] * vector.Y + _matrix[10] * vector.Z
            );
        }

        public ITuple3D MapNormal(ITuple3D normal)
        {
            return new Tuple3D(
                _invMatrix[0] * normal.X + _invMatrix[1] * normal.Y + _invMatrix[2] * normal.Z,
                _invMatrix[4] * normal.X + _invMatrix[5] * normal.Y + _invMatrix[6] * normal.Z,
                _invMatrix[8] * normal.X + _invMatrix[9] * normal.Y + _invMatrix[10] * normal.Z
            );
        }

        public IAffineMap3D InverseMap()
        {
            return new LinearMap3Dp(_invMatrix, _matrix);
        }

        //TODO: Implement equality tests and IsIdentity
    }
}
