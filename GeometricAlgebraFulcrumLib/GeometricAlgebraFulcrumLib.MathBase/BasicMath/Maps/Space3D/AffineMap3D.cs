using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D
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
            => _matrix.Determinant.IsNegative();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineMap3D()
        {
            _matrix = SquareMatrix4.CreateIdentityMatrix();
            _invMatrix = SquareMatrix4.CreateIdentityMatrix();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineMap3D(SquareMatrix4 matrix)
        {
            _matrix = new SquareMatrix4(matrix);
            _invMatrix = matrix.Inverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineMap3D(SquareMatrix4 matrix, SquareMatrix4 invMatrix)
        {
            _matrix = new SquareMatrix4(matrix);
            _invMatrix = new SquareMatrix4(invMatrix);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineMap3D SelfTranspose()
        {
            _matrix.SelfTranspose();
            _invMatrix.SelfTranspose();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineMap3D SelfInverse()
        {
            (_matrix, _invMatrix) = (_invMatrix, _matrix);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineMap3D SelfInverseTranspose()
        {
            (_matrix, _invMatrix) = (_invMatrix, _matrix);

            _matrix.SelfTranspose();
            _invMatrix.SelfTranspose();

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineMap3D ResetToTranslation(double dx, double dy, double dz)
        {
            _matrix = SquareMatrix4.CreateTranslationMatrix3D(dx, dy, dz);
            _invMatrix = SquareMatrix4.CreateTranslationMatrix3D(-dx, -dy, -dz);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineMap3D PrependTranslation(double dx, double dy, double dz)
        {
            _matrix = SquareMatrix4.CreateTranslationMatrix3D(dx, dy, dz) * _matrix;
            _invMatrix = _invMatrix * SquareMatrix4.CreateTranslationMatrix3D(-dx, -dy, -dz);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineMap3D AppendTranslation(double dx, double dy, double dz)
        {
            _matrix = _matrix * SquareMatrix4.CreateTranslationMatrix3D(dx, dy, dz);
            _invMatrix = SquareMatrix4.CreateTranslationMatrix3D(-dx, -dy, -dz) * _invMatrix;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineMap3D ResetToScaling(double sx, double sy, double sz)
        {
            _matrix = SquareMatrix4.CreateScalingMatrix3D(sx, sy, sz);
            _invMatrix = SquareMatrix4.CreateScalingMatrix3D(1.0d / sx, 1.0d / sy, 1.0d / sz);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineMap3D PrependScaling(double sx, double sy, double sz)
        {
            _matrix = SquareMatrix4.CreateScalingMatrix3D(sx, sy, sz) * _matrix;
            _invMatrix = _invMatrix * SquareMatrix4.CreateScalingMatrix3D(1.0d / sx, 1.0d / sy, 1.0d / sz);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineMap3D AppendScaling(double sx, double sy, double sz)
        {
            _matrix = _matrix * SquareMatrix4.CreateScalingMatrix3D(sx, sy, sz);
            _invMatrix = SquareMatrix4.CreateScalingMatrix3D(1.0d / sx, 1.0d / sy, 1.0d / sz) * _invMatrix;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineMap3D ResetToScaling(double s)
        {
            _matrix = SquareMatrix4.CreateScalingMatrix3D(s);
            _invMatrix = SquareMatrix4.CreateScalingMatrix3D(1.0d / s);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineMap3D PrependScaling(double s)
        {
            _matrix = SquareMatrix4.CreateScalingMatrix3D(s) * _matrix;
            _invMatrix = _invMatrix * SquareMatrix4.CreateScalingMatrix3D(1.0d / s);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineMap3D AppendScaling(double s)
        {
            _matrix = _matrix * SquareMatrix4.CreateScalingMatrix3D(s);
            _invMatrix = SquareMatrix4.CreateScalingMatrix3D(1.0d / s) * _invMatrix;

            return this;
        }

        //TODO: Complete all other maps here
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SquareMatrix4 GetSquareMatrix4()
        {
            return new SquareMatrix4(_matrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix4x4 GetMatrix4x4()
        {
            return _matrix.GetMatrix4x4();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[,] GetArray2D()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SquareMatrix4 ToMatrix(bool useInvMatrix)
        {
            return new SquareMatrix4(useInvMatrix ? _invMatrix : _matrix);
        }

        public Float64Vector3D MapPoint(IFloat64Tuple3D point)
        {
            var pointX = _matrix.Scalar00 * point.X + _matrix.Scalar01 * point.Y + _matrix.Scalar02 * point.Z + _matrix.Scalar03;
            var pointY = _matrix.Scalar10 * point.X + _matrix.Scalar11 * point.Y + _matrix.Scalar12 * point.Z + _matrix.Scalar13;
            var pointZ = _matrix.Scalar20 * point.X + _matrix.Scalar21 * point.Y + _matrix.Scalar22 * point.Z + _matrix.Scalar23;
            var pointW = _matrix.Scalar30 * point.X + _matrix.Scalar31 * point.Y + _matrix.Scalar32 * point.Z + _matrix.Scalar33;

            if (pointW.IsOne())
                return Float64Vector3D.Create(pointX, pointY, pointZ);

            var s = 1.0d / pointW;
            return Float64Vector3D.Create(pointX * s, pointY * s, pointZ * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D MapVector(IFloat64Tuple3D vector)
        {
            return Float64Vector3D.Create(_matrix.Scalar00 * vector.X + _matrix.Scalar01 * vector.Y + _matrix.Scalar02 * vector.Z,
                _matrix.Scalar10 * vector.X + _matrix.Scalar11 * vector.Y + _matrix.Scalar12 * vector.Z,
                _matrix.Scalar20 * vector.X + _matrix.Scalar21 * vector.Y + _matrix.Scalar22 * vector.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D MapNormal(IFloat64Tuple3D normal)
        {
            return Float64Vector3D.Create(_invMatrix.Scalar00 * normal.X + _invMatrix.Scalar10 * normal.Y + _invMatrix.Scalar20 * normal.Z,
                _invMatrix.Scalar01 * normal.X + _invMatrix.Scalar11 * normal.Y + _invMatrix.Scalar21 * normal.Z,
                _invMatrix.Scalar02 * normal.X + _invMatrix.Scalar12 * normal.Y + _invMatrix.Scalar22 * normal.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IAffineMap3D GetInverseAffineMap()
        {
            return new AffineMap3D(_invMatrix, _matrix);
        }

        //TODO: Implement equality tests and IsIdentity
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
